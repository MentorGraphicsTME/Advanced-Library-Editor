<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmBuildFromExcel
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
        Me.Label7 = New System.Windows.Forms.Label()
        Me.gb_Options = New System.Windows.Forms.GroupBox()
        Me.cbox_ignoreHeader = New System.Windows.Forms.CheckBox()
        Me.chkbox_WorksheetName = New System.Windows.Forms.CheckBox()
        Me.chkbox_RebuildParts = New System.Windows.Forms.CheckBox()
        Me.chkbox_SaveEachPart = New System.Windows.Forms.CheckBox()
        Me.chkbox_RemoveIncomplete = New System.Windows.Forms.CheckBox()
        Me.chkbox_GetHeight = New System.Windows.Forms.CheckBox()
        Me.chkbox_RefDesPartitions = New System.Windows.Forms.CheckBox()
        Me.chkboxMultiThread = New System.Windows.Forms.CheckBox()
        Me.cbox_RefDes = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.btnBuild = New System.Windows.Forms.Button()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.tsm_Threads = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tv_Parts = New System.Windows.Forms.TreeView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.gb_ExcelInfo = New System.Windows.Forms.GroupBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboxUnit = New System.Windows.Forms.ComboBox()
        Me.btnClearDescription = New System.Windows.Forms.Button()
        Me.cboxDescription = New System.Windows.Forms.ComboBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.btnClearPartLabel = New System.Windows.Forms.Button()
        Me.btnClearPartName = New System.Windows.Forms.Button()
        Me.cboxPartLabel = New System.Windows.Forms.ComboBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboxPartName = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.lbl_duplicate = New System.Windows.Forms.Label()
        Me.btnClearHeight = New System.Windows.Forms.Button()
        Me.btnClearRefDes = New System.Windows.Forms.Button()
        Me.btnClearCell = New System.Windows.Forms.Button()
        Me.btnClearSymbol = New System.Windows.Forms.Button()
        Me.btnClearPN = New System.Windows.Forms.Button()
        Me.btnClearPartition = New System.Windows.Forms.Button()
        Me.cbox_Height = New System.Windows.Forms.ComboBox()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.cbox_CellName = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cbox_SymbolName = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cbox_PartNumber = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboxPartPartition = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboxActiveSheet = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lbl_Excel = New System.Windows.Forms.Label()
        Me.tboxWorkbook = New System.Windows.Forms.TextBox()
        Me.btn_Browse_Excel = New System.Windows.Forms.Button()
        Me.plAction = New System.Windows.Forms.Panel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.chkboxLogPerPartition = New System.Windows.Forms.CheckBox()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.tabMain = New System.Windows.Forms.TabPage()
        Me.tabResults = New System.Windows.Forms.TabPage()
        Me.dgvResults = New System.Windows.Forms.DataGridView()
        Me.Column1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clPartsInSpreadsheet = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Column5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.clTotalFailed = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btnPythonLog = New System.Windows.Forms.Button()
        Me.tsTimeTotal = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.gb_Options.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.gb_ExcelInfo.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.plAction.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.TabControl1.SuspendLayout()
        Me.tabMain.SuspendLayout()
        Me.tabResults.SuspendLayout()
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label7
        '
        Me.Label7.Location = New System.Drawing.Point(8, 287)
        Me.Label7.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(99, 17)
        Me.Label7.TabIndex = 97
        Me.Label7.Text = "Height:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'gb_Options
        '
        Me.gb_Options.Controls.Add(Me.cbox_ignoreHeader)
        Me.gb_Options.Controls.Add(Me.chkbox_WorksheetName)
        Me.gb_Options.Controls.Add(Me.chkbox_RebuildParts)
        Me.gb_Options.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_Options.Enabled = False
        Me.gb_Options.Location = New System.Drawing.Point(0, 599)
        Me.gb_Options.Margin = New System.Windows.Forms.Padding(4)
        Me.gb_Options.Name = "gb_Options"
        Me.gb_Options.Padding = New System.Windows.Forms.Padding(4)
        Me.gb_Options.Size = New System.Drawing.Size(323, 115)
        Me.gb_Options.TabIndex = 0
        Me.gb_Options.TabStop = False
        Me.gb_Options.Text = "Read Options"
        '
        'cbox_ignoreHeader
        '
        Me.cbox_ignoreHeader.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.cbox_ignoreHeader.AutoSize = True
        Me.cbox_ignoreHeader.Location = New System.Drawing.Point(19, 82)
        Me.cbox_ignoreHeader.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_ignoreHeader.Name = "cbox_ignoreHeader"
        Me.cbox_ignoreHeader.Size = New System.Drawing.Size(245, 21)
        Me.cbox_ignoreHeader.TabIndex = 0
        Me.cbox_ignoreHeader.Text = "Ignore First Row (used as header)"
        Me.cbox_ignoreHeader.UseVisualStyleBackColor = True
        '
        'chkbox_WorksheetName
        '
        Me.chkbox_WorksheetName.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkbox_WorksheetName.AutoSize = True
        Me.chkbox_WorksheetName.Location = New System.Drawing.Point(19, 54)
        Me.chkbox_WorksheetName.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_WorksheetName.Name = "chkbox_WorksheetName"
        Me.chkbox_WorksheetName.Size = New System.Drawing.Size(279, 21)
        Me.chkbox_WorksheetName.TabIndex = 0
        Me.chkbox_WorksheetName.Text = "Use Excel Worksheet name for partition"
        Me.chkbox_WorksheetName.UseVisualStyleBackColor = True
        '
        'chkbox_RebuildParts
        '
        Me.chkbox_RebuildParts.AutoSize = True
        Me.chkbox_RebuildParts.Location = New System.Drawing.Point(19, 25)
        Me.chkbox_RebuildParts.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_RebuildParts.Name = "chkbox_RebuildParts"
        Me.chkbox_RebuildParts.Size = New System.Drawing.Size(175, 21)
        Me.chkbox_RebuildParts.TabIndex = 3
        Me.chkbox_RebuildParts.Text = "Rebuild duplicate parts"
        Me.chkbox_RebuildParts.UseVisualStyleBackColor = True
        '
        'chkbox_SaveEachPart
        '
        Me.chkbox_SaveEachPart.AutoSize = True
        Me.chkbox_SaveEachPart.Location = New System.Drawing.Point(17, 141)
        Me.chkbox_SaveEachPart.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_SaveEachPart.Name = "chkbox_SaveEachPart"
        Me.chkbox_SaveEachPart.Size = New System.Drawing.Size(245, 21)
        Me.chkbox_SaveEachPart.TabIndex = 4
        Me.chkbox_SaveEachPart.Text = "Save after building a part (Slower)"
        Me.chkbox_SaveEachPart.UseVisualStyleBackColor = True
        '
        'chkbox_RemoveIncomplete
        '
        Me.chkbox_RemoveIncomplete.AutoSize = True
        Me.chkbox_RemoveIncomplete.Checked = True
        Me.chkbox_RemoveIncomplete.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_RemoveIncomplete.Location = New System.Drawing.Point(17, 111)
        Me.chkbox_RemoveIncomplete.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_RemoveIncomplete.Name = "chkbox_RemoveIncomplete"
        Me.chkbox_RemoveIncomplete.Size = New System.Drawing.Size(190, 21)
        Me.chkbox_RemoveIncomplete.TabIndex = 2
        Me.chkbox_RemoveIncomplete.Text = "Remove incomplete parts"
        Me.chkbox_RemoveIncomplete.UseVisualStyleBackColor = True
        '
        'chkbox_GetHeight
        '
        Me.chkbox_GetHeight.AutoSize = True
        Me.chkbox_GetHeight.Location = New System.Drawing.Point(17, 84)
        Me.chkbox_GetHeight.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_GetHeight.Name = "chkbox_GetHeight"
        Me.chkbox_GetHeight.Size = New System.Drawing.Size(268, 21)
        Me.chkbox_GetHeight.TabIndex = 1
        Me.chkbox_GetHeight.Text = "Get height from cell placement outline"
        Me.chkbox_GetHeight.UseVisualStyleBackColor = True
        '
        'chkbox_RefDesPartitions
        '
        Me.chkbox_RefDesPartitions.AutoSize = True
        Me.chkbox_RefDesPartitions.Enabled = False
        Me.chkbox_RefDesPartitions.Location = New System.Drawing.Point(17, 57)
        Me.chkbox_RefDesPartitions.Margin = New System.Windows.Forms.Padding(4)
        Me.chkbox_RefDesPartitions.Name = "chkbox_RefDesPartitions"
        Me.chkbox_RefDesPartitions.Size = New System.Drawing.Size(203, 21)
        Me.chkbox_RefDesPartitions.TabIndex = 0
        Me.chkbox_RefDesPartitions.Text = "Create Partitions by Refdes"
        Me.chkbox_RefDesPartitions.UseVisualStyleBackColor = True
        '
        'chkboxMultiThread
        '
        Me.chkboxMultiThread.AutoSize = True
        Me.chkboxMultiThread.Checked = True
        Me.chkboxMultiThread.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkboxMultiThread.Location = New System.Drawing.Point(17, 28)
        Me.chkboxMultiThread.Margin = New System.Windows.Forms.Padding(4)
        Me.chkboxMultiThread.Name = "chkboxMultiThread"
        Me.chkboxMultiThread.Size = New System.Drawing.Size(167, 21)
        Me.chkboxMultiThread.TabIndex = 0
        Me.chkboxMultiThread.Text = "Enable Multithreading"
        Me.chkboxMultiThread.UseVisualStyleBackColor = True
        '
        'cbox_RefDes
        '
        Me.cbox_RefDes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_RefDes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_RefDes.FormattingEnabled = True
        Me.cbox_RefDes.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_RefDes.Location = New System.Drawing.Point(108, 250)
        Me.cbox_RefDes.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_RefDes.Name = "cbox_RefDes"
        Me.cbox_RefDes.Size = New System.Drawing.Size(89, 24)
        Me.cbox_RefDes.TabIndex = 96
        '
        'Label6
        '
        Me.Label6.Location = New System.Drawing.Point(8, 254)
        Me.Label6.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(99, 17)
        Me.Label6.TabIndex = 95
        Me.Label6.Text = "Ref Des:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBuild
        '
        Me.btnBuild.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnBuild.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnBuild.Location = New System.Drawing.Point(284, 8)
        Me.btnBuild.Margin = New System.Windows.Forms.Padding(4)
        Me.btnBuild.Name = "btnBuild"
        Me.btnBuild.Size = New System.Drawing.Size(100, 28)
        Me.btnBuild.TabIndex = 93
        Me.btnBuild.Text = "Process"
        Me.btnBuild.UseVisualStyleBackColor = False
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(641, 20)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Browse to an Excel file to start..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'StatusStrip1
        '
        Me.StatusStrip1.ImageScalingSize = New System.Drawing.Size(20, 20)
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.tsm_Threads, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 749)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Padding = New System.Windows.Forms.Padding(1, 0, 19, 0)
        Me.StatusStrip1.Size = New System.Drawing.Size(731, 25)
        Me.StatusStrip1.TabIndex = 96
        Me.StatusStrip1.Text = "StatusStrip1"
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
        'tv_Parts
        '
        Me.tv_Parts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Parts.Location = New System.Drawing.Point(0, 0)
        Me.tv_Parts.Margin = New System.Windows.Forms.Padding(4)
        Me.tv_Parts.Name = "tv_Parts"
        Me.tv_Parts.Size = New System.Drawing.Size(389, 476)
        Me.tv_Parts.TabIndex = 0
        Me.tv_Parts.Visible = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(3, 3)
        Me.SplitContainer1.Margin = New System.Windows.Forms.Padding(4)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_ExcelInfo)
        Me.SplitContainer1.Panel1.Controls.Add(Me.Panel1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_Options)
        Me.SplitContainer1.Panel1MinSize = 226
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tv_Parts)
        Me.SplitContainer1.Panel2.Controls.Add(Me.plAction)
        Me.SplitContainer1.Panel2MinSize = 226
        Me.SplitContainer1.Size = New System.Drawing.Size(717, 714)
        Me.SplitContainer1.SplitterDistance = 323
        Me.SplitContainer1.SplitterWidth = 5
        Me.SplitContainer1.TabIndex = 95
        '
        'gb_ExcelInfo
        '
        Me.gb_ExcelInfo.Controls.Add(Me.Label12)
        Me.gb_ExcelInfo.Controls.Add(Me.cboxUnit)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearDescription)
        Me.gb_ExcelInfo.Controls.Add(Me.cboxDescription)
        Me.gb_ExcelInfo.Controls.Add(Me.Label11)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearPartLabel)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearPartName)
        Me.gb_ExcelInfo.Controls.Add(Me.cboxPartLabel)
        Me.gb_ExcelInfo.Controls.Add(Me.Label9)
        Me.gb_ExcelInfo.Controls.Add(Me.cboxPartName)
        Me.gb_ExcelInfo.Controls.Add(Me.Label10)
        Me.gb_ExcelInfo.Controls.Add(Me.lbl_duplicate)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearHeight)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearRefDes)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearCell)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearSymbol)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearPN)
        Me.gb_ExcelInfo.Controls.Add(Me.btnClearPartition)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_Height)
        Me.gb_ExcelInfo.Controls.Add(Me.Label7)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_RefDes)
        Me.gb_ExcelInfo.Controls.Add(Me.Label6)
        Me.gb_ExcelInfo.Controls.Add(Me.btnRead)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_CellName)
        Me.gb_ExcelInfo.Controls.Add(Me.Label5)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_SymbolName)
        Me.gb_ExcelInfo.Controls.Add(Me.Label4)
        Me.gb_ExcelInfo.Controls.Add(Me.cbox_PartNumber)
        Me.gb_ExcelInfo.Controls.Add(Me.Label3)
        Me.gb_ExcelInfo.Controls.Add(Me.cboxPartPartition)
        Me.gb_ExcelInfo.Controls.Add(Me.Label2)
        Me.gb_ExcelInfo.Controls.Add(Me.cboxActiveSheet)
        Me.gb_ExcelInfo.Controls.Add(Me.Label1)
        Me.gb_ExcelInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gb_ExcelInfo.Enabled = False
        Me.gb_ExcelInfo.Location = New System.Drawing.Point(0, 70)
        Me.gb_ExcelInfo.Margin = New System.Windows.Forms.Padding(4)
        Me.gb_ExcelInfo.Name = "gb_ExcelInfo"
        Me.gb_ExcelInfo.Padding = New System.Windows.Forms.Padding(4)
        Me.gb_ExcelInfo.Size = New System.Drawing.Size(323, 529)
        Me.gb_ExcelInfo.TabIndex = 2
        Me.gb_ExcelInfo.TabStop = False
        Me.gb_ExcelInfo.Text = "Excel Info"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(8, 217)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(142, 17)
        Me.Label12.TabIndex = 176
        Me.Label12.Text = "Optional Parameters:"
        '
        'cboxUnit
        '
        Me.cboxUnit.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxUnit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxUnit.FormattingEnabled = True
        Me.cboxUnit.Items.AddRange(New Object() {"IN", "TH", "MM", "UM"})
        Me.cboxUnit.Location = New System.Drawing.Point(205, 283)
        Me.cboxUnit.Margin = New System.Windows.Forms.Padding(4)
        Me.cboxUnit.Name = "cboxUnit"
        Me.cboxUnit.Size = New System.Drawing.Size(55, 24)
        Me.cboxUnit.TabIndex = 175
        '
        'btnClearDescription
        '
        Me.btnClearDescription.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearDescription.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearDescription.Location = New System.Drawing.Point(205, 381)
        Me.btnClearDescription.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearDescription.Name = "btnClearDescription"
        Me.btnClearDescription.Size = New System.Drawing.Size(27, 26)
        Me.btnClearDescription.TabIndex = 174
        Me.btnClearDescription.Text = "X"
        Me.btnClearDescription.UseVisualStyleBackColor = False
        Me.btnClearDescription.Visible = False
        '
        'cboxDescription
        '
        Me.cboxDescription.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxDescription.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxDescription.FormattingEnabled = True
        Me.cboxDescription.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cboxDescription.Location = New System.Drawing.Point(108, 382)
        Me.cboxDescription.Margin = New System.Windows.Forms.Padding(4)
        Me.cboxDescription.Name = "cboxDescription"
        Me.cboxDescription.Size = New System.Drawing.Size(89, 24)
        Me.cboxDescription.TabIndex = 173
        '
        'Label11
        '
        Me.Label11.Location = New System.Drawing.Point(8, 386)
        Me.Label11.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(99, 17)
        Me.Label11.TabIndex = 172
        Me.Label11.Text = "Description:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnClearPartLabel
        '
        Me.btnClearPartLabel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearPartLabel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearPartLabel.Location = New System.Drawing.Point(205, 348)
        Me.btnClearPartLabel.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearPartLabel.Name = "btnClearPartLabel"
        Me.btnClearPartLabel.Size = New System.Drawing.Size(27, 26)
        Me.btnClearPartLabel.TabIndex = 171
        Me.btnClearPartLabel.Text = "X"
        Me.btnClearPartLabel.UseVisualStyleBackColor = False
        Me.btnClearPartLabel.Visible = False
        '
        'btnClearPartName
        '
        Me.btnClearPartName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearPartName.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearPartName.Location = New System.Drawing.Point(205, 315)
        Me.btnClearPartName.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearPartName.Name = "btnClearPartName"
        Me.btnClearPartName.Size = New System.Drawing.Size(27, 26)
        Me.btnClearPartName.TabIndex = 170
        Me.btnClearPartName.Text = "X"
        Me.btnClearPartName.UseVisualStyleBackColor = False
        Me.btnClearPartName.Visible = False
        '
        'cboxPartLabel
        '
        Me.cboxPartLabel.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxPartLabel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxPartLabel.FormattingEnabled = True
        Me.cboxPartLabel.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cboxPartLabel.Location = New System.Drawing.Point(108, 349)
        Me.cboxPartLabel.Margin = New System.Windows.Forms.Padding(4)
        Me.cboxPartLabel.Name = "cboxPartLabel"
        Me.cboxPartLabel.Size = New System.Drawing.Size(89, 24)
        Me.cboxPartLabel.TabIndex = 169
        '
        'Label9
        '
        Me.Label9.Location = New System.Drawing.Point(8, 353)
        Me.Label9.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(99, 17)
        Me.Label9.TabIndex = 168
        Me.Label9.Text = "Part Label:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboxPartName
        '
        Me.cboxPartName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxPartName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxPartName.FormattingEnabled = True
        Me.cboxPartName.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cboxPartName.Location = New System.Drawing.Point(108, 316)
        Me.cboxPartName.Margin = New System.Windows.Forms.Padding(4)
        Me.cboxPartName.Name = "cboxPartName"
        Me.cboxPartName.Size = New System.Drawing.Size(89, 24)
        Me.cboxPartName.TabIndex = 167
        '
        'Label10
        '
        Me.Label10.Location = New System.Drawing.Point(8, 320)
        Me.Label10.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(99, 17)
        Me.Label10.TabIndex = 166
        Me.Label10.Text = "Part Name:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lbl_duplicate
        '
        Me.lbl_duplicate.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.lbl_duplicate.AutoSize = True
        Me.lbl_duplicate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.5!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbl_duplicate.ForeColor = System.Drawing.Color.Red
        Me.lbl_duplicate.Location = New System.Drawing.Point(40, 449)
        Me.lbl_duplicate.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lbl_duplicate.Name = "lbl_duplicate"
        Me.lbl_duplicate.Size = New System.Drawing.Size(231, 18)
        Me.lbl_duplicate.TabIndex = 165
        Me.lbl_duplicate.Text = "At least one column is a duplicate."
        Me.lbl_duplicate.Visible = False
        '
        'btnClearHeight
        '
        Me.btnClearHeight.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearHeight.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearHeight.Location = New System.Drawing.Point(268, 281)
        Me.btnClearHeight.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearHeight.Name = "btnClearHeight"
        Me.btnClearHeight.Size = New System.Drawing.Size(27, 26)
        Me.btnClearHeight.TabIndex = 164
        Me.btnClearHeight.Text = "X"
        Me.btnClearHeight.UseVisualStyleBackColor = False
        Me.btnClearHeight.Visible = False
        '
        'btnClearRefDes
        '
        Me.btnClearRefDes.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearRefDes.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearRefDes.Location = New System.Drawing.Point(205, 249)
        Me.btnClearRefDes.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearRefDes.Name = "btnClearRefDes"
        Me.btnClearRefDes.Size = New System.Drawing.Size(27, 26)
        Me.btnClearRefDes.TabIndex = 163
        Me.btnClearRefDes.Text = "X"
        Me.btnClearRefDes.UseVisualStyleBackColor = False
        Me.btnClearRefDes.Visible = False
        '
        'btnClearCell
        '
        Me.btnClearCell.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearCell.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearCell.Location = New System.Drawing.Point(205, 170)
        Me.btnClearCell.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearCell.Name = "btnClearCell"
        Me.btnClearCell.Size = New System.Drawing.Size(27, 26)
        Me.btnClearCell.TabIndex = 162
        Me.btnClearCell.Text = "X"
        Me.btnClearCell.UseVisualStyleBackColor = False
        Me.btnClearCell.Visible = False
        '
        'btnClearSymbol
        '
        Me.btnClearSymbol.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearSymbol.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearSymbol.Location = New System.Drawing.Point(205, 137)
        Me.btnClearSymbol.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearSymbol.Name = "btnClearSymbol"
        Me.btnClearSymbol.Size = New System.Drawing.Size(27, 26)
        Me.btnClearSymbol.TabIndex = 161
        Me.btnClearSymbol.Text = "X"
        Me.btnClearSymbol.UseVisualStyleBackColor = False
        Me.btnClearSymbol.Visible = False
        '
        'btnClearPN
        '
        Me.btnClearPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearPN.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearPN.Location = New System.Drawing.Point(205, 103)
        Me.btnClearPN.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearPN.Name = "btnClearPN"
        Me.btnClearPN.Size = New System.Drawing.Size(27, 26)
        Me.btnClearPN.TabIndex = 160
        Me.btnClearPN.Text = "X"
        Me.btnClearPN.UseVisualStyleBackColor = False
        Me.btnClearPN.Visible = False
        '
        'btnClearPartition
        '
        Me.btnClearPartition.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClearPartition.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnClearPartition.Location = New System.Drawing.Point(205, 70)
        Me.btnClearPartition.Margin = New System.Windows.Forms.Padding(4)
        Me.btnClearPartition.Name = "btnClearPartition"
        Me.btnClearPartition.Size = New System.Drawing.Size(27, 26)
        Me.btnClearPartition.TabIndex = 159
        Me.btnClearPartition.Text = "X"
        Me.btnClearPartition.UseVisualStyleBackColor = False
        Me.btnClearPartition.Visible = False
        '
        'cbox_Height
        '
        Me.cbox_Height.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Height.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Height.FormattingEnabled = True
        Me.cbox_Height.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_Height.Location = New System.Drawing.Point(108, 283)
        Me.cbox_Height.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_Height.Name = "cbox_Height"
        Me.cbox_Height.Size = New System.Drawing.Size(89, 24)
        Me.cbox_Height.TabIndex = 98
        '
        'btnRead
        '
        Me.btnRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRead.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRead.Location = New System.Drawing.Point(219, 489)
        Me.btnRead.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(97, 28)
        Me.btnRead.TabIndex = 94
        Me.btnRead.Text = "Read"
        Me.btnRead.UseVisualStyleBackColor = False
        '
        'cbox_CellName
        '
        Me.cbox_CellName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_CellName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_CellName.FormattingEnabled = True
        Me.cbox_CellName.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_CellName.Location = New System.Drawing.Point(108, 170)
        Me.cbox_CellName.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_CellName.Name = "cbox_CellName"
        Me.cbox_CellName.Size = New System.Drawing.Size(89, 24)
        Me.cbox_CellName.TabIndex = 1
        '
        'Label5
        '
        Me.Label5.Location = New System.Drawing.Point(8, 174)
        Me.Label5.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(99, 17)
        Me.Label5.TabIndex = 0
        Me.Label5.Text = "Cell Name:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbox_SymbolName
        '
        Me.cbox_SymbolName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_SymbolName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_SymbolName.FormattingEnabled = True
        Me.cbox_SymbolName.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_SymbolName.Location = New System.Drawing.Point(108, 137)
        Me.cbox_SymbolName.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_SymbolName.Name = "cbox_SymbolName"
        Me.cbox_SymbolName.Size = New System.Drawing.Size(89, 24)
        Me.cbox_SymbolName.TabIndex = 1
        '
        'Label4
        '
        Me.Label4.Location = New System.Drawing.Point(8, 140)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(99, 17)
        Me.Label4.TabIndex = 0
        Me.Label4.Text = "Symbol Name:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cbox_PartNumber
        '
        Me.cbox_PartNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_PartNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_PartNumber.FormattingEnabled = True
        Me.cbox_PartNumber.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cbox_PartNumber.Location = New System.Drawing.Point(108, 103)
        Me.cbox_PartNumber.Margin = New System.Windows.Forms.Padding(4)
        Me.cbox_PartNumber.Name = "cbox_PartNumber"
        Me.cbox_PartNumber.Size = New System.Drawing.Size(89, 24)
        Me.cbox_PartNumber.TabIndex = 1
        '
        'Label3
        '
        Me.Label3.Location = New System.Drawing.Point(8, 107)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 17)
        Me.Label3.TabIndex = 0
        Me.Label3.Text = "Part Number:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboxPartPartition
        '
        Me.cboxPartPartition.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxPartPartition.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxPartPartition.FormattingEnabled = True
        Me.cboxPartPartition.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M"})
        Me.cboxPartPartition.Location = New System.Drawing.Point(108, 70)
        Me.cboxPartPartition.Margin = New System.Windows.Forms.Padding(4)
        Me.cboxPartPartition.Name = "cboxPartPartition"
        Me.cboxPartPartition.Size = New System.Drawing.Size(89, 24)
        Me.cboxPartPartition.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(8, 74)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(99, 17)
        Me.Label2.TabIndex = 0
        Me.Label2.Text = "Partition:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.Panel1.Size = New System.Drawing.Size(323, 70)
        Me.Panel1.TabIndex = 1
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
        Me.tboxWorkbook.Size = New System.Drawing.Size(256, 22)
        Me.tboxWorkbook.TabIndex = 83
        '
        'btn_Browse_Excel
        '
        Me.btn_Browse_Excel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Browse_Excel.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Browse_Excel.Location = New System.Drawing.Point(280, 31)
        Me.btn_Browse_Excel.Margin = New System.Windows.Forms.Padding(4)
        Me.btn_Browse_Excel.Name = "btn_Browse_Excel"
        Me.btn_Browse_Excel.Size = New System.Drawing.Size(40, 25)
        Me.btn_Browse_Excel.TabIndex = 84
        Me.btn_Browse_Excel.Text = "..."
        Me.btn_Browse_Excel.UseVisualStyleBackColor = False
        '
        'plAction
        '
        Me.plAction.Controls.Add(Me.GroupBox1)
        Me.plAction.Controls.Add(Me.btnBuild)
        Me.plAction.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.plAction.Location = New System.Drawing.Point(0, 476)
        Me.plAction.Name = "plAction"
        Me.plAction.Size = New System.Drawing.Size(389, 238)
        Me.plAction.TabIndex = 94
        Me.plAction.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkboxLogPerPartition)
        Me.GroupBox1.Controls.Add(Me.chkbox_SaveEachPart)
        Me.GroupBox1.Controls.Add(Me.chkboxMultiThread)
        Me.GroupBox1.Controls.Add(Me.chkbox_RemoveIncomplete)
        Me.GroupBox1.Controls.Add(Me.chkbox_RefDesPartitions)
        Me.GroupBox1.Controls.Add(Me.chkbox_GetHeight)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.GroupBox1.Location = New System.Drawing.Point(0, 43)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(389, 195)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Build Options"
        '
        'chkboxLogPerPartition
        '
        Me.chkboxLogPerPartition.AutoSize = True
        Me.chkboxLogPerPartition.Location = New System.Drawing.Point(17, 168)
        Me.chkboxLogPerPartition.Margin = New System.Windows.Forms.Padding(4)
        Me.chkboxLogPerPartition.Name = "chkboxLogPerPartition"
        Me.chkboxLogPerPartition.Size = New System.Drawing.Size(182, 21)
        Me.chkboxLogPerPartition.TabIndex = 5
        Me.chkboxLogPerPartition.Text = "One log file per partition"
        Me.chkboxLogPerPartition.UseVisualStyleBackColor = True
        '
        'TabControl1
        '
        Me.TabControl1.Alignment = System.Windows.Forms.TabAlignment.Bottom
        Me.TabControl1.Controls.Add(Me.tabMain)
        Me.TabControl1.Controls.Add(Me.tabResults)
        Me.TabControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TabControl1.Location = New System.Drawing.Point(0, 0)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(731, 749)
        Me.TabControl1.TabIndex = 97
        '
        'tabMain
        '
        Me.tabMain.Controls.Add(Me.SplitContainer1)
        Me.tabMain.Location = New System.Drawing.Point(4, 4)
        Me.tabMain.Name = "tabMain"
        Me.tabMain.Padding = New System.Windows.Forms.Padding(3)
        Me.tabMain.Size = New System.Drawing.Size(723, 720)
        Me.tabMain.TabIndex = 0
        Me.tabMain.Text = "Main"
        Me.tabMain.UseVisualStyleBackColor = True
        '
        'tabResults
        '
        Me.tabResults.Controls.Add(Me.dgvResults)
        Me.tabResults.Controls.Add(Me.Panel2)
        Me.tabResults.Location = New System.Drawing.Point(4, 4)
        Me.tabResults.Name = "tabResults"
        Me.tabResults.Padding = New System.Windows.Forms.Padding(3)
        Me.tabResults.Size = New System.Drawing.Size(723, 720)
        Me.tabResults.TabIndex = 1
        Me.tabResults.Text = "Results"
        Me.tabResults.UseVisualStyleBackColor = True
        '
        'dgvResults
        '
        Me.dgvResults.AllowUserToAddRows = False
        Me.dgvResults.AllowUserToDeleteRows = False
        Me.dgvResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvResults.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.Column1, Me.Column2, Me.clPartsInSpreadsheet, Me.Column3, Me.Column4, Me.Column5, Me.clTotalFailed})
        Me.dgvResults.Dock = System.Windows.Forms.DockStyle.Fill
        Me.dgvResults.Location = New System.Drawing.Point(3, 3)
        Me.dgvResults.Name = "dgvResults"
        Me.dgvResults.RowHeadersVisible = False
        Me.dgvResults.RowTemplate.Height = 24
        Me.dgvResults.Size = New System.Drawing.Size(717, 667)
        Me.dgvResults.TabIndex = 0
        '
        'Column1
        '
        Me.Column1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Column1.HeaderText = "Partition"
        Me.Column1.Name = "Column1"
        Me.Column1.ReadOnly = True
        Me.Column1.Width = 89
        '
        'Column2
        '
        Me.Column2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Column2.HeaderText = "Time to Process"
        Me.Column2.Name = "Column2"
        Me.Column2.ReadOnly = True
        Me.Column2.Width = 127
        '
        'clPartsInSpreadsheet
        '
        Me.clPartsInSpreadsheet.HeaderText = "Parts in Speadsheet"
        Me.clPartsInSpreadsheet.Name = "clPartsInSpreadsheet"
        Me.clPartsInSpreadsheet.ReadOnly = True
        '
        'Column3
        '
        Me.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Column3.HeaderText = "Parts To Build"
        Me.Column3.Name = "Column3"
        Me.Column3.ReadOnly = True
        Me.Column3.Width = 116
        '
        'Column4
        '
        Me.Column4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Column4.HeaderText = "Success"
        Me.Column4.Name = "Column4"
        Me.Column4.ReadOnly = True
        Me.Column4.Width = 90
        '
        'Column5
        '
        Me.Column5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.DisplayedCells
        Me.Column5.HeaderText = "Failed"
        Me.Column5.Name = "Column5"
        Me.Column5.ReadOnly = True
        Me.Column5.Width = 75
        '
        'clTotalFailed
        '
        Me.clTotalFailed.HeaderText = "Total Failed"
        Me.clTotalFailed.Name = "clTotalFailed"
        Me.clTotalFailed.ReadOnly = True
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btnPythonLog)
        Me.Panel2.Controls.Add(Me.tsTimeTotal)
        Me.Panel2.Controls.Add(Me.Label8)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(3, 670)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(717, 47)
        Me.Panel2.TabIndex = 1
        '
        'btnPythonLog
        '
        Me.btnPythonLog.Location = New System.Drawing.Point(527, 12)
        Me.btnPythonLog.Name = "btnPythonLog"
        Me.btnPythonLog.Size = New System.Drawing.Size(154, 27)
        Me.btnPythonLog.TabIndex = 2
        Me.btnPythonLog.Text = "Open Advanced Log"
        Me.btnPythonLog.UseVisualStyleBackColor = True
        Me.btnPythonLog.Visible = False
        '
        'tsTimeTotal
        '
        Me.tsTimeTotal.AutoSize = True
        Me.tsTimeTotal.Location = New System.Drawing.Point(145, 17)
        Me.tsTimeTotal.Name = "tsTimeTotal"
        Me.tsTimeTotal.Size = New System.Drawing.Size(16, 17)
        Me.tsTimeTotal.TabIndex = 1
        Me.tsTimeTotal.Text = "0"
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(5, 17)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(134, 17)
        Me.Label8.TabIndex = 0
        Me.Label8.Text = "Total Elapsed Time:"
        '
        'frmBuildFromExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(731, 774)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(701, 601)
        Me.Name = "frmBuildFromExcel"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Build PDB from Excel"
        Me.gb_Options.ResumeLayout(False)
        Me.gb_Options.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.gb_ExcelInfo.ResumeLayout(False)
        Me.gb_ExcelInfo.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.plAction.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.TabControl1.ResumeLayout(False)
        Me.tabMain.ResumeLayout(False)
        Me.tabResults.ResumeLayout(False)
        CType(Me.dgvResults, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents gb_Options As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_RebuildParts As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_RemoveIncomplete As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_GetHeight As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_ignoreHeader As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_WorksheetName As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_RefDesPartitions As System.Windows.Forms.CheckBox
    Friend WithEvents chkboxMultiThread As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_RefDes As System.Windows.Forms.ComboBox
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents btnBuild As System.Windows.Forms.Button
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tv_Parts As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents gb_ExcelInfo As System.Windows.Forms.GroupBox
    Friend WithEvents cbox_Height As System.Windows.Forms.ComboBox
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents cbox_CellName As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents cbox_SymbolName As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents cbox_PartNumber As System.Windows.Forms.ComboBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents cboxPartPartition As System.Windows.Forms.ComboBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboxActiveSheet As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents lbl_Excel As System.Windows.Forms.Label
    Friend WithEvents tboxWorkbook As System.Windows.Forms.TextBox
    Friend WithEvents btn_Browse_Excel As System.Windows.Forms.Button
    Friend WithEvents chkbox_SaveEachPart As System.Windows.Forms.CheckBox
    Friend WithEvents btnClearHeight As System.Windows.Forms.Button
    Friend WithEvents btnClearRefDes As System.Windows.Forms.Button
    Friend WithEvents btnClearCell As System.Windows.Forms.Button
    Friend WithEvents btnClearSymbol As System.Windows.Forms.Button
    Friend WithEvents btnClearPN As System.Windows.Forms.Button
    Friend WithEvents btnClearPartition As System.Windows.Forms.Button
    Friend WithEvents lbl_duplicate As System.Windows.Forms.Label
    Friend WithEvents tsm_Threads As ToolStripStatusLabel
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents tabMain As TabPage
    Friend WithEvents tabResults As TabPage
    Friend WithEvents dgvResults As DataGridView
    Friend WithEvents Panel2 As Panel
    Friend WithEvents tsTimeTotal As System.Windows.Forms.Label
    Friend WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents plAction As Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents chkboxLogPerPartition As System.Windows.Forms.CheckBox
    Friend WithEvents btnPythonLog As System.Windows.Forms.Button
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents clPartsInSpreadsheet As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents clTotalFailed As DataGridViewTextBoxColumn
    Friend WithEvents cboxUnit As ComboBox
    Friend WithEvents btnClearDescription As System.Windows.Forms.Button
    Friend WithEvents cboxDescription As ComboBox
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents btnClearPartLabel As System.Windows.Forms.Button
    Friend WithEvents btnClearPartName As System.Windows.Forms.Button
    Friend WithEvents cboxPartLabel As ComboBox
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents cboxPartName As ComboBox
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
End Class
