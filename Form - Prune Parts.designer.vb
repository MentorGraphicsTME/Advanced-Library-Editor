<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPruneParts
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
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gb_Actions = New System.Windows.Forms.GroupBox()
        Me.rbtn_PruneLibrary = New System.Windows.Forms.RadioButton()
        Me.rbtn_PruneParts = New System.Windows.Forms.RadioButton()
        Me.btn_Process = New System.Windows.Forms.Button()
        Me.pl_FileInput = New System.Windows.Forms.Panel()
        Me.pl_Excel = New System.Windows.Forms.Panel()
        Me.cboxActiveSheet = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbox_PN = New System.Windows.Forms.ComboBox()
        Me.chkbox_ReadAllSheets = New System.Windows.Forms.CheckBox()
        Me.lblPN = New System.Windows.Forms.Label()
        Me.chkbox_IgnoreHeader = New System.Windows.Forms.CheckBox()
        Me.lbl_AltCellFile = New System.Windows.Forms.Label()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.tbox_Input = New System.Windows.Forms.TextBox()
        Me.btn_Browse = New System.Windows.Forms.Button()
        Me.lbox_Parts = New System.Windows.Forms.ListBox()
        Me.StatusStrip1.SuspendLayout
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SplitContainer1.Panel1.SuspendLayout
        Me.SplitContainer1.Panel2.SuspendLayout
        Me.SplitContainer1.SuspendLayout
        Me.gb_Actions.SuspendLayout
        Me.pl_FileInput.SuspendLayout
        Me.pl_Excel.SuspendLayout
        Me.SuspendLayout
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(447, 17)
        Me.ts_Status.Spring = true
        Me.ts_Status.Text = "Browse to a part list to begin..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 456)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(532, 22)
        Me.StatusStrip1.TabIndex = 96
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'WaitGif
        '
        Me.WaitGif.BackColor = System.Drawing.Color.Transparent
        Me.WaitGif.Enabled = false
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_Actions)
        Me.SplitContainer1.Panel1.Controls.Add(Me.pl_FileInput)
        Me.SplitContainer1.Panel1MinSize = 269
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbox_Parts)
        Me.SplitContainer1.Panel2MinSize = 226
        Me.SplitContainer1.Size = New System.Drawing.Size(532, 456)
        Me.SplitContainer1.SplitterDistance = 281
        Me.SplitContainer1.TabIndex = 95
        '
        'gb_Actions
        '
        Me.gb_Actions.Controls.Add(Me.rbtn_PruneLibrary)
        Me.gb_Actions.Controls.Add(Me.rbtn_PruneParts)
        Me.gb_Actions.Controls.Add(Me.btn_Process)
        Me.gb_Actions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_Actions.Enabled = false
        Me.gb_Actions.Location = New System.Drawing.Point(0, 387)
        Me.gb_Actions.Name = "gb_Actions"
        Me.gb_Actions.Size = New System.Drawing.Size(281, 69)
        Me.gb_Actions.TabIndex = 84
        Me.gb_Actions.TabStop = false
        Me.gb_Actions.Text = "Options"
        '
        'rbtn_PruneLibrary
        '
        Me.rbtn_PruneLibrary.AutoSize = true
        Me.rbtn_PruneLibrary.Location = New System.Drawing.Point(7, 39)
        Me.rbtn_PruneLibrary.Name = "rbtn_PruneLibrary"
        Me.rbtn_PruneLibrary.Size = New System.Drawing.Size(163, 17)
        Me.rbtn_PruneLibrary.TabIndex = 4
        Me.rbtn_PruneLibrary.Text = "Prune library to match part list"
        Me.rbtn_PruneLibrary.UseVisualStyleBackColor = true
        '
        'rbtn_PruneParts
        '
        Me.rbtn_PruneParts.AutoSize = true
        Me.rbtn_PruneParts.Checked = true
        Me.rbtn_PruneParts.Location = New System.Drawing.Point(7, 16)
        Me.rbtn_PruneParts.Name = "rbtn_PruneParts"
        Me.rbtn_PruneParts.Size = New System.Drawing.Size(142, 17)
        Me.rbtn_PruneParts.TabIndex = 3
        Me.rbtn_PruneParts.TabStop = true
        Me.rbtn_PruneParts.Text = "Prune part list from library"
        Me.rbtn_PruneParts.UseVisualStyleBackColor = true
        '
        'btn_Process
        '
        Me.btn_Process.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_Process.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Process.Location = New System.Drawing.Point(181, 26)
        Me.btn_Process.Name = "btn_Process"
        Me.btn_Process.Size = New System.Drawing.Size(92, 34)
        Me.btn_Process.TabIndex = 2
        Me.btn_Process.Text = "Process"
        Me.btn_Process.UseVisualStyleBackColor = false
        '
        'pl_FileInput
        '
        Me.pl_FileInput.Controls.Add(Me.pl_Excel)
        Me.pl_FileInput.Controls.Add(Me.lbl_AltCellFile)
        Me.pl_FileInput.Controls.Add(Me.btnRead)
        Me.pl_FileInput.Controls.Add(Me.tbox_Input)
        Me.pl_FileInput.Controls.Add(Me.btn_Browse)
        Me.pl_FileInput.Dock = System.Windows.Forms.DockStyle.Top
        Me.pl_FileInput.Location = New System.Drawing.Point(0, 0)
        Me.pl_FileInput.Name = "pl_FileInput"
        Me.pl_FileInput.Size = New System.Drawing.Size(281, 203)
        Me.pl_FileInput.TabIndex = 1
        '
        'pl_Excel
        '
        Me.pl_Excel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.pl_Excel.Controls.Add(Me.cboxActiveSheet)
        Me.pl_Excel.Controls.Add(Me.Label1)
        Me.pl_Excel.Controls.Add(Me.cbox_PN)
        Me.pl_Excel.Controls.Add(Me.chkbox_ReadAllSheets)
        Me.pl_Excel.Controls.Add(Me.lblPN)
        Me.pl_Excel.Controls.Add(Me.chkbox_IgnoreHeader)
        Me.pl_Excel.Enabled = false
        Me.pl_Excel.Location = New System.Drawing.Point(0, 52)
        Me.pl_Excel.Name = "pl_Excel"
        Me.pl_Excel.Size = New System.Drawing.Size(281, 97)
        Me.pl_Excel.TabIndex = 119
        '
        'cboxActiveSheet
        '
        Me.cboxActiveSheet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cboxActiveSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxActiveSheet.Enabled = false
        Me.cboxActiveSheet.FormattingEnabled = true
        Me.cboxActiveSheet.Location = New System.Drawing.Point(80, 64)
        Me.cboxActiveSheet.Name = "cboxActiveSheet"
        Me.cboxActiveSheet.Size = New System.Drawing.Size(193, 21)
        Me.cboxActiveSheet.TabIndex = 120
        '
        'Label1
        '
        Me.Label1.AutoSize = true
        Me.Label1.Enabled = false
        Me.Label1.Location = New System.Drawing.Point(9, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "Worksheet:"
        '
        'cbox_PN
        '
        Me.cbox_PN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.cbox_PN.BackColor = System.Drawing.SystemColors.Control
        Me.cbox_PN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_PN.FormattingEnabled = true
        Me.cbox_PN.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_PN.Location = New System.Drawing.Point(223, 10)
        Me.cbox_PN.Name = "cbox_PN"
        Me.cbox_PN.Size = New System.Drawing.Size(50, 21)
        Me.cbox_PN.TabIndex = 114
        '
        'chkbox_ReadAllSheets
        '
        Me.chkbox_ReadAllSheets.AutoSize = true
        Me.chkbox_ReadAllSheets.Checked = true
        Me.chkbox_ReadAllSheets.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_ReadAllSheets.Location = New System.Drawing.Point(12, 40)
        Me.chkbox_ReadAllSheets.Name = "chkbox_ReadAllSheets"
        Me.chkbox_ReadAllSheets.Size = New System.Drawing.Size(99, 17)
        Me.chkbox_ReadAllSheets.TabIndex = 118
        Me.chkbox_ReadAllSheets.Text = "Read all sheets"
        Me.chkbox_ReadAllSheets.UseVisualStyleBackColor = true
        '
        'lblPN
        '
        Me.lblPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.lblPN.AutoSize = true
        Me.lblPN.Location = New System.Drawing.Point(148, 14)
        Me.lblPN.Name = "lblPN"
        Me.lblPN.Size = New System.Drawing.Size(69, 13)
        Me.lblPN.TabIndex = 115
        Me.lblPN.Text = "Part Number:"
        '
        'chkbox_IgnoreHeader
        '
        Me.chkbox_IgnoreHeader.AutoSize = true
        Me.chkbox_IgnoreHeader.Checked = true
        Me.chkbox_IgnoreHeader.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_IgnoreHeader.Location = New System.Drawing.Point(12, 12)
        Me.chkbox_IgnoreHeader.Name = "chkbox_IgnoreHeader"
        Me.chkbox_IgnoreHeader.Size = New System.Drawing.Size(119, 17)
        Me.chkbox_IgnoreHeader.TabIndex = 117
        Me.chkbox_IgnoreHeader.Text = "Ignore Header Row"
        Me.chkbox_IgnoreHeader.UseVisualStyleBackColor = true
        '
        'lbl_AltCellFile
        '
        Me.lbl_AltCellFile.AutoSize = true
        Me.lbl_AltCellFile.Location = New System.Drawing.Point(12, 9)
        Me.lbl_AltCellFile.Name = "lbl_AltCellFile"
        Me.lbl_AltCellFile.Size = New System.Drawing.Size(45, 13)
        Me.lbl_AltCellFile.TabIndex = 85
        Me.lbl_AltCellFile.Text = "Part File"
        '
        'btnRead
        '
        Me.btnRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnRead.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRead.Enabled = false
        Me.btnRead.Location = New System.Drawing.Point(181, 158)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(92, 34)
        Me.btnRead.TabIndex = 84
        Me.btnRead.Text = "Read"
        Me.btnRead.UseVisualStyleBackColor = false
        '
        'tbox_Input
        '
        Me.tbox_Input.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.tbox_Input.Location = New System.Drawing.Point(12, 25)
        Me.tbox_Input.Name = "tbox_Input"
        Me.tbox_Input.Size = New System.Drawing.Size(225, 20)
        Me.tbox_Input.TabIndex = 83
        '
        'btn_Browse
        '
        Me.btn_Browse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btn_Browse.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Browse.Location = New System.Drawing.Point(243, 25)
        Me.btn_Browse.Name = "btn_Browse"
        Me.btn_Browse.Size = New System.Drawing.Size(30, 20)
        Me.btn_Browse.TabIndex = 84
        Me.btn_Browse.Text = "..."
        Me.btn_Browse.UseVisualStyleBackColor = false
        '
        'lbox_Parts
        '
        Me.lbox_Parts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lbox_Parts.FormattingEnabled = true
        Me.lbox_Parts.Location = New System.Drawing.Point(0, 0)
        Me.lbox_Parts.Name = "lbox_Parts"
        Me.lbox_Parts.Size = New System.Drawing.Size(247, 456)
        Me.lbox_Parts.TabIndex = 0
        '
        'frmPruneParts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(532, 478)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.HelpButton = true
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmPruneParts"
        Me.ShowIcon = false
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Prune Parts"
        Me.StatusStrip1.ResumeLayout(false)
        Me.StatusStrip1.PerformLayout
        Me.SplitContainer1.Panel1.ResumeLayout(false)
        Me.SplitContainer1.Panel2.ResumeLayout(false)
        CType(Me.SplitContainer1,System.ComponentModel.ISupportInitialize).EndInit
        Me.SplitContainer1.ResumeLayout(false)
        Me.gb_Actions.ResumeLayout(false)
        Me.gb_Actions.PerformLayout
        Me.pl_FileInput.ResumeLayout(false)
        Me.pl_FileInput.PerformLayout
        Me.pl_Excel.ResumeLayout(false)
        Me.pl_Excel.PerformLayout
        Me.ResumeLayout(false)
        Me.PerformLayout

End Sub
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents pl_FileInput As System.Windows.Forms.Panel
    Friend WithEvents lbl_AltCellFile As System.Windows.Forms.Label
    Friend WithEvents tbox_Input As System.Windows.Forms.TextBox
    Friend WithEvents btn_Browse As System.Windows.Forms.Button
    Friend WithEvents btn_Process As System.Windows.Forms.Button
    Friend WithEvents gb_Actions As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_ReadAllSheets As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_IgnoreHeader As System.Windows.Forms.CheckBox
    Friend WithEvents lblPN As System.Windows.Forms.Label
    Friend WithEvents cbox_PN As System.Windows.Forms.ComboBox
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents pl_Excel As System.Windows.Forms.Panel
    Friend WithEvents cboxActiveSheet As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents rbtn_PruneLibrary As System.Windows.Forms.RadioButton
    Friend WithEvents rbtn_PruneParts As System.Windows.Forms.RadioButton
    Friend WithEvents lbox_Parts As System.Windows.Forms.ListBox
End Class
