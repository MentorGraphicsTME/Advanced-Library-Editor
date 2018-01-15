<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmModifyUserLayers
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
        Me.components = New System.ComponentModel.Container()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnNew = New System.Windows.Forms.Button()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.PropertyAction = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsm_RemoveProperty = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_SeperatorPromote = New System.Windows.Forms.ToolStripSeparator()
        Me.tsm_AddDefaultValue = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_RemoveDefaultValue = New System.Windows.Forms.ToolStripMenuItem()
        Me.tv_Properties = New DictionaryTreeView.dictTreeView()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.PropertyAction.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 386)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(314, 22)
        Me.StatusStrip1.TabIndex = 11
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(229, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Click read to begin..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WaitGif
        '
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnNew)
        Me.Panel1.Controls.Add(Me.btnProcess)
        Me.Panel1.Controls.Add(Me.btnRead)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 343)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(314, 43)
        Me.Panel1.TabIndex = 12
        '
        'btnNew
        '
        Me.btnNew.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnNew.Enabled = False
        Me.btnNew.Location = New System.Drawing.Point(120, 10)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 2
        Me.btnNew.Text = "New"
        Me.btnNew.UseVisualStyleBackColor = False
        '
        'btnProcess
        '
        Me.btnProcess.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnProcess.Enabled = False
        Me.btnProcess.Location = New System.Drawing.Point(215, 10)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(75, 23)
        Me.btnProcess.TabIndex = 1
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = False
        '
        'btnRead
        '
        Me.btnRead.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRead.Location = New System.Drawing.Point(25, 10)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(75, 23)
        Me.btnRead.TabIndex = 0
        Me.btnRead.Text = "Read"
        Me.btnRead.UseVisualStyleBackColor = False
        '
        'PropertyAction
        '
        Me.PropertyAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsm_RemoveProperty, Me.tsm_SeperatorPromote, Me.tsm_AddDefaultValue, Me.tsm_RemoveDefaultValue})
        Me.PropertyAction.Name = "MyContextMenu"
        Me.PropertyAction.Size = New System.Drawing.Size(191, 76)
        '
        'tsm_RemoveProperty
        '
        Me.tsm_RemoveProperty.Name = "tsm_RemoveProperty"
        Me.tsm_RemoveProperty.Size = New System.Drawing.Size(190, 22)
        Me.tsm_RemoveProperty.Text = "Remove"
        Me.tsm_RemoveProperty.Visible = False
        '
        'tsm_SeperatorPromote
        '
        Me.tsm_SeperatorPromote.Name = "tsm_SeperatorPromote"
        Me.tsm_SeperatorPromote.Size = New System.Drawing.Size(187, 6)
        Me.tsm_SeperatorPromote.Visible = False
        '
        'tsm_AddDefaultValue
        '
        Me.tsm_AddDefaultValue.Enabled = False
        Me.tsm_AddDefaultValue.Name = "tsm_AddDefaultValue"
        Me.tsm_AddDefaultValue.Size = New System.Drawing.Size(190, 22)
        Me.tsm_AddDefaultValue.Text = "Add Default Value"
        '
        'tsm_RemoveDefaultValue
        '
        Me.tsm_RemoveDefaultValue.Enabled = False
        Me.tsm_RemoveDefaultValue.Name = "tsm_RemoveDefaultValue"
        Me.tsm_RemoveDefaultValue.Size = New System.Drawing.Size(190, 22)
        Me.tsm_RemoveDefaultValue.Text = "Remove Default Value"
        '
        'tv_Properties
        '
        Me.tv_Properties.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv_Properties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Properties.Location = New System.Drawing.Point(0, 0)
        Me.tv_Properties.Name = "tv_Properties"
        Me.tv_Properties.Size = New System.Drawing.Size(314, 343)
        Me.tv_Properties.TabIndex = 13
        '
        'frmModifyUserLayers
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(314, 408)
        Me.Controls.Add(Me.tv_Properties)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmModifyUserLayers"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Modify User Properties"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.PropertyAction.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents PropertyAction As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsm_RemoveProperty As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_SeperatorPromote As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsm_AddDefaultValue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_RemoveDefaultValue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents tv_Properties As DictionaryTreeView.dictTreeView
End Class
