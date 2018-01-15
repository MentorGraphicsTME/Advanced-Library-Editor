<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmChangeOutlineStrokes
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkSilkscreen = New System.Windows.Forms.CheckBox()
        Me.chkAssembly = New System.Windows.Forms.CheckBox()
        Me.chkPlacement = New System.Windows.Forms.CheckBox()
        Me.gbStroke = New System.Windows.Forms.GroupBox()
        Me.chkbox_FindAndReplace = New System.Windows.Forms.CheckBox()
        Me.tbox_New = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbox_Original = New System.Windows.Forms.TextBox()
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.tv_Cells = New System.Windows.Forms.TreeView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.plMain = New System.Windows.Forms.Panel()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ComboBox2 = New System.Windows.Forms.ComboBox()
        Me.GroupBox1.SuspendLayout()
        Me.gbStroke.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.plMain.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkSilkscreen)
        Me.GroupBox1.Controls.Add(Me.chkAssembly)
        Me.GroupBox1.Controls.Add(Me.chkPlacement)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox1.Location = New System.Drawing.Point(261, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(171, 95)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Outlines to Change"
        '
        'chkSilkscreen
        '
        Me.chkSilkscreen.AutoSize = True
        Me.chkSilkscreen.Location = New System.Drawing.Point(7, 68)
        Me.chkSilkscreen.Name = "chkSilkscreen"
        Me.chkSilkscreen.Size = New System.Drawing.Size(75, 17)
        Me.chkSilkscreen.TabIndex = 2
        Me.chkSilkscreen.Text = "Silkscreen"
        Me.chkSilkscreen.UseVisualStyleBackColor = True
        '
        'chkAssembly
        '
        Me.chkAssembly.AutoSize = True
        Me.chkAssembly.Location = New System.Drawing.Point(7, 44)
        Me.chkAssembly.Name = "chkAssembly"
        Me.chkAssembly.Size = New System.Drawing.Size(70, 17)
        Me.chkAssembly.TabIndex = 1
        Me.chkAssembly.Text = "Assembly"
        Me.chkAssembly.UseVisualStyleBackColor = True
        '
        'chkPlacement
        '
        Me.chkPlacement.AutoSize = True
        Me.chkPlacement.Location = New System.Drawing.Point(7, 20)
        Me.chkPlacement.Name = "chkPlacement"
        Me.chkPlacement.Size = New System.Drawing.Size(76, 17)
        Me.chkPlacement.TabIndex = 0
        Me.chkPlacement.Text = "Placement"
        Me.chkPlacement.UseVisualStyleBackColor = True
        '
        'gbStroke
        '
        Me.gbStroke.Controls.Add(Me.ComboBox2)
        Me.gbStroke.Controls.Add(Me.ComboBox1)
        Me.gbStroke.Controls.Add(Me.chkbox_FindAndReplace)
        Me.gbStroke.Controls.Add(Me.tbox_New)
        Me.gbStroke.Controls.Add(Me.Label2)
        Me.gbStroke.Controls.Add(Me.tbox_Original)
        Me.gbStroke.Dock = System.Windows.Forms.DockStyle.Top
        Me.gbStroke.Location = New System.Drawing.Point(261, 95)
        Me.gbStroke.Name = "gbStroke"
        Me.gbStroke.Size = New System.Drawing.Size(171, 361)
        Me.gbStroke.TabIndex = 1
        Me.gbStroke.TabStop = False
        Me.gbStroke.Text = "Width"
        '
        'chkbox_FindAndReplace
        '
        Me.chkbox_FindAndReplace.AutoSize = True
        Me.chkbox_FindAndReplace.Checked = True
        Me.chkbox_FindAndReplace.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_FindAndReplace.Location = New System.Drawing.Point(10, 23)
        Me.chkbox_FindAndReplace.Name = "chkbox_FindAndReplace"
        Me.chkbox_FindAndReplace.Size = New System.Drawing.Size(113, 17)
        Me.chkbox_FindAndReplace.TabIndex = 5
        Me.chkbox_FindAndReplace.Text = "Find and Replace:"
        Me.chkbox_FindAndReplace.UseVisualStyleBackColor = True
        '
        'tbox_New
        '
        Me.tbox_New.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_New.Location = New System.Drawing.Point(7, 92)
        Me.tbox_New.Name = "tbox_New"
        Me.tbox_New.Size = New System.Drawing.Size(101, 20)
        Me.tbox_New.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 76)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(87, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "With New Value:"
        '
        'tbox_Original
        '
        Me.tbox_Original.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_Original.Location = New System.Drawing.Point(10, 40)
        Me.tbox_Original.Name = "tbox_Original"
        Me.tbox_Original.Size = New System.Drawing.Size(98, 20)
        Me.tbox_Original.TabIndex = 1
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnUpdate.Location = New System.Drawing.Point(345, 477)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(75, 23)
        Me.btnUpdate.TabIndex = 2
        Me.btnUpdate.Text = "Update"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.tv_Cells)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Left
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(261, 515)
        Me.GroupBox2.TabIndex = 28
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Cells"
        '
        'tv_Cells
        '
        Me.tv_Cells.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv_Cells.CheckBoxes = True
        Me.tv_Cells.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Cells.Location = New System.Drawing.Point(3, 16)
        Me.tv_Cells.Name = "tv_Cells"
        Me.tv_Cells.Size = New System.Drawing.Size(255, 496)
        Me.tv_Cells.TabIndex = 24
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 515)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(432, 22)
        Me.StatusStrip1.TabIndex = 29
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(347, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Select some cells to begin..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WaitGif
        '
        Me.WaitGif.BackColor = System.Drawing.Color.Transparent
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'plMain
        '
        Me.plMain.Controls.Add(Me.btnUpdate)
        Me.plMain.Controls.Add(Me.gbStroke)
        Me.plMain.Controls.Add(Me.GroupBox1)
        Me.plMain.Controls.Add(Me.GroupBox2)
        Me.plMain.Dock = System.Windows.Forms.DockStyle.Fill
        Me.plMain.Location = New System.Drawing.Point(0, 0)
        Me.plMain.Name = "plMain"
        Me.plMain.Size = New System.Drawing.Size(432, 515)
        Me.plMain.TabIndex = 30
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Items.AddRange(New Object() {"IN", "TH", "MM", "UM"})
        Me.ComboBox1.Location = New System.Drawing.Point(114, 39)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(44, 21)
        Me.ComboBox1.TabIndex = 6
        '
        'ComboBox2
        '
        Me.ComboBox2.FormattingEnabled = True
        Me.ComboBox2.Items.AddRange(New Object() {"IN", "TH", "MM", "UM"})
        Me.ComboBox2.Location = New System.Drawing.Point(115, 91)
        Me.ComboBox2.Name = "ComboBox2"
        Me.ComboBox2.Size = New System.Drawing.Size(43, 21)
        Me.ComboBox2.TabIndex = 7
        '
        'frmChangeOutlineStrokes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(432, 537)
        Me.Controls.Add(Me.plMain)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MinimumSize = New System.Drawing.Size(434, 558)
        Me.Name = "frmChangeOutlineStrokes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Outline Strokes"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.gbStroke.ResumeLayout(False)
        Me.gbStroke.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.plMain.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkSilkscreen As System.Windows.Forms.CheckBox
    Friend WithEvents chkAssembly As System.Windows.Forms.CheckBox
    Friend WithEvents chkPlacement As System.Windows.Forms.CheckBox
    Friend WithEvents gbStroke As System.Windows.Forms.GroupBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents tv_Cells As System.Windows.Forms.TreeView
    Friend WithEvents chkbox_FindAndReplace As System.Windows.Forms.CheckBox
    Friend WithEvents tbox_New As System.Windows.Forms.TextBox
    Friend WithEvents tbox_Original As System.Windows.Forms.TextBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents plMain As System.Windows.Forms.Panel
    Friend WithEvents ComboBox2 As ComboBox
    Friend WithEvents ComboBox1 As ComboBox
End Class
