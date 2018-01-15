Imports System.Text
Imports System.Threading
Imports System.IO
Imports System.Drawing

Public Class frmHealPDB

    'Integers
    Dim i_Errors As Integer = 0
    Dim i_Warnings As Integer = 0
    Dim i_Notes As Integer = 0

    'Dictionary
    Dim dicLogReport As New Dictionary(Of String, Object)

    'String
    Dim sSymbolCase As String = Nothing
    Dim sCellCase As String = Nothing

    'Object
    'Property frmmain.librarydata As Data

    'Event
    Event eHealComplete(Errors As Boolean, Warnings As Boolean)

    'Delegates
    Delegate Sub d_HealComplete(ByVal Errors As Boolean, ByVal Warnings As Boolean)
    Delegate Sub d_UpdateStatus(ByVal status As String)
    Delegate Sub d_Increment()

    Private Sub btn_HealLibrary_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_HealLibrary.Click

        i_Errors = ts_Errors.Text = "0"
        i_Notes = ts_Warnings.Text = "0"
        i_Warnings = ts_Notes.Text = "0"

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "\Heal PDB.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "\Heal PDB.log")

        End If

        WaitGif.Enabled = True

        ts_Status.Text = "Starting heal process..."

        AddHandler eHealComplete, AddressOf HealComplete

        sSymbolCase = cbox_SymbolCase.Text
        sCellCase = cbox_CellCase.Text

        Dim t_Heal As Thread = New Threading.Thread(AddressOf Heal)
        t_Heal.IsBackground = True
        t_Heal.Start()

    End Sub

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub HealComplete(ByVal Errors As Boolean, ByVal Warnings As Boolean)
        If Me.InvokeRequired Then

            Dim d As New d_HealComplete(AddressOf HealComplete)
            Me.Invoke(d, New Object() {Errors, Warnings})
        Else
            WaitGif.Enabled = False

            If Errors = True Then

                ts_Status.Text = "Finished with errors."

                Dim reply As DialogResult = MessageBox.Show("The Heal process is finished, but there were errors. Would you like to view the results?", "Finished", _
                  MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Heal PDB")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\Heal PDB.log")
                End If

            ElseIf Warnings = True Then

                ts_Status.Text = "Finished with warnings."

                Dim reply As DialogResult = MessageBox.Show("The Heal process is finished, but there were warnings. Would you like to view the results?", "Finished", _
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    Dim new_process As New Process
                    new_process.StartInfo.FileName = frmMain.librarydata.LogPath & "\Heal PDB.log"
                    new_process.StartInfo.Verb = "Open"
                    new_process.Start()

                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\Heal PDB.log")
                End If

            Else
                ts_Status.Text = "Finished"
                MessageBox.Show("Heal Process completed with no errors or warnings.", "Information:", MessageBoxButtons.OK, MessageBoxIcon.Information)

            End If
        End If
    End Sub

    Private Function HealPDB(ByVal sPartition As Object) As Dictionary(Of String, HealInfo)
        Dim pedHealApp As New MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        'Creates a handle to the Parts Editor in Library Manager
        'pedHealApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        pedHealApp = frmMain.libDoc.PartEditor
        Try
            pedDoc = pedHealApp.ActiveDatabaseEx
            'pedDoc = pedHealApp.OpenDatabaseEx(frmMain.librarydata.LibPath, False)
        Catch ex As Exception
            MsgBox("Error: A partition is reserved in the library. Please open Library Manager and unreserve all partitions.")
            Return Nothing
        End Try

        Dim dicLogReport As New Dictionary(Of String, HealInfo)

        For Each Partition As MGCPCBPartsEditor.Partition In pedDoc.Partitions     'Step through each part partition in the parts editor

            If chklistbox_PDBPartitions.CheckedItems.Contains(Partition.Name) Then

                Dim cHealPDB As Heal_PDB = New Heal_PDB()
                AddHandler cHealPDB.eUpdateStatus, AddressOf UpdateStatus

                cHealPDB.bSupplementData = False

                cHealPDB.bRepairErrors = chkbox_RepairErrors.Checked
                cHealPDB.bUpdatePartType = chkbox_UpdatePartType.Checked
                cHealPDB.bRemoveSpaceFromCell = chkbox_RemoveSpacesfromCells.Checked
                cHealPDB.bAddNCPins = chkbox_AddNC.Checked
                cHealPDB.bUpdateSymPartition = chkbox_UpdateSymPartition.Checked

                If chkbox_ChangeSymbolCase.Checked = True Then
                    cHealPDB.s_SymbolCase = sSymbolCase
                End If

                If chkbox_ChangeCellCase.Checked = True Then
                    cHealPDB.s_CellCase = sCellCase
                End If

                cHealPDB.LibraryData = frmMain.librarydata
                AddHandler cHealPDB.LogError, AddressOf LogError
                AddHandler cHealPDB.LogWarning, AddressOf LogWarning
                AddHandler cHealPDB.LogNote, AddressOf LogNote

                cHealPDB.HealParts(Partition)
                dicLogReport.Add(Partition.Name, cHealPDB.HealLog)

                Try
                    pedHealApp.SaveActiveDatabase()
                Catch ex As Exception

                    Dim oHealInfo As Log

                    dicLogReport.Remove(Partition.Name)
                    dicLogReport.Add(Partition.Name, Nothing)

                End Try

                For Each part As MGCPCBPartsEditor.Part In Partition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll)
                    Dim partNumber As String = part.Number

                    If (partNumber.EndsWith("^new")) Or (partNumber.EndsWith("^NewPart")) Then
                        Dim partName As String() = Split(partNumber, "^")
                        part.Number = partName(0)
                    End If

                Next

                Try
                    pedHealApp.SaveActiveDatabase()
                Catch ex As Exception

                    Dim oHealInfo As Log

                    dicLogReport.Remove(Partition.Name)
                    dicLogReport.Add(Partition.Name, Nothing)

                End Try

            End If

        Next

        pedDoc = Nothing
        'pedHealApp.Quit()
        pedHealApp = Nothing
        Return dicLogReport

    End Function

    Private Sub LogError()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogError)
            Me.Invoke(d)
        Else
            If ts_Errors.Enabled = True Then
                ts_Errors.Text += 1
            End If
        End If
    End Sub

    Private Sub LogWarning()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogWarning)
            Me.Invoke(d)
        Else
            If ts_Warnings.Enabled = True Then
                ts_Warnings.Text += 1
            End If
        End If
    End Sub

    Private Sub LogNote()
        If Me.InvokeRequired Then

            Dim d As New d_Increment(AddressOf LogNote)
            Me.Invoke(d)
        Else
            If ts_Notes.Enabled = True Then
                ts_Notes.Text += 1
            End If

        End If
    End Sub

    Private Sub chkbox_ChangeSymbolCase_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_ChangeSymbolCase.CheckedChanged

        If chkbox_ChangeSymbolCase.Checked = True Then
            cbox_SymbolCase.Enabled = True
        Else
            cbox_SymbolCase.Enabled = False
            cbox_SymbolCase.SelectedIndex = -1
        End If

    End Sub

    Private Sub onLoad(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        For Each sPartition As String In frmMain.librarydata.PartsByPartition.Keys

            chklistbox_PDBPartitions.Items.Add(sPartition, True)

        Next

    End Sub

    Private Sub chkbox_UpdateSymPartition_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        If chkbox_UpdateSymPartition.Checked = False Then

            chkbox_RepairErrors.Checked = False
            chkbox_RepairErrors.Enabled = False

        Else

            chkbox_RepairErrors.Enabled = True

        End If

    End Sub

    Private Sub chkbox_ChangeCellCase_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_ChangeCellCase.CheckedChanged

        If chkbox_ChangeCellCase.Checked = True Then
            cbox_CellCase.Enabled = True
        Else
            cbox_CellCase.Enabled = False
            cbox_CellCase.SelectedIndex = -1
        End If

    End Sub

    Private Sub chkbox_Error_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_Error.CheckedChanged
        If chkbox_Error.Checked = True Then
            ts_Errors.Enabled = True
        Else
            ts_Errors.Enabled = False
        End If
    End Sub

    Private Sub chkbox_Warning_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_Warning.CheckedChanged
        If chkbox_Warning.Checked = True Then
            ts_Warnings.Enabled = True
        Else
            ts_Warnings.Enabled = False
        End If
    End Sub

    Private Sub chkbox_Note_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles chkbox_Note.CheckedChanged
        If chkbox_Note.Checked = True Then
            ts_Notes.Enabled = True
        Else
            ts_Notes.Enabled = False
        End If
    End Sub

    Private Sub chklistbox_PDBPartitions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles chklistbox_PDBPartitions.SelectedIndexChanged

        If chklistbox_PDBPartitions.CheckedItems.Count > 0 Then
            btn_HealLibrary.Enabled = True
        Else
            btn_HealLibrary.Enabled = False
        End If

    End Sub

    Public Sub Heal()

        Dim dicLogReport As Dictionary(Of String, HealInfo) = HealPDB(Nothing)

        Dim b_Warnings As Boolean = False
        Dim b_Errors As Boolean = False

        If chkbox_Error.Checked = True Or chkbox_Warning.Checked = True Or chkbox_Note.Checked = True Then
            For Each kvp As KeyValuePair(Of String, HealInfo) In dicLogReport
                'Grab part number and part attributes:
                Dim sPartition As String = kvp.Key
                Dim oHealInfo As HealInfo = kvp.Value

                If IsNothing(oHealInfo) Then

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                        writer.WriteLine(sPartition & ":")
                        writer.WriteLine("Fatal Error: Could not save database.")
                        writer.WriteLine()

                    End Using

                    b_Errors = True

                    Continue For

                End If
                If oHealInfo.Log.Count > 0 Then
                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                        writer.WriteLine(sPartition & ":")
                        writer.WriteLine("Parts Successfully Fixed: " & oHealInfo.Success)
                        writer.WriteLine("Unable to Fix: " & oHealInfo.Failed)
                        writer.WriteLine("Percentage Complete: " & Format(oHealInfo.Success / (oHealInfo.Success + oHealInfo.Failed) * 100, "0.00") & "%")
                        writer.WriteLine()

                    End Using

                    For Each pair As KeyValuePair(Of String, Log) In oHealInfo.Log

                        If pair.Value.Errors.Count > 0 And chkbox_Error.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        ElseIf pair.Value.Warnings.Count > 0 And chkbox_Warning.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        ElseIf pair.Value.Notes.Count > 0 And chkbox_Note.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                                writer.WriteLine(vbTab & pair.Key)

                            End Using
                        Else
                            Continue For
                        End If

                        If pair.Value.Errors.Count > 0 And chkbox_Error.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                                writer.WriteLine(vbTab & vbTab & "Errors:")

                            End Using

                            For Each sErr As String In pair.Value.Errors
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sErr)

                                End Using
                            Next

                            b_Errors = True

                        End If

                        If pair.Value.Warnings.Count > 0 And chkbox_Warning.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                                writer.WriteLine(vbTab & vbTab & "Warnings:")

                            End Using

                            For Each sWrn As String In pair.Value.Warnings
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sWrn)

                                End Using
                            Next

                            b_Warnings = True
                        End If

                        If pair.Value.Notes.Count > 0 And chkbox_Note.Checked = True Then
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                                writer.WriteLine(vbTab & vbTab & "Notes:")

                            End Using

                            For Each sNote As String In pair.Value.Notes
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                                    writer.WriteLine(vbTab & vbTab & vbTab & sNote)

                                End Using
                            Next

                        End If

                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                            writer.WriteLine()

                        End Using

                    Next

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Heal PDB.log", True)

                        writer.WriteLine()

                    End Using

                End If

            Next
        End If

        RaiseEvent eHealComplete(b_Errors, b_Warnings)
    End Sub

End Class