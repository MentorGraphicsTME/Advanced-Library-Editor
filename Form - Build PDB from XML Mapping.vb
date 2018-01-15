Imports System.Threading
Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Drawing

Public Class frmBuild_PDB_from_Mapping

#Region "Public Fields + Properties + Events + Delegates + Enums"

    Public Enum BuildPDBError
        errDueToUnableToOpenPDBEditor = 0
        errDueToUnableToFindFile = 1
    End Enum

    Delegate Sub d_BuildComplete(ByVal dicLogReport As Dictionary(Of String, ArrayList), ByVal iPartsFailed As Integer, ByVal iPartsBuilt As Integer)

    Delegate Sub d_BuildFailed(ByVal buildError As BuildPDBError)

    Delegate Sub d_ReadComplete(ByVal bProblems As Boolean, ByVal iBadParts As Integer)

    Delegate Sub d_UpdateMainParts(ByVal Partition As String, ByVal Part As String)

    'Delegates
    Delegate Sub d_UpdateStatus(ByVal status As String)

    Event eBuildComplete(ByVal dicLogReport As Dictionary(Of String, ArrayList), ByVal iPartsFailed As Integer, ByVal iPartsBuilt As Integer)

    Event eBuildFailed(ByVal buildError As Integer)

    Event eReadComplete(bProblems As Boolean)

    Event eUpdateMainParts(ByVal Partition As String, ByVal Part As String)

    'Events
    Event eUpdateStatus(status As String)

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    'Dictionary
    Dim dicBadParts As New Dictionary(Of String, Dictionary(Of String, List(Of String)))(StringComparer.OrdinalIgnoreCase)

    'Integers
    Dim iPartCount As Integer = 0

    'List
    Dim l_UnableToSavePartition As New List(Of String)

    'Objects
    Dim oPartitions As New AAL.PartPartitions

    'Strings
    Dim s_MappingPath As String

#End Region

