<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHealPDB
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
        Me.BW_DxDesigner = New System.ComponentModel.BackgroundWorker()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkbox_ChangeCellCase = New System.Windows.Forms.CheckBox()
        Me.cbox_CellCase = New System.Windows.Forms.ComboBox()
        Me.chkbox_UpdateSymPartition = New System.Windows.Forms.CheckBox()
        Me.chkbox_RemoveSpacesfromCells = New System.Windows.Forms.CheckBox()
        Me.chkbox_ChangeSymbolCase = New System.Windows.Forms.CheckBox()
        Me.cbox_SymbolCase = New System.Windows.Forms.ComboBox()
        Me.chkbox_UpdatePartType = New System.Windows.Forms.CheckBox()
        Me.chkbox_RepairErrors = New System.Windows.Forms.CheckBox()
        Me.chkbox_AddNC = New System.Windows.Forms.CheckBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.ts_Notes = New System.Windows.Forms.Label()
        Me.btn_HealLibrary = New System.Windows.Forms.Button()
        Me.ts_Warnings = New System.Windows.Forms.Label()
        Me.chkbox_Note = New System.Windows.Forms.CheckBox()
        Me.chkbox_Warning = New System.Windows.Forms.CheckBox()
        Me.ts_Errors = New System.Windows.Forms.Label()
        Me.chkbox_Error = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chklistbox_PDBPartitions = New System.Windows.Forms.CheckedListBox()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 245)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(497, 22)
        Me.StatusStrip1.TabIndex = 69
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(412, 17)
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
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(497, 245)
        Me.SplitContainer1.SplitterDistance = 294
        Me.SplitContainer1.TabIndex = 70
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkbox_ChangeCellCase)
        Me.GroupBox1.Controls.Add(Me.cbox_CellCase)
        Me.GroupBox1.Controls.Add(Me.chkbox_UpdateSymPartition)
        Me.GroupBox1.Controls.Add(Me.chkbox_RemoveSpacesfromCells)
        Me.GroupBox1.Controls.Add(Me.chkbox_ChangeSymbolCase)
        Me.GroupBox1.Controls.Add(Me.cbox_SymbolCase)
        Me.GroupBox1.Controls.Add(Me.chkbox_UpdatePartType)
        Me.GroupBox1.Controls.Add(Me.chkbox_RepairErrors)
        Me.GroupBox1.Controls.Add(Me.chkbox_AddNC)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(294, 177)
        Me.GroupBox1.TabIndex = 75
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Options:"
        '
        'chkbox_ChangeCellCase
        '
        Me.chkbox_ChangeCellCase.AutoSize = True
        Me.chkbox_ChangeCellCase.Location = New System.Drawing.Point(9, 132)
        Me.chkbox_ChangeCellCase.Name = "chkbox_ChangeCellCase"
        Me.chkbox_ChangeCellCase.Size = New System.Drawing.Size(110, 17)
        Me.chkbox_ChangeCellCase.TabIndex = 74
        Me.chkbox_ChangeCellCase.Text = "Change Cell Case"
        Me.chkbox_ChangeCellCase.UseVisualStyleBackColor = True
        '
        'cbox_CellCase
        '
        Me.cbox_CellCase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_CellCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_CellCase.Enabled = False
        Me.cbox_CellCase.FormattingEnabled = True
        Me.cbox_CellCase.Items.AddRange(New Object() {"Uppercase", "Lowercase"})
        Me.cbox_CellCase.Location = New System.Drawing.Point(143, 130)
        Me.cbox_CellCase.Name = "cbox_CellCase"
        Me.cbox_CellCase.Size = New System.Drawing.Size(143, 21)
        Me.cbox_CellCase.TabIndex = 73
        '
        'chkbox_UpdateSymPartition
        '
        Me.chkbox_UpdateSymPartition.AutoSize = True
        Me.chkbox_UpdateSymPartition.Checked = True
        Me.chkbox_UpdateSymPartition.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_UpdateSymPartition.Location = New System.Drawing.Point(9, 16)
        Me.chkbox_UpdateSymPartition.Name = "chkbox_UpdateSymPartition"
        Me.chkbox_UpdateSymPartition.Size = New System.Drawing.Size(144, 17)
        Me.chkbox_UpdateSymPartition.TabIndex = 72
        Me.chkbox_UpdateSymPartition.Text = "Update Symbol Partitions"
        Me.chkbox_UpdateSymPartition.UseVisualStyleBackColor = True
        '
        'chkbox_RemoveSpacesfromCells
        '
        Me.chkbox_RemoveSpacesfromCells.AutoSize = True
        Me.chkbox_RemoveSpacesfromCells.Location = New System.Drawing.Point(9, 74)
        Me.chkbox_RemoveSpacesfromCells.Name = "chkbox_RemoveSpacesfromCells"
        Me.chkbox_RemoveSpacesfromCells.Size = New System.Drawing.Size(153, 17)
        Me.chkbox_RemoveSpacesfromCells.TabIndex = 71
        Me.chkbox_RemoveSpacesfromCells.Text = "Remove Spaces from Cells"
        Me.chkbox_RemoveSpacesfromCells.UseVisualStyleBackColor = True
        '
        'chkbox_ChangeSymbolCase
        '
        Me.chkbox_ChangeSymbolCase.AutoSize = True
        Me.chkbox_ChangeSymbolCase.Location = New System.Drawing.Point(9, 103)
        Me.chkbox_ChangeSymbolCase.Name = "chkbox_ChangeSymbolCase"
        Me.chkbox_ChangeSymbolCase.Size = New System.Drawing.Size(127, 17)
        Me.chkbox_ChangeSymbolCase.TabIndex = 71
        Me.chkbox_ChangeSymbolCase.Text = "Change Symbol Case"
        Me.chkbox_ChangeSymbolCase.UseVisualStyleBackColor = True
        '
        'cbox_SymbolCase
        '
        Me.cbox_SymbolCase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_SymbolCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_SymbolCase.Enabled = False
        Me.cbox_SymbolCase.FormattingEnabled = True
        Me.cbox_SymbolCase.Items.AddRange(New Object() {"Uppercase", "Lowercase"})
        Me.cbox_SymbolCase.Location = New System.Drawing.Point(142, 101)
        Me.cbox_SymbolCase.Name = "cbox_SymbolCase"
        Me.cbox_SymbolCase.Size = New System.Drawing.Size(142, 21)
        Me.cbox_SymbolCase.TabIndex = 64
        '
        'chkbox_UpdatePartType
        '
        Me.chkbox_UpdatePartType.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbox_UpdatePartType.AutoSize = True
        Me.chkbox_UpdatePartType.Checked = True
        Me.chkbox_UpdatePartType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_UpdatePartType.Location = New System.Drawing.Point(176, 16)
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
        Me.chkbox_RepairErrors.Location = New System.Drawing.Point(9, 45)
        Me.chkbox_RepairErrors.Name = "chkbox_RepairErrors"
        Me.chkbox_RepairErrors.Size = New System.Drawing.Size(138, 17)
        Me.chkbox_RepairErrors.TabIndex = 63
        Me.chkbox_RepairErrors.Text = "Attempt to Repair Errors"
        Me.chkbox_RepairErrors.UseVisualStyleBackColor = True
        '
        'chkbox_AddNC
        '
        Me.chkbox_AddNC.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbox_AddNC.AutoSize = True
        Me.chkbox_AddNC.Checked = True
        Me.chkbox_AddNC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_AddNC.Location = New System.Drawing.Point(176, 45)
        Me.chkbox_AddNC.Name = "chkbox_AddNC"
        Me.chkbox_AddNC.Size = New System.Drawing.Size(86, 17)
        Me.chkbox_AddNC.TabIndex = 62
        Me.chkbox_AddNC.Text = "Add NC Pins"
        Me.chkbox_AddNC.UseVisualStyleBackColor = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.ts_Notes)
        Me.Panel2.Controls.Add(Me.btn_HealLibrary)
        Me.Panel2.Controls.Add(Me.ts_Warnings)
        Me.Panel2.Controls.Add(Me.chkbox_Note)
        Me.Panel2.Controls.Add(Me.chkbox_Warning)
        Me.Panel2.Controls.Add(Me.ts_Errors)
        Me.Panel2.Controls.Add(Me.chkbox_Error)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 177)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(294, 68)
        Me.Panel2.TabIndex = 76
        '
        'ts_Notes
        '
        Me.ts_Notes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ts_Notes.AutoSize = True
        Me.ts_Notes.Location = New System.Drawing.Point(91, 48)
        Me.ts_Notes.Name = "ts_Notes"
        Me.ts_Notes.Size = New System.Drawing.Size(13, 13)
        Me.ts_Notes.TabIndex = 77
        Me.ts_Notes.Text = "0"
        '
        'btn_HealLibrary
        '
        Me.btn_HealLibrary.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_HealLibrary.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_HealLibrary.Location = New System.Drawing.Point(183, 9)
        Me.btn_HealLibrary.Margin = New System.Windows.Forms.Padding(2)
        Me.btn_HealLibrary.Name = "btn_HealLibrary"
        Me.btn_HealLibrary.Size = New System.Drawing.Size(100, 51)
        Me.btn_HealLibrary.TabIndex = 71
        Me.btn_HealLibrary.Text = "Process"
        Me.btn_HealLibrary.UseVisualStyleBackColor = False
        '
        'ts_Warnings
        '
        Me.ts_Warnings.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ts_Warnings.AutoSize = True
        Me.ts_Warnings.Location = New System.Drawing.Point(91, 28)
        Me.ts_Warnings.Name = "ts_Warnings"
        Me.ts_Warnings.Size = New System.Drawing.Size(13, 13)
        Me.ts_Warnings.TabIndex = 76
        Me.ts_Warnings.Text = "0"
        '
        'chkbox_Note
        '
        Me.chkbox_Note.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkbox_Note.AutoSize = True
        Me.chkbox_Note.Checked = True
        Me.chkbox_Note.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Note.Location = New System.Drawing.Point(10, 46)
        Me.chkbox_Note.Name = "chkbox_Note"
        Me.chkbox_Note.Size = New System.Drawing.Size(54, 17)
        Me.chkbox_Note.TabIndex = 71
        Me.chkbox_Note.Text = "Notes"
        Me.chkbox_Note.UseVisualStyleBackColor = True
        '
        'chkbox_Warning
        '
        Me.chkbox_Warning.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkbox_Warning.AutoSize = True
        Me.chkbox_Warning.Checked = True
        Me.chkbox_Warning.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Warning.Location = New System.Drawing.Point(10, 26)
        Me.chkbox_Warning.Name = "chkbox_Warning"
        Me.chkbox_Warning.Size = New System.Drawing.Size(71, 17)
        Me.chkbox_Warning.TabIndex = 72
        Me.chkbox_Warning.Text = "Warnings"
        Me.chkbox_Warning.UseVisualStyleBackColor = True
        '
        'ts_Errors
        '
        Me.ts_Errors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ts_Errors.AutoSize = True
        Me.ts_Errors.Location = New System.Drawing.Point(91, 8)
        Me.ts_Errors.Name = "ts_Errors"
        Me.ts_Errors.Size = New System.Drawing.Size(13, 13)
        Me.ts_Errors.TabIndex = 75
        Me.ts_Errors.Text = "0"
        '
        'chkbox_Error
        '
        Me.chkbox_Error.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkbox_Error.AutoSize = True
        Me.chkbox_Error.Checked = True
        Me.chkbox_Error.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Error.Location = New System.Drawing.Point(10, 6)
        Me.chkbox_Error.Name = "chkbox_Error"
        Me.chkbox_Error.Size = New System.Drawing.Size(53, 17)
        Me.chkbox_Error.TabIndex = 70
        Me.chkbox_Error.Text = "Errors"
        Me.chkbox_Error.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chklistbox_PDBPartitions)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(199, 245)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "PDB Partitions:"
        '
        'chklistbox_PDBPartitions
        '
        Me.chklistbox_PDBPartitions.BackColor = System.Drawing.SystemColors.Window
        Me.chklistbox_PDBPartitions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklistbox_PDBPartitions.CheckOnClick = True
        Me.chklistbox_PDBPartitions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklistbox_PDBPartitions.FormattingEnabled = True
        Me.chklistbox_PDBPartitions.Location = New System.Drawing.Point(3, 16)
        Me.chklistbox_PDBPartitions.Name = "chklistbox_PDBPartitions"
        Me.chklistbox_PDBPartitions.Size = New System.Drawing.Size(193, 226)
        Me.chklistbox_PDBPartitions.TabIndex = 75
        '
        'frmHealPDB
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(497, 267)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MinimumSize = New System.Drawing.Size(350, 260)
        Me.Name = "frmHealPDB"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Heal PDB - Basic"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents BW_DxDesigner As System.ComponentModel.BackgroundWorker
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_ChangeCellCase As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_CellCase As System.Windows.Forms.ComboBox
    Friend WithEvents chkbox_UpdateSymPartition As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_RemoveSpacesfromCells As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_ChangeSymbolCase As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_SymbolCase As System.Windows.Forms.ComboBox
    Friend WithEvents chkbox_UpdatePartType As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_RepairErrors As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_AddNC As System.Windows.Forms.CheckBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents ts_Notes As System.Windows.Forms.Label
    Friend WithEvents chkbox_Note As System.Windows.Forms.CheckBox
    Friend WithEvents ts_Warnings As System.Windows.Forms.Label
    Friend WithEvents chkbox_Error As System.Windows.Forms.CheckBox
    Friend WithEvents ts_Errors As System.Windows.Forms.Label
    Friend WithEvents chkbox_Warning As System.Windows.Forms.CheckBox
    Friend WithEvents btn_HealLibrary As System.Windows.Forms.Button
    Friend WithEvents chklistbox_PDBPartitions As System.Windows.Forms.CheckedListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
End Class
