Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Drawing

Public Class frmHealPDBwithExpedition

    'Integers
    Dim i_Errors As Integer = 0
    Dim i_Warnings As Integer = 0
    Dim i_Notes As Integer = 0
    Dim iBadPartsFromNetural As Integer = 0

    'Dictionary
    Dim dicLogReport As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)
    Dim dicBadParts As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
    Dim dicModifiedParts As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

    Private Property changeCase As Boolean

    Private Property symbolCase As String

    'Object
    'Property frmmain.librarydata As Data

    'Delegates
    Delegate Sub d_ScanComplete()
    Delegate Sub d_HealComplete(ByVal Errors As Boolean, ByVal Warnings As Boolean)
    Delegate Sub d_UpdateStatus(ByVal status As String)
    Delegate Sub d_Increment()

    'Events
    Event eUpdateStatus(status As String)
    Event eHealComplete(Errors As Boolean, Warnings As Boolean)

    Private Sub Mod_PDB_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        AddHandler eUpdateStatus, AddressOf UpdateStatus

        For Each sPartition As String In frmMain.librarydata.PartsByPartition.Keys

            chklistbox_PDBPartitions.Items.Add(sPartition, True)

        Next

    End Sub

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
            ts_Errors.Text += 1
        End If
    End Sub

    Private Sub LogWarning()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogWarning)
            Me.Invoke(d)
        Else
            ts_Warnings.Text += 1
        End If
    End Sub

    Private Sub LogNote()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogNote)
            Me.Invoke(d)
        Else
            ts_Notes.Text += 1
        End If
    End Sub

    Private Sub HealComplete(ByVal Errors As Boolean, ByVal Warnings As Boolean)
        If Me.InvokeRequired Then

            Dim d As New d_HealComplete(AddressOf HealComplete)
            Me.Invoke(d, New Object() {Errors, Warnings})
        Else
            WaitGif.Enabled = False

            If iBadPartsFromNetural > 0 Then
                Dim reply As DialogResult = MessageBox.Show("During the project analysis, some parts were found to be incomplete. Would you like to view the log file?", "Incomplete Parts:", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Heal PDB from Design - Bad Part Data")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Heal PDB from Design - Bad Part Data.log")
                End If
            End If

            If Errors = True Or Warnings = True Then

                Dim reply As DialogResult

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Heal PDB from Project")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Heal PDB from Project.log")
                End If

                If Errors = True Then
                    ts_Status.Text = "Finished with errors."

                    reply = MessageBox.Show("The Heal process is finished, but there were errors. Would you like to view the results?", "Finished", _
                      MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)
                Else
                    ts_Status.Text = "Finished with warnings."

                    reply = MessageBox.Show("The Heal process is finished, but there were warnings. Would you like to view the results?", "Finished", _
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
                End If

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Heal PDB from Project")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Heal PDB from Project.log")
                End If

            Else
                ts_Status.Text = "Finished"
                MessageBox.Show("Heal Process completed with no errors or warnings.", "Information:", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        End If
    End Sub

    Private Sub btn_HealLibrary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_HealLibrary.Click

        i_Errors = ts_Errors.Text = "0"
        i_Notes = ts_Warnings.Text = "0"
        i_Warnings = ts_Notes.Text = "0"

        If tbox_DxDesigner.Text = "" And tbox_ExpeditionPCB.Text = "" Then

            MsgBox("Please select either a DxDesigner source or Expedition PCB source.")
            tbox_DxDesigner.Focus()
            Exit Sub

        End If
        btn_HealLibrary.Enabled = False
        gb_Options.Enabled = False
        chklistbox_PDBPartitions.Enabled = False
        WaitGif.Enabled = True
        changeCase = chkbox_ChangeSymbolCase.Checked
        symbolCase = cbox_SymbolCase.Text

        ts_Status.Text = "Starting heal process..."

        'AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eHealComplete, AddressOf HealComplete
        Dim t_Heal As Thread = New Threading.Thread(AddressOf Healing)
        t_Heal.IsBackground = True
        t_Heal.Start()

    End Sub

    Private Function HealPDB(ByVal sPartition As Object, ByRef dicPrjPartData As Dictionary(Of String, Object))
        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        'Creates a handle to the Parts Editor in Library Manager
        'pedApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        pedApp = frmMain.libDoc.PartEditor
        pedApp.LockServer()
        Try
            pedDoc = pedApp.ActiveDatabaseEx
            'pedDoc = pedApp.OpenDatabaseEx(frmMain.librarydata.LibPath, False)
        Catch ex As Exception
            pedApp = Nothing
            MsgBox("Error: A partition is reserved in the library. Please open Library Manager and unreserve all partitions.")
            Exit Function
        End Try

        Dim dicLogReport As New Dictionary(Of String, HealInfo)

        For Each Partition As MGCPCBPartsEditor.Partition In pedDoc.Partitions     'Step through each part partition in the parts editor

            If chklistbox_PDBPartitions.CheckedItems.Contains(Partition.Name) Then

                RaiseEvent eUpdateStatus("Healing partition: " & Partition.Name)

                Dim cHealPDB As Heal_PDB = New Heal_PDB()
                AddHandler cHealPDB.eUpdateStatus, AddressOf UpdateStatus

                cHealPDB.bSupplementData = False

                cHealPDB.bRepairErrors = chkbox_RepairErrors.Checked
                cHealPDB.bUpdatePartType = chkbox_UpdatePartType.Checked
                cHealPDB.bRemoveSpaceFromCell = chkbox_RemoveSpacesfromCells.Checked
                cHealPDB.bAddNCPins = chkbox_AddNC.Checked
                cHealPDB.bSupplementData = True
                cHealPDB.bUpdateSymPartition = chkbox_UpdateSymPartition.Checked

                If changeCase = True Then
                    cHealPDB.s_SymbolCase = symbolCase
                End If

                cHealPDB.PartsToHeal = dicPrjPartData.Keys.ToList()
                cHealPDB.dicPrjPartData = dicPrjPartData
                cHealPDB.LibraryData = frmMain.librarydata
                AddHandler cHealPDB.LogError, AddressOf LogError
                AddHandler cHealPDB.LogWarning, AddressOf LogWarning
                AddHandler cHealPDB.LogNote, AddressOf LogNote

                cHealPDB.HealParts(Partition)
                dicLogReport.Add(Partition.Name, cHealPDB.HealLog)

                Try
                    pedApp.SaveActiveDatabase()
                Catch ex As Exception

                    Dim oHealInfo As Log

                    dicLogReport.Remove(Partition.Name)
                    dicLogReport.Add(Partition.Name, Nothing)

                    'If dicLogReport.TryGetValue(Partition.Name, oHealInfo) Then

                    '    oHealInfo.Errors.Add("Fatal Error: Could not save database.")

                    '    dicLogReport.Remove(Partition.Name)
                    '    dicLogReport.Add(Partition.Name, oHealInfo)

                    'End If

                End Try

                For Each part As MGCPCBPartsEditor.Part In Partition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll)
                    Dim partNumber As String = part.Number

                    If (partNumber.Contains("^new")) Or (partNumber.Contains("^NewPart")) Then
                        Dim partName As String() = Split(partNumber, "^")
                        part.Number = partName(0)
                    End If

                Next

                Try
                    pedApp.SaveActiveDatabase()
                Catch ex As Exception

                    Dim oHealInfo As Log

                    dicLogReport.Remove(Partition.Name)
                    dicLogReport.Add(Partition.Name, Nothing)

                    'If dicLogReport.TryGetValue(Partition.Name, oHealInfo) Then

                    '    oHealInfo.Errors.Add("Fatal Error: Could not save database.")

                    '    dicLogReport.Remove(Partition.Name)
                    '    dicLogReport.Add(Partition.Name, oHealInfo)

                    'End If

                End Try

            End If

        Next

        pedApp.SaveActiveDatabase()
        pedApp.UnlockServer()
        pedDoc = Nothing
        pedApp = Nothing
        Return dicLogReport

    End Function

    Private Sub chkbox_ChangeSymbolCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_ChangeSymbolCase.CheckedChanged

        If chkbox_ChangeSymbolCase.Checked = True Then
            cbox_SymbolCase.Enabled = True
        Else
            cbox_SymbolCase.Enabled = False
        End If

    End Sub

    Private Sub chkbox_UpdateSymPartition_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_UpdateSymPartition.CheckedChanged

        If chkbox_UpdateSymPartition.Checked = False Then

            chkbox_RepairErrors.Checked = False
            chkbox_RepairErrors.Enabled = False

        Else

            chkbox_RepairErrors.Enabled = True

        End If

    End Sub

    Private Sub btn_BrowseDxD_Click(sender As System.Object, e As System.EventArgs) Handles btn_BrowseDxD.Click

        Select Case chkbox_AddNC.Checked
            Case True
                Using ofd As New OpenFileDialog
                    ofd.Filter = "DxDesigner Schematic (*.prj)|*.prj"
                    'ofd.Filter = "All files (*.*)|*.*"
                    ofd.Title = "Select File"
                    ofd.InitialDirectory = frmMain.librarydata.LibPath

                    If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                        tbox_DxDesigner.Clear()
                        tbox_DxDesigner.Text = ofd.FileName

                        If Not tbox_ExpeditionPCB.Text = Nothing Then

                            btn_HealLibrary.Enabled = True

                        End If

                    End If

                End Using

            Case False

                Using ofd As New OpenFileDialog
                    ofd.Filter = "DxDesigner Schematic (*.prj)|*.prj"
                    'ofd.Filter = "All files (*.*)|*.*"
                    ofd.Title = "Select File"
                    ofd.InitialDirectory = frmMain.librarydata.LibPath

                    If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                        tbox_DxDesigner.Clear()
                        tbox_DxDesigner.Text = ofd.FileName

                        If Not tbox_ExpeditionPCB.Text = Nothing Then

                            btn_HealLibrary.Enabled = True

                        End If

                    End If

                End Using

        End Select
    End Sub

    Private Sub Healing()

        Dim dicPrjPartData As New Dictionary(Of String, Object)

        If File.Exists(frmMain.librarydata.LogPath & "\Heal PDB from Project - Bad Part Data.log") Then

            File.Delete(frmMain.librarydata.LogPath & "\Heal PDB from Project - Bad Part Data.log")

        End If

        If File.Exists(frmMain.librarydata.LogPath & "\Heal PDB from Project - Modified Part Data.log.log") Then

            File.Delete(frmMain.librarydata.LogPath & "\Heal PDB from Project - Modified Part Data.log.log")

        End If

        Dim ocReadEEProject(1) As ProjectRead
        Dim newThreads(2) As Thread

        WaitGif.Enabled = True

        RaiseEvent eUpdateStatus("Reading Project Data")

        Dim ocDxDProjectInfo As ProjectRead = New ProjectRead
        ocDxDProjectInfo.closeDxD = True
        ocDxDProjectInfo.dicDxdPartData.Clear()
        ocDxDProjectInfo.sDxDPath = tbox_DxDesigner.Text
        ocReadEEProject(0) = ocDxDProjectInfo

        newThreads(0) = New Thread(AddressOf ocReadEEProject(0).openDxD)
        newThreads(0).IsBackground = True
        newThreads(0).Start(frmMain.progID)

        Dim ocExpProjectInfo As ProjectRead = New ProjectRead
        AddHandler ocExpProjectInfo.reUpdateStatus, AddressOf UpdateStatus
        ocExpProjectInfo.closeExp = True
        ocExpProjectInfo.dicExpRefDesCellData.Clear()
        ocExpProjectInfo.sExpPath = tbox_ExpeditionPCB.Text
        ocReadEEProject(1) = ocExpProjectInfo

        newThreads(1) = New Thread(AddressOf ocReadEEProject(1).openExp)
        newThreads(1).IsBackground = True
        newThreads(1).Start(frmMain.progID)

        newThreads(0).Join()
        If ocReadEEProject(0).success = False Then
            MsgBox("Could not get data from DxDesigner.")
            RaiseEvent eUpdateStatus("Could not get data from DxDesigner.")
            Exit Sub
        End If

        newThreads(1).Join()
        If ocReadEEProject(1).success = False Then
            MsgBox("Could not get data from Expedition.")
            RaiseEvent eUpdateStatus("Could not get data from Expedition.")
            Exit Sub
        End If

        RaiseEvent eUpdateStatus("Compiling Generic Part List")

        MergeProjectData(ocReadEEProject(0).dicDxDRefDes, ocReadEEProject(0).dicDxdPartData, ocReadEEProject(1).dicExpRefDesCellData, dicPrjPartData)

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log")

        End If

        Dim dicLogReport As Dictionary(Of String, HealInfo) = HealPDB(Nothing, dicPrjPartData)

        'End If

        Dim b_Warnings As Boolean = False
        Dim b_Errors As Boolean = False

        For Each kvp As KeyValuePair(Of String, HealInfo) In dicLogReport
            'Grab part number and part attributes:
            Dim sPartition As String = kvp.Key
            Dim oHealInfo As HealInfo = kvp.Value

            If IsNothing(oHealInfo) Then

                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                    writer.WriteLine(sPartition & ":")
                    writer.WriteLine("Fatal Error: Could not save database.")
                    writer.WriteLine()

                End Using

                b_Errors = True

                Continue For

            End If
            If oHealInfo.Log.Count > 0 Then
                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                    writer.WriteLine(sPartition & ":")
                    writer.WriteLine("Parts Successfully Fixed: " & oHealInfo.Success)
                    writer.WriteLine("Unable to Fix: " & oHealInfo.Failed)
                    writer.WriteLine("Percentage Complete: " & (oHealInfo.Success / (oHealInfo.Success + oHealInfo.Failed) * 100) & "%")
                    writer.WriteLine()

                End Using

                For Each pair As KeyValuePair(Of String, Log) In oHealInfo.Log

                    If pair.Value.Errors.Count > 0 Then
                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                            writer.WriteLine(vbTab & pair.Key)

                        End Using
                    ElseIf pair.Value.Warnings.Count > 0 Then
                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                            writer.WriteLine(vbTab & pair.Key)

                        End Using
                    ElseIf pair.Value.Notes.Count > 0 Then
                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                            writer.WriteLine(vbTab & pair.Key)

                        End Using
                    Else
                        Continue For
                    End If

                    If pair.Value.Errors.Count > 0 Then
                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                            writer.WriteLine(vbTab & vbTab & "Errors:")

                        End Using

                        For Each sErr As String In pair.Value.Errors
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                                writer.WriteLine(vbTab & vbTab & vbTab & sErr)

                            End Using
                        Next

                        b_Errors = True

                    End If

                    If pair.Value.Warnings.Count > 0 Then
                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                            writer.WriteLine(vbTab & vbTab & "Warnings:")

                        End Using

                        For Each sWrn As String In pair.Value.Warnings
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                                writer.WriteLine(vbTab & vbTab & vbTab & sWrn)

                            End Using
                        Next

                        b_Warnings = True
                    End If

                    If pair.Value.Notes.Count > 0 Then
                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                            writer.WriteLine(vbTab & vbTab & "Notes:")

                        End Using

                        For Each sNote As String In pair.Value.Notes
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                                writer.WriteLine(vbTab & vbTab & vbTab & sNote)

                            End Using
                        Next

                    End If

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                        writer.WriteLine()

                    End Using

                Next

                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\" & "Heal PDB from Project.log", True)

                    writer.WriteLine()

                End Using

            End If

        Next

        RaiseEvent eHealComplete(b_Errors, b_Warnings)

    End Sub

    Private Sub btn_BrowseExp_Click(sender As System.Object, e As System.EventArgs) Handles btn_BrowseExp.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "Expedition PCB (*.pcb)|*.pcb"
            '            ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"
            ofd.InitialDirectory = frmMain.librarydata.LibPath

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                tbox_ExpeditionPCB.Text = ofd.FileName

                Dim PCBPath = Path.GetDirectoryName(ofd.FileName)
                Dim PCBName = ofd.FileName

                btn_HealLibrary.Enabled = True
                btn_HealLibrary.Focus()

            End If
        End Using
    End Sub

    Private Function MergeProjectData(ByRef dicDxDesignerRefDes As Dictionary(Of String, List(Of String)), ByRef dicDxDesignerPartData As Dictionary(Of String, AAL.PartPartition), ByRef dicExpeditionData As Dictionary(Of String, Object), ByRef dicPrjPartData As Dictionary(Of String, Object)) As Dictionary(Of String, Object)

        Dim sb_Output As New StringBuilder
        Dim sb_RefDesPartList As New StringBuilder
        Dim sb_PNMissingFromBoard As New StringBuilder
        Dim bMissingPNs As Boolean = False

        sb_Output.AppendLine("# Generated " & Date.Now)
        sb_Output.AppendLine("# Part Number" & vbTab & "Cell" & vbTab & "Symbol" & vbTab & "Reference Designator")
        sb_Output.AppendLine("#-------------------------------------------------------------------")

        For Each partition As AAL.PartPartition In dicDxDesignerPartData.Values
            For Each part As AAL.Part In partition.Parts

                Dim bProblem As Boolean = False
                Dim sb_PN As New StringBuilder
                sb_PN.Append(part.Number & vbTab)

                Dim sSymbols As String = Nothing
                Dim alSymbolList As New ArrayList

                For Each sSymbol In part.Symbols.Keys
                    If IsNothing(sSymbols) Then
                        sSymbols = sSymbol
                    Else
                        sSymbols += "," & sSymbol
                    End If
                    bProblem = addSymbol(sSymbol, bProblem, alSymbolList, part.Number)
                Next

                sb_PN.Append(sSymbols & vbTab)

                Dim sCells As String = Nothing

                Dim alCellList As New ArrayList()

                If (dicDxDesignerRefDes.ContainsKey(part.Number)) Then

                    For Each sRefDes As String In dicDxDesignerRefDes.Item(part.Number)
                        If dicExpeditionData.ContainsKey(sRefDes) Then
                            If IsNothing(sCells) Then
                                sCells = dicExpeditionData.Item(sRefDes).CellName
                            Else
                                sCells += "," & dicExpeditionData.Item(sRefDes).CellName
                            End If

                            If Not alCellList.Contains(dicExpeditionData.Item(sRefDes).CellName) Then
                                alCellList.Add(dicExpeditionData.Item(sRefDes).CellName)
                            End If

                            bProblem = AddCell(dicExpeditionData.Item(sRefDes).CellName, bProblem, alCellList, part.Number)
                            sb_RefDesPartList.AppendLine(sRefDes & " - Dxd:" & part.Number & ", Exp:" & dicExpeditionData.Item(sRefDes).PartNumber)
                            sb_PN.Append(sCells)
                        Else
                            bMissingPNs = True
                            sb_PNMissingFromBoard.AppendLine(sRefDes & " - Dxd:" & part.Number)
                        End If
                    Next

                End If

                sb_Output.AppendLine(sb_PN.ToString())

                If alCellList.Count = 0 Then
                    Dim l_Info As List(Of String) = Nothing
                    If dicBadParts.ContainsKey(part.Number) Then
                        l_Info = dicBadParts.Item(part.Number)
                    Else
                        l_Info = New List(Of String)
                    End If

                    l_Info.Add("No valid cells found, most likely this part is not on the board.")

                    dicBadParts.Item(part.Number) = l_Info

                    bProblem = True
                End If

                If bProblem = False Then

                    Dim PartInfo As Object = New With {.RefDesPrefix = "", .Symbols = alSymbolList, .Cells = alCellList}

                    If Not dicPrjPartData.ContainsKey(part.Number.Trim) Then
                        dicPrjPartData.Item(part.Number.Trim) = PartInfo
                    Else
                        Dim sBadPN As Boolean = True
                    End If

                End If
            Next

        Next

        If File.Exists(Path.GetDirectoryName(tbox_DxDesigner.Text) & "\LogFiles\Generic Part List.txt") Then
            File.Delete(Path.GetDirectoryName(tbox_DxDesigner.Text) & "\LogFiles\Generic Part List.txt")
        End If

        If Not Directory.Exists(Path.GetDirectoryName(tbox_DxDesigner.Text) & "\LogFiles\") Then
            Directory.CreateDirectory(Path.GetDirectoryName(tbox_DxDesigner.Text) & "\LogFiles\")
        End If

        Using writer As StreamWriter = New StreamWriter(Path.GetDirectoryName(tbox_DxDesigner.Text) & "\LogFiles\Generic Part List.txt", False)
            writer.Write(sb_Output.ToString())
        End Using

        If bMissingPNs = True Then
            Using writer As StreamWriter = New StreamWriter(Path.GetDirectoryName(tbox_DxDesigner.Text) & "\LogFiles\Ref Des Part List.log", True, System.Text.Encoding.ASCII)
                writer.WriteLine("The following parts were found in the schematic but not in the PCB:")
                writer.WriteLine(sb_PNMissingFromBoard.ToString)
                writer.WriteLine()
                writer.WriteLine("The following parts were found in both the schematic and PCB:")
                writer.WriteLine(sb_RefDesPartList.ToString)
            End Using
        End If

        If dicBadParts.Count > 0 Then

            Dim sb_BadParts As New StringBuilder

            For Each kvp As KeyValuePair(Of String, List(Of String)) In dicBadParts

                sb_BadParts.AppendLine(kvp.Key)

                For Each s_Reason As String In kvp.Value
                    sb_BadParts.AppendLine(vbTab & s_Reason)
                Next

                iBadPartsFromNetural += 1

            Next

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB from Design - Bad Part Data.log", True, System.Text.Encoding.ASCII)
                writer.WriteLine(sb_BadParts.ToString())
                writer.WriteLine()
                writer.WriteLine(iBadPartsFromNetural & " parts have issues that may cause errors with the update process.")
            End Using

        End If

        If dicModifiedParts.Count > 0 Then

            Dim sb_ModifiedParts As New StringBuilder

            For Each kvp As KeyValuePair(Of String, List(Of String)) In dicModifiedParts

                sb_ModifiedParts.AppendLine(kvp.Key)

                For Each s_Reason As String In kvp.Value
                    sb_ModifiedParts.AppendLine(vbTab & s_Reason)
                Next

            Next

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB from Design- Modified Part Data.log", True, System.Text.Encoding.ASCII)
                writer.WriteLine(sb_ModifiedParts.ToString())
            End Using

        End If

    End Function

    Private Function addSymbol(ByVal sSymbol As String, ByVal bProblem As Boolean, ByRef SymbolList As ArrayList, ByVal PartNumber As String) As Boolean
        Dim symname As String
        Dim sSymPartition As String = Nothing

        sSymbol = sSymbol.Trim()

        If String.IsNullOrEmpty(sSymbol) Then
            Return False
        End If

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

                If Not SymbolList.Contains(sSymPartition & ":" & symname) Then SymbolList.Add(sSymPartition & ":" & symname)

            Else

                If Not SymbolList.Contains(l_Partitions(0) & ":" & symname) Then SymbolList.Add(l_Partitions(0) & ":" & symname)

                Dim l_Info As List(Of String) = Nothing

                If dicModifiedParts.ContainsKey(PartNumber) Then
                    l_Info = dicModifiedParts.Item(PartNumber)
                Else
                    l_Info = New List(Of String)
                End If

                Dim sMessage As String = "Unable to find symbol " & symname & " in partition " & sSymPartition & " but did find it in partition " & l_Partitions(0)

                If Not (l_Info.Contains(sMessage)) Then
                    l_Info.Add(sMessage)
                End If

                dicModifiedParts.Item(PartNumber) = l_Info

            End If

        Else
            Dim l_Info As List(Of String) = Nothing
            If dicBadParts.ContainsKey(PartNumber) Then
                l_Info = dicBadParts.Item(PartNumber)
            Else
                l_Info = New List(Of String)
            End If

            l_Info.Add("Missing Symbol: " & sSymbol)

            dicBadParts.Item(PartNumber) = l_Info

            bProblem = True

        End If

        Return bProblem
    End Function

    Private Function AddCell(ByVal sCell As String, ByVal bProblem As Boolean, ByRef CellList As ArrayList, ByVal PartNumber As String)
        sCell = sCell.Trim()
        Dim sPartition As String = Nothing

        If String.IsNullOrEmpty(sCell) Then
            Return False
        End If

        If sCell.Contains(":") Then

            Dim sCellNameSplit() As String = sCell.Split(":")

            sPartition = sCellNameSplit(0)
            sCell = sCellNameSplit(1)

        End If

        Dim l_CellPartitions As List(Of String)
        If frmMain.librarydata.CellList.ContainsKey(sCell) Then

            l_CellPartitions = frmMain.librarydata.CellList.Item(sCell)

            Dim sCellName As String = sCell

            Dim sCellPartition As String

            If IsNothing(sPartition) Then
                sCellPartition = l_CellPartitions(0)
            Else
                For Each CellPartition In l_CellPartitions
                    If CellPartition = sPartition Then
                        sCellPartition = sPartition
                        Exit For
                    End If
                Next

                If String.IsNullOrEmpty(sPartition) Then

                    sCellPartition = l_CellPartitions(0)

                    Dim l_Info As List(Of String) = Nothing

                    If dicModifiedParts.ContainsKey(PartNumber) Then
                        l_Info = dicModifiedParts.Item(PartNumber)
                    Else
                        l_Info = New List(Of String)
                    End If

                    Dim sMessage As String = "Unable to find cell " & sCell & " in partition " & sPartition & " but did find it in partition " & l_CellPartitions(0)

                    If Not (l_Info.Contains(sMessage)) Then
                        l_Info.Add(sMessage)
                    End If

                    dicModifiedParts.Item(PartNumber) = l_Info

                End If

            End If

            CellList.Add(sCellPartition & ":" & sCell)

        Else

            Dim l_Info As List(Of String) = Nothing
            If dicBadParts.ContainsKey(PartNumber) Then
                l_Info = dicBadParts.Item(PartNumber)
            Else
                l_Info = New List(Of String)
            End If

            l_Info.Add("Missing Cell from Library: " & sCell & ", removing from list.")

            dicBadParts.Item(PartNumber) = l_Info

            'bProblem = True

        End If

        Return bProblem

    End Function

End Class