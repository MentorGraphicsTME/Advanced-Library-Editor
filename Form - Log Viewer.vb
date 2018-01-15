Imports System.IO
Imports System.Drawing

Public Class frmLogViewer

#Region "Public Fields + Properties + Events + Delegates + Enums"

    'Property LogPath As String
    Property LogFile As String = String.Empty

    Shared Property LogPath As String

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    Dim dicLogFiles As New Dictionary(Of String, String)
    Dim indexOfSearchText As Integer = 0
    Dim kvp_Errors As New KeyValuePair(Of String, Color)("Error", Color.Red)
    Dim kvp_Warnings As New KeyValuePair(Of String, Color)("Warning", Color.Orange)
    Dim LastModified As Date
    Dim SearchIndex As New List(Of Integer)

#End Region

#Region "Private Methods"

    Private Sub btn_LogFileFolder_Click(sender As System.Object, e As System.EventArgs) Handles btn_LogFileFolder.Click

        Process.Start("explorer.exe", Path.GetDirectoryName(LogPath))

    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs)
        indexOfSearchText -= 1

        If indexOfSearchText < 1 Then
            indexOfSearchText = SearchIndex.Count - 1
        End If

        rtb_Log.Select(SearchIndex(indexOfSearchText), 0)
        rtb_Log.ScrollToCaret()
    End Sub

    'End Sub
    Private Sub Button4_Click(sender As System.Object, e As System.EventArgs) Handles btnOpenWith.Click
        Process.Start("rundll32.exe", "shell32.dll, OpenAs_RunDLL " & dicLogFiles.Item(lbox_LogFiles.SelectedValue))
    End Sub

    Private Sub frmLogViewer_Enter(sender As System.Object, e As System.EventArgs) Handles MyBase.Enter
        If Not lbox_LogFiles.SelectedIndex = -1 Then
            If Not String.IsNullOrEmpty(LogFile) Then
                If Not lbox_LogFiles.SelectedValue = LogFile Then
                    lbox_LogFiles.SelectedIndex = lbox_LogFiles.FindStringExact(LogFile)
                    Exit Sub
                End If

            End If

            Dim tempLastModified As Date = File.GetLastWriteTime(dicLogFiles.Item(lbox_LogFiles.SelectedValue))

            If (Not tempLastModified = LastModified) Then

                LastModified = tempLastModified

                If (MessageBox.Show("Log file has been modified since it was opened." & Environment.NewLine & Environment.NewLine & "Would you like to refresh it?", "Refresh Logfile?", MessageBoxButtons.YesNo) = System.Windows.Forms.DialogResult.Yes) Then

                    rtb_Log.Clear()
                    rtb_Log.LoadFile(dicLogFiles.Item(lbox_LogFiles.SelectedValue), RichTextBoxStreamType.PlainText)

                End If

            End If
        End If
    End Sub

    Private Sub frmLogViewer_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        If Directory.Exists(LogPath) Then
            dicLogFiles.Clear()
            lbox_LogFiles.Items.Clear()
            For Each fileOnDisk As String In My.Computer.FileSystem.GetFiles(LogPath, FileIO.SearchOption.SearchTopLevelOnly)
                dicLogFiles.Add(Path.GetFileName(fileOnDisk), fileOnDisk)
            Next
        End If

        lbox_LogFiles.DataSource = dicLogFiles.Keys.ToList()

        If Not String.IsNullOrEmpty(LogFile) Then
            lbox_LogFiles.SelectedIndex = lbox_LogFiles.FindStringExact(LogFile)
            btnOpenWith.Enabled = True
        End If

    End Sub

    'Private Sub Button1_Click(sender As System.Object, e As System.EventArgs)

    ' indexOfSearchText += 1

    ' If indexOfSearchText > SearchIndex.Count Then indexOfSearchText = 0 End If

    ' rtb_Log.Select(SearchIndex(indexOfSearchText - 1), tbox_Filter.Text.Length)
    ' rtb_Log.Select(SearchIndex(indexOfSearchText), tbox_Filter.Text.Length)
    ' rtb_Log.SelectionBackColor = Color.Goldenrod rtb_Log.ScrollToCaret()

    'End Sub

    'Private Sub Button3_Click(sender As System.Object, e As System.EventArgs)

    ' rtb_Log.SelectAll() rtb_Log.SelectionBackColor = Color.Transparent

    ' If String.IsNullOrEmpty(tbox_Filter.Text) Then Exit Sub End If

    ' Dim index As Integer = 0

    ' Do While Not index < 0 And index < rtb_Log.Text.Length

    ' index = rtb_Log.Find(tbox_Filter.Text, index, rtb_Log.Text.Length, RichTextBoxFinds.None) If
    ' index = -1 Then Exit Do End If

    ' SearchIndex.Add(index)

    ' 'rtb_Log.Select(index, tbox_Filter.Text.Length) 'rtb_Log.SelectionBackColor = Color.Goldenrod

    ' index += tbox_Filter.Text.Length

    ' Loop

    ' indexOfSearchText = 0

    ' rtb_Log.Select(SearchIndex(indexOfSearchText), 0) rtb_Log.ScrollToCaret()
    Private Sub lbox_LogFiles_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles lbox_LogFiles.SelectedIndexChanged

        If Not lbox_LogFiles.SelectedIndex = -1 Then

            LastModified = File.GetLastWriteTime(dicLogFiles.Item(lbox_LogFiles.SelectedValue))

            rtb_Log.LoadFile(dicLogFiles.Item(lbox_LogFiles.SelectedValue), RichTextBoxStreamType.PlainText)

            Me.Text = "Log Viewer - " & dicLogFiles.Item(lbox_LogFiles.SelectedValue) & " (Read Only)"

            btnOpenWith.Enabled = True
        Else

            btnOpenWith.Enabled = False

        End If

    End Sub

    Private Sub tb_Zoom_Scroll(sender As System.Object, e As System.EventArgs) Handles tb_Zoom.Scroll

        Dim scalar As Single

        Select Case tb_Zoom.Value

            Case 0
                lblScale.Text = "- x" & 2
                scalar = 0.1
            Case 1
                lblScale.Text = "- x" & 1.8
                scalar = 0.2
            Case 2
                lblScale.Text = "- x" & 1.6
                scalar = 0.4
            Case 3
                lblScale.Text = "- x" & 1.4
                scalar = 0.6
            Case 4
                lblScale.Text = "- x" & 1.2
                scalar = 0.8
            Case 5
                lblScale.Text = "x" & 1
                scalar = 1
            Case 6
                lblScale.Text = "x" & 1.2
                scalar = 1.2
            Case 7
                lblScale.Text = "x" & 1.4
                scalar = 1.4
            Case 8
                lblScale.Text = "x" & 1.6
                scalar = 1.6
            Case 9
                lblScale.Text = "x" & 1.8
                scalar = 1.8
            Case 10
                lblScale.Text = "x" & 2
                scalar = 2
        End Select

        rtb_Log.Font = New System.Drawing.Font(rtb_Log.SelectionFont.FontFamily, (8 * scalar), rtb_Log.SelectionFont.Style)

    End Sub

#End Region

End Class