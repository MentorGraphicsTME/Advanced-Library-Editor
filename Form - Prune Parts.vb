Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Drawing

Public Class frmPruneParts

#Region "Public Fields + Properties + Events + Delegates + Enums"

    Delegate Sub d_Complete()

    Delegate Sub d_Increment()

    'Delegate
    Delegate Sub d_PruneComplete()

    Delegate Sub d_UpdateMainParts(ByVal Partition As String, ByVal Part As String)

    Delegate Sub d_UpdateStatus(ByVal status As String)

    Event ePruneComplete()

    Event eReadComplete()

    'Event
    Event eUpdateComplete()

    Event eUpdateMainParts(ByVal Partition As String, ByVal Part As String)

    Event eUpdateStatus(status As String)

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    'Boolean
    Dim bCloseExcel As Boolean = False

    'Dictionary
    Dim dicBadParts As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    Dim dicLogReport As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

    'List
    Dim lParts As New List(Of String)

    'String
    Dim PN_Col As String

    'Object
    Dim xlsApp As Excel.Application

    Dim xlsBook As Excel.Workbook
    Dim xlsSheet As Excel.Worksheet = Nothing
    Dim xmlDoc As Xml.XmlDocument

#End Region

#Region "Private Methods"

    Private Sub btn_Browse_Click(sender As System.Object, e As System.EventArgs) Handles btn_Browse.Click

        Using ofd As New OpenFileDialog
            ofd.Filter = "Excel Files|*.xlsx;*.xls| Text File|*.txt"
            ' ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"
            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                If (Path.GetExtension(ofd.FileName) = ".xls" Or Path.GetExtension(ofd.FileName) = ".xlsx") Then

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

                    If xlsBook.Worksheets.Count = 1 Then

                        cboxActiveSheet.SelectedIndex = 0
                    Else

                        cboxActiveSheet.SelectedIndex = -1

                    End If

                End If

                tbox_Input.Text = ofd.FileName

                btnRead.Enabled = True

            End If

            frmMain.BringToFront()

        End Using

    End Sub

    Private Sub btn_Process_Click(sender As System.Object, e As System.EventArgs) Handles btn_Process.Click

        If File.Exists(frmMain.librarydata.LogPath & "\Prune Parts.log") Then

            File.Delete(frmMain.librarydata.LogPath & "\Prune Parts.log")

        End If

        WaitGif.Enabled = True

        btn_Process.Enabled = False

        If (rbtn_PruneLibrary.Checked) Then
            ts_Status.Text = "Pruning library..."
        Else
            ts_Status.Text = "Pruning parts..."
        End If

        Dim t_Prune As Thread = New Threading.Thread(AddressOf Prune)
        t_Prune.Start()
    End Sub

    Private Sub btnRead_Click(sender As System.Object, e As System.EventArgs) Handles btnRead.Click

        Dim ext As String = Path.GetExtension(tbox_Input.Text)

        If (ext = ".xls" Or ext = ".xlsx") And cbox_PN.SelectedIndex = -1 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Prune Part(s):", "Please select a part number column before proceeding...", ToolTipIcon.Error)
            MessageBox.Show("Please select a part number column before proceeding...")
            Exit Sub
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Prune Part.log") Then

            File.Delete(frmMain.librarydata.LogPath & "Prune Part.log")

        End If

        pl_FileInput.Enabled = False
        WaitGif.Enabled = True

        lbox_Parts.Items.Clear()

        Dim t_Update As Thread

        If Not ext = ".txt" Then
            PN_Col = cbox_PN.Text
            ts_Status.Text = "Reading Excel Spreadsheet..."
            t_Update = New Threading.Thread(AddressOf ReadWorkbook)
        Else
            ts_Status.Text = "Reading Text File..."
            t_Update = New Threading.Thread(AddressOf ReadTextFile)
        End If

        t_Update.IsBackground = True
        t_Update.Start()

    End Sub

    Private Sub cboxActiveSheet_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboxActiveSheet.SelectedIndexChanged

        If Not cboxActiveSheet.SelectedIndex = -1 Then
            xlsSheet = xlsBook.Sheets(cboxActiveSheet.Text)
            xlsSheet.Select()
        End If

    End Sub

    Private Sub chkbox_ReadAllSheets_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_ReadAllSheets.CheckedChanged

        If chkbox_ReadAllSheets.Checked = True Then
            Label1.Enabled = False
            cboxActiveSheet.Enabled = False
            cboxActiveSheet.SelectedIndex = -1
        Else
            Label1.Enabled = True
            cboxActiveSheet.Enabled = True
            cboxActiveSheet.SelectedIndex = 0
        End If

    End Sub

    Private Sub frmPruneParts_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        Try

            xlsApp = GetObject(, "Excel.Application")
            'MsgBox(excelApp.ActiveWorkbook.FullName())
            xlsBook = xlsApp.ActiveWorkbook
            If Not IsNothing(xlsBook) Then

                tbox_Input.Text = xlsBook.FullName

                pl_Excel.Enabled = True

                btnRead.Enabled = True

                cbox_PN.SelectedIndex = -1

                For Each sheet In xlsBook.Worksheets

                    cboxActiveSheet.Items.Add(sheet.name)

                Next

                If xlsBook.Worksheets.Count = 1 Then

                    cboxActiveSheet.SelectedIndex = 0
                Else

                    cboxActiveSheet.SelectedIndex = -1

                End If
            End If
        Catch ex As Exception

        End Try

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eReadComplete, AddressOf ReadExcelComplete
        AddHandler ePruneComplete, AddressOf PruneComplete
        AddHandler eUpdateMainParts, AddressOf UpdateMainParts

    End Sub

    Private Sub Prune()
        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        pedApp = frmMain.libDoc.PartEditor
        Try
            'pedDoc = pedHealApp.OpenDatabaseEx(frmMain.librarydata.LibPath, False)
            pedDoc = pedApp.ActiveDatabaseEx
        Catch ex As Exception
            MsgBox("Error: A partition is reserved in the library. Please open Library Manager and unreserve all partitions.")
            Exit Sub
        End Try

        Dim sbLog As New StringBuilder

        For Each pedPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions
            For Each pedPart As MGCPCBPartsEditor.Part In pedPartition.Parts
                If rbtn_PruneLibrary.Checked Then
                    If lParts.Contains(pedPart.Number) Then
                        Continue For
                    End If
                    sbLog.AppendLine(pedPart.Number)
                    RaiseEvent eUpdateMainParts(pedPartition.Name, pedPart.Number)
                    pedPart.Delete()
                Else
                    If lParts.Contains(pedPart.Number) Then
                        RaiseEvent eUpdateMainParts(pedPartition.Name, pedPart.Number)
                        pedPart.Delete()
                        sbLog.AppendLine(pedPart.Number)
                    End If
                End If
            Next
        Next

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Purge Part.log", True, System.Text.Encoding.ASCII)
            If rbtn_PruneLibrary.Checked Then
                writer.WriteLine("Option: Prune library to part list.")
            Else
                writer.WriteLine("Option: Prune part list from library.")
            End If

            writer.WriteLine()
            writer.WriteLine(sbLog.ToString())
        End Using
        pedApp.SaveActiveDatabase()
        pedDoc = Nothing
        pedApp.Quit()
        pedApp = Nothing

        RaiseEvent ePruneComplete()
    End Sub

    Private Sub PruneComplete()
        If Me.InvokeRequired Then

            Dim d As New d_PruneComplete(AddressOf PruneComplete)
            Me.Invoke(d)
        Else
            WaitGif.Enabled = False
            btn_Process.Enabled = False
            ts_Status.Text = "Prune Finished."

            Dim reply As DialogResult = MessageBox.Show("The prune process finished. Would you like to view the results?", "Finished",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                frmMain.OpenLogFile("Prune Parts")
            Else
                MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\Prune Parts.log")
            End If

            ts_Status.Text = "Finished"

        End If
    End Sub

    Private Sub ReadExcel(xlsSheet As Excel.Worksheet)

        Dim i As Integer

        If chkbox_IgnoreHeader.Checked = True Then
            i = 2
        Else
            i = 1
        End If

        Do While Not IsNothing(xlsSheet.Range(PN_Col & i).Value)

            RaiseEvent eUpdateStatus("Reading " & xlsSheet.Name & " row: " & i)

            Dim sPN As String = xlsSheet.Range(PN_Col & i).Value

            If Not frmMain.librarydata.PartList.ContainsKey(sPN) Then

                dicBadParts.Item(sPN) = "Part Number does not exist in library."
            Else
                If Not lParts.Contains(sPN) Then
                    lParts.Add(sPN)
                End If

            End If

            i += 1

        Loop
    End Sub

    Private Sub ReadExcelComplete()
        If Me.InvokeRequired Then

            Dim d As New d_Complete(AddressOf ReadExcelComplete)
            Me.Invoke(d)
        Else

            Dim iBadPartCount As Integer = 0

            If dicBadParts.Count > 0 Then

                For Each kvpParts As KeyValuePair(Of String, String) In dicBadParts

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Purge Parts - Bad Data.log", True, System.Text.Encoding.ASCII)
                        writer.WriteLine(kvpParts.Key & " - " & kvpParts.Value)
                    End Using

                    iBadPartCount += 1

                Next

                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Purge Parts - Bad Data.log", True, System.Text.Encoding.ASCII)
                    writer.WriteLine()
                    writer.WriteLine(iBadPartCount & " parts could not be found.")
                End Using

            End If

            WaitGif.Enabled = False

            For Each part As String In lParts
                lbox_Parts.Items.Add(part)
            Next

            ts_Status.Text = "Read Complete: " & lParts.Count & " parts."

            btn_Process.Enabled = True

            If dicBadParts.Count > 0 Then

                Dim replyCell As DialogResult = MessageBox.Show("Excel file has been read, but " & dicBadParts.Keys.Count() & " parts could not be found. Would you like to view the results?", "Finished",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If replyCell = DialogResult.Yes Then
                    frmMain.OpenLogFile("Purge Parts - Bad Data")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Purge Parts - Bad Data.log")
                End If
            End If

            If lParts.Count > 0 Then
                gb_Actions.Enabled = True
            End If

        End If
    End Sub

    Private Sub ReadTextFile()

        Dim arFile As String() = File.ReadAllLines(tbox_Input.Text)

        Dim line As String
        For Each line In arFile
            Dim linesplit As String()
            Dim bProblem As Boolean = False

            If line.Contains(";") Then
                linesplit = Regex.Split(line, ";")
            Else
                linesplit = Regex.Split(line, "\s+")
            End If

            Dim sPN As String = linesplit(0)

            If frmMain.librarydata.PartList.ContainsKey(sPN.Trim) Then
                If Not lParts.Contains(sPN) Then
                    lParts.Add(sPN)
                End If
            Else
                dicBadParts.Item(sPN) = "Part Number does not exist in library."
                Continue For
            End If

        Next

        RaiseEvent eReadComplete()

    End Sub

    Private Sub ReadWorkbook()

        xlsBook = xlsApp.ActiveWorkbook

        If chkbox_ReadAllSheets.Checked = True Then

            For Each xlsSheet As Excel.Worksheet In xlsBook.Worksheets

                xlsSheet.Select()

                ReadExcel(xlsSheet)

            Next
        Else

            ReadExcel(xlsSheet)

        End If

        GC.Collect()
        GC.WaitForPendingFinalizers()

        If bCloseExcel = True Then
            xlsBook.Close()
            Marshal.FinalReleaseComObject(xlsBook)

            xlsApp.Quit()
            Marshal.FinalReleaseComObject(xlsApp)
        End If

        xlsBook = Nothing
        xlsApp = Nothing

        RaiseEvent eReadComplete()
    End Sub

    Private Sub tboxFile_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbox_Input.TextChanged

        If tbox_Input.TextLength > 0 And File.Exists(tbox_Input.Text) Then
            btnRead.Enabled = True
        Else
            btnRead.Enabled = False
        End If

    End Sub

    Private Sub UpdateMainParts(Partition As String, Part As String)

        If Me.InvokeRequired Then

            Dim d As New d_UpdateMainParts(AddressOf UpdateMainParts)
            Me.Invoke(d, New Object() {Partition, Part})
        Else
            Dim l_Parts As List(Of String)
            If frmMain.librarydata.PartsByPartition.ContainsKey(Partition) Then
                l_Parts = frmMain.librarydata.PartsByPartition.Item(Partition)
            Else
                l_Parts = New List(Of String)
            End If

            l_Parts.Remove(Part)
            frmMain.librarydata.PartsByPartition.Item(Partition) = l_Parts
            Dim l_Partition As New List(Of String)
            If Not frmMain.librarydata.PartList.ContainsKey(Part) Then
                frmMain.librarydata.PartList.Remove(Part)
            End If

            frmMain.ts_Parts.Text = "Parts: " & frmMain.librarydata.PartList.Count
        End If

    End Sub

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

#End Region

End Class