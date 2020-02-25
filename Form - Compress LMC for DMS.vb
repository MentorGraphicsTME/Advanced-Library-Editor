Imports System.Text
Imports Microsoft.Office.Interop
Imports System.IO

Public Class frmShrinkLMC4DMS

#Region "Private Fields"

    Dim dicDMSLoaderFilebyPartition As New Dictionary(Of String, ArrayList)
    Dim dicGenericPNtoPartition As New Dictionary(Of String, String)
    Dim dicPNtoGenericPN As New Dictionary(Of String, String)

#End Region

#Region "Private Methods"

    Private Sub Evaluate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEvaluate.Click

        'Dim oWaitscreen As New Automation_Waitscreen.WaitScreen
        Dim dicGenericPartAtts As New Dictionary(Of String, Object)
        Dim dicAllGenericEntries As New Dictionary(Of String, ArrayList)
        Dim iCount As Integer = 0
        Dim iCountTotal As Integer = 0
        Dim iGenericMappings As Integer = 0
        dicGenericPNtoPartition.Clear()
        dicPNtoGenericPN.Clear()

        'oWaitscreen.ShowWaitScreen("Running Library Assessment")

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log")

        End If

        If (Not System.IO.Directory.Exists(frmMain.librarydata.LogPath & "\ALE-DMS\")) Then
            System.IO.Directory.CreateDirectory(frmMain.librarydata.LogPath & "\ALE-DMS\")
        End If

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "\ALE-DMS\DMS Loader - Full Central Library.txt") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "\ALE-DMS\DMS Loader - Full Central Library.txt")

        End If

        'Creates a handle to the Parts Editor in Library Manager
        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        Try
            pedDoc = pedApp.OpenDatabaseEx(frmMain.libDoc.FullName, False)
        Catch ex As Exception
            MsgBox("Error: A partition is reserved in the library. Please open Library Manager and unreserve all partitions.")
            Exit Sub
        End Try

        Dim oPartition As MGCPCBPartsEditor.Partition
        Dim alPartitions As New ArrayList

        For Each Entry In chklistbox_PartPartitions.CheckedItems
            alPartitions.Add(Entry.ToString())
        Next

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
            writer.WriteLine("Number of parts before making a generic PDB entry: " & tbox_MinParts.Text)
        End Using

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
            writer.WriteLine()
        End Using

        'Dim oXL As Excel.Application
        'Dim oWB As Excel.Workbook
        'Dim oSheet As Excel.Worksheet
        'Dim oRange As Excel.Range

        '' Start Excel and get Application object.
        'oXL = New Excel.Application

        '' Set some properties
        'oXL.Visible = True
        'oXL.DisplayAlerts = False

        '' Get a new workbook.
        'oWB = oXL.Workbooks.Add

        For Each oPartition In pedDoc.Partitions     'Step through each part partition in the parts editor

            If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "\ALE-DMS\DMS Loader - " & oPartition.Name & ".txt") Then

                My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "\ALE-DMS\DMS Loader - " & oPartition.Name & ".txt")

            End If

            If alPartitions.Contains(oPartition.Name) Then

                'oWB.Sheets.Add()
                'oSheet = DirectCast(oWB.ActiveSheet, Excel.Worksheet)
                'oSheet.Name = oPartition.Name

                Dim oLoopPDB As Compress = New Compress()
                oLoopPDB.parts = oPartition.Parts
                oLoopPDB.dicGenericPartAtts = dicGenericPartAtts
                oLoopPDB.dicAllGenericEntries = dicAllGenericEntries
                oLoopPDB.alNoCell.Clear()
                'oLoopPDB.oSheet = oSheet
                oLoopPDB.alNoSym.Clear()
                oLoopPDB.alNoSymOrCell.Clear()
                oLoopPDB.dicPDBType = frmMain.librarydata.PDBType
                oLoopPDB.LoopParts()

                Dim bPrintPartition As Boolean = True
                For Each kvp As KeyValuePair(Of String, Object) In oLoopPDB.dicGenericPartAtts

                    If Not dicGenericPartAtts.ContainsKey(kvp.Key) Then

                        dicGenericPartAtts.Add(kvp.Key, kvp.Value)

                    End If

                Next

                For Each kvp As KeyValuePair(Of String, ArrayList) In oLoopPDB.dicGenericPDB
                    'Grab part number and part attributes:
                    Dim sGenericPart As String = kvp.Key
                    Dim alPNs As ArrayList = kvp.Value

                    If alPNs.Count >= tbox_MinParts.Text Then
                        Dim sPartition As String
                        If dicGenericPNtoPartition.TryGetValue(sGenericPart, sPartition) Then

                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
                                writer.WriteLine(vbTab & sGenericPart & " - found in partition " & sPartition)
                            End Using
                        Else
                            'dicAllGenericEntries.Add(sGenericPart, alPNs)

                            If bPrintPartition = True Then
                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
                                    writer.WriteLine(oPartition.Name)
                                End Using

                                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
                                    writer.WriteLine("Generic Mappings:")
                                End Using
                                bPrintPartition = False
                            End If

                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
                                writer.WriteLine(vbTab & sGenericPart)
                            End Using

                            iGenericMappings += 1

                        End If

                    End If

                Next
                bPrintPartition = True
                For Each kvp As KeyValuePair(Of String, ArrayList) In oLoopPDB.dicGenericPDB
                    'Grab part number and part attributes:
                    Dim sGenericPart As String = kvp.Key
                    Dim alPNs As ArrayList = kvp.Value

                    iCountTotal = iCountTotal + alPNs.Count

                    If alPNs.Count >= tbox_MinParts.Text Then

                        If bPrintPartition = True Then

                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
                                writer.WriteLine()
                            End Using

                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
                                writer.WriteLine("Generic Mappings Details:")
                            End Using

                            bPrintPartition = False
                        End If

                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
                            writer.WriteLine(vbTab & sGenericPart)
                        End Using

                        For Each sPN In alPNs

                            'PN1234,DX:EE:generic-0603, Resistor

                            dicPNtoGenericPN.Add(sPN, sGenericPart)

                            Dim sPartition As String
                            If dicGenericPNtoPartition.TryGetValue(sGenericPart, sPartition) Then
                                Dim alDMSItems As ArrayList
                                Dim alTempDMSItems As New ArrayList
                                dicDMSLoaderFilebyPartition.TryGetValue(sPartition, alDMSItems)

                                If Not IsNothing(alDMSItems) Then

                                    alTempDMSItems = alDMSItems

                                End If

                                alTempDMSItems.Add(sPN & ",DX:" & tbox_LibraryScheme.Text & ":" & sGenericPart & ", " & sPartition)

                                dicDMSLoaderFilebyPartition.Remove(sPartition)

                                dicDMSLoaderFilebyPartition.Add(sPartition, alTempDMSItems)
                            Else
                                Dim alDMSItems As ArrayList
                                If dicDMSLoaderFilebyPartition.TryGetValue(oPartition.Name, alDMSItems) Then

                                    Dim alTempDMSItems As New ArrayList
                                    If Not IsNothing(alDMSItems) Then

                                        alTempDMSItems = alDMSItems

                                    End If

                                    alTempDMSItems.Add(sPN & ",DX:" & tbox_LibraryScheme.Text & ":" & sGenericPart & ", " & oPartition.Name)

                                    dicDMSLoaderFilebyPartition.Remove(oPartition.Name)

                                    dicDMSLoaderFilebyPartition.Add(oPartition.Name, alTempDMSItems)
                                Else
                                    Dim alDMSOutput As New ArrayList()
                                    alDMSOutput.Add(sPN & ",DX:" & tbox_LibraryScheme.Text & ":" & sGenericPart & ", " & oPartition.Name)
                                    dicGenericPNtoPartition.Add(sGenericPart, oPartition.Name)
                                    dicDMSLoaderFilebyPartition.Add(oPartition.Name, alDMSOutput)
                                End If

                            End If

                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
                                writer.WriteLine(vbTab & vbTab & sPN)
                            End Using

                            iCount += 1

                        Next

                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
                            writer.WriteLine()
                        End Using

                    End If

                Next
            Else

                If chkbox_Non_Compressed_Partitions.Checked = True Then

                    Using DMSwriter As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\DMS Loader - " & oPartition.Name & ".txt", True, Encoding.ASCII)
                        DMSwriter.WriteLine("@PartNumber, Mapping, Partition")
                    End Using

                    For Each oPart As MGCPCBPartsEditor.Part In oPartition.Parts

                        Using DMSwriter As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\DMS Loader - " & oPartition.Name & ".txt", True, Encoding.ASCII)
                            DMSwriter.WriteLine(oPart.Number & ",DX:" & tbox_LibraryScheme.Text & ":" & oPart.Number & ", " & oPartition.Name)
                        End Using

                    Next

                End If

            End If

        Next

        For Each kvp As KeyValuePair(Of String, ArrayList) In dicDMSLoaderFilebyPartition

            Dim sPartition As String = kvp.Key
            Dim alDMSItems As ArrayList = kvp.Value

            If chkbox_OuputSeperateFiles.Checked = True Then
                Using DMSwriter As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\DMS Loader - " & sPartition & ".txt", True, Encoding.ASCII)
                    DMSwriter.WriteLine("@PartNumber, Mapping, Partition")
                End Using

                For Each sLine In alDMSItems

                    Using DMSwriter As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\DMS Loader - " & sPartition & ".txt", True, Encoding.ASCII)
                        DMSwriter.WriteLine(sLine)
                    End Using

                Next
            Else
                Using DMSwriter As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\DMS Loader - Full Central Library.txt", True, Encoding.ASCII)
                    DMSwriter.WriteLine("@PartNumber, Mapping, Partition")
                End Using

                For Each sLine In alDMSItems

                    Using DMSwriter As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\DMS Loader - Full Central Library.txt", True, Encoding.ASCII)
                        DMSwriter.WriteLine(sLine)
                    End Using

                Next

            End If

        Next

        'oWaitscreen.CloseWaitScreen()

        pedApp.CloseActiveDatabase(True)
        pedDoc = Nothing

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
            writer.WriteLine("Results:")
        End Using

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
            writer.WriteLine("Total Parts: " & iCountTotal)
        End Using

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
            writer.WriteLine("Total Generic Mappings Created: " & iGenericMappings)
        End Using

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log", True, Encoding.ASCII)
            writer.WriteLine("Total Parts resolved to a Generic Mapping: " & iCount)
        End Using

        Dim reply As DialogResult = MessageBox.Show("Library evaluation has finished. Would you like to view the results?", "Finished",
             MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

        If reply = DialogResult.Yes Then
            Dim new_process As New Process
            new_process.StartInfo.FileName = frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log"
            new_process.StartInfo.Verb = "Open"
            new_process.Start()
        Else
            MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "\ALE-DMS\Report - Shrink LMC for DMS.log")
        End If

        btnProcess.Enabled = True

    End Sub

    Private Sub frmShrinkLMC4DMS_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        For Each sPartition As String In frmMain.librarydata.PartsByPartition.Keys

            chklistbox_PartPartitions.Items.Add(sPartition, True)

        Next

    End Sub

    Private Sub Process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click

        'Dim oWaitscreen As New Automation_Waitscreen.WaitScreen
        Dim alCreatedEntries As New ArrayList

        'oWaitscreen.ShowWaitScreen("Creating Generic PDB Entries")

        'Creates a handle to the Parts Editor in Library Manager
        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        Try
            pedDoc = pedApp.OpenDatabaseEx(frmMain.libDoc.FullName, False)
        Catch ex As Exception
            MsgBox("Error: A partition is reserved in the library. Please open Library Manager and unreserve all partitions.")
            Exit Sub
        End Try

        Dim oPartition As MGCPCBPartsEditor.Partition
        Dim alPartitions As New ArrayList

        For Each Entry In chklistbox_PartPartitions.CheckedItems
            alPartitions.Add(Entry.ToString())
        Next

        For Each oPartition In pedDoc.Partitions     'Step through each part partition in the parts editor

            If alPartitions.Contains(oPartition.Name) Then
                Dim oLoopPDB As Compress = New Compress()
                oLoopPDB.parts = oPartition.Parts
                oLoopPDB.alCreatedEntries = alCreatedEntries

                oLoopPDB.dicGenericEntries = dicPNtoGenericPN

                oLoopPDB.CreateGenericPDBEntries()

                alCreatedEntries = oLoopPDB.alCreatedEntries

            End If

        Next

        pedApp.CloseActiveDatabase(True)
        pedDoc = Nothing

        'oWaitscreen.CloseWaitScreen()

        Dim reply As DialogResult = MessageBox.Show("Generic PDB entries have been created. Would you like to view the DMS load file?", "Finished",
     MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

        If reply = DialogResult.Yes Then
            Dim new_process As New Process
            new_process.StartInfo.FileName = frmMain.librarydata.LogPath & "DMS Generic Mappings.txt"
            new_process.StartInfo.Verb = "Open"
            new_process.Start()
        Else
            MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "DMS Generic Mappings.txt")
        End If

    End Sub

#End Region

End Class