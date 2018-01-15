<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSwapPart
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
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgvPNSwap = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.chkbox_Label = New System.Windows.Forms.CheckBox()
        Me.chkbox_Name = New System.Windows.Forms.CheckBox()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.chkbox_Number = New System.Windows.Forms.CheckBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pl_Excel = New System.Windows.Forms.Panel()
        Me.cboxActiveSheet = New System.Windows.Forms.ComboBox()
        Me.lbl_worksheet = New System.Windows.Forms.Label()
        Me.cbox_Before = New System.Windows.Forms.ComboBox()
        Me.cbox_After = New System.Windows.Forms.ComboBox()
        Me.chkbox_ReadAllSheets = New System.Windows.Forms.CheckBox()
        Me.lblRefDes = New System.Windows.Forms.Label()
        Me.chkbox_IgnoreHeader = New System.Windows.Forms.CheckBox()
        Me.lblPN = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_Browse_SwapPN = New System.Windows.Forms.Button()
        Me.tbox_Input = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnEvaluate = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblNoChange = New System.Windows.Forms.Label()
        Me.lblMissing = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblDup = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgvPNSwap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel8.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.pl_Excel.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 456)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(568, 22)
        Me.StatusStrip1.TabIndex = 11
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(483, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Browse to a swap file to being..."
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
        Me.Panel1.Controls.Add(Me.dgvPNSwap)
        Me.Panel1.Controls.Add(Me.Panel8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel1.Location = New System.Drawing.Point(290, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(278, 456)
        Me.Panel1.TabIndex = 13
        '
        'dgvPNSwap
        '
        Me.dgvPNSwap.AllowUserToAddRows = False
        Me.dgvPNSwap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPNSwap.BackgroundColor = System.Drawing.SystemColors.ControlLight
        Me.dgvPNSwap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvPNSwap.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPNSwap.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle1
        Me.dgvPNSwap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPNSwap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPNSwap.DefaultCellStyle = DataGridViewCellStyle2
        Me.dgvPNSwap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPNSwap.Location = New System.Drawing.Point(0, 0)
        Me.dgvPNSwap.Name = "dgvPNSwap"
        Me.dgvPNSwap.ReadOnly = True
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPNSwap.RowHeadersDefaultCellStyle = DataGridViewCellStyle3
        Me.dgvPNSwap.RowHeadersVisible = False
        Me.dgvPNSwap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPNSwap.Size = New System.Drawing.Size(278, 375)
        Me.dgvPNSwap.TabIndex = 8
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.HeaderText = "Original"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.HeaderText = "New"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        '
        'Panel8
        '
        Me.Panel8.Controls.Add(Me.chkbox_Label)
        Me.Panel8.Controls.Add(Me.chkbox_Name)
        Me.Panel8.Controls.Add(Me.btnProcess)
        Me.Panel8.Controls.Add(Me.chkbox_Number)
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel8.Enabled = False
        Me.Panel8.Location = New System.Drawing.Point(0, 375)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(278, 81)
        Me.Panel8.TabIndex = 9
        '
        'chkbox_Label
        '
        Me.chkbox_Label.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkbox_Label.AutoSize = True
        Me.chkbox_Label.Location = New System.Drawing.Point(18, 55)
        Me.chkbox_Label.Name = "chkbox_Label"
        Me.chkbox_Label.Size = New System.Drawing.Size(86, 17)
        Me.chkbox_Label.TabIndex = 0
        Me.chkbox_Label.Text = "Modify Label"
        Me.chkbox_Label.UseVisualStyleBackColor = True
        '
        'chkbox_Name
        '
        Me.chkbox_Name.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkbox_Name.AutoSize = True
        Me.chkbox_Name.Location = New System.Drawing.Point(18, 32)
        Me.chkbox_Name.Name = "chkbox_Name"
        Me.chkbox_Name.Size = New System.Drawing.Size(88, 17)
        Me.chkbox_Name.TabIndex = 0
        Me.chkbox_Name.Text = "Modify Name"
        Me.chkbox_Name.UseVisualStyleBackColor = True
        '
        'btnProcess
        '
        Me.btnProcess.Anchor = System.Windows.Forms.AnchorStyles.Right
        Me.btnProcess.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnProcess.Location = New System.Drawing.Point(190, 32)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(77, 40)
        Me.btnProcess.TabIndex = 8
        Me.btnProcess.Text = "Update PDB"
        Me.btnProcess.UseVisualStyleBackColor = False
        '
        'chkbox_Number
        '
        Me.chkbox_Number.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkbox_Number.AutoSize = True
        Me.chkbox_Number.Location = New System.Drawing.Point(18, 9)
        Me.chkbox_Number.Name = "chkbox_Number"
        Me.chkbox_Number.Size = New System.Drawing.Size(97, 17)
        Me.chkbox_Number.TabIndex = 0
        Me.chkbox_Number.Text = "Modify Number"
        Me.chkbox_Number.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.pl_Excel)
        Me.GroupBox1.Controls.Add(Me.Panel3)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(290, 456)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Input"
        '
        'pl_Excel
        '
        Me.pl_Excel.Controls.Add(Me.cboxActiveSheet)
        Me.pl_Excel.Controls.Add(Me.lbl_worksheet)
        Me.pl_Excel.Controls.Add(Me.cbox_Before)
        Me.pl_Excel.Controls.Add(Me.cbox_After)
        Me.pl_Excel.Controls.Add(Me.chkbox_ReadAllSheets)
        Me.pl_Excel.Controls.Add(Me.lblRefDes)
        Me.pl_Excel.Controls.Add(Me.chkbox_IgnoreHeader)
        Me.pl_Excel.Controls.Add(Me.lblPN)
        Me.pl_Excel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pl_Excel.Enabled = False
        Me.pl_Excel.Location = New System.Drawing.Point(3, 69)
        Me.pl_Excel.Name = "pl_Excel"
        Me.pl_Excel.Size = New System.Drawing.Size(284, 101)
        Me.pl_Excel.TabIndex = 120
        '
        'cboxActiveSheet
        '
        Me.cboxActiveSheet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxActiveSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxActiveSheet.FormattingEnabled = True
        Me.cboxActiveSheet.Location = New System.Drawing.Point(80, 66)
        Me.cboxActiveSheet.Name = "cboxActiveSheet"
        Me.cboxActiveSheet.Size = New System.Drawing.Size(196, 21)
        Me.cboxActiveSheet.TabIndex = 122
        '
        'lbl_worksheet
        '
        Me.lbl_worksheet.AutoSize = True
        Me.lbl_worksheet.Location = New System.Drawing.Point(9, 69)
        Me.lbl_worksheet.Name = "lbl_worksheet"
        Me.lbl_worksheet.Size = New System.Drawing.Size(62, 13)
        Me.lbl_worksheet.TabIndex = 121
        Me.lbl_worksheet.Text = "Worksheet:"
        '
        'cbox_Before
        '
        Me.cbox_Before.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Before.BackColor = System.Drawing.SystemColors.Control
        Me.cbox_Before.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Before.FormattingEnabled = True
        Me.cbox_Before.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_Before.Location = New System.Drawing.Point(226, 10)
        Me.cbox_Before.Name = "cbox_Before"
        Me.cbox_Before.Size = New System.Drawing.Size(50, 21)
        Me.cbox_Before.TabIndex = 114
        '
        'cbox_After
        '
        Me.cbox_After.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_After.BackColor = System.Drawing.SystemColors.Control
        Me.cbox_After.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_After.FormattingEnabled = True
        Me.cbox_After.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_After.Location = New System.Drawing.Point(226, 37)
        Me.cbox_After.Name = "cbox_After"
        Me.cbox_After.Size = New System.Drawing.Size(50, 21)
        Me.cbox_After.TabIndex = 113
        '
        'chkbox_ReadAllSheets
        '
        Me.chkbox_ReadAllSheets.AutoSize = True
        Me.chkbox_ReadAllSheets.Location = New System.Drawing.Point(12, 40)
        Me.chkbox_ReadAllSheets.Name = "chkbox_ReadAllSheets"
        Me.chkbox_ReadAllSheets.Size = New System.Drawing.Size(99, 17)
        Me.chkbox_ReadAllSheets.TabIndex = 118
        Me.chkbox_ReadAllSheets.Text = "Read all sheets"
        Me.chkbox_ReadAllSheets.UseVisualStyleBackColor = True
        '
        'lblRefDes
        '
        Me.lblRefDes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblRefDes.AutoSize = True
        Me.lblRefDes.Location = New System.Drawing.Point(179, 14)
        Me.lblRefDes.Name = "lblRefDes"
        Me.lblRefDes.Size = New System.Drawing.Size(41, 13)
        Me.lblRefDes.TabIndex = 115
        Me.lblRefDes.Text = "Before:"
        '
        'chkbox_IgnoreHeader
        '
        Me.chkbox_IgnoreHeader.AutoSize = True
        Me.chkbox_IgnoreHeader.Location = New System.Drawing.Point(12, 12)
        Me.chkbox_IgnoreHeader.Name = "chkbox_IgnoreHeader"
        Me.chkbox_IgnoreHeader.Size = New System.Drawing.Size(119, 17)
        Me.chkbox_IgnoreHeader.TabIndex = 117
        Me.chkbox_IgnoreHeader.Text = "Ignore Header Row"
        Me.chkbox_IgnoreHeader.UseVisualStyleBackColor = True
        '
        'lblPN
        '
        Me.lblPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPN.AutoSize = True
        Me.lblPN.Location = New System.Drawing.Point(188, 41)
        Me.lblPN.Name = "lblPN"
        Me.lblPN.Size = New System.Drawing.Size(32, 13)
        Me.lblPN.TabIndex = 116
        Me.lblPN.Text = "After:"
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.btn_Browse_SwapPN)
        Me.Panel3.Controls.Add(Me.tbox_Input)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 16)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(284, 53)
        Me.Panel3.TabIndex = 93
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(53, 13)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "Swap List"
        '
        'btn_Browse_SwapPN
        '
        Me.btn_Browse_SwapPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Browse_SwapPN.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Browse_SwapPN.Location = New System.Drawing.Point(247, 25)
        Me.btn_Browse_SwapPN.Name = "btn_Browse_SwapPN"
        Me.btn_Browse_SwapPN.Size = New System.Drawing.Size(29, 20)
        Me.btn_Browse_SwapPN.TabIndex = 59
        Me.btn_Browse_SwapPN.Text = "..."
        Me.btn_Browse_SwapPN.UseVisualStyleBackColor = False
        '
        'tbox_Input
        '
        Me.tbox_Input.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_Input.Location = New System.Drawing.Point(12, 25)
        Me.tbox_Input.Name = "tbox_Input"
        Me.tbox_Input.Size = New System.Drawing.Size(232, 20)
        Me.tbox_Input.TabIndex = 58
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnEvaluate)
        Me.Panel2.Controls.Add(Me.Panel7)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lblNoChange)
        Me.Panel2.Controls.Add(Me.lblMissing)
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.lblDup)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(3, 375)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(284, 78)
        Me.Panel2.TabIndex = 92
        '
        'btnEvaluate
        '
        Me.btnEvaluate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEvaluate.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnEvaluate.Enabled = False
        Me.btnEvaluate.Location = New System.Drawing.Point(197, 41)
        Me.btnEvaluate.Name = "btnEvaluate"
        Me.btnEvaluate.Size = New System.Drawing.Size(77, 27)
        Me.btnEvaluate.TabIndex = 91
        Me.btnEvaluate.Text = "Evaluate List"
        Me.btnEvaluate.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Orange
        Me.Panel7.Location = New System.Drawing.Point(9, 9)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(15, 15)
        Me.Panel7.TabIndex = 84
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 31)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(45, 13)
        Me.Label3.TabIndex = 90
        Me.Label3.Text = "Missing:"
        '
        'lblNoChange
        '
        Me.lblNoChange.AutoSize = True
        Me.lblNoChange.Location = New System.Drawing.Point(100, 52)
        Me.lblNoChange.Name = "lblNoChange"
        Me.lblNoChange.Size = New System.Drawing.Size(13, 13)
        Me.lblNoChange.TabIndex = 83
        Me.lblNoChange.Text = "0"
        '
        'lblMissing
        '
        Me.lblMissing.AutoSize = True
        Me.lblMissing.Location = New System.Drawing.Point(100, 31)
        Me.lblMissing.Name = "lblMissing"
        Me.lblMissing.Size = New System.Drawing.Size(13, 13)
        Me.lblMissing.TabIndex = 88
        Me.lblMissing.Text = "0"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Yellow
        Me.Panel6.Location = New System.Drawing.Point(9, 51)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(15, 15)
        Me.Panel6.TabIndex = 85
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(9, 30)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(15, 15)
        Me.Panel5.TabIndex = 89
        '
        'lblDup
        '
        Me.lblDup.AutoSize = True
        Me.lblDup.Location = New System.Drawing.Point(100, 10)
        Me.lblDup.Name = "lblDup"
        Me.lblDup.Size = New System.Drawing.Size(13, 13)
        Me.lblDup.TabIndex = 82
        Me.lblDup.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 10)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(55, 13)
        Me.Label5.TabIndex = 87
        Me.Label5.Text = "Duplicate:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(30, 52)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "No Change:"
        '
        'frmSwapPart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(568, 478)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Name = "frmSwapPart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Rename Parts"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgvPNSwap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel8.ResumeLayout(False)
        Me.Panel8.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.pl_Excel.ResumeLayout(False)
        Me.pl_Excel.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents dgvPNSwap As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Panel8 As System.Windows.Forms.Panel
    Friend WithEvents chkbox_Label As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_Name As System.Windows.Forms.CheckBox
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents chkbox_Number As System.Windows.Forms.CheckBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents pl_Excel As System.Windows.Forms.Panel
    Friend WithEvents cboxActiveSheet As System.Windows.Forms.ComboBox
    Friend WithEvents lbl_worksheet As System.Windows.Forms.Label
    Friend WithEvents cbox_Before As System.Windows.Forms.ComboBox
    Friend WithEvents cbox_After As System.Windows.Forms.ComboBox
    Friend WithEvents chkbox_ReadAllSheets As System.Windows.Forms.CheckBox
    Friend WithEvents lblRefDes As System.Windows.Forms.Label
    Friend WithEvents chkbox_IgnoreHeader As System.Windows.Forms.CheckBox
    Friend WithEvents lblPN As System.Windows.Forms.Label
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btn_Browse_SwapPN As System.Windows.Forms.Button
    Friend WithEvents tbox_Input As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnEvaluate As System.Windows.Forms.Button
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblNoChange As System.Windows.Forms.Label
    Friend WithEvents lblMissing As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblDup As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
End Class
