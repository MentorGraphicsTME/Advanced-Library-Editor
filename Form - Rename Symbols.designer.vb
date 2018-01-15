<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRenameSymbols
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
        Me.chkbox_HealPDB = New System.Windows.Forms.CheckBox()
        Me.btnEvaluate = New System.Windows.Forms.Button()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lbl_worksheet = New System.Windows.Forms.Label()
        Me.cbox_worksheet = New System.Windows.Forms.ComboBox()
        Me.chkbox_ReadAllSheets = New System.Windows.Forms.CheckBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chkbox_IgnoreDuplicatesMissing = New System.Windows.Forms.CheckBox()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.lblNoChange = New System.Windows.Forms.Label()
        Me.lblDupPN = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblToRename = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblMissingSymbols = New System.Windows.Forms.Label()
        Me.chkbox_IgnoreHeader = New System.Windows.Forms.CheckBox()
        Me.lblPN = New System.Windows.Forms.Label()
        Me.lblRefDes = New System.Windows.Forms.Label()
        Me.cbox_After = New System.Windows.Forms.ComboBox()
        Me.cbox_Before = New System.Windows.Forms.ComboBox()
        Me.lbl_Browse_A = New System.Windows.Forms.Label()
        Me.btn_Browse_Excel = New System.Windows.Forms.Button()
        Me.tbox_Input = New System.Windows.Forms.TextBox()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.dgv_Symbols = New System.Windows.Forms.DataGridView()
        Me.dgvFrom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.pl_Process = New System.Windows.Forms.Panel()
        Me.Panel2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.dgv_Symbols, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pl_Process.SuspendLayout()
        Me.SuspendLayout()
        '
        'chkbox_HealPDB
        '
        Me.chkbox_HealPDB.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.chkbox_HealPDB.AutoSize = True
        Me.chkbox_HealPDB.Checked = True
        Me.chkbox_HealPDB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_HealPDB.Location = New System.Drawing.Point(8, 19)
        Me.chkbox_HealPDB.Name = "chkbox_HealPDB"
        Me.chkbox_HealPDB.Size = New System.Drawing.Size(166, 17)
        Me.chkbox_HealPDB.TabIndex = 93
        Me.chkbox_HealPDB.Text = "Update symbol names in PDB"
        Me.chkbox_HealPDB.UseVisualStyleBackColor = True
        '
        'btnEvaluate
        '
        Me.btnEvaluate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEvaluate.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnEvaluate.Enabled = False
        Me.btnEvaluate.Location = New System.Drawing.Point(199, 96)
        Me.btnEvaluate.Name = "btnEvaluate"
        Me.btnEvaluate.Size = New System.Drawing.Size(80, 27)
        Me.btnEvaluate.TabIndex = 64
        Me.btnEvaluate.Text = "Evaluate List"
        Me.btnEvaluate.UseVisualStyleBackColor = False
        '
        'btnProcess
        '
        Me.btnProcess.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnProcess.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnProcess.Location = New System.Drawing.Point(182, 14)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(87, 27)
        Me.btnProcess.TabIndex = 91
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = False
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.lbl_worksheet)
        Me.Panel2.Controls.Add(Me.cbox_worksheet)
        Me.Panel2.Controls.Add(Me.chkbox_ReadAllSheets)
        Me.Panel2.Controls.Add(Me.Panel1)
        Me.Panel2.Controls.Add(Me.chkbox_IgnoreHeader)
        Me.Panel2.Controls.Add(Me.lblPN)
        Me.Panel2.Controls.Add(Me.lblRefDes)
        Me.Panel2.Controls.Add(Me.cbox_After)
        Me.Panel2.Controls.Add(Me.cbox_Before)
        Me.Panel2.Controls.Add(Me.lbl_Browse_A)
        Me.Panel2.Controls.Add(Me.btn_Browse_Excel)
        Me.Panel2.Controls.Add(Me.tbox_Input)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(291, 456)
        Me.Panel2.TabIndex = 91
        '
        'lbl_worksheet
        '
        Me.lbl_worksheet.AutoSize = True
        Me.lbl_worksheet.Enabled = False
        Me.lbl_worksheet.Location = New System.Drawing.Point(12, 123)
        Me.lbl_worksheet.Name = "lbl_worksheet"
        Me.lbl_worksheet.Size = New System.Drawing.Size(62, 13)
        Me.lbl_worksheet.TabIndex = 107
        Me.lbl_worksheet.Text = "Worksheet:"
        '
        'cbox_worksheet
        '
        Me.cbox_worksheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_worksheet.Enabled = False
        Me.cbox_worksheet.FormattingEnabled = True
        Me.cbox_worksheet.Location = New System.Drawing.Point(80, 120)
        Me.cbox_worksheet.Name = "cbox_worksheet"
        Me.cbox_worksheet.Size = New System.Drawing.Size(199, 21)
        Me.cbox_worksheet.TabIndex = 106
        '
        'chkbox_ReadAllSheets
        '
        Me.chkbox_ReadAllSheets.AutoSize = True
        Me.chkbox_ReadAllSheets.Enabled = False
        Me.chkbox_ReadAllSheets.Location = New System.Drawing.Point(12, 95)
        Me.chkbox_ReadAllSheets.Name = "chkbox_ReadAllSheets"
        Me.chkbox_ReadAllSheets.Size = New System.Drawing.Size(99, 17)
        Me.chkbox_ReadAllSheets.TabIndex = 105
        Me.chkbox_ReadAllSheets.Text = "Read all sheets"
        Me.chkbox_ReadAllSheets.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chkbox_IgnoreDuplicatesMissing)
        Me.Panel1.Controls.Add(Me.btnEvaluate)
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Controls.Add(Me.Panel7)
        Me.Panel1.Controls.Add(Me.lblNoChange)
        Me.Panel1.Controls.Add(Me.lblDupPN)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.Label6)
        Me.Panel1.Controls.Add(Me.lblToRename)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.Panel5)
        Me.Panel1.Controls.Add(Me.lblMissingSymbols)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 323)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(291, 133)
        Me.Panel1.TabIndex = 104
        '
        'chkbox_IgnoreDuplicatesMissing
        '
        Me.chkbox_IgnoreDuplicatesMissing.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkbox_IgnoreDuplicatesMissing.AutoSize = True
        Me.chkbox_IgnoreDuplicatesMissing.Checked = True
        Me.chkbox_IgnoreDuplicatesMissing.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_IgnoreDuplicatesMissing.Location = New System.Drawing.Point(9, 101)
        Me.chkbox_IgnoreDuplicatesMissing.Name = "chkbox_IgnoreDuplicatesMissing"
        Me.chkbox_IgnoreDuplicatesMissing.Size = New System.Drawing.Size(168, 17)
        Me.chkbox_IgnoreDuplicatesMissing.TabIndex = 93
        Me.chkbox_IgnoreDuplicatesMissing.Text = "Ignore Duplicates and Missing"
        Me.chkbox_IgnoreDuplicatesMissing.UseVisualStyleBackColor = True
        '
        'Panel6
        '
        Me.Panel6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel6.BackColor = System.Drawing.Color.Yellow
        Me.Panel6.Location = New System.Drawing.Point(9, 74)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(15, 15)
        Me.Panel6.TabIndex = 91
        '
        'Panel7
        '
        Me.Panel7.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel7.BackColor = System.Drawing.Color.Orange
        Me.Panel7.Location = New System.Drawing.Point(9, 53)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(15, 15)
        Me.Panel7.TabIndex = 90
        '
        'lblNoChange
        '
        Me.lblNoChange.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblNoChange.AutoSize = True
        Me.lblNoChange.Location = New System.Drawing.Point(100, 75)
        Me.lblNoChange.Name = "lblNoChange"
        Me.lblNoChange.Size = New System.Drawing.Size(13, 13)
        Me.lblNoChange.TabIndex = 89
        Me.lblNoChange.Text = "0"
        '
        'lblDupPN
        '
        Me.lblDupPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblDupPN.AutoSize = True
        Me.lblDupPN.Location = New System.Drawing.Point(100, 33)
        Me.lblDupPN.Name = "lblDupPN"
        Me.lblDupPN.Size = New System.Drawing.Size(13, 13)
        Me.lblDupPN.TabIndex = 88
        Me.lblDupPN.Text = "0"
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(28, 14)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(66, 13)
        Me.Label1.TabIndex = 98
        Me.Label1.Text = "To Rename:"
        '
        'Label6
        '
        Me.Label6.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(30, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 92
        Me.Label6.Text = "No Change:"
        '
        'lblToRename
        '
        Me.lblToRename.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblToRename.AutoSize = True
        Me.lblToRename.Location = New System.Drawing.Point(100, 14)
        Me.lblToRename.Name = "lblToRename"
        Me.lblToRename.Size = New System.Drawing.Size(13, 13)
        Me.lblToRename.TabIndex = 97
        Me.lblToRename.Text = "0"
        '
        'Label5
        '
        Me.Label5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 33)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 93
        Me.Label5.Text = "Duplicate:"
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 96
        Me.Label3.Text = "Missing:"
        '
        'Panel5
        '
        Me.Panel5.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(9, 32)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(15, 15)
        Me.Panel5.TabIndex = 95
        '
        'lblMissingSymbols
        '
        Me.lblMissingSymbols.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblMissingSymbols.AutoSize = True
        Me.lblMissingSymbols.Location = New System.Drawing.Point(100, 54)
        Me.lblMissingSymbols.Name = "lblMissingSymbols"
        Me.lblMissingSymbols.Size = New System.Drawing.Size(13, 13)
        Me.lblMissingSymbols.TabIndex = 94
        Me.lblMissingSymbols.Text = "0"
        '
        'chkbox_IgnoreHeader
        '
        Me.chkbox_IgnoreHeader.AutoSize = True
        Me.chkbox_IgnoreHeader.Enabled = False
        Me.chkbox_IgnoreHeader.Location = New System.Drawing.Point(12, 68)
        Me.chkbox_IgnoreHeader.Name = "chkbox_IgnoreHeader"
        Me.chkbox_IgnoreHeader.Size = New System.Drawing.Size(119, 17)
        Me.chkbox_IgnoreHeader.TabIndex = 103
        Me.chkbox_IgnoreHeader.Text = "Ignore Header Row"
        Me.chkbox_IgnoreHeader.UseVisualStyleBackColor = True
        '
        'lblPN
        '
        Me.lblPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPN.AutoSize = True
        Me.lblPN.Enabled = False
        Me.lblPN.Location = New System.Drawing.Point(191, 97)
        Me.lblPN.Name = "lblPN"
        Me.lblPN.Size = New System.Drawing.Size(32, 13)
        Me.lblPN.TabIndex = 102
        Me.lblPN.Text = "After:"
        '
        'lblRefDes
        '
        Me.lblRefDes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRefDes.AutoSize = True
        Me.lblRefDes.Enabled = False
        Me.lblRefDes.Location = New System.Drawing.Point(182, 70)
        Me.lblRefDes.Name = "lblRefDes"
        Me.lblRefDes.Size = New System.Drawing.Size(41, 13)
        Me.lblRefDes.TabIndex = 101
        Me.lblRefDes.Text = "Before:"
        '
        'cbox_After
        '
        Me.cbox_After.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_After.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_After.Enabled = False
        Me.cbox_After.FormattingEnabled = True
        Me.cbox_After.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_After.Location = New System.Drawing.Point(229, 93)
        Me.cbox_After.Name = "cbox_After"
        Me.cbox_After.Size = New System.Drawing.Size(50, 21)
        Me.cbox_After.TabIndex = 99
        '
        'cbox_Before
        '
        Me.cbox_Before.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Before.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Before.Enabled = False
        Me.cbox_Before.FormattingEnabled = True
        Me.cbox_Before.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_Before.Location = New System.Drawing.Point(229, 66)
        Me.cbox_Before.Name = "cbox_Before"
        Me.cbox_Before.Size = New System.Drawing.Size(50, 21)
        Me.cbox_Before.TabIndex = 100
        '
        'lbl_Browse_A
        '
        Me.lbl_Browse_A.AutoSize = True
        Me.lbl_Browse_A.Location = New System.Drawing.Point(12, 9)
        Me.lbl_Browse_A.Name = "lbl_Browse_A"
        Me.lbl_Browse_A.Size = New System.Drawing.Size(68, 13)
        Me.lbl_Browse_A.TabIndex = 85
        Me.lbl_Browse_A.Text = "From-To File:"
        '
        'btn_Browse_Excel
        '
        Me.btn_Browse_Excel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Browse_Excel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Browse_Excel.Location = New System.Drawing.Point(249, 25)
        Me.btn_Browse_Excel.Name = "btn_Browse_Excel"
        Me.btn_Browse_Excel.Size = New System.Drawing.Size(30, 20)
        Me.btn_Browse_Excel.TabIndex = 84
        Me.btn_Browse_Excel.Text = "..."
        Me.btn_Browse_Excel.UseVisualStyleBackColor = False
        '
        'tbox_Input
        '
        Me.tbox_Input.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_Input.Location = New System.Drawing.Point(12, 25)
        Me.tbox_Input.Name = "tbox_Input"
        Me.tbox_Input.Size = New System.Drawing.Size(228, 20)
        Me.tbox_Input.TabIndex = 83
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Menu
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 456)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(568, 22)
        Me.StatusStrip1.TabIndex = 91
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_status
        '
        Me.ts_status.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.ts_status.Name = "ts_status"
        Me.ts_status.Size = New System.Drawing.Size(483, 17)
        Me.ts_status.Spring = True
        Me.ts_status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WaitGif
        '
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'dgv_Symbols
        '
        Me.dgv_Symbols.AllowUserToAddRows = False
        Me.dgv_Symbols.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgv_Symbols.BackgroundColor = System.Drawing.SystemColors.ControlLight
        Me.dgv_Symbols.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgv_Symbols.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Symbols.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvFrom, Me.dgvTo})
        Me.dgv_Symbols.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_Symbols.Location = New System.Drawing.Point(291, 0)
        Me.dgv_Symbols.Name = "dgv_Symbols"
        Me.dgv_Symbols.RowHeadersVisible = False
        Me.dgv_Symbols.Size = New System.Drawing.Size(277, 406)
        Me.dgv_Symbols.TabIndex = 92
        '
        'dgvFrom
        '
        Me.dgvFrom.HeaderText = "Symbol Before"
        Me.dgvFrom.Name = "dgvFrom"
        Me.dgvFrom.ReadOnly = True
        '
        'dgvTo
        '
        Me.dgvTo.HeaderText = "Symbol After"
        Me.dgvTo.Name = "dgvTo"
        Me.dgvTo.ReadOnly = True
        '
        'pl_Process
        '
        Me.pl_Process.Controls.Add(Me.chkbox_HealPDB)
        Me.pl_Process.Controls.Add(Me.btnProcess)
        Me.pl_Process.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pl_Process.Enabled = False
        Me.pl_Process.Location = New System.Drawing.Point(291, 406)
        Me.pl_Process.Name = "pl_Process"
        Me.pl_Process.Size = New System.Drawing.Size(277, 50)
        Me.pl_Process.TabIndex = 93
        '
        'frmRenameSymbols
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(568, 478)
        Me.Controls.Add(Me.dgv_Symbols)
        Me.Controls.Add(Me.pl_Process)
        Me.Controls.Add(Me.Panel2)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(550, 500)
        Me.Name = "frmRenameSymbols"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rename Symbols"
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        CType(Me.dgv_Symbols, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pl_Process.ResumeLayout(False)
        Me.pl_Process.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents lbl_Browse_A As System.Windows.Forms.Label
    Friend WithEvents tbox_Input As System.Windows.Forms.TextBox
    Friend WithEvents btn_Browse_Excel As System.Windows.Forms.Button
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents chkbox_HealPDB As System.Windows.Forms.CheckBox
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents dgv_Symbols As System.Windows.Forms.DataGridView
    Friend WithEvents dgvFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pl_Process As System.Windows.Forms.Panel
    Friend WithEvents btnEvaluate As System.Windows.Forms.Button
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblMissingSymbols As System.Windows.Forms.Label
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents lblDupPN As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents lblNoChange As System.Windows.Forms.Label
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents lblToRename As System.Windows.Forms.Label
    Friend WithEvents chkbox_IgnoreHeader As System.Windows.Forms.CheckBox
    Friend WithEvents lblPN As System.Windows.Forms.Label
    Friend WithEvents lblRefDes As System.Windows.Forms.Label
    Friend WithEvents cbox_After As System.Windows.Forms.ComboBox
    Friend WithEvents cbox_Before As System.Windows.Forms.ComboBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents chkbox_IgnoreDuplicatesMissing As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_ReadAllSheets As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_worksheet As ComboBox
    Friend WithEvents lbl_worksheet As System.Windows.Forms.Label
End Class
