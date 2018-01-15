<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddAltSymbol
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
        Me.tv_Parts = New System.Windows.Forms.TreeView()
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.pl_FileInput = New System.Windows.Forms.Panel()
        Me.pl_Excel = New System.Windows.Forms.Panel()
        Me.cboxActiveSheet = New System.Windows.Forms.ComboBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cbox_PN = New System.Windows.Forms.ComboBox()
        Me.cbox_Symbol = New System.Windows.Forms.ComboBox()
        Me.chkbox_ReadAllSheets = New System.Windows.Forms.CheckBox()
        Me.lblPN = New System.Windows.Forms.Label()
        Me.chkbox_IgnoreHeader = New System.Windows.Forms.CheckBox()
        Me.lblSymbol = New System.Windows.Forms.Label()
        Me.lbl_SymbolFile = New System.Windows.Forms.Label()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.tbox_Input = New System.Windows.Forms.TextBox()
        Me.btn_Browse = New System.Windows.Forms.Button()
        Me.gb_Actions = New System.Windows.Forms.GroupBox()
        Me.btn_Process = New System.Windows.Forms.Button()
        Me.ts_Notes = New System.Windows.Forms.Label()
        Me.chkbox_Error = New System.Windows.Forms.CheckBox()
        Me.ts_Warnings = New System.Windows.Forms.Label()
        Me.ts_Errors = New System.Windows.Forms.Label()
        Me.chkbox_Note = New System.Windows.Forms.CheckBox()
        Me.chkbox_Warning = New System.Windows.Forms.CheckBox()
        Me.StatusStrip1.SuspendLayout()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.pl_FileInput.SuspendLayout()
        Me.pl_Excel.SuspendLayout()
        Me.gb_Actions.SuspendLayout()
        Me.SuspendLayout()
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(447, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Browse to a Part/Alt. Symbol file to begin..."
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
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Size = New System.Drawing.Size(70, 17)
        '
        'tv_Parts
        '
        Me.tv_Parts.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Parts.Location = New System.Drawing.Point(0, 0)
        Me.tv_Parts.Name = "tv_Parts"
        Me.tv_Parts.Size = New System.Drawing.Size(259, 456)
        Me.tv_Parts.TabIndex = 0
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.pl_FileInput)
        Me.SplitContainer1.Panel1.Controls.Add(Me.gb_Actions)
        Me.SplitContainer1.Panel1MinSize = 269
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.tv_Parts)
        Me.SplitContainer1.Panel2MinSize = 226
        Me.SplitContainer1.Size = New System.Drawing.Size(532, 456)
        Me.SplitContainer1.SplitterDistance = 269
        Me.SplitContainer1.TabIndex = 95
        '
        'pl_FileInput
        '
        Me.pl_FileInput.Controls.Add(Me.pl_Excel)
        Me.pl_FileInput.Controls.Add(Me.lbl_SymbolFile)
        Me.pl_FileInput.Controls.Add(Me.btnRead)
        Me.pl_FileInput.Controls.Add(Me.tbox_Input)
        Me.pl_FileInput.Controls.Add(Me.btn_Browse)
        Me.pl_FileInput.Dock = System.Windows.Forms.DockStyle.Top
        Me.pl_FileInput.Location = New System.Drawing.Point(0, 0)
        Me.pl_FileInput.Name = "pl_FileInput"
        Me.pl_FileInput.Size = New System.Drawing.Size(269, 203)
        Me.pl_FileInput.TabIndex = 85
        '
        'pl_Excel
        '
        Me.pl_Excel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pl_Excel.Controls.Add(Me.cboxActiveSheet)
        Me.pl_Excel.Controls.Add(Me.Label1)
        Me.pl_Excel.Controls.Add(Me.cbox_PN)
        Me.pl_Excel.Controls.Add(Me.cbox_Symbol)
        Me.pl_Excel.Controls.Add(Me.chkbox_ReadAllSheets)
        Me.pl_Excel.Controls.Add(Me.lblPN)
        Me.pl_Excel.Controls.Add(Me.chkbox_IgnoreHeader)
        Me.pl_Excel.Controls.Add(Me.lblSymbol)
        Me.pl_Excel.Enabled = False
        Me.pl_Excel.Location = New System.Drawing.Point(0, 52)
        Me.pl_Excel.Name = "pl_Excel"
        Me.pl_Excel.Size = New System.Drawing.Size(269, 97)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Enabled = False
        Me.Label1.Location = New System.Drawing.Point(9, 67)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(62, 13)
        Me.Label1.TabIndex = 119
        Me.Label1.Text = "Worksheet:"
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
        'cbox_Symbol
        '
        Me.cbox_Symbol.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_Symbol.BackColor = System.Drawing.SystemColors.Control
        Me.cbox_Symbol.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cbox_Symbol.FormattingEnabled = True
        Me.cbox_Symbol.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_Symbol.Location = New System.Drawing.Point(211, 37)
        Me.cbox_Symbol.Name = "cbox_Symbol"
        Me.cbox_Symbol.Size = New System.Drawing.Size(50, 21)
        Me.cbox_Symbol.TabIndex = 113
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
        Me.lblPN.AutoSize = True
        Me.lblPN.Location = New System.Drawing.Point(136, 14)
        Me.lblPN.Name = "lblPN"
        Me.lblPN.Size = New System.Drawing.Size(69, 13)
        Me.lblPN.TabIndex = 115
        Me.lblPN.Text = "Part Number:"
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
        'lblSymbol
        '
        Me.lblSymbol.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblSymbol.AutoSize = True
        Me.lblSymbol.Location = New System.Drawing.Point(178, 41)
        Me.lblSymbol.Name = "lblSymbol"
        Me.lblSymbol.Size = New System.Drawing.Size(27, 13)
        Me.lblSymbol.TabIndex = 116
        Me.lblSymbol.Text = "Cell:"
        '
        'lbl_SymbolFile
        '
        Me.lbl_SymbolFile.AutoSize = True
        Me.lbl_SymbolFile.Location = New System.Drawing.Point(12, 9)
        Me.lbl_SymbolFile.Name = "lbl_SymbolFile"
        Me.lbl_SymbolFile.Size = New System.Drawing.Size(85, 13)
        Me.lbl_SymbolFile.TabIndex = 85
        Me.lbl_SymbolFile.Text = "Part/Alt. Symbol File"
        '
        'btnRead
        '
        Me.btnRead.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRead.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnRead.Enabled = False
        Me.btnRead.Location = New System.Drawing.Point(169, 158)
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
        'gb_Actions
        '
        Me.gb_Actions.Controls.Add(Me.btn_Process)
        Me.gb_Actions.Controls.Add(Me.ts_Notes)
        Me.gb_Actions.Controls.Add(Me.chkbox_Error)
        Me.gb_Actions.Controls.Add(Me.ts_Warnings)
        Me.gb_Actions.Controls.Add(Me.ts_Errors)
        Me.gb_Actions.Controls.Add(Me.chkbox_Note)
        Me.gb_Actions.Controls.Add(Me.chkbox_Warning)
        Me.gb_Actions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_Actions.Enabled = False
        Me.gb_Actions.Location = New System.Drawing.Point(0, 369)
        Me.gb_Actions.Name = "gb_Actions"
        Me.gb_Actions.Size = New System.Drawing.Size(269, 87)
        Me.gb_Actions.TabIndex = 84
        Me.gb_Actions.TabStop = False
        Me.gb_Actions.Text = "Options"
        '
        'btn_Process
        '
        Me.btn_Process.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Process.Location = New System.Drawing.Point(169, 44)
        Me.btn_Process.Name = "btn_Process"
        Me.btn_Process.Size = New System.Drawing.Size(92, 34)
        Me.btn_Process.TabIndex = 2
        Me.btn_Process.Text = "Process"
        Me.btn_Process.UseVisualStyleBackColor = False
        '
        'ts_Notes
        '
        Me.ts_Notes.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ts_Notes.AutoSize = True
        Me.ts_Notes.Location = New System.Drawing.Point(93, 63)
        Me.ts_Notes.Name = "ts_Notes"
        Me.ts_Notes.Size = New System.Drawing.Size(13, 13)
        Me.ts_Notes.TabIndex = 83
        Me.ts_Notes.Text = "0"
        '
        'chkbox_Error
        '
        Me.chkbox_Error.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkbox_Error.AutoSize = True
        Me.chkbox_Error.Checked = True
        Me.chkbox_Error.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Error.Location = New System.Drawing.Point(12, 21)
        Me.chkbox_Error.Name = "chkbox_Error"
        Me.chkbox_Error.Size = New System.Drawing.Size(53, 17)
        Me.chkbox_Error.TabIndex = 78
        Me.chkbox_Error.Text = "Errors"
        Me.chkbox_Error.UseVisualStyleBackColor = True
        '
        'ts_Warnings
        '
        Me.ts_Warnings.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ts_Warnings.AutoSize = True
        Me.ts_Warnings.Location = New System.Drawing.Point(93, 43)
        Me.ts_Warnings.Name = "ts_Warnings"
        Me.ts_Warnings.Size = New System.Drawing.Size(13, 13)
        Me.ts_Warnings.TabIndex = 82
        Me.ts_Warnings.Text = "0"
        '
        'ts_Errors
        '
        Me.ts_Errors.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.ts_Errors.AutoSize = True
        Me.ts_Errors.Location = New System.Drawing.Point(93, 23)
        Me.ts_Errors.Name = "ts_Errors"
        Me.ts_Errors.Size = New System.Drawing.Size(13, 13)
        Me.ts_Errors.TabIndex = 81
        Me.ts_Errors.Text = "0"
        '
        'chkbox_Note
        '
        Me.chkbox_Note.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkbox_Note.AutoSize = True
        Me.chkbox_Note.Checked = True
        Me.chkbox_Note.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Note.Location = New System.Drawing.Point(12, 61)
        Me.chkbox_Note.Name = "chkbox_Note"
        Me.chkbox_Note.Size = New System.Drawing.Size(54, 17)
        Me.chkbox_Note.TabIndex = 79
        Me.chkbox_Note.Text = "Notes"
        Me.chkbox_Note.UseVisualStyleBackColor = True
        '
        'chkbox_Warning
        '
        Me.chkbox_Warning.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.chkbox_Warning.AutoSize = True
        Me.chkbox_Warning.Checked = True
        Me.chkbox_Warning.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Warning.Location = New System.Drawing.Point(12, 41)
        Me.chkbox_Warning.Name = "chkbox_Warning"
        Me.chkbox_Warning.Size = New System.Drawing.Size(71, 17)
        Me.chkbox_Warning.TabIndex = 80
        Me.chkbox_Warning.Text = "Warnings"
        Me.chkbox_Warning.UseVisualStyleBackColor = True
        '
        'frmAddAltSymbol
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(532, 478)
        Me.Controls.Add(Me.SplitContainer1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.HelpButton = True
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddAltSymbol"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Add Symbol(s)"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.pl_FileInput.ResumeLayout(False)
        Me.pl_FileInput.PerformLayout()
        Me.pl_Excel.ResumeLayout(False)
        Me.pl_Excel.PerformLayout()
        Me.gb_Actions.ResumeLayout(False)
        Me.gb_Actions.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tv_Parts As System.Windows.Forms.TreeView
    Friend WithEvents SplitContainer1 As System.Windows.Forms.SplitContainer
    Friend WithEvents btn_Process As System.Windows.Forms.Button
    Friend WithEvents ts_Notes As System.Windows.Forms.Label
    Friend WithEvents ts_Warnings As System.Windows.Forms.Label
    Friend WithEvents chkbox_Note As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_Warning As System.Windows.Forms.CheckBox
    Friend WithEvents ts_Errors As System.Windows.Forms.Label
    Friend WithEvents chkbox_Error As System.Windows.Forms.CheckBox
    Friend WithEvents gb_Actions As System.Windows.Forms.GroupBox
    Friend WithEvents pl_FileInput As System.Windows.Forms.Panel
    Friend WithEvents pl_Excel As System.Windows.Forms.Panel
    Friend WithEvents cboxActiveSheet As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cbox_PN As System.Windows.Forms.ComboBox
    Friend WithEvents cbox_Symbol As System.Windows.Forms.ComboBox
    Friend WithEvents chkbox_ReadAllSheets As System.Windows.Forms.CheckBox
    Friend WithEvents lblPN As System.Windows.Forms.Label
    Friend WithEvents chkbox_IgnoreHeader As System.Windows.Forms.CheckBox
    Friend WithEvents lblSymbol As System.Windows.Forms.Label
    Friend WithEvents lbl_SymbolFile As System.Windows.Forms.Label
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents tbox_Input As System.Windows.Forms.TextBox
    Friend WithEvents btn_Browse As System.Windows.Forms.Button
End Class
