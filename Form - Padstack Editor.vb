Imports PadstackEditorLib
Imports System.Text
Imports System.IO
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.ComponentModel
Imports System.Drawing

Public Class frmPadstackEditor
    'Mentor Graphics
    Dim expApp As MGCPCB.Application
    Dim expDoc As MGCPCB.Document
    Dim oPadStackDB As PadstackDB
    'Dim libDoc As LibraryManager.IMGCLMLibrary
    'Property frmmain.librarydata As Data

    Enum Unit

        <Description("th")> th = 0
        <Description("in")> inch = 1
        <Description("mm")> mm = 2
        <Description("um")> um = 3

    End Enum

    'Dictionary
    Dim dicPads As New Dictionary(Of String, String)
    Dim dicRenamePads As New Dictionary(Of String, String)
    Dim dic_PadstackTypes As New Dictionary(Of String, String)
    Dim dic_BadHoleNames As New Dictionary(Of String, List(Of String))
    Dim dic_BadPadNames As New Dictionary(Of String, List(Of String))

    'List
    Dim l_Filter As New List(Of String)

    'Boolean
    Dim bErrors As Boolean = False
    Dim bWarnings As Boolean = False

    'String
    Dim SolderMaskUnit As String
    Dim SolderMaskGrow As Boolean = False
    Dim SolderPasteUnit As String
    Dim SolderPasteGrow As Boolean = False
    Dim ModifyUnit As String

    'Event
    Event eUpdateComplete()
    Event eUpdateStatus(status As String)

    'Delegate
    Delegate Sub d_UpdateStatus(ByVal status As String)
    Delegate Sub d_UpdateComplete()

    'Enum Unit
    '    inch = 0
    '    MM = 2
    '    TH = 1
    '    UM = 3
    'End Enum

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Modify.Click

        WaitGif.Enabled = True

        If File.Exists(frmMain.librarydata.LogPath & "Padstack Editor.log") Then

            File.Delete(frmMain.librarydata.LogPath & "Padstack Editor.log")

        End If

        l_Filter.Clear()

        dicPads.Clear()
        dicRenamePads.Clear()

        For Each s_Type As String In chklistbox_Filter.CheckedItems
            Dim s_PadstackType As String
            dic_PadstackTypes.TryGetValue(s_Type, s_PadstackType)
            l_Filter.Add(s_PadstackType)
        Next

        If cbox_Mask_GrowShrink.Text = "Grow" Then
            SolderMaskGrow = True
        End If

        If cbox_Paste_GrowShrink.Text = "Grow" Then
            SolderPasteGrow = True
        End If

        If chkbox_ModifyUnits.Checked = True Then

            ModifyUnit = cbox_ChangeUnit.Text

        End If

        SolderMaskUnit = cbox_UnitSolderMask.Text
        SolderPasteUnit = cbox_UnitSolderPaste.Text

        Dim t_Update As Thread = New Threading.Thread(AddressOf UpdatePadstacks)
        t_Update.IsBackground = True
        t_Update.Start()

    End Sub

    Private Sub btn_Browse_Exp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Using ofd As New OpenFileDialog
            ofd.Filter = "Expedition PCB (*.pcb)|*.pcb"
            '            ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"
            ofd.InitialDirectory = frmMain.librarydata.LibPath

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                'tbox_ExpeditionPCB.Text = ofd.FileName

            End If
        End Using

    End Sub

    Sub openExp()

        expApp = CreateObject("MGCPCB.ExpeditionPCBApplication" & "." & frmMain.progID)
        expApp.Visible = True
        'expDoc = GetLicensedDoc(expApp, tbox_ExpeditionPCB.Text)

    End Sub

    Private Sub chkbox_AddSoldermask_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If (chkbox_AddTopSoldermask.Checked = True) Or (chkbox_AddBottomSoldermask.Checked = True) Then
            cbox_UnitSolderMask.Enabled = True
            tbox_Soldermask.Enabled = True
        Else
            cbox_UnitSolderMask.Enabled = False
            tbox_Soldermask.Enabled = False
        End If

    End Sub

    Private Sub cbox_Unit_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        'Select Case cbox_Unit.Text

        '    Case "in"
        '        lblUnit1.Text = "th"
        '        lblUnit2.Text = "mm"
        '        lblUnit3.Text = "um"

        '    Case "th"
        '        lblUnit1.Text = "in"
        '        lblUnit2.Text = "mm"
        '        lblUnit3.Text = "um"

        '    Case "mm"
        '        lblUnit1.Text = "in"
        '        lblUnit2.Text = "th"
        '        lblUnit3.Text = "um"

        '    Case "um"
        '        lblUnit1.Text = "in"
        '        lblUnit2.Text = "th"
        '        lblUnit3.Text = "mm"

        'End Select

        'Try
        '    Select Case cbox_Unit.Text

        '        Case "in"

        '            lblNum1.Text = Math.Round(tbox_Soldermask.Text * 1000, 4)
        '            lblNum2.Text = Math.Round(tbox_Soldermask.Text * 25.4, 4)
        '            lblNum3.Text = Math.Round(tbox_Soldermask.Text * 25400, 4)

        '        Case "th"

        '            lblNum1.Text = Math.Round(tbox_Soldermask.Text / 1000, 4)
        '            lblNum2.Text = Math.Round(tbox_Soldermask.Text * 25.4, 4)
        '            lblNum3.Text = Math.Round(tbox_Soldermask.Text * 0.0254, 4)

        '        Case "mm"

        '            lblNum1.Text = Math.Round(tbox_Soldermask.Text * 0.03937, 4)
        '            lblNum2.Text = Math.Round(tbox_Soldermask.Text * 39.37, 4)
        '            lblNum3.Text = Math.Round(tbox_Soldermask.Text * 1000, 4)

        '        Case "um"

        '            lblNum1.Text = Math.Round(tbox_Soldermask.Text * 0.00003937, 4)
        '            lblNum2.Text = Math.Round(tbox_Soldermask.Text * 0.03937, 4)
        '            lblNum3.Text = Math.Round(tbox_Soldermask.Text / 1000, 4)

        '    End Select
        'Catch ex As Exception

        'End Try

    End Sub

    Private Sub chkbox_Soldermask_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_AddBottomSoldermask.CheckedChanged, chkbox_AddTopSoldermask.CheckedChanged

        If (chkbox_AddTopSoldermask.Checked = True) Or (chkbox_AddBottomSoldermask.Checked = True) Then

            gb_MaskModify.Enabled = True
            chklistbox_Filter.Enabled = True

        Else
            If (chkbox_BotSolderpaste.Checked = False) And (chkbox_TopSolderPaste.Checked = False) And chkbox_AddBottomSoldermask.Checked = False And chkbox_AddTopSoldermask.Checked = False Then
                chklistbox_Filter.Enabled = False
            End If

            chkbox_CopyMask.Checked = False
            tbox_Soldermask.Clear()
            chkbox_ModifyMask.Checked = False
            cbox_Mask_GrowShrink.SelectedIndex = -1
            cbox_UnitSolderMask.SelectedIndex = -1
            gb_MaskModify.Enabled = False

        End If

    End Sub

    Private Sub chkbox_SolderPaste_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_BotSolderpaste.CheckedChanged, chkbox_TopSolderPaste.CheckedChanged

        If (chkbox_BotSolderpaste.Checked = True) Or (chkbox_TopSolderPaste.Checked = True) Then

            gb_PasteModify.Enabled = True
            chklistbox_Filter.Enabled = True

        Else
            If (chkbox_BotSolderpaste.Checked = False) And (chkbox_TopSolderPaste.Checked = False) And chkbox_AddBottomSoldermask.Checked = False And chkbox_AddTopSoldermask.Checked = False Then
                chklistbox_Filter.Enabled = False
            End If

            chkbox_CopyPaste.Checked = False
            chkbox_ModifyPaste.Checked = False
            tbox_Solderpaste.Clear()
            cbox_Paste_GrowShrink.SelectedIndex = -1
            cbox_UnitSolderPaste.SelectedIndex = -1
            gb_PasteModify.Enabled = False

        End If

    End Sub

    'Private Sub chkbox_CopyMaskPad_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_CopyMaskPad.CheckedChanged

    '    If chkbox_CopyMaskPad.Checked = True Then
    '        tbox_Soldermask.Enabled = False
    '        cbox_Mask_GrowShrink.Enabled = False
    '        cbox_UnitSolderMask.Enabled = False
    '    Else
    '        tbox_Soldermask.Enabled = True
    '        cbox_Mask_GrowShrink.Enabled = True
    '        cbox_UnitSolderMask.Enabled = True
    '    End If

    'End Sub

    'Private Sub chkbox_CopyPastePad_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_CopyPastePad.CheckedChanged

    '    If chkbox_CopyPastePad.Checked = True Then
    '        tbox_Solderpaste.Enabled = False
    '        cbox_Paste_GrowShrink.Enabled = False
    '        cbox_UnitSolderPaste.Enabled = False
    '    Else
    '        tbox_Solderpaste.Enabled = True
    '        cbox_Paste_GrowShrink.Enabled = True
    '        cbox_UnitSolderPaste.Enabled = True

    '    End If

    'End Sub

    Private Sub frmPadstackEditor_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        dic_PadstackTypes.Add("Bond Pin", "epsdbPadstackTypeBondPin")
        dic_PadstackTypes.Add("Fiducial", "epsdbPadstackTypeFiducial")
        dic_PadstackTypes.Add("Mounting Hole", "epsdbPadstackTypeMountingHole")
        dic_PadstackTypes.Add("Pin - Die", "epsdbPadstackTypePartStackPin")
        dic_PadstackTypes.Add("Pin - SMD", "epsdbPadstackTypePinSMD")
        dic_PadstackTypes.Add("Pin - Through", "epsdbPadstackTypePinThrough")
        dic_PadstackTypes.Add("Shearing Hole", "epsdbPadstackTypeShearingHole")
        dic_PadstackTypes.Add("Tooling Hole", "epsdbPadstackTypeToolingHole")
        dic_PadstackTypes.Add("Via", "epsdbPadstackTypeVia")

        For Each i In [Enum].GetValues(GetType(Unit))
            cbox_ChangeUnit.Items.Add(i)
            cbox_UnitSolderMask.Items.Add(i)
            cbox_UnitSolderPaste.Items.Add(i)
        Next

        AddHandler eUpdateComplete, AddressOf UpdateComplete
        AddHandler eUpdateStatus, AddressOf UpdateStatus

    End Sub

    Private Sub UpdatePadstacks()

        Dim PadStackEdDlg As New PadstackEditorDlg
        oPadStackDB = PadStackEdDlg.OpenDatabaseEx(frmMain.libDoc.FullName)

        Dim sb_Report As New StringBuilder

        If chkbox_HoleAutoName.Checked = True Then

            sb_Report.AppendLine("Renamed Holes:")

            For Each oHole As Hole In oPadStackDB.Holes

                sb_Report.AppendLine(vbTab & oHole.Name & ":")

                Dim sNameBefore As String = oHole.Name
                If oHole.AutoName = True Then
                    oHole.AutoName = False
                End If

                If chkbox_ModifyUnits.Checked = True Then

                    RaiseEvent eUpdateStatus("Changing unit for pad: " & oHole.Name)

                    Select Case oHole.Units

                        Case EPsDBUnit.epsdbUnitInch
                            sb_Report.AppendLine(vbTab & vbTab & "Unit Before: in, Unit After: " & ModifyUnit)
                        Case EPsDBUnit.epsdbUnitMils
                            sb_Report.AppendLine(vbTab & vbTab & "Unit Before: th, Unit After: " & ModifyUnit)
                        Case EPsDBUnit.epsdbUnitMM
                            sb_Report.AppendLine(vbTab & vbTab & "Unit Before: mm, Unit After: " & ModifyUnit)
                        Case EPsDBUnit.epsdbUnitUM
                            sb_Report.AppendLine(vbTab & vbTab & "Unit Before: um, Unit After: " & ModifyUnit)
                    End Select

                    Select Case ModifyUnit

                        Case "inch"
                            oHole.Units = EPsDBUnit.epsdbUnitInch
                        Case "th"
                            oHole.Units = EPsDBUnit.epsdbUnitMils
                        Case "mm"
                            oHole.Units = EPsDBUnit.epsdbUnitMM
                        Case "um"
                            oHole.Units = EPsDBUnit.epsdbUnitUM
                    End Select

                End If

                RaiseEvent eUpdateStatus("Auto naming hole: " & oHole.Name)

                oHole.AutoName = True

                If oHole.Name.Contains("_") Then
                    If Regex.IsMatch(oHole.Name.Substring(0, oHole.Name.IndexOf("_")), "[0-9]") = True Then
                        If Not sNameBefore = oHole.Name Then
                            sb_Report.AppendLine(vbTab & vbTab & "Renamed to: " & oHole.Name)
                        End If
                    Else
                        sb_Report.AppendLine(vbTab & vbTab & "ERROR: Unable to auto name hole " & sNameBefore & " because it would result in the invalid name of: " & oHole.Name & ".")
                        oHole.AutoName = False
                        If Not dic_BadHoleNames.ContainsKey(sNameBefore) Then
                            dic_BadHoleNames.Item(oHole.Name) = New List(Of String)
                        End If
                    End If
                Else
                    If Not sNameBefore = oHole.Name Then
                        sb_Report.AppendLine(vbTab & vbTab & "Renamed to: " & oHole.Name)
                    End If
                End If

            Next

            sb_Report.AppendLine()

        End If

        If chkbox_PadsAutoName.Checked = True Then
            sb_Report.AppendLine("Renamed Pads:")
        End If

        For Each oPad As Pad In oPadStackDB.Pads

            Dim sShapeName As String

            Dim sNameBefore As String = oPad.Name

            If chkbox_PadsAutoName.Checked = True Then

                RaiseEvent eUpdateStatus("Auto naming pad: " & oPad.Name)

            End If

            Select Case oPad.Shape

                Case EPsDBPadShape.epsdbPadShapeRound
                    sShapeName = "Round"

                    Dim iPadHeight As Double = oPad.Height

                    ModifyPadUnit(oPad, sb_Report)

                    If chkbox_PadsAutoName.Checked = True Then
                        If oPad.AutoName = True Then
                            oPad.AutoName = False
                        End If
                        oPad.AutoName = True

                    End If

                    dicPads.Item(sShapeName & " " & iPadHeight & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY) = oPad.Name

                Case EPsDBPadShape.epsdbPadShapeOblong
                    sShapeName = "Oblong"

                    Dim iPadWidth As Double = oPad.Width
                    Dim iPadHeight As Double = oPad.Height

                    ModifyPadUnit(oPad, sb_Report)

                    If chkbox_PadsAutoName.Checked = True Then
                        If oPad.AutoName = True Then
                            oPad.AutoName = False
                        End If
                        oPad.AutoName = True
                    End If

                    dicPads.Item(sShapeName & " " & iPadHeight & " " & iPadWidth & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY) = oPad.Name

                Case EPsDBPadShape.epsdbPadShapeSquare
                    sShapeName = "Square"

                    Dim iPadHeight As Double = oPad.Height

                    ModifyPadUnit(oPad, sb_Report)

                    If chkbox_PadsAutoName.Checked = True Then
                        If oPad.AutoName = True Then
                            oPad.AutoName = False
                        End If
                        oPad.AutoName = True
                    End If

                    dicPads.Item(sShapeName & " " & iPadHeight & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY) = oPad.Name

                Case EPsDBPadShape.epsdbPadShapeOctagon
                    sShapeName = "Octagon"

                    Dim iPadHeight As Double = oPad.Height

                    ModifyPadUnit(oPad, sb_Report)

                    If chkbox_PadsAutoName.Checked = True Then
                        If oPad.AutoName = True Then
                            oPad.AutoName = False
                        End If
                        oPad.AutoName = True
                    End If

                    dicPads.Item(sShapeName & " " & iPadHeight & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY) = oPad.Name

                Case EPsDBPadShape.epsdbPadShapeRadiusCornerRectangle
                    sShapeName = "RadRectangle"
                    Dim iCornerHeight As Double = oPad.CornerHeight
                    Dim iPadWidth As Double = oPad.Width
                    Dim iPadHeight As Double = oPad.Height

                    ModifyPadUnit(oPad, sb_Report)

                    If chkbox_PadsAutoName.Checked = True Then
                        If oPad.AutoName = True Then
                            oPad.AutoName = False
                        End If
                        oPad.AutoName = True
                    End If

                    dicPads.Item(sShapeName & " " & iPadHeight & " " & iPadWidth & " " & iCornerHeight & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY) = oPad.Name

                Case EPsDBPadShape.epsdbPadShapeChamferedRectangle
                    sShapeName = "ChamfRectangle"

                    ModifyPadUnit(oPad, sb_Report)

                    Dim iCornerHeight As Double = oPad.CornerHeight
                    Dim iPadWidth As Double = oPad.Width
                    Dim iPadHeight As Double = oPad.Height

                    If chkbox_PadsAutoName.Checked = True Then
                        If oPad.AutoName = True Then
                            oPad.AutoName = False
                        End If
                        oPad.AutoName = True
                    End If

                    dicPads.Item(sShapeName & " " & iPadHeight & " " & iPadWidth & " " & iCornerHeight & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY) = oPad.Name

                Case EPsDBPadShape.epsdbPadShapeRectangle
                    sShapeName = "Rectangle"

                    Dim iPadWidth As Double = oPad.Width
                    Dim iPadHeight As Double = oPad.Height

                    ModifyPadUnit(oPad, sb_Report)

                    If chkbox_PadsAutoName.Checked = True Then
                        If oPad.AutoName = True Then
                            oPad.AutoName = False
                        End If
                        oPad.AutoName = True
                    End If

                    dicPads.Item(sShapeName & " " & iPadHeight & " " & iPadWidth & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY) = oPad.Name

                Case EPsDBPadShape.epsdbPadShapeOctagonalFinger
                    sShapeName = "OctFinger"

                    Dim iPadWidth As Double = oPad.Width
                    Dim iPadHeight As Double = oPad.Height

                    ModifyPadUnit(oPad, sb_Report)

                    If chkbox_PadsAutoName.Checked = True Then
                        If oPad.AutoName = True Then
                            oPad.AutoName = False
                        End If
                        oPad.AutoName = True
                    End If

                    dicPads.Item(sShapeName & " " & iPadHeight & " " & iPadWidth & " " & oPad.OriginOffsetX & " " & oPad.OriginOffsetY) = oPad.Name

                Case Else
                    If chkbox_PadsAutoName.Checked = True And Not oPad.Shape = EPsDBPadShape.epsdbPadShapeCustom Then

                        ModifyPadUnit(oPad, sb_Report)

                        If oPad.AutoName = True Then
                            oPad.AutoName = False
                        End If
                        oPad.AutoName = True
                    End If

            End Select

            If chkbox_PadsAutoName.Checked = True And Not oPad.Shape = EPsDBPadShape.epsdbPadShapeCustom Then

                If oPad.Name.Contains("_") Then
                    If Regex.IsMatch(oPad.Name.Substring(0, oPad.Name.IndexOf("_")), "[0-9]") = True Then
                        If Not sNameBefore = oPad.Name Then
                            sb_Report.AppendLine(vbTab & vbTab & "Renamed to: " & oPad.Name)
                        End If
                    Else
                        sb_Report.AppendLine(vbTab & vbTab & "ERROR: Unable to auto name pad " & sNameBefore & " because it would result in the invalid name of: " & oPad.Name & ".")
                        oPad.AutoName = False
                        If Not dic_BadPadNames.ContainsKey(sNameBefore) Then
                            dic_BadPadNames.Item(oPad.Name) = New List(Of String)
                        End If
                    End If
                Else
                    If Not sNameBefore = oPad.Name Then
                        sb_Report.AppendLine(vbTab & vbTab & "Renamed to: " & oPad.Name)
                    End If
                End If

            End If

        Next

        'If sType = "Mask" Then
        '    If sSide = "Top" Then
        '        oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = oPadStackDB.FindPad(sPadName)
        '    ElseIf sSide = "Bottom" Then
        '        oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = oPadStackDB.FindPad(sPadName)
        '    End If
        'Else
        '    If sSide = "Top" Then
        '        oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste) = oPadStackDB.FindPad(sPadName)
        '    ElseIf sSide = "Bottom" Then
        '        oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste) = oPadStackDB.FindPad(sPadName)
        '    End If
        'End If

        sb_Report.AppendLine()
        sb_Report.AppendLine("Updating pads in padstacks:")

        For Each oPadStack As Padstack In oPadStackDB.Padstacks

            Dim oMountPad As Pad = oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerTopMount)
            Dim oBottomPad As Pad = oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerBottomMount)
            Dim sShapeName As String

            RaiseEvent eUpdateStatus("Modfying padstack: " & oPadStack.Name)
            Dim bPrintName As Boolean = True

            If ((chkbox_AddTopSoldermask.Checked = True) Or (chkbox_AddBottomSoldermask.Checked = True)) And l_Filter.Contains(oPadStack.Type.ToString()) Then

                'Dim iGrowBy As String = cbox_Unit.Text

                If bPrintName = True Then
                    sb_Report.AppendLine(vbTab & oPadStack.Name)
                    bPrintName = False
                End If

                sb_Report.AppendLine(vbTab & vbTab & vbTab & "Soldermask:")

                If (chkbox_CopyMask.Checked = True) And chkbox_ModifyMask.Checked = False Then
                    If chkbox_AddTopSoldermask.Checked = True And Not IsNothing(oMountPad) Then

                        Dim CurrentPad As String = "None"

                        If Not IsNothing(oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerTopMountSoldermask)) Then
                            CurrentPad = oMountPad.Name
                        End If
                        sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Top - From: " & CurrentPad & ", To: " & oMountPad.Name)

                        oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = oMountPad

                    End If

                    If chkbox_AddBottomSoldermask.Checked = True And Not IsNothing(oBottomPad) Then

                        Dim CurrentPad As String = "None"

                        If Not IsNothing(oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask)) Then
                            CurrentPad = oBottomPad.Name
                        End If

                        sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Bottom - From: " & CurrentPad & ", To: " & oBottomPad.Name)

                        oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = oBottomPad

                    End If

                Else

                    If chkbox_AddTopSoldermask.Checked = True Then
                        Try

                            Dim oAPE_BuildPad As BuildPad = New BuildPad
                            oAPE_BuildPad.dicPads = dicPads
                            oAPE_BuildPad.oPadStack = oPadStack
                            oAPE_BuildPad.oPadStackDB = oPadStackDB
                            oAPE_BuildPad.GrowOrShrinkUnit = SolderMaskUnit
                            oAPE_BuildPad.iGrowOrShrinkNum = tbox_Soldermask.Text
                            oAPE_BuildPad.bGrow = SolderMaskGrow

                            If chkbox_CopyMask.Checked = True Then
                                oAPE_BuildPad.oPad = oMountPad
                            ElseIf IsNothing(oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask)) Then
                                sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Top - No Pad defined for Top Soldermask and Copy Top Pad is not checked.")
                                GoTo UnableToCompleteTopMask
                            Else
                                oAPE_BuildPad.oPad = oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask)
                            End If

                            Dim newPad As PadstackEditorLib.Pad

                            Select Case oAPE_BuildPad.oPad.Shape

                                Case EPsDBPadShape.epsdbPadShapeRound

                                    sShapeName = "Round"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOblong

                                    sShapeName = "Oblong"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeSquare

                                    sShapeName = "Square"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOctagon

                                    sShapeName = "Octagon"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeRadiusCornerRectangle

                                    sShapeName = "RadRectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeightCorner
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeChamferedRectangle

                                    sShapeName = "ChamfRectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeightCorner
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeRectangle

                                    sShapeName = "Rectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOctagonalFinger

                                    sShapeName = "OctFinger"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = newPad

                            End Select

                            If IsNothing(oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSoldermask)) Then
                                sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Adding Top - " & newPad.Name)
                            Else
                                If oAPE_BuildPad.NewPad = True Then
                                    sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Top - From: " & oAPE_BuildPad.oPad.Name & ", To New Pad: " & newPad.Name)
                                Else
                                    sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Top - From: " & oAPE_BuildPad.oPad.Name & ", To: " & newPad.Name)
                                End If
                            End If

                            oAPE_BuildPad = Nothing

                        Catch ex As Exception
                            sb_Report.AppendLine("ERROR: " & oPadStack.Name & " - " & ex.Message)
                            bErrors = True

                        End Try

