<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBuild_PDB_from_Mapping
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
        Me.gb_Options = New System.Windows.Forms.GroupBox()
        Me.chkbox_RebuildParts = New System.Windows.Forms.CheckBox()
        Me.chkbox_RemoveIncomplete = New System.Windows.Forms.CheckBox()
        Me.chkbox_GetHeight = New System.Windows.Forms.CheckBox()
        Me.chkbox_RefDesPartitions = New System.Windows.Forms.CheckBox()
        Me.btnBuild = New System.Windows.Forms.Button()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tv_Parts = New System.Windows.Forms.TreeView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gb_ExcelInfo = New System.Windows.Forms.GroupBox()
        Me.lbl_Excel = New System.Windows.Forms.Label()
        Me.tbox_XMLMapping = New System.Windows.Forms.TextBox()
        Me.btn_BrowseXMLMapping = New System.Windows.Forms.Button()
        Me.gb_Options.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.gb_ExcelInfo.SuspendLayout()
        Me.SuspendLayout()
        '
        'gb_Options
        '
        Me.gb_Options.Controls.Add(Me.chkbox_RebuildParts)
        Me.gb_Options.Controls.Add(Me.chkbox_RemoveIncomplete)
        Me.gb_Options.Controls.Add(Me.chkbox_GetHeight)
        Me.gb_Options.Controls.Add(Me.chkbox_RefDesPartitions)
        Me.gb_Options.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_Options.Location = New System.Drawing.Point(0, 321)
        Me.gb_Options.Name = "gb_Options"
        Me.gb_Options.Size = New System.Drawing.Size(264, 115)
        Me.gb_Options.TabIndex = 0
        Me.gb_Options.TabStop = False
        Me.gb_Options.Text = "Options"
        '
        'chkbox_RebuildParts
        '
        Me.chkbox_RebuildParts.AutoSize = True
        Me.chkbox_RebuildParts.Location = New System.Drawing.Point(12, 88)
        Me.chkbox_RebuildParts.Name = "chkbox_RebuildParts"
        Me.chkbox_RebuildParts.Size = New System.Drawing.Size(134, 17)
        Me.chkbox_RebuildParts.TabIndex = 3
        Me.chkbox_RebuildParts.Text = "Rebuild duplicate parts"
        Me.chkbox_RebuildParts.UseVisualStyleBackColor = True
        '
        'chkbox_RemoveIncomplete
        '
        Me.chkbox_RemoveIncomplete.AutoSize = True
        Me.chkbox_RemoveIncomplete.Checked = True
        Me.chkbox_RemoveIncomplete.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_RemoveIncomplete.Location = New System.Drawing.Point(12, 65)
        Me.chkbox_RemoveIncomplete.Name = "chkbox_RemoveIncomplete"
        Me.chkbox_RemoveIncomplete.Size = New System.Drawing.Size(146, 17)
        Me.chkbox_RemoveIncomplete.TabIndex = 2
        Me.chkbox_RemoveIncomplete.Text = "Remove incomplete parts"
        Me.chkbox_RemoveIncomplete.UseVisualStyleBackColor = True
        '
        'chkbox_GetHeight
        '
        Me.chkbox_GetHeight.AutoSize = True
        Me.chkbox_GetHeight.Location = New System.Drawing.Point(12, 42)
        Me.chkbox_GetHeight.Name = "chkbox_GetHeight"
        Me.chkbox_GetHeight.Size = New System.Drawing.Size(203, 17)
        Me.chkbox_GetHeight.TabIndex = 1
        Me.chkbox_GetHeight.Text = "Get height from cell placement outline"
        Me.chkbox_GetHeight.UseVisualStyleBackColor = True
        '
        'chkbox_RefDesPartitions
        '
        Me.chkbox_RefDesPartitions.AutoSize = True
        Me.chkbox_RefDesPartitions.Location = New System.Drawing.Point(12, 19)
        Me.chkbox_RefDesPartitions.Name = "chkbox_RefDesPartitions"
        Me.chkbox_RefDesPartitions.Size = New System.Drawing.Size(154, 17)
        Me.chkbox_RefDesPartitions.TabIndex = 0
        Me.chkbox_RefDesPartitions.Text = "Create Partitions by Refdes"
        Me.chkbox_RefDesPartitions.UseVisualStyleBackColor = True
        '
        'btnBuild
        '
        Me.btnBuild.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBuild.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnBuild.Enabled = False
        Me.btnBuild.Location = New System.Drawing.Point(172, 282)
        Me.btnBuild.Name = "btnBuild"
        Me.btnBuild.Size = New System.Drawing.Size(75, 23)
        Me.btnBuild.TabIndex = 93
        Me.btnBuild.Text = "Process"
        Me.btnBuild.UseVisualStyleBackColor = False
        '
        'btnRead
        '
        Me.btnRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRead.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRead.Enabled = False
        Me.btnRead.Location = New System.Drawing.Point(172, 84)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(75, 23)
        Me.btnRead.TabIndex = 94
        Me.btnRead.Text = "Read"
        Me.btnRead.UseVisualStyleBackColor = False
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(440, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Browse to XML Mapping to being..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 436)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(525, 22)
        Me.StatusStrip1.TabIndex = 96
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'WaitGif
        '
        Me.WaitGif.BackColor = System.Drawing.Color.Transparent
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'tv_Parts
        '
        Me.tv_Parts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Parts.Location = New System.Drawing.Point(0, 0)
        Me.tv_Parts.Name = "tv_Parts"
        Me.tv_Parts.Size = New System.Drawing.Size(257, 436)
        Me.tv_Parts.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_ExcelInfo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_Options)
        Me.SplitContainer1.Panel1MinSize = 226
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tv_Parts)
        Me.SplitContainer1.Panel2MinSize = 226
        Me.SplitContainer1.Size = New System.Drawing.Size(525, 436)
        Me.SplitContainer1.SplitterDistance = 264
        Me.SplitContainer1.TabIndex = 95
        '
        'gb_ExcelInfo
        '
        Me.gb_ExcelInfo.Controls.Add(Me.lbl_Excel)
        Me.gb_ExcelInfo.Controls.Add(Me.tbox_XMLMapping)
        Me.gb_ExcelInfo.Controls.Add(Me.btnBuild)
        Me.gb_ExcelInfo.Controls.Add(Me.btn_BrowseXMLMapping)
        Me.gb_ExcelInfo.Controls.Add(Me.btnRead)
        Me.gb_ExcelInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gb_ExcelInfo.Location = New System.Drawing.Point(0, 0)
        Me.gb_ExcelInfo.Name = "gb_ExcelInfo"
        Me.gb_ExcelInfo.Size = New System.Drawing.Size(264, 321)
        Me.gb_ExcelInfo.TabIndex = 2
        Me.gb_ExcelInfo.TabStop = False
        Me.gb_ExcelInfo.Text = "Info"
        '
        'lbl_Excel
        '
        Me.lbl_Excel.AutoSize = True
        Me.lbl_Excel.Location = New System.Drawing.Point(12, 28)
        Me.lbl_Excel.Name = "lbl_Excel"
        Me.lbl_Excel.Size = New System.Drawing.Size(92, 13)
        Me.lbl_Excel.TabIndex = 85
        Me.lbl_Excel.Text = "Part File Location:"
        '
        'tbox_XMLMapping
        '
        Me.tbox_XMLMapping.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_XMLMapping.Location = New System.Drawing.Point(12, 44)
        Me.tbox_XMLMapping.Name = "tbox_XMLMapping"
        Me.tbox_XMLMapping.Size = New System.Drawing.Size(199, 20)
        Me.tbox_XMLMapping.TabIndex = 83
        '
        'btn_BrowseXMLMapping
        '
        Me.btn_BrowseXMLMapping.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_BrowseXMLMapping.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_BrowseXMLMapping.Location = New System.Drawing.Point(217, 44)
        Me.btn_BrowseXMLMapping.Name = "btn_BrowseXMLMapping"
        Me.btn_BrowseXMLMapping.Size = New System.Drawing.Size(30, 20)
        Me.btn_BrowseXMLMapping.TabIndex = 84
        Me.btn_BrowseXMLMapping.Text = "..."
        Me.btn_BrowseXMLMapping.UseVisualStyleBackColor = False
        '
        'frmBuild_PDB_from_Mapping
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(525, 458)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmBuild_PDB_from_Mapping"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Build PDB from XML Mapping"
        Me.gb_Options.ResumeLayout(false)
        Me.gb_Options.PerformLayout
        Me.StatusStrip1.ResumeLayout(false)
        Me.StatusStrip1.PerformLayout
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        Me.gb_ExcelInfo.ResumeLayout(false)
        Me.gb_ExcelInfo.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents gb_Options As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_RemoveIncomplete As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_GetHeight As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_RefDesPartitions As System.Windows.Forms.CheckBox
    Friend WithEvents btnBuild As System.Windows.Forms.Button
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tv_Parts As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gb_ExcelInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_Excel As System.Windows.Forms.Label
    Friend WithEvents tbox_XMLMapping As System.Windows.Forms.TextBox
    Friend WithEvents btn_BrowseXMLMapping As System.Windows.Forms.Button
    Friend WithEvents chkbox_RebuildParts As System.Windows.Forms.CheckBox
End Class
