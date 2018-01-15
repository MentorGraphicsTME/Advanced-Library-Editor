Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Drawing

Public Class frmBOMCoverage

    'Events
    Event eUpdateStatus(ByVal status As String)
    Event eReadComplete()
    Event eLogComplete()

    'String
    Dim s_BOMfile As String = Nothing
    Dim PN_Col As String

    'Object
    Dim xlsApp As Excel.Application
    Dim xlsBook As Excel.Workbook
    Dim xlsSheet As Excel.Worksheet = Nothing

    'Boolean
    Dim bCloseExcel As Boolean = False

    'Delegates
    Delegate Sub d_AnalysisComplete()
    Delegate Sub d_UpdateStatus(ByVal status As String)
    Delegate Sub d_IntSetText(ByVal count As Integer)
    Delegate Sub d_DoubleSetText(ByVal percent As Double)
    Delegate Sub d_LogComplete()


    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then
            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
            frmMain.NotifyIcon.ShowBalloonTip(2000, "BOM Coverage: ", status, ToolTipIcon.Info)
        End If
    End Sub

    Private Sub ReadComplete()
        If Me.InvokeRequired Then
            Dim d As New d_AnalysisComplete(AddressOf ReadComplete)
            Me.Invoke(d)
        Else

            WaitGif.Enabled = False
            ts_Status.Text = "BOM Coverage Read is complete..."

        End If
    End Sub

    Private Sub LogComplete()
        If Me.InvokeRequired Then
            Dim d As New d_LogComplete(AddressOf LogComplete)
            Me.Invoke(d)
        Else
            Dim replySymbol As DialogResult = MessageBox.Show("BOM Coverage Report log has been created. Would you like to view the results?", "Finished",
        MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

            If replySymbol = DialogResult.Yes Then
                frmMain.OpenLogFile("BOM Coverage Report")
            Else
                MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "BOM Coverage Report.log")
            End If
        End If
    End Sub

    Private Sub ReadCount(ByVal count As Integer)
        If Me.lblPartsRead.InvokeRequired Then
            Dim d As New d_IntSetText(AddressOf ReadCount)
            Me.Invoke(d, New Object() {[count]})
        Else
            Me.lblPartsRead.Text = count
        End If
    End Sub

    Private Sub FoundCount(ByVal count As Integer)
        If Me.lblPartsFound.InvokeRequired Then
            Dim d As New d_IntSetText(AddressOf FoundCount)
            Me.Invoke(d, New Object() {[count]})
        Else
            Me.lblPartsFound.Text = count
        End If
    End Sub

    Private Sub PercentCoverage(ByVal percent As Double)
        If Me.lblPercentCoverage.InvokeRequired Then
            Dim d As New d_DoubleSetText(AddressOf PercentCoverage)
            Me.Invoke(d, New Object() {percent})
        Else
            Me.lblPercentCoverage.Text = percent & "%"
        End If
    End Sub


    Private Sub btn_Read_Click(sender As System.Object, e As System.EventArgs) Handles btnEvaluate.Click

        If tbox_Input.Text = "" Or IsNothing(tbox_Input.Text) Then
            MessageBox.Show("Please choose an input file.")
            Exit Sub
        End If

        PN_Col = PN_cbox.Text

        pl_Excel.Enabled = False

        btnEvaluate.Enabled = False

        s_BOMfile = tbox_Input.Text

        WaitGif.Enabled = True

        Dim t_ReadInputFile As Thread = New Threading.Thread(AddressOf Read_Log)
        t_ReadInputFile.IsBackground = True
        t_ReadInputFile.Start()

    End Sub

    Private Sub Read_Log()
        'The purpose of this function is to read the file, whether it be a text(comma-delmited), .csv, or excel
        'spreadsheet. After reading the contents, it will placed the parsed part numbers into a list of string
        'values. Then various function and subroutines are called in order to compare and generate found and
        'not found lists. The values are then logged into the log viewer.

        Dim Read_Parts As List(Of String) = New List(Of String) 'List of read part numbers
        Dim noDup_ReadParts As List(Of String) = New List(Of String) 'List of part numbers without duplicates

        If (Path.GetExtension(s_BOMfile) = ".xls") Or (Path.GetExtension(s_BOMfile) = ".xlsx") Then
            'The assumption is that each row is filled in within the part number column and
            'there are no gaps between each row.

            Dim i As Integer
            xlsSheet.Select()
            If chkbox_IgnoreHeader.Checked = True Then
                i = 2
            ElseIf chkbox_IgnoreHeader.Checked = False Then
                i = 1
            End If

            Do While Not IsNothing(xlsSheet.Range(PN_Col & i).Value)

                If IsNothing(xlsSheet.Range(PN_Col & i).Value) Then
                    Exit Do
                End If

                Dim PartNumber As String = xlsSheet.Range(PN_Col & i).Value
                PartNumber = PartNumber.Trim()
                Read_Parts.Add(PartNumber)

                i += 1
            Loop

            noDup_ReadParts = Read_Parts.Distinct.ToList()
            ReadCount(noDup_ReadParts.Count)
            Marshal.FinalReleaseComObject(xlsSheet)


        ElseIf Path.GetExtension(s_BOMfile) = ".txt" Or Path.GetExtension(s_BOMfile) = ".csv" Then

            Dim arFile As String() = File.ReadAllLines(s_BOMfile)
            Dim line As String

            For Each line In arFile
                Dim linesplit As String()

                If line.Contains(",") Then
                    linesplit = Regex.Split(line, ",")
                ElseIf IsNothing(line) Then
                    Continue For
                Else
                    Read_Parts.Add(line)
                    Continue For
                End If

                If linesplit.Length > 0 And Not (LCase(linesplit(0)).Contains("part number")) Then
                    Read_Parts.Add(linesplit(0).Trim) ' Add each part number to to the part nunmber list.
                End If
            Next

            noDup_ReadParts = Read_Parts.Distinct.ToList()
            ReadCount(noDup_ReadParts.Count)

        End If

        BOMComparison(noDup_ReadParts)  'Compare the BOM and the library
        RaiseEvent eReadComplete() 'Notify the user that the read is complete
        Thread.Sleep(2000) 'Wait two seconds
        RaiseEvent eUpdateStatus("BOM Coverage Report log being created ...") 'Update the user that logging is beginning
        LogBOMCoverage(FoundList(noDup_ReadParts), NotFoundList(noDup_ReadParts)) 'Create the log file
        RaiseEvent eLogComplete() 'Log is Complete
        RaiseEvent eUpdateStatus("BOM Coverage Report Complete ...") 'Update the user that logging is complete

    End Sub

    Private Sub LogBOMCoverage(ByVal PartsFoundList As List(Of String), ByVal PartsNotFoundList As List(Of String))
        'Log the parts that were found and the parts that weren't found and export them
        'to the "BOM Coverage Report.log" file

        Using writera As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "BOM Coverage Report.log", True, System.Text.Encoding.ASCII)
            'Write the name of the input file and organize the log file to write the part numbers found

            writera.WriteLine("BOM Coverage Report for " & s_BOMfile & vbNewLine)
            writera.WriteLine("Parts Found:")
            For Each sFound_Part As String In PartsFoundList
                writera.WriteLine(vbTab & sFound_Part)
            Next
            Dim i As Integer
            For i = 0 To 40
                writera.Write("-")
            Next
            writera.WriteLine(vbNewLine & "Parts Not Found:")
            For Each sNotFound_Part As String In PartsNotFoundList
                writera.WriteLine(vbTab & sNotFound_Part)
            Next
        End Using

    End Sub


    Private Sub BOMComparison(ByVal Partlist As List(Of String))
        'Compare each individual part in the part list read from the file and figure out
        'how many parts in the library were found to be in the passed in list of parts.

        Dim iPartsFound As Integer = 0
        For Each lib_Part As String In frmMain.librarydata.PartList.Keys
            For Each file_Part As String In Partlist
                If lib_Part = file_Part Then
                    iPartsFound += 1
                End If
            Next
        Next

        FoundCount(iPartsFound) 'Update the number of parts found
        Dim percent As Double = Math.Round((iPartsFound / Partlist.Count) * 100.0, 2)
        PercentCoverage(percent) 'Update the percentage found vs not found

    End Sub

    Private Function FoundList(ByVal PartList As List(Of String)) As List(Of String)
        Dim p_List = New List(Of String)

        For Each lib_Part As String In frmMain.librarydata.PartList.Keys
            For Each file_Part As String In PartList
                If lib_Part = file_Part Then
                    p_List.Add(lib_Part)
                End If
            Next
        Next
        Return p_List
    End Function

    Private Function NotFoundList(ByVal PartList As List(Of String)) As List(Of String)

        Dim q_List = New List(Of String)

        For Each part As String In PartList
            If Not frmMain.librarydata.PartList.ContainsKey(part) Then
                q_List.Add(part)
            End If
        Next
        Return q_List
    End Function

    Private Sub frmBOMCoverage_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        If File.Exists(frmMain.librarydata.LogPath & "BOM Coverage Report.log") Then
            File.Delete(frmMain.librarydata.LogPath & "BOM Coverage Report.log")
        End If

        AddHandler eReadComplete, AddressOf ReadComplete
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eLogComplete, AddressOf LogComplete

    End Sub

    Private Sub frmBOMCoverage_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        If bCloseExcel = True Then

            GC.Collect()
            GC.WaitForPendingFinalizers()

            xlsBook.Close()
            Marshal.FinalReleaseComObject(xlsBook)

            xlsApp.Quit()
            Marshal.FinalReleaseComObject(xlsApp)
        End If

        xlsBook = Nothing
        xlsApp = Nothing
    End Sub


    Private Sub cboxActiveSheet_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboxActiveSheet.SelectedIndexChanged
        If Not cboxActiveSheet.SelectedIndex = -1 Then
            xlsSheet = xlsBook.Sheets(cboxActiveSheet.Text)
            xlsSheet.Select()
        End If
    End Sub


    Private Sub btn_Browse_BOMCoverageClick(sender As System.Object, e As System.EventArgs) Handles btn_Browse_SwapPN.Click

        Using ofd As New OpenFileDialog
            ofd.Filter = "Text files (*.txt)|*.txt|Excel Files|*.xlsx;*.xls;*.csv"
            '            ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                s_BOMfile = ofd.FileName
                tbox_Input.Text = ofd.FileName

                ReadCount(0)
                FoundCount(0)
                PercentCoverage(0)

                Dim ext As String = Path.GetExtension(tbox_Input.Text)

                If (ext = ".xls" Or ext = ".xlsx") Then

                    bCloseExcel = True

                    pl_Excel.Enabled = True

                    ts_Status.Text = "Opening spreadsheet..."

                    Me.Refresh()

                    xlsApp = New Excel.Application

                    xlsApp.Visible = True

                    xlsApp.Workbooks.Open(ofd.FileName)

                    xlsBook = xlsApp.ActiveWorkbook

                    For Each sheet In xlsBook.Worksheets

                        cboxActiveSheet.Items.Add(sheet.name)

                    Next

                    xlsSheet = xlsBook.ActiveSheet

                    If xlsBook.Worksheets.Count = 1 Then

                        cboxActiveSheet.SelectedIndex = 0

                    Else

                        cboxActiveSheet.Text = xlsSheet.Name

                    End If

                    ts_Status.Text = String.Empty
                    btnEvaluate.Enabled = True

                ElseIf (ext = ".txt" Or ext = ".csv") Then
                    pl_Excel.Enabled = True
                    chkbox_IgnoreHeader.Enabled = False
                    cboxActiveSheet.Enabled = False
                    PN_cbox.Enabled = False
                    btnEvaluate.Enabled = True
                    lbl_PartNumber.Enabled = False
                    lbl_Worksheet.Enabled = False

                End If
            End If

        End Using
    End Sub

End Class