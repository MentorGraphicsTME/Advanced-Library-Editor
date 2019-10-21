<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBatch_PDB_Properties
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
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tsm_Threads = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.plSpreadsheet = New System.Windows.Forms.Panel()
        Me.gb_ExcelInfo = New System.Windows.Forms.GroupBox()
        Me.cbox_ignoreHeader = New System.Windows.Forms.CheckBox()
        Me.lbl_duplicate = New System.Windows.Forms.Label()
        Me.btnClearValue = New System.Windows.Forms.Button()
        Me.btnClearName = New System.Windows.Forms.Button()
        Me.btnClearPN = New System.Windows.Forms.Button()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.cbox_Value = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbox_Name = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbox_PartNumber = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboxActiveSheet = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_Excel = New System.Windows.Forms.Label()
        Me.tboxWorkbook = New System.Windows.Forms.TextBox()
        Me.btn_Browse_Excel = New System.Windows.Forms.Button()
        Me.gb_Options = New System.Windows.Forms.GroupBox()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.cboxAction = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.chkbox_AddNonCommonProperty = New System.Windows.Forms.CheckBox()
        Me.dgvInput = New System.Windows.Forms.DataGridView()
        Me.clPartNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clValue = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.StatusStrip1.SuspendLayout()
        Me.plSpreadsheet.SuspendLayout()
        Me.gb_ExcelInfo.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.gb_Options.SuspendLayout()
        CType(Me.dgvInput, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.tsm_Threads, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 568)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(813, 25)
        Me.StatusStrip1.TabIndex = 97
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(723, 20)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Browse to an Excel file to start..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'tsm_Threads
        '
        Me.tsm_Threads.BackColor = System.Drawing.Color.Transparent
        Me.tsm_Threads.BorderSides = CType((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.tsm_Threads.Name = "tsm_Threads"
        Me.tsm_Threads.Size = New System.Drawing.Size(125, 24)
        Me.tsm_Threads.Text = "Active Threads: 0"
        Me.tsm_Threads.Visible = False
        '
        'WaitGif
        '
        Me.WaitGif.BackColor = System.Drawing.Color.Transparent
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 20)
        '
        'plSpreadsheet
        '
        Me.plSpreadsheet.Controls.Add(Me.gb_ExcelInfo)
        Me.plSpreadsheet.Controls.Add(Me.Panel1)
        Me.plSpreadsheet.Controls.Add(Me.gb_Options)
        Me.plSpreadsheet.Dock = System.Windows.Forms.DockStyle.Left
        Me.plSpreadsheet.Location = New System.Drawing.Point(0, 0)
        Me.plSpreadsheet.Name = "plSpreadsheet"
        Me.plSpreadsheet.Size = New System.Drawing.Size(319, 568)
        Me.plSpreadsheet.TabIndex = 98
        '
        'gb_ExcelInfo
        '
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_ignoreHeader)
        Me.gb_ExcelInfo.Controls.Add(Me.lbl_duplicate)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearValue)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearName)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearPN)
        Me.gb_ExcelInfo.Controls.Add(Me.btnRead)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_Value)
        Me.gb_ExcelInfo.Controls.Add(Me.Label5)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_Name)
        Me.gb_ExcelInfo.Controls.Add(Me.Label4)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_PartNumber)
        Me.gb_ExcelInfo.Controls.Add(Me.Label3)
        Me.gb_ExcelInfo.Controls.Add(Me.cboxActiveSheet)
        Me.gb_ExcelInfo.Controls.Add(Me.Label1)
        Me.gb_ExcelInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gb_ExcelInfo.Enabled = False
        Me.gb_ExcelInfo.Location = New System.Drawing.Point(0, 70)
        Me.gb_ExcelInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.gb_ExcelInfo.Name = "gb_ExcelInfo"
        Me.gb_ExcelInfo.Padding = New System.Windows.Forms.Padding(4)
        Me.gb_ExcelInfo.Size = New System.Drawing.Size(319, 383)
        Me.gb_ExcelInfo.TabIndex = 4
        Me.gb_ExcelInfo.TabStop = False
        Me.gb_ExcelInfo.Text = "Excel Info"
        '
        'cbox_ignoreHeader
        '
        Me.cbox_ignoreHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbox_ignoreHeader.AutoSize = True
        Me.cbox_ignoreHeader.Location = New System.Drawing.Point(16, 309)
        Me.cbox_ignoreHeader.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_ignoreHeader.Name = "cbox_ignoreHeader"
        Me.cbox_ignoreHeader.Size = New System.Drawing.Size(245, 21)
        Me.cbox_ignoreHeader.TabIndex = 167
        Me.cbox_ignoreHeader.Text = "Ignore First Row (used as header)"
        Me.cbox_ignoreHeader.UseVisualStyleBackColor = True
        '
        'lbl_duplicate
        '
        Me.lbl_duplicate.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbl_duplicate.AutoSize = True
        Me.lbl_duplicate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_duplicate.ForeColor = System.Drawing.Color.Red
        Me.lbl_duplicate.Location = New System.Drawing.Point(45, 183)
        Me.lbl_duplicate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_duplicate.Name = "lbl_duplicate"
        Me.lbl_duplicate.Size = New System.Drawing.Size(231, 18)
        Me.lbl_duplicate.TabIndex = 165
        Me.lbl_duplicate.Text = "At least one column is a duplicate."
        Me.lbl_duplicate.Visible = False
        '
        'btnClearValue
        '
        Me.btnClearValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearValue.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearValue.Location = New System.Drawing.Point(163, 136)
        Me.btnClearValue.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearValue.Name = "btnClearValue"
        Me.btnClearValue.Size = New System.Drawing.Size(27, 26)
        Me.btnClearValue.TabIndex = 162
        Me.btnClearValue.Text = "X"
        Me.btnClearValue.UseVisualStyleBackColor = False
        Me.btnClearValue.Visible = False
        '
        'btnClearName
        '
        Me.btnClearName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearName.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearName.Location = New System.Drawing.Point(163, 103)
        Me.btnClearName.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearName.Name = "btnClearName"
        Me.btnClearName.Size = New System.Drawing.Size(27, 26)
        Me.btnClearName.TabIndex = 161
        Me.btnClearName.Text = "X"
        Me.btnClearName.UseVisualStyleBackColor = False
        Me.btnClearName.Visible = False
        '
        'btnClearPN
        '
        Me.btnClearPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearPN.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearPN.Location = New System.Drawing.Point(163, 69)
        Me.btnClearPN.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearPN.Name = "btnClearPN"
        Me.btnClearPN.Size = New System.Drawing.Size(27, 26)
        Me.btnClearPN.TabIndex = 160
        Me.btnClearPN.Text = "X"
        Me.btnClearPN.UseVisualStyleBackColor = False
        Me.btnClearPN.Visible = False
        '
        'btnRead
        '
        Me.btnRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRead.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRead.Location = New System.Drawing.Point(214, 338)
        Me.btnRead.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(97, 28)
        Me.btnRead.TabIndex = 94
        Me.btnRead.Text = "Read"
        Me.btnRead.UseVisualStyleBackColor = False
        '
        'cbox_Value
        '
        Me.cbox_Value.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Value.FormattingEnabled = True
        Me.cbox_Value.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_Value.Location = New System.Drawing.Point(108, 136)
        Me.cbox_Value.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_Value.Name = "cbox_Value"
        Me.cbox_Value.Size = New System.Drawing.Size(47, 24)
        Me.cbox_Value.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 140)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 17)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Value:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbox_Name
        '
        Me.cbox_Name.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Name.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Name.FormattingEnabled = True
        Me.cbox_Name.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_Name.Location = New System.Drawing.Point(108, 103)
        Me.cbox_Name.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_Name.Name = "cbox_Name"
        Me.cbox_Name.Size = New System.Drawing.Size(47, 24)
        Me.cbox_Name.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 106)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 17)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Name:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbox_PartNumber
        '
        Me.cbox_PartNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_PartNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_PartNumber.FormattingEnabled = True
        Me.cbox_PartNumber.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_PartNumber.Location = New System.Drawing.Point(108, 69)
        Me.cbox_PartNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_PartNumber.Name = "cbox_PartNumber"
        Me.cbox_PartNumber.Size = New System.Drawing.Size(47, 24)
        Me.cbox_PartNumber.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 73)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Part Number:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboxActiveSheet
        '
        Me.cboxActiveSheet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxActiveSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxActiveSheet.FormattingEnabled = True
        Me.cboxActiveSheet.Location = New System.Drawing.Point(108, 37)
        Me.cboxActiveSheet.Margin = New System.Windows.Forms.Padding(4)
        Me.cboxActiveSheet.Name = "cboxActiveSheet"
        Me.cboxActiveSheet.Size = New System.Drawing.Size(187, 24)
        Me.cboxActiveSheet.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(8, 41)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Worksheet:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lbl_Excel)
        Me.Panel1.Controls.Add(Me.tboxWorkbook)
        Me.Panel1.Controls.Add(Me.btn_Browse_Excel)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(319, 70)
        Me.Panel1.TabIndex = 3
        '
        'lbl_Excel
        '
        Me.lbl_Excel.AutoSize = True
        Me.lbl_Excel.Location = New System.Drawing.Point(16, 11)
        Me.lbl_Excel.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_Excel.Name = "lbl_Excel"
        Me.lbl_Excel.Size = New System.Drawing.Size(93, 17)
        Me.lbl_Excel.TabIndex = 85
        Me.lbl_Excel.Text = "Spreadsheet:"
        '
        'tboxWorkbook
        '
        Me.tboxWorkbook.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tboxWorkbook.Location = New System.Drawing.Point(16, 31)
        Me.tboxWorkbook.Margin = New System.Windows.Forms.Padding(4)
        Me.tboxWorkbook.Name = "tboxWorkbook"
        Me.tboxWorkbook.Size = New System.Drawing.Size(249, 22)
        Me.tboxWorkbook.TabIndex = 83
        '
        'btn_Browse_Excel
        '
        Me.btn_Browse_Excel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Browse_Excel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Browse_Excel.Location = New System.Drawing.Point(273, 30)
        Me.btn_Browse_Excel.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_Browse_Excel.Name = "btn_Browse_Excel"
        Me.btn_Browse_Excel.Size = New System.Drawing.Size(40, 25)
        Me.btn_Browse_Excel.TabIndex = 84
        Me.btn_Browse_Excel.Text = "..."
        Me.btn_Browse_Excel.UseVisualStyleBackColor = False
        '
        'gb_Options
        '
        Me.gb_Options.Controls.Add(Me.btnProcess)
        Me.gb_Options.Controls.Add(Me.cboxAction)
        Me.gb_Options.Controls.Add(Me.Label2)
        Me.gb_Options.Controls.Add(Me.chkbox_AddNonCommonProperty)
        Me.gb_Options.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_Options.Enabled = False
        Me.gb_Options.Location = New System.Drawing.Point(0, 453)
        Me.gb_Options.Margin = New System.Windows.Forms.Padding(4)
        Me.gb_Options.Name = "gb_Options"
        Me.gb_Options.Padding = New System.Windows.Forms.Padding(4)
        Me.gb_Options.Size = New System.Drawing.Size(319, 115)
        Me.gb_Options.TabIndex = 167
        Me.gb_Options.TabStop = False
        Me.gb_Options.Visible = False
        '
        'btnProcess
        '
        Me.btnProcess.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnProcess.Enabled = False
        Me.btnProcess.Location = New System.Drawing.Point(214, 23)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(97, 28)
        Me.btnProcess.TabIndex = 6
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = False
        '
        'cboxAction
        '
        Me.cboxAction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxAction.FormattingEnabled = True
        Me.cboxAction.Items.AddRange(New Object() {"Add/Modify", "Remove"})
        Me.cboxAction.Location = New System.Drawing.Point(74, 25)
        Me.cboxAction.Name = "cboxAction"
        Me.cboxAction.Size = New System.Drawing.Size(134, 24)
        Me.cboxAction.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(16, 29)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 17)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Action:"
        '
        'chkbox_AddNonCommonProperty
        '
        Me.chkbox_AddNonCommonProperty.Checked = True
        Me.chkbox_AddNonCommonProperty.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_AddNonCommonProperty.Location = New System.Drawing.Point(16, 61)
        Me.chkbox_AddNonCommonProperty.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_AddNonCommonProperty.Name = "chkbox_AddNonCommonProperty"
        Me.chkbox_AddNonCommonProperty.Size = New System.Drawing.Size(295, 46)
        Me.chkbox_AddNonCommonProperty.TabIndex = 3
        Me.chkbox_AddNonCommonProperty.Text = "Add Non-Common Properties to Common Property File"
        Me.chkbox_AddNonCommonProperty.UseVisualStyleBackColor = True
        '
        'dgvInput
        '
        Me.dgvInput.AllowUserToAddRows = False
        Me.dgvInput.AllowUserToDeleteRows = False
        Me.dgvInput.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvInput.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.clPartNumber, Me.clName, Me.clValue})
        Me.dgvInput.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvInput.Location = New System.Drawing.Point(319, 0)
        Me.dgvInput.Name = "dgvInput"
        Me.dgvInput.RowHeadersVisible = False
        Me.dgvInput.RowTemplate.Height = 24
        Me.dgvInput.Size = New System.Drawing.Size(494, 568)
        Me.dgvInput.TabIndex = 99
        '
        'clPartNumber
        '
        Me.clPartNumber.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.clPartNumber.HeaderText = "Part Number"
        Me.clPartNumber.Name = "clPartNumber"
        Me.clPartNumber.ReadOnly = True
        '
        'clName
        '
        Me.clName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.clName.HeaderText = "Name"
        Me.clName.Name = "clName"
        Me.clName.ReadOnly = True
        '
        'clValue
        '
        Me.clValue.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.clValue.HeaderText = "Value"
        Me.clValue.Name = "clValue"
        Me.clValue.ReadOnly = True
        '
        'frmBatch_PDB_Properties
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(813, 593)
        Me.Controls.Add(Me.dgvInput)
        Me.Controls.Add(Me.plSpreadsheet)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmBatch_PDB_Properties"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Batch PDB Properties"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.plSpreadsheet.ResumeLayout(False)
        Me.gb_ExcelInfo.ResumeLayout(False)
        Me.gb_ExcelInfo.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.gb_Options.ResumeLayout(False)
        Me.gb_Options.PerformLayout()
        CType(Me.dgvInput, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents StatusStrip1 As StatusStrip
    Friend WithEvents ts_Status As ToolStripStatusLabel
    Friend WithEvents tsm_Threads As ToolStripStatusLabel
    Friend WithEvents WaitGif As ToolStripStatusLabel
    Friend WithEvents plSpreadsheet As Panel
    Friend WithEvents gb_ExcelInfo As System.Windows.Forms.GroupBox
    Friend WithEvents lbl_duplicate As System.Windows.Forms.Label
    Friend WithEvents btnClearValue As System.Windows.Forms.Button
    Friend WithEvents btnClearName As System.Windows.Forms.Button
    Friend WithEvents btnClearPN As System.Windows.Forms.Button
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents cbox_Value As ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbox_Name As ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbox_PartNumber As ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboxActiveSheet As ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As Panel
    Friend WithEvents lbl_Excel As System.Windows.Forms.Label
    Friend WithEvents tboxWorkbook As System.Windows.Forms.TextBox
    Friend WithEvents btn_Browse_Excel As System.Windows.Forms.Button
    Friend WithEvents dgvInput As DataGridView
    Friend WithEvents clPartNumber As DataGridViewTextBoxColumn
    Friend WithEvents clName As DataGridViewTextBoxColumn
    Friend WithEvents clValue As DataGridViewTextBoxColumn
    Friend WithEvents cbox_ignoreHeader As System.Windows.Forms.CheckBox
    Friend WithEvents gb_Options As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_AddNonCommonProperty As System.Windows.Forms.CheckBox
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents cboxAction As ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
End Class
