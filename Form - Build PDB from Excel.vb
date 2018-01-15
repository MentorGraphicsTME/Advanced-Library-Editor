Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Text
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Threading.Tasks
Imports AAL
Imports MGCPCBPartsEditor

Public Class frmBuildFromExcel

#Region "Public Fields + Properties + Events + Delegates + Enums"

    Public Enum BuildPDBError
        errDueToUnableToOpenPDBEditor = 0
        errDueToUnableToFindFile = 1
    End Enum

    Delegate Sub d_BuildComplete(ByVal iPartsFailed As Integer, ByVal iPartsBuilt As Integer, ByVal partsNotConsidered As Integer)

    Delegate Sub d_BuildFailed(ByVal buildError As BuildPDBError)

    Delegate Sub d_PartitionResults(ByVal partition As String, ByVal time As Decimal, ByVal totalParts As Integer, ByVal partsBuilt As Integer, ByVal partsFailed As Integer)

    'Delegate
    Delegate Sub d_UpdateComplete(ByVal partsNotConsidered As Integer)

    Delegate Sub d_UpdateMainParts(ByVal Partition As String, ByVal Part As String)

    Delegate Sub d_UpdateStatus(ByVal status As String)

    Delegate Sub d_UpdateThreads()

    Delegate Sub d_UpdateTimer()

    Event eBuildComplete(ByVal iPartsFailed As Integer, ByVal iPartsBuilt As Integer, ByVal partsNotConsidered As Integer)

    Event eBuildFailed(ByVal buildError As BuildPDBError)

    'ePartitionResults(pdbPartition.Name, TimeToProcess.ElapsedMilliseconds, kvp.Value.Count(), partitionPartsBuilt, kvp.Value.Count() - partitionPartsBuilt)
    Event ePartitionResults(ByVal partition As String, ByVal time As Decimal, ByVal totalParts As Integer, ByVal partsBuilt As Integer, ByVal partsFailed As Integer)

    'Event
    Event eUpdateComplete(partsNotConsidered As Integer)

    Event eUpdateMainParts(ByVal Partition As String, ByVal Part As String)

    Event eUpdateStatus(status As String)

    Event eUpdateThreads()

    Public Property LogFile As String
    Public Property mappingsLeftToProcess As Integer

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    Private Property activeSheet As Worksheet

    Private Property partsLeftToRead As Integer
    Dim ActiveThreads As Integer = 1

    Private addLock As New Object

    'Arraylist
    Dim alPartPartitionList As New ArrayList()

    'Boolean
    Dim bCloseExcel As Boolean = False

    Dim col_Cell As String
    Dim col_Height As String

    'String
    Dim col_Partition As String

    Dim col_PartNumber As String
    Dim col_RefDes As String
    Dim col_Symbol As String

    'Dictionary
    Dim dic_Height As New Dictionary(Of String, Double)

    Dim dic_UnableToSave As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

    'Dim dicBadData As New Dictionary(Of String, Dictionary(Of String, List(Of String)))(StringComparer.OrdinalIgnoreCase)
    Dim dicBadPartitionData As New Dictionary(Of String, Dictionary(Of String, AAL.Part))(StringComparer.OrdinalIgnoreCase)

    Dim dicBadParts As New Dictionary(Of String, Dictionary(Of String, List(Of String)))(StringComparer.OrdinalIgnoreCase)
    Dim dicDuplicateParts As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
    Dim dicExceptionParts As New Dictionary(Of String, Integer)

    'UserInput
    Dim dicInputData As New Dictionary(Of String, Dictionary(Of String, Array))

    Dim dicLogReport As New Dictionary(Of String, ArrayList)
    Dim dicMappingData As New SortedDictionary(Of String, Dictionary(Of String, AAL.Part))(StringComparer.OrdinalIgnoreCase)
    Dim dicModifiedParts As New Dictionary(Of String, Dictionary(Of String, List(Of String)))(StringComparer.OrdinalIgnoreCase)

    'Dictionary
    Dim dicPartitionData As New Dictionary(Of String, AAL.PartPartition)(StringComparer.OrdinalIgnoreCase)

    Dim dicSuccessfulByPartition As New Dictionary(Of String, List(Of String))

    Private errLock As New Object
    Dim errStack As Stack(Of AAL.Part)
    Private getPartInfoLock As New Object

    'Integers
    Dim iBadPartsFromExcel As Integer = 0

    Dim iErrorCount As Integer = 0
    Dim iModifiedPartsFromExcel As Integer = 0
    Private inputLock As New Object

    'Dim xmlDoc As Xml.XmlDocument
    'Integer
    Dim iPartCount As Integer = 0

    Dim iPartsBuilt As Integer = 0
    Dim iPartsFailed As Integer = 0

    'List
    Dim lexcelInfoSelections As New List(Of String)

    Private logLock As New Object
    Private logPartLock As New Object
    Dim maxTasks As Integer = 1
    Private modifyLock As New Object
    Private newPartLock As New Object
    Dim partitionPartsBuilt As Integer = 0
    Dim partitionPartsFailed As Integer = 0
    Dim partsStack As Stack(Of AAL.Part)
    Dim pdbPartition As MGCPCBPartsEditor.Partition

    Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg

    Dim pedDoc As MGCPCBPartsEditor.PartsDB

    Dim timerTotal As New Stopwatch

    ''Object
    Dim xlsApp As Excel.Application

    Dim xlsBook As Excel.Workbook
    Dim xlsSheet As Excel.Worksheet = Nothing
    Dim xlsSheetName As String = Nothing

#End Region

