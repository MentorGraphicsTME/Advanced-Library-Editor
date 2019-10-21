Imports System.IO
Imports System.Text
Imports System.Runtime.InteropServices
Imports Microsoft.Win32

Public Class ProjectRead

#Region "Public Fields"

    Public dxdApp As ViewDraw.Application
    Public expApp As MGCPCB.Application
    Public expDoc, expOpenDoc As MGCPCB.Document

#End Region

#Region "Private Fields"

    Dim dxdDoc

#End Region

#Region "Public Events"

    Public Event eFinished()

    Public Event reUpdateStatus(status As String)

#End Region

#Region "Public Properties"

    Property buildPinMapping As Boolean = False
    Property closeDxD As Boolean = False
    Property closeExp As Boolean = False
    Property dicDxDListOfPart As New Dictionary(Of String, List(Of AAL.Part))

    'Part Number' -> List of Parts
    Property dicDxDPart As New Dictionary(Of String, AAL.Part)

    'Part Number' -> Part
    Property dicDxdPartData As New Dictionary(Of String, AAL.PartPartition)

    Property dicDxDRefDes As New Dictionary(Of String, List(Of String))
    Property dicExpPartData As New Dictionary(Of String, List(Of AAL.Cell))
    Property dicExpRefDesCellData As New Dictionary(Of String, Object)
    Property sDxDPath As String

    'Part Number -> List of Reference Designators
    Property sExpPath As String

    Property sLogPath As String
    Property success As Boolean = True

#End Region

