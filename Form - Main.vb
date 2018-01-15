Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.Text.RegularExpressions
Imports Microsoft.Office.Interop
Imports Microsoft.Office.Interop.Excel
Imports System.Environment
Imports System.Xml
Imports System.Runtime.InteropServices

Public Class frmMain

#Region "Public Fields + Properties + Events + Delegates + Enums"

    Property b_PopulateMenus As Boolean = True

    Shared Property bConnectToLMC As Boolean = False

    Delegate Sub d_CellCount()

    'Delegates
    Delegate Sub d_OpenComplete()

    Delegate Sub d_PartCount()

    Delegate Sub d_PopulateCellMenu(dic_CellList_In As Dictionary(Of String, List(Of String)), dic_CellsByPartition_In As Dictionary(Of String, List(Of String)))

    Delegate Sub d_PopulateFileMenu()

    Delegate Sub d_PopulateLMCHistory(ByVal LibPath As String)

    Delegate Sub d_PopulateOtherMenu(dicPadstacksType As Dictionary(Of String, List(Of String)), dicPadstacks As Dictionary(Of String, String))

    Delegate Sub d_PopulatePartMenu(ByVal dic_PartList_In As Dictionary(Of String, String), ByVal dic_PartsByPartition_In As Dictionary(Of String, List(Of String)))

    Delegate Sub d_PopulateSymbolMenu(ByVal dic_SymbolList_In As Dictionary(Of String, List(Of String)), ByVal dic_SymbolsByPartition_In As Dictionary(Of String, List(Of String)))

    Delegate Sub d_ReadComplete()

    Delegate Sub d_ReportCells(ByVal CellsReport As Dictionary(Of String, AAL.CellPartition))

    Delegate Sub d_ReportParts(ByVal oPartitions As AAL.PartPartitions)

    Delegate Sub d_ReportSymbols(ByVal SymbolsReport As Dictionary(Of String, AAL.SymbolPartition))

    Delegate Sub d_SymbolCount()

    Delegate Sub d_SymbolNamesToExcel()

    Delegate Sub d_UpdateHealCount()

    Delegate Sub d_UpdateStatus(ByVal status As String)

    'Dictionary
    Property dicLMCHistory As New Dictionary(Of String, String)

    Property dicPartPropertyReport As New SortedDictionary(Of String, StringBuilder)

    Property dicSymPropertyReport As New SortedDictionary(Of String, StringBuilder)

    Event eExportSymbolNamesComplete()

    Event ePartHeightReportComplete()

    Event ePDBtoTypeTableComplete()

    Event ePopluateHistory(LibPath As String)

    Event ePopulateMenus()

    Event eReadPartPropertiesComplete()

    Event eReadSymbolPropertiesComplete()

    'Event
    'Event eOpenComplete()
    Event eUpdateStatus(status As String)

    Shared Property libApp As LibraryManager.LibraryManagerApp

    Shared Property libDoc As LibraryManager.IMGCLMLibrary

    Shared Property librarydata As New Data

    'Boolean
    Property lookatlog As Boolean = False

    Shared Property progID As String

    'String
    'Property LMCPath As String
    Property strSDD_HOME As String = Nothing

    Property tsm_Cell As New ToolStripMenuItem
    Property tsm_LibraryIntegrity As New ToolStripMenuItem
    Property tsm_LogFiles As New ToolStripMenuItem

    'Menu Items
    Property tsm_OpenExplorer As New ToolStripMenuItem

    Property tsm_Other As New ToolStripMenuItem
    Property tsm_PDB As New ToolStripMenuItem
    Property tsm_Symbol As New ToolStripMenuItem

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    Dim bHealPDB As Boolean = False
    Dim dicLogFiles As New Dictionary(Of String, String)
    Dim i_CellCount As Integer = 0

    'Integer
    Dim i_PartCount As Integer = 0

    Dim i_PartsLeftToHeal As Integer = 0
    Dim i_SymbolCount As Integer = 0

    'Objects
    Dim ini As IniFile

    Dim s_PreviousStatus As String
    Dim sublock As New Object
    Dim symbol As LibraryManager.MGCLMSymbol

    'Threads
    Dim tReadPDB, tReadSymbols, tReadCells, tReadPadstacks As Thread

    Dim tsm_ReadPDBtoNeut As New ToolStripMenuItem
    Dim VxVersion As String

#End Region

