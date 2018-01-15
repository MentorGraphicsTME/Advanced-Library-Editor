<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExportCellInfo
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
        Me.btn_Export = New System.Windows.Forms.Button()
        Me.chkbox_CloseXLS = New System.Windows.Forms.CheckBox()
        Me.gb_General = New System.Windows.Forms.GroupBox()
        Me.chkbox_UserLayers = New System.Windows.Forms.CheckBox()
        Me.chkbox_LastModified = New System.Windows.Forms.CheckBox()
        Me.chkbox_PackageType = New System.Windows.Forms.CheckBox()
        Me.chkbox_Height = New System.Windows.Forms.CheckBox()
        Me.chkbox_Units = New System.Windows.Forms.CheckBox()
        Me.chkbox_MaxCompSize = New System.Windows.Forms.CheckBox()
        Me.chkbox_PinCount = New System.Windows.Forms.CheckBox()
        Me.chkbox_PinInfo = New System.Windows.Forms.CheckBox()
        Me.Chkbox_Description = New System.Windows.Forms.CheckBox()
        Me.chkbox_Cellname = New System.Windows.Forms.CheckBox()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.gb_Options = New System.Windows.Forms.GroupBox()
        Me.chkbox_IndividualWorkbook = New System.Windows.Forms.CheckBox()
        Me.GroupBox2 = New System.Windows.Forms.GroupBox()
        Me.tv_Cells = New System.Windows.Forms.TreeView()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.gb_General.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.gb_Options.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btn_Export
        '
        Me.btn_Export.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btn_Export.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Export.Location = New System.Drawing.Point(100, 77)
        Me.btn_Export.Name = "btn_Export"
        Me.btn_Export.Size = New System.Drawing.Size(88, 26)
        Me.btn_Export.TabIndex = 0
        Me.btn_Export.Text = "Export"
        Me.btn_Export.UseVisualStyleBackColor = False
        '
        'chkbox_CloseXLS
        '
        Me.chkbox_CloseXLS.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkbox_CloseXLS.AutoSize = True
        Me.chkbox_CloseXLS.Location = New System.Drawing.Point(18, 46)
        Me.chkbox_CloseXLS.Name = "chkbox_CloseXLS"
        Me.chkbox_CloseXLS.Size = New System.Drawing.Size(134, 17)
        Me.chkbox_CloseXLS.TabIndex = 1
        Me.chkbox_CloseXLS.Text = "Close Excel Workbook"
        Me.chkbox_CloseXLS.UseVisualStyleBackColor = True
        '
        'gb_General
        '
        Me.gb_General.Controls.Add(Me.chkbox_UserLayers)
        Me.gb_General.Controls.Add(Me.chkbox_LastModified)
        Me.gb_General.Controls.Add(Me.chkbox_PackageType)
        Me.gb_General.Controls.Add(Me.chkbox_Height)
        Me.gb_General.Controls.Add(Me.chkbox_Units)
        Me.gb_General.Controls.Add(Me.chkbox_MaxCompSize)
        Me.gb_General.Controls.Add(Me.chkbox_PinCount)
        Me.gb_General.Controls.Add(Me.chkbox_PinInfo)
        Me.gb_General.Controls.Add(Me.Chkbox_Description)
        Me.gb_General.Controls.Add(Me.chkbox_Cellname)
        Me.gb_General.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gb_General.Location = New System.Drawing.Point(0, 0)
        Me.gb_General.Name = "gb_General"
        Me.gb_General.Size = New System.Drawing.Size(200, 346)
        Me.gb_General.TabIndex = 4
        Me.gb_General.TabStop = False
        Me.gb_General.Text = "General"
        '
        'chkbox_UserLayers
        '
        Me.chkbox_UserLayers.AutoSize = True
        Me.chkbox_UserLayers.Checked = True
        Me.chkbox_UserLayers.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_UserLayers.Location = New System.Drawing.Point(18, 229)
        Me.chkbox_UserLayers.Name = "chkbox_UserLayers"
        Me.chkbox_UserLayers.Size = New System.Drawing.Size(82, 17)
        Me.chkbox_UserLayers.TabIndex = 4
        Me.chkbox_UserLayers.Text = "User Layers"
        Me.chkbox_UserLayers.UseVisualStyleBackColor = True
        '
        'chkbox_LastModified
        '
        Me.chkbox_LastModified.AutoSize = True
        Me.chkbox_LastModified.Checked = True
        Me.chkbox_LastModified.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_LastModified.Location = New System.Drawing.Point(18, 90)
        Me.chkbox_LastModified.Name = "chkbox_LastModified"
        Me.chkbox_LastModified.Size = New System.Drawing.Size(89, 17)
        Me.chkbox_LastModified.TabIndex = 0
        Me.chkbox_LastModified.Text = "Last Modified"
        Me.chkbox_LastModified.UseVisualStyleBackColor = True
        '
        'chkbox_PackageType
        '
        Me.chkbox_PackageType.AutoSize = True
        Me.chkbox_PackageType.Checked = True
        Me.chkbox_PackageType.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_PackageType.Location = New System.Drawing.Point(18, 67)
        Me.chkbox_PackageType.Name = "chkbox_PackageType"
        Me.chkbox_PackageType.Size = New System.Drawing.Size(96, 17)
        Me.chkbox_PackageType.TabIndex = 0
        Me.chkbox_PackageType.Text = "Package Type"
        Me.chkbox_PackageType.UseVisualStyleBackColor = True
        '
        'chkbox_Height
        '
        Me.chkbox_Height.AutoSize = True
        Me.chkbox_Height.Checked = True
        Me.chkbox_Height.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Height.Location = New System.Drawing.Point(18, 113)
        Me.chkbox_Height.Name = "chkbox_Height"
        Me.chkbox_Height.Size = New System.Drawing.Size(57, 17)
        Me.chkbox_Height.TabIndex = 0
        Me.chkbox_Height.Text = "Height"
        Me.chkbox_Height.UseVisualStyleBackColor = True
        '
        'chkbox_Units
        '
        Me.chkbox_Units.AutoSize = True
        Me.chkbox_Units.Checked = True
        Me.chkbox_Units.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Units.Location = New System.Drawing.Point(18, 137)
        Me.chkbox_Units.Name = "chkbox_Units"
        Me.chkbox_Units.Size = New System.Drawing.Size(50, 17)
        Me.chkbox_Units.TabIndex = 0
        Me.chkbox_Units.Text = "Units"
        Me.chkbox_Units.UseVisualStyleBackColor = True
        '
        'chkbox_MaxCompSize
        '
        Me.chkbox_MaxCompSize.AutoSize = True
        Me.chkbox_MaxCompSize.Checked = True
        Me.chkbox_MaxCompSize.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_MaxCompSize.Location = New System.Drawing.Point(18, 206)
        Me.chkbox_MaxCompSize.Name = "chkbox_MaxCompSize"
        Me.chkbox_MaxCompSize.Size = New System.Drawing.Size(126, 17)
        Me.chkbox_MaxCompSize.TabIndex = 0
        Me.chkbox_MaxCompSize.Text = "Max Component Size"
        Me.chkbox_MaxCompSize.UseVisualStyleBackColor = True
        '
        'chkbox_PinCount
        '
        Me.chkbox_PinCount.AutoSize = True
        Me.chkbox_PinCount.Checked = True
        Me.chkbox_PinCount.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_PinCount.Location = New System.Drawing.Point(18, 44)
        Me.chkbox_PinCount.Name = "chkbox_PinCount"
        Me.chkbox_PinCount.Size = New System.Drawing.Size(72, 17)
        Me.chkbox_PinCount.TabIndex = 0
        Me.chkbox_PinCount.Text = "Pin Count"
        Me.chkbox_PinCount.UseVisualStyleBackColor = True
        '
        'chkbox_PinInfo
        '
        Me.chkbox_PinInfo.AutoSize = True
        Me.chkbox_PinInfo.Checked = True
        Me.chkbox_PinInfo.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_PinInfo.Location = New System.Drawing.Point(18, 183)
        Me.chkbox_PinInfo.Name = "chkbox_PinInfo"
        Me.chkbox_PinInfo.Size = New System.Drawing.Size(62, 17)
        Me.chkbox_PinInfo.TabIndex = 0
        Me.chkbox_PinInfo.Text = "Pin Info"
        Me.chkbox_PinInfo.UseVisualStyleBackColor = True
        '
        'Chkbox_Description
        '
        Me.Chkbox_Description.AutoSize = True
        Me.Chkbox_Description.Checked = True
        Me.Chkbox_Description.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Chkbox_Description.Location = New System.Drawing.Point(18, 160)
        Me.Chkbox_Description.Name = "Chkbox_Description"
        Me.Chkbox_Description.Size = New System.Drawing.Size(79, 17)
        Me.Chkbox_Description.TabIndex = 0
        Me.Chkbox_Description.Text = "Description"
        Me.Chkbox_Description.UseVisualStyleBackColor = True
        '
        'chkbox_Cellname
        '
        Me.chkbox_Cellname.AutoSize = True
        Me.chkbox_Cellname.Checked = True
        Me.chkbox_Cellname.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkbox_Cellname.Location = New System.Drawing.Point(18, 20)
        Me.chkbox_Cellname.Name = "chkbox_Cellname"
        Me.chkbox_Cellname.Size = New System.Drawing.Size(54, 17)
        Me.chkbox_Cellname.TabIndex = 0
        Me.chkbox_Cellname.Text = "Name"
        Me.chkbox_Cellname.UseVisualStyleBackColor = True
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.gb_General)
        Me.Panel3.Controls.Add(Me.gb_Options)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel3.Location = New System.Drawing.Point(251, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(200, 460)
        Me.Panel3.TabIndex = 5
        '
        'gb_Options
        '
        Me.gb_Options.Controls.Add(Me.chkbox_CloseXLS)
        Me.gb_Options.Controls.Add(Me.btn_Export)
        Me.gb_Options.Controls.Add(Me.chkbox_IndividualWorkbook)
        Me.gb_Options.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.gb_Options.Location = New System.Drawing.Point(0, 346)
        Me.gb_Options.Name = "gb_Options"
        Me.gb_Options.Size = New System.Drawing.Size(200, 114)
        Me.gb_Options.TabIndex = 5
        Me.gb_Options.TabStop = False
        Me.gb_Options.Text = "Options"
        '
        'chkbox_IndividualWorkbook
        '
        Me.chkbox_IndividualWorkbook.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkbox_IndividualWorkbook.AutoSize = True
        Me.chkbox_IndividualWorkbook.Location = New System.Drawing.Point(18, 23)
        Me.chkbox_IndividualWorkbook.Name = "chkbox_IndividualWorkbook"
        Me.chkbox_IndividualWorkbook.Size = New System.Drawing.Size(165, 17)
        Me.chkbox_IndividualWorkbook.TabIndex = 1
        Me.chkbox_IndividualWorkbook.Text = "Create one workbook per cell"
        Me.chkbox_IndividualWorkbook.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.tv_Cells)
        Me.GroupBox2.Dock = System.Windows.Forms.DockStyle.Fill
        Me.GroupBox2.Location = New System.Drawing.Point(0, 0)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(251, 460)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Partitions/Cells"
        '
        'tv_Cells
        '
        Me.tv_Cells.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv_Cells.CheckBoxes = True
        Me.tv_Cells.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Cells.Location = New System.Drawing.Point(3, 16)
        Me.tv_Cells.Name = "tv_Cells"
        Me.tv_Cells.Size = New System.Drawing.Size(245, 441)
        Me.tv_Cells.TabIndex = 0
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 460)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(451, 22)
        Me.StatusStrip1.TabIndex = 8
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(366, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Select some cells to begin..."
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
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
        'frmExportCellInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(451, 482)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.Panel3)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MinimumSize = New System.Drawing.Size(467, 520)
        Me.Name = "frmExportCellInfo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Export Cell Info to Excel"
        Me.gb_General.ResumeLayout(False)
        Me.gb_General.PerformLayout()
        Me.Panel3.ResumeLayout(False)
        Me.gb_Options.ResumeLayout(False)
        Me.gb_Options.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btn_Export As System.Windows.Forms.Button
    Friend WithEvents gb_General As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_MaxCompSize As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_PinCount As System.Windows.Forms.CheckBox
    Friend WithEvents Chkbox_Description As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_Cellname As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_PackageType As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_Height As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_Units As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_LastModified As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_PinInfo As System.Windows.Forms.CheckBox
    Friend WithEvents chkbox_CloseXLS As System.Windows.Forms.CheckBox
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents chkbox_IndividualWorkbook As System.Windows.Forms.CheckBox
    Friend WithEvents tv_Cells As System.Windows.Forms.TreeView
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents chkbox_UserLayers As System.Windows.Forms.CheckBox
    Friend WithEvents gb_Options As System.Windows.Forms.GroupBox
End Class
