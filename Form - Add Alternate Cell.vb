﻿Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Drawing

Public Class frmAddAltCell

    'Object
    Dim xlsApp As Excel.Application
    Dim xlsBook As Excel.Workbook
    Dim xlsSheet As Excel.Worksheet = Nothing
    Dim xmlDoc As Xml.XmlDocument

    'Dictionary
    Dim dicPartsToModify As New Dictionary(Of String, Dictionary(Of String, AAL.Part))(StringComparer.OrdinalIgnoreCase)
    Dim dicBadParts As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Dim dicLogReport As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
    Dim dicAltCellInfo As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

    'String
    Dim PN_Col As String
    Dim Cell_Col As String

    'Integers
    Dim i_Errors As Integer = 0
    Dim i_Warnings As Integer = 0
    Dim i_Notes As Integer = 0

    'Boolean
    Dim bCloseExcel As Boolean = False

    'Event
    Event eUpdateComplete()
    Event eReadComplete()
    Event eUpdateStatus(status As String)
    Event eHealComplete(Errors As Boolean, Warnings As Boolean)

    'Delegate
    Delegate Sub d_HealComplete(ByVal Errors As Boolean, ByVal Warnings As Boolean)
    Delegate Sub d_UpdateStatus(ByVal status As String)
    Delegate Sub d_Increment()
    Delegate Sub d_Complete()

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub LogError()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogError)
            Me.Invoke(d)
        Else
            If ts_Errors.Enabled = True Then
                Try
                    Dim errors As Integer = ts_Errors.Text
                    errors += 1
                    ts_Errors.Text = errors
                Catch ex As Exception
                    Dim message As String = ex.Message
                End Try
            End If
        End If
    End Sub

    Private Sub LogWarning()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogWarning)
            Me.Invoke(d)
        Else
            If ts_Warnings.Enabled = True Then
                Try
                    Dim warnings As Integer = ts_Warnings.Text
                    warnings += 1
                    ts_Warnings.Text = warnings
                Catch ex As Exception
                    Dim message As String = ex.Message
                End Try
            End If
        End If
    End Sub

    Private Sub LogNote()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogNote)
            Me.Invoke(d)
        Else
            If ts_Notes.Enabled = True Then
                Try
                    Dim notes As Integer = ts_Notes.Text
                    notes += 1
                    ts_Notes.Text = notes
                Catch ex As Exception
                    Dim message As String = ex.Message
                End Try

            End If

        End If
    End Sub

    Private Sub btn_Browse_Click(sender As System.Object, e As System.EventArgs) Handles btn_Browse.Click

        Using ofd As New OpenFileDialog
            ofd.Filter = "Excel Files|*.xlsx;*.xls| Text File|*.txt"
            '            ofd.Filter = "All files (*.*)|*.*"
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

                    ts_Notes.Text = String.Empty

                End If

                tbox_Input.Text = ofd.FileName

                btnRead.Enabled = True

            End If

            frmMain.BringToFront()

        End Using

    End Sub

    Private Sub btn_Process_Click(sender As System.Object, e As System.EventArgs) Handles btn_Process.Click
        i_Errors = ts_Errors.Text = "0"
        i_Notes = ts_Warnings.Text = "0"
        i_Warnings = ts_Notes.Text = "0"

        If File.Exists(frmMain.librarydata.LogPath & "\Add Cells.log") Then

            File.Delete(frmMain.librarydata.LogPath & "\Add Cells.log")

        End If

        WaitGif.Enabled = True
        chkbox_Error.Enabled = False
        chkbox_Warning.Enabled = False
        chkbox_Note.Enabled = False
        btn_Process.Enabled = False

        ts_Status.Text = "Starting heal process..."

        AddHandler eHealComplete, AddressOf HealComplete

        Dim t_Heal As Thread = New Threading.Thread(AddressOf Heal)
        t_Heal.IsBackground = True
        t_Heal.Start()
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

    Private Function AddCell(ByVal sCell As String, ByVal bProblem As Boolean, ByRef oPart As AAL.Part)
        Dim sLocation As String

        sCell = sCell.Trim()
        Dim sPartition As String = Nothing

        If sCell.Contains(":") Then

            Dim sCellNameSplit() As String = sCell.Split(":")

            sPartition = sCellNameSplit(0)
            sCell = sCellNameSplit(1)

        End If

        If sCell.StartsWith("<T>") Then

            sCell = sCell.Remove(0, 3)
            sLocation = "T"

        ElseIf sCell.StartsWith("<B>") Then
            sCell = sCell.Remove(0, 3)
            sLocation = "B"
        End If

        If frmMain.librarydata.CellList.ContainsKey(sCell) Then

            Select Case sLocation

                Case "T"
                    If IsNothing(oPart.TopCell) Then
                        oPart.TopCell = sCell
                    End If

                    oPart.Cells.Add(sCell, Nothing)

                Case "B"
                    If IsNothing(oPart.BotCell) Then
                        oPart.BotCell = sCell
                    End If

            End Select

            If Not oPart.Cells.ContainsKey(sCell) Then oPart.Cells.Add(sCell, Nothing)
        Else

            dicBadParts.Item(oPart.Number) = "Missing Cell: " & sCell

            bProblem = True

        End If

        Return bProblem

    End Function

    Private Sub ReadExcelComplete()
        If Me.InvokeRequired Then

            Dim d As New d_Complete(AddressOf ReadExcelComplete)
            Me.Invoke(d)
        Else

            Dim iBadPartCount As Integer = 0

            If dicBadParts.Count > 0 Then

                For Each kvpParts As KeyValuePair(Of String, String) In dicBadParts

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cell - Bad Data.log", True, System.Text.Encoding.ASCII)
                        writer.WriteLine(kvpParts.Key & " - " & kvpParts.Value)
                    End Using

                    iBadPartCount += 1

                Next

                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells - Bad Data.log", True, System.Text.Encoding.ASCII)
                    writer.WriteLine()
                    writer.WriteLine(iBadPartCount & " parts had issues during the update process.")
                End Using

            End If

            WaitGif.Enabled = False

            Dim iCount As Integer = 0

            For Each kvp_Partition As KeyValuePair(Of String, Dictionary(Of String, AAL.Part)) In dicPartsToModify

                Dim oPartitionNode As New TreeNode
                oPartitionNode.Text = kvp_Partition.Key

                For Each kvp_Part As KeyValuePair(Of String, AAL.Part) In kvp_Partition.Value

                    Dim oPartNode As New TreeNode
                    oPartNode.Text = kvp_Part.Key

                    Dim oCellLabelNode As New TreeNode
                    oCellLabelNode.Text = "Cell(s):"

                    For Each sCell In kvp_Part.Value.Cells.Keys
                        Dim oCellNode As New TreeNode
                        oCellNode.Text = sCell
                        oCellLabelNode.Nodes.Add(oCellNode)
                        iCount += 1
                    Next

                    oCellLabelNode.Expand()

                    oPartNode.Nodes.Add(oCellLabelNode)

                    oPartitionNode.Nodes.Add(oPartNode)
                Next

                tv_Parts.Nodes.Add(oPartitionNode)

            Next

            tv_Parts.Sort()

            ts_Status.Text = "Read Complete: " & iCount & " parts to be modified."

            btn_Process.Enabled = True

            If dicBadParts.Count > 0 Then

                Dim replyCell As DialogResult = MessageBox.Show("Excel file has been read, but " & dicBadParts.Keys.Count() & " parts might not be fully modified. Would you like to view the results?", "Finished", _
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If replyCell = DialogResult.Yes Then
                    frmMain.OpenLogFile("Add Cells - Bad Data")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Add Cells - Bad Data.log")
                End If
            End If

            If dicPartsToModify.Count > 0 Then
                gb_Actions.Enabled = True
            End If

        End If
    End Sub

    Private Sub HealComplete(Errors As Boolean, Warnings As Boolean)
        If Me.InvokeRequired Then

            Dim d As New d_HealComplete(AddressOf HealComplete)
            Me.Invoke(d, New Object() {Errors, Warnings})
        Else
            WaitGif.Enabled = False
            chkbox_Error.Enabled = True
            chkbox_Warning.Enabled = True
            chkbox_Note.Enabled = True
            If Errors = True Then

                ts_Status.Text = "Finished with errors."

                Dim reply As DialogResult = MessageBox.Show("The Add Cells process finished, but there were errors. Would you like to view the results?", "Finished", _
                  MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Add Cells")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\Add Cells.log")
                End If

            ElseIf Warnings = True Then

                ts_Status.Text = "Finished with warnings."

                Dim reply As DialogResult = MessageBox.Show("The Add Cells process finished, but there were warnings. Would you like to view the results?", "Finished", _
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Add Cells")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\Add Cells.log")
                End If

            Else
                ts_Status.Text = "Finished"
                MessageBox.Show("Heal Process completed with no errors or warnings.", "Information:", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        End If
    End Sub

    Private Sub Heal()

        Dim cellEd As CellEditorAddinLib.CellEditorDlg
        Dim cellDB As CellEditorAddinLib.CellDB

        RaiseEvent eUpdateStatus("Opening cell editor...")

        ' Open the Cell Editor dialog and open the library database
        'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
        cellEd = frmMain.libDoc.CellEditor
        cellDB = cellEd.ActiveDatabase

        For Each oCellPartition As CellEditorAddinLib.Partition In cellDB.Partitions()

            For Each ceCell As CellEditorAddinLib.Cell In oCellPartition.Cells  ' process each cell in the partition

                RaiseEvent eUpdateStatus("Getting pin count for: " & cellDB.Name)
                dicAltCellInfo.Item(ceCell.Name) = ceCell.PinCount

            Next

        Next

        cellDB = Nothing
        cellEd.Quit()
        cellEd = Nothing

        RaiseEvent eUpdateStatus("Adding alternate cells...")

        Dim pedHealApp As New MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        'Creates a handle to the Parts Editor in Library Manager
        'pedHealApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        pedHealApp = frmMain.libDoc.PartEditor
        Try
            'pedDoc = pedHealApp.OpenDatabaseEx(frmMain.librarydata.LibPath, False)
            pedDoc = pedHealApp.ActiveDatabaseEx
        Catch ex As Exception
            MsgBox("Error: A partition is reserved in the library. Please open Library Manager and unreserve all partitions.")
            Exit Sub
        End Try

        Dim dicLogReport As New Dictionary(Of String, HealInfo)(StringComparer.OrdinalIgnoreCase)
        For Each kvp_Partition As KeyValuePair(Of String, Dictionary(Of String, AAL.Part)) In dicPartsToModify

            For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions(kvp_Partition.Key)     'Step through each part partition in the parts editor

                Dim cHealPDB As Heal_PDB = New Heal_PDB()
                AddHandler cHealPDB.eUpdateStatus, AddressOf UpdateStatus

                cHealPDB.bSupplementData = False
                cHealPDB.bRepairErrors = False
                cHealPDB.bUpdatePartType = False
                cHealPDB.bRemoveSpaceFromCell = False
                cHealPDB.bAddNCPins = True
                cHealPDB.bUpdateSymPartition = False
                cHealPDB.bAddCells = True
                cHealPDB.LibraryData = frmMain.librarydata
                cHealPDB.PartsToHeal = kvp_Partition.Value.Keys.ToList()
                cHealPDB.dicAlternateCells = kvp_Partition.Value
                cHealPDB.dicAltCellInfo = dicAltCellInfo

                AddHandler cHealPDB.LogError, AddressOf LogError
                AddHandler cHealPDB.LogWarning, AddressOf LogWarning
                AddHandler cHealPDB.LogNote, AddressOf LogNote

                cHealPDB.HealParts(pdbPartition)
                dicLogReport.Item(pdbPartition.Name) = cHealPDB.HealLog

                Try
                    pedHealApp.SaveActiveDatabase()
                Catch ex As Exception

                    dicLogReport.Item(pdbPartition.Name) = Nothing

                End Try

            Next

        Next

        For Each pedPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions
            For Each part As MGCPCBPartsEditor.Part In pedPartition.Parts
                Dim partNumber As String = part.Number

                If (partNumber.EndsWith("^new")) Or (partNumber.EndsWith("^NewPart")) Then
                    Dim partName As String() = Split(partNumber, "^")
                    part.Number = partName(0)
                End If

            Next

            Try
                pedHealApp.SaveActiveDatabase()
            Catch ex As Exception

                dicLogReport.Item(pedPartition.Name) = Nothing

            End Try
        Next

        pedDoc = Nothing
        pedHealApp.Quit()
        pedHealApp = Nothing

        'End If

        Dim b_Warnings As Boolean = False
        Dim b_Errors As Boolean = False

        If chkbox_Error.Checked = True Or chkbox_Warning.Checked = True Or chkbox_Note.Checked = True Then
            For Each kvp As KeyValuePair(Of String, HealInfo) In dicLogReport
                'Grab part number and part attributes:
                Dim sPartition As String = kvp.Key
                Dim oHealInfo As HealInfo = kvp.Value

                If IsNothing(oHealInfo) Then

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                        writer.WriteLine(sPartition & ":")
                        writer.WriteLine("Fatal Error: Could not save database.")
                        writer.WriteLine()

                    End Using

                    b_Errors = True

                    Continue For

                End If

                If oHealInfo.Log.Count > 0 Then
                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                        writer.WriteLine(sPartition & ":")
                        writer.WriteLine()

                    End Using

                    For Each pair As KeyValuePair(Of String, Log) In oHealInfo.Log

                        If pair.Value.Errors.Count > 0 And chkbox_Error.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        ElseIf pair.Value.Warnings.Count > 0 And chkbox_Warning.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        ElseIf pair.Value.Notes.Count > 0 And chkbox_Note.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        Else
                            Continue For
                        End If

                        If pair.Value.Errors.Count > 0 And chkbox_Error.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                                writer.WriteLine(vbTab & vbTab & "Errors:")

                            End Using

                            For Each sErr As String In pair.Value.Errors
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sErr)

                                End Using
                            Next

                            b_Errors = True

                        End If

                        If pair.Value.Warnings.Count > 0 And chkbox_Warning.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                                writer.WriteLine(vbTab & vbTab & "Warnings:")

                            End Using

                            For Each sWrn As String In pair.Value.Warnings
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sWrn)

                                End Using
                            Next

                            b_Warnings = True
                        End If

                        If pair.Value.Notes.Count > 0 And chkbox_Note.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                                writer.WriteLine(vbTab & vbTab & "Notes:")

                            End Using

                            For Each sNote As String In pair.Value.Notes
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sNote)

                                End Using
                            Next

                        End If

                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                            writer.WriteLine()

                        End Using

                    Next

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Add Cells.log", True)

                        writer.WriteLine()

                    End Using

                End If

            Next
        End If

        RaiseEvent eHealComplete(b_Errors, b_Warnings)
    End Sub

    Private Sub frmAddAltCell_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        Try

            xlsApp = GetObject(, "Excel.Application")
            'MsgBox(excelApp.ActiveWorkbook.FullName())
            xlsBook = xlsApp.ActiveWorkbook
            tbox_Input.Text = xlsBook.FullName

            pl_Excel.Enabled = True

            btnRead.Enabled = True

            cbox_PN.SelectedIndex = -1
            cbox_Cell.SelectedIndex = -1

            For Each sheet In xlsBook.Worksheets

                cboxActiveSheet.Items.Add(sheet.name)

            Next

            If xlsBook.Worksheets.Count = 1 Then

                cboxActiveSheet.SelectedIndex = 0

            Else

                cboxActiveSheet.SelectedIndex = -1

            End If

        Catch ex As Exception

        End Try

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eReadComplete, AddressOf ReadExcelComplete

    End Sub

    Private Sub tboxFile_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbox_Input.TextChanged

        If tbox_Input.TextLength > 0 And File.Exists(tbox_Input.Text) Then
            btnRead.Enabled = True
        Else
            btnRead.Enabled = False
        End If

    End Sub

    Private Sub btnRead_Click(sender As System.Object, e As System.EventArgs) Handles btnRead.Click

        Dim ext As String = Path.GetExtension(tbox_Input.Text)

        If (ext = ".xls" Or ext = ".xlsx") And (cbox_PN.SelectedIndex = -1 Or cbox_Cell.SelectedIndex = -1) Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Add Cell(s):", "Please select a part number and cell column before proceeding...", ToolTipIcon.Error)
            MessageBox.Show("Please select a part number and cell column before proceeding.")
            Exit Sub
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Add Cell - Bad Data.log") Then

            File.Delete(frmMain.librarydata.LogPath & "Add Cell - Bad Data.log")

        End If

        pl_FileInput.Enabled = False
        WaitGif.Enabled = True

        tv_Parts.Nodes.Clear()

        Dim t_Update As Thread

        If Not ext = ".txt" Then
            PN_Col = cbox_PN.Text
            Cell_Col = cbox_Cell.Text

            ts_Status.Text = "Reading Excel Spreadsheet..."
            t_Update = New Threading.Thread(AddressOf ReadWorkbook)

        Else
            ts_Status.Text = "Reading Text File..."

            t_Update = New Threading.Thread(AddressOf ReadTextFile)
        End If

        t_Update.IsBackground = True
        t_Update.Start()

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

            If linesplit.Length > 1 Then

                Dim sPN As String = linesplit(0)
                Dim sCell As String = linesplit(1)
                Dim sPartition As String

                If frmMain.librarydata.PartList.ContainsKey(sPN.Trim) Then
                    sPartition = frmMain.librarydata.PartList.Item(sPN)
                Else
                    dicBadParts.Item(sPN) = "Part Number does not exist in library."
                End If

                Dim dicParts As Dictionary(Of String, AAL.Part)

                Dim oPart As AAL.Part

                If dicPartsToModify.ContainsKey(sPartition) Then
                    dicParts = dicPartsToModify.Item(sPartition)
                    If dicParts.ContainsKey(sPN) Then
                        oPart = dicParts.Item(sPN)
                    Else
                        oPart = New AAL.Part
                        oPart.Number = sPN
                    End If
                Else
                    dicParts = New Dictionary(Of String, AAL.Part)(StringComparer.OrdinalIgnoreCase)
                    oPart = New AAL.Part
                    oPart.Number = sPN
                End If

                bProblem = AddCell(sCell, bProblem, oPart)

                If oPart.Cells.Count > 0 And Not bProblem = True Then
                    dicParts.Item(sPN) = oPart
                    dicPartsToModify.Item(sPartition) = dicParts
                End If

            End If

        Next

        RaiseEvent eReadComplete()

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

            Dim sPartition As String
            Dim bProblem As Boolean = False
            Dim sPN As String = xlsSheet.Range(PN_Col & i).Value

            If Not frmMain.librarydata.PartList.ContainsKey(xlsSheet.Range(PN_Col & i).Value) Then

                dicBadParts.Item(xlsSheet.Range(PN_Col & i).Value) = "Part Number does not exist in library."

                bProblem = True

                i += 1
                Continue Do

            Else
                sPN = xlsSheet.Range(PN_Col & i).Value

                If frmMain.librarydata.PartList.ContainsKey(sPN.Trim) Then
                    sPartition = frmMain.librarydata.PartList.Item(sPN)
                Else
                    dicBadParts.Item(sPN) = "Part Number does not exist in library."
                    i += 1
                    Continue Do
                End If

            End If

            Dim xlsCellValue As String = xlsSheet.Range(Cell_Col & i).Value

            If String.IsNullOrEmpty(xlsCellValue) Then

                dicBadParts.Item(sPN) = "No Cell Defined."

                bProblem = True

                i += 1
                Continue Do

            Else

                Dim dicParts As Dictionary(Of String, AAL.Part)
                Dim oPart As AAL.Part

                If dicPartsToModify.ContainsKey(sPartition) Then
                    dicParts = dicPartsToModify.Item(sPartition)
                    If dicParts.ContainsKey(sPN) Then
                        oPart = dicParts.Item(sPN)
                    Else
                        oPart = New AAL.Part
                        oPart.Number = sPN
                    End If
                Else
                    dicParts = New Dictionary(Of String, AAL.Part)(StringComparer.OrdinalIgnoreCase)
                    oPart = New AAL.Part
                    oPart.Number = sPN
                End If

                If xlsCellValue.Contains(";") Then
                    Dim sCellSplit As String() = Split(xlsSheet.Range(Cell_Col & i).Value, ";")

                    For Each sCell In sCellSplit

                        bProblem = AddCell(sCell, bProblem, oPart)

                    Next

                ElseIf xlsCellValue.Contains(",") Then
                    Dim sCellSplit As String() = Split(xlsSheet.Range(Cell_Col & i).Value, ",")

                    For Each sCell In sCellSplit

                        bProblem = AddCell(sCell, bProblem, oPart)

                    Next

                Else

                    Dim sCell As String = xlsSheet.Range(Cell_Col & i).Value

                    bProblem = AddCell(sCell, bProblem, oPart)

                End If

                If oPart.Cells.Count > 0 And Not bProblem = True Then
                    dicParts.Item(sPN) = oPart
                    dicPartsToModify.Item(sPartition) = dicParts
                End If

            End If

            i += 1

        Loop
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

    Private Sub cboxActiveSheet_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboxActiveSheet.SelectedIndexChanged

        If Not cboxActiveSheet.SelectedIndex = -1 Then
            xlsSheet = xlsBook.Sheets(cboxActiveSheet.Text)
            xlsSheet.Select()
        End If

    End Sub
End Class