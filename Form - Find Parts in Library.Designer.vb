<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmFindPartsinLibrary
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.statusStrip = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lbl_workbook = New System.Windows.Forms.Label()
        Me.lbl_spreadsheet = New System.Windows.Forms.Label()
        Me.tbox_input = New System.Windows.Forms.TextBox()
        Me.cbox_spreadsheet = New System.Windows.Forms.ComboBox()
        Me.btn_browse_excel = New System.Windows.Forms.Button()
        Me.lbl_partNumber = New System.Windows.Forms.Label()
        Me.cbox_partNumber = New System.Windows.Forms.ComboBox()
        Me.pnl_exceloptions = New System.Windows.Forms.Panel()
        Me.lbl_exceloptions = New System.Windows.Forms.Label()
        Me.btn_Read = New System.Windows.Forms.Button()
        Me.statusStrip.SuspendLayout()
        Me.pnl_exceloptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'statusStrip
        '
        Me.statusStrip.BackColor = System.Drawing.SystemColors.Control
        Me.statusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.statusStrip.Location = New System.Drawing.Point(0, 178)
        Me.statusStrip.Name = "statusStrip"
        Me.statusStrip.Size = New System.Drawing.Size(389, 22)
        Me.statusStrip.TabIndex = 28
        '
        'ts_Status
        '
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(304, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Select an input file ..."
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
        'lbl_workbook
        '
        Me.lbl_workbook.AutoSize = True
        Me.lbl_workbook.Location = New System.Drawing.Point(9, 15)
        Me.lbl_workbook.Name = "lbl_workbook"
        Me.lbl_workbook.Size = New System.Drawing.Size(34, 13)
        Me.lbl_workbook.TabIndex = 29
        Me.lbl_workbook.Text = "Input:"
        '
        'lbl_spreadsheet
        '
        Me.lbl_spreadsheet.AutoSize = True
        Me.lbl_spreadsheet.Location = New System.Drawing.Point(12, 31)
        Me.lbl_spreadsheet.Name = "lbl_spreadsheet"
        Me.lbl_spreadsheet.Size = New System.Drawing.Size(62, 13)
        Me.lbl_spreadsheet.TabIndex = 29
        Me.lbl_spreadsheet.Text = "Worksheet:"
        '
        'tbox_input
        '
        Me.tbox_input.Location = New System.Drawing.Point(49, 12)
        Me.tbox_input.Name = "tbox_input"
        Me.tbox_input.Size = New System.Drawing.Size(269, 20)
        Me.tbox_input.TabIndex = 30
        '
        'cbox_spreadsheet
        '
        Me.cbox_spreadsheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_spreadsheet.FormattingEnabled = True
        Me.cbox_spreadsheet.Location = New System.Drawing.Point(80, 28)
        Me.cbox_spreadsheet.Name = "cbox_spreadsheet"
        Me.cbox_spreadsheet.Size = New System.Drawing.Size(216, 21)
        Me.cbox_spreadsheet.TabIndex = 31
        '
        'btn_browse_excel
        '
        Me.btn_browse_excel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_browse_excel.Location = New System.Drawing.Point(324, 11)
        Me.btn_browse_excel.Name = "btn_browse_excel"
        Me.btn_browse_excel.Size = New System.Drawing.Size(32, 20)
        Me.btn_browse_excel.TabIndex = 85
        Me.btn_browse_excel.Text = "..."
        Me.btn_browse_excel.UseVisualStyleBackColor = False
        '
        'lbl_partNumber
        '
        Me.lbl_partNumber.AutoSize = True
        Me.lbl_partNumber.Location = New System.Drawing.Point(5, 61)
        Me.lbl_partNumber.Name = "lbl_partNumber"
        Me.lbl_partNumber.Size = New System.Drawing.Size(69, 13)
        Me.lbl_partNumber.TabIndex = 86
        Me.lbl_partNumber.Text = "Part Number:"
        '
        'cbox_partNumber
        '
        Me.cbox_partNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_partNumber.FormattingEnabled = True
        Me.cbox_partNumber.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P"})
        Me.cbox_partNumber.Location = New System.Drawing.Point(80, 58)
        Me.cbox_partNumber.Name = "cbox_partNumber"
        Me.cbox_partNumber.Size = New System.Drawing.Size(51, 21)
        Me.cbox_partNumber.TabIndex = 31
        '
        'pnl_exceloptions
        '
        Me.pnl_exceloptions.Controls.Add(Me.lbl_exceloptions)
        Me.pnl_exceloptions.Controls.Add(Me.cbox_spreadsheet)
        Me.pnl_exceloptions.Controls.Add(Me.lbl_partNumber)
        Me.pnl_exceloptions.Controls.Add(Me.lbl_spreadsheet)
        Me.pnl_exceloptions.Controls.Add(Me.cbox_partNumber)
        Me.pnl_exceloptions.Enabled = False
        Me.pnl_exceloptions.Location = New System.Drawing.Point(12, 38)
        Me.pnl_exceloptions.Name = "pnl_exceloptions"
        Me.pnl_exceloptions.Size = New System.Drawing.Size(328, 91)
        Me.pnl_exceloptions.TabIndex = 87
        '
        'lbl_exceloptions
        '
        Me.lbl_exceloptions.AutoSize = True
        Me.lbl_exceloptions.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_exceloptions.Location = New System.Drawing.Point(5, 9)
        Me.lbl_exceloptions.Name = "lbl_exceloptions"
        Me.lbl_exceloptions.Size = New System.Drawing.Size(72, 13)
        Me.lbl_exceloptions.TabIndex = 87
        Me.lbl_exceloptions.Text = "Excel Options"
        '
        'btn_Read
        '
        Me.btn_Read.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btn_Read.Enabled = False
        Me.btn_Read.Location = New System.Drawing.Point(119, 146)
        Me.btn_Read.Name = "btn_Read"
        Me.btn_Read.Size = New System.Drawing.Size(151, 29)
        Me.btn_Read.TabIndex = 88
        Me.btn_Read.Text = "Read"
        Me.btn_Read.UseVisualStyleBackColor = True
        '
        'frmFindPartsinLibrary
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(389, 200)
        Me.Controls.Add(Me.btn_Read)
        Me.Controls.Add(Me.pnl_exceloptions)
        Me.Controls.Add(Me.btn_browse_excel)
        Me.Controls.Add(Me.tbox_input)
        Me.Controls.Add(Me.lbl_workbook)
        Me.Controls.Add(Me.statusStrip)
        Me.Name = "frmFindPartsinLibrary"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Find Parts in Library"
        Me.statusStrip.ResumeLayout(False)
        Me.statusStrip.PerformLayout()
        Me.pnl_exceloptions.ResumeLayout(False)
        Me.pnl_exceloptions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents statusStrip As StatusStrip
    Friend WithEvents ts_Status As ToolStripStatusLabel
    Friend WithEvents WaitGif As ToolStripStatusLabel
    Friend WithEvents lbl_workbook As System.Windows.Forms.Label
    Friend WithEvents lbl_spreadsheet As System.Windows.Forms.Label
    Friend WithEvents tbox_input As System.Windows.Forms.TextBox
    Friend WithEvents cbox_spreadsheet As ComboBox
    Friend WithEvents btn_browse_excel As System.Windows.Forms.Button
    Friend WithEvents lbl_partNumber As System.Windows.Forms.Label
    Friend WithEvents cbox_partNumber As ComboBox
    Friend WithEvents pnl_exceloptions As Panel
    Friend WithEvents lbl_exceloptions As System.Windows.Forms.Label
    Friend WithEvents btn_Read As System.Windows.Forms.Button
End Class
