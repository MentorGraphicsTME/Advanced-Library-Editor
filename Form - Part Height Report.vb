Imports System.Text
Imports System.Drawing

Public Class frmPartHeightRpt
    ' Variable to hold the library document
    Dim cellEd As CellEditorAddinLib.CellEditorDlg
    Dim cellDB As CellEditorAddinLib.CellDB
    Dim oCellDoc As MGCPCB.Document
    Dim oCellPartitions As CellEditorAddinLib.Partitions
    Dim oCellPartition As CellEditorAddinLib.Partition
    Dim oCells As CellEditorAddinLib.Cells
    Dim oCell As CellEditorAddinLib.Cell
    Property sCellPrefix As String

    'Objects
    'Property frmmain.librarydata As Data

    'dictionary
    Property dicErrors As New Dictionary(Of String, String)
    Dim dicPartitionToPart As New Dictionary(Of String, List(Of String))
    Dim dicPartPartitionAndName As New Dictionary(Of String, ArrayList)

    Private Sub btnCancel_Click(sender As System.Object, e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub ListBox_Partitions_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles ListBox_Partitions.SelectedIndexChanged
        ' Create variables to store lists
        Dim alParts As New ArrayList()
        Dim partsToProcess As List(Of String)

        ' If the partition is stored with all partitions, store its parts in alParts
        If dicPartPartitionAndName.TryGetValue(ListBox_Partitions.SelectedItem, alParts) Then

            ' Empty the parts list box
            chklistbox_Parts.Items.Clear()

            ' If the partition is stored with the ones to process parts from, get its list of parts to process
            If dicPartitionToPart.TryGetValue(ListBox_Partitions.SelectedItem, partsToProcess) Then

                ' For each part in the list of all parts in the partition
                For Each sPart In alParts
                    ' If the parts to process contains the part
                    If partsToProcess.Contains(sPart) Then
                        ' add it with a checked box
                        chklistbox_Parts.Items.Add(sPart, True)
                    Else
                        ' If the parts to process DOESN'T contain the part, add it with an unchecked box
                        chklistbox_Parts.Items.Add(sPart)
                    End If
                Next

            Else

                ' If there is no partition with parts to process, then add all the parts unchecked
                For Each sPart In alParts
                    chklistbox_Parts.Items.Add(sPart)
                Next
            End If

        Else

            ' If there is no partition stored, then empty the part box
            chklistbox_Parts.Items.Clear()
        End If
    End Sub

    Private Sub frmPartHeightRpt_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        'Upon loading the form, populate the partition listbox
        For Each sPartition As String In frmMain.librarydata.PartsByPartition.Keys
            ListBox_Partitions.Items.Add(sPartition)
        Next

        ' Create a dictionary of partitions and their parts
        CreatePartDictionary()

    End Sub

    Private Sub chklistbox_Parts_ItemCheck(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles chklistbox_Parts.ItemCheck
        ' Create a list to hold parts to process
        Dim partsToProcess As List(Of String)

        ' If a part was just checked
        If e.NewValue = CheckState.Checked Then
            ' If the current partition already has a list of parts to be processed
            If dicPartitionToPart.TryGetValue(ListBox_Partitions.SelectedItem, partsToProcess) Then
                ' If the current part isn't already in the list, add it
                If Not partsToProcess.Contains(chklistbox_Parts.SelectedItem) Then
                    partsToProcess.Add(chklistbox_Parts.SelectedItem)
                End If
            Else
                ' If the current partition doesn't have a list of parts to be processed, create it and add it to the dic
                Dim tempPartsToProcess As New List(Of String)
                tempPartsToProcess.Add(chklistbox_Parts.SelectedItem)
                dicPartitionToPart.Add(ListBox_Partitions.SelectedItem, tempPartsToProcess)
            End If

        Else
            ' If the part was just unchecked and the partition has a list of parts to process
            If dicPartitionToPart.TryGetValue(ListBox_Partitions.SelectedItem, partsToProcess) Then
                ' If the unchecked part is in the list, remove it and update the parts to process
                If partsToProcess.Contains(chklistbox_Parts.SelectedItem) Then
                    partsToProcess.Remove(chklistbox_Parts.SelectedItem)
                    dicPartitionToPart.Remove(ListBox_Partitions.SelectedItem)
                    dicPartitionToPart.Add(ListBox_Partitions.SelectedItem, partsToProcess)

                End If
            End If
        End If

    End Sub

    Private Sub btnSelectAll_Click(sender As System.Object, e As System.EventArgs) Handles btnSelectAll.Click
        Dim i As Integer
        For i = 0 To (chklistbox_Parts.Items.Count - 1) 'Listbox is the listbox's name
            chklistbox_Parts.SetItemChecked(i, True)
        Next

        Dim tempPartsToProcess As New List(Of String)

        For Each item In chklistbox_Parts.CheckedItems

            tempPartsToProcess.Add(item)

        Next

        If dicPartitionToPart.ContainsKey(ListBox_Partitions.SelectedItem) Then

            dicPartitionToPart.Remove(ListBox_Partitions.SelectedItem)
            dicPartitionToPart.Add(ListBox_Partitions.SelectedItem, tempPartsToProcess)
        Else
            dicPartitionToPart.Add(ListBox_Partitions.SelectedItem, tempPartsToProcess)
        End If

    End Sub

    Private Sub btnClearSelected_Click(sender As System.Object, e As System.EventArgs) Handles btnClearSelected.Click
        Dim i As Integer
        For i = 0 To (chklistbox_Parts.Items.Count - 1) 'Listbox is the listbox's name
            chklistbox_Parts.SetItemChecked(i, False)
        Next

        If dicPartitionToPart.ContainsKey(ListBox_Partitions.SelectedItem) Then

            dicPartitionToPart.Remove(ListBox_Partitions.SelectedItem)

        End If
    End Sub

    ' Creates a dictionary similar to the dicCellPartitionAndName created in the main form, only for parts.
    Private Sub CreatePartDictionary()

        ' Variables for the part editor
        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        ' Initialize the part editor
        'pedApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        pedApp = frmMain.libDoc.PartEditor
        pedDoc = pedApp.ActiveDatabaseEx
        'pedDoc = pedApp.OpenDatabaseEx(frmMain.libDoc.FullName, False)

        ' Cycle through partitions
        For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions

            Dim alPartList As New ArrayList

            ' Create variables for cycling through parts
            Dim oParts As MGCPCBPartsEditor.Parts = pdbPartition.Parts  ' get the parts from the partition
            Dim oPart As MGCPCBPartsEditor.Part

            ' Go through each part in the current partition and add it to an array list
            For Each oPart In oParts
                alPartList.Add(oPart.Name)
            Next

            ' Sort the part array list alphabetically to make it more user friendly
            alPartList.Sort()

            ' Create an entry in the dictionary for the partition and parts
            dicPartPartitionAndName.Add(pdbPartition.Name, alPartList)

        Next

        ' Close without saving
        'pedApp.CloseActiveDatabase(False)
        pedApp.Quit()
        pedApp = Nothing
        pedDoc = Nothing

    End Sub

    Private Sub btnGenerate_Click(sender As System.Object, e As System.EventArgs) Handles btnGenerate.Click

        'Dim oWaitScreen As New Automation_Waitscreen.WaitScreen

        'Try
        '    oWaitScreen.CloseWaitScreen()
        'Catch ex As Exception

        'End Try

        'oWaitScreen.ShowWaitScreen("Generating height report...")

        ' Variable for creating the report
        Dim swReadDatabase As System.IO.StreamWriter
        Dim sColumns As String = "{0,-20}{1,-15}{2,-30}{3,-15}"

        ' ---------------------------
        ' Set up the stream writer to create report
        ' ---------------------------

        ' Check if the report file already exists.
        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "Parts Height Report.log") Then
            ' If it does exist, delete it.
            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "Parts Height Report.log")
        End If

        swReadDatabase = My.Computer.FileSystem.OpenTextFileWriter(frmMain.librarydata.LogPath & "Parts Height Report.log", _
                                                                  True, Encoding.ASCII)  'Create Config File swOutput Stream
        swReadDatabase.WriteLine(" ")
        swReadDatabase.WriteLine("Parts Height Report")
        swReadDatabase.WriteLine(" ")
        swReadDatabase.WriteLine("============================================================================")
        swReadDatabase.Write(vbCrLf)

        ' Get the part editor from the library document
        Dim partEditor = frmMain.libDoc.PartEditor

        ' Get the PDB
        Dim activeDB = partEditor.ActiveDatabaseEx

        ' Get the partitions and declare variables for cycling through them
        Dim partPartitions = activeDB.Partitions
        Dim partPartition
        Dim parts
        Dim part

        ' Declare variables for cell processing
        Dim oCellPartitions As CellEditorAddinLib.Partitions            ' Partitions
        Dim oCellPartition As CellEditorAddinLib.Partition
        Dim oCells As CellEditorAddinLib.Cells                          ' Cells
        Dim oCell As CellEditorAddinLib.Cell

        ' Central Library cell processing variables
        Dim cellEd As CellEditorAddinLib.CellEditorDlg                  ' Cell editor (dialogue)
        'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
        cellEd = frmMain.libDoc.CellEditor
        Dim cellDB As CellEditorAddinLib.CellDB                         ' Cell database
        Dim myLibDoc As LibraryManager.IMGCLMLibrary = frmMain.libDoc   ' Get the lib doc from the main form
        'cellDB = cellEd.OpenDatabase(myLibDoc.FullName, False)
        cellDB = cellEd.ActiveDatabase

        For Each partPartition In partPartitions

            Dim partsToProcess As List(Of String)
            Dim lPartsProcessed As New List(Of String)

            ' If the partition is in the dictionary, get the cells to process
            If dicPartitionToPart.TryGetValue(partPartition.Name, partsToProcess) Then

                parts = partPartition.parts

                ' As long as there are actually cells to process...
                If parts.Count > 0 And partsToProcess.Count > 0 Then

                    'Create the column headers
                    Dim header As String
                    header = String.Format(sColumns, "Part Number", "Part Height", "Associated Cell", "Cell Height")
                    swReadDatabase.WriteLine(header)
                    header = String.Format(sColumns, "-----------", "-----------", "---------------", "-----------")
                    swReadDatabase.WriteLine(header)

                    For Each part In parts

                        If partsToProcess.Contains(part.Name) Then
                            Dim sHeight As String = "--"

                            ' If the part has a height, store it in the height string
                            Dim properties = part.Properties
                            Dim prop
                            For Each prop In properties
                                If prop.Name = "Height" Then
                                    sHeight = prop.Value
                                End If
                            Next

                            ' Get / print cell info
                            Dim alCellRefs As New ArrayList
                            alCellRefs.Clear()
                            Dim oCellRefs = part.CellReferences
                            Dim oCellRef

                            For Each oCellRef In oCellRefs
                                alCellRefs.Add(oCellRef.Name)
                            Next

                            oCellPartitions = cellDB.Partitions()               ' Get the partitions in the cell database
                            For Each oCellPartition In oCellPartitions          ' Process each partition
                                oCells = oCellPartition.Cells                   ' Get the cells in the partition
                                For Each oCell In oCells                        ' Try to find the entered cell
                                    If alCellRefs.Contains(oCell.Name) Then

                                        ' Store the cell info
                                        Dim cellInfo As String
                                        cellInfo = String.Format(sColumns, part.Number, sHeight, oCell.Name, oCell.Height.ToString)

                                        ' Write to the log
                                        swReadDatabase.WriteLine(cellInfo)

                                    End If
                                Next
                            Next
                        End If
                    Next
                End If
            End If
        Next

        'cellEd.CloseActiveDatabase()

        ' Close the streamwriter for log file
        swReadDatabase.Close()

        ' Save the database and close the editor
        partEditor.SaveActiveDatabase()
        partEditor = Nothing
        activeDB = Nothing
        'partEditor.Quit()

        'Try
        '    oWaitScreen.CloseWaitScreen()
        'Catch ex As Exception

        'End Try

        'oWaitScreen = Nothing

        ' Show the user the cell comparison results
        Dim openLog As DialogResult = MessageBox.Show("Part height report complete." & Environment.NewLine & Environment.NewLine & "Would you like to view the report?", "Part Height Report", _
      MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1)

        If openLog = DialogResult.Yes Then
            frmMain.OpenLogFile("Parts Height Report")
        Else
            MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Parts Height Report.log")
        End If

    End Sub
End Class