<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frm_UpdateCellName
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
        Me.chklstbox_Partitions = New System.Windows.Forms.CheckedListBox()
        Me.gb_Actions = New System.Windows.Forms.GroupBox()
        Me.btnModify = New System.Windows.Forms.Button()
        Me.cbox_SymbolCase = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.GroupBox1.SuspendLayout()
        Me.gb_Actions.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chklstbox_Partitions)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(284, 214)
        Me.GroupBox1.TabIndex = 32
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cell Partitions"
        '
        'chklstbox_Partitions
        '
        Me.chklstbox_Partitions.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.chklstbox_Partitions.CheckOnClick = True
        Me.chklstbox_Partitions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklstbox_Partitions.FormattingEnabled = True
        Me.chklstbox_Partitions.Location = New System.Drawing.Point(3, 16)
        Me.chklstbox_Partitions.Name = "chklstbox_Partitions"
        Me.chklstbox_Partitions.Size = New System.Drawing.Size(278, 195)
        Me.chklstbox_Partitions.Sorted = True
        Me.chklstbox_Partitions.TabIndex = 26
        '
        'gb_Actions
        '
        Me.gb_Actions.Controls.Add(Me.Label1)
        Me.gb_Actions.Controls.Add(Me.cbox_SymbolCase)
        Me.gb_Actions.Controls.Add(Me.btnModify)
        Me.gb_Actions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_Actions.Location = New System.Drawing.Point(0, 214)
        Me.gb_Actions.Name = "gb_Actions"
        Me.gb_Actions.Size = New System.Drawing.Size(284, 48)
        Me.gb_Actions.TabIndex = 33
        Me.gb_Actions.TabStop = False
        Me.gb_Actions.Text = "Actions"
        '
        'btnModify
        '
        Me.btnModify.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnModify.Location = New System.Drawing.Point(177, 13)
        Me.btnModify.Name = "btnModify"
        Me.btnModify.Size = New System.Drawing.Size(95, 23)
        Me.btnModify.TabIndex = 22
        Me.btnModify.Text = "Change Case"
        Me.btnModify.UseVisualStyleBackColor = False
        '
        'cbox_SymbolCase
        '
        Me.cbox_SymbolCase.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_SymbolCase.BackColor = System.Drawing.SystemColors.Control
        Me.cbox_SymbolCase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_SymbolCase.FormattingEnabled = True
        Me.cbox_SymbolCase.Items.AddRange(New Object() {"Uppercase", "Lowercase"})
        Me.cbox_SymbolCase.Location = New System.Drawing.Point(46, 14)
        Me.cbox_SymbolCase.Name = "cbox_SymbolCase"
        Me.cbox_SymbolCase.Size = New System.Drawing.Size(125, 21)
        Me.cbox_SymbolCase.TabIndex = 65
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(6, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(34, 13)
        Me.Label1.TabIndex = 66
        Me.Label1.Text = "Case:"
        '
        'frm_UpdateCellName
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 262)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.gb_Actions)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frm_UpdateCellName"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Update Cell Name"
        Me.GroupBox1.ResumeLayout(False)
        Me.gb_Actions.ResumeLayout(False)
        Me.gb_Actions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chklstbox_Partitions As System.Windows.Forms.CheckedListBox
    Friend WithEvents gb_Actions As System.Windows.Forms.GroupBox
    Friend WithEvents btnModify As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbox_SymbolCase As System.Windows.Forms.ComboBox
End Class
