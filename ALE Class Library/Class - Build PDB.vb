Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Text

Public Class Build_PDB

#Region "Fields + Properties + Events + Delegates + Enums"

    Property Errors() As List(Of String)
        Get
            Return _Errors
        End Get
        Set(ByVal value As List(Of String))
            _Errors = value
        End Set
    End Property

    Property Notes() As List(Of String)
        Get
            Return _Notes
        End Get
        Set(ByVal value As List(Of String))
            _Notes = value
        End Set
    End Property

    Property Warnings() As List(Of String)
        Get
            Return _Warnings
        End Get
        Set(ByVal value As List(Of String))
            _Warnings = value
        End Set
    End Property

#End Region

#Region "Public Fields + Properties + Events + Delegates + Enums"

    Property aalGates As AAL.Gates

    'Property objPartInfo As Object
    'Dim oCentralLibInfo As New ALE_Central_Library.Read
    Property aalPart As AAL.Part

    Property aalSymbolPartitions As AAL.SymbolPartitions

    Property AltSymbols As New List(Of AAL.Symbol)

    Property b_Healing As Boolean = False

    Property b_RemoveIncompleteParts As Boolean = True

    'Boolean
    Property b_Success As Boolean

    Property bRemoveInvalidObjects As Boolean = False

    Property bValidCellsRequired As Boolean = True

    Event eUpdateStatus(status As String)

    'Dictionary
    Property Heights As New Dictionary(Of String, Double)(StringComparer.OrdinalIgnoreCase)

    'Integer
    Property intPartType As Integer

    Property iPartCount As Integer

    Property LibraryData As Data

    'Property NCPins As ArrayList
    Property LogAlternateSymbols As New ArrayList

    Event LogError()

    'Events
    Event LogNote()

    Enum LogType
        Warning
        Note
        Err
    End Enum

    Event LogWarning()

    'ArrayList
    Property PartAtts As ArrayList

    'String
    Property PartLabel As String

    Property PartsBuilt As Integer
    Property PartsFailed As Integer

    'Object
    'Property pdbPartition As MGCPCBPartsEditor.IMGCPDBPartition

    Public Property readSymbols As Boolean = True

    Property xmlDebug As Xml.XmlDocument

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    Dim _CellPinNumbers As New List(Of String)
    Dim _Errors As New List(Of String)
    Dim _FullPack As String = Nothing
    Dim _ImplicitPinNumbers As New List(Of String)
    Dim _MasterCellPins As New ArrayList()
    Dim _Notes As New List(Of String)
    Dim _partCount As Integer

    'List
    Dim _PinNames As New List(Of String)

    Dim _PinNumbers As New List(Of String)
    Dim _PinsInMappingCount As Integer = 0
    Dim _SymSupplyPins As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
    Dim _Warnings As New List(Of String)
    Dim b_findRefDes As Boolean
    Dim b_GetPinNumbers As Boolean = True
    Dim b_getSymSupplyPins As Boolean = False
    Dim b_PossibleFracturedPart As Boolean = True
    Dim b_PossibleSlottedPart As Boolean = False
    Dim nodePart As Xml.XmlNode

    'Dim oWriteXMLMapping As New AAL.XMLMapping
    Dim pdbPartName As String

#End Region

#Region "Public Methods"

    Function BuildGate(ByRef pdbPart As MGCPCBPartsEditor.Part, ByVal oSymbol As AAL.Symbol) As MGCPCBPartsEditor.Gate

        Dim l_PinNames As New List(Of String)

        Dim pdbGate As MGCPCBPartsEditor.Gate = pdbPart.PinMapping.PutGate("gate" & oSymbol.Name, oSymbol.Slots(0).SymbolPins.Count, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeLogical)

        Dim iPin As Integer = 0
        Dim sPinName As String
        Dim sPinType

        Dim sb_PinTooLong As New StringBuilder

        For Each aalPin As AAL.SymbolPin In oSymbol.Slots(0).SymbolPins.Values

            sPinType = aalPin.Type
            sPinName = aalPin.Name
            sPinName = sPinName.Replace("<", "[")
            sPinName = sPinName.Replace(">", "]")
            sPinName = sPinName.Replace("<", "[")
            sPinName = sPinName.Replace(">", "]")

            iPin += 1

            If Not l_PinNames.Contains(sPinName) Then
                Try
                    pdbGate.PutPinDefinition(iPin, sPinName, MGCPCBPartsEditor.EPDBPinPropertyType.epdbPinPropertyPinType, sPinType)
                    l_PinNames.Add(sPinName)
                Catch ex As Exception
                    sb_PinTooLong.AppendLine("(" & sPinName.Length & "): " & sPinName)
                End Try
            Else
                Log(LogType.Err, "[Invalid Input] Symbol " & oSymbol.Partition & ":" & oSymbol.Name & " contains duplicate pin names " & sPinName)

                Return Nothing
            End If

        Next

        If sb_PinTooLong.Length > 0 Then
            Log(LogType.Err, "[Invalid Input] Symbol " & oSymbol.Partition & ":" & oSymbol.Name & " contains pin names with lengths greater than automation can currently handle:" & Environment.NewLine & vbTab & vbTab & vbTab & vbTab & sb_PinTooLong.ToString())

            Return Nothing
        Else
            Return pdbGate
        End If

    End Function

    Public Sub Log(ByVal Type As LogType, ByVal Reason As String)
        'Logs warning, notes, and errors on the PDB build process
        'for a given partition and a given part

        Select Case Type
            Case LogType.Warning
                If Not _Warnings.Contains(Reason) Then
                    _Warnings.Add(Reason)
                    RaiseEvent eUpdateStatus("Warning: " & Reason)
                    RaiseEvent LogWarning()
                End If
            Case LogType.Note
                If Not _Notes.Contains(Reason) Then
                    _Notes.Add(Reason)
                    RaiseEvent eUpdateStatus("Note: " & Reason)
                    RaiseEvent LogNote()
                End If

            Case Else
                If Not _Errors.Contains(Reason) Then
                    _Errors.Add(Reason)
                    RaiseEvent eUpdateStatus("Error: " & Reason)
                    RaiseEvent LogError()
                End If

        End Select

    End Sub

    Function NewPart(ByRef pdbPart As MGCPCBPartsEditor.Part)

        _SymSupplyPins.Clear()
        _partCount = 1
        b_getSymSupplyPins = False

        Try
            If aalPart.ImplicitPins.Count = 0 Then b_getSymSupplyPins = True
        Catch ex As Exception

        End Try

        If aalPart.Gates.Count > 0 Then
            aalGates = aalPart.Gates
            b_GetPinNumbers = False
        End If

        If aalPart.Number.Contains(":") Then
            Dim PartNumSplit As String() = Split(aalPart.Number, ":")
            aalPart.Number = PartNumSplit(1)
        End If

        If aalPart.Number.Contains("|") Then
            Dim sPNPipeSplit As String() = Split(aalPart.Number, "|")
            PartLabel = aalPart.Number
            If Not LibraryData.PDBType.ContainsKey(sPNPipeSplit(0)) Then
                aalPart.Number = sPNPipeSplit(0)
            End If
        End If

        'Number
        pdbPart.Number = aalPart.Number

        ' Name
        If IsNothing(aalPart.Name) Then
            pdbPart.Name = aalPart.Number
        Else
            pdbPart.Name = aalPart.Name
        End If

        'Label
        If Not IsNothing(aalPart.Label) Then
            pdbPart.Label = aalPart.Label
        ElseIf IsNothing(PartLabel) Then
            pdbPart.Label = aalPart.Number
        Else
            pdbPart.Label = PartLabel
        End If

        'RefDes
        If Not aalPart.RefDes = Nothing Then

            If aalPart.RefDes.Contains("_") Then aalPart.RefDes = aalPart.RefDes.Replace("_", "")

            If aalPart.RefDes.Length > 3 Then aalPart.RefDes = SplitBySize(aalPart.RefDes, 3)

            Dim lLen As Long
            Dim sChar As String
            Dim sRefDes As String

            lLen = Len(aalPart.RefDes)
            For iRef = 1 To Len(aalPart.RefDes)
                If (Not IsNumeric(Mid(aalPart.RefDes, iRef, 1))) Or (Mid(aalPart.RefDes, iRef, 1) = "_") Then
                    sRefDes = sRefDes & Mid(aalPart.RefDes, iRef, 1)
                End If
            Next iRef

            aalPart.RefDes = sRefDes

            pdbPart.RefDesPrefix = aalPart.RefDes
            b_findRefDes = False
        Else

            b_findRefDes = True

        End If

        'pdbPart Type

        If b_findRefDes = False Then
            If aalPart.Type > 0 Then
                pdbPart.Type = aalPart.Type
            Else
                If LibraryData.PDBType.ContainsKey(aalPart.RefDes) Then
                    Dim value As String = LibraryData.PDBType.Item(aalPart.RefDes)
                    Select Case value
                        Case "BJT"
                            pdbPart.Type = 1
                        Case "Capacitor"
                            pdbPart.Type = 2
                        Case "Connector"
                            pdbPart.Type = 4
                        Case "Diode"
                            pdbPart.Type = 8
                        Case "IC"
                            pdbPart.Type = 16
                        Case "Inductor"
                            pdbPart.Type = 32
                        Case "Jumper"
                            pdbPart.Type = 128
                        Case "RCNetwork"
                            pdbPart.Type = 2048
                        Case "Resistor"
                            pdbPart.Type = 4096
                        Case "Switch"
                            pdbPart.Type = 8192
                        Case "VoltageDivider"
                            pdbPart.Type = 16384
                        Case Else
                            pdbPart.Type = 512
                    End Select
                Else
                    pdbPart.Type = 512
                End If
            End If
        Else
            pdbPart.Type = 512
        End If

        If Not IsNothing(xmlDebug) Then
            nodePart = AAL.XMLMapping.WritePartNode(xmlDebug, xmlDebug.DocumentElement, aalPart.Partition, pdbPart.Number)
        End If

        Try
            pdbPart.Commit()
            'frmBuild_PDB.dicPDBParts.Add(sPartNum, pdbPartition.Name)
        Catch ex As Exception
            Log(LogType.Err, "[Build PDB Error] Could not commit part after setting part's type. " & ex.Message)
            pdbPart.Delete()
            Return False
        End Try

        If String.IsNullOrEmpty(aalPart.Description) Then
            pdbPart.Description = "Built by the " & My.Application.Info.Title & " v" & My.Application.Info.Version.ToString()
        Else
            pdbPart.Description = aalPart.Description
        End If

        Dim oProperty As MGCPCBPartsEditor.Property

        If aalPart.Height = 0 And Heights.Count > 0 Then

            For Each sCell As String In aalPart.Cells.Keys

                If Heights.ContainsKey(sCell) Then
                    Dim Height As Double = Heights.Item(sCell.ToUpper)
                    If Height > aalPart.Height Then
                        aalPart.Height = Height
                    End If
                End If

            Next

            Try
                If Not IsNothing(aalPart.Unit) Then
                    oProperty = pdbPart.PutPropertyEx("Height", aalPart.Height, aalPart.Unit)
                Else
                    oProperty = pdbPart.PutPropertyEx("Height", aalPart.Height)
                End If
            Catch ex As Exception
                Log(LogType.Warning, "[Build PDB Error] Cannot add property: ""Height"", with value: " & aalPart.Height)
            End Try

        ElseIf Not aalPart.Height = 0 Then
            If Not IsNothing(xmlDebug) Then
                AAL.XMLMapping.WritePropertyNode(xmlDebug, nodePart, "Height", aalPart.Height)
            End If

            Try
                If Not IsNothing(aalPart.Unit) Then
                    oProperty = pdbPart.PutPropertyEx("Height", aalPart.Height, aalPart.Unit)
                Else
                    oProperty = pdbPart.PutPropertyEx("Height", aalPart.Height)
                End If
            Catch ex As Exception
                Log(LogType.Warning, "[Build PDB Error] Cannot add property: ""Height"", with value: " & aalPart.Height)
            End Try
        End If

        If aalPart.Properties.Count > 0 Then
            AddProperties(pdbPart, aalPart)
        End If

        Dim Success As Boolean

        If IsNothing(aalGates) Then
            Success = createPinMapping(pdbPart, aalPart)
        Else
            Success = addPinMapping(pdbPart)
        End If

        If Success = True And pdbPart.Incomplete = False Then
            PartsBuilt += 1
            Dim bCommit As Boolean = pdbPart.Incomplete

            'If Not IsNothing(xmlDebug) Then
            '    xmlDebug.DocumentElement.RemoveChild(nodePart)
            'End If

            If (aalPart.MissingCells.Count > 0) Then
                For Each cell As String In aalPart.MissingCells
                    Log(LogType.Warning, "[Invalid Input] Cell: " & cell & " was not found.")
                Next
            End If

            If (aalPart.MissingSymbols.Count > 0) Then
                For Each symbol As String In aalPart.MissingSymbols
                    Log(LogType.Warning, "[Invalid Input] Symbol: " & symbol & " was not found.")
                Next
            End If

            Return True
        Else
            If Errors.Count = 0 Then
                Log(LogType.Err, "[Build PDB Error] Part mapping is incomplete. Check symbol compatibility.")
            End If

            PartsFailed += 1

            If b_RemoveIncompleteParts = True Then
                pdbPart.Delete()
            End If

            Return False
        End If

    End Function

    Public Function SplitBySize(ByVal strInput As String, ByVal iSize As Integer)
        Dim alRefDes As New ArrayList()
        Dim iLength As Integer = strInput.Length()
        Dim iWords As Integer = iLength / iSize + IIf((iLength Mod iSize <> 0), 1, 0)
        Dim i As Integer
        For i = 0 To iLength Step iSize
            alRefDes.Add(Mid(strInput, i + 1, iSize))
        Next i
        Return alRefDes(0)
    End Function