#Region "Public Methods"

    Sub closeDxDApp()

        dxdApp.CloseProject()
        dxdApp.Quit()

    End Sub

    Sub closeExpApp()

        Try
            expDoc.Close()
        Catch ex As Exception
        Finally
            expDoc = Nothing
            expApp.Quit()
            expApp = Nothing
        End Try

    End Sub

    Function GetPN(ByVal dxdComponent As ViewDraw.Component) As String

        Dim oPN As ViewDraw.IVdAttr = Nothing
        Dim SubsPN As String = "Temp"

        oPN = dxdComponent.FindAttribute("Part Number")

        If IsNothing(oPN) Then
            oPN = dxdComponent.SymbolBlock.FindAttribute("Part Number")
        End If

        If Not IsNothing(oPN) Then
            If Not String.IsNullOrEmpty(oPN.InstanceValue) Then
                SubsPN = oPN.InstanceValue
            ElseIf Not String.IsNullOrEmpty(oPN.Value) Then
                SubsPN = oPN.Value
            Else

            End If
        End If
        Return SubsPN
    End Function

    Function GetProperty(ByRef dxdComp As ViewDraw.Component, ByVal PropertyName As String, Optional ByVal AltPropertyName As String = Nothing) As ViewDraw.IVdAttr

        Dim refdes As String = dxdComp.Refdes

        Dim compAtt As ViewDraw.IVdAttr = dxdComp.FindAttribute(PropertyName)       ' instance (InstanceValue) and/or block (Value)

        If IsNothing(compAtt) Then
            compAtt = dxdComp.SymbolBlock.FindAttribute(PropertyName) ' built into symbol comes here - will have Value, no InstanceValue
            If IsNothing(compAtt) And Not String.IsNullOrEmpty(AltPropertyName) Then
                compAtt = dxdComp.FindAttribute(AltPropertyName)
                If IsNothing(compAtt) Then
                    compAtt = dxdComp.SymbolBlock.FindAttribute(AltPropertyName)
                End If
            End If
        End If

        Return compAtt
    End Function

    Sub openDxD(prgID As String)

        Dim regKey = Registry.CurrentUser.OpenSubKey("Environment", True)
        Dim WDIRPath = regKey.GetValue("WDIR")
        Dim sWDIRsplit As String() = Split(WDIRPath, ";")

        Try
            dxdApp = GetObject(, "ViewDraw.Application")
            dxdApp.SilentMode = "VDSM_ALL" 'ViewDraw.VdSilentMode.VDSM_ALL

            If Not dxdApp.GetProjectData.GetProjectFilePath = sDxDPath() Then

                MsgBox("Active DxDesigner project does not match the DxDesigner project selected.")
                success = False

            End If
        Catch ex As Exception

            For Each sPath In sWDIRsplit

                If File.Exists(sPath & "\scripts.ini") Then

                    FileSystem.Rename(sPath & "\scripts.ini", sPath & "\scripts.ini_ALE")

                End If

            Next

            dxdApp = CreateObject("Viewdraw.Application" & "." & prgID)
            dxdApp.Visible = False
            dxdApp.OpenProject(sDxDPath)
            'dxdApp.Visible = True
        End Try

        If success = True Then
            TraverseDxD()
        End If

        For Each sPath In sWDIRsplit

            If File.Exists(sPath & "\scripts.ini_ALE") Then

                FileSystem.Rename(sPath & "\scripts.ini_ALE", sPath & "\scripts.ini")

            End If

        Next

        RaiseEvent eFinished()
        'MsgBox(dxdDoc.ToString)

    End Sub

    Sub openExp(prgID As String)

        Try
            expApp = GetObject(, "MGCPCB.ExpeditionPCBApplication")

            If Not expApp.ActiveDocument.FullName = sExpPath Then

                MsgBox("Active Expedition project does not match the Expedition project selected.")
                success = False

            End If
        Catch ex As Exception
            expApp = CreateObject("MGCPCB.ExpeditionPCBApplication" & "." & prgID)
            expApp.Visible = False
        End Try

        If success = True Then
            Dim MGCLicense As New AAL.License
            expDoc = MGCLicense.GetExp(expApp, sExpPath)

            If IsNothing(expDoc) Then
                RaiseEvent reUpdateStatus("Unable open Expedition: " & MGCLicense.ValidationError)
                MGCLicense = Nothing
                Exit Sub
            End If

            MGCLicense = Nothing

            readExpRefDesCellname()
        End If

        If closeExp = True Then

            closeExpApp()

        End If

        RaiseEvent eFinished()

    End Sub

    Sub readExpRefDesCellname()
        dicExpRefDesCellData.Clear()

        For Each expComp As MGCPCB.Component In expDoc.Components
            If Not dicExpRefDesCellData.ContainsKey(expComp.RefDes) Then
                Dim expPart As Object = New With {.PartNumber = expComp.PartNumber, .CellName = expComp.CellName}
                dicExpRefDesCellData.Item(expComp.RefDes) = expPart
            End If

        Next
    End Sub

    Sub TraverseDxD()
        dxdApp.SilentMode = ViewDraw.VdSilentMode.VDSM_ALL

        Dim dxdComps
        Dim sActiveDesign = dxdApp.GetActiveDesign()
        Dim stopblock = dxdApp.GetProjectData.GetiCDBDesignRootBlock(sActiveDesign)

        dxdApp.SchematicSheetDocuments.Close()
        dxdComps = dxdApp.DesignComponents("", stopblock, "-1", "std", True)

        For Each dxdComponent As ViewDraw.Component In dxdComps
            If Not String.IsNullOrEmpty(dxdComponent.Refdes) Then
                Dim compPath As ViewDraw.IVdBlock
                compPath = dxdComponent.SymbolBlock
                Dim sPartition As String = compPath.LibraryName
                Dim sSymbolName As String = compPath.GetName(ViewDraw.VdNameType.FULL_PATH_FROM_BLOCK) 'ViewDraw.VdNameType.SHORT_NAME)
                Dim sPN = GetPN(dxdComponent)
                Dim oCell As ViewDraw.IVdAttr = GetProperty(dxdComponent, "Cell Name", "PKG_TYPE")
                Dim oAltCell As ViewDraw.IVdAttr = GetProperty(dxdComponent, "Alt Cell Name", "ALT_PKG_LST")

                Dim dxdPart As AAL.Part = Nothing

                'If Not lsDxDRefDes.Contains(dxdComponent.Refdes) Then
                'lsDxDRefDes.Add(dxdComponent.Refdes)
                'End If

                If dicDxDPart.ContainsKey(dxdComponent.Refdes) Then
                    dxdPart = dicDxDPart.Item(dxdComponent.Refdes)
                Else
                    dxdPart = New AAL.Part()
                    dxdPart.Number = sPN
                    dxdPart.Partition = sActiveDesign
                End If

                If buildPinMapping = True Then

                    If String.IsNullOrEmpty(dxdPart.RefDes) Then
                        For Each c As Char In dxdComponent.Refdes.ToCharArray
                            If Not IsNumeric(c) Then
                                dxdPart.RefDes = dxdPart.RefDes & c
                            End If
                        Next
                    End If

                    Dim aalSymbol As AAL.Symbol

                    If dxdPart.Symbols.Keys.Contains(sPartition + ":" + sSymbolName) Then
                        aalSymbol = dxdPart.Symbols(sPartition + ":" + sSymbolName)
                    Else
                        aalSymbol = New AAL.Symbol
                        aalSymbol.Name = sSymbolName
                        aalSymbol.Partition = sPartition

                    End If
                    Dim pins As ViewDraw.IVdObjs = dxdComponent.GetConnections()

                    Dim previousSlot As New Dictionary(Of String, String)
                    Dim altSlot As Boolean = False

                    For Each slot As AAL.Slot In aalSymbol.Slots
                        If slot.SymbolName = aalSymbol.Partition + ":" + aalSymbol.Name Then

                            For Each pin As AAL.SymbolPin In slot.SymbolPins.Values
                                previousSlot(pin.Name) = pin.Number
                            Next
                            Exit For
                        End If
                    Next

                    Dim aalSlot As AAL.Slot = New AAL.Slot
                    aalSlot.SymbolName = aalSymbol.Partition + ":" + aalSymbol.Name

                    Dim LogicalPinCount As Integer = 0

                    For i As Integer = 1 To pins.Count
                        Dim dxdPin As ViewDraw.Connection = pins.Item(i)
                        Dim aalPin As AAL.SymbolPin = New AAL.SymbolPin()
                        aalPin.Number = dxdPin.CompPin.Number
                        aalPin.Name = dxdPin.CompPin.Pin.GetName(ViewDraw.VdNameType.SHORT_NAME)
                        If altSlot = False And previousSlot.ContainsKey(aalPin.Name) Then
                            Dim pinNum As String = previousSlot.Item(aalPin.Name)
                            If Not aalPin.Number = pinNum Then
                                altSlot = True
                            End If
                        End If

                        If aalSlot.SymbolPins.ContainsKey(aalPin.Name) Then
                            Dim aalPinTemp As AAL.SymbolPin = aalSlot.SymbolPins(aalPin.Name)
                            If Not aalPinTemp.Number = "" And aalPin.Number = "" Then
                                Continue For
                            End If
                        End If

                        aalSlot.SymbolPins(aalPin.Name) = aalPin
                        dxdPart.PinCount += 1
                        LogicalPinCount += 1
                    Next

                    If (Not previousSlot.Count = 0 And altSlot = True) Or (previousSlot.Count = 0) Then
                        aalSymbol.addSlot(aalSlot)
                        aalSymbol.LogicalPinCount = LogicalPinCount
                    End If

                    'Dim aalGate As AAL.Gate

                    'If dxdPart.Gates.ContainsKey(aalSlot.SymbolName) Then
                    '    aalGate = dxdPart.Gates.Item(aalSlot.SymbolName)
                    'Else
                    '    aalGate = New AAL.Gate()
                    '    aalGate.Name = aalSlot.SymbolName
                    'End If

                    'aalGate.Symbol = aalSymbol

                    'aalSlot.Name = "Slot_" & aalGate.Slots.Count()

                    'aalGate.Slots.Add(aalSlot, True)
                    'dxdPart.Gates.Add(aalGate, True)

                    dxdPart.Symbols.Add(aalSymbol)

                End If

                Dim lRefDes As List(Of String)

                If dicDxDRefDes.ContainsKey(dxdPart.Number) Then
                    lRefDes = dicDxDRefDes.Item(dxdPart.Number)
                Else
                    lRefDes = New List(Of String)
                End If

                If Not lRefDes.Contains(dxdComponent.Refdes) Then
                    lRefDes.Add(dxdComponent.Refdes)
                End If

                dicDxDRefDes.Item(dxdPart.Number) = lRefDes

                dicDxDPart.Item(dxdComponent.Refdes) = dxdPart

            End If
        Next

        For Each part As AAL.Part In dicDxDPart.Values
            If dicDxdPartData.ContainsKey(part.Partition) Then
                Dim partition As AAL.PartPartition = dicDxdPartData.Item(part.Partition)
                If partition.ContainsKey(part.Number) Then
                    Dim masterPart As AAL.Part = partition.Item(part.Number)
                    For Each sym As KeyValuePair(Of String, AAL.Symbol) In part.Symbols
                        If Not masterPart.Symbols.ContainsKey(sym.Key) Then
                            masterPart.Symbols.Add(sym.Key, sym.Value)
                        Else
                            Dim masterSym As AAL.Symbol = masterPart.Symbols.Item(sym.Key)
                            If sym.Value.Slots.Count > masterSym.Slots.Count Then
                                masterPart.Symbols.Item(sym.Key) = sym.Value
                            End If
                        End If
                    Next
                    partition.Add(masterPart, True)
                Else
                    partition.Add(part, True)
                End If
            Else
                Dim partition As New AAL.PartPartition
                partition.Name = part.Partition
                partition.Add(part)
                dicDxdPartData.Item(partition.Name) = partition
            End If
        Next

        If closeDxD = True Then

            closeDxDApp()

        End If

    End Sub

#End Region

End Class