#Region "Private Methods"

    Private Sub btn_BrowsePartContainer_Click(sender As System.Object, e As System.EventArgs) Handles btn_BrowseXMLMapping.Click

        Dim s_Path As String = Nothing

        Using ofd As New OpenFileDialog
            ofd.Filter = "XML Part Mapping|*.xml"
            ' ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                tbox_XMLMapping.Text = ofd.FileName
                btnRead.Enabled = True
            End If

        End Using
    End Sub

    Private Sub btnBuild_Click(sender As System.Object, e As System.EventArgs) Handles btnBuild.Click

        ts_Status.Text = "Building PDBs..."

        WaitGif.Enabled = True

        Dim t_Build As Thread = New Threading.Thread(AddressOf BuildPDB)
        t_Build.IsBackground = True
        t_Build.Start()

    End Sub

    Private Sub btnRead_Click(sender As System.Object, e As System.EventArgs) Handles btnRead.Click

        tv_Parts.Nodes.Clear()

        ts_Status.Text = "Reading XML Mapping..."

        WaitGif.Enabled = True
        dicBadParts.Clear()
        s_MappingPath = tbox_XMLMapping.Text

        iPartCount = 0

        If File.Exists(frmMain.librarydata.LogPath & "Build PDB from XML Mapping - Bad Part Data.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Build PDB from XML Mapping - Bad Part Data.log")
        End If

        Dim t_ReadParts As Thread = New Threading.Thread(AddressOf ReadParts)
        t_ReadParts.IsBackground = True
        t_ReadParts.Start(tbox_XMLMapping.Text)

    End Sub

    Private Sub BuildComplete(ByVal dicLogReport As Dictionary(Of String, ArrayList), ByVal iPartsFailed As Integer, ByVal iPartsBuilt As Integer)
        If Me.InvokeRequired Then

            Dim d As New d_BuildComplete(AddressOf BuildComplete)
            Me.Invoke(d, New Object() {dicLogReport, iPartsFailed, iPartsBuilt})
        Else

            For Each kvp As KeyValuePair(Of String, ArrayList) In dicLogReport
                'Grab part number and part attributes:
                Dim Partition As String = kvp.Key
                Dim objLogAtts As ArrayList = kvp.Value

                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                    writer.WriteLine(Partition & ":")
                End Using

                If l_UnableToSavePartition.Contains(Partition) Then
                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                        writer.WriteLine(vbTab & "ERROR: Unable to save partition.")
                    End Using
                Else
                    For Each att In objLogAtts

                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                            writer.WriteLine(vbTab & att(0))
                        End Using

                        If att(1).Count > 0 Then

                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                                writer.WriteLine(vbTab & vbTab & "Errors:")
                            End Using

                            Dim err As String
                            For Each err In att(1)

                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                                    writer.WriteLine(vbTab & vbTab & vbTab & err)
                                End Using

                            Next
                            att(1).Clear()
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                                writer.WriteLine()
                            End Using

                        End If

                        If att(2).Count > 0 Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                                writer.WriteLine(vbTab & vbTab & "Warnings:")
                            End Using
                            Dim wrn As String
                            For Each wrn In att(2)

                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                                    writer.WriteLine(vbTab & vbTab & vbTab & wrn)
                                End Using

                            Next
                            att(2).Clear()
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                                writer.WriteLine()
                            End Using

                        End If

                        If att(3).Count > 0 Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                                writer.WriteLine(vbTab & vbTab & "Notes:")
                            End Using
                            Dim note As String
                            For Each note In att(3)

                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                                    writer.WriteLine(vbTab & vbTab & vbTab & note)
                                End Using

                            Next

                            att(3).Clear()

                        End If

                    Next
                End If

            Next

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log", True, System.Text.Encoding.ASCII)
                writer.WriteLine()
                writer.WriteLine("Successfully Built: " & iPartsBuilt)
                writer.WriteLine("Failed to Build: " & iPartsFailed)
            End Using

            WaitGif.Enabled = False
            ts_Status.Text = "Successfully Built: " & iPartsBuilt & ", Failed to Build: " & iPartsFailed

            If iPartsFailed > 0 And iPartsFailed <> 1 Then
                Dim reply As DialogResult = MessageBox.Show(iPartsBuilt & " parts were successfully built but " & iPartsFailed & " failed to build. Would you like to view the results?", "Finished",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB from XML Mapping")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log")
                End If
            ElseIf iPartsFailed = 1 Then
                Dim reply As DialogResult = MessageBox.Show(1 & " part was successfully built but " & iPartsFailed & " failed to build. Would you like to view the results?", "Finished",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB from XML Mapping")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log")
                End If

            ElseIf (l_UnableToSavePartition.Count > 0) Then

                Dim s_ErrorMessage As New StringBuilder

                For Each Partition As String In l_UnableToSavePartition
                    s_ErrorMessage.AppendLine(Partition)
                Next

                Dim reply As DialogResult = MessageBox.Show("Unable to save the following partitions:" & Environment.NewLine & Environment.NewLine & s_ErrorMessage.ToString() & Environment.NewLine & Environment.NewLine & "Would you like to view the results?", "Finished",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB from XML Mapping")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log")
                End If
            Else
                MsgBox(iPartsBuilt & " parts were successfully built.")
            End If

            If File.Exists(frmMain.librarydata.LogPath & "Build PDB - Alternate Symbols.log") Then

                Dim reply As DialogResult = MessageBox.Show("ALE was unable to add alternate symbols to some parts. Would you like to view the results?", "Finished",
MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB - Alternate Symbols")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB - Alternate Symbols.log")
                End If

            End If

        End If
    End Sub

    Private Sub BuildFailed(ByVal buildError As BuildPDBError)
        If Me.InvokeRequired Then
            Dim d As New d_BuildFailed(AddressOf BuildFailed)
            Me.Invoke(d, New Object() {buildError})
        Else
            WaitGif.Enabled = False
            If buildError = BuildPDBError.errDueToUnableToOpenPDBEditor Then
                MsgBox("Unable to open the PDB editor. This is most likely due to a reserved partition.")
                ts_Status.Text = "Build Failed."
            ElseIf buildError = BuildPDBError.errDueToUnableToFindFile Then
                MsgBox("RefDesPartitions.caf was not found. Please locate the file to create partitions by reference designators.")
                ts_Status.Text = "Build Failed."
            End If
        End If
    End Sub

    Private Sub BuildPDB()

        'MentorGraphics
        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        'Integers
        Dim iPartsBuilt As Integer = 0
        Dim iPartsFailed As Integer = 0

        'ArrayList
        Dim alnewPDBPartitions As New ArrayList()

        'Dictionaries
        Dim dicLogReport As New Dictionary(Of String, ArrayList)
        Dim dic_Height As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)

        'Creates a handle to the Parts Editor in Library Manager
        'pedApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        pedApp = frmMain.libDoc.PartEditor
        Try
            'pedDoc = pedApp.OpenDatabaseEx(frmMain.librarydata.LibPath, False)
            pedDoc = pedApp.ActiveDatabaseEx
        Catch ex As Exception
            RaiseEvent eBuildFailed(BuildPDBError.errDueToUnableToFindFile)
            pedApp.Quit()
            pedApp = Nothing
            Exit Sub
        End Try

        If chkbox_GetHeight.Checked = True Then

            Dim cellEd As CellEditorAddinLib.CellEditorDlg
            Dim cellDB As CellEditorAddinLib.CellDB

            RaiseEvent eUpdateStatus("Opening cell editor...")

            ' Open the Cell Editor dialog and open the library database
            'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
            'cellDB = cellEd.OpenDatabase(frmMain.librarydata.LibPath, False)
            cellEd = frmMain.libDoc.CellEditor
            cellDB = cellEd.ActiveDatabase

            For Each oCellPartition As CellEditorAddinLib.Partition In cellDB.Partitions()

                For Each oCell As CellEditorAddinLib.Cell In oCellPartition.Cells  ' process each cell in the partition

                    RaiseEvent eUpdateStatus("Getting height for: " & cellDB.Name)
                    Select Case pedDoc.CurrentUnit

                        Case MGCPCBPartsEditor.EPDBUnit.epdbUnitInch
                            dic_Height.Add(oCellPartition.Name & ":" & oCell.Name, oCell.Height(CellEditorAddinLib.ECellDBUnit.ecelldbUnitInch))
                        Case MGCPCBPartsEditor.EPDBUnit.epdbUnitMils
                            dic_Height.Add(oCellPartition.Name & ":" & oCell.Name, oCell.Height(CellEditorAddinLib.ECellDBUnit.ecelldbUnitMils))
                        Case MGCPCBPartsEditor.EPDBUnit.epdbUnitMM
                            dic_Height.Add(oCellPartition.Name & ":" & oCell.Name, oCell.Height(CellEditorAddinLib.ECellDBUnit.ecelldbUnitMM))
                        Case MGCPCBPartsEditor.EPDBUnit.epdbUnitUM
                            dic_Height.Add(oCellPartition.Name & ":" & oCell.Name, oCell.Height(CellEditorAddinLib.ECellDBUnit.ecelldbUnitUM))
                    End Select

                Next

            Next

            cellEd.SaveActiveDatabase()
            'cellEd.CloseActiveDatabase(True)
            cellDB = Nothing
            cellEd.Quit()
            cellEd = Nothing

        End If

        RaiseEvent eUpdateStatus("Creating any missing partitions...")

        For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions    'Step through each part partition in the parts editor
            alnewPDBPartitions.Add(pdbPartition.Name)
        Next

        If File.Exists(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log") Then

            File.Delete(frmMain.librarydata.LogPath & "Build PDB from XML Mapping.log")

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

            End Try
        End If

        If Not IsNothing(oPartitions.Keys) Then
            For Each partitionName In oPartitions.Keys
                If Not alnewPDBPartitions.Contains(partitionName) Then

                    pedDoc.NewPartition(partitionName)

                End If
            Next
        End If

        For Each oPartition As AAL.PartPartition In oPartitions.Values
            Dim pdbPartition As MGCPCBPartsEditor.Partition = Nothing

            For Each pdbPartition In pedDoc.Partitions    'Step through each part partition in the parts editor

                Dim b_Difference As Boolean = String.Compare(pdbPartition.Name, oPartition.Name, True)

                If b_Difference = False Then
                    Exit For
                Else
                    pdbPartition = Nothing
                End If

            Next

            If IsNothing(pdbPartition) Then
                pdbPartition = pedDoc.NewPartition(oPartition.Name)
            End If

            'For Each oPart As AAL.Part In oPartition.Parts

            ' If chkbox_RebuildParts.Checked = True Then For Each pdbPart As MGCPCBPartsEditor.Part
            ' In pdbPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, oPart.Number)
            ' pdbPart.Delete() Next End If

            ' RaiseEvent eUpdateStatus("Building " & oPartition.Name & ":" & oPart.Number) Dim
            ' ocBuildPDB As Build_PDB = New Build_PDB ocBuildPDB.pdbPartition = pdbPartition
            ' ocBuildPDB.Heights = dic_Height ocBuildPDB.aalPart = oPart 'ocBuildPDB.dicGates =
            ' Nothing ocBuildPDB.b_RemoveIncompleteParts = chkbox_RemoveIncomplete.Checked
            ' ocBuildPDB.LibraryData = frmMain.librarydata 'ocBuildPDB.alPinAtts = partInfo(5)

            ' Dim success As Boolean = ocBuildPDB.NewPart()

            ' If success = True Then RaiseEvent eUpdateMainParts(pdbPartition.Name,
            ' ocBuildPDB.aalPart.Number) End If

            ' Dim objLogAtts = New Object() {oPart.Number, ocBuildPDB.Errors, ocBuildPDB.Warnings,
            ' ocBuildPDB.Notes, ocBuildPDB.LogAlternateSymbols}

            ' Dim alLog As ArrayList

            ' If Not (ocBuildPDB.Errors.Count = 0) Or Not (ocBuildPDB.Notes.Count = 0) Or Not
            ' (ocBuildPDB.Warnings.Count = 0) Or Not (ocBuildPDB.LogAlternateSymbols.Count = 0) Then
            ' If dicLogReport.TryGetValue(pdbPartition.Name, alLog) Then

            ' alLog.Add(objLogAtts) dicLogReport.Remove(pdbPartition.Name)
            ' dicLogReport.Add(pdbPartition.Name, alLog) Else

            ' Dim alLogNew As New ArrayList() alLogNew.Add(objLogAtts)
            ' dicLogReport.Add(pdbPartition.Name, alLogNew)

            ' End If End If

            ' If success = True Then

            ' iPartsBuilt += 1 Else

            ' iPartsFailed += 1

            ' End If

            'Next

            Try
                pedApp.SaveActiveDatabase()
            Catch ex As Exception
                l_UnableToSavePartition.Add(pdbPartition.Name)
            End Try

        Next

        ' pedApp.CloseActiveDatabase(True)
        pedApp.Quit()
        pedApp = Nothing
        pedDoc = Nothing

        RaiseEvent eBuildComplete(dicLogReport, iPartsFailed, iPartsBuilt)

    End Sub

    Private Sub frmBuild_PDB_from_Mapping_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eReadComplete, AddressOf ReadComplete
        AddHandler eBuildComplete, AddressOf BuildComplete
        AddHandler eBuildFailed, AddressOf BuildFailed
        AddHandler eUpdateMainParts, AddressOf UpdateMainParts

    End Sub

    Private Sub ReadComplete(ByVal bProblems As Boolean, Optional ByVal i_BadParts As Integer = 0)
        If Me.InvokeRequired Then

            If dicBadParts.Values.Count > 0 Then

                For Each kvp As KeyValuePair(Of String, Dictionary(Of String, List(Of String))) In dicBadParts

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping - Bad Part Data.log", True, System.Text.Encoding.ASCII)
                        writer.WriteLine(kvp.Key)
                    End Using

                    For Each kvpParts As KeyValuePair(Of String, List(Of String)) In kvp.Value

                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping - Bad Part Data.log", True, System.Text.Encoding.ASCII)
                            writer.WriteLine(vbTab & kvpParts.Key)
                        End Using

                        For Each s_Reason As String In kvpParts.Value
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping - Bad Part Data.log", True, System.Text.Encoding.ASCII)
                                writer.WriteLine(vbTab & vbTab & s_Reason)
                            End Using
                        Next

                        i_BadParts += 1

                    Next

                Next

                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Build PDB from XML Mapping - Bad Part Data.log", True, System.Text.Encoding.ASCII)
                    writer.WriteLine()
                    writer.WriteLine(i_BadParts & " parts are unable to be built because of various problems found.")
                End Using

            End If

            Dim d As New d_ReadComplete(AddressOf ReadComplete)
            Me.Invoke(d, New Object() {bProblems, i_BadParts})
        Else

            WaitGif.Enabled = False
            For Each oPartition As AAL.PartPartition In oPartitions.Values

                Dim oParentNode As New TreeNode

                oParentNode.Text = oPartition.Name

                For Each oPart As AAL.Part In oPartition.Values

                    Dim oPartNode As New TreeNode
                    oPartNode.Text = oPart.Number

                    Dim oSymbolLabelNode As New TreeNode
                    oSymbolLabelNode.Text = "Symbol(s):"

                    For Each sSymbol In oPart.Symbols.Keys.ToList
                        Dim oSymbolNode As New TreeNode
                        oSymbolNode.Text = sSymbol
                        oSymbolLabelNode.Nodes.Add(oSymbolNode)
                    Next

                    oSymbolLabelNode.Expand()

                    oPartNode.Nodes.Add(oSymbolLabelNode)

                    Dim oCellLabelNode As New TreeNode
                    oCellLabelNode.Text = "Cell(s):"

                    For Each sCell In oPart.Cells.Keys.ToList
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

            If i_BadParts > 0 Then

                Dim replyCell As DialogResult = MessageBox.Show("Excel file has been read, but " & i_BadParts & " parts cannot be processed. Would you like to view the results?", "Finished",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If replyCell = DialogResult.Yes Then
                    frmMain.OpenLogFile("Build PDB from XML Mapping - Bad Part Data")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Build PDB from XML Mapping - Bad Part Data.log")
                End If
            End If

            ts_Status.Text = "Read Complete: " & iPartCount & " eligible parts found."

        End If

        If Not tv_Parts.Nodes.Count = 0 Then

            btnBuild.Enabled = True

        End If

    End Sub

    Private Sub ReadParts(s_Path As String)

        Dim bProblems As Boolean = False

        Dim xmlDoc As New Xml.XmlDocument

        xmlDoc.Load(s_MappingPath)

        For Each nodePart As Xml.XmlNode In xmlDoc.DocumentElement.ChildNodes()
            bProblems = False
            Dim oPart As New AAL.Part

            oPart.Number = nodePart.Attributes("Number").Value
            If String.IsNullOrEmpty(nodePart.Attributes("Partition").Value) Then
                oPart.Partition = "ALE"
            Else
                oPart.Partition = nodePart.Attributes("Partition").Value
            End If

            RaiseEvent eUpdateStatus("Analyzing Part: " & oPart.Partition & ":" & oPart.Number)

            oPart.RefDes = nodePart.Attributes("RefDes").Value

            For Each nodeCell As Xml.XmlNode In nodePart.SelectSingleNode("Cells").ChildNodes

                Dim oCell As New AAL.Cell

                oCell.Name = nodeCell.Attributes("Name").Value
                oCell.Partition = nodeCell.Attributes("Partition").Value
                Dim sSide As String
                If Not IsNothing(nodeCell.Attributes("Side")) Then
                    sSide = nodeCell.Attributes("Side").Value
                End If

                If frmMain.librarydata.CellList.ContainsKey(oCell.Name) Then
                    Dim l_CellPartitions As List(Of String) = frmMain.librarydata.CellList.Item(oCell.Name)
                    Dim bFoundCell As Boolean = False

                    If IsNothing(oCell.Partition) Then
                        oCell.Partition = l_CellPartitions(0)
                        bFoundCell = True
                    Else
                        If l_CellPartitions.Contains(oCell.Partition, StringComparer.OrdinalIgnoreCase) Then
                            bFoundCell = True
                        End If

                        If bFoundCell = False Then
                            oCell.Partition = l_CellPartitions(0)
                            bFoundCell = True
                        End If

                    End If

                    If bFoundCell = False Then
                        Dim dic_Parts As Dictionary(Of String, List(Of String))
                        Dim l_Info As List(Of String) = Nothing
                        If dicBadParts.ContainsKey(oPart.Partition) Then
                            dic_Parts = dicBadParts.Item(oPart.Partition)
                        Else
                            dic_Parts = New Dictionary(Of String, List(Of String))
                            l_Info = New List(Of String)
                        End If

                        If dic_Parts.ContainsKey(oPart.Number) Then

                            l_Info = dic_Parts.Item(oPart.Number)
                        Else
                            l_Info = New List(Of String)
                        End If

                        l_Info.Add("Missing Cell: " & oCell.Partition & ":" & oCell.Name)

                        dic_Parts.Item(oPart.Number) = l_Info

                        dicBadParts.Item(oPart.Partition) = dic_Parts

                        bProblems = True
                    End If
                Else

                    Dim dic_Parts As Dictionary(Of String, List(Of String))
                    Dim l_Info As List(Of String) = Nothing
                    If dicBadParts.TryGetValue(oPart.Partition, dic_Parts) Then
                        dicBadParts.Remove(oPart.Partition)
                    End If

                    If IsNothing(dic_Parts) Then
                        dic_Parts = New Dictionary(Of String, List(Of String))
                        l_Info = New List(Of String)
                    Else
                        If dic_Parts.TryGetValue(oPart.Number, l_Info) Then

                            dic_Parts.Remove(oPart.Number)
                        Else
                            l_Info = New List(Of String)
                        End If
                    End If

                    l_Info.Add("Missing Cell: " & oCell.Name)

                    dic_Parts.Add(oPart.Number, l_Info)

                    dicBadParts.Add(oPart.Partition, dic_Parts)

                    bProblems = True

                    Continue For

                End If

                Select Case sSide

                    Case "Top"
                        oPart.TopCell = oCell.Name
                    Case "Bottom"
                        oPart.BotCell = oCell.Name

                End Select

                oPart.Cells.Add(oCell.Name, oCell)

            Next

            For Each nodeGate As Xml.XmlNode In nodePart.SelectSingleNode("Gates").ChildNodes

                Dim oGate As New AAL.Gate

                For Each nodeSymbol As Xml.XmlNode In nodeGate.SelectSingleNode("Symbols").ChildNodes
                    Dim oSymbol As New AAL.Symbol

                    oSymbol.Name = nodeSymbol.Attributes("Name").Value
                    oSymbol.Partition = nodeSymbol.Attributes("Partition").Value

                    Dim l_Partitions As List(Of String)
                    If frmMain.librarydata.SymbolList.TryGetValue(oSymbol.Name, l_Partitions) Then

                        Dim bFoundSymbol As Boolean = False

                        If IsNothing(oSymbol.Partition) Then
                            oSymbol.Partition = l_Partitions(0)
                            bFoundSymbol = True
                        Else
                            If l_Partitions.Contains(oSymbol.Partition, StringComparer.OrdinalIgnoreCase) Then
                                bFoundSymbol = True
                            End If

                            If bFoundSymbol = False Then
                                oSymbol.Partition = l_Partitions(0)
                                bFoundSymbol = True
                            End If

                        End If

                        If bFoundSymbol = False Then
                            Dim dic_Parts As Dictionary(Of String, List(Of String))
                            Dim l_Info As List(Of String) = Nothing
                            If dicBadParts.TryGetValue(oPart.Partition, dic_Parts) Then
                                dicBadParts.Remove(oPart.Partition)
                            End If

                            If IsNothing(dic_Parts) Then
                                dic_Parts = New Dictionary(Of String, List(Of String))
                                l_Info = New List(Of String)
                            Else
                                If dic_Parts.TryGetValue(oPart.Number, l_Info) Then

                                    dic_Parts.Remove(oPart.Number)
                                Else
                                    l_Info = New List(Of String)
                                End If
                            End If

                            l_Info.Add("Missing Symbol: " & oSymbol.Name)

                            dic_Parts.Add(oPart.Number, l_Info)

                            dicBadParts.Add(oPart.Partition, dic_Parts)

                            bProblems = True

                        End If
                    Else
                        Dim dic_Parts As Dictionary(Of String, List(Of String))
                        Dim l_Info As List(Of String) = Nothing
                        If dicBadParts.TryGetValue(oPart.Partition, dic_Parts) Then
                            dicBadParts.Remove(oPart.Partition)
                        End If

                        If IsNothing(dic_Parts) Then
                            dic_Parts = New Dictionary(Of String, List(Of String))
                            l_Info = New List(Of String)
                        Else
                            If dic_Parts.TryGetValue(oPart.Number, l_Info) Then

                                dic_Parts.Remove(oPart.Number)
                            Else
                                l_Info = New List(Of String)
                            End If
                        End If

                        l_Info.Add("Missing Symbol: " & oSymbol.Name)

                        dic_Parts.Add(oPart.Number, l_Info)

                        dicBadParts.Add(oPart.Partition, dic_Parts)

                        bProblems = True
                    End If

                    If String.IsNullOrEmpty(oGate.Name) Then

                        oGate.Name = "gate" & oSymbol.Name
                        oGate.Symbol = oSymbol

                    End If

                    If Not oPart.Symbols.ContainsKey(oSymbol.Partition & ":" & oSymbol.Name) Then
                        oPart.Symbols.Add(oSymbol.Partition & ":" & oSymbol.Name, oSymbol)
                    End If
                Next

                For Each nodeSlot As Xml.XmlNode In nodeGate.SelectSingleNode("Slots").ChildNodes

                    Dim oSlot As New AAL.Slot

                    oSlot.Name = nodeSlot.Attributes("Name").Value

                    For Each nodePin As Xml.XmlNode In nodeSlot.ChildNodes

                        Dim aalPin As New AAL.SymbolPin

                        aalPin.Name = nodePin.Attributes("Name").Value
                        aalPin.Number = nodePin.Attributes("Number").Value

                        oSlot.SymbolPins(aalPin.Name) = aalPin

                    Next

                    oGate.Slots.Add(oSlot.Name, oSlot)

                Next

                oPart.Gates.Add(oGate)

            Next

            If Not IsNothing(nodePart.SelectSingleNode("Properties")) Then
                For Each nodeProperty As Xml.XmlNode In nodePart.SelectSingleNode("Properties").ChildNodes

                    Dim sValue As String

                    If (String.Compare(nodeProperty.Attributes("Name").Value, "DESC", True) = 0) Or (String.Compare(nodeProperty.Attributes("Name").Value, "Description", True) = 0) Then
                        oPart.Description = nodeProperty.Attributes("Value").Value
                    Else
                        If oPart.Properties.ContainsKey(nodeProperty.Attributes("Name").Value) Then
                            sValue = oPart.Properties.Item(nodeProperty.Attributes("Name").Value)
                        End If

                        sValue = nodeProperty.Attributes("Value").Value

                        oPart.Properties.Item(nodeProperty.Attributes("Name").Value) = sValue
                    End If

                Next
            End If

            If Not IsNothing(nodePart.SelectSingleNode("Implicits")) Then
                For Each nodeImplicit As Xml.XmlNode In nodePart.SelectSingleNode("Implicits").ChildNodes

                    Dim l_Pins As List(Of String)

                    If oPart.ImplicitPins.ContainsKey(nodeImplicit.Attributes("Name").Value) Then
                        l_Pins = oPart.ImplicitPins.Item(nodeImplicit.Attributes("Name").Value)
                    Else
                        l_Pins = New List(Of String)
                    End If

                    For Each nodePin As Xml.XmlNode In nodeImplicit.ChildNodes
                        l_Pins.Add(nodePin.Attributes("Number").Value)
                    Next

                    oPart.ImplicitPins.Item(nodeImplicit.Attributes("Name").Value) = l_Pins

                Next
            End If

            If bProblems = False Then

                Dim oPartition As AAL.PartPartition

                If oPartitions.ContainsKey(oPart.Partition) Then
                    oPartition = oPartitions.Item(oPart.Partition)
                Else
                    oPartition = New AAL.PartPartition
                    oPartition.Name = oPart.Partition
                End If

                oPartition.Add(oPart)

                oPartitions.Item(oPart.Partition) = oPartition

            End If

        Next

        RaiseEvent eReadComplete(bProblems)

    End Sub

    Private Sub tbox_XMLMapping_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbox_XMLMapping.TextChanged

        If File.Exists(tbox_XMLMapping.Text) Then
            btnRead.Enabled = True
        End If

    End Sub

    Private Sub UpdateMainParts(Partition As String, Part As String)

        If Me.InvokeRequired Then

            Dim d As New d_UpdateMainParts(AddressOf UpdateMainParts)
            Me.Invoke(d, New Object() {Partition, Part})
        Else
            Dim l_Parts As List(Of String)
            If frmMain.librarydata.PartsByPartition.TryGetValue(Partition, l_Parts) Then
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

    'Property frmmain.librarydata As Data
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