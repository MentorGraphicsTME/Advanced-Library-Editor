<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmIntegrityCheck
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
        Me.lblUnusedPadstacks = New System.Windows.Forms.Label()
        Me.lblUnusedCells = New System.Windows.Forms.Label()
        Me.lblUnusedSymbols = New System.Windows.Forms.Label()
        Me.lblPartsNoSymbols = New System.Windows.Forms.Label()
        Me.lblPartsNoSymOrCell = New System.Windows.Forms.Label()
        Me.lblPartsNoCells = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnPurgeAllItems = New System.Windows.Forms.Button()
        Me.btnScanLibrary = New System.Windows.Forms.Button()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.tv_Problems = New System.Windows.Forms.TreeView()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.btn_Deselect = New System.Windows.Forms.Button()
        Me.btn_Select = New System.Windows.Forms.Button()
        Me.gbInfo = New System.Windows.Forms.GroupBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.StatusStrip1.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.gbInfo.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblUnusedPadstacks
        '
        Me.lblUnusedPadstacks.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUnusedPadstacks.Location = New System.Drawing.Point(146, 113)
        Me.lblUnusedPadstacks.Name = "lblUnusedPadstacks"
        Me.lblUnusedPadstacks.Size = New System.Drawing.Size(50, 13)
        Me.lblUnusedPadstacks.TabIndex = 1
        Me.lblUnusedPadstacks.Text = "0"
        Me.lblUnusedPadstacks.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblUnusedCells
        '
        Me.lblUnusedCells.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUnusedCells.Location = New System.Drawing.Point(146, 94)
        Me.lblUnusedCells.Name = "lblUnusedCells"
        Me.lblUnusedCells.Size = New System.Drawing.Size(50, 13)
        Me.lblUnusedCells.TabIndex = 1
        Me.lblUnusedCells.Text = "0"
        Me.lblUnusedCells.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblUnusedSymbols
        '
        Me.lblUnusedSymbols.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblUnusedSymbols.Location = New System.Drawing.Point(146, 75)
        Me.lblUnusedSymbols.Name = "lblUnusedSymbols"
        Me.lblUnusedSymbols.Size = New System.Drawing.Size(50, 13)
        Me.lblUnusedSymbols.TabIndex = 1
        Me.lblUnusedSymbols.Text = "0"
        Me.lblUnusedSymbols.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPartsNoSymbols
        '
        Me.lblPartsNoSymbols.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPartsNoSymbols.Location = New System.Drawing.Point(146, 37)
        Me.lblPartsNoSymbols.Name = "lblPartsNoSymbols"
        Me.lblPartsNoSymbols.Size = New System.Drawing.Size(50, 13)
        Me.lblPartsNoSymbols.TabIndex = 1
        Me.lblPartsNoSymbols.Text = "0"
        Me.lblPartsNoSymbols.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPartsNoSymOrCell
        '
        Me.lblPartsNoSymOrCell.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPartsNoSymOrCell.Location = New System.Drawing.Point(146, 56)
        Me.lblPartsNoSymOrCell.Name = "lblPartsNoSymOrCell"
        Me.lblPartsNoSymOrCell.Size = New System.Drawing.Size(50, 13)
        Me.lblPartsNoSymOrCell.TabIndex = 1
        Me.lblPartsNoSymOrCell.Text = "0"
        Me.lblPartsNoSymOrCell.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblPartsNoCells
        '
        Me.lblPartsNoCells.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblPartsNoCells.Location = New System.Drawing.Point(146, 18)
        Me.lblPartsNoCells.Name = "lblPartsNoCells"
        Me.lblPartsNoCells.Size = New System.Drawing.Size(50, 13)
        Me.lblPartsNoCells.TabIndex = 1
        Me.lblPartsNoCells.Text = "0"
        Me.lblPartsNoCells.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(12, 113)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(98, 13)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Orphan Padstacks:"
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(12, 94)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(70, 13)
        Me.Label9.TabIndex = 1
        Me.Label9.Text = "Orphan Cells:"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(12, 75)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(87, 13)
        Me.Label7.TabIndex = 1
        Me.Label7.Text = "Orphan Symbols:"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(12, 37)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(98, 13)
        Me.Label5.TabIndex = 1
        Me.Label5.Text = "Parts w/o Symbols:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(12, 56)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 1
        Me.Label3.Text = "Orphan Parts"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(81, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Parts w/o Cells:"
        '
        'btnPurgeAllItems
        '
        Me.btnPurgeAllItems.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btnPurgeAllItems.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnPurgeAllItems.Enabled = False
        Me.btnPurgeAllItems.Location = New System.Drawing.Point(118, 9)
        Me.btnPurgeAllItems.Name = "btnPurgeAllItems"
        Me.btnPurgeAllItems.Size = New System.Drawing.Size(111, 55)
        Me.btnPurgeAllItems.TabIndex = 0
        Me.btnPurgeAllItems.Text = "Delete Selected Items"
        Me.btnPurgeAllItems.UseVisualStyleBackColor = False
        '
        'btnScanLibrary
        '
        Me.btnScanLibrary.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnScanLibrary.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btnScanLibrary.Location = New System.Drawing.Point(102, 148)
        Me.btnScanLibrary.Name = "btnScanLibrary"
        Me.btnScanLibrary.Size = New System.Drawing.Size(94, 39)
        Me.btnScanLibrary.TabIndex = 0
        Me.btnScanLibrary.Text = "Scan Library"
        Me.btnScanLibrary.UseVisualStyleBackColor = False
        '
        'StatusStrip1
        '
        Me.StatusStrip1.BackColor = System.Drawing.SystemColors.Menu
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 533)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.Size = New System.Drawing.Size(462, 22)
        Me.StatusStrip1.TabIndex = 1
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(377, 17)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Scan Library to begin..."
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
        'tv_Problems
        '
        Me.tv_Problems.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.tv_Problems.CheckBoxes = True
        Me.tv_Problems.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tv_Problems.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll
        Me.tv_Problems.Location = New System.Drawing.Point(3, 16)
        Me.tv_Problems.Name = "tv_Problems"
        Me.tv_Problems.Size = New System.Drawing.Size(246, 441)
        Me.tv_Problems.TabIndex = 0
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.btn_Deselect)
        Me.Panel2.Controls.Add(Me.btn_Select)
        Me.Panel2.Controls.Add(Me.btnPurgeAllItems)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel2.Enabled = False
        Me.Panel2.Location = New System.Drawing.Point(3, 457)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(246, 73)
        Me.Panel2.TabIndex = 4
        '
        'btn_Deselect
        '
        Me.btn_Deselect.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_Deselect.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Deselect.Location = New System.Drawing.Point(17, 41)
        Me.btn_Deselect.Name = "btn_Deselect"
        Me.btn_Deselect.Size = New System.Drawing.Size(95, 23)
        Me.btn_Deselect.TabIndex = 2
        Me.btn_Deselect.Text = "Deselect All"
        Me.btn_Deselect.UseVisualStyleBackColor = False
        '
        'btn_Select
        '
        Me.btn_Select.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.btn_Select.BackColor = System.Drawing.SystemColors.ButtonFace
        Me.btn_Select.Location = New System.Drawing.Point(17, 9)
        Me.btn_Select.Name = "btn_Select"
        Me.btn_Select.Size = New System.Drawing.Size(95, 23)
        Me.btn_Select.TabIndex = 1
        Me.btn_Select.Text = "Select All"
        Me.btn_Select.UseVisualStyleBackColor = False
        '
        'gbInfo
        '
        Me.gbInfo.Controls.Add(Me.tv_Problems)
        Me.gbInfo.Controls.Add(Me.Panel2)
        Me.gbInfo.Dock = System.Windows.Forms.DockStyle.Fill
        Me.gbInfo.Location = New System.Drawing.Point(210, 0)
        Me.gbInfo.Name = "gbInfo"
        Me.gbInfo.Size = New System.Drawing.Size(252, 533)
        Me.gbInfo.TabIndex = 6
        Me.gbInfo.TabStop = False
        Me.gbInfo.Text = "Info"
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.lblUnusedPadstacks)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.btnScanLibrary)
        Me.Panel1.Controls.Add(Me.Label7)
        Me.Panel1.Controls.Add(Me.lblUnusedCells)
        Me.Panel1.Controls.Add(Me.Label9)
        Me.Panel1.Controls.Add(Me.Label5)
        Me.Panel1.Controls.Add(Me.lblUnusedSymbols)
        Me.Panel1.Controls.Add(Me.Label2)
        Me.Panel1.Controls.Add(Me.Label3)
        Me.Panel1.Controls.Add(Me.lblPartsNoSymbols)
        Me.Panel1.Controls.Add(Me.lblPartsNoCells)
        Me.Panel1.Controls.Add(Me.lblPartsNoSymOrCell)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(210, 533)
        Me.Panel1.TabIndex = 3
        '
        'frmIntegrityCheck
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(462, 555)
        Me.Controls.Add(Me.gbInfo)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.StatusStrip1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow
        Me.MinimumSize = New System.Drawing.Size(264, 370)
        Me.Name = "frmIntegrityCheck"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "LMC Integrity Check"
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.gbInfo.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblUnusedCells As System.Windows.Forms.Label
    Friend WithEvents lblUnusedSymbols As System.Windows.Forms.Label
    Friend WithEvents lblPartsNoSymbols As System.Windows.Forms.Label
    Friend WithEvents lblPartsNoSymOrCell As System.Windows.Forms.Label
    Friend WithEvents lblPartsNoCells As System.Windows.Forms.Label
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnPurgeAllItems As System.Windows.Forms.Button
    Friend WithEvents lblUnusedPadstacks As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnScanLibrary As System.Windows.Forms.Button
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents tv_Problems As System.Windows.Forms.TreeView
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents btn_Deselect As System.Windows.Forms.Button
    Friend WithEvents btn_Select As System.Windows.Forms.Button
    Friend WithEvents gbInfo As System.Windows.Forms.GroupBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
End Class
