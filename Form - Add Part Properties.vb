Imports System.Threading
Imports System.Drawing

Public Class frmAddPartProperty

#Region "Private Fields"

    'dictionary
    Dim dicPartProperties As Dictionary(Of String, List(Of AAL.Part))

    'Strings
    Dim sProperty As String

    Dim sValue As String

#End Region

#Region "Public Delegates"

    Delegate Sub d_CheckNode(ByVal value As TreeNode, ByVal checkstate As Boolean)

    Delegate Sub d_CheckNodesComplete()

    Delegate Sub d_UpdateFinished()

    Delegate Sub d_UpdateStatus(ByVal status As String)

    Delegate Sub dReadComplete()

    'Delegates
    Delegate Sub ScanComplete()

#End Region

#Region "Public Events"

    Event eCheckNode(ByVal node As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)

    Event eReadComplete()

    Event eUpdateFailed()

    Event eUpdateNodesFinished()

    'Events
    Event eUpdatePartsComplete()

    Event eUpdateStatus(status As String)

#End Region

#Region "Private Methods"

    Private Sub AddProperties(ByVal dicParts As Dictionary(Of String, List(Of String)))

        RaiseEvent eUpdateStatus("Creating instance of PDB editor...")

        'MentorGraphics
        Dim pedApp As New MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        Try
            pedDoc = pedApp.OpenDatabaseEx(frmMain.librarydata.LibPath, False)
        Catch ex As Exception
            RaiseEvent eUpdateFailed()
            pedApp.Quit()
            pedApp = Nothing
            Exit Sub
        End Try

        For Each kvp_Partition As KeyValuePair(Of String, List(Of String)) In dicParts

            Dim pdbPartition As MGCPCBPartsEditor.Partition = pedDoc.Partitions(kvp_Partition.Key).Item(1)

            For Each sPart As String In kvp_Partition.Value

                RaiseEvent eUpdateStatus("Adding property to: " & pdbPartition.Name & ":" & sPart)

                Dim pdbPart As MGCPCBPartsEditor.Part = pdbPartition.Parts.Item(sPart)

                pdbPart.PutPropertyEx(sProperty, sValue)

                pedApp.SaveActiveDatabase()

            Next

            pdbPartition = Nothing

            pedApp.SaveActiveDatabase()
        Next

        Try

            pedApp.SaveActiveDatabase()
            pedApp.Quit()
        Catch ex As Exception

        End Try

        RaiseEvent eUpdatePartsComplete()
    End Sub

    Private Sub btn_ReportCurrentProperties_Click(sender As System.Object, e As System.EventArgs) Handles btn_ReportCurrentProperties.Click

        dicPartProperties = New Dictionary(Of String, List(Of AAL.Part))

        tv_Parts.Enabled = False

        WaitGif.Enabled = True

        ts_Status.Text = "Reading part properties..."

        Dim th_ReadProperties As Thread = New Thread(AddressOf ReadProperties)
        th_ReadProperties.IsBackground = True
        th_ReadProperties.Start()

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles btn_Add.Click

        WaitGif.Enabled = True

        tv_Parts.Enabled = False

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
            Exit Sub

        End If

        sProperty = cbox_PartProperties.Text
        sValue = tbValue.Text

        ts_Status.Text = "Adding properties..."
        frmMain.NotifyIcon.ShowBalloonTip(2000, "Add Part Property:", "Adding properties.", ToolTipIcon.Info)

        Dim th_UpdateCount As Thread = New Thread(AddressOf AddProperties)
        th_UpdateCount.IsBackground = True
        th_UpdateCount.Start(dicPartsToProcess)

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
            tv_Parts.Enabled = True
        End If

    End Sub

    Private Sub frmAddPartProperty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eReadComplete, AddressOf ReadComplete
        AddHandler eUpdatePartsComplete, AddressOf UpdateFinished

        For Each kvp As KeyValuePair(Of String, List(Of String)) In frmMain.librarydata.PartsByPartition

            Dim nodeParent As TreeNode = tv_Parts.Nodes.Add(kvp.Key)

            For Each sPart As String In kvp.Value
                nodeParent.Nodes.Add(sPart)
            Next

        Next

        cbox_PartProperties.DataSource = frmMain.librarydata.PDBCommonProperties

        tv_Parts.Sort()

    End Sub

    Private Sub ReadComplete()

        If Me.InvokeRequired Then

            Dim d As New dReadComplete(AddressOf ReadComplete)
            Me.Invoke(d)
        Else

            tv_Parts.Nodes.Clear()

            For Each kvp_Partition As KeyValuePair(Of String, List(Of AAL.Part)) In dicPartProperties

                Dim oPartitionNode As TreeNode = tv_Parts.Nodes.Add(kvp_Partition.Key)

                For Each oPart As AAL.Part In kvp_Partition.Value

                    Dim oPartNode As TreeNode = oPartitionNode.Nodes.Add(oPart.Number)

                    For Each kvp_Property As KeyValuePair(Of String, String) In oPart.Properties

                        oPartNode.Nodes.Add(kvp_Property.Key & ": " & kvp_Property.Value)

                    Next

                    tv_Parts.Refresh()

                Next

            Next

            tv_Parts.Sort()

            tv_Parts.Enabled = True

            WaitGif.Enabled = False
            ts_Status.Text = "Read complete."

        End If

    End Sub

    Private Sub ReadProperties()
        Dim pedApp As New MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        Try
            pedDoc = pedApp.OpenDatabaseEx(frmMain.librarydata.LibPath, True)
        Catch ex As Exception
            RaiseEvent eUpdateFailed()
            pedApp.Quit()
            pedApp = Nothing
            Exit Sub
        End Try

        For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions
            RaiseEvent eUpdateStatus("Opening partition: " & pdbPartition.Name)
            Dim l_Parts As New List(Of AAL.Part)

            For Each pdbPart As MGCPCBPartsEditor.Part In pdbPartition.Parts

                RaiseEvent eUpdateStatus("Analyzing: " & pdbPartition.Name & ":" & pdbPart.Number)

                Dim oPart As New AAL.Part

                oPart.Number = pdbPart.Number

                For Each pdbProperty As MGCPCBPartsEditor.Property In pdbPart.Properties

                    RaiseEvent eUpdateStatus("Adding property: " & pdbPartition.Name & ":" & pdbPart.Number & " - " & pdbProperty.Name)
                    oPart.Properties.Add(pdbProperty.Name, pdbProperty.Value.ToString())

                Next

                l_Parts.Add(oPart)

            Next

            dicPartProperties.Item(pdbPartition.Name) = l_Parts

        Next

        RaiseEvent eUpdateStatus("Finished")
        pedApp.Quit()

        RaiseEvent eReadComplete()

    End Sub

    Private Sub tbValue_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbValue.TextChanged

        If String.IsNullOrEmpty(tbValue.Text) Then
            btn_Add.Enabled = False
        Else
            btn_Add.Enabled = True
        End If

    End Sub

    Private Sub tv_Parts_BeforeCheck(sender As System.Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tv_Parts.BeforeCheck

        tv_Parts.Enabled = False
        Dim oCheckNodes As New NodeCount

        RemoveHandler tv_Parts.BeforeCheck, AddressOf tv_Parts_BeforeCheck

        Dim oPartCount As New CheckNode
        AddHandler oPartCount.CheckNode, AddressOf CheckNode
        AddHandler oPartCount.CheckNodesComplete, AddressOf CheckNodesComplete
        Dim th_CheckNodes As Thread = New Thread(AddressOf oPartCount.Update)
        th_CheckNodes.IsBackground = True
        th_CheckNodes.Start(e.Node)

    End Sub

    Private Sub UpdateFinished()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateFinished(AddressOf UpdateFinished)
            Me.Invoke(d)
        Else
            ts_Status.Text = "Process complete."
            WaitGif.Enabled = False
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Add Part Properties:", "Process Complete.", ToolTipIcon.Info)
            tv_Parts.Enabled = True

        End If
    End Sub

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
            rtBox_Output.AppendText(status)
        End If
    End Sub

#End Region

End Class