#Region "Public Methods"

    Public Sub closeLM()

        If Not IsNothing(libDoc) Then

            If Not bConnectToLMC = True Then

                libDoc = Nothing
                libApp.Quit()
                libApp = Nothing

            End If

        End If

    End Sub

    Public Sub ExitOnClick(ByVal sender As Object, ByVal e As EventArgs) Handles tsm_Exit.Click
        System.Windows.Forms.Application.Exit()
    End Sub

    Public Sub HealPDBClick(ByVal sender As Object, ByVal e As EventArgs)

        frmHealPDBwithExpedition.MdiParent = Me
        frmHealPDBwithExpedition.Show()
    End Sub

    Public Sub ModSymbolClick(ByVal sender As Object, ByVal e As EventArgs)

        frmModSymbols.MdiParent = Me
        frmModSymbols.Show()

    End Sub

    Public Sub OpenLMC(ByVal bOpenLMC As Boolean)

        If bOpenLMC = True Then

            closeLM()

            Try
                libApp = CreateObject("LibraryManager.Application" & "." & progID)
                libDoc = libApp.OpenLibrary(librarydata.LibPath)
            Catch ex As Exception
                MessageBox.Show("Could not open Library Manager...")
                MessageBox.Show(ex.Message.ToString())
                End
            End Try

            bConnectToLMC = False

        End If

        Try
            i_PartCount = 0
            i_CellCount = 0
            i_SymbolCount = 0

            libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL)

            If libDoc.FlowType = LibraryManager.MGCLMFlowModeType.kFLOW_DC Then
                librarydata.Type = Data.LibType.DC
            ElseIf libDoc.FlowType = LibraryManager.MGCLMFlowModeType.kFLOW_DX Then
                librarydata.Type = Data.LibType.DX
            End If

            Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg
            Dim pedDoc As MGCPCBPartsEditor.PartsDB

            'Creates a handle to the Parts Editor in Library Manager
            'pedApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")

            pedApp = libDoc.PartEditor
            pedDoc = pedApp.ActiveDatabaseEx

            For Each sProperty As String In pedDoc.AvailablePartProperties
                librarydata.PDBCommonProperties.Add(sProperty)
            Next

            ' close the editor
            'pedApp.Quit()
            pedApp.Quit()
            pedApp = Nothing
            pedDoc = Nothing

            librarydata.ReadCentLibPRP()

            Dim arMapCfgFile As String() = File.ReadAllLines(strSDD_HOME & "\standard\map.cfg")

            For Each line In arMapCfgFile

                Dim linesplit As String() = Regex.Split(line, "\s+\\=\s+")

                Dim Before As String = linesplit(0).Replace("""", String.Empty).Trim
                Dim After As String = linesplit(1).Replace("""", String.Empty).Trim

                If Not String.Empty = Before Then
                    If Not librarydata.PropertyMapping.ContainsKey(Before) Then librarydata.PropertyMapping.Add(Before, After)
                End If

            Next

            Dim arPDBTypeTablecafFile As String() = File.ReadAllLines(strSDD_HOME & "\standard\config\pcb\default\PDBTypeTable.caf")

            For Each line In arPDBTypeTablecafFile

                If line.StartsWith(".Ref") Then

                    Dim RefSplit As String() = Split(line, vbTab)

                    Dim RefDes As String = RefSplit(1).Replace("""", "")

                    librarydata.PDBType.Add(RefDes, RefSplit(3))

                End If

            Next

            Dim ErrorLog As Boolean = False

            RaiseEvent ePopluateHistory(librarydata.LibPath)

            librarydata.LogPath = libDoc.Path & "Logfiles\ALE\"

            If Not Directory.Exists(librarydata.LogPath) Then
                Directory.CreateDirectory(librarydata.LogPath)
            End If

            If b_PopulateMenus = True Then
                RaiseEvent ePopulateMenus()
            End If

            Dim oCentralLibInfo(2) As LibraryRead

            oCentralLibInfo(0) = New LibraryRead()
            AddHandler oCentralLibInfo(0).ePartsComplete, AddressOf ReadPartComplete
            AddHandler oCentralLibInfo(0).eAddPart, AddressOf PartCount
            oCentralLibInfo(0).libDoc = libDoc

            tReadPDB = New Thread(AddressOf oCentralLibInfo(0).ReadLMCPartPartitions)
            tReadPDB.Name = "Read PDB Thread"
            tReadPDB.IsBackground = True
            tReadPDB.Start()

            oCentralLibInfo(1) = New LibraryRead()
            AddHandler oCentralLibInfo(1).eSymbolsComplete, AddressOf ReadSymbolComplete
            AddHandler oCentralLibInfo(1).eAddSymbol, AddressOf SymbolCount
            oCentralLibInfo(1).libDoc = libDoc

            tReadSymbols = New Thread(AddressOf oCentralLibInfo(1).ReadLMCSymbolPartitions)
            tReadSymbols.Name = "Read Symbols Thread"
            tReadSymbols.IsBackground = True
            tReadSymbols.Start()

            oCentralLibInfo(2) = New LibraryRead()
            AddHandler oCentralLibInfo(2).eCellsComplete, AddressOf ReadCellComplete
            AddHandler oCentralLibInfo(2).eAddCell, AddressOf CellCount
            oCentralLibInfo(2).libDoc = libDoc

            tReadCells = New Thread(AddressOf oCentralLibInfo(2).ReadLMCCellPartitions)
            tReadCells.Name = "Read Cells Thread"
            tReadCells.IsBackground = True
            tReadCells.Start()

            Dim oReadPadstacks As New LibraryRead
            AddHandler oReadPadstacks.ePadstacksComplete, AddressOf ReadPadstacksComplete
            oReadPadstacks.libDoc = libDoc

            tReadPadstacks = New Thread(AddressOf oReadPadstacks.readpadstacks)
            tReadPadstacks.Name = "ReadPadstacks Thread"
            tReadPadstacks.IsBackground = True
            tReadPadstacks.Start()

            tReadCells.Join()
            tReadPDB.Join()
            tReadSymbols.Join()
            tReadPadstacks.Join()

            Dim integratedSum As Integer = 0
            For i As Integer = 0 To oCentralLibInfo.Count - 1
                integratedSum += oCentralLibInfo(i).reservedCellPartitions
                integratedSum += oCentralLibInfo(i).reservedPartPartitions
                integratedSum += oCentralLibInfo(i).reservedSymbolPartitions
            Next

            If integratedSum > 0 Then
                MessageBox.Show("Reserved partitions were found when reading the library." _
                                & vbNewLine & vbNewLine & "To apply any changes to the partitions, please " _
                                & "unreserve the partitions in xDM Library.", "Unreserved Partitions")
            End If
        Catch ex As Exception
            MessageBox.Show("Error: Open LMC" & Environment.NewLine & Environment.NewLine & ex.Message.ToString())
        End Try

    End Sub

    Public Sub OpenLogFile(Optional ByVal LogFile As String = Nothing)
        frmLogViewer.LogFile = LogFile
        frmLogViewer.LogPath = librarydata.LogPath
        frmLogViewer.MdiParent = Me
        frmLogViewer.Show()
    End Sub

    Public Sub PopulateLMCHistory(LibPath As String)

        If Me.InvokeRequired Then

            Dim d As New d_PopulateLMCHistory(AddressOf PopulateLMCHistory)
            Me.Invoke(d, New Object() {LibPath})
        Else

            tsm_OpenLMC.DropDownItems.Clear()

            Dim oOpenLMC As ToolStripItem = tsm_OpenLMC.DropDownItems.Add("Browse...")
            AddHandler oOpenLMC.Click, AddressOf tsm_BrowseLMC_Click
            AddHandler oOpenLMC.MouseHover, AddressOf ShowHistoryPath
            tsm_OpenLMC.DropDownItems.Add(oOpenLMC)

            tsm_OpenLMC.DropDownItems.Add(New ToolStripSeparator)

            Dim temp_History As New Dictionary(Of String, String)

            temp_History.Add(Path.GetFileName(librarydata.LibPath), librarydata.LibPath)
            Dim oNewLMC As ToolStripItem = tsm_OpenLMC.DropDownItems.Add(Path.GetFileName(librarydata.LibPath))
            AddHandler oNewLMC.Click, AddressOf OnHistoryClick
            AddHandler oNewLMC.MouseHover, AddressOf ShowHistoryPath
            AddHandler oNewLMC.MouseLeave, AddressOf ClearHistoryStatus

            If dicLMCHistory.Count = 5 Then
                Dim i As Integer = 0

                For Each kvp As KeyValuePair(Of String, String) In dicLMCHistory

                    If Not i = 0 And Not temp_History.ContainsKey(kvp.Key) Then
                        temp_History.Item(kvp.Key) = kvp.Value
                        Dim oLMCHistory As ToolStripItem = tsm_OpenLMC.DropDownItems.Add(kvp.Key)
                        AddHandler oLMCHistory.Click, AddressOf OnHistoryClick
                        AddHandler oLMCHistory.MouseHover, AddressOf ShowHistoryPath
                        AddHandler oLMCHistory.MouseLeave, AddressOf ClearHistoryStatus
                        tsm_OpenLMC.DropDownItems.Add(oLMCHistory)
                    End If

                Next
            Else

                For Each kvp As KeyValuePair(Of String, String) In dicLMCHistory

                    If Not temp_History.ContainsKey(kvp.Key) Then
                        temp_History.Item(kvp.Key) = kvp.Value
                        Dim oLMCHistory As ToolStripItem = tsm_OpenLMC.DropDownItems.Add(kvp.Key)
                        AddHandler oLMCHistory.Click, AddressOf OnHistoryClick
                        AddHandler oLMCHistory.MouseHover, AddressOf ShowHistoryPath
                        AddHandler oLMCHistory.MouseLeave, AddressOf ClearHistoryStatus
                        tsm_OpenLMC.DropDownItems.Add(oLMCHistory)
                    End If

                Next

            End If

            dicLMCHistory = temp_History

        End If

    End Sub

    Public Sub PopulateMenu()

        If Me.InvokeRequired Then

            Dim d As New d_PopulateFileMenu(AddressOf PopulateMenu)
            Me.Invoke(d)
        Else

            tsm_File.DropDownItems.Insert(2, New ToolStripSeparator)

            tsm_OpenExplorer.Text = "Open in &Explorer Window"
            tsm_File.DropDownItems.Insert(3, tsm_OpenExplorer)
            AddHandler tsm_OpenExplorer.Click, AddressOf OtherOpenExplorer
            tsm_OpenExplorer.Enabled = False

            tsm_LibraryIntegrity.Text = "Check Library &Integrity"
            tsm_File.DropDownItems.Insert(4, tsm_LibraryIntegrity)
            AddHandler tsm_LibraryIntegrity.Click, AddressOf runIntegrityCheck
            tsm_LibraryIntegrity.Enabled = False

            tsm_File.DropDownItems.Insert(5, New ToolStripSeparator)
            tsm_File.DropDownItems.Insert(6, tsm_LogFiles)
            tsm_LogFiles.Text = "Log Files"
            AddHandler tsm_LogFiles.Click, AddressOf OnLogFileClick

            tsm_Symbol.Text = "&Symbol"
            MenuStrip.Items.Insert(1, tsm_Symbol)
            tsm_Symbol.Enabled = False

            tsm_PDB.Text = "&Parts"
            MenuStrip.Items.Insert(2, tsm_PDB)
            tsm_PDB.Enabled = False

            tsm_Cell.Text = "&Cell"
            MenuStrip.Items.Insert(3, tsm_Cell)
            tsm_Cell.Enabled = False

            tsm_Other.Text = "&Other"
            MenuStrip.Items.Insert(4, tsm_Other)
            tsm_Other.Enabled = False
        End If

    End Sub

    Public Sub ReportCelltoLogClick(ByVal sender As Object, ByVal e As EventArgs)

        ts_Status.Text = "Compiling cell report."

        NotifyIcon.ShowBalloonTip(1000, "Compiling report:", "Cells to Log", ToolTipIcon.Info)

        WaitGif.Enabled = True

        If File.Exists(librarydata.LogPath & "Cells.log") Then
            File.Delete(librarydata.LogPath & "Cells.log")
        End If

        Dim oGetPartInfo As LibraryRead = New LibraryRead()
        oGetPartInfo.libDoc = libDoc
        AddHandler oGetPartInfo.eCellsReportComplete, AddressOf CellReportComplete

        Dim newThread As Thread
        newThread = New Thread(AddressOf oGetPartInfo.ReadLMCCellPartitions)
        newThread.IsBackground = False
        newThread.Start(True)

    End Sub

#End Region

#Region "Private Methods"

    Private Sub AddAlternateCellClick()
        frmAddAltCell.MdiParent = Me
        frmAddAltCell.Show()
    End Sub

    Private Sub AddAlternateSymbolClick(sender As Object, e As EventArgs)

        frmAddAltSymbol.MdiParent = Me
        frmAddAltSymbol.Show()
    End Sub

    Private Sub AddAltSymbolsClick(ByVal sender As Object, ByVal e As EventArgs)

        frmAddAltSymbol.MdiParent = Me
        frmAddAltSymbol.Show()

    End Sub

    Private Sub APEclick(ByVal sender As Object, ByVal e As EventArgs)

        frmPadstackEditor.MdiParent = Me
        frmPadstackEditor.Show()

    End Sub

    Private Sub BasicHealPDBClick(ByVal sender As Object, ByVal e As EventArgs)

        frmHealPDB.MdiParent = Me
        frmHealPDB.Show()

    End Sub

    Private Sub BuildBGAClick(ByVal sender As Object, ByVal e As EventArgs)

        MessageBox.Show("BGA Builder has been retired in favor of the following script: " & Environment.NewLine & Environment.NewLine & "http://communities.mentor.com/mgcx/docs/DOC-3579")

    End Sub

    Private Sub BuildPDBfromExcelClick(ByVal sender As Object, ByVal e As EventArgs)

        frmBuildFromExcel.MdiParent = Me
        frmBuildFromExcel.Show()

    End Sub

    Private Sub BuildPDBfromIntegratedClick(ByVal sender As Object, ByVal e As EventArgs)
        frmBuildPDBfromProject.MdiParent = Me
        frmBuildPDBfromProject.Show()
    End Sub

    Private Sub BuildPDBfromMappingClick(sender As Object, e As EventArgs)

        frmBuild_PDB_from_Mapping.MdiParent = Me
        frmBuild_PDB_from_Mapping.Show()
    End Sub

    Private Sub CellCount()
        If Me.InvokeRequired Then

            Dim d As New d_CellCount(AddressOf CellCount)
            Me.Invoke(d)
        Else

            i_CellCount += 1

            ts_Cells.Text = "Cells: " & i_CellCount

        End If
    End Sub

    Private Sub CellReportComplete(CellsReport As Dictionary(Of String, AAL.CellPartition))
        If Me.InvokeRequired Then

            Dim sb_Log As New StringBuilder

            For Each aalPartition As AAL.CellPartition In CellsReport.Values

                sb_Log.AppendLine(aalPartition.Name & ":")

                For Each aalCell As AAL.Cell In aalPartition.Cells

                    sb_Log.AppendLine(vbTab & aalCell.Name)

                    sb_Log.AppendLine(vbTab & vbTab & "Parts:")

                    If aalCell.AssociatedParts.Count = 0 Then
                        sb_Log.AppendLine(vbTab & vbTab & vbTab & "Error: No parts associated with this cell.")
                    Else

                        For Each sPart As String In aalCell.AssociatedParts
                            sb_Log.AppendLine(vbTab & vbTab & vbTab & sPart)
                        Next

                    End If

                    sb_Log.AppendLine(vbTab & vbTab & "Padstacks:")

                    If aalCell.Padstacks.Count = 0 Then
                        sb_Log.AppendLine(vbTab & vbTab & vbTab & "Error: No padstacks associated with this cell.")
                    Else

                        For Each sPadstack As String In aalCell.Padstacks
                            sb_Log.AppendLine(vbTab & vbTab & vbTab & sPadstack)
                        Next

                    End If

                    sb_Log.AppendLine()
                Next

            Next

            Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Cells.log", True, System.Text.Encoding.ASCII)
                writer.Write(sb_Log.ToString)
            End Using

            Dim d As New d_ReportCells(AddressOf CellReportComplete)
            Me.Invoke(d, New Object() {CellsReport})
        Else

            NotifyIcon.ShowBalloonTip(1000, "Report Complete:", "Cells to Log", ToolTipIcon.Info)

            WaitGif.Enabled = False

            ts_Status.Text = "Cell report complete: " & librarydata.LogPath & "Cells.log"

            Dim replySymbol As DialogResult = MessageBox.Show("Cell report has been created. Would you like to view the results?", "Finished", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

            If replySymbol = DialogResult.Yes Then
                OpenLogFile("Cells")
            Else
                MessageBox.Show("For more information, please see:" & Environment.NewLine & librarydata.LogPath & "Cells.log")
            End If

        End If
    End Sub

    Private Sub ChangeCellFont(ByVal sender As Object, ByVal e As EventArgs)
        frmUpdateCellFonts.MdiParent = Me
        frmUpdateCellFonts.Show()
    End Sub

    Private Sub ChangeNameCase(ByVal sender As Object, ByVal e As EventArgs)
        frm_UpdateCellName.MdiParent = Me
        frm_UpdateCellName.Show()
    End Sub

    Private Sub ChangeOutlineStrokeClick(ByVal sender As Object, ByVal e As EventArgs)
        frmChangeOutlineStrokes.MdiParent = Me
        frmChangeOutlineStrokes.Show()
    End Sub

    Private Sub ClearHistoryStatus(sender As Object, e As EventArgs)
        ts_Status.Text = s_PreviousStatus

    End Sub

    Private Sub ConnectToLMC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles tsm_ConnectLMC.Click

        Try

            'strSDD_HOME = Environment.GetEnvironmentVariable("SDD_HOME")

            libApp = GetObject(, "LibraryManager.Application")
            libDoc = libApp.ActiveLibrary

            bConnectToLMC = True

            Dim ErrorLog As Boolean = False

            NotifyIcon.ShowBalloonTip(2000, "Opening library:", "Opening library: " & libDoc.FullName, ToolTipIcon.Info)
            ts_Status.Text = "Opening library: " & libDoc.FullName
            Me.Text = "Advanced Library Editor - " & libDoc.FullName
        Catch ex As Exception
            NotifyIcon.ShowBalloonTip(2000, "Error:", "Could not connect to Central Library. Please try again...", ToolTipIcon.Error)
            MessageBox.Show("Could not connect to Central Library. Please try again..." & Environment.NewLine & Environment.NewLine & ex.ToString)
            ts_Cells.Enabled = False
            ts_Symbols.Enabled = False
            ts_Parts.Enabled = False
            Exit Sub
        End Try

        librarydata = New Data
        librarydata.LibPath = libDoc.FullName

        WaitGif.Enabled = True
        ts_Cells.Enabled = True
        ts_Symbols.Enabled = True
        ts_Parts.Enabled = True

        Dim t_OpenLMC As Thread = New Threading.Thread(AddressOf OpenLMC)
        t_OpenLMC.IsBackground = True
        t_OpenLMC.Start(False)

    End Sub

    Private Sub DuplicatePartClick()

        frmDuplicatePart.MdiParent = Me
        frmDuplicatePart.Show()
    End Sub

    Private Sub ExportCellClick()
        frmExportCellInfo.MdiParent = Me
        frmExportCellInfo.Show()
    End Sub

    Private Sub ExportSymNameClick(ByVal sender As Object, ByVal e As EventArgs)

        ts_Status.Text = "Exporting symbol names to excel."

        NotifyIcon.ShowBalloonTip(1000, "Exporting:", "Symbol names to excel", ToolTipIcon.Info)

        WaitGif.Enabled = True

        Dim newThread As Thread
        newThread = New Thread(AddressOf SymbolNamesToExcel)
        newThread.IsBackground = False
        newThread.Start()

    End Sub

    Private Sub GeneratePartHeight()

        Dim dic_Height As New Dictionary(Of String, Double)

        Dim cellEd As CellEditorAddinLib.CellEditorDlg
        Dim cellDB As CellEditorAddinLib.CellDB

        Dim pedApp As New MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        Dim sb_Report As New StringBuilder
        Dim Unit As String = String.Empty

        Try
            pedDoc = pedApp.OpenDatabaseEx(libDoc.FullName, True)
        Catch ex As Exception
            MsgBox("Error: A partition is reserved in the library. Please open Library Manager and unreserve all partitions.")
            Exit Sub
        End Try

        Select Case pedDoc.CurrentUnit

            Case MGCPCBPartsEditor.EPDBUnit.epdbUnitInch
                Unit = "IN"
            Case MGCPCBPartsEditor.EPDBUnit.epdbUnitMils
                Unit = "TH"
            Case MGCPCBPartsEditor.EPDBUnit.epdbUnitMM
                Unit = "MM"
            Case MGCPCBPartsEditor.EPDBUnit.epdbUnitUM
                Unit = "UM"
        End Select

        RaiseEvent eUpdateStatus("Opening cell editor...")

        ' Open the Cell Editor dialog and open the library database
        'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
        'cellDB = cellEd.OpenDatabase(librarydata.LibPath, False)
        cellEd = frmMain.libDoc.CellEditor
        cellDB = cellEd.ActiveDatabase

        For Each oCellPartition As CellEditorAddinLib.Partition In cellDB.Partitions()

            For Each oCell As CellEditorAddinLib.Cell In oCellPartition.Cells  ' process each cell in the partition

                Dim sCellName As String = oCellPartition.Name & ":" & oCell.Name

                RaiseEvent eUpdateStatus("Getting height for: " & sCellName.ToUpper)
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

        'cellEd.CloseActiveDatabase(True)
        cellEd.SaveActiveDatabase()
        cellDB = Nothing
        cellEd.Quit()
        cellEd = Nothing

        For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions

            For Each pdbPart As MGCPCBPartsEditor.Part In pdbPartition.Parts

                RaiseEvent eUpdateStatus("Exporting part height for: " & pdbPartition.Name & ":" & pdbPart.Number)

                Dim d_Height As Double = 0.0

                For Each pdbProperty As MGCPCBPartsEditor.Property In pdbPart.Properties

                    If pdbProperty.Name = "Height" Then
                        d_Height = pdbProperty.Value
                        Exit For
                    End If

                Next

                For Each pdbCell As MGCPCBPartsEditor.CellReference In pdbPart.CellReferences

                    If d_Height = 0.0 And dic_Height.ContainsKey(pdbCell.Name) Then
                        sb_Report.AppendLine(pdbPartition.Name & vbTab & pdbPart.Number & vbTab & d_Height & vbTab & pdbCell.Name & vbTab & dic_Height.Item(pdbCell.Name))
                    Else
                        sb_Report.AppendLine(pdbPartition.Name & vbTab & pdbPart.Number & vbTab & d_Height & vbTab & pdbCell.Name & vbTab & "0")
                    End If

                Next

            Next

        Next

        pedDoc = Nothing
        pedApp.Quit()
        pedApp = Nothing

        Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Part Height.log", True)

            writer.WriteLine("Partition" & vbTab & "Part Number" & vbTab & "Part Height (" & Unit & ")" & vbTab & "Cell Name" & vbTab & "Cell Height (" & Unit & ")")
            writer.WriteLine(sb_Report.ToString())

        End Using

        RaiseEvent ePartHeightReportComplete()

    End Sub

    Private Sub HealPDBfromExcelClick(ByVal sender As Object, ByVal e As EventArgs)

        frmHealPDBfromExcel.MdiParent = Me
        frmHealPDBfromExcel.Show()

    End Sub

    Private Sub Main_Close(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.FormClosing

        Dim count As Integer = 0

        For Each kvp As KeyValuePair(Of String, String) In dicLMCHistory
            If count = 5 Then
                Exit For
            Else
                Dim LMCName As String = kvp.Key
                librarydata.LibPath = kvp.Value

                ini.WriteValue("History", "LMC#" & count, kvp.Value)

            End If

            count += 1
        Next

        NotifyIcon.Dispose()

        closeLM()

    End Sub

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'Dim s_StartMenu As String = Environment.GetFolderPath(SpecialFolder.StartMenu)

        'If Not Directory.Exists(s_StartMenu & "\Programs\Mentor Graphics SDD\Automation Utilities\") Then

        ' Directory.CreateDirectory(s_StartMenu & "\Programs\Mentor Graphics SDD\Automation Utilities\")

        'End If

        'If File.Exists(s_StartMenu & "\Programs\Mentor Graphics SDD\Automation Utilities\Advanced Library Editor.lnk") Then
        '    File.Delete(s_StartMenu & "\Programs\Mentor Graphics SDD\Automation Utilities\Advanced Library Editor.lnk")
        'End If

        'Dim wsh As Object = CreateObject("WScript.Shell")

        'wsh = CreateObject("WScript.Shell")

        'Dim MyShortcut

        'MyShortcut = wsh.CreateShortcut(s_StartMenu & "\Programs\Mentor Graphics SDD\Automation Utilities\Advanced Library Editor.lnk")

        '' Set shortcut object properties and save it

        'MyShortcut.TargetPath = wsh.ExpandEnvironmentStrings(System.Windows.Forms.Application.ExecutablePath)

        'MyShortcut.WorkingDirectory = wsh.ExpandEnvironmentStrings(System.Windows.Forms.Application.StartupPath)

        'MyShortcut.WindowStyle = 4

        ''Use this next line to assign a icon other then the default icon for the exe

        'MyShortcut.IconLocation = wsh.ExpandEnvironmentStrings(System.Windows.Forms.Application.ExecutablePath)

        ''Save the shortcut

        'MyShortcut.Save()

        If File.Exists(System.Windows.Forms.Application.StartupPath & "\ALE.ini") Then

            ini = New IniFile(System.Windows.Forms.Application.StartupPath & "\ALE.ini")

            For value As Integer = 0 To 4

                Dim History1 As String = ini.ReadValue("History", "LMC#" & value)

                If File.Exists(History1) Then
                    If Not IsNothing(History1) Then
                        dicLMCHistory.Item(Path.GetFileName(History1)) = History1
                        Dim oLMCHistory As ToolStripItem = tsm_OpenLMC.DropDownItems.Add(Path.GetFileName(History1))
                        AddHandler oLMCHistory.Click, AddressOf OnHistoryClick
                        AddHandler oLMCHistory.MouseHover, AddressOf ShowHistoryPath
                        AddHandler oLMCHistory.MouseLeave, AddressOf ClearHistoryStatus
                    End If
                End If

            Next
        Else
            Using writer As StreamWriter = New StreamWriter(System.Windows.Forms.Application.StartupPath & "\ALE.ini", True, System.Text.Encoding.ASCII)
                writer.WriteLine("[History]")
                writer.WriteLine("LMC#0=")
                writer.WriteLine("LMC#1=")
                writer.WriteLine("LMC#2=")
                writer.WriteLine("LMC#3=")
                writer.WriteLine("LMC#4=")
            End Using

            ini = New IniFile(System.Windows.Forms.Application.StartupPath & "\ALE.ini")
        End If

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler ePopulateMenus, AddressOf PopulateMenu
        AddHandler ePopluateHistory, AddressOf PopulateLMCHistory
        AddHandler eExportSymbolNamesComplete, AddressOf SymbolNamesToExcelComplete
        AddHandler ePartHeightReportComplete, AddressOf PartHeightReportComplete
        AddHandler ePDBtoTypeTableComplete, AddressOf PDBtoTypeTableComplete

        strSDD_HOME = System.Environment.GetEnvironmentVariable("SDD_HOME", EnvironmentVariableTarget.Process)
        progID = "1"

        If strSDD_HOME = Nothing Then
            Dim vxEnv As MGCPCBReleaseEnvironmentLib.MGCPCBReleaseEnvServer
            'vxEnv = CreateObject("MGCPCBReleaseEnvServer.Application")
            vxEnv = New MGCPCBReleaseEnvironmentLib.MGCPCBReleaseEnvServer
            vxEnv.SetEnvironment()
            VxVersion = vxEnv.sddVersion

            strSDD_HOME = vxEnv.sddHome
            Dim idx As Integer = strSDD_HOME.IndexOf("SDD_HOME")
            strSDD_HOME = strSDD_HOME.Substring(0, idx).Replace("\", "\\") & "SDD_HOME"
            vxEnv.SetEnvironment(strSDD_HOME)
            progID = vxEnv.ProgIDVersion
        ElseIf (Not System.Environment.GetEnvironmentVariable("PROG_ID_VER") = String.Empty) Then

            progID = System.Environment.GetEnvironmentVariable("PROG_ID_VER")

        End If

        If (My.Application.CommandLineArgs.Count > 0) Then
            librarydata.LibPath = My.Application.CommandLineArgs(0).ToString

            ts_Status.Text = "Opening library: " & librarydata.LibPath
            NotifyIcon.ShowBalloonTip(2000, "Opening library:", "Opening library: " & librarydata.LibPath, ToolTipIcon.Info)
            Me.Text = "Advanced Library Editor - " & librarydata.LibPath

            WaitGif.Enabled = True
            ts_Cells.Enabled = True
            ts_Symbols.Enabled = True
            ts_Parts.Enabled = True

            librarydata = New Data

            Dim t_OpenLMC As Thread = New Threading.Thread(AddressOf OpenLMC)
            t_OpenLMC.IsBackground = True
            t_OpenLMC.Start(True)

        End If

    End Sub

    Private Sub ModifyCellAttributesClick()
        frmModifyCellAtts.MdiParent = Me
        frmModifyCellAtts.Show()
    End Sub

    Private Sub ModifyPartPropertiesClick()
        frmModifyPartDes.MdiParent = Me
        frmModifyPartDes.Show()
    End Sub

    Private Sub ModifyUserPropertiesClick(sender As Object, e As EventArgs)
        frmModifyUserLayers.MdiParent = Me
        frmModifyUserLayers.Show()
    End Sub

    Private Sub OnAboutClick(ByVal sender As Object, ByVal e As EventArgs) Handles AboutToolStripMenuItem.Click

        Dim oAbout As New AboutLoader.StrippedLoader
        oAbout.Show(My.Application.Info.Version.ToString(), "Advanced Library Editor", Type.LibraryManager, True)

    End Sub

    Private Sub OnHistoryClick(ByVal sender As Object, ByVal e As EventArgs)

        Try
            libApp = GetObject(, "LibraryManager.Application" & "." & progID)
            NotifyIcon.ShowBalloonTip(2000, "Error:", "Library Manager is open, please choose File - Connect to LMC or close Library Manager", ToolTipIcon.Error)
            MessageBox.Show("Library Manager is open, please choose File - Connect to LMC or close Library Manager")
            ts_Cells.Enabled = False
            ts_Symbols.Enabled = False
            ts_Parts.Enabled = False
            Exit Sub
        Catch ex As Exception

        End Try

        'mRepairSysIndex.Enabled = True
        'strSDD_HOME = Environment.GetEnvironmentVariable("SDD_HOME")

        librarydata = New Data

        librarydata.LibPath = dicLMCHistory.Item(sender.text)

        NotifyIcon.ShowBalloonTip(2000, "Opening library:", "Opening library: " & librarydata.LibPath, ToolTipIcon.Info)
        ts_Status.Text = "Opening library: " & librarydata.LibPath

        Me.Text = "Advanced Library Editor - " & librarydata.LibPath

        WaitGif.Enabled = True
        ts_Cells.Enabled = True
        ts_Symbols.Enabled = True
        ts_Parts.Enabled = True
        Me.Refresh()
        Dim t_OpenLMC As Thread = New Threading.Thread(AddressOf OpenLMC)
        t_OpenLMC.IsBackground = True
        t_OpenLMC.Start(True)

    End Sub

    Private Sub OnLogFileClick(sender As Object, e As EventArgs)
        OpenLogFile()
    End Sub

    Private Sub openComplete()
        If Me.InvokeRequired Then

            Dim d As New d_OpenComplete(AddressOf openComplete)
            Me.Invoke(d)
        Else
            b_PopulateMenus = False

            WaitGif.Enabled = False

            NotifyIcon.ShowBalloonTip(2000, "Library Opened:", librarydata.LibPath & " successfully opened.", ToolTipIcon.Info)

            ts_Status.Text = librarydata.LibPath & " successfully opened."

            If bHealPDB = True Then
                Dim reply As DialogResult = MessageBox.Show("The symbol renaming process is complete." & Environment.NewLine & Environment.NewLine & "Would you like to view the results?", "Complete",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    OpenLogFile("Heal PDB")
                Else

                    MessageBox.Show("For more information, please see:" & Environment.NewLine & librarydata.LogPath & "Rename Symbols - Heal PDB.log")

                End If
            End If

        End If
    End Sub

    Private Sub OptionNotAvaiable(ByVal sender As Object, ByVal e As EventArgs)

        MsgBox("Option not available at this time.")

    End Sub

    Private Sub OtherOpenExplorer(ByVal sender As Object, ByVal e As EventArgs)

        If IsNothing(librarydata.LibPath) Then
            MsgBox("Please open a central library before trying to open it in an Explorer window.")
        Else
            Process.Start("explorer.exe", Path.GetDirectoryName(librarydata.LibPath))
        End If

    End Sub

    Private Sub PartCount()
        If Me.InvokeRequired Then

            Dim d As New d_PartCount(AddressOf PartCount)
            Me.Invoke(d)
        Else

            i_PartCount += 1

            ts_Parts.Text = "Parts: " & i_PartCount

        End If
    End Sub

    Private Sub PartCSVReportComplete(aalPartitions As AAL.PartPartitions)
        If Me.InvokeRequired Then

            For Each aalPartition As AAL.PartPartition In aalPartitions.Values

                Dim i As Integer = 2

                Dim dicParts As New Dictionary(Of String, String())

                Dim arProperties As New ArrayList

                For Each aalPart As AAL.Part In aalPartition.Parts
                    RaiseEvent eUpdateStatus("Exporting: " & aalPartition.Name & ":" & aalPart.Number)

                    If aalPart.Cells.Count = 0 And aalPart.Symbols.Count = 0 Then
                        Continue For
                    Else

                        Dim lSymbols As List(Of String) = aalPart.Symbols.Keys.ToList

                        Dim sSymbols As String = String.Join(",", lSymbols)

                        Dim lCells As List(Of String) = aalPart.Cells.Keys.ToList

                        Dim sCells As String = String.Join(",", lCells)

                        Dim properties As String() = {sSymbols, sCells}

                        For Each kvp As KeyValuePair(Of String, String) In aalPart.Properties
                            Dim iCol As Integer
                            If Not arProperties.Contains(kvp.Key) Then
                                arProperties.Add(kvp.Key)
                                iCol = arProperties.IndexOf(kvp.Key)
                            End If

                            iCol = arProperties.IndexOf(kvp.Key)
                            If (properties.Count < (iCol + 3)) Then
                                Array.Resize(properties, (iCol + 3))
                            End If
                            properties(iCol + 2) = kvp.Value
                        Next

                        dicParts.Item(aalPart.Number) = properties

                    End If

                Next

                If My.Computer.FileSystem.FileExists(librarydata.LogPath & "Tab Separated PDB Info - " & aalPartition.Name & ".txt") Then

                    My.Computer.FileSystem.DeleteFile(librarydata.LogPath & "Tab Separated PDB Info - " & aalPartition.Name & ".txt")

                End If

                If dicParts.Count > 0 Then
                    Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Tab Separated PDB Info - " & aalPartition.Name & ".txt", True, System.Text.Encoding.ASCII)

                        writer.Write("Part Number" & vbTab & "Symbol(s)" & vbTab & "Cell(s)")
                        For Each prop As String In arProperties
                            writer.Write(vbTab & prop)
                        Next
                        writer.WriteLine()
                        For Each kvp As KeyValuePair(Of String, String()) In dicParts
                            writer.Write(kvp.Key)
                            For Each prop As String In kvp.Value
                                writer.Write(vbTab & prop)
                            Next
                            writer.WriteLine()
                        Next

                    End Using
                End If

            Next

            Dim d As New d_ReportParts(AddressOf PartCSVReportComplete)
            Me.Invoke(d, New Object() {Nothing})
        Else

            NotifyIcon.ShowBalloonTip(1000, "Report Complete:", "PDB to Tab Separated File", ToolTipIcon.Info)

            WaitGif.Enabled = False

            ts_Status.Text = "Part report complete: " & librarydata.LogPath

            MessageBox.Show("Part report has been created.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

        End If
    End Sub

    Private Sub PartExcelReportComplete(oPartitions As AAL.PartPartitions)
        If Me.InvokeRequired Then

            Dim xlsApp As Excel.Application
            Dim xlsBook As Excel.Workbook
            Dim oRange As Excel.Range

            ' Start Excel and get Application object.
            xlsApp = New Excel.Application

            ' Set some properties
            xlsApp.Visible = True
            xlsApp.DisplayAlerts = False

            xlsBook = xlsApp.Workbooks.Add

            While (xlsBook.Sheets.Count() > 1)
                xlsBook.Sheets(2).delete()
            End While

            For Each oPartition As AAL.PartPartition In oPartitions.Values

                If oPartition.Parts.Count > 0 Then
                    Dim oSheet As Excel.Worksheet
                    oSheet = xlsBook.Sheets.Add
                    oSheet.Name = oPartition.Name
                    oSheet.Select()

                    oSheet.Cells(1, 1) = "Part Number"
                    oSheet.Cells(1, 2) = "Symbols"
                    oSheet.Cells(1, 3) = "Cells"

                    Dim i As Integer = 2
                    Dim arProperties As New ArrayList

                    For Each oPart As AAL.Part In oPartition.Parts
                        oSheet.Cells(i, 1) = oPart.Number
                        RaiseEvent eUpdateStatus("Exporting: " & oPartition.Name & ":" & oPart.Number)

                        If oPart.Cells.Count = 0 And oPart.Symbols.Count = 0 Then
                            Continue For
                        Else

                            Dim lSymbols As List(Of String) = oPart.Symbols.Keys.ToList

                            Dim sSymbols As String = String.Join(",", lSymbols)

                            oSheet.Cells(i, 2) = sSymbols

                            Dim lCells As List(Of String) = oPart.Cells.Keys.ToList

                            Dim sCells As String = String.Join(",", lCells)

                            oSheet.Cells(i, 3) = sCells
                            Dim iCol As Integer
                            For Each kvp As KeyValuePair(Of String, String) In oPart.Properties

                                If Not arProperties.Contains(kvp.Key) Then
                                    arProperties.Add(kvp.Key)
                                    iCol = arProperties.IndexOf(kvp.Key)
                                    oSheet.Cells(1, iCol + 4) = kvp.Key
                                End If

                                iCol = arProperties.IndexOf(kvp.Key)
                                oSheet.Cells(i, iCol + 4) = kvp.Value
                            Next

                            If Not arProperties.Contains("Part Label") Then
                                arProperties.Add("Part Label")
                                iCol = arProperties.IndexOf("Part Label")
                                oSheet.Cells(1, iCol + 4) = "Part Label"
                            Else
                                iCol = arProperties.IndexOf("Part Label")
                            End If

                            oSheet.Cells(i, iCol + 4) = oPart.Label

                            If Not arProperties.Contains("Part Name") Then
                                arProperties.Add("Part Name")
                                iCol = arProperties.IndexOf("Part Name")
                                oSheet.Cells(1, iCol + 4) = "Part Name"
                            Else
                                iCol = arProperties.IndexOf("Part Name")
                            End If

                            oSheet.Cells(i, iCol + 4) = oPart.Name

                            If Not arProperties.Contains("Description") Then
                                arProperties.Add("Description")
                                iCol = arProperties.IndexOf("Description")
                                oSheet.Cells(1, iCol + 4) = "Description"
                            Else
                                iCol = arProperties.IndexOf("Description")
                            End If

                            oSheet.Cells(i, iCol + 4) = oPart.Description

                        End If

                        i += 1

                    Next
                End If

            Next

            xlsBook.Sheets("Sheet1").Delete()

            xlsBook.SaveAs(librarydata.LogPath & "PDB to Excel.xls", Excel.XlFileFormat.xlWorkbookDefault)

            GC.Collect()
            GC.WaitForPendingFinalizers()
            Marshal.FinalReleaseComObject(xlsBook)
            Marshal.FinalReleaseComObject(xlsApp)

            Dim d As New d_ReportParts(AddressOf PartExcelReportComplete)
            Me.Invoke(d, New Object() {oPartitions})
        Else

            NotifyIcon.ShowBalloonTip(1000, "Report Complete:", "Parts to Excel", ToolTipIcon.Info)

            WaitGif.Enabled = False

            ts_Status.Text = "Part report complete: " & librarydata.LogPath & "PDB to Excel.xls"

            MessageBox.Show("Part report has been created.", "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

        End If

    End Sub

    Private Sub PartHeightReportClick(ByVal sender As Object, ByVal e As EventArgs)

        ts_Status.Text = "Compiling part height report."

        NotifyIcon.ShowBalloonTip(1000, "Compiling report:", "Part height to Log", ToolTipIcon.Info)

        WaitGif.Enabled = True

        If File.Exists(librarydata.LogPath & "Part Height.log") Then
            File.Delete(librarydata.LogPath & "Part Height.log")
        End If

        Dim newThread As Thread
        newThread = New Thread(AddressOf GeneratePartHeight)
        newThread.IsBackground = False
        newThread.Start()

    End Sub

    Private Sub PartHeightReportComplete()
        If Me.InvokeRequired Then

            Dim d As New d_SymbolNamesToExcel(AddressOf PartHeightReportComplete)
            Me.Invoke(d)
        Else

            ts_Status.Text = "Part height report has been generated: " & librarydata.LogPath & "Part Height.log"

            NotifyIcon.ShowBalloonTip(1000, "Report Generated:", librarydata.LogPath & "Part Height.log", ToolTipIcon.Info)
            WaitGif.Enabled = False

            If MessageBox.Show("The part height report has been generated." & Environment.NewLine & Environment.NewLine & "Would you like to view the results?", "Complete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = DialogResult.Yes Then
                OpenLogFile("Part Height.log")
            Else

                MessageBox.Show("For more information, please see:" & Environment.NewLine & librarydata.LogPath & "Part Height.log")

            End If

        End If
    End Sub

    Private Sub PartNeutralReportComplete(Partitions As AAL.PartPartitions)
        If Me.InvokeRequired Then

            Dim sb_Report As New StringBuilder

            For Each oPartition As AAL.PartPartition In Partitions.Values

                For Each oPart As AAL.Part In oPartition.Parts
                    sb_Report.Append(oPart.Number & vbTab)
                    RaiseEvent eUpdateStatus("Exporting: " & oPartition.Name & ":" & oPart.Number)

                    If oPart.Cells.Count = 0 And oPart.Symbols.Count = 0 Then
                        Continue For
                    Else

                        Dim sSymbols As String = Nothing

                        For Each sSymbol In oPart.Symbols.Keys

                            If String.IsNullOrEmpty(sSymbols) Then
                                sSymbols = sSymbol
                            Else
                                sSymbols += "," & sSymbol
                            End If

                        Next

                        sb_Report.Append(sSymbols & vbTab)

                        Dim sCells As String = Nothing

                        For Each sCell In oPart.Cells.Keys

                            If String.IsNullOrEmpty(sCells) Then
                                sCells = sCell
                            Else
                                sCells += "," & sCell
                            End If

                        Next

                        sb_Report.Append(sCells & vbTab)

                    End If

                    sb_Report.AppendLine(oPartition.Name)

                Next

            Next

            Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Parts to Generic List.txt", True, System.Text.Encoding.ASCII)
                writer.WriteLine(sb_Report.ToString())
            End Using

            Dim d As New d_ReportParts(AddressOf PartNeutralReportComplete)
            Me.Invoke(d, New Object() {Partitions})
        Else

            NotifyIcon.ShowBalloonTip(1000, "Report Complete:", "Parts to Generic List", ToolTipIcon.Info)

            WaitGif.Enabled = False

            ts_Status.Text = "Part report complete: " & librarydata.LogPath & "Parts to Generic List.txt"

            Dim reply As DialogResult = MessageBox.Show("Generic Part list has been created." & Environment.NewLine & Environment.NewLine & "Would you like to view the results?", "Complete",
    MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                OpenLogFile("Parts to Generic List")
            Else
                MessageBox.Show("For more information, please see:" & Environment.NewLine & librarydata.LogPath & "Parts to Generic List.txt")

            End If

        End If
    End Sub

    Private Sub PartPropertiesReportComplete()
        If Me.InvokeRequired Then

            If librarydata.PDBNoncommonProperties.Count > 0 Then
                Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Part Properties.log", True)

                    writer.WriteLine("Non-Common Properties:")

                End Using
                For Each sProperty As String In librarydata.PDBNoncommonProperties

                    Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Part Properties.log", True)

                        writer.WriteLine(vbTab & sProperty)

                    End Using

                Next

                Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Part Properties.log", True)

                    writer.WriteLine()

                End Using
            End If

            For Each kvp As KeyValuePair(Of String, StringBuilder) In dicPartPropertyReport

                Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Part Properties.log", True)

                    writer.WriteLine(kvp.Value.ToString())
                    writer.WriteLine()

                End Using

            Next

            Dim d As New d_ReadComplete(AddressOf PartPropertiesReportComplete)
            Me.Invoke(d)
        Else

            NotifyIcon.ShowBalloonTip(1000, "Report Complete:", "Part Properties to Log", ToolTipIcon.Info)

            WaitGif.Enabled = False

            ts_Status.Text = "Symbol report complete: " & librarydata.LogPath & "Part Properties.log"

            RemoveHandler eReadPartPropertiesComplete, AddressOf PartPropertiesReportComplete

            Dim reply As DialogResult = MessageBox.Show("Part property report has been created. Would you like to view the results?", "Finished",
      MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                OpenLogFile("Part Properties")
            Else
                MessageBox.Show("For more information, please see:" & Environment.NewLine & Environment.NewLine & librarydata.LogPath & "Part Properties.log")
            End If

        End If
    End Sub

    Private Sub PartReportComplete(oPartitions As AAL.PartPartitions)
        If Me.InvokeRequired Then

            Dim sb_Log As New StringBuilder

            For Each oPartition As AAL.PartPartition In oPartitions.Values

                sb_Log.AppendLine(oPartition.Name & ":")

                For Each oPart As AAL.Part In oPartition.Values

                    sb_Log.AppendLine(vbTab & oPart.Number)

                    If oPart.Cells.Count = 0 And oPart.Symbols.Count = 0 Then
                        sb_Log.AppendLine(vbTab & vbTab & "Error: No Symbol or Cells associated with this part")
                    Else

                        If oPart.Symbols.Count = 0 Then
                            sb_Log.AppendLine(vbTab & vbTab & "Error: Part contains no symbols")
                        Else
                            sb_Log.AppendLine(vbTab & vbTab & "Associated Symbols:")

                            For Each sSymbol In oPart.Symbols.Keys

                                sb_Log.AppendLine(vbTab & vbTab & vbTab & sSymbol)

                            Next
                        End If

                        If oPart.Cells.Count = 0 Then
                            sb_Log.AppendLine(vbTab & vbTab & "Error: Part contains no cells")
                        Else
                            sb_Log.AppendLine(vbTab & vbTab & "Associated Cells:")

                            For Each sCell In oPart.Cells.Keys

                                sb_Log.AppendLine(vbTab & vbTab & vbTab & sCell)

                            Next
                        End If

                    End If

                    sb_Log.AppendLine()
                Next

            Next

            Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Parts.log", True, System.Text.Encoding.ASCII)
                writer.Write(sb_Log.ToString)
            End Using

            Dim d As New d_ReportParts(AddressOf PartReportComplete)
            Me.Invoke(d, New Object() {oPartitions})
        Else

            NotifyIcon.ShowBalloonTip(1000, "Report Complete:", "Parts to Log", ToolTipIcon.Info)

            WaitGif.Enabled = False

            ts_Status.Text = "Part report complete: " & librarydata.LogPath & "Parts.log"

            Dim replySymbol As DialogResult = MessageBox.Show("Part report has been created. Would you like to view the results?", "Finished", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

            If replySymbol = DialogResult.Yes Then
                OpenLogFile("Parts")
            Else
                MessageBox.Show("For more information, please see:" & Environment.NewLine & librarydata.LogPath & "Parts.log")
            End If

        End If

    End Sub

    Private Sub PDBtoTypeTable()
        Dim dic_RefDes As New SortedDictionary(Of String, String)

        'Dim strSDD_HOME As String = Nothing
        'strSDD_HOME = Environment.GetEnvironmentVariable("SDD_HOME")

        Dim arFile As String() = File.ReadAllLines(strSDD_HOME & "\standard\config\pcb\default\PDBTypeTable.caf")

        For Each line In arFile

            If line.StartsWith(".Ref") Then

                Dim RefSplit As String() = Split(line, vbTab)

                Dim RefDes As String = RefSplit(1).Replace("""", "")

                dic_RefDes.Add(RefDes, RefSplit(3))

            End If

        Next

        Dim sb_PDBTypeTable As New StringBuilder

        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        'Creates a handle to the Parts Editor in Library Manager
        'pedApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        pedApp = frmMain.libDoc.PartEditor
        pedDoc = pedApp.ActiveDatabaseEx
        'pedDoc = pedApp.OpenDatabaseEx(libDoc.FullName, False)

        If pedDoc.Partitions.Count > 0 Then
            For Each pedPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions

                For Each part As MGCPCBPartsEditor.Part In pedPartition.Parts

                    If Not dic_RefDes.ContainsKey(part.RefDesPrefix) Then

                        dic_RefDes.Add(part.RefDesPrefix, "Misc")

                    End If

                Next
            Next
        End If
        Try
            pedApp.CloseActiveDatabase(False)
            pedDoc = Nothing
        Catch ex As Exception
            pedApp.Quit()
            pedApp = Nothing
        End Try

        sb_PDBTypeTable.AppendLine("! This is the Reference Designator Type Conversion file.")
        sb_PDBTypeTable.AppendLine("! Version 2000.0")
        sb_PDBTypeTable.AppendLine()
        sb_PDBTypeTable.AppendLine()
        sb_PDBTypeTable.AppendLine("! Valid Types")
        sb_PDBTypeTable.AppendLine("!")
        sb_PDBTypeTable.AppendLine("!	BJT")
        sb_PDBTypeTable.AppendLine("!	Capacitor")
        sb_PDBTypeTable.AppendLine("!	Connector")
        sb_PDBTypeTable.AppendLine("!	Diode")
        sb_PDBTypeTable.AppendLine("!  IC")
        sb_PDBTypeTable.AppendLine("!  Inductor")
        sb_PDBTypeTable.AppendLine("!  JFET")
        sb_PDBTypeTable.AppendLine("!  Jumper")
        sb_PDBTypeTable.AppendLine("!  MESFET")
        sb_PDBTypeTable.AppendLine("!  Misc")
        sb_PDBTypeTable.AppendLine("!  MOSFET")
        sb_PDBTypeTable.AppendLine("!  RCNetwork")
        sb_PDBTypeTable.AppendLine("!  Resistor")
        sb_PDBTypeTable.AppendLine("!  Switch")
        sb_PDBTypeTable.AppendLine("!  VoltageDivider")
        sb_PDBTypeTable.AppendLine()

        For Each kvp As KeyValuePair(Of String, String) In dic_RefDes

            sb_PDBTypeTable.AppendLine(".Ref" & vbTab & """" & kvp.Key & """" & vbTab & "..Type" & vbTab & kvp.Value)

        Next

        Try
            Microsoft.VisualBasic.Rename(strSDD_HOME & "\standard\config\pcb\default\PDBTypeTable.caf", strSDD_HOME & "\standard\config\pcb\default\PDBTypeTable.caf" & "_old")
        Catch ex As Exception

            File.Delete(strSDD_HOME & "\standard\config\pcb\default\PDBTypeTable.caf")
        Finally
            Using writer As StreamWriter = New StreamWriter(strSDD_HOME & "\standard\config\pcb\default\PDBTypeTable.caf", True, System.Text.Encoding.ASCII)
                writer.Write(sb_PDBTypeTable.ToString())
            End Using
        End Try

        RaiseEvent ePDBtoTypeTableComplete()

    End Sub

    Private Sub PDBtoTypeTableComplete()
        If Me.InvokeRequired Then

            Dim d As New d_SymbolNamesToExcel(AddressOf PDBtoTypeTableComplete)
            Me.Invoke(d)
        Else
            ts_Status.Text = "PDBTypeTable.caf file has been updated..."

            NotifyIcon.ShowBalloonTip(1000, "Update Complete:", strSDD_HOME & "\standard\config\pcb\default\PDBTypeTable.caf", ToolTipIcon.Info)

            Dim reply As DialogResult = MessageBox.Show("The PDB TypeTable process is complete." & Environment.NewLine & Environment.NewLine & "Would you like to view the results?", "Complete",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                Process.Start("rundll32.exe", "shell32.dll, OpenAs_RunDLL " & strSDD_HOME & "\standard\config\pcb\default\PDBTypeTable.caf")
            Else

                MessageBox.Show("For more information, please see:" & Environment.NewLine & strSDD_HOME & "\standard\config\pcb\default\PDBTypeTable.caf")

            End If

            WaitGif.Enabled = False

        End If

    End Sub

    Private Sub PruneParts()
        frmPruneParts.MdiParent = Me
        frmPruneParts.Show()
    End Sub

    Private Sub PurgeSymbolsClick(ByVal sender As Object, ByVal e As EventArgs)

        frmSymPurge.MdiParent = Me
        frmSymPurge.Show()

    End Sub

    Private Sub ReadCellComplete(dic_CellList As Dictionary(Of String, List(Of String)), dic_CellsByPartition As Dictionary(Of String, List(Of String)))

        If Me.InvokeRequired Then

            Dim d As New d_PopulateCellMenu(AddressOf ReadCellComplete)
            Me.Invoke(d, New Object() {dic_CellList, dic_CellsByPartition})
        Else

            librarydata.CellList = dic_CellList
            librarydata.CellsByPartition = dic_CellsByPartition

            If b_PopulateMenus = True Then

                'Cell Menu
                Dim mModifyCell As New ToolStripMenuItem
                Dim mSwitchPadstacks As New ToolStripMenuItem
                Dim mModifyCellAttributes As New ToolStripMenuItem
                Dim mModifyUserProperties As New ToolStripMenuItem
                Dim mRenameUserLayer As New ToolStripMenuItem
                Dim mReportCell As New ToolStripMenuItem
                Dim mBuildBGA As New ToolStripMenuItem
                Dim mChangeCellFont As New ToolStripMenuItem
                Dim mReportCelltoLog As New ToolStripMenuItem
                Dim mReportCelltoCSV As New ToolStripMenuItem
                Dim mCompareCellPCBandLib As New ToolStripMenuItem
                Dim mExportCellInfo As New ToolStripMenuItem
                Dim mRenameCell As New ToolStripMenuItem
                Dim mCellProperties As New ToolStripMenuItem
                Dim mChangeOutlineStroke As New ToolStripMenuItem
                Dim mUpdateCellLayers As New ToolStripMenuItem
                Dim mAddPins As New ToolStripMenuItem

                'mAddPins.Text = "Add Pins to Cell"
                'tsm_Cell.DropDownItems.Add(mAddPins)
                'AddHandler mAddPins.Click, AddressOf AddPinsClick

                'mBuildBGA.Text = "Build BGA from Excel"
                'tsm_Cell.DropDownItems.Add(mBuildBGA)
                'AddHandler mBuildBGA.Click, AddressOf BuildBGAClick

                mRenameCell.Text = "Change Name Case"
                tsm_Cell.DropDownItems.Add(mRenameCell)
                AddHandler mRenameCell.Click, AddressOf ChangeNameCase

                mChangeOutlineStroke.Text = "Change Outline Strokes"
                tsm_Cell.DropDownItems.Add(mChangeOutlineStroke)
                AddHandler mChangeOutlineStroke.Click, AddressOf ChangeOutlineStrokeClick

                'mSwitchPadstacks.Text = "Swap Padstacks"
                'tsm_Cell.DropDownItems.Add(mSwitchPadstacks)
                'AddHandler mSwitchPadstacks.Click, AddressOf SwitchPadstacksClick

                mChangeCellFont.Text = "Change Text Style"
                tsm_Cell.DropDownItems.Add(mChangeCellFont)
                AddHandler mChangeCellFont.Click, AddressOf ChangeCellFont

                mExportCellInfo.Text = "Export Cell Info"
                tsm_Cell.DropDownItems.Add(mExportCellInfo)
                AddHandler mExportCellInfo.Click, AddressOf ExportCellClick

                mModifyCellAttributes.Text = "Modify Attributes"
                tsm_Cell.DropDownItems.Add(mModifyCellAttributes)
                AddHandler mModifyCellAttributes.Click, AddressOf ModifyCellAttributesClick

                mModifyUserProperties.Text = "Modify User Properties"
                tsm_Cell.DropDownItems.Add(mModifyUserProperties)
                AddHandler mModifyUserProperties.Click, AddressOf ModifyUserPropertiesClick

                tsm_Cell.DropDownItems.Add(New ToolStripSeparator)

                mReportCell.Text = "&Report"
                tsm_Cell.DropDownItems.Add(mReportCell)

                mReportCelltoLog.Text = "&To Log"
                mReportCell.DropDownItems.Add(mReportCelltoLog)
                AddHandler mReportCelltoLog.Click, AddressOf ReportCelltoLogClick

                tsm_Cell.Enabled = True

            End If

        End If

    End Sub

    Private Sub ReadLMCPartProperties()
        Dim newThreads(libDoc.Partitions(LibraryManager.MGCLMObjectType.kPART).Count) As Thread
        Dim i As Integer = 0
        Dim pedApp As New MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        'Creates a handle to the Parts Editor in Library Manager
        'pedApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        pedApp = libDoc.PartEditor
        'pedDoc = pedApp.OpenDatabaseEx(libDoc.FullName, True)
        pedDoc = pedApp.ActiveDatabaseEx

        For Each Partition As MGCPCBPartsEditor.Partition In pedDoc.Partitions
            Dim ReadPartAtts As New LibraryRead
            AddHandler ReadPartAtts.eReturnPartPropeties, AddressOf ReadPartPartitionComplete
            ReadPartAtts.CommonProperties = librarydata.PDBCommonProperties
            ReadPartAtts.libDoc = libDoc
            newThreads(i) = New Thread(AddressOf ReadPartAtts.readPDBProperties)
            newThreads(i).IsBackground = True
            newThreads(i).Start(Partition)
            i += 1
        Next

        For i = 0 To pedDoc.Partitions.Count - 1
            newThreads(i).Join()
        Next

        ' close the editor
        pedDoc = Nothing
        pedApp.Quit()
        pedApp = Nothing
        'pedApp.CloseActiveDatabase()
        'pedApp.Quit()

        RaiseEvent eReadPartPropertiesComplete()
    End Sub

    Private Sub ReadLMCSymbolProperties()

        Dim newThreads(libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL).Count) As Thread
        Dim i As Integer = 0
        For Each lmPartition As LibraryManager.IMGCLMPartition In libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL)
            Dim ReadSymAtts As New Symbols()
            AddHandler ReadSymAtts.eReadComplete, AddressOf ReadSymPartitionComplete
            ReadSymAtts.LibraryData = librarydata
            ReadSymAtts.libDoc = libDoc
            newThreads(i) = New Thread(AddressOf ReadSymAtts.ReadAttributes)
            newThreads(i).IsBackground = True
            newThreads(i).Start(lmPartition.Name)
            i += 1
        Next

        For i = 0 To libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL).Count - 1
            newThreads(i).Join()
        Next

        RaiseEvent eReadSymbolPropertiesComplete()
    End Sub

    Private Sub ReadPadstacksComplete(dicPadstacksType As Dictionary(Of String, List(Of String)), dicPadstacks As Dictionary(Of String, String))

        If Me.InvokeRequired Then

            librarydata.PadstacksByType = dicPadstacksType
            librarydata.PadstackList = dicPadstacks

            Dim d As New d_PopulateOtherMenu(AddressOf ReadPadstacksComplete)
            Me.Invoke(d, New Object() {dicPadstacksType, dicPadstacks})
        Else

            Dim mDMS As New ToolStripMenuItem
            'Padstacks SubMenu
            Dim mReportPadstack As New ToolStripMenuItem
            Dim mReportPadstacktoLog As New ToolStripMenuItem
            Dim mReportPadstacktoExcel As New ToolStripMenuItem
            Dim tsm_Padstacks As New ToolStripMenuItem
            Dim mAPE As New ToolStripMenuItem
            Dim mMagicButton As New ToolStripMenuItem

            'Other
            mDMS.Text = "&Compress LMC for DMS"
            tsm_Other.DropDownItems.Add(mDMS)
            mDMS.Enabled = False
            AddHandler mDMS.Click, AddressOf ShrinkDMSClick

            tsm_Padstacks.Text = "&Padstacks"
            tsm_Other.DropDownItems.Add(tsm_Padstacks)

            mAPE.Text = "Batch Padstack Editor"
            tsm_Padstacks.DropDownItems.Add(mAPE)
            AddHandler mAPE.Click, AddressOf APEclick

            mReportPadstack.Text = "&Report"
            tsm_Padstacks.DropDownItems.Add(mReportPadstack)

            mReportPadstacktoLog.Text = "&To Text File"
            mReportPadstack.DropDownItems.Add(mReportPadstacktoLog)
            mReportPadstacktoLog.Enabled = False
            'AddHandler mReportPadstacktoLog.Click, AddressOf ReportPadstacktoLogClick

            mReportPadstacktoExcel.Text = "&To Excel File"
            mReportPadstack.DropDownItems.Add(mReportPadstacktoExcel)
            mReportPadstacktoExcel.Enabled = False

            tsm_Other.Enabled = True
            tsm_OpenExplorer.Enabled = True

        End If

    End Sub

    Private Sub ReadPartComplete(dic_PartList As Dictionary(Of String, String), dic_PartsByPartition As Dictionary(Of String, List(Of String)))

        If File.Exists(libDoc.Path & "\SymbolLibs\ALE_Rename_Symbols.txt") Then

            Dim dicLogReport As New Dictionary(Of String, HealInfo)

            Dim pedAppHeal As New MGCPCBPartsEditor.PartsEditorDlg
            Dim pedDocHeal As MGCPCBPartsEditor.PartsDB
            Dim parts As MGCPCBPartsEditor.Parts
            Dim part As MGCPCBPartsEditor.Part

            'Creates a handle to the Parts Editor in Library Manager
            'pedAppHeal = CreateObject("MGCPCBLibraries.PartsEditorDlg")
            pedAppHeal = libDoc.PartEditor
            Try
                pedDocHeal = pedAppHeal.ActiveDatabaseEx
                'pedDocHeal = pedAppHeal.OpenDatabaseEx(libDoc.FullName, False)
            Catch ex As Exception
                MsgBox("Error: A partition is reserved in the library. Please open Library Manager and unreserve all partitions.")
                Exit Sub
            End Try

            pedDocHeal = Nothing
            pedAppHeal.SaveActiveDatabase()
            pedAppHeal.Quit()
            pedAppHeal = Nothing
            'pedAppHeal.CloseActiveDatabase(True)
            'pedAppHeal.Quit()

            If File.Exists(libDoc.Path & "\Work\Heal PDB Diagnosis.xml") Then
                File.Delete(libDoc.Path & "\Work\Heal PDB Diagnosis.xml")
            End If

            Dim xmlSettings As New Xml.XmlWriterSettings
            xmlSettings.Indent = True
            xmlSettings.IndentChars = vbTab
            xmlSettings.NewLineChars = vbNewLine
            xmlSettings.NewLineHandling = Xml.NewLineHandling.Replace

            Dim dicPartsToFix As New Dictionary(Of String, List(Of String))

            bHealPDB = True

            If File.Exists(librarydata.LogPath & "Rename Symbols - Heal PDB.log") Then
                File.Delete(librarydata.LogPath & "Rename Symbols - Heal PDB.log")
            End If

            If File.Exists(libDoc.Path & "\Work\Rename_Symbols_Associated_Parts.xml") Then

                dicPartsToFix = New Dictionary(Of String, List(Of String))

                Dim xmlDoc As New XmlDocument
                xmlDoc.Load(libDoc.Path & "\Work\Rename_Symbols_Associated_Parts.xml")

                For Each xmlPartition As XmlNode In xmlDoc.DocumentElement.ChildNodes

                    Dim l_Parts As New List(Of String)

                    For Each xmlPart As XmlNode In xmlPartition.ChildNodes

                        l_Parts.Add(xmlPart.InnerText)

                    Next

                    dicPartsToFix.Add(xmlPartition.Attributes("Name").Value, l_Parts)

                Next

            End If

            'oWaitScreen.ShowWaitScreen("Updating Symbols in PDB")
            Dim dicSymbolRename As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

            Using xmlWriter As Xml.XmlWriter = Xml.XmlWriter.Create(libDoc.Path & "\Work\Heal PDB Diagnosis.xml", xmlSettings)
                xmlWriter.WriteStartDocument()
                xmlWriter.WriteStartElement("HealPDB")
                xmlWriter.WriteStartElement("Parts")
                xmlWriter.WriteEndElement()
                xmlWriter.WriteStartElement("SymbolsToRename")

                Dim arRenameSymbolsFile As String() = File.ReadAllLines(libDoc.Path & "\SymbolLibs\ALE_Rename_Symbols.txt")
                Dim arFileLength As Integer = arRenameSymbolsFile.Length
                For Each arLine In arRenameSymbolsFile

                    Dim sLineSplit As String() = Split(arLine, vbTab)

                    If Not dicSymbolRename.ContainsKey(sLineSplit(0)) Then
                        dicSymbolRename.Add(sLineSplit(0), sLineSplit(1))
                    End If

                    xmlWriter.WriteStartElement("Symbol")
                    xmlWriter.WriteStartAttribute("Before")
                    xmlWriter.WriteString(sLineSplit(0))
                    xmlWriter.WriteEndAttribute()
                    xmlWriter.WriteStartAttribute("After")
                    xmlWriter.WriteString(sLineSplit(1))
                    xmlWriter.WriteEndAttribute()

                    xmlWriter.WriteEndElement()

                Next

                xmlWriter.WriteEndElement()

                xmlWriter.WriteEndElement()
                xmlWriter.WriteEndDocument()

            End Using

            Dim xmlDiagnosticsDoc As New Xml.XmlDocument

            xmlDiagnosticsDoc.Load(libDoc.Path & "\Work\Heal PDB Diagnosis.xml")

            For Each l_Parts As List(Of String) In dicPartsToFix.Values
                i_PartsLeftToHeal += l_Parts.Count
            Next

            For Each kvp As KeyValuePair(Of String, List(Of String)) In dicPartsToFix

                'pedAppHeal = CreateObject("MGCPCBLibraries.PartsEditorDlg")
                pedAppHeal = libDoc.PartEditor

                'pedDocHeal = pedAppHeal.OpenDatabaseEx(libDoc.FullName, False)
                pedDocHeal = pedAppHeal.ActiveDatabaseEx
                For Each Partition As MGCPCBPartsEditor.Partition In pedDocHeal.Partitions(kvp.Key)     'Step through each part partition in the parts editor

                    Dim cHealPDB As Heal_PDB = Nothing
                    cHealPDB = New Heal_PDB()
                    'cHealPDB.dicPEDCellPin = dicPEDCellPin
                    AddHandler cHealPDB.eUpdateCount, AddressOf UpdateHealCount
                    cHealPDB.LibraryData = librarydata
                    cHealPDB.dicPrjPartData = Nothing
                    cHealPDB.dicRenameSymbols = dicSymbolRename
                    cHealPDB.bRepairErrors = False
                    cHealPDB.PartsToHeal = kvp.Value
                    cHealPDB.xmlDiagnosticsDoc = xmlDiagnosticsDoc

                    cHealPDB.HealParts(Partition)

                    xmlDiagnosticsDoc = cHealPDB.xmlDiagnosticsDoc

                    dicLogReport.Add(Partition.Name, cHealPDB.HealLog)

                    Try
                        pedAppHeal.SaveActiveDatabase()
                    Catch ex As Exception

                        Dim oHealInfo As Log

                        dicLogReport.Remove(Partition.Name)
                        dicLogReport.Add(Partition.Name, Nothing)

                    End Try

                    parts = Nothing

                    parts = Partition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll)
                    part = Nothing
                    For Each part In parts
                        Dim partNumber As String = part.Number

                        If (partNumber.Contains("^new")) Or (partNumber.Contains("^NewPart")) Then
                            Dim partName As String() = Split(partNumber, "^")
                            part.Number = partName(0)
                        End If

                    Next

                    Partition = Nothing

                Next

                xmlDiagnosticsDoc.Save(libDoc.Path & "\Work\Heal PDB Diagnosis.xml")

                pedDocHeal = Nothing
                pedAppHeal.SaveActiveDatabase()
                'pedAppHeal.CloseActiveDatabase(True)
                'pedAppHeal.Quit()
                pedAppHeal = Nothing

            Next

            xmlDiagnosticsDoc.Save(libDoc.Path & "\Work\Heal PDB Diagnosis.xml")

            Dim bFailures As Boolean = False

            For Each kvp As KeyValuePair(Of String, HealInfo) In dicLogReport
                'Grab part number and part attributes:
                Dim sPartition As String = kvp.Key
                Dim oHealInfo As HealInfo = kvp.Value

                If IsNothing(oHealInfo) Then

                    Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                        writer.WriteLine(sPartition & ":")
                        writer.WriteLine("Fatal Error: Could not save database.")
                        writer.WriteLine()

                    End Using

                    Continue For

                End If

                If oHealInfo.Log.Count > 0 Then
                    Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                        writer.WriteLine(sPartition & ":")
                        writer.WriteLine("Parts Successfully Fixed: " & oHealInfo.Success)
                        writer.WriteLine("Unable to Fix: " & oHealInfo.Failed)
                        writer.WriteLine("Percentage Complete: " & (oHealInfo.Success / (oHealInfo.Success + oHealInfo.Failed) * 100) & "%")
                        writer.WriteLine()

                    End Using

                    For Each pair As KeyValuePair(Of String, Log) In oHealInfo.Log

                        If pair.Value.Errors.Count > 0 Then
                            bFailures = True
                            Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        ElseIf pair.Value.Warnings.Count > 0 Then
                            Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        ElseIf pair.Value.Notes.Count > 0 Then
                            Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        Else
                            Continue For
                        End If

                        If pair.Value.Errors.Count > 0 Then
                            Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                                writer.WriteLine(vbTab & vbTab & "Errors:")

                            End Using

                            For Each sErr As String In pair.Value.Errors
                                Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sErr)

                                End Using
                            Next

                        End If

                        If pair.Value.Warnings.Count > 0 Then
                            Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                                writer.WriteLine(vbTab & vbTab & "Warnings:")

                            End Using

                            For Each sWrn As String In pair.Value.Warnings
                                Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sWrn)

                                End Using
                            Next

                        End If

                        If pair.Value.Notes.Count > 0 Then
                            Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                                writer.WriteLine(vbTab & vbTab & "Notes:")

                            End Using

                            For Each sNote As String In pair.Value.Notes
                                Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sNote)

                                End Using
                            Next

                        End If

                        Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                            writer.WriteLine()

                        End Using

                    Next

                    Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Rename Symbols - Heal PDB.log", True)

                        writer.WriteLine()

                    End Using

                End If

            Next

            If bFailures = False Then
                File.Delete(libDoc.Path & "\Work\Heal PDB Diagnosis.xml")
            End If

            File.Delete(libDoc.Path & "\SymbolLibs\ALE_Rename_Symbols.txt")

        End If

        Do Until tReadSymbols.IsAlive = False And tReadCells.IsAlive = False And tReadPadstacks.IsAlive = False

        Loop

        If Me.InvokeRequired Then

            Dim d_PDBMenu As New d_PopulatePartMenu(AddressOf ReadPartComplete)
            Me.Invoke(d_PDBMenu, New Object() {dic_PartList, dic_PartsByPartition})
        Else
            librarydata.PartList = dic_PartList
            librarydata.PartsByPartition = dic_PartsByPartition
            If b_PopulateMenus = True Then

                Dim mHealPDB As New ToolStripMenuItem
                Dim mHealBasic As New ToolStripMenuItem
                Dim mHealUsingExcel As New ToolStripMenuItem
                Dim mHealUsingEE As New ToolStripMenuItem
                Dim mSwapPN As New ToolStripMenuItem
                Dim mUpdateTopCell As New ToolStripMenuItem
                Dim mBuildFromEE As New ToolStripMenuItem
                Dim mBuildPDB As New ToolStripMenuItem
                Dim mBuildFromCadence As New ToolStripMenuItem
                Dim mBuildExcelFile As New ToolStripMenuItem
                Dim mBuildFromMapping As New ToolStripMenuItem
                Dim mRemovePDBProps As New ToolStripMenuItem
                Dim mPartProperties As New ToolStripMenuItem
                Dim mSwapCells As New ToolStripMenuItem
                Dim mModifyPartProperties As New ToolStripMenuItem
                Dim mAddPartProperty As New ToolStripMenuItem
                Dim mModifyCellRefs As New ToolStripMenuItem
                Dim mAddAlternateCell As New ToolStripMenuItem
                Dim mAddAlternateSymbol As New ToolStripMenuItem
                Dim mRepartitionPDB As New ToolStripMenuItem
                Dim mReportPDB As New ToolStripMenuItem
                Dim mReportPDBtoLog As New ToolStripMenuItem
                Dim mReportPDBtoTypeTable As New ToolStripMenuItem
                Dim mReportPDBtoDatabook As New ToolStripMenuItem
                Dim mReportPDBtoExcel As New ToolStripMenuItem
                Dim mReportPDBtoCSV As New ToolStripMenuItem
                Dim mReportPDBtoNeutral As New ToolStripMenuItem
                Dim mPartHeightReport As New ToolStripMenuItem
                Dim mDuplicatePart As New ToolStripMenuItem
                Dim mAddAltSymbols As New ToolStripMenuItem
                Dim mReportPDBPropertiestoLog As New ToolStripMenuItem
                Dim mReportPDBBOMCoverage As New ToolStripMenuItem
                Dim mPruneParts As New ToolStripMenuItem

                mAddAlternateCell.Text = "Add Cell(s)"
                tsm_PDB.DropDownItems.Add(mAddAlternateCell)
                AddHandler mAddAlternateCell.Click, AddressOf AddAlternateCellClick

                mAddAlternateSymbol.Text = "Add Symbol(s)"
                tsm_PDB.DropDownItems.Add(mAddAlternateSymbol)
                AddHandler mAddAlternateSymbol.Click, AddressOf AddAlternateSymbolClick

                mBuildPDB.Text = "Build PDB"
                tsm_PDB.DropDownItems.Add(mBuildPDB)

                mBuildExcelFile.Text = "from Excel"
                mBuildPDB.DropDownItems.Add(mBuildExcelFile)
                'AddHandler mBuildPDB.Click, AddressOf OptionNotAvaiable
                AddHandler mBuildExcelFile.Click, AddressOf BuildPDBfromExcelClick

                mBuildFromEE.Text = "from Integrated Project"
                mBuildPDB.DropDownItems.Add(mBuildFromEE)
                AddHandler mBuildFromEE.Click, AddressOf BuildPDBfromIntegratedClick

                mBuildFromMapping.Text = "from XML Mapping"
                mBuildPDB.DropDownItems.Add(mBuildFromMapping)
                mBuildFromMapping.Enabled = True
                'AddHandler mBuildFromDatabook.Click, AddressOf OptionNotAvaiable
                AddHandler mBuildFromMapping.Click, AddressOf BuildPDBfromMappingClick

                mDuplicatePart.Text = "Copy One to Many"
                tsm_PDB.DropDownItems.Add(mDuplicatePart)
                AddHandler mDuplicatePart.Click, AddressOf DuplicatePartClick
                mDuplicatePart.Enabled = True

                mHealPDB.Text = "Heal PDB"
                tsm_PDB.DropDownItems.Add(mHealPDB)

                mHealBasic.Text = "Basic"
                mHealPDB.DropDownItems.Add(mHealBasic)
                AddHandler mHealBasic.Click, AddressOf BasicHealPDBClick

                'mHealUsingExcel.Text = "using &Excel Data"
                'mHealPDB.DropDownItems.Add(mHealUsingExcel)
                'mHealUsingExcel.Enabled = False
                ''AddHandler mHealUsingExcel.Click, AddressOf HealPDBfromExcelClick

                mHealUsingEE.Text = "from Integrated Project"
                mHealPDB.DropDownItems.Add(mHealUsingEE)
                AddHandler mHealUsingEE.Click, AddressOf HealPDBClick

                mPartProperties.Text = "&Properties"
                tsm_PDB.DropDownItems.Add(mPartProperties)

                mAddPartProperty.Text = "Add New Property"
                mPartProperties.DropDownItems.Add(mAddPartProperty)
                AddHandler mAddPartProperty.Click, AddressOf TestClick

                mModifyPartProperties.Text = "Modify Existing Property"
                mPartProperties.DropDownItems.Add(mModifyPartProperties)
                AddHandler mModifyPartProperties.Click, AddressOf ModifyPartPropertiesClick

                mRemovePDBProps.Text = "Remove Properties"
                mPartProperties.DropDownItems.Add(mRemovePDBProps)
                AddHandler mRemovePDBProps.Click, AddressOf RemovePDBPropsClick

                mPruneParts.Text = "Prune Parts"
                tsm_PDB.DropDownItems.Add(mPruneParts)
                AddHandler mPruneParts.Click, AddressOf PruneParts

                mSwapPN.Text = "&Rename Parts"
                tsm_PDB.DropDownItems.Add(mSwapPN)
                AddHandler mSwapPN.Click, AddressOf SwapPNClick

                mSwapCells.Text = "Swap Cells"
                tsm_PDB.DropDownItems.Add(mSwapCells)
                AddHandler mSwapCells.Click, AddressOf SwapCellsInPDB

                tsm_PDB.DropDownItems.Add(New ToolStripSeparator)

                mReportPDB.Text = "&Report"
                tsm_PDB.DropDownItems.Add(mReportPDB)

                mPartHeightReport.Text = "Part Height Report"
                mReportPDB.DropDownItems.Add(mPartHeightReport)
                AddHandler mPartHeightReport.Click, AddressOf PartHeightReportClick

                mReportPDBtoLog.Text = "To &Log"
                mReportPDB.DropDownItems.Add(mReportPDBtoLog)
                AddHandler mReportPDBtoLog.Click, AddressOf ReportPDBtoLogClick

                mReportPDBtoTypeTable.Text = "To &PDBTypeTable.caf"
                mReportPDB.DropDownItems.Add(mReportPDBtoTypeTable)
                AddHandler mReportPDBtoTypeTable.Click, AddressOf ReportPDBtoTypeTable

                mReportPDBtoExcel.Text = "To Excel"
                mReportPDB.DropDownItems.Add(mReportPDBtoExcel)
                AddHandler mReportPDBtoExcel.Click, AddressOf ReportPDBtoExcel

                mReportPDBtoCSV.Text = "To Tab Separated File"
                mReportPDB.DropDownItems.Add(mReportPDBtoCSV)
                AddHandler mReportPDBtoCSV.Click, AddressOf ReportPDBtoCSV

                mReportPDBtoNeutral.Text = "To Generic File"
                mReportPDB.DropDownItems.Add(mReportPDBtoNeutral)
                AddHandler mReportPDBtoNeutral.Click, AddressOf ReportPDBtoNeutral

                mReportPDBPropertiestoLog.Text = "&Properties to Log"
                mReportPDB.DropDownItems.Add(mReportPDBPropertiestoLog)
                AddHandler mReportPDBPropertiestoLog.Click, AddressOf ReportPDBPropertiestoLogClick

                mReportPDBBOMCoverage.Text = "&BOM Coverage"
                mReportPDB.DropDownItems.Add(mReportPDBBOMCoverage)
                AddHandler mReportPDBBOMCoverage.Click, AddressOf ReportPDBBOMCoverageClick

                If librarydata.Type = Data.LibType.DC Then
                    mHealPDB.Enabled = False
                    mBuildPDB.Enabled = False
                End If

                tsm_PDB.Enabled = True

                tsm_LibraryIntegrity.Enabled = True

            End If

            Dim d As New d_OpenComplete(AddressOf openComplete)
            Me.Invoke(d)

        End If

    End Sub

    Private Sub ReadPartPartitionComplete(ByVal sPartition As String, ByVal dic_Properties As Dictionary(Of String, List(Of String)), ByVal dic_NonCommonProperties As Dictionary(Of String, List(Of String)))
        SyncLock sublock

            If dic_Properties.Count > 0 Then

                Dim sb_PartAtts As New StringBuilder

                sb_PartAtts.AppendLine(sPartition & ":")

                Dim dic_PartitionProperties As New Dictionary(Of String, AAL.SymbolProperty)(StringComparer.OrdinalIgnoreCase)

                For Each kvp As KeyValuePair(Of String, List(Of String)) In dic_Properties
                    Dim sPart As String = kvp.Key
                    Dim l_Properties As List(Of String) = kvp.Value

                    Dim l_LocalNonCommonProperties As List(Of String)
                    Dim l_LocalCommonProperties As New List(Of String)(l_Properties)
                    If dic_NonCommonProperties.ContainsKey(sPart) Then
                        l_LocalNonCommonProperties = dic_NonCommonProperties.Item(sPart)
                    End If

                    For Each sProperty In l_Properties
                        If l_LocalNonCommonProperties.Contains(sProperty, StringComparer.OrdinalIgnoreCase) Then
                            l_LocalCommonProperties.Remove(sProperty)
                        End If
                    Next

                    If l_LocalCommonProperties.Count > 0 Or l_LocalNonCommonProperties.Count > 0 Then
                        sb_PartAtts.AppendLine(vbTab & sPart)
                    End If

                    If l_LocalCommonProperties.Count > 0 Then
                        sb_PartAtts.AppendLine(vbTab & vbTab & "Common Properties:")

                        For Each sProperty As String In l_LocalCommonProperties
                            sb_PartAtts.AppendLine(vbTab & vbTab & vbTab & sProperty)
                            librarydata.PDBNoncommonProperties.Remove(sProperty)
                        Next
                    End If

                    If l_LocalNonCommonProperties.Count > 0 Then
                        sb_PartAtts.AppendLine(vbTab & vbTab & "NonCommon Properties:")

                        For Each sProperty As String In l_LocalNonCommonProperties
                            sb_PartAtts.AppendLine(vbTab & vbTab & vbTab & sProperty)
                            If Not librarydata.PDBNoncommonProperties.Contains(sProperty) Then librarydata.PDBNoncommonProperties.Add(sProperty)
                        Next

                    End If

                Next

                dicPartPropertyReport.Add(sPartition, sb_PartAtts)

            End If

        End SyncLock
    End Sub

    Private Sub ReadSymbolComplete(dic_SymbolList_In As Dictionary(Of String, List(Of String)), dic_SymbolsByPartition_In As Dictionary(Of String, List(Of String)))

        If Me.InvokeRequired Then

            If File.Exists(librarydata.LogPath & "Duplicate Symbols.log") Then
                File.Delete(librarydata.LogPath & "Duplicate Symbols.log")
            End If

            Dim sb_DuplicateSymbols As New StringBuilder
            For Each kvp As KeyValuePair(Of String, List(Of String)) In dic_SymbolList_In

                If kvp.Value.Count > 1 Then
                    sb_DuplicateSymbols.AppendLine(kvp.Key)
                    For Each sPartition As String In kvp.Value
                        sb_DuplicateSymbols.AppendLine(vbTab & sPartition)
                    Next
                    sb_DuplicateSymbols.AppendLine()
                End If

            Next

            If sb_DuplicateSymbols.Length > 0 Then
                Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Duplicate Symbols.log", True, System.Text.Encoding.ASCII)
                    writer.Write(sb_DuplicateSymbols.ToString)
                End Using
            End If

            Dim d As New d_PopulateSymbolMenu(AddressOf ReadSymbolComplete)
            Me.Invoke(d, New Object() {dic_SymbolList_In, dic_SymbolsByPartition_In})
        Else
            librarydata.SymbolList = dic_SymbolList_In
            librarydata.SymbolsByPartition = dic_SymbolsByPartition_In

            If b_PopulateMenus = True And librarydata.Type = Data.LibType.DX Then

                'Dim dic_SymMenuItems As New SortedDictionary(Of String, ToolStripMenuItem)

                'Symbol Menu
                Dim mModSymbol As New ToolStripMenuItem
                Dim mPurgeSymbols As New ToolStripMenuItem
                Dim mResetSymbols As New ToolStripMenuItem
                Dim mRenameSymbols As New ToolStripMenuItem
                Dim mReportSymbol As New ToolStripMenuItem
                Dim mReportSymboltoLog As New ToolStripMenuItem
                Dim mReportSymboltoCSV As New ToolStripMenuItem
                Dim mExportSymName As New ToolStripMenuItem
                Dim mReportSymbolPropertiestoLog As New ToolStripMenuItem

                mModSymbol.Text = "Modify &Properties"
                tsm_Symbol.DropDownItems.Add(mModSymbol)
                mModSymbol.Enabled = True
                AddHandler mModSymbol.Click, AddressOf ModSymbolClick

                'mPurgeSymbols.Text = "Purge Library of .X Symbols"
                'tsm_Symbol.DropDownItems.Add(mPurgeSymbols)
                'mPurgeSymbols.Enabled = False
                'AddHandler mPurgeSymbols.Click, AddressOf PurgeSymbolsClick

                mRenameSymbols.Text = "&Rename Symbols"
                tsm_Symbol.DropDownItems.Add(mRenameSymbols)
                AddHandler mRenameSymbols.Click, AddressOf RenameSymbols

                mResetSymbols.Text = "R&eset Symbols"
                tsm_Symbol.DropDownItems.Add(mResetSymbols)
                mResetSymbols.Enabled = True
                AddHandler mResetSymbols.Click, AddressOf ResetSymbols

                tsm_Symbol.DropDownItems.Add(New ToolStripSeparator)

                mReportSymbol.Text = "&Report"
                tsm_Symbol.DropDownItems.Add(mReportSymbol)

                mExportSymName.Text = "Names to Excel"
                mReportSymbol.DropDownItems.Add(mExportSymName)
                AddHandler mExportSymName.Click, AddressOf ExportSymNameClick

                mReportSymboltoLog.Text = "To &Log"
                mReportSymbol.DropDownItems.Add(mReportSymboltoLog)
                AddHandler mReportSymboltoLog.Click, AddressOf ReportSymboltoLogClick

                mReportSymbolPropertiestoLog.Text = "&Properties to Log"
                mReportSymbol.DropDownItems.Add(mReportSymbolPropertiestoLog)
                AddHandler mReportSymbolPropertiestoLog.Click, AddressOf ReportSymbolPropertiestoLogClick

                'For Each tsm_Item As ToolStripItem In dic_SymMenuItems.Values
                '    tsm_Symbol.DropDownItems.Add(tsm_Item)
                'Next

                tsm_Symbol.Enabled = True

            End If

        End If
    End Sub

    Private Sub ReadSymPartitionComplete(ByVal sPartition As String, ByVal dic_UniqueSymProperties As Dictionary(Of String, AAL.SymbolProperty), ByVal dic_Symbols As Dictionary(Of String, Object), ByVal dic_SymPropswithTrailingSpaces As Dictionary(Of String, List(Of String)), ByVal dic_UniquePinProperties As Dictionary(Of String, AAL.SymbolPinProperty), ByVal dic_NonCommonProperties As Dictionary(Of String, List(Of String)))

        SyncLock sublock

            If dic_Symbols.Count > 0 Then

                Dim sb_SymbolAtts As New StringBuilder

                sb_SymbolAtts.AppendLine(sPartition & ":")

                Dim dic_PartitionProperties As New Dictionary(Of String, AAL.SymbolProperty)(StringComparer.OrdinalIgnoreCase)
                Dim dic_PartitionPinProperties As New Dictionary(Of String, AAL.SymbolPinProperty)(StringComparer.OrdinalIgnoreCase)

                For Each kvp As KeyValuePair(Of String, Object) In dic_Symbols
                    Dim sSym As String = kvp.Key
                    Dim dic_SymbolProperties As Dictionary(Of String, AAL.SymbolProperty) = kvp.Value.Symbol
                    Dim dic_SymbolPinProperties As Dictionary(Of String, AAL.SymbolPinProperty) = kvp.Value.Pin

                    Dim l_NonCommonProperties As New List(Of String)
                    Dim l_NonCommonPinProperties As New List(Of String)

                    Dim bPrintSymbolName As Boolean = True
                    Dim bPrintHeader As Boolean = True
                    Dim bPrintPinHeader As Boolean = True

                    For Each oProperty As AAL.SymbolProperty In dic_SymbolProperties.Values

                        If oProperty.Name.StartsWith("@") Or oProperty.Name = "FORWARD_PCB" Then
                            Continue For
                        End If

                        If bPrintSymbolName = True Then
                            sb_SymbolAtts.AppendLine(vbTab & sSym)
                            bPrintSymbolName = False
                        End If

                        If Not dic_PartitionProperties.ContainsKey(oProperty.Name) Then
                            dic_PartitionProperties.Item(oProperty.Name) = oProperty
                        End If

                        If frmMain.librarydata.SymbolNoncommonProperties.Contains(oProperty.Name.Trim, StringComparer.OrdinalIgnoreCase) Then
                            If Not l_NonCommonProperties.Contains(oProperty.Name.Trim) Then l_NonCommonProperties.Add(oProperty.Name.Trim)
                        ElseIf frmMain.librarydata.SymbolCommonProperties.Contains(oProperty.Name.Trim, StringComparer.OrdinalIgnoreCase) Then
                            If bPrintHeader = True Then
                                sb_SymbolAtts.AppendLine(vbTab & vbTab & "Common Properties:")
                                bPrintHeader = False
                            End If
                            sb_SymbolAtts.AppendLine(vbTab & vbTab & vbTab & oProperty.Name)
                        Else
                            l_NonCommonProperties.Add(oProperty.Name.Trim)
                            frmMain.librarydata.SymbolNoncommonProperties.Add(oProperty.Name.Trim)
                        End If

                    Next

                    For Each oProperty As AAL.SymbolPinProperty In dic_SymbolPinProperties.Values

                        If oProperty.Name.StartsWith("@") Or oProperty.Name = "FORWARD_PCB" Then
                            Continue For
                        End If

                        If bPrintSymbolName = True Then
                            sb_SymbolAtts.AppendLine(vbTab & sSym)
                            bPrintSymbolName = False
                        End If

                        If Not dic_PartitionProperties.ContainsKey(oProperty.Name) Then
                            dic_PartitionPinProperties.Item(oProperty.Name) = oProperty
                        End If

                        If frmMain.librarydata.SymbolNoncommonPinProperties.Contains(oProperty.Name.Trim, StringComparer.OrdinalIgnoreCase) Then
                            If Not l_NonCommonPinProperties.Contains(oProperty.Name.Trim) Then l_NonCommonPinProperties.Add(oProperty.Name.Trim)
                        ElseIf frmMain.librarydata.SymbolCommonProperties.Contains(oProperty.Name.Trim, StringComparer.OrdinalIgnoreCase) Then
                            If bPrintPinHeader = True Then
                                sb_SymbolAtts.AppendLine(vbTab & vbTab & "Common Pin Properties:")
                                bPrintPinHeader = False
                            End If
                            sb_SymbolAtts.AppendLine(vbTab & vbTab & vbTab & oProperty.Name)
                        Else
                            l_NonCommonPinProperties.Add(oProperty.Name.Trim)
                            frmMain.librarydata.SymbolNoncommonPinProperties.Add(oProperty.Name.Trim)
                        End If

                    Next

                    If l_NonCommonProperties.Count > 0 Then
                        sb_SymbolAtts.AppendLine(vbTab & vbTab & "NonCommon Properties:")
                        l_NonCommonProperties.Sort()
                        For Each sProperty As String In l_NonCommonProperties
                            sb_SymbolAtts.AppendLine(vbTab & vbTab & vbTab & sProperty)
                        Next

                    End If

                    If l_NonCommonPinProperties.Count > 0 Then
                        sb_SymbolAtts.AppendLine(vbTab & vbTab & "NonCommon Pin Properties:")
                        l_NonCommonPinProperties.Sort()
                        For Each sProperty As String In l_NonCommonPinProperties
                            sb_SymbolAtts.AppendLine(vbTab & vbTab & vbTab & sProperty)
                        Next

                    End If

                    If bPrintSymbolName = False Then
                        sb_SymbolAtts.AppendLine()
                    End If

                Next

                dicSymPropertyReport.Add(sPartition, sb_SymbolAtts)

            End If

        End SyncLock

    End Sub

    Private Sub RemovePDBPropsClick(ByVal sender As Object, ByVal e As EventArgs)
        frmRemovePDBProperties.MdiParent = Me
        frmRemovePDBProperties.Show()
    End Sub

    Private Sub RenameSymbols(ByVal sender As Object, ByVal e As EventArgs)

        Dim reply As DialogResult = MessageBox.Show("To rename symbols, ALE will need to re-index your library. If you are confident that your library will re-index, please continue. Otherwise, please re-index your library before running Rename Symbols", "Information:",
              MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

        If reply = DialogResult.OK Then

            frmRenameSymbols.MdiParent = Me
            frmRenameSymbols.Show()

        End If

    End Sub

    Private Sub RepairSysIndexClick(ByVal sender As Object, ByVal e As EventArgs)

        Dim alExts As New ArrayList

        alExts.Add(".1")
        alExts.Add(".2")
        alExts.Add(".3")
        alExts.Add(".4")
        alExts.Add(".5")
        alExts.Add(".6")
        alExts.Add(".7")
        alExts.Add(".8")

        For Each fileOnDisk As String In My.Computer.FileSystem.GetFiles(librarydata.LogPath, FileIO.SearchOption.SearchTopLevelOnly)
            Dim ext As String = Path.GetExtension(fileOnDisk)
            Dim r As Regex = New Regex("\.txt") ' Looks to see if the extension is a number
            Dim m As Match = r.Match(ext)
            If (m.Success) Then

                Dim sPreviousLine As String

                Dim arFile As String() = File.ReadAllLines(fileOnDisk)
                Dim arFileLength As Integer = arFile.Length
                For Each arLine In arFile

                    If arLine.StartsWith("	ERROR - Problem adding symbol") Then

                        Dim arLineSplit As String() = Split(arLine, "	ERROR - Problem adding symbol ")

                        Dim sRawSymbolPath As String = arLineSplit(1).Trim
                        Dim iLastIndex As Integer = sRawSymbolPath.LastIndexOf(" ")
                        Dim iLength As Integer = (sRawSymbolPath.Length - 1) - iLastIndex

                        Dim sSymbolName As String = sRawSymbolPath.Substring(iLastIndex + 1, iLength)
                        Dim sSymbolPath As String = sRawSymbolPath.Substring(0, iLastIndex) & "\"

                        For Each sExt In alExts

                            Dim sPath As String = sSymbolPath & "sym\" & sSymbolName & sExt

                            If File.Exists(sPath) Then
                                File.Delete(sPath)
                            End If

                        Next

                    End If

                Next

            End If

        Next

    End Sub

    Private Sub ReportPadstacktoLogClick()
        Throw New NotImplementedException
    End Sub

    Private Sub ReportPDBBOMCoverageClick(ByVal sender As Object, ByVal e As EventArgs)

        frmBOMCoverage.MdiParent = Me
        frmBOMCoverage.Show()

    End Sub

    Private Sub ReportPDBPropertiestoLogClick(sender As Object, e As EventArgs)
        ts_Status.Text = "Compiling part property report."

        NotifyIcon.ShowBalloonTip(1000, "Compiling report:", "Part properties to Log", ToolTipIcon.Info)

        WaitGif.Enabled = True

        If My.Computer.FileSystem.FileExists(librarydata.LogPath & "Part Properties.log") Then

            My.Computer.FileSystem.DeleteFile(librarydata.LogPath & "Part Properties.log")

        End If

        dicPartPropertyReport.Clear()
        librarydata.PDBNoncommonProperties.Clear()

        AddHandler eReadPartPropertiesComplete, AddressOf PartPropertiesReportComplete

        Dim newThread As Thread
        newThread = New Thread(AddressOf ReadLMCPartProperties)
        newThread.IsBackground = False
        newThread.Start()
    End Sub

    Private Sub ReportPDBtoCSV(sender As Object, e As EventArgs)
        ts_Status.Text = "Compiling part report."

        NotifyIcon.ShowBalloonTip(1000, "Compiling report:", "PDB to Tab Separated File", ToolTipIcon.Info)

        WaitGif.Enabled = True

        Dim oGetPartInfo As LibraryRead = New LibraryRead()
        oGetPartInfo.libDoc = libDoc
        AddHandler oGetPartInfo.ePartsReportComplete, AddressOf PartCSVReportComplete

        Dim newThread As Thread
        newThread = New Thread(Sub() oGetPartInfo.ReadPDBParts(libDoc.PartEditor))
        newThread.IsBackground = False
        newThread.Start()
    End Sub

    Private Sub ReportPDBtoDatabook(ByVal sender As Object, ByVal e As EventArgs)
        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB
        Dim pedPartitions As MGCPCBPartsEditor.Partitions
        Dim pedPartition As MGCPCBPartsEditor.Partition
        Dim pedParts As MGCPCBPartsEditor.Parts
        Dim pedPart As MGCPCBPartsEditor.Part

        'Creates a handle to the Parts Editor in Library Manager
        'pedApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        pedApp = libDoc.PartEditor
        Try
            'pedDoc = pedApp.OpenDatabaseEx(libDoc.FullName, False)
            pedDoc = libApp.ActiveLibrary
        Catch ex As Exception
            MsgBox("Error: A partition is reserved in the library. Please open Library Manager and unreserve all partitions.")
        End Try

        Dim oXL As Excel.Application
        Dim oWB As Excel.Workbook
        Dim oSheet As Excel.Worksheet

        ' Start Excel and get Application object.
        oXL = New Excel.Application

        ' Set some properties
        oXL.Visible = True
        oXL.DisplayAlerts = False

        ' Get a new workbook.
        oWB = oXL.Workbooks.Add

        CType(oXL.ActiveWorkbook.Sheets(1), Excel.Worksheet).Delete()
        CType(oXL.ActiveWorkbook.Sheets(2), Excel.Worksheet).Delete()

        pedPartitions = pedDoc.Partitions

        For Each pedPartition In pedPartitions    'Step through each part partition in the parts editor

            Dim dicPartProps As New Dictionary(Of String, Integer)
            Dim iPartPropCount As Integer = 1
            pedParts = Nothing
            pedParts = pedPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll)

            If pedParts.Count > 0 Then

                ' Get the active sheet
                oSheet = DirectCast(oWB.ActiveSheet, Excel.Worksheet)

                Dim part As MGCPCBPartsEditor.Part

                oSheet.Cells(1, 1) = "Symbol"
                oSheet.Cells(1, 2) = "Part Number"

                For Each part In pedParts

                    Dim partProps As MGCPCBPartsEditor.Properties
                    partProps = part.Properties
                    Dim partProp As MGCPCBPartsEditor.Property

                    For Each partProp In partProps

                        If Not partProp.Name = "Part Number" Then
                            If Not dicPartProps.ContainsKey(partProp.Name) Then

                                dicPartProps.Add(partProp.Name, iPartPropCount)
                                oSheet.Cells(1, dicPartProps.Count + 2) = partProp.Name
                                iPartPropCount += 1
                            End If
                        End If

                    Next

                Next

                Dim iRow As Integer = 2

                For Each part In pedParts

                    oSheet.Cells(iRow, 2) = part.Number

                    Dim partProps As MGCPCBPartsEditor.Properties
                    partProps = part.Properties
                    Dim partProp As MGCPCBPartsEditor.Property

                    For Each partProp In partProps

                        Dim iColCount As Integer
                        dicPartProps.TryGetValue(partProp.Name, iColCount)
                        Try
                            oSheet.Cells(iRow, iColCount + 2) = partProp.Value
                        Catch ex As Exception

                        End Try

                    Next

                    Dim AssociatedSymbols As MGCPCBPartsEditor.SymbolReferences
                    Dim symRef As MGCPCBPartsEditor.SymbolReference
                    AssociatedSymbols = part.SymbolReferences

                    Dim iSym As Integer = 1
                    Dim sSymString As String
                    For Each symRef In AssociatedSymbols

                        If iSym = 1 Then
                            sSymString = symRef.Name
                        Else
                            sSymString = sSymString & "," & symRef.Name
                        End If
                        iSym += 1
                    Next
                    oSheet.Cells(iRow, 1) = sSymString

                    iRow += 1

                Next

                If Not oWB.Sheets.Count = pedPartitions.Count Then
                    oWB.Sheets.Add()
                End If

            End If
        Next

    End Sub

    Private Sub ReportPDBtoExcel(sender As Object, e As EventArgs)

        ts_Status.Text = "Compiling part report."

        NotifyIcon.ShowBalloonTip(1000, "Compiling report:", "Parts to Excel", ToolTipIcon.Info)

        WaitGif.Enabled = True

        If My.Computer.FileSystem.FileExists(librarydata.LogPath & "PDB to Excel.xls") Then

            My.Computer.FileSystem.DeleteFile(librarydata.LogPath & "PDB to Excel.xls")

        End If

        Dim oGetPartInfo As LibraryRead = New LibraryRead()
        oGetPartInfo.libDoc = libDoc
        AddHandler oGetPartInfo.ePartsReportComplete, AddressOf PartExcelReportComplete

        Dim newThread As Thread
        newThread = New Thread(Sub() oGetPartInfo.ReadPDBParts(libDoc.PartEditor))
        newThread.IsBackground = False
        newThread.Start()

    End Sub

    Private Sub ReportPDBtoLogClick(ByVal sender As Object, ByVal e As EventArgs)

        ts_Status.Text = "Compiling part report."

        NotifyIcon.ShowBalloonTip(1000, "Compiling report:", "Parts to Log", ToolTipIcon.Info)

        WaitGif.Enabled = True

        If File.Exists(librarydata.LogPath & "Parts.log") Then
            File.Delete(librarydata.LogPath & "Parts.log")
        End If

        Dim oGetPartInfo As LibraryRead = New LibraryRead()
        oGetPartInfo.libDoc = libDoc
        AddHandler oGetPartInfo.ePartsReportComplete, AddressOf PartReportComplete

        Dim newThread As Thread
        newThread = New Thread(AddressOf oGetPartInfo.ReadLMCPartPartitions)
        newThread.IsBackground = False
        newThread.Start(True)

    End Sub

    Private Sub ReportPDBtoNeutral(sender As Object, e As EventArgs)
        ts_Status.Text = "Compiling part report."

        NotifyIcon.ShowBalloonTip(1000, "Compiling report:", "Parts to Generic List", ToolTipIcon.Info)

        WaitGif.Enabled = True

        If My.Computer.FileSystem.FileExists(librarydata.LogPath & "Parts to Generic List.txt") Then

            My.Computer.FileSystem.DeleteFile(librarydata.LogPath & "Parts to Generic Listl.txt")

        End If

        Dim oGetPartInfo As LibraryRead = New LibraryRead()
        oGetPartInfo.libDoc = libDoc
        AddHandler oGetPartInfo.ePartsReportComplete, AddressOf PartNeutralReportComplete

        Dim newThread As Thread
        newThread = New Thread(Sub() oGetPartInfo.ReadPDBParts(libDoc.PartEditor))
        newThread.IsBackground = False
        newThread.Start()
    End Sub

    Private Sub ReportPDBtoTypeTable(ByVal sender As Object, ByVal e As EventArgs)

        ts_Status.Text = "Compiling part report."

        NotifyIcon.ShowBalloonTip(1000, "Compiling report:", "PDB to Type Table", ToolTipIcon.Info)

        WaitGif.Enabled = True

        Dim newThread As Thread
        newThread = New Thread(AddressOf PDBtoTypeTable)
        newThread.IsBackground = False
        newThread.Start()

    End Sub

    Private Sub ReportSymbolPropertiestoLogClick(sender As Object, e As EventArgs)
        ts_Status.Text = "Compiling symbol property report."

        NotifyIcon.ShowBalloonTip(1000, "Compiling report:", "Symbols properties to Log", ToolTipIcon.Info)

        WaitGif.Enabled = True

        If My.Computer.FileSystem.FileExists(librarydata.LogPath & "Symbol Properties.log") Then

            My.Computer.FileSystem.DeleteFile(librarydata.LogPath & "Symbol Properties.log")

        End If

        dicSymPropertyReport.Clear()
        librarydata.SymbolNoncommonProperties.Clear()

        AddHandler eReadSymbolPropertiesComplete, AddressOf SymbolPropertiesReportComplete

        Dim newThread As Thread
        newThread = New Thread(AddressOf ReadLMCSymbolProperties)
        newThread.IsBackground = False
        newThread.Start()
    End Sub

    Private Sub ReportSymboltoLogClick(ByVal sender As Object, ByVal e As EventArgs)

        ts_Status.Text = "Compiling symbol report."

        NotifyIcon.ShowBalloonTip(1000, "Compiling report:", "Symbols to Log", ToolTipIcon.Info)

        WaitGif.Enabled = True

        If My.Computer.FileSystem.FileExists(librarydata.LogPath & "Symbols.log") Then

            My.Computer.FileSystem.DeleteFile(librarydata.LogPath & "Symbols.log")

        End If

        Dim oGetPartInfo As LibraryRead = New LibraryRead()
        oGetPartInfo.libDoc = libDoc
        AddHandler oGetPartInfo.eSymbolsReportComplete, AddressOf SymbolReportComplete

        Dim newThread As Thread
        newThread = New Thread(AddressOf oGetPartInfo.ReadLMCSymbolPartitions)
        newThread.IsBackground = False
        newThread.Start(True)

    End Sub

    Private Sub ResetSymbols(ByVal sender As Object, ByVal e As EventArgs)

        frmResetSymPins.MdiParent = Me
        frmResetSymPins.Show()

    End Sub

    Private Sub runIntegrityCheck(ByVal sender As Object, ByVal e As EventArgs)

        frmIntegrityCheck.MdiParent = Me
        frmIntegrityCheck.Show()

    End Sub

    Private Sub ShowHistoryPath(sender As Object, e As EventArgs)

        s_PreviousStatus = ts_Status.Text

        Dim s_HistoryPath As String

        dicLMCHistory.TryGetValue(sender.text, s_HistoryPath)

        sender.ToolTipText = s_HistoryPath

        ts_Status.Text = "Open Library: " & s_HistoryPath

    End Sub

    Private Sub ShrinkDMSClick(ByVal sender As Object, ByVal e As EventArgs)

        'frmShrinkLMC4DMS.MdiParent = Me
        'frmShrinkLMC4DMS.Show()

    End Sub

    Private Sub SwapCellsInPDB(sender As Object, e As EventArgs)

        frm_SwapCellsinPDB.MdiParent = Me
        frm_SwapCellsinPDB.Show()
    End Sub

    Private Sub SwapPNClick(ByVal sender As Object, ByVal e As EventArgs)

        frmSwapPart.MdiParent = Me
        frmSwapPart.Show()
    End Sub

    Private Sub Sym_ToLowercase(ByVal sender As Object, ByVal e As EventArgs)

        'oWaitscreen.ShowWaitScreen("Running")

        Dim sSymDirectory As String = Path.GetDirectoryName(librarydata.LibPath) & "\SymbolLibs\"

        Dim Root As New DirectoryInfo(Path.GetDirectoryName(librarydata.LibPath) & "\SymbolLibs\")

        Dim Dirs As DirectoryInfo() = Root.GetDirectories("*.*")

        Dim cRemoveChars(Dirs.Count - 1) As Symbols
        For i As Integer = 0 To Dirs.Count - 1
            Dim cReadSymAtt As Symbols = New Symbols()
            cRemoveChars(i) = cReadSymAtt
        Next

        'Start process for delete symbols
        Dim threadDeleteSym(Dirs.Count) As Thread
        For i As Integer = 0 To Dirs.Count - 1
            threadDeleteSym(i) = New Thread(AddressOf cRemoveChars(i).changeCase)
            threadDeleteSym(i).IsBackground = True
            threadDeleteSym(i).Start(Dirs(i).FullName)
        Next i

        For i As Integer = 0 To Dirs.Count - 1
            threadDeleteSym(i).Join()
        Next

        MsgBox("Done")

    End Sub

    Private Sub SymbolCount()
        If Me.InvokeRequired Then

            Dim d As New d_SymbolCount(AddressOf SymbolCount)
            Me.Invoke(d)
        Else

            i_SymbolCount += 1

            ts_Symbols.Text = "Symbols: " & i_SymbolCount

        End If
    End Sub

    Private Sub SymbolNamesToExcel()

        Dim xlsApp As New Excel.Application
        Dim xlsBook As Excel.Workbook

        xlsApp.Visible = True
        xlsApp.DisplayAlerts = False
        xlsBook = xlsApp.Workbooks.Add()

        Dim xlsSheet As Excel.Worksheet = xlsBook.Sheets(1)

        While (xlsBook.Sheets.Count() > 1)
            xlsBook.Sheets(2).delete()
        End While

        Dim sSymDirectory As String = libDoc.Path & "\SymbolLibs\"

        'oWaitScreen.ShowWaitScreen("Exporting Symbol Names")

        Dim lSheetnames As New List(Of String)

        Dim NewSheet As Boolean = False

        For Each kvp As KeyValuePair(Of String, List(Of String)) In librarydata.SymbolsByPartition
            'Grab part number and part attributes:
            Dim SymPartition As String = kvp.Key
            Dim l_Symbols As List(Of String) = kvp.Value

            If l_Symbols.Count > 0 Then

                If NewSheet = True Then
                    xlsSheet = xlsBook.Sheets.Add()
                End If

                ' Get the active sheet
                Dim i As Integer = 1
                Dim sName As String = SymPartition

                If SymPartition.Length > 30 Then
                    sName = SymPartition.Substring(0, 30)
                End If

                Do Until Not lSheetnames.Contains(sName)

                    If i > 10 Then
                        If sName = 30 Then
                            sName = SymPartition.Substring(0, 27) & "_" & i
                        Else
                            sName = SymPartition & "_" & i
                        End If
                    Else
                        If sName = 30 Then
                            sName = SymPartition.Substring(0, 28) & "_" & i
                        Else
                            sName = SymPartition & "_" & i
                        End If
                    End If

                    i += 1
                Loop

                lSheetnames.Add(sName)

                xlsSheet.Name = sName

                Dim iRowCount As Integer = 1
                For Each sSym In l_Symbols

                    xlsSheet.Cells(iRowCount, 1) = sSym
                    iRowCount += 1

                Next

                NewSheet = True

            End If

        Next

        GC.Collect()
        GC.WaitForPendingFinalizers()

        Marshal.FinalReleaseComObject(xlsSheet)
        Marshal.FinalReleaseComObject(xlsBook)
        Marshal.FinalReleaseComObject(xlsApp)

        RaiseEvent eExportSymbolNamesComplete()

    End Sub

    Private Sub SymbolNamesToExcelComplete()
        If Me.InvokeRequired Then

            Dim d As New d_SymbolNamesToExcel(AddressOf SymbolNamesToExcelComplete)
            Me.Invoke(d)
        Else
            ts_Status.Text = "Export symbol names to excel complete..."

            NotifyIcon.ShowBalloonTip(1000, "Report Complete:", "Export symbol names to excel", ToolTipIcon.Info)

            WaitGif.Enabled = False

        End If
    End Sub

    Private Sub SymbolPropertiesReportComplete()

        If Me.InvokeRequired Then

            If librarydata.SymbolNoncommonProperties.Count > 0 Then
                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Properties.log", True)
                    writer.WriteLine("NonCommon Symbol Properties:")
                    frmMain.librarydata.SymbolNoncommonProperties.Sort()
                    For Each sProperty As String In frmMain.librarydata.SymbolNoncommonProperties
                        writer.WriteLine(vbTab & sProperty)
                    Next

                    writer.WriteLine()
                End Using
            End If

            If librarydata.SymbolNoncommonPinProperties.Count > 0 Then
                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Properties.log", True)
                    writer.WriteLine("NonCommon Pin Properties:")
                    frmMain.librarydata.SymbolNoncommonPinProperties.Sort()
                    For Each sProperty As String In frmMain.librarydata.SymbolNoncommonPinProperties
                        writer.WriteLine(vbTab & sProperty)
                    Next
                    writer.WriteLine()
                End Using
            End If

            For Each kvp As KeyValuePair(Of String, StringBuilder) In dicSymPropertyReport

                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Properties.log", True)

                    writer.WriteLine(kvp.Value.ToString())
                    writer.WriteLine()

                End Using

            Next

            Dim d As New d_ReadComplete(AddressOf SymbolPropertiesReportComplete)
            Me.Invoke(d)
        Else

            NotifyIcon.ShowBalloonTip(1000, "Report Complete:", "Symbol Properties to Log", ToolTipIcon.Info)

            WaitGif.Enabled = False

            ts_Status.Text = "Symbol report complete: " & librarydata.LogPath & "Symbols.log"

            RemoveHandler eReadSymbolPropertiesComplete, AddressOf SymbolPropertiesReportComplete

            Dim reply As DialogResult = MessageBox.Show("Symbols property report has been created. Would you like to view the results?", "Finished",
      MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                OpenLogFile("Symbol Properties")
            Else
                MessageBox.Show("For more information, please see:" & Environment.NewLine & Environment.NewLine & librarydata.LogPath & "Symbol Properties.log")
            End If

        End If

    End Sub

    Private Sub SymbolReportComplete(ByVal SymbolsReport As Dictionary(Of String, AAL.SymbolPartition))
        If Me.InvokeRequired Then

            Dim d As New d_ReportSymbols(AddressOf SymbolReportComplete)
            Me.Invoke(d, New Object() {SymbolsReport})
        Else

            Dim sb_Log As New StringBuilder

            For Each aalPartition As AAL.SymbolPartition In SymbolsReport.Values

                sb_Log.AppendLine(aalPartition.Name & ":")

                For Each aalSymbol As AAL.Symbol In aalPartition.Symbols

                    sb_Log.AppendLine(vbTab & aalSymbol.Name)

                    If aalSymbol.AssociatedParts.Count = 0 Then
                        sb_Log.AppendLine(vbTab & vbTab & "Error: No parts associated with this symbol.")
                    Else

                        For Each sPart As String In aalSymbol.AssociatedParts
                            sb_Log.AppendLine(vbTab & vbTab & sPart)
                        Next

                    End If

                    sb_Log.AppendLine()
                Next

            Next

            Using writer As StreamWriter = New StreamWriter(librarydata.LogPath & "Symbols.log", True, System.Text.Encoding.ASCII)
                writer.Write(sb_Log.ToString)
            End Using

            NotifyIcon.ShowBalloonTip(1000, "Report Complete:", "Symbols to Log", ToolTipIcon.Info)

            WaitGif.Enabled = False

            ts_Status.Text = "Symbol report complete: " & librarydata.LogPath & "Symbols.log"

            Dim replySymbol As DialogResult = MessageBox.Show("Symbols report has been created. Would you like to view the results?", "Finished", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

            If replySymbol = DialogResult.Yes Then
                OpenLogFile("Symbols")
            Else
                MessageBox.Show("For more information, please see:" & Environment.NewLine & librarydata.LogPath & "Symbols.log")
            End If

        End If

    End Sub

    Private Sub TestClick()
        frmAddPartProperty.MdiParent = Me
        frmAddPartProperty.Show()
    End Sub

    Private Sub tsm_BrowseLMC_Click(sender As System.Object, e As System.EventArgs) Handles tsm_BrowseLMC.Click

        closeLM()

        lookatlog = False

        Try
            libApp = GetObject(, "LibraryManager.Application" & "." & progID)
            NotifyIcon.ShowBalloonTip(2000, "Error:", "Library Manager is open, please choose File - Connect to LMC or close Library Manager", ToolTipIcon.Error)
            MsgBox("Library Manager is open, please choose File - Connect to LMC or close Library Manager")
            ts_Cells.Enabled = False
            ts_Symbols.Enabled = False
            ts_Parts.Enabled = False
            Exit Sub
        Catch ex As Exception

        End Try

        Using ofd As New OpenFileDialog
            ofd.Filter = "Central Library (*.lmc)|*.lmc"
            ' ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                librarydata = New Data
                librarydata.LibPath = ofd.FileName
            Else

                Exit Sub

            End If
        End Using

        'strSDD_HOME = Environment.GetEnvironmentVariable("SDD_HOME")

        NotifyIcon.ShowBalloonTip(2000, "Opening library:", "Opening library: " & librarydata.LibPath, ToolTipIcon.Info)
        ts_Status.Text = "Opening library: " & librarydata.LibPath

        WaitGif.Enabled = True
        ts_Cells.Enabled = True
        ts_Symbols.Enabled = True
        ts_Parts.Enabled = True

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        'AddHandler eOpenComplete, AddressOf openComplete
        AddHandler ePopulateMenus, AddressOf PopulateMenu
        AddHandler ePopluateHistory, AddressOf PopulateLMCHistory
        Dim t_OpenLMC As Thread = New Threading.Thread(AddressOf OpenLMC)
        t_OpenLMC.IsBackground = True
        t_OpenLMC.Start(True)

    End Sub

    Private Sub UpdateHealCount()
        If Me.InvokeRequired Then

            i_PartsLeftToHeal -= 1

            Dim d As New d_UpdateHealCount(AddressOf UpdateHealCount)
            Me.Invoke(d)
        Else
            ts_Status.Text = "Parts left to heal: " & i_PartsLeftToHeal
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

Public Class IniFile

    Private Declare Ansi Function GetPrivateProfileString Lib "kernel32.dll" Alias "GetPrivateProfileStringA" _
        (ByVal lpApplicationName As String,
         ByVal lpKeyName As String,
         ByVal lpDefault As String,
         ByVal lpReturnedString As System.Text.StringBuilder,
         ByVal nSize As Integer,
         ByVal lpFileName As String) _
     As Integer

    Private Declare Ansi Function WritePrivateProfileString Lib "kernel32.dll" Alias "WritePrivateProfileStringA" _
        (ByVal lpApplicationName As String,
         ByVal lpKeyName As String,
         ByVal lpString As String,
         ByVal lpFileName As String) _
    As Integer

#Region "Public Fields + Properties + Events + Delegates + Enums"

    Public Property Path As String

#End Region

#Region "Public Constructors + Destructors"

    ''' <summary>
    ''' IniFile Constructor 
    ''' </summary>
    ''' <param name="IniPath"> The path to the INI file. </param>
    Public Sub New(ByVal IniPath As String)
        _Path = IniPath
    End Sub

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Read value from INI file 
    ''' </summary>
    ''' <param name="section"> The section of the file to look in </param>
    ''' <param name="key">     The key in the section to look for </param>
    Public Function ReadValue(ByVal section As String, ByVal key As String) As String
        Dim sb As New System.Text.StringBuilder(255)
        Dim i = GetPrivateProfileString(section, key, "", sb, 255, Path)
        Return sb.ToString()
    End Function

    ''' <summary>
    ''' Write value to INI file 
    ''' </summary>
    ''' <param name="section"> The section of the file to write in </param>
    ''' <param name="key">     The key in the section to write </param>
    ''' <param name="value">   The value to write for the key </param>
    Public Sub WriteValue(ByVal section As String, ByVal key As String, ByVal value As String)
        WritePrivateProfileString(section, key, value, Path)
    End Sub

#End Region

End Class