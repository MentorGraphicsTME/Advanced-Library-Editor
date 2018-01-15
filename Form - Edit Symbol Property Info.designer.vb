<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEdit_Property_Info
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
        Me.btn_VisibilityClear = New System.Windows.Forms.Button()
        Me.btn_AlignmentClear = New System.Windows.Forms.Button()
        Me.btn_ColorClear = New System.Windows.Forms.Button()
        Me.btn_SizeClear = New System.Windows.Forms.Button()
        Me.btn_FontClear = New System.Windows.Forms.Button()
        Me.chkbox_Automatic = New System.Windows.Forms.CheckBox()
        Me.btnColor = New System.Windows.Forms.Button()
        Me.lbl_FontColor = New System.Windows.Forms.Label()
        Me.lbl_Visibility = New System.Windows.Forms.Label()
        Me.cbox_Visibility = New System.Windows.Forms.ComboBox()
        Me.lbl_Alignment = New System.Windows.Forms.Label()
        Me.cbox_Alignment = New System.Windows.Forms.ComboBox()
        Me.lbl_FontSize = New System.Windows.Forms.Label()
        Me.cbox_Size = New System.Windows.Forms.ComboBox()
        Me.lbl_Font = New System.Windows.Forms.Label()
        Me.cbox_Font = New System.Windows.Forms.ComboBox()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.ColorDialog = New System.Windows.Forms.ColorDialog()
        Me.GroupBox1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btn_VisibilityClear)
        Me.GroupBox1.Controls.Add(Me.btn_AlignmentClear)
        Me.GroupBox1.Controls.Add(Me.btn_ColorClear)
        Me.GroupBox1.Controls.Add(Me.btn_SizeClear)
        Me.GroupBox1.Controls.Add(Me.btn_FontClear)
        Me.GroupBox1.Controls.Add(Me.chkbox_Automatic)
        Me.GroupBox1.Controls.Add(Me.btnColor)
        Me.GroupBox1.Controls.Add(Me.lbl_FontColor)
        Me.GroupBox1.Controls.Add(Me.lbl_Visibility)
        Me.GroupBox1.Controls.Add(Me.cbox_Visibility)
        Me.GroupBox1.Controls.Add(Me.lbl_Alignment)
        Me.GroupBox1.Controls.Add(Me.cbox_Alignment)
        Me.GroupBox1.Controls.Add(Me.lbl_FontSize)
        Me.GroupBox1.Controls.Add(Me.cbox_Size)
        Me.GroupBox1.Controls.Add(Me.lbl_Font)
        Me.GroupBox1.Controls.Add(Me.cbox_Font)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(224, 209)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Property Info"
        '
        'btn_VisibilityClear
        '
        Me.btn_VisibilityClear.Location = New System.Drawing.Point(195, 165)
        Me.btn_VisibilityClear.Name = "btn_VisibilityClear"
        Me.btn_VisibilityClear.Size = New System.Drawing.Size(21, 21)
        Me.btn_VisibilityClear.TabIndex = 30
        Me.btn_VisibilityClear.Text = "X"
        Me.btn_VisibilityClear.UseVisualStyleBackColor = True
        Me.btn_VisibilityClear.Visible = False
        '
        'btn_AlignmentClear
        '
        Me.btn_AlignmentClear.Location = New System.Drawing.Point(195, 138)
        Me.btn_AlignmentClear.Name = "btn_AlignmentClear"
        Me.btn_AlignmentClear.Size = New System.Drawing.Size(21, 21)
        Me.btn_AlignmentClear.TabIndex = 23
        Me.btn_AlignmentClear.Text = "X"
        Me.btn_AlignmentClear.UseVisualStyleBackColor = True
        Me.btn_AlignmentClear.Visible = False
        '
        'btn_ColorClear
        '
        Me.btn_ColorClear.Location = New System.Drawing.Point(195, 76)
        Me.btn_ColorClear.Name = "btn_ColorClear"
        Me.btn_ColorClear.Size = New System.Drawing.Size(21, 21)
        Me.btn_ColorClear.TabIndex = 22
        Me.btn_ColorClear.Text = "X"
        Me.btn_ColorClear.UseVisualStyleBackColor = True
        Me.btn_ColorClear.Visible = False
        '
        'btn_SizeClear
        '
        Me.btn_SizeClear.Location = New System.Drawing.Point(195, 47)
        Me.btn_SizeClear.Name = "btn_SizeClear"
        Me.btn_SizeClear.Size = New System.Drawing.Size(21, 21)
        Me.btn_SizeClear.TabIndex = 21
        Me.btn_SizeClear.Text = "X"
        Me.btn_SizeClear.UseVisualStyleBackColor = True
        Me.btn_SizeClear.Visible = False
        '
        'btn_FontClear
        '
        Me.btn_FontClear.Location = New System.Drawing.Point(195, 20)
        Me.btn_FontClear.Name = "btn_FontClear"
        Me.btn_FontClear.Size = New System.Drawing.Size(21, 21)
        Me.btn_FontClear.TabIndex = 20
        Me.btn_FontClear.Text = "X"
        Me.btn_FontClear.UseVisualStyleBackColor = True
        Me.btn_FontClear.Visible = False
        '
        'chkbox_Automatic
        '
        Me.chkbox_Automatic.AutoSize = True
        Me.chkbox_Automatic.Location = New System.Drawing.Point(68, 104)
        Me.chkbox_Automatic.Name = "chkbox_Automatic"
        Me.chkbox_Automatic.Size = New System.Drawing.Size(104, 17)
        Me.chkbox_Automatic.TabIndex = 19
        Me.chkbox_Automatic.Text = "Set to Automatic"
        Me.chkbox_Automatic.UseVisualStyleBackColor = True
        '
        'btnColor
        '
        Me.btnColor.Location = New System.Drawing.Point(67, 75)
        Me.btnColor.Name = "btnColor"
        Me.btnColor.Size = New System.Drawing.Size(122, 23)
        Me.btnColor.TabIndex = 18
        Me.btnColor.UseVisualStyleBackColor = True
        '
        'lbl_FontColor
        '
        Me.lbl_FontColor.AutoSize = True
        Me.lbl_FontColor.Location = New System.Drawing.Point(9, 80)
        Me.lbl_FontColor.Name = "lbl_FontColor"
        Me.lbl_FontColor.Size = New System.Drawing.Size(58, 13)
        Me.lbl_FontColor.TabIndex = 17
        Me.lbl_FontColor.Text = "Font Color:"
        '
        'lbl_Visibility
        '
        Me.lbl_Visibility.AutoSize = True
        Me.lbl_Visibility.Location = New System.Drawing.Point(9, 169)
        Me.lbl_Visibility.Name = "lbl_Visibility"
        Me.lbl_Visibility.Size = New System.Drawing.Size(46, 13)
        Me.lbl_Visibility.TabIndex = 14
        Me.lbl_Visibility.Text = "Visibility:"
        '
        'cbox_Visibility
        '
        Me.cbox_Visibility.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Visibility.FormattingEnabled = True
        Me.cbox_Visibility.Items.AddRange(New Object() {"All Hidden", "All Visible", "Name Visible", "Value Visible"})
        Me.cbox_Visibility.Location = New System.Drawing.Point(68, 165)
        Me.cbox_Visibility.Name = "cbox_Visibility"
        Me.cbox_Visibility.Size = New System.Drawing.Size(121, 21)
        Me.cbox_Visibility.TabIndex = 13
        '
        'lbl_Alignment
        '
        Me.lbl_Alignment.AutoSize = True
        Me.lbl_Alignment.Location = New System.Drawing.Point(9, 142)
        Me.lbl_Alignment.Name = "lbl_Alignment"
        Me.lbl_Alignment.Size = New System.Drawing.Size(56, 13)
        Me.lbl_Alignment.TabIndex = 12
        Me.lbl_Alignment.Text = "Alignment:"
        '
        'cbox_Alignment
        '
        Me.cbox_Alignment.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Alignment.FormattingEnabled = True
        Me.cbox_Alignment.Items.AddRange(New Object() {"Lower Center", "Lower Left", "Lower Right", "Middle Center", "Middle Left", "Middle Right", "Upper Center", "Upper Left", "Upper Right"})
        Me.cbox_Alignment.Location = New System.Drawing.Point(68, 138)
        Me.cbox_Alignment.Name = "cbox_Alignment"
        Me.cbox_Alignment.Size = New System.Drawing.Size(121, 21)
        Me.cbox_Alignment.TabIndex = 11
        '
        'lbl_FontSize
        '
        Me.lbl_FontSize.AutoSize = True
        Me.lbl_FontSize.Location = New System.Drawing.Point(9, 51)
        Me.lbl_FontSize.Name = "lbl_FontSize"
        Me.lbl_FontSize.Size = New System.Drawing.Size(54, 13)
        Me.lbl_FontSize.TabIndex = 10
        Me.lbl_FontSize.Text = "Font Size:"
        '
        'cbox_Size
        '
        Me.cbox_Size.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Size.FormattingEnabled = True
        Me.cbox_Size.Items.AddRange(New Object() {"6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "22", "24", "26", "28", "36", "48", "72"})
        Me.cbox_Size.Location = New System.Drawing.Point(68, 47)
        Me.cbox_Size.Name = "cbox_Size"
        Me.cbox_Size.Size = New System.Drawing.Size(121, 21)
        Me.cbox_Size.TabIndex = 9
        '
        'lbl_Font
        '
        Me.lbl_Font.AutoSize = True
        Me.lbl_Font.Location = New System.Drawing.Point(9, 24)
        Me.lbl_Font.Name = "lbl_Font"
        Me.lbl_Font.Size = New System.Drawing.Size(28, 13)
        Me.lbl_Font.TabIndex = 8
        Me.lbl_Font.Text = "Font"
        '
        'cbox_Font
        '
        Me.cbox_Font.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Font.FormattingEnabled = True
        Me.cbox_Font.Items.AddRange(New Object() {"Fixed", "Gothic", "Kanji", "Old English", "Plot", "Roman", "Roman Bold", "Roman Bold Italic", "Roman Italic", "Sans Serif", "Sans Serif Bold", "Script", "Script Bold"})
        Me.cbox_Font.Location = New System.Drawing.Point(68, 20)
        Me.cbox_Font.Name = "cbox_Font"
        Me.cbox_Font.Size = New System.Drawing.Size(121, 21)
        Me.cbox_Font.TabIndex = 7
        '
        'btnSave
        '
        Me.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnSave.Location = New System.Drawing.Point(30, 13)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 21
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Location = New System.Drawing.Point(120, 13)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 20
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnSave)
        Me.Panel1.Controls.Add(Me.btnCancel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 209)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(224, 48)
        Me.Panel1.TabIndex = 29
        '
        'frmEdit_Property_Info
        '
        Me.AcceptButton = Me.btnSave
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.CancelButton = Me.btnCancel
        Me.ClientSize = New System.Drawing.Size(224, 257)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmEdit_Property_Info"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Property Info"
        Me.TopMost = True
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_Automatic As System.Windows.Forms.CheckBox
    Friend WithEvents btnColor As System.Windows.Forms.Button
    Friend WithEvents lbl_FontColor As System.Windows.Forms.Label
    Friend WithEvents lbl_Visibility As System.Windows.Forms.Label
    Friend WithEvents cbox_Visibility As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_Alignment As System.Windows.Forms.Label
    Friend WithEvents cbox_Alignment As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_FontSize As System.Windows.Forms.Label
    Friend WithEvents cbox_Size As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_Font As System.Windows.Forms.Label
    Friend WithEvents cbox_Font As System.Windows.Forms.ComboBox
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents ColorDialog As System.Windows.Forms.ColorDialog
    Friend WithEvents btn_VisibilityClear As System.Windows.Forms.Button
    Friend WithEvents btn_AlignmentClear As System.Windows.Forms.Button
    Friend WithEvents btn_ColorClear As System.Windows.Forms.Button
    Friend WithEvents btn_SizeClear As System.Windows.Forms.Button
    Friend WithEvents btn_FontClear As System.Windows.Forms.Button
End Class