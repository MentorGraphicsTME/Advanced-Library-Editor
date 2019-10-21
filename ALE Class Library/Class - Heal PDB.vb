Imports System.IO

Public Class Heal_PDB

#Region "Private Fields"

    'Arraylist
    Dim alCellPinNumbers As New ArrayList

    Dim b_HealPart As Boolean = True
    Dim bBuildSucces As Boolean
    Dim bCommitPart As Boolean
    Dim bSymbolRename As Boolean = False

    'Boolean
    Dim CancelNCPin, CopyPinMappingFailed As Boolean

    'Dim l_CellName As New List(Of String)
    Dim l_AlternateSymbolReferences As New List(Of MGCPCBPartsEditor.SymbolReference)

    'List
    Dim l_SymbolName As New List(Of String)

    Dim nodePart As Xml.XmlNode
    Dim origPartLabel, origPartName
    Dim oWriteDiagnostics As New AAL.XMLMapping

    'Integers
    Dim partType As Integer

    'Local Variables
    Dim pedHealApp As New MGCPCBPartsEditor.PartsEditorDlg

    'Strings
    Dim sPartitionName As String

    Dim symbol As LibraryManager.IMGCLMSymbol

#End Region

#Region "Public Events"

    Event eUpdateCount()

    'Events
    Event eUpdateStatus(status As String)

    Event LogError()

    'Events
    Event LogNote()

    Event LogWarning()

#End Region

#Region "Public Properties"

    Property bAddCells As Boolean

    Property bAddNCPins As Boolean = False

    Property bAddSymbols As Boolean

    Property bAddSymbolsCells As Boolean

    Property bRemoveAlternateCells As Boolean = False

    Property bRemoveSpaceFromCell As Boolean = False

    Property bRepairErrors As Boolean = False

    'Boolean
    Property bSupplementData As Boolean = False

    Property bUpdatePartType As Boolean = False

    Property bUpdateSymPartition As Boolean = True

    Property dicAltCellInfo As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

    Property dicAlternateCells As New Dictionary(Of String, AAL.Part)(StringComparer.OrdinalIgnoreCase)

    Property dicAlternateSymbols As New Dictionary(Of String, List(Of AAL.Symbol))(StringComparer.OrdinalIgnoreCase)

    Property dicAltSymbolInfo As New Dictionary(Of String, AAL.Symbol)(StringComparer.OrdinalIgnoreCase)

    Property dicDxdPartData As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

    'Dictionaries
    Property dicPEDCellPin As New Dictionary(Of String, ArrayList)(StringComparer.OrdinalIgnoreCase)

    Property dicPrjPartData As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

    Property dicRenameCells As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    Property dicRenameSymbols As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Property HealLog As New HealInfo

    Property LibraryData As Data

    'Objects
    Property oResults As Object

    Property PartsToHeal As New List(Of String)

    Property s_CellCase As String = Nothing

    Property s_SymbolCase As String = Nothing

    Property sCellPrefix As String

    Property sMainCell As String

    Property Verbose As Boolean = False

    Property xmlDiagnosticsDoc As Xml.XmlDocument

#End Region

