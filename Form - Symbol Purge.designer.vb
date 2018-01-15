<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSymPurge
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
        Me.chklist_SymExt = New System.Windows.Forms.CheckedListBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.btnPurge = New System.Windows.Forms.Button()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chklist_SymExt
        '
        Me.chklist_SymExt.CheckOnClick = True
        Me.chklist_SymExt.Dock = System.Windows.Forms.DockStyle.Fill
        Me.chklist_SymExt.FormattingEnabled = True
        Me.chklist_SymExt.Items.AddRange(New Object() {".1", ".2", ".3", ".4", ".5", ".6", ".7", ".8", ".9", ".10"})
        Me.chklist_SymExt.Location = New System.Drawing.Point(0, 0)
        Me.chklist_SymExt.MultiColumn = True
        Me.chklist_SymExt.Name = "chklist_SymExt"
        Me.chklist_SymExt.Size = New System.Drawing.Size(137, 164)
        Me.chklist_SymExt.TabIndex = 0
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.btnPurge)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 164)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(137, 61)
        Me.Panel1.TabIndex = 1
        '
        'btnPurge
        '
        Me.btnPurge.Location = New System.Drawing.Point(31, 19)
        Me.btnPurge.Name = "btnPurge"
        Me.btnPurge.Size = New System.Drawing.Size(75, 23)
        Me.btnPurge.TabIndex = 0
        Me.btnPurge.Text = "Purge"
        Me.btnPurge.UseVisualStyleBackColor = True
        '
        'frmSymPurge
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(137, 225)
        Me.Controls.Add(Me.chklist_SymExt)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmSymPurge"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Symbol Purge"
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chklist_SymExt As System.Windows.Forms.CheckedListBox
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents btnPurge As System.Windows.Forms.Button
End Class
