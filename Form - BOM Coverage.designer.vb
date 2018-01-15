<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmBOMCoverage
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
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.pl_Excel = New System.Windows.Forms.Panel()
        Me.lbl_PartNumber = New System.Windows.Forms.Label()
        Me.PN_cbox = New System.Windows.Forms.ComboBox()
        Me.btnEvaluate = New System.Windows.Forms.Button()
        Me.cboxActiveSheet = New System.Windows.Forms.ComboBox()
        Me.lbl_Worksheet = New System.Windows.Forms.Label()
        Me.chkbox_IgnoreHeader = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.btn_Browse_SwapPN = New System.Windows.Forms.Button()
        Me.tbox_Input = New System.Windows.Forms.TextBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblPercentCoverage = New System.Windows.Forms.Label()
        Me.lblPartsFound = New System.Windows.Forms.Label()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.lblPartsRead = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.StatusStrip1.SuspendLayout()
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
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 369)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(449, 22)
        Me.StatusStrip1.TabIndex = 11
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(364, 17)
        Me.ts_Status.Spring = True
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
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.pl_Excel)
        Me.GroupBox1.Controls.Add(Me.Panel3)
        Me.GroupBox1.Controls.Add(Me.Panel2)
        Me.GroupBox1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox1.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(449, 369)
        Me.GroupBox1.TabIndex = 14
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Input"
        '
        'pl_Excel
        '
        Me.pl_Excel.Controls.Add(Me.lbl_PartNumber)
        Me.pl_Excel.Controls.Add(Me.PN_cbox)
        Me.pl_Excel.Controls.Add(Me.btnEvaluate)
        Me.pl_Excel.Controls.Add(Me.cboxActiveSheet)
        Me.pl_Excel.Controls.Add(Me.lbl_Worksheet)
        Me.pl_Excel.Controls.Add(Me.chkbox_IgnoreHeader)
        Me.pl_Excel.Dock = System.Windows.Forms.DockStyle.Top
        Me.pl_Excel.Enabled = False
        Me.pl_Excel.Location = New System.Drawing.Point(3, 83)
        Me.pl_Excel.Name = "pl_Excel"
        Me.pl_Excel.Size = New System.Drawing.Size(443, 151)
        Me.pl_Excel.TabIndex = 120
        '
        'lbl_PartNumber
        '
        Me.lbl_PartNumber.AutoSize = True
        Me.lbl_PartNumber.Location = New System.Drawing.Point(9, 79)
        Me.lbl_PartNumber.Name = "lbl_PartNumber"
        Me.lbl_PartNumber.Size = New System.Drawing.Size(69, 13)
        Me.lbl_PartNumber.TabIndex = 124
        Me.lbl_PartNumber.Text = "Part Number:"
        '
        'PN_cbox
        '
        Me.PN_cbox.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.PN_cbox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.PN_cbox.FormattingEnabled = True
        Me.PN_cbox.Items.AddRange(New Object() {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z"})
        Me.PN_cbox.Location = New System.Drawing.Point(210, 71)
        Me.PN_cbox.Name = "PN_cbox"
        Me.PN_cbox.Size = New System.Drawing.Size(47, 21)
        Me.PN_cbox.TabIndex = 123
        '
        'btnEvaluate
        '
        Me.btnEvaluate.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnEvaluate.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnEvaluate.Enabled = False
        Me.btnEvaluate.Location = New System.Drawing.Point(158, 107)
        Me.btnEvaluate.Name = "btnEvaluate"
        Me.btnEvaluate.Size = New System.Drawing.Size(99, 29)
        Me.btnEvaluate.TabIndex = 91
        Me.btnEvaluate.Text = "Read"
        Me.btnEvaluate.UseVisualStyleBackColor = False
        '
        'cboxActiveSheet
        '
        Me.cboxActiveSheet.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboxActiveSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboxActiveSheet.FormattingEnabled = True
        Me.cboxActiveSheet.Location = New System.Drawing.Point(109, 40)
        Me.cboxActiveSheet.Name = "cboxActiveSheet"
        Me.cboxActiveSheet.Size = New System.Drawing.Size(148, 21)
        Me.cboxActiveSheet.TabIndex = 122
        '
        'lbl_Worksheet
        '
        Me.lbl_Worksheet.AutoSize = True
        Me.lbl_Worksheet.Location = New System.Drawing.Point(9, 46)
        Me.lbl_Worksheet.Name = "lbl_Worksheet"
        Me.lbl_Worksheet.Size = New System.Drawing.Size(62, 13)
        Me.lbl_Worksheet.TabIndex = 121
        Me.lbl_Worksheet.Text = "Worksheet:"
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
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.Label4)
        Me.Panel3.Controls.Add(Me.btn_Browse_SwapPN)
        Me.Panel3.Controls.Add(Me.tbox_Input)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel3.Location = New System.Drawing.Point(3, 16)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(443, 67)
        Me.Panel3.TabIndex = 93
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(9, 9)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(101, 13)
        Me.Label4.TabIndex = 60
        Me.Label4.Text = "Select a BOM file ..."
        '
        'btn_Browse_SwapPN
        '
        Me.btn_Browse_SwapPN.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Browse_SwapPN.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Browse_SwapPN.Location = New System.Drawing.Point(406, 25)
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
        Me.tbox_Input.Size = New System.Drawing.Size(391, 20)
        Me.tbox_Input.TabIndex = 58
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.Panel7)
        Me.Panel2.Controls.Add(Me.Label3)
        Me.Panel2.Controls.Add(Me.lblPercentCoverage)
        Me.Panel2.Controls.Add(Me.lblPartsFound)
        Me.Panel2.Controls.Add(Me.Panel6)
        Me.Panel2.Controls.Add(Me.Panel5)
        Me.Panel2.Controls.Add(Me.lblPartsRead)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Location = New System.Drawing.Point(3, 254)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(443, 112)
        Me.Panel2.TabIndex = 92
        '
        'Panel7
        '
        Me.Panel7.BackColor = System.Drawing.Color.Orange
        Me.Panel7.Location = New System.Drawing.Point(9, 20)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(15, 15)
        Me.Panel7.TabIndex = 84
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(30, 52)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(107, 13)
        Me.Label3.TabIndex = 90
        Me.Label3.Text = "Part Numbers Found:"
        '
        'lblPercentCoverage
        '
        Me.lblPercentCoverage.AutoSize = True
        Me.lblPercentCoverage.Location = New System.Drawing.Point(163, 83)
        Me.lblPercentCoverage.Name = "lblPercentCoverage"
        Me.lblPercentCoverage.Size = New System.Drawing.Size(13, 13)
        Me.lblPercentCoverage.TabIndex = 83
        Me.lblPercentCoverage.Text = "0"
        '
        'lblPartsFound
        '
        Me.lblPartsFound.AutoSize = True
        Me.lblPartsFound.Location = New System.Drawing.Point(163, 52)
        Me.lblPartsFound.Name = "lblPartsFound"
        Me.lblPartsFound.Size = New System.Drawing.Size(13, 13)
        Me.lblPartsFound.TabIndex = 88
        Me.lblPartsFound.Text = "0"
        '
        'Panel6
        '
        Me.Panel6.BackColor = System.Drawing.Color.Yellow
        Me.Panel6.Location = New System.Drawing.Point(9, 50)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(15, 15)
        Me.Panel6.TabIndex = 85
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.Red
        Me.Panel5.Location = New System.Drawing.Point(9, 81)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(15, 15)
        Me.Panel5.TabIndex = 89
        '
        'lblPartsRead
        '
        Me.lblPartsRead.AutoSize = True
        Me.lblPartsRead.Location = New System.Drawing.Point(163, 22)
        Me.lblPartsRead.Name = "lblPartsRead"
        Me.lblPartsRead.Size = New System.Drawing.Size(13, 13)
        Me.lblPartsRead.TabIndex = 82
        Me.lblPartsRead.Text = "0"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(30, 22)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(103, 13)
        Me.Label5.TabIndex = 87
        Me.Label5.Text = "Part Numbers Read:"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(30, 83)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(64, 13)
        Me.Label6.TabIndex = 86
        Me.Label6.Text = "% Coverage"
        '
        'frmBOMCoverage
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(449, 391)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmBOMCoverage"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "BOM Coverage"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
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
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents pl_Excel As System.Windows.Forms.Panel
    Friend WithEvents lbl_Worksheet As System.Windows.Forms.Label
    Friend WithEvents chkbox_IgnoreHeader As System.Windows.Forms.CheckBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents btn_Browse_SwapPN As System.Windows.Forms.Button
    Friend WithEvents tbox_Input As System.Windows.Forms.TextBox
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btnEvaluate As System.Windows.Forms.Button
    Friend WithEvents Panel7 As System.Windows.Forms.Panel
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblPercentCoverage As System.Windows.Forms.Label
    Friend WithEvents lblPartsFound As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents lblPartsRead As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cboxActiveSheet As ComboBox
    Friend WithEvents lbl_PartNumber As System.Windows.Forms.Label
    Friend WithEvents PN_cbox As ComboBox
End Class
