<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmUpdateCellFonts
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
        Me.lblExample = New System.Windows.Forms.Label()
        Me.cbox_FontType = New System.Windows.Forms.ComboBox()
        Me.btnUpdateFont = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ts_WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chbMirrored = New System.Windows.Forms.CheckBox()
        Me.chbUnderline = New System.Windows.Forms.CheckBox()
        Me.chbItalic = New System.Windows.Forms.CheckBox()
        Me.chbBold = New System.Windows.Forms.CheckBox()
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.GroupBox4 = New System.Windows.Forms.GroupBox()
        Me.cbOrientation = New System.Windows.Forms.ComboBox()
        Me.GroupBox5 = New System.Windows.Forms.GroupBox()
        Me.cbox_HeightUnits = New System.Windows.Forms.ComboBox()
        Me.tbAspect = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbHeight = New System.Windows.Forms.TextBox()
        Me.gbUpdate = New System.Windows.Forms.GroupBox()
        Me.chkbox_Silkscreen = New System.Windows.Forms.CheckBox()
        Me.cbTextType = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.chklisbox_Partitions = New System.Windows.Forms.CheckedListBox()
        Me.StatusStrip1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.GroupBox5.SuspendLayout()
        Me.gbUpdate.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblExample
        '
        Me.lblExample.AutoSize = True
        Me.lblExample.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExample.Location = New System.Drawing.Point(6, 43)
        Me.lblExample.Name = "lblExample"
        Me.lblExample.Size = New System.Drawing.Size(127, 24)
        Me.lblExample.TabIndex = 83
        Me.lblExample.Text = "Example Text"
        '
        'cbox_FontType
        '
        Me.cbox_FontType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_FontType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_FontType.FormattingEnabled = True
        Me.cbox_FontType.Location = New System.Drawing.Point(6, 19)
        Me.cbox_FontType.Name = "cbox_FontType"
        Me.cbox_FontType.Size = New System.Drawing.Size(254, 21)
        Me.cbox_FontType.TabIndex = 81
        '
        'btnUpdateFont
        '
        Me.btnUpdateFont.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnUpdateFont.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnUpdateFont.Enabled = False
        Me.btnUpdateFont.Location = New System.Drawing.Point(172, 290)
        Me.btnUpdateFont.Name = "btnUpdateFont"
        Me.btnUpdateFont.Size = New System.Drawing.Size(103, 23)
        Me.btnUpdateFont.TabIndex = 80
        Me.btnUpdateFont.Text = "Update Text"
        Me.btnUpdateFont.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.ts_WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 321)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(490, 22)
        Me.StatusStrip1.SizingGrip = False
        Me.StatusStrip1.TabIndex = 84
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(405, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'ts_WaitGif
        '
        Me.ts_WaitGif.Enabled = False
        Me.ts_WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.ts_WaitGif.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.ts_WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ts_WaitGif.Name = "ts_WaitGif"
        Me.ts_WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chbMirrored)
        Me.GroupBox1.Controls.Add(Me.chbUnderline)
        Me.GroupBox1.Controls.Add(Me.chbItalic)
        Me.GroupBox1.Controls.Add(Me.chbBold)
        Me.GroupBox1.Location = New System.Drawing.Point(6, 89)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(269, 43)
        Me.GroupBox1.TabIndex = 85
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Style"
        '
        'chbMirrored
        '
        Me.chbMirrored.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chbMirrored.AutoSize = True
        Me.chbMirrored.Location = New System.Drawing.Point(194, 19)
        Me.chbMirrored.Name = "chbMirrored"
        Me.chbMirrored.Size = New System.Drawing.Size(64, 17)
        Me.chbMirrored.TabIndex = 3
        Me.chbMirrored.Text = "Mirrored"
        Me.chbMirrored.UseVisualStyleBackColor = True
        '
        'chbUnderline
        '
        Me.chbUnderline.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chbUnderline.AutoSize = True
        Me.chbUnderline.Location = New System.Drawing.Point(117, 19)
        Me.chbUnderline.Name = "chbUnderline"
        Me.chbUnderline.Size = New System.Drawing.Size(71, 17)
        Me.chbUnderline.TabIndex = 2
        Me.chbUnderline.Text = "Underline"
        Me.chbUnderline.UseVisualStyleBackColor = True
        '
        'chbItalic
        '
        Me.chbItalic.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chbItalic.AutoSize = True
        Me.chbItalic.Location = New System.Drawing.Point(63, 19)
        Me.chbItalic.Name = "chbItalic"
        Me.chbItalic.Size = New System.Drawing.Size(48, 17)
        Me.chbItalic.TabIndex = 1
        Me.chbItalic.Text = "Italic"
        Me.chbItalic.UseVisualStyleBackColor = True
        '
        'chbBold
        '
        Me.chbBold.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.chbBold.AutoSize = True
        Me.chbBold.Location = New System.Drawing.Point(10, 19)
        Me.chbBold.Name = "chbBold"
        Me.chbBold.Size = New System.Drawing.Size(47, 17)
        Me.chbBold.TabIndex = 0
        Me.chbBold.Text = "Bold"
        Me.chbBold.UseVisualStyleBackColor = True
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.cbox_FontType)
        Me.GroupBox3.Controls.Add(Me.lblExample)
        Me.GroupBox3.Location = New System.Drawing.Point(6, 12)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(269, 71)
        Me.GroupBox3.TabIndex = 87
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Font"
        '
        'GroupBox4
        '
        Me.GroupBox4.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.GroupBox4.Controls.Add(Me.cbOrientation)
        Me.GroupBox4.Location = New System.Drawing.Point(192, 138)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(83, 71)
        Me.GroupBox4.TabIndex = 88
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Orientation"
        '
        'cbOrientation
        '
        Me.cbOrientation.AutoCompleteCustomSource.AddRange(New String() {"0", "90", "180", "270"})
        Me.cbOrientation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbOrientation.FormattingEnabled = True
        Me.cbOrientation.Items.AddRange(New Object() {"0", "90", "180", "270"})
        Me.cbOrientation.Location = New System.Drawing.Point(7, 25)
        Me.cbOrientation.Name = "cbOrientation"
        Me.cbOrientation.Size = New System.Drawing.Size(69, 21)
        Me.cbOrientation.TabIndex = 0
        '
        'GroupBox5
        '
        Me.GroupBox5.Controls.Add(Me.cbox_HeightUnits)
        Me.GroupBox5.Controls.Add(Me.tbAspect)
        Me.GroupBox5.Controls.Add(Me.Label3)
        Me.GroupBox5.Controls.Add(Me.Label2)
        Me.GroupBox5.Controls.Add(Me.tbHeight)
        Me.GroupBox5.Location = New System.Drawing.Point(6, 138)
        Me.GroupBox5.Name = "GroupBox5"
        Me.GroupBox5.Size = New System.Drawing.Size(180, 71)
        Me.GroupBox5.TabIndex = 89
        Me.GroupBox5.TabStop = False
        Me.GroupBox5.Text = "Size"
        '
        'cbox_HeightUnits
        '
        Me.cbox_HeightUnits.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_HeightUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_HeightUnits.Enabled = False
        Me.cbox_HeightUnits.FormattingEnabled = True
        Me.cbox_HeightUnits.Items.AddRange(New Object() {"in", "th", "mm", "um"})
        Me.cbox_HeightUnits.Location = New System.Drawing.Point(125, 16)
        Me.cbox_HeightUnits.Name = "cbox_HeightUnits"
        Me.cbox_HeightUnits.Size = New System.Drawing.Size(40, 21)
        Me.cbox_HeightUnits.TabIndex = 54
        '
        'tbAspect
        '
        Me.tbAspect.Location = New System.Drawing.Point(83, 42)
        Me.tbAspect.Name = "tbAspect"
        Me.tbAspect.Size = New System.Drawing.Size(82, 20)
        Me.tbAspect.TabIndex = 3
        Me.tbAspect.Text = "1"
        Me.tbAspect.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(7, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Aspect Ratio:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 19)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Height:"
        '
        'tbHeight
        '
        Me.tbHeight.Location = New System.Drawing.Point(83, 16)
        Me.tbHeight.Name = "tbHeight"
        Me.tbHeight.Size = New System.Drawing.Size(38, 20)
        Me.tbHeight.TabIndex = 0
        Me.tbHeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'gbUpdate
        '
        Me.gbUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gbUpdate.Controls.Add(Me.chkbox_Silkscreen)
        Me.gbUpdate.Controls.Add(Me.cbTextType)
        Me.gbUpdate.Controls.Add(Me.Label1)
        Me.gbUpdate.Location = New System.Drawing.Point(6, 215)
        Me.gbUpdate.Name = "gbUpdate"
        Me.gbUpdate.Size = New System.Drawing.Size(269, 69)
        Me.gbUpdate.TabIndex = 92
        Me.gbUpdate.TabStop = False
        Me.gbUpdate.Text = "Update Options"
        '
        'chkbox_Silkscreen
        '
        Me.chkbox_Silkscreen.AutoSize = True
        Me.chkbox_Silkscreen.Location = New System.Drawing.Point(165, 39)
        Me.chkbox_Silkscreen.Name = "chkbox_Silkscreen"
        Me.chkbox_Silkscreen.Size = New System.Drawing.Size(99, 17)
        Me.chkbox_Silkscreen.TabIndex = 85
        Me.chkbox_Silkscreen.Text = "Silkscreen Only"
        Me.chkbox_Silkscreen.UseVisualStyleBackColor = True
        '
        'cbTextType
        '
        Me.cbTextType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbTextType.AutoCompleteCustomSource.AddRange(New String() {"All ", "Generic", "Part Number", "Part Property", "Pin", "Ref Des"})
        Me.cbTextType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbTextType.FormattingEnabled = True
        Me.cbTextType.Items.AddRange(New Object() {"All", "Generic", "Part Number", "Part Property", "Pin Logical", "Pin Physical", "Pin User Defined", "Ref Des"})
        Me.cbTextType.Location = New System.Drawing.Point(10, 37)
        Me.cbTextType.Name = "cbTextType"
        Me.cbTextType.Size = New System.Drawing.Size(132, 21)
        Me.cbTextType.TabIndex = 84
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(63, 13)
        Me.Label1.TabIndex = 83
        Me.Label1.Text = "Text Types:"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chklisbox_Partitions)
        Me.GroupBox2.Location = New System.Drawing.Point(282, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(199, 301)
        Me.GroupBox2.TabIndex = 93
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Partitions"
        '
        'chklisbox_Partitions
        '
        Me.chklisbox_Partitions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklisbox_Partitions.CheckOnClick = True
        Me.chklisbox_Partitions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklisbox_Partitions.FormattingEnabled = True
        Me.chklisbox_Partitions.Location = New System.Drawing.Point(3, 16)
        Me.chklisbox_Partitions.Name = "chklisbox_Partitions"
        Me.chklisbox_Partitions.Size = New System.Drawing.Size(193, 282)
        Me.chklisbox_Partitions.TabIndex = 0
        '
        'frmUpdateCellFonts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(490, 343)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.gbUpdate)
        Me.Controls.Add(Me.GroupBox5)
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.btnUpdateFont)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmUpdateCellFonts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Text Style"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox5.ResumeLayout(False)
        Me.GroupBox5.PerformLayout()
        Me.gbUpdate.ResumeLayout(False)
        Me.gbUpdate.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblExample As System.Windows.Forms.Label
    Friend WithEvents cbox_FontType As System.Windows.Forms.ComboBox
    Friend WithEvents btnUpdateFont As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ts_WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chbMirrored As System.Windows.Forms.CheckBox
    Friend WithEvents chbUnderline As System.Windows.Forms.CheckBox
    Friend WithEvents chbItalic As System.Windows.Forms.CheckBox
    Friend WithEvents chbBold As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cbOrientation As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox5 As System.Windows.Forms.GroupBox
    Friend WithEvents tbHeight As System.Windows.Forms.TextBox
    Friend WithEvents gbUpdate As System.Windows.Forms.GroupBox
    Friend WithEvents cbTextType As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbAspect As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbox_HeightUnits As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chklisbox_Partitions As System.Windows.Forms.CheckedListBox
    Friend WithEvents chkbox_Silkscreen As System.Windows.Forms.CheckBox
End Class
