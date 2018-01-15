Imports System.Drawing.Text
Imports System.ComponentModel
Imports System.Drawing

Public Class frmUpdateCellFonts

    'Events
    Event e_UpdateComplete()
    Event e_UpdateStatus(ByVal s_Status As String)

    'Thread
    Dim t As Threading.Thread

    'Objects
    'Dim cellEd As CellEditorAddinLib.CellEditorDlg
    'Dim cellDB As CellEditorAddinLib.CellDB
    Dim oCellDoc As MGCPCB.Document
    Dim dicTextTypes As New Dictionary(Of String, Integer)
    Dim dicHeightTypes As New Dictionary(Of String, Integer)
    Dim alVectorFonts As New ArrayList
    Dim iTextType As Integer

    ' Form info variables
    Dim iOrientation As Integer
    Dim bOrientation As Boolean
    Dim bBold As Boolean
    Dim bItalic As Boolean
    Dim bUnderline As Boolean
    Dim bMirrored As Boolean
    Dim iHeight As Integer
    Dim iHeightType As Integer
    Dim dAspectRatio As Double
    Dim bUpdateSilkscreenOnly As Boolean

    'String
    Dim s_Font As String

    'Delegate
    Delegate Sub d_UpdateComplete()
    'Delegate Sub d_UpdateStatus(ByVal s_Status As String)

    Private Sub btnUpdateFont_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateFont.Click

        ts_WaitGif.Enabled = True

        ts_Status.Text = "Updating Cells..."

        s_Font = cbox_FontType.Text

        ' Get data from form
        Dim bValidFormData As Boolean = GetFormData()

        If bValidFormData Then
            AddHandler e_UpdateComplete, AddressOf UpdateComplete
            t = New Threading.Thread(AddressOf UpdateCells)
            t.IsBackground = True
            t.Start()
        End If

    End Sub

    Private Sub GetVectorFonts()
        alVectorFonts.Add("std-proportional")
        alVectorFonts.Add("vf_fixed")
        alVectorFonts.Add("vf_gothic")
        alVectorFonts.Add("vf_iges1003")
        alVectorFonts.Add("vf_iso")
        alVectorFonts.Add("vf_iso88591-m")
        alVectorFonts.Add("vf_iso88591-p")
        alVectorFonts.Add("vf_leroy")
        alVectorFonts.Add("vf_microfilm")
        alVectorFonts.Add("vf_mil_std")
        alVectorFonts.Add("vf_std")
    End Sub

    Private Function GetFormData() As Boolean

        ' Get the style
        bBold = chbBold.Checked
        bItalic = chbItalic.Checked
        bUnderline = chbUnderline.Checked
        bMirrored = chbMirrored.Checked

        ' Get the height
        If tbHeight.Text = "" Then
            iHeight = 0
        Else
            Try
                iHeight = Integer.Parse(tbHeight.Text)
            Catch ex As Exception
                MsgBox("Please enter a valid numerical height.")
                Return False
            End Try
        End If

        'Get Height Type
        iHeightType = dicHeightTypes.Item(cbox_HeightUnits.Text)

        ' Get aspect ratio
        Try
            dAspectRatio = Double.Parse(tbAspect.Text)
        Catch ex As Exception
            MsgBox("Please enter a valid numerical aspect ratio. If you are unsure, leave it as 1.")
            Return False
        End Try

        ' Get all texts VS silkscreen only
        bUpdateSilkscreenOnly = chkbox_Silkscreen.Checked

        ' Get the orientation
        Try
            iOrientation = Integer.Parse(cbOrientation.SelectedItem)
            bOrientation = True
        Catch ex As Exception
            bOrientation = False
        End Try

        ' Get the text type
        iTextType = dicTextTypes(cbTextType.Text)

        Return True

    End Function
    Private Sub frmUpdateCellFonts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        ' Create collections
        GetVectorFonts()
        PopulateDictionaries()
        cbTextType.SelectedIndex = 0
        cbox_HeightUnits.SelectedIndex = 0

        Dim installed_fonts As New InstalledFontCollection
        Dim font_families() As FontFamily = installed_fonts.Families

        For Each font_family As FontFamily In font_families

            cbox_FontType.Items.Add(font_family.Name)

        Next

        For Each item In alVectorFonts
            cbox_FontType.Items.Add(item)
        Next

        For Each sPartition As String In frmMain.librarydata.CellsByPartition.Keys
            chklisbox_Partitions.Items.Add(sPartition)
            chklisbox_Partitions.Sorted = True
        Next

    End Sub

    Private Sub cbox_FontType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cbox_FontType.SelectedIndexChanged

        Try
            Dim ofont As New System.Drawing.Font(cbox_FontType.Text, 14)
            lblExample.Font = ofont
        Catch ex As Exception

        End Try

        If alVectorFonts.Contains(cbox_FontType.Text) Then
            tbAspect.Enabled = True
        Else
            tbAspect.Text = "1"
            tbAspect.Enabled = False
        End If

    End Sub

    Private Sub PopulateDictionaries()
        dicTextTypes("All") = -1
        dicTextTypes("Generic") = 2
        dicTextTypes("Part Number") = 4
        dicTextTypes("Part Property") = 64
        dicTextTypes("Pin Logical") = 16
        dicTextTypes("Pin Physical") = 32
        dicTextTypes("Pin User Defined") = 8
        dicTextTypes("Ref Des") = 1

        dicHeightTypes("in") = 3
        dicHeightTypes("th") = 2
        dicHeightTypes("um") = 5
        dicHeightTypes("mm") = 4

    End Sub

    Private Sub UpdateCells()

        ' Get the partitions in the cell database
        For Each sPartition As String In chklisbox_Partitions.CheckedItems

            Dim cellEd As CellEditorAddinLib.CellEditorDlg = frmMain.libDoc.CellEditor
            Dim cellDB As CellEditorAddinLib.CellDB = cellEd.ActiveDatabase

            Dim cePartition As CellEditorAddinLib.Partition = Nothing

            For Each oCellPartition As CellEditorAddinLib.Partition In cellDB.Partitions()

                If oCellPartition.Name = sPartition Then
                    cePartition = oCellPartition
                End If

            Next

            If Not IsNothing(cePartition) Then

                For Each oCell As CellEditorAddinLib.Cell In cePartition.Cells  ' Process each cell in the partition
                    Raise(e_UpdateStatusEvent, DirectCast("Updating: " & oCell.Name, String))

                    Dim oFabTextsColl As MGCPCB.FabricationLayerTexts
                    Dim oFabText As MGCPCB.FabricationLayerText
                    Dim oUserTextsColl As MGCPCB.UserLayerTexts
                    Dim oUserText As MGCPCB.UserLayerText
                    Dim oConductiveTextColl As MGCPCB.ConductorLayerTexts
                    Dim oConductiveText As MGCPCB.ConductorLayerText

                    Dim bOpenedCellEditor As Boolean

                    Try
                        oCellDoc = oCell.Edit()     ' Edit invokes Cell Editor, returning an AutoActive document
                        bOpenedCellEditor = True
                    Catch ex As Exception
                        bOpenedCellEditor = False
                    End Try

                    ' If the cell editor opened, continue
                    If bOpenedCellEditor = True Then

                        Dim oCellEditorApp As MGCPCB.IMGCPCBApplication
                        oCellEditorApp = Nothing
                        oCellEditorApp = oCellDoc.Application

                        ' These texts only get changed if the user wants to update all
                        If bUpdateSilkscreenOnly = False Then

                            ' PROCESS USER TEXTS
                            oUserTextsColl = oCellDoc.UserLayerTexts
                            For Each oUserText In oUserTextsColl

                                ' If the text type matches the type that the user selected, then update the formatting
                                If iTextType = oUserText.TextType Or iTextType = -1 Then

                                    If s_Font <> "" Then
                                        oUserText.Format.Font = s_Font
                                    End If

                                    ' Update Style
                                    oUserText.Format.Bold = bBold
                                    oUserText.Format.Italic = bItalic
                                    oUserText.Format.Underline = bUnderline
                                    oUserText.Format.Mirrored = bMirrored

                                    ' Update Height
                                    If Not iHeight = 0 Then
                                        Dim oUnit As MGCPCB.EPcbUnit = iHeightType
                                        oUserText.Format.Height(oUnit) = iHeight
                                    End If

                                    ' Update Orientation
                                    If bOrientation Then
                                        oUserText.Format.Orientation = iOrientation
                                    End If

                                    ' Update aspect ratio
                                    oUserText.Format.AspectRatio = dAspectRatio

                                End If
                            Next

                            ' PROCESS CONDUCTIVE TEXTS
                            oConductiveTextColl = oCellDoc.ConductorLayerTexts
                            For Each oConductiveText In oConductiveTextColl

                                ' If the text type matches the type that the user selected, then update the formatting
                                If iTextType = oConductiveText.TextType Or iTextType = -1 Then

                                    If s_Font <> "" Then
                                        oConductiveText.Format.Font = s_Font
                                    End If

                                    ' Update Style
                                    oConductiveText.Format.Bold = bBold
                                    oConductiveText.Format.Italic = bItalic
                                    oConductiveText.Format.Underline = bUnderline
                                    oConductiveText.Format.Mirrored = bMirrored

                                    ' Update Height
                                    If Not iHeight = 0 Then
                                        Dim oUnit As MGCPCB.EPcbUnit = iHeightType
                                        oConductiveText.Format.Height(oUnit) = iHeight
                                    End If

                                    ' Update Orientation
                                    If bOrientation Then
                                        oConductiveText.Format.Orientation = iOrientation
                                    End If

                                    ' Update aspect ratio
                                    oConductiveText.Format.AspectRatio = dAspectRatio

                                End If

                            Next
                        End If

                        ' PROCESS FABRICATION TEXTS
                        ' These texts get changed regardless of which option the user selects
                        oFabTextsColl = oCellDoc.FabricationLayerTexts
                        For Each oFabText In oFabTextsColl

                            If (chkbox_Silkscreen.Checked = True And Not oFabText.Type = MGCPCB.EPcbFabricationType.epcbFabSilkscreen) Then
                                Continue For
                            End If

                            ' If the text type matches the type that the user selected, then update the formatting
                            'iCurTextType = oFabText.TextType
                            If Not iTextType = -1 Then
                                If Not iTextType = oFabText.TextType Then
                                    Continue For
                                End If
                            End If

                            If Not String.IsNullOrEmpty(s_Font) Then
                                oFabText.Format.Font = s_Font
                            End If

                            ' Update Style
                            oFabText.Format.Bold = bBold
                            oFabText.Format.Italic = bItalic
                            oFabText.Format.Underline = bUnderline
                            oFabText.Format.Mirrored = bMirrored

                            ' Update Height
                            If Not iHeight = 0 Then
                                Dim oUnit As MGCPCB.EPcbUnit = iHeightType
                                oFabText.Format.Height(oUnit) = iHeight
                            End If

                            ' Update Orientation
                            If bOrientation Then
                                oFabText.Format.Orientation = iOrientation
                            End If

                            ' Update aspect ratio
                            oFabText.Format.AspectRatio = dAspectRatio

                        Next

                        ' Close and save the cell doc
                        oCellDoc.Close(True)
                    End If

                Next
            End If

            Raise(e_UpdateStatusEvent, DirectCast("Saving cell partition...", String))

            cellEd.SaveActiveDatabase()
            cellEd.Quit()
            cellEd = Nothing
            cellDB = Nothing

            Raise(e_UpdateStatusEvent, DirectCast("Opening a new instance of cell editor...", String))

            cellEd = frmMain.libDoc.CellEditor
            cellDB = cellEd.ActiveDatabase

        Next

        RaiseEvent e_UpdateComplete()

    End Sub

    Private Sub UpdateComplete()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateComplete(AddressOf UpdateComplete)
            Me.Invoke(d)

        Else

            ts_Status.Text = "Update complete..."
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Process Complete", "Modify Cell Text Style", ToolTipIcon.None)
            ts_WaitGif.Enabled = False

        End If
    End Sub

    Private Sub UpdateStatus(ByVal s_Status As String) Handles Me.e_UpdateStatus
        ts_Status.Text = s_Status
    End Sub

    Sub Raise(ByVal [event] As [Delegate], ByVal ParamArray data As Object())
        'If the event has no handlers just exit the method call.
        If [event] Is Nothing Then Return

        'Enumerates through the list of handlers.
        For Each D As [Delegate] In [event].GetInvocationList()
            'Casts the handler's parent instance to ISynchronizeInvoke.
            Dim T As ISynchronizeInvoke = DirectCast(D.Target, ISynchronizeInvoke)

            'If an invoke is required (working on a seperate thread) then invoke it
            'on the parent thread, otherwise we can invoke it directly.
            If T.InvokeRequired Then T.Invoke(D, data) Else D.DynamicInvoke(data)
        Next
    End Sub

    Private Sub tbHeight_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbHeight.TextChanged

        If String.IsNullOrEmpty(tbHeight.Text) Then
            cbox_HeightUnits.Enabled = False
        Else
            cbox_HeightUnits.Enabled = True
        End If

    End Sub

    Private Sub chklisbox_Partitions_ItemCheck(sender As System.Object, e As System.Windows.Forms.ItemCheckEventArgs) Handles chklisbox_Partitions.ItemCheck

        If e.NewValue = CheckState.Checked Then
            If chklisbox_Partitions.CheckedItems.Count >= 0 Then
                btnUpdateFont.Enabled = True
            End If
        Else
            If chklisbox_Partitions.CheckedItems.Count = 1 Then
                btnUpdateFont.Enabled = False
            End If
        End If
    End Sub

    Private Sub cbTextType_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbTextType.SelectedIndexChanged

        If cbTextType.Text = "All" Or cbTextType.Text = "Generic" Or cbTextType.Text = "Part Number" Or cbTextType.Text = "Ref Des" Then
            chkbox_Silkscreen.Enabled = True
        Else
            chkbox_Silkscreen.Checked = False
            chkbox_Silkscreen.Enabled = False
        End If

    End Sub
End Class