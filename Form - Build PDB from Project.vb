Imports System.Threading
Imports System.IO
Imports System.Text
Imports System.Threading.Tasks
Imports AAL

Public Class frmBuildPDBfromProject

#Region "Private Fields"

    Dim ActiveThreads As Integer = 1

    'Boolean
    Dim bByPassRead As Boolean = False

    Dim bConnectToDxD As Boolean = False

    Dim bConnectToExp As Boolean = False

    Dim bConnectToLMC As Boolean = False

    Dim bMissingPNs As Boolean = False

    Dim bReadComplete As Boolean = False

    'Dictionary
    Dim dic_Height As New Dictionary(Of String, Double)

    Dim dic_UnableToSave As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

    Dim dicBadParts As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

    Dim dicDuplicateParts As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    Dim dicLogReport As New Dictionary(Of String, ArrayList)

    Dim dicPartsToProcess As New Dictionary(Of String, AAL.PartPartition)(StringComparer.OrdinalIgnoreCase)

    Dim dicSuccessfulByPartition As New Dictionary(Of String, List(Of String))

    'Integers
    Dim iBadParts As Integer = 0

    Dim iPartCount As Integer = 0

    Dim iPartsBuilt As Integer = 0

    Dim iPartsFailed As Integer = 0

    'List
    Dim l_UnableToSavePartition As New List(Of String)

    Private logLock As New Object

    Dim partsLeftToProcess As Integer

    Dim partsStack As Stack(Of AAL.Part)

    Dim pdbPartition As MGCPCBPartsEditor.Partition

    'Other
    Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg

    Dim pedDoc As MGCPCBPartsEditor.PartsDB

    'String
    Dim sExpPath As String

    Dim sProjectpath As String

    Dim timerTotal As New Stopwatch

    'Threads
    Dim tReadProject As Thread

    Dim xmlDebug As Xml.XmlDocument

#End Region

#Region "Public Delegates"

    Delegate Sub d_BuildComplete(ByVal iPartsFailed As Integer, ByVal iPartsBuilt As Integer, ByVal partsNotConsidered As Integer)

    'Delegates
    Delegate Sub d_ReadComplete()

    Delegate Sub d_UpdateMainParts(ByVal Partition As String, ByVal Part As String)

    Delegate Sub d_UpdateProjectStatus()

    Delegate Sub d_UpdateStatus(ByVal status As String)

    Delegate Sub d_UpdateThreads()

#End Region

#Region "Public Events"

    Event eBuildComplete(ByVal iPartsFailed As Integer, ByVal iPartsBuilt As Integer, ByVal partsNotConsidered As Integer)

    'Events
    Event eBuildFailed()

    Event eReadComplete()

    Event eUpdateMainParts(ByVal Partition As String, ByVal Part As String)

    Event eUpdateStatus(status As String)

    Event eUpdateThreads()

#End Region

#Region "Public Properties"

    Public Property bRebuildParts As Boolean
    Public Property LogFile As String

#End Region

