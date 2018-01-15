Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Text
Imports System.Threading

Public Class Symbols

#Region "Public Fields + Properties + Events + Delegates + Enums"

    'Arraylist
    Property alSymExt As New ArrayList()

    Property AutomaticGraphics As Boolean = False

    Property AutomaticText As Boolean = False

    Property bRemoveTrailingSpaces As Boolean = False

    'Boolean
    Property bUpdateTimeStamp As Boolean = False

    Property dic_ModifiedPinProperties As New Dictionary(Of String, AAL.SymbolPinProperty)(StringComparer.OrdinalIgnoreCase)
    Property dic_ModifiedProperties As New Dictionary(Of String, AAL.SymbolProperty)(StringComparer.OrdinalIgnoreCase)
    Property dic_RenamedPinProperties As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Property dic_RenamedProperties As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    'Dictionary
    Property dicPDBProperties As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    Property dicSymbolNames As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
    Property dicSymbolRename As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    Public Event eProcessComplete(ByVal Partition As String, ByVal Report As StringBuilder)

    Public Event eReadComplete(ByVal Partition As String, ByVal UniqueSymAtts As Dictionary(Of String, AAL.SymbolProperty), ByVal Symbols As Dictionary(Of String, Object), ByVal SymPropswithTrailingSpaces As Dictionary(Of String, List(Of String)), ByVal UniquePinAtts As Dictionary(Of String, AAL.SymbolPinProperty), ByVal dic_NonCommonProperties As Dictionary(Of String, List(Of String)))

    Public Event eReadPropertyValuesComplete(ByVal Partition As String, ByVal Symbols As Dictionary(Of String, ArrayList), ByVal PartitionProperties As ArrayList)

    Public Event eUpdateCount(ByVal status As String)

    Property l_NewPinProperties As New List(Of AAL.SymbolPinProperty)
    Property l_NewProperties As New List(Of AAL.SymbolProperty)
    Property l_RemovePinProperties As New List(Of String)
    Property l_RemoveProperties As New List(Of String)

    'List
    Property l_SymNames As New List(Of String)

    Property libDoc As LibraryManager.IMGCLMLibrary
    Property LibraryData As Data

    'Object
    Property oDate As Date

    'Events
    Public Event RenameSymbolsComplete(ByVal Partition As String, ByVal DuplicateSymbols As List(Of String))

    Property ResetSymbolPins As Boolean = False

    Public Event SymbolCount(ByVal iSymbolCount As Integer)

    Public Event SymbolFinish()

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    Dim PrecisionUnit As Boolean = False
    'Rename Symbol
    'Rename Symbol
    'From Purge Symbols

    'Strings
    Dim sNewString, sline, attFont, attFontSize, attFontColor As String

#End Region

