<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPadstackEditor
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btn_Modify = New System.Windows.Forms.Button()
        Me.chkbox_HoleAutoName = New System.Windows.Forms.CheckBox()
        Me.cbox_UnitSolderPaste = New System.Windows.Forms.ComboBox()
        Me.tbox_Solderpaste = New System.Windows.Forms.TextBox()
        Me.chkbox_BotSolderpaste = New System.Windows.Forms.CheckBox()
        Me.chkbox_TopSolderPaste = New System.Windows.Forms.CheckBox()
        Me.cbox_Paste_GrowShrink = New System.Windows.Forms.ComboBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkbox_AddTopSoldermask = New System.Windows.Forms.CheckBox()
        Me.gb_MaskModify = New System.Windows.Forms.GroupBox()
        Me.chkbox_ModifyMask = New System.Windows.Forms.CheckBox()
        Me.chkbox_CopyMask = New System.Windows.Forms.CheckBox()
        Me.tbox_Soldermask = New System.Windows.Forms.TextBox()
        Me.cbox_Mask_GrowShrink = New System.Windows.Forms.ComboBox()
        Me.cbox_UnitSolderMask = New System.Windows.Forms.ComboBox()
        Me.chkbox_AddBottomSoldermask = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.gb_PasteModify = New System.Windows.Forms.GroupBox()
        Me.chkbox_ModifyPaste = New System.Windows.Forms.CheckBox()
        Me.chkbox_CopyPaste = New System.Windows.Forms.CheckBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.chkbox_ModifyUnits = New System.Windows.Forms.CheckBox()
        Me.cbox_ChangeUnit = New System.Windows.Forms.ComboBox()
        Me.chkbox_PadsAutoName = New System.Windows.Forms.CheckBox()
        Me.chklistbox_Filter = New System.Windows.Forms.CheckedListBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.GroupBox6 = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.GroupBox3.SuspendLayout()
        Me.gb_MaskModify.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.gb_PasteModify.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox6.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_Modify
        '
        Me.btn_Modify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Modify.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Modify.Location = New System.Drawing.Point(21, 354)
        Me.btn_Modify.Name = "btn_Modify"
        Me.btn_Modify.Size = New System.Drawing.Size(85, 31)
        Me.btn_Modify.TabIndex = 2
        Me.btn_Modify.Text = "Modify"
        Me.btn_Modify.UseVisualStyleBackColor = False
        '
        'chkbox_HoleAutoName
        '
        Me.chkbox_HoleAutoName.AutoSize = True
        Me.chkbox_HoleAutoName.Location = New System.Drawing.Point(12, 23)
        Me.chkbox_HoleAutoName.Name = "chkbox_HoleAutoName"
        Me.chkbox_HoleAutoName.Size = New System.Drawing.Size(53, 17)
        Me.chkbox_HoleAutoName.TabIndex = 3
        Me.chkbox_HoleAutoName.Text = "Holes"
        Me.chkbox_HoleAutoName.UseVisualStyleBackColor = True
        '
        'cbox_UnitSolderPaste
        '
        Me.cbox_UnitSolderPaste.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_UnitSolderPaste.Enabled = False
        Me.cbox_UnitSolderPaste.FormattingEnabled = True
        Me.cbox_UnitSolderPaste.Location = New System.Drawing.Point(77, 65)
        Me.cbox_UnitSolderPaste.Name = "cbox_UnitSolderPaste"
        Me.cbox_UnitSolderPaste.Size = New System.Drawing.Size(48, 21)
        Me.cbox_UnitSolderPaste.TabIndex = 8
        '
        'tbox_Solderpaste
        '
        Me.tbox_Solderpaste.Enabled = False
        Me.tbox_Solderpaste.Location = New System.Drawing.Point(9, 65)
        Me.tbox_Solderpaste.Name = "tbox_Solderpaste"
        Me.tbox_Solderpaste.Size = New System.Drawing.Size(62, 20)
        Me.tbox_Solderpaste.TabIndex = 7
        Me.tbox_Solderpaste.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'chkbox_BotSolderpaste
        '
        Me.chkbox_BotSolderpaste.AutoSize = True
        Me.chkbox_BotSolderpaste.Location = New System.Drawing.Point(12, 42)
        Me.chkbox_BotSolderpaste.Name = "chkbox_BotSolderpaste"
        Me.chkbox_BotSolderpaste.Size = New System.Drawing.Size(117, 17)
        Me.chkbox_BotSolderpaste.TabIndex = 10
        Me.chkbox_BotSolderpaste.Text = "Add/Modify Bottom"
        Me.chkbox_BotSolderpaste.UseVisualStyleBackColor = True
        '
        'chkbox_TopSolderPaste
        '
        Me.chkbox_TopSolderPaste.AutoSize = True
        Me.chkbox_TopSolderPaste.Location = New System.Drawing.Point(12, 19)
        Me.chkbox_TopSolderPaste.Name = "chkbox_TopSolderPaste"
        Me.chkbox_TopSolderPaste.Size = New System.Drawing.Size(103, 17)
        Me.chkbox_TopSolderPaste.TabIndex = 9
        Me.chkbox_TopSolderPaste.Text = "Add/Modify Top"
        Me.chkbox_TopSolderPaste.UseVisualStyleBackColor = True
        '
        'cbox_Paste_GrowShrink
        '
        Me.cbox_Paste_GrowShrink.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Paste_GrowShrink.Enabled = False
        Me.cbox_Paste_GrowShrink.FormattingEnabled = True
        Me.cbox_Paste_GrowShrink.Items.AddRange(New Object() {"Grow", "Shrink"})
        Me.cbox_Paste_GrowShrink.Location = New System.Drawing.Point(131, 65)
        Me.cbox_Paste_GrowShrink.Name = "cbox_Paste_GrowShrink"
        Me.cbox_Paste_GrowShrink.Size = New System.Drawing.Size(52, 21)
        Me.cbox_Paste_GrowShrink.TabIndex = 8
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkbox_AddTopSoldermask)
        Me.GroupBox3.Controls.Add(Me.gb_MaskModify)
        Me.GroupBox3.Controls.Add(Me.chkbox_AddBottomSoldermask)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(195, 164)
        Me.GroupBox3.TabIndex = 9
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Soldermask"
        '
        'chkbox_AddTopSoldermask
        '
        Me.chkbox_AddTopSoldermask.AutoSize = True
        Me.chkbox_AddTopSoldermask.Location = New System.Drawing.Point(12, 19)
        Me.chkbox_AddTopSoldermask.Name = "chkbox_AddTopSoldermask"
        Me.chkbox_AddTopSoldermask.Size = New System.Drawing.Size(103, 17)
        Me.chkbox_AddTopSoldermask.TabIndex = 14
        Me.chkbox_AddTopSoldermask.Text = "Add/Modify Top"
        Me.chkbox_AddTopSoldermask.UseVisualStyleBackColor = True
        '
        'gb_MaskModify
        '
        Me.gb_MaskModify.Controls.Add(Me.chkbox_ModifyMask)
        Me.gb_MaskModify.Controls.Add(Me.chkbox_CopyMask)
        Me.gb_MaskModify.Controls.Add(Me.tbox_Soldermask)
        Me.gb_MaskModify.Controls.Add(Me.cbox_Mask_GrowShrink)
        Me.gb_MaskModify.Controls.Add(Me.cbox_UnitSolderMask)
        Me.gb_MaskModify.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_MaskModify.Enabled = False
        Me.gb_MaskModify.Location = New System.Drawing.Point(3, 65)
        Me.gb_MaskModify.Name = "gb_MaskModify"
        Me.gb_MaskModify.Size = New System.Drawing.Size(189, 96)
        Me.gb_MaskModify.TabIndex = 19
        Me.gb_MaskModify.TabStop = False
        Me.gb_MaskModify.Text = "How:"
        '
        'chkbox_ModifyMask
        '
        Me.chkbox_ModifyMask.AutoSize = True
        Me.chkbox_ModifyMask.Location = New System.Drawing.Point(9, 42)
        Me.chkbox_ModifyMask.Name = "chkbox_ModifyMask"
        Me.chkbox_ModifyMask.Size = New System.Drawing.Size(133, 17)
        Me.chkbox_ModifyMask.TabIndex = 18
        Me.chkbox_ModifyMask.Text = "Modify Soldermask By:"
        Me.chkbox_ModifyMask.UseVisualStyleBackColor = True
        '
        'chkbox_CopyMask
        '
        Me.chkbox_CopyMask.AutoSize = True
        Me.chkbox_CopyMask.Location = New System.Drawing.Point(9, 19)
        Me.chkbox_CopyMask.Name = "chkbox_CopyMask"
        Me.chkbox_CopyMask.Size = New System.Drawing.Size(132, 17)
        Me.chkbox_CopyMask.TabIndex = 13
        Me.chkbox_CopyMask.Text = "Copy Top\Bottom Pad"
        Me.chkbox_CopyMask.UseVisualStyleBackColor = True
        '
        'tbox_Soldermask
        '
        Me.tbox_Soldermask.Enabled = False
        Me.tbox_Soldermask.Location = New System.Drawing.Point(9, 65)
        Me.tbox_Soldermask.Name = "tbox_Soldermask"
        Me.tbox_Soldermask.Size = New System.Drawing.Size(62, 20)
        Me.tbox_Soldermask.TabIndex = 15
        Me.tbox_Soldermask.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'cbox_Mask_GrowShrink
        '
        Me.cbox_Mask_GrowShrink.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Mask_GrowShrink.Enabled = False
        Me.cbox_Mask_GrowShrink.FormattingEnabled = True
        Me.cbox_Mask_GrowShrink.Items.AddRange(New Object() {"Grow", "Shrink"})
        Me.cbox_Mask_GrowShrink.Location = New System.Drawing.Point(131, 65)
        Me.cbox_Mask_GrowShrink.Name = "cbox_Mask_GrowShrink"
        Me.cbox_Mask_GrowShrink.Size = New System.Drawing.Size(52, 21)
        Me.cbox_Mask_GrowShrink.TabIndex = 16
        '
        'cbox_UnitSolderMask
        '
        Me.cbox_UnitSolderMask.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_UnitSolderMask.Enabled = False
        Me.cbox_UnitSolderMask.FormattingEnabled = True
        Me.cbox_UnitSolderMask.Location = New System.Drawing.Point(77, 65)
        Me.cbox_UnitSolderMask.Name = "cbox_UnitSolderMask"
        Me.cbox_UnitSolderMask.Size = New System.Drawing.Size(48, 21)
        Me.cbox_UnitSolderMask.TabIndex = 17
        '
        'chkbox_AddBottomSoldermask
        '
        Me.chkbox_AddBottomSoldermask.AutoSize = True
        Me.chkbox_AddBottomSoldermask.Location = New System.Drawing.Point(12, 42)
        Me.chkbox_AddBottomSoldermask.Name = "chkbox_AddBottomSoldermask"
        Me.chkbox_AddBottomSoldermask.Size = New System.Drawing.Size(117, 17)
        Me.chkbox_AddBottomSoldermask.TabIndex = 13
        Me.chkbox_AddBottomSoldermask.Text = "Add/Modify Bottom"
        Me.chkbox_AddBottomSoldermask.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.gb_PasteModify)
        Me.GroupBox2.Controls.Add(Me.chkbox_TopSolderPaste)
        Me.GroupBox2.Controls.Add(Me.chkbox_BotSolderpaste)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 164)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(195, 155)
        Me.GroupBox2.TabIndex = 8
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Solderpaste"
        '
        'gb_PasteModify
        '
        Me.gb_PasteModify.Controls.Add(Me.chkbox_ModifyPaste)
        Me.gb_PasteModify.Controls.Add(Me.chkbox_CopyPaste)
        Me.gb_PasteModify.Controls.Add(Me.cbox_Paste_GrowShrink)
        Me.gb_PasteModify.Controls.Add(Me.cbox_UnitSolderPaste)
        Me.gb_PasteModify.Controls.Add(Me.tbox_Solderpaste)
        Me.gb_PasteModify.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_PasteModify.Enabled = False
        Me.gb_PasteModify.Location = New System.Drawing.Point(3, 61)
        Me.gb_PasteModify.Name = "gb_PasteModify"
        Me.gb_PasteModify.Size = New System.Drawing.Size(189, 91)
        Me.gb_PasteModify.TabIndex = 14
        Me.gb_PasteModify.TabStop = False
        Me.gb_PasteModify.Text = "How:"
        '
        'chkbox_ModifyPaste
        '
        Me.chkbox_ModifyPaste.AutoSize = True
        Me.chkbox_ModifyPaste.Location = New System.Drawing.Point(9, 42)
        Me.chkbox_ModifyPaste.Name = "chkbox_ModifyPaste"
        Me.chkbox_ModifyPaste.Size = New System.Drawing.Size(134, 17)
        Me.chkbox_ModifyPaste.TabIndex = 19
        Me.chkbox_ModifyPaste.Text = "Modify Solderpaste By:"
        Me.chkbox_ModifyPaste.UseVisualStyleBackColor = True
        '
        'chkbox_CopyPaste
        '
        Me.chkbox_CopyPaste.AutoSize = True
        Me.chkbox_CopyPaste.Location = New System.Drawing.Point(9, 19)
        Me.chkbox_CopyPaste.Name = "chkbox_CopyPaste"
        Me.chkbox_CopyPaste.Size = New System.Drawing.Size(132, 17)
        Me.chkbox_CopyPaste.TabIndex = 13
        Me.chkbox_CopyPaste.Text = "Copy Top\Bottom Pad"
        Me.chkbox_CopyPaste.UseVisualStyleBackColor = True
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.chkbox_ModifyUnits)
        Me.GroupBox5.Controls.Add(Me.cbox_ChangeUnit)
        Me.GroupBox5.Controls.Add(Me.chkbox_PadsAutoName)
        Me.GroupBox5.Controls.Add(Me.chkbox_HoleAutoName)
        Me.GroupBox5.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox5.Location = New System.Drawing.Point(0, 319)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(195, 80)
        Me.GroupBox5.TabIndex = 13
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Auto Name:"
        '
        'chkbox_ModifyUnits
        '
        Me.chkbox_ModifyUnits.AutoSize = True
        Me.chkbox_ModifyUnits.Enabled = False
        Me.chkbox_ModifyUnits.Location = New System.Drawing.Point(12, 50)
        Me.chkbox_ModifyUnits.Name = "chkbox_ModifyUnits"
        Me.chkbox_ModifyUnits.Size = New System.Drawing.Size(103, 17)
        Me.chkbox_ModifyUnits.TabIndex = 20
        Me.chkbox_ModifyUnits.Text = "Change units to:"
        Me.chkbox_ModifyUnits.UseVisualStyleBackColor = True
        '
        'cbox_ChangeUnit
        '
        Me.cbox_ChangeUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_ChangeUnit.Enabled = False
        Me.cbox_ChangeUnit.FormattingEnabled = True
        Me.cbox_ChangeUnit.Location = New System.Drawing.Point(121, 48)
        Me.cbox_ChangeUnit.Name = "cbox_ChangeUnit"
        Me.cbox_ChangeUnit.Size = New System.Drawing.Size(65, 21)
        Me.cbox_ChangeUnit.TabIndex = 10
        '
        'chkbox_PadsAutoName
        '
        Me.chkbox_PadsAutoName.AutoSize = True
        Me.chkbox_PadsAutoName.Location = New System.Drawing.Point(90, 23)
        Me.chkbox_PadsAutoName.Name = "chkbox_PadsAutoName"
        Me.chkbox_PadsAutoName.Size = New System.Drawing.Size(50, 17)
        Me.chkbox_PadsAutoName.TabIndex = 3
        Me.chkbox_PadsAutoName.Text = "Pads"
        Me.chkbox_PadsAutoName.UseVisualStyleBackColor = True
        '
        'chklistbox_Filter
        '
        Me.chklistbox_Filter.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklistbox_Filter.CheckOnClick = True
        Me.chklistbox_Filter.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklistbox_Filter.Enabled = False
        Me.chklistbox_Filter.FormattingEnabled = True
        Me.chklistbox_Filter.Items.AddRange(New Object() {"Bond Pin", "Fiducial", "Mounting Hole", "Pin - Die", "Pin - SMD", "Pin - Through", "Shearing Hole", "Tooling Hole", "Via"})
        Me.chklistbox_Filter.Location = New System.Drawing.Point(3, 16)
        Me.chklistbox_Filter.Name = "chklistbox_Filter"
        Me.chklistbox_Filter.Size = New System.Drawing.Size(120, 325)
        Me.chklistbox_Filter.TabIndex = 16
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 399)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(321, 22)
        Me.StatusStrip1.TabIndex = 17
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(236, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WaitGif
        '
        Me.WaitGif.BackColor = System.Drawing.Color.Transparent
        Me.WaitGif.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'GroupBox6
        '
        Me.GroupBox6.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox6.Controls.Add(Me.chklistbox_Filter)
        Me.GroupBox6.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox6.Name = "GroupBox6"
        Me.GroupBox6.Size = New System.Drawing.Size(126, 344)
        Me.GroupBox6.TabIndex = 18
        Me.GroupBox6.TabStop = False
        Me.GroupBox6.Text = "Padstack Filter"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox6)
        Me.Panel1.Controls.Add(Me.btn_Modify)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(195, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(126, 399)
        Me.Panel1.TabIndex = 19
        '
        'frmPadstackEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(321, 421)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmPadstackEditor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Padstack Editor"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.gb_MaskModify.ResumeLayout(False)
        Me.gb_MaskModify.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.gb_PasteModify.ResumeLayout(False)
        Me.gb_PasteModify.PerformLayout()
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox6.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Modify As System.Windows.Forms.Button
    Friend WithEvents chkbox_HoleAutoName As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_UnitSolderPaste As System.Windows.Forms.ComboBox
    Friend WithEvents tbox_Solderpaste As System.Windows.Forms.TextBox
    Friend WithEvents chkbox_BotSolderpaste As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_TopSolderPaste As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_Paste_GrowShrink As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cbox_Mask_GrowShrink As System.Windows.Forms.ComboBox
    Friend WithEvents cbox_UnitSolderMask As System.Windows.Forms.ComboBox
    Friend WithEvents tbox_Soldermask As System.Windows.Forms.TextBox
    Friend WithEvents chkbox_AddBottomSoldermask As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_AddTopSoldermask As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_CopyMask As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_CopyPaste As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_PadsAutoName As System.Windows.Forms.CheckBox
    Friend WithEvents chklistbox_Filter As System.Windows.Forms.CheckedListBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents GroupBox6 As System.Windows.Forms.GroupBox
    Friend WithEvents gb_MaskModify As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_ModifyMask As System.Windows.Forms.CheckBox
    Friend WithEvents gb_PasteModify As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_ModifyPaste As System.Windows.Forms.CheckBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkbox_ModifyUnits As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_ChangeUnit As System.Windows.Forms.ComboBox
End Class