#End Region

#Region "Internal Methods"

    Friend Sub Dispose()
        If Not IsNothing(aalGates) Then
            aalGates.Clear()
            aalGates = Nothing
        End If

        If Not IsNothing(aalPart) Then
            aalPart.Dispose()
            aalPart = Nothing
        End If

        If Not IsNothing(aalSymbolPartitions) Then
            aalSymbolPartitions.Clear()
            aalSymbolPartitions = Nothing
        End If

        If Not IsNothing(AltSymbols) Then
            AltSymbols.Clear()
            AltSymbols = Nothing
        End If

        If Not IsNothing(Heights) Then
            Heights.Clear()
            Heights = Nothing
        End If

        LibraryData = Nothing

        If Not IsNothing(LogAlternateSymbols) Then
            LogAlternateSymbols.Clear()
            LogAlternateSymbols = Nothing
        End If

        If Not IsNothing(PartAtts) Then
            PartAtts.Clear()
            PartAtts = Nothing
        End If

        If Not IsNothing(_CellPinNumbers) Then
            _CellPinNumbers.Clear()
            _CellPinNumbers = Nothing
        End If

        If Not IsNothing(_Errors) Then
            _Errors.Clear()
            _Errors = Nothing
        End If

        If Not IsNothing(_ImplicitPinNumbers) Then
            _ImplicitPinNumbers.Clear()
            _ImplicitPinNumbers = Nothing
        End If

        If Not IsNothing(_MasterCellPins) Then
            _MasterCellPins.Clear()
            _MasterCellPins = Nothing
        End If

        If Not IsNothing(_Notes) Then
            _Notes.Clear()
            _Notes = Nothing
        End If

        If Not IsNothing(_PinNames) Then
            _PinNames.Clear()
            _PinNames = Nothing
        End If

        If Not IsNothing(_PinNumbers) Then
            _PinNumbers.Clear()
            _PinNumbers = Nothing
        End If

        If Not IsNothing(_SymSupplyPins) Then
            _SymSupplyPins.Clear()
            _SymSupplyPins = Nothing
        End If

        If Not IsNothing(_Warnings) Then
            _Warnings.Clear()
            _Warnings = Nothing
        End If

        nodePart = Nothing

    End Sub

#End Region

