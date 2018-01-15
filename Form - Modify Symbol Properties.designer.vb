<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmModSymbols
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
        Me.components = New System.ComponentModel.Container()
        Me.gb_symbols = New System.Windows.Forms.GroupBox()
        Me.SelectionFilterMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ts_SelectAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.ts_RemoveAll = New System.Windows.Forms.ToolStripMenuItem()
        Me.tv_Symbols = New System.Windows.Forms.TreeView()
        Me.gb_Options = New System.Windows.Forms.GroupBox()
        Me.chkbox_RemoveSpaces = New System.Windows.Forms.CheckBox()
        Me.lbl_TimeMachine = New System.Windows.Forms.Label()
        Me.dateChooser = New System.Windows.Forms.DateTimePicker()
        Me.btn_Process = New System.Windows.Forms.Button()
        Me.btn_Read = New System.Windows.Forms.Button()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.SplitContainer2 = New System.Windows.Forms.SplitContainer()
        Me.gb_Properties = New System.Windows.Forms.GroupBox()
        Me.tv_Properties = New System.Windows.Forms.TreeView()
        Me.PropertyAction = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsm_PromoteCommonSymbol = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_SeperatorPromote = New System.Windows.Forms.ToolStripSeparator()
        Me.tsm_EditProperty = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_Copy = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsm_Rename = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_RemoveProperty = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsm_AddDefaultValue = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_RemoveDefaultValue = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.tv_PinProperties = New System.Windows.Forms.TreeView()
        Me.PinPropertyAction = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsm_EditPinPropertyAtts = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_CopyPinPropertyAtts = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsm_RenamePinProperty = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_RemovePinProperty = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsm_AddPinPropertyValue = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_RemovePinPropertyValue = New System.Windows.Forms.ToolStripMenuItem()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.RemovePinAction = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsm_RemovePinAction = New System.Windows.Forms.ToolStripMenuItem()
        Me.RemovePropertyAction = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.tsm_RemovePropertyAction = New System.Windows.Forms.ToolStripMenuItem()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsm_PromotePinProperty = New System.Windows.Forms.ToolStripMenuItem()
        Me.gb_symbols.SuspendLayout()
        Me.SelectionFilterMenu.SuspendLayout()
        Me.gb_Options.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer2.Panel1.SuspendLayout()
        Me.SplitContainer2.Panel2.SuspendLayout()
        Me.SplitContainer2.SuspendLayout()
        Me.gb_Properties.SuspendLayout()
        Me.PropertyAction.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.PinPropertyAction.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.RemovePinAction.SuspendLayout()
        Me.RemovePropertyAction.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'gb_symbols
        '
        Me.gb_symbols.ContextMenuStrip = Me.SelectionFilterMenu
        Me.gb_symbols.Controls.Add(Me.tv_Symbols)
        Me.gb_symbols.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gb_symbols.Location = New System.Drawing.Point(0, 0)
        Me.gb_symbols.Name = "gb_symbols"
        Me.gb_symbols.Size = New System.Drawing.Size(237, 400)
        Me.gb_symbols.TabIndex = 19
        Me.gb_symbols.TabStop = False
        Me.gb_symbols.Text = "Partitions/Symbols:"
        '
        'SelectionFilterMenu
        '
        Me.SelectionFilterMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_SelectAll, Me.ts_RemoveAll})
        Me.SelectionFilterMenu.Name = "ContextMenuStrip1"
        Me.SelectionFilterMenu.Size = New System.Drawing.Size(135, 48)
        '
        'ts_SelectAll
        '
        Me.ts_SelectAll.Name = "ts_SelectAll"
        Me.ts_SelectAll.Size = New System.Drawing.Size(134, 22)
        Me.ts_SelectAll.Text = "Select All"
        '
        'ts_RemoveAll
        '
        Me.ts_RemoveAll.Name = "ts_RemoveAll"
        Me.ts_RemoveAll.Size = New System.Drawing.Size(134, 22)
        Me.ts_RemoveAll.Text = "Remove All"
        '
        'tv_Symbols
        '
        Me.tv_Symbols.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv_Symbols.CheckBoxes = True
        Me.tv_Symbols.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Symbols.Location = New System.Drawing.Point(3, 16)
        Me.tv_Symbols.Name = "tv_Symbols"
        Me.tv_Symbols.Size = New System.Drawing.Size(231, 381)
        Me.tv_Symbols.TabIndex = 0
        '
        'gb_Options
        '
        Me.gb_Options.Controls.Add(Me.chkbox_RemoveSpaces)
        Me.gb_Options.Controls.Add(Me.lbl_TimeMachine)
        Me.gb_Options.Controls.Add(Me.dateChooser)
        Me.gb_Options.Controls.Add(Me.btn_Process)
        Me.gb_Options.Controls.Add(Me.btn_Read)
        Me.gb_Options.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_Options.Location = New System.Drawing.Point(0, 400)
        Me.gb_Options.Name = "gb_Options"
        Me.gb_Options.Size = New System.Drawing.Size(237, 137)
        Me.gb_Options.TabIndex = 18
        Me.gb_Options.TabStop = False
        Me.gb_Options.Text = "Options"
        '
        'chkbox_RemoveSpaces
        '
        Me.chkbox_RemoveSpaces.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkbox_RemoveSpaces.AutoSize = True
        Me.chkbox_RemoveSpaces.Enabled = False
        Me.chkbox_RemoveSpaces.Location = New System.Drawing.Point(9, 110)
        Me.chkbox_RemoveSpaces.Name = "chkbox_RemoveSpaces"
        Me.chkbox_RemoveSpaces.Size = New System.Drawing.Size(142, 17)
        Me.chkbox_RemoveSpaces.TabIndex = 14
        Me.chkbox_RemoveSpaces.Text = "Remove Trailing Spaces"
        Me.chkbox_RemoveSpaces.UseVisualStyleBackColor = True
        '
        'lbl_TimeMachine
        '
        Me.lbl_TimeMachine.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lbl_TimeMachine.AutoSize = True
        Me.lbl_TimeMachine.Enabled = False
        Me.lbl_TimeMachine.Location = New System.Drawing.Point(7, 64)
        Me.lbl_TimeMachine.Name = "lbl_TimeMachine"
        Me.lbl_TimeMachine.Size = New System.Drawing.Size(175, 13)
        Me.lbl_TimeMachine.TabIndex = 17
        Me.lbl_TimeMachine.Text = "Time Machine (Reset Time Stamps)"
        '
        'dateChooser
        '
        Me.dateChooser.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dateChooser.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dateChooser.Location = New System.Drawing.Point(6, 83)
        Me.dateChooser.Name = "dateChooser"
        Me.dateChooser.ShowCheckBox = True
        Me.dateChooser.ShowUpDown = True
        Me.dateChooser.Size = New System.Drawing.Size(225, 20)
        Me.dateChooser.TabIndex = 16
        '
        'btn_Process
        '
        Me.btn_Process.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn_Process.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Process.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btn_Process.Enabled = False
        Me.btn_Process.Location = New System.Drawing.Point(124, 25)
        Me.btn_Process.Name = "btn_Process"
        Me.btn_Process.Size = New System.Drawing.Size(89, 24)
        Me.btn_Process.TabIndex = 3
        Me.btn_Process.Text = "Process"
        Me.btn_Process.UseVisualStyleBackColor = False
        '
        'btn_Read
        '
        Me.btn_Read.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.btn_Read.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Read.Location = New System.Drawing.Point(18, 25)
        Me.btn_Read.Name = "btn_Read"
        Me.btn_Read.Size = New System.Drawing.Size(89, 24)
        Me.btn_Read.TabIndex = 2
        Me.btn_Read.Text = "Read"
        Me.btn_Read.UseVisualStyleBackColor = False
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_symbols)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_Options)
        Me.SplitContainer1.Panel1MinSize = 230
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.SplitContainer2)
        Me.SplitContainer1.Size = New System.Drawing.Size(459, 537)
        Me.SplitContainer1.SplitterDistance = 237
        Me.SplitContainer1.TabIndex = 21
        '
        'SplitContainer2
        '
        Me.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer2.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer2.Name = "SplitContainer2"
        Me.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer2.Panel1
        '
        Me.SplitContainer2.Panel1.Controls.Add(Me.gb_Properties)
        '
        'SplitContainer2.Panel2
        '
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox1)
        Me.SplitContainer2.Panel2.Controls.Add(Me.GroupBox2)
        Me.SplitContainer2.Size = New System.Drawing.Size(218, 537)
        Me.SplitContainer2.SplitterDistance = 316
        Me.SplitContainer2.TabIndex = 3
        '
        'gb_Properties
        '
        Me.gb_Properties.Controls.Add(Me.tv_Properties)
        Me.gb_Properties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gb_Properties.Location = New System.Drawing.Point(0, 0)
        Me.gb_Properties.Name = "gb_Properties"
        Me.gb_Properties.Size = New System.Drawing.Size(218, 316)
        Me.gb_Properties.TabIndex = 0
        Me.gb_Properties.TabStop = False
        Me.gb_Properties.Text = "Properties"
        '
        'tv_Properties
        '
        Me.tv_Properties.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv_Properties.ContextMenuStrip = Me.PropertyAction
        Me.tv_Properties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Properties.Location = New System.Drawing.Point(3, 16)
        Me.tv_Properties.Name = "tv_Properties"
        Me.tv_Properties.Size = New System.Drawing.Size(212, 297)
        Me.tv_Properties.TabIndex = 0
        '
        'PropertyAction
        '
        Me.PropertyAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsm_PromoteCommonSymbol, Me.tsm_SeperatorPromote, Me.tsm_EditProperty, Me.tsm_Copy, Me.ToolStripSeparator1, Me.tsm_Rename, Me.tsm_RemoveProperty, Me.ToolStripSeparator2, Me.tsm_AddDefaultValue, Me.tsm_RemoveDefaultValue})
        Me.PropertyAction.Name = "MyContextMenu"
        Me.PropertyAction.Size = New System.Drawing.Size(191, 176)
        '
        'tsm_PromoteCommonSymbol
        '
        Me.tsm_PromoteCommonSymbol.Name = "tsm_PromoteCommonSymbol"
        Me.tsm_PromoteCommonSymbol.Size = New System.Drawing.Size(190, 22)
        Me.tsm_PromoteCommonSymbol.Text = "Promote to common"
        Me.tsm_PromoteCommonSymbol.Visible = False
        '
        'tsm_SeperatorPromote
        '
        Me.tsm_SeperatorPromote.Name = "tsm_SeperatorPromote"
        Me.tsm_SeperatorPromote.Size = New System.Drawing.Size(187, 6)
        Me.tsm_SeperatorPromote.Visible = False
        '
        'tsm_EditProperty
        '
        Me.tsm_EditProperty.Name = "tsm_EditProperty"
        Me.tsm_EditProperty.Size = New System.Drawing.Size(190, 22)
        Me.tsm_EditProperty.Text = "Edit Attributes"
        '
        'tsm_Copy
        '
        Me.tsm_Copy.Name = "tsm_Copy"
        Me.tsm_Copy.Size = New System.Drawing.Size(190, 22)
        Me.tsm_Copy.Text = "Copy attributes from"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(187, 6)
        '
        'tsm_Rename
        '
        Me.tsm_Rename.Name = "tsm_Rename"
        Me.tsm_Rename.Size = New System.Drawing.Size(190, 22)
        Me.tsm_Rename.Text = "Rename"
        '
        'tsm_RemoveProperty
        '
        Me.tsm_RemoveProperty.Name = "tsm_RemoveProperty"
        Me.tsm_RemoveProperty.Size = New System.Drawing.Size(190, 22)
        Me.tsm_RemoveProperty.Text = "Remove"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(187, 6)
        '
        'tsm_AddDefaultValue
        '
        Me.tsm_AddDefaultValue.Name = "tsm_AddDefaultValue"
        Me.tsm_AddDefaultValue.Size = New System.Drawing.Size(190, 22)
        Me.tsm_AddDefaultValue.Text = "Add Default Value"
        '
        'tsm_RemoveDefaultValue
        '
        Me.tsm_RemoveDefaultValue.Name = "tsm_RemoveDefaultValue"
        Me.tsm_RemoveDefaultValue.Size = New System.Drawing.Size(190, 22)
        Me.tsm_RemoveDefaultValue.Text = "Remove Default Value"
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.tv_PinProperties)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 48)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(218, 169)
        Me.GroupBox1.TabIndex = 1
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pin Properties"
        '
        'tv_PinProperties
        '
        Me.tv_PinProperties.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv_PinProperties.ContextMenuStrip = Me.PinPropertyAction
        Me.tv_PinProperties.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_PinProperties.Location = New System.Drawing.Point(3, 16)
        Me.tv_PinProperties.Name = "tv_PinProperties"
        Me.tv_PinProperties.Size = New System.Drawing.Size(212, 150)
        Me.tv_PinProperties.TabIndex = 0
        '
        'PinPropertyAction
        '
        Me.PinPropertyAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsm_PromotePinProperty, Me.ToolStripSeparator5, Me.tsm_EditPinPropertyAtts, Me.tsm_CopyPinPropertyAtts, Me.ToolStripSeparator3, Me.tsm_RenamePinProperty, Me.tsm_RemovePinProperty, Me.ToolStripSeparator4, Me.tsm_AddPinPropertyValue, Me.tsm_RemovePinPropertyValue})
        Me.PinPropertyAction.Name = "MyContextMenu"
        Me.PinPropertyAction.Size = New System.Drawing.Size(191, 198)
        '
        'tsm_EditPinPropertyAtts
        '
        Me.tsm_EditPinPropertyAtts.Name = "tsm_EditPinPropertyAtts"
        Me.tsm_EditPinPropertyAtts.Size = New System.Drawing.Size(190, 22)
        Me.tsm_EditPinPropertyAtts.Text = "Edit Attributes"
        '
        'tsm_CopyPinPropertyAtts
        '
        Me.tsm_CopyPinPropertyAtts.Name = "tsm_CopyPinPropertyAtts"
        Me.tsm_CopyPinPropertyAtts.Size = New System.Drawing.Size(190, 22)
        Me.tsm_CopyPinPropertyAtts.Text = "Copy attributes from"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(187, 6)
        '
        'tsm_RenamePinProperty
        '
        Me.tsm_RenamePinProperty.Name = "tsm_RenamePinProperty"
        Me.tsm_RenamePinProperty.Size = New System.Drawing.Size(190, 22)
        Me.tsm_RenamePinProperty.Text = "Rename"
        '
        'tsm_RemovePinProperty
        '
        Me.tsm_RemovePinProperty.Name = "tsm_RemovePinProperty"
        Me.tsm_RemovePinProperty.Size = New System.Drawing.Size(190, 22)
        Me.tsm_RemovePinProperty.Text = "Remove"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(187, 6)
        '
        'tsm_AddPinPropertyValue
        '
        Me.tsm_AddPinPropertyValue.Name = "tsm_AddPinPropertyValue"
        Me.tsm_AddPinPropertyValue.Size = New System.Drawing.Size(190, 22)
        Me.tsm_AddPinPropertyValue.Text = "Add Default Value"
        '
        'tsm_RemovePinPropertyValue
        '
        Me.tsm_RemovePinPropertyValue.Name = "tsm_RemovePinPropertyValue"
        Me.tsm_RemovePinPropertyValue.Size = New System.Drawing.Size(190, 22)
        Me.tsm_RemovePinPropertyValue.Text = "Remove Default Value"
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.Button2)
        Me.GroupBox2.Controls.Add(Me.Button1)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(218, 48)
        Me.GroupBox2.TabIndex = 2
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Add Property"
        '
        'Button2
        '
        Me.Button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button2.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button2.Location = New System.Drawing.Point(116, 15)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "Pin"
        Me.Button2.UseVisualStyleBackColor = False
        '
        'Button1
        '
        Me.Button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Button1.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.Button1.Location = New System.Drawing.Point(27, 15)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Symbol"
        Me.Button1.UseVisualStyleBackColor = False
        '
        'RemovePinAction
        '
        Me.RemovePinAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsm_RemovePinAction})
        Me.RemovePinAction.Name = "RemovePinAction"
        Me.RemovePinAction.Size = New System.Drawing.Size(118, 26)
        '
        'tsm_RemovePinAction
        '
        Me.tsm_RemovePinAction.Name = "tsm_RemovePinAction"
        Me.tsm_RemovePinAction.Size = New System.Drawing.Size(117, 22)
        Me.tsm_RemovePinAction.Text = "Remove"
        '
        'RemovePropertyAction
        '
        Me.RemovePropertyAction.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsm_RemovePropertyAction})
        Me.RemovePropertyAction.Name = "RemovePinAction"
        Me.RemovePropertyAction.Size = New System.Drawing.Size(118, 26)
        '
        'tsm_RemovePropertyAction
        '
        Me.tsm_RemovePropertyAction.Name = "tsm_RemovePropertyAction"
        Me.tsm_RemovePropertyAction.Size = New System.Drawing.Size(117, 22)
        Me.tsm_RemovePropertyAction.Text = "Remove"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(374, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Press ""Read"" to begin..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WaitGif
        '
        Me.WaitGif.BackColor = System.Drawing.Color.Transparent
        Me.WaitGif.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.WaitGif.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        Me.WaitGif.Text = "ToolStripStatusLabel2"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Control
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 537)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(459, 22)
        Me.StatusStrip1.TabIndex = 20
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(187, 6)
        Me.ToolStripSeparator5.Visible = False
        '
        'tsm_PromotePinProperty
        '
        Me.tsm_PromotePinProperty.Name = "tsm_PromotePinProperty"
        Me.tsm_PromotePinProperty.Size = New System.Drawing.Size(190, 22)
        Me.tsm_PromotePinProperty.Text = "Promote to common"
        Me.tsm_PromotePinProperty.Visible = False
        '
        'frmModSymbols
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(459, 559)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.HelpButton = True
        Me.Margin = New System.Windows.Forms.Padding(2)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(475, 593)
        Me.Name = "frmModSymbols"
        Me.ShowIcon = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Modify Symbol Propeties"
        Me.gb_symbols.ResumeLayout(False)
        Me.SelectionFilterMenu.ResumeLayout(False)
        Me.gb_Options.ResumeLayout(False)
        Me.gb_Options.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.SplitContainer2.Panel1.ResumeLayout(False)
        Me.SplitContainer2.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer2, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer2.ResumeLayout(False)
        Me.gb_Properties.ResumeLayout(False)
        Me.PropertyAction.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.PinPropertyAction.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.RemovePinAction.ResumeLayout(False)
        Me.RemovePropertyAction.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents gb_symbols As System.Windows.Forms.GroupBox
    Friend WithEvents gb_Options As System.Windows.Forms.GroupBox
    Friend WithEvents btn_Process As System.Windows.Forms.Button
    Friend WithEvents btn_Read As System.Windows.Forms.Button
    Friend WithEvents chkbox_RemoveSpaces As System.Windows.Forms.CheckBox
    Friend WithEvents tv_Symbols As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents SelectionFilterMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents ts_SelectAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ts_RemoveAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PropertyAction As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsm_EditProperty As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_Copy As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsm_Rename As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_RemoveProperty As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsm_AddDefaultValue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_RemoveDefaultValue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents gb_Properties As System.Windows.Forms.GroupBox
    Friend WithEvents tv_Properties As System.Windows.Forms.TreeView
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents tv_PinProperties As System.Windows.Forms.TreeView
    Friend WithEvents PinPropertyAction As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsm_EditPinPropertyAtts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_CopyPinPropertyAtts As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsm_RenamePinProperty As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_RemovePinProperty As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsm_AddPinPropertyValue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_RemovePinPropertyValue As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents dateChooser As System.Windows.Forms.DateTimePicker
    Friend WithEvents lbl_TimeMachine As System.Windows.Forms.Label
    Friend WithEvents RemovePinAction As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsm_RemovePinAction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents RemovePropertyAction As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents tsm_RemovePropertyAction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents tsm_PromoteCommonSymbol As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_SeperatorPromote As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents SplitContainer2 As System.Windows.Forms.SplitContainer
    Friend WithEvents tsm_PromotePinProperty As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator

End Class
