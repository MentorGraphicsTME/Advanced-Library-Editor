Imports System.Threading
Imports System.IO
Imports System.Text
Imports System.Drawing

Public Class frmRemovePDBProperties

    'string
    Dim sb_Report As New StringBuilder

    'dictionary
    Dim dicPropertiesAndParts As Dictionary(Of String, Dictionary(Of String, List(Of String)))

    'List
    Dim lPropertiesToRemove As New List(Of String)

    'Events
    Event eUpdatePartsComplete()
    Event eUpdateNodesFinished()
    Event eUpdateStatus(status As String)
    Event eReadComplete()
    Event eUpdateFailed()
    Event eReturnParts(dicParts As Dictionary(Of String, List(Of String)))

    'Delegates
    Delegate Sub ScanComplete()
    Delegate Sub d_UpdateFinished()
    Delegate Sub d_CheckNode(ByVal value As TreeNode, ByVal checkstate As Boolean)
    Delegate Sub d_CheckNodesComplete()
    Delegate Sub dReadComplete()
    Delegate Sub dUpdateTree()
    Delegate Sub d_UpdateStatus(ByVal status As String)
    Delegate Sub dUpdateTreeParts(ByVal Parts As Dictionary(Of String, List(Of String)))

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub frmRemovePDBProperties_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eReadComplete, AddressOf ReadComplete
        AddHandler eUpdatePartsComplete, AddressOf UpdateFinished
        AddHandler eReturnParts, AddressOf UpdatePartsTree

        dicPropertiesAndParts = New Dictionary(Of String, Dictionary(Of String, List(Of String)))

        SplitContainer1.Panel2.Enabled = False

        WaitGif.Enabled = True

        ts_Status.Text = "Gathering part properties..."

        Dim th_ReadProperties As Thread = New Thread(AddressOf ReadProperties)
        th_ReadProperties.IsBackground = True
        th_ReadProperties.Start()

    End Sub

    Private Sub ReadComplete()

        If Me.InvokeRequired Then

            Dim d As New dReadComplete(AddressOf ReadComplete)
            Me.Invoke(d)
        Else

            tv_Properties.Nodes.Clear()

            For Each sProperty As String In dicPropertiesAndParts.Keys
                tv_Properties.Nodes.Add(sProperty)
            Next

            tv_Properties.Sort()

            tv_Properties.Enabled = True
            SplitContainer1.Panel2.Enabled = True

            WaitGif.Enabled = False
            ts_Status.Text = "Read complete."

        End If

    End Sub

    Private Sub ReadProperties()
        If Me.InvokeRequired Then

            Dim d As New dUpdateTree(AddressOf ReadProperties)
            Me.Invoke(d)
        Else
            Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg
            Dim pedDoc As MGCPCBPartsEditor.PartsDB

            pedApp = frmMain.libDoc.PartEditor
            pedDoc = pedApp.ActiveDatabaseEx

            dicPropertiesAndParts.Clear()
            tv_Parts.Nodes.Clear()

            For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions

                'Dim l_Parts As New List(Of AAL.Part)

                For Each pdbPart As MGCPCBPartsEditor.Part In pdbPartition.Parts

                    RaiseEvent eUpdateStatus("Analyzing: " & pdbPartition.Name & ":" & pdbPart.Number)

                    'Dim oPart As New AAL.Part

                    'oPart.Number = pdbPart.Number

                    For Each pdbProperty As MGCPCBPartsEditor.Property In pdbPart.Properties

                        Dim dicPartition As Dictionary(Of String, List(Of String))
                        Dim PartitionParts As List(Of String)

                        If dicPropertiesAndParts.ContainsKey(pdbProperty.Name) Then
                            dicPartition = dicPropertiesAndParts.Item(pdbProperty.Name)

                            If dicPartition.ContainsKey(pdbPartition.Name) Then
                                PartitionParts = dicPartition.Item(pdbPartition.Name)
                            Else
                                PartitionParts = New List(Of String)
                            End If
                        Else
                            dicPartition = New Dictionary(Of String, List(Of String))
                            PartitionParts = New List(Of String)
                        End If

                        If Not PartitionParts.Contains(pdbPart.Number) Then
                            PartitionParts.Add(pdbPart.Number)
                        End If

                        dicPartition.Item(pdbPartition.Name) = PartitionParts
                        dicPropertiesAndParts.Item(pdbProperty.Name) = dicPartition

                        'Try
                        '    Dim propvalue As String = pdbProperty.Value.ToString()
                        '    oPart.Properties.Add(pdbProperty.Name, propvalue)
                        'Catch ex As Exception
                        '    oPart.Properties.Add(pdbProperty.Name, String.Empty)
                        'End Try

                    Next

                    'l_Parts.Add(oPart)

                Next

            Next

            pedApp = Nothing
            pedDoc = Nothing

            RaiseEvent eReadComplete()
        End If

    End Sub

    Private Sub UpdateFinished()
        If Me.InvokeRequired Then

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Remove Part Property.log", True)
                writer.WriteLine(sb_Report.ToString())
            End Using

            Dim d As New d_UpdateFinished(AddressOf UpdateFinished)
            Me.Invoke(d)
        Else
            ts_Status.Text = "Process complete."
            WaitGif.Enabled = False
            btn_Modify.Enabled = True
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Remove Part Properties:", "Process Complete.", ToolTipIcon.Info)
            tv_Parts.Enabled = True
            SplitContainer1.Panel2.Enabled = True

            If sb_Report.Length > 0 Then

                Dim replyCell As DialogResult = MessageBox.Show("Remove part properties has finished. Would you like to view the results?", "Finished", _
                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

                If replyCell = DialogResult.Yes Then
                    frmMain.OpenLogFile("Remove Part Property")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Remove Part Property.log")
                End If
            End If

        End If
    End Sub

    Private Sub UpdatePartList()
        Dim dicParts As New Dictionary(Of String, List(Of String))

        For Each oNode As TreeNode In tv_Properties.Nodes

            If oNode.Checked = True Then

                Dim dicPDBParts As Dictionary(Of String, List(Of String))

                dicPDBParts = dicPropertiesAndParts.Item(oNode.Text)

                For Each kvp As KeyValuePair(Of String, List(Of String)) In dicPDBParts

                    Dim lParts As List(Of String)

                    If dicParts.ContainsKey(kvp.Key) Then
                        lParts = dicParts.Item(kvp.Key)
                    Else
                        lParts = New List(Of String)
                    End If

                    For Each sPart As String In kvp.Value
                        If Not lParts.Contains(sPart) Then lParts.Add(sPart)
                    Next

                    dicParts.Item(kvp.Key) = lParts

                Next

            End If

        Next

        RaiseEvent eReturnParts(dicParts)

    End Sub

    Private Sub UpdatePartsTree(dicParts As Dictionary(Of String, List(Of String)))

        If Me.InvokeRequired Then

            Dim d As New dUpdateTreeParts(AddressOf UpdatePartsTree)
            Me.Invoke(d, New Object() {dicParts})
        Else

            tv_Parts.Nodes.Clear()

            For Each kvp As KeyValuePair(Of String, List(Of String)) In dicParts

                Dim nodeParent As TreeNode = tv_Parts.Nodes.Add(kvp.Key)

                For Each sPart As String In kvp.Value
                    nodeParent.Nodes.Add(sPart)
                Next

            Next

            tv_Parts.Sort()

            SplitContainer1.Enabled = True
            WaitGif.Enabled = False
            ts_Status.Text = String.Empty

        End If

    End Sub

    Private Sub tv_Properties_BeforeCheck(sender As System.Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tv_Properties.BeforeCheck

        If e.Node.Checked = False Then
            lPropertiesToRemove.Add(e.Node.Text)
        Else
            lPropertiesToRemove.Remove(e.Node.Text)
        End If

        SplitContainer1.Enabled = False
        WaitGif.Enabled = True
        ts_Status.Text = "Updating part list..."

        Dim th_UpdateParts As Thread = New Thread(AddressOf UpdatePartList)
        th_UpdateParts.IsBackground = True
        th_UpdateParts.Start()

    End Sub

    Private Sub btn_Modify_Click(sender As System.Object, e As System.EventArgs) Handles btn_Modify.Click

        If lPropertiesToRemove.Count = 0 Then
            ts_Status.Text = "No properties were selected to be removed..."
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Remove Part Property:", "No properties were selected.", ToolTipIcon.Error)
            Exit Sub
        End If

        Dim dicPartsToProcess As New Dictionary(Of String, List(Of String))

        For Each oNode As TreeNode In tv_Parts.Nodes
            If oNode.Level = 0 Then

                Dim sPartition As String = oNode.Text

                Dim l_PartsToProcess As New List(Of String)

                For Each oChildNode As TreeNode In oNode.Nodes

                    If oChildNode.Checked = True Then
                        l_PartsToProcess.Add(oChildNode.Text)
                    End If

                Next

                If l_PartsToProcess.Count > 0 Then
                    dicPartsToProcess.Item(sPartition) = l_PartsToProcess
                End If

            End If

        Next

        If (dicPartsToProcess.Count = 0) Then
            MessageBox.Show("Please select some parts to modify before proceeding...")
            tv_Parts.Enabled = True
            SplitContainer1.Panel2.Enabled = True
            Exit Sub

        End If

        If File.Exists(frmMain.librarydata.LogPath & "\Remove Part Property.log") Then

            File.Delete(frmMain.librarydata.LogPath & "\Remove Part Property.log")

        End If

        ts_Status.Text = "Removing properties..."
        frmMain.NotifyIcon.ShowBalloonTip(2000, "Remove Part Property:", "Removing properties.", ToolTipIcon.Info)

        WaitGif.Enabled = True
        btn_Modify.Enabled = False
        tv_Parts.Enabled = False

        Dim th_UpdateParts As Thread = New Thread(AddressOf RemoveProperties)
        th_UpdateParts.IsBackground = True
        th_UpdateParts.Start(dicPartsToProcess)

    End Sub

    Private Sub RemoveProperties(dicPartsToProcess As Dictionary(Of String, List(Of String)))

        For Each kvp_Partition As KeyValuePair(Of String, List(Of String)) In dicPartsToProcess

            RaiseEvent eUpdateStatus("Creating instance of PDB editor...")

            'MentorGraphics
            Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg
            Dim pedDoc As MGCPCBPartsEditor.PartsDB

            pedApp = frmMain.libDoc.PartEditor
            pedDoc = pedApp.ActiveDatabaseEx

            Dim pdbPartition As MGCPCBPartsEditor.Partition = pedDoc.Partitions(kvp_Partition.Key).Item(1)

            sb_Report.AppendLine(pdbPartition.Name)

            For Each sPart As String In kvp_Partition.Value

                Dim bPrintPart As Boolean = True

                RaiseEvent eUpdateStatus("Modifying: " & pdbPartition.Name & ":" & sPart)

                Dim pdbPart As MGCPCBPartsEditor.Part = pdbPartition.Parts.Item(sPart)
                Dim bFoundProperty As Boolean = False

                For Each pdbProperty As MGCPCBPartsEditor.Property In pdbPart.Properties

                    If lPropertiesToRemove.Contains(pdbProperty.Name, StringComparer.OrdinalIgnoreCase) Then

                        If bPrintPart = True Then
                            sb_Report.AppendLine(vbTab & sPart)
                            bPrintPart = False
                        End If

                        Try
                            If String.IsNullOrEmpty(pdbProperty.Name) Then
                                sb_Report.AppendLine(vbTab & vbTab & pdbProperty.Name & ", Value: none")
                            Else
                                sb_Report.AppendLine(vbTab & vbTab & pdbProperty.Name & ", Value:" & pdbProperty.Value)
                            End If
                        Catch ex As Exception
                            sb_Report.AppendLine(vbTab & vbTab & pdbProperty.Name & ", Value was corrupted.")
                        End Try

                        pdbProperty.Delete()

                    End If

                Next

                pdbPart = Nothing

            Next

            pdbPartition = Nothing

            pedApp.SaveActiveDatabase()
            pedApp = Nothing
            pedDoc = Nothing

        Next

        ReadProperties()

        RaiseEvent eUpdatePartsComplete()

    End Sub

    Private Sub tv_Parts_BeforeCheck(sender As System.Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tv_Parts.BeforeCheck
        SplitContainer1.Enabled = False
        RemoveHandler tv_Parts.BeforeCheck, AddressOf tv_Parts_BeforeCheck

        Dim oPartCount As New CheckNode
        AddHandler oPartCount.CheckNode, AddressOf CheckNode
        AddHandler oPartCount.CheckNodesComplete, AddressOf CheckNodesComplete
        Dim th_CheckNodes As Thread = New Thread(AddressOf oPartCount.Update)
        th_CheckNodes.IsBackground = True
        th_CheckNodes.Start(e.Node)
    End Sub

    Private Sub CheckNode(ByVal node As TreeNode, ByVal checkstate As Boolean)
        If Me.InvokeRequired Then

            Dim d As New d_CheckNode(AddressOf CheckNode)
            Me.Invoke(d, New Object() {node, checkstate})
        Else
            node.Checked = checkstate
        End If

    End Sub

    Private Sub CheckNodesComplete()
        If Me.InvokeRequired Then
            Dim d As New d_CheckNodesComplete(AddressOf CheckNodesComplete)
            Me.Invoke(d)
        Else

            AddHandler tv_Parts.BeforeCheck, AddressOf tv_Parts_BeforeCheck
            SplitContainer1.Enabled = True
        End If

    End Sub

End Class