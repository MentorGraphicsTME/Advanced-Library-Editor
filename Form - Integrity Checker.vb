Imports System.Text
Imports System.Threading
Imports System.IO
Imports Microsoft.Win32
Imports System.Runtime.InteropServices
Imports System.Drawing

Public Class frmIntegrityCheck

    'Objects
    Dim oCellLock As New Object
    Dim oPartLock As Object
    Dim oSymbolLock As Object
    Dim nodeSymbols As TreeNode

    'List
    Dim l_MissingPadstacks As New List(Of String)

    'Dictionaries
    Dim dicOrphanCells As New Dictionary(Of String, List(Of String))
    Dim dicOrphanSymbols As New Dictionary(Of String, List(Of String))
    Dim dicOrphanParts As New Dictionary(Of String, List(Of String))
    Dim dicOrphanPadstacks As New Dictionary(Of String, List(Of String))
    Dim dicPartsMissingSymbols As New Dictionary(Of String, List(Of String))
    Dim dicPartsMissingCells As New Dictionary(Of String, List(Of String))
    Dim dicPartsToProccess As New Dictionary(Of String, List(Of String))
    Dim dicCellsToProccess As New Dictionary(Of String, List(Of String))
    Dim dicSymbolsToProcess As New Dictionary(Of String, List(Of String))
    Dim dicPadstacksToProcess As New Dictionary(Of String, List(Of String))

    'Events
    Event eCheckNode(ByVal node As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)
    Event eUpdateNodesFinished()
    Event eUpdateStatus(status As String)
    Event eDecrementPadstacks()
    Event eReportFinished()
    Event ePurgeFinished()

    'Delegates
    Delegate Sub d_CheckNode(ByVal value As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)
    Delegate Sub d_ReportFinished()
    Delegate Sub d_UpdateStatus(ByVal status As String)
    Delegate Sub d_IncrementSymbol()
    Delegate Sub d_IncrementCell()
    Delegate Sub d_IncrementPart()
    Delegate Sub d_IncrementPadstacks()
    Delegate Sub d_CheckFinished()
    Delegate Sub d_PurgeFinished()
    Delegate Sub d_IncrementPartMissingSymbol()
    Delegate Sub d_IncrementPartMissingCell()

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub btnScanLibrary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScanLibrary.Click

        WaitGif.Enabled = True

        ts_Status.Text = "Scanning Central Library"

        frmMain.NotifyIcon.ShowBalloonTip(2000, "Library Integrity Check:", "Scanning Central Library", ToolTipIcon.Info)

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "\Library Integrity.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "\Library Integrity.log")

        End If

        gbInfo.Enabled = False
        btnScanLibrary.Enabled = False

        tv_Problems.Nodes.Clear()

        btnPurgeAllItems.Enabled = False
        RemoveHandler tv_Problems.BeforeCheck, AddressOf tv_Problems_BeforeCheck
        AddHandler eReportFinished, AddressOf ReportComplete
        Dim th_ProcessThread As Thread = New Thread(AddressOf RunIntegrityReport)
        th_ProcessThread.IsBackground = True
        th_ProcessThread.Start()

    End Sub

    Private Sub RunIntegrityReport()

        Dim th_Cells(frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kCELL).Count) As Thread

        Dim i As Integer = 0

        For Each lmCellPartition As LibraryManager.IMGCLMPartition In frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kCELL)

            Dim oGetOrphan As New LibraryRead

            oGetOrphan.libDoc = frmMain.libDoc
            AddHandler oGetOrphan.eOrphanCell, AddressOf IncrementOrphanCell
            AddHandler oGetOrphan.eOrphanCellsComplete, AddressOf CellPartitionComplete

            th_Cells(i) = New Thread(AddressOf oGetOrphan.GetOrphanCells)
            th_Cells(i).Start(lmCellPartition.Name)

            i += 1
        Next

        Dim th_Symbols(frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL).Count) As Thread
        i = 0
        For Each lmSymbolPartition As LibraryManager.IMGCLMPartition In frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL)

            Dim oGetOrphan As New LibraryRead

            oGetOrphan.libDoc = frmMain.libDoc
            AddHandler oGetOrphan.eOrphanSymbol, AddressOf IncrementOrphanSymbol
            AddHandler oGetOrphan.eOrphanSymbolsComplete, AddressOf SymbolPartitionComplete

            th_Symbols(i) = New Thread(AddressOf oGetOrphan.GetOrphanSymbols)
            th_Symbols(i).Start(lmSymbolPartition.Name)

            i += 1
        Next

        Dim th_Parts(frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kPART).Count) As Thread
        i = 0
        For Each lmPartPartition As LibraryManager.IMGCLMPartition In frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kPART)

            Dim oGetOrphan As New LibraryRead

            oGetOrphan.libDoc = frmMain.libDoc
            AddHandler oGetOrphan.eOrphanPart, AddressOf IncrementOrphanPart
            AddHandler oGetOrphan.ePartMissingSymbol, AddressOf IncrementPartMissingSmbol
            AddHandler oGetOrphan.ePartMissingCell, AddressOf IncrementPartMissingCell
            AddHandler oGetOrphan.eOrphanPartsComplete, AddressOf PartPartitionComplete

            th_Parts(i) = New Thread(AddressOf oGetOrphan.GetOrphanParts)
            th_Parts(i).Start(lmPartPartition.Name)

            i += 1
        Next

        'For Each lmPadstack As LibraryManager.IMGCLMPadstack In frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kPADSTACK)
        '    If lmPadstack.AssociatedCells.Count > 0 Then
        '        Dim l_Padstacks As List(Of String) = dicOrphanPadstacks.Item(lmPadstack.PartitionName)

        '        If l_Padstacks.Contains(lmPadstack.Name) Then
        '            l_Padstacks.Remove(lmPadstack.Name)
        '        End If

        '        dicOrphanPadstacks.Item(lmPadstack.PartitionName) = l_Padstacks

        '    Else
        '        IncrementOrphanPadstack()
        '    End If
        'Next

        For iThread As Integer = 0 To frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kCELL).Count - 1
            th_Cells(iThread).Join()
        Next

        For iThread As Integer = 0 To frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL).Count - 1
            th_Symbols(iThread).Join()
        Next

        For iThread As Integer = 0 To frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kPART).Count - 1
            th_Parts(iThread).Join()
        Next

        RaiseEvent eReportFinished()

    End Sub

    Private Sub btnViewReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If IO.File.Exists(frmMain.librarydata.LogPath & "Library Integrity.log") Then
            Dim new_process As New Process
            new_process.StartInfo.FileName = frmMain.librarydata.LogPath & "Library Integrity.log"
            new_process.StartInfo.Verb = "Open"
            new_process.Start()
        Else

            MsgBox("File currently unavailable to open...")

        End If

    End Sub

    Private Sub btnPurgeAllItems_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPurgeAllItems.Click

        dicCellsToProccess.Clear()
        dicSymbolsToProcess.Clear()
        dicPadstacksToProcess.Clear()
        dicPartsToProccess.Clear()

        For Each nodeParent As TreeNode In tv_Problems.Nodes

            Select Case nodeParent.Text

                Case "Orphan Parts"

                    For Each nodePartition As TreeNode In nodeParent.Nodes

                        Dim lParts As List(Of String)

                        If dicPartsToProccess.ContainsKey(nodePartition.Text) Then
                            lParts = dicPartsToProccess.Item(nodePartition.Text)
                        Else
                            lParts = New List(Of String)
                        End If

                        For Each nodePart As TreeNode In nodePartition.Nodes
                            If nodePart.Checked = True Then
                                lParts.Add(nodePart.Text)
                            End If
                        Next

                        If lParts.Count > 0 Then
                            dicPartsToProccess.Item(nodePartition.Text) = lParts
                        End If

                    Next

                Case "Orphan Cells"

                    For Each nodePartition As TreeNode In nodeParent.Nodes

                        Dim l_Cells As List(Of String)

                        If dicCellsToProccess.ContainsKey(nodePartition.Text) Then
                            l_Cells = dicCellsToProccess.Item(nodePartition.Text)
                        Else
                            l_Cells = New List(Of String)
                        End If

                        For Each nodeCell As TreeNode In nodePartition.Nodes
                            If nodeCell.Checked = True Then
                                l_Cells.Add(nodeCell.Text)
                            End If
                        Next

                        If l_Cells.Count > 0 Then
                            dicCellsToProccess.Item(nodePartition.Text) = l_Cells
                        End If

                    Next

                Case "Orphan Padstacks"

                    Dim oPadstack As New AAL.Padstack

                    For Each nodePartition As TreeNode In nodeParent.Nodes
                        Dim l_Padstacks As New List(Of String)
                        For Each nodePadstack As TreeNode In nodePartition.Nodes
                            If nodePadstack.Checked = True Then
                                l_Padstacks.Add(nodePadstack.Text)
                            End If
                        Next

                        If l_Padstacks.Count > 0 Then
                            dicPadstacksToProcess.Add(nodePartition.Text, l_Padstacks)
                        End If

                    Next

                Case "Orphan Symbols"

                    If frmMain.librarydata.Type = Data.LibType.DX Then
                        For Each nodePartition As TreeNode In nodeParent.Nodes

                            Dim l_Symbols As List(Of String)

                            If dicSymbolsToProcess.ContainsKey(nodePartition.Text) Then
                                l_Symbols = dicSymbolsToProcess.Item(nodePartition.Text)
                            Else
                                l_Symbols = New List(Of String)
                            End If

                            For Each nodeSymbol As TreeNode In nodePartition.Nodes
                                If nodeSymbol.Checked = True Then
                                    l_Symbols.Add(nodeSymbol.Text)
                                End If
                            Next

                            If l_Symbols.Count > 0 Then
                                dicSymbolsToProcess.Item(nodePartition.Text) = l_Symbols
                            End If

                        Next

                        If dicSymbolsToProcess.Count > 0 Then
                            If MessageBox.Show("Deleting symbols will require library manager to be re-indexed. When re-indexing the library, PDB data must be perfect in order to re-index right." & Environment.NewLine & Environment.NewLine & "Press OK to continue...", "Warning:", MessageBoxButtons.OKCancel,
    MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Cancel Then
                                Exit Sub
                            End If
                        End If
                    End If

                Case "Parts Missing Cells"

                    For Each nodePartition As TreeNode In nodeParent.Nodes

                        Dim lParts As List(Of String)

                        If dicPartsToProccess.ContainsKey(nodePartition.Text) Then
                            lParts = dicPartsToProccess.Item(nodePartition.Text)
                        Else
                            lParts = New List(Of String)
                        End If

                        For Each nodePart As TreeNode In nodePartition.Nodes
                            If nodePart.Checked = True Then
                                lParts.Add(nodePart.Text)
                            End If
                        Next

                        If lParts.Count > 0 Then
                            dicPartsToProccess.Item(nodePartition.Text) = lParts
                        End If

                    Next

                Case "Parts Missing Symbols"

                    For Each nodePartition As TreeNode In nodeParent.Nodes

                        Dim lParts As List(Of String)

                        If dicPartsToProccess.ContainsKey(nodePartition.Text) Then
                            lParts = dicPartsToProccess.Item(nodePartition.Text)
                        Else
                            lParts = New List(Of String)
                        End If

                        For Each nodePart As TreeNode In nodePartition.Nodes
                            If nodePart.Checked = True Then
                                lParts.Add(nodePart.Text)
                            End If
                        Next

                        If lParts.Count > 0 Then
                            dicPartsToProccess.Item(nodePartition.Text) = lParts
                        End If

                    Next

            End Select

        Next

        ts_Status.Text = "Removing selected items..."
        WaitGif.Enabled = True
        gbInfo.Enabled = False

        AddHandler ePurgeFinished, AddressOf DeleteComplete
        Dim th_ProcessThread As Thread = New Thread(AddressOf DeleteSelectedItems)
        th_ProcessThread.IsBackground = True
        th_ProcessThread.Start()

    End Sub

    Private Sub frmIntegrityCheck_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        dicOrphanPadstacks = frmMain.librarydata.PadstacksByType

    End Sub

    Private Sub ReportComplete()

        If Me.InvokeRequired Then

            Dim d As New d_ReportFinished(AddressOf ReportComplete)
            Me.Invoke(d)
        Else

            If Not Directory.Exists(frmMain.librarydata.LogPath & "\Library Integrity Check\") Then
                Directory.CreateDirectory(frmMain.librarydata.LogPath & "\Library Integrity Check\")
            End If

            Dim sb_Log As StringBuilder

            If dicOrphanParts.Count > 0 Or dicPartsMissingCells.Count > 0 Or dicPartsMissingSymbols.Count > 0 Or dicOrphanSymbols.Count > 0 Or dicOrphanCells.Count > 0 Or dicOrphanPadstacks.Count > 0 Then
                sb_Log = New StringBuilder
                sb_Log.AppendLine("Date: " & Date.Now.ToString)
                sb_Log.AppendLine()
                btnPurgeAllItems.Enabled = True
            End If

            If dicOrphanParts.Count > 0 Then
                Dim nodeParts As TreeNode = tv_Problems.Nodes.Add("Orphan Parts")
                sb_Log.AppendLine("Orphan Parts:")
                nodeParts.Checked = True
                For Each kvp As KeyValuePair(Of String, List(Of String)) In dicOrphanParts
                    Dim nodePartition As TreeNode = nodeParts.Nodes.Add(kvp.Key)
                    sb_Log.AppendLine(vbTab & kvp.Key & ":")
                    nodePartition.Checked = True
                    For Each sPart As String In kvp.Value
                        sb_Log.AppendLine(vbTab & vbTab & sPart)
                        Dim nodePart As TreeNode = nodePartition.Nodes.Add(sPart)
                        nodePart.Checked = True
                    Next

                Next

                sb_Log.AppendLine()

            End If

            If dicPartsMissingCells.Count > 0 Then
                Dim nodeParts As TreeNode = tv_Problems.Nodes.Add("Parts Missing Cells")
                nodeParts.Checked = True
                sb_Log.AppendLine("Parts Missing Cells:")
                For Each kvp As KeyValuePair(Of String, List(Of String)) In dicPartsMissingCells
                    sb_Log.AppendLine(vbTab & kvp.Key & ":")
                    Dim nodePartition As TreeNode = nodeParts.Nodes.Add(kvp.Key)
                    nodePartition.Checked = True
                    For Each sPart As String In kvp.Value
                        sb_Log.AppendLine(vbTab & vbTab & sPart)
                        Dim nodePart As TreeNode = nodePartition.Nodes.Add(sPart)
                        nodePart.Checked = True

                    Next

                Next
                sb_Log.AppendLine()
            End If

            If dicPartsMissingSymbols.Count > 0 Then
                Dim nodeParts As TreeNode = tv_Problems.Nodes.Add("Parts Missing Symbols")
                sb_Log.AppendLine("Parts Missing Symbols:")
                nodeParts.Checked = True
                For Each kvp As KeyValuePair(Of String, List(Of String)) In dicPartsMissingSymbols
                    sb_Log.AppendLine(vbTab & kvp.Key & ":")
                    Dim nodePartition As TreeNode = nodeParts.Nodes.Add(kvp.Key)
                    nodePartition.Checked = True
                    For Each sPart As String In kvp.Value
                        sb_Log.AppendLine(vbTab & vbTab & sPart)
                        Dim nodePart As TreeNode = nodePartition.Nodes.Add(sPart)
                        nodePart.Checked = True
                    Next

                Next
                sb_Log.AppendLine()
            End If

            If dicOrphanSymbols.Count > 0 Then
                nodeSymbols = tv_Problems.Nodes.Add("Orphan Symbols")
                nodeSymbols.Name = "Symbols"
                sb_Log.AppendLine("Orphan Symbols:")
                If frmMain.librarydata.Type = Data.LibType.DX Then
                    nodeSymbols.Checked = True
                End If

                For Each kvp As KeyValuePair(Of String, List(Of String)) In dicOrphanSymbols
                    sb_Log.AppendLine(vbTab & kvp.Key & ":")
                    Dim nodePartition As TreeNode = Nothing
                    nodePartition = nodeSymbols.Nodes.Add(kvp.Key)
                    If frmMain.librarydata.Type = Data.LibType.DX Then

                        nodePartition.Checked = True
                    End If

                    For Each sPart As String In kvp.Value
                        sb_Log.AppendLine(vbTab & vbTab & sPart)
                        Dim nodePart As TreeNode = nodePartition.Nodes.Add(sPart)
                        If frmMain.librarydata.Type = Data.LibType.DX Then

                            nodePart.Checked = True
                        End If

                    Next

                Next
                sb_Log.AppendLine()
            End If

            If dicOrphanCells.Count > 0 Then
                Dim nodeCells As TreeNode = tv_Problems.Nodes.Add("Orphan Cells")
                sb_Log.AppendLine("Orphan Cells:")
                nodeCells.Checked = True
                For Each kvp As KeyValuePair(Of String, List(Of String)) In dicOrphanCells
                    sb_Log.AppendLine(vbTab & kvp.Key & ":")
                    Dim nodePartition As TreeNode = nodeCells.Nodes.Add(kvp.Key)
                    nodePartition.Checked = True
                    For Each sPart As String In kvp.Value
                        sb_Log.AppendLine(vbTab & vbTab & sPart)
                        Dim nodePart As TreeNode = nodePartition.Nodes.Add(sPart)
                        nodePart.Checked = True
                    Next

                Next
                sb_Log.AppendLine()
            End If

            If dicOrphanPadstacks.Count > 0 Then
                Dim nodeCells As TreeNode = tv_Problems.Nodes.Add("Orphan Padstacks")
                sb_Log.AppendLine("Orphan Padstacks:")
                nodeCells.Checked = True
                For Each kvp As KeyValuePair(Of String, List(Of String)) In dicOrphanPadstacks
                    If kvp.Value.Count > 0 Then
                        sb_Log.AppendLine(vbTab & kvp.Key & ":")
                        Dim nodeType As TreeNode = nodeCells.Nodes.Add(kvp.Key)
                        nodeType.Checked = True
                        For Each sPadstack As String In kvp.Value
                            sb_Log.AppendLine(vbTab & vbTab & vbTab & sPadstack)
                            Dim nodePadstack As TreeNode = nodeType.Nodes.Add(sPadstack)
                            nodePadstack.Checked = True
                            lblUnusedPadstacks.Text += 1
                        Next
                    End If
                Next
                sb_Log.AppendLine()
            End If

            If Not IsNothing(sb_Log) Then
                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\Library Integrity.log", True, System.Text.Encoding.ASCII)
                    writer.WriteLine(sb_Log.ToString())
                End Using

                Dim sBackupDirectory As String = frmMain.librarydata.LogPath & "\Library Integrity Check\" & DateTime.Now.ToString("MMddyyyyHHmmss")

                If Not Directory.Exists(sBackupDirectory) Then
                    Directory.CreateDirectory(sBackupDirectory)
                End If

                File.Copy(frmMain.librarydata.LogPath & "\Library Integrity.log", sBackupDirectory & "\Library Integrity.log")

            End If

            If l_MissingPadstacks.Count > 0 Then
                For Each sPadstack As String In l_MissingPadstacks
                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\Missing Padstacks.log", True, System.Text.Encoding.ASCII)
                        writer.WriteLine(sPadstack)
                    End Using
                Next
            End If

            Panel2.Enabled = True

            tv_Problems.Sort()
            AddHandler tv_Problems.BeforeCheck, AddressOf tv_Problems_BeforeCheck
            ts_Status.Text = "Process complete."
            WaitGif.Enabled = False
            gbInfo.Enabled = True
            btnPurgeAllItems.Enabled = True

            frmMain.NotifyIcon.ShowBalloonTip(2000, "Process Complete:", "Scan Central Library", ToolTipIcon.Info)

            If MessageBox.Show("Library Integrity process has completed. Would you like to view the results?", "Finished",
              MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                frmMain.OpenLogFile("Library Integrity")
            Else
                MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\Library Integrity.log")
            End If

            If l_MissingPadstacks.Count > 0 Then
                If MessageBox.Show("Some padstacks were found on cells that do not exist in the central library. Would you like to view the results?", "Missing Padstacks",
       MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                    frmMain.OpenLogFile("Missing Padstacks")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\Missing Padstacks.log")
                End If
            End If

        End If
    End Sub

    Private Sub CellPartitionComplete(PartitionName As String, Orphans As List(Of String), UsedPadsatcks As Dictionary(Of String, List(Of String)))

        SyncLock oCellLock
            If Orphans.Count > 0 Then
                dicOrphanCells.Add(PartitionName, Orphans)
            End If

            For Each kvp As KeyValuePair(Of String, List(Of String)) In UsedPadsatcks

                Dim padstackType As String

                If String.IsNullOrEmpty(kvp.Key) Or kvp.Key = "None" Then

                    For Each sPadstack As String In kvp.Value

                        If Not frmMain.librarydata.PadstackList.ContainsKey(sPadstack) Then
                            If Not l_MissingPadstacks.Contains(sPadstack) Then
                                l_MissingPadstacks.Add(sPadstack)
                            End If

                            Continue For
                        End If

                        padstackType = frmMain.librarydata.PadstackList.Item(sPadstack)

                        Dim l_Padstacks As List(Of String) = dicOrphanPadstacks.Item(padstackType)

                        l_Padstacks.Remove(sPadstack)

                        dicOrphanPadstacks.Item(padstackType) = l_Padstacks
                    Next

                Else
                    padstackType = kvp.Key

                    Dim l_Padstacks As List(Of String) = dicOrphanPadstacks.Item(padstackType)

                    For Each sPadstack As String In kvp.Value
                        If Not frmMain.librarydata.PadstackList.ContainsKey(sPadstack) Then
                            If Not l_MissingPadstacks.Contains(sPadstack) Then
                                l_MissingPadstacks.Add(sPadstack)
                            End If
                            Continue For
                        End If
                        l_Padstacks.Remove(sPadstack)
                    Next

                    dicOrphanPadstacks.Item(padstackType) = l_Padstacks

                End If

            Next
        End SyncLock

    End Sub

    Private Sub IncrementOrphanPadstack()
        If Me.InvokeRequired Then

            Dim d As New d_IncrementPadstacks(AddressOf IncrementOrphanPadstack)
            Me.Invoke(d)
        Else
            lblUnusedPadstacks.Text += 1
        End If
    End Sub

    Private Sub IncrementOrphanCell()
        If Me.InvokeRequired Then

            Dim d As New d_IncrementCell(AddressOf IncrementOrphanCell)
            Me.Invoke(d)
        Else
            lblUnusedCells.Text += 1
        End If
    End Sub

    Private Sub IncrementOrphanSymbol()
        If Me.InvokeRequired Then

            Dim d As New d_IncrementSymbol(AddressOf IncrementOrphanSymbol)
            Me.Invoke(d)
        Else
            lblUnusedSymbols.Text += 1
        End If
    End Sub

    Private Sub SymbolPartitionComplete(PartitionName As String, Orphans As List(Of String))

        If Orphans.Count > 0 Then
            dicOrphanSymbols.Add(PartitionName, Orphans)
        End If

    End Sub

    Private Sub IncrementOrphanPart()
        If Me.InvokeRequired Then

            Dim d As New d_IncrementPart(AddressOf IncrementOrphanPart)
            Me.Invoke(d)
        Else
            lblPartsNoSymOrCell.Text += 1
        End If
    End Sub

    Private Sub IncrementPartMissingSmbol()
        If Me.InvokeRequired Then

            Dim d As New d_IncrementPartMissingSymbol(AddressOf IncrementPartMissingSmbol)
            Me.Invoke(d)
        Else
            lblPartsNoSymbols.Text += 1
        End If
    End Sub

    Private Sub PartPartitionComplete(PartitionName As String, Orphans As List(Of String), MissingSymbols As List(Of String), MissingCells As List(Of String))

        If Orphans.Count > 0 Then
            dicOrphanParts.Add(PartitionName, Orphans)
        End If

        If MissingCells.Count > 0 Then
            dicPartsMissingCells.Add(PartitionName, MissingCells)
        End If

        If MissingSymbols.Count > 0 Then
            dicPartsMissingSymbols.Add(PartitionName, MissingSymbols)
        End If
    End Sub

    Private Sub IncrementPartMissingCell()
        If Me.InvokeRequired Then

            Dim d As New d_IncrementPartMissingCell(AddressOf IncrementPartMissingCell)
            Me.Invoke(d)
        Else
            lblPartsNoCells.Text += 1
        End If
    End Sub

    Private Sub btn_Select_Click(sender As System.Object, e As System.EventArgs) Handles btn_Select.Click
        RemoveHandler tv_Problems.BeforeCheck, AddressOf tv_Problems_BeforeCheck

        gbInfo.Enabled = False

        'lblPartsNoCells.Text = 0
        'lblPartsNoSymbols.Text = 0
        'lblPartsNoSymOrCell.Text = 0
        'lblUnusedCells.Text = 0
        'lblUnusedPadstacks.Text = 0
        'lblUnusedSymbols.Text = 0

        AddHandler eUpdateNodesFinished, AddressOf CheckNodeComplete
        AddHandler eCheckNode, AddressOf CheckNode
        Dim th_UpdateCount As Thread = New Thread(AddressOf CheckAllNodes)
        th_UpdateCount.IsBackground = True
        th_UpdateCount.Start(True)
    End Sub

    Private Sub CheckNodeComplete()
        If Me.InvokeRequired Then

            Dim d As New d_CheckFinished(AddressOf CheckNodeComplete)
            Me.Invoke(d)
        Else
            AddHandler tv_Problems.BeforeCheck, AddressOf tv_Problems_BeforeCheck
            RemoveHandler eUpdateNodesFinished, AddressOf CheckNodeComplete
            RemoveHandler eCheckNode, AddressOf CheckNode

            gbInfo.Enabled = True

        End If
    End Sub

    Private Sub CheckAllNodes(ByVal Checked As Boolean)

        Dim i As Integer = 0

        Dim oNodeCount(tv_Problems.Nodes.Count - 1) As NodeCount

        For Each oNode As TreeNode In tv_Problems.Nodes

            RaiseEvent eCheckNode(oNode, oNode.Checked, Checked)

            For Each oSubNode As TreeNode In oNode.Nodes

                RaiseEvent eCheckNode(oSubNode, oSubNode.Checked, Checked)

                oNodeCount(i) = New NodeCount
                AddHandler oNodeCount(i).CheckNode, AddressOf CheckNode
                oNodeCount(i).NewCheckedState = Checked
                oNodeCount(i).CurrentCheckedState = oSubNode.Checked
                oNodeCount(i).Update(oSubNode)
                RemoveHandler oNodeCount(i).CheckNode, AddressOf CheckNode
            Next

            i += 1

        Next

        RaiseEvent eUpdateNodesFinished()
    End Sub

    Private Sub CheckNode(ByVal node As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)
        If Me.InvokeRequired Then
            Dim d As New d_CheckNode(AddressOf CheckNode)
            Me.Invoke(d, New Object() {node, CurrentCheckedState, NewCheckedState})
        Else

            node.Checked = NewCheckedState
        End If

    End Sub

    Private Sub tv_Problems_BeforeCheck(sender As System.Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tv_Problems.BeforeCheck
        gbInfo.Enabled = False
        Dim oCheckNodes As New NodeCount
        oCheckNodes.NewCheckedState = Not e.Node.Checked
        oCheckNodes.CurrentCheckedState = e.Node.Checked

        RemoveHandler tv_Problems.BeforeCheck, AddressOf tv_Problems_BeforeCheck
        AddHandler oCheckNodes.eUpdateNodesFinished, AddressOf CheckNodeComplete
        AddHandler oCheckNodes.CheckNode, AddressOf CheckNode
        Dim th_CheckNodes As Thread = New Thread(AddressOf oCheckNodes.Update)
        th_CheckNodes.IsBackground = True
        th_CheckNodes.Start(e.Node)
    End Sub

    Private Sub btn_Deselect_Click(sender As System.Object, e As System.EventArgs) Handles btn_Deselect.Click
        RemoveHandler tv_Problems.BeforeCheck, AddressOf tv_Problems_BeforeCheck

        gbInfo.Enabled = False

        AddHandler eUpdateNodesFinished, AddressOf CheckNodeComplete
        AddHandler eCheckNode, AddressOf CheckNode
        Dim th_UpdateCount As Thread = New Thread(AddressOf CheckAllNodes)
        th_UpdateCount.IsBackground = True
        th_UpdateCount.Start(False)
    End Sub

    Private Sub DeleteSelectedItems()

        Dim thPadstacks As Thread
        Dim thParts As Thread
        Dim thCells As Thread
        Dim thSymbols As Thread

        If dicCellsToProccess.Count > 0 Then
            Dim oDeleteCells As Library_Integrity = New Library_Integrity()
            oDeleteCells.dic_CellsToDelete = dicCellsToProccess
            oDeleteCells.libDoc = frmMain.libDoc
            thCells = New Thread(AddressOf oDeleteCells.DeleteUnusedCells)
            thCells.IsBackground = True
            thCells.Start()
        End If

        If dicPadstacksToProcess.Count > 0 Then
            Dim oDeletePadstacks As Library_Integrity = New Library_Integrity()
            oDeletePadstacks.dic_PadstacksToDelete = dicPadstacksToProcess
            oDeletePadstacks.libDoc = frmMain.libDoc
            thPadstacks = New Thread(AddressOf oDeletePadstacks.DeleteUnsedPadstacks)
            thPadstacks.IsBackground = True
            thPadstacks.Start()
        End If

        If dicPartsToProccess.Count > 0 Then
            Dim oDeleteParts As Library_Integrity = New Library_Integrity()
            oDeleteParts.dic_PartsToDelete = dicPartsToProccess
            oDeleteParts.libDoc = frmMain.libDoc
            thParts = New Thread(AddressOf oDeleteParts.DeleteUnsedParts)
            thParts.IsBackground = True
            thParts.Start()
        End If

        If dicSymbolsToProcess.Count > 0 Then

            Dim oDeleteParts As Library_Integrity = New Library_Integrity()
            oDeleteParts.dic_SymbolsToDelete = dicSymbolsToProcess
            oDeleteParts.libDoc = frmMain.libDoc
            thSymbols = New Thread(AddressOf oDeleteParts.DeleteUnusedSymbols)
            thSymbols.IsBackground = True
            thSymbols.Start()

            thSymbols.Join()

            Dim Now As DateTime = DateTime.Now

            Dim sNow As String = Now.ToLongTimeString

            sNow = sNow.Replace(" ", "_")
            sNow = sNow.Replace(":", "_")

            Dim sPathBefore As String = frmMain.libDoc.Path & "SysIndex.cbf"
            Dim sPathAfter As String = frmMain.libDoc.Path & "SysIndex" & ".cbf_" & sNow

            If File.Exists(frmMain.libDoc.Path & "SysIndex.cbf") Then
                Microsoft.VisualBasic.Rename(sPathBefore, sPathAfter)
            End If

        End If

        If Not IsNothing(thCells) Then
            thCells.Join()
        End If

        If Not IsNothing(thPadstacks) Then
            thPadstacks.Join()
        End If

        If Not IsNothing(thParts) Then
            thParts.Join()
        End If

        RaiseEvent ePurgeFinished()

    End Sub

    Private Sub DeleteComplete()

        If Me.InvokeRequired Then
            Dim d As New d_PurgeFinished(AddressOf DeleteComplete)
            Me.Invoke(d)
        Else

            frmMain.NotifyIcon.ShowBalloonTip(2000, "Process Complete:", "Library Integrity Check", ToolTipIcon.Info)

            frmMain.tsm_OpenExplorer.Enabled = False
            frmMain.tsm_Cell.Enabled = False
            frmMain.tsm_Other.Enabled = False
            frmMain.tsm_Symbol.Enabled = False
            frmMain.tsm_PDB.Enabled = False
            frmMain.tsm_LibraryIntegrity.Enabled = False

            If Not File.Exists(frmMain.libDoc.Path & "\SysIndex.cbf") Then
                If MessageBox.Show("ALE and Library Manager needs to restart. Please reopen ALE.", "Restart ALE", MessageBoxButtons.OK, _
                            Nothing, MessageBoxDefaultButton.Button1) = DialogResult.OK Then

                    frmMain.libDoc.Close()
                    frmMain.libDoc = Nothing
                    frmMain.libApp.Quit()
                    frmMain.libApp = Nothing

                End If
            End If

            ts_Status.Text = "Finished removing objects."
            WaitGif.Enabled = False
            gbInfo.Enabled = False

        End If

    End Sub

#Region "HideNodeCheckbox"
    Private Const TVIF_STATE As Integer = &H8
    Private Const TVIS_STATEIMAGEMASK As Integer = &HF000
    Private Const TV_FIRST As Integer = &H1100
    Private Const TVM_SETITEM As Integer = TV_FIRST + 63

    <StructLayout(LayoutKind.Sequential)>
    Public Structure TVITEM
        Public mask As Integer
        Friend hItem As IntPtr
        Public state As Integer
        Public stateMask As Integer
        <MarshalAs(UnmanagedType.LPTStr)> Public lpszText As String
        Public cchTextMax As Integer
        Public iImage As Integer
        Public iSelectedImage As Integer
        Public cChildren As Integer
        Friend lParam As IntPtr
    End Structure

    Private Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByRef lParam As TVITEM) As Integer

    Private Sub HideRootCheckBox(ByVal node As TreeNode)
        Dim tvi As New TVITEM
        tvi.hItem = node.Handle
        tvi.mask = TVIF_STATE
        tvi.stateMask = TVIS_STATEIMAGEMASK
        tvi.state = 0
        SendMessage(node.TreeView.Handle, TVM_SETITEM, IntPtr.Zero, tvi)
    End Sub

    Private Sub tv_MouseDown(sender As Object, e As MouseEventArgs) Handles tv_Problems.MouseDown

        If sender.SelectedNode Is Nothing Then Exit Sub
        sender.SelectedNode = Nothing

    End Sub

    Private Sub tv_DrawNode(sender As Object, e As DrawTreeNodeEventArgs) Handles tv_Problems.DrawNode

        If Not IsNothing(nodeSymbols) Then
            If e.Node.Name = "Symbols" Then
                HideRootCheckBox(e.Node)
            ElseIf e.Node.Level = 1 Then
                If e.Node.Parent.Name = "Symbols" Then
                    HideRootCheckBox(e.Node)
                End If
            ElseIf e.Node.Level = 2 Then
                If e.Node.Parent.Parent.Name = "Symbols" Then
                    HideRootCheckBox(e.Node)
                End If
            End If
        End If

        e.DrawDefault = True
    End Sub

#End Region

End Class