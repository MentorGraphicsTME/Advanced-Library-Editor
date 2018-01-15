<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBuildPDBfromProject
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.btn_Read = New System.Windows.Forms.Button()
        Me.gb_Layout = New System.Windows.Forms.GroupBox()
        Me.btn_BrowseExp = New System.Windows.Forms.Button()
        Me.tbox_PCB = New System.Windows.Forms.TextBox()
        Me.DxDbox = New System.Windows.Forms.GroupBox()
        Me.tbox_DxD = New System.Windows.Forms.TextBox()
        Me.btn_BrowseDxD = New System.Windows.Forms.Button()
        Me.gb_Options = New System.Windows.Forms.GroupBox()
        Me.chkbox_RebuildParts = New System.Windows.Forms.CheckBox()
        Me.tv_Parts = New System.Windows.Forms.TreeView()
        Me.plBuildOptions = New System.Windows.Forms.Panel()
        Me.btn_Create = New System.Windows.Forms.Button()
        Me.gbAction = New System.Windows.Forms.GroupBox()
        Me.chkboxMultiThread = New System.Windows.Forms.CheckBox()
        Me.chkbox_RefDesPartitions = New System.Windows.Forms.CheckBox()
        Me.chkbox_SaveEachPart = New System.Windows.Forms.CheckBox()
        Me.chkbox_GetHeight = New System.Windows.Forms.CheckBox()
        Me.chkbox_RemoveIncomplete = New System.Windows.Forms.CheckBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsm_Threads = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsm_Exp = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsm_DxD = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.gb_Layout.SuspendLayout()
        Me.DxDbox.SuspendLayout()
        Me.gb_Options.SuspendLayout()
        Me.plBuildOptions.SuspendLayout()
        Me.gbAction.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.btn_Read)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_Layout)
        Me.SplitContainer1.Panel1.Controls.Add(Me.DxDbox)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_Options)
        Me.SplitContainer1.Panel1MinSize = 226
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tv_Parts)
        Me.SplitContainer1.Panel2.Controls.Add(Me.plBuildOptions)
        Me.SplitContainer1.Panel2MinSize = 226
        Me.SplitContainer1.Size = New System.Drawing.Size(721, 643)
        Me.SplitContainer1.SplitterDistance = 364
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 98
        '
        'btn_Read
        '
        Me.btn_Read.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Read.Enabled = False
        Me.btn_Read.Location = New System.Drawing.Point(201, 150)
        Me.btn_Read.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_Read.Name = "btn_Read"
        Me.btn_Read.Size = New System.Drawing.Size(153, 28)
        Me.btn_Read.TabIndex = 23
        Me.btn_Read.Text = "Analyze"
        Me.btn_Read.UseVisualStyleBackColor = False
        '
        'gb_Layout
        '
        Me.gb_Layout.Controls.Add(Me.btn_BrowseExp)
        Me.gb_Layout.Controls.Add(Me.tbox_PCB)
        Me.gb_Layout.Dock = System.Windows.Forms.DockStyle.Top
        Me.gb_Layout.Location = New System.Drawing.Point(0, 59)
        Me.gb_Layout.Margin = New System.Windows.Forms.Padding(4)
        Me.gb_Layout.Name = "gb_Layout"
        Me.gb_Layout.Padding = New System.Windows.Forms.Padding(4)
        Me.gb_Layout.Size = New System.Drawing.Size(364, 62)
        Me.gb_Layout.TabIndex = 22
        Me.gb_Layout.TabStop = False
        Me.gb_Layout.Text = "Expedition PCB"
        '
        'btn_BrowseExp
        '
        Me.btn_BrowseExp.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_BrowseExp.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_BrowseExp.Location = New System.Drawing.Point(310, 21)
        Me.btn_BrowseExp.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_BrowseExp.Name = "btn_BrowseExp"
        Me.btn_BrowseExp.Size = New System.Drawing.Size(44, 28)
        Me.btn_BrowseExp.TabIndex = 7
        Me.btn_BrowseExp.Text = "..."
        Me.btn_BrowseExp.UseVisualStyleBackColor = False
        '
        'tbox_PCB
        '
        Me.tbox_PCB.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_PCB.Location = New System.Drawing.Point(8, 22)
        Me.tbox_PCB.Margin = New System.Windows.Forms.Padding(4)
        Me.tbox_PCB.Name = "tbox_PCB"
        Me.tbox_PCB.Size = New System.Drawing.Size(293, 22)
        Me.tbox_PCB.TabIndex = 6
        '
        'DxDbox
        '
        Me.DxDbox.Controls.Add(Me.tbox_DxD)
        Me.DxDbox.Controls.Add(Me.btn_BrowseDxD)
        Me.DxDbox.Dock = System.Windows.Forms.DockStyle.Top
        Me.DxDbox.Location = New System.Drawing.Point(0, 0)
        Me.DxDbox.Margin = New System.Windows.Forms.Padding(4)
        Me.DxDbox.Name = "DxDbox"
        Me.DxDbox.Padding = New System.Windows.Forms.Padding(4)
        Me.DxDbox.Size = New System.Drawing.Size(364, 59)
        Me.DxDbox.TabIndex = 21
        Me.DxDbox.TabStop = False
        Me.DxDbox.Text = "DxDesigner Project"
        '
        'tbox_DxD
        '
        Me.tbox_DxD.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_DxD.Location = New System.Drawing.Point(8, 23)
        Me.tbox_DxD.Margin = New System.Windows.Forms.Padding(4)
        Me.tbox_DxD.Name = "tbox_DxD"
        Me.tbox_DxD.Size = New System.Drawing.Size(293, 22)
        Me.tbox_DxD.TabIndex = 1
        '
        'btn_BrowseDxD
        '
        Me.btn_BrowseDxD.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_BrowseDxD.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_BrowseDxD.Location = New System.Drawing.Point(310, 22)
        Me.btn_BrowseDxD.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_BrowseDxD.Name = "btn_BrowseDxD"
        Me.btn_BrowseDxD.Size = New System.Drawing.Size(44, 28)
        Me.btn_BrowseDxD.TabIndex = 2
        Me.btn_BrowseDxD.Text = "..."
        Me.btn_BrowseDxD.UseVisualStyleBackColor = False
        '
        'gb_Options
        '
        Me.gb_Options.Controls.Add(Me.chkbox_RebuildParts)
        Me.gb_Options.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_Options.Enabled = False
        Me.gb_Options.Location = New System.Drawing.Point(0, 580)
        Me.gb_Options.Margin = New System.Windows.Forms.Padding(4)
        Me.gb_Options.Name = "gb_Options"
        Me.gb_Options.Padding = New System.Windows.Forms.Padding(4)
        Me.gb_Options.Size = New System.Drawing.Size(364, 63)
        Me.gb_Options.TabIndex = 0
        Me.gb_Options.TabStop = False
        Me.gb_Options.Text = "Read Options"
        '
        'chkbox_RebuildParts
        '
        Me.chkbox_RebuildParts.AutoSize = True
        Me.chkbox_RebuildParts.Location = New System.Drawing.Point(13, 28)
        Me.chkbox_RebuildParts.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_RebuildParts.Name = "chkbox_RebuildParts"
        Me.chkbox_RebuildParts.Size = New System.Drawing.Size(175, 21)
        Me.chkbox_RebuildParts.TabIndex = 5
        Me.chkbox_RebuildParts.Text = "Rebuild duplicate parts"
        Me.chkbox_RebuildParts.UseVisualStyleBackColor = True
        '
        'tv_Parts
        '
        Me.tv_Parts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Parts.Location = New System.Drawing.Point(0, 0)
        Me.tv_Parts.Margin = New System.Windows.Forms.Padding(4)
        Me.tv_Parts.Name = "tv_Parts"
        Me.tv_Parts.Size = New System.Drawing.Size(352, 425)
        Me.tv_Parts.TabIndex = 0
        '
        'plBuildOptions
        '
        Me.plBuildOptions.Controls.Add(Me.btn_Create)
        Me.plBuildOptions.Controls.Add(Me.gbAction)
        Me.plBuildOptions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.plBuildOptions.Location = New System.Drawing.Point(0, 425)
        Me.plBuildOptions.Name = "plBuildOptions"
        Me.plBuildOptions.Size = New System.Drawing.Size(352, 218)
        Me.plBuildOptions.TabIndex = 1
        Me.plBuildOptions.Visible = False
        '
        'btn_Create
        '
        Me.btn_Create.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btn_Create.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Create.Location = New System.Drawing.Point(186, 16)
        Me.btn_Create.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_Create.Name = "btn_Create"
        Me.btn_Create.Size = New System.Drawing.Size(153, 28)
        Me.btn_Create.TabIndex = 24
        Me.btn_Create.Text = "Process"
        Me.btn_Create.UseVisualStyleBackColor = False
        '
        'gbAction
        '
        Me.gbAction.Controls.Add(Me.chkboxMultiThread)
        Me.gbAction.Controls.Add(Me.chkbox_RefDesPartitions)
        Me.gbAction.Controls.Add(Me.chkbox_SaveEachPart)
        Me.gbAction.Controls.Add(Me.chkbox_GetHeight)
        Me.gbAction.Controls.Add(Me.chkbox_RemoveIncomplete)
        Me.gbAction.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbAction.Location = New System.Drawing.Point(0, 48)
        Me.gbAction.Margin = New System.Windows.Forms.Padding(4)
        Me.gbAction.Name = "gbAction"
        Me.gbAction.Padding = New System.Windows.Forms.Padding(4)
        Me.gbAction.Size = New System.Drawing.Size(352, 170)
        Me.gbAction.TabIndex = 1
        Me.gbAction.TabStop = False
        Me.gbAction.Text = "Build Options"
        '
        'chkboxMultiThread
        '
        Me.chkboxMultiThread.AutoSize = True
        Me.chkboxMultiThread.Checked = True
        Me.chkboxMultiThread.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxMultiThread.Location = New System.Drawing.Point(17, 23)
        Me.chkboxMultiThread.Margin = New System.Windows.Forms.Padding(4)
        Me.chkboxMultiThread.Name = "chkboxMultiThread"
        Me.chkboxMultiThread.Size = New System.Drawing.Size(167, 21)
        Me.chkboxMultiThread.TabIndex = 6
        Me.chkboxMultiThread.Text = "Enable Multithreading"
        Me.chkboxMultiThread.UseVisualStyleBackColor = True
        '
        'chkbox_RefDesPartitions
        '
        Me.chkbox_RefDesPartitions.AutoSize = True
        Me.chkbox_RefDesPartitions.Location = New System.Drawing.Point(17, 52)
        Me.chkbox_RefDesPartitions.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_RefDesPartitions.Name = "chkbox_RefDesPartitions"
        Me.chkbox_RefDesPartitions.Size = New System.Drawing.Size(203, 21)
        Me.chkbox_RefDesPartitions.TabIndex = 0
        Me.chkbox_RefDesPartitions.Text = "Create Partitions by Refdes"
        Me.chkbox_RefDesPartitions.UseVisualStyleBackColor = True
        '
        'chkbox_SaveEachPart
        '
        Me.chkbox_SaveEachPart.AutoSize = True
        Me.chkbox_SaveEachPart.Location = New System.Drawing.Point(17, 135)
        Me.chkbox_SaveEachPart.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_SaveEachPart.Name = "chkbox_SaveEachPart"
        Me.chkbox_SaveEachPart.Size = New System.Drawing.Size(245, 21)
        Me.chkbox_SaveEachPart.TabIndex = 4
        Me.chkbox_SaveEachPart.Text = "Save after building a part (Slower)"
        Me.chkbox_SaveEachPart.UseVisualStyleBackColor = True
        '
        'chkbox_GetHeight
        '
        Me.chkbox_GetHeight.AutoSize = True
        Me.chkbox_GetHeight.Enabled = False
        Me.chkbox_GetHeight.Location = New System.Drawing.Point(17, 79)
        Me.chkbox_GetHeight.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_GetHeight.Name = "chkbox_GetHeight"
        Me.chkbox_GetHeight.Size = New System.Drawing.Size(268, 21)
        Me.chkbox_GetHeight.TabIndex = 1
        Me.chkbox_GetHeight.Text = "Get height from cell placement outline"
        Me.chkbox_GetHeight.UseVisualStyleBackColor = True
        '
        'chkbox_RemoveIncomplete
        '
        Me.chkbox_RemoveIncomplete.AutoSize = True
        Me.chkbox_RemoveIncomplete.Checked = True
        Me.chkbox_RemoveIncomplete.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_RemoveIncomplete.Location = New System.Drawing.Point(17, 107)
        Me.chkbox_RemoveIncomplete.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_RemoveIncomplete.Name = "chkbox_RemoveIncomplete"
        Me.chkbox_RemoveIncomplete.Size = New System.Drawing.Size(190, 21)
        Me.chkbox_RemoveIncomplete.TabIndex = 2
        Me.chkbox_RemoveIncomplete.Text = "Remove incomplete parts"
        Me.chkbox_RemoveIncomplete.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.tsm_Threads, Me.tsm_Exp, Me.tsm_DxD, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 643)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(721, 25)
        Me.StatusStrip1.TabIndex = 99
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(631, 20)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Browse to a schematic or PCB"
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tsm_Threads
        '
        Me.tsm_Threads.BackColor = System.Drawing.Color.Transparent
        Me.tsm_Threads.BorderSides = CType((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsm_Threads.Name = "tsm_Threads"
        Me.tsm_Threads.Size = New System.Drawing.Size(125, 24)
        Me.tsm_Threads.Text = "Active Threads: 1"
        Me.tsm_Threads.Visible = False
        '
        'tsm_Exp
        '
        Me.tsm_Exp.BackColor = System.Drawing.Color.Transparent
        Me.tsm_Exp.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.tsm_Exp.Name = "tsm_Exp"
        Me.tsm_Exp.Size = New System.Drawing.Size(37, 24)
        Me.tsm_Exp.Text = "Exp"
        Me.tsm_Exp.Visible = False
        '
        'tsm_DxD
        '
        Me.tsm_DxD.BackColor = System.Drawing.Color.Transparent
        Me.tsm_DxD.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.tsm_DxD.Name = "tsm_DxD"
        Me.tsm_DxD.Size = New System.Drawing.Size(42, 24)
        Me.tsm_DxD.Text = "DxD"
        Me.tsm_DxD.Visible = False
        '
        'WaitGif
        '
        Me.WaitGif.BackColor = System.Drawing.Color.Transparent
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 20)
        '
        'frmBuildPDBfromProject
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(721, 668)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmBuildPDBfromProject"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Build PDB from DxD/EE Project"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.gb_Layout.ResumeLayout(False)
        Me.gb_Layout.PerformLayout()
        Me.DxDbox.ResumeLayout(False)
        Me.DxDbox.PerformLayout()
        Me.gb_Options.ResumeLayout(False)
        Me.gb_Options.PerformLayout()
        Me.plBuildOptions.ResumeLayout(False)
        Me.gbAction.ResumeLayout(False)
        Me.gbAction.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gb_Options As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_SaveEachPart As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_RemoveIncomplete As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_GetHeight As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_RefDesPartitions As System.Windows.Forms.CheckBox
    Friend WithEvents tv_Parts As System.Windows.Forms.TreeView
    Friend WithEvents gb_Layout As System.Windows.Forms.GroupBox
    Friend WithEvents btn_BrowseExp As System.Windows.Forms.Button
    Friend WithEvents tbox_PCB As System.Windows.Forms.TextBox
    Friend WithEvents DxDbox As System.Windows.Forms.GroupBox
    Friend WithEvents tbox_DxD As System.Windows.Forms.TextBox
    Friend WithEvents btn_BrowseDxD As System.Windows.Forms.Button
    Friend WithEvents btn_Read As System.Windows.Forms.Button
    Friend WithEvents gbAction As System.Windows.Forms.GroupBox
    Friend WithEvents btn_Create As System.Windows.Forms.Button
    Friend WithEvents chkbox_RebuildParts As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxMultiThread As System.Windows.Forms.CheckBox
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ts_Status As ToolStripStatusLabel
    Friend WithEvents tsm_Threads As ToolStripStatusLabel
    Friend WithEvents WaitGif As ToolStripStatusLabel
    Friend WithEvents tsm_Exp As ToolStripStatusLabel
    Friend WithEvents tsm_DxD As ToolStripStatusLabel
    Friend WithEvents plBuildOptions As Panel
End Class
