<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.MenuStrip = New System.Windows.Forms.MenuStrip()
        Me.tsm_File = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_OpenLMC = New System.Windows.Forms.ToolStripMenuItem()
        Me.tsm_BrowseLMC = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsm_ConnectLMC = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tsm_Exit = New System.Windows.Forms.ToolStripMenuItem()
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.StatusStrip1 = New System.Windows.Forms.StatusStrip()
        Me.ts_Status = New System.Windows.Forms.ToolStripStatusLabel()
        Me.WaitGif = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ts_Parts = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ts_Symbols = New System.Windows.Forms.ToolStripStatusLabel()
        Me.ts_Cells = New System.Windows.Forms.ToolStripStatusLabel()
        Me.NotifyIcon = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.MenuStrip.SuspendLayout()
        Me.StatusStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip
        '
        Me.MenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsm_File, Me.AboutToolStripMenuItem})
        Me.MenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip.MdiWindowListItem = Me.tsm_File
        Me.MenuStrip.Name = "MenuStrip"
        Me.MenuStrip.Padding = New System.Windows.Forms.Padding(4, 2, 0, 2)
        Me.MenuStrip.Size = New System.Drawing.Size(788, 24)
        Me.MenuStrip.TabIndex = 11
        Me.MenuStrip.Text = "MenuStrip1"
        '
        'tsm_File
        '
        Me.tsm_File.Checked = True
        Me.tsm_File.CheckState = System.Windows.Forms.CheckState.Checked
        Me.tsm_File.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsm_OpenLMC, Me.tsm_ConnectLMC, Me.ToolStripSeparator1, Me.tsm_Exit})
        Me.tsm_File.Name = "tsm_File"
        Me.tsm_File.Size = New System.Drawing.Size(37, 20)
        Me.tsm_File.Text = "&File"
        '
        'tsm_OpenLMC
        '
        Me.tsm_OpenLMC.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsm_BrowseLMC, Me.ToolStripSeparator2})
        Me.tsm_OpenLMC.Name = "tsm_OpenLMC"
        Me.tsm_OpenLMC.Size = New System.Drawing.Size(161, 22)
        Me.tsm_OpenLMC.Text = "&Open LMC"
        '
        'tsm_BrowseLMC
        '
        Me.tsm_BrowseLMC.Name = "tsm_BrowseLMC"
        Me.tsm_BrowseLMC.Size = New System.Drawing.Size(121, 22)
        Me.tsm_BrowseLMC.Text = "Browse..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(118, 6)
        '
        'tsm_ConnectLMC
        '
        Me.tsm_ConnectLMC.Name = "tsm_ConnectLMC"
        Me.tsm_ConnectLMC.Size = New System.Drawing.Size(161, 22)
        Me.tsm_ConnectLMC.Text = "&Connect to LMC"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(158, 6)
        '
        'tsm_Exit
        '
        Me.tsm_Exit.Name = "tsm_Exit"
        Me.tsm_Exit.Size = New System.Drawing.Size(161, 22)
        Me.tsm_Exit.Text = "E&xit"
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(52, 20)
        Me.AboutToolStripMenuItem.Text = "About"
        '
        'StatusStrip1
        '
        Me.StatusStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ts_Status, Me.WaitGif, Me.ts_Parts, Me.ts_Symbols, Me.ts_Cells})
        Me.StatusStrip1.Location = New System.Drawing.Point(0, 539)
        Me.StatusStrip1.Name = "StatusStrip1"
        Me.StatusStrip1.ShowItemToolTips = True
        Me.StatusStrip1.Size = New System.Drawing.Size(788, 24)
        Me.StatusStrip1.TabIndex = 14
        Me.StatusStrip1.Text = "StatusStrip1"
        '
        'ts_Status
        '
        Me.ts_Status.BackColor = System.Drawing.Color.Transparent
        Me.ts_Status.Name = "ts_Status"
        Me.ts_Status.Size = New System.Drawing.Size(513, 19)
        Me.ts_Status.Spring = True
        Me.ts_Status.Text = "Click File - Open LMC to open a central library"
        Me.ts_Status.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'WaitGif
        '
        Me.WaitGif.BackColor = System.Drawing.Color.Transparent
        Me.WaitGif.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.WaitGif.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.WaitGif.Enabled = False
        Me.WaitGif.Image = Global.Advanced_Library_Editor.My.Resources.Resources.waitbar
        Me.WaitGif.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.WaitGif.Name = "WaitGif"
        Me.WaitGif.Padding = New System.Windows.Forms.Padding(0, 0, 10, 0)
        Me.WaitGif.Size = New System.Drawing.Size(84, 19)
        Me.WaitGif.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ts_Parts
        '
        Me.ts_Parts.BackColor = System.Drawing.Color.Transparent
        Me.ts_Parts.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ts_Parts.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.ts_Parts.Enabled = False
        Me.ts_Parts.Name = "ts_Parts"
        Me.ts_Parts.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.ts_Parts.Size = New System.Drawing.Size(54, 19)
        Me.ts_Parts.Text = "Parts: 0"
        '
        'ts_Symbols
        '
        Me.ts_Symbols.BackColor = System.Drawing.Color.Transparent
        Me.ts_Symbols.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Right
        Me.ts_Symbols.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.ts_Symbols.Enabled = False
        Me.ts_Symbols.Name = "ts_Symbols"
        Me.ts_Symbols.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.ts_Symbols.Size = New System.Drawing.Size(73, 19)
        Me.ts_Symbols.Text = "Symbols: 0"
        '
        'ts_Cells
        '
        Me.ts_Cells.BackColor = System.Drawing.Color.Transparent
        Me.ts_Cells.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.ts_Cells.Enabled = False
        Me.ts_Cells.Name = "ts_Cells"
        Me.ts_Cells.Padding = New System.Windows.Forms.Padding(0, 0, 5, 0)
        Me.ts_Cells.Size = New System.Drawing.Size(49, 19)
        Me.ts_Cells.Text = "Cells: 0"
        '
        'NotifyIcon
        '
        Me.NotifyIcon.Icon = CType(resources.GetObject("NotifyIcon.Icon"), System.Drawing.Icon)
        Me.NotifyIcon.Text = "Advanced Library Editor"
        Me.NotifyIcon.Visible = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Window
        Me.ClientSize = New System.Drawing.Size(788, 563)
        Me.Controls.Add(Me.StatusStrip1)
        Me.Controls.Add(Me.MenuStrip)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.Name = "frmMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Advanced Library Editor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip.ResumeLayout(False)
        Me.MenuStrip.PerformLayout()
        Me.StatusStrip1.ResumeLayout(False)
        Me.StatusStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents tsm_File As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_OpenLMC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents tsm_BrowseLMC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsm_ConnectLMC As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tsm_Exit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents StatusStrip1 As System.Windows.Forms.StatusStrip
    Friend WithEvents ts_Status As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents WaitGif As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ts_Parts As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ts_Symbols As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ts_Cells As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents NotifyIcon As System.Windows.Forms.NotifyIcon
End Class