UnableToCompleteTopMask:

                    End If

                    If chkbox_AddBottomSoldermask.Checked = True Then

                        Try

                            Dim oAPE_BuildPad As BuildPad = New BuildPad
                            oAPE_BuildPad.dicPads = dicPads
                            oAPE_BuildPad.oPadStack = oPadStack
                            oAPE_BuildPad.oPadStackDB = oPadStackDB
                            oAPE_BuildPad.GrowOrShrinkUnit = SolderMaskUnit
                            oAPE_BuildPad.iGrowOrShrinkNum = tbox_Soldermask.Text
                            oAPE_BuildPad.bGrow = SolderMaskGrow

                            If chkbox_CopyMask.Checked = True Then
                                oAPE_BuildPad.oPad = oBottomPad
                            ElseIf IsNothing(oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask)) Then
                                sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Bottom - No Pad defined for Bottom Soldermask and Copy Bottom Pad is not checked.")
                                GoTo UnableToCompleteBottomMask
                            Else
                                oAPE_BuildPad.oPad = oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask)
                            End If

                            Dim newPad As PadstackEditorLib.Pad

                            Select Case oAPE_BuildPad.oPad.Shape

                                Case EPsDBPadShape.epsdbPadShapeRound

                                    sShapeName = "Round"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOblong

                                    sShapeName = "Oblong"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeSquare

                                    sShapeName = "Square"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOctagon

                                    sShapeName = "Octagon"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeRadiusCornerRectangle

                                    sShapeName = "RadRectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeightCorner
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeChamferedRectangle

                                    sShapeName = "ChamfRectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeightCorner
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeRectangle

                                    sShapeName = "Rectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOctagonalFinger

                                    sShapeName = "OctFinger"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = newPad

                            End Select

                            If IsNothing(oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask)) Then
                                sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Adding Bottom - " & newPad.Name)
                            Else
                                If oAPE_BuildPad.NewPad = True Then
                                    sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Bottom - From: " & oAPE_BuildPad.oPad.Name & ", To New Pad: " & newPad.Name)
                                Else
                                    sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Bottom - From: " & oAPE_BuildPad.oPad.Name & ", To: " & newPad.Name)
                                End If
                            End If

                            oAPE_BuildPad = Nothing

                        Catch ex As Exception

                            sb_Report.AppendLine("ERROR: " & oPadStack.Name & " - " & ex.Message)
                            bErrors = True

                        End Try
