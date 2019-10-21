<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddPartProperty
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
        Me.btn_Add = New System.Windows.Forms.Button()
        Me.cbox_PartProperties = New System.Windows.Forms.ComboBox()
        Me.tbValue = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.rtBox_Output = New System.Windows.Forms.RichTextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.tv_Parts = New System.Windows.Forms.TreeView()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btn_ReportCurrentProperties = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.StatusStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_Add
        '
        Me.btn_Add.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_Add.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Add.Enabled = False
        Me.btn_Add.Location = New System.Drawing.Point(355, 684)
        Me.btn_Add.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_Add.Name = "btn_Add"
        Me.btn_Add.Size = New System.Drawing.Size(136, 28)
        Me.btn_Add.TabIndex = 0
        Me.btn_Add.Text = "Add Property"
        Me.btn_Add.UseVisualStyleBackColor = False
        '
        'cbox_PartProperties
        '
        Me.cbox_PartProperties.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_PartProperties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_PartProperties.FormattingEnabled = True
        Me.cbox_PartProperties.Location = New System.Drawing.Point(345, 40)
        Me.cbox_PartProperties.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_PartProperties.Name = "cbox_PartProperties"
        Me.cbox_PartProperties.Size = New System.Drawing.Size(290, 24)
        Me.cbox_PartProperties.TabIndex = 24
        '
        'tbValue
        '
        Me.tbValue.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbValue.Location = New System.Drawing.Point(341, 107)
        Me.tbValue.Margin = New System.Windows.Forms.Padding(4)
        Me.tbValue.Name = "tbValue"
        Me.tbValue.Size = New System.Drawing.Size(294, 22)
        Me.tbValue.TabIndex = 27
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(341, 88)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(48, 17)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Value:"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(341, 20)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 17)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Property:"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 761)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(652, 25)
        Me.StatusStrip1.TabIndex = 27
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(567, 20)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Select some parts to begin..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WaitGif
        '
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 20)
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(652, 761)
        Me.TabControl1.TabIndex = 29
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.btn_Add)
        Me.TabPage1.Controls.Add(Me.tbValue)
        Me.TabPage1.Controls.Add(Me.Panel1)
        Me.TabPage1.Controls.Add(Me.Label2)
        Me.TabPage1.Controls.Add(Me.cbox_PartProperties)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 25)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(644, 732)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Main"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.rtBox_Output)
        Me.TabPage2.Location = New System.Drawing.Point(4, 25)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(649, 570)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Debug"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'rtBox_Output
        '
        Me.rtBox_Output.Dock = System.Windows.Forms.DockStyle.Fill
        Me.rtBox_Output.Location = New System.Drawing.Point(3, 3)
        Me.rtBox_Output.Name = "rtBox_Output"
        Me.rtBox_Output.ReadOnly = True
        Me.rtBox_Output.Size = New System.Drawing.Size(643, 564)
        Me.rtBox_Output.TabIndex = 0
        Me.rtBox_Output.Text = ""
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.GroupBox1)
        Me.Panel1.Controls.Add(Me.GroupBox2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(3, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(330, 726)
        Me.Panel1.TabIndex = 0
        '
        'tv_Parts
        '
        Me.tv_Parts.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv_Parts.CheckBoxes = True
        Me.tv_Parts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Parts.Location = New System.Drawing.Point(4, 19)
        Me.tv_Parts.Margin = New System.Windows.Forms.Padding(4)
        Me.tv_Parts.Name = "tv_Parts"
        Me.tv_Parts.Size = New System.Drawing.Size(322, 635)
        Me.tv_Parts.TabIndex = 24
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tv_Parts)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox1.Size = New System.Drawing.Size(330, 658)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parts"
        '
        'btn_ReportCurrentProperties
        '
        Me.btn_ReportCurrentProperties.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_ReportCurrentProperties.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_ReportCurrentProperties.Location = New System.Drawing.Point(74, 23)
        Me.btn_ReportCurrentProperties.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_ReportCurrentProperties.Name = "btn_ReportCurrentProperties"
        Me.btn_ReportCurrentProperties.Size = New System.Drawing.Size(195, 28)
        Me.btn_ReportCurrentProperties.TabIndex = 28
        Me.btn_ReportCurrentProperties.Text = "Report Current Properties"
        Me.btn_ReportCurrentProperties.UseVisualStyleBackColor = False
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btn_ReportCurrentProperties)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 658)
        Me.GroupBox2.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBox2.Size = New System.Drawing.Size(330, 68)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Options"
        '
        'frmAddPartProperty
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(652, 786)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(627, 641)
        Me.Name = "frmAddPartProperty"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Part Property"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Add As System.Windows.Forms.Button
    Friend WithEvents cbox_PartProperties As System.Windows.Forms.ComboBox
    Friend WithEvents tbValue As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents rtBox_Output As RichTextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tv_Parts As TreeView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents btn_ReportCurrentProperties As System.Windows.Forms.Button
End Class
