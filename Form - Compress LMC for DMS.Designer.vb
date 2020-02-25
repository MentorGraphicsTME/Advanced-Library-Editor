<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmShrinkLMC4DMS
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.chkbox_OuputSeperateFiles = New System.Windows.Forms.CheckBox()
        Me.chkbox_Non_Compressed_Partitions = New System.Windows.Forms.CheckBox()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.btnEvaluate = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.tbox_MinParts = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tbox_LibraryScheme = New System.Windows.Forms.TextBox()
        Me.chklistbox_PartPartitions = New System.Windows.Forms.CheckedListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.GroupBox3)
        Me.GroupBox1.Controls.Add(Me.btnProcess)
        Me.GroupBox1.Controls.Add(Me.btnEvaluate)
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.tbox_MinParts)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.tbox_LibraryScheme)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 287)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(272, 149)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Options"
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.chkbox_OuputSeperateFiles)
        Me.GroupBox3.Controls.Add(Me.chkbox_Non_Compressed_Partitions)
        Me.GroupBox3.Location = New System.Drawing.Point(7, 67)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(259, 45)
        Me.GroupBox3.TabIndex = 4
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Create DMS Loader for:"
        '
        'chkbox_OuputSeperateFiles
        '
        Me.chkbox_OuputSeperateFiles.AutoSize = True
        Me.chkbox_OuputSeperateFiles.Location = New System.Drawing.Point(9, 19)
        Me.chkbox_OuputSeperateFiles.Name = "chkbox_OuputSeperateFiles"
        Me.chkbox_OuputSeperateFiles.Size = New System.Drawing.Size(92, 17)
        Me.chkbox_OuputSeperateFiles.TabIndex = 3
        Me.chkbox_OuputSeperateFiles.Text = "Each Partition"
        Me.chkbox_OuputSeperateFiles.UseVisualStyleBackColor = True
        '
        'chkbox_Non_Compressed_Partitions
        '
        Me.chkbox_Non_Compressed_Partitions.AutoSize = True
        Me.chkbox_Non_Compressed_Partitions.Location = New System.Drawing.Point(103, 19)
        Me.chkbox_Non_Compressed_Partitions.Name = "chkbox_Non_Compressed_Partitions"
        Me.chkbox_Non_Compressed_Partitions.Size = New System.Drawing.Size(153, 17)
        Me.chkbox_Non_Compressed_Partitions.TabIndex = 3
        Me.chkbox_Non_Compressed_Partitions.Text = "Non-Compressed Partitions"
        Me.chkbox_Non_Compressed_Partitions.UseVisualStyleBackColor = True
        '
        'btnProcess
        '
        Me.btnProcess.Enabled = False
        Me.btnProcess.Location = New System.Drawing.Point(97, 118)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(75, 23)
        Me.btnProcess.TabIndex = 2
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'btnEvaluate
        '
        Me.btnEvaluate.Location = New System.Drawing.Point(16, 118)
        Me.btnEvaluate.Name = "btnEvaluate"
        Me.btnEvaluate.Size = New System.Drawing.Size(75, 23)
        Me.btnEvaluate.TabIndex = 2
        Me.btnEvaluate.Text = "Evaluate"
        Me.btnEvaluate.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(13, 45)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(188, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Minimum Parts before making Generic:"
        '
        'tbox_MinParts
        '
        Me.tbox_MinParts.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_MinParts.Location = New System.Drawing.Point(199, 41)
        Me.tbox_MinParts.Name = "tbox_MinParts"
        Me.tbox_MinParts.Size = New System.Drawing.Size(61, 20)
        Me.tbox_MinParts.TabIndex = 0
        Me.tbox_MinParts.Text = "5"
        Me.tbox_MinParts.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(13, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(114, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Library Scheme Name:"
        '
        'tbox_LibraryScheme
        '
        Me.tbox_LibraryScheme.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_LibraryScheme.Location = New System.Drawing.Point(133, 16)
        Me.tbox_LibraryScheme.Name = "tbox_LibraryScheme"
        Me.tbox_LibraryScheme.Size = New System.Drawing.Size(127, 20)
        Me.tbox_LibraryScheme.TabIndex = 0
        Me.tbox_LibraryScheme.Text = "EE"
        '
        'chklistbox_PartPartitions
        '
        Me.chklistbox_PartPartitions.CheckOnClick = True
        Me.chklistbox_PartPartitions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklistbox_PartPartitions.FormattingEnabled = True
        Me.chklistbox_PartPartitions.Location = New System.Drawing.Point(3, 16)
        Me.chklistbox_PartPartitions.Name = "chklistbox_PartPartitions"
        Me.chklistbox_PartPartitions.Size = New System.Drawing.Size(266, 268)
        Me.chklistbox_PartPartitions.TabIndex = 1
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.chklistbox_PartPartitions)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(272, 287)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Part Partitions"
        '
        'frmShrinkLMC4DMS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(272, 436)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(275, 375)
        Me.Name = "frmShrinkLMC4DMS"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Compress LMC for DMS"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chklistbox_PartPartitions As System.Windows.Forms.CheckedListBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents tbox_MinParts As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents tbox_LibraryScheme As System.Windows.Forms.TextBox
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents btnEvaluate As System.Windows.Forms.Button
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_OuputSeperateFiles As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_Non_Compressed_Partitions As System.Windows.Forms.CheckBox
End Class