#Region "Private Methods"

    Private Sub btn_BrowseDxD_Click(sender As Object, e As EventArgs) Handles btn_BrowseDxD.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "DxDesigner Project Files|*.prj"
            ofd.Title = "Select File"
            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                tbox_DxD.Text = ofd.FileName
                btn_BrowseDxD.Enabled = False
                tbox_DxD.ReadOnly = True
                If (tbox_PCB.ReadOnly = True) And tbox_DxD.ReadOnly = True Then
                    btn_Read.Enabled = True
                    gb_Options.Enabled = True
                    UpdateStatus("Click Read to proceed...")
                ElseIf tbox_DxD.ReadOnly = False Then
                    UpdateStatus("Browse to an DxDesigner schematic...")
                End If
            End If
        End Using
    End Sub

    Private Sub btn_BrowseExp_Click(sender As Object, e As EventArgs) Handles btn_BrowseExp.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "xPCB  Layout Design Files(*.pcb)|*.pcb"
            ' ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"
            ofd.RestoreDirectory = True

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                tbox_PCB.Text = ofd.FileName
                tbox_PCB.ReadOnly = True
                btn_BrowseExp.Enabled = False

                If (tbox_PCB.ReadOnly = True) And tbox_DxD.ReadOnly = True Then
                    btn_Read.Enabled = True
                    gb_Options.Enabled = True
                    UpdateStatus("Click Read to proceed...")
                ElseIf tbox_DxD.ReadOnly = False Then
                    UpdateStatus("Browse to an DxDesigner schematic...")
                End If

            End If
        End Using
    End Sub

    Private Sub btn_Create_Click(sender As Object, e As EventArgs) Handles btn_Create.Click
        ts_Status.Text = "Building PDBs..."

        WaitGif.Enabled = True
        tsm_Exp.Visible = False
        tsm_DxD.Visible = False

        tsm_Threads.Visible = True

        Dim t_Build As Thread = New Threading.Thread(AddressOf BuildPDB)
        t_Build.IsBackground = True
        t_Build.Start()
    End Sub

    Private Sub btn_Read_Click(sender As Object, e As EventArgs) Handles btn_Read.Click
        frmMain.NotifyIcon.ShowBalloonTip(2000, "Analyzing:", "Analyzing Project and Library...", ToolTipIcon.Info)

        tsm_Exp.ForeColor = Color.Red
        tsm_DxD.ForeColor = Color.Red

        tsm_Exp.Visible = True
        tsm_DxD.Visible = True

        bRebuildParts = chkbox_RebuildParts.Checked

        gb_Options.Enabled = False

        tsm_Threads.Visible = False

        WaitGif.Visible = True
        WaitGif.Enabled = True
        tv_Parts.Nodes.Clear()
        dicBadParts.Clear()
        dicDuplicateParts.Clear()
        dicPartsToProcess.Clear()

        UpdateStatus("Analyzing Project and Library...")
        btn_Read.Enabled = False

        sExpPath = tbox_PCB.Text
        sProjectpath = Path.GetDirectoryName(tbox_DxD.Text)

        tv_Parts.Nodes.Clear()

        tReadProject = New Threading.Thread(AddressOf ReadProject)
        tReadProject.IsBackground = True
        tReadProject.Start()

    End Sub

    Private Sub BuildComplete(ByVal iPartsFailed As Integer, ByVal iPartsBuilt As Integer, ByVal partsNotConsidered As Integer)
        If Me.InvokeRequired Then
            RaiseEvent eUpdateStatus("Logging errors...")

            partsNotConsidered = WriteLogFiles(dicLogReport, iPartsFailed, iPartsBuilt)

            Dim d As New d_BuildComplete(AddressOf BuildComplete)
            Me.Invoke(d, New Object() {iPartsFailed, iPartsBuilt, partsNotConsidered})
        Else

            'frmmain.librarydata.PartList = frmmain.librarydata.PartList

            timerTotal.Stop()

            'Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from Project.log", True, System.Text.Encoding.ASCII)
            '    writer.WriteLine()
            '    writer.WriteLine("Successfully Built: " & iPartsBuilt)
            '    writer.WriteLine("Failed to Build: " & iPartsFailed)
            'End Using

            gbAction.Visible = False
            WaitGif.Enabled = False
            ts_Status.Text = "Successfully Built: " & iPartsBuilt & ", Failed to Build: " & iPartsFailed + partsNotConsidered

            If Not IsNothing(xmlDebug) Then
                xmlDebug.Save(frmMain.librarydata.LogPath & "\Build PDB - Generic XML Mappings.xml")
            End If

            If iPartsFailed > 0 Then
                If Not IsNothing(xmlDebug) Then
                    xmlDebug.Save(frmMain.librarydata.LogPath & "\Build PDB - Generic XML Mappings.xml")
                End If

                Dim reply As DialogResult = MessageBox.Show(iPartsBuilt & " parts were successfully built but " & iPartsFailed & " failed to build. Would you like to view the results?", "Finished",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB from Project.log")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from Project.log")
                End If

            ElseIf (l_UnableToSavePartition.Count > 0) Then

                Dim s_ErrorMessage As New StringBuilder

                For Each Partition As String In l_UnableToSavePartition
                    s_ErrorMessage.AppendLine(Partition)
                Next

                Dim reply As DialogResult = MessageBox.Show("Unable to save the following partitions:" & Environment.NewLine & Environment.NewLine & s_ErrorMessage.ToString() & Environment.NewLine & Environment.NewLine & "Would you like to view the results?", "Finished",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB from Project.log")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from Project.log")
                End If
            Else
                MsgBox(iPartsBuilt & " parts were successfully built.")
            End If

            If File.Exists(frmMain.librarydata.LogPath & "Build PDB - Alternate Symbols.log") Then

                Dim reply As DialogResult = MessageBox.Show("ALE was unable to add alternate symbols to some parts. Would you like to view the results?", "Finished",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB - Alternate Symbols.log")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB - Alternate Symbols.log")
                End If
            End If

        End If
    End Sub

    Private Function BuildPart() As Object

        While partsStack.Count > 0

            ActiveThreads += 1
            RaiseEvent eUpdateThreads()
            Try
                If partsStack.Count > 0 Then
                    Dim aalPart As AAL.Part = partsStack.Pop

                    If Not IsNothing(aalPart) Then

                        If bRebuildParts = True Then
                            If frmMain.librarydata.PartList.ContainsKey(aalPart.Number) Then
                                For Each pdbTempPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions
                                    For Each pdbPart As MGCPCBPartsEditor.Part In pdbTempPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, aalPart.Number)
                                        pdbPart.Delete()
                                    Next
                                Next
                            End If
                        End If

                        aalPart.Partition = pdbPartition.Name
                        Dim ocBuildPDB As Build_PDB = New Build_PDB
                        'ocBuildPDB.pdbPartition = pdbPartition
                        'ocBuildPDB.l_SymbolNames = kvp_Part.Value.Symbols.Keys.ToList()
                        'ocBuildPDB.l_CellName = kvp_Part.Value.Cells.Keys.ToList()
                        ocBuildPDB.Heights = dic_Height
                        ocBuildPDB.aalPart = aalPart

                        ocBuildPDB.xmlDebug = xmlDebug
                        'ocBuildPDB.dicGates = Nothing
                        ocBuildPDB.b_RemoveIncompleteParts = chkbox_RemoveIncomplete.Checked
                        ocBuildPDB.LibraryData = frmMain.librarydata
                        ocBuildPDB.readSymbols = False

                        Dim success As Boolean
                        Dim pdbMapping As MGCPCBPartsEditor.Part = pdbPartition.NewPart()
                        Try

                            success = ocBuildPDB.NewPart(pdbMapping)
                        Catch ex As Exception
                            Dim err As String = ex.ToString()
                        End Try

                        ActiveThreads -= 1

                        RaiseEvent eUpdateThreads()

                        If (success) Then
                            iPartsBuilt += 1
                            RaiseEvent eUpdateMainParts(pdbPartition.Name, aalPart.Number)
                            'iPartsBuilt += 1
                            For Each part As Part In aalPart.AssociatedParts

                                If bRebuildParts = True Then
                                    If frmMain.librarydata.PartList.ContainsKey(part.Number) Then
                                        For Each pdbTempPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions()
                                            For Each pdbPart As MGCPCBPartsEditor.Part In pdbTempPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, part.Number)
                                                pdbPart.Delete()
                                            Next
                                        Next
                                    End If

                                End If

                                Try
                                    Dim pdbPart As MGCPCBPartsEditor.Part = pdbMapping.Copy(part.Number, part.Name, part.Label)

                                    pdbPart.Description = part.Description

                                    Dim oProperty As MGCPCBPartsEditor.Property

                                    If Not part.Height = 0 Then

                                        Try
                                            If Not IsNothing(part.Unit) Then
                                                oProperty = pdbPart.PutPropertyEx("Height", part.Height, part.Unit)
                                            Else
                                                oProperty = pdbPart.PutPropertyEx("Height", part.Height)
                                            End If
                                        Catch ex As Exception
                                            ocBuildPDB.Warnings.Add("[Build PDB Error] Cannot add property: ""Height"", with value: " & part.Height)
                                        End Try
                                    End If

                                    iPartsBuilt += 1

                                    RaiseEvent eUpdateMainParts(pdbPartition.Name, part.Number)
                                Catch ex As Exception
                                    ocBuildPDB.Errors.Add("Can not copy pin mapping from " & pdbMapping.Number & " to " & part.Number & ". Error: " & ex.Message.ToString())
                                End Try
                            Next
                        Else
                            iPartsFailed += 1

                            For Each part As Part In aalPart.AssociatedParts
                                ocBuildPDB.Errors.Add("Failed to add part " & part.Number & " because it was associated with a mapping " & aalPart.Number & " that filed to build.")
                                iPartsFailed += 1

                            Next

                        End If

                        Dim logItem As New BuildPDBLogItem(success, aalPart.Number, ocBuildPDB.Errors, ocBuildPDB.Warnings, ocBuildPDB.Notes, ocBuildPDB.LogAlternateSymbols)

                        LogPart(logItem)

                        aalPart.Dispose()
                        aalPart = Nothing
                        'ocBuildPDB.Dispose()
                        'ocBuildPDB = Nothing

                        partsLeftToProcess -= 1

                        RaiseEvent eUpdateStatus(pdbPartition.Name & " parts left to process: " & partsLeftToProcess)

                    End If
                End If
            Catch ex As Exception
                Dim message As String = ex.ToString()
            End Try

            'Return objLogAtts
        End While

    End Function

    Private Sub BuildPDB()

        dicLogReport = New Dictionary(Of String, ArrayList)
        'ArrayList
        Dim alnewPDBPartitions As New ArrayList()

        'Dictionaries

        Dim dic_Height As New Dictionary(Of String, Double)

        pedApp = frmMain.libDoc.PartEditor
        pedDoc = pedApp.ActiveDatabaseEx

        If chkbox_GetHeight.Checked = True Then

            Dim cellEd As CellEditorAddinLib.CellEditorDlg
            Dim cellDB As CellEditorAddinLib.CellDB

            RaiseEvent eUpdateStatus("Opening cell editor...")

            ' Open the Cell Editor dialog and open the library database
            'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
            cellEd = frmMain.libDoc.CellEditor
            cellDB = cellEd.ActiveDatabase
            'cellDB = cellEd.OpenDatabase(frmMain.librarydata.LibPath, False)

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

        If File.Exists(frmMain.librarydata.LogPath & "Build PDB from Project.log") Then

            File.Delete(frmMain.librarydata.LogPath & "Build PDB from Project.log")

        End If

        If File.Exists(frmMain.librarydata.LogPath & "Build PDB - Alternate Symbols.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Build PDB - Alternate Symbols.log")
        End If

        iPartsBuilt = 0
        iPartsFailed = 0

        partsStack = New Stack(Of AAL.Part)

        Dim srCaf As System.IO.StreamReader
        Dim sline As String

        If chkbox_RefDesPartitions.Checked = True Then
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

            End Try
        Else
            If alnewPDBPartitions.Count = 0 Then
                alnewPDBPartitions.Add("Temp")
            End If
        End If

        For Each partitionName In dicPartsToProcess.Keys
            If Not alnewPDBPartitions.Contains(partitionName) Then

                pedDoc.NewPartition(partitionName)

            End If
        Next

        If File.Exists(frmMain.librarydata.LogPath & "\Build PDB - Generic XML Mappings.xml") Then
            File.Delete(frmMain.librarydata.LogPath & "\Build PDB - Generic XML Mappings.xml")
        End If

        If chkboxMultiThread.Checked = False Then
            xmlDebug = New Xml.XmlDocument

            Dim xmlSettings As New Xml.XmlWriterSettings
            xmlSettings.Indent = True
            xmlSettings.IndentChars = vbTab
            xmlSettings.NewLineChars = vbNewLine
            xmlSettings.NewLineHandling = Xml.NewLineHandling.Replace

            If Not File.Exists(frmMain.librarydata.LogPath & "\Build PDB - Generic XML Mappings.xml") Then
                Using writer As Xml.XmlWriter = Xml.XmlWriter.Create(frmMain.librarydata.LogPath & "\Build PDB - Generic XML Mappings.xml", xmlSettings)
                    writer.WriteStartDocument()
                    writer.WriteStartElement("Mapping")
                    writer.WriteEndElement()
                    writer.WriteEndDocument()

                End Using
            End If

            xmlDebug.Load(frmMain.librarydata.LogPath & "\Build PDB - Generic XML Mappings.xml")
        End If

        For Each kvp As KeyValuePair(Of String, AAL.PartPartition) In dicPartsToProcess

            While (IsNothing(pedDoc))
                RaiseEvent eUpdateStatus("Refreshing connection to PDB Editor.")
                pedApp = frmMain.libDoc.PartEditor
                pedDoc = pedApp.ActiveDatabaseEx
                Thread.Sleep(2500)
            End While

            pdbPartition = pedDoc.Partitions(kvp.Key).Item(1)
            Dim dicSuccessfulByPartition As New Dictionary(Of String, List(Of String))

            If IsNothing(pdbPartition) Then
                pdbPartition = pedDoc.NewPartition(kvp.Key)
            End If

            Dim dicPartData As Dictionary(Of String, AAL.Part) = kvp.Value
            partsLeftToProcess = dicPartData.Keys.Count

            RaiseEvent eUpdateStatus(pdbPartition.Name & " parts left to process: " & partsStack.Count)

            'If chkbox_SaveEachPart.Checked Or chkboxMultiThread.Checked = False Then
            '    For Each kvp_Part As KeyValuePair(Of String, AAL.Part) In dicPartData

            ' partsStack.Push(kvp_Part.Value)

            ' BuildPart()

            '    Next
            'Else
            '    Dim tasks As New List(Of Task(Of Object))

            ' 'Dim partsToProcess As Integer = dicPartData.Count Dim refreshPDBEditorLimit As Integer
            ' = 3000

            ' For Each part As AAL.Part In dicPartData.Values partsStack.Push(part) Next

            ' For i As Integer = 0 To 100 tasks.Add(Task.Factory.StartNew(Of Object)(Function()
            ' BuildPart())) Next

            ' Task.WaitAll(tasks.ToArray())

            'End If

            If chkbox_SaveEachPart.Checked Or chkboxMultiThread.Checked = False Then
                For Each kvp_Part As KeyValuePair(Of String, AAL.Part) In dicPartData

                    partsStack.Push(kvp_Part.Value)

                    BuildPart()

                Next
            Else
                Dim tasks As New List(Of Task(Of Object))

                'Dim partsToProcess As Integer = dicPartData.Count
                'Dim refreshPDBEditorLimit As Integer = 3000
                Dim parts As Array = dicPartData.Values.ToArray
                Dim index As Integer = 0
                While partsLeftToProcess > 0
                    Dim Max As Integer = Math.Min(partsLeftToProcess, 1000)
                    Dim maxTasks As Integer = Math.Min(partsLeftToProcess, 100)

                    For j As Integer = 0 To Max - 1
                        partsStack.Push(parts(index))
                        index += 1
                    Next

                    For i As Integer = 0 To maxTasks - 1
                        tasks.Add(Task.Factory.StartNew(Of Object)(Function() BuildPart()))
                    Next

                    Task.WaitAll(tasks.ToArray())

                    RaiseEvent eUpdateStatus("Saving Partition.")

                    Dim bSaved As Boolean

                    Try
                        pedApp.SaveActiveDatabase()
                        bSaved = True
                    Catch ex As Exception
                        iPartsFailed += iPartsBuilt
                        dic_UnableToSave.Item(kvp.Key) = dicPartData.Keys.ToList
                        bSaved = False
                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from Project.log", True, System.Text.Encoding.ASCII)
                            writer.WriteLine("Could not save active database. Error returned: " & ex.Message.ToString())
                        End Using
                    End Try

                    Try
                        pedApp.Quit()
                    Catch ex As Exception

                    End Try

                    pedApp = Nothing
                    pedDoc = Nothing

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
                iPartsFailed += iPartsBuilt
                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from Project - Duplicate Parts.log", True, System.Text.Encoding.ASCII)
                    writer.WriteLine("Could not save active database. Error returned: " & ex.Message.ToString())
                End Using
                dic_UnableToSave.Item(kvp.Key) = dicPartData.Keys.ToList
                bSavedPartition = False
            Finally
                If bSavedPartition Then
                    'iPartsBuilt += iPartsBuilt
                    If dicSuccessfulByPartition.Keys.Contains(pdbPartition.Name) Then
                        For Each part As String In dicSuccessfulByPartition.Item(pdbPartition.Name)
                            RaiseEvent eUpdateMainParts(pdbPartition.Name, part)
                        Next
                    End If
                End If
            End Try

            pedApp.Quit()
            pedApp = Nothing
            pedDoc = Nothing

            If dicPartData.Count > 0 Then
                GC.Collect()
            End If

        Next

        'If Not IsNothing(pedApp) Then
        '    pedApp.SaveActiveDatabase()
        'End If

        'pedApp.Quit()
        'pedApp = Nothing
        'pedDoc = Nothing

        RaiseEvent eBuildComplete(iPartsFailed, iPartsBuilt, 0)

    End Sub

    Private Sub chkboxMultiThread_CheckedChanged(sender As Object, e As EventArgs) Handles chkboxMultiThread.CheckedChanged
        If chkboxMultiThread.Checked = True Then
            tsm_Threads.Enabled = True
        Else
            tsm_Threads.Enabled = False
        End If
    End Sub

    Private Sub DxDFinished()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateProjectStatus(AddressOf DxDFinished)
            Me.Invoke(d)
        Else
            tsm_DxD.ForeColor = Color.Green
        End If
    End Sub

    Private Sub ExpFinished()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateProjectStatus(AddressOf ExpFinished)
            Me.Invoke(d)
        Else
            tsm_Exp.ForeColor = Color.Green
        End If
    End Sub

    Private Sub frmBuildPDBfromProject_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eUpdateThreads, AddressOf UpdateThreadCount
        AddHandler eReadComplete, AddressOf ReadComplete
        AddHandler eUpdateMainParts, AddressOf UpdateMainParts
        AddHandler eBuildComplete, AddressOf BuildComplete

    End Sub

    Private Sub LogPart(ByVal logItem As BuildPDBLogItem)
        SyncLock logLock

            Try

                If Not (logItem.Errors.Count = 0) Or Not (logItem.Warnings.Count = 0) Or Not (logItem.Notes.Count = 0) Or Not (logItem.AlternateSymbols.Count = 0) Then
                    If dicLogReport.ContainsKey(pdbPartition.Name) Then
                        dicLogReport.Item(pdbPartition.Name).Add(logItem)
                    Else

                        Dim alLogNew As New ArrayList()
                        alLogNew.Add(logItem)
                        dicLogReport.Add(pdbPartition.Name, alLogNew)

                    End If
                End If

                If logItem.success Then

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

                            bSavedPart = False
                            'dicLogReport.Remove(pdbPartition.Name)
                        Finally
                            If bSavedPart Then
                                RaiseEvent eUpdateMainParts(pdbPartition.Name, logItem.PN)
                            End If
                        End Try
                    End If
                End If
            Catch ex As Exception
                Dim message As String = ex.ToString
            End Try

        End SyncLock
    End Sub

    Private Sub OptimizeMappings()
        RaiseEvent eUpdateStatus("Optimizing part mappings...")

        Dim dicParts As New Dictionary(Of String, AAL.PartPartition)(StringComparer.OrdinalIgnoreCase)

        For Each aalPartition As AAL.PartPartition In dicPartsToProcess.Values

            Dim dicMappings As New AAL.PartPartition

            For Each aalPart As AAL.Part In aalPartition.Parts

                If dicMappings.ContainsKey(aalPart.HashCode) Then
                    dicMappings(aalPart.HashCode()).AssociatedParts.Add(aalPart)
                Else
                    dicMappings(aalPart.HashCode()) = aalPart
                End If
            Next

            dicParts.Item(aalPartition.Name) = dicMappings

        Next

        dicPartsToProcess = dicParts

    End Sub

    Private Sub ReadbuttonChk()
        If tbox_DxD.ReadOnly = True And tbox_PCB.ReadOnly = True Then
            btn_Read.Enabled = True
            ts_Status.Text = "Click Read to proceed..."
        End If

    End Sub

    Private Sub ReadComplete()
        If Me.InvokeRequired Then

            Dim d As New d_ReadComplete(AddressOf ReadComplete)
            Me.Invoke(d)
        Else
            For Each kvp As KeyValuePair(Of String, AAL.PartPartition) In dicPartsToProcess
                Dim parent As TreeNode = tv_Parts.Nodes.Add(kvp.Key)
                For Each part As AAL.Part In kvp.Value.Parts
                    Dim partNode As TreeNode = parent.Nodes.Add(part.Number)
                    If (part.Symbols.Count > 0) Then
                        Dim symbolsNode As TreeNode = partNode.Nodes.Add("Symbols:")
                        For Each symbol As String In part.Symbols.Keys
                            symbolsNode.Nodes.Add(symbol)
                        Next
                    End If

                    If (part.Cells.Count > 0) Then
                        Dim cellsNode As TreeNode = partNode.Nodes.Add("Cells:")
                        For Each cell As String In part.Cells.Keys
                            cellsNode.Nodes.Add(cell)
                        Next
                    End If

                    If (part.AssociatedParts.Count > 0) Then
                        Dim associatedNode As TreeNode = partNode.Nodes.Add("Associated Parts:")
                        For Each subPart As Part In part.AssociatedParts
                            associatedNode.Nodes.Add(subPart.Number)
                        Next
                    End If

                Next
            Next

            tv_Parts.Sort()

            WaitGif.Enabled = False

            If dicPartsToProcess.Count > 0 Then
                plBuildOptions.Visible = True
                RaiseEvent eUpdateStatus(iPartCount & " eligible parts\" & dicPartsToProcess.First.Value.Count & " unique mappings.")
            End If

            If dicBadParts.Count > 0 Then

                Dim replyCell As DialogResult = MessageBox.Show("Project has been read, but " & iBadParts & " parts cannot be processed. Would you like to view the results?", "Finished",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If replyCell = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB from Project - Bad Part Data")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from Project - Bad Part Data.log")
                End If
            End If

            If dicDuplicateParts.Count > 0 Then
                Dim replySymbol As DialogResult = MessageBox.Show("Project has been read, but duplicate parts were not found. Would you like to view the results?", "Finished",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If replySymbol = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB from Project - Duplicate Parts")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from Project - Duplicate Parts.log")
                End If
            End If

        End If
    End Sub

    Private Sub ReadProject()
        RaiseEvent eUpdateStatus("Reading Project Data")

        Dim ocDxDProjectInfo As ProjectRead = New ProjectRead
        ocDxDProjectInfo.closeDxD = True
        ocDxDProjectInfo.dicDxdPartData.Clear()
        ocDxDProjectInfo.sDxDPath = tbox_DxD.Text
        ocDxDProjectInfo.buildPinMapping = True

        AddHandler ocDxDProjectInfo.eFinished, AddressOf DxDFinished

        Dim tReadDxD As Thread = New Thread(AddressOf ocDxDProjectInfo.openDxD)
        tReadDxD.IsBackground = True
        tReadDxD.Start(frmMain.progID)

        Dim ocExpProjectInfo As ProjectRead = New ProjectRead
        ocExpProjectInfo.closeExp = True
        ocExpProjectInfo.dicExpRefDesCellData.Clear()
        ocExpProjectInfo.sExpPath = tbox_PCB.Text

        AddHandler ocExpProjectInfo.eFinished, AddressOf ExpFinished

        Dim tReadExp As Thread = New Thread(AddressOf ocExpProjectInfo.openExp)
        tReadExp.IsBackground = True
        tReadExp.Start(frmMain.progID)

        tReadExp.Join()

        tReadDxD.Join()

        Dim dicParts As Dictionary(Of String, AAL.PartPartition) = ocDxDProjectInfo.dicDxdPartData
        Dim dicDxDesignerRef As Dictionary(Of String, List(Of String)) = ocDxDProjectInfo.dicDxDRefDes
        Dim dicExpeditionData As Dictionary(Of String, Object) = ocExpProjectInfo.dicExpRefDesCellData

        'For Each cell As Object In ocExpProjectInfo.dicExpRefDesCellData.Values
        '    Dim pn As String = cell.PartNumber

        ' For Each Partition As AAL.PartPartition In dicParts.Values If (Partition.Contains(pn)) Then

        ' Dim part As AAL.Part = Partition.Item(pn) If Not part.Cells.ContainsKey(cell.CellName) And
        ' frmMain.librarydata.CellList.ContainsKey(cell.CellName) Then Dim aalCell As New AAL.Cell
        ' aalCell.Name = cell.CellName aalCell.Partition =
        ' frmMain.librarydata.CellList.Item(cell.CellName).First part.Cells.Item(cell.Cellname) =
        ' aalCell End If Partition.Item(part.Number) = part dicParts.Item(Partition.Name) = Partition
        ' Exit For End If Next

        ' 'dicParts.Item(pn) = part

        'Next

        dicPartsToProcess = New Dictionary(Of String, AAL.PartPartition)

        For Each kvp As KeyValuePair(Of String, AAL.PartPartition) In dicParts
            Dim Partition As AAL.PartPartition = kvp.Value
            Partition.Name = kvp.Key
            For Each part As AAL.Part In Partition.Parts
                If frmMain.librarydata.PartList.ContainsKey(part.Number) And bRebuildParts = False Then
                    dicDuplicateParts.Item(part.Number) = frmMain.librarydata.PartList.Item(part.Number)
                    Continue For
                End If

                Dim valid As Boolean = True

                If (part.Symbols.Count = 0) Then
                    Dim problems As List(Of String)
                    If (dicBadParts.ContainsKey(part.Number)) Then
                        problems = dicBadParts.Item(part.Number)
                    Else
                        problems = New List(Of String)
                    End If
                    problems.Add("No symbol defined.")
                    dicBadParts.Item(part.Number) = problems
                    valid = False
                Else
                    For Each symbol As AAL.Symbol In part.Symbols.Values
                        If Not frmMain.librarydata.SymbolList.ContainsKey(symbol.Name) Then
                            Dim problems As List(Of String)
                            If (dicBadParts.ContainsKey(part.Number)) Then
                                problems = dicBadParts.Item(part.Number)
                            Else
                                problems = New List(Of String)
                            End If
                            problems.Add("Symbol " & symbol.Name & " does not exist in library.")
                            dicBadParts.Item(part.Number) = problems
                            valid = False
                        Else
                            Dim l_Partitions As List(Of String) = frmMain.librarydata.SymbolList.Item(symbol.Name)
                            If Not l_Partitions.Contains(symbol.Partition, StringComparer.CurrentCultureIgnoreCase) Then
                                Dim problems As List(Of String)
                                If (dicBadParts.ContainsKey(part.Number)) Then
                                    problems = dicBadParts.Item(part.Number)
                                Else
                                    problems = New List(Of String)
                                End If

                                problems.Add("Symbol " & symbol.Name & " does not exist in partition " & symbol.Partition & ".")
                                dicBadParts.Item(part.Number) = problems
                                valid = False
                            End If

                        End If
                    Next
                End If

                If (dicDxDesignerRef.Keys.Contains(part.Number, StringComparer.CurrentCultureIgnoreCase)) Then

                    For Each sRefDes As String In dicDxDesignerRef.Item(part.Number)
                        If dicExpeditionData.ContainsKey(sRefDes) Then

                            Dim cell As Object = dicExpeditionData.Item(sRefDes)

                            If Not part.Cells.ContainsKey(cell.CellName) And frmMain.librarydata.CellList.ContainsKey(cell.CellName) Then
                                Dim aalCell As New AAL.Cell
                                aalCell.Name = cell.CellName
                                aalCell.Partition = frmMain.librarydata.CellList.Item(cell.CellName).First
                                part.Cells.Item(cell.Cellname) = aalCell
                            End If

                        End If
                    Next
                End If

                If (part.Cells.Count = 0) Then
                    Dim problems As List(Of String)
                    If (dicBadParts.ContainsKey(part.Number)) Then
                        problems = dicBadParts.Item(part.Number)
                    Else
                        problems = New List(Of String)
                    End If
                    problems.Add("No cell defined.")
                    dicBadParts.Item(part.Number) = problems
                    valid = False
                Else
                    For Each cell As String In part.Cells.Keys
                        If Not frmMain.librarydata.CellList.ContainsKey(cell) Then
                            Dim problems As List(Of String) = dicBadParts.Item(part.Number)
                            problems.Add("Cell " & cell & " does not exist in library.")
                            dicBadParts.Item(part.Number) = problems
                            valid = False
                        End If
                    Next
                End If

                If valid = True Then
                    iPartCount += 1
                    Dim validPartition As AAL.PartPartition
                    If dicPartsToProcess.ContainsKey(part.Partition) Then
                        validPartition = dicPartsToProcess.Item(part.Partition)
                    Else
                        validPartition = New AAL.PartPartition
                        validPartition.Name = part.Partition
                    End If

                    validPartition.Add(part)
                    validPartition.Name = kvp.Key
                    dicPartsToProcess.Item(kvp.Key) = validPartition
                End If
            Next
        Next

        Dim xmlDoc As Xml.XmlDocument = New Xml.XmlDocument

        If Not Directory.Exists(sProjectpath & "\LogFiles\") Then
            Directory.CreateDirectory(sProjectpath & "\LogFiles\")
        End If

        If File.Exists(sProjectpath & "\LogFiles\Parts.xml") Then
            File.Delete(sProjectpath & "\LogFiles\Parts.xml")
        End If

        LogFile = frmMain.librarydata.LogPath & " Build PDB from Project.log"

        If File.Exists(LogFile) Then
            File.Delete(LogFile)
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Build PDB from Project - Duplicate Parts.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Build PDB from Project - Duplicate Parts.log")
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Build PDB from Project - Bad Part Data.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Build PDB from Project - Bad Part Data.log")
        End If

        Dim xmlSettings As New Xml.XmlWriterSettings
        xmlSettings.Indent = True
        xmlSettings.IndentChars = vbTab
        xmlSettings.NewLineChars = vbNewLine
        xmlSettings.NewLineHandling = Xml.NewLineHandling.Replace

        If Not File.Exists(sProjectpath & "\LogFiles\Parts.xml") Then
            Using writer As Xml.XmlWriter = Xml.XmlWriter.Create(sProjectpath & "\LogFiles\Parts.xml", xmlSettings)
                writer.WriteStartDocument()
                writer.WriteStartElement("Mapping")

                writer.WriteEndElement()
                writer.WriteEndDocument()

            End Using
        End If

        xmlDoc.Load(sProjectpath & "\LogFiles\Parts.xml")

        For Each Partition As AAL.PartPartition In dicPartsToProcess.Values
            For Each part As AAL.Part In Partition.Parts
                part.toXML(xmlDoc)
            Next
        Next

        xmlDoc.Save(sProjectpath & "\LogFiles\Parts.xml")

        OptimizeMappings()

        If dicBadParts.Count > 0 Then

            Dim sb_BadParts As New StringBuilder

            For Each kvp As KeyValuePair(Of String, List(Of String)) In dicBadParts

                sb_BadParts.AppendLine(kvp.Key)

                For Each s_Reason As String In kvp.Value
                    sb_BadParts.AppendLine(vbTab & s_Reason)
                Next

                iBadParts += 1

            Next

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from Project - Bad Part Data.log", True, System.Text.Encoding.ASCII)
                writer.WriteLine(iBadParts & " parts are unable to be built because of various problems found.")
                writer.WriteLine()
                writer.WriteLine(sb_BadParts.ToString())
            End Using

        End If

        If dicDuplicateParts.Count > 0 Then

            Dim sb_BadParts As New StringBuilder

            For Each kvp As KeyValuePair(Of String, String) In dicDuplicateParts
                sb_BadParts.AppendLine("Part: " & kvp.Key & ", Found in partition: " & kvp.Value)
            Next

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from Project - Duplicate Parts.log", True, System.Text.Encoding.ASCII)
                writer.WriteLine(sb_BadParts.ToString())
            End Using

        End If

        RaiseEvent eReadComplete()

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

#End Region

End Class