<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmLogViewer
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
        Me.GroupBox3 = New System.Windows.Forms.GroupBox()
        Me.lbox_LogFiles = New System.Windows.Forms.ListBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btn_LogFileFolder = New System.Windows.Forms.Button()
        Me.btnOpenWith = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblScale = New System.Windows.Forms.Label()
        Me.rtb_Log = New System.Windows.Forms.RichTextBox()
        Me.tb_Zoom = New System.Windows.Forms.TrackBar()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.tb_Zoom, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox3)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Panel1MinSize = 230
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Size = New System.Drawing.Size(1069, 619)
        Me.SplitContainer1.SplitterDistance = 230
        Me.SplitContainer1.TabIndex = 3
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.lbox_LogFiles)
        Me.GroupBox3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox3.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(230, 559)
        Me.GroupBox3.TabIndex = 1
        Me.GroupBox3.TabStop = False
        Me.GroupBox3.Text = "Log Files"
        '
        'lbox_LogFiles
        '
        Me.lbox_LogFiles.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.lbox_LogFiles.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbox_LogFiles.FormattingEnabled = True
        Me.lbox_LogFiles.Location = New System.Drawing.Point(3, 16)
        Me.lbox_LogFiles.Name = "lbox_LogFiles"
        Me.lbox_LogFiles.Size = New System.Drawing.Size(224, 540)
        Me.lbox_LogFiles.TabIndex = 0
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btn_LogFileFolder)
        Me.GroupBox2.Controls.Add(Me.btnOpenWith)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 559)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(230, 60)
        Me.GroupBox2.TabIndex = 0
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Open:"
        '
        'btn_LogFileFolder
        '
        Me.btn_LogFileFolder.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_LogFileFolder.Location = New System.Drawing.Point(111, 25)
        Me.btn_LogFileFolder.Name = "btn_LogFileFolder"
        Me.btn_LogFileFolder.Size = New System.Drawing.Size(111, 23)
        Me.btn_LogFileFolder.TabIndex = 1
        Me.btn_LogFileFolder.Text = "Log files folder"
        Me.btn_LogFileFolder.UseVisualStyleBackColor = True
        '
        'btnOpenWith
        '
        Me.btnOpenWith.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnOpenWith.Enabled = False
        Me.btnOpenWith.Location = New System.Drawing.Point(9, 25)
        Me.btnOpenWith.Name = "btnOpenWith"
        Me.btnOpenWith.Size = New System.Drawing.Size(95, 23)
        Me.btnOpenWith.TabIndex = 0
        Me.btnOpenWith.Text = "Log file with..."
        Me.btnOpenWith.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblScale)
        Me.Panel1.Controls.Add(Me.rtb_Log)
        Me.Panel1.Controls.Add(Me.tb_Zoom)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(835, 619)
        Me.Panel1.TabIndex = 6
        '
        'lblScale
        '
        Me.lblScale.Location = New System.Drawing.Point(3, 101)
        Me.lblScale.Name = "lblScale"
        Me.lblScale.Size = New System.Drawing.Size(34, 13)
        Me.lblScale.TabIndex = 6
        Me.lblScale.Text = "x1.0"
        Me.lblScale.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'rtb_Log
        '
        Me.rtb_Log.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.rtb_Log.BackColor = System.Drawing.SystemColors.Window
        Me.rtb_Log.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.rtb_Log.Location = New System.Drawing.Point(43, 0)
        Me.rtb_Log.Name = "rtb_Log"
        Me.rtb_Log.ReadOnly = True
        Me.rtb_Log.Size = New System.Drawing.Size(790, 617)
        Me.rtb_Log.TabIndex = 2
        Me.rtb_Log.Text = ""
        '
        'tb_Zoom
        '
        Me.tb_Zoom.Location = New System.Drawing.Point(4, 2)
        Me.tb_Zoom.Name = "tb_Zoom"
        Me.tb_Zoom.Orientation = System.Windows.Forms.Orientation.Vertical
        Me.tb_Zoom.Size = New System.Drawing.Size(45, 104)
        Me.tb_Zoom.TabIndex = 4
        Me.tb_Zoom.Value = 5
        '
        'frmLogViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(1069, 619)
        Me.Controls.Add(Me.SplitContainer1)
        Me.MaximizeBox = False
        Me.Name = "frmLogViewer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Log Viewer"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        CType(Me.tb_Zoom, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

End Sub
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents lbox_LogFiles As System.Windows.Forms.ListBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents rtb_Log As System.Windows.Forms.RichTextBox
    Friend WithEvents btnOpenWith As System.Windows.Forms.Button
    Friend WithEvents btn_LogFileFolder As System.Windows.Forms.Button
    Friend WithEvents tb_Zoom As System.Windows.Forms.TrackBar
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lblScale As System.Windows.Forms.Label
End Class