UnableToCompleteBottomMask:

                    End If

                End If
                'oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerTopMountSoldermask) = oMountPad

                'If Not oBottomPad.Name = "(No Pad)" Then

                '    oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask) = oBottomPad

                'End If

            End If

            If ((chkbox_TopSolderPaste.Checked = True) Or (chkbox_BotSolderpaste.Checked = True)) And l_Filter.Contains(oPadStack.Type.ToString()) Then

                If bPrintName = True Then
                    sb_Report.AppendLine(vbTab & oPadStack.Name)
                    bPrintName = False
                End If

                sb_Report.AppendLine(vbTab & vbTab & vbTab & "Solderpaste:")

                If (chkbox_CopyPaste.Checked = True) And chkbox_ModifyPaste.Checked = False Then

                    If chkbox_TopSolderPaste.Checked = True And Not IsNothing(oMountPad) Then

                        Dim CurrentPad As String = "None"

                        If Not IsNothing(oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste)) Then
                            CurrentPad = oMountPad.Name
                        End If
                        sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Top - From: " & CurrentPad & ", To: " & oMountPad.Name)

                        oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste) = oMountPad

                    End If

                    If chkbox_BotSolderpaste.Checked = True And Not IsNothing(oBottomPad) Then

                        Dim CurrentPad As String = "None"

                        If Not IsNothing(oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste)) Then
                            CurrentPad = oBottomPad.Name
                        End If

                        sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Bottom - From: " & CurrentPad & ", To: " & oBottomPad.Name)

                        oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste) = oBottomPad

                    End If

                Else

                    If chkbox_TopSolderPaste.Checked = True Then

                        Try

                            Dim oAPE_BuildPad As BuildPad = New BuildPad
                            oAPE_BuildPad.dicPads = dicPads
                            oAPE_BuildPad.oPadStack = oPadStack
                            oAPE_BuildPad.oPadStackDB = oPadStackDB
                            oAPE_BuildPad.GrowOrShrinkUnit = SolderPasteUnit
                            oAPE_BuildPad.iGrowOrShrinkNum = tbox_Solderpaste.Text
                            oAPE_BuildPad.bGrow = SolderPasteGrow

                            If chkbox_CopyPaste.Checked = True Then
                                oAPE_BuildPad.oPad = oBottomPad
                            ElseIf IsNothing(oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste)) Then
                                sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Top - No Pad defined for Top Solderpaste and Copy Top Pad is not checked.")
                                GoTo UnableToCompleteTopPaste
                            Else
                                oAPE_BuildPad.oPad = oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste)
                            End If

                            Dim newPad As PadstackEditorLib.Pad

                            Select Case oAPE_BuildPad.oPad.Shape

                                Case EPsDBPadShape.epsdbPadShapeRound

                                    sShapeName = "Round"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOblong

                                    sShapeName = "Oblong"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeSquare

                                    sShapeName = "Square"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOctagon

                                    sShapeName = "Octagon"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeRadiusCornerRectangle

                                    sShapeName = "RadRectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeightCorner
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeChamferedRectangle

                                    sShapeName = "ChamfRectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeightCorner
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeRectangle

                                    sShapeName = "Rectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOctagonalFinger

                                    sShapeName = "OctFinger"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste) = newPad

                            End Select

                            If IsNothing(oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste)) Then
                                sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Adding Top - " & newPad.Name)
                            Else
                                If oAPE_BuildPad.NewPad = True Then
                                    sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Top - From: " & oAPE_BuildPad.oPad.Name & ", To New Pad: " & newPad.Name)
                                Else
                                    sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Top - From: " & oAPE_BuildPad.oPad.Name & ", To: " & newPad.Name)
                                End If
                            End If

                            oAPE_BuildPad = Nothing

                        Catch ex As Exception
                            sb_Report.AppendLine("ERROR: " & oPadStack.Name & " - " & ex.Message)
                            bErrors = True

                        End Try
