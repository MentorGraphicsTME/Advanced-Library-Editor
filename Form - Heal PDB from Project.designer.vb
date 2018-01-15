<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHealPDBwithExpedition
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
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gb_Options = New System.Windows.Forms.GroupBox()
        Me.chkbox_UpdateSymPartition = New System.Windows.Forms.CheckBox()
        Me.chkbox_RemoveSpacesfromCells = New System.Windows.Forms.CheckBox()
        Me.chkbox_ChangeSymbolCase = New System.Windows.Forms.CheckBox()
        Me.lblSourceBrowse_B = New System.Windows.Forms.Label()
        Me.cbox_SymbolCase = New System.Windows.Forms.ComboBox()
        Me.lblSourceBrowse_A = New System.Windows.Forms.Label()
        Me.btn_BrowseExp = New System.Windows.Forms.Button()
        Me.btn_BrowseDxD = New System.Windows.Forms.Button()
        Me.tbox_ExpeditionPCB = New System.Windows.Forms.TextBox()
        Me.tbox_DxDesigner = New System.Windows.Forms.TextBox()
        Me.chkbox_UpdatePartType = New System.Windows.Forms.CheckBox()
        Me.chkbox_RepairErrors = New System.Windows.Forms.CheckBox()
        Me.chkbox_AddNC = New System.Windows.Forms.CheckBox()
        Me.chklistbox_PDBPartitions = New System.Windows.Forms.CheckedListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ts_Notes = New System.Windows.Forms.Label()
        Me.ts_Warnings = New System.Windows.Forms.Label()
        Me.ts_Errors = New System.Windows.Forms.Label()
        Me.btn_HealLibrary = New System.Windows.Forms.Button()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.BW_DxDesigner = New System.ComponentModel.BackgroundWorker()
        Me.Panel2.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.gb_Options.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.SplitContainer1)
        Me.Panel2.Controls.Add(Me.StatusStrip1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(528, 378)
        Me.Panel2.TabIndex = 38
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_Options)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.chklistbox_PDBPartitions)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(528, 356)
        Me.SplitContainer1.SplitterDistance = 278
        Me.SplitContainer1.TabIndex = 74
        '
        'gb_Options
        '
        Me.gb_Options.Controls.Add(Me.chkbox_UpdateSymPartition)
        Me.gb_Options.Controls.Add(Me.chkbox_RemoveSpacesfromCells)
        Me.gb_Options.Controls.Add(Me.chkbox_ChangeSymbolCase)
        Me.gb_Options.Controls.Add(Me.lblSourceBrowse_B)
        Me.gb_Options.Controls.Add(Me.cbox_SymbolCase)
        Me.gb_Options.Controls.Add(Me.lblSourceBrowse_A)
        Me.gb_Options.Controls.Add(Me.btn_BrowseExp)
        Me.gb_Options.Controls.Add(Me.btn_BrowseDxD)
        Me.gb_Options.Controls.Add(Me.tbox_ExpeditionPCB)
        Me.gb_Options.Controls.Add(Me.tbox_DxDesigner)
        Me.gb_Options.Controls.Add(Me.chkbox_UpdatePartType)
        Me.gb_Options.Controls.Add(Me.chkbox_RepairErrors)
        Me.gb_Options.Controls.Add(Me.chkbox_AddNC)
        Me.gb_Options.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gb_Options.Location = New System.Drawing.Point(0, 0)
        Me.gb_Options.Name = "gb_Options"
        Me.gb_Options.Size = New System.Drawing.Size(278, 356)
        Me.gb_Options.TabIndex = 65
        Me.gb_Options.TabStop = False
        Me.gb_Options.Text = "Options:"
        '
        'chkbox_UpdateSymPartition
        '
        Me.chkbox_UpdateSymPartition.AutoSize = True
        Me.chkbox_UpdateSymPartition.Checked = True
        Me.chkbox_UpdateSymPartition.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_UpdateSymPartition.Location = New System.Drawing.Point(12, 19)
        Me.chkbox_UpdateSymPartition.Name = "chkbox_UpdateSymPartition"
        Me.chkbox_UpdateSymPartition.Size = New System.Drawing.Size(144, 17)
        Me.chkbox_UpdateSymPartition.TabIndex = 73
        Me.chkbox_UpdateSymPartition.Text = "Update Symbol Partitions"
        Me.chkbox_UpdateSymPartition.UseVisualStyleBackColor = True
        '
        'chkbox_RemoveSpacesfromCells
        '
        Me.chkbox_RemoveSpacesfromCells.AutoSize = True
        Me.chkbox_RemoveSpacesfromCells.Location = New System.Drawing.Point(12, 65)
        Me.chkbox_RemoveSpacesfromCells.Name = "chkbox_RemoveSpacesfromCells"
        Me.chkbox_RemoveSpacesfromCells.Size = New System.Drawing.Size(153, 17)
        Me.chkbox_RemoveSpacesfromCells.TabIndex = 71
        Me.chkbox_RemoveSpacesfromCells.Text = "Remove Spaces from Cells"
        Me.chkbox_RemoveSpacesfromCells.UseVisualStyleBackColor = True
        '
        'chkbox_ChangeSymbolCase
        '
        Me.chkbox_ChangeSymbolCase.AutoSize = True
        Me.chkbox_ChangeSymbolCase.Location = New System.Drawing.Point(11, 192)
        Me.chkbox_ChangeSymbolCase.Name = "chkbox_ChangeSymbolCase"
        Me.chkbox_ChangeSymbolCase.Size = New System.Drawing.Size(127, 17)
        Me.chkbox_ChangeSymbolCase.TabIndex = 71
        Me.chkbox_ChangeSymbolCase.Text = "Change Symbol Case"
        Me.chkbox_ChangeSymbolCase.UseVisualStyleBackColor = True
        '
        'lblSourceBrowse_B
        '
        Me.lblSourceBrowse_B.AutoSize = True
        Me.lblSourceBrowse_B.Location = New System.Drawing.Point(12, 140)
        Me.lblSourceBrowse_B.Name = "lblSourceBrowse_B"
        Me.lblSourceBrowse_B.Size = New System.Drawing.Size(83, 13)
        Me.lblSourceBrowse_B.TabIndex = 70
        Me.lblSourceBrowse_B.Text = "Expedition PCB:"
        '
        'cbox_SymbolCase
        '
        Me.cbox_SymbolCase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_SymbolCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_SymbolCase.Enabled = False
        Me.cbox_SymbolCase.FormattingEnabled = True
        Me.cbox_SymbolCase.Items.AddRange(New Object() {"Uppercase", "Lowercase"})
        Me.cbox_SymbolCase.Location = New System.Drawing.Point(146, 190)
        Me.cbox_SymbolCase.Name = "cbox_SymbolCase"
        Me.cbox_SymbolCase.Size = New System.Drawing.Size(123, 21)
        Me.cbox_SymbolCase.TabIndex = 64
        '
        'lblSourceBrowse_A
        '
        Me.lblSourceBrowse_A.AutoSize = True
        Me.lblSourceBrowse_A.Location = New System.Drawing.Point(12, 96)
        Me.lblSourceBrowse_A.Name = "lblSourceBrowse_A"
        Me.lblSourceBrowse_A.Size = New System.Drawing.Size(118, 13)
        Me.lblSourceBrowse_A.TabIndex = 69
        Me.lblSourceBrowse_A.Text = "DxDesigner Schematic:"
        '
        'btn_BrowseExp
        '
        Me.btn_BrowseExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_BrowseExp.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_BrowseExp.Location = New System.Drawing.Point(239, 155)
        Me.btn_BrowseExp.Name = "btn_BrowseExp"
        Me.btn_BrowseExp.Size = New System.Drawing.Size(30, 20)
        Me.btn_BrowseExp.TabIndex = 68
        Me.btn_BrowseExp.Text = "..."
        Me.btn_BrowseExp.UseVisualStyleBackColor = False
        '
        'btn_BrowseDxD
        '
        Me.btn_BrowseDxD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_BrowseDxD.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_BrowseDxD.Location = New System.Drawing.Point(239, 112)
        Me.btn_BrowseDxD.Name = "btn_BrowseDxD"
        Me.btn_BrowseDxD.Size = New System.Drawing.Size(30, 20)
        Me.btn_BrowseDxD.TabIndex = 67
        Me.btn_BrowseDxD.Text = "..."
        Me.btn_BrowseDxD.UseVisualStyleBackColor = False
        '
        'tbox_ExpeditionPCB
        '
        Me.tbox_ExpeditionPCB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_ExpeditionPCB.Location = New System.Drawing.Point(12, 156)
        Me.tbox_ExpeditionPCB.Name = "tbox_ExpeditionPCB"
        Me.tbox_ExpeditionPCB.Size = New System.Drawing.Size(218, 20)
        Me.tbox_ExpeditionPCB.TabIndex = 66
        '
        'tbox_DxDesigner
        '
        Me.tbox_DxDesigner.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_DxDesigner.Location = New System.Drawing.Point(12, 112)
        Me.tbox_DxDesigner.Name = "tbox_DxDesigner"
        Me.tbox_DxDesigner.Size = New System.Drawing.Size(218, 20)
        Me.tbox_DxDesigner.TabIndex = 65
        '
        'chkbox_UpdatePartType
        '
        Me.chkbox_UpdatePartType.AutoSize = True
        Me.chkbox_UpdatePartType.Checked = True
        Me.chkbox_UpdatePartType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_UpdatePartType.Location = New System.Drawing.Point(164, 19)
        Me.chkbox_UpdatePartType.Name = "chkbox_UpdatePartType"
        Me.chkbox_UpdatePartType.Size = New System.Drawing.Size(110, 17)
        Me.chkbox_UpdatePartType.TabIndex = 61
        Me.chkbox_UpdatePartType.Text = "Update Part Type"
        Me.chkbox_UpdatePartType.UseVisualStyleBackColor = True
        '
        'chkbox_RepairErrors
        '
        Me.chkbox_RepairErrors.AutoSize = True
        Me.chkbox_RepairErrors.Checked = True
        Me.chkbox_RepairErrors.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_RepairErrors.Location = New System.Drawing.Point(12, 42)
        Me.chkbox_RepairErrors.Name = "chkbox_RepairErrors"
        Me.chkbox_RepairErrors.Size = New System.Drawing.Size(138, 17)
        Me.chkbox_RepairErrors.TabIndex = 63
        Me.chkbox_RepairErrors.Text = "Attempt to Repair Errors"
        Me.chkbox_RepairErrors.UseVisualStyleBackColor = True
        '
        'chkbox_AddNC
        '
        Me.chkbox_AddNC.AutoSize = True
        Me.chkbox_AddNC.Checked = True
        Me.chkbox_AddNC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_AddNC.Location = New System.Drawing.Point(164, 42)
        Me.chkbox_AddNC.Name = "chkbox_AddNC"
        Me.chkbox_AddNC.Size = New System.Drawing.Size(86, 17)
        Me.chkbox_AddNC.TabIndex = 62
        Me.chkbox_AddNC.Text = "Add NC Pins"
        Me.chkbox_AddNC.UseVisualStyleBackColor = True
        '
        'chklistbox_PDBPartitions
        '
        Me.chklistbox_PDBPartitions.CheckOnClick = True
        Me.chklistbox_PDBPartitions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklistbox_PDBPartitions.FormattingEnabled = True
        Me.chklistbox_PDBPartitions.Location = New System.Drawing.Point(0, 0)
        Me.chklistbox_PDBPartitions.Name = "chklistbox_PDBPartitions"
        Me.chklistbox_PDBPartitions.Size = New System.Drawing.Size(246, 299)
        Me.chklistbox_PDBPartitions.TabIndex = 1
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.ts_Notes)
        Me.Panel1.Controls.Add(Me.ts_Warnings)
        Me.Panel1.Controls.Add(Me.ts_Errors)
        Me.Panel1.Controls.Add(Me.btn_HealLibrary)
        Me.Panel1.Controls.Add(Me.Label4)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 299)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(246, 57)
        Me.Panel1.TabIndex = 73
        '
        'ts_Notes
        '
        Me.ts_Notes.AutoSize = True
        Me.ts_Notes.Location = New System.Drawing.Point(69, 39)
        Me.ts_Notes.Name = "ts_Notes"
        Me.ts_Notes.Size = New System.Drawing.Size(13, 13)
        Me.ts_Notes.TabIndex = 77
        Me.ts_Notes.Text = "0"
        '
        'ts_Warnings
        '
        Me.ts_Warnings.AutoSize = True
        Me.ts_Warnings.Location = New System.Drawing.Point(69, 22)
        Me.ts_Warnings.Name = "ts_Warnings"
        Me.ts_Warnings.Size = New System.Drawing.Size(13, 13)
        Me.ts_Warnings.TabIndex = 76
        Me.ts_Warnings.Text = "0"
        '
        'ts_Errors
        '
        Me.ts_Errors.AutoSize = True
        Me.ts_Errors.Location = New System.Drawing.Point(69, 5)
        Me.ts_Errors.Name = "ts_Errors"
        Me.ts_Errors.Size = New System.Drawing.Size(13, 13)
        Me.ts_Errors.TabIndex = 75
        Me.ts_Errors.Text = "0"
        '
        'btn_HealLibrary
        '
        Me.btn_HealLibrary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_HealLibrary.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_HealLibrary.Location = New System.Drawing.Point(135, 8)
        Me.btn_HealLibrary.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_HealLibrary.Name = "btn_HealLibrary"
        Me.btn_HealLibrary.Size = New System.Drawing.Size(100, 40)
        Me.btn_HealLibrary.TabIndex = 71
        Me.btn_HealLibrary.Text = "Update"
        Me.btn_HealLibrary.UseVisualStyleBackColor = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(10, 39)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 74
        Me.Label4.Text = "Notes:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(10, 22)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(55, 13)
        Me.Label3.TabIndex = 73
        Me.Label3.Text = "Warnings:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(10, 5)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 13)
        Me.Label2.TabIndex = 72
        Me.Label2.Text = "Errors:"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 356)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(528, 22)
        Me.StatusStrip1.TabIndex = 72
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(443, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WaitGif
        '
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'frmHealPDBwithExpedition
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(96.0!, 96.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(528, 378)
        Me.Controls.Add(Me.Panel2)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.Name = "frmHealPDBwithExpedition"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Heal PDB from DxD/EE Project"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.gb_Options.ResumeLayout(False)
        Me.gb_Options.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gb_Options As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_UpdateSymPartition As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_RemoveSpacesfromCells As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_ChangeSymbolCase As System.Windows.Forms.CheckBox
    Friend WithEvents lblSourceBrowse_B As System.Windows.Forms.Label
    Friend WithEvents cbox_SymbolCase As System.Windows.Forms.ComboBox
    Friend WithEvents lblSourceBrowse_A As System.Windows.Forms.Label
    Friend WithEvents btn_BrowseExp As System.Windows.Forms.Button
    Friend WithEvents btn_BrowseDxD As System.Windows.Forms.Button
    Friend WithEvents tbox_ExpeditionPCB As System.Windows.Forms.TextBox
    Friend WithEvents tbox_DxDesigner As System.Windows.Forms.TextBox
    Friend WithEvents chkbox_UpdatePartType As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_RepairErrors As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_AddNC As System.Windows.Forms.CheckBox
    Friend WithEvents chklistbox_PDBPartitions As System.Windows.Forms.CheckedListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ts_Notes As System.Windows.Forms.Label
    Friend WithEvents ts_Warnings As System.Windows.Forms.Label
    Friend WithEvents ts_Errors As System.Windows.Forms.Label
    Friend WithEvents btn_HealLibrary As System.Windows.Forms.Button
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents BW_DxDesigner As System.ComponentModel.BackgroundWorker
End Class
