Imports System.Threading
Imports System.Drawing

Public Class frm_UpdateCellName

    'Property frmmain.librarydata As Data

    'Delegates
    Delegate Sub d_ThreadComplete()

    Private Sub frm_UpdateCellName_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        MdiParent = frmMain

        For Each sPartition As String In frmMain.librarydata.CellsByPartition.Keys
            chklstbox_Partitions.Items.Add(sPartition)
        Next

    End Sub

    Private Sub btnModify_Click(sender As Object, e As EventArgs) Handles btnModify.Click

        If Not cbox_SymbolCase.SelectedIndex = -1 And Not chklstbox_Partitions.CheckedItems.Count = 0 Then

            ' oWaitScreen.ShowWaitScreen("Repairing Cells")

            Dim clModCells As New Rename
            clModCells.oMoreInfo.NameCase = cbox_SymbolCase.Text
            clModCells.oMoreInfo.Partitions.AddRange(chklstbox_Partitions.CheckedItems)
            Dim newThread As Thread

            AddHandler clModCells.ThreadComplete, AddressOf ModifyNameComplete
            newThread = New Thread(Sub() clModCells.LoopCells(frmMain.libDoc.CellEditor))
            newThread.IsBackground = True
            newThread.Start()

        End If

    End Sub

    Private Sub ModifyNameComplete()

        If Me.InvokeRequired Then
            Dim d As New d_ThreadComplete(AddressOf ModifyNameComplete)
            Me.Invoke(d)
        Else

            'oWaitScreen.CloseWaitScreen()
            MessageBox.Show("Rename Complete...")

        End If

    End Sub

End Class