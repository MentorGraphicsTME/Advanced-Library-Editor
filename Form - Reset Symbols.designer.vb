<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmResetSymPins
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkbox_PinLocations = New System.Windows.Forms.CheckBox()
        Me.chkbox_TextColors = New System.Windows.Forms.CheckBox()
        Me.chkbox_GraphicColors = New System.Windows.Forms.CheckBox()
        Me.btnReset = New System.Windows.Forms.Button()
        Me.chklistbox_SymPartitions = New System.Windows.Forms.CheckedListBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.Window
        Me.Panel1.Controls.Add(Me.chkbox_PinLocations)
        Me.Panel1.Controls.Add(Me.chkbox_TextColors)
        Me.Panel1.Controls.Add(Me.chkbox_GraphicColors)
        Me.Panel1.Controls.Add(Me.btnReset)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(187, 257)
        Me.Panel1.TabIndex = 0
        '
        'chkbox_PinLocations
        '
        Me.chkbox_PinLocations.AutoSize = True
        Me.chkbox_PinLocations.Location = New System.Drawing.Point(12, 56)
        Me.chkbox_PinLocations.Name = "chkbox_PinLocations"
        Me.chkbox_PinLocations.Size = New System.Drawing.Size(90, 17)
        Me.chkbox_PinLocations.TabIndex = 1
        Me.chkbox_PinLocations.Text = "Pin Locations"
        Me.chkbox_PinLocations.UseVisualStyleBackColor = True
        '
        'chkbox_TextColors
        '
        Me.chkbox_TextColors.AutoSize = True
        Me.chkbox_TextColors.Location = New System.Drawing.Point(12, 34)
        Me.chkbox_TextColors.Name = "chkbox_TextColors"
        Me.chkbox_TextColors.Size = New System.Drawing.Size(79, 17)
        Me.chkbox_TextColors.TabIndex = 1
        Me.chkbox_TextColors.Text = "Text Colors"
        Me.chkbox_TextColors.UseVisualStyleBackColor = True
        '
        'chkbox_GraphicColors
        '
        Me.chkbox_GraphicColors.AutoSize = True
        Me.chkbox_GraphicColors.Location = New System.Drawing.Point(12, 12)
        Me.chkbox_GraphicColors.Name = "chkbox_GraphicColors"
        Me.chkbox_GraphicColors.Size = New System.Drawing.Size(95, 17)
        Me.chkbox_GraphicColors.TabIndex = 1
        Me.chkbox_GraphicColors.Text = "Graphic Colors"
        Me.chkbox_GraphicColors.UseVisualStyleBackColor = True
        '
        'btnReset
        '
        Me.btnReset.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnReset.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnReset.Location = New System.Drawing.Point(87, 211)
        Me.btnReset.Name = "btnReset"
        Me.btnReset.Size = New System.Drawing.Size(94, 34)
        Me.btnReset.TabIndex = 0
        Me.btnReset.Text = "Reset Symbols"
        Me.btnReset.UseVisualStyleBackColor = False
        '
        'chklistbox_SymPartitions
        '
        Me.chklistbox_SymPartitions.CheckOnClick = True
        Me.chklistbox_SymPartitions.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklistbox_SymPartitions.FormattingEnabled = True
        Me.chklistbox_SymPartitions.Location = New System.Drawing.Point(187, 0)
        Me.chklistbox_SymPartitions.Name = "chklistbox_SymPartitions"
        Me.chklistbox_SymPartitions.Size = New System.Drawing.Size(225, 257)
        Me.chklistbox_SymPartitions.TabIndex = 1
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 257)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(412, 22)
        Me.StatusStrip1.TabIndex = 97
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(327, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Select what to reset..."
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
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'frmResetSymPins
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(412, 279)
        Me.Controls.Add(Me.chklistbox_SymPartitions)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(240, 275)
        Me.Name = "frmResetSymPins"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reset Symbols:"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chklistbox_SymPartitions As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnReset As System.Windows.Forms.Button
    Friend WithEvents chkbox_PinLocations As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_TextColors As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_GraphicColors As System.Windows.Forms.CheckBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
End Class
