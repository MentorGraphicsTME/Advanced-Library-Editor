Imports System.Text.RegularExpressions
Imports System.Threading
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Drawing
Public Class frm_SwapCellsinPDB

    'Events
    Event eHealComplete(Errors As Boolean, Warnings As Boolean)
    Event eUpdateStatus(status As String)
    Event eAnalysisComplete()

    'String
    Dim s_SwapFile As String = Nothing
    Dim Before_Col As String
    Dim After_Col As String

    ''Object
    Dim xlsApp As Excel.Application
    Dim xlsBook As Excel.Workbook
    Dim xlsSheet As Excel.Worksheet

    'Boolean
    Dim bCloseExcel As Boolean = False

    'Dictionary
    Dim dic_CellSwap As New Dictionary(Of String, String)

    'Delegates
    Delegate Sub d_HealComplete(ByVal Errors As Boolean, ByVal Warnings As Boolean)
    Delegate Sub d_UpdateStatus(ByVal status As String)
    Delegate Sub d_AnalysisComplete()
    Delegate Sub d_Increment()

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
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

            Dim iDupPN As Integer = 0
            Dim iNoChange As Integer = 0
            Dim iMissingCell As Integer = 0

            For Each kvp As KeyValuePair(Of String, String) In dic_CellSwap
                Dim i As Integer = dgvCellSwap.Rows.Add(kvp.Key, kvp.Value)
                If Not kvp.Key = kvp.Value Then
                    If Not frmMain.librarydata.CellList.ContainsKey(kvp.Value) Then
                        dgvCellSwap.Rows(dgvCellSwap.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Red
                        iMissingCell += 1

                        If Not dic_Missing.ContainsKey(kvp.Value) Then dic_Missing.Add(kvp.Value, kvp.Key)

                    Else
                        If dic_TempDictionary.ContainsKey(kvp.Value) Then
                            dgvCellSwap.Rows(dgvCellSwap.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Orange
                            iDupPN += 1

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
                    dgvCellSwap.Rows(dgvCellSwap.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                    iNoChange += 1
                End If
                dgvCellSwap.Refresh()
                dgvCellSwap.FirstDisplayedScrollingRowIndex = i
            Next

            dic_CellSwap = dic_TempDictionary
            dic_TempDictionary = Nothing

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB - Bad Data.log", True)

                writer.WriteLine("Duplicated Input Values:")
                For Each kvp_Duplicates As KeyValuePair(Of String, List(Of String)) In dic_Duplicates

                    writer.WriteLine(vbTab & kvp_Duplicates.Key)

                    For Each sCell As String In kvp_Duplicates.Value
                        writer.WriteLine(vbTab & vbTab & sCell)
                    Next

                Next

                writer.WriteLine()
                writer.WriteLine("Missing Cells:")
                For Each kvp_Missing As KeyValuePair(Of String, String) In dic_Missing

                    writer.WriteLine(vbTab & "To: " & kvp_Missing.Value & ", From: " & kvp_Missing.Key)

                Next

            End Using

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB - Input.log", True)
                For Each kvp As KeyValuePair(Of String, String) In dic_CellSwap
                    writer.WriteLine(kvp.Key & vbTab & kvp.Value)
                Next
            End Using

            lblMissing.Text = iMissingCell
            lblNoChange.Text = iNoChange
            lblDup.Text = iDupPN

            If Not IsNothing(xlsApp) Then
                GC.Collect()
                GC.WaitForPendingFinalizers()

                xlsBook.Close()
                Marshal.FinalReleaseComObject(xlsBook)

                xlsApp.Quit()
                Marshal.FinalReleaseComObject(xlsApp)
                xlsApp = Nothing

            End If

            WaitGif.Enabled = False

            If iNoChange = 0 & iDupPN = 0 & iMissingCell = 0 Then
                ts_Status.Text = "All items are unique, please proceed..."
                frmMain.NotifyIcon.ShowBalloonTip(2000, "Swap Cells in PDB:", "All items were found to be unique.", ToolTipIcon.Info)
                Panel2.Enabled = True
            Else
                MsgBox("There were problems found. Please fix these problems before proceeding...", MsgBoxStyle.OkOnly, "Error:")
                frmMain.NotifyIcon.ShowBalloonTip(2000, "Swap Cells in PDB:", "Problems found with input file.", ToolTipIcon.Info)
                Panel2.Enabled = False
            End If

        End If
    End Sub

    Private Function EvaluateList()

        Dim index As Integer = 0
        Dim iDupPN As Integer = 0
        Dim iNoChange As Integer = 0

        dic_CellSwap.Clear()

        For Each row In dgvCellSwap.Rows

            'dgvPNSwap.Rows(row.Index).Selected = True

            dgvCellSwap.Rows(row.Index).DefaultCellStyle.BackColor = System.Drawing.Color.White

            Dim OriginalPN = dgvCellSwap.Rows(row.Index).Cells(0).Value()
            Dim NewPN = dgvCellSwap.Rows(row.Index).Cells(1).Value()

            If Not IsNothing(OriginalPN) Then
                If Not OriginalPN = NewPN Then
                    If Not dic_CellSwap.ContainsValue(NewPN) Then
                        dic_CellSwap.Add(OriginalPN, NewPN)
                    Else
                        dgvCellSwap.Rows(row.Index).DefaultCellStyle.BackColor = System.Drawing.Color.Red
                        iDupPN = iDupPN + 1
                    End If
                Else
                    dgvCellSwap.Rows(row.Index).DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                    iNoChange = iNoChange + 1
                End If

                'dgvPNSwap.Rows(row.Index).Selected = False
            End If
        Next

        lblNoChange.Text = iNoChange
        lblDup.Text = iDupPN

        If iDupPN = 0 Then

            Return True

        Else

            Return False

        End If

    End Function

    Private Sub ReadSwapFile()

        dic_CellSwap.Clear()

        If (Path.GetExtension(s_SwapFile) = ".xls") Or (Path.GetExtension(s_SwapFile) = ".xlsx") Then

            If IsNothing(xlsApp) Then
                xlsApp = GetObject(, "Excel.Application")
                'MsgBox(excelApp.ActiveWorkbook.FullName())
                xlsBook = xlsApp.ActiveWorkbook
                xlsSheet = xlsBook.Sheets(cboxActiveSheet.Text)
            End If

            If chkbox_ReadAllSheets.Checked = True Then

                For Each xlsSheet As Excel.Worksheet In xlsBook.Worksheets

                    xlsSheet.Select()

                    readExcel(xlsSheet)

                Next
            Else

                readExcel(xlsSheet)

            End If

        ElseIf Path.GetExtension(s_SwapFile) = ".txt" Then

            Dim arFile As String() = File.ReadAllLines(s_SwapFile)

            Dim line As String

            For Each line In arFile

                line = line.Trim()

                Dim linesplit As String()

                If line.Contains(",") Then
                    linesplit = Regex.Split(line, ",")
                ElseIf line.Contains(";") Then
                    linesplit = Regex.Split(line, ";")
                Else
                    linesplit = Regex.Split(line, "\s+")
                End If

                If linesplit.Length > 1 Then

                    Dim sublinesplit As String()
                    Dim cellnameFrom As String = linesplit(0)
                    Dim cellnameTo As String = linesplit(1)

                    If linesplit(0).Contains(":") Then
                        sublinesplit = Regex.Split(linesplit(0), ":")
                        cellnameFrom = sublinesplit(0)
                    End If

                    If linesplit(1).Contains(":") Then
                        sublinesplit = Regex.Split(linesplit(1), ":")
                        cellnameTo = sublinesplit(1)
                    End If

                    dic_CellSwap.Item(cellnameFrom) = cellnameTo

                End If

            Next

        End If

        RaiseEvent eAnalysisComplete()
    End Sub

    Public Sub Heal()

        Dim dicLogReport As Dictionary(Of String, HealInfo) = HealPDB(Nothing)

        'End If

        Dim b_Warnings As Boolean = False
        Dim b_Errors As Boolean = False

        If chkbox_Error.Checked = True Or chkbox_Warning.Checked = True Or chkbox_Note.Checked = True Then
            For Each kvp As KeyValuePair(Of String, HealInfo) In dicLogReport
                'Grab part number and part attributes:
                Dim sPartition As String = kvp.Key
                Dim oHealInfo As HealInfo = kvp.Value

                If IsNothing(oHealInfo) Then

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                        writer.WriteLine(sPartition & ":")
                        writer.WriteLine("Fatal Error: Could not save database.")
                        writer.WriteLine()

                    End Using

                    b_Errors = True

                    Continue For

                End If
                If oHealInfo.Log.Count > 0 Then
                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                        writer.WriteLine(sPartition & ":")
                        writer.WriteLine("Parts Successfully Fixed: " & oHealInfo.Success)
                        writer.WriteLine("Unable to Fix: " & oHealInfo.Failed)
                        writer.WriteLine("Percentage Complete: " & (oHealInfo.Success / (oHealInfo.Success + oHealInfo.Failed) * 100) & "%")
                        writer.WriteLine()

                    End Using

                    For Each pair As KeyValuePair(Of String, Log) In oHealInfo.Log

                        If pair.Value.Errors.Count > 0 And chkbox_Error.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        ElseIf pair.Value.Warnings.Count > 0 And chkbox_Warning.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        ElseIf pair.Value.Notes.Count > 0 And chkbox_Note.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        Else
                            Continue For
                        End If

                        If pair.Value.Errors.Count > 0 And chkbox_Error.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                                writer.WriteLine(vbTab & vbTab & "Errors:")

                            End Using

                            For Each sErr As String In pair.Value.Errors
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sErr)

                                End Using
                            Next

                            b_Errors = True

                        End If

                        If pair.Value.Warnings.Count > 0 And chkbox_Warning.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                                writer.WriteLine(vbTab & vbTab & "Warnings:")

                            End Using

                            For Each sWrn As String In pair.Value.Warnings
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sWrn)

                                End Using
                            Next

                            b_Warnings = True
                        End If

                        If pair.Value.Notes.Count > 0 And chkbox_Note.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                                writer.WriteLine(vbTab & vbTab & "Notes:")

                            End Using

                            For Each sNote As String In pair.Value.Notes
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sNote)

                                End Using
                            Next

                        End If

                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                            writer.WriteLine()

                        End Using

                    Next

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Swap Cells in PDB.log", True)

                        writer.WriteLine()

                    End Using

                End If

            Next
        End If

        RaiseEvent eHealComplete(b_Errors, b_Warnings)
    End Sub

    Private Function HealPDB(ByVal sPartition As Object)
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
            Exit Function
        End Try

        Dim dicLogReport As New Dictionary(Of String, HealInfo)

        For Each Partition As MGCPCBPartsEditor.Partition In pedDoc.Partitions     'Step through each part partition in the parts editor

            Dim cHealPDB As Heal_PDB = New Heal_PDB()
            AddHandler cHealPDB.eUpdateStatus, AddressOf UpdateStatus

            cHealPDB.LibraryData = frmMain.librarydata
            cHealPDB.dicRenameCells = dic_CellSwap
            AddHandler cHealPDB.LogError, AddressOf LogError
            AddHandler cHealPDB.LogWarning, AddressOf LogWarning
            AddHandler cHealPDB.LogNote, AddressOf LogNote

            cHealPDB.HealParts(Partition)
            dicLogReport.Add(Partition.Name, cHealPDB.HealLog)

            Try
                pedHealApp.SaveActiveDatabase()
            Catch ex As Exception

                Dim oHealInfo As Log

                dicLogReport.Remove(Partition.Name)
                dicLogReport.Add(Partition.Name, Nothing)

            End Try

            For Each part As MGCPCBPartsEditor.Part In Partition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll)
                Dim partNumber As String = part.Number

                If (partNumber.EndsWith("^new")) Or (partNumber.EndsWith("^NewPart")) Then
                    Dim partName As String() = Split(partNumber, "^")
                    part.Number = partName(0)
                End If

            Next

            Try
                pedHealApp.SaveActiveDatabase()
            Catch ex As Exception

                Dim oHealInfo As Log

                dicLogReport.Remove(Partition.Name)
                dicLogReport.Add(Partition.Name, Nothing)

            End Try

        Next

        pedDoc = Nothing
        'pedHealApp.Quit()
        pedHealApp = Nothing
        Return dicLogReport

    End Function

    Private Sub LogError()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogError)
            Me.Invoke(d)
        Else
            If chkbox_Error.Enabled = True Then
                ts_Errors.Text += 1
            End If
        End If
    End Sub

    Private Sub LogWarning()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogWarning)
            Me.Invoke(d)
        Else
            If ts_Warnings.Enabled = True Then
                ts_Warnings.Text += 1
            End If
        End If
    End Sub

    Private Sub LogNote()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogNote)
            Me.Invoke(d)
        Else
            If ts_Notes.Enabled = True Then
                ts_Notes.Text += 1
            End If

        End If
    End Sub

    Private Sub HealComplete(ByVal Errors As Boolean, ByVal Warnings As Boolean)
        If Me.InvokeRequired Then

            Dim d As New d_HealComplete(AddressOf HealComplete)
            Me.Invoke(d, New Object() {Errors, Warnings})
        Else
            WaitGif.Enabled = False

            If Errors = True Then

                ts_Status.Text = "Finished with errors."

                Dim reply As DialogResult = MessageBox.Show("The update process is finished, but there were errors. Would you like to view the results?", "Finished", _
                  MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Swap Cells in PDB")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\Swap Cells in PDB.log")
                End If

            ElseIf Warnings = True Then

                ts_Status.Text = "Finished with warnings."

                Dim reply As DialogResult = MessageBox.Show("The update process is finished, but there were warnings. Would you like to view the results?", "Finished", _
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Swap Cells in PDB")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\Swap Cells in PDB.log")
                End If

            Else
                ts_Status.Text = "Finished"
                MessageBox.Show("Update process completed with no errors or warnings.", "Information:", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        End If
    End Sub

    Private Sub dgvCellSwap_CellEndEdit(sender As System.Object, e As System.Windows.Forms.DataGridViewCellEventArgs)
        Panel2.Enabled = False
    End Sub

    Private Sub frm_SwapCellsinPDB_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

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
            xlsApp = Nothing
            xlsBook = Nothing
            xlsSheet = Nothing
        End Try

        AddHandler eAnalysisComplete, AddressOf AnalysisComplete
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eHealComplete, AddressOf HealComplete

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

                dgvCellSwap.Rows.Clear()

            End If

        End Using
    End Sub

    Private Sub btnEvaluate_Click_1(sender As System.Object, e As System.EventArgs) Handles btnEvaluate.Click
        Dim ext As String = Path.GetExtension(tbox_Input.Text)

        If (ext = ".xls" Or ext = ".xlsx") And (cbox_Before.SelectedIndex = -1 Or cbox_After.SelectedIndex = -1) Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Swap Cells in PDB:", "Please select a before and after column before proceeding...", ToolTipIcon.Error)
            MessageBox.Show("Please select a before and after column before proceeding.")
            Exit Sub
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Swap Cells in PDB - Bad Data.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Swap Cells in PDB - Bad Data.log")
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Swap Cells in PDB - Input.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Swap Cells in PDB - Input.log")
        End If

        Before_Col = cbox_Before.Text
        After_Col = cbox_After.Text

        pl_Excel.Enabled = False

        btnEvaluate.Enabled = False

        s_SwapFile = tbox_Input.Text

        dgvCellSwap.Rows.Clear()

        frmMain.NotifyIcon.ShowBalloonTip(2000, "Swap Cells in PDB:", "Reading input file...", ToolTipIcon.Info)
        ts_Status.Text = "Reading input file..."

        WaitGif.Enabled = True

        Dim t_ReadSwapFile As Thread = New Threading.Thread(AddressOf ReadSwapFile)
        t_ReadSwapFile.IsBackground = True
        t_ReadSwapFile.Start()
    End Sub

    Private Sub btnProcess_Click(sender As System.Object, e As System.EventArgs) Handles btnProcess.Click
        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "\Swap Cells in PDB.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "\Swap Cells in PDB.log")

        End If

        WaitGif.Enabled = True

        GroupBox1.Enabled = False
        btnProcess.Enabled = False
        chkbox_Error.Enabled = False
        chkbox_Note.Enabled = False
        chkbox_Warning.Enabled = False

        ts_Status.Text = "Starting update process..."
        frmMain.NotifyIcon.ShowBalloonTip(2000, "Swap Cells in PDB:", "Starting update process...", ToolTipIcon.Info)

        Dim t_Heal As Thread = New Threading.Thread(AddressOf Heal)
        t_Heal.IsBackground = True
        t_Heal.Start()
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

                dic_CellSwap.Item(sBefore) = sAfter

            End If

            index += 1
        Loop
    End Sub

    Private Sub dgvCellSwap_Leave(sender As System.Object, e As System.EventArgs) Handles dgvCellSwap.Leave
        dgvCellSwap.ClearSelection()
    End Sub

    Private Sub chkbox_ReadAllSheets_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_ReadAllSheets.CheckedChanged

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
End Class