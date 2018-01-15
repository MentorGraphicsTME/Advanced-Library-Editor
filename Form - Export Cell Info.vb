Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Threading
Imports System.Drawing

Public Class frmExportCellInfo

    Property sCellPrefix As String

    Dim dicPartitionToCell As New Dictionary(Of String, List(Of String))

    'dictionary
    Property dicErrors As New Dictionary(Of String, String)

    'Events
    Event eProcessComplete()
    Event eUpdateStatus(status As String)

    'Object
    'Property frmmain.librarydata As Data

    'Delegates
    Delegate Sub d_CheckNode(ByVal value As TreeNode, ByVal checkstate As Boolean)
    Delegate Sub d_CheckNodesComplete()
    Delegate Sub d_ProcessComplete()
    Delegate Sub d_UpdateStatus(ByVal status As String)

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub btn_Export_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Export.Click

        If chkbox_Cellname.Checked = False And Chkbox_Description.Checked = False And
                chkbox_Height.Checked = False And chkbox_LastModified.Checked = False And
                chkbox_MaxCompSize.Checked = False And chkbox_PackageType.Checked = False And
                chkbox_PinCount.Checked = False And chkbox_PinInfo.Checked = False And
                chkbox_UserLayers.Checked = False And chkbox_Units.Checked = False Then
            MessageBox.Show("Please select at least one type of information to be exported.")
            Exit Sub
        End If

        gb_Options.Enabled = False
        gb_General.Enabled = False

        dicPartitionToCell.Clear()

        WaitGif.Enabled = True

        frmMain.NotifyIcon.ShowBalloonTip(2000, "Export Cell Info", "Gathering Data.", ToolTipIcon.Info)
        ts_Status.Text = "Gathering Data..."


        For Each PartitionNode As TreeNode In tv_Cells.Nodes

            If PartitionNode.Checked = True Then
                Dim l_Cells As New List(Of String)(frmMain.librarydata.CellsByPartition(PartitionNode.Text))
                dicPartitionToCell.Item(PartitionNode.Text) = l_Cells
            ElseIf PartitionNode.Checked = False Then
                Dim l_Cells As New List(Of String)
                For Each CellNode As TreeNode In PartitionNode.Nodes
                    If CellNode.Checked = True And frmMain.librarydata.CellsByPartition(PartitionNode.Text).Contains(CellNode.Text) Then
                        l_Cells.Add(CellNode.Text)
                    End If
                Next
                If l_Cells.Count <> 0 Then
                    dicPartitionToCell.Item(PartitionNode.Text) = l_Cells
                End If
            End If
        Next

        frmMain.NotifyIcon.ShowBalloonTip(2000, "Export Cell Info", "Exporting Info.", ToolTipIcon.Info)

        Dim t_ProcessProperties As Thread = New Threading.Thread(AddressOf Process_Cells)
        t_ProcessProperties.IsBackground = True
        t_ProcessProperties.Start()

    End Sub

    Private Sub frmExportCellInfo_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        For Each kvp As KeyValuePair(Of String, List(Of String)) In frmMain.librarydata.CellsByPartition

            Dim oPartitionNode As TreeNode = tv_Cells.Nodes.Add(kvp.Key)

            For Each sCell As String In kvp.Value
                oPartitionNode.Nodes.Add(sCell)
            Next

        Next

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eProcessComplete, AddressOf ExportCellsComplete

        tv_Cells.Sort()

    End Sub

    Private Sub chkbox_IndividualWorkbook_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_IndividualWorkbook.CheckedChanged

        If chkbox_IndividualWorkbook.Checked = True Then

            If chkbox_CloseXLS.Checked = False Then

                chkbox_CloseXLS.Checked = True
                MessageBox.Show("It is recommended to allow excel to close each workbook.")

            End If

        End If

    End Sub

    Private Sub tv_Cells_BeforeCheck(sender As System.Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tv_Cells.BeforeCheck
        RemoveHandler tv_Cells.BeforeCheck, AddressOf tv_Cells_BeforeCheck

        tv_Cells.Enabled = False
        gb_General.Enabled = False
        gb_Options.Enabled = False

        Dim oCellCheck As New CheckNode
        AddHandler oCellCheck.CheckNode, AddressOf CheckNode
        AddHandler oCellCheck.CheckNodesComplete, AddressOf CheckNodesComplete
        Dim th_UpdateCount As Thread = New Thread(AddressOf oCellCheck.Update)
        th_UpdateCount.IsBackground = True
        th_UpdateCount.Start(e.Node)
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

            AddHandler tv_Cells.BeforeCheck, AddressOf tv_Cells_BeforeCheck
            tv_Cells.Enabled = True
            gb_General.Enabled = True
            gb_Options.Enabled = True

        End If

    End Sub

    Private Sub Process_Cells()

        Dim cellEd As CellEditorAddinLib.CellEditorDlg
        Dim cellDB As CellEditorAddinLib.CellDB
        Dim oCellDoc As MGCPCB.Document

        RaiseEvent eUpdateStatus("Opening cell editor...")

        Try

            'Open the Cell Editor dialog and open the library database
            'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
            'cellDB = cellEd.OpenDatabase(frmMain.libDoc.FullName, False)
            cellEd = frmMain.libDoc.CellEditor
            cellDB = cellEd.ActiveDatabase

        Catch ex As Exception
            cellEd.Quit()
            cellEd = Nothing
            'cellEd.CloseActiveDatabase(False)
            MsgBox("Could not open cell database. Please check to see if any partitions are reserved")
            RaiseEvent eUpdateStatus("Could not open cell editor...")
            End
        End Try

        Dim oXL As Excel.Application

        For Each cePartition As CellEditorAddinLib.Partition In cellDB.Partitions()

            Dim lCellsProcessed As New List(Of String)

            If dicPartitionToCell.ContainsKey(cePartition.Name) Then

                Dim cellsToProcess As List(Of String) = dicPartitionToCell.Item(cePartition.Name)

                If cePartition.Cells.Count > 0 And cellsToProcess.Count > 0 Then

                    Dim oWB As Excel.Workbook
                    Dim oSheet As Excel.Worksheet
                    Dim oRange As Excel.Range

                    ' Start Excel and get Application object.
                    oXL = New Excel.Application

                    ' Set some properties
                    oXL.Visible = True
                    oXL.DisplayAlerts = False

                    If chkbox_IndividualWorkbook.Checked = False Then
                        ' Get a new workbook.
                        oWB = oXL.Workbooks.Add
                    End If

                    Dim sheetNames As New List(Of String)

                    For Each ceCell As CellEditorAddinLib.Cell In cePartition.Cells(CellEditorAddinLib.ECellDBCellType.ecelldbCellTypePackage)

                        If cellsToProcess.Contains(ceCell.Name) Then

                            RaiseEvent eUpdateStatus("Exporting " & cePartition.Name & ":" & ceCell.Name)

                            If chkbox_IndividualWorkbook.Checked = True Then
                                ' Get a new workbook.
                                oWB = oXL.Workbooks.Add

                            End If

                            oWB = oXL.ActiveWorkbook
                            oWB.Sheets.Add()

                            ' Get the active sheet
                            oSheet = oWB.ActiveSheet

                            Dim sCellName As String = ceCell.Name

                            If sCellName.Length > 31 Or lCellsProcessed.Contains(sCellName) Then

                                Dim i As Integer = 1

                                Do Until Not lCellsProcessed.Contains(sCellName.Substring(0, 28) & "_" & i)

                                    i += 1

                                Loop

                                oSheet.Name = sCellName.Substring(0, 28) & "_" & i
                                lCellsProcessed.Add(sCellName.Substring(0, 28) & "_" & i)


                            Else
                                oSheet.Name = sCellName
                                lCellsProcessed.Add(sCellName)
                            End If

                            Dim iRowCount As Integer = 1

                            If chkbox_Cellname.Checked = True Then
                                oSheet.Cells(iRowCount, 1) = "Cell Name:"
                                oSheet.Cells(iRowCount, 2) = ceCell.Name
                                iRowCount += 1
                            End If

                            If chkbox_LastModified.Checked = True Then
                                oSheet.Cells(iRowCount, 1) = "Last Modified:"
                                oSheet.Cells(iRowCount, 2) = ceCell.LastModification
                                iRowCount += 1
                            End If
                            If chkbox_PackageType.Checked = True Then
                                oSheet.Cells(iRowCount, 1) = "Package Type:"
                                Select Case ceCell.PackageGroup

                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageBareDie
                                        oSheet.Cells(iRowCount, 2) = "Bare Die"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageBGA
                                        oSheet.Cells(iRowCount, 2) = "BGA"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageBuried
                                        oSheet.Cells(iRowCount, 2) = "Buried"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageConnector
                                        oSheet.Cells(iRowCount, 2) = "Connector"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageDIP
                                        oSheet.Cells(iRowCount, 2) = "DIP"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageDiscrete_Axial
                                        oSheet.Cells(iRowCount, 2) = "Discrete - Axial"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageDiscrete_Chip
                                        oSheet.Cells(iRowCount, 2) = "Discrete - Chip"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageDiscrete_Other
                                        oSheet.Cells(iRowCount, 2) = "Discrete - Other"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageDiscrete_Radial
                                        oSheet.Cells(iRowCount, 2) = "Discrete - Radial"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageEdgeConnector
                                        oSheet.Cells(iRowCount, 2) = "Edge Connector"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageEmbeddedCapacitor
                                        oSheet.Cells(iRowCount, 2) = "Embedded Capacitor"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageEmbeddedResistor
                                        oSheet.Cells(iRowCount, 2) = "Embedded Resistor"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageFlipChip
                                        oSheet.Cells(iRowCount, 2) = "Flip Chip"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageGeneral
                                        oSheet.Cells(iRowCount, 2) = "General"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageJumper
                                        oSheet.Cells(iRowCount, 2) = "Jumper"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageLCC
                                        oSheet.Cells(iRowCount, 2) = "LCC"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageOther
                                        oSheet.Cells(iRowCount, 2) = "Other"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackagePGA
                                        oSheet.Cells(iRowCount, 2) = "PGA"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackagePLCC
                                        oSheet.Cells(iRowCount, 2) = "PLCC"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageReusableCircuit
                                        oSheet.Cells(iRowCount, 2) = "Reusable Circuit"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageRFCircuit
                                        oSheet.Cells(iRowCount, 2) = "RF Circuit"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageRFShape
                                        oSheet.Cells(iRowCount, 2) = "RF Shape"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageSIP
                                        oSheet.Cells(iRowCount, 2) = "SIP"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageSOIC
                                        oSheet.Cells(iRowCount, 2) = "SOIC"
                                    Case CellEditorAddinLib.ECellDBPackageGroup.ecelldbPackageTestPoint
                                        oSheet.Cells(iRowCount, 2) = "Testpoint"
                                End Select
                                iRowCount += 1
                            End If

                            If Chkbox_Description.Checked = True Then
                                oSheet.Cells(iRowCount, 1) = "Description:"
                                oSheet.Cells(iRowCount, 2) = ceCell.Description
                                iRowCount += 1
                            End If

                            If chkbox_Units.Checked = True Then
                                oSheet.Cells(iRowCount, 1) = "Unit:"

                                Select Case ceCell.Units

                                    Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitInch
                                        oSheet.Cells(iRowCount, 2) = "Inches"
                                    Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitMils
                                        oSheet.Cells(iRowCount, 2) = "Thousandths"
                                    Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitMM
                                        oSheet.Cells(iRowCount, 2) = "Millimeters"
                                    Case CellEditorAddinLib.ECellDBUnit.ecelldbUnitUM
                                        oSheet.Cells(iRowCount, 2) = "Micrometers"
                                End Select
                                iRowCount += 1
                            End If

                            If chkbox_Height.Checked = True Then
                                oSheet.Cells(iRowCount, 1) = "Height:"
                                oSheet.Cells(iRowCount, 2) = ceCell.Height
                                iRowCount += 1
                            End If

                            Dim bOpenedCellEditor As Boolean = False

                            Try
                                oCellDoc = ceCell.Edit()     ' Edit invokes Cell Editor, returning an AutoActive document
                                bOpenedCellEditor = True
                            Catch ex As Exception
                                dicErrors.Add(ceCell.Name, ex.Message)
                                bOpenedCellEditor = False
                            End Try

                            If bOpenedCellEditor = True Then

                                Dim oCellEditorApp As MGCPCB.IMGCPCBApplication
                                oCellEditorApp = Nothing
                                oCellEditorApp = oCellDoc.Application

                                If chkbox_MaxCompSize.Checked = True Then

                                    iRowCount += 1

                                    oSheet.Cells(iRowCount, 1) = "Max Component Size:"

                                    Dim iMaxX As Double
                                    Dim iMaxY As Double
                                    Dim iMinX As Double
                                    Dim iMinY As Double

                                    Dim oPlacementOutlines As MGCPCB.PlacementOutlines
                                    Dim oPlacementOutline As MGCPCB.PlacementOutline

                                    oPlacementOutlines = oCellDoc.PlacementOutlines

                                    For Each oPlacementOutline In oPlacementOutlines

                                        iMaxX = oPlacementOutline.Extrema.MaxX
                                        iMaxY = oPlacementOutline.Extrema.MaxY
                                        iMinX = oPlacementOutline.Extrema.MinX
                                        iMinY = oPlacementOutline.Extrema.MinY

                                    Next

                                    If iMinX < 0 Then

                                        iMinX = iMinX * -1

                                    End If

                                    If iMinY < 0 Then

                                        iMinY = iMinY * -1

                                    End If

                                    oSheet.Cells(iRowCount, 2) = (iMinX + iMaxX) & "x" & (iMinY + iMaxY)
                                    iRowCount += 1

                                    oSheet.Cells(iRowCount, 1) = "Surface Area"
                                    Select Case oCellDoc.CurrentUnit

                                        Case MGCPCB.EPcbUnit.epcbUnitInch
                                            oSheet.Cells(iRowCount, 2) = ((iMinX + iMaxX) * (iMinY + iMaxY)) & " sq. in."
                                        Case MGCPCB.EPcbUnit.epcbUnitMils
                                            oSheet.Cells(iRowCount, 2) = ((iMinX + iMaxX) * (iMinY + iMaxY)) & " sq. th."
                                        Case MGCPCB.EPcbUnit.epcbUnitMM
                                            oSheet.Cells(iRowCount, 2) = ((iMinX + iMaxX) * (iMinY + iMaxY)) & " sq. mm."
                                        Case MGCPCB.EPcbUnit.epcbUnitUM
                                            oSheet.Cells(iRowCount, 2) = ((iMinX + iMaxX) * (iMinY + iMaxY)) & " sq. um."
                                    End Select

                                    iRowCount += 1

                                End If

                                If chkbox_UserLayers.Checked = True Then

                                    iRowCount += 1

                                    oSheet.Cells(iRowCount, 1) = "User Layers"

                                    Dim oUserLayers As MGCPCB.UserLayers
                                    Dim oUserLayer As MGCPCB.UserLayer

                                    oUserLayers = oCellDoc.UserLayers

                                    For Each oUserLayer In oUserLayers

                                        oSheet.Cells(iRowCount, 2) = oUserLayer.Name
                                        iRowCount += 1
                                    Next

                                    iRowCount += 1

                                End If

                                Dim oPins As MGCPCB.Pins
                                Dim oPin As MGCPCB.Pin

                                oPins = oCellDoc.Pins

                                If chkbox_PinCount.Checked = True Then
                                    oSheet.Cells(iRowCount, 1) = "Pin Count:"
                                    oSheet.Cells(iRowCount, 2) = oPins.Count
                                    iRowCount += 1
                                End If

                                If chkbox_PinInfo.Checked = True Then

                                    iRowCount += 1

                                    oSheet.Cells(iRowCount, 1) = "Pin Name"
                                    oSheet.Cells(iRowCount, 2) = "X-Location"
                                    oSheet.Cells(iRowCount, 3) = "Y-Location"
                                    oSheet.Cells(iRowCount, 4) = "Orientation"
                                    oSheet.Cells(iRowCount, 5) = "Padstack"
                                    iRowCount += 1

                                    For Each oPin In oPins

                                        oSheet.Cells(iRowCount, 1) = oPin.Name
                                        oSheet.Cells(iRowCount, 2) = oPin.PositionX
                                        oSheet.Cells(iRowCount, 3) = oPin.PositionY
                                        oSheet.Cells(iRowCount, 4) = oPin.Orientation
                                        oSheet.Cells(iRowCount, 5) = oPin.OriginalPadstack
                                        iRowCount += 1

                                    Next
                                End If

                                Try
                                    oCellDoc.Close(True)    ' save modified cell
                                Catch ex As Exception
                                    oCellDoc.Close(False)    ' close cell without saving if something goes wrong
                                    dicErrors.Add(ceCell.Name, ex.Message)
                                Finally
                                    oCellDoc = Nothing
                                End Try

                                Try
                                    cellEd.SaveActiveDatabase()
                                Catch ex As Exception
                                    dicErrors.Add(ceCell.Name, ex.Message)
                                End Try

                            Else
                                Try
                                    dicErrors.Add(ceCell.Name, "Unable to open cell")
                                Catch ex As Exception

                                End Try
                                oCellDoc = Nothing

                            End If

                            If chkbox_IndividualWorkbook.Checked = True Then
                                If Not Directory.Exists(frmMain.librarydata.LogPath & "Cell Export" & "\" & cePartition.Name) Then

                                    Directory.CreateDirectory(frmMain.librarydata.LogPath & "Cell Export" & "\" & cePartition.Name)

                                End If

                                oSheet.SaveAs(Filename:=frmMain.librarydata.LogPath & "Cell Export\" & cePartition.Name & "\" & ceCell.Name, FileFormat:=Excel.XlFileFormat.xlWorkbookDefault)

                                If chkbox_CloseXLS.Checked = True Then
                                    oXL.ActiveWorkbook.Close(True)
                                End If
                            End If
                            cellsToProcess.Remove(ceCell.Name)
                        End If


                    Next

                    If chkbox_IndividualWorkbook.Checked = False Then
                        If Not Directory.Exists(frmMain.librarydata.LogPath & "Cell Export") Then

                            Directory.CreateDirectory(frmMain.librarydata.LogPath & "Cell Export")

                        End If

                        oSheet.SaveAs(Filename:=frmMain.librarydata.LogPath & "Cell Export\" & cePartition.Name, FileFormat:=Excel.XlFileFormat.xlWorkbookDefault)

                        If chkbox_CloseXLS.Checked = True Then
                            oXL.ActiveWorkbook.Close(True)
                            oXL.Quit()
                        End If
                    End If
                End If
            End If



        Next

        'cellEd.CloseActiveDatabase(False)
        cellEd.SaveActiveDatabase()
        cellEd.Quit()
        cellEd = Nothing

        RaiseEvent eProcessComplete()


    End Sub

    Private Sub ExportCellsComplete()
        If Me.InvokeRequired Then
            Dim d As New d_ProcessComplete(AddressOf ExportCellsComplete)
            Me.Invoke(d)

        Else

            gb_Options.Enabled = True
            gb_General.Enabled = True

            WaitGif.Enabled = False

            frmMain.NotifyIcon.ShowBalloonTip(2000, "Export Cell Info", "Process Complete.", ToolTipIcon.Info)
            ts_Status.Text = "Export complete."
        End If
    End Sub

End Class