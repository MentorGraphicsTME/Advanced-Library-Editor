Public Class Compress

    Property parts As MGCPCBPartsEditor.Parts
    Property alNoSymOrCell As New ArrayList
    Property alNoSym As New ArrayList
    Property alNoCell As New ArrayList
    Property dicGenericPDB As New Dictionary(Of String, ArrayList)
    Property dicPDBType As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Property dicGenericEntries As New Dictionary(Of String, String)
    Property dicAllGenericEntries As New Dictionary(Of String, ArrayList)
    Property alCreatedEntries As New ArrayList
    Property alPartExists As New ArrayList()
    Property dicGenericPartAtts As New Dictionary(Of String, Object)
    Property dicGenericPNtoPartition As New Dictionary(Of String, String)

    'Property oSheet As Excel.Worksheet

    Dim iRowCount As Integer = 2
    Dim part As MGCPCBPartsEditor.Part

    Sub LoopParts()

        'oSheet.Cells(1, 1) = "Generic Mapping"
        'oSheet.Cells(1, 2) = "Symbols"
        'oSheet.Cells(1, 3) = "Cells"

        For Each part In parts
            Dim sValue As String
            Dim sNamePrefix As String

            Dim oCompAtts As Object = LookatPinMapping(part)

            Dim sSymbolCellName As String = oCompAtts(0)
            Dim alCells As ArrayList = oCompAtts(2)
            Dim alSyms As ArrayList = oCompAtts(1)

            If IsNothing(sSymbolCellName) Then

                alNoSymOrCell.Add(part.Number)

            ElseIf sSymbolCellName = "NoCell" Then

                alNoCell.Add(part.Number)

            ElseIf sSymbolCellName = "NoSym" Then

                alNoSym.Add(part.Number)

            Else

                If sSymbolCellName.Length > 30 Then

                    sSymbolCellName = sSymbolCellName.Substring(0, 30)

                End If

                If dicGenericPartAtts.ContainsKey(sSymbolCellName) Then
                    Dim oGenericCompAtts As Object
                    dicGenericPartAtts.TryGetValue(sSymbolCellName, oGenericCompAtts)

                    Dim sGenericSymbolCellName As String = oGenericCompAtts(0)
                    Dim alGenericCells As New ArrayList
                    For Each sCell In oGenericCompAtts(2)
                        alGenericCells.Add(sCell)
                    Next

                    Dim alGenericSyms As New ArrayList
                    For Each sSym In oGenericCompAtts(1)
                        alGenericSyms.Add(sSym)
                    Next

                    Dim bAddPNtoGeneric As Boolean = True

                    If alGenericCells.Count = alCells.Count Then

                        For Each sCell In alCells

                            If alGenericCells.Contains(sCell) Then

                                alGenericCells.Remove(sCell)

                            End If

                        Next

                        If alGenericCells.Count > 0 Then

                            LookForAlternateMappings(sSymbolCellName, alCells, alSyms)

                        Else

                            If alGenericSyms.Count = alSyms.Count Then

                                For Each sSym In alSyms

                                    If alGenericSyms.Contains(sSym) Then

                                        alGenericSyms.Remove(sSym)

                                    End If

                                Next

                                If alGenericSyms.Count > 0 Then

                                    LookForAlternateMappings(sSymbolCellName, alCells, alSyms)

                                Else

                                    Dim alPNs As ArrayList
                                    If dicGenericPDB.TryGetValue(sSymbolCellName, alPNs) Then
                                        Dim alTempPns As New ArrayList
                                        alTempPns = alPNs
                                        alTempPns.Add(part.Number)

                                        dicGenericPDB.Remove(sSymbolCellName)

                                        dicGenericPDB.Add(sSymbolCellName, alTempPns)
                                    Else
                                        Dim alTempPns As New ArrayList
                                        alTempPns.Add(part.Number)

                                        dicGenericPDB.Add(sSymbolCellName, alTempPns)

                                    End If

                                End If
                            Else

                                LookForAlternateMappings(sSymbolCellName, alCells, alSyms)

                            End If

                        End If
                    Else

                        LookForAlternateMappings(sSymbolCellName, alCells, alSyms)

                    End If

                Else

                    'Dim sNewPN As String = sNamePrefix & "_" & sSymbolCellName
                    If sSymbolCellName.Length > 30 Then

                        sSymbolCellName = sSymbolCellName.Substring(0, 30)

                    End If

                    'Dim alPNs As New ArrayList
                    'If dicGenericPDB.TryGetValue(sNewPN, alPNs) Then
                    '    Dim alTempPns As New ArrayList
                    '    alTempPns = alPNs
                    '    alTempPns.Add(part.Number)

                    '    dicGenericPDB.Remove(sNewPN)

                    '    dicGenericPDB.Add(sNewPN, alTempPns)

                    'Else
                    Dim alTempPns As New ArrayList
                    alTempPns.Add(part.Number)
                    dicGenericPDB.Add(sSymbolCellName, alTempPns)
                    dicGenericPartAtts.Add(sSymbolCellName, oCompAtts)

                    'oSheet.Cells(iRowCount, 1) = sSymbolCellName
                    'Dim sCells As String = Nothing
                    'For Each sCell In oCompAtts(2)
                    '    If sCells = Nothing Then
                    '        sCells = sCell
                    '    Else

                    '        sCells = sCell & "," & sCells

                    '    End If
                    'Next

                    'oSheet.Cells(iRowCount, 3) = sCells

                    'Dim sSyms As String = Nothing
                    'For Each sSym In oCompAtts(1)
                    '    If sSyms = Nothing Then
                    '        sSyms = sSym
                    '    Else

                    '        sSyms = sSym & "," & sSyms

                    '    End If
                    'Next

                    'oSheet.Cells(iRowCount, 2) = sSyms

                    iRowCount += 1

                    'End If

                End If

            End If

        Next

    End Sub

    Private Function LookatPinMapping(ByVal part As MGCPCBPartsEditor.Part)

        Dim sSymbolCell As String
        Dim oCompAtts As Object
        Dim AssociatedSymbols As MGCPCBPartsEditor.SymbolReferences
        AssociatedSymbols = part.PinMapping.SymbolReferences

        Dim AssociatedCells As MGCPCBPartsEditor.CellReferences
        AssociatedCells = part.CellReferences

        If (AssociatedSymbols.Count = 0) And (AssociatedCells.Count = 0) Then
            Return Nothing

        ElseIf AssociatedCells.Count = 0 Then

            oCompAtts = New Object() {"NoCell", Nothing, Nothing}
            Return oCompAtts
        ElseIf AssociatedSymbols.Count = 0 Then

            oCompAtts = New Object() {"NoSym", Nothing, Nothing}
            Return oCompAtts
        Else
            Dim alCells As New ArrayList
            Dim oCell As MGCPCBPartsEditor.CellReference

            For Each oCell In AssociatedCells

                alCells.Add(LCase(oCell.Name))

            Next

            Dim alsymbols As New ArrayList
            Dim oSymbol As MGCPCBPartsEditor.SymbolReference

            For Each oSymbol In AssociatedSymbols

                alsymbols.Add(LCase(oSymbol.Name))

            Next

            Dim sSymName As String
            If AssociatedSymbols(1).Name.Contains(":") Then
                Dim sSymNameSpit As String() = Split(AssociatedSymbols(1).Name, ":")
                sSymName = sSymNameSpit(1)
            Else
                sSymName = AssociatedSymbols(1).Name
            End If

            Dim sCellName As String = AssociatedCells(1).Name

            sSymbolCell = sSymName & "-" & sCellName

            oCompAtts = New Object() {sSymbolCell, alsymbols, alCells}

            Return oCompAtts
        End If

    End Function

    Sub CreateGenericPDBEntries()

        For Each part In parts

            Dim sGenericEntry As String

            If dicGenericEntries.TryGetValue(part.Number, sGenericEntry) Then

                If alCreatedEntries.Contains(sGenericEntry) Then
                    part.Delete()
                Else
                    part.Number = sGenericEntry
                    part.Name = sGenericEntry
                    part.Label = sGenericEntry
                    alCreatedEntries.Add(sGenericEntry)

                End If

            End If

        Next

    End Sub

    Private Sub LookForAlternateMappings(ByVal sSymbolCellName As String, ByVal alCells As ArrayList, ByVal alSyms As ArrayList)
        Dim j As Integer = 1
        Dim bFoundMatch As Boolean = False
        Do Until Not dicGenericPartAtts.ContainsKey(sSymbolCellName & "_" & j) Or (bFoundMatch = True)

            Dim oGenericCompAtts As Object
            dicGenericPartAtts.TryGetValue(sSymbolCellName & "_" & j, oGenericCompAtts)

            Dim sGenericSymbolCellName As String = oGenericCompAtts(0)
            Dim alGenericCells As New ArrayList
            For Each sCell In oGenericCompAtts(2)
                alGenericCells.Add(sCell)
            Next

            Dim alGenericSyms As New ArrayList
            For Each sSym In oGenericCompAtts(1)
                alGenericSyms.Add(sSym)
            Next

            If alGenericCells.Count = alCells.Count Then

                For Each sCell In alCells

                    If alGenericCells.Contains(sCell) Then

                        alGenericCells.Remove(sCell)

                    End If

                Next

                If alGenericCells.Count = 0 Then

                    If alGenericSyms.Count = alSyms.Count Then

                        For Each sSym In alSyms

                            If alGenericSyms.Contains(sSym) Then

                                alGenericSyms.Remove(sSym)

                            End If

                        Next

                        If alGenericSyms.Count = 0 Then

                            bFoundMatch = True

                            Dim alPNs As ArrayList
                            If dicGenericPDB.TryGetValue(sSymbolCellName & "_" & j, alPNs) Then
                                Dim alTempPns As New ArrayList
                                alTempPns = alPNs
                                alTempPns.Add(part.Number)

                                dicGenericPDB.Remove(sSymbolCellName & "_" & j)

                                dicGenericPDB.Add(sSymbolCellName & "_" & j, alTempPns)
                            Else
                                Dim alTempPns As New ArrayList
                                alTempPns.Add(part.Number)

                                dicGenericPDB.Add(sSymbolCellName & "_" & j, alTempPns)

                            End If

                        End If

                    End If
                End If

            End If

            j += 1

        Loop

        If bFoundMatch = False Then
            Dim oGenericCompAtts As Object = New Object() {sSymbolCellName & "_" & j, alSyms, alCells}
            Dim alTempPns As New ArrayList
            alTempPns.Add(part.Number)

            dicGenericPartAtts.Add(sSymbolCellName & "_" & j, oGenericCompAtts)
            dicGenericPDB.Add(sSymbolCellName & "_" & j, alTempPns)

            'oSheet.Cells(iRowCount, 1) = sSymbolCellName & "_" & j
            'Dim sCells As String = Nothing
            'For Each sCell In alCells
            '    If sCells = Nothing Then
            '        sCells = sCell
            '    Else

            '        sCells = sCell & "," & sCells

            '    End If
            'Next

            'oSheet.Cells(iRowCount, 3) = sCells

            'Dim sSyms As String = Nothing
            'For Each sSym In alSyms
            '    If sSyms = Nothing Then
            '        sSyms = sSym
            '    Else

            '        sSyms = sSym & "," & sSyms

            '    End If
            'Next

            'oSheet.Cells(iRowCount, 2) = sSyms

            iRowCount += 1

        End If

    End Sub

End Class