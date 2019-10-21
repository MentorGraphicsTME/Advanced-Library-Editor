Imports System.Threading.Tasks
Imports System.Xml
Imports AAL
Imports LibraryManager

Public Class LibraryRead

#Region "Internal Fields"

    Friend xmlDoc As XmlDocument

#End Region

#Region "Private Fields"

    'Dictionary
    Dim dicCellsNoPins As New Dictionary(Of String, List(Of String))

    Dim libApp As LibraryManager.LibraryManagerApp
    Dim lookatlog As Boolean = False

    'Mentor Graphics
    Dim oPad As PadstackEditorLib.Pad

    'String
    Dim sLogPath As String

#End Region

#Region "Public Events"

    'Event
    Event eAddCell()

    Event eAddPart()

    Event eAddSymbol()

    Event eCellsComplete(ByVal CellList As Dictionary(Of String, List(Of String)), ByVal dic_CellsByPartition As Dictionary(Of String, List(Of String)))

    Event eCellsReportComplete(ByVal CellsReport As Dictionary(Of String, AAL.CellPartition))

    Event eOrphanCell()

    Event eOrphanCellsComplete(ByVal PartitionName As String, ByVal Orphans As List(Of String), ByVal UsedPadsatcks As Dictionary(Of String, List(Of String)))

    Event eOrphanPart()

    Event eOrphanPartsComplete(ByVal PartitionName As String, ByVal OrphanReport As List(Of String), ByVal MissingSymbolsReport As List(Of String), ByVal MissingCellsReport As List(Of String))

    Event eOrphanSymbol()

    Event eOrphanSymbolsComplete(ByVal PartitionName As String, ByVal Orphans As List(Of String))

    Event ePadstacksComplete(dicPadstacksByType As Dictionary(Of String, List(Of String)), dicPadstacks As Dictionary(Of String, String))

    Event ePartMissingCell()

    Event ePartMissingSymbol()

    Event ePartsComplete(ByVal PartList As Dictionary(Of String, String), ByVal dic_PartsByPartition_In As Dictionary(Of String, List(Of String)))

    Event ePartsReportComplete(ByVal Partitions As AAL.PartPartitions)

    Event eReturnPartPropeties(partition As String, dic_PartProperties As Dictionary(Of String, List(Of String)), dic_PartNonCommonProperties As Dictionary(Of String, List(Of String)))

    Event eReturnPDBXML(ByVal PDBXML As Xml.XmlDocument, ByVal PDB As String)

    Event eReuseBlocksComplete(ByVal ReuseBlockList As Dictionary(Of String, String), ByVal dic_ReuseBlocksByPartition As Dictionary(Of String, List(Of String)))

    Event eSymbolsComplete(ByVal SymbolList As Dictionary(Of String, List(Of String)), ByVal dic_SymbolsByPartition As Dictionary(Of String, List(Of String)))

    Event eSymbolsReportComplete(ByVal SymbolsReport As Dictionary(Of String, AAL.SymbolPartition))

    Event eUpdateStatus(status As String)

#End Region

#Region "Public Properties"

    Property alCellsNoSymbols As New ArrayList()

    Property alNoCellsNoSymbols As New ArrayList()

    Property alSymbolsNoCells As New ArrayList()

    Property alUnusedCells As New ArrayList()

    'Arraylist
    Property alUnusedSymbols As New ArrayList()

    'Boolean
    Property bFin As Boolean = False

    'List
    Property CommonProperties As New List(Of String)

    'Dim pedDoc As MGCPCBPartsEditor.PartsDB
    Property libDoc As LibraryManager.IMGCLMLibrary

    Property reservedCellPartitions As Integer = 0

    'Integer
    Property reservedPartPartitions As Integer = 0

    Property reservedSymbolPartitions As Integer = 0

#End Region