#Region "Public Methods"

    Function changeCase(ByVal sSymDirectory)
        For Each fileOnDisk As String In My.Computer.FileSystem.GetFiles(sSymDirectory, FileIO.SearchOption.SearchAllSubDirectories)
            Dim ext As String = Path.GetExtension(fileOnDisk)
            Dim r As Regex = New Regex("\.\d+$") ' Looks to see if the extension is a number
            Dim m As Match = r.Match(ext)
            If (m.Success) Then

                Dim sOldPath As String = Path.GetFullPath(fileOnDisk)

                Dim sPath As String = sOldPath.Substring(0, sOldPath.LastIndexOf("\") + 1)

                Dim sNewPath As String = sPath & LCase(Path.GetFileNameWithoutExtension(fileOnDisk)) & ext

                File.Replace(sOldPath, sNewPath, sNewPath)

            End If

        Next
    End Function

    Function ExportSymName(ByVal sSymDirectory)

        For Each fileOnDisk As String In My.Computer.FileSystem.GetFiles(sSymDirectory, FileIO.SearchOption.SearchAllSubDirectories)
            Dim ext As String = Path.GetExtension(fileOnDisk)
            Dim r As Regex = New Regex("\.\d+$") ' Looks to see if the extension is a number
            Dim m As Match = r.Match(ext)
            If (m.Success) Then

                If Not l_SymNames.Contains(Path.GetFileNameWithoutExtension(fileOnDisk)) Then
                    l_SymNames.Add(Path.GetFileNameWithoutExtension(fileOnDisk))

                End If

            End If

        Next

        l_SymNames.Sort()
        Dim sDir As String = sSymDirectory.Substring(sSymDirectory.LastIndexOf("\") + 1)
        dicSymbolNames.Add(sDir, l_SymNames)

    End Function

    Sub ProcessSymbols(ByVal sPartition As String)
        Dim i As Integer = 0

        Dim sb_FullChangeReport As New StringBuilder

        sb_FullChangeReport.AppendLine(sPartition)

        For Each fileOnDisk As String In My.Computer.FileSystem.GetFiles(libDoc.Path & "\Symbollibs\" & sPartition & "\sym\", FileIO.SearchOption.SearchTopLevelOnly)
            Dim ext As String = Path.GetExtension(fileOnDisk)
            Dim r As Regex = New Regex("\.\d+$") ' Looks to see if the extension is a number
            Dim m As Match = r.Match(ext)
            If (m.Success) Then

                Dim b_PrecisionUnit As Boolean = False
                Dim bChangesMade As Boolean = False
                Dim bPrintModifiedHeader As Boolean = True
                Dim sb_ChangeReport As New StringBuilder
                sb_ChangeReport.AppendLine(vbTab & Path.GetFileName(fileOnDisk))
                Dim sb_RemovedProperties As New StringBuilder

                Dim arFile As String() = File.ReadAllLines(fileOnDisk)

                Dim iMaxX As Double = 0
                Dim iMaxY As Double = 0
                Dim iMinX As Double = 10000000
                Dim iMinY As Double = 10000000

                If ResetSymbolPins = True Then

                    For Each line In arFile

                        If line.StartsWith("P ") Then

                            Dim slineSplit As String() = Split(line, " ")

                            If slineSplit(2) > iMaxX Then

                                iMaxX = slineSplit(2)

                            End If

                            If slineSplit(3) > iMaxX Then

                                iMaxY = slineSplit(3)

                            End If

                            If slineSplit(2) < iMinX Then

                                iMinX = slineSplit(2)

                            End If

                            If slineSplit(3) < iMinY Then

                                iMinY = slineSplit(3)

                            End If

                        End If

                    Next

                End If

                Dim currentlineCount As Integer = 0
                Dim currentLine As String

                Dim sb_Symbol As New StringBuilder

                Do While currentlineCount < arFile.Length

                    currentLine = arFile.GetValue(currentlineCount)

                    If String.IsNullOrWhiteSpace(currentLine) Then
                        Continue For
                    ElseIf currentLine.StartsWith("D ") Then
                        Dim s_lineSplit As String() = Split(currentLine.Replace("a ", String.Empty), " ")

                        For Each s_Value In s_lineSplit
                            If IsNumeric(s_Value) Then
                                Dim iNumber As Double = s_Value
                                If Not iNumber = 0 And 1 < (Math.Abs(iNumber) / 25400) Then
                                    b_PrecisionUnit = True
                                    Exit For
                                End If
                            End If
                        Next
                        sb_Symbol.AppendLine(currentLine)
                        'ElseIf currentLine.StartsWith("E ") Then
                        '    sb_Symbol.Append(currentLine)
                        '    currentlineCount = currentlineCount + 1
                    ElseIf currentLine.StartsWith("|R") And bUpdateTimeStamp = True Then

                        '|R 12:53:9_12-1-10
                        Dim sTimeline As String() = Split(currentLine, " ")
                        sb_ChangeReport.AppendLine(vbTab & vbTab & "Timestamp Change:")
                        sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Before:")
                        sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & sTimeline(1))

                        sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "After:")
                        sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & oDate.ToString("hh:mm:ss_MM-dd-yy"))
                        sb_Symbol.AppendLine("|R " & oDate.ToString("hh:mm:ss_MM-dd-yy"))
                        bChangesMade = True

                    ElseIf currentLine.StartsWith("i ") And l_NewProperties.Count > 0 Then
                        sb_Symbol.AppendLine(currentLine)
                        If l_NewProperties.Count > 0 Then
                            sb_ChangeReport.AppendLine(vbTab & vbTab & "New Properties:")
                            For Each oNewProperty As AAL.SymbolProperty In l_NewProperties
                                sb_ChangeReport.AppendLine(vbTab & vbTab & oNewProperty.ToString())
                                sb_Symbol.AppendLine(oNewProperty.Encrypt(b_PrecisionUnit).ToString())
                            Next
                            sb_ChangeReport.AppendLine(vbTab & vbTab & "End New Properties:")
                            bChangesMade = True
                        End If

                    ElseIf currentLine.StartsWith("U ") Then

                        Dim oOldProperty As New AAL.SymbolProperty

                        If oOldProperty.Decrypt(currentLine, b_PrecisionUnit) = False Then
                            Continue For
                        End If

                        currentLine = arFile.GetValue(currentlineCount + 1)

                        If currentLine.StartsWith("Q ") Then
                            currentlineCount += 1
                            currentLine = arFile.GetValue(currentlineCount + 1)
                            If currentLine.StartsWith("|FNTSTL ") Then
                                currentlineCount += 1
                                If oOldProperty.Decrypt(currentLine, b_PrecisionUnit) = False Then
                                    Continue For
                                End If
                            End If
                        ElseIf currentLine.StartsWith("|FNTSTL ") Then
                            currentlineCount += 1
                            If oOldProperty.Decrypt(currentLine, b_PrecisionUnit) = False Then
                                Continue For
                            End If
                        End If

                        If LibraryData.PropertyMapping.ContainsKey(oOldProperty.Name) Then
                            oOldProperty.Name = LibraryData.PropertyMapping.Item(oOldProperty.Name)
                        End If

                        If l_RemoveProperties.Contains(oOldProperty.Name) Then
                            sb_RemovedProperties.AppendLine(vbTab & vbTab & vbTab & oOldProperty.Name)
                            bChangesMade = True
                            currentlineCount += 1
                            Continue Do
                        End If

                        If dic_ModifiedProperties.ContainsKey(oOldProperty.Name) Then
                            Dim oModifiedProperty As AAL.SymbolProperty = dic_ModifiedProperties.Item(oOldProperty.Name)

                            Dim oTempProperty As AAL.SymbolProperty = CompareProperties(sb_ChangeReport, oModifiedProperty, oOldProperty)

                            If Not IsNothing(oTempProperty) Then
                                oOldProperty = oTempProperty
                                bChangesMade = True
                            End If

                        End If

                        Dim sPreviousName As String = LibraryData.MappingKeybyValue(oOldProperty.Name)
                        If Not String.IsNullOrEmpty(sPreviousName) Then
                            oOldProperty.Name = sPreviousName
                        End If

                        If AutomaticText = True Then

                            oOldProperty.AutomaticColor = True

                        End If

                        sb_Symbol.AppendLine(oOldProperty.Encrypt(b_PrecisionUnit).ToString())

                        currentlineCount += oOldProperty.Variants.Count

                    ElseIf currentLine.StartsWith("P ") Then

                        Dim PinPoint As New AAL.Point
                        Dim pinSplit As String() = Split(currentLine, " ")
                        PinPoint.X = pinSplit(2)
                        PinPoint.Y = pinSplit(3)

                        If b_PrecisionUnit = True Then
                            PinPoint = PinPoint \ 25400
                        End If

                        Dim iRotation As Integer = 0
                        Dim iFontSize As Integer = 0

                        sb_Symbol.AppendLine(currentLine)

                        currentlineCount += 1
                        currentLine = arFile.GetValue(currentlineCount)

                        Do Until currentLine.StartsWith("P ") Or ((currentlineCount + 1) = arFile.Length) Or currentLine.StartsWith("U ") Or currentLine.StartsWith("c ") Or currentLine.StartsWith("a ") Or currentLine.StartsWith("l ") Or currentLine.StartsWith("b ")

                            If currentLine.StartsWith("A ") Then

                                Dim oOldPinProperty As New AAL.SymbolPinProperty

                                oOldPinProperty.Decrypt(currentLine, b_PrecisionUnit)

                                If Not oOldPinProperty.Name.ToLower = "pintype" Or oOldPinProperty.Name = "#" Then
                                    currentLine = arFile.GetValue(currentlineCount + 1)

                                    If currentLine.StartsWith("Q ") Then
                                        currentlineCount += 1
                                        currentLine = arFile.GetValue(currentlineCount + 1)
                                        If currentLine.StartsWith("|FNTSTL ") Then
                                            currentlineCount += 1
                                            oOldPinProperty.Decrypt(currentLine, b_PrecisionUnit)
                                        End If
                                    ElseIf currentLine.StartsWith("|FNTSTL ") Then
                                        currentlineCount += 1
                                        oOldPinProperty.Decrypt(currentLine, b_PrecisionUnit)
                                    End If

                                    If oOldPinProperty.Name = "#" And ResetSymbolPins = True Then

                                        oOldPinProperty.Location.X = PinPoint.X
                                        oOldPinProperty.Location.Y = PinPoint.Y
                                        oOldPinProperty.Direction = iRotation
                                        oOldPinProperty.FontSize = iFontSize

                                        If PinPoint.X = iMaxX Or oOldPinProperty.Location.Y = iMinY Then

                                            oOldPinProperty.Alignment = AAL.Justification.LowerRight

                                        ElseIf oOldPinProperty.Location.X = iMinX Or oOldPinProperty.Location.Y = iMinY Then

                                            oOldPinProperty.Alignment = AAL.Justification.LowerLeft

                                        End If

                                    End If

                                    If l_RemovePinProperties.Contains(oOldPinProperty.Name) Then
                                        sb_RemovedProperties.AppendLine(vbTab & vbTab & vbTab & oOldPinProperty.Name)
                                        bChangesMade = True
                                        Continue Do
                                    End If

                                    If dic_ModifiedPinProperties.ContainsKey(oOldPinProperty.Name) Then
                                        Dim oModifiedProperty As AAL.SymbolPinProperty = dic_ModifiedPinProperties.Item(oOldPinProperty.Name)
                                        Dim oTempProperty As AAL.SymbolPinProperty = CompareProperties(sb_ChangeReport, oModifiedProperty, oOldPinProperty)

                                        If Not IsNothing(oTempProperty) Then
                                            oOldPinProperty = oTempProperty
                                            bChangesMade = True
                                        End If
                                    End If

                                    sb_Symbol.AppendLine(oOldPinProperty.Encrypt(b_PrecisionUnit).ToString())
                                Else
                                    sb_Symbol.AppendLine(currentLine)

                                End If

                            ElseIf currentLine.StartsWith("L ") Then
                                Dim value = Nothing
                                Dim lineSplit As String() = Split(currentLine, " ")
                                Dim slineAttribute As String() = Split(lineSplit(7), "=", 1)

                                If Not lineSplit(3) = 0 Then
                                    If b_PrecisionUnit = True Then
                                        iFontSize = lineSplit(3) / 25400
                                    Else
                                        iFontSize = lineSplit(3)
                                    End If
                                End If

                                iRotation = lineSplit(4)

                                sb_Symbol.AppendLine(currentLine)

                                currentLine = arFile.GetValue(currentlineCount + 1)

                                If currentLine.StartsWith("Q ") Then
                                    currentlineCount += 1
                                    currentLine = arFile.GetValue(currentlineCount + 1)
                                End If

                                If currentLine.StartsWith("|FNTSTL ") Then

                                    If AutomaticText = True Then
                                        Dim slineFNTSTL As String() = Split(currentLine, " ")

                                        slineFNTSTL(1) = -1

                                        currentLine = String.Join(" ", slineFNTSTL)

                                    End If

                                    currentlineCount += 1
                                    sb_Symbol.AppendLine(currentLine)
                                    If l_NewPinProperties.Count > 0 Then
                                        sb_ChangeReport.AppendLine(vbTab & vbTab & "New Pin Properties:")
                                        For Each oNewPinProperty As AAL.SymbolPinProperty In l_NewPinProperties
                                            sb_ChangeReport.AppendLine(vbTab & vbTab & oNewPinProperty.ToString())
                                            sb_Symbol.AppendLine(oNewPinProperty.Encrypt(b_PrecisionUnit).ToString())
                                        Next
                                        sb_ChangeReport.AppendLine(vbTab & vbTab & "End New Pin Properties:")
                                        bChangesMade = True
                                    End If
                                End If

                                If currentLine.StartsWith("|GRPHSTL ") And AutomaticGraphics = True Then
                                    currentlineCount += 1
                                End If
                            Else
                                sb_Symbol.AppendLine(currentLine)
                            End If

                            currentlineCount += 1
                            currentLine = arFile.GetValue(currentlineCount)

                        Loop

                        currentlineCount -= 1
                        currentLine = arFile.GetValue(currentlineCount)

                    ElseIf (currentLine.StartsWith("c ") Or currentLine.StartsWith("a ") Or currentLine.StartsWith("l ") Or currentLine.StartsWith("b ")) And AutomaticGraphics = True Then

                        sb_Symbol.AppendLine(currentLine)

                        currentLine = arFile.GetValue(currentlineCount + 1)

                        If currentLine.StartsWith("Q ") Then

                            sb_Symbol.AppendLine(currentLine)

                            currentlineCount += 1
                            currentLine = arFile.GetValue(currentlineCount + 1)
                        End If

                        If currentLine.StartsWith("|GRPHSTL ") Then
                            currentlineCount += 1
                            Dim linesplit As String()

                            linesplit = Regex.Split(currentLine, "\s+")

                            linesplit(1) = -1

                            currentLine = String.Join(" ", linesplit)

                            sb_Symbol.AppendLine(currentLine)
                        Else
                            sb_Symbol.AppendLine("|GRPHSTL -1 0 0 1")
                        End If
                    Else

                        sb_Symbol.AppendLine(currentLine)

                    End If

                    If currentlineCount + 1 < arFile.Length Then
                        currentLine = arFile.GetValue(currentlineCount + 1)

                        If (currentLine.StartsWith("|FNTSTL")) Then
                            currentlineCount += 2
                        Else
                            currentlineCount += 1
                        End If
                    Else
                        Exit Do
                    End If

                Loop

                If My.Computer.FileSystem.FileExists(Path.GetFullPath(fileOnDisk) & "_old") Then

                    My.Computer.FileSystem.DeleteFile(Path.GetFullPath(fileOnDisk) & "_old")

                End If

                Dim sSymPath As String = fileOnDisk

                If File.Exists(sSymPath & "_bak") Then
                    File.Delete(sSymPath & "_bak")
                End If

                File.Move(sSymPath, sSymPath & "_bak")

                Using writer As StreamWriter = New StreamWriter(fileOnDisk, True, System.Text.Encoding.ASCII)
                    writer.Write(sb_Symbol.ToString())
                End Using

                If bChangesMade = True Then

                    If sb_RemovedProperties.Length > 0 Then
                        sb_ChangeReport.AppendLine(vbTab & vbTab & "The following properties were removed:")
                        sb_ChangeReport.AppendLine(sb_RemovedProperties.ToString())
                    End If

                    sb_FullChangeReport.Append(sb_ChangeReport.ToString())

                End If

                sb_ChangeReport = Nothing
                sb_RemovedProperties = Nothing

                RaiseEvent eUpdateCount("process")

            End If

        Next

        If sb_FullChangeReport.Length > 0 Then
            RaiseEvent eProcessComplete(sPartition, sb_FullChangeReport)
        Else
            RaiseEvent eProcessComplete(sPartition, Nothing)
        End If

        sb_FullChangeReport = Nothing

    End Sub

    Function PropertiesAndValues(ByVal sPartition As String)

        Dim dic_Symbols As New Dictionary(Of String, ArrayList)

        Dim addline As Boolean = False

        Dim propertyIndex As New ArrayList

        For Each fileOnDisk As String In My.Computer.FileSystem.GetFiles(libDoc.Path & "\Symbollibs\" & sPartition & "\sym\", FileIO.SearchOption.SearchTopLevelOnly)
            Dim ext As String = Path.GetExtension(fileOnDisk)
            Dim r As Regex = New Regex("\.\d+$") ' Looks to see if the extension is a number
            Dim m As Match = r.Match(ext)
            If (m.Success) Then
                Dim oGetData As New AAL.Read
                Dim oSymbol As New AAL.Symbol

                Dim l_NonCommonProperties As New List(Of String)

                oSymbol = oGetData.ASCII2SymbolObject(Path.GetFileNameWithoutExtension(fileOnDisk), sPartition, libDoc.Path, oSymbol)

                Dim l_SymAttsWithSpaces As List(Of String)

                Dim SymbolProperties As ArrayList

                If dic_Symbols.ContainsKey(Path.GetFileName(fileOnDisk)) Then
                    SymbolProperties = dic_Symbols.Item(Path.GetFileName(fileOnDisk))
                Else
                    SymbolProperties = New ArrayList
                    For i As Integer = 0 To propertyIndex.Count - 1
                        SymbolProperties.Add(Nothing)
                    Next

                End If

                If Not IsNothing(oSymbol) Then

                    For Each oProperty As AAL.SymbolProperty In oSymbol.Properties.Values
                        Dim Prop As New KeyValuePair(Of String, String)(oProperty.Name.Trim(), oProperty.Value)
                        If Not propertyIndex.Contains(oProperty.Name.Trim()) Then
                            propertyIndex.Add(oProperty.Name.Trim())
                            SymbolProperties.Add(Prop)
                        Else
                            Dim index As Integer = propertyIndex.IndexOf(oProperty.Name.Trim())
                            If Not SymbolProperties.Contains(oProperty.Name) Then SymbolProperties.Insert(index, Prop)
                        End If

                    Next
                Else
                    Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "Bad Symbols.log", True, System.Text.Encoding.ASCII)
                        writer.Write(Path.GetFileName(fileOnDisk))
                    End Using
                End If

                If SymbolProperties.Count > 0 Then
                    dic_Symbols.Item(Path.GetFileName(fileOnDisk)) = SymbolProperties
                End If

            End If

            RaiseEvent eUpdateCount("analyze")

        Next

        RaiseEvent eReadPropertyValuesComplete(sPartition, dic_Symbols, propertyIndex)

        'MsgBox("finished with" & sSymDirectory)

    End Function

    Function PurgeDotX(ByVal sSymDirectory)

        For Each fileOnDisk As String In My.Computer.FileSystem.GetFiles(sSymDirectory, FileIO.SearchOption.SearchAllSubDirectories)
            Dim ext As String = Path.GetExtension(fileOnDisk)
            Dim r As Regex = New Regex("\.\d+$") ' Looks to see if the extension is a number
            Dim m As Match = r.Match(ext)
            If (m.Success) Then

                If alSymExt.Contains(ext) Then

                    File.Delete(fileOnDisk)

                End If

            End If

        Next

    End Function

    Function ReadAttributes(ByVal sPartition As String)

        Dim dic_UniqueSymProperties As New Dictionary(Of String, AAL.SymbolProperty)(StringComparer.OrdinalIgnoreCase)
        Dim dic_UniquePinProperties As New Dictionary(Of String, AAL.SymbolPinProperty)(StringComparer.OrdinalIgnoreCase)
        Dim dic_Symbols As New Dictionary(Of String, Object)
        Dim dic_SymPropswithTrailingSpaces As New Dictionary(Of String, List(Of String))
        Dim dic_NonCommonProperties As New Dictionary(Of String, List(Of String))

        Dim addline As Boolean = False

        For Each fileOnDisk As String In My.Computer.FileSystem.GetFiles(libDoc.Path & "\Symbollibs\" & sPartition & "\sym\", FileIO.SearchOption.SearchTopLevelOnly)
            Dim ext As String = Path.GetExtension(fileOnDisk)
            Dim r As Regex = New Regex("\.\d+$") ' Looks to see if the extension is a number
            Dim m As Match = r.Match(ext)
            If (m.Success) Then
                Dim oGetData As New AAL.Read
                Dim oSymbol As New AAL.Symbol

                Dim l_NonCommonProperties As New List(Of String)

                oSymbol = oGetData.ASCII2SymbolObject(Path.GetFileNameWithoutExtension(fileOnDisk), sPartition, libDoc.Path, oSymbol)

                Dim l_SymAttsWithSpaces As List(Of String)

                If dic_SymPropswithTrailingSpaces.ContainsKey(Path.GetFileName(fileOnDisk)) Then
                    l_SymAttsWithSpaces = dic_Symbols.Item(Path.GetFileName(fileOnDisk))
                Else
                    l_SymAttsWithSpaces = New List(Of String)
                End If

                Dim dic_SymbolProperties As Dictionary(Of String, AAL.SymbolProperty)
                Dim dic_PinProperties As Dictionary(Of String, AAL.SymbolPinProperty)

                If dic_Symbols.ContainsKey(Path.GetFileName(fileOnDisk)) Then
                    dic_SymbolProperties = dic_Symbols.Item(Path.GetFileName(fileOnDisk)).Symbol
                    dic_PinProperties = dic_Symbols.Item(Path.GetFileName(fileOnDisk)).Pin
                Else
                    dic_SymbolProperties = New Dictionary(Of String, AAL.SymbolProperty)(StringComparer.OrdinalIgnoreCase)
                    dic_PinProperties = New Dictionary(Of String, AAL.SymbolPinProperty)(StringComparer.OrdinalIgnoreCase)
                End If

                If Not IsNothing(oSymbol) Then

                    For Each oProperty As AAL.SymbolProperty In oSymbol.Properties.Values
                        If Not dic_UniqueSymProperties.ContainsKey(oProperty.Name) Then dic_UniqueSymProperties.Add(oProperty.Name, oProperty)
                        If Not dic_SymbolProperties.ContainsKey(oProperty.Name) Then dic_SymbolProperties.Add(oProperty.Name, oProperty)

                        If Not LibraryData.SymbolCommonProperties.Contains(oProperty.Name.Trim, StringComparer.OrdinalIgnoreCase) Or Not (LibraryData.SymbolCommonProperties.Contains(Replace(oProperty.Name.Trim, "_", " "), StringComparer.OrdinalIgnoreCase)) Then
                            If LibraryData.PropertyMapping.ContainsKey(oProperty.Name.Trim) Then
                                Dim sRenamedProperty As String = LibraryData.PropertyMapping.Item(oProperty.Name.Trim)
                                If LibraryData.SymbolCommonProperties.Contains(sRenamedProperty, StringComparer.OrdinalIgnoreCase) Then
                                    oProperty.Name = sRenamedProperty
                                Else
                                    l_NonCommonProperties.Add(sRenamedProperty)
                                End If
                            Else
                                l_NonCommonProperties.Add(oProperty.Name.Trim)
                            End If
                        End If

                        If oProperty.Name.EndsWith(" ") And Not l_SymAttsWithSpaces.Contains(oProperty.Name.ToLower) Then
                            l_SymAttsWithSpaces.Add(oProperty.Name.ToLower)
                        End If
                    Next

                    For Each oPinProperty As AAL.SymbolPinProperty In oSymbol.PinProperties.Values
                        If Not dic_UniquePinProperties.ContainsKey(oPinProperty.Name) Then dic_UniquePinProperties.Add(oPinProperty.Name, oPinProperty)

                        If Not dic_PinProperties.ContainsKey(oPinProperty.Name) Then dic_PinProperties.Add(oPinProperty.Name, oPinProperty)

                        If Not LibraryData.SymbolCommonProperties.Contains(oPinProperty.Name.Trim, StringComparer.OrdinalIgnoreCase) Or Not (LibraryData.SymbolCommonProperties.Contains(Replace(oPinProperty.Name.Trim, "_", " "), StringComparer.OrdinalIgnoreCase)) Then
                            If LibraryData.PropertyMapping.ContainsKey(oPinProperty.Name.Trim) Then
                                Dim sRenamedProperty As String = LibraryData.PropertyMapping.Item(oPinProperty.Name.Trim)
                                If LibraryData.SymbolCommonProperties.Contains(sRenamedProperty, StringComparer.OrdinalIgnoreCase) Then
                                    oPinProperty.Name = sRenamedProperty
                                Else
                                    l_NonCommonProperties.Add(sRenamedProperty)
                                End If
                            Else
                                l_NonCommonProperties.Add(oPinProperty.Name.Trim)
                            End If
                        End If

                        If oPinProperty.Name.EndsWith(" ") And Not l_SymAttsWithSpaces.Contains(oPinProperty.Name.ToLower) Then
                            l_SymAttsWithSpaces.Add(oPinProperty.Name.ToLower)
                        End If

                    Next
                Else
                    Using writer As StreamWriter = New StreamWriter(LibraryData.LogPath & "Bad Symbols.log", True, System.Text.Encoding.ASCII)
                        writer.Write(Path.GetFileName(fileOnDisk))
                    End Using
                End If

                If l_SymAttsWithSpaces.Count > 0 Then
                    dic_SymPropswithTrailingSpaces.Add(Path.GetFileName(fileOnDisk), l_SymAttsWithSpaces)
                End If

                If dic_SymbolProperties.Count > 0 Or dic_PinProperties.Count > 0 Then
                    Dim SymbolProperties As Object = New With {.Symbol = dic_SymbolProperties, .Pin = dic_PinProperties}

                    dic_Symbols.Item(Path.GetFileName(fileOnDisk)) = SymbolProperties
                End If

                If l_NonCommonProperties.Count > 0 Then
                    dic_NonCommonProperties.Add(Path.GetFileName(fileOnDisk), l_NonCommonProperties)
                End If

            End If

            RaiseEvent eUpdateCount("analyze")

        Next

        RaiseEvent eReadComplete(sPartition, dic_UniqueSymProperties, dic_Symbols, dic_SymPropswithTrailingSpaces, dic_UniquePinProperties, dic_NonCommonProperties)

        'MsgBox("finished with" & sSymDirectory)

    End Function

    Function RenameSym(ByVal SymPartition As String)

        Dim iSymbols As Integer = My.Computer.FileSystem.GetFiles(libDoc.Path & "\SymbolLibs\" & SymPartition & "\sym", FileIO.SearchOption.SearchTopLevelOnly).Count

        RaiseEvent SymbolCount(iSymbols)

        Dim l_DuplicateSymbols As New List(Of String)

        For Each fileOnDisk As String In My.Computer.FileSystem.GetFiles(libDoc.Path & "\SymbolLibs\" & SymPartition & "\sym", FileIO.SearchOption.SearchTopLevelOnly)
            Dim ext As String = Path.GetExtension(fileOnDisk)
            Dim r As Regex = New Regex("\.\d+$") ' Looks to see if the extension is a number
            Dim m As Match = r.Match(ext)
            If (m.Success) Then

                Dim sSymbol As String = Path.GetFileNameWithoutExtension(fileOnDisk)
                Dim sRenameTo As String
                If dicSymbolRename.TryGetValue(sSymbol, sRenameTo) Then

                    If Not IsNothing(sRenameTo) Then
                        Dim sRenamePath As String = Path.GetDirectoryName(fileOnDisk) & "\" & sRenameTo & Path.GetExtension(fileOnDisk)

                        If Not fileOnDisk = sRenamePath Then

                            If Not File.Exists(sRenamePath) Then
                                Dim arFile As String() = File.ReadAllLines(fileOnDisk)

                                Dim arFileLength As Integer = arFile.Length

                                Dim sb_Symbol As New StringBuilder

                                For Each arLine In arFile

                                    If arLine.StartsWith("K ") Then
                                        Dim slineSplit As String() = Split(arLine, " ")

                                        slineSplit(2) = sRenameTo

                                        arLine = Join(slineSplit, " ")

                                    End If

                                    sb_Symbol.AppendLine(arLine)

                                Next

                                Using writer As StreamWriter = New StreamWriter(sRenamePath, True, System.Text.Encoding.ASCII)
                                    writer.WriteLine(sb_Symbol.ToString())
                                End Using

                                File.Delete(fileOnDisk)
                            Else
                                l_DuplicateSymbols.Add(Path.GetFileName(fileOnDisk))

                                Dim sBackupPath As String = Path.GetDirectoryName(fileOnDisk) & "\sym_Backup"

                                If Not IO.Directory.Exists(Path.GetDirectoryName(fileOnDisk) & "\sym_Backup") Then

                                    Directory.CreateDirectory(Path.GetDirectoryName(fileOnDisk) & "\sym_Backup")

                                End If

                                File.Move(fileOnDisk, Path.GetDirectoryName(fileOnDisk) & "\sym_Backup\" & Path.GetFileName(fileOnDisk))

                            End If

                        End If
                    End If

                End If

            End If

            RaiseEvent SymbolFinish()

        Next

        RaiseEvent RenameSymbolsComplete(SymPartition, l_DuplicateSymbols)

    End Function

#End Region

#Region "Private Methods"

    Private Function CompareProperties(ByRef sb_ChangeReport As StringBuilder, ByVal oPropertyModifications As Object, ByVal oCurrentProperty As Object) As Object

        Dim bPrintPropertyName As Boolean = True
        Dim bChangesMade As Boolean = False

        If Not String.Compare(oPropertyModifications.Name, oCurrentProperty.Name, True) = 0 And oPropertyModifications.Modifications.Contains(AAL.PropertyMods.Name) Then

            If bPrintPropertyName = True Then
                sb_ChangeReport.AppendLine(vbTab & vbTab & oPropertyModifications.Name)
                bPrintPropertyName = False
            End If

            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Renamed:")
            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "From: " & oCurrentProperty.Name)

            oCurrentProperty.Name = oPropertyModifications.Name
            bChangesMade = True
        End If

        If Not String.IsNullOrEmpty(oPropertyModifications.Value) And oPropertyModifications.Modifications.Contains(AAL.PropertyMods.Value) Then

            If Not oPropertyModifications.Value = oCurrentProperty.Value Then

                If bPrintPropertyName = True Then
                    sb_ChangeReport.AppendLine(vbTab & vbTab & oCurrentProperty.Name)
                    bPrintPropertyName = False
                End If

                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Value:")
                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before: " & oCurrentProperty.Value)
                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "After: " & oPropertyModifications.Value)

                oCurrentProperty.Value = oPropertyModifications.Value

                bChangesMade = True

            End If

        End If

        If oPropertyModifications.Modifications.Contains(AAL.PropertyMods.Color) Then

            If oCurrentProperty.AutomaticColor = True And oPropertyModifications.AutomaticColor = False Then

                If bPrintPropertyName = True Then
                    sb_ChangeReport.AppendLine(vbTab & vbTab & oCurrentProperty.Name)
                    bPrintPropertyName = False
                End If

                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Color:")
                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before: Automatic")
                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "After (RGB): " & oPropertyModifications.Color.R & ", " & oPropertyModifications.Color.G & ", " & oPropertyModifications.Color.B)
                oCurrentProperty.Color = oPropertyModifications.Color
                oCurrentProperty.AutomaticColor = False
                bChangesMade = True

            ElseIf oCurrentProperty.AutomaticColor = False And oPropertyModifications.AutomaticColor = True Then

                If bPrintPropertyName = True Then
                    sb_ChangeReport.AppendLine(vbTab & vbTab & oCurrentProperty.Name)
                    bPrintPropertyName = False
                End If

                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Color:")
                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before (RGB): " & oCurrentProperty.Color.R & ", " & oCurrentProperty.Color.G & ", " & oCurrentProperty.Color.B)
                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "After: Automatic")
                oCurrentProperty.Color = Nothing
                oCurrentProperty.AutomaticColor = True

                bChangesMade = True

            ElseIf Not oCurrentProperty.Color = oPropertyModifications.Color Then

                If bPrintPropertyName = True Then
                    sb_ChangeReport.AppendLine(vbTab & vbTab & oCurrentProperty.Name)
                    bPrintPropertyName = False
                End If

                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Color:")
                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before (RGB): " & oCurrentProperty.Color.R & ", " & oCurrentProperty.Color.G & ", " & oCurrentProperty.Color.B)
                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "After (RGB): " & oPropertyModifications.Color.R & ", " & oPropertyModifications.Color.G & ", " & oPropertyModifications.Color.B)
                oCurrentProperty.Color = oPropertyModifications.Color

                bChangesMade = True

            End If

        End If

        If Not oPropertyModifications.FontType = oCurrentProperty.FontType And oPropertyModifications.Modifications.Contains(AAL.PropertyMods.Font) Then

            If bPrintPropertyName = True Then
                sb_ChangeReport.AppendLine(vbTab & vbTab & oCurrentProperty.Name)
                bPrintPropertyName = False
            End If

            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Font Style:")
            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before: " & oCurrentProperty.FontTypeToString)
            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "After: " & oPropertyModifications.FontTypeToString)

            oCurrentProperty.FontType = oPropertyModifications.FontType
            bChangesMade = True

        End If

        If Not oPropertyModifications.FontSize = 0 And oPropertyModifications.Modifications.Contains(AAL.PropertyMods.FontSize) Then
            If Not oPropertyModifications.FontSize = oCurrentProperty.FontSize Then

                If bPrintPropertyName = True Then
                    sb_ChangeReport.AppendLine(vbTab & vbTab & oCurrentProperty.Name)
                    bPrintPropertyName = False
                End If

                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Font Size:")
                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before: " & oCurrentProperty.FontSize)
                sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "After: " & oPropertyModifications.FontSize)

                oCurrentProperty.FontType = oPropertyModifications.FontType
                bChangesMade = True
            End If
        End If

        'If Not oPropertyModifications.Alignment = 9999 Then
        If Not oPropertyModifications.Alignment = oCurrentProperty.Alignment And oPropertyModifications.Modifications.Contains(AAL.PropertyMods.Justification) Then

            If bPrintPropertyName = True Then
                sb_ChangeReport.AppendLine(vbTab & vbTab & oCurrentProperty.Name)
                bPrintPropertyName = False
            End If

            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Alignment:")
            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before: " & oCurrentProperty.AlignmentToString)
            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "After: " & oPropertyModifications.AlignmentToString)

            oCurrentProperty.Alignment = oPropertyModifications.Alignment
            bChangesMade = True
        End If
        ' End If

        'If Not oPropertyModifications.Direction = 9999 Then
        If Not oPropertyModifications.Direction = oCurrentProperty.Direction And oPropertyModifications.Modifications.Contains(AAL.PropertyMods.Orientation) Then

            If bPrintPropertyName = True Then
                sb_ChangeReport.AppendLine(vbTab & vbTab & oCurrentProperty.Name)
                bPrintPropertyName = False
            End If

            sb_ChangeReport.AppendLine(vbTab & vbTab & "Direction:")
            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Before: " & oCurrentProperty.DirectionToString)
            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "After: " & oPropertyModifications.DirectionToString)

            oCurrentProperty.Direction = oPropertyModifications.Direction
            bChangesMade = True
        End If
        'End If

        'If Not oPropertyModifications.Display = 9999 Then
        If Not oPropertyModifications.Display = oCurrentProperty.Display And oPropertyModifications.Modifications.Contains(AAL.PropertyMods.Visbility) Then

            If bPrintPropertyName = True Then
                sb_ChangeReport.AppendLine(vbTab & vbTab & oCurrentProperty.Name)
                bPrintPropertyName = False
            End If

            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Visibility:")
            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before: " & oCurrentProperty.DisplayToString)
            sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "After: " & oPropertyModifications.DisplayToString)

            oCurrentProperty.Display = oPropertyModifications.Display
            bChangesMade = True
        End If
        'End If

        If bChangesMade = True Then
            Return oCurrentProperty
        Else
            Return Nothing
        End If

    End Function

