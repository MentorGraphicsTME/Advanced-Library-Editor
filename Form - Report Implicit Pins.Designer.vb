<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCheckImplicitPins
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmCheckImplicitPins))
        Me.lv_SymInfo = New System.Windows.Forms.ListView()
        Me.cl_Partition = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cl_PartNumber = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader1 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.ColumnHeader3 = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.rb_Prefix = New System.Windows.Forms.RadioButton()
        Me.rb_Ext = New System.Windows.Forms.RadioButton()
        Me.plMarkParts = New System.Windows.Forms.Panel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbox_PartTag = New System.Windows.Forms.TextBox()
        Me.btnMarkParts = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chklistbox_PDBPartitions = New System.Windows.Forms.CheckedListBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnScan = New System.Windows.Forms.Button()
        Me.plMarkParts.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lv_SymInfo
        '
        Me.lv_SymInfo.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.cl_Partition, Me.cl_PartNumber, Me.ColumnHeader1, Me.ColumnHeader3})
        Me.lv_SymInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lv_SymInfo.FullRowSelect = True
        Me.lv_SymInfo.Location = New System.Drawing.Point(266, 0)
        Me.lv_SymInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.lv_SymInfo.Name = "lv_SymInfo"
        Me.lv_SymInfo.Size = New System.Drawing.Size(739, 617)
        Me.lv_SymInfo.TabIndex = 6
        Me.lv_SymInfo.UseCompatibleStateImageBehavior = False
        Me.lv_SymInfo.View = System.Windows.Forms.View.Details
        '
        'cl_Partition
        '
        Me.cl_Partition.Text = "Partition"
        Me.cl_Partition.Width = 100
        '
        'cl_PartNumber
        '
        Me.cl_PartNumber.Text = "Part Number"
        Me.cl_PartNumber.Width = 150
        '
        'ColumnHeader1
        '
        Me.ColumnHeader1.Text = "No Connect"
        Me.ColumnHeader1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader1.Width = 143
        '
        'ColumnHeader3
        '
        Me.ColumnHeader3.Text = "Supply"
        Me.ColumnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.ColumnHeader3.Width = 135
        '
        'rb_Prefix
        '
        Me.rb_Prefix.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rb_Prefix.AutoSize = True
        Me.rb_Prefix.Checked = True
        Me.rb_Prefix.Location = New System.Drawing.Point(17, 9)
        Me.rb_Prefix.Margin = New System.Windows.Forms.Padding(4)
        Me.rb_Prefix.Name = "rb_Prefix"
        Me.rb_Prefix.Size = New System.Drawing.Size(151, 21)
        Me.rb_Prefix.TabIndex = 92
        Me.rb_Prefix.TabStop = True
        Me.rb_Prefix.Text = "Tag Part with Prefix"
        Me.rb_Prefix.UseVisualStyleBackColor = True
        '
        'rb_Ext
        '
        Me.rb_Ext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.rb_Ext.AutoSize = True
        Me.rb_Ext.Location = New System.Drawing.Point(17, 37)
        Me.rb_Ext.Margin = New System.Windows.Forms.Padding(4)
        Me.rb_Ext.Name = "rb_Ext"
        Me.rb_Ext.Size = New System.Drawing.Size(175, 21)
        Me.rb_Ext.TabIndex = 93
        Me.rb_Ext.Text = "Tax Part with Extension"
        Me.rb_Ext.UseVisualStyleBackColor = True
        '
        'plMarkParts
        '
        Me.plMarkParts.Controls.Add(Me.Label2)
        Me.plMarkParts.Controls.Add(Me.tbox_PartTag)
        Me.plMarkParts.Controls.Add(Me.btnMarkParts)
        Me.plMarkParts.Controls.Add(Me.rb_Ext)
        Me.plMarkParts.Controls.Add(Me.rb_Prefix)
        Me.plMarkParts.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.plMarkParts.Enabled = False
        Me.plMarkParts.Location = New System.Drawing.Point(266, 617)
        Me.plMarkParts.Margin = New System.Windows.Forms.Padding(4)
        Me.plMarkParts.Name = "plMarkParts"
        Me.plMarkParts.Size = New System.Drawing.Size(739, 71)
        Me.plMarkParts.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(202, 14)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(37, 17)
        Me.Label2.TabIndex = 97
        Me.Label2.Text = "Tag:"
        '
        'tbox_PartTag
        '
        Me.tbox_PartTag.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_PartTag.Location = New System.Drawing.Point(205, 34)
        Me.tbox_PartTag.Margin = New System.Windows.Forms.Padding(4)
        Me.tbox_PartTag.Name = "tbox_PartTag"
        Me.tbox_PartTag.Size = New System.Drawing.Size(355, 22)
        Me.tbox_PartTag.TabIndex = 96
        '
        'btnMarkParts
        '
        Me.btnMarkParts.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMarkParts.Location = New System.Drawing.Point(573, 8)
        Me.btnMarkParts.Margin = New System.Windows.Forms.Padding(4)
        Me.btnMarkParts.Name = "btnMarkParts"
        Me.btnMarkParts.Size = New System.Drawing.Size(153, 50)
        Me.btnMarkParts.TabIndex = 94
        Me.btnMarkParts.Text = "Tag Implicit Parts"
        Me.btnMarkParts.UseVisualStyleBackColor = True
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 688)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(1005, 25)
        Me.StatusStrip1.TabIndex = 100
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(915, 20)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Browse to a schematic or PCB"
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chklistbox_PDBPartitions)
        Me.GroupBox2.Controls.Add(Me.Panel2)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(266, 688)
        Me.GroupBox2.TabIndex = 101
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
        Me.chklistbox_PDBPartitions.Location = New System.Drawing.Point(4, 19)
        Me.chklistbox_PDBPartitions.Margin = New System.Windows.Forms.Padding(4)
        Me.chklistbox_PDBPartitions.Name = "chklistbox_PDBPartitions"
        Me.chklistbox_PDBPartitions.Size = New System.Drawing.Size(258, 622)
        Me.chklistbox_PDBPartitions.TabIndex = 75
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnScan)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(4, 641)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(258, 43)
        Me.Panel2.TabIndex = 76
        Me.Panel2.Visible = False
        '
        'btnScan
        '
        Me.btnScan.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnScan.Location = New System.Drawing.Point(154, 4)
        Me.btnScan.Margin = New System.Windows.Forms.Padding(4)
        Me.btnScan.Name = "btnScan"
        Me.btnScan.Size = New System.Drawing.Size(100, 28)
        Me.btnScan.TabIndex = 87
        Me.btnScan.Text = "Scan"
        Me.btnScan.UseVisualStyleBackColor = True
        '
        'frmCheckImplicitPins
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLightLight
        Me.ClientSize = New System.Drawing.Size(1005, 713)
        Me.Controls.Add(Me.lv_SymInfo)
        Me.Controls.Add(Me.plMarkParts)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmCheckImplicitPins"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Report/Tag Implicit Pin Parts"
        Me.plMarkParts.ResumeLayout(False)
        Me.plMarkParts.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lv_SymInfo As System.Windows.Forms.ListView
    Friend WithEvents cl_PartNumber As System.Windows.Forms.ColumnHeader
    Friend WithEvents cl_Partition As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader1 As System.Windows.Forms.ColumnHeader
    Friend WithEvents ColumnHeader3 As System.Windows.Forms.ColumnHeader
    Friend WithEvents rb_Prefix As System.Windows.Forms.RadioButton
    Friend WithEvents rb_Ext As System.Windows.Forms.RadioButton
    Friend WithEvents plMarkParts As System.Windows.Forms.Panel
    Friend WithEvents btnMarkParts As System.Windows.Forms.Button
    Friend WithEvents tbox_PartTag As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ts_Status As ToolStripStatusLabel
    Friend WithEvents WaitGif As ToolStripStatusLabel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chklistbox_PDBPartitions As CheckedListBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents btnScan As System.Windows.Forms.Button
End Class
