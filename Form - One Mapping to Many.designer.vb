<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmDuplicatePart
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
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.plSpreadsheetInfo = New System.Windows.Forms.Panel()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.chkbox_IgnoreDuplicatesMissing = New System.Windows.Forms.CheckBox()
        Me.btnEvaluate = New System.Windows.Forms.Button()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblNoChange = New System.Windows.Forms.Label()
        Me.lblMissingPNs = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblDupPN = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.gb_ExcelInfo = New System.Windows.Forms.GroupBox()
        Me.chkbox_SINotation = New System.Windows.Forms.CheckBox()
        Me.chkbox_IgnoreHeader = New System.Windows.Forms.CheckBox()
        Me.chkbox_ReadAllSheets = New System.Windows.Forms.CheckBox()
        Me.btnClearValue = New System.Windows.Forms.Button()
        Me.btnClearDescription = New System.Windows.Forms.Button()
        Me.btnClearNew = New System.Windows.Forms.Button()
        Me.btnClearSource = New System.Windows.Forms.Button()
        Me.cbox_Value = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbox_Description = New System.Windows.Forms.ComboBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.cbox_After = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cbox_Before = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboxActiveSheet = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_Browse_SwapPN = New System.Windows.Forms.Button()
        Me.tbox_Input = New System.Windows.Forms.TextBox()
        Me.dgvPNSwap = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.gb_Options = New System.Windows.Forms.GroupBox()
        Me.chkbox_RemoveOriginal = New System.Windows.Forms.CheckBox()
        Me.btn_Copy = New System.Windows.Forms.Button()
        Me.btnClearName = New System.Windows.Forms.Button()
        Me.btnClearLabel = New System.Windows.Forms.Button()
        Me.cboxName = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboxLabel = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.plSpreadsheetInfo.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.gb_ExcelInfo.SuspendLayout()
        Me.Panel3.SuspendLayout()
        CType(Me.dgvPNSwap, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gb_Options.SuspendLayout()
        Me.SuspendLayout()
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 585)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(851, 25)
        Me.StatusStrip1.TabIndex = 15
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(761, 20)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Browse to a part list to begin..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WaitGif
        '
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 20)
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.plSpreadsheetInfo)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(423, 585)
        Me.Panel1.TabIndex = 17
        '
        'plSpreadsheetInfo
        '
        Me.plSpreadsheetInfo.Controls.Add(Me.Panel2)
        Me.plSpreadsheetInfo.Controls.Add(Me.gb_ExcelInfo)
        Me.plSpreadsheetInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.plSpreadsheetInfo.Enabled = False
        Me.plSpreadsheetInfo.Location = New System.Drawing.Point(0, 65)
        Me.plSpreadsheetInfo.Name = "plSpreadsheetInfo"
        Me.plSpreadsheetInfo.Size = New System.Drawing.Size(423, 520)
        Me.plSpreadsheetInfo.TabIndex = 97
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.chkbox_IgnoreDuplicatesMissing)
        Me.Panel2.Controls.Add(Me.btnEvaluate)
        Me.Panel2.Controls.Add(Me.Panel7)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lblNoChange)
        Me.Panel2.Controls.Add(Me.lblMissingPNs)
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.lblDupPN)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(0, 394)
        Me.Panel2.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(423, 126)
        Me.Panel2.TabIndex = 98
        '
        'chkbox_IgnoreDuplicatesMissing
        '
        Me.chkbox_IgnoreDuplicatesMissing.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkbox_IgnoreDuplicatesMissing.AutoSize = True
        Me.chkbox_IgnoreDuplicatesMissing.Checked = True
        Me.chkbox_IgnoreDuplicatesMissing.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_IgnoreDuplicatesMissing.Location = New System.Drawing.Point(12, 92)
        Me.chkbox_IgnoreDuplicatesMissing.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_IgnoreDuplicatesMissing.Name = "chkbox_IgnoreDuplicatesMissing"
        Me.chkbox_IgnoreDuplicatesMissing.Size = New System.Drawing.Size(219, 21)
        Me.chkbox_IgnoreDuplicatesMissing.TabIndex = 93
        Me.chkbox_IgnoreDuplicatesMissing.Text = "Ignore Duplicates and Missing"
        Me.chkbox_IgnoreDuplicatesMissing.UseVisualStyleBackColor = True
        '
        'btnEvaluate
        '
        Me.btnEvaluate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEvaluate.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnEvaluate.Location = New System.Drawing.Point(303, 80)
        Me.btnEvaluate.Margin = New System.Windows.Forms.Padding(4)
        Me.btnEvaluate.Name = "btnEvaluate"
        Me.btnEvaluate.Size = New System.Drawing.Size(103, 33)
        Me.btnEvaluate.TabIndex = 91
        Me.btnEvaluate.Text = "Evaluate List"
        Me.btnEvaluate.UseVisualStyleBackColor = False
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Orange
        Me.Panel7.Location = New System.Drawing.Point(12, 11)
        Me.Panel7.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(20, 18)
        Me.Panel7.TabIndex = 84
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(40, 38)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(59, 17)
        Me.Label3.TabIndex = 90
        Me.Label3.Text = "Missing:"
        '
        'lblNoChange
        '
        Me.lblNoChange.AutoSize = True
        Me.lblNoChange.Location = New System.Drawing.Point(133, 64)
        Me.lblNoChange.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblNoChange.Name = "lblNoChange"
        Me.lblNoChange.Size = New System.Drawing.Size(16, 17)
        Me.lblNoChange.TabIndex = 83
        Me.lblNoChange.Text = "0"
        '
        'lblMissingPNs
        '
        Me.lblMissingPNs.AutoSize = True
        Me.lblMissingPNs.Location = New System.Drawing.Point(133, 38)
        Me.lblMissingPNs.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMissingPNs.Name = "lblMissingPNs"
        Me.lblMissingPNs.Size = New System.Drawing.Size(16, 17)
        Me.lblMissingPNs.TabIndex = 88
        Me.lblMissingPNs.Text = "0"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Yellow
        Me.Panel6.Location = New System.Drawing.Point(12, 63)
        Me.Panel6.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(20, 18)
        Me.Panel6.TabIndex = 85
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(12, 37)
        Me.Panel5.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(20, 18)
        Me.Panel5.TabIndex = 89
        '
        'lblDupPN
        '
        Me.lblDupPN.AutoSize = True
        Me.lblDupPN.Location = New System.Drawing.Point(133, 12)
        Me.lblDupPN.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDupPN.Name = "lblDupPN"
        Me.lblDupPN.Size = New System.Drawing.Size(16, 17)
        Me.lblDupPN.TabIndex = 82
        Me.lblDupPN.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(40, 12)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(71, 17)
        Me.Label5.TabIndex = 87
        Me.Label5.Text = "Duplicate:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(40, 64)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(83, 17)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "No Change:"
        '
        'gb_ExcelInfo
        '
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearName)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearLabel)
        Me.gb_ExcelInfo.Controls.Add(Me.cboxName)
        Me.gb_ExcelInfo.Controls.Add(Me.Label1)
        Me.gb_ExcelInfo.Controls.Add(Me.cboxLabel)
        Me.gb_ExcelInfo.Controls.Add(Me.Label7)
        Me.gb_ExcelInfo.Controls.Add(Me.chkbox_SINotation)
        Me.gb_ExcelInfo.Controls.Add(Me.chkbox_IgnoreHeader)
        Me.gb_ExcelInfo.Controls.Add(Me.chkbox_ReadAllSheets)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearValue)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearDescription)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearNew)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearSource)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_Value)
        Me.gb_ExcelInfo.Controls.Add(Me.Label2)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_Description)
        Me.gb_ExcelInfo.Controls.Add(Me.Label8)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_After)
        Me.gb_ExcelInfo.Controls.Add(Me.Label9)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_Before)
        Me.gb_ExcelInfo.Controls.Add(Me.Label10)
        Me.gb_ExcelInfo.Controls.Add(Me.cboxActiveSheet)
        Me.gb_ExcelInfo.Controls.Add(Me.Label11)
        Me.gb_ExcelInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gb_ExcelInfo.Location = New System.Drawing.Point(0, 0)
        Me.gb_ExcelInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.gb_ExcelInfo.Name = "gb_ExcelInfo"
        Me.gb_ExcelInfo.Padding = New System.Windows.Forms.Padding(4)
        Me.gb_ExcelInfo.Size = New System.Drawing.Size(423, 520)
        Me.gb_ExcelInfo.TabIndex = 97
        Me.gb_ExcelInfo.TabStop = False
        Me.gb_ExcelInfo.Text = "Excel Info"
        '
        'chkbox_SINotation
        '
        Me.chkbox_SINotation.AutoSize = True
        Me.chkbox_SINotation.Location = New System.Drawing.Point(12, 307)
        Me.chkbox_SINotation.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_SINotation.Name = "chkbox_SINotation"
        Me.chkbox_SINotation.Size = New System.Drawing.Size(194, 21)
        Me.chkbox_SINotation.TabIndex = 168
        Me.chkbox_SINotation.Text = "Values contain SI notation"
        Me.chkbox_SINotation.UseVisualStyleBackColor = True
        '
        'chkbox_IgnoreHeader
        '
        Me.chkbox_IgnoreHeader.AutoSize = True
        Me.chkbox_IgnoreHeader.Location = New System.Drawing.Point(12, 336)
        Me.chkbox_IgnoreHeader.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_IgnoreHeader.Name = "chkbox_IgnoreHeader"
        Me.chkbox_IgnoreHeader.Size = New System.Drawing.Size(245, 21)
        Me.chkbox_IgnoreHeader.TabIndex = 166
        Me.chkbox_IgnoreHeader.Text = "Ignore First Row (used as header)"
        Me.chkbox_IgnoreHeader.UseVisualStyleBackColor = True
        '
        'chkbox_ReadAllSheets
        '
        Me.chkbox_ReadAllSheets.AutoSize = True
        Me.chkbox_ReadAllSheets.Location = New System.Drawing.Point(12, 365)
        Me.chkbox_ReadAllSheets.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_ReadAllSheets.Name = "chkbox_ReadAllSheets"
        Me.chkbox_ReadAllSheets.Size = New System.Drawing.Size(131, 21)
        Me.chkbox_ReadAllSheets.TabIndex = 165
        Me.chkbox_ReadAllSheets.Text = "Read All Sheets"
        Me.chkbox_ReadAllSheets.UseVisualStyleBackColor = True
        '
        'btnClearValue
        '
        Me.btnClearValue.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearValue.Location = New System.Drawing.Point(259, 255)
        Me.btnClearValue.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearValue.Name = "btnClearValue"
        Me.btnClearValue.Size = New System.Drawing.Size(27, 26)
        Me.btnClearValue.TabIndex = 162
        Me.btnClearValue.Text = "X"
        Me.btnClearValue.UseVisualStyleBackColor = False
        Me.btnClearValue.Visible = False
        '
        'btnClearDescription
        '
        Me.btnClearDescription.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearDescription.Location = New System.Drawing.Point(259, 222)
        Me.btnClearDescription.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearDescription.Name = "btnClearDescription"
        Me.btnClearDescription.Size = New System.Drawing.Size(27, 26)
        Me.btnClearDescription.TabIndex = 161
        Me.btnClearDescription.Text = "X"
        Me.btnClearDescription.UseVisualStyleBackColor = False
        Me.btnClearDescription.Visible = False
        '
        'btnClearNew
        '
        Me.btnClearNew.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearNew.Location = New System.Drawing.Point(259, 103)
        Me.btnClearNew.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearNew.Name = "btnClearNew"
        Me.btnClearNew.Size = New System.Drawing.Size(27, 26)
        Me.btnClearNew.TabIndex = 160
        Me.btnClearNew.Text = "X"
        Me.btnClearNew.UseVisualStyleBackColor = False
        Me.btnClearNew.Visible = False
        '
        'btnClearSource
        '
        Me.btnClearSource.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearSource.Location = New System.Drawing.Point(259, 70)
        Me.btnClearSource.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearSource.Name = "btnClearSource"
        Me.btnClearSource.Size = New System.Drawing.Size(27, 26)
        Me.btnClearSource.TabIndex = 159
        Me.btnClearSource.Text = "X"
        Me.btnClearSource.UseVisualStyleBackColor = False
        Me.btnClearSource.Visible = False
        '
        'cbox_Value
        '
        Me.cbox_Value.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Value.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Value.FormattingEnabled = True
        Me.cbox_Value.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_Value.Location = New System.Drawing.Point(108, 255)
        Me.cbox_Value.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_Value.Name = "cbox_Value"
        Me.cbox_Value.Size = New System.Drawing.Size(131, 24)
        Me.cbox_Value.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(5, 259)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Value:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbox_Description
        '
        Me.cbox_Description.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Description.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Description.FormattingEnabled = True
        Me.cbox_Description.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_Description.Location = New System.Drawing.Point(108, 222)
        Me.cbox_Description.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_Description.Name = "cbox_Description"
        Me.cbox_Description.Size = New System.Drawing.Size(131, 24)
        Me.cbox_Description.TabIndex = 1
        '
        'Label8
        '
        Me.Label8.Location = New System.Drawing.Point(5, 225)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(99, 17)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Description:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbox_After
        '
        Me.cbox_After.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_After.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_After.FormattingEnabled = True
        Me.cbox_After.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_After.Location = New System.Drawing.Point(108, 103)
        Me.cbox_After.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_After.Name = "cbox_After"
        Me.cbox_After.Size = New System.Drawing.Size(131, 24)
        Me.cbox_After.TabIndex = 1
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(5, 107)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 17)
        Me.Label9.TabIndex = 0
        Me.Label9.Text = "New PN:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbox_Before
        '
        Me.cbox_Before.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Before.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Before.FormattingEnabled = True
        Me.cbox_Before.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_Before.Location = New System.Drawing.Point(108, 70)
        Me.cbox_Before.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_Before.Name = "cbox_Before"
        Me.cbox_Before.Size = New System.Drawing.Size(131, 24)
        Me.cbox_Before.TabIndex = 1
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(5, 74)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(99, 17)
        Me.Label10.TabIndex = 0
        Me.Label10.Text = "Source PN:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.cboxActiveSheet.Size = New System.Drawing.Size(291, 24)
        Me.cboxActiveSheet.TabIndex = 1
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(5, 41)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(99, 17)
        Me.Label11.TabIndex = 0
        Me.Label11.Text = "Worksheet:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.btn_Browse_SwapPN)
        Me.Panel3.Controls.Add(Me.tbox_Input)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(423, 65)
        Me.Panel3.TabIndex = 94
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 11)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(93, 17)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "Spreadsheet:"
        '
        'btn_Browse_SwapPN
        '
        Me.btn_Browse_SwapPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Browse_SwapPN.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Browse_SwapPN.Location = New System.Drawing.Point(373, 31)
        Me.btn_Browse_SwapPN.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_Browse_SwapPN.Name = "btn_Browse_SwapPN"
        Me.btn_Browse_SwapPN.Size = New System.Drawing.Size(39, 25)
        Me.btn_Browse_SwapPN.TabIndex = 59
        Me.btn_Browse_SwapPN.Text = "..."
        Me.btn_Browse_SwapPN.UseVisualStyleBackColor = False
        '
        'tbox_Input
        '
        Me.tbox_Input.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_Input.Location = New System.Drawing.Point(16, 31)
        Me.tbox_Input.Margin = New System.Windows.Forms.Padding(4)
        Me.tbox_Input.Name = "tbox_Input"
        Me.tbox_Input.Size = New System.Drawing.Size(352, 22)
        Me.tbox_Input.TabIndex = 58
        '
        'dgvPNSwap
        '
        Me.dgvPNSwap.AllowUserToAddRows = False
        Me.dgvPNSwap.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill
        Me.dgvPNSwap.BackgroundColor = System.Drawing.SystemColors.ControlLight
        Me.dgvPNSwap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.dgvPNSwap.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPNSwap.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle4
        Me.dgvPNSwap.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvPNSwap.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2})
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window
        DataGridViewCellStyle5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText
        DataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.[False]
        Me.dgvPNSwap.DefaultCellStyle = DataGridViewCellStyle5
        Me.dgvPNSwap.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvPNSwap.Location = New System.Drawing.Point(423, 0)
        Me.dgvPNSwap.Margin = New System.Windows.Forms.Padding(4)
        Me.dgvPNSwap.Name = "dgvPNSwap"
        Me.dgvPNSwap.ReadOnly = True
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle6.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.dgvPNSwap.RowHeadersDefaultCellStyle = DataGridViewCellStyle6
        Me.dgvPNSwap.RowHeadersVisible = False
        Me.dgvPNSwap.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvPNSwap.Size = New System.Drawing.Size(428, 512)
        Me.dgvPNSwap.TabIndex = 18
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
        'gb_Options
        '
        Me.gb_Options.Controls.Add(Me.chkbox_RemoveOriginal)
        Me.gb_Options.Controls.Add(Me.btn_Copy)
        Me.gb_Options.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_Options.Enabled = False
        Me.gb_Options.Location = New System.Drawing.Point(423, 512)
        Me.gb_Options.Margin = New System.Windows.Forms.Padding(4)
        Me.gb_Options.Name = "gb_Options"
        Me.gb_Options.Padding = New System.Windows.Forms.Padding(4)
        Me.gb_Options.Size = New System.Drawing.Size(428, 73)
        Me.gb_Options.TabIndex = 19
        Me.gb_Options.TabStop = False
        Me.gb_Options.Text = "Options"
        '
        'chkbox_RemoveOriginal
        '
        Me.chkbox_RemoveOriginal.AutoSize = True
        Me.chkbox_RemoveOriginal.Location = New System.Drawing.Point(21, 30)
        Me.chkbox_RemoveOriginal.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_RemoveOriginal.Name = "chkbox_RemoveOriginal"
        Me.chkbox_RemoveOriginal.Size = New System.Drawing.Size(161, 21)
        Me.chkbox_RemoveOriginal.TabIndex = 64
        Me.chkbox_RemoveOriginal.Text = "Remove original part"
        Me.chkbox_RemoveOriginal.UseVisualStyleBackColor = True
        '
        'btn_Copy
        '
        Me.btn_Copy.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Copy.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Copy.Location = New System.Drawing.Point(309, 23)
        Me.btn_Copy.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_Copy.Name = "btn_Copy"
        Me.btn_Copy.Size = New System.Drawing.Size(103, 33)
        Me.btn_Copy.TabIndex = 63
        Me.btn_Copy.Text = "Copy"
        Me.btn_Copy.UseVisualStyleBackColor = False
        '
        'btnClearName
        '
        Me.btnClearName.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearName.Location = New System.Drawing.Point(259, 178)
        Me.btnClearName.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearName.Name = "btnClearName"
        Me.btnClearName.Size = New System.Drawing.Size(27, 26)
        Me.btnClearName.TabIndex = 174
        Me.btnClearName.Text = "X"
        Me.btnClearName.UseVisualStyleBackColor = False
        Me.btnClearName.Visible = False
        '
        'btnClearLabel
        '
        Me.btnClearLabel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearLabel.Location = New System.Drawing.Point(259, 145)
        Me.btnClearLabel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearLabel.Name = "btnClearLabel"
        Me.btnClearLabel.Size = New System.Drawing.Size(27, 26)
        Me.btnClearLabel.TabIndex = 173
        Me.btnClearLabel.Text = "X"
        Me.btnClearLabel.UseVisualStyleBackColor = False
        Me.btnClearLabel.Visible = False
        '
        'cboxName
        '
        Me.cboxName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxName.FormattingEnabled = True
        Me.cboxName.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cboxName.Location = New System.Drawing.Point(108, 178)
        Me.cboxName.Margin = New System.Windows.Forms.Padding(4)
        Me.cboxName.Name = "cboxName"
        Me.cboxName.Size = New System.Drawing.Size(131, 24)
        Me.cboxName.TabIndex = 171
        '
        'Label1
        '
        Me.Label1.Location = New System.Drawing.Point(5, 182)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(99, 17)
        Me.Label1.TabIndex = 169
        Me.Label1.Text = "Part Name:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboxLabel
        '
        Me.cboxLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxLabel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxLabel.FormattingEnabled = True
        Me.cboxLabel.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cboxLabel.Location = New System.Drawing.Point(108, 145)
        Me.cboxLabel.Margin = New System.Windows.Forms.Padding(4)
        Me.cboxLabel.Name = "cboxLabel"
        Me.cboxLabel.Size = New System.Drawing.Size(131, 24)
        Me.cboxLabel.TabIndex = 172
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(5, 148)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 17)
        Me.Label7.TabIndex = 170
        Me.Label7.Text = "Part Label:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'frmDuplicatePart
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(851, 610)
        Me.Controls.Add(Me.dgvPNSwap)
        Me.Controls.Add(Me.gb_Options)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MinimumSize = New System.Drawing.Size(661, 543)
        Me.Name = "frmDuplicatePart"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Copy One to Many"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.plSpreadsheetInfo.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.gb_ExcelInfo.ResumeLayout(False)
        Me.gb_ExcelInfo.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.Panel3.PerformLayout()
        CType(Me.dgvPNSwap, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gb_Options.ResumeLayout(False)
        Me.gb_Options.PerformLayout()
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
    Friend WithEvents gb_Options As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_RemoveOriginal As System.Windows.Forms.CheckBox
    Friend WithEvents btn_Copy As System.Windows.Forms.Button
    Friend WithEvents plSpreadsheetInfo As Panel
    Friend WithEvents Panel2 As Panel
    Friend WithEvents chkbox_IgnoreDuplicatesMissing As System.Windows.Forms.CheckBox
    Friend WithEvents btnEvaluate As System.Windows.Forms.Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblNoChange As System.Windows.Forms.Label
    Friend WithEvents lblMissingPNs As System.Windows.Forms.Label
    Friend WithEvents Panel6 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents lblDupPN As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents gb_ExcelInfo As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_IgnoreHeader As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_ReadAllSheets As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearValue As System.Windows.Forms.Button
    Friend WithEvents btnClearDescription As System.Windows.Forms.Button
    Friend WithEvents btnClearNew As System.Windows.Forms.Button
    Friend WithEvents btnClearSource As System.Windows.Forms.Button
    Friend WithEvents cbox_Value As ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbox_Description As ComboBox
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents cbox_After As ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cbox_Before As ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents cboxActiveSheet As ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Panel3 As Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btn_Browse_SwapPN As System.Windows.Forms.Button
    Friend WithEvents tbox_Input As System.Windows.Forms.TextBox
    Friend WithEvents chkbox_SINotation As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearName As System.Windows.Forms.Button
    Friend WithEvents btnClearLabel As System.Windows.Forms.Button
    Friend WithEvents cboxName As ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboxLabel As ComboBox
    Friend WithEvents Label7 As System.Windows.Forms.Label
End Class
