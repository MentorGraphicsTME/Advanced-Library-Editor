Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text
Imports System.Threading
Imports System.Drawing
Public Class frmResetSymPins

    'Objects
    'Property frmmain.librarydata As Data
    'Property LibDoc As LibraryManager.IMGCLMLibrary

    'Integers
    Dim i_Count As Integer = 0

    'List
    Dim l_PartitionsToProcess As New List(Of String)

    'Delegates
    Delegate Sub d_UpdateCount()
    Delegate Sub d_UpdateStatus(ByVal status As String)

    'Other
    Dim countlock As New Object

    'Events
    Event eProcessComplete()
    Event eUpdateStatus(status As String)

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        For Each partition As String In frmMain.librarydata.SymbolsByPartition.Keys

            chklistbox_SymPartitions.Items.Add(partition.ToString, True)

        Next

    End Sub

    Private Sub btnReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReset.Click

        If chklistbox_SymPartitions.CheckedItems.Count = 0 Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Reset Symbols:", "No symbol partitions selected...", ToolTipIcon.Error)
            ts_Status.Text = "No symbol partitions selected..."
            chklistbox_SymPartitions.Focus()
            Exit Sub
        End If

        If chkbox_GraphicColors.Checked = False And chkbox_PinLocations.Checked = False And chkbox_TextColors.Checked = False Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Reset Symbols:", "Nothing to modify..", ToolTipIcon.Error)
            ts_Status.Text = "Nothing was selected to modify..."
            Exit Sub
        End If

        frmMain.NotifyIcon.ShowBalloonTip(2000, "Reset Symbols:", "Processing...", ToolTipIcon.Info)
        ts_Status.Text = "Reading input file..."

        AddHandler eProcessComplete, AddressOf ResetSymbolsComplete
        Dim t_Update As Thread = New Threading.Thread(AddressOf ResetSymbols)
        t_Update.IsBackground = True
        t_Update.Start()

    End Sub

    Private Sub ResetSymbols()

        Dim newThreads(chklistbox_SymPartitions.CheckedItems.Count) As Thread

        For i As Integer = 0 To chklistbox_SymPartitions.CheckedItems.Count - 1

            Dim resetSymbols As Symbols = New Symbols()
            resetSymbols.libDoc = frmMain.libDoc
            resetSymbols.LibraryData = frmMain.librarydata
            resetSymbols.AutomaticText = chkbox_TextColors.Checked
            resetSymbols.AutomaticGraphics = chkbox_GraphicColors.Checked
            resetSymbols.ResetSymbolPins = chkbox_PinLocations.Checked

            AddHandler resetSymbols.eUpdateCount, AddressOf UpdateCount

            i_Count += frmMain.librarydata.SymbolsByPartition.Item(chklistbox_SymPartitions.CheckedItems(i).ToString()).Count()
            RaiseEvent eUpdateStatus("Symbols left to process: " & i_Count)

            newThreads(i) = New Thread(AddressOf resetSymbols.ProcessSymbols)
            newThreads(i).IsBackground = True
            newThreads(i).Start(chklistbox_SymPartitions.CheckedItems(i).ToString())

        Next

        For i As Integer = 0 To chklistbox_SymPartitions.CheckedItems.Count - 1
            newThreads(i).Join()
        Next

        RaiseEvent eProcessComplete()

    End Sub

    Private Sub UpdateCount()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateCount(AddressOf UpdateCount)
            Me.Invoke(d)
        Else
            SyncLock countlock
                i_Count -= 1
                RaiseEvent eUpdateStatus("Symbols left to analyze: " & i_Count)
            End SyncLock
        End If
    End Sub

    Private Sub ResetSymbolsComplete()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateCount(AddressOf ResetSymbolsComplete)
            Me.Invoke(d)
        Else

            WaitGif.Enabled = False
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Reset Symbols:", "Finished...", ToolTipIcon.Info)
            ts_Status.Text = "Reset symbols has finished..."

        End If
    End Sub

End Class