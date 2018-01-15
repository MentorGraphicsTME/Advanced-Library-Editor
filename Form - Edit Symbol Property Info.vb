Public Class frmEdit_Property_Info

    Property oParentNode As TreeNode
    Property argsInfo As New ReturnValue
    Dim bChangesMade As Boolean = False
    Property bNewProperty As Boolean = False

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        If bNewProperty = True Then

            If cbox_Font.SelectedIndex = -1 Then
                MessageBox.Show("Please provide a property Font Style.")
                cbox_Font.Select()
                Exit Sub
            End If

            If cbox_Font.SelectedIndex = -1 Then
                MessageBox.Show("Please provide a property Font Size.")
                cbox_Size.Select()
                Exit Sub
            End If

            If argsInfo.AutomaticColor = False And String.IsNullOrEmpty(btnColor.Text) Then
                MessageBox.Show("Please provide a property Font color.")
                btnColor.Select()
                Exit Sub
            End If

            If cbox_Alignment.SelectedIndex = -1 Then
                MessageBox.Show("Please provide a property alignment value.")
                cbox_Alignment.Select()
                Exit Sub
            End If

            If cbox_Visibility.SelectedIndex = -1 Then
                MessageBox.Show("Please define the property visibility.")
                cbox_Visibility.Select()
                Exit Sub
            End If

        End If

        'If bChangesMade = True Then
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        'Else
        '    Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        'End If

        Close()

        'RaiseEvent evReturnValue(Me, argsInfo)

    End Sub

    Private Sub frmEdit_Property_Info_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Me.Location = New Point(Parent.Width / 2 - Me.Width / 2, Parent.Height / 2 - Me.Height / 2)
        Me.Text = Me.Text & " - " & oParentNode.Text

        'For Each oChildNode As TreeNode In oParentNode.Nodes

        '    Select Case oChildNode.Name

        '        Case "Font"
        '            cbox_Font.Text = oChildNode.Text
        '            btn_FontClear.Enabled = True
        '        Case "Size"
        '            cbox_Size.Text = oChildNode.Text
        '            btn_SizeClear.Enabled = True
        '        Case "Visibility"
        '            cbox_Visibility.Text = oChildNode.Text
        '            btn_VisibilityClear.Enabled = True
        '        Case "Alignment"
        '            cbox_Alignment.Text = oChildNode.Text
        '            btn_AlignmentClear.Enabled = True

        '    End Select

        'Next

    End Sub

    Private Sub btnColor_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnColor.Click

        If ColorDialog.ShowDialog = System.Windows.Forms.DialogResult.OK Then

            btnColor.Text = "R:" & ColorDialog.Color.R & " G:" & ColorDialog.Color.G & " B:" & ColorDialog.Color.B
            btnColor.BackColor = Color.FromArgb(ColorDialog.Color.R, ColorDialog.Color.G, ColorDialog.Color.B)

            argsInfo.Color = ColorDialog.Color
            btn_ColorClear.Visible = True

        End If

    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()

    End Sub

    Private Sub chkbox_Automatic_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_Automatic.CheckedChanged

        If chkbox_Automatic.Checked = True Then
            btnColor.Text = "Automatic"
            argsInfo.AutomaticColor = True
            argsInfo.Color = Nothing
            btn_ColorClear.Visible = False
        Else
            argsInfo.AutomaticColor = False
            btnColor.Text = String.Empty
        End If

        btnColor.BackColor = System.Drawing.SystemColors.Window

    End Sub

    Private Sub cbox_Font_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbox_Font.SelectedIndexChanged

        If Not cbox_Font.SelectedIndex = -1 Then
            argsInfo.Font = cbox_Font.Text
            btn_FontClear.Visible = True
        End If

    End Sub

    Private Sub cbox_Size_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbox_Size.SelectedIndexChanged

        If Not cbox_Size.SelectedIndex = -1 Then
            argsInfo.Size = cbox_Size.Text
            btn_SizeClear.Visible = True
        End If

    End Sub

    Private Sub cbox_Alignment_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbox_Alignment.SelectedIndexChanged
        If Not cbox_Alignment.SelectedIndex = -1 Then
            argsInfo.Alignment = cbox_Alignment.Text
            btn_AlignmentClear.Visible = True
        End If
    End Sub

    Private Sub cbox_Visibility_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbox_Visibility.SelectedIndexChanged
        If Not cbox_Visibility.SelectedIndex = -1 Then
            argsInfo.Visibility = cbox_Visibility.Text
            btn_VisibilityClear.Visible = True
        End If
    End Sub

    Private Sub btn_FontClear_Click(sender As System.Object, e As System.EventArgs) Handles btn_FontClear.Click
        cbox_Font.SelectedIndex = -1
        btn_FontClear.Visible = False
        argsInfo.Font = Nothing
    End Sub

    Private Sub btn_SizeClear_Click(sender As System.Object, e As System.EventArgs) Handles btn_SizeClear.Click
        cbox_Size.SelectedIndex = -1
        btn_SizeClear.Visible = False
        argsInfo.Size = Nothing
    End Sub

    Private Sub btn_ColorClear_Click(sender As System.Object, e As System.EventArgs) Handles btn_ColorClear.Click
        btnColor.Text = ""
        btn_ColorClear.Visible = False
        argsInfo.Color = Nothing
        btnColor.BackColor = System.Drawing.SystemColors.Window

    End Sub

    Private Sub btn_AlignmentClear_Click(sender As System.Object, e As System.EventArgs) Handles btn_AlignmentClear.Click
        cbox_Alignment.SelectedIndex = -1
        btn_AlignmentClear.Visible = False
        argsInfo.Alignment = Nothing
    End Sub

    Private Sub btn_VisibilityClear_Click(sender As System.Object, e As System.EventArgs) Handles btn_VisibilityClear.Click
        cbox_Visibility.SelectedIndex = -1
        btn_VisibilityClear.Visible = False
        argsInfo.Visibility = Nothing
    End Sub
End Class

Public Class ReturnValue
    Inherits System.EventArgs
    Private _Font As String
    Private _Size As Integer
    Private _Color As Color = Nothing
    Private _Alignment As String
    Private _Visibility As String
    Private _AutomaticColor As Boolean = False

    Public Property Font() As String
        Get
            Return _Font
        End Get
        Set(ByVal value As String)
            _Font = value
        End Set
    End Property

    Public Property Size() As Integer
        Get
            Return _Size
        End Get
        Set(ByVal value As Integer)
            _Size = value
        End Set
    End Property

    Public Property Color() As System.Drawing.Color
        Get
            Return _Color
        End Get
        Set(ByVal value As System.Drawing.Color)
            _Color = value
        End Set
    End Property
    Public Property Visibility() As String
        Get
            Return _Visibility
        End Get
        Set(ByVal value As String)
            _Visibility = value
        End Set
    End Property
    Public Property Alignment() As String
        Get
            Return _Alignment
        End Get
        Set(ByVal value As String)
            _Alignment = value
        End Set
    End Property
    Public Property AutomaticColor() As Boolean
        Get
            Return _AutomaticColor
        End Get
        Set(ByVal value As Boolean)
            _AutomaticColor = value
        End Set
    End Property

End Class