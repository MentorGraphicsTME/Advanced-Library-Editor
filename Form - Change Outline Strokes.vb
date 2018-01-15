Imports System.Threading
Imports System.IO
Imports System.Text
Imports System.Drawing

Public Class frmChangeOutlineStrokes

    'MGCPCB Units
    Dim uOrigStroke As MGCPCB.EPcbUnit
    Dim uNewStroke As MGCPCB.EPcbUnit
    Dim usOrigStroke As String
    Dim usNewStroke As String

    'Double
    Dim dOrigStroke As Double
    Dim dNewStroke As Double

    'Decimal

    'Boolean
    Dim changePlacement As Boolean
    Dim changeAssembly As Boolean
    Dim changeSilkscreen As Boolean

    'Events
    Event eUpdateStatus(status As String)
    Event eUpdateComplete()

    'Delegates
    Delegate Sub d_CheckNodesComplete()
    Delegate Sub d_UpdateComplete()
    Delegate Sub d_UpdateStatus(ByVal status As String)
    Delegate Sub d_CheckNode(ByVal value As TreeNode, ByVal checkstate As Boolean)

    'dictionary
    Property dicErrors As New Dictionary(Of String, String)
    Dim dicPartitionToCell As New Dictionary(Of String, List(Of String))

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub ChangeOutlines(ByVal dicCellsToProcess As Dictionary(Of String, List(Of String)))
        ' Open the Cell Editor dialog and open the library database
        'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
        ' Variables to hold the application thingos
        Dim cellEd As CellEditorAddinLib.CellEditorDlg
        Dim cellDB As CellEditorAddinLib.CellDB

        RaiseEvent eUpdateStatus("Opening cell editor...")

        Dim sb_Log As New StringBuilder

        For Each kvp As KeyValuePair(Of String, List(Of String)) In dicCellsToProcess

            Try
                cellEd = frmMain.libDoc.CellEditor
                cellDB = cellEd.ActiveDatabase
                'cellDB = cellEd.OpenDatabase(frmMain.libDoc.FullName, False)

            Catch ex As Exception
                cellEd = Nothing
                cellEd.Quit()
                'cellEd.CloseActiveDatabase(False)
                MsgBox("Could not open cell database. Please check to see if any partitions are reserved.")
                End
            End Try

            Dim cePartition As CellEditorAddinLib.Partition = cellDB.Partitions.Item(kvp.Key)

            sb_Log.AppendLine(cePartition.Name & ":")

            For Each sCell As String In kvp.Value

                Dim ceCell As CellEditorAddinLib.Cell = cePartition.Cells.Item(sCell)

                If ceCell IsNot Nothing Then

                    RaiseEvent eUpdateStatus("Modifying " & cePartition.Name & ":" & sCell)

                    sb_Log.AppendLine(vbTab & ceCell.Name & ":")

                    Dim cellDoc As MGCPCB.Document
                    Dim bOpenedCellEditor As Boolean = False

                    Try
                        cellDoc = ceCell.Edit()     ' Edit invokes Cell Editor, returning an AutoActive document
                        bOpenedCellEditor = True
                    Catch ex As Exception
                        cellDoc = Nothing
                        dicErrors.Add(ceCell.Name, ex.Message)
                        bOpenedCellEditor = False
                    Finally

                        If bOpenedCellEditor = True Then

                            Dim oCellEditorApp As MGCPCB.IMGCPCBApplication
                            oCellEditorApp = Nothing
                            oCellEditorApp = cellDoc.Application
                            cellDoc.CurrentUnit = uNewStroke



                            If changePlacement = True Then
                                For Each placementOutline As MGCPCB.PlacementOutline In cellDoc.PlacementOutlines
                                    If chkbox_FindAndReplace.Checked = False Then
                                        placementOutline.Geometry.LineDisplayWidth = dNewStroke
                                    Else
                                        If placementOutline.Geometry.LineDisplayWidth(uOrigStroke) = dOrigStroke Then
                                            placementOutline.Geometry.LineDisplayWidth = dNewStroke
                                        End If
                                    End If
                                Next
                            End If

                            If changeAssembly = True Or changeSilkscreen = True Then
                                For Each fabGFX As MGCPCB.FabricationLayerGfx In cellDoc.FabricationLayerGfxs

                                    If (fabGFX.Type = MGCPCB.EPcbFabricationType.epcbFabAssembly And chkAssembly.Checked = True) Or (fabGFX.Type = MGCPCB.EPcbFabricationType.epcbFabSilkscreen And chkSilkscreen.Checked = True) Then
                                        If chkbox_FindAndReplace.Checked = False Then
                                            fabGFX.Geometry.LineDisplayWidth = dNewStroke
                                            fabGFX.Geometry.LineWidth = dNewStroke
                                        Else
                                            If fabGFX.Geometry.LineDisplayWidth(uOrigStroke) = dOrigStroke Then
                                                fabGFX.Geometry.LineDisplayWidth = dNewStroke
                                                fabGFX.Geometry.LineWidth = dNewStroke
                                            End If
                                        End If
                                    End If

                                Next
                            End If
                            Try
                                cellDoc.Save()
                                cellDoc.Close(True)
                                cellDoc = Nothing
                            Catch ex As Exception
                                cellDoc.Close(False)
                                cellDoc = Nothing
                            End Try
                        End If
                    End Try
                End If
            Next

            cellDB = Nothing
            cellEd.SaveActiveDatabase()
            cellEd.Quit()
            cellEd = Nothing
        Next

        RaiseEvent eUpdateComplete()


    End Sub

    Private Sub btnUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdate.Click


        If chkAssembly.Checked = False And chkPlacement.Checked = False And chkSilkscreen.Checked = False Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Nothing selected to be modifed...", ToolTipIcon.Error)
            ts_Status.Text = "Nothing selected to be modified..."
            Exit Sub
        End If

        Dim dicCellsToProcess As New Dictionary(Of String, List(Of String))

        For Each oNode As TreeNode In tv_Cells.Nodes
            Dim l_CellsToProcess As New List(Of String)
            If Not oNode.Checked = True Then

                For Each oChildNode As TreeNode In oNode.Nodes

                    If oChildNode.Checked = True Then
                        l_CellsToProcess.Add(oChildNode.Text)
                    End If

                Next

                If l_CellsToProcess.Count > 0 Then
                    dicCellsToProcess.Add(oNode.Text, l_CellsToProcess)
                End If

            Else
                dicCellsToProcess.Item(oNode.Text) = frmMain.librarydata.CellsByPartition.Item(oNode.Text)
            End If

        Next

        If (dicCellsToProcess.Count = 0) Then
            ts_Status.Text = "No cells were found to be processed..."
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "No cells selected to process...", ToolTipIcon.Error)
            tv_Cells.Enabled = True

            Exit Sub

        End If

        changePlacement = chkPlacement.Checked
        changeAssembly = chkAssembly.Checked
        changeSilkscreen = chkSilkscreen.Checked

        Dim noOrigStroke As Boolean = False

        If chkbox_FindAndReplace.Checked = True Then
            Try
                dOrigStroke = tbox_Original.Text
                usOrigStroke = ComboBox1.Text

                Select Case usOrigStroke
                    Case "IN"
                        uOrigStroke = MGCPCB.EPcbUnit.epcbUnitInch
                    Case "MM"
                        uOrigStroke = MGCPCB.EPcbUnit.epcbUnitMM
                    Case "TH"
                        uOrigStroke = MGCPCB.EPcbUnit.epcbUnitMils
                    Case "UM"
                        uOrigStroke = MGCPCB.EPcbUnit.epcbUnitUM
                End Select
            Catch ex As Exception
                noOrigStroke = True
                MessageBox.Show("Please enter a valid search value for the outline stroke width")
            End Try
        End If

        Dim noNewStroke As Boolean = False

        Try
            dNewStroke = tbox_New.Text
            usNewStroke = ComboBox2.Text

            Select Case usNewStroke
                Case "IN"
                    uNewStroke = MGCPCB.EPcbUnit.epcbUnitInch
                Case "MM"
                    uNewStroke = MGCPCB.EPcbUnit.epcbUnitMM
                Case "TH"
                    uNewStroke = MGCPCB.EPcbUnit.epcbUnitMils
                Case "UM"
                    uNewStroke = MGCPCB.EPcbUnit.epcbUnitUM
            End Select

        Catch ex As Exception
            noNewStroke = True
            MessageBox.Show("Please enter a valid new value for the outline stroke width.")
        End Try

        If Not (noNewStroke Or noOrigStroke) Then

            If File.Exists(frmMain.librarydata.LogPath & "Modify Cell Outlines.log") Then
                File.Delete(frmMain.librarydata.LogPath & "Modify Cell Outlines.log")
            End If

            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Processing.", ToolTipIcon.Info)
            ts_Status.Text = "Modifying cell attributes..."

            plMain.Enabled = False
            WaitGif.Enabled = True

            AddHandler eUpdateComplete, AddressOf UpdateComplete
            Dim th_UpdateCount As Thread = New Thread(AddressOf ChangeOutlines)
            th_UpdateCount.IsBackground = True
            th_UpdateCount.Start(dicCellsToProcess)

        End If


    End Sub

    Private Sub frmRenameUserLayers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        For Each kvp As KeyValuePair(Of String, List(Of String)) In frmMain.librarydata.CellsByPartition

            Dim nodeParent As TreeNode = tv_Cells.Nodes.Add(kvp.Key)

            For Each sPart As String In kvp.Value
                nodeParent.Nodes.Add(sPart)
            Next

        Next

        tv_Cells.Sort()

    End Sub

    Private Sub tv_Cells_BeforeCheck(sender As System.Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tv_Cells.BeforeCheck
        Me.Enabled = False

        RemoveHandler tv_Cells.BeforeCheck, AddressOf tv_Cells_BeforeCheck
        Dim oCheckNodes As New CheckNode
        AddHandler oCheckNodes.CheckNode, AddressOf CheckNode
        AddHandler oCheckNodes.CheckNodesComplete, AddressOf CheckNodesComplete
        Dim th_CheckNodes As Thread = New Thread(AddressOf oCheckNodes.Update)
        th_CheckNodes.IsBackground = True
        th_CheckNodes.Start(e.Node)

    End Sub

    Private Sub CheckNodesComplete()
        If Me.InvokeRequired Then
            Dim d As New d_CheckNodesComplete(AddressOf CheckNodesComplete)
            Me.Invoke(d)
        Else

            AddHandler tv_Cells.BeforeCheck, AddressOf tv_Cells_BeforeCheck
            Me.Enabled = True
        End If

    End Sub

    Private Sub CheckNode(ByVal node As TreeNode, ByVal checkstate As Boolean)
        If Me.InvokeRequired Then

            Dim d As New d_CheckNode(AddressOf CheckNode)
            Me.Invoke(d, New Object() {node, checkstate})
        Else
            node.Checked = checkstate
        End If

    End Sub

    Private Sub chkbox_ModifyAll_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_FindAndReplace.CheckedChanged

        tbox_Original.Clear()
        If chkbox_FindAndReplace.Checked = True Then
            ComboBox1.Enabled = True
            tbox_Original.Enabled = True
            Label2.Text = "With New Value:"
        Else
            tbox_Original.Enabled = False
            ComboBox1.Text = ""
            ComboBox1.Enabled = False
            Label2.Text = "New Value:"
        End If

    End Sub

    'Private Sub CheckNode(node As TreeNode, CurrentCheckedState As Boolean, NewCheckedState As Boolean)
    '    If Me.InvokeRequired Then

    '        Dim d As New d_CheckNode(AddressOf CheckNode)
    '        Me.Invoke(d, New Object() {node, CheckState})
    '    Else
    '        node.Checked = CheckState
    '    End If
    'End Sub

    Private Sub UpdateComplete()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateComplete(AddressOf UpdateComplete)
            Me.Invoke(d)
        Else
            plMain.Enabled = True
            WaitGif.Enabled = False
            RaiseEvent eUpdateStatus("Update Complete.")
        End If
    End Sub

End Class