#End Region

    'Private Function CompareProperties(ByRef sb_ChangeReport As StringBuilder, ByVal oPropertyModifications As AAL.SymbolPinProperty, ByVal oCurrentProperty As AAL.SymbolPinProperty) As AAL.SymbolPinProperty
    '    Dim bPrintPropertyName As Boolean = True
    '    Dim bChangesMade As Boolean = False

    ' If Not String.Compare(oPropertyModifications.Name, oCurrentProperty.Name, True) = 0 Then

    ' If bPrintPropertyName = True Then sb_ChangeReport.AppendLine(vbTab & vbTab &
    ' oCurrentProperty.Name) bPrintPropertyName = False End If

    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Renamed:") sb_ChangeReport.AppendLine(vbTab
    ' & vbTab & vbTab & vbTab & "From: " & oCurrentProperty.Name)

    ' oCurrentProperty.Name = oPropertyModifications.Name bChangesMade = True End If

    ' If Not String.IsNullOrEmpty(oPropertyModifications.Value) Then

    ' If Not oPropertyModifications.Value = oCurrentProperty.Value Then

    ' If bPrintPropertyName = True Then sb_ChangeReport.AppendLine(vbTab & vbTab &
    ' oCurrentProperty.Name) bPrintPropertyName = False End If

    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Value:") sb_ChangeReport.AppendLine(vbTab &
    ' vbTab & vbTab & vbTab & "Before: " & oCurrentProperty.Value) sb_ChangeReport.AppendLine(vbTab &
    ' vbTab & vbTab & vbTab & "After: " & oPropertyModifications.Value)

    ' oCurrentProperty.Value = oPropertyModifications.Value

    ' bChangesMade = True

    ' End If

    ' End If

    ' If oCurrentProperty.AutomaticColor = True And oPropertyModifications.AutomaticColor = False Then

    ' If bPrintPropertyName = True Then sb_ChangeReport.AppendLine(vbTab & vbTab &
    ' oCurrentProperty.Name) bPrintPropertyName = False End If

    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Color:") sb_ChangeReport.AppendLine(vbTab &
    ' vbTab & vbTab & vbTab & "Before: Automatic") sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab &
    ' vbTab & "After (RGB): " & oPropertyModifications.Color.R & ", " &
    ' oPropertyModifications.Color.G & ", " & oPropertyModifications.Color.B) oCurrentProperty.Color
    ' = oPropertyModifications.Color oCurrentProperty.AutomaticColor = False bChangesMade = True

    ' ElseIf oCurrentProperty.AutomaticColor = False And oPropertyModifications.AutomaticColor = True Then

    ' If bPrintPropertyName = True Then sb_ChangeReport.AppendLine(vbTab & vbTab &
    ' oCurrentProperty.Name) bPrintPropertyName = False End If

    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Color:") sb_ChangeReport.AppendLine(vbTab &
    ' vbTab & vbTab & vbTab & "Before (RGB): " & oCurrentProperty.Color.R & ", " &
    ' oCurrentProperty.Color.G & ", " & oCurrentProperty.Color.B) sb_ChangeReport.AppendLine(vbTab &
    ' vbTab & vbTab & vbTab & "After: Automatic") oCurrentProperty.Color = Nothing
    ' oCurrentProperty.AutomaticColor = True

    ' bChangesMade = True

    ' ElseIf Not oCurrentProperty.Color = oPropertyModifications.Color Then

    ' If bPrintPropertyName = True Then sb_ChangeReport.AppendLine(vbTab & vbTab &
    ' oCurrentProperty.Name) bPrintPropertyName = False End If

    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Color:") sb_ChangeReport.AppendLine(vbTab &
    ' vbTab & vbTab & vbTab & "Before (RGB): " & oCurrentProperty.Color.R & ", " &
    ' oCurrentProperty.Color.G & ", " & oCurrentProperty.Color.B) sb_ChangeReport.AppendLine(vbTab &
    ' vbTab & vbTab & vbTab & "After (RGB): " & oPropertyModifications.Color.R & ", " &
    ' oPropertyModifications.Color.G & ", " & oPropertyModifications.Color.B) oCurrentProperty.Color
    ' = oPropertyModifications.Color

    ' bChangesMade = True

    ' End If

    ' If Not oPropertyModifications.FontType = oCurrentProperty.FontType Then

    ' If bPrintPropertyName = True Then sb_ChangeReport.AppendLine(vbTab & vbTab &
    ' oCurrentProperty.Name) bPrintPropertyName = False End If

    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Font Style:")
    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before: " &
    ' oCurrentProperty.FontTypeToString) sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab &
    ' "After: " & oPropertyModifications.FontTypeToString)

    ' oCurrentProperty.FontType = oPropertyModifications.FontType bChangesMade = True

    ' End If

    ' If Not oPropertyModifications.FontSize = 0 Then If Not oPropertyModifications.FontSize =
    ' oCurrentProperty.FontSize Then

    ' If bPrintPropertyName = True Then sb_ChangeReport.AppendLine(vbTab & vbTab &
    ' oCurrentProperty.Name) bPrintPropertyName = False End If

    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Font Size:")
    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before: " &
    ' oCurrentProperty.FontSize) sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "After: "
    ' & oPropertyModifications.FontSize)

    ' oCurrentProperty.FontType = oPropertyModifications.FontType bChangesMade = True End If End If

    ' If Not oPropertyModifications.Alignment = oCurrentProperty.Alignment Then

    ' If bPrintPropertyName = True Then sb_ChangeReport.AppendLine(vbTab & vbTab &
    ' oCurrentProperty.Name) bPrintPropertyName = False End If

    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Alignment:")
    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before: " &
    ' oCurrentProperty.AlignmentToString) sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab &
    ' "After: " & oPropertyModifications.AlignmentToString)

    ' oCurrentProperty.Alignment = oPropertyModifications.Alignment bChangesMade = True End If

    ' If Not oPropertyModifications.Direction = oCurrentProperty.Direction Then

    ' If bPrintPropertyName = True Then sb_ChangeReport.AppendLine(vbTab & vbTab &
    ' oCurrentProperty.Name) bPrintPropertyName = False End If

    ' sb_ChangeReport.AppendLine(vbTab & vbTab & "Direction:") sb_ChangeReport.AppendLine(vbTab &
    ' vbTab & vbTab & "Before: " & oCurrentProperty.DirectionToString)
    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "After: " & oPropertyModifications.DirectionToString)

    ' oCurrentProperty.Direction = oPropertyModifications.Direction bChangesMade = True End If

    ' If Not oPropertyModifications.Display = oCurrentProperty.Display Then

    ' If bPrintPropertyName = True Then sb_ChangeReport.AppendLine(vbTab & vbTab &
    ' oCurrentProperty.Name) bPrintPropertyName = False End If

    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & "Visibility:")
    ' sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab & "Before: " &
    ' oCurrentProperty.DisplayToString) sb_ChangeReport.AppendLine(vbTab & vbTab & vbTab & vbTab &
    ' "After: " & oPropertyModifications.DisplayToString)

    ' oCurrentProperty.Display = oPropertyModifications.Display bChangesMade = True End If

    ' If bChangesMade = True Then Return oCurrentProperty Else Return Nothing End If

    'End Function

End Class