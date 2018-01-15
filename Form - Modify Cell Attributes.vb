Imports System.Threading
Imports System.Text
Imports System.IO
Imports System.Drawing

Public Class frmModifyCellAtts

    'Strings
    Dim sPackageGroup As String
    Dim sUnits As String
    Dim sDescriptionBefore As String
    Dim sDescriptionAfter As String
    Dim sClearanceTypeBefore As String
    Dim sClearanceTypeAfter As String
    Dim sHeight As String
    Dim sHeightUnit As String
    Dim sUndersideUnit As String
    Dim sUndersideHeight As String
    Dim sVerification As String

    'Boolean
    Dim bAllowCellOverhang As Boolean
    Dim bMoveablePins As Boolean

    'Events
    Event eUpdateCellsComplete()
    Event eCheckNode(ByVal node As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)
    Event eUpdateNodesFinished()
    Event eUpdateStatus(status As String)

    'Delegates
    Delegate Sub ScanComplete()
    Delegate Sub d_CheckNode(ByVal value As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)
    Delegate Sub d_UpdateFinished()
    Delegate Sub d_UpdateStatus(ByVal status As String)

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub UpdateFinished()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateFinished(AddressOf UpdateFinished)
            Me.Invoke(d)
        Else
            ts_Status.Text = "Process complete."
            WaitGif.Enabled = False
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Process Complete:", "Modify Cell Attributes.", ToolTipIcon.Info)

            Dim reply As DialogResult = MessageBox.Show("Cell attributes have been modified." & Environment.NewLine & Environment.NewLine & "Would you like to view the results?", "Complete", _
      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then
                frmMain.OpenLogFile("Modify Cell Attributes")
            Else

                MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Modify Cell Attributes.log")

            End If

            tv_Cells.Enabled = True
            SplitContainer1.Panel2.Enabled = True

        End If
    End Sub

    Private Sub frmModifyCellProp_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        AddHandler eUpdateStatus, AddressOf UpdateStatus

        For Each kvp As KeyValuePair(Of String, List(Of String)) In frmMain.librarydata.CellsByPartition

            Dim nodeParent As TreeNode = tv_Cells.Nodes.Add(kvp.Key)

            For Each sPart As String In kvp.Value
                nodeParent.Nodes.Add(sPart)
            Next

        Next

        AddHandler eUpdateCellsComplete, AddressOf UpdateFinished

        tv_Cells.Sort()

    End Sub

    Private Sub btnUpdate_Click(sender As System.Object, e As System.EventArgs) Handles btnUpdate.Click

        If chkbox_PG.Checked = True And cbox_PG.SelectedIndex = -1 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Select a new package group...", ToolTipIcon.Error)
            ts_Status.Text = "Select a new package group."
            cbox_PG.Select()
            Exit Sub
        End If

        If chkbox_Units.Checked = True And cbox_Units.SelectedIndex = -1 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Select a new unit...", ToolTipIcon.Error)
            ts_Status.Text = "Select a new unit."
            cbox_Units.Select()
            Exit Sub
        End If

        If chkbox_Description.Checked = True And tbox_Descript_After.TextLength = 0 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Provide a new description...", ToolTipIcon.Error)
            ts_Status.Text = "Provide a new description."
            tbox_Descript_After.Select()
            Exit Sub
        End If

        If chkbox_CT.Checked = True And tbox_CT_After.TextLength = 0 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Provide a new value for clearance type...", ToolTipIcon.Error)
            ts_Status.Text = "Provide a new value for clearance type."
            tbox_CT_After.Select()
            Exit Sub
        End If

        If chkbox_Height.Checked = True And tbox_Height.TextLength = 0 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Provide a new height...", ToolTipIcon.Error)
            ts_Status.Text = "Provide a new height."
            tbox_Height.Select()
            Exit Sub
        ElseIf chkbox_Height.Checked = True And IsNumeric(tbox_Height.Text) = False Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Provide a new numeric value for height...", ToolTipIcon.Error)
            ts_Status.Text = "Provide a new numeric value for height."
            tbox_Height.Select()
            Exit Sub
        End If

        If chkbox_Underside.Checked = True And tbox_Underside.TextLength = 0 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Provide a new underside height...", ToolTipIcon.Error)
            ts_Status.Text = "Provide a new underside height."
            tbox_Underside.Select()
            Exit Sub
        ElseIf chkbox_Underside.Checked = True And IsNumeric(tbox_Underside.Text) = False Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Provide a new numeric value for underside height...", ToolTipIcon.Error)
            ts_Status.Text = "Provide a new numeric value for underside height."
            tbox_Underside.Select()
            Exit Sub
        End If

        If chkbox_Verification.Checked = True And cbox_Verification.SelectedIndex = -1 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Provide value for cell verification status...", ToolTipIcon.Error)
            ts_Status.Text = "Provide value for cell verification status."
            cbox_Verification.Select()
            Exit Sub
        End If

        If chkbox_CellOverhang.Checked = True And cbox_CellOverhang.SelectedIndex = -1 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Provide a value for cell overhang...", ToolTipIcon.Error)
            ts_Status.Text = "Provide a value for cell overhang."
            cbox_CellOverhang.Select()
            Exit Sub
        End If

        If chkbox_Movable.Checked = True And cbox_MoveablePins.SelectedIndex = -1 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Provide a value for moveable pins...", ToolTipIcon.Error)
            ts_Status.Text = "Provide a value for moveable pins."
            cbox_MoveablePins.Select()
            Exit Sub
        End If

        WaitGif.Enabled = True

        tv_Cells.Enabled = False

        SplitContainer1.Panel2.Enabled = False

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
            MessageBox.Show("Please select some cells to modify before proceeding...")
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "No cells selected to process...", ToolTipIcon.Error)
            tv_Cells.Enabled = True

            SplitContainer1.Panel2.Enabled = True
            Exit Sub

        End If

        sPackageGroup = cbox_PG.Text
        sUnits = cbox_Units.Text
        sDescriptionBefore = tbox_Descript_Before.Text
        sDescriptionAfter = tbox_Descript_After.Text
        sClearanceTypeBefore = tbox_CT_Before.Text
        sClearanceTypeAfter = tbox_CT_After.Text
        sHeight = tbox_Height.Text
        sHeightUnit = cbox_HeightUnits.Text
        sUndersideUnit = cbox_UndersideHeightUnits.Text
        sUndersideHeight = tbox_Underside.Text
        sVerification = cbox_Verification.Text

        If Not String.IsNullOrEmpty(cbox_CellOverhang.Text) Then
            bAllowCellOverhang = cbox_CellOverhang.Text
        End If

        If Not String.IsNullOrEmpty(cbox_MoveablePins.Text) Then
            bMoveablePins = cbox_MoveablePins.Text
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Modify Cell Attributes.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Modify Cell Attributes.log")
        End If

        frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Cell Attributes", "Processing.", ToolTipIcon.Info)
        ts_Status.Text = "Modifying cell attributes..."

        Dim th_UpdateCount As Thread = New Thread(AddressOf ModifyProperties)
        th_UpdateCount.IsBackground = True
        th_UpdateCount.Start(dicCellsToProcess)

    End Sub

    Private Sub tv_Cells_BeforeCheck(sender As System.Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tv_Cells.BeforeCheck

        Me.Enabled = False
        Dim oCheckNodes As New NodeCount
        oCheckNodes.NewCheckedState = Not e.Node.Checked
        oCheckNodes.CurrentCheckedState = e.Node.Checked

        RemoveHandler tv_Cells.BeforeCheck, AddressOf tv_Cells_BeforeCheck
        AddHandler oCheckNodes.eUpdateNodesFinished, AddressOf CheckNodeComplete
        AddHandler oCheckNodes.CheckNode, AddressOf CheckNode
        Dim th_CheckNodes As Thread = New Thread(AddressOf oCheckNodes.Update)
        th_CheckNodes.IsBackground = True
        th_CheckNodes.Start(e.Node)

    End Sub

    Private Sub CheckNode(ByVal node As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)
        If Me.InvokeRequired Then
            Dim d As New d_CheckNode(AddressOf CheckNode)
            Me.Invoke(d, New Object() {node, CurrentCheckedState, NewCheckedState})
        Else
            node.Checked = NewCheckedState
        End If

    End Sub

    Private Sub CheckNodeComplete()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateFinished(AddressOf CheckNodeComplete)
            Me.Invoke(d)
        Else
            AddHandler tv_Cells.BeforeCheck, AddressOf tv_Cells_BeforeCheck
            RemoveHandler eUpdateNodesFinished, AddressOf CheckNodeComplete
            RemoveHandler eCheckNode, AddressOf CheckNode

            Me.Enabled = True

        End If
    End Sub

    Private Sub CheckAllNodes(ByVal Checked As Boolean)

        Dim i As Integer = 0

        Dim oNodeCount(tv_Cells.Nodes.Count - 1) As NodeCount

        For Each oNode As TreeNode In tv_Cells.Nodes

            If oNode.Nodes.Count > 0 Then

                RaiseEvent eCheckNode(oNode, oNode.Checked, Checked)

                oNodeCount(i) = New NodeCount
                AddHandler oNodeCount(i).CheckNode, AddressOf CheckNode
                oNodeCount(i).NewCheckedState = Checked
                oNodeCount(i).CurrentCheckedState = oNode.Checked
                oNodeCount(i).Update(oNode)

                RemoveHandler oNodeCount(i).CheckNode, AddressOf CheckNode

            End If

            i += 1

        Next

        RaiseEvent eUpdateNodesFinished()
    End Sub

    Private Sub ModifyProperties(ByVal dicCellsToProcess As Dictionary(Of String, List(Of String)))
        ' Variables to hold the application thingos
        Dim cellEd As CellEditorAddinLib.CellEditorDlg
        Dim cellDB As CellEditorAddinLib.CellDB

        RaiseEvent eUpdateStatus("Opening cell editor...")

        Try

            ' Open the Cell Editor dialog and open the library database
            'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
            cellEd = frmMain.libDoc.CellEditor
            cellDB = cellEd.ActiveDatabase
            'cellDB = cellEd.OpenDatabase(frmMain.libDoc.FullName, False)

        Catch ex As Exception
            'cellEd.CloseActiveDatabase(False)
            cellEd.Quit()
            cellEd = Nothing
            MsgBox("Could not open cell database. Please check to see if any partitions are reserved")
            End
        End Try

        Dim sb_Log As New StringBuilder

        For Each kvp As KeyValuePair(Of String, List(Of String)) In dicCellsToProcess

            Dim cePartition As CellEditorAddinLib.Partition = cellDB.Partitions.Item(kvp.Key)

            sb_Log.AppendLine(cePartition.Name & ":")

            For Each sCell As String In kvp.Value

                Dim ceCell As CellEditorAddinLib.Cell = cePartition.Cells.Item(sCell)

                RaiseEvent eUpdateStatus("Modifying " & cePartition.Name & ":" & sCell)

                sb_Log.AppendLine(vbTab & ceCell.Name & ":")

                '--------------------------
                ' UPDATE PACKAGE GROUP
                '--------------------------
                If chkbox_PG.Checked = True Then

                    Try

                        Dim Type As CellEditorAddinLib.ECellDBPackageGroup = ceCell.PackageGroup

                        Select Case sPackageGroup

                            Case "Buried"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageBuried
                            Case "Connector"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageConnector
                            Case "Discrete - Axial"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageDiscrete_Axial
                            Case "Discrete - Chip"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageDiscrete_Chip
                            Case "Discrete - Other"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageDiscrete_Other
                            Case "Discrete - Radial"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageDiscrete_Radial
                            Case "Edge Connector"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageEdgeConnector
                            Case "Embedded Capacitor"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageEmbeddedCapacitor
                            Case "Embedded Resistor"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageEmbeddedResistor
                            Case "General"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageGeneral
                            Case "Jumper"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageJumper
                            Case "IC - BGA"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageBGA
                            Case "IC - Bare Die"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageBareDie
                            Case ("IC - DIP")
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageDIP
                            Case "IC - Flip Chip"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageFlipChip
                            Case "IC - LCC"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageLCC
                            Case "IC - Other"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageOther
                            Case "IC - PGA"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackagePGA
                            Case "IC - PLCC"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackagePLCC
                            Case "IC - SIP"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageSIP
                            Case "IC - SOIC"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageSOIC
                            Case "Testpoint"
                                ceCell.PackageGroup = CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageTestPoint
                        End Select

                        sb_Log.AppendLine(vbTab & vbTab & "Package Type: (Before) " & Type.ToString() & ", (After) " & sPackageGroup)

                    Catch ex As Exception
                        sb_Log.AppendLine(vbTab & vbTab & "Unable to change package type. The system reported the following error: " & ex.Message())

                    End Try

                End If

                '--------------------------
                ' UPDATE UNITS
                '--------------------------
                If chkbox_Units.Checked = True Then

                    sb_Log.AppendLine(vbTab & vbTab & "Units: (Before) " & ceCell.Units.ToString() & ", (After) " & sUnits)

                    Select Case sUnits

                        Case "Inches"
                            ceCell.Units = CellEditorAddinLib.ECellDBUnit.ecelldbUnitInch
                        Case "Thousandths"
                            ceCell.Units = CellEditorAddinLib.ECellDBUnit.ecelldbUnitMils
                        Case "Millimeters"
                            ceCell.Units = CellEditorAddinLib.ECellDBUnit.ecelldbUnitMM
                        Case "Micrometers"
                            ceCell.Units = CellEditorAddinLib.ECellDBUnit.ecelldbUnitUM

                    End Select

                End If

                '--------------------------
                ' UPDATE DESCRIPTION
                '--------------------------
                If chkbox_Description.Checked = True Then

                    Dim sBefore As String = ceCell.Description.ToString()

                    If String.IsNullOrEmpty(sDescriptionBefore) Then
                        Try
                            ceCell.Description = sDescriptionAfter
                            sb_Log.AppendLine(vbTab & vbTab & "Description: (Before) " & sBefore & ", (After) " & ceCell.Description.ToString())
                        Catch ex As Exception
                            sb_Log.AppendLine("Invalid Description: " & sDescriptionAfter)
                        End Try

                    Else
                        ' Otherwise, replace the original entered string with the new entered string
                        Dim sCurrentDes As String = ceCell.Description
                        Dim sNewDes As String = sCurrentDes.Replace(sDescriptionBefore, sDescriptionAfter)
                        Try
                            ceCell.Description = sNewDes
                            sb_Log.AppendLine(vbTab & vbTab & "Description: (Before) " & sBefore & ", (After) " & ceCell.Description.ToString())
                        Catch ex As Exception
                            sb_Log.AppendLine("Invalid Description: " & sNewDes)
                        End Try
                    End If

                End If

                '--------------------------
                ' UPDATE CLEARANCE TYPE
                '--------------------------
                If chkbox_CT.Checked = True Then

                    Dim sBefore As String = ceCell.ClearanceType.ToString()

                    If String.IsNullOrEmpty(sClearanceTypeBefore) Then
                        Try
                            ceCell.ClearanceType = sClearanceTypeAfter
                            sb_Log.AppendLine(vbTab & vbTab & "Clearance Type: (Before) " & sBefore & ", (After) " & ceCell.ClearanceType.ToString())
                        Catch ex As Exception
                            sb_Log.AppendLine("Invalid clearance type: " & sClearanceTypeAfter)
                        End Try

                    Else
                        ' Otherwise, replace the original entered string with the new entered string
                        Dim sCurrentDes As String = ceCell.ClearanceType
                        Dim sNewClear As String = sCurrentDes.Replace(sClearanceTypeBefore, sClearanceTypeAfter)
                        Try
                            ceCell.ClearanceType = sNewClear
                            sb_Log.AppendLine(vbTab & vbTab & "Clearance Type: (Before) " & sBefore & ", (After) " & ceCell.ClearanceType.ToString())
                        Catch ex As Exception
                            sb_Log.AppendLine("Invalid clearance type: " & sNewClear)
                        End Try
                    End If

                End If

                '--------------------------
                ' UPDATE HEIGHT
                '--------------------------
                If chkbox_Height.Checked = True Then

                    Dim oHeightUnit As AAL.Number.Unit

                    Select Case sHeightUnit

                        Case "in"
                            oHeightUnit = AAL.Number.Unit.Inches
                        Case "mm"
                            oHeightUnit = AAL.Number.Unit.Millimeter
                        Case "th"
                            oHeightUnit = AAL.Number.Unit.Thousandth
                        Case "um"
                            oHeightUnit = AAL.Number.Unit.Micrometer

                    End Select

                    Dim oNumber As New AAL.Number

                    Select Case ceCell.Units

                        Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitInch

                            Dim dNewHeight As Double = oNumber.Convert(sHeight, oHeightUnit, AAL.Number.Unit.Inches)

                            sb_Log.AppendLine(vbTab & vbTab & "Height: (Before) " & ceCell.Height & "in" & ", (After) " & dNewHeight & "in")

                            ceCell.Height = dNewHeight

                        Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitMils

                            Dim dNewHeight As Double = oNumber.Convert(sHeight, oHeightUnit, AAL.Number.Unit.Thousandth)

                            sb_Log.AppendLine(vbTab & vbTab & "Height: (Before) " & ceCell.Height & "th" & ", (After) " & dNewHeight & "th")

                            ceCell.Height = dNewHeight

                        Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitMM

                            Dim dNewHeight As Double = oNumber.Convert(sHeight, oHeightUnit, AAL.Number.Unit.Millimeter)

                            sb_Log.AppendLine(vbTab & vbTab & "Height: (Before) " & ceCell.Height & "mm" & ", (After) " & dNewHeight & "mm")

                            ceCell.Height = dNewHeight

                        Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitUM

                            Dim dNewHeight As Double = oNumber.Convert(sHeight, oHeightUnit, AAL.Number.Unit.Micrometer)

                            sb_Log.AppendLine(vbTab & vbTab & "Height: (Before) " & ceCell.Height & "um" & ", (After) " & dNewHeight & "um")

                            ceCell.Height = dNewHeight

                    End Select

                End If

                '--------------------------
                ' UPDATE UNDERSIDE SPACE
                '--------------------------
                If chkbox_Underside.Checked = True Then

                    Dim oHeightUnit As AAL.Number.Unit

                    Select Case sUndersideUnit

                        Case "in"
                            oHeightUnit = AAL.Number.Unit.Inches
                        Case "mm"
                            oHeightUnit = AAL.Number.Unit.Millimeter
                        Case "th"
                            oHeightUnit = AAL.Number.Unit.Thousandth
                        Case "um"
                            oHeightUnit = AAL.Number.Unit.Micrometer

                    End Select

                    Dim oNumberConvert As New AAL.Number

                    Select Case ceCell.Units

                        Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitInch

                            Dim dNewHeight As Double = oNumberConvert.Convert(sUndersideHeight, oHeightUnit, AAL.Number.Unit.Inches)

                            sb_Log.AppendLine(vbTab & vbTab & "Underside Space: (Before) " & ceCell.UndersideSpace & "in" & ", (After) " & dNewHeight & "in")

                            ceCell.Height = dNewHeight

                        Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitMils

                            Dim dNewHeight As Double = oNumberConvert.Convert(sUndersideHeight, oHeightUnit, AAL.Number.Unit.Thousandth)

                            sb_Log.AppendLine(vbTab & vbTab & "Underside Space: (Before) " & ceCell.UndersideSpace & "th" & ", (After) " & dNewHeight & "th")

                            ceCell.Height = dNewHeight

                        Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitMM

                            Dim dNewHeight As Double = oNumberConvert.Convert(sUndersideHeight, oHeightUnit, AAL.Number.Unit.Millimeter)

                            sb_Log.AppendLine(vbTab & vbTab & "Underside Space: (Before) " & ceCell.UndersideSpace & "mm" & ", (After) " & dNewHeight & "mm")

                            ceCell.Height = dNewHeight

                        Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitUM

                            Dim dNewHeight As Double = oNumberConvert.Convert(sUndersideHeight, oHeightUnit, AAL.Number.Unit.Micrometer)

                            sb_Log.AppendLine(vbTab & vbTab & "Underside Space: (Before) " & ceCell.UndersideSpace & "um" & ", (After) " & dNewHeight & "um")

                            ceCell.Height = dNewHeight

                    End Select

                End If

                '--------------------------
                ' UPDATE CELL OVERHANG
                '--------------------------
                If chkbox_CellOverhang.Checked = True Then
                    sb_Log.AppendLine(vbTab & vbTab & "Cell Overhang: (Before) " & ceCell.AllowCellOverhang & ", (After) " & bAllowCellOverhang)

                    ceCell.AllowCellOverhang = bAllowCellOverhang

                End If

                '--------------------------
                ' UPDATE VERIFICATION STATUS
                '--------------------------
                If chkbox_Verification.Checked = True Then

                    sb_Log.AppendLine(vbTab & vbTab & "Verification: (Before) " & ceCell.Verified.ToString() & ", (After) " & sVerification)

                    Select Case sVerification

                        Case "Verified"
                            ceCell.Verified = CellEditorAddinLib.ECellDBStatus.ecelldbStatusVerified
                        Case "Unverified"
                            ceCell.Verified = CellEditorAddinLib.ECellDBStatus.ecelldbStatusUnverified
                    End Select

                End If

                sb_Log.AppendLine()

            Next

            sb_Log.AppendLine()

        Next

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Modify Cell Attributes.log", True, System.Text.Encoding.ASCII)
            writer.WriteLine(sb_Log.ToString())
        End Using

        cellEd.SaveActiveDatabase()
        'cellEd.Quit()

        cellEd.Quit()
        cellEd = Nothing
        cellDB = Nothing

        RaiseEvent eUpdateCellsComplete()

    End Sub

    Private Sub cb_Units_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_Units.CheckedChanged

        cbox_Units.Enabled = chkbox_Units.Checked

        If chkbox_Units.Checked = False Then
            cbox_Units.SelectedIndex = -1
        End If

    End Sub

    Private Sub chkbox_PG_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_PG.CheckedChanged
        cbox_PG.Enabled = chkbox_PG.Checked

        If chkbox_PG.Checked = False Then
            cbox_PG.SelectedIndex = -1
        End If
    End Sub

    Private Sub chkbox_CT_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_CT.CheckedChanged
        tbox_CT_After.Enabled = chkbox_CT.Checked
        tbox_CT_Before.Enabled = chkbox_CT.Checked

        If chkbox_CT.Checked = False Then
            tbox_CT_After.Clear()
            tbox_CT_Before.Clear()
        End If

    End Sub

    Private Sub chkbox_Height_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_Height.CheckedChanged
        tbox_Height.Enabled = chkbox_Height.Checked
        cbox_HeightUnits.Enabled = chkbox_Height.Checked

        If chkbox_Height.Checked = False Then
            tbox_Height.Clear()
            cbox_HeightUnits.SelectedIndex = -1
        Else '
            cbox_HeightUnits.SelectedIndex = 0
        End If

    End Sub

    Private Sub cb_Underside_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_Underside.CheckedChanged
        tbox_Underside.Enabled = chkbox_Underside.Checked
        cbox_UndersideHeightUnits.Enabled = chkbox_Underside.Checked

        If chkbox_Underside.Checked = False Then
            tbox_Underside.Clear()
            cbox_UndersideHeightUnits.SelectedIndex = -1
        Else
            cbox_UndersideHeightUnits.SelectedIndex = 0
        End If

    End Sub

    Private Sub chkbox_Verification_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_Verification.CheckedChanged

        cbox_Verification.Enabled = chkbox_Verification.Checked

        If chkbox_Verification.Checked = False Then
            cbox_Verification.SelectedIndex = -1
        End If

    End Sub

    Private Sub chkbox_CellOverhang_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_CellOverhang.CheckedChanged

        cbox_CellOverhang.Enabled = chkbox_CellOverhang.Checked

        If chkbox_CellOverhang.Checked = False Then
            cbox_CellOverhang.SelectedIndex = -1
        End If

    End Sub

    Private Sub chkbox_Movable_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_Movable.CheckedChanged

        cbox_MoveablePins.Enabled = chkbox_Movable.Checked

        If chkbox_Movable.Checked = False Then
            cbox_MoveablePins.SelectedIndex = -1
        End If

    End Sub

    Private Sub chkbox_Description_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_Description.CheckedChanged

        tbox_Descript_Before.Enabled = chkbox_Description.Checked
        tbox_Descript_After.Enabled = chkbox_Description.Checked

        If chkbox_Description.Checked = False Then
            tbox_Descript_After.Clear()
            tbox_Descript_Before.Clear()
        End If

    End Sub

    Private Sub tbox_Height_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbox_Height.TextChanged

        If IsNumeric(tbox_Height.Text) = False Then
            tbox_Height.ForeColor = Color.Red
        Else
            tbox_Height.ForeColor = SystemColors.ControlText
        End If

    End Sub

    Private Sub tbox_Underside_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbox_Underside.TextChanged
        If IsNumeric(tbox_Underside.Text) = False Then
            tbox_Underside.ForeColor = Color.Red
        Else
            tbox_Underside.ForeColor = SystemColors.ControlText
        End If
    End Sub

    Private Sub btnSelectAll_Click(sender As System.Object, e As System.EventArgs)
        RemoveHandler tv_Cells.BeforeCheck, AddressOf tv_Cells_BeforeCheck

        AddHandler eUpdateNodesFinished, AddressOf CheckNodeComplete
        AddHandler eCheckNode, AddressOf CheckNode
        Dim th_UpdateCount As Thread = New Thread(AddressOf CheckAllNodes)
        th_UpdateCount.IsBackground = True
        th_UpdateCount.Start(True)

    End Sub

    Private Sub btnClearSelected_Click(sender As System.Object, e As System.EventArgs)
        RemoveHandler tv_Cells.BeforeCheck, AddressOf tv_Cells_BeforeCheck

        AddHandler eUpdateNodesFinished, AddressOf CheckNodeComplete
        AddHandler eCheckNode, AddressOf CheckNode
        Dim th_UpdateCount As Thread = New Thread(AddressOf CheckAllNodes)
        th_UpdateCount.IsBackground = True
        th_UpdateCount.Start(False)
    End Sub
End Class