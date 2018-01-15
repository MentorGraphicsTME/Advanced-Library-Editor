<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmModifyPartDes
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
        Me.btn_Modify = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tv_Parts = New System.Windows.Forms.TreeView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.btn_AllProperties = New System.Windows.Forms.RadioButton()
        Me.btn_UsedProperties = New System.Windows.Forms.RadioButton()
        Me.chkbox_Regex = New System.Windows.Forms.CheckBox()
        Me.lbl_Modification = New System.Windows.Forms.Label()
        Me.cbox_Modification = New System.Windows.Forms.ComboBox()
        Me.tbox_NewPattern = New System.Windows.Forms.TextBox()
        Me.tbox_Pattern = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lbl_Property = New System.Windows.Forms.Label()
        Me.cbox_PartProperties = New System.Windows.Forms.ComboBox()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.dgv_Parts = New System.Windows.Forms.DataGridView()
        Me.dgvFrom = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.dgvTo = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.plProperties = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbox_BatchProperties = New System.Windows.Forms.ComboBox()
        Me.pl_Process = New System.Windows.Forms.Panel()
        Me.chkbox_AddProperty = New System.Windows.Forms.CheckBox()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.pl_FileInput = New System.Windows.Forms.Panel()
        Me.pl_Excel = New System.Windows.Forms.Panel()
        Me.cboxActiveSheet = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbox_PN = New System.Windows.Forms.ComboBox()
        Me.cbox_Cell = New System.Windows.Forms.ComboBox()
        Me.chkbox_ReadAllSheets = New System.Windows.Forms.CheckBox()
        Me.lblPN = New System.Windows.Forms.Label()
        Me.chkbox_IgnoreHeader = New System.Windows.Forms.CheckBox()
        Me.lblValue = New System.Windows.Forms.Label()
        Me.lbl_AltCellFile = New System.Windows.Forms.Label()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.tbox_Input = New System.Windows.Forms.TextBox()
        Me.btn_Browse = New System.Windows.Forms.Button()
        Me.lbl_PropertyValue = New System.Windows.Forms.Label()
        Me.CheckBox1 = New System.Windows.Forms.CheckBox()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.Panel1.SuspendLayout()
        CType(Me.dgv_Parts, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.plProperties.SuspendLayout()
        Me.pl_Process.SuspendLayout()
        Me.pl_FileInput.SuspendLayout()
        Me.pl_Excel.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_Modify
        '
        Me.btn_Modify.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Modify.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Modify.Enabled = False
        Me.btn_Modify.Location = New System.Drawing.Point(166, 421)
        Me.btn_Modify.Name = "btn_Modify"
        Me.btn_Modify.Size = New System.Drawing.Size(102, 23)
        Me.btn_Modify.TabIndex = 0
        Me.btn_Modify.Text = "Process"
        Me.btn_Modify.UseVisualStyleBackColor = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox2)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.CheckBox1)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbl_PropertyValue)
        Me.SplitContainer1.Panel2.Controls.Add(Me.chkbox_Regex)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbl_Modification)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cbox_Modification)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbox_NewPattern)
        Me.SplitContainer1.Panel2.Controls.Add(Me.tbox_Pattern)
        Me.SplitContainer1.Panel2.Controls.Add(Me.Label2)
        Me.SplitContainer1.Panel2.Controls.Add(Me.lbl_Property)
        Me.SplitContainer1.Panel2.Controls.Add(Me.btn_Modify)
        Me.SplitContainer1.Panel2.Controls.Add(Me.cbox_PartProperties)
        Me.SplitContainer1.Size = New System.Drawing.Size(585, 457)
        Me.SplitContainer1.SplitterDistance = 301
        Me.SplitContainer1.TabIndex = 30
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tv_Parts)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(301, 376)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Parts"
        '
        'tv_Parts
        '
        Me.tv_Parts.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv_Parts.CheckBoxes = True
        Me.tv_Parts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Parts.Location = New System.Drawing.Point(3, 16)
        Me.tv_Parts.Name = "tv_Parts"
        Me.tv_Parts.Size = New System.Drawing.Size(295, 357)
        Me.tv_Parts.TabIndex = 24
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.btn_AllProperties)
        Me.GroupBox2.Controls.Add(Me.btn_UsedProperties)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox2.Location = New System.Drawing.Point(0, 376)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(301, 81)
        Me.GroupBox2.TabIndex = 27
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "View Options"
        '
        'btn_AllProperties
        '
        Me.btn_AllProperties.AutoSize = True
        Me.btn_AllProperties.Location = New System.Drawing.Point(28, 25)
        Me.btn_AllProperties.Name = "btn_AllProperties"
        Me.btn_AllProperties.Size = New System.Drawing.Size(86, 17)
        Me.btn_AllProperties.TabIndex = 30
        Me.btn_AllProperties.TabStop = True
        Me.btn_AllProperties.Text = "All Properties"
        Me.btn_AllProperties.UseVisualStyleBackColor = True
        '
        'btn_UsedProperties
        '
        Me.btn_UsedProperties.AutoSize = True
        Me.btn_UsedProperties.Location = New System.Drawing.Point(28, 51)
        Me.btn_UsedProperties.Name = "btn_UsedProperties"
        Me.btn_UsedProperties.Size = New System.Drawing.Size(124, 17)
        Me.btn_UsedProperties.TabIndex = 29
        Me.btn_UsedProperties.TabStop = True
        Me.btn_UsedProperties.Text = "Only Used Properties"
        Me.btn_UsedProperties.UseVisualStyleBackColor = True
        '
        'chkbox_Regex
        '
        Me.chkbox_Regex.AutoSize = True
        Me.chkbox_Regex.Location = New System.Drawing.Point(10, 142)
        Me.chkbox_Regex.Name = "chkbox_Regex"
        Me.chkbox_Regex.Size = New System.Drawing.Size(139, 17)
        Me.chkbox_Regex.TabIndex = 33
        Me.chkbox_Regex.Text = "Use Regular Expression"
        Me.chkbox_Regex.UseVisualStyleBackColor = True
        Me.chkbox_Regex.Visible = False
        '
        'lbl_Modification
        '
        Me.lbl_Modification.AutoSize = True
        Me.lbl_Modification.Location = New System.Drawing.Point(7, 49)
        Me.lbl_Modification.Name = "lbl_Modification"
        Me.lbl_Modification.Size = New System.Drawing.Size(67, 13)
        Me.lbl_Modification.TabIndex = 32
        Me.lbl_Modification.Text = "Modification:"
        '
        'cbox_Modification
        '
        Me.cbox_Modification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Modification.FormattingEnabled = True
        Me.cbox_Modification.Items.AddRange(New Object() {"Add", "Append", "Prepend", "Remove", "Replace"})
        Me.cbox_Modification.Location = New System.Drawing.Point(10, 66)
        Me.cbox_Modification.Name = "cbox_Modification"
        Me.cbox_Modification.Size = New System.Drawing.Size(108, 21)
        Me.cbox_Modification.TabIndex = 31
        '
        'tbox_NewPattern
        '
        Me.tbox_NewPattern.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_NewPattern.Location = New System.Drawing.Point(10, 178)
        Me.tbox_NewPattern.Name = "tbox_NewPattern"
        Me.tbox_NewPattern.Size = New System.Drawing.Size(261, 20)
        Me.tbox_NewPattern.TabIndex = 29
        '
        'tbox_Pattern
        '
        Me.tbox_Pattern.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_Pattern.Location = New System.Drawing.Point(10, 116)
        Me.tbox_Pattern.Name = "tbox_Pattern"
        Me.tbox_Pattern.Size = New System.Drawing.Size(261, 20)
        Me.tbox_Pattern.TabIndex = 27
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(7, 100)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(131, 13)
        Me.Label2.TabIndex = 26
        Me.Label2.Text = "Search for Property Value:"
        '
        'lbl_Property
        '
        Me.lbl_Property.AutoSize = True
        Me.lbl_Property.Location = New System.Drawing.Point(7, 9)
        Me.lbl_Property.Name = "lbl_Property"
        Me.lbl_Property.Size = New System.Drawing.Size(49, 13)
        Me.lbl_Property.TabIndex = 25
        Me.lbl_Property.Text = "Property:"
        '
        'cbox_PartProperties
        '
        Me.cbox_PartProperties.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_PartProperties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_PartProperties.FormattingEnabled = True
        Me.cbox_PartProperties.Location = New System.Drawing.Point(10, 25)
        Me.cbox_PartProperties.Name = "cbox_PartProperties"
        Me.cbox_PartProperties.Size = New System.Drawing.Size(258, 21)
        Me.cbox_PartProperties.TabIndex = 24
        '
        'WaitGif
        '
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'ts_Status
        '
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(514, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Select some parts to begin..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 489)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(599, 22)
        Me.StatusStrip1.TabIndex = 29
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(599, 489)
        Me.TabControl1.TabIndex = 31
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.SplitContainer1)
        Me.TabPage1.Location = New System.Drawing.Point(4, 4)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(591, 463)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Single Value"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Panel1)
        Me.TabPage2.Controls.Add(Me.pl_FileInput)
        Me.TabPage2.Location = New System.Drawing.Point(4, 4)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(591, 463)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Batch"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.dgv_Parts)
        Me.Panel1.Controls.Add(Me.plProperties)
        Me.Panel1.Controls.Add(Me.pl_Process)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(272, 3)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(316, 457)
        Me.Panel1.TabIndex = 98
        '
        'dgv_Parts
        '
        Me.dgv_Parts.AllowUserToAddRows = False
        Me.dgv_Parts.BackgroundColor = System.Drawing.SystemColors.ControlLight
        Me.dgv_Parts.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgv_Parts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgv_Parts.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.dgvFrom, Me.dgvTo})
        Me.dgv_Parts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgv_Parts.Location = New System.Drawing.Point(0, 59)
        Me.dgv_Parts.Name = "dgv_Parts"
        Me.dgv_Parts.ReadOnly = True
        Me.dgv_Parts.RowHeadersVisible = False
        Me.dgv_Parts.Size = New System.Drawing.Size(316, 348)
        Me.dgv_Parts.TabIndex = 94
        '
        'dgvFrom
        '
        Me.dgvFrom.HeaderText = "Part Number"
        Me.dgvFrom.Name = "dgvFrom"
        Me.dgvFrom.ReadOnly = True
        Me.dgvFrom.Width = 145
        '
        'dgvTo
        '
        Me.dgvTo.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.dgvTo.HeaderText = "Value"
        Me.dgvTo.Name = "dgvTo"
        Me.dgvTo.ReadOnly = True
        '
        'plProperties
        '
        Me.plProperties.Controls.Add(Me.Label4)
        Me.plProperties.Controls.Add(Me.cbox_BatchProperties)
        Me.plProperties.Dock = System.Windows.Forms.DockStyle.Top
        Me.plProperties.Location = New System.Drawing.Point(0, 0)
        Me.plProperties.Name = "plProperties"
        Me.plProperties.Size = New System.Drawing.Size(316, 59)
        Me.plProperties.TabIndex = 96
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(-3, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(94, 13)
        Me.Label4.TabIndex = 99
        Me.Label4.Text = "Property to modify:"
        '
        'cbox_BatchProperties
        '
        Me.cbox_BatchProperties.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_BatchProperties.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_BatchProperties.FormattingEnabled = True
        Me.cbox_BatchProperties.Location = New System.Drawing.Point(0, 25)
        Me.cbox_BatchProperties.Name = "cbox_BatchProperties"
        Me.cbox_BatchProperties.Size = New System.Drawing.Size(316, 21)
        Me.cbox_BatchProperties.TabIndex = 98
        '
        'pl_Process
        '
        Me.pl_Process.Controls.Add(Me.chkbox_AddProperty)
        Me.pl_Process.Controls.Add(Me.btnProcess)
        Me.pl_Process.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pl_Process.Enabled = False
        Me.pl_Process.Location = New System.Drawing.Point(0, 407)
        Me.pl_Process.Name = "pl_Process"
        Me.pl_Process.Size = New System.Drawing.Size(316, 50)
        Me.pl_Process.TabIndex = 95
        '
        'chkbox_AddProperty
        '
        Me.chkbox_AddProperty.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.chkbox_AddProperty.AutoSize = True
        Me.chkbox_AddProperty.Checked = True
        Me.chkbox_AddProperty.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_AddProperty.Location = New System.Drawing.Point(18, 20)
        Me.chkbox_AddProperty.Name = "chkbox_AddProperty"
        Me.chkbox_AddProperty.Size = New System.Drawing.Size(178, 17)
        Me.chkbox_AddProperty.TabIndex = 93
        Me.chkbox_AddProperty.Text = "Add property if not found on part"
        Me.chkbox_AddProperty.UseVisualStyleBackColor = True
        '
        'btnProcess
        '
        Me.btnProcess.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnProcess.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnProcess.Location = New System.Drawing.Point(209, 6)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(87, 35)
        Me.btnProcess.TabIndex = 91
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = False
        '
        'pl_FileInput
        '
        Me.pl_FileInput.Controls.Add(Me.pl_Excel)
        Me.pl_FileInput.Controls.Add(Me.lbl_AltCellFile)
        Me.pl_FileInput.Controls.Add(Me.btnRead)
        Me.pl_FileInput.Controls.Add(Me.tbox_Input)
        Me.pl_FileInput.Controls.Add(Me.btn_Browse)
        Me.pl_FileInput.Dock = System.Windows.Forms.DockStyle.Left
        Me.pl_FileInput.Location = New System.Drawing.Point(3, 3)
        Me.pl_FileInput.Name = "pl_FileInput"
        Me.pl_FileInput.Size = New System.Drawing.Size(269, 457)
        Me.pl_FileInput.TabIndex = 97
        '
        'pl_Excel
        '
        Me.pl_Excel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pl_Excel.Controls.Add(Me.cboxActiveSheet)
        Me.pl_Excel.Controls.Add(Me.Label5)
        Me.pl_Excel.Controls.Add(Me.cbox_PN)
        Me.pl_Excel.Controls.Add(Me.cbox_Cell)
        Me.pl_Excel.Controls.Add(Me.chkbox_ReadAllSheets)
        Me.pl_Excel.Controls.Add(Me.lblPN)
        Me.pl_Excel.Controls.Add(Me.chkbox_IgnoreHeader)
        Me.pl_Excel.Controls.Add(Me.lblValue)
        Me.pl_Excel.Enabled = False
        Me.pl_Excel.Location = New System.Drawing.Point(0, 52)
        Me.pl_Excel.Name = "pl_Excel"
        Me.pl_Excel.Size = New System.Drawing.Size(269, 351)
        Me.pl_Excel.TabIndex = 119
        '
        'cboxActiveSheet
        '
        Me.cboxActiveSheet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxActiveSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxActiveSheet.Enabled = False
        Me.cboxActiveSheet.FormattingEnabled = True
        Me.cboxActiveSheet.Location = New System.Drawing.Point(80, 64)
        Me.cboxActiveSheet.Name = "cboxActiveSheet"
        Me.cboxActiveSheet.Size = New System.Drawing.Size(181, 21)
        Me.cboxActiveSheet.TabIndex = 120
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Enabled = False
        Me.Label5.Location = New System.Drawing.Point(9, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(62, 13)
        Me.Label5.TabIndex = 119
        Me.Label5.Text = "Worksheet:"
        '
        'cbox_PN
        '
        Me.cbox_PN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_PN.BackColor = System.Drawing.SystemColors.Control
        Me.cbox_PN.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_PN.FormattingEnabled = True
        Me.cbox_PN.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_PN.Location = New System.Drawing.Point(211, 10)
        Me.cbox_PN.Name = "cbox_PN"
        Me.cbox_PN.Size = New System.Drawing.Size(50, 21)
        Me.cbox_PN.TabIndex = 114
        '
        'cbox_Cell
        '
        Me.cbox_Cell.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Cell.BackColor = System.Drawing.SystemColors.Control
        Me.cbox_Cell.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Cell.FormattingEnabled = True
        Me.cbox_Cell.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_Cell.Location = New System.Drawing.Point(211, 37)
        Me.cbox_Cell.Name = "cbox_Cell"
        Me.cbox_Cell.Size = New System.Drawing.Size(50, 21)
        Me.cbox_Cell.TabIndex = 113
        '
        'chkbox_ReadAllSheets
        '
        Me.chkbox_ReadAllSheets.AutoSize = True
        Me.chkbox_ReadAllSheets.Checked = True
        Me.chkbox_ReadAllSheets.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_ReadAllSheets.Location = New System.Drawing.Point(12, 40)
        Me.chkbox_ReadAllSheets.Name = "chkbox_ReadAllSheets"
        Me.chkbox_ReadAllSheets.Size = New System.Drawing.Size(99, 17)
        Me.chkbox_ReadAllSheets.TabIndex = 118
        Me.chkbox_ReadAllSheets.Text = "Read all sheets"
        Me.chkbox_ReadAllSheets.UseVisualStyleBackColor = True
        '
        'lblPN
        '
        Me.lblPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPN.Location = New System.Drawing.Point(136, 14)
        Me.lblPN.Name = "lblPN"
        Me.lblPN.Size = New System.Drawing.Size(69, 13)
        Me.lblPN.TabIndex = 115
        Me.lblPN.Text = "Part Number:"
        Me.lblPN.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkbox_IgnoreHeader
        '
        Me.chkbox_IgnoreHeader.AutoSize = True
        Me.chkbox_IgnoreHeader.Checked = True
        Me.chkbox_IgnoreHeader.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_IgnoreHeader.Location = New System.Drawing.Point(12, 12)
        Me.chkbox_IgnoreHeader.Name = "chkbox_IgnoreHeader"
        Me.chkbox_IgnoreHeader.Size = New System.Drawing.Size(119, 17)
        Me.chkbox_IgnoreHeader.TabIndex = 117
        Me.chkbox_IgnoreHeader.Text = "Ignore Header Row"
        Me.chkbox_IgnoreHeader.UseVisualStyleBackColor = True
        '
        'lblValue
        '
        Me.lblValue.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblValue.Location = New System.Drawing.Point(139, 41)
        Me.lblValue.Name = "lblValue"
        Me.lblValue.Size = New System.Drawing.Size(66, 13)
        Me.lblValue.TabIndex = 116
        Me.lblValue.Text = "Value:"
        Me.lblValue.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_AltCellFile
        '
        Me.lbl_AltCellFile.AutoSize = True
        Me.lbl_AltCellFile.Location = New System.Drawing.Point(12, 9)
        Me.lbl_AltCellFile.Name = "lbl_AltCellFile"
        Me.lbl_AltCellFile.Size = New System.Drawing.Size(50, 13)
        Me.lbl_AltCellFile.TabIndex = 85
        Me.lbl_AltCellFile.Text = "Input File"
        '
        'btnRead
        '
        Me.btnRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRead.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRead.Enabled = False
        Me.btnRead.Location = New System.Drawing.Point(171, 414)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(92, 34)
        Me.btnRead.TabIndex = 84
        Me.btnRead.Text = "Read"
        Me.btnRead.UseVisualStyleBackColor = False
        '
        'tbox_Input
        '
        Me.tbox_Input.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_Input.Location = New System.Drawing.Point(12, 25)
        Me.tbox_Input.Name = "tbox_Input"
        Me.tbox_Input.Size = New System.Drawing.Size(213, 20)
        Me.tbox_Input.TabIndex = 83
        '
        'btn_Browse
        '
        Me.btn_Browse.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Browse.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Browse.Location = New System.Drawing.Point(231, 25)
        Me.btn_Browse.Name = "btn_Browse"
        Me.btn_Browse.Size = New System.Drawing.Size(30, 20)
        Me.btn_Browse.TabIndex = 84
        Me.btn_Browse.Text = "..."
        Me.btn_Browse.UseVisualStyleBackColor = False
        '
        'lbl_PropertyValue
        '
        Me.lbl_PropertyValue.AutoSize = True
        Me.lbl_PropertyValue.Location = New System.Drawing.Point(7, 160)
        Me.lbl_PropertyValue.Name = "lbl_PropertyValue"
        Me.lbl_PropertyValue.Size = New System.Drawing.Size(79, 13)
        Me.lbl_PropertyValue.TabIndex = 34
        Me.lbl_PropertyValue.Text = "Property Value:"
        '
        'CheckBox1
        '
        Me.CheckBox1.AutoSize = True
        Me.CheckBox1.Location = New System.Drawing.Point(156, 142)
        Me.CheckBox1.Name = "CheckBox1"
        Me.CheckBox1.Size = New System.Drawing.Size(73, 17)
        Me.CheckBox1.TabIndex = 35
        Me.CheckBox1.Text = "Substring "
        Me.CheckBox1.UseVisualStyleBackColor = True
        Me.CheckBox1.Visible = False
        '
        'frmModifyPartDes
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(599, 511)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MinimumSize = New System.Drawing.Size(615, 545)
        Me.Name = "frmModifyPartDes"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modify Part Properties"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        Me.SplitContainer1.Panel2.PerformLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage2.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        CType(Me.dgv_Parts, System.ComponentModel.ISupportInitialize).EndInit()
        Me.plProperties.ResumeLayout(False)
        Me.plProperties.PerformLayout()
        Me.pl_Process.ResumeLayout(False)
        Me.pl_Process.PerformLayout()
        Me.pl_FileInput.ResumeLayout(False)
        Me.pl_FileInput.PerformLayout()
        Me.pl_Excel.ResumeLayout(False)
        Me.pl_Excel.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Modify As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tv_Parts As System.Windows.Forms.TreeView
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents tbox_Pattern As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lbl_Property As System.Windows.Forms.Label
    Friend WithEvents cbox_PartProperties As System.Windows.Forms.ComboBox
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tbox_NewPattern As System.Windows.Forms.TextBox
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents TabPage1 As System.Windows.Forms.TabPage
    Friend WithEvents TabPage2 As System.Windows.Forms.TabPage
    Friend WithEvents dgv_Parts As System.Windows.Forms.DataGridView
    Friend WithEvents pl_Process As System.Windows.Forms.Panel
    Friend WithEvents chkbox_AddProperty As System.Windows.Forms.CheckBox
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents plProperties As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbox_BatchProperties As System.Windows.Forms.ComboBox
    Friend WithEvents dgvFrom As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents dgvTo As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents pl_FileInput As System.Windows.Forms.Panel
    Friend WithEvents pl_Excel As System.Windows.Forms.Panel
    Friend WithEvents cboxActiveSheet As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbox_PN As System.Windows.Forms.ComboBox
    Friend WithEvents cbox_Cell As System.Windows.Forms.ComboBox
    Friend WithEvents chkbox_ReadAllSheets As System.Windows.Forms.CheckBox
    Friend WithEvents lblPN As System.Windows.Forms.Label
    Friend WithEvents chkbox_IgnoreHeader As System.Windows.Forms.CheckBox
    Friend WithEvents lblValue As System.Windows.Forms.Label
    Friend WithEvents lbl_AltCellFile As System.Windows.Forms.Label
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents tbox_Input As System.Windows.Forms.TextBox
    Friend WithEvents btn_Browse As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Modification As System.Windows.Forms.Label
    Friend WithEvents cbox_Modification As ComboBox
    Friend WithEvents chkbox_Regex As System.Windows.Forms.CheckBox
    Friend WithEvents btn_UsedProperties As RadioButton
    Friend WithEvents btn_AllProperties As RadioButton
    Friend WithEvents lbl_PropertyValue As System.Windows.Forms.Label
    Friend WithEvents CheckBox1 As System.Windows.Forms.CheckBox
End Class