#Region "Public Methods"

    Function CopyCellReferences(ByRef pdbNewPart As MGCPCBPartsEditor.Part, ByVal pdbPart As MGCPCBPartsEditor.Part, ByRef oLogInfo As Log)

        alCellPinNumbers.Clear()

        Dim iCellFailedCount As Integer = 0
        'Dim FoundTopCell As Boolean = False
        Dim TopCell As String
        Dim BotCell As String
        Dim AltCells As New List(Of String)
        Dim alMasterCellPinNumbers As New ArrayList

        For Each pdbCellReference As MGCPCBPartsEditor.CellReference In pdbPart.CellReferences

            Dim alTempCellPinNumbers As New ArrayList
            alTempCellPinNumbers.AddRange(pdbCellReference.PinNumbers)

            If (alMasterCellPinNumbers.Count = 0) Then
                alMasterCellPinNumbers.AddRange(pdbPart.CellReferences.Item(1).PinNumbers)
            End If

            If Not (alMasterCellPinNumbers.Count = alTempCellPinNumbers.Count) Then
                If String.IsNullOrEmpty(pdbCellReference.Name) Then
                    Dim problem As Boolean = True
                End If
                oLogInfo.Add(Log.Type.Other, "Multiple cells are referenced but their pin counts do not match. See master cell: " & TopCell & "(" & alMasterCellPinNumbers.Count & ") and alternate cell: " & pdbCellReference.Name & "(" & alTempCellPinNumbers.Count() & ")")
                RaiseEvent LogError()
                Return False
            End If

            Dim cellRefName As String

            If dicRenameCells.ContainsKey(pdbCellReference.Name) Then
                cellRefName = dicRenameCells.Item(pdbCellReference.Name)
                oLogInfo.Add(Log.Type.Note, "Changing cell " & pdbCellReference.Name & " to " & cellRefName)
                RaiseEvent LogNote()
            Else
                If sCellPrefix = "" Then
                    cellRefName = pdbCellReference.Name
                Else
                    cellRefName = sCellPrefix & pdbCellReference.Name
                End If

                If bRemoveSpaceFromCell = True Then
                    cellRefName = cellRefName.Replace(" ", "_")
                End If

                If IsNothing(s_CellCase) Then

                ElseIf s_CellCase = "Lowercase" Then
                    cellRefName = LCase(cellRefName)
                ElseIf s_CellCase = "Uppercase" Then
                    cellRefName = UCase(cellRefName)
                End If
            End If

            cellRefName = cellRefName.Replace("\", "_")
            'cellRefName = cellRefName.Replace("/", "_")
            cellRefName = cellRefName.Replace(",", "_")
            cellRefName = cellRefName.Replace("(", "_")
            cellRefName = cellRefName.Replace(")", "_")

            Dim cellRefType As MGCPCBPartsEditor.EPDBCellReferenceType = pdbCellReference.Type

            If Not AltCells.Contains(cellRefName, StringComparer.OrdinalIgnoreCase) And Not String.Compare(TopCell, cellRefName, True) = 0 And Not String.Compare(BotCell, cellRefName, True) = 0 Then

                If LibraryData.CellList.ContainsKey(cellRefName) Then
                    If pdbCellReference.Type = MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefTop Then
                        TopCell = cellRefName
                    ElseIf pdbCellReference.Type = MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefBottom Then
                        BotCell = cellRefName
                    Else
                        AltCells.Add(cellRefName)
                    End If
                    'pdbNewPart.PinMapping.PutCellReference(cellRefName, cellRefType)
                Else
                    Select Case cellRefType

                        Case MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefAll
                            oLogInfo.Add(Log.Type.Warning, "Cell " & cellRefName & " was not found in the central library. Removing from pdbPart.")
                            RaiseEvent LogWarning()

                        Case MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefAlternate
                            oLogInfo.Add(Log.Type.Warning, "Alternate Cell " & cellRefName & " was not found in the central library. Removing from pdbPart.")
                            RaiseEvent LogWarning()

                        Case MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefBottom
                            oLogInfo.Add(Log.Type.Warning, "Bottom Cell " & cellRefName & " was not found in the central library. Removing from pdbPart.")
                            RaiseEvent LogWarning()

                        Case MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefTop
                            oLogInfo.Add(Log.Type.Warning, "Top Cell " & cellRefName & " was not found in the central library. Removing from pdbPart.")
                            RaiseEvent LogWarning()

                    End Select

                    iCellFailedCount += 1

                    'If iCellFailedCount = pdbPart.CellReferences.Count Then

                    ' Return True

                    'End If

                End If
            Else
                oLogInfo.Add(Log.Type.Warning, "Removing cell " & cellRefName & " because it is already used on the part")
                RaiseEvent LogWarning()

            End If

        Next

        If bSupplementData = True Then

            If dicPrjPartData.ContainsKey(pdbPart.Number) Then

                Dim oPartAtts As Object = dicPrjPartData.Item(pdbPart.Number)

                For Each cellRefName As String In oPartAtts.Cells

                    If bRemoveSpaceFromCell = True Then
                        cellRefName = cellRefName.Replace(" ", "_")
                    End If

                    cellRefName = cellRefName.Replace("\", "_")
                    'cellRefName = cellRefName.Replace("/", "_")
                    cellRefName = cellRefName.Replace(",", "_")
                    cellRefName = cellRefName.Replace("(", "_")
                    cellRefName = cellRefName.Replace(")", "_")

                    If cellRefName.Contains(":") Then
                        Dim NameSplit As String() = cellRefName.Split(":")
                        cellRefName = NameSplit(1)
                    End If

                    If Not AltCells.Contains(cellRefName, StringComparer.OrdinalIgnoreCase) And Not String.Compare(TopCell, cellRefName, True) = 0 And Not String.Compare(BotCell, cellRefName, True) = 0 Then
                        If String.IsNullOrEmpty(TopCell) Then
                            TopCell = cellRefName
                            oLogInfo.Add(Log.Type.Note, "Adding cell " & cellRefName & " as the Top Cell.")
                            RaiseEvent LogNote()
                        Else
                            AltCells.Add(cellRefName)
                            oLogInfo.Add(Log.Type.Note, "Adding cell " & cellRefName & " as an Alternate Cell.")
                            RaiseEvent LogNote()
                        End If
                    End If
                Next

            End If

        ElseIf bAddCells = True Then

            Dim oSupplementPart As AAL.Part = dicAlternateCells(pdbPart.Number)

            If Not String.IsNullOrEmpty(oSupplementPart.TopCell) Then

                If String.IsNullOrEmpty(TopCell) Then
                    oLogInfo.Add(Log.Type.Note, "Adding cell " & oSupplementPart.TopCell & " as the top cell.")
                Else
                    oLogInfo.Add(Log.Type.Note, "Changing top cell from " & TopCell & " to " & oSupplementPart.TopCell & ".")
                    AltCells.Add(TopCell)
                End If

                TopCell = oSupplementPart.TopCell

                RaiseEvent LogNote()
            End If

            If Not String.IsNullOrEmpty(oSupplementPart.BotCell) Then

                If String.IsNullOrEmpty(BotCell) Then
                    oLogInfo.Add(Log.Type.Note, "Adding cell " & oSupplementPart.BotCell & " as the bottom cell.")
                Else
                    oLogInfo.Add(Log.Type.Note, "Changing bottom cell from " & BotCell & " to " & oSupplementPart.BotCell & ".")
                    AltCells.Add(BotCell)
                End If

                BotCell = oSupplementPart.BotCell

                RaiseEvent LogNote()
            End If

            For Each sCell As String In oSupplementPart.Cells.Keys

                sCell = sCell.Replace("\", "_")
                sCell = sCell.Replace("/", "_")
                sCell = sCell.Replace(",", "_")
                sCell = sCell.Replace("(", "_")
                sCell = sCell.Replace(")", "_")

                If Not AltCells.Contains(sCell, StringComparer.OrdinalIgnoreCase) And Not String.Compare(TopCell, sCell, True) = 0 And Not String.Compare(BotCell, sCell, True) = 0 Then

                    If String.IsNullOrEmpty(TopCell) Then
                        oLogInfo.Add(Log.Type.Note, "Adding cell " & oSupplementPart.TopCell & " as the top cell.")
                        RaiseEvent LogNote()
                        TopCell = sCell
                        Continue For
                    End If

                    AltCells.Add(sCell)
                    oLogInfo.Add(Log.Type.Note, "Adding cell " & sCell & " as an Alternate Cell.")
                    RaiseEvent LogNote()
                End If

            Next

        End If

        alMasterCellPinNumbers.Clear()

        If Not String.IsNullOrEmpty(TopCell) Then
            Dim cellRef As MGCPCBPartsEditor.CellReference = pdbNewPart.PinMapping.PutCellReference(TopCell, MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefTop)
            alMasterCellPinNumbers.AddRange(cellRef.PinNumbers)

            Try
                pdbNewPart.PinMapping.Commit()
            Catch ex As Exception
                oLogInfo.Add(Log.Type.Other, "Unable to commit top cell " & TopCell)
                RaiseEvent LogError()
                Return False
            End Try

        End If

        If Not String.IsNullOrEmpty(BotCell) Then
            Dim cellRef As MGCPCBPartsEditor.CellReference = pdbNewPart.PinMapping.PutCellReference(BotCell, MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefBottom)

            Dim alTempCellPinNumbers As New ArrayList
            alTempCellPinNumbers.AddRange(cellRef.PinNumbers)

            Try
                pdbNewPart.PinMapping.Commit()
            Catch ex As Exception
                oLogInfo.Add(Log.Type.Other, "Unable to commit bottom cell " & BotCell)
                RaiseEvent LogError()
                Return False
            End Try

            If alMasterCellPinNumbers.Count = 0 Then
                alMasterCellPinNumbers.AddRange(cellRef.PinNumbers)
            ElseIf Not alMasterCellPinNumbers.Count = alTempCellPinNumbers.Count Then

                pdbNewPart.CellReferences(MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefBottom).Remove(pdbNewPart.CellReferences(MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefBottom).Count)
                oLogInfo.Add(Log.Type.Other, "Multiple cells are referenced but their pin counts do not match. See master cell: " & TopCell & "(" & alMasterCellPinNumbers.Count & ") and bottom cell: " & BotCell & "(" & alTempCellPinNumbers.Count() & ")")
                RaiseEvent LogError()
            End If

        End If

        If AltCells.Count > 0 Then
            For Each altcell In AltCells  ' Scott 6/26/15: changed this loop since AltCells is a simple list, not an arraylist

                If String.IsNullOrEmpty(TopCell) Then ' if no top cell remains after cleanup above, make the first alternate the top cell

                    ' Scott 6/25/15: use of "AltCells.IndexOf(i)" was incorrect for list type

                    Dim cellRef As MGCPCBPartsEditor.CellReference = pdbNewPart.PinMapping.PutCellReference(altcell, MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefTop)
                    alMasterCellPinNumbers.AddRange(cellRef.PinNumbers)
                    TopCell = altcell   ' set this first alternate to be the top cell name
                Else ' add an alternate cell only if the pin count matches the top cell

                    Dim cellRef As MGCPCBPartsEditor.CellReference = pdbNewPart.PinMapping.PutCellReference(altcell, MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefAlternate)

                    Dim alTempCellPinNumbers As New ArrayList
                    alTempCellPinNumbers.AddRange(cellRef.PinNumbers)

                    If Not alMasterCellPinNumbers.Count = alTempCellPinNumbers.Count Then   ' if the alternate pin count doesn't match the top cell, remove the alternate we just added

                        pdbNewPart.CellReferences(MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefAlternate).Remove(pdbNewPart.CellReferences(MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefAlternate).Count)
                        If Not String.IsNullOrEmpty(TopCell) Then
                            oLogInfo.Add(Log.Type.Other, "Multiple cells are referenced but their pin counts do not match. See top cell: " & TopCell & "(" & alMasterCellPinNumbers.Count & ") and alternate cell: " & altcell & "(" & alTempCellPinNumbers.Count() & ")")
                        Else
                            oLogInfo.Add(Log.Type.Other, "Multiple cells are referenced but their pin counts do not match. See bottom cell: " & BotCell & "(" & alMasterCellPinNumbers.Count & ") and alternate cell: " & altcell & "(" & alTempCellPinNumbers.Count() & ")")
                        End If
                        RaiseEvent LogError()

                    End If

                End If

                Try
                    pdbNewPart.PinMapping.Commit()
                Catch ex As Exception
                    oLogInfo.Add(Log.Type.Other, "Unable to commit alternate cell " & altcell)
                    RaiseEvent LogError()
                    Return False
                End Try

            Next
        End If

        Return True

    End Function

    Function CopyGates(ByRef pdbNewPart As MGCPCBPartsEditor.Part, ByVal pdbPart As MGCPCBPartsEditor.Part, ByRef oLogInfo As Log)

        If Verbose = True Then
            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                writer.WriteLine(vbTab & "Starting Copy Gates")
            End Using
        End If

        Dim b_Results As Boolean
        Dim pinDefinitionsCount As Integer

        'Copy logical gates first
        For Each gate As MGCPCBPartsEditor.Gate In pdbPart.PinMapping.Gates

            pinDefinitionsCount = gate.PinDefinitions.Count
            If Not pinDefinitionsCount = 0 Then

                Try
                    If Verbose = True Then
                        Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                            writer.WriteLine(vbTab & vbTab & "Adding Gate:" & gate.Name)
                        End Using
                    End If
                    Dim newGate As MGCPCBPartsEditor.Gate = pdbNewPart.PinMapping.PutGate(gate.Name, pinDefinitionsCount, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeLogical)
                    If CopyPinDefinitions(newGate, gate) = False Then
                        Return False
                    End If
                    pdbNewPart.PinMapping.Commit()
                Catch ex As Exception
                    oLogInfo.Add(Log.Type.Other, "Could not commit gate: " & gate.Name)
                    RaiseEvent LogError()
                    Return False
                End Try
            End If
        Next

        'Now copy NoRoute
        For Each gate As MGCPCBPartsEditor.Gate In pdbPart.PinMapping.NoRoute
            Try
                If Verbose = True Then
                    Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                        writer.WriteLine(vbTab & vbTab & "Adding Gate:" & gate.Name)
                    End Using
                End If
                pinDefinitionsCount = gate.PinDefinitions.Count
                Dim newGate As MGCPCBPartsEditor.Gate = pdbNewPart.PinMapping.PutGate(gate.Name, pinDefinitionsCount, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeNoRoute)

                b_Results = CopyPinDefinitions(newGate, gate)

                If b_Results = False Then
                    Return False
                End If
                pdbNewPart.PinMapping.Commit()
            Catch ex As Exception
                oLogInfo.Add(Log.Type.Other, "Could not commit gate: " & gate.Name)
                RaiseEvent LogError()
                Return False
            End Try

        Next

        'Now copy NoConnect
        For Each gate As MGCPCBPartsEditor.Gate In pdbPart.PinMapping.NoConnect
            Try
                If Verbose = True Then
                    Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                        writer.WriteLine(vbTab & vbTab & "Adding Gate:" & gate.Name)
                    End Using
                End If
                pinDefinitionsCount = gate.PinDefinitions.Count
                Dim newGate As MGCPCBPartsEditor.Gate = pdbNewPart.PinMapping.PutGate(gate.Name, pinDefinitionsCount, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeNoConnect)

                b_Results = CopyPinDefinitions(newGate, gate)

                If b_Results = False Then
                    Return False
                End If

                pdbNewPart.PinMapping.Commit()
            Catch ex As Exception
                oLogInfo.Add(Log.Type.Other, "Could not commit gate: " & gate.Name)
                RaiseEvent LogError()
                Return False
            End Try

        Next

        'Now copy Supply gates
        For Each gate As MGCPCBPartsEditor.Gate In pdbPart.PinMapping.Supply
            Try
                If Verbose = True Then
                    Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                        writer.WriteLine(vbTab & vbTab & "Adding Gate:" & gate.Name)
                    End Using
                End If
                pinDefinitionsCount = gate.PinDefinitions.Count
                Dim newGate As MGCPCBPartsEditor.Gate = pdbNewPart.PinMapping.PutGate(gate.Name, pinDefinitionsCount, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeSupply)

                b_Results = CopyPinDefinitions(newGate, gate)

                If b_Results = False Then
                    Return False
                End If

                pdbNewPart.PinMapping.Commit()
            Catch ex As Exception
                oLogInfo.Add(Log.Type.Other, "Could not commit gate: " & gate.Name)
                RaiseEvent LogError()
                Return False
            End Try

        Next

        Return True

    End Function

    Function CopyPart(ByVal pdbPartition As MGCPCBPartsEditor.Partition, ByVal pdbPart As MGCPCBPartsEditor.Part, ByRef pdbNewPart As MGCPCBPartsEditor.Part, ByRef oLogInfo As Log, ByRef oPart As AAL.Part) As Boolean

        pdbNewPart = Nothing
        pdbNewPart = pdbPartition.NewPart()
        sPartitionName = pdbPartition.Name

        ' Name
        Dim partName As String
        partName = pdbPart.Name
        origPartName = pdbPart.Name
        pdbNewPart.Name = partName
        oPart.Name = pdbPart.Name

        'Number
        Dim partNumber As String
        partNumber = pdbPart.Number & "^new"
        pdbNewPart.Number = partNumber
        oPart.Number = partNumber

        'Label
        Dim partLabel As String
        partLabel = pdbPart.Label
        origPartLabel = pdbPart.Label
        pdbNewPart.Label = partLabel
        oPart.Label = pdbPart.Label

        'RefDesPrefix
        Dim partRefDesPrefix As String
        partRefDesPrefix = pdbPart.RefDesPrefix
        pdbNewPart.RefDesPrefix = partRefDesPrefix
        oPart.RefDes = pdbPart.RefDesPrefix

        'Type
        If bUpdatePartType = True Then
            If LibraryData.PDBType.ContainsKey(partRefDesPrefix) Then
                Select Case LibraryData.PDBType.Item(partRefDesPrefix)
                    Case "BJT"
                        pdbNewPart.Type = 1
                        oLogInfo.Add(Log.Type.Note, "Setting to BJT based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case "Capacitor"
                        pdbNewPart.Type = 2
                        oLogInfo.Add(Log.Type.Note, "Setting to Capacitor based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case "Connector"
                        pdbNewPart.Type = 4
                        oLogInfo.Add(Log.Type.Note, "Setting to Connector based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case "Diode"
                        pdbNewPart.Type = 8
                        oLogInfo.Add(Log.Type.Note, "Setting to Diode based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case "IC"
                        pdbNewPart.Type = 16
                        oLogInfo.Add(Log.Type.Note, "Setting to IC based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case "Inductor"
                        pdbNewPart.Type = 32
                        oLogInfo.Add(Log.Type.Note, "Setting to Inductor based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case "Jumper"
                        pdbNewPart.Type = 128
                        oLogInfo.Add(Log.Type.Note, "Setting to Jumper based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case "RCNetwork"
                        pdbNewPart.Type = 2048
                        oLogInfo.Add(Log.Type.Note, "Setting to RCNetwork based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case "Resistor"
                        pdbNewPart.Type = 4096
                        oLogInfo.Add(Log.Type.Note, "Setting to Resistor based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case "Switch"
                        pdbNewPart.Type = 8192
                        oLogInfo.Add(Log.Type.Note, "Setting to Switch based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case "VoltageDivider"
                        pdbNewPart.Type = 16384
                        oLogInfo.Add(Log.Type.Note, "Setting to Voltage Divider based on reference designator """ & partRefDesPrefix & """.")
                        RaiseEvent LogNote()

                    Case Else
                        pdbNewPart.Type = 512
                        oLogInfo.Add(Log.Type.Note, "Setting to Misc because reference designator """ & partRefDesPrefix & """ is not in PDBTypeTable.caf.")
                        RaiseEvent LogNote()

                End Select
            Else
                pdbNewPart.Type = 512
                oLogInfo.Add(Log.Type.Warning, "Set Part Type: Could not set part type to Nothing. Setting to Misc because reference designator """ & partRefDesPrefix & """ is not in PDBTypeTable.caf.")
                RaiseEvent LogWarning()
            End If
        Else
            Dim partType
            partType = pdbPart.Type
            Try
                pdbNewPart.Type = partType
            Catch ex As Exception
                Dim value

                If (partType = 524288) Or (partType = 0) Then

                    partType = "Null"

                End If

                If LibraryData.PDBType.ContainsKey(partRefDesPrefix) Then
                    Select Case LibraryData.PDBType.Item(partRefDesPrefix)
                        Case "BJT"
                            pdbNewPart.Type = 1
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to BJT based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case "Capacitor"
                            pdbNewPart.Type = 2
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to Capacitor based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case "Connector"
                            pdbNewPart.Type = 4
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to Connector based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case "Diode"
                            pdbNewPart.Type = 8
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to Diode based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case "IC"
                            pdbNewPart.Type = 16
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to IC based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case "Inductor"
                            pdbNewPart.Type = 32
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to Inductor based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case "Jumper"
                            pdbNewPart.Type = 128
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to Jumper based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case "RCNetwork"
                            pdbNewPart.Type = 2048
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to RCNetwork based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case "Resistor"
                            pdbNewPart.Type = 4096
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to Resistor based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case "Switch"
                            pdbNewPart.Type = 8192
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to Switch based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case "VoltageDivider"
                            pdbNewPart.Type = 16384
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to Voltage Divider based on reference designator """ & partRefDesPrefix & """.")
                            RaiseEvent LogNote()

                        Case Else
                            pdbNewPart.Type = 512
                            oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to Misc because reference designator """ & partRefDesPrefix & """ is not in PDBTypeTable.caf.")
                            RaiseEvent LogNote()

                    End Select
                Else
                    pdbNewPart.Type = 512
                    oLogInfo.Add(Log.Type.Note, "Set Part Type: Could not set part type to " & partType & ". Setting to Misc because reference designator """ & partRefDesPrefix & """ is not in PDBTypeTable.caf.")
                    RaiseEvent LogNote()

                End If
            End Try

        End If

        'Description

        pdbNewPart.Description = pdbPart.Description
        oPart.Description = pdbPart.Description

        Try
            pdbNewPart.Commit()
        Catch ex As Exception
            oLogInfo.Add(Log.Type.Other, "Initial Part Commit - Unable to commit new part")
            RaiseEvent LogError()
            Return False
        End Try

        Dim b_Result As Boolean = True
        b_Result = CopyProperties(pdbNewPart, pdbPart, oLogInfo, oPart)

        'If b_Result = False Then
        '    Return False
        'End If

        b_Result = CopyPinMapping(pdbNewPart, pdbPart, oLogInfo)

        If b_Result = False Then
            If Not IsNothing(xmlDiagnosticsDoc) Then
                nodePart = oWriteDiagnostics.WritePartNode(xmlDiagnosticsDoc, xmlDiagnosticsDoc.DocumentElement.SelectSingleNode("Parts"), pdbPartition.Name, pdbPart.Number)
            End If
            Return False
        End If

        Return True
    End Function

    Function CopyPinDefinitions(ByRef newGate As MGCPCBPartsEditor.Gate, ByVal gate As MGCPCBPartsEditor.Gate)

        If Verbose = True Then
            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                writer.WriteLine(vbTab & vbTab & "Copy Pin Definitions to new gate:")
            End Using
        End If

        For Each pdbPin As MGCPCBPartsEditor.PinDefinition In gate.PinDefinitions

            Dim SwapIdentifier As String = pdbPin.SwapIdentifier
            SwapIdentifier = SwapIdentifier.Replace("\", "_")
            SwapIdentifier = SwapIdentifier.Replace("/", "_")
            SwapIdentifier = SwapIdentifier.Replace(",", "_")
            SwapIdentifier = SwapIdentifier.Replace("(", "_")
            SwapIdentifier = SwapIdentifier.Replace(")", "_")
            If SwapIdentifier.Length > 50 Then
                SwapIdentifier = SwapIdentifier.Substring(0, 49)
            End If

            If Verbose = True Then
                Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                    writer.WriteLine(vbTab & vbTab & vbTab & "Index: " & pdbPin.Index & ", Swap Identifier: " & SwapIdentifier & ", Type: " & pdbPin.PinValueType)
                End Using
            End If

            newGate.PutPinDefinition(pdbPin.Index, SwapIdentifier, pdbPin.PinPropertyType, pdbPin.PinValueType)

        Next

        Return True

    End Function

    Function CopyPinMapping(ByRef pdbNewPart As MGCPCBPartsEditor.Part, ByVal pdbPart As MGCPCBPartsEditor.Part, ByRef oLogInfo As Log)

        CopyPinMappingFailed = False

        Dim b_Result As Boolean

        b_Result = CopySymbolReferences(pdbNewPart, pdbPart, oLogInfo)
        If b_Result = False Then
            'MsgBox("Problem in Copy Symbol References")
            Return False
        End If

        If pdbNewPart.SymbolReferences.Count > 0 And Verbose = True Then
            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                writer.WriteLine(vbTab & "Associated Symbols:")
            End Using
            For Each pdbSymRef As MGCPCBPartsEditor.SymbolReference In pdbNewPart.SymbolReferences
                Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                    writer.WriteLine(vbTab & vbTab & pdbSymRef.Name)
                End Using
            Next
        End If

        b_Result = CopyCellReferences(pdbNewPart, pdbPart, oLogInfo)
        If b_Result = False Then
            'MsgBox("Problem in Copy Cell References")
            Return False
        End If

        'Try
        '    pdbNewPart.PinMapping.Commit()
        'Catch ex As Exception
        '    oLogInfo.Add(Log.Type.Other, "Could not commit after adding Symbols and Cells. This is most likely the result of missing Symbol or Cell data.")
        '    RaiseEvent LogError()
        '    Return False
        'End Try

        If pdbNewPart.SymbolReferences.Count = 0 Then
            oLogInfo.Add(Log.Type.Other, "Could not recommit symbols (" & pdbPart.SymbolReferences.Count & ").")
            RaiseEvent LogError()
            Return False
        End If

        If pdbNewPart.CellReferences.Count > 0 And Verbose = True Then
            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                writer.WriteLine(vbTab & "Associated Cells:")
            End Using
            For Each pdbCell As MGCPCBPartsEditor.CellReference In pdbNewPart.PinMapping.CellReferences
                Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                    writer.WriteLine(vbTab & vbTab & pdbCell.Name)
                End Using
            Next
        End If

        For Each pdbCell As MGCPCBPartsEditor.CellReference In pdbNewPart.PinMapping.CellReferences

            alCellPinNumbers.AddRange(pdbCell.PinNumbers)
            Exit For

        Next

        If CopyGates(pdbNewPart, pdbPart, oLogInfo) = False Then

            oLogInfo.Add(Log.Type.Other, "Could not commit after Copy Gates. This is most likely the result of bad gate information.")
            RaiseEvent LogError()
            Return False

        End If
        If Verbose = True Then
            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                writer.WriteLine(vbTab & "Currently available Gates:")
            End Using
            For Each pdbGate As MGCPCBPartsEditor.Gate In pdbNewPart.PinMapping.Gates

                Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                    writer.WriteLine(vbTab & vbTab & pdbGate.Name)
                End Using

            Next
        End If

        If (pdbNewPart.PinMapping.Gates.Count = 0) Then
            oLogInfo.Add(Log.Type.Other, "Copy Gates: After pin mapping commit, gate count is currently 0.")
            RaiseEvent LogError()
            Return False
        End If

        'If Not (pdbNewPart.SymbolReferences.Count = 0) And Not (pdbNewPart.CellReferences.Count = 0) Then

        Dim dicPinList As New Dictionary(Of String, Integer)

        If CopySlots(pdbNewPart, pdbPart, oLogInfo) = False Then
            Return False
        End If

        Try
            pdbNewPart.PinMapping.Commit()
        Catch ex As Exception
            If bRepairErrors = True Then
                oLogInfo.Add(Log.Type.Other, "Could not commit mapping after Copy Slots")
                RaiseEvent LogError()
                CopyPinMappingFailed = True
                Return False
            Else
                oLogInfo.Add(Log.Type.Other, "Could not commit mapping after Copy Slots, please rerun PDB Heal with """ & "Attempt to Repair Errors""")
                RaiseEvent LogError()
                Return False
            End If

        End Try

        If (bAddNCPins = True) And (CancelNCPin = False) Then
            Dim iPinCount As Integer = 1
            Dim sNCPins As String
            If alCellPinNumbers.Count > 0 Then
                Dim sNCPin
                For Each sNCPin In alCellPinNumbers
                    Dim objNCGate As MGCPCBPartsEditor.Gate = Nothing
                    If pdbNewPart.PinMapping.NoConnect.Count > 0 Then
                        objNCGate = pdbNewPart.PinMapping.NoConnect(1)
                    Else
                        objNCGate = pdbNewPart.PinMapping.PutGate("No Connect", 1, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeNoConnect)
                    End If

                    ' Make sure this pin is not already defined as a no-connect
                    Dim objNCSlot As MGCPCBPartsEditor.Slot = Nothing

                    Dim SymbolReference As MGCPCBPartsEditor.SymbolReference = Nothing

                    If iPinCount = 1 Then
                        sNCPins = sNCPin
                    Else
                        sNCPins = sNCPins & ", " & sNCPin
                    End If

                    objNCSlot = pdbNewPart.PinMapping.PutSlot(objNCGate, SymbolReference)
                    objNCSlot.PutPin(1, sNCPin)
                    iPinCount += 1
                Next

                oLogInfo.Add(Log.Type.Note, "Adding NC pins (" & sNCPins & ")")
                RaiseEvent LogNote()

                Try
                    pdbNewPart.PinMapping.Commit()
                Catch ex As Exception
                    oLogInfo.Add(Log.Type.Other, "Could not commit after adding NC Pins")
                    RaiseEvent LogError()
                    Return False
                End Try

            End If
        End If

        Return True

        'Else
        'Return False
        'End If

    End Function

    Function CopyPins(ByRef destSlot As MGCPCBPartsEditor.Slot, ByVal sourceSlot As MGCPCBPartsEditor.Slot)

        For Each pdbPin As MGCPCBPartsEditor.IMGCPDBPin In sourceSlot.Pins
            If Verbose = True Then
                Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                    writer.WriteLine(vbTab & vbTab & "Number: " & pdbPin.Number & ", Name: " & pdbPin.Name & ", Index: " & pdbPin.Index)
                End Using
            End If

            If (bAddNCPins = True) And (CancelNCPin = False) Then

                destSlot.PutPin(pdbPin.Index, pdbPin.Number, pdbPin.Name)

                If alCellPinNumbers.Contains(pdbPin.Number) Then
                    alCellPinNumbers.Remove(pdbPin.Number)
                End If
            Else
                destSlot.PutPin(pdbPin.Index, pdbPin.Number, pdbPin.Name)
            End If

        Next

        Return True

    End Function

    Function CopyProperties(ByRef pdbNewPart As MGCPCBPartsEditor.Part, ByVal pdbPart As MGCPCBPartsEditor.Part, ByRef oLogInfo As Log, ByRef oPart As AAL.Part)

        If pdbPart.Properties.Count > 0 Then
            For Each pdbProp As MGCPCBPartsEditor.Property In pdbPart.Properties
                Try
                    Dim propName As String
                    propName = pdbProp.Name

                    Dim propValue As String
                    propValue = pdbProp.Value

                    oPart.Properties.Add(pdbProp.Name, pdbProp.Value)

                    Dim propType
                    propType = pdbProp.Type

                    Dim propObj, propValueString
                    propValueString = pdbProp.GetValueString

                    propObj = Nothing
                    Try
                        propObj = pdbNewPart.PutPropertyEx(propName, propValue)
                    Catch ex As Exception
                        oLogInfo.Add(Log.Type.Warning, "Copy Properties: Removing " & propName & " with value of " & propValue & " because this property is not defined as a common PDB property.")
                        RaiseEvent LogWarning()
                        Return False
                    End Try
                Catch ex As Exception

                End Try

            Next
        End If

        Return True
    End Function

    Function CopySlots(ByRef pdbNewPart As MGCPCBPartsEditor.Part, ByVal pdbPart As MGCPCBPartsEditor.Part, ByRef oLogInfo As Log)

        If Verbose = True Then
            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                writer.WriteLine(vbTab & "Starting Copy Slots")
            End Using
        End If

        If IsNothing(pdbPart.PinMapping.Slots) Then
            oLogInfo.Add(Log.Type.Other, "Copy Slots: Part contains no slots.")
            RaiseEvent LogError()
            CopyPinMappingFailed = True
            Return False

        ElseIf pdbPart.PinMapping.Slots.Count < 1 Then
            oLogInfo.Add(Log.Type.Other, "Copy Slots: Part contains no slots.")
            RaiseEvent LogError()
            CopyPinMappingFailed = True
            Return False
        Else
            For Each slot As MGCPCBPartsEditor.Slot In pdbPart.PinMapping.Slots
                If Verbose = True Then
                    If Not IsNothing(slot.SymbolReference) Then
                        Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                            writer.WriteLine(vbTab & "Slot: " & slot.SymbolReference.Name)
                        End Using
                    End If

                    Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                        writer.WriteLine(vbTab & "Finding Gate: " & slot.Gate.Name)
                    End Using
                End If

                Dim newSlot As MGCPCBPartsEditor.Slot
                Dim destGate As MGCPCBPartsEditor.Gate
                destGate = FindGate(slot.Gate.Name, pdbNewPart, oLogInfo)

                If destGate Is Nothing Then
                    oLogInfo.Add(Log.Type.Other, "Copy Slots: Could not find gate " & slot.Gate.Name.Trim)
                    RaiseEvent LogError()
                    Return False
                End If

                Dim sGateName As String = destGate.Name

                If Verbose = True Then
                    Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                        writer.WriteLine(vbTab & "Looking for Symbol:")
                    End Using
                End If

                Dim destSymbolReference As MGCPCBPartsEditor.SymbolReference = Nothing
                If Not IsNothing(slot.SymbolReference) Then
                    Dim slotname As String = slot.SymbolReference.Name.Trim

                    If IsNothing(s_SymbolCase) Then

                    ElseIf s_SymbolCase = "Lowercase" Then
                        Dim slotNameSplit As String() = Split(slotname, ":")
                        If slotNameSplit.Count > 1 Then
                            slotname = slotNameSplit(0) & ":" & LCase(slotNameSplit(1))
                        Else
                            slotname = LCase(slotNameSplit(0))
                        End If
                    ElseIf s_SymbolCase = "Uppercase" Then
                        Dim slotNameSplit As String() = Split(slotname, ":")
                        If slotNameSplit.Count > 1 Then
                            slotname = slotNameSplit(0) & ":" & UCase(slotNameSplit(1))
                        Else
                            slotname = UCase(slotNameSplit(0))
                        End If
                    End If

                    For Each pdbSym As MGCPCBPartsEditor.SymbolReference In pdbNewPart.SymbolReferences
                        If Verbose = True Then
                            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                                writer.WriteLine(vbTab & vbTab & pdbSym.Name)
                            End Using
                        End If

                        If slotname.Contains(":") Then

                            If pdbSym.Name = slotname Then
                                destSymbolReference = pdbSym
                                If Verbose = True Then
                                    Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                                        writer.WriteLine(vbTab & vbTab & "Found Symbol")
                                    End Using
                                End If

                                Exit For
                            Else
                                Dim slotNameSplit As String() = Split(slotname, ":")
                                Dim symNameSplit As String() = Split(pdbSym.Name, ":")
                                'Dim sSymRename As String
                                If symNameSplit.Count > 1 Then
                                    Dim symbolRefname As String = symNameSplit(1)
                                    If Not dicRenameSymbols.Count = 0 Then

                                        If dicRenameSymbols.ContainsKey(slotNameSplit(1)) Then
                                            If dicRenameSymbols.Item(slotNameSplit(1)) = symbolRefname Then
                                                destSymbolReference = pdbSym
                                                Exit For
                                            End If
                                        End If
                                    Else

                                        If IsNothing(s_SymbolCase) Then
                                            If symbolRefname = slotNameSplit(1) Then
                                                destSymbolReference = pdbSym
                                                Exit For
                                            End If
                                        ElseIf s_SymbolCase = "Lowercase" Then
                                            If LCase(symbolRefname) = LCase(slotNameSplit(1)) Then
                                                destSymbolReference = pdbSym
                                                Exit For
                                            End If
                                        ElseIf s_SymbolCase = "Uppercase" Then
                                            If UCase(symbolRefname) = UCase(slotNameSplit(1)) Then
                                                destSymbolReference = pdbSym
                                                Exit For
                                            End If
                                        End If

                                    End If
                                Else
                                    If Not IsNothing(dicRenameSymbols) Then

                                        If dicRenameSymbols.ContainsKey(slotNameSplit(1)) Then
                                            If dicRenameSymbols.Item(slotNameSplit(1)) = pdbSym.Name Then
                                                destSymbolReference = pdbSym
                                                Exit For
                                            End If
                                        End If
                                    Else
                                        If pdbSym.Name = slotNameSplit(1) Then
                                            destSymbolReference = pdbSym
                                            Exit For
                                        End If
                                    End If
                                End If

                            End If
                        Else

                            Dim name As String = pdbSym.Name
                            If name.Contains(":") Then
                                Dim symNameSplit As String() = Split(name, ":")
                                If symNameSplit(1) = slotname Then
                                    destSymbolReference = pdbSym
                                    Exit For
                                End If
                            ElseIf name = slotname Then
                                destSymbolReference = pdbSym
                                Exit For
                            End If

                        End If

                    Next

                    If IsNothing(destSymbolReference) Then
                        If Verbose = True Then
                            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                                writer.WriteLine(vbTab & "Could not find associated slot for symbol: " & slot.SymbolReference.Name)
                            End Using
                        End If

                        oLogInfo.Add(Log.Type.Other, "Copy Slots: Could not find associated slot for symbol: " & slot.SymbolReference.Name.Trim)
                        RaiseEvent LogError()
                        Return False
                    End If

                End If

                Try
                    If Verbose = True Then
                        If Not IsNothing(destSymbolReference) Then
                            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                                writer.WriteLine(vbTab & "Adding Slot:" & destSymbolReference.Name & ", to Gate: " & sGateName)
                            End Using
                        Else
                            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                                writer.WriteLine(vbTab & "Adding Slot to Gate: " & sGateName)
                            End Using
                        End If
                    End If

                    Dim Gate As Object = destGate
                    Dim Symbol As Object = destSymbolReference

                    newSlot = pdbNewPart.PinMapping.PutSlot(Gate, Symbol)
                Catch ex As Exception
                    If Verbose = True Then

                        If Not IsNothing(destSymbolReference) Then
                            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                                writer.WriteLine(vbTab & "Failed to add Slot:" & destSymbolReference.Name & ", to Gate: " & sGateName)
                            End Using
                        Else
                            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                                writer.WriteLine(vbTab & "Failed to add Slot to Gate: " & sGateName)
                            End Using
                        End If

                    End If

                    Try
                        Dim Symbol As Object
                        Dim Gate As Object

                        Symbol = New System.Runtime.InteropServices.DispatchWrapper(destSymbolReference)
                        Gate = New System.Runtime.InteropServices.DispatchWrapper(destGate)
                        If Verbose = True Then
                            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                                writer.WriteLine(vbTab & "Trying again to add Slot:" & destSymbolReference.Name & ", to Gate: " & sGateName)
                            End Using
                        End If

                        newSlot = pdbNewPart.PinMapping.PutSlot(Gate, Symbol)
                    Catch err As Exception
                        oLogInfo.Add(Log.Type.Other, "Copy Slots: Error creating new slot for " & sGateName)
                        RaiseEvent LogError()
                        Return False
                    End Try

                End Try

                If Verbose = True Then
                    Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                        writer.WriteLine(vbTab & "Copying Pins:")
                    End Using
                End If

                If CopyPins(newSlot, slot) = False Then
                    oLogInfo.Add(Log.Type.Other, "Copy Slots: Unable to copy pins.")
                    RaiseEvent LogError()
                    Return False
                End If

            Next

            If bAddSymbols = True And pdbNewPart.PinMapping.Gates.Count = 1 Then
                For Each pdbDefinedSlot As MGCPCBPartsEditor.Slot In pdbNewPart.PinMapping.Gates.Item(1).Slots

                    Dim dicSlotPins As New Dictionary(Of String, Integer)

                    For Each pdbPin As MGCPCBPartsEditor.IMGCPDBPin In pdbDefinedSlot.Pins
                        Try
                            dicSlotPins.Add(pdbPin.Number, pdbPin.Index)
                        Catch ex As Exception
                            oLogInfo.Add(Log.Type.Other, "A symbol slipped through the had duplicate pin numbers. Please check your symbols for duplicate pin numbers.")
                            RaiseEvent LogError()
                            Return False
                        End Try
                    Next

                    For Each pdbSymRef As MGCPCBPartsEditor.SymbolReference In pdbNewPart.PinMapping.SymbolReferences

                        If dicAltSymbolInfo.ContainsKey(pdbSymRef.Name) Then

                            Dim oSymbol As AAL.Symbol = dicAltSymbolInfo.Item(pdbSymRef.Name)
                            Dim l_TempSlots As New List(Of AAL.Slot)(oSymbol.Slots)

                            For Each oSlot As AAL.Slot In l_TempSlots
                                If oSymbol.Slots.Count = 1 Then
                                    Dim pdbSlot As MGCPCBPartsEditor.Slot = pdbNewPart.PinMapping.PutSlot(pdbNewPart.PinMapping.Gates.Item(1), pdbSymRef)
                                    For Each aalPin As AAL.SymbolPin In oSlot.SymbolPins.Values
                                        Dim i_Index As Integer = 0
                                        If dicSlotPins.ContainsKey(aalPin.Number) Then
                                            i_Index = dicSlotPins.Item(aalPin.Number)
                                            pdbSlot.PutPin(i_Index, aalPin.Number, aalPin.Name)
                                        End If
                                    Next
                                Else
                                    Dim l_SlotPinsTemp As New List(Of String)(dicSlotPins.Keys)
                                    For Each aalPin As AAL.SymbolPin In oSlot.SymbolPins.Values
                                        If l_SlotPinsTemp.Contains(aalPin.Number) Then l_SlotPinsTemp.Remove(aalPin.Number)
                                    Next

                                    If l_SlotPinsTemp.Count = 0 Then
                                        Dim pdbSlot As MGCPCBPartsEditor.Slot = pdbNewPart.PinMapping.PutSlot(pdbNewPart.PinMapping.Gates.Item(1), pdbSymRef)
                                        For Each aalPin As AAL.SymbolPin In oSlot.SymbolPins.Values
                                            Dim i_Index As Integer = 0
                                            If dicSlotPins.ContainsKey(aalPin.Number) Then
                                                i_Index = dicSlotPins.Item(aalPin.Number)
                                                pdbSlot.PutPin(i_Index, aalPin.Number, aalPin.Name)
                                            End If
                                        Next
                                        l_TempSlots.Remove(oSlot)
                                        Exit For
                                    End If
                                End If
                            Next

                        End If

                    Next

                Next
            End If

            Return True
        End If

    End Function

    Function CopySymbolReferences(ByRef pdbNewPart As MGCPCBPartsEditor.Part, ByVal pdbPart As MGCPCBPartsEditor.Part, ByRef oLogInfo As Log)

        Dim alCurrentSymbols As New ArrayList()

        For Each symbolRef As MGCPCBPartsEditor.SymbolReference In pdbPart.PinMapping.SymbolReferences

            Dim sCurrentSymPartition As String

            Dim symbolRefName As String = symbolRef.Name

            Dim l_Partitions As List(Of String)
            'Dim value As LibraryManager.IMGCLMSymbol
            If symbolRefName.Contains(":") Then
                Dim symNameSplit As String() = Split(symbolRefName, ":")
                sCurrentSymPartition = symNameSplit(0)
                symbolRefName = symNameSplit(1)
            Else
                If bUpdateSymPartition = False And LibraryData.Type = Data.LibType.DX Then
                    oLogInfo.Add(Log.Type.Other, "Symbol: " & symbolRefName & " does not contain a partition name and Update Symbol Partition is set to false. Please re-Run heal with Update Symbol Partition enabled.")
                    RaiseEvent LogError()
                    Return False
                End If

            End If

            Dim sSymRename As String

            If Not IsNothing(dicRenameSymbols) Or Not (dicRenameSymbols.Count = 0) Then

                If dicRenameSymbols.ContainsKey(symbolRefName) Then

                    sSymRename = dicRenameSymbols.Item(symbolRefName)
                    bSymbolRename = True

                    If LibraryData.SymbolList.ContainsKey(sSymRename) Then
                        l_Partitions = LibraryData.SymbolList.Item(sSymRename)
                    End If

                    If IsNothing(s_SymbolCase) Then
                        symbolRefName = sSymRename
                        oLogInfo.Add(Log.Type.Note, "Changing symbol " & symbolRef.Name & " to " & l_Partitions(0) & ":" & sSymRename)
                        RaiseEvent LogNote()

                    ElseIf s_SymbolCase = "Lowercase" Then
                        symbolRefName = LCase(sSymRename)
                        oLogInfo.Add(Log.Type.Note, "Changing symbol " & symbolRef.Name & " to " & LCase(l_Partitions(0) & ":" & sSymRename))
                        RaiseEvent LogNote()

                    ElseIf s_SymbolCase = "Uppercase" Then
                        symbolRefName = UCase(sSymRename)
                        oLogInfo.Add(Log.Type.Note, "Changing symbol " & symbolRef.Name & " to " & UCase(l_Partitions(0) & ":" & sSymRename))
                        RaiseEvent LogNote()

                    End If
                Else
                    bSymbolRename = False
                End If

            End If

            If bUpdateSymPartition = True And LibraryData.Type = Data.LibType.DX Then

                Dim sPartition As String

                If LibraryData.SymbolList.ContainsKey(symbolRefName) Then
                    l_Partitions = LibraryData.SymbolList.Item(symbolRefName)

                    If Not String.IsNullOrWhiteSpace(sCurrentSymPartition) Then
                        If (l_Partitions.Contains(sCurrentSymPartition, StringComparer.OrdinalIgnoreCase)) Then
                            sPartition = sCurrentSymPartition
                        Else
                            sPartition = l_Partitions(0)
                        End If
                    Else
                        sPartition = l_Partitions(0)
                    End If

                    If IsNothing(s_SymbolCase) Then
                        symbolRefName = l_Partitions(0) & ":" & symbolRefName
                    ElseIf s_SymbolCase = "Lowercase" Then
                        symbolRefName = l_Partitions(0) & ":" & LCase(symbolRefName)
                    ElseIf s_SymbolCase = "Uppercase" Then
                        symbolRefName = l_Partitions(0) & ":" & UCase(symbolRefName)
                    End If
                Else

                    oLogInfo.Add(Log.Type.Other, "Symbol " & symbolRefName & " was not found in central library.")
                    RaiseEvent LogError()
                    Return False
                End If
            Else

                If LibraryData.Type = Data.LibType.DC Then
                    If s_SymbolCase = "Lowercase" Then
                        symbolRefName = LCase(symbolRefName)
                        oLogInfo.Add(Log.Type.Note, "Changing symbol " & symbolRef.Name & " to " & LCase(sSymRename))
                        RaiseEvent LogNote()

                    ElseIf s_SymbolCase = "Uppercase" Then
                        symbolRefName = UCase(symbolRefName)
                        oLogInfo.Add(Log.Type.Other, "Changing symbol " & symbolRef.Name & " to " & UCase(sSymRename))
                        RaiseEvent LogError()
                    End If
                Else
                    If IsNothing(s_SymbolCase) Then
                        symbolRefName = sCurrentSymPartition & ":" & symbolRefName
                    ElseIf s_SymbolCase = "Lowercase" Then
                        symbolRefName = sCurrentSymPartition & ":" & LCase(symbolRefName)
                        oLogInfo.Add(Log.Type.Note, "Changing symbol " & symbolRef.Name & " to " & LCase(sSymRename))
                        RaiseEvent LogNote()

                    ElseIf s_SymbolCase = "Uppercase" Then
                        symbolRefName = sCurrentSymPartition & ":" & UCase(symbolRefName)
                        oLogInfo.Add(Log.Type.Other, "Changing symbol " & symbolRef.Name & " to " & UCase(sSymRename))
                        RaiseEvent LogError()
                    End If
                End If
            End If

            symbolRefName = symbolRefName.Trim

            If Not alCurrentSymbols.Contains(symbolRefName) Then

                Dim newSymbolRef As MGCPCBPartsEditor.SymbolReference = pdbNewPart.PinMapping.PutSymbolReference(symbolRefName)

                If symbolRef.Default = True Then
                    newSymbolRef.SetAsDefault()
                End If

                alCurrentSymbols.Add(symbolRefName)
            Else

                oLogInfo.Add(Log.Type.Warning, "Removing symbol " & symbolRefName & " because it is already used on the pdbPart.")
                RaiseEvent LogWarning()

            End If

            Try
                pdbNewPart.PinMapping.Commit()
            Catch ex As Exception
                oLogInfo.Add(Log.Type.Other, "Unable to commit symbol " & symbolRefName)
                RaiseEvent LogError()
                Return False
            End Try

        Next

        If bAddSymbols = True And dicAlternateSymbols.ContainsKey(pdbPart.Number) And pdbPart.PinMapping.Gates.Count = 1 Then

            AddAlternateSymbols(pdbNewPart, pdbPart, dicAlternateSymbols.Item(pdbPart.Number), oLogInfo)

        End If

        Return True
    End Function

    Function FindGate(ByVal gateName As String, ByRef pdbNewPart As MGCPCBPartsEditor.Part, ByRef oLogInfo As Log) As MGCPCBPartsEditor.Gate

        If pdbNewPart.PinMapping.Gates.Count = 0 Then
            If Verbose = True Then
                Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                    writer.WriteLine(vbTab & vbTab & "Gate count is currently 0")
                End Using
            End If

            oLogInfo.Add(Log.Type.Other, "Find Gate: Current Gate count is 0")
            RaiseEvent LogError()
            Return Nothing
        End If

        'Logical gates
        For Each gate As MGCPCBPartsEditor.Gate In pdbNewPart.PinMapping.Gates
            If Verbose = True Then
                Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                    writer.WriteLine(vbTab & vbTab & "Gate: " & gate.Name)
                End Using
            End If
            If gate.Name.Trim = gateName.Trim Then
                If Verbose = True Then
                    Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                        writer.WriteLine(vbTab & vbTab & "Found Gate")
                    End Using
                End If
                Return gate
            End If
        Next

        'NoRoute
        For Each gate As MGCPCBPartsEditor.Gate In pdbNewPart.PinMapping.NoRoute
            If gate.Name = gateName Then
                Return gate
            End If
        Next

        'NoConnect
        For Each gate As MGCPCBPartsEditor.Gate In pdbNewPart.PinMapping.NoConnect
            If gate.Name = gateName Then
                Return gate
            End If
        Next

        'Supply
        For Each gate As MGCPCBPartsEditor.Gate In pdbNewPart.PinMapping.Supply
            If gate.Name = gateName Then
                Return gate
            End If
        Next

        Return Nothing
    End Function

    Function HealPart(pdbPartition As MGCPCBPartsEditor.Partition, pdbPart As MGCPCBPartsEditor.Part, oLogInfo As Log) As Boolean

        l_SymbolName.Clear()

        Dim b_Result As Boolean
        Dim oPart As New AAL.Part
        Dim pdbNewPart As MGCPCBPartsEditor.Part

        If Verbose = True Then
            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                writer.WriteLine(pdbPart.Number)
            End Using
        End If

        b_Result = CopyPart(pdbPartition, pdbPart, pdbNewPart, oLogInfo, oPart)

        If Verbose = True Then
            Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "\Debug - Heal PDB.log", True)
                writer.WriteLine()
            End Using
        End If

        If b_Result = False Then
            pdbNewPart.Delete()

            If (bRepairErrors = True) Then
                RaiseEvent eUpdateStatus("Rebuilding " & pdbPartition.Name & ":" & pdbPart.Number)

                Dim intpartType As Integer
                If bUpdatePartType = True Then

                    If LibraryData.PDBType.ContainsKey(pdbPart.RefDesPrefix) Then

                        Select Case LibraryData.PDBType.Item(pdbPart.RefDesPrefix)
                            Case "BJT"
                                intpartType = 1
                            Case "Capacitor"
                                intpartType = 2
                            Case "Connector"
                                intpartType = 4
                            Case "Diode"
                                intpartType = 8
                            Case "IC"
                                intpartType = 16
                            Case "Inductor"
                                intpartType = 32
                            Case "Jumper"
                                intpartType = 128
                            Case "RCNetwork"
                                intpartType = 2048
                            Case "Resistor"
                                intpartType = 4096
                            Case "Switch"
                                intpartType = 8192
                            Case "VoltageDivider"
                                intpartType = 16384
                            Case Else
                                intpartType = 512
                        End Select
                    End If
                Else

                    intpartType = pdbPart.Type

                End If

                Dim alPartAtts As New ArrayList
                Dim att As MGCPCBPartsEditor.Property
                For Each att In pdbPart.Properties
                    alPartAtts.Add(att.Name)
                Next

                Dim sPartNum As String = pdbPart.Number
                Dim sPartLabel As String = pdbPart.Label
                Dim sPartName As String = pdbPart.Name
                Dim sPartDescription As String = pdbPart.Description
                Dim sPartRefDes As String = pdbPart.RefDesPrefix

                Dim oSym As MGCPCBPartsEditor.SymbolReference
                Dim oCell As MGCPCBPartsEditor.CellReference

                'Dim CreatedPart As Boolean

                l_SymbolName.Clear()

                Dim ocBuildPDB As New Build_PDB
                ocBuildPDB.aalPart = oPart
                ocBuildPDB.PartLabel = sPartLabel
                ocBuildPDB.intPartType = intpartType
                ocBuildPDB.PartAtts = alPartAtts

                For Each pdbSym In pdbPart.SymbolReferences

                    If Not l_SymbolName.Contains(pdbSym.Name) Then

                        l_SymbolName.Add(pdbSym.Name)

                    End If

                Next

                'For Each pdbCell In pdbPart.CellReferences

                ' If Not l_CellName.Contains(pdbCell.Name) Then

                ' l_CellName.Add(pdbCell.Name)

                ' End If

                'Next

                'ocBuildPDB.l_SymbolNames = l_SymbolName
                'ocBuildPDB.l_CellName = l_CellName
                'ocBuildPDB.pdbPartition = pdbPartition
                'ocBuildPDB.LibraryData = LibraryData
                'ocBuildPDB.b_Healing = True
                'bBuildSucces = ocBuildPDB.NewPart()

                If bBuildSucces = True Then
                    oLogInfo.Add(Log.Type.Note, "Pin Mapping was successfully repaired.")
                    RaiseEvent LogNote()

                    pdbPart.Delete()
                    Return True
                Else

                    Dim sErrorReason As String

                    If oLogInfo.Errors.Count > 0 Then
                        sErrorReason = oLogInfo.Errors(oLogInfo.Errors.Count - 1)
                        sErrorReason = sErrorReason.Replace(".", String.Empty)
                        oLogInfo.Add(Log.Type.Other, "Unable to repair.")
                        RaiseEvent LogError()
                    Else
                        oLogInfo.Add(Log.Type.Other, "Unable to repair")
                        RaiseEvent LogError()
                    End If

                End If
            Else
                oLogInfo.Warnings.Clear()
                oLogInfo.Notes.Clear()
            End If
        Else

            pdbPart.Delete()
            Return True
        End If

        Return False

    End Function

    Sub HealParts(pdbPartition As MGCPCBPartsEditor.Partition)

        If PartsToHeal.Count > 0 Then

            For Each sPart As String In PartsToHeal
                For Each pdbPart As MGCPCBPartsEditor.Part In pdbPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, sPart)

                    RaiseEvent eUpdateStatus("Updating " & pdbPartition.Name & ":" & pdbPart.Number)

                    Dim oLogInfo As New Log

                    If HealPart(pdbPartition, pdbPart, oLogInfo) = True Then
                        HealLog.Success += 1
                    Else
                        HealLog.Failed += 1
                    End If

                    If oLogInfo.Errors.Count > 0 Or oLogInfo.Warnings.Count > 0 Or oLogInfo.Notes.Count > 0 Then
                        HealLog.Log.Add(sPart, oLogInfo)
                    End If

                    RaiseEvent eUpdateCount()

                Next
            Next
        Else
            For Each pdbPart As MGCPCBPartsEditor.Part In pdbPartition.Parts

                Dim sPart As String = pdbPart.Number

                RaiseEvent eUpdateStatus("Updating " & pdbPartition.Name & ":" & pdbPart.Number)

                Dim oLogInfo As New Log

                If HealPart(pdbPartition, pdbPart, oLogInfo) = True Then
                    HealLog.Success += 1
                Else
                    HealLog.Failed += 1
                End If

                If oLogInfo.Errors.Count > 0 Or oLogInfo.Warnings.Count > 0 Or oLogInfo.Notes.Count > 0 Then
                    HealLog.Log.Add(sPart, oLogInfo)
                End If

            Next
        End If

        pdbPartition.VerifyAllPartsProperties()

    End Sub

#End Region

#Region "Private Methods"

    Private Function AddAlternateSymbols(pdbNewPart As MGCPCBPartsEditor.Part, pdbPart As MGCPCBPartsEditor.Part, l_AltSymbols As List(Of AAL.Symbol), oLogInfo As Log) As Boolean
        'Dim l_AlternateSymbolReferences As New List(Of MGCPCBPartsEditor.SymbolReference)

        For Each oTempSymbol As AAL.Symbol In l_AltSymbols
            Dim oGetData As New AAL.Read
            Dim i As Integer = 1
            Do Until File.Exists(Path.GetDirectoryName(LibraryData.LibPath) & "\SymbolLibs\" & oTempSymbol.Partition & "\sym\" & oTempSymbol.Name & "." & i)
                If i > 10 Then
                    oLogInfo.Add(Log.Type.Other, "Unable to find symbol: " & oTempSymbol.Name)
                    RaiseEvent LogError()
                End If
                i += 1
            Loop

            Dim oSymbol As New AAL.Symbol
            oSymbol = oGetData.ASCII2SymbolObject(oTempSymbol.Name, oTempSymbol.Partition, Path.GetDirectoryName(LibraryData.LibPath), oTempSymbol)

            If IsNothing(oSymbol) Then
                oLogInfo.Add(Log.Type.Other, "Unable to successfully read symbol: " & oTempSymbol.Partition & ":" & oTempSymbol.Name)
                RaiseEvent LogError()
            End If

            If oSymbol.LogicalPinCount + oSymbol.ImplicitPinCount = pdbPart.PinMapping.PinCount Then
                Dim oSymbolReference As MGCPCBPartsEditor.SymbolReference = pdbNewPart.PinMapping.PutSymbolReference(oSymbol.Partition & ":" & oSymbol.Name)
                l_AlternateSymbolReferences.Add(oSymbolReference)

                Try
                    pdbNewPart.PinMapping.Commit()
                    dicAltSymbolInfo.Add(oSymbol.Partition & ":" & oSymbol.Name, oSymbol)
                    oLogInfo.Add(Log.Type.Note, "Added alternate symbol: " & oSymbol.Partition & ":" & oSymbol.Name)
                    RaiseEvent LogNote()
                Catch ex As Exception
                    oLogInfo.Add(Log.Type.Warning, "Could not add alternate symbol: " & oSymbol.Partition & ":" & oSymbol.Name)
                    RaiseEvent LogWarning()
                    Continue For
                End Try

            End If

        Next

    End Function

#End Region

End Class

Public Class HealInfo

#Region "Private Fields"

    Dim _Failed As Integer = 0

    Dim _Log As New Dictionary(Of String, Log)

    Dim _Success As Integer = 0

#End Region

#Region "Properties"

    Property Failed() As Integer
        Get
            Return _Failed
        End Get
        Set(ByVal value As Integer)
            _Failed = value
        End Set
    End Property

    Property Log() As Dictionary(Of String, Log)
        Get
            Return _Log
        End Get
        Set(ByVal value As Dictionary(Of String, Log))
            _Log = value
        End Set
    End Property

    Property Success() As Integer
        Get
            Return _Success
        End Get
        Set(ByVal value As Integer)
            _Success = value
        End Set
    End Property

#End Region

End Class