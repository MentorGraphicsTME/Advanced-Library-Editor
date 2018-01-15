<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmHealPDBfromExcel
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
        Me.gbActions = New System.Windows.Forms.GroupBox()
        Me.btnProcess = New System.Windows.Forms.Button()
        Me.btnRead = New System.Windows.Forms.Button()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.lbl_Browse_A = New System.Windows.Forms.Label()
        Me.tboxWorkbook = New System.Windows.Forms.TextBox()
        Me.btn_Browse_Excel = New System.Windows.Forms.Button()
        Me.lblCell = New System.Windows.Forms.Label()
        Me.lblSymbol = New System.Windows.Forms.Label()
        Me.lblPN = New System.Windows.Forms.Label()
        Me.cbox_CellName = New System.Windows.Forms.ComboBox()
        Me.cbox_PartNumber = New System.Windows.Forms.ComboBox()
        Me.cbox_SymbolName = New System.Windows.Forms.ComboBox()
        Me.chkbox_Note = New System.Windows.Forms.CheckBox()
        Me.chkbox_Warning = New System.Windows.Forms.CheckBox()
        Me.chkbox_Error = New System.Windows.Forms.CheckBox()
        Me.gbLog = New System.Windows.Forms.GroupBox()
        Me.gbOptions = New System.Windows.Forms.GroupBox()
        Me.chkbox_UpdatePartType = New System.Windows.Forms.CheckBox()
        Me.chkbox_RepairErrors = New System.Windows.Forms.CheckBox()
        Me.chkbox_AddNC = New System.Windows.Forms.CheckBox()
        Me.gbActions.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.gbLog.SuspendLayout()
        Me.gbOptions.SuspendLayout()
        Me.SuspendLayout()
        '
        'gbActions
        '
        Me.gbActions.Controls.Add(Me.btnProcess)
        Me.gbActions.Controls.Add(Me.btnRead)
        Me.gbActions.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gbActions.Enabled = False
        Me.gbActions.Location = New System.Drawing.Point(0, 204)
        Me.gbActions.Name = "gbActions"
        Me.gbActions.Size = New System.Drawing.Size(258, 53)
        Me.gbActions.TabIndex = 83
        Me.gbActions.TabStop = False
        Me.gbActions.Text = "Actions:"
        '
        'btnProcess
        '
        Me.btnProcess.Enabled = False
        Me.btnProcess.Location = New System.Drawing.Point(141, 19)
        Me.btnProcess.Name = "btnProcess"
        Me.btnProcess.Size = New System.Drawing.Size(75, 23)
        Me.btnProcess.TabIndex = 91
        Me.btnProcess.Text = "Process"
        Me.btnProcess.UseVisualStyleBackColor = True
        '
        'btnRead
        '
        Me.btnRead.Location = New System.Drawing.Point(40, 19)
        Me.btnRead.Name = "btnRead"
        Me.btnRead.Size = New System.Drawing.Size(75, 23)
        Me.btnRead.TabIndex = 92
        Me.btnRead.Text = "Read"
        Me.btnRead.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.lbl_Browse_A)
        Me.GroupBox2.Controls.Add(Me.tboxWorkbook)
        Me.GroupBox2.Controls.Add(Me.btn_Browse_Excel)
        Me.GroupBox2.Controls.Add(Me.lblCell)
        Me.GroupBox2.Controls.Add(Me.lblSymbol)
        Me.GroupBox2.Controls.Add(Me.lblPN)
        Me.GroupBox2.Controls.Add(Me.cbox_CellName)
        Me.GroupBox2.Controls.Add(Me.cbox_PartNumber)
        Me.GroupBox2.Controls.Add(Me.cbox_SymbolName)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Top
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(258, 107)
        Me.GroupBox2.TabIndex = 85
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Excel Info:"
        '
        'lbl_Browse_A
        '
        Me.lbl_Browse_A.AutoSize = True
        Me.lbl_Browse_A.Location = New System.Drawing.Point(6, 23)
        Me.lbl_Browse_A.Name = "lbl_Browse_A"
        Me.lbl_Browse_A.Size = New System.Drawing.Size(51, 13)
        Me.lbl_Browse_A.TabIndex = 82
        Me.lbl_Browse_A.Text = "Location:"
        '
        'tboxWorkbook
        '
        Me.tboxWorkbook.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tboxWorkbook.Location = New System.Drawing.Point(6, 39)
        Me.tboxWorkbook.Name = "tboxWorkbook"
        Me.tboxWorkbook.Size = New System.Drawing.Size(206, 20)
        Me.tboxWorkbook.TabIndex = 80
        '
        'btn_Browse_Excel
        '
        Me.btn_Browse_Excel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Browse_Excel.Location = New System.Drawing.Point(218, 39)
        Me.btn_Browse_Excel.Name = "btn_Browse_Excel"
        Me.btn_Browse_Excel.Size = New System.Drawing.Size(30, 20)
        Me.btn_Browse_Excel.TabIndex = 81
        Me.btn_Browse_Excel.Text = "..."
        Me.btn_Browse_Excel.UseVisualStyleBackColor = True
        '
        'lblCell
        '
        Me.lblCell.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCell.AutoSize = True
        Me.lblCell.Enabled = False
        Me.lblCell.Location = New System.Drawing.Point(173, 62)
        Me.lblCell.Name = "lblCell"
        Me.lblCell.Size = New System.Drawing.Size(58, 13)
        Me.lblCell.TabIndex = 88
        Me.lblCell.Text = "Cell Name:"
        '
        'lblSymbol
        '
        Me.lblSymbol.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.lblSymbol.AutoSize = True
        Me.lblSymbol.Enabled = False
        Me.lblSymbol.Location = New System.Drawing.Point(88, 62)
        Me.lblSymbol.Name = "lblSymbol"
        Me.lblSymbol.Size = New System.Drawing.Size(75, 13)
        Me.lblSymbol.TabIndex = 86
        Me.lblSymbol.Text = "Symbol Name:"
        '
        'lblPN
        '
        Me.lblPN.AutoSize = True
        Me.lblPN.Enabled = False
        Me.lblPN.Location = New System.Drawing.Point(3, 62)
        Me.lblPN.Name = "lblPN"
        Me.lblPN.Size = New System.Drawing.Size(69, 13)
        Me.lblPN.TabIndex = 87
        Me.lblPN.Text = "Part Number:"
        '
        'cbox_CellName
        '
        Me.cbox_CellName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cbox_CellName.Enabled = False
        Me.cbox_CellName.FormattingEnabled = True
        Me.cbox_CellName.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_CellName.Location = New System.Drawing.Point(176, 78)
        Me.cbox_CellName.Name = "cbox_CellName"
        Me.cbox_CellName.Size = New System.Drawing.Size(76, 21)
        Me.cbox_CellName.TabIndex = 83
        '
        'cbox_PartNumber
        '
        Me.cbox_PartNumber.Enabled = False
        Me.cbox_PartNumber.FormattingEnabled = True
        Me.cbox_PartNumber.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_PartNumber.Location = New System.Drawing.Point(6, 78)
        Me.cbox_PartNumber.Name = "cbox_PartNumber"
        Me.cbox_PartNumber.Size = New System.Drawing.Size(76, 21)
        Me.cbox_PartNumber.TabIndex = 84
        '
        'cbox_SymbolName
        '
        Me.cbox_SymbolName.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.cbox_SymbolName.Enabled = False
        Me.cbox_SymbolName.FormattingEnabled = True
        Me.cbox_SymbolName.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.cbox_SymbolName.Location = New System.Drawing.Point(91, 78)
        Me.cbox_SymbolName.Name = "cbox_SymbolName"
        Me.cbox_SymbolName.Size = New System.Drawing.Size(76, 21)
        Me.cbox_SymbolName.TabIndex = 85
        '
        'chkbox_Note
        '
        Me.chkbox_Note.AutoSize = True
        Me.chkbox_Note.Checked = True
        Me.chkbox_Note.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Note.Location = New System.Drawing.Point(16, 65)
        Me.chkbox_Note.Name = "chkbox_Note"
        Me.chkbox_Note.Size = New System.Drawing.Size(54, 17)
        Me.chkbox_Note.TabIndex = 71
        Me.chkbox_Note.Text = "Notes"
        Me.chkbox_Note.UseVisualStyleBackColor = True
        '
        'chkbox_Warning
        '
        Me.chkbox_Warning.AutoSize = True
        Me.chkbox_Warning.Checked = True
        Me.chkbox_Warning.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Warning.Location = New System.Drawing.Point(16, 42)
        Me.chkbox_Warning.Name = "chkbox_Warning"
        Me.chkbox_Warning.Size = New System.Drawing.Size(71, 17)
        Me.chkbox_Warning.TabIndex = 72
        Me.chkbox_Warning.Text = "Warnings"
        Me.chkbox_Warning.UseVisualStyleBackColor = True
        '
        'chkbox_Error
        '
        Me.chkbox_Error.AutoSize = True
        Me.chkbox_Error.Checked = True
        Me.chkbox_Error.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Error.Location = New System.Drawing.Point(16, 19)
        Me.chkbox_Error.Name = "chkbox_Error"
        Me.chkbox_Error.Size = New System.Drawing.Size(53, 17)
        Me.chkbox_Error.TabIndex = 70
        Me.chkbox_Error.Text = "Errors"
        Me.chkbox_Error.UseVisualStyleBackColor = True
        '
        'gbLog
        '
        Me.gbLog.Controls.Add(Me.chkbox_Error)
        Me.gbLog.Controls.Add(Me.chkbox_Warning)
        Me.gbLog.Controls.Add(Me.chkbox_Note)
        Me.gbLog.Dock = System.Windows.Forms.DockStyle.Right
        Me.gbLog.Enabled = False
        Me.gbLog.Location = New System.Drawing.Point(169, 107)
        Me.gbLog.Name = "gbLog"
        Me.gbLog.Size = New System.Drawing.Size(89, 97)
        Me.gbLog.TabIndex = 86
        Me.gbLog.TabStop = False
        Me.gbLog.Text = "Log:"
        '
        'gbOptions
        '
        Me.gbOptions.Controls.Add(Me.chkbox_UpdatePartType)
        Me.gbOptions.Controls.Add(Me.chkbox_RepairErrors)
        Me.gbOptions.Controls.Add(Me.chkbox_AddNC)
        Me.gbOptions.Dock = System.Windows.Forms.DockStyle.Left
        Me.gbOptions.Enabled = False
        Me.gbOptions.Location = New System.Drawing.Point(0, 107)
        Me.gbOptions.Name = "gbOptions"
        Me.gbOptions.Size = New System.Drawing.Size(163, 97)
        Me.gbOptions.TabIndex = 87
        Me.gbOptions.TabStop = False
        Me.gbOptions.Text = "Options:"
        '
        'chkbox_UpdatePartType
        '
        Me.chkbox_UpdatePartType.AutoSize = True
        Me.chkbox_UpdatePartType.Checked = True
        Me.chkbox_UpdatePartType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_UpdatePartType.Location = New System.Drawing.Point(12, 42)
        Me.chkbox_UpdatePartType.Name = "chkbox_UpdatePartType"
        Me.chkbox_UpdatePartType.Size = New System.Drawing.Size(110, 17)
        Me.chkbox_UpdatePartType.TabIndex = 61
        Me.chkbox_UpdatePartType.Text = "Update Part Type"
        Me.chkbox_UpdatePartType.UseVisualStyleBackColor = True
        '
        'chkbox_RepairErrors
        '
        Me.chkbox_RepairErrors.AutoSize = True
        Me.chkbox_RepairErrors.Checked = True
        Me.chkbox_RepairErrors.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_RepairErrors.Location = New System.Drawing.Point(12, 19)
        Me.chkbox_RepairErrors.Name = "chkbox_RepairErrors"
        Me.chkbox_RepairErrors.Size = New System.Drawing.Size(138, 17)
        Me.chkbox_RepairErrors.TabIndex = 63
        Me.chkbox_RepairErrors.Text = "Attempt to Repair Errors"
        Me.chkbox_RepairErrors.UseVisualStyleBackColor = True
        '
        'chkbox_AddNC
        '
        Me.chkbox_AddNC.AutoSize = True
        Me.chkbox_AddNC.Checked = True
        Me.chkbox_AddNC.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_AddNC.Location = New System.Drawing.Point(12, 65)
        Me.chkbox_AddNC.Name = "chkbox_AddNC"
        Me.chkbox_AddNC.Size = New System.Drawing.Size(86, 17)
        Me.chkbox_AddNC.TabIndex = 62
        Me.chkbox_AddNC.Text = "Add NC Pins"
        Me.chkbox_AddNC.UseVisualStyleBackColor = True
        '
        'frmHealPDBfromExcel
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(258, 257)
        Me.Controls.Add(Me.gbLog)
        Me.Controls.Add(Me.gbOptions)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.gbActions)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmHealPDBfromExcel"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Heal PDB using Excel Data"
        Me.gbActions.ResumeLayout(False)
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.gbLog.ResumeLayout(False)
        Me.gbLog.PerformLayout()
        Me.gbOptions.ResumeLayout(False)
        Me.gbOptions.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents gbActions As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents lblCell As System.Windows.Forms.Label
    Friend WithEvents lblSymbol As System.Windows.Forms.Label
    Friend WithEvents lblPN As System.Windows.Forms.Label
    Friend WithEvents cbox_CellName As System.Windows.Forms.ComboBox
    Friend WithEvents cbox_PartNumber As System.Windows.Forms.ComboBox
    Friend WithEvents cbox_SymbolName As System.Windows.Forms.ComboBox
    Friend WithEvents btn_Browse_Excel As System.Windows.Forms.Button
    Friend WithEvents tboxWorkbook As System.Windows.Forms.TextBox
    Friend WithEvents lbl_Browse_A As System.Windows.Forms.Label
    Friend WithEvents chkbox_Note As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_Warning As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_Error As System.Windows.Forms.CheckBox
    Friend WithEvents gbLog As System.Windows.Forms.GroupBox
    Friend WithEvents btnProcess As System.Windows.Forms.Button
    Friend WithEvents btnRead As System.Windows.Forms.Button
    Friend WithEvents gbOptions As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_UpdatePartType As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_RepairErrors As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_AddNC As System.Windows.Forms.CheckBox
End Class
