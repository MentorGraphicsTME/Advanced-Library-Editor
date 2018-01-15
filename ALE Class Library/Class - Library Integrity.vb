Imports System.IO
Imports System.Text.RegularExpressions

Public Class Library_Integrity

    'Events
    Event eSymbolsFinished(dicSymbolsRemoved As Dictionary(Of String, List(Of String)))
    Event eCellsFinished(dicCellsRemoved As Dictionary(Of String, List(Of String)))
    Event ePartsFinished(dictionary As Dictionary(Of String, List(Of String)))

    'Dictionaries
    Property dic_CellsToDelete As New Dictionary(Of String, List(Of String))
    Property dic_PartsToDelete As New Dictionary(Of String, List(Of String))
    Property dic_SymbolsToDelete As New Dictionary(Of String, List(Of String))
    Property dic_PadstacksToDelete As New Dictionary(Of String, List(Of String))

    ''Objects
    Property Partition As MGCPCBPartsEditor.Partition
    Property oParts As MGCPCBPartsEditor.Parts
    Dim pedApp As New MGCPCBPartsEditor.PartsEditorDlg
    Dim cellEd As CellEditorAddinLib.CellEditorDlg
    Dim cellDB As CellEditorAddinLib.CellDB
    Dim PadStackEdDlg As PadstackEditorLib.PadstackEditorDlg
    Dim oPadStackDB As PadstackEditorLib.PadstackDB
    Property libDoc As LibraryManager.IMGCLMLibrary

    Sub DeleteUnusedSymbols()

        Dim dicSymbolsRemoved As New Dictionary(Of String, List(Of String))

        For Each kvp As KeyValuePair(Of String, List(Of String)) In dic_SymbolsToDelete

            Dim lSymbols As New List(Of String)

            For Each fileOnDisk As String In My.Computer.FileSystem.GetFiles(libDoc.Path & "\SymbolLibs\" & kvp.Key & "\sym", FileIO.SearchOption.SearchTopLevelOnly)
                Dim ext As String = Path.GetExtension(fileOnDisk)
                Dim r As Regex = New Regex("\.\d+$") ' Looks to see if the extension is a number
                Dim m As Match = r.Match(ext)
                If (m.Success) Then

                    Dim sSymbol As String = Path.GetFileNameWithoutExtension(fileOnDisk)

                    If kvp.Value.Contains(sSymbol) Then
                        lSymbols.Add(Path.GetFileName(fileOnDisk))
                        File.Delete(fileOnDisk)

                    End If

                End If

            Next

            'dicSymbolsRemoved.Add(kvp.Key, lSymbols)

        Next

        RaiseEvent eSymbolsFinished(dicSymbolsRemoved)

    End Sub

    Sub DeleteUnusedCells()

        ' Open the Cell Editor dialog and open the library database
        'cellEd = CreateObject("cellEditorAddin.cellEditorDlg")
        cellEd = libDoc.CellEditor
        cellDB = cellEd.ActiveDatabase
        'cellDB = cellEd.OpenDatabase(libDoc.FullName, False)

        Dim dicCellsRemoved As New Dictionary(Of String, List(Of String))

        For Each oCellPartition As CellEditorAddinLib.Partition In cellDB.Partitions()

            If Not dic_CellsToDelete.ContainsKey(oCellPartition.Name) Then
                Continue For
            End If

            Dim l_CellsRemoved As New List(Of String)
            Dim l_Cells As List(Of String)

            l_Cells = dic_CellsToDelete.Item(oCellPartition.Name)

            For Each oCell As CellEditorAddinLib.Cell In oCellPartition.Cells  ' process each cell in the partition

                If l_Cells.Contains(oCell.Name) Then
                    l_CellsRemoved.Add(oCell.Name)
                    oCell.Delete()

                End If

            Next

            'dicCellsRemoved.Add(oCellPartition.Name, l_CellsRemoved)

        Next

        'cellEd.CloseActiveDatabase(True)
        'cellEd.Quit()
        cellDB = Nothing
        cellEd.SaveActiveDatabase()
        cellEd.Quit()
        cellEd = Nothing

        RaiseEvent eCellsFinished(dicCellsRemoved)

    End Sub

    Sub DeleteUnsedParts()

        pedApp = libDoc.PartEditor
        'pedApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        Dim pedDoc As MGCPCBPartsEditor.PartsDB
        'pedDoc = pedApp.OpenDatabaseEx(libDoc.FullName, False)
        pedDoc = pedApp.ActiveDatabaseEx
        Dim oPartition As MGCPCBPartsEditor.Partition

        Dim dicPartsRemoved As New Dictionary(Of String, List(Of String))

        For Each kvp As KeyValuePair(Of String, List(Of String)) In dic_PartsToDelete

            Dim l_Parts As New List(Of String)

            For Each oPartition In pedDoc.Partitions(kvp.Key)

                For Each sPart As String In kvp.Value
                    For Each oPart As MGCPCBPartsEditor.Part In oPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, sPart)
                        l_Parts.Add(oPart.Number)
                        oPart.Delete()

                    Next
                Next

            Next

            'dic_PartsToDelete.Add(kvp.Key, l_Parts)

        Next

        'pedApp.CloseActiveDatabase(True)
        'pedApp.Quit()
        pedDoc = Nothing
        pedApp.SaveActiveDatabase()
        pedApp.Quit()
        pedApp = Nothing

        RaiseEvent ePartsFinished(dicPartsRemoved)

    End Sub

    Sub DeleteUnsedPadstacks()

        PadStackEdDlg = libDoc.PadstackEditor
        oPadStackDB = PadStackEdDlg.ActiveDatabase

        'Dim dicPadstacksRemoved As New Dictionary(Of String, List(Of String))

        'Dim oPadstack As New AAL.Padstack

        For Each kvp As KeyValuePair(Of String, List(Of String)) In dic_PadstacksToDelete

            For Each sPadstack As String In kvp.Value
                If sPadstack = "026VIA" Then Continue For
                Dim pePadStack As PadstackEditorLib.Padstack = oPadStackDB.FindPadstack(sPadstack)

                If Not IsNothing(pePadStack) Then
                    pePadStack.Delete()
                End If

            Next

            'For Each pePadStack As PadstackEditorLib.Padstack In oPadStackDB.Padstacks(oPadstack.StringToObject(kvp.Key))

            '    If kvp.Value.Contains(pePadStack.Name) Then
            '        l_Padstacks.Add(pePadStack.Name)
            '        If Not pePadStack.Name = "026VIA" Then
            '            pePadStack.Delete()
            '        End If

            '    End If

            'Next

            'dicPadstacksRemoved.Add(kvp.Key.ToString(), l_Padstacks)

        Next

        oPadStackDB = Nothing

        PadStackEdDlg.SaveActiveDatabase()
        Try
            PadStackEdDlg.Quit()

        Catch ex As Exception

        End Try

        PadStackEdDlg = Nothing

    End Sub

End Class