UnableToCompleteTopPaste:

                    End If

                    If chkbox_BotSolderpaste.Checked = True Then

                        Try

                            Dim oAPE_BuildPad As BuildPad = New BuildPad
                            oAPE_BuildPad.dicPads = dicPads
                            oAPE_BuildPad.oPadStack = oPadStack
                            oAPE_BuildPad.oPadStackDB = oPadStackDB
                            oAPE_BuildPad.GrowOrShrinkUnit = SolderPasteUnit
                            oAPE_BuildPad.iGrowOrShrinkNum = tbox_Solderpaste.Text
                            oAPE_BuildPad.bGrow = SolderPasteGrow

                            If IsNothing(oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste)) Then
                                oAPE_BuildPad.oPad = oBottomPad
                            ElseIf IsNothing(oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste)) Then
                                sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Bottom - No Pad defined for Bottom Solderpaste and Copy Bottom Pad is not checked.")
                                GoTo UnableToCompleteBottomPaste
                            Else
                                oAPE_BuildPad.oPad = oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste)
                            End If

                            Dim newPad As PadstackEditorLib.Pad

                            Select Case oAPE_BuildPad.oPad.Shape

                                Case EPsDBPadShape.epsdbPadShapeRound

                                    sShapeName = "Round"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOblong

                                    sShapeName = "Oblong"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeSquare

                                    sShapeName = "Square"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOctagon

                                    sShapeName = "Octagon"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadHeight
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeRadiusCornerRectangle

                                    sShapeName = "RadRectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeightCorner
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeChamferedRectangle

                                    sShapeName = "ChamfRectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeightCorner
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeRectangle

                                    sShapeName = "Rectangle"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste) = newPad

                                Case EPsDBPadShape.epsdbPadShapeOctagonalFinger

                                    sShapeName = "OctFinger"
                                    oAPE_BuildPad.sShapeName = sShapeName

                                    newPad = oAPE_BuildPad.PadWidthHeight()
                                    oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste) = newPad

                            End Select

                            If IsNothing(oPadStack.Pad(PadstackEditorLib.EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste)) Then
                                sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Adding Bottom - " & newPad.Name)
                            Else
                                If oAPE_BuildPad.NewPad = True Then
                                    sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Bottom - From: " & oAPE_BuildPad.oPad.Name & ", To New Pad: " & newPad.Name)
                                Else
                                    sb_Report.AppendLine(vbTab & vbTab & vbTab & vbTab & "Bottom - From: " & oAPE_BuildPad.oPad.Name & ", To: " & newPad.Name)
                                End If
                            End If

                            oAPE_BuildPad = Nothing

                        Catch ex As Exception
                            sb_Report.AppendLine("ERROR: " & oPadStack.Name & " - " & ex.Message)
                            bErrors = True

                        End Try
