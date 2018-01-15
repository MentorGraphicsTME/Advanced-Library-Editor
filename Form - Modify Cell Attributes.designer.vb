<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmModifyCellAtts
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
        Me.btnUpdate = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tv_Cells = New System.Windows.Forms.TreeView()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.cbox_UndersideHeightUnits = New System.Windows.Forms.ComboBox()
        Me.cbox_HeightUnits = New System.Windows.Forms.ComboBox()
        Me.chkbox_Description = New System.Windows.Forms.CheckBox()
        Me.tbox_Descript_Before = New System.Windows.Forms.TextBox()
        Me.tbox_Descript_After = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cbox_MoveablePins = New System.Windows.Forms.ComboBox()
        Me.chkbox_Movable = New System.Windows.Forms.CheckBox()
        Me.chkbox_PG = New System.Windows.Forms.CheckBox()
        Me.cbox_CellOverhang = New System.Windows.Forms.ComboBox()
        Me.cbox_PG = New System.Windows.Forms.ComboBox()
        Me.chkbox_CellOverhang = New System.Windows.Forms.CheckBox()
        Me.cbox_Units = New System.Windows.Forms.ComboBox()
        Me.chkbox_Verification = New System.Windows.Forms.CheckBox()
        Me.chkbox_Units = New System.Windows.Forms.CheckBox()
        Me.cbox_Verification = New System.Windows.Forms.ComboBox()
        Me.chkbox_CT = New System.Windows.Forms.CheckBox()
        Me.tbox_Underside = New System.Windows.Forms.TextBox()
        Me.tbox_CT_Before = New System.Windows.Forms.TextBox()
        Me.chkbox_Underside = New System.Windows.Forms.CheckBox()
        Me.tbox_CT_After = New System.Windows.Forms.TextBox()
        Me.tbox_Height = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.chkbox_Height = New System.Windows.Forms.CheckBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnUpdate
        '
        Me.btnUpdate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnUpdate.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnUpdate.Location = New System.Drawing.Point(151, 441)
        Me.btnUpdate.Name = "btnUpdate"
        Me.btnUpdate.Size = New System.Drawing.Size(104, 23)
        Me.btnUpdate.TabIndex = 21
        Me.btnUpdate.Text = "Modify Attributes"
        Me.btnUpdate.UseVisualStyleBackColor = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.GroupBox1)
        Me.SplitContainer1.Panel1MinSize = 200
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer1.Size = New System.Drawing.Size(514, 474)
        Me.SplitContainer1.SplitterDistance = 243
        Me.SplitContainer1.TabIndex = 26
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tv_Cells)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(243, 474)
        Me.GroupBox1.TabIndex = 27
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Cells"
        '
        'tv_Cells
        '
        Me.tv_Cells.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv_Cells.CheckBoxes = True
        Me.tv_Cells.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Cells.Location = New System.Drawing.Point(3, 16)
        Me.tv_Cells.Name = "tv_Cells"
        Me.tv_Cells.Size = New System.Drawing.Size(237, 455)
        Me.tv_Cells.TabIndex = 24
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.cbox_UndersideHeightUnits)
        Me.GroupBox2.Controls.Add(Me.cbox_HeightUnits)
        Me.GroupBox2.Controls.Add(Me.chkbox_Description)
        Me.GroupBox2.Controls.Add(Me.tbox_Descript_Before)
        Me.GroupBox2.Controls.Add(Me.tbox_Descript_After)
        Me.GroupBox2.Controls.Add(Me.Label1)
        Me.GroupBox2.Controls.Add(Me.Label2)
        Me.GroupBox2.Controls.Add(Me.cbox_MoveablePins)
        Me.GroupBox2.Controls.Add(Me.chkbox_Movable)
        Me.GroupBox2.Controls.Add(Me.chkbox_PG)
        Me.GroupBox2.Controls.Add(Me.btnUpdate)
        Me.GroupBox2.Controls.Add(Me.cbox_CellOverhang)
        Me.GroupBox2.Controls.Add(Me.cbox_PG)
        Me.GroupBox2.Controls.Add(Me.chkbox_CellOverhang)
        Me.GroupBox2.Controls.Add(Me.cbox_Units)
        Me.GroupBox2.Controls.Add(Me.chkbox_Verification)
        Me.GroupBox2.Controls.Add(Me.chkbox_Units)
        Me.GroupBox2.Controls.Add(Me.cbox_Verification)
        Me.GroupBox2.Controls.Add(Me.chkbox_CT)
        Me.GroupBox2.Controls.Add(Me.tbox_Underside)
        Me.GroupBox2.Controls.Add(Me.tbox_CT_Before)
        Me.GroupBox2.Controls.Add(Me.chkbox_Underside)
        Me.GroupBox2.Controls.Add(Me.tbox_CT_After)
        Me.GroupBox2.Controls.Add(Me.tbox_Height)
        Me.GroupBox2.Controls.Add(Me.Label4)
        Me.GroupBox2.Controls.Add(Me.chkbox_Height)
        Me.GroupBox2.Controls.Add(Me.Label5)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(267, 474)
        Me.GroupBox2.TabIndex = 46
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Attributes"
        '
        'cbox_UndersideHeightUnits
        '
        Me.cbox_UndersideHeightUnits.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_UndersideHeightUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_UndersideHeightUnits.Enabled = False
        Me.cbox_UndersideHeightUnits.FormattingEnabled = True
        Me.cbox_UndersideHeightUnits.Items.AddRange(New Object() {"in", "th", "mm", "um"})
        Me.cbox_UndersideHeightUnits.Location = New System.Drawing.Point(206, 288)
        Me.cbox_UndersideHeightUnits.Name = "cbox_UndersideHeightUnits"
        Me.cbox_UndersideHeightUnits.Size = New System.Drawing.Size(49, 21)
        Me.cbox_UndersideHeightUnits.TabIndex = 54
        '
        'cbox_HeightUnits
        '
        Me.cbox_HeightUnits.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_HeightUnits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_HeightUnits.Enabled = False
        Me.cbox_HeightUnits.FormattingEnabled = True
        Me.cbox_HeightUnits.Items.AddRange(New Object() {"in", "th", "mm", "um"})
        Me.cbox_HeightUnits.Location = New System.Drawing.Point(206, 247)
        Me.cbox_HeightUnits.Name = "cbox_HeightUnits"
        Me.cbox_HeightUnits.Size = New System.Drawing.Size(49, 21)
        Me.cbox_HeightUnits.TabIndex = 53
        '
        'chkbox_Description
        '
        Me.chkbox_Description.AutoSize = True
        Me.chkbox_Description.Location = New System.Drawing.Point(11, 79)
        Me.chkbox_Description.Name = "chkbox_Description"
        Me.chkbox_Description.Size = New System.Drawing.Size(230, 17)
        Me.chkbox_Description.TabIndex = 48
        Me.chkbox_Description.Text = "Description (Leave before blank to replace)"
        Me.chkbox_Description.UseVisualStyleBackColor = True
        '
        'tbox_Descript_Before
        '
        Me.tbox_Descript_Before.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_Descript_Before.Enabled = False
        Me.tbox_Descript_Before.Location = New System.Drawing.Point(55, 99)
        Me.tbox_Descript_Before.Name = "tbox_Descript_Before"
        Me.tbox_Descript_Before.Size = New System.Drawing.Size(199, 20)
        Me.tbox_Descript_Before.TabIndex = 49
        '
        'tbox_Descript_After
        '
        Me.tbox_Descript_After.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_Descript_After.Enabled = False
        Me.tbox_Descript_After.Location = New System.Drawing.Point(55, 125)
        Me.tbox_Descript_After.Name = "tbox_Descript_After"
        Me.tbox_Descript_After.Size = New System.Drawing.Size(198, 20)
        Me.tbox_Descript_After.TabIndex = 50
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(11, 103)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 13)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Before"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(11, 129)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(29, 13)
        Me.Label2.TabIndex = 52
        Me.Label2.Text = "After"
        '
        'cbox_MoveablePins
        '
        Me.cbox_MoveablePins.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_MoveablePins.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_MoveablePins.Enabled = False
        Me.cbox_MoveablePins.FormattingEnabled = True
        Me.cbox_MoveablePins.Items.AddRange(New Object() {"True", "False"})
        Me.cbox_MoveablePins.Location = New System.Drawing.Point(135, 395)
        Me.cbox_MoveablePins.Name = "cbox_MoveablePins"
        Me.cbox_MoveablePins.Size = New System.Drawing.Size(119, 21)
        Me.cbox_MoveablePins.TabIndex = 47
        '
        'chkbox_Movable
        '
        Me.chkbox_Movable.AutoSize = True
        Me.chkbox_Movable.Location = New System.Drawing.Point(11, 397)
        Me.chkbox_Movable.Name = "chkbox_Movable"
        Me.chkbox_Movable.Size = New System.Drawing.Size(123, 17)
        Me.chkbox_Movable.TabIndex = 46
        Me.chkbox_Movable.Text = "Allow moveable Pins"
        Me.chkbox_Movable.UseVisualStyleBackColor = True
        '
        'chkbox_PG
        '
        Me.chkbox_PG.AutoSize = True
        Me.chkbox_PG.Location = New System.Drawing.Point(12, 24)
        Me.chkbox_PG.Name = "chkbox_PG"
        Me.chkbox_PG.Size = New System.Drawing.Size(101, 17)
        Me.chkbox_PG.TabIndex = 26
        Me.chkbox_PG.Text = "Package Group"
        Me.chkbox_PG.UseVisualStyleBackColor = True
        '
        'cbox_CellOverhang
        '
        Me.cbox_CellOverhang.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_CellOverhang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_CellOverhang.Enabled = False
        Me.cbox_CellOverhang.FormattingEnabled = True
        Me.cbox_CellOverhang.Items.AddRange(New Object() {"True", "False"})
        Me.cbox_CellOverhang.Location = New System.Drawing.Point(136, 363)
        Me.cbox_CellOverhang.Name = "cbox_CellOverhang"
        Me.cbox_CellOverhang.Size = New System.Drawing.Size(119, 21)
        Me.cbox_CellOverhang.TabIndex = 45
        '
        'cbox_PG
        '
        Me.cbox_PG.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_PG.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_PG.Enabled = False
        Me.cbox_PG.FormattingEnabled = True
        Me.cbox_PG.Items.AddRange(New Object() {"Buried", "Connector", "Discrete - Axial", "Discrete - Chip", "Discrete - Other", "Discrete - Radial", "Edge Connector", "Embedded Capacitor", "Embedded Resistor", "General", "Jumper", "IC - Bare Die", "IC - BGA", "IC - DIP", "IC - Flip Chip", "IC - LCC", "IC - Other", "IC - PGA", "IC - PLCC", "IC - SIP", "IC - SOIC", "Testpoint"})
        Me.cbox_PG.Location = New System.Drawing.Point(12, 41)
        Me.cbox_PG.Name = "cbox_PG"
        Me.cbox_PG.Size = New System.Drawing.Size(131, 21)
        Me.cbox_PG.TabIndex = 23
        '
        'chkbox_CellOverhang
        '
        Me.chkbox_CellOverhang.AutoSize = True
        Me.chkbox_CellOverhang.Location = New System.Drawing.Point(12, 365)
        Me.chkbox_CellOverhang.Name = "chkbox_CellOverhang"
        Me.chkbox_CellOverhang.Size = New System.Drawing.Size(118, 17)
        Me.chkbox_CellOverhang.TabIndex = 44
        Me.chkbox_CellOverhang.Text = "Allow cell overhang"
        Me.chkbox_CellOverhang.UseVisualStyleBackColor = True
        '
        'cbox_Units
        '
        Me.cbox_Units.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Units.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Units.Enabled = False
        Me.cbox_Units.FormattingEnabled = True
        Me.cbox_Units.Items.AddRange(New Object() {"Inches", "Thousandths", "Millimeters", "Micrometers"})
        Me.cbox_Units.Location = New System.Drawing.Point(150, 41)
        Me.cbox_Units.Name = "cbox_Units"
        Me.cbox_Units.Size = New System.Drawing.Size(104, 21)
        Me.cbox_Units.TabIndex = 25
        '
        'chkbox_Verification
        '
        Me.chkbox_Verification.AutoSize = True
        Me.chkbox_Verification.Location = New System.Drawing.Point(12, 332)
        Me.chkbox_Verification.Name = "chkbox_Verification"
        Me.chkbox_Verification.Size = New System.Drawing.Size(114, 17)
        Me.chkbox_Verification.TabIndex = 43
        Me.chkbox_Verification.Text = "Verification Status:"
        Me.chkbox_Verification.UseVisualStyleBackColor = True
        '
        'chkbox_Units
        '
        Me.chkbox_Units.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.chkbox_Units.AutoSize = True
        Me.chkbox_Units.Location = New System.Drawing.Point(150, 24)
        Me.chkbox_Units.Name = "chkbox_Units"
        Me.chkbox_Units.Size = New System.Drawing.Size(50, 17)
        Me.chkbox_Units.TabIndex = 27
        Me.chkbox_Units.Text = "Units"
        Me.chkbox_Units.UseVisualStyleBackColor = True
        '
        'cbox_Verification
        '
        Me.cbox_Verification.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Verification.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Verification.Enabled = False
        Me.cbox_Verification.FormattingEnabled = True
        Me.cbox_Verification.Items.AddRange(New Object() {"Verified", "Unverified"})
        Me.cbox_Verification.Location = New System.Drawing.Point(136, 330)
        Me.cbox_Verification.Name = "cbox_Verification"
        Me.cbox_Verification.Size = New System.Drawing.Size(119, 21)
        Me.cbox_Verification.TabIndex = 41
        '
        'chkbox_CT
        '
        Me.chkbox_CT.AutoSize = True
        Me.chkbox_CT.Location = New System.Drawing.Point(12, 161)
        Me.chkbox_CT.Name = "chkbox_CT"
        Me.chkbox_CT.Size = New System.Drawing.Size(252, 17)
        Me.chkbox_CT.TabIndex = 30
        Me.chkbox_CT.Text = "Clearance Type (Leave before blank to replace)"
        Me.chkbox_CT.UseVisualStyleBackColor = True
        '
        'tbox_Underside
        '
        Me.tbox_Underside.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_Underside.Enabled = False
        Me.tbox_Underside.Location = New System.Drawing.Point(136, 288)
        Me.tbox_Underside.Name = "tbox_Underside"
        Me.tbox_Underside.Size = New System.Drawing.Size(64, 20)
        Me.tbox_Underside.TabIndex = 40
        '
        'tbox_CT_Before
        '
        Me.tbox_CT_Before.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_CT_Before.Enabled = False
        Me.tbox_CT_Before.Location = New System.Drawing.Point(56, 181)
        Me.tbox_CT_Before.Name = "tbox_CT_Before"
        Me.tbox_CT_Before.Size = New System.Drawing.Size(199, 20)
        Me.tbox_CT_Before.TabIndex = 31
        '
        'chkbox_Underside
        '
        Me.chkbox_Underside.AutoSize = True
        Me.chkbox_Underside.Location = New System.Drawing.Point(12, 290)
        Me.chkbox_Underside.Name = "chkbox_Underside"
        Me.chkbox_Underside.Size = New System.Drawing.Size(111, 17)
        Me.chkbox_Underside.TabIndex = 39
        Me.chkbox_Underside.Text = "Underside Space:"
        Me.chkbox_Underside.UseVisualStyleBackColor = True
        '
        'tbox_CT_After
        '
        Me.tbox_CT_After.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_CT_After.Enabled = False
        Me.tbox_CT_After.Location = New System.Drawing.Point(56, 207)
        Me.tbox_CT_After.Name = "tbox_CT_After"
        Me.tbox_CT_After.Size = New System.Drawing.Size(198, 20)
        Me.tbox_CT_After.TabIndex = 32
        '
        'tbox_Height
        '
        Me.tbox_Height.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tbox_Height.Enabled = False
        Me.tbox_Height.Location = New System.Drawing.Point(136, 248)
        Me.tbox_Height.Name = "tbox_Height"
        Me.tbox_Height.Size = New System.Drawing.Size(64, 20)
        Me.tbox_Height.TabIndex = 37
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(12, 185)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(38, 13)
        Me.Label4.TabIndex = 33
        Me.Label4.Text = "Before"
        '
        'chkbox_Height
        '
        Me.chkbox_Height.AutoSize = True
        Me.chkbox_Height.Location = New System.Drawing.Point(12, 250)
        Me.chkbox_Height.Name = "chkbox_Height"
        Me.chkbox_Height.Size = New System.Drawing.Size(60, 17)
        Me.chkbox_Height.TabIndex = 36
        Me.chkbox_Height.Text = "Height:"
        Me.chkbox_Height.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 211)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(29, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "After"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 474)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(514, 22)
        Me.StatusStrip1.TabIndex = 27
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(429, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Select some cells to begin..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WaitGif
        '
        Me.WaitGif.BackColor = System.Drawing.Color.Transparent
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'frmModifyCellAtts
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(514, 496)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(530, 530)
        Me.Name = "frmModifyCellAtts"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modify Cell Attributes"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnUpdate As System.Windows.Forms.Button
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tv_Cells As System.Windows.Forms.TreeView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents cbox_Units As System.Windows.Forms.ComboBox
    Friend WithEvents cbox_PG As System.Windows.Forms.ComboBox
    Friend WithEvents chkbox_Units As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_PG As System.Windows.Forms.CheckBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents tbox_CT_After As System.Windows.Forms.TextBox
    Friend WithEvents tbox_CT_Before As System.Windows.Forms.TextBox
    Friend WithEvents chkbox_CT As System.Windows.Forms.CheckBox
    Friend WithEvents tbox_Height As System.Windows.Forms.TextBox
    Friend WithEvents chkbox_Height As System.Windows.Forms.CheckBox
    Friend WithEvents tbox_Underside As System.Windows.Forms.TextBox
    Friend WithEvents chkbox_Underside As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_CellOverhang As System.Windows.Forms.ComboBox
    Friend WithEvents chkbox_CellOverhang As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_Verification As System.Windows.Forms.CheckBox
    Friend WithEvents cbox_Verification As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cbox_MoveablePins As System.Windows.Forms.ComboBox
    Friend WithEvents chkbox_Movable As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_Description As System.Windows.Forms.CheckBox
    Friend WithEvents tbox_Descript_Before As System.Windows.Forms.TextBox
    Friend WithEvents tbox_Descript_After As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cbox_HeightUnits As System.Windows.Forms.ComboBox
    Friend WithEvents cbox_UndersideHeightUnits As System.Windows.Forms.ComboBox
End Class