#Region "Private Methods"

    Private Function AddGateAndSlots(ByRef pdbMapping As MGCPCBPartsEditor.PinMapping, dic_SymRefs As Dictionary(Of String, MGCPCBPartsEditor.SymbolReference)) As Boolean

        For Each aalGate As AAL.Gate In aalGates.Values
            RaiseEvent eUpdateStatus("")
            If dic_SymRefs.ContainsKey(aalGate.Symbol.Partition & ":" & aalGate.Symbol.Name) Then

                Dim symRef As MGCPCBPartsEditor.SymbolReference = dic_SymRefs.Item(aalGate.Symbol.Partition & ":" & aalGate.Symbol.Name)

                Dim PinList As New Dictionary(Of String, Integer)
                RaiseEvent eUpdateStatus(vbTab & "Adding gates:")
                Dim pdbGate As MGCPCBPartsEditor.Gate = pdbMapping.PutGate(aalGate.Name, aalGate.Slots.ElementAt(0).Value.SymbolPins.Count, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeLogical)

                Dim iPinIndex As Integer = 0
                Dim sPinName As String
                Dim sPinType

                For Each aalPin As AAL.SymbolPin In aalGate.Slots.ElementAt(0).Value.SymbolPins.Values

                    sPinType = aalPin.Type
                    sPinName = aalPin.Name
                    sPinName = sPinName.Replace("<", "[")
                    sPinName = sPinName.Replace(">", "]")
                    RaiseEvent eUpdateStatus(vbTab & vbTab & "Adding pin name " & aalPin.Name & " to gate.")
                    iPinIndex += 1

                    pdbGate.PutPinDefinition(iPinIndex, sPinName, MGCPCBPartsEditor.EPDBPinPropertyType.epdbPinPropertyPinType, sPinType)

                    PinList.Add(sPinName, iPinIndex)

                Next

                Dim l_pdbSlot As New ArrayList()

                Dim iSlot As Integer = 0
                RaiseEvent eUpdateStatus("")
                For Each aalSlot As AAL.Slot In aalGate.Slots.Values

                    l_pdbSlot.Add(pdbMapping.PutSlot(pdbGate, symRef))
                    RaiseEvent eUpdateStatus(vbTab & "Adding slot to gate.")
                    For Each aalPin As AAL.SymbolPin In aalSlot.SymbolPins.Values
                        PinList.TryGetValue(aalPin.Name, iPinIndex)

                        Dim pinNumber As String = aalPin.Number

                        If String.IsNullOrWhiteSpace(pinNumber) Then
                            Log(LogType.Err, "[Invalid Input] Symbol pin name: " & aalPin.Name & " of symbol " & aalGate.Symbol.Partition & ":  " & aalGate.Symbol.Name & " is missing a pin number.")
                            Return False
                        End If

                        pinNumber = pinNumber.Replace("[", "")
                        pinNumber = pinNumber.Replace("]", "")
                        pinNumber = pinNumber.Replace("{", "")
                        pinNumber = pinNumber.Replace("}", "")
                        pinNumber = pinNumber.Replace("(", "")
                        pinNumber = pinNumber.Replace(")", "")

                        'Pin Number
                        Try

                            If String.IsNullOrEmpty(pinNumber) Then
                                Return False
                            End If

                            If _CellPinNumbers.Contains(pinNumber) Then
                                _CellPinNumbers.Remove(pinNumber)
                            Else
                                If Not _MasterCellPins.Contains(pinNumber) And bValidCellsRequired = True Then
                                    Log(LogType.Err, "[Invalid Input] Cell does not contain pin number " & pinNumber & " found in symbol (" & aalGate.Symbol.Partition & ":" & aalGate.Symbol.Name & ")")
                                    Return False
                                End If
                            End If

                            RaiseEvent eUpdateStatus(vbTab & vbTab & "Adding pin number " & pinNumber & " to slot.")
                            l_pdbSlot(iSlot).PutPin(iPinIndex, pinNumber, aalPin.Name)
                            _PinsInMappingCount += 1
                        Catch ex As Exception
                            Log(LogType.Err, "[Build PDB Error] Could not commit slot number " & iSlot & ", this is normally caused by pins not being defined on the symbol.")

                            Return False

                        End Try

                    Next
                    iSlot += 1
                Next
            Else
                Log(LogType.Err, "[Invalid Input] Could not find symbol reference " & aalGate.Symbol.Partition & ":" & aalGate.Symbol.Name)
                Return False
            End If

        Next

        Return True
    End Function

    Private Function AddMultiSlotted(ByRef dic_PinIndex As Dictionary(Of String, Integer), ByRef aalSymbol As AAL.Symbol, ByRef nodeGate As Xml.XmlNode, ByRef pdbGate As MGCPCBPartsEditor.Gate, ByRef pdbSymbolReference As MGCPCBPartsEditor.SymbolReference, ByRef pdbPart As MGCPCBPartsEditor.Part, ByRef i_SymbolPinCount As Integer, ByVal l_MasterCellPins As ArrayList, ByRef l_pdbSlots As List(Of MGCPCBPartsEditor.Slot), ByRef l_MasterSymbolPins As List(Of String)) As Boolean

        Dim i As Integer = 1

        For Each aalSlot As AAL.Slot In aalSymbol.Slots

            Dim xmlSlot As New AAL.Slot
            xmlSlot.SymbolName = pdbSymbolReference.Name
            xmlSlot.Name = "Slot_" & i
            Dim pdbSlot As MGCPCBPartsEditor.Slot

            Try
                pdbSlot = pdbPart.PinMapping.PutSlot(pdbGate, pdbSymbolReference)
            Catch ex As Exception
                Log(LogType.Err, "[Build PDB Error] " & aalPart.Partition & ":" & aalPart.Number & ". Could not add slot into pdb gate.")
                Return False
            End Try

            Dim iPinIndex As Integer = 0
            For Each aalPin As AAL.SymbolPin In aalSlot.SymbolPins.Values
                iPinIndex += 1

                Dim pinNumber As String = aalPin.Number

                If String.IsNullOrWhiteSpace(pinNumber) Then
                    Log(LogType.Err, "[Invalid Input] Symbol pin name: " & aalPin.Name & " of symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " is missing a pin number.")
                    Return False
                End If

                pinNumber = pinNumber.Replace("[", "")
                pinNumber = pinNumber.Replace("]", "")
                pinNumber = pinNumber.Replace("{", "")
                pinNumber = pinNumber.Replace("}", "")
                pinNumber = pinNumber.Replace("(", "")
                pinNumber = pinNumber.Replace(")", "")

                'Pin Number

                If _CellPinNumbers.Contains(pinNumber) Then
                    _CellPinNumbers.Remove(pinNumber)
                Else
                    If Not l_MasterCellPins.Contains(pinNumber) And bValidCellsRequired = True Then
                        Log(LogType.Err, "[Invalid Input] Cell does not contain pin number " & pinNumber & " found in symbol (" & aalSymbol.Partition & ":" & aalSymbol.Name & ")")
                        Return False
                    End If
                End If

                If l_MasterSymbolPins.Contains(pinNumber) Then l_MasterSymbolPins.Remove(pinNumber)

                'Dim iPreviousIndex As Integer
                'If dic_PinIndex.TryGetValue(aalPin.Name, iPreviousIndex) Then
                '    pdbSlot.PutPin(iPreviousIndex, aalPin.Number, aalPin.Name)
                'Else
                '    pdbSlot.PutPin(iPinIndex, aalPin.Number, aalPin.Name)
                '    dic_PinIndex.Add(aalPin.Name, iPinIndex)
                '    iPinIndex += 1
                'End If

                If IsNothing(dic_PinIndex) Then
                    dic_PinIndex = New Dictionary(Of String, Integer)
                    Try
                        pdbSlot.PutPin(iPinIndex, pinNumber, aalPin.Name)
                    Catch ex As Exception
                        'oLogReport.Add("Error", pdbPart.Number, "Could not commit slot number " & iSlot & ", this is normally caused by pins not being defined on the symbol.")
                        Return False
                    End Try
                    i_SymbolPinCount += 1
                    dic_PinIndex.Add(pinNumber, iPinIndex)
                    xmlSlot.SymbolPins(aalPin.Name) = aalPin
                Else

                    If Not dic_PinIndex.ContainsKey(pinNumber) Then
                        Try
                            pdbSlot.PutPin(iPinIndex, pinNumber, aalPin.Name)
                        Catch ex As Exception
                            'oLogReport.Add("Error", pdbPart.Number, "Could not commit slot number " & iSlot & ", this is normally caused by pins not being defined on the symbol.")
                            Return False
                        End Try
                        i_SymbolPinCount += 1
                        xmlSlot.SymbolPins(aalPin.Name) = aalPin
                        dic_PinIndex.Add(pinNumber, iPinIndex)
                    Else
                        Dim iPreviousIndex As Integer = dic_PinIndex.Item(pinNumber)
                        Try
                            pdbSlot.PutPin(iPreviousIndex, pinNumber, aalPin.Name)
                        Catch ex As Exception
                            'oLogReport.Add("Error", pdbPart.Number, "Could not commit slot number " & iSlot & ", this is normally caused by pins not being defined on the symbol.")
                            Return False
                        End Try

                        i_SymbolPinCount += 1
                        xmlSlot.SymbolPins(aalPin.Name) = aalPin
                    End If
                End If

                _PinsInMappingCount += 1

            Next

            l_pdbSlots.Add(pdbSlot)
            If Not IsNothing(xmlDebug) Then
                AAL.XMLMapping.WriteSlotNode(xmlDebug, nodeGate, xmlSlot)
            End If

            i += 1
        Next

        Return True

    End Function

    Private Sub addNCPins(ByRef pdbMapping As MGCPCBPartsEditor.IMGCPDBPinMapping)
        If _CellPinNumbers.Count > 0 Then
            Dim sNCPin
            For Each sNCPin In _CellPinNumbers
                AddNoConnectPin(pdbMapping, sNCPin)
            Next
        End If
    End Sub

    Private Sub AddNoConnectPin(ByRef pinMapping As MGCPCBPartsEditor.PinMapping, ByVal sMissingPinNumber As Object)
        Dim objNCGate As MGCPCBPartsEditor.Gate = Nothing
        If pinMapping.NoConnect.Count > 0 Then
            objNCGate = pinMapping.NoConnect(1)
        Else
            objNCGate = pinMapping.PutGate("No Connect", 1, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeNoConnect)
        End If

        ' Make sure this pin is not already defined as a no-connect
        Dim objNCSlot

        Dim SymbolReference As MGCPCBPartsEditor.SymbolReference = Nothing

        'symRefParam = New System.Runtime.InteropServices.DispatchWrapper(SymbolReference)

        objNCSlot = pinMapping.PutSlot(objNCGate, SymbolReference)
        objNCSlot.PutPin(1, sMissingPinNumber)
    End Sub

    Private Function addPinMapping(ByRef pdbPart As MGCPCBPartsEditor.Part) As Boolean

        Dim Success As Boolean

        If aalPart.Cells.Count > 0 Then
            Success = CreateCellReferences(pdbPart)

            If Success = True Then
                Try
                    pdbPart.PinMapping.Commit()
                Catch ex As Exception
                    Log(LogType.Err, "[Build PDB Error] Could not save cells to PDB.")

                    Return False
                End Try
            Else

                If (aalPart.MissingCells.Count > 0) Then
                    For Each cell As String In aalPart.MissingCells
                        Log(LogType.Warning, "[Invalid Input] Cell: " & cell & " was not found.")
                    Next
                End If

                Log(LogType.Err, "[Build PDB Error] Could not add cells to PDB.")
                Return False
            End If
        End If

        Success = AddSymbolReferences(pdbPart)

        If Success = True Then
            Try
                pdbPart.PinMapping.Commit()
            Catch ex As Exception
                Log(LogType.Err, "[Build PDB Error] Could not save symbols to PDB.")
                Return False
            End Try
        Else
            Log(LogType.Err, "[Build PDB Error] Could not add symbols to PDB.")
            Return False
        End If

        If pdbPart.SymbolReferences.Count = 0 Then Return False

        Dim dic_SymRefs As New Dictionary(Of String, MGCPCBPartsEditor.SymbolReference)

        For Each SymRef As MGCPCBPartsEditor.SymbolReference In pdbPart.SymbolReferences
            dic_SymRefs.Add(SymRef.Name, SymRef)
        Next

        Success = AddGateAndSlots(pdbPart.PinMapping, dic_SymRefs)

        If Success = False Then Return False

        Try
            pdbPart.PinMapping.Commit()
        Catch ex As Exception
            Log(LogType.Err, "[Build PDB Error] Could not commit pin mapping.")

            'sErrorReason = "Could not commit pin mapping after adding Supply pins"
            'ErrorLog()
            Return False
        End Try

        'b_Success = addSupplyPins(pdbPart.PinMapping)

        'If b_Success = False Then
        '    Return False
        'End If

        'Try
        '    pdbPart.PinMapping.Commit()
        'Catch ex As Exception
        '    Log(LogType.Other, "Could not commit pin mapping after adding Supply pins")
        '
        '    'oLogReport.Add("Error", pdbPart.Number, "Could not commit pin mapping after adding Supply pins")
        '    Return False
        'End Try

        addNCPins(pdbPart.PinMapping)

        Try
            pdbPart.PinMapping.Commit()
        Catch ex As Exception
            Log(LogType.Err, "[Build PDB Error] Could not commit pin mapping after adding NC pins.")

            'oLogReport.Add("Error", pdbPart.Number, "Could not commit pin mapping after adding NC pins")
            Return False
        End Try

        Return True

    End Function

    Private Function AddProperties(ByRef pdbPart As MGCPCBPartsEditor.Part, ByRef oPart As AAL.Part)
        'Adds properties to parts editor part using the properties
        'used in the AAL part object. Returns true when done.

        For Each kvp As KeyValuePair(Of String, String) In oPart.Properties
            Dim propObj

            propObj = Nothing

            Try
                propObj = pdbPart.PutPropertyEx(kvp.Key, kvp.Value)
            Catch ex As Exception
                Log(LogType.Warning, "[Build PDB Error] Cannot add this property " & kvp.Key & " until it is defined as a common property.")
            End Try

        Next

        Return True

    End Function

    Private Function AddSupplyPin(ByRef pdbMapping As MGCPCBPartsEditor.IMGCPDBPinMapping, ByVal sSupplyName As String, ByVal sSupplyPinNumber As Object, ByRef objSupplyGate As MGCPCBPartsEditor.Gate) As Boolean

        ' Make sure this pin is not already defined as a no-connect
        Dim objSupplySlot As MGCPCBPartsEditor.Slot

        Dim SymbolReference As MGCPCBPartsEditor.SymbolReference = Nothing

        'symRefParam = New System.Runtime.InteropServices.DispatchWrapper(SymbolReference)

        If _CellPinNumbers.Contains(sSupplyPinNumber) Then
            _CellPinNumbers.Remove(sSupplyPinNumber)
        Else
            If Not _MasterCellPins.Contains(sSupplyPinNumber) And Not bValidCellsRequired = True Then
                Return False
            End If
        End If

        objSupplySlot = pdbMapping.PutSlot(objSupplyGate, SymbolReference)
        objSupplySlot.PutPin(1, sSupplyPinNumber)
        Return True
    End Function

    Private Function addSupplyPins(ByRef pinMapping As MGCPCBPartsEditor.PinMapping) As Boolean

        If aalPart.ImplicitPins.Count = 0 Then
            aalPart.ImplicitPins = _SymSupplyPins
        End If

        Dim Results As Boolean = True

        For Each kvp As KeyValuePair(Of String, List(Of String)) In aalPart.ImplicitPins
            'Grab part number and part attributes:
            Dim sSupplyName As String = kvp.Key
            Dim l_SupplyPins As List(Of String) = kvp.Value
            Dim objSupplyGate As MGCPCBPartsEditor.Gate = Nothing
            objSupplyGate = pinMapping.PutGate(sSupplyName, 1, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeSupply)

            For Each sSupplyPinNumber As String In l_SupplyPins
                If AddSupplyPin(pinMapping, sSupplyName, sSupplyPinNumber, objSupplyGate) = False Then
                    Log(LogType.Err, "[Invalid Input] Cell does not contain pin number " & sSupplyPinNumber & " currently defined as an implicit pin(" & sSupplyName & ")")
                    Results = False
                End If

            Next

            If Not IsNothing(xmlDebug) Then
                AAL.XMLMapping.WriteImplicitNode(xmlDebug, nodePart, sSupplyName, l_SupplyPins)
            End If

        Next

        Return Results

    End Function

    Private Function AddSymbolReferences(ByRef pdbPart As MGCPCBPartsEditor.Part) As Boolean
        RaiseEvent eUpdateStatus(vbTab & "Adding Symbol(s):")

        Dim iSkippedSymbolCount As Integer = 0
        For Each aalSymbol As AAL.Symbol In aalPart.Symbols.Values
            Dim l_LocalPinNumbers As New List(Of String)
            Dim dic_DupPinCheck As New Dictionary(Of String, String)

            For Each kvp As KeyValuePair(Of String, List(Of String)) In _SymSupplyPins

                For Each sPin As String In kvp.Value
                    dic_DupPinCheck.Add(sPin, kvp.Key)
                Next

            Next

            If _partCount < aalSymbol.Slots.Count Then _partCount = aalSymbol.Slots.Count

            For Each kvp_Signal As KeyValuePair(Of String, List(Of String)) In aalSymbol.SupplyPins

                Dim l_Pins As List(Of String)
                If _SymSupplyPins.TryGetValue(kvp_Signal.Key, l_Pins) Then
                    _SymSupplyPins.Remove(kvp_Signal.Key)
                Else
                    l_Pins = New List(Of String)
                End If

                For Each sPin As String In kvp_Signal.Value
                    Dim sSupply As String
                    If Not String.IsNullOrWhiteSpace(sPin) Then
                        If dic_DupPinCheck.TryGetValue(sPin, sSupply) Then
                            If Not sSupply = kvp_Signal.Key Then
                                Log(LogType.Err, "[Invalid Input] Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " calls for supply pin number " & sPin & " to be defined as " & kvp_Signal.Key & " but pin " & sPin & " is already defined as " & sSupply)
                                Return False
                            End If
                        Else
                            If _PinNumbers.Contains(sPin) Then
                                Log(LogType.Err, "[Invalid Input] Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " contains an implicit pin (" & sPin & ") that was previously defined as a logical pin.")
                                Return False
                            End If
                            If Not l_Pins.Contains(sPin) Then l_Pins.Add(sPin)
                            If Not _ImplicitPinNumbers.Contains(sPin) Then _ImplicitPinNumbers.Add(sPin)
                            dic_DupPinCheck.Add(sPin, kvp_Signal.Key)
                        End If
                    End If
                Next

                _SymSupplyPins.Add(kvp_Signal.Key, l_Pins)
            Next

            For Each aalSlot As AAL.Slot In aalSymbol.Slots
                For Each aalPin As AAL.SymbolPin In aalSlot.SymbolPins.Values

                    Dim pinNumber As String = aalPin.Number

                    If String.IsNullOrWhiteSpace(pinNumber) Then
                        Log(LogType.Err, "[Invalid Input] Symbol pin name: " & aalPin.Name & " of symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " is missing a pin number.")
                        Return False
                    End If

                    pinNumber = pinNumber.Replace("[", "")
                    pinNumber = pinNumber.Replace("]", "")
                    pinNumber = pinNumber.Replace("{", "")
                    pinNumber = pinNumber.Replace("}", "")
                    pinNumber = pinNumber.Replace("(", "")
                    pinNumber = pinNumber.Replace(")", "")

                    If Not _PinNames.Contains(aalPin.Name, StringComparer.OrdinalIgnoreCase) Then
                        _PinNames.Add(aalPin.Name)
                    End If

                    If _ImplicitPinNumbers.Contains(pinNumber) Then
                        Log(LogType.Err, "[Invalid Input] Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " contains a pin (" & pinNumber & ") that was previously defined as an implicit pin.")

                        Return False
                    ElseIf Not _PinNumbers.Contains(pinNumber, StringComparer.OrdinalIgnoreCase) Then
                        _PinNumbers.Add(pinNumber)
                    ElseIf l_LocalPinNumbers.Contains(pinNumber) And aalSymbol.Slots.Count = 1 Then
                        Log(LogType.Err, "[Invalid Input] Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " contains pins with duplicate pin number: " & pinNumber)

                        Return False
                    Else
                        b_PossibleFracturedPart = False
                        b_PossibleSlottedPart = True
                        If aalSymbol.Slots.Count > 1 Then
                            If (aalPart.Symbols.Count - iSkippedSymbolCount) > 1 Then
                                Dim sNote As String = "Treating part as a slotted part because pin " & pinNumber & " is found on multiple symbols."
                                Log(LogType.Note, sNote)
                            End If
                        ElseIf (aalSymbol.Slots.Count = 1 And Not (aalSymbol.LogicalPinCount + aalSymbol.ImplicitPinCount = _CellPinNumbers.Count) And bValidCellsRequired = True) Then
                            b_PossibleSlottedPart = False
                        End If
                    End If
                    l_LocalPinNumbers.Add(pinNumber)

                Next
            Next

            If aalSymbol.Slots.Count = 1 And (aalSymbol.LogicalPinCount + aalSymbol.ImplicitPinCount = _CellPinNumbers.Count) And bValidCellsRequired = True Then
                _FullPack = aalSymbol.Partition & ":" & aalSymbol.Name
            End If

            Dim oSymbolReference As MGCPCBPartsEditor.SymbolReference = pdbPart.PinMapping.PutSymbolReference(aalSymbol.Partition & ":" & aalSymbol.Name)
            RaiseEvent eUpdateStatus(vbTab & vbTab & aalSymbol.Partition & ":" & aalSymbol.Name)
        Next

        Return True
    End Function

    Private Function BuildGateAndSlots(ByRef pdbPart As MGCPCBPartsEditor.Part, dic_SymRefs As Dictionary(Of String, MGCPCBPartsEditor.SymbolReference), ByRef aalPart As AAL.Part) As Boolean

        Dim i_SymbolPinCount As Integer = 0

        _MasterCellPins = New ArrayList(_CellPinNumbers)
        'l_MasterCellPins = alCellPinNumbers
        Dim l_MasterSymbolPins As New List(Of String)(_PinNumbers)

        If b_PossibleFracturedPart = False And dic_SymRefs.Count > 1 Then
            Dim numOfAttempts As Integer = 0
BuildGateInit:
            Dim dic_Gates As New Dictionary(Of MGCPCBPartsEditor.Gate, List(Of MGCPCBPartsEditor.Slot))

            If b_PossibleSlottedPart Then

                Dim l_pdbSlots As New List(Of MGCPCBPartsEditor.Slot)

                Dim oMasterSymbol As AAL.Symbol

                For Each aalSymbol As AAL.Symbol In aalPart.Symbols.Values

                    If ((Not String.Compare(aalSymbol.Partition & ":" & aalSymbol.Name, _FullPack, True) = 0) Or (String.IsNullOrEmpty(_FullPack))) And IsNothing(oMasterSymbol) Then

                        oMasterSymbol = aalSymbol

                    End If

                Next

                pdbPartName = pdbPart.Number
                Dim pdbGate As MGCPCBPartsEditor.Gate = BuildGate(pdbPart, oMasterSymbol)

                If IsNothing(pdbGate) Then
                    Return False
                End If

                Dim nodeGate As Xml.XmlNode
                If Not IsNothing(xmlDebug) Then
                    nodeGate = AAL.XMLMapping.WriteGatesNode(xmlDebug, nodePart, oMasterSymbol)
                Else
                    nodeGate = Nothing
                End If

                Dim dic_pinList As Dictionary(Of String, Integer)

                For Each pdbSymbolReference As MGCPCBPartsEditor.SymbolReference In dic_SymRefs.Values

                    If Not String.Compare(pdbSymbolReference.Name, _FullPack, True) = 0 Then

                        Dim oSymbol As AAL.Symbol

                        aalPart.Symbols.TryGetValue(pdbSymbolReference.Name, oSymbol)

                        Dim bSuccessful As Boolean = AddMultiSlotted(dic_pinList, oSymbol, nodeGate, pdbGate, pdbSymbolReference, pdbPart, i_SymbolPinCount, _MasterCellPins, l_pdbSlots, l_MasterSymbolPins)

                        If bSuccessful = False Then
                            Return False
                        End If

                    End If

                Next

                dic_Gates.Add(pdbGate, l_pdbSlots)
            Else
                For Each aalSymbol As AAL.Symbol In aalPart.Symbols.Values
                    Dim iPin As Integer = 0
                    If Not String.Compare(aalSymbol.Partition & ":" & aalSymbol.Name, _FullPack, True) = 0 Then

                        Dim pdbGate As MGCPCBPartsEditor.Gate = BuildGate(pdbPart, aalSymbol)

                        If IsNothing(pdbGate) Then
                            Return False
                        Else
                            Dim pdbSymbolReference As MGCPCBPartsEditor.SymbolReference

                            If dic_SymRefs.ContainsKey(aalSymbol.Partition & ":" & aalSymbol.Name) Or dic_SymRefs.ContainsKey(aalSymbol.Name) Then
                                pdbSymbolReference = dic_SymRefs.Item(aalSymbol.Partition & ":" & aalSymbol.Name)

                                Dim pdbSlot As MGCPCBPartsEditor.Slot = pdbPart.PinMapping.PutSlot(pdbGate, pdbSymbolReference)

                                For Each aalPin As AAL.SymbolPin In aalSymbol.Slots.Item(0).SymbolPins.Values
                                    iPin += 1
                                    'Pin Number

                                    Dim pinNumber As String = aalPin.Number

                                    pinNumber = pinNumber.Replace("[", "")
                                    pinNumber = pinNumber.Replace("]", "")
                                    pinNumber = pinNumber.Replace("{", "")
                                    pinNumber = pinNumber.Replace("}", "")
                                    pinNumber = pinNumber.Replace("(", "")
                                    pinNumber = pinNumber.Replace(")", "")

                                    If l_MasterSymbolPins.Contains(pinNumber) Then l_MasterSymbolPins.Remove(pinNumber)

                                    Try

                                        If _CellPinNumbers.Contains(pinNumber) Then
                                            _CellPinNumbers.Remove(pinNumber)
                                        Else
                                            If Not _MasterCellPins.Contains(pinNumber) And bValidCellsRequired = True Then
                                                Log(LogType.Err, "[Invalid Input] Cell does not contain pin number " & pinNumber & " found in symbol (" & aalSymbol.Partition & ":" & aalSymbol.Name & ")")
                                                Return False
                                            End If
                                        End If

                                        pdbSlot.PutPin(iPin, pinNumber, aalPin.Name)
                                        _PinsInMappingCount += 1
                                    Catch ex As Exception
                                        'oLogReport.Add("Error", pdbPart.Number, "Could not commit slot number " & iSlot & ", this is normally caused by pins not being defined on the symbol.")
                                        Return False
                                    End Try
                                    i_SymbolPinCount += 1
                                Next

                                Dim l_Slots As New List(Of MGCPCBPartsEditor.Slot)

                                l_Slots.Add(pdbSlot)

                                dic_Gates.Add(pdbGate, l_Slots)
                            Else
                                Log(LogType.Err, "[Invalid Input] " & aalSymbol.Partition & ":" & aalSymbol.Name & " symbol reference name was not found in symbol.")
                                Return False
                            End If

                        End If

                    End If

                Next

            End If

            If Not IsNothing(_FullPack) Then

                If l_MasterSymbolPins.Count > 0 Then
                    Log(LogType.Err, "[Build PDB Error] Not all pins were mapped. This is normally because of implicit and explicit NC pins.")
                    Return False
                End If

                If dic_SymRefs.ContainsKey(_FullPack) Then
                    Dim pdbFullPackSymbol As MGCPCBPartsEditor.SymbolReference = dic_SymRefs.Item(_FullPack)
                    Dim oSymbol As AAL.Symbol

                    aalPart.Symbols.TryGetValue(pdbFullPackSymbol.Name, oSymbol)

                    If Not IsNothing(xmlDebug) Then
                        AAL.XMLMapping.WriteGatesNode(xmlDebug, nodePart, oSymbol)
                    End If

                    If dic_Gates.Count = 1 Then

                        Dim l_pdbSlots As List(Of MGCPCBPartsEditor.Slot)

                        l_pdbSlots = dic_Gates.First.Value

                        Dim l_TempSlots As New List(Of AAL.Slot)(oSymbol.Slots)

                        For Each pdbDefinedSlot As MGCPCBPartsEditor.Slot In l_pdbSlots
                            Dim dic_SlotPins As New Dictionary(Of String, Integer)

                            For Each pdbPin As MGCPCBPartsEditor.IMGCPDBPin In pdbDefinedSlot.Pins
                                Try
                                    dic_SlotPins.Add(pdbPin.Number, pdbPin.Index)
                                Catch ex As Exception
                                    Log(LogType.Err, "[Invalid Input] Symbol " & pdbFullPackSymbol.Name & " has duplicate pin numbers.")
                                    Return False
                                End Try
                            Next

                            For Each aalSlot As AAL.Slot In l_TempSlots
                                Dim pdbSlot As MGCPCBPartsEditor.Slot
                                If oSymbol.Slots.Count = 1 Then
                                    Try
                                        numOfAttempts += 1
                                        pdbSlot = pdbPart.PinMapping.PutSlot(dic_Gates.First.Key, pdbFullPackSymbol)
                                    Catch ex As Exception
                                        If numOfAttempts < 25 Then
                                            l_TempSlots = Nothing
                                            dic_SlotPins = Nothing
                                            dic_Gates = Nothing
                                            GoTo BuildGateInit
                                        End If
                                        Log(LogType.Err, "[Build PDB Error] Failed to put slot into the symbol " & pdbFullPackSymbol.Name & " using the specified gate.")
                                        Return False
                                    End Try

                                    For Each aalPin As AAL.SymbolPin In aalSlot.SymbolPins.Values

                                        Dim pinNumber As String = aalPin.Number

                                        pinNumber = pinNumber.Replace("[", "")
                                        pinNumber = pinNumber.Replace("]", "")
                                        pinNumber = pinNumber.Replace("{", "")
                                        pinNumber = pinNumber.Replace("}", "")
                                        pinNumber = pinNumber.Replace("(", "")
                                        pinNumber = pinNumber.Replace(")", "")

                                        If dic_SlotPins.ContainsKey(pinNumber) Then
                                            Dim i_Index As Integer = dic_SlotPins.Item(pinNumber)
                                            Try
                                                pdbSlot.PutPin(i_Index, pinNumber, aalPin.Name)
                                            Catch ex As Exception
                                                Log(LogType.Err, "[Build PDB Error] Failed to put pin " & pinNumber & " from symbol pins.")
                                                Return False
                                            End Try

                                        End If
                                    Next
                                Else
                                    Dim l_SlotPinsTemp As New List(Of String)(dic_SlotPins.Keys)
                                    For Each aalPin As AAL.SymbolPin In aalSlot.SymbolPins.Values
                                        Dim pinNumber As String = aalPin.Number

                                        pinNumber = pinNumber.Replace("[", "")
                                        pinNumber = pinNumber.Replace("]", "")
                                        pinNumber = pinNumber.Replace("{", "")
                                        pinNumber = pinNumber.Replace("}", "")
                                        pinNumber = pinNumber.Replace("(", "")
                                        pinNumber = pinNumber.Replace(")", "")

                                        If l_SlotPinsTemp.Contains(pinNumber) Then l_SlotPinsTemp.Remove(pinNumber)
                                    Next

                                    If l_SlotPinsTemp.Count = 0 Then
                                        Dim slot As MGCPCBPartsEditor.Slot = pdbPart.PinMapping.PutSlot(dic_Gates.First.Key, pdbFullPackSymbol)
                                        For Each aalPin As AAL.SymbolPin In aalSlot.SymbolPins.Values
                                            Dim pinNumber As String = aalPin.Number

                                            pinNumber = pinNumber.Replace("[", "")
                                            pinNumber = pinNumber.Replace("]", "")
                                            pinNumber = pinNumber.Replace("{", "")
                                            pinNumber = pinNumber.Replace("}", "")
                                            pinNumber = pinNumber.Replace("(", "")
                                            pinNumber = pinNumber.Replace(")", "")

                                            If dic_SlotPins.ContainsKey(pinNumber) Then
                                                Dim i_Index As Integer = dic_SlotPins.Item(pinNumber)
                                                slot.PutPin(i_Index, pinNumber, aalPin.Name)
                                            End If

                                        Next
                                        l_TempSlots.Remove(aalSlot)
                                        Exit For
                                    End If
                                End If
                            Next
                        Next
                    Else

                        For Each kvp As KeyValuePair(Of MGCPCBPartsEditor.Gate, List(Of MGCPCBPartsEditor.Slot)) In dic_Gates
                            Dim dic_SlotPins As New Dictionary(Of String, Integer)

                            For Each pdbPin As MGCPCBPartsEditor.IMGCPDBPin In kvp.Value.Item(0).Pins
                                Try
                                    dic_SlotPins.Add(pdbPin.Number, pdbPin.Index)
                                Catch ex As Exception
                                    Log(LogType.Err, "[Invalid Input] Symbol " & _FullPack & " has duplicate pin numbers.")
                                    Return False
                                End Try
                            Next

                            Dim pdbSlot As MGCPCBPartsEditor.Slot = pdbPart.PinMapping.PutSlot(kvp.Key, pdbFullPackSymbol)

                            For Each aalPin As AAL.SymbolPin In oSymbol.Slots.Item(0).SymbolPins.Values
                                Dim i_Index As Integer = 0

                                Dim pinNumber As String = aalPin.Number

                                pinNumber = pinNumber.Replace("[", "")
                                pinNumber = pinNumber.Replace("]", "")
                                pinNumber = pinNumber.Replace("{", "")
                                pinNumber = pinNumber.Replace("}", "")
                                pinNumber = pinNumber.Replace("(", "")
                                pinNumber = pinNumber.Replace(")", "")

                                If dic_SlotPins.TryGetValue(pinNumber, i_Index) Then
                                    pdbSlot.PutPin(i_Index, pinNumber, aalPin.Name)
                                End If
                            Next

                        Next

                    End If

                End If

            End If
        Else

            Dim l_pdbSlots As New List(Of MGCPCBPartsEditor.Slot)

            Dim dic_pinList As Dictionary(Of String, Integer)
            For Each pdbSymbolReference As MGCPCBPartsEditor.SymbolReference In dic_SymRefs.Values

                If aalPart.Symbols.ContainsKey(pdbSymbolReference.Name) Then
                    Dim aalSymbol As AAL.Symbol = aalPart.Symbols(pdbSymbolReference.Name)
                    Dim pdbGate As MGCPCBPartsEditor.Gate = BuildGate(pdbPart, aalSymbol)

                    If IsNothing(pdbGate) Then
                        Return False
                    End If
                    Dim nodeGate As Xml.XmlNode

                    If Not IsNothing(xmlDebug) Then
                        nodeGate = AAL.XMLMapping.WriteGatesNode(xmlDebug, nodePart, aalSymbol)
                    End If

                    Dim iPin As Integer = 0

                    If aalSymbol.Slots.Count = 1 Then
                        Dim pdbSlot As MGCPCBPartsEditor.Slot
                        Try
                            pdbSlot = pdbPart.PinMapping.PutSlot(pdbGate, pdbSymbolReference)
                        Catch ex As Exception
                            Log(LogType.Err, "[Build PDB Error] Unable to create slot for symbol " & pdbSymbolReference.Name & ". Unhandled exception of: " & ex.Message)
                            Return False
                        End Try

                        Dim xmlSlot As New AAL.Slot
                        xmlSlot.Name = "Slot_1"

                        For Each aalPin As AAL.SymbolPin In aalSymbol.Slots.Item(0).SymbolPins.Values
                            iPin += 1
                            xmlSlot.SymbolPins(aalPin.Name) = aalPin
                            'Pin Number

                            Dim pinNumber As String = aalPin.Number

                            pinNumber = pinNumber.Replace("[", "")
                            pinNumber = pinNumber.Replace("]", "")
                            pinNumber = pinNumber.Replace("{", "")
                            pinNumber = pinNumber.Replace("}", "")
                            pinNumber = pinNumber.Replace("(", "")
                            pinNumber = pinNumber.Replace(")", "")

                            Try
                                If _CellPinNumbers.Contains(pinNumber) Then
                                    _CellPinNumbers.Remove(pinNumber)
                                Else
                                    If Not _MasterCellPins.Contains(pinNumber) And bValidCellsRequired = True Then
                                        Log(LogType.Err, "[Invalid Input] Cell does not contain pin number " & pinNumber & " found in symbol (" & aalSymbol.Partition & ":" & aalSymbol.Name & ")")
                                        Return False
                                    End If
                                End If
                                pdbSlot.PutPin(iPin, pinNumber, aalPin.Name)
                                _PinsInMappingCount += 1
                            Catch ex As Exception
                                'oLogReport.Add("Error", pdbPart.Number, "Could not commit slot number " & iSlot & ", this is normally caused by pins not being defined on the symbol.")
                                Return False
                            End Try
                            i_SymbolPinCount += 1
                        Next

                        If Not IsNothing(xmlDebug) Then
                            AAL.XMLMapping.WriteSlotNode(xmlDebug, nodeGate, xmlSlot)
                        End If

                        'If oSymbol.GroundSlots.Count > 0 Then

                        ' iPin = 0

                        ' Dim pdbGroundGate As MGCPCBPartsEditor.Gate =
                        ' pdbPart.PinMapping.PutGate("Ground" & aalGate.Name, 1, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeLogical)

                        ' pdbGroundGate.PutPinDefinition(1, "Ground",
                        ' MGCPCBPartsEditor.EPDBPinPropertyType.epdbPinPropertyPinType, "Ground")

                        ' Dim l_GroundSlot As New ArrayList()

                        ' For Each aalPin As AAL.SymbolPin In aalGate.GroundPins

                        ' l_GroundSlot.Add(pinMapping.PutSlot(pdbGroundGate, symRef))

                        ' iPinIndex += 1

                        ' 'Pin Number Try If alCellPinNumbers.Contains(aalPin.Number) Then
                        ' alCellPinNumbers.Remove(aalPin.Number) End If

                        ' l_GroundSlot(iSlot).PutPin(iPinIndex, aalPin.Number, aalPin.Name)
                        ' i_PinsInMappingCount += 1 Catch ex As Exception Log(LogType.Other, "Could
                        ' not commit slot number " & iSlot & ", this is normally caused by pins not
                        ' being defined on the symbol.") Return False

                        ' End Try

                        ' iSlot += 1

                        '    Next
                        'End If

                        'If aalGate.PowerPins.Count > 0 Then

                        ' Dim pdbGroundGate As MGCPCBPartsEditor.Gate =
                        ' pdbPart.PinMapping.PutGate("Power" & aalGate.Name, 1, MGCPCBPartsEditor.EPDBGateType.epdbGateTypeLogical)

                        ' pdbGroundGate.PutPinDefinition(1, "Power",
                        ' MGCPCBPartsEditor.EPDBPinPropertyType.epdbPinPropertyPinType, "Power")

                        ' Dim l_PowerSlot As New ArrayList()

                        ' iSlot = 0 iPinIndex = 0 For Each aalPin As AAL.SymbolPin In aalGate.PowerPins

                        ' l_PowerSlot.Add(pinMapping.PutSlot(pdbGroundGate, symRef))

                        ' iPinIndex += 1

                        ' 'Pin Number Try If alCellPinNumbers.Contains(aalPin.Number) Then
                        ' alCellPinNumbers.Remove(aalPin.Number) End If

                        ' l_PowerSlot(iSlot).PutPin(iPinIndex, aalPin.Number, aalPin.Name)
                        ' i_PinsInMappingCount += 1 Catch ex As Exception Log(LogType.Other, "Could
                        ' not commit slot number " & iSlot & ", this is normally caused by pins not
                        ' being defined on the symbol.") Return False

                        ' End Try

                        ' iSlot += 1

                        '    Next
                        'End If
                    Else
                        Dim bSuccessful As Boolean = AddMultiSlotted(dic_pinList, aalSymbol, nodeGate, pdbGate, pdbSymbolReference, pdbPart, i_SymbolPinCount, _MasterCellPins, l_pdbSlots, l_MasterSymbolPins)

                        If bSuccessful = False Then
                            Return False
                        End If

                    End If

                End If

            Next
        End If

        'If i_SymbolPinCount > i_CellPinCount Then
        '    Log(LogType.Other, "Total symbol pins (" & i_SymbolPinCount & " ) are greater than total cell pins (" & i_CellPinCount & ")")
        '    Return False
        'End If
        Return True
    End Function

    Private Function CreateCellReferences(ByRef pdbPart As MGCPCBPartsEditor.Part) As Boolean
        Dim cellCount As Integer = 0

        Dim sAllCell As String = Nothing
        Dim sTopCell As String = Nothing
        Dim sBottomCell As String = Nothing
        Dim l_AltCells As New List(Of String)

        For Each aalCell As AAL.Cell In aalPart.Cells.Values
            If aalPart.Cells.Count > 1 Then
                Dim bIgnore As Boolean = False

                If Not String.IsNullOrEmpty(aalPart.TopCell) Then

                    If String.Compare(aalPart.TopCell, aalCell.Name, True) = 0 Then 'If top cell matches this cell name

                        If IsNothing(sTopCell) Then
                            sTopCell = aalCell.Name
                            'If Not IsNothing(xmlDoc) Then
                            '    oWriteXMLMapping.WriteCellNode(xmlDoc, nodePart, oCell, "T")
                            'End If
                        Else
                            If Not l_AltCells.Contains(aalCell.Name, StringComparer.OrdinalIgnoreCase) Then
                                l_AltCells.Add(aalCell.Name)
                                Dim sNote As String = aalCell.Name & " is being added as an alternate cell because " & sTopCell & " is already defined as the top cell."
                                Log(LogType.Note, sNote)

                                'If Not IsNothing(xmlDoc) Then
                                '    oWriteXMLMapping.WriteCellNode(xmlDoc, nodePart, oCell, "A")
                                'End If

                            End If
                        End If
                        Continue For
                    End If

                End If

                If Not String.IsNullOrEmpty(aalPart.BotCell) And bIgnore = False Then

                    If String.Compare(aalPart.BotCell, aalCell.Name, True) = 0 Then

                        If IsNothing(sBottomCell) Then
                            sBottomCell = aalCell.Name
                            'If Not IsNothing(xmlDoc) Then
                            '    oWriteXMLMapping.WriteCellNode(xmlDoc, nodePart, oCell, "B")
                            'End If
                        Else
                            If Not l_AltCells.Contains(aalCell.Name, StringComparer.OrdinalIgnoreCase) Then
                                l_AltCells.Add(aalCell.Name)
                                Log(LogType.Note, aalCell.Name & " is being added as an alternate cell because " & sBottomCell & " is already defined as the bottom cell.")

                                'If Not IsNothing(xmlDoc) Then
                                '    oWriteXMLMapping.WriteCellNode(xmlDoc, nodePart, oCell, "A")
                                'End If

                            End If
                        End If

                        Continue For
                    End If

                End If

                If String.IsNullOrEmpty(sTopCell) Then
                    sTopCell = aalCell.Name
                    'If Not IsNothing(xmlDoc) Then
                    '    oWriteXMLMapping.WriteCellNode(xmlDoc, nodePart, oCell, "T")
                    'End If
                Else
                    If Not l_AltCells.Contains(aalCell.Name, StringComparer.OrdinalIgnoreCase) Then
                        l_AltCells.Add(aalCell.Name)
                        'If Not IsNothing(xmlDoc) Then
                        '    oWriteXMLMapping.WriteCellNode(xmlDoc, nodePart, oCell, "A")
                        'End If
                    End If
                End If

                cellCount += 1
            Else

                If Not String.IsNullOrEmpty(aalPart.BotCell) Then

                    If String.Compare(aalPart.BotCell, aalCell.Name, True) = 0 Then
                        sBottomCell = aalCell.Name
                        'If Not IsNothing(xmlDoc) Then
                        '    oWriteXMLMapping.WriteCellNode(xmlDoc, nodePart, oCell, "B")
                        'End If
                    End If
                Else
                    sTopCell = aalCell.Name
                    'If Not IsNothing(xmlDoc) Then
                    '    oWriteXMLMapping.WriteCellNode(xmlDoc, nodePart, oCell, "T")
                    'End If
                End If
            End If

        Next

        RaiseEvent eUpdateStatus(vbTab & "Adding Cell(s):")
        Dim pdbCellReference As MGCPCBPartsEditor.CellReference

        If Not IsNothing(sTopCell) Then
            Try
                pdbCellReference = pdbPart.PinMapping.PutCellReference(sTopCell, MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefTop)
                RaiseEvent eUpdateStatus(vbTab & vbTab & sTopCell)
                If _CellPinNumbers.Count > 0 Then

                    Dim alTempCellPinNumbers As New ArrayList
                    alTempCellPinNumbers.AddRange(pdbCellReference.PinNumbers)

                    If _CellPinNumbers.Count = alTempCellPinNumbers.Count Then
                        _CellPinNumbers.Clear()

                        Dim tempPins As New ArrayList()
                        tempPins.AddRange(pdbCellReference.PinNumbers)
                        _CellPinNumbers = tempPins.Cast(Of String).ToList()

                        If _CellPinNumbers.Count > aalPart.PinCount Then
                            aalPart.PinCount = _CellPinNumbers.Count
                        End If
                    ElseIf alTempCellPinNumbers.Count = 0 Then
                        Log(LogType.Warning, "[Invalid Input] Removing cell " & sTopCell & " because it has no pins.")
                        pdbPart.PinMapping.CellReferences().Item(sTopCell).Delete()
                    Else
                        Log(LogType.Err, "[Invalid Input] Multiple cells are referenced but their pin counts do not match. Top cell, " & sTopCell & ", contains " & _CellPinNumbers.Count() &
                            " pins. Alternate cell, " & pdbCellReference.Name & ", contains " & alTempCellPinNumbers.Count & " pins.")
                        pdbCellReference.Delete()
                    End If
                Else

                    Dim tempPins As New ArrayList()
                    tempPins.AddRange(pdbCellReference.PinNumbers)
                    _CellPinNumbers = tempPins.Cast(Of String).ToList()

                    If _CellPinNumbers.Count > aalPart.PinCount Then aalPart.PinCount = _CellPinNumbers.Count
                End If
            Catch ex As Exception
                Return False
            End Try
        End If

        If Not IsNothing(sBottomCell) Then
            Try
                pdbCellReference = pdbPart.PinMapping.PutCellReference(sBottomCell, MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefBottom)
                RaiseEvent eUpdateStatus(vbTab & vbTab & sBottomCell)

                If _CellPinNumbers.Count > 0 Then

                    Dim alTempCellPinNumbers As New ArrayList
                    alTempCellPinNumbers.AddRange(pdbCellReference.PinNumbers)

                    If _CellPinNumbers.Count = alTempCellPinNumbers.Count Then
                        _CellPinNumbers.Clear()

                        Dim tempPins As New ArrayList()
                        tempPins.AddRange(pdbCellReference.PinNumbers)
                        _CellPinNumbers = tempPins.Cast(Of String).ToList()

                        If _CellPinNumbers.Count > aalPart.PinCount Then aalPart.PinCount = _CellPinNumbers.Count
                    ElseIf alTempCellPinNumbers.Count = 0 Then
                        Log(LogType.Warning, "[Invalid Input] Removing cell " & sBottomCell & " because it has no pins.")
                        pdbCellReference.Delete()
                    Else
                        Log(LogType.Err, " [Invalid Input] Multiple cells are referenced but their pin counts do not match. Top cell, " & sTopCell & ", contains " & _CellPinNumbers.Count() &
                            " pins. Alternate cell, " & pdbCellReference.Name & ", contains " & alTempCellPinNumbers.Count & " pins.")
                        pdbCellReference.Delete()
                    End If
                Else
                    Dim tempPins As New ArrayList()
                    tempPins.AddRange(pdbCellReference.PinNumbers)
                    _CellPinNumbers = tempPins.Cast(Of String).ToList()

                    If _CellPinNumbers.Count > aalPart.PinCount Then aalPart.PinCount = _CellPinNumbers.Count
                End If
            Catch ex As Exception
                Return False
            End Try
        End If

        For Each sCell As String In l_AltCells
            Try
                pdbCellReference = pdbPart.PinMapping.PutCellReference(sCell, MGCPCBPartsEditor.EPDBCellReferenceType.epdbCellRefAlternate)
                RaiseEvent eUpdateStatus(vbTab & vbTab & sCell)

                If _CellPinNumbers.Count > 0 Then

                    Dim alTempCellPinNumbers As New ArrayList
                    alTempCellPinNumbers.AddRange(pdbCellReference.PinNumbers)

                    If _CellPinNumbers.Count = alTempCellPinNumbers.Count Then
                        _CellPinNumbers.Clear()

                        Dim tempPins As New ArrayList()
                        tempPins.AddRange(pdbCellReference.PinNumbers)
                        _CellPinNumbers = tempPins.Cast(Of String).ToList()

                        If _CellPinNumbers.Count > aalPart.PinCount Then aalPart.PinCount = _CellPinNumbers.Count
                    ElseIf alTempCellPinNumbers.Count = 0 Then
                        Log(LogType.Warning, "[Invalid Input] Removing cell " & sCell & " because it has no pins.")
                        pdbCellReference.Delete()
                    Else
                        Log(LogType.Err, "[Invalid Input] Multiple cells are referenced but their pin counts do not match. Top cell, " & sTopCell & ", contains " & _CellPinNumbers.Count() &
                            " pins. Alternate cell, " & pdbCellReference.Name & ", contains " & alTempCellPinNumbers.Count & " pins.")
                        pdbCellReference.Delete()
                        Continue For
                    End If
                Else
                    Dim tempPins As New ArrayList()
                    tempPins.AddRange(pdbCellReference.PinNumbers)
                    _CellPinNumbers = tempPins.Cast(Of String).ToList()

                    If _CellPinNumbers.Count > aalPart.PinCount Then aalPart.PinCount = _CellPinNumbers.Count
                End If
            Catch ex As Exception
                Return False
            End Try
        Next

        If (pdbPart.PinMapping.CellReferences.Count = 0) Then
            Return False
        End If

        If pdbPart.PinMapping.CellReferences.Count = 0 Then
            Return False
        Else
            Return True
        End If

    End Function

    Private Function createPinMapping(ByRef pdbPart As MGCPCBPartsEditor.Part, ByRef oPart As AAL.Part) As Boolean

        'Dim pdbMapping As MGCPCBPartsEditor.PinMapping
        'pdbMapping = pdbPart.PinMapping

        If oPart.Cells.Count > 0 Then
            Dim Success As Boolean
            Success = CreateCellReferences(pdbPart)

            If Success Then
                Try
                    pdbPart.PinMapping.Commit()
                Catch ex As Exception
                    Log(LogType.Err, "[Build PDB Error] Could not save cells to PDB.")
                    Return False
                End Try
            Else

                If (aalPart.MissingCells.Count > 0) Then
                    For Each cell As String In aalPart.MissingCells
                        Log(LogType.Warning, "[Invalid Input] Cell: " & cell & " was not found.")
                    Next
                End If
                Return False
            End If
        End If

        'If oPart.Symbols.Count > 0 Then
        '    Success = AddSymbolReferences(pdbPart)
        'Else
        If (readSymbols) Then
            b_Success = CreateSymbolReferences(pdbPart, oPart)
        Else
            b_Success = AddSymbolReferences(pdbPart)
        End If

        'End If

        If b_Success Then
            Try
                pdbPart.PinMapping.Commit()
            Catch ex As Exception
                Log(LogType.Err, "[Build PDB Error] Could not save symbols to PDB.")
                Return False
            End Try
        Else
            Return False
        End If

        'If Not IsNothing(dicGates) Then

        ' Success = UsePreBuiltGateAndSlots(pdbMapping, partSymRef, Part)

        'Else

        If pdbPart.SymbolReferences.Count = 0 Then Return False

        Dim dic_SymRefs As New Dictionary(Of String, MGCPCBPartsEditor.SymbolReference)

        For Each SymRef As MGCPCBPartsEditor.SymbolReference In pdbPart.SymbolReferences
            dic_SymRefs.Add(SymRef.Name, SymRef)
        Next

        If ((b_PossibleFracturedPart = True) Or b_PossibleSlottedPart = True) And (aalPart.MissingSymbols.Count > 0) Then
            Log(LogType.Err, "[Invalid Input] Part is a fractured or slotted device but some symbols were not found during the initial data read.")
            For Each symbol As String In aalPart.MissingSymbols
                Log(LogType.Err, symbol)
            Next
            For Each cell As String In aalPart.MissingCells
                Log(LogType.Err, cell)
            Next
            Return False
        Else
            For Each symbol As String In aalPart.MissingSymbols
                Log(LogType.Note, symbol)
            Next
            For Each cell As String In aalPart.MissingCells
                Log(LogType.Note, cell)
            Next
        End If

        b_Success = BuildGateAndSlots(pdbPart, dic_SymRefs, oPart)

        If b_Success = False Then
            Return False
        End If

        Try
            pdbPart.PinMapping.Commit()
        Catch ex As Exception
            Log(LogType.Err, "[Build PDB Error] Could not commit pin mapping. " & ex.Message)
            Return False
        End Try

        b_Success = addSupplyPins(pdbPart.PinMapping)

        If b_Success = False Then Return False

        Try
            pdbPart.PinMapping.Commit()
        Catch ex As Exception
            Log(LogType.Err, "[Build PDB Error] Could not commit pin mapping after adding Supply pins.")
            Return False
        End Try

        addNCPins(pdbPart.PinMapping)

        Try
            pdbPart.PinMapping.Commit()
        Catch ex As Exception
            Log(LogType.Err, "[Build PDB Error] Could not commit pin mapping after adding NC pins.")
            Return False
        End Try

        Return True

    End Function

    Private Function CreateSymbolReferences(ByRef pdbPart As MGCPCBPartsEditor.Part, ByRef aalPart As AAL.Part) As Boolean

        Dim iMasterSlotCount As Integer = 0

        Dim aalSymbols As New AAL.Symbols

        Dim iSkippedSymbolCount As Integer = 0

        For Each aalTempSymbol As AAL.Symbol In aalPart.Symbols.Values

            Dim dxdLogicalSymbol = Nothing

            Dim l_LocalPinNumbers As New List(Of String)

            If LibraryData.SymbolList.ContainsKey(aalTempSymbol.Name) Then
                Dim l_Partitions As List(Of String) = LibraryData.SymbolList.Item(aalTempSymbol.Name)

                If Not aalPart.Gates.Count = 0 Then
                    Dim pdbSymbolReference As MGCPCBPartsEditor.SymbolReference = pdbPart.PinMapping.PutSymbolReference(aalTempSymbol.Partition & ":" & aalTempSymbol.Name)
                    Continue For
                End If

                Dim bFoundSymbol As Boolean = False

                If IsNothing(aalTempSymbol.Partition) Then
                    aalTempSymbol.Partition = l_Partitions(0)
                    bFoundSymbol = True
                Else
                    For Each s_Partition In l_Partitions

                        If String.Compare(s_Partition, aalTempSymbol.Partition, True) = 0 Then
                            bFoundSymbol = True
                            Exit For
                        End If

                    Next
                End If

                If bFoundSymbol = True Then

                    Dim aalSymbol As New AAL.Symbol
                    If aalTempSymbol.LogicalPinCount = 0 Then
                        Dim aalGetData As New AAL.Read
                        Try
                            aalSymbol = aalGetData.ASCII2SymbolObject(aalTempSymbol.Name, aalTempSymbol.Partition, Path.GetDirectoryName(LibraryData.LibPath), aalTempSymbol)
                        Catch ex As Exception
                            Log(LogType.Err, "[Build PDB Error] Could not create symbol from ASCII.")
                            Return False
                        End Try

                        If IsNothing(aalSymbol) Then
                            Log(LogType.Err, "[Build PDB Error] Unable to successfully read symbol: " & aalTempSymbol.Partition & ":" & aalTempSymbol.Name)
                            Return False
                        End If
                    Else
                        aalSymbol = aalTempSymbol
                    End If

                    'For Each kvp As KeyValuePair(Of String, List(Of String)) In oSymbol.SupplyPins

                    'Next

                    If (aalSymbol.ImplicitPinCount + aalSymbol.LogicalPinCount) > _CellPinNumbers.Count And bValidCellsRequired = True Then
                        Log(LogType.Err, "[Invalid Input] Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " contains more pins (" & aalSymbol.ImplicitPinCount + aalSymbol.LogicalPinCount & ") than the total cell pin count (" & _CellPinNumbers.Count & ")")
                        iSkippedSymbolCount += 1
                        Continue For
                    End If

                    If b_findRefDes = True Then
                        If Not IsNothing(aalSymbol.RefDesPrefix) And Not aalSymbol.RefDesPrefix = "" Then
                            aalPart.RefDes = aalSymbol.RefDesPrefix
                        Else
                            aalPart.RefDes = "Tmp"
                            Log(LogType.Warning, "Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " has no reference designator defined, setting reference designator to Tmp.")

                        End If

                        If aalPart.RefDes.Contains("_") Then

                            aalPart.RefDes = aalPart.RefDes.Replace("_", "")

                        End If

                        If aalPart.RefDes.Length > 3 Then
                            aalPart.RefDes = SplitBySize(aalPart.RefDes, 3)
                        End If

                        Dim lLen As Long
                        Dim sChar As String
                        Dim sRefDes As String

                        'lLen = Len(oPart.RefDes)
                        If aalPart.RefDes.Length > 3 Then
                            For lCtr = 1 To 3
                                sChar = Mid(aalPart.RefDes, lCtr, 1)
                                If Mid(sChar, lCtr, 1) Like "[0-9A-Za-z]" Then
                                    sRefDes = sRefDes & sChar
                                End If
                            Next
                            aalPart.RefDes = sRefDes
                        End If

                        Try
                            pdbPart.RefDesPrefix = aalPart.RefDes
                        Catch ex As Exception
                            pdbPart.RefDesPrefix = "Tmp"
                            Log(LogType.Warning, "Cannot set ref des prefix to: " & aalPart.RefDes & ", setting it to Tmp instead.")

                        End Try

                        Dim sType As String

                        If LibraryData.PDBType.TryGetValue(pdbPart.RefDesPrefix, sType) Then
                            Select Case sType
                                Case "BJT"
                                    pdbPart.Type = 1
                                Case "Capacitor"
                                    pdbPart.Type = 2
                                Case "Connector"
                                    pdbPart.Type = 4
                                Case "Diode"
                                    pdbPart.Type = 8
                                Case "IC"
                                    pdbPart.Type = 16
                                Case "Inductor"
                                    pdbPart.Type = 32
                                Case "Jumper"
                                    pdbPart.Type = 128
                                Case "RCNetwork"
                                    pdbPart.Type = 2048
                                Case "Resistor"
                                    pdbPart.Type = 4096
                                Case "Switch"
                                    pdbPart.Type = 8192
                                Case "VoltageDivider"
                                    pdbPart.Type = 16384
                                Case Else
                                    pdbPart.Type = 512
                            End Select
                        Else
                            pdbPart.Type = 512
                        End If

                    End If

                    If aalSymbol.LogicalPinCount = 0 And b_GetPinNumbers = True Then
                        Log(LogType.Err, "[Invalid Input] Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " contains no pin numbers.")
                        Return False
                    End If

                    Dim dic_DupPinCheck As New Dictionary(Of String, String)

                    For Each kvp As KeyValuePair(Of String, List(Of String)) In _SymSupplyPins

                        For Each sPin As String In kvp.Value
                            dic_DupPinCheck.Add(sPin, kvp.Key)
                        Next

                    Next

                    If _partCount < aalSymbol.Slots.Count Then _partCount = aalSymbol.Slots.Count

                    For Each kvp_Signal As KeyValuePair(Of String, List(Of String)) In aalSymbol.SupplyPins

                        Dim l_Pins As List(Of String)
                        If _SymSupplyPins.TryGetValue(kvp_Signal.Key, l_Pins) Then
                            _SymSupplyPins.Remove(kvp_Signal.Key)
                        Else
                            l_Pins = New List(Of String)
                        End If

                        For Each sPin As String In kvp_Signal.Value
                            Dim sSupply As String
                            If Not String.IsNullOrWhiteSpace(sPin) Then
                                If dic_DupPinCheck.TryGetValue(sPin, sSupply) Then
                                    If Not sSupply = kvp_Signal.Key Then
                                        Log(LogType.Err, "[Invalid Input] Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " calls for supply pin number " & sPin & " to be defined as " & kvp_Signal.Key & " but pin " & sPin & " is already defined as " & sSupply)
                                        Return False
                                    End If
                                Else
                                    If _PinNumbers.Contains(sPin) Then
                                        Log(LogType.Err, "[Invalid Input] Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " contains an implicit pin (" & sPin & ") that was previously defined as a logical pin.")
                                        Return False
                                    End If
                                    If Not l_Pins.Contains(sPin) Then l_Pins.Add(sPin)
                                    If Not _ImplicitPinNumbers.Contains(sPin) Then _ImplicitPinNumbers.Add(sPin)
                                    dic_DupPinCheck.Add(sPin, kvp_Signal.Key)
                                End If
                            End If
                        Next

                        _SymSupplyPins.Add(kvp_Signal.Key, l_Pins)
                    Next

                    For Each aalSlot As AAL.Slot In aalSymbol.Slots
                        For Each aalPin As AAL.SymbolPin In aalSlot.SymbolPins.Values

                            Dim pinNumber As String = aalPin.Number

                            If String.IsNullOrWhiteSpace(pinNumber) Then
                                Log(LogType.Err, "[Invalid Input] Symbol pin name: " & aalPin.Name & " of symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " is missing a pin number.")
                                Return False
                            End If

                            pinNumber = pinNumber.Replace("[", "")
                            pinNumber = pinNumber.Replace("]", "")
                            pinNumber = pinNumber.Replace("{", "")
                            pinNumber = pinNumber.Replace("}", "")
                            pinNumber = pinNumber.Replace("(", "")
                            pinNumber = pinNumber.Replace(")", "")

                            If Not _PinNames.Contains(aalPin.Name, StringComparer.OrdinalIgnoreCase) Then
                                _PinNames.Add(aalPin.Name)
                            End If

                            If _ImplicitPinNumbers.Contains(pinNumber) Then
                                Log(LogType.Err, "[Invalid Input] Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " contains a pin (" & pinNumber & ") that was previously defined as an implicit pin.")

                                Return False
                            ElseIf Not _PinNumbers.Contains(pinNumber, StringComparer.OrdinalIgnoreCase) Then
                                _PinNumbers.Add(pinNumber)
                            ElseIf l_LocalPinNumbers.Contains(pinNumber) And aalSymbol.Slots.Count = 1 Then
                                Log(LogType.Err, "[Invalid Input] Symbol " & aalSymbol.Partition & ":" & aalSymbol.Name & " contains pins with duplicate pin number: " & pinNumber)

                                Return False
                            Else
                                b_PossibleFracturedPart = False
                                b_PossibleSlottedPart = True
                                If aalSymbol.Slots.Count > 1 Then
                                    If (aalPart.Symbols.Count - iSkippedSymbolCount) > 1 Then
                                        Dim sNote As String = "Treating part as a slotted part because pin " & pinNumber & " is found on multiple symbols."
                                        Log(LogType.Note, sNote)
                                    End If
                                ElseIf (aalSymbol.Slots.Count = 1 And Not (aalSymbol.LogicalPinCount + aalSymbol.ImplicitPinCount = _CellPinNumbers.Count) And bValidCellsRequired = True) Then
                                    b_PossibleSlottedPart = False
                                End If
                            End If
                            l_LocalPinNumbers.Add(pinNumber)

                        Next
                    Next

                    If aalSymbol.Slots.Count = 1 And (aalSymbol.LogicalPinCount + aalSymbol.ImplicitPinCount = _CellPinNumbers.Count) And bValidCellsRequired = True Then
                        _FullPack = aalSymbol.Partition & ":" & aalSymbol.Name
                    End If

                    Dim oSymbolReference As MGCPCBPartsEditor.SymbolReference = pdbPart.PinMapping.PutSymbolReference(aalSymbol.Partition & ":" & aalSymbol.Name)

                    If Not IsNothing(_FullPack) Then
                        If _FullPack = aalSymbol.Partition & ":" & aalSymbol.Name Then
                            oSymbolReference.SetAsDefault()
                        End If
                    End If

                    aalSymbols.Add(aalSymbol)
                Else
                    Log(LogType.Err, "[Invalid Input] Symbol " & aalTempSymbol.Name & " was not found in central library.")
                    Return False
                End If
            Else

                Dim pdbSymbolReference As MGCPCBPartsEditor.SymbolReference
                pdbSymbolReference = pdbPart.PinMapping.PutSymbolReference(aalTempSymbol.Name)
                Log(LogType.Err, "[Invalid Input] Symbol " & aalTempSymbol.Name & " was not found in central library.")
                Return False
            End If

        Next

        aalPart.Symbols.Clear()
        aalPart.Symbols = aalSymbols

        Return True
    End Function

#End Region

End Class