UnableToCompleteBottomPaste:

                    End If

                End If

            End If

            If chkbox_PadsAutoName.Checked = True Then

                RaiseEvent eUpdateStatus("Updating pads in padstack: " & oPadStack.Name)

                Dim printPadHeader As Boolean = True

                For count As Integer = 0 To 7

                    Dim pdstkPad As Pad
                    Dim pdstkPadLayer As EPsDBPadLayer
                    Dim sType As String

                    Select Case count

                        Case 0
                            pdstkPad = oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerTopMountSoldermask)
                            pdstkPadLayer = EPsDBPadLayer.epsdbPadLayerTopMountSoldermask
                            sType = "Top Soldermask"
                        Case 1
                            pdstkPad = oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask)
                            pdstkPadLayer = EPsDBPadLayer.epsdbPadLayerBottomMountSoldermask
                            sType = "Bottom Soldermask"
                        Case 2
                            pdstkPad = oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste)
                            pdstkPadLayer = EPsDBPadLayer.epsdbPadLayerTopMountSolderPaste
                            sType = "Top Solderpaste"
                        Case 3
                            pdstkPad = oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste)
                            pdstkPadLayer = EPsDBPadLayer.epsdbPadLayerBottomMountSolderPaste
                            sType = "Bottom Solderpaste"
                        Case 4
                            pdstkPad = oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerPlaneThermal)
                            pdstkPadLayer = EPsDBPadLayer.epsdbPadLayerPlaneThermal
                            sType = "Thermal"
                        Case 5
                            pdstkPad = oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerInternal)
                            pdstkPadLayer = EPsDBPadLayer.epsdbPadLayerInternal
                            sType = "Internal"
                        Case 6
                            pdstkPad = oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerTopMount)
                            pdstkPadLayer = EPsDBPadLayer.epsdbPadLayerTopMount
                            sType = "Top"
                        Case 7
                            pdstkPad = oPadStack.Pad(EPsDBPadLayer.epsdbPadLayerBottomMount)
                            pdstkPadLayer = EPsDBPadLayer.epsdbPadLayerBottomMount
                            sType = "Bottom"
                    End Select

                    If IsNothing(pdstkPad) Then
                        Continue For
                    End If

                    If dic_BadPadNames.ContainsKey(pdstkPad.Name) Then

                        If Not dic_BadPadNames.Item(pdstkPad.Name).Contains(oPadStack.Name) Then
                            dic_BadPadNames.Item(pdstkPad.Name).Add(oPadStack.Name)
                        End If

                        Continue For

                    End If

                    If pdstkPad.AutoName = True And Not pdstkPad.Shape = EPsDBPadShape.epsdbPadShapeCustom And pdstkPad.Name.Contains("_") Then

                        Dim sPadstackName As String = pdstkPad.Name.Substring(0, pdstkPad.Name.IndexOf("_") - 1)

                        If bPrintName = True Then
                            sb_Report.AppendLine(vbTab & oPadStack.Name)
                            bPrintName = False
                        End If

                        If printPadHeader = True Then
                            sb_Report.AppendLine(vbTab & vbTab & "Pads:")
                            printPadHeader = False
                        End If

                        sb_Report.AppendLine(vbTab & vbTab & vbTab & sType & " - From: " & pdstkPad.Name & ", To: " & sPadstackName)

                        oPadStack.Pad(pdstkPadLayer) = oPadStackDB.FindPad(sPadstackName.Trim)

                    End If

                Next

            End If

            If chkbox_HoleAutoName.Checked = True And oPadStack.Holes.Count > 0 Then

                RaiseEvent eUpdateStatus("Updating holes in padstack: " & oPadStack.Name)

                If dic_BadHoleNames.ContainsKey(oPadStack.Holes.Item(1).Name) Then

                    If Not dic_BadHoleNames.Item(oPadStack.Holes.Item(1).Name).Contains(oPadStack.Name) Then
                        dic_BadHoleNames.Item(oPadStack.Holes.Item(1).Name).Add(oPadStack.Name)
                    End If

                    Continue For

                End If

                If oPadStack.Holes.Item(1).AutoName = True And oPadStack.Holes.Item(1).Name.Contains("_") Then

                    Dim sHoleName As String = oPadStack.Holes.Item(1).Name.Substring(0, oPadStack.Holes.Item(1).Name.IndexOf("_") - 1)

                    If bPrintName = True Then
                        sb_Report.AppendLine(vbTab & oPadStack.Name)
                        bPrintName = False
                    End If

                    sb_Report.AppendLine(vbTab & vbTab & "Holes:")

                    sb_Report.AppendLine(vbTab & vbTab & vbTab & "From: " & oPadStack.Holes.Item(1).Name & ", To: " & sHoleName)

                    oPadStack.Hole = oPadStackDB.FindHole(sHoleName.Trim)

                End If

            End If

        Next

        Dim bPrintHeader As Boolean = True

        Dim sb_UnableToDelete As New StringBuilder

        For Each oPad As Pad In oPadStackDB.Pads

            If oPad.AutoName = True And Not oPad.Shape = EPsDBPadShape.epsdbPadShapeCustom Then

                If oPad.Name.Contains("_") Then

                    RaiseEvent eUpdateStatus("Removing duplicate pad: " & oPad.Name)
                    If bPrintHeader = True Then
                        sb_Report.AppendLine()
                        sb_Report.AppendLine("Removing the following duplicate pads:")
                        bPrintHeader = False
                    End If

                    Dim sNameBefore As String = oPad.Name

                    Try
                        oPad.Delete()
                        sb_Report.AppendLine(vbTab & sNameBefore)
                    Catch ex As Exception
                        sb_UnableToDelete.AppendLine(vbTab & "ERROR: " & oPad.Name)
                        bWarnings = True
                    End Try

                End If

            End If

        Next

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Padstack Editor.log", True, System.Text.Encoding.ASCII)

            If dic_BadPadNames.Count > 0 Then
                writer.WriteLine("Auto naming the following pads caused invalid names:")
                For Each kvp As KeyValuePair(Of String, List(Of String)) In dic_BadPadNames
                    writer.WriteLine(vbTab & kvp.Key)
                    writer.WriteLine(vbTab & vbTab & "Associated padstacks:")
                    If kvp.Value.Count = 0 Then
                        writer.WriteLine(vbTab & vbTab & vbTab & "None")
                        Continue For
                    End If
                    For Each padstack As String In kvp.Value
                        writer.WriteLine(vbTab & vbTab & vbTab & padstack)
                    Next
                Next
                writer.WriteLine()
            End If

            If dic_BadHoleNames.Count > 0 Then
                writer.WriteLine("Auto naming the following holes caused invalid names:")
                For Each kvp As KeyValuePair(Of String, List(Of String)) In dic_BadHoleNames
                    writer.WriteLine(vbTab & kvp.Key)
                    writer.WriteLine(vbTab & vbTab & "Associated padstacks:")
                    If kvp.Value.Count = 0 Then
                        writer.WriteLine(vbTab & vbTab & vbTab & "None")
                        Continue For
                    End If
                    For Each padstack As String In kvp.Value
                        writer.WriteLine(vbTab & vbTab & vbTab & padstack)
                    Next
                Next
                writer.WriteLine()
            End If

            writer.Write(sb_Report.ToString)

            writer.WriteLine()

            writer.WriteLine("Unable to remove the following duplicate pads because they are still being used:")
            writer.WriteLine(sb_UnableToDelete.ToString)

        End Using

        oPadStackDB = Nothing
        Try
            PadStackEdDlg.SaveActiveDatabase()
        Catch ex As Exception

            bErrors = True

        End Try

        PadStackEdDlg.Quit()

        RaiseEvent eUpdateComplete()

    End Sub

    Private Sub UpdateComplete()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateComplete(AddressOf UpdateComplete)
            Me.Invoke(d)
        Else

            WaitGif.Enabled = False

            If bErrors = True Or bWarnings = True Or dic_BadHoleNames.Count > 0 Or dic_BadPadNames.Count > 0 Then
                ts_Status.Text = "Update completed with errors..."
                Dim reply As DialogResult = MessageBox.Show("Padstack Editor process completed, but there were errors or warnings. Would you like to view the results?", "Finished", _
                  MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Padstack Editor")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Padstack Editor.log")
                End If

            Else
                ts_Status.Text = "Update complete..."
                Dim reply As DialogResult = MessageBox.Show("Padstack Editor completed with no errors or warnings. Would you like to view the results?", "Finished", _
  MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Padstack Editor")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Padstack Editor.log")
                End If

            End If

        End If
    End Sub

    Private Sub chkbox_ModifyMask_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_ModifyMask.CheckedChanged

        If chkbox_ModifyMask.Checked = True Then
            tbox_Soldermask.Enabled = True
            cbox_Mask_GrowShrink.Enabled = True
            cbox_UnitSolderMask.Enabled = True
        Else
            tbox_Soldermask.Clear()
            cbox_Mask_GrowShrink.SelectedIndex = -1
            cbox_UnitSolderMask.SelectedIndex = -1
            tbox_Soldermask.Enabled = False
            cbox_Mask_GrowShrink.Enabled = False
            cbox_UnitSolderMask.Enabled = False
        End If

    End Sub

    Private Sub chkbox_ModifyPaste_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_ModifyPaste.CheckedChanged
        If chkbox_ModifyPaste.Checked = True Then
            tbox_Solderpaste.Enabled = True
            cbox_Paste_GrowShrink.Enabled = True
            cbox_UnitSolderPaste.Enabled = True
        Else
            tbox_Solderpaste.Clear()
            cbox_Paste_GrowShrink.SelectedIndex = -1
            cbox_UnitSolderPaste.SelectedIndex = -1
            tbox_Solderpaste.Enabled = False
            cbox_Paste_GrowShrink.Enabled = False
            cbox_UnitSolderPaste.Enabled = False
        End If
    End Sub

    Private Sub tbox_Soldermask_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbox_Soldermask.TextChanged

        If tbox_Soldermask.TextLength = 0 Then
            cbox_Mask_GrowShrink.Enabled = False
            cbox_UnitSolderMask.Enabled = False
        Else
            cbox_Mask_GrowShrink.Enabled = True
            cbox_UnitSolderMask.Enabled = True
        End If

    End Sub

    Private Sub tbox_Solderpaste_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbox_Solderpaste.TextChanged
        If tbox_Solderpaste.TextLength = 0 Then
            cbox_Paste_GrowShrink.Enabled = False
            cbox_UnitSolderPaste.Enabled = False
        Else
            cbox_Paste_GrowShrink.Enabled = True
            cbox_UnitSolderPaste.Enabled = True
        End If
    End Sub

    Private Sub chkbox_ModifyUnits_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_ModifyUnits.CheckedChanged

        If chkbox_ModifyUnits.Checked = True Then
            cbox_ChangeUnit.Enabled = True
            cbox_ChangeUnit.SelectedIndex = 0
        Else
            cbox_ChangeUnit.Enabled = False
            cbox_ChangeUnit.SelectedIndex = -1
        End If

    End Sub

    Private Sub chkbox_HoleAutoName_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_HoleAutoName.CheckedChanged

        If chkbox_HoleAutoName.Checked = False And chkbox_PadsAutoName.Checked = False Then
            chkbox_ModifyUnits.Checked = False
            chkbox_ModifyUnits.Enabled = False
        Else
            chkbox_ModifyUnits.Enabled = True
        End If

    End Sub

    Private Sub chkbox_PadsAutoName_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_PadsAutoName.CheckedChanged
        If chkbox_HoleAutoName.Checked = False And chkbox_PadsAutoName.Checked = False Then
            chkbox_ModifyUnits.Checked = False
            chkbox_ModifyUnits.Enabled = False
        Else
            chkbox_ModifyUnits.Enabled = True
        End If
    End Sub

    Private Sub ModifyPadUnit(ByRef oPad As Pad, ByRef sb_Report As StringBuilder)
        If chkbox_ModifyUnits.Checked = True Then

            RaiseEvent eUpdateStatus("Changing unit for pad: " & oPad.Name)

            sb_Report.AppendLine(vbTab & oPad.Name & ":")

            Select Case oPad.Units

                Case EPsDBUnit.epsdbUnitInch
                    sb_Report.AppendLine(vbTab & vbTab & "Unit Before: in, Unit After: " & ModifyUnit)
                Case EPsDBUnit.epsdbUnitMils
                    sb_Report.AppendLine(vbTab & vbTab & "Unit Before: th, Unit After: " & ModifyUnit)
                Case EPsDBUnit.epsdbUnitMM
                    sb_Report.AppendLine(vbTab & vbTab & "Unit Before: mm, Unit After: " & ModifyUnit)
                Case EPsDBUnit.epsdbUnitUM
                    sb_Report.AppendLine(vbTab & vbTab & "Unit Before: um, Unit After: " & ModifyUnit)
            End Select

            Select Case ModifyUnit

                Case "inch"
                    oPad.Units = EPsDBUnit.epsdbUnitInch
                Case "th"
                    oPad.Units = EPsDBUnit.epsdbUnitMils
                Case "mm"
                    oPad.Units = EPsDBUnit.epsdbUnitMM
                Case "um"
                    oPad.Units = EPsDBUnit.epsdbUnitUM
            End Select

        End If
    End Sub

End Class