#Region "Private Methods"

    Private Function AddCell(ByVal sCell As String, ByVal bProblem As Boolean, ByRef aalPart As AAL.Part)
        Dim sLocation As String

        sCell = sCell.Trim()
        Dim Partition As String = Nothing

        If sCell.Contains(":") Then

            Dim sCellNameSplit() As String = sCell.Split(":")

            Partition = sCellNameSplit(0)
            sCell = sCellNameSplit(1)

        End If

        If sCell.StartsWith("<T>") Then

            sCell = sCell.Remove(0, 3)
            sLocation = "T"

        ElseIf sCell.StartsWith("<B>") Then
            sCell = sCell.Remove(0, 3)
            sLocation = "B"
        End If

        Dim l_CellPartitions As List(Of String)
        If frmMain.librarydata.CellList.ContainsKey(sCell) Then
            l_CellPartitions = frmMain.librarydata.CellList.Item(sCell)
            Dim oCell As New AAL.Cell
            oCell.Name = sCell
            If IsNothing(Partition) Then
                oCell.Partition = l_CellPartitions(0)
            Else
                For Each CellPartition In l_CellPartitions
                    If CellPartition = Partition Then
                        oCell.Partition = Partition
                        Exit For
                    End If
                Next

                If IsNothing(oCell.Partition) Then

                    oCell.Partition = l_CellPartitions(0)

                    Dim dic_Parts As Dictionary(Of String, List(Of String))
                    Dim l_Info As List(Of String) = Nothing

                    If dicModifiedParts.ContainsKey(aalPart.Partition.Trim) Then
                        dic_Parts = dicModifiedParts.Item(aalPart.Partition.Trim)
                    Else
                        dic_Parts = New Dictionary(Of String, List(Of String))
                    End If

                    If dic_Parts.ContainsKey(aalPart.Number) Then
                        l_Info = dic_Parts.Item(aalPart.Number)
                    Else
                        l_Info = New List(Of String)
                    End If

                    Dim sMessage As String = "[Invalid Input] Unable to find cell " & sCell & " in partition " & Partition & " but did find it in partition " & l_CellPartitions(0)
                    aalPart.ReadInfo.Add(sMessage)
                    If Not (l_Info.Contains(sMessage)) Then
                        l_Info.Add(sMessage)
                    End If

                    dic_Parts.Item(aalPart.Number) = l_Info

                    dicModifiedParts.Item(aalPart.Partition.Trim) = dic_Parts

                End If

            End If

            Select Case sLocation

                Case "T"
                    If IsNothing(aalPart.TopCell) Then
                        aalPart.TopCell = oCell.Name
                    Else
                        Dim dic_Parts As Dictionary(Of String, List(Of String))
                        Dim l_Info As List(Of String) = Nothing
                        If dicBadParts.ContainsKey(aalPart.Partition.Trim) Then
                            dic_Parts = dicBadParts.Item(aalPart.Partition.Trim)
                        End If

                        If IsNothing(dic_Parts) Then
                            dic_Parts = New Dictionary(Of String, List(Of String))
                            l_Info = New List(Of String)
                        Else
                            If dic_Parts.ContainsKey(aalPart.Number) Then
                                l_Info = dic_Parts.Item(aalPart.Number)
                            Else
                                l_Info = New List(Of String)
                            End If
                        End If

                        l_Info.Add("Assigning " & sCell & " as an alternate cell because " & aalPart.TopCell & " is already assigned as this part's top cell.")
                        aalPart.ReadInfo.Add("Assigning " & sCell & " as an alternate cell because " & aalPart.TopCell & " is already assigned as this part's top cell.")
                        dic_Parts.Item(aalPart.Number) = l_Info

                        dicBadParts.Item(aalPart.Partition.Trim) = dic_Parts

                        bProblem = True
                    End If

                Case "B"
                    If IsNothing(aalPart.BotCell) Then
                        aalPart.BotCell = oCell.Name
                    Else
                        Dim dic_Parts As Dictionary(Of String, List(Of String))
                        Dim l_Info As List(Of String) = Nothing
                        If dicBadParts.ContainsKey(aalPart.Partition.Trim) Then
                            dic_Parts = dicBadParts.Item(aalPart.Partition.Trim)
                        End If

                        If IsNothing(dic_Parts) Then
                            dic_Parts = New Dictionary(Of String, List(Of String))
                            l_Info = New List(Of String)
                        Else
                            If dic_Parts.ContainsKey(aalPart.Number) Then
                                l_Info = dic_Parts.Item(aalPart.Number)
                                dic_Parts.Remove(aalPart.Number)
                            Else
                                l_Info = New List(Of String)
                            End If
                        End If

                        l_Info.Add("Assigning " & sCell & " as an alternate cell because " & aalPart.BotCell & " is already assigned as this part's bottom cell.")
                        aalPart.ReadInfo.Add("Assigning " & sCell & " as an alternate cell because " & aalPart.BotCell & " is already assigned as this part's bottom cell.")
                        dic_Parts.Item(aalPart.Number) = l_Info

                        dicBadParts.Item(aalPart.Partition.Trim) = dic_Parts
                        bProblem = True
                    End If

            End Select

            If Not aalPart.Cells.ContainsKey(oCell.Partition & ":" & sCell) Then aalPart.Cells.Add(oCell.Partition & ":" & sCell, oCell)
        Else

            Dim dic_Parts As Dictionary(Of String, List(Of String))
            Dim l_Info As List(Of String) = Nothing
            If dicBadParts.ContainsKey(aalPart.Partition.Trim) Then
                dic_Parts = dicBadParts.Item(aalPart.Partition.Trim)
            End If

            If IsNothing(dic_Parts) Then
                dic_Parts = New Dictionary(Of String, List(Of String))
                l_Info = New List(Of String)
            Else
                If dic_Parts.ContainsKey(aalPart.Number) Then
                    l_Info = dic_Parts.Item(aalPart.Number)
                Else
                    l_Info = New List(Of String)
                End If
            End If

            aalPart.MissingCells.Add(sCell)
            dic_Parts.Item(aalPart.Number) = l_Info

            dicBadParts.Item(aalPart.Partition.Trim) = dic_Parts

            bProblem = True

        End If

        Return bProblem

    End Function

    Private Sub AddPart(aalPart As AAL.Part)

        SyncLock addLock

            Dim aalPartition As AAL.PartPartition
            If dicPartitionData.ContainsKey(aalPart.Partition) Then
                aalPartition = dicPartitionData.Item(aalPart.Partition)
            Else
                aalPartition = New PartPartition()
                aalPartition.Name = aalPart.Partition
            End If

            aalPartition.Item(aalPart.Number) = aalPart
            dicPartitionData.Item(aalPart.Partition.Trim) = aalPartition

        End SyncLock

    End Sub

    Private Function addSymbol(ByVal sSymbol As String, ByVal bProblem As Boolean, ByRef aalPart As AAL.Part) As Boolean
        Dim symname As String
        Dim sSymPartition As String = Nothing

        sSymbol = sSymbol.Trim()

        Dim symbolSplit As String() = Split(sSymbol, ":")

        If symbolSplit.Count > 1 Then
            sSymPartition = symbolSplit(0)
            symname = symbolSplit(1)
        Else
            symname = sSymbol
        End If

        If symname.Contains(".") Then
            symname = symname.Substring(0, symname.IndexOf("."))
        End If

        Dim l_Partitions As List(Of String)
        If frmMain.librarydata.SymbolList.ContainsKey(symname) Then
            l_Partitions = frmMain.librarydata.SymbolList.Item(symname)
            Dim bFoundSymbol As Boolean = False

            If IsNothing(sSymPartition) Then
                sSymPartition = l_Partitions(0)
                bFoundSymbol = True
            Else
                For Each s_Partition In l_Partitions

                    If String.Compare(s_Partition, sSymPartition, True) = 0 Then
                        bFoundSymbol = True
                        Exit For
                    End If

                Next
            End If

            If bFoundSymbol = True Then
                Dim oSymbol As New AAL.Symbol
                oSymbol.Name = symname
                oSymbol.Partition = sSymPartition

                If Not aalPart.Symbols.ContainsKey(oSymbol.Partition & ":" & oSymbol.Name) Then aalPart.Symbols.Add(oSymbol.Partition & ":" & oSymbol.Name, oSymbol)
            Else

                Dim oSymbol As New AAL.Symbol
                oSymbol.Name = symname
                oSymbol.Partition = l_Partitions(0)

                If Not aalPart.Symbols.ContainsKey(oSymbol.Partition & ":" & oSymbol.Name) Then aalPart.Symbols.Add(oSymbol.Partition & ":" & oSymbol.Name, oSymbol)

                Dim dic_Parts As Dictionary(Of String, List(Of String))
                Dim l_Info As List(Of String) = Nothing

                If dicModifiedParts.ContainsKey(aalPart.Partition.Trim) Then
                    dic_Parts = dicModifiedParts.Item(aalPart.Partition.Trim)
                Else
                    dic_Parts = New Dictionary(Of String, List(Of String))
                End If

                If dic_Parts.ContainsKey(aalPart.Number) Then
                    l_Info = dic_Parts.Item(aalPart.Number)
                Else
                    l_Info = New List(Of String)
                End If

                Dim sMessage As String = "[Invalid Input] Unable to find symbol " & symname & " in partition " & sSymPartition & " but did find it in partition " & l_Partitions(0)
                aalPart.ReadInfo.Add(sMessage)
                If Not (l_Info.Contains(sMessage)) Then
                    l_Info.Add(sMessage)
                End If

                dic_Parts.Item(aalPart.Number) = l_Info

                dicModifiedParts.Item(aalPart.Partition.Trim) = dic_Parts

            End If
        Else
            Dim dic_Parts As Dictionary(Of String, List(Of String))
            Dim l_Info As List(Of String) = Nothing
            If dicBadParts.ContainsKey(aalPart.Partition.Trim) Then
                dic_Parts = dicBadParts.Item(aalPart.Partition.Trim)
            End If

            If IsNothing(dic_Parts) Then
                dic_Parts = New Dictionary(Of String, List(Of String))
                l_Info = New List(Of String)
            Else
                If dic_Parts.ContainsKey(aalPart.Number) Then
                    l_Info = dic_Parts.Item(aalPart.Number)
                Else
                    l_Info = New List(Of String)
                End If
            End If

            aalPart.MissingSymbols.Add(sSymbol)
            l_Info.Add("[Invalid Input] Missing Symbol: " & sSymbol)
            aalPart.ReadInfo.Add("[Invalid Input] Missing Symbol: " & sSymbol)
            dic_Parts.Item(aalPart.Number) = l_Info

            dicBadParts.Item(aalPart.Partition.Trim) = dic_Parts

            bProblem = True

        End If

        Return bProblem
    End Function

    Private Sub btn_Browse_Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Browse_Excel.Click

        Using ofd As New OpenFileDialog
            ofd.Filter = "Excel Files|*.xlsx;*.xls"
            ' ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                bCloseExcel = True

                tboxWorkbook.Text = Path.GetFileName(ofd.FileName)

                xlsApp = New Excel.Application

                xlsApp.Visible = True

                xlsApp.Workbooks.Open(ofd.FileName)

                xlsBook = xlsApp.ActiveWorkbook

                'MsgBox("Neutral Data has been loaded...")

                For Each sheet In xlsBook.Worksheets

                    cboxActiveSheet.Items.Add(sheet.name)

                Next

                If xlsBook.Worksheets.Count = 1 Then

                    cboxActiveSheet.SelectedIndex = 0
                Else

                    cboxActiveSheet.SelectedIndex = -1

                End If

                cboxPartPartition.SelectedIndex = -1
                cbox_CellName.SelectedIndex = -1
                cbox_PartNumber.SelectedIndex = -1
                cbox_SymbolName.SelectedIndex = -1

                gb_ExcelInfo.Enabled = True
                gb_Options.Enabled = True

                ts_Status.Text = "Provide the required information..."

            End If

            Me.BringToFront()

        End Using

    End Sub

    Private Sub btnBuild_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuild.Click

        ts_Status.Text = "Building PDBs..."
        btnBuild.Enabled = False
        WaitGif.Enabled = True
        tsm_Threads.Visible = True
        dgvResults.Rows.Clear()

        TabControl1.SelectedTab = tabResults

        LogFile = frmMain.librarydata.LogPath & " Build PDB from Excel.log"

        If File.Exists(LogFile) Then
            File.Delete(LogFile)
        End If

        Dim t_Build As Thread = New Threading.Thread(AddressOf BuildPDB)
        t_Build.IsBackground = True
        t_Build.Start()

    End Sub

    Private Sub btnClearCell_Click(sender As Object, e As EventArgs) Handles btnClearCell.Click
        cbox_CellName.SelectedIndex = -1
        btnClearCell.Visible = False
    End Sub

    Private Sub btnClearHeight_Click(sender As Object, e As EventArgs) Handles btnClearHeight.Click
        cbox_Height.SelectedIndex = -1
        btnClearHeight.Visible = False
    End Sub

    Private Sub btnClearPartition_Click(sender As Object, e As EventArgs) Handles btnClearPartition.Click
        cboxPartPartition.SelectedIndex = -1
        btnClearPartition.Visible = False
    End Sub

    Private Sub btnClearPN_Click(sender As Object, e As EventArgs) Handles btnClearPN.Click
        cbox_PartNumber.SelectedIndex = -1
        btnClearPN.Visible = False
    End Sub

    Private Sub btnClearRefDes_Click(sender As Object, e As EventArgs) Handles btnClearRefDes.Click
        cbox_RefDes.SelectedIndex = -1
        btnClearRefDes.Visible = False
    End Sub

    Private Sub btnClearSymbol_Click(sender As Object, e As EventArgs) Handles btnClearSymbol.Click
        cbox_SymbolName.SelectedIndex = -1
        btnClearSymbol.Visible = False
    End Sub

    Private Sub btnPythonLog_Click(sender As Object, e As EventArgs) Handles btnPythonLog.Click

        Dim new_process As New Process
        new_process.StartInfo.FileName = frmMain.librarydata.LogPath & "\Heal PDB.log"
        new_process.StartInfo.Verb = "Open"
        new_process.Start()

        Dim webAddress As String = "http://www.example.com/"
        Process.Start(webAddress)

    End Sub

    Private Sub btnRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRead.Click

        If cboxPartPartition.SelectedItem = Nothing And Not chkbox_WorksheetName.Checked Then
            MsgBox("Please select a partition column.")
            Exit Sub
        End If

        If Not (cbox_PartNumber.SelectedItem = Nothing) Or Not (cbox_SymbolName.SelectedItem = Nothing) Or Not (cbox_CellName.SelectedItem = Nothing) Then

            col_Partition = Nothing
            col_PartNumber = Nothing
            col_Symbol = Nothing
            col_Cell = Nothing
            col_RefDes = Nothing
            col_Height = Nothing

            'dicBadParts = New Dictionary(Of String, List(Of String))
            'dicMissingCells = New Dictionary(Of String, List(Of String))
            iPartCount = 0

            If Not cboxPartPartition.SelectedIndex = -1 Then
                col_Partition = cboxPartPartition.Text
            End If
            If Not cbox_PartNumber.SelectedIndex = -1 Then
                col_PartNumber = cbox_PartNumber.Text
            End If
            If Not cbox_CellName.SelectedIndex = -1 Then
                col_Cell = cbox_CellName.Text
            End If
            If Not cbox_SymbolName.SelectedIndex = -1 Then
                col_Symbol = cbox_SymbolName.Text
            End If
            If Not cbox_RefDes.SelectedIndex = -1 Then
                col_RefDes = cbox_RefDes.Text
            End If
            If Not cbox_Height.SelectedIndex = -1 Then
                col_Height = cbox_Height.Text
            End If

            tv_Parts.Nodes.Clear()
            dicBadParts.Clear()
            dicDuplicateParts.Clear()
            dicModifiedParts.Clear()
            dicPartitionData.Clear()

            iBadPartsFromExcel = 0
            iModifiedPartsFromExcel = 0
            iPartCount = 0

            ts_Status.Text = "Reading Excel Spreadsheet..."

            WaitGif.Enabled = True
            tsm_Threads.Visible = True

            Dim t_Update As Thread = New Threading.Thread(AddressOf ReadExcel)
            t_Update.IsBackground = True
            t_Update.Start()

        End If

    End Sub

    Private Sub BuildComplete(ByVal iPartsFailed As Integer, ByVal iPartsBuilt As Integer, ByVal partsNotConsidered As Integer)
        If Me.InvokeRequired Then

            'If (chkboxLogPerPartition.Checked = False) Then
            partsNotConsidered = WriteLogFiles(dicLogReport, iPartsFailed, iPartsBuilt)
            'Else
            '    For Each Partition As String In dicBadParts.Keys
            '        partsNotConsidered += dicBadParts.Item(Partition).Count
            '    Next
            'End If

            Dim d As New d_BuildComplete(AddressOf BuildComplete)
            Me.Invoke(d, New Object() {iPartsFailed, iPartsBuilt, partsNotConsidered})
        Else
            timerTotal.Stop()
            RaiseEvent eUpdateStatus("Logging errors...")
            WaitGif.Enabled = True
            tsm_Threads.Visible = False

            'If File.Exists("Main.py") Then
            '    btnPythonLog.Visible = True
            'Else
            '    btnPythonLog.Visible = False
            'End If

            'If the partition name from worksheets check box is checked, then
            ' we build a sheet of parts unable to save in a worksheet named after
            ' the partition. Otherwise, we create the worksheet

            WaitGif.Enabled = False
            ts_Status.Text = "Successfully Built: " & iPartsBuilt & ", Failed to Build: " & iPartsFailed + partsNotConsidered

            'xmlDoc.Save(frmMain.librarydata.LogPath & "\Build PDB Errors - Generic XML Mappings.xml")

            If iPartsFailed > 0 Then

                'xmlDoc.Save(frmMain.librarydata.LogPath & "\Build PDB Errors - Generic XML Mappings.xml")
                Dim reply As DialogResult = MessageBox.Show(iPartsBuilt & " parts were successfully built but " & iPartsFailed + partsNotConsidered & " failed to build. Would you like to view the results?", "Finished",
            MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    If (chkboxLogPerPartition.Checked = True) Then
                        frmMain.OpenLogFile()
                    Else
                        frmMain.OpenLogFile("Build PDB from Excel.log")
                    End If
                Else
                    MessageBox.Show("For more information, please see: " & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from Excel.log")
                End If

            ElseIf (dic_UnableToSave.Keys.Count > 0) Then

                Dim s_ErrorMessage As New StringBuilder

                For Each Partition As String In dic_UnableToSave.Keys
                    s_ErrorMessage.AppendLine(Partition)
                Next

                Dim reply As DialogResult = MessageBox.Show("Unable to save the following partitions:" & Environment.NewLine & Environment.NewLine & s_ErrorMessage.ToString() & Environment.NewLine & Environment.NewLine & "Would you like to view the results?", "Finished",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    If (chkboxLogPerPartition.Checked = True) Then
                        frmMain.OpenLogFile()
                    Else
                        frmMain.OpenLogFile("Build PDB from Excel.log")
                    End If
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from Excel.log")
                End If
            Else
                MsgBox(iPartsBuilt & " parts were successfully built.")
            End If

        End If

        btnBuild.Enabled = True
    End Sub

    Private Sub BuildFailed(ByVal buildError As BuildPDBError)
        If Me.InvokeRequired Then
            Dim d As New d_BuildFailed(AddressOf BuildFailed)
            Me.Invoke(d, New Object() {buildError})
        Else
            WaitGif.Enabled = False
            If buildError = BuildPDBError.errDueToUnableToOpenPDBEditor Then
                MsgBox("Unable to open the PDB editor. This is most likely due to a reserved partition.")
            ElseIf buildError = BuildPDBError.errDueToUnableToFindFile Then
                MsgBox("RefDesPartitions.caf was not found. Please locate the file to create partitions by reference designators.")
            End If
            ts_Status.Text = "Build Failed."
        End If
    End Sub

    Private Function BuildPart() As Object
        Try
            While partsStack.Count > 0

                While ActiveThreads > maxTasks
                    Thread.Sleep(5000)
                End While

                Dim aalPart As AAL.Part
                Try
                    If partsStack.Count > 0 Then
                        aalPart = partsStack.Pop

                        If Not IsNothing(aalPart) Then
                            ActiveThreads += 1
                            RaiseEvent eUpdateThreads()
                            If chkbox_RebuildParts.Checked = True Then

                                For Each pdbTempPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions()
                                    For Each pdbPart As MGCPCBPartsEditor.Part In pdbTempPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, aalPart.Number)
                                        pdbPart.Delete()
                                        GoTo FoundPart
                                    Next
                                Next

                            ElseIf (dicExceptionParts.ContainsKey(aalPart.Number)) Then
                                For Each pdbTempPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions(aalPart.Partition)
                                    For Each pdbPart As MGCPCBPartsEditor.Part In pdbTempPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, aalPart.Number)
                                        pdbPart.Delete()
                                    Next
                                Next
                            End If

FoundPart:

                            aalPart.Partition = pdbPartition.Name
                            Dim ocBuildPDB As Build_PDB = New Build_PDB
                            'ocBuildPDB.pdbPartition = pdbPartition
                            'ocBuildPDB.l_SymbolNames = kvp_Part.Value.Symbols.Keys.ToList()
                            'ocBuildPDB.l_CellName = kvp_Part.Value.Cells.Keys.ToList()
                            ocBuildPDB.Heights = dic_Height
                            ocBuildPDB.aalPart = aalPart
                            'ocBuildPDB.xmlDoc = xmlDoc
                            'ocBuildPDB.dicGates = Nothing
                            ocBuildPDB.b_RemoveIncompleteParts = chkbox_RemoveIncomplete.Checked
                            ocBuildPDB.LibraryData = frmMain.librarydata
                            'ocBuildPDB.alPinAtts = partInfo(5)

                            Dim success As Boolean

                            Dim pdbMapping As MGCPCBPartsEditor.Part = pdbPartition.NewPart()

                            Try
                                success = ocBuildPDB.NewPart(pdbMapping)

                                Thread.Sleep(500)

                                RaiseEvent eUpdateThreads()
                                'Dim LogWarnings As New ArrayList
                                'Dim LogErrors As New ArrayList
                                'Dim LogNotes As New ArrayList

                                'xmlDoc = ocBuildPDB.xmlDoc

                                If (success) Then
                                    partitionPartsBuilt += 1
                                    'iPartsBuilt += 1
                                    If IsNothing(aalPart.AssociatedParts) Then
                                        ocBuildPDB.Errors.Add("Mapping: " & aalPart.HashCode & " has a null Associated Parts container.")
                                    Else
                                        For Each pn As String In aalPart.AssociatedParts

                                            If chkbox_RebuildParts.Checked = True Then
                                                If frmMain.librarydata.PartList.ContainsKey(pn) Then
                                                    For Each pdbTempPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions()
                                                        For Each pdbPart As MGCPCBPartsEditor.Part In pdbTempPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, pn)
                                                            pdbPart.Delete()
                                                        Next
                                                    Next
                                                End If

                                            End If

                                            Try
                                                pdbMapping.Copy(pn, pn, pn)
                                                partitionPartsBuilt += 1
                                                iPartsBuilt += 1
                                                RaiseEvent eUpdateMainParts(pdbPartition.Name, pn)
                                            Catch ex As Exception
                                                ocBuildPDB.Errors.Add("Can not copy pin mapping from " & pdbMapping.Number & " to " & pn & ". Error: " & ex.Message.ToString())
                                            End Try
                                        Next
                                    End If

                                    Dim logItem As New BuildPDBLogItem(success, aalPart.Number, ocBuildPDB.Errors, ocBuildPDB.Warnings, ocBuildPDB.Notes, ocBuildPDB.LogAlternateSymbols)

                                    LogPart(logItem)

                                    mappingsLeftToProcess -= 1
                                Else

                                    If Not dicExceptionParts.ContainsKey(aalPart.Number) Then
                                        errStack.Push(aalPart)
                                        dicExceptionParts.Add(aalPart.Number, 1)
                                    ElseIf dicExceptionParts(aalPart.Number) > 2 Then

                                        partitionPartsFailed += 1

                                        Dim logItem As New BuildPDBLogItem(success, aalPart.Number, ocBuildPDB.Errors, ocBuildPDB.Warnings, ocBuildPDB.Notes, ocBuildPDB.LogAlternateSymbols)

                                        LogPart(logItem)

                                        If IsNothing(aalPart.AssociatedParts) Then
                                            ocBuildPDB.Errors.Add("Mapping: " & aalPart.HashCode & " has a null Associated Parts container.")
                                        Else
                                            For Each pn As String In aalPart.AssociatedParts
                                                ocBuildPDB.Errors.Add("Failed to add part " & pn & " because it was associated with a mapping " & aalPart.Number & " that failed to build.")
                                                partitionPartsFailed += 1

                                            Next
                                        End If

                                        mappingsLeftToProcess -= 1
                                    Else
                                        errStack.Push(aalPart)
                                        dicExceptionParts(aalPart.Number) += 1
                                    End If

                                End If

                                If success Then
                                    aalPart.Dispose()
                                    aalPart = Nothing
                                End If
                            Catch ex As Exception
                                If Not dicExceptionParts.ContainsKey(aalPart.Number) Then
                                    errStack.Push(aalPart)
                                    dicExceptionParts.Add(aalPart.Number, 1)
                                ElseIf dicExceptionParts(aalPart.Number) > 2 Then

                                    Dim err As String = ex.ToString()

                                    WriterErrorToLog(aalPart.Partition & ":" & aalPart.Number & " - " & err & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())

                                    ocBuildPDB.Errors.Add(err)

                                    partitionPartsFailed += 1

                                    If IsNothing(aalPart.AssociatedParts) Then
                                        ocBuildPDB.Errors.Add("Mapping: " & aalPart.HashCode & " has a null Associated Parts container.")
                                    Else
                                        For Each pn As String In aalPart.AssociatedParts
                                            ocBuildPDB.Errors.Add("Failed to add part " & pn & " because it was associated with a mapping " & aalPart.Number & " that failed to build.")
                                            partitionPartsFailed += 1

                                        Next
                                    End If

                                    Dim logItem As New BuildPDBLogItem(success, aalPart.Number, ocBuildPDB.Errors, ocBuildPDB.Warnings, ocBuildPDB.Notes, ocBuildPDB.LogAlternateSymbols)

                                    LogPart(logItem)

                                    mappingsLeftToProcess -= 1
                                Else
                                    errStack.Push(aalPart)
                                    dicExceptionParts(aalPart.Number) += 1
                                End If

                            End Try

                            ActiveThreads -= 1

                            RaiseEvent eUpdateStatus(pdbPartition.Name & " mappings left to process: " & mappingsLeftToProcess)
                        End If
                    End If
                Catch ex As Exception
                    Thread.Sleep(5000)
                    partitionPartsFailed += 1
                    Dim message As String = ex.ToString()
                    If Not IsNothing(aalPart) Then
                        If Not dicExceptionParts.ContainsKey(aalPart.Number) Then
                            errStack.Push(aalPart)
                            dicExceptionParts.Add(aalPart.Number, 1)
                        ElseIf dicExceptionParts(aalPart.Number) > 2 Then

                            Dim err As String = ex.ToString()

                            WriterErrorToLog(aalPart.Partition & ":" & aalPart.Number & " - " & err & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())

                            Dim errorList As New List(Of String)
                            errorList.Add(err)

                            partitionPartsFailed += 1
                            Try
                                For Each pn As String In aalPart.AssociatedParts
                                    errorList.Add("Failed to add part " & pn & " because it was associated with a mapping " & aalPart.Number & " that failed to build.")
                                    partitionPartsFailed += 1

                                Next

                                Dim logItem As New BuildPDBLogItem(False, aalPart.Number, errorList, New List(Of String), New List(Of String), New ArrayList())

                                LogPart(logItem)
                            Catch exAlt As Exception
                                WriterErrorToLog(aalPart.Partition & ":" & aalPart.Number & " - " & err & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
                            End Try

                            mappingsLeftToProcess -= 1
                        Else
                            errStack.Push(aalPart)
                            dicExceptionParts(aalPart.Number) += 1
                        End If
                        WriterErrorToLog(aalPart.Partition & ":" & aalPart.Number & " - " & ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
                    Else
                        WriterErrorToLog(message & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
                    End If

                    mappingsLeftToProcess -= 1

                    RaiseEvent eUpdateStatus(pdbPartition.Name & " mappings left to process: " & mappingsLeftToProcess)
                End Try

                'Return objLogAtts
            End While
        Catch ex As Exception
            WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
        End Try

    End Function

    Private Sub BuildPDB()

        'MentorGraphics

        If File.Exists(frmMain.librarydata.LogPath & "Build PDB from Excel.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Build PDB from Excel.log")
        End If

        timerTotal = New Stopwatch
        timerTotal.Start()

        Task.Factory.StartNew(Of Object)(Function() UpdateTimer())

        pedApp = frmMain.libDoc.PartEditor
        pedDoc = pedApp.ActiveDatabaseEx

        'ArrayList
        Dim alnewPDBPartitions As New ArrayList()
        dicLogReport = New Dictionary(Of String, ArrayList)

        If chkbox_GetHeight.Checked = True Then

            Dim cellEd As CellEditorAddinLib.CellEditorDlg
            Dim cellDB As CellEditorAddinLib.CellDB

            RaiseEvent eUpdateStatus("Opening cell editor...")

            ' Open the Cell Editor dialog and open the library database

            cellEd = frmMain.libDoc.CellEditor
            cellDB = cellEd.ActiveDatabase

            For Each oCellPartition As CellEditorAddinLib.Partition In cellDB.Partitions()

                For Each oCell As CellEditorAddinLib.Cell In oCellPartition.Cells  ' process each cell in the partition

                    Dim sCellName As String = oCellPartition.Name & ":" & oCell.Name

                    RaiseEvent eUpdateStatus("Getting height for - " & sCellName.ToUpper)
                    Select Case pedDoc.CurrentUnit

                        Case MGCPCBPartsEditor.EPDBUnit.epdbUnitInch
                            dic_Height.Add(sCellName.ToUpper, oCell.Height(CellEditorAddinLib.ECellDBUnit.ecelldbUnitInch))
                        Case MGCPCBPartsEditor.EPDBUnit.epdbUnitMils
                            dic_Height.Add(sCellName.ToUpper, oCell.Height(CellEditorAddinLib.ECellDBUnit.ecelldbUnitMils))
                        Case MGCPCBPartsEditor.EPDBUnit.epdbUnitMM
                            dic_Height.Add(sCellName.ToUpper, oCell.Height(CellEditorAddinLib.ECellDBUnit.ecelldbUnitMM))
                        Case MGCPCBPartsEditor.EPDBUnit.epdbUnitUM
                            dic_Height.Add(sCellName.ToUpper, oCell.Height(CellEditorAddinLib.ECellDBUnit.ecelldbUnitUM))
                    End Select
                Next
            Next

            cellEd.SaveActiveDatabase()
            cellDB = Nothing
            cellEd.Quit()
            cellEd = Nothing

        End If

        RaiseEvent eUpdateStatus("Creating any missing partitions...")

        For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions     'Step through each part partition in the parts editor
            alnewPDBPartitions.Add(pdbPartition.Name)
        Next

        If File.Exists(frmMain.librarydata.LogPath & "Build PDB from Excel.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Build PDB from Excel.log")
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Build PDB from Excel.tsv") Then
            File.Delete(frmMain.librarydata.LogPath & "Build PDB from Excel.tsv")
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Build PDB - Alternate Symbols.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Build PDB - Alternate Symbols.log")
        End If

        iPartsBuilt = 0
        iPartsFailed = 0

        Dim srCaf As System.IO.StreamReader
        Dim sline As String

        If chkbox_RefDesPartitions.Checked = True Then

            If Not File.Exists(frmMain.librarydata.LogPath & "RefDesPartitions.caf") Then
                RaiseEvent eBuildFailed(BuildPDBError.errDueToUnableToFindFile)
                Exit Sub
            End If

            Using sr As StreamReader = New StreamReader(frmMain.librarydata.LogPath & "RefDesPartitions.caf")
                While Not sr.EndOfStream
                    sline = sr.ReadLine()
                    If Not sline Is Nothing Then
                        Dim linesplit As String() = Split(sline, vbTab)
                        alnewPDBPartitions.Add(linesplit(1))
                    End If
                End While
            End Using

            Try
                srCaf.Close()
            Catch ex As Exception
                WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
            End Try
        Else
            If alnewPDBPartitions.Count = 0 Then
                alnewPDBPartitions.Add("Temp")
            End If
        End If

        If File.Exists(frmMain.librarydata.LogPath & "\Build PDB Errors - Generic XML Mappings.xml") Then
            File.Delete(frmMain.librarydata.LogPath & "\Build PDB Errors - Generic XML Mappings.xml")
        End If

        partsStack = New Stack(Of AAL.Part)
        errStack = New Stack(Of AAL.Part)
        'xmlDoc = New Xml.XmlDocument

        'Dim xmlSettings As New Xml.XmlWriterSettings
        'xmlSettings.Indent = True
        'xmlSettings.IndentChars = vbTab
        'xmlSettings.NewLineChars = vbNewLine
        'xmlSettings.NewLineHandling = Xml.NewLineHandling.Replace

        'If Not File.Exists(frmMain.librarydata.LogPath & "\Build PDB Errors - Generic XML Mappings.xml") Then
        'Using writer As Xml.XmlWriter = Xml.XmlWriter.Create(frmMain.librarydata.LogPath & "\Build PDB Errors - Generic XML Mappings.xml", xmlSettings)
        'writer.WriteStartDocument()
        'writer.WriteStartElement("Mapping")
        'writer.WriteEndElement()
        'writer.WriteEndDocument()

        'End Using
        'End If

        'xmlDoc.Load(frmMain.librarydata.LogPath & "\Build PDB Errors - Generic XML Mappings.xml")

        'Dim maxPartsCanBuild As Integer = 250

        For Each kvp As KeyValuePair(Of String, Dictionary(Of String, AAL.Part)) In dicMappingData
            partitionPartsBuilt = 0
            partitionPartsFailed = 0
            RaiseEvent eUpdateStatus("Refreshing connection to PDB Editor.")
            While (IsNothing(pedDoc))
                pedApp = frmMain.libDoc.PartEditor
                pedDoc = pedApp.ActiveDatabaseEx
                Thread.Sleep(2500)
            End While

            Dim TimeToProcess As New Stopwatch
            TimeToProcess.Start()

            mappingsLeftToProcess = kvp.Value.Count
            ActiveThreads = 0

            Try
                If Not frmMain.librarydata.PartsByPartition.Keys.Contains(kvp.Key) And Not IsNothing(kvp.Key) And kvp.Key <> vbNullString Then
                    pdbPartition = pedDoc.NewPartition(kvp.Key)
                    frmMain.librarydata.PartsByPartition.Add(kvp.Key.Trim, New List(Of String))
                Else
                    pdbPartition = pedDoc.Partitions(kvp.Key).Item(1)
                End If
            Catch ex As Exception
                WriterErrorToLog(kvp.Key & " - " & ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
            End Try

            Dim dicPartData As Dictionary(Of String, AAL.Part) = kvp.Value

            If chkbox_SaveEachPart.Checked Or chkboxMultiThread.Checked = False Then
                For Each kvp_Part As KeyValuePair(Of String, AAL.Part) In dicPartData
                    'If chkbox_RebuildParts.Checked = True Then
                    '    For Each pdbTempPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions
                    '        For Each pdbPart As MGCPCBPartsEditor.Part In pdbTempPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, kvp_Part.Value.Number)
                    '            pdbPart.Delete()
                    '        Next
                    '    Next
                    'End If

                    partsStack.Push(kvp_Part.Value)

                    BuildPart()

                Next
            Else
                Dim tasks As New List(Of Task(Of Object))

                'Dim partsToProcess As Integer = dicPartData.Count
                'Dim refreshPDBEditorLimit As Integer = 3000
                Dim parts As Array = dicPartData.Values.ToArray
                Dim index As Integer = 0
                While mappingsLeftToProcess > 0
                    Dim Max As Integer = Math.Min(mappingsLeftToProcess, 1000)

                    If Not errStack.Count = 0 Then
                        Max = Max - errStack.Count

                        While Not errStack.Count = 0
                            partsStack.Push(errStack.Pop)
                        End While

                    End If

                    Dim j As Integer = 0

                    While ((index < parts.Length) And (j < Max))
                        partsStack.Push(parts(index))
                        index += 1
                        j += 1
                    End While

                    maxTasks = Math.Min(partsStack.Count, 100)

                    For i As Integer = 0 To maxTasks - 1
                        tasks.Add(Task.Factory.StartNew(Of Object)(Function() BuildPart()))
                        Thread.Sleep(500)
                    Next

                    Task.WaitAll(tasks.ToArray())

                    RaiseEvent eUpdateStatus("Saving Partition.")

                    Dim bSaved As Boolean

                    Try
                        pedApp.SaveActiveDatabase()
                        bSaved = True
                    Catch ex As Exception
                        iPartsFailed += partitionPartsBuilt
                        dic_UnableToSave.Item(kvp.Key) = dicPartData.Keys.ToList
                        bSaved = False
                        WriterErrorToLog(kvp.Key & " - " & ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
                    Finally
                        'If bSaved Then
                        '    If dicSuccessfulByPartition.Keys.Contains(pdbPartition.Name) Then
                        '        For Each part As String In dicSuccessfulByPartition.Item(pdbPartition.Name)
                        '            RaiseEvent eUpdateMainParts(pdbPartition.Name, part)
                        '        Next
                        '    End If
                        'End If
                    End Try

                    Try
                        pedApp.Quit()
                    Catch ex As Exception
                        WriterErrorToLog(kvp.Key & " - " & ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
                    End Try

                    pedApp = Nothing
                    pedDoc = Nothing

                    If errStack.Count = 0 And index = parts.Length Then
                        mappingsLeftToProcess = 0
                    End If

                    If dicPartData.Count > 0 Then
                        GC.Collect()
                    End If

                    RaiseEvent eUpdateStatus("Refreshing connection to PDB Editor.")
                    While (IsNothing(pedDoc))
                        pedApp = frmMain.libDoc.PartEditor
                        pedDoc = pedApp.ActiveDatabaseEx
                        Thread.Sleep(2500)
                    End While

                    pdbPartition = pedDoc.Partitions(kvp.Key).Item(1)

                End While

            End If

            RaiseEvent eUpdateStatus("Saving Partition.")
            Dim bSavedPartition As Boolean

            Try
                pedApp.SaveActiveDatabase()
                bSavedPartition = True
            Catch ex As Exception
                iPartsFailed += partitionPartsBuilt
                dic_UnableToSave.Item(kvp.Key) = dicPartData.Keys.ToList
                bSavedPartition = False
                WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
            Finally
                Thread.Sleep(5000)
                'If bSavedPartition Then
                '    If dicSuccessfulByPartition.Keys.Contains(pdbPartition.Name) Then
                '        For Each part As String In dicSuccessfulByPartition.Item(pdbPartition.Name)
                '            RaiseEvent eUpdateMainParts(pdbPartition.Name, part)
                '        Next
                '    End If
                'End If
            End Try

            TimeToProcess.Stop()

            RaiseEvent ePartitionResults(kvp.Key, TimeToProcess.ElapsedMilliseconds, partitionPartsBuilt + partitionPartsFailed, partitionPartsBuilt, partitionPartsFailed)
            Try
                pedApp.Quit()
            Catch ex As Exception
                WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
            End Try

            pedApp = Nothing
            pedDoc = Nothing

            If dicPartData.Count > 0 Then
                GC.Collect()
            End If

            If chkboxLogPerPartition.Checked And dicBadParts.Keys.Contains(kvp.Key) Then

                Dim dicLogReport As New Dictionary(Of String, ArrayList)

                For Each Partition As String In dicBadParts.Keys

                    Dim dicspecificPartition As Dictionary(Of String, List(Of String)) = dicBadParts.Item(Partition)
                    Dim alLog As New ArrayList
                    For Each partnumber As String In dicspecificPartition.Keys
                        Dim errorList As List(Of String) = dicspecificPartition.Item(partnumber)
                        Dim logItem As New BuildPDBLogItem(False, partnumber, errorList, New List(Of String), New List(Of String), New ArrayList)
                        If dicLogReport.Keys.Contains(Partition) Then
                            alLog = dicLogReport.Item(Partition)
                            alLog.Add(logItem)
                            dicLogReport.Item(Partition) = alLog
                        Else
                            Dim alLogNew As New ArrayList()
                            alLogNew.Add(logItem)
                            dicLogReport.Add(Partition, alLogNew)
                        End If
                    Next
                Next

                PartitionToLog(kvp.Key)

            End If

        Next

        'pedApp.Quit()
        'pedApp = Nothing
        'pedDoc = Nothing

        If chkboxLogPerPartition.Checked = False Then
            For Each Partition As String In dicBadParts.Keys
                Dim dicspecificPartition As Dictionary(Of String, List(Of String)) = dicBadParts.Item(Partition)
                Dim alLog As New ArrayList
                For Each partnumber As String In dicspecificPartition.Keys
                    Dim errorList As List(Of String) = dicspecificPartition.Item(partnumber)
                    Dim logItem As New BuildPDBLogItem(False, partnumber, errorList, New List(Of String), New List(Of String), New ArrayList)

                    If dicLogReport.Keys.Contains(Partition) Then
                        alLog = dicLogReport.Item(Partition)
                        alLog.Add(logItem)
                        dicLogReport.Item(Partition) = alLog
                    Else
                        Dim alLogNew As New ArrayList()
                        alLogNew.Add(logItem)
                        dicLogReport.Add(Partition, alLogNew)
                    End If
                Next
            Next
        End If

        RaiseEvent eBuildComplete(iPartsFailed, iPartsBuilt, 0)

    End Sub

    Private Sub cbox_CellName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_CellName.SelectedIndexChanged
        If Not sender.selectedIndex = -1 Then
            btnClearCell.Visible = True
        End If
    End Sub

    Private Sub cbox_CellName_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbox_CellName.SelectionChangeCommitted
        For Each currentcbox As ComboBox In gb_ExcelInfo.Controls.OfType(Of ComboBox).Except({cbox_CellName, cboxActiveSheet})
            If currentcbox.SelectedIndex = cbox_CellName.SelectedIndex Then
                lbl_duplicate.Visible = True
                btnRead.Enabled = False
                Exit Sub
            End If
        Next
        lbl_duplicate.Visible = False
        btnRead.Enabled = True
    End Sub

    Private Sub cbox_Height_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_Height.SelectedIndexChanged
        If Not sender.selectedIndex = -1 Then
            btnClearHeight.Visible = True
        End If
    End Sub

    Private Sub cbox_Height_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbox_Height.SelectionChangeCommitted
        For Each currentcbox As ComboBox In gb_ExcelInfo.Controls.OfType(Of ComboBox).Except({cbox_Height, cboxActiveSheet})
            If currentcbox.SelectedIndex = cbox_Height.SelectedIndex Then
                lbl_duplicate.Visible = True
                btnRead.Enabled = False
                Exit Sub
            End If
        Next
        lbl_duplicate.Visible = False
        btnRead.Enabled = True
    End Sub

    Private Sub cbox_PartNumber_SelectedIndexChanged(sender As ComboBox, e As EventArgs) Handles cbox_PartNumber.SelectedIndexChanged
        If Not sender.SelectedIndex = -1 Then
            btnClearPN.Visible = True
        End If
    End Sub

    Private Sub cbox_PartNumber_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbox_PartNumber.SelectionChangeCommitted
        For Each currentcbox As ComboBox In gb_ExcelInfo.Controls.OfType(Of ComboBox).Except({cbox_PartNumber, cboxActiveSheet})
            If currentcbox.SelectedIndex = cbox_PartNumber.SelectedIndex Then
                lbl_duplicate.Visible = True
                btnRead.Enabled = False
                Exit Sub
            End If
        Next
        lbl_duplicate.Visible = False
        btnRead.Enabled = True
    End Sub

    Private Sub cbox_RefDes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_RefDes.SelectedIndexChanged
        If Not sender.selectedIndex = -1 Then
            btnClearRefDes.Visible = True
        End If
    End Sub

    Private Sub cbox_RefDes_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbox_RefDes.SelectionChangeCommitted
        For Each currentcbox As ComboBox In gb_ExcelInfo.Controls.OfType(Of ComboBox).Except({cbox_RefDes, cboxActiveSheet})
            If currentcbox.SelectedIndex = cbox_RefDes.SelectedIndex Then
                lbl_duplicate.Visible = True
                btnRead.Enabled = False
                Exit Sub
            End If
        Next
        lbl_duplicate.Visible = False
        btnRead.Enabled = True
    End Sub

    Private Sub cbox_SymbolName_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_SymbolName.SelectedIndexChanged
        If Not sender.selectedIndex = -1 Then
            btnClearSymbol.Visible = True
        End If
    End Sub

    Private Sub cbox_SymbolName_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cbox_SymbolName.SelectionChangeCommitted
        For Each currentcbox As ComboBox In gb_ExcelInfo.Controls.OfType(Of ComboBox).Except({cbox_SymbolName, cboxActiveSheet})
            If currentcbox.SelectedIndex = cbox_SymbolName.SelectedIndex Then
                lbl_duplicate.Visible = True
                btnRead.Enabled = False
                Exit Sub
            End If
        Next
        lbl_duplicate.Visible = False
        btnRead.Enabled = True
    End Sub

    Private Sub cboxActiveSheet_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cboxActiveSheet.SelectedIndexChanged

        If Not cboxActiveSheet.SelectedIndex = -1 Then
            xlsSheet = xlsBook.Sheets(cboxActiveSheet.Text)
            xlsSheet.Select()
        End If

    End Sub

    Private Sub cboxPartPartition_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboxPartPartition.SelectedIndexChanged
        If Not sender.selectedIndex = -1 Then
            btnClearPartition.Visible = True
        End If
    End Sub

    Private Sub cboxPartPartition_SelectionChangeCommitted(sender As Object, e As EventArgs) Handles cboxPartPartition.SelectionChangeCommitted
        For Each currentcbox As ComboBox In gb_ExcelInfo.Controls.OfType(Of ComboBox).Except({cboxPartPartition, cboxActiveSheet})
            If currentcbox.SelectedIndex = cboxPartPartition.SelectedIndex Then
                lbl_duplicate.Visible = True
                btnRead.Enabled = False
                Exit Sub
            End If
        Next
        lbl_duplicate.Visible = False
        btnRead.Enabled = True
    End Sub

    Private Sub chkbox_GetHeight_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_GetHeight.CheckedChanged

        If chkbox_GetHeight.Checked = True Then

            cbox_Height.Enabled = False
            cbox_Height.SelectedIndex = -1
        Else

            cbox_Height.Enabled = True

        End If

    End Sub

    Private Sub chkbox_RefDesPartitions_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_RefDesPartitions.CheckedChanged

        If chkbox_RefDesPartitions.Checked = True Then

            chkbox_WorksheetName.Enabled = False
            chkbox_WorksheetName.Checked = False
            cboxActiveSheet.Enabled = False
            cboxPartPartition.Enabled = False
        Else
            chkbox_WorksheetName.Enabled = True
            cboxActiveSheet.Enabled = True
            cboxPartPartition.Enabled = True

        End If

    End Sub

    Private Sub chkbox_WorksheetName_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_WorksheetName.CheckedChanged

        If chkbox_WorksheetName.Checked = True Then

            MsgBox("Be sure all worksheets use the same column scheme")
            chkbox_RefDesPartitions.Enabled = False
            chkbox_RefDesPartitions.Checked = False
            cboxActiveSheet.Enabled = False
            cboxPartPartition.Enabled = False
            cboxPartPartition.SelectedIndex = -1
            cboxActiveSheet.SelectedIndex = -1
        Else
            chkbox_RefDesPartitions.Enabled = True
            cboxActiveSheet.Enabled = True
            cboxPartPartition.Enabled = True

        End If

    End Sub

    Private Sub chkboxMultiThread_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxMultiThread.CheckedChanged
        If chkboxMultiThread.Checked = True Then
            tsm_Threads.Enabled = True
        Else
            tsm_Threads.Enabled = False
        End If
    End Sub

    Private Sub frmBuildFromExcel_FormClosing(sender As System.Object, e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
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

    Private Sub frmBuildFromExcel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Location = New System.Drawing.Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eUpdateThreads, AddressOf UpdateThreadCount
        AddHandler eBuildComplete, AddressOf BuildComplete
        AddHandler eBuildFailed, AddressOf BuildFailed
        AddHandler eUpdateMainParts, AddressOf UpdateMainParts
        AddHandler eUpdateComplete, AddressOf UpdateComplete
        AddHandler ePartitionResults, AddressOf LogPartitionResults

        Try
            xlsApp = GetObject(, "Excel.Application")
            'MsgBox(excelApp.ActiveWorkbook.FullName())
            xlsBook = xlsApp.ActiveWorkbook
            If Not IsNothing(xlsBook) Then

                tboxWorkbook.Text = Path.GetFileName(xlsBook.Name)

                For Each sheet In xlsBook.Worksheets

                    cboxActiveSheet.Items.Add(sheet.name)

                Next

                xlsSheet = xlsBook.ActiveSheet

                If xlsBook.Worksheets.Count = 1 Then

                    cboxActiveSheet.SelectedIndex = 0
                Else

                    cboxActiveSheet.SelectedIndex = -1

                End If

                cbox_CellName.SelectedIndex = -1
                cbox_PartNumber.SelectedIndex = -1
                cbox_SymbolName.SelectedIndex = -1

                gb_ExcelInfo.Enabled = True
                gb_Options.Enabled = True
                ts_Status.Text = "Provided the needed information..."
            End If
        Catch ex As Exception
            WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
        End Try

    End Sub

    Private Sub LogBadPart(aalPart As AAL.Part, Optional errMsg As String = "")

        SyncLock errLock

            If Not String.IsNullOrEmpty(errMsg) Then
                Dim dic_Parts As Dictionary(Of String, List(Of String))
                Dim l_Info As List(Of String) = Nothing

                If dicBadParts.ContainsKey(aalPart.Partition.Trim) Then
                    dic_Parts = dicBadParts.Item(aalPart.Partition.Trim)
                Else
                    dic_Parts = New Dictionary(Of String, List(Of String))
                    l_Info = New List(Of String)
                End If

                If dic_Parts.ContainsKey(aalPart.Number) Then
                    l_Info = dic_Parts.Item(aalPart.Number)
                Else
                    l_Info = New List(Of String)
                End If

                If Not l_Info.Contains(errMsg) Then
                    l_Info.Add(errMsg)
                End If

                dic_Parts.Item(aalPart.Number) = l_Info

                dicBadParts.Item(aalPart.Partition.Trim) = dic_Parts
            End If

            Dim dicBadPartData As Dictionary(Of String, AAL.Part)

            If dicBadPartitionData.ContainsKey(aalPart.Partition.Trim) Then
                dicBadPartData = dicBadPartitionData.Item(aalPart.Partition.Trim)
                dicBadPartData.Item(aalPart.Number) = aalPart
            Else
                dicBadPartData = New Dictionary(Of String, AAL.Part)
                dicBadPartData.Add(aalPart.Number, aalPart)
            End If

            dicBadPartitionData.Item(aalPart.Partition.Trim) = dicBadPartData
        End SyncLock

    End Sub

    Private Sub LogModifiedPart(aalPart As AAL.Part, Msg As String)

        SyncLock modifyLock
            Dim dic_Parts As Dictionary(Of String, List(Of String))
            Dim l_Info As List(Of String) = Nothing

            If dicModifiedParts.ContainsKey(aalPart.Partition.Trim) Then
                dic_Parts = dicModifiedParts.Item(aalPart.Partition.Trim)
            Else
                dic_Parts = New Dictionary(Of String, List(Of String))
                l_Info = New List(Of String)
            End If

            If dic_Parts.ContainsKey(aalPart.Number) Then
                l_Info = dic_Parts.Item(aalPart.Number)
            Else
                l_Info = New List(Of String)
            End If

            l_Info.Add(Msg)

            dic_Parts.Item(aalPart.Number) = l_Info

            dicModifiedParts.Item(aalPart.Partition.Trim) = dic_Parts
        End SyncLock

    End Sub

    Private Sub LogPart(ByVal logItem As BuildPDBLogItem)
        SyncLock logLock

            Try

                If logItem.success Then
                    'partitionPartsBuilt += 1

                    'dicPDBParts.Add(partnumber, pdbPartition.Name)

                    If dicSuccessfulByPartition.Keys.Contains(pdbPartition.Name) Then
                        dicSuccessfulByPartition.Item(pdbPartition.Name).Add(logItem.PN)
                    Else
                        dicSuccessfulByPartition.Add(pdbPartition.Name, New List(Of String))
                        dicSuccessfulByPartition.Item(pdbPartition.Name).Add(logItem.PN)
                    End If

                    Dim bSavedPart As Boolean

                    If chkbox_SaveEachPart.Checked Or chkboxMultiThread.Checked = False Then
                        Try
                            pedApp.SaveActiveDatabase()
                            bSavedPart = True
                        Catch ex As Exception
                            If dic_UnableToSave.Keys.Contains(pdbPartition.Name) Then
                                dic_UnableToSave.Item(pdbPartition.Name).Add(logItem.PN)
                            Else
                                dic_UnableToSave.Add(pdbPartition.Name, New List(Of String))
                                dic_UnableToSave.Item(pdbPartition.Name).Add(logItem.PN)
                            End If

                            iPartsFailed += 1
                            bSavedPart = False
                            WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
                            'dicLogReport.Remove(pdbPartition.Name)
                        Finally
                            If bSavedPart Then
                                RaiseEvent eUpdateMainParts(pdbPartition.Name, logItem.PN)
                            End If
                        End Try
                    Else
                        iPartsBuilt += 1
                    End If
                Else

                    If dicLogReport.ContainsKey(pdbPartition.Name) Then
                        dicLogReport.Item(pdbPartition.Name).Add(logItem)
                    Else

                        Dim alLogNew As New ArrayList()
                        alLogNew.Add(logItem)
                        dicLogReport.Add(pdbPartition.Name, alLogNew)

                    End If

                    iPartsFailed += 1
                End If
            Catch ex As Exception
                Dim message As String = ex.ToString
                WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
            End Try

        End SyncLock
    End Sub

    Private Function LogPartInfo(aalPart As AAL.Part) As String()

        SyncLock getPartInfoLock
            Dim dicPartInputData As Dictionary(Of String, Array)

            If dicInputData.ContainsKey(aalPart.Partition) Then
                dicPartInputData = dicInputData.Item(aalPart.Partition)
            Else
                dicPartInputData = New Dictionary(Of String, Array)
                dicInputData.Add(aalPart.Partition, dicPartInputData)
            End If

            If dicPartInputData.ContainsKey(aalPart.Number) Then
                Return dicPartInputData.Item(aalPart.Number)
            Else
                Dim partInfo(4) As String
                dicPartInputData.Add(aalPart.Number, partInfo)
                Return partInfo
            End If
        End SyncLock

    End Function

    Private Function LogPartInfo(aalPart As AAL.Part, partInfo As String()) As String()

        SyncLock inputLock
            Dim dicPartInputData As Dictionary(Of String, Array)

            If dicInputData.ContainsKey(aalPart.Partition) Then
                dicPartInputData = dicInputData.Item(aalPart.Partition)
            Else
                dicPartInputData = New Dictionary(Of String, Array)
                dicInputData.Add(aalPart.Partition, dicPartInputData)
            End If

            dicPartInputData.Item(aalPart.Number) = partInfo
            dicInputData.Item(aalPart.Partition) = dicPartInputData
        End SyncLock

    End Function

    Private Sub LogPartitionResults(partition As String, time As Decimal, totalParts As Integer, partsBuilt As Integer, partsFailed As Integer)
        If Me.InvokeRequired Then

            Dim d As New d_PartitionResults(AddressOf LogPartitionResults)
            Me.Invoke(d, New Object() {partition, time, totalParts, partsBuilt, partsFailed})
        Else

            Dim t As TimeSpan = TimeSpan.FromMilliseconds(time)
            Dim answer As String = String.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds)

            dgvResults.Rows.Add(New String() {partition, answer, totalParts, partsBuilt, partsFailed})
        End If
    End Sub

    Private Function newReadPart(partition As String, PN As String) As AAL.Part
        SyncLock newPartLock
            Dim aalPartition As AAL.PartPartition
            If dicPartitionData.ContainsKey(partition) Then
                aalPartition = dicPartitionData.Item(partition)
                If aalPartition.ContainsKey(PN) Then
                    Return aalPartition.Item(PN)
                End If
            End If

            Dim aalpart As New AAL.Part
            aalpart.Number = PN
            aalpart.Partition = partition
            Return aalpart
        End SyncLock

    End Function

    Private Sub OptimizeMappings()
        RaiseEvent eUpdateStatus("Optimizing part mappings...")
        For Each aalPartition As AAL.PartPartition In dicPartitionData.Values

            Dim dicMappings As New Dictionary(Of String, AAL.Part)

            For Each aalPart As AAL.Part In aalPartition.Parts

                If dicMappings.ContainsKey(aalPart.HashCode) Then
                    dicMappings(aalPart.HashCode()).AssociatedParts.Add(aalPart.Number)
                Else
                    dicMappings(aalPart.HashCode()) = aalPart
                End If

            Next

            dicMappingData.Item(aalPartition.Name) = dicMappings

        Next

    End Sub

    Private Sub PartitionToLog(partition As String)
        Dim partitionWS As Worksheet

        With xlsBook
            Dim worksheetList As New List(Of String)
            For Each worksheet As Worksheet In .Worksheets
                worksheetList.Add(worksheet.Name)
            Next

            Dim sheetName As String

            If (partition & "_Errs").Length > 30 Then
                sheetName = partition.Substring(0, 18) & "_Errs"
            Else
                sheetName = partition & "_Errs"
            End If

            sheetName = sheetName.Replace("\", "_")

            If Not worksheetList.Contains(sheetName) Then
                partitionWS = .Sheets.Add()
                partitionWS.Name = sheetName.Replace("/", "_")
            Else
                partitionWS = .Sheets.Item(sheetName)

            End If
        End With

        Dim logFile As String = frmMain.librarydata.LogPath & "Build PDB from Excel"

        If chkboxLogPerPartition.Checked Then
            logFile = logFile & " - " & partition
        End If

        logFile = logFile & ".log"

        If chkboxLogPerPartition.Checked Then
            If My.Computer.FileSystem.FileExists(logFile) Then

                My.Computer.FileSystem.DeleteFile(logFile)

            End If
        End If

        Dim tsvFile As String = frmMain.librarydata.LogPath & "Build PDB from Excel"

        If chkboxLogPerPartition.Checked Then
            tsvFile = tsvFile & " - " & partition
        End If

        tsvFile = tsvFile & ".tsv"

        Dim partIter As Integer = 1

        Try
            Using writer As StreamWriter = New StreamWriter(logFile, True, System.Text.Encoding.ASCII)
                Using tsv As StreamWriter = New StreamWriter(tsvFile, True, System.Text.Encoding.ASCII)
                    writer.WriteLine(partition & ": ")

                    For Each att As BuildPDBLogItem In dicLogReport(partition)

                        If (att.success = True) Then

                            tsv.WriteLine(partition & vbTab & att.PN & vbTab & "Success")

                            Continue For
                        End If

                        writer.WriteLine(vbTab & att.PN)
                        If att.Errors.Count > 0 Then

                            writer.WriteLine(vbTab & vbTab & "Errors:")

                            'If chkbox_WorksheetName.Checked Then
                            '    With xlsBook
                            '        .Sheets(partition & "_BuildErrors").Range(col_PartNumber & partitionIter).Value = "'" & att(0)
                            '        .Sheets(partition & "_BuildErrors").Range(col_Cell & partitionIter).Value = "'" & dicInputData.Item(partition).Item(att(0))(3)
                            '        .Sheets(partition & "_BuildErrors").Range(col_Symbol & partitionIter).Value = "'" & dicInputData.Item(partition).Item(att(0))(2)
                            '        If Not IsNothing(col_Height) Then
                            '            .Sheets(partition & "_BuildErrors").Range(col_Height & partitionIter).Value = "'" & dicInputData.Item(partition).Item(att(0))(1)
                            '        End If
                            '        If Not IsNothing(col_RefDes) Then
                            '            .Sheets(partition & "_BuildErrors").Range(col_RefDes & partitionIter).Value = "'" & dicInputData.Item(partition).Item(att(0))(0)
                            '        End If
                            '    End With
                            'Else
                            partitionWS.Range(col_PartNumber & partIter).Value = "'" & att.PN
                            partitionWS.Range(col_Partition & partIter).Value = "'" & partition
                            partitionWS.Range(col_Cell & partIter).Value = "'" & dicInputData.Item(partition).Item(att.PN)(3)
                            partitionWS.Range(col_Symbol & partIter).Value = "'" & dicInputData.Item(partition).Item(att.PN)(2)
                            If Not IsNothing(col_Height) Then
                                partitionWS.Range(col_Height & partIter).Value = "'" & dicInputData.Item(partition).Item(att.PN)(1)
                            End If
                            If Not IsNothing(col_RefDes) Then
                                partitionWS.Range(col_RefDes & partIter).Value = "'" & dicInputData.Item(partition).Item(att.PN)(0)
                            End If

                            'End If

                            For Each err As String In att.Errors
                                writer.WriteLine(vbTab & vbTab & vbTab & err)

                                tsv.WriteLine(partition & vbTab & att.PN & vbTab & "Error" & vbTab & err)

                            Next

                            If dic_UnableToSave.Keys.Contains(partition) Then
                                If dic_UnableToSave.Item(partition).Contains(att.PN) Then

                                    writer.WriteLine(vbTab & vbTab & vbTab & "Unable to save part into active database.")

                                End If
                            End If

                            writer.WriteLine()

                            partIter += 1
                        End If

                        If att.Warnings.Count > 0 Then

                            writer.WriteLine(vbTab & vbTab & "Warnings:")

                            For Each wrn As String In att.Warnings

                                writer.WriteLine(vbTab & vbTab & vbTab & wrn)
                                tsv.WriteLine(partition & vbTab & att.PN & vbTab & "Warning" & vbTab & wrn)
                            Next

                            writer.WriteLine()

                        End If

                        If att.Notes.Count > 0 Then

                            writer.WriteLine(vbTab & vbTab & "Notes:")

                            For Each note As String In att.Notes

                                writer.WriteLine(vbTab & vbTab & vbTab & note)
                                tsv.WriteLine(partition & vbTab & att.PN & vbTab & "Note" & vbTab & note)
                            Next

                            writer.WriteLine()

                        End If

                        If att.AlternateSymbols.Count > 0 Then

                            writer.WriteLine(vbTab & vbTab & "Alternate Symbols:")

                            For Each symbol As String In att.AlternateSymbols

                                writer.WriteLine(vbTab & vbTab & vbTab & symbol)

                            Next

                            writer.WriteLine()

                            att.AlternateSymbols.Clear()

                        End If
                        'partitionIter += 1
                    Next
                End Using

                writer.WriteLine()
                If (chkboxLogPerPartition.Checked) Then
                    writer.WriteLine("Successfully Built: " & partitionPartsBuilt)
                    If dicBadParts.ContainsKey(partition) Then
                        writer.WriteLine("Failed to Build: " & partitionPartsFailed + dicBadParts.Item(partition).Count)
                    Else
                        writer.WriteLine("Failed to Build: " & partitionPartsFailed)
                    End If
                End If

            End Using
        Catch ex As Exception
            Dim message As String = ex.ToString
            WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
        End Try

    End Sub

    Private Sub ReadExcel()

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "Read Excel for PDB.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "Read Excel for PDB.log")

        End If

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "Build PDB from Excel - Bad Part Data.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "Build PDB from Excel - Bad Part Data.log")

        End If

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "Build PDB from Excel - Modified Part Data.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "Build PDB from Excel - Modified Part Data.log")

        End If

        If (IsNothing(xlsApp)) Then
            Try
                xlsApp = GetObject(, "Excel.Application")
                xlsBook = xlsApp.ActiveWorkbook
            Catch ex As Exception
                WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
            End Try
        End If

        'frmMain.swLog = My.Computer.FileSystem.OpenTextFileWriter(frmmain.librarydata.LogPath & "Read Excel for PDB.log", True, Encoding.ASCII)  'Create Config File swOutput Stream
        If chkbox_WorksheetName.Checked = True Then

            For Each xlsSheet As Excel.Worksheet In xlsBook.Worksheets

                ReadExcelWorkbook(xlsSheet)

                'Marshal.FinalReleaseComObject(xlsSheet)

            Next
        Else
            ReadExcelWorkbook(xlsSheet)
        End If

        OptimizeMappings()

        RaiseEvent eUpdateComplete(0)

    End Sub

    Private Sub ReadExcelWorkbook(ByVal xls_Sheet As Excel.Worksheet)

        xls_Sheet.Select()

        Dim i As Integer = 1

        If cbox_ignoreHeader.Checked = True Then i = 2

        Dim sb_Parts As New StringBuilder

        Do While Not IsNothing(xls_Sheet.Range(col_PartNumber & i).Value)

            RaiseEvent eUpdateStatus("Reading " & xls_Sheet.Name & " row: " & i)

            Dim bProblem As Boolean = False
            Dim aalPart As New AAL.Part

            If chkbox_WorksheetName.Checked = True Then
                aalPart.Partition = xls_Sheet.Name.Trim
            Else
                aalPart.Partition = xls_Sheet.Range(col_Partition & i).Value.trim
                xlsSheetName = xlsSheet.Name
            End If

            If IsNothing(aalPart.Partition.Trim) Then bProblem = True

            aalPart.Number = xls_Sheet.Range(col_PartNumber & i).Value
            aalPart.Number = aalPart.Number.TrimEnd(New Char() {"\r", "\n", "\s", "\t"})

            Dim aalPartition As AAL.PartPartition
            Dim dicBadPartData As Dictionary(Of String, AAL.Part)

            Dim sExistingPartition As String
            If frmMain.librarydata.PartList.ContainsKey(aalPart.Number) And chkbox_RebuildParts.Checked = False Then
                sExistingPartition = frmMain.librarydata.PartList.Item(aalPart.Number)
                Dim dic_Parts As Dictionary(Of String, List(Of String))
                Dim l_Info As List(Of String) = Nothing
                If dicBadParts.ContainsKey(aalPart.Partition.Trim) Then
                    dic_Parts = dicBadParts.Item(aalPart.Partition.Trim)
                Else
                    dic_Parts = New Dictionary(Of String, List(Of String))
                    l_Info = New List(Of String)
                End If

                If dic_Parts.ContainsKey(aalPart.Number) Then
                    l_Info = dic_Parts.Item(aalPart.Number)
                Else
                    l_Info = New List(Of String)
                End If

                l_Info.Add("[Invalid Input] Duplicate part number (Found in partition: " & sExistingPartition.Trim & ").")

                dic_Parts.Item(aalPart.Number) = l_Info

                dicBadParts.Item(aalPart.Partition.Trim) = dic_Parts

                bProblem = True

                If dicBadPartitionData.ContainsKey(aalPart.Partition.Trim) Then
                    dicBadPartData = dicBadPartitionData.Item(aalPart.Partition.Trim)
                    dicBadPartData.Item(aalPart.Number) = aalPart
                Else
                    dicBadPartData = New Dictionary(Of String, AAL.Part)
                    dicBadPartData.Add(aalPart.Number, aalPart)
                End If

                dicBadPartitionData.Item(aalPart.Partition.Trim) = dicBadPartData

                i = i + 1

                Continue Do
            End If

            ' dicPartData stores information about the part such as the partition, the name of the
            ' part, the number of pins, etc. It maps the string literal to the actual part object

            If dicPartitionData.ContainsKey(aalPart.Partition.Trim) Then
                aalPartition = dicPartitionData.Item(aalPart.Partition.Trim)
                If aalPartition.ContainsKey(aalPart.Number) Then
                    aalPart = aalPartition.Item(aalPart.Number)
                End If
            Else
                aalPartition = New AAL.PartPartition
                aalPartition.Name = aalPart.Partition.Trim
            End If

            Dim dicPartInputData As Dictionary(Of String, Array)
            Dim partInfo(4) As String

            If dicInputData.ContainsKey(aalPart.Partition) Then
                dicPartInputData = dicInputData.Item(aalPart.Partition)
            Else
                dicPartInputData = New Dictionary(Of String, Array)
                dicInputData.Add(aalPart.Partition, dicPartInputData)
            End If

            If dicPartInputData.ContainsKey(aalPart.Number) Then
                partInfo = dicPartInputData.Item(aalPart.Number)
            Else
                dicPartInputData.Add(aalPart.Number, partInfo)
            End If

            If Not IsNothing(col_RefDes) Then
                Dim refDes As String = xls_Sheet.Range(col_RefDes & i).Value
                For iRef = 1 To Len(refDes)
                    If (Not IsNumeric(Mid(refDes, iRef, 1))) Or (Mid(refDes, iRef, 1) = "_") Then
                        aalPart.RefDes = aalPart.RefDes() & Mid(refDes, iRef, 1)
                    End If
                Next iRef
                partInfo(0) = aalPart.RefDes
            Else
                partInfo(0) = ""
            End If

            If Not IsNothing(col_Height) Then
                If Not String.IsNullOrEmpty(xls_Sheet.Range(col_Height & i).Value) Then
                    If Regex.IsMatch(xls_Sheet.Range(col_Height & i).Value, "^[0-9 ]+$") Then
                        aalPart.Height = xls_Sheet.Range(col_Height & i).Value
                    End If
                End If
                partInfo(1) = aalPart.Height
            Else
                partInfo(1) = ""
            End If

            Dim xlsSymValue As String = xls_Sheet.Range(col_Symbol & i).Value

            If Not IsNothing(xlsSymValue) Then
                partInfo(2) = xlsSymValue
            Else
                partInfo(2) = ""
            End If

            If String.IsNullOrEmpty(xlsSymValue) Then
                Dim dic_Parts As Dictionary(Of String, List(Of String))
                Dim l_Info As List(Of String) = Nothing
                If dicBadParts.ContainsKey(aalPart.Partition.Trim) Then
                    dic_Parts = dicBadParts.Item(aalPart.Partition.Trim)
                Else
                    dic_Parts = New Dictionary(Of String, List(Of String))
                    l_Info = New List(Of String)
                End If

                If dic_Parts.ContainsKey(aalPart.Number) Then
                    l_Info = dic_Parts.Item(aalPart.Number)
                Else
                    l_Info = New List(Of String)
                End If

                l_Info.Add("[Invalid Input] No symbol defined.")

                dic_Parts.Item(aalPart.Number) = l_Info

                dicBadParts.Item(aalPart.Partition.Trim) = dic_Parts

                bProblem = True
            Else
                If xlsSymValue.Contains(";") Then
                    Dim sSymSplit As String() = Split(xls_Sheet.Range(col_Symbol & i).Value, ";")
                    For Each sSymbol As String In sSymSplit
                        If Not String.IsNullOrEmpty(sSymbol) Then
                            bProblem = addSymbol(sSymbol, bProblem, aalPart)
                        End If

                    Next

                ElseIf xlsSymValue.Contains(",") Then
                    Dim sSymSplit As String() = Split(xls_Sheet.Range(col_Symbol & i).Value, ",")

                    For Each sSymbol As String In sSymSplit
                        If Not String.IsNullOrEmpty(sSymbol) Then

                            bProblem = addSymbol(sSymbol, bProblem, aalPart)
                        End If
                    Next
                Else

                    Dim sSymbol As String = xls_Sheet.Range(col_Symbol & i).Value

                    bProblem = addSymbol(sSymbol, bProblem, aalPart)
                End If
            End If

            Dim xlsCellValue As String = xls_Sheet.Range(col_Cell & i).Value

            If Not IsNothing(xlsCellValue) Then
                partInfo(3) = xlsCellValue
            Else
                partInfo(3) = ""
            End If

            If String.IsNullOrEmpty(xlsCellValue) Then
                Dim dic_Parts As Dictionary(Of String, List(Of String))
                Dim l_Info As List(Of String) = Nothing
                If dicBadParts.ContainsKey(aalPart.Partition.Trim) Then
                    dic_Parts = dicBadParts.Item(aalPart.Partition.Trim)
                Else
                    dic_Parts = New Dictionary(Of String, List(Of String))
                    l_Info = New List(Of String)
                End If

                If dic_Parts.ContainsKey(aalPart.Number) Then
                    l_Info = dic_Parts.Item(aalPart.Number)
                Else
                    l_Info = New List(Of String)
                End If

                l_Info.Add("[Invalid Input] No cell defined.")

                dic_Parts.Item(aalPart.Number) = l_Info

                dicBadParts.Item(aalPart.Partition.Trim) = dic_Parts

                bProblem = True
            Else
                If xlsCellValue.Contains(";") Then
                    Dim sCellSplit As String() = Split(xls_Sheet.Range(col_Cell & i).Value, ";")
                    For Each sCell In sCellSplit
                        If Not String.IsNullOrEmpty(sCell) Then
                            bProblem = AddCell(sCell, bProblem, aalPart)
                        End If

                    Next

                ElseIf xlsCellValue.Contains(",") Then
                    Dim sCellSplit As String() = Split(xls_Sheet.Range(col_Cell & i).Value, ",")
                    For Each sCell In sCellSplit
                        If Not String.IsNullOrEmpty(sCell) Then
                            bProblem = AddCell(sCell, bProblem, aalPart)
                        End If

                    Next
                Else

                    Dim sCell As String = xls_Sheet.Range(col_Cell & i).Value
                    bProblem = AddCell(sCell, bProblem, aalPart)
                End If
            End If

            i = i + 1

            If (aalPart.Cells.Count > 0) And aalPart.Symbols.Count > 0 Then
                aalPartition.Item(aalPart.Number) = aalPart
                dicPartitionData.Item(aalPart.Partition.Trim) = aalPartition

                Dim dic_Parts As Dictionary(Of String, List(Of String))
                If dicBadParts.ContainsKey(aalPart.Partition.Trim) Then
                    dic_Parts = dicBadParts.Item(aalPart.Partition.Trim)
                    If (dic_Parts.ContainsKey(aalPart.Number)) Then
                        dic_Parts.Remove(aalPart.Number)
                        dicBadParts.Item(aalPart.Partition) = dic_Parts
                    End If
                End If
            Else
                If dicBadPartitionData.ContainsKey(aalPart.Partition.Trim) Then
                    dicBadPartData = dicBadPartitionData.Item(aalPart.Partition.Trim)
                    dicBadPartData.Item(aalPart.Number) = aalPart
                Else
                    dicBadPartData = New Dictionary(Of String, AAL.Part)
                    dicBadPartData.Add(aalPart.Number, aalPart)
                End If

                dicBadPartitionData.Item(aalPart.Partition.Trim) = dicBadPartData
            End If

            dicPartInputData.Item(aalPart.Number) = partInfo
            dicInputData.Item(aalPart.Partition) = dicPartInputData
        Loop

    End Sub

    Private Sub UpdateComplete(partsNotConsidered As Integer)
        If Me.InvokeRequired Then
            LogFile = frmMain.librarydata.LogPath & " Build PDB from Excel - Bad Input Data.log"

            If File.Exists(LogFile) Then
                File.Delete(LogFile)
            End If

            If dicBadParts.Count > 0 Then
                Dim dicLogReport As New Dictionary(Of String, ArrayList)

                For Each Partition As String In dicBadParts.Keys

                    Dim dicspecificPartition As Dictionary(Of String, List(Of String)) = dicBadParts.Item(Partition)
                    Dim alLog As New ArrayList
                    For Each partnumber As String In dicspecificPartition.Keys
                        Dim errorList As List(Of String) = dicspecificPartition.Item(partnumber)
                        Dim logItem As New BuildPDBLogItem(False, partnumber, errorList, New List(Of String), New List(Of String), New ArrayList)
                        If dicLogReport.Keys.Contains(Partition) Then
                            alLog = dicLogReport.Item(Partition)
                            alLog.Add(logItem)
                            dicLogReport.Item(Partition) = alLog
                        Else
                            Dim alLogNew As New ArrayList()
                            alLogNew.Add(logItem)
                            dicLogReport.Add(Partition, alLogNew)
                        End If
                    Next

                Next

                partsNotConsidered = WriteLogFiles(dicLogReport, 0, 0)
            End If

            Dim d As New d_UpdateComplete(AddressOf UpdateComplete)
            Me.Invoke(d, New Object() {partsNotConsidered})
        Else

            WaitGif.Enabled = False
            tsm_Threads.Visible = False
            For Each kvp As KeyValuePair(Of String, AAL.PartPartition) In dicPartitionData

                Dim oParentNode As New TreeNode

                oParentNode.Text = kvp.Key

                For Each aalPart As AAL.Part In kvp.Value.Parts

                    Dim oPartNode As New TreeNode
                    oPartNode.Text = aalPart.Number

                    Dim aalSymbolLabelNode As New TreeNode
                    aalSymbolLabelNode.Text = "Symbol(s):"

                    For Each sSymbol In aalPart.Symbols.Keys.ToList
                        Dim aalSymbolNode As New TreeNode
                        aalSymbolNode.Text = sSymbol
                        aalSymbolLabelNode.Nodes.Add(aalSymbolNode)
                    Next

                    aalSymbolLabelNode.Expand()

                    oPartNode.Nodes.Add(aalSymbolLabelNode)

                    Dim oCellLabelNode As New TreeNode
                    oCellLabelNode.Text = "Cell(s):"

                    For Each sCell In aalPart.Cells.Keys.ToList
                        Dim oCellNode As New TreeNode
                        oCellNode.Text = sCell
                        oCellLabelNode.Nodes.Add(oCellNode)
                    Next

                    oCellLabelNode.Expand()

                    oPartNode.Nodes.Add(oCellLabelNode)

                    oParentNode.Nodes.Add(oPartNode)

                    iPartCount += 1

                Next

                oParentNode.Expand()

                tv_Parts.Nodes.Add(oParentNode)

            Next

            If partsNotConsidered > 0 Then

                Dim reply As DialogResult = MessageBox.Show(partsNotConsidered & " parts are not being considered because of missing data. Would you like to view the results?", "Finished",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB from Excel.log")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from Excel.log")
                End If
            End If

            If dicInputData.Count > 0 Then
                tv_Parts.Visible = True
            End If

            If dicModifiedParts.Count > 0 Then

                Dim replyCell As DialogResult = MessageBox.Show("Excel file has been read, but " & iModifiedPartsFromExcel & " parts needed to be modified vs the data found in the spreadsheet. Would you like to view the results?", "Finished",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If replyCell = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB from Excel - Modified Part Data")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from Excel - Modified Part Data.log")
                End If
            End If
            ts_Status.Text = "Read Complete: " & iPartCount & " eligible parts found."
            plAction.Visible = True

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
                frmMain.librarydata.PartsByPartition.Remove(Partition)
            Else
                l_Parts = New List(Of String)
            End If

            l_Parts.Add(Part)
            frmMain.librarydata.PartsByPartition.Add(Partition, l_Parts)
            Dim l_Partition As New List(Of String)
            If Not frmMain.librarydata.PartList.ContainsKey(Part) Then
                frmMain.librarydata.PartList.Add(Part, Partition)
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

    Private Sub UpdateThreadCount()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateThreads(AddressOf UpdateThreadCount)
            Me.Invoke(d)
        Else
            tsm_Threads.Text = "Active Threads: " & ActiveThreads
        End If
    End Sub

    Private Function UpdateTimer() As Object

        If Me.InvokeRequired Then
            While (timerTotal.IsRunning)
                Dim d As New d_UpdateTimer(AddressOf UpdateTimer)
                Me.Invoke(d)
                Thread.Sleep(5000)
            End While
        Else

            Dim t As TimeSpan = TimeSpan.FromMilliseconds(timerTotal.ElapsedMilliseconds)
            Dim answer As String = String.Format("{0:D2}h:{1:D2}m:{2:D2}s:{3:D3}ms",
                        t.Hours,
                        t.Minutes,
                        t.Seconds,
                        t.Milliseconds)

            tsTimeTotal.Text = answer
        End If

    End Function

    Private Function WriteLogFiles(dicLogReport As Dictionary(Of String, ArrayList), iPartsFailed As Integer, iPartsBuilt As Integer) As Integer

        Dim partsNotConsidered As Integer = 0
        For Each Partition As String In dicBadParts.Keys
            partsNotConsidered += dicBadParts.Item(Partition).Count
        Next

        Dim i As Integer = 1

        Dim partitionIter As Integer = 1
        Dim partIter As Integer = 1

        For Each kvp As KeyValuePair(Of String, ArrayList) In dicLogReport

            'Grab part number and part attributes:
            Dim Partition As String = kvp.Key
            Dim alLog As ArrayList = kvp.Value

            Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                writer.WriteLine(Partition & ":")
            End Using

            For Each logItem As BuildPDBLogItem In alLog

                Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                    writer.WriteLine(vbTab & logItem.PN)
                End Using

                If logItem.Errors.Count > 0 Then

                    Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                        writer.WriteLine(vbTab & vbTab & "Errors:")
                    End Using

                    Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                        For Each err As String In logItem.Errors
                            writer.WriteLine(vbTab & vbTab & vbTab & err)
                        Next
                    End Using

                    If dic_UnableToSave.Keys.Contains(Partition) Then
                        If dic_UnableToSave.Item(Partition).Contains(logItem.PN) Then
                            Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                                writer.WriteLine(vbTab & vbTab & vbTab & "Unable to save part into active database.")
                            End Using
                        End If
                    End If

                    Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                        writer.WriteLine()
                    End Using
                    partIter += 1
                End If

                If logItem.Warnings.Count > 0 Then

                    Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                        writer.WriteLine(vbTab & vbTab & "Warnings:")
                    End Using

                    For Each wrn As String In logItem.Warnings

                        Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                            writer.WriteLine(vbTab & vbTab & vbTab & wrn)
                        End Using

                    Next

                    Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                        writer.WriteLine()
                    End Using

                End If

                If logItem.Notes.Count > 0 Then

                    Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                        writer.WriteLine(vbTab & vbTab & "Notes:")
                    End Using
                    For Each note As String In logItem.Notes

                        Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                            writer.WriteLine(vbTab & vbTab & vbTab & note)
                        End Using

                    Next

                    Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                        writer.WriteLine()
                    End Using

                End If

                If logItem.AlternateSymbols.Count > 0 Then
                    Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                        writer.WriteLine(vbTab & vbTab & "Alternate Symbols:")
                    End Using
                    For Each symbol As String In logItem.AlternateSymbols

                        Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                            writer.WriteLine(vbTab & vbTab & vbTab & symbol)
                        End Using

                    Next
                    Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                        writer.WriteLine()
                    End Using
                End If
                partitionIter += 1
            Next
            partitionIter = 1
        Next

        Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
            writer.WriteLine()
            writer.WriteLine("Successfully Built: " & iPartsBuilt)
            writer.WriteLine("Failed to Build: " & iPartsFailed + partsNotConsidered)
        End Using

        Return partsNotConsidered
    End Function

    Private Sub WriterErrorToLog(errMsg As String)
        SyncLock errLock
            Dim logFile As String = frmMain.librarydata.LogPath & "Build PDB from Excel - General Errors.log"

            Using writer As StreamWriter = New StreamWriter(logFile, True, System.Text.Encoding.ASCII)
                writer.WriteLine(DateTime.Now() & " - " & errMsg)
            End Using
        End SyncLock

    End Sub

#End Region

End Class