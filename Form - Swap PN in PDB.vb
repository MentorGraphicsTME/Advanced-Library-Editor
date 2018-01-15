Imports System.IO
Imports System.Text
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Drawing
Public Class frmSwapPart

    'Events
    Event eUpdateComplete(Errors As Boolean, Warnings As Boolean)
    Event eUpdateStatus(status As String)
    Event eAnalysisComplete()

    'String
    Dim s_SwapFile As String = Nothing
    Dim Before_Col As String
    Dim After_Col As String

    ''Object
    Dim xlsApp As Excel.Application
    Dim xlsBook As Excel.Workbook
    Dim xlsSheet As Excel.Worksheet = Nothing

    'Dictionary
    Dim dic_PNSwap As New Dictionary(Of String, String)
    'Dim dicPartList As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    'Boolean
    Dim b_UpdatePN As Boolean = False
    Dim b_UpdateLabel As Boolean = False
    Dim b_UpdateName As Boolean = False
    Dim bCloseExcel As Boolean = False

    'Delegates
    Delegate Sub d_UpdateComplete(ByVal Errors As Boolean, ByVal Warnings As Boolean)
    Delegate Sub d_UpdateStatus(ByVal status As String)
    Delegate Sub d_AnalysisComplete()
    Delegate Sub d_Increment()

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click
        If File.Exists(frmMain.librarydata.LogPath & "\Rename Parts.log") Then

            File.Delete(frmMain.librarydata.LogPath & "\Rename Parts.log")

        End If

        WaitGif.Enabled = True

        b_UpdateLabel = chkbox_Label.Checked
        b_UpdateName = chkbox_Name.Checked
        b_UpdatePN = chkbox_Number.Checked

        chkbox_Label.Enabled = False
        chkbox_Name.Enabled = False
        chkbox_Number.Enabled = False
        GroupBox1.Enabled = False

        ts_Status.Text = "Starting update process..."
        frmMain.NotifyIcon.ShowBalloonTip(2000, "Rename Parts:", "Starting update process...", ToolTipIcon.Info)

        Dim t_SwapParts As Thread = New Threading.Thread(AddressOf SwapParts)
        t_SwapParts.IsBackground = True
        t_SwapParts.Start()

    End Sub

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub btn_Eval_Click(sender As System.Object, e As System.EventArgs) Handles btnEvaluate.Click

        Dim ext As String = Path.GetExtension(tbox_Input.Text)

        If (ext = ".xls" Or ext = ".xlsx") And (cbox_Before.SelectedIndex = -1 Or cbox_After.SelectedIndex = -1) Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Rename Parts:", "Please select a before and after column before proceeding...", ToolTipIcon.Error)
            MessageBox.Show("Please select a before and after column before proceeding...")
            Exit Sub
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Rename Parts - Bad Data.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Rename Parts - Bad Data.log")
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Rename Parts - Input.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Rename Parts - Input.log")
        End If

        Before_Col = cbox_Before.Text
        After_Col = cbox_After.Text

        pl_Excel.Enabled = False

        btnEvaluate.Enabled = False

        s_SwapFile = tbox_Input.Text

        dgvPNSwap.Rows.Clear()

        frmMain.NotifyIcon.ShowBalloonTip(2000, "Rename Parts:", "Reading input file...", ToolTipIcon.Info)
        ts_Status.Text = "Reading input file..."

        WaitGif.Enabled = True

        Dim t_ReadSwapFile As Thread = New Threading.Thread(AddressOf ReadSwapFile)
        t_ReadSwapFile.IsBackground = True
        t_ReadSwapFile.Start()

    End Sub

    Private Sub ReadSwapFile()

        dic_PNSwap.Clear()

        If (Path.GetExtension(s_SwapFile) = ".xls") Or (Path.GetExtension(s_SwapFile) = ".xlsx") Then

            bCloseExcel = True

            xlsApp = New Excel.Application

            xlsApp.Visible = True

            xlsBook = xlsApp.Workbooks.Open(s_SwapFile)

            For Each xls_Sheet As Excel.Worksheet In xlsBook.Worksheets

                xls_Sheet.Select()

                Dim i As Integer = 1

                If chkbox_IgnoreHeader.Checked = True Then
                    i = 2
                End If

                Do While Not IsNothing(xls_Sheet.Range(Before_Col & i).Value)

                    RaiseEvent eUpdateStatus("Reading " & xls_Sheet.Name & " row: " & i)

                    Dim sBefore As String
                    Dim sAfter As String

                    sBefore = xls_Sheet.Range(Before_Col & i).Value

                    sBefore = sBefore.Trim()

                    sAfter = xls_Sheet.Range(After_Col & i).Value

                    sAfter = sAfter.Trim()

                    If Not dic_PNSwap.ContainsKey(sBefore) Then dic_PNSwap.Add(sBefore, sAfter)

                    i += 1

                Loop

                Marshal.FinalReleaseComObject(xls_Sheet)

            Next

        ElseIf Path.GetExtension(s_SwapFile) = ".txt" Then

            Dim arFile As String() = File.ReadAllLines(s_SwapFile)

            Dim line As String
            For Each line In arFile
                Dim linesplit As String()

                If line.Contains(",") Then
                    linesplit = Regex.Split(line, ",")
                ElseIf line.Contains(";") Then
                    linesplit = Regex.Split(line, ";")
                Else
                    linesplit = Regex.Split(line, "\s+")
                End If

                If linesplit.Length = 2 Then

                    If Not dic_PNSwap.ContainsKey(linesplit(0)) Then dic_PNSwap.Add(linesplit(0), linesplit(1))

                End If

            Next

        End If

        RaiseEvent eAnalysisComplete()
    End Sub

    Private Sub AnalysisComplete()
        If Me.InvokeRequired Then

            Dim d As New d_AnalysisComplete(AddressOf AnalysisComplete)
            Me.Invoke(d)
        Else

            UpdateStatus("Analyzing results...")
            Refresh()

            Dim dic_Duplicates As New Dictionary(Of String, List(Of String))
            Dim dic_Missing As New Dictionary(Of String, String)

            Dim dic_TempDictionary As New Dictionary(Of String, String)

            Dim iDup As Integer = 0
            Dim iNoChange As Integer = 0
            Dim iMissing As Integer = 0

            For Each kvp As KeyValuePair(Of String, String) In dic_PNSwap
                Dim i As Integer = dgvPNSwap.Rows.Add(kvp.Key, kvp.Value)
                If Not kvp.Key = kvp.Value Then
                    If Not frmMain.librarydata.PartList.ContainsKey(kvp.Key) Then
                        dgvPNSwap.Rows(dgvPNSwap.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Red
                        iMissing += 1

                        dic_Missing.Item(kvp.Key) = kvp.Value

                    Else
                        If dic_TempDictionary.ContainsKey(kvp.Key) Then
                            dgvPNSwap.Rows(dgvPNSwap.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Orange
                            iDup += 1

                            Dim l_Cells As List(Of String)
                            If dic_Duplicates.ContainsKey(kvp.Key) Then
                                l_Cells = dic_Duplicates.Item(kvp.Key)
                            Else
                                l_Cells = New List(Of String)
                            End If

                            If Not l_Cells.Contains(kvp.Value) Then
                                l_Cells.Add(kvp.Value)
                                dic_Duplicates.Item(kvp.Key) = l_Cells
                            End If

                        Else
                            dic_TempDictionary.Add(kvp.Key, kvp.Value)
                        End If
                    End If
                Else
                    dgvPNSwap.Rows(dgvPNSwap.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                    iNoChange += 1
                End If
                dgvPNSwap.Refresh()
                dgvPNSwap.FirstDisplayedScrollingRowIndex = i
            Next

            dic_PNSwap = dic_TempDictionary
            dic_TempDictionary = Nothing

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts - Bad Data.log", True)

                writer.WriteLine("Duplicated Input Values:")
                For Each kvp_Duplicates As KeyValuePair(Of String, List(Of String)) In dic_Duplicates

                    writer.WriteLine(vbTab & kvp_Duplicates.Key)

                    For Each sCell As String In kvp_Duplicates.Value
                        writer.WriteLine(vbTab & vbTab & sCell)
                    Next

                Next

                writer.WriteLine()
                writer.WriteLine("Missing Parts:")
                For Each kvp_Missing As KeyValuePair(Of String, String) In dic_Missing

                    writer.WriteLine(vbTab & "To: " & kvp_Missing.Key & ", From: " & kvp_Missing.Value)

                Next

            End Using

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts - Input.log", True)
                For Each kvp As KeyValuePair(Of String, String) In dic_PNSwap
                    writer.WriteLine(kvp.Key & vbTab & kvp.Value)
                Next
            End Using

            lblMissing.Text = iMissing
            lblNoChange.Text = iNoChange
            lblDup.Text = iDup

            'If the file is a text file and xlsApp is defined, then do the following...

            If (Path.GetExtension(s_SwapFile)) <> ".txt" And (Not IsNothing(xlsApp)) Then
                GC.Collect()
                GC.WaitForPendingFinalizers()

                xlsBook.Close()
                Marshal.FinalReleaseComObject(xlsBook)

                xlsApp.Quit()
                Marshal.FinalReleaseComObject(xlsApp)
                xlsApp = Nothing

            End If

            WaitGif.Enabled = False

            If iNoChange = 0 & iDup = 0 & iMissing = 0 Then
                ts_Status.Text = "All items are unique, please proceed..."
                frmMain.NotifyIcon.ShowBalloonTip(2000, "Rename Parts:", "All items were found to be unique.", ToolTipIcon.Info)
                Panel8.Enabled = True
            Else
                MsgBox("There were problems found. Please fix these problems before proceeding...", MsgBoxStyle.OkOnly, "Error:")
                frmMain.NotifyIcon.ShowBalloonTip(2000, "Rename Parts:", "Problems found with input file.", ToolTipIcon.Info)
                Panel8.Enabled = False
            End If

        End If
    End Sub

    Private Sub UpdateComplete(ByVal Errors As Boolean, ByVal Warnings As Boolean)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateComplete(AddressOf UpdateComplete)
            Me.Invoke(d, New Object() {Errors, Warnings})
        Else
            WaitGif.Enabled = False

            If Errors = True Then

                ts_Status.Text = "Finished with errors."

                Dim reply As DialogResult = MessageBox.Show("The update process is finished, but there were errors. Would you like to view the results?", "Finished", _
                  MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Rename Parts")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\Rename Parts.log")
                End If

            Else
                ts_Status.Text = "Finished"
                MessageBox.Show("Update process completed with no errors or warnings.", "Information:", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        End If
    End Sub

    Private Sub SwapParts()

        Dim pedHealApp As New MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        'Creates a handle to the Parts Editor in Library Manager
        'pedHealApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        pedHealApp = frmMain.libDoc.PartEditor
        Try
            pedDoc = pedHealApp.ActiveDatabaseEx
            'pedDoc = pedHealApp.OpenDatabaseEx(frmMain.librarydata.LibPath, False)
        Catch ex As Exception
            MsgBox("Error: A partition is reserved in the library. Please open Library Manager and unreserve all partitions.")
            RaiseEvent eUpdateComplete(True, False)
            Exit Sub
        End Try

        Dim b_Errors As Boolean = False
        Dim b_Warnings As Boolean = False
        Dim b_PrintPartition As Boolean = False

        For Each partition As MGCPCBPartsEditor.Partition In pedDoc.Partitions

            Dim l_DuplicatePNs As New List(Of String)
            Dim reservedPNs As New List(Of String)

            For Each part As MGCPCBPartsEditor.Part In partition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll)

                Dim updatePN As String

                RaiseEvent eUpdateStatus("Analyzing: " & partition.Name & ":" & part.Number)

                If dic_PNSwap.TryGetValue(part.Number, updatePN) Then

                    Dim sPartition As String
                    If frmMain.librarydata.PartList.TryGetValue(updatePN, sPartition) Then
                        l_DuplicatePNs.Add(part.Number & " to " & updatePN & " because it already exists in partition: " & sPartition)
                        b_Errors = True
                    Else

                        RaiseEvent eUpdateStatus("Updating: " & partition.Name & ":" & part.Number)

                        Dim b_PrintPN As Boolean = True
                        Dim originalPartNumber = part.Number
                        Dim originalPartName = part.Name
                        Dim originalPartLabel = part.Label

                        'Number
                        If b_UpdatePN = True Then
                            b_PrintPN = False

                            Try
                                part.Number = updatePN
                            Catch
                                b_Errors = True
                                reservedPNs.Add(part.Number)
                                Continue For
                            End Try

                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)
                                writer.WriteLine(vbTab & "Updating Part Number: " & originalPartNumber & " to: " & updatePN)
                            End Using

                            frmMain.librarydata.PartList.Remove(originalPartNumber)
                            frmMain.librarydata.PartList.Add(updatePN, partition.Name)

                        End If

                        ' Name
                        If b_UpdateName = True Then
                            If b_PrintPN = True Then
                                b_PrintPN = False
                                Try
                                    part.Name = updatePN
                                Catch ex As Exception
                                    b_Errors = True
                                    Continue For
                                End Try

                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)
                                    writer.WriteLine(vbTab & "Updating Part Number: " & originalPartNumber)
                                End Using
                            End If

                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)
                                writer.WriteLine(vbTab & vbTab & "Updating Part Name from: " & originalPartName & " to: " & updatePN)
                            End Using


                        End If

                        ' Label
                        If b_UpdateLabel = True Then
                            If b_PrintPN = True Then
                                b_PrintPN = False
                                Try
                                    part.Label = updatePN
                                Catch ex As Exception
                                    b_Errors = True
                                    Continue For
                                End Try
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)

                                    writer.WriteLine(vbTab & "Updating Part Number: " & part.Number)

                                End Using
                            End If

                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)

                                writer.WriteLine(vbTab & vbTab & "Updating Part Label from: " & originalPartLabel & " to: " & updatePN)

                            End Using

                        End If

                    End If

                End If

            Next

            If l_DuplicatePNs.Count > 0 Or reservedPNs.Count > 0 Then
                If l_DuplicatePNs.Count > 0 Then
                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)
                        writer.WriteLine(partition.Name & ":")
                    End Using
                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)
                        writer.WriteLine(vbTab & "Could not update the following parts:")
                    End Using

                    For Each sPart In l_DuplicatePNs
                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)
                            writer.WriteLine(vbTab & vbTab & sPart)
                        End Using
                    Next

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)
                        writer.WriteLine()
                    End Using
                End If



                If reservedPNs.Count > 0 Then
                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)
                            writer.WriteLine(partition.Name & ":")
                            writer.WriteLine(vbTab & "The following parts were not able to be edited due to their reserved partition.")
                        End Using

                        For Each sPart In reservedPNs
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)
                                writer.WriteLine(vbTab & vbTab & sPart)
                            End Using
                        Next

                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Rename Parts.log", True)
                            writer.WriteLine()
                        End Using

                    End If

                End If

        Next

        pedHealApp.SaveActiveDatabase()

        ' unload
        'pedHealApp.CloseActiveDatabase(True)
        pedDoc = Nothing
        pedHealApp = Nothing

        RaiseEvent eUpdateComplete(b_Errors, b_Warnings)

    End Sub

    Private Sub dgvPNSwap_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs)
        Panel8.Enabled = False
    End Sub

    Private Sub frmSwapPart_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        'dicPartList = frmMain.librarydata.PartList
        Try

            xlsApp = GetObject(, "Excel.Application")
            'MsgBox(excelApp.ActiveWorkbook.FullName())
            xlsBook = xlsApp.ActiveWorkbook
            tbox_Input.Text = Path.GetFileName(xlsBook.Name)

            pl_Excel.Enabled = True
            btnEvaluate.Enabled = True

            cbox_Before.SelectedIndex = -1
            cbox_After.SelectedIndex = -1

            For Each sheet In xlsBook.Worksheets

                cboxActiveSheet.Items.Add(sheet.name)

            Next

            xlsSheet = xlsBook.ActiveSheet

            If xlsBook.Worksheets.Count = 1 Then

                cboxActiveSheet.SelectedIndex = 0

            Else

                cboxActiveSheet.Text = xlsSheet.Name

            End If

            btnEvaluate.Enabled = True

        Catch ex As Exception

        End Try

        AddHandler eAnalysisComplete, AddressOf AnalysisComplete
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eUpdateComplete, AddressOf UpdateComplete

    End Sub

    Private Sub readExcel(xlsSheet As Excel.Worksheet)

        Dim index As Integer = 1
        If chkbox_IgnoreHeader.Checked = True Then
            index = 2
        End If

        Do While Not IsNothing(xlsSheet.Range(Before_Col & index).Value)
            RaiseEvent eUpdateStatus("Reading " & xlsSheet.Name & " row: " & index)

            Dim sBefore As String = xlsSheet.Range((Before_Col & index).ToString()).Value.ToString

            sBefore = sBefore.Trim()

            Dim linesplit As String()

            If sBefore.Contains(",") Then
                linesplit = Regex.Split(sBefore, ",")
            ElseIf sBefore.Contains(";") Then
                linesplit = Regex.Split(sBefore, ";")
            Else
                linesplit = Regex.Split(sBefore, "\s+")
            End If

            If Not String.IsNullOrEmpty(xlsSheet.Range((After_Col & index).ToString()).Value.ToString) Then

                Dim sAfter As String = xlsSheet.Range((After_Col & index).ToString()).Value.ToString.Trim

                dic_PNSwap.Item(sBefore) = sAfter

            End If

            index += 1
        Loop

    End Sub

    Private Sub chkbox_ReadAllSheets_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_ReadAllSheets.Click
        If chkbox_ReadAllSheets.Checked = True Then
            lbl_worksheet.Enabled = False
            cboxActiveSheet.Enabled = False
            cboxActiveSheet.SelectedIndex = -1
        Else
            lbl_worksheet.Enabled = True
            cboxActiveSheet.Enabled = True
            cboxActiveSheet.SelectedIndex = 0
        End If
    End Sub

    Private Sub cboxActiveSheet_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboxActiveSheet.SelectedIndexChanged
        If Not cboxActiveSheet.SelectedIndex = -1 Then
            xlsSheet = xlsBook.Sheets(cboxActiveSheet.Text)
            xlsSheet.Select()
        End If
    End Sub

    Private Sub dgvPNSwap_Leave(sender As System.Object, e As System.EventArgs) Handles dgvPNSwap.Leave
        dgvPNSwap.ClearSelection()
    End Sub

    Private Sub btn_Browse_SwapPN_Click(sender As System.Object, e As System.EventArgs) Handles btn_Browse_SwapPN.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "Text files (*.txt)|*.txt|Excel Files|*.xlsx;*.xls"
            '            ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                s_SwapFile = ofd.FileName
                tbox_Input.Text = ofd.FileName

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

                End If

                btnEvaluate.Enabled = True

                dgvPNSwap.Rows.Clear()

            End If

        End Using
    End Sub

    Private Sub chkbox_ReadAllSheets_CheckedChanged_1(sender As Object, e As EventArgs) Handles chkbox_ReadAllSheets.CheckedChanged

        If chkbox_ReadAllSheets.Checked = True Then
            lbl_worksheet.Enabled = False
            cboxActiveSheet.Enabled = False
            cboxActiveSheet.SelectedIndex = -1
        Else
            lbl_worksheet.Enabled = True
            cboxActiveSheet.Enabled = True
            cboxActiveSheet.SelectedIndex = 0
        End If
    End Sub
End Class