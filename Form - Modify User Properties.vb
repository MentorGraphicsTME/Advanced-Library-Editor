Imports System.Threading
Imports System.Runtime.InteropServices
Imports System.Drawing

Public Class frmModifyUserLayers

    Dim dic_Properties As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
    Dim l_NewProperties As New List(Of String)
    Dim l_RemoveProperties As New List(Of String)

    'Events
    Event eUpdateCellsComplete()
    Event eAnalysisComplete()
    Event eUpdateStatus(status As String)

    'Delegates
    Delegate Sub ScanComplete()
    Delegate Sub d_ReadFinished()
    Delegate Sub d_UpdateStatus(ByVal status As String)

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click

        dic_Properties.Clear()

        WaitGif.Enabled = True
        btnRead.Enabled = False
        frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify User Properties", "Analyzing cells...", ToolTipIcon.Info)
        ts_Status.Text = "Analyzing cells..."

        Dim th_Read As Thread = New Thread(AddressOf ReadProperties)
        th_Read.IsBackground = True
        th_Read.Start()

    End Sub

    Private Sub frmModifyUserLayers_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eAnalysisComplete, AddressOf AnalysisComplete

    End Sub

    Private Sub ReadProperties()
        ' Variables to hold the application thingos
        Dim cellEd As CellEditorAddinLib.CellEditorDlg
        Dim cellDB As CellEditorAddinLib.CellDB

        RaiseEvent eUpdateStatus("Opening cell editor...")

        Try

            ' Open the Cell Editor dialog and open the library database
            'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
            cellEd = frmMain.libDoc.CellEditor
            cellDB = cellEd.ActiveDatabase
            'cellDB = cellEd.OpenDatabase(frmMain.libDoc.FullName, False)

        Catch ex As Exception
            'cellEd.CloseActiveDatabase(False)
            cellEd.Quit()
            cellEd = Nothing
            MsgBox("Unable to open cell editor. Error returned was:" & Environment.NewLine & ex.Message.ToString())
            End
        End Try

        For Each cePartition As CellEditorAddinLib.Partition In cellDB.Partitions

            For Each ceCell As CellEditorAddinLib.Cell In cePartition.Cells

                RaiseEvent eUpdateStatus("Reading: " & cePartition.Name & ":" & ceCell.Name)

                For Each ceProperty As CellEditorAddinLib.CellProperty In ceCell.Properties

                    Dim l_Cells As List(Of String)

                    If dic_Properties.ContainsKey(ceProperty.Name) Then
                        l_Cells = dic_Properties.Item(ceProperty.Name)
                    Else
                        l_Cells = New List(Of String)
                    End If

                    If Not l_Cells.Contains(cePartition.Name & ":" & ceCell.Name) Then
                        l_Cells.Add(cePartition.Name & ":" & ceCell.Name)
                        dic_Properties.Item(ceProperty.Name) = l_Cells
                    End If

                Next

            Next

        Next

        cellDB = Nothing
        cellEd.Quit()
        cellEd = Nothing

        RaiseEvent eAnalysisComplete()

    End Sub

    Private Sub AnalysisComplete()
        If Me.InvokeRequired Then

            Dim d As New ScanComplete(AddressOf AnalysisComplete)
            Me.Invoke(d)
        Else
            tv_Properties.Load(dic_Properties, True)

            WaitGif.Enabled = False
            btnRead.Enabled = True
            btnNew.Enabled = True
            btnProcess.Enabled = True
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify User Properties", "Analysis Complete...", ToolTipIcon.Info)
            ts_Status.Text = ""

        End If
    End Sub

    Private Sub tsm_Remove_Click(sender As Object, e As EventArgs) Handles tsm_RemoveProperty.Click
        If l_RemoveProperties.Contains(tv_Properties.SelectedNode.Text) Then
            tv_Properties.SelectedNode.ForeColor = System.Drawing.SystemColors.MenuText
            l_RemoveProperties.Remove(tv_Properties.SelectedNode.Text)
        Else
            tv_Properties.SelectedNode.ForeColor = Color.Gray
            l_RemoveProperties.Add(tv_Properties.SelectedNode.Text)
        End If
    End Sub

    Private Const TVIF_STATE As Integer = &H8
    Private Const TVIS_STATEIMAGEMASK As Integer = &HF000
    Private Const TV_FIRST As Integer = &H1100
    Private Const TVM_SETITEM As Integer = TV_FIRST + 63

    <StructLayout(LayoutKind.Sequential)>
    Public Structure TVITEM
        Public mask As Integer
        Public hItem As IntPtr
        Public state As Integer
        Public stateMask As Integer
        <MarshalAs(UnmanagedType.LPTStr)> Public lpszText As String
        Public cchTextMax As Integer
        Public iImage As Integer
        Public iSelectedImage As Integer
        Public cChildren As Integer
        Public lParam As IntPtr
    End Structure

    Private Declare Auto Function SendMessage Lib "User32.dll" (ByVal hwnd As IntPtr, ByVal msg As Integer, ByVal wParam As IntPtr, ByRef lParam As TVITEM) As Integer

    Private Sub HideRootCheckBox(ByVal node As TreeNode)
        Dim tvi As New TVITEM
        tvi.hItem = node.Handle
        tvi.mask = TVIF_STATE
        tvi.stateMask = TVIS_STATEIMAGEMASK
        tvi.state = 0
        SendMessage(tv_Properties.Handle, TVM_SETITEM, IntPtr.Zero, tvi)
    End Sub

    Private Sub tv_Properties_DrawNode(ByVal sender As Object, ByVal e As DrawTreeNodeEventArgs)
        If e.Node.Level = 0 Then
            HideRootCheckBox(e.Node)
        End If

        e.DrawDefault = True
    End Sub

    Private Sub btnNew_Click(sender As Object, e As EventArgs) Handles btnNew.Click

        Dim sPropertyName As String = InputBox("Provide a property name. ", "New Property", "New Property Name")

        If Not dic_Properties.ContainsKey(sPropertyName) Or l_NewProperties.Contains(sPropertyName, StringComparer.CurrentCultureIgnoreCase) Then
            l_NewProperties.Add(sPropertyName)
            Dim newProperty As TreeNode = tv_Properties.Nodes.Add(sPropertyName)
            newProperty.ForeColor = Color.DarkBlue
        End If

    End Sub

    Private Sub tv_Properties_NodeMouseClick(sender As Object, e As TreeNodeMouseClickEventArgs) Handles tv_Properties.NodeMouseClick
        If e.Button = System.Windows.Forms.MouseButtons.Right And e.Node.Level = 0 Then
            tsm_RemoveDefaultValue.Enabled = False
            tsm_RemoveDefaultValue.Checked = False
            tsm_AddDefaultValue.Enabled = False
            tsm_RemoveProperty.Checked = False

            If l_RemoveProperties.Contains(e.Node.Text) Then

                tsm_RemoveProperty.Checked = True

            End If
        End If
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click

        WaitGif.Enabled = True
        btnRead.Enabled = False
        frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify User Properties", "Processing cells...", ToolTipIcon.Info)
        ts_Status.Text = "Processing cells..."

        Dim th_Process As Thread = New Thread(AddressOf ProcessProperties)
        th_Process.IsBackground = True
        th_Process.Start()

    End Sub

    Private Sub ProcessProperties()
        ' Variables to hold the application thingos
        Dim cellEd As CellEditorAddinLib.CellEditorDlg
        Dim cellDB As CellEditorAddinLib.CellDB

        RaiseEvent eUpdateStatus("Opening cell editor...")

        Try

            ' Open the Cell Editor dialog and open the library database
            'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
            'cellDB = cellEd.OpenDatabase(frmMain.libDoc.FullName, False)
            cellEd = frmMain.libDoc.CellEditor
            cellDB = cellEd.ActiveDatabase

        Catch ex As Exception
            cellEd.Quit()
            cellEd = Nothing
            'cellEd.CloseActiveDatabase(False)
            MsgBox("Unable to open cell editor. Error returned was:" & Environment.NewLine & ex.Message.ToString())
            End
        End Try

        If l_NewProperties.Count > 0 Then
            For Each cePartition As CellEditorAddinLib.Partition In cellDB.Partitions

                For Each ceCell As CellEditorAddinLib.Cell In cePartition.Cells

                    RaiseEvent eUpdateStatus("Reading: " & cePartition.Name & ":" & ceCell.Name)

                    For Each sProperty As String In l_NewProperties
                        ceCell.PutProperty(sProperty, String.Empty)
                    Next

                    For Each sProperty As String In l_RemoveProperties

                        If dic_Properties(sProperty).Contains(cePartition.Name & ":" & ceCell.Name) Then
                            ceCell.Properties.Item(sProperty).Delete()
                        End If

                    Next

                Next

            Next
        Else

        End If

        cellEd.SaveActiveDatabase()
        cellDB = Nothing
        cellEd.Quit()
        cellEd = Nothing
    End Sub

End Class