#Region "Public Methods"

    Sub GetOrphanCells(ByVal sPartition As String)

        For Each lmPartition As LibraryManager.IMGCLMPartition In libDoc.Partitions(LibraryManager.MGCLMObjectType.kCELL)      'Get the collection of Cell Partitions

            If lmPartition.Name = sPartition Then
                Dim lOrphanCells As New List(Of String)
                Dim dicPadstacks As New Dictionary(Of String, List(Of String))
                For Each cell As LibraryManager.IMGCLMCell In lmPartition
                    If cell.AssociatedParts.Count = 0 Then

                        RaiseEvent eOrphanCell()
                        lOrphanCells.Add(cell.Name)
                    End If

                    For Each lmPadstack As LibraryManager.IMGCLMPadstack In cell.AssociatedPadstacks

                        Dim lPadstacks As List(Of String)
                        Dim sPadstackType As String

                        If String.IsNullOrEmpty(lmPadstack.PartitionName) Then
                            sPadstackType = "None"
                        Else
                            sPadstackType = lmPadstack.PartitionName
                        End If

                        If dicPadstacks.ContainsKey(sPadstackType) Then
                            lPadstacks = dicPadstacks.Item(sPadstackType)
                        Else
                            lPadstacks = New List(Of String)
                        End If

                        If Not lPadstacks.Contains(lmPadstack.Name) Then
                            lPadstacks.Add(lmPadstack.Name)
                        End If

                        dicPadstacks.Item(sPadstackType) = lPadstacks

                    Next

                Next

                RaiseEvent eOrphanCellsComplete(sPartition, lOrphanCells, dicPadstacks)

            End If

        Next

    End Sub

    Sub GetOrphanParts(ByVal sPartition As String)

        For Each lmPartition As LibraryManager.IMGCLMPartition In libDoc.Partitions(LibraryManager.MGCLMObjectType.kPART)      'Get the collection of Part Partitions

            If lmPartition.Name = sPartition Then

                Dim lOrphanParts As New List(Of String)
                Dim lMissingSymbols As New List(Of String)
                Dim lMissingCells As New List(Of String)
                For Each lmPart As LibraryManager.MGCLMPart In lmPartition

                    If lmPart.AssociatedCells.Count = 0 And lmPart.AssociatedSymbols.Count = 0 Then
                        RaiseEvent eOrphanPart()
                        lOrphanParts.Add(lmPart.Name)
                    ElseIf lmPart.AssociatedCells.Count = 0 Then
                        RaiseEvent ePartMissingCell()
                        lMissingCells.Add(lmPart.Name)
                    ElseIf lmPart.AssociatedSymbols.Count = 0 Then
                        RaiseEvent ePartMissingSymbol()
                        lMissingSymbols.Add(lmPart.Name)
                    End If

                Next

                RaiseEvent eOrphanPartsComplete(sPartition, lOrphanParts, lMissingSymbols, lMissingCells)

                Exit Sub

            End If

        Next

    End Sub

    Sub GetOrphanSymbols(ByVal sPartition As String)

        For Each lmPartition As LibraryManager.IMGCLMPartition In libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL)      'Get the collection of Symbol Partitions
            If lmPartition.Name = sPartition Then
                Dim lOrphanSymbols As New List(Of String)
                For Each symbol As LibraryManager.IMGCLMSymbol In lmPartition

                    If symbol.AssociatedParts.Count = 0 Then

                        RaiseEvent eOrphanSymbol()
                        lOrphanSymbols.Add(symbol.Name)

                    End If

                Next
                RaiseEvent eOrphanSymbolsComplete(sPartition, lOrphanSymbols)
                Exit Sub
            End If

        Next

    End Sub

    Sub readCellandPinData(ByRef pedApp As MGCPCBPartsEditor.IMGCPDBPartsEditorDlg, ByRef dicPEDCellPin As Dictionary(Of String, ArrayList))

        Dim pedDoc As MGCPCBPartsEditor.IMGCPDBPartsDB
        pedDoc = pedApp.ActiveDatabaseEx

        If pedDoc.Partitions.Count > 0 Then
            For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions    'Step through each part partition in the parts editor

                For Each pedPart As MGCPCBPartsEditor.Part In pdbPartition.Parts()

                    For Each cellref As MGCPCBPartsEditor.CellReference In pedPart.CellReferences

                        Dim alPinNumbers As New ArrayList
                        alPinNumbers.AddRange(cellref.PinNumbers)

                        Try
                            dicPEDCellPin.Add(cellref.Name, alPinNumbers)
                        Catch ex As Exception

                        End Try

                    Next
                Next
            Next

        End If

    End Sub

    Sub ReadLMCCellPartitions(ByVal Report As Boolean)

        Try
            Dim l_CellPartitions As New List(Of String)
            Dim dic_CellList As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
            Dim dic_CellsByPartition As New Dictionary(Of String, List(Of String))
            Dim dicCellsReport As New Dictionary(Of String, AAL.CellPartition)

            If Report = True Then
                readpadstacks()
            End If

            Dim tasks As New List(Of Task(Of AAL.CellPartition))

            For Each lmPartition As LibraryManager.IMGCLMPartition In libDoc.Partitions(LibraryManager.MGCLMObjectType.kCELL)      'Get the collection of Cell Partitions

                If lmPartition.Reservation <> vbNullString Then
                    reservedCellPartitions += 1
                End If

                l_CellPartitions.Add(lmPartition.Name)

                tasks.Add(Task.Factory.StartNew(Of AAL.CellPartition)(Function() ReadLMCCells(lmPartition, Report)))

            Next

            Task.WaitAll(tasks.ToArray())

            For Each readTask As Task(Of AAL.CellPartition) In tasks
                Dim aalPartition As AAL.CellPartition = readTask.Result

                For Each aalCell As AAL.Cell In aalPartition.Cells

                    Dim l_Partitions As List(Of String)
                    If dic_CellList.ContainsKey(aalCell.Name) Then
                        l_Partitions = dic_CellList(aalCell.Name)
                    Else
                        l_Partitions = New List(Of String)
                    End If

                    l_Partitions.Add(aalPartition.Name)
                    dic_CellList(aalCell.Name) = l_Partitions

                Next

                dic_CellsByPartition.Add(aalPartition.Name, aalPartition.Keys.ToList())

            Next

            'If Report = True Then

            ' For Each aalCel As AAL.Cell In

            ' dicCellsReport.Add(lmPartition.Name, dicCellReport)

            ' End If

            'frmMain.swLog.WriteLine()

            If Report = True Then
                RaiseEvent eCellsReportComplete(dicCellsReport)
            Else
                RaiseEvent eCellsComplete(dic_CellList, dic_CellsByPartition)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: Read LMC Cell Partitions" & Environment.NewLine & Environment.NewLine & ex.Message.ToString())
        End Try

    End Sub

    Sub ReadLMCPartPartitions(ByVal Report As Boolean)
        Try
            Dim dic_PartList As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
            Dim dic_PartsByPartition As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
            Dim l_PartPartitions As New List(Of String)
            Dim aalPartitions As New AAL.PartPartitions

            Dim tasks As New List(Of Task(Of AAL.PartPartition))

            For Each lmPartition As LibraryManager.IMGCLMPartition In libDoc.Partitions(LibraryManager.MGCLMObjectType.kPART)      'Get the collection of Part Partitions
                If lmPartition.Reservation <> vbNullString Then
                    reservedPartPartitions += 1
                End If

                l_PartPartitions.Add(lmPartition.Name)

                tasks.Add(Task.Factory.StartNew(Of AAL.PartPartition)(Function() ReadLMCParts(lmPartition, Report)))

            Next

            Task.WaitAll(tasks.ToArray())

            For Each readTask As Task(Of AAL.PartPartition) In tasks
                Dim dic_PartInPartition As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                Dim aalPartition As AAL.PartPartition = readTask.Result

                For Each part As AAL.Part In aalPartition.Parts
                    dic_PartList.Add(part.Number, aalPartition.Name)
                    dic_PartInPartition.Add(part.Number, aalPartition.Name)
                Next

                dic_PartsByPartition.Add(aalPartition.Name, dic_PartInPartition.Keys.ToList())

                If Report = True Then
                    aalPartitions.Add(aalPartition)
                End If
            Next

            If Report = True Then
                RaiseEvent ePartsReportComplete(aalPartitions)
            Else
                RaiseEvent ePartsComplete(dic_PartList, dic_PartsByPartition)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: Read LMC Part Partitions" & Environment.NewLine & Environment.NewLine & ex.Message.ToString())
        End Try

    End Sub

    Sub ReadLMCSymbolPartitions(ByVal Report As Boolean)

        Try
            Dim dicSymbolList As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
            Dim dicSymbolsByPartition As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
            Dim dicSymbolsReport As New Dictionary(Of String, AAL.SymbolPartition)

            Dim tasks As New List(Of Task(Of AAL.SymbolPartition))

            For Each lmPartition As LibraryManager.IMGCLMPartition In libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL)      'Get the collection of Symbol Partitions

                If lmPartition.Reservation <> vbNullString Then
                    reservedSymbolPartitions += 1
                End If

                tasks.Add(Task.Factory.StartNew(Of AAL.SymbolPartition)(Function() ReadLMCSymbols(lmPartition, Report)))

            Next

            Task.WaitAll(tasks.ToArray())

            For Each readTask As Task(Of AAL.SymbolPartition) In tasks
                Dim aalPartition As AAL.SymbolPartition = readTask.Result

                For Each aalSymbol As AAL.Symbol In aalPartition.Symbols

                    Dim l_Partitions As List(Of String)
                    If dicSymbolList.ContainsKey(aalSymbol.Name) Then
                        l_Partitions = dicSymbolList(aalSymbol.Name)
                    Else
                        l_Partitions = New List(Of String)
                    End If

                    l_Partitions.Add(aalPartition.Name)
                    dicSymbolList(aalSymbol.Name) = l_Partitions

                Next

                dicSymbolsByPartition.Add(aalPartition.Name, aalPartition.Keys.ToList())

                If Report = True Then
                    dicSymbolsReport.Add(aalPartition.Name, aalPartition)
                End If

            Next

            If Report = True Then
                RaiseEvent eSymbolsReportComplete(dicSymbolsReport)
            Else
                RaiseEvent eSymbolsComplete(dicSymbolList, dicSymbolsByPartition)
            End If
        Catch ex As Exception
            MessageBox.Show("Error: Read LMC Symbol Partitions" & Environment.NewLine & Environment.NewLine & ex.Message.ToString())
        End Try

    End Sub

    Sub readpadstacks()
        Try
            Dim dicPadstacksByType As New Dictionary(Of String, List(Of String))
            Dim dicPadstacks As New Dictionary(Of String, String)

            Dim oPadstack As New AAL.Padstack

            For Each lmPadstackType As LibraryManager.IMGCLMPartition In libDoc.Partitions(LibraryManager.MGCLMObjectType.kPADSTACK)

                Dim l_Padstacks As New List(Of String)

                If lmPadstackType.Name = "All" Then Continue For

                For Each lmPadstack As LibraryManager.IMGCLMPadstack In lmPadstackType

                    l_Padstacks.Add(lmPadstack.Name)
                    dicPadstacks.Add(lmPadstack.Name, lmPadstack.PartitionName)

                Next

                dicPadstacksByType.Add(lmPadstackType.Name, l_Padstacks)

            Next

            RaiseEvent ePadstacksComplete(dicPadstacksByType, dicPadstacks)
        Catch ex As Exception
            MessageBox.Show("Error: Read Padstacks" & Environment.NewLine & Environment.NewLine & ex.Message.ToString())
        End Try

    End Sub

    Sub readPartCellData(ByRef pedApp As MGCPCBPartsEditor.IMGCPDBPartsEditorDlg, ByVal dicPEDPartsList As Dictionary(Of String, Object))

        Dim pedDoc As MGCPCBPartsEditor.IMGCPDBPartsDB
        pedDoc = pedApp.ActiveDatabaseEx

        If pedDoc.Partitions.Count > 0 Then
            For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions    'Step through each part partition in the parts editor

                For Each pedPart As MGCPCBPartsEditor.Part In pdbPartition.Parts

                    Dim AssociatedCells As MGCPCBPartsEditor.CellReferences
                    Dim cellref As MGCPCBPartsEditor.CellReference
                    AssociatedCells = pedPart.CellReferences
                    Dim alPartCells As New ArrayList

                    For Each cellref In AssociatedCells
                        Dim cellAtts = New Object() {cellref.Name, cellref.Type}
                        alPartCells.Add(cellAtts)
                    Next

                    dicPEDPartsList.Add(pedPart.Number, alPartCells)

                Next
            Next

        End If

    End Sub

    Sub ReadPDBParts(ByRef pedApp As MGCPCBPartsEditor.IMGCPDBPartsEditorDlg)
        Dim oPartitions As New AAL.PartPartitions
        Dim pedDoc As MGCPCBPartsEditor.IMGCPDBPartsDB
        pedDoc = pedApp.ActiveDatabaseEx

        For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions()

            Dim l_Parts As New List(Of String)

            Dim oPartition As New AAL.PartPartition
            oPartition.Name = pdbPartition.Name

            For Each pdbPart As MGCPCBPartsEditor.Part In pdbPartition.Parts

                Dim oPart As New AAL.Part

                oPart.Number = pdbPart.Number
                oPart.Name = pdbPart.Name
                oPart.Name = pdbPart.Label
                oPart.Description = pdbPart.Description

                If pdbPart.PinMapping.CellReferences.Count > 0 Then
                    For Each pdbCell As MGCPCBPartsEditor.CellReference In pdbPart.PinMapping.CellReferences
                        oPart.Cells.Item(pdbCell.Name) = Nothing
                    Next

                End If

                If pdbPart.PinMapping.SymbolReferences.Count > 0 Then

                    For Each pdbSymbol As MGCPCBPartsEditor.SymbolReference In pdbPart.PinMapping.SymbolReferences
                        oPart.Symbols.Item(pdbSymbol.Name) = Nothing
                    Next

                End If

                For Each pdbProperty As MGCPCBPartsEditor.Property In pdbPart.Properties
                    Try
                        oPart.Properties.Item(pdbProperty.Name) = pdbProperty.Value
                    Catch ex As Exception
                        oPart.Properties.Item(pdbProperty.Name) = "ERROR: Corrupted Value"
                    End Try
                Next

                oPartition.Add(oPart)
            Next

            oPartitions.Add(oPartition)

        Next

        RaiseEvent ePartsReportComplete(oPartitions)

    End Sub

    Sub readPDBProperties(ByVal pedPartition As MGCPCBPartsEditor.Partition)

        Dim dic_PartProperties As New Dictionary(Of String, List(Of String))
        Dim dic_PartNonCommonProperties As New Dictionary(Of String, List(Of String))

        For Each pedPart As MGCPCBPartsEditor.Part In pedPartition.Parts

            Dim l_Properties As New List(Of String)
            Dim l_NonCommonProperties As New List(Of String)

            For Each pdbProperty As MGCPCBPartsEditor.Property In pedPart.Properties

                If Not l_Properties.Contains(pdbProperty.Name) Then l_Properties.Add(pdbProperty.Name)

                If Not CommonProperties.Contains(pdbProperty.Name) Then l_NonCommonProperties.Add(pdbProperty.Name)

            Next

            dic_PartProperties.Add(pedPart.Number, l_Properties)
            dic_PartNonCommonProperties.Add(pedPart.Number, l_NonCommonProperties)

        Next

        RaiseEvent eReturnPartPropeties(pedPartition.Name, dic_PartProperties, dic_PartNonCommonProperties)

    End Sub

    Sub ReadReuseBlocks(ByVal sPartition As String)

        Dim dic_PartList As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
        Dim dic_ReuseBlcoksByPartition As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

        For Each lmPartition As LibraryManager.IMGCLMPartition In libDoc.Partitions(LibraryManager.MGCLMObjectType.kREUSEBLOCK)      'Get the collection of Part Partitions

            Dim l_ReuseBlocks As New List(Of String)

            For Each lmReuseBlock As LibraryManager.MGCLMReuseBlock In lmPartition

                l_ReuseBlocks.Add(lmReuseBlock.Name)
                dic_PartList.Add(lmReuseBlock.Name, lmPartition.Name)

            Next

            If l_ReuseBlocks.Count > 0 Then
                dic_ReuseBlcoksByPartition.Add(lmPartition.Name, l_ReuseBlocks)
            End If

        Next

        RaiseEvent eReuseBlocksComplete(dic_PartList, dic_ReuseBlcoksByPartition)

    End Sub

#End Region

#Region "Internal Methods"

    Friend Sub exportPDBtoXML(pdbPartition As MGCPCBPartsEditor.Partition)

        Dim l_Parts As New List(Of String)

        Dim oPartition As New AAL.PartPartition
        oPartition.Name = pdbPartition.Name

        For Each pdbPart As MGCPCBPartsEditor.Part In pdbPartition.Parts

            Dim aalPart As New AAL.Part(pdbPart)

            aalPart.toXML(xmlDoc)

        Next

        RaiseEvent eReturnPDBXML(xmlDoc, pdbPartition.Name)
    End Sub

#End Region

#Region "Private Methods"

    Private Function ReadLMCCells(lmPartition As IMGCLMPartition, report As Boolean) As CellPartition
        Dim aalCellPartition As New CellPartition
        aalCellPartition.Name = lmPartition.Name

        Dim listOfNoPinCells As New List(Of String)
        Dim dicCellReport As New Dictionary(Of AAL.Cell, List(Of String))

        For Each lmCell As LibraryManager.IMGCLMCell In lmPartition
            'frmMain.swLog.WriteLine(vbTab & cell.Name)

            RaiseEvent eAddCell()

            Dim aalCell As New AAL.Cell
            aalCell.Name = lmCell.Name

            If report = True Then

                For Each part As LibraryManager.IMGCLMPart In lmCell.AssociatedParts
                    aalCell.AssociatedParts.Add(part.Name)
                Next

                For Each padstack As LibraryManager.IMGCLMPadstack In lmCell.AssociatedPadstacks

                    If Not aalCell.Padstacks.Contains(padstack.PartitionName & ":" & padstack.Name) Then aalCell.Padstacks.Add(padstack.PartitionName & ":" & padstack.Name)

                    'If alPadStackNames.Contains(padstack.Name) Then

                    ' alPadStackNames.Remove(padstack.Name)

                    'End If
                Next
            End If

            aalCellPartition.Add(aalCell)

            'l_Cells.Add(lmCell.Name)
            '    Dim l_Partitions As List(Of String)
            '    If dic_CellList.ContainsKey(lmCell.Name) Then
            '        l_Partitions = dic_CellList.Item(lmCell.Name)
            '    Else
            '        l_Partitions = New List(Of String)
            '    End If

            ' l_Partitions.Add(lmPartition.Name)

            ' dic_CellList.Item(lmCell.Name) = l_Partitions

            'End If

        Next

        Return aalCellPartition
    End Function

    Private Function ReadLMCParts(lmPartition As IMGCLMPartition, report As Boolean) As AAL.PartPartition

        Dim l_Parts As New List(Of String)

        Dim aalPartition As New AAL.PartPartition

        aalPartition.Name = lmPartition.Name

        For Each lmPart As LibraryManager.MGCLMPart In lmPartition

            RaiseEvent eAddPart()

            Dim oPart As New AAL.Part

            oPart.Number = lmPart.Name
            If report = True Then
                Dim AssociatedCells
                AssociatedCells = lmPart.AssociatedCells

                If AssociatedCells.Count > 0 Then
                    For Each cell As LibraryManager.IMGCLMCell In AssociatedCells
                        oPart.Cells.Add(cell.PartitionName & ":" & cell.Name, Nothing)
                    Next

                    If lmPart.AssociatedSymbols.Count = 0 Then
                        alCellsNoSymbols.Add(lmPart.Name)
                    End If
                Else

                    If lmPart.AssociatedSymbols.Count = 0 Then
                        alNoCellsNoSymbols.Add(lmPart.Name)
                    Else
                        alSymbolsNoCells.Add(lmPart.Name)
                    End If

                End If

                If lmPart.AssociatedSymbols.Count > 0 Then

                    For Each symbol As LibraryManager.IMGCLMSymbol In lmPart.AssociatedSymbols
                        oPart.Symbols.Add(symbol.PartitionName, symbol.Name, Nothing)
                    Next

                End If
            End If
            aalPartition.Add(oPart)

            l_Parts.Add(lmPart.Name)
        Next

        Return aalPartition

    End Function

    Private Function ReadLMCSymbols(lmPartition As IMGCLMPartition, report As Boolean) As SymbolPartition
        Dim aalPartition As New AAL.SymbolPartition(lmPartition.Name)
        For Each symbol As LibraryManager.IMGCLMSymbol In lmPartition

            RaiseEvent eAddSymbol()

            Dim aalSymbol As New AAL.Symbol
            aalSymbol.Name = symbol.Name

            If report = True Then

                For Each part As LibraryManager.MGCLMPart In symbol.AssociatedParts
                    aalSymbol.AssociatedParts.Add(part.PartitionName & ":" & part.Name)
                Next

            End If

            aalPartition.Add(aalSymbol)

        Next

        Return aalPartition
    End Function

#End Region

End Class