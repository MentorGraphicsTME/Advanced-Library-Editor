Imports System.ComponentModel
Imports System.Text.RegularExpressions
Imports System.IO

Public Class Data

#Region "Public Fields + Properties + Events + Delegates + Enums"

    Property CellList As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
    Property CellsByPartition As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

    'Dictionary
    Property CellsByPartitionByType As New Dictionary(Of String, Dictionary(Of CellEditorAddinLib.ECellDBCellType, List(Of String)))

    Enum EditorType

        <Description("Not Defined")> NotDefined = 0
        <Description("Schematic/Symbol")> Symbol = 1
        <Description("Part")> Part = 2
        <Description("I/O Model")> IOModel = 3
        <Description("Schematic/Symbol, Part")> Symbol_Part = 4
        <Description("Schematic/Symbol, I/O Model")> Symbol_PIOModel = 5
        <Description("Part, I/O Model")> Part_IOModel = 6
        <Description("Schematic/Symbol, Part, I/O Model")> Symbol_Part_IOModel = 7

    End Enum

    'String
    Property LibPath As String

    Enum LibType

        <Description("DX")> DX = 0
        <Description("DC")> DC = 1

    End Enum

    Property LogPath As String
    Property PadstackList As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Property PadstacksByType As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
    Property PartList As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Property PartsByPartition As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

    'List
    Property PDBCommonProperties As New List(Of String)

    Property PDBNoncommonProperties As New List(Of String)
    Property PDBType As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Property PropertyMapping As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    Enum PropertyType

        <Description("Symbol")> Symbol = 0
        <Description("Pin")> Pin = 1
        <Description("Both")> Both = 2

    End Enum

    Property SI As Dictionary(Of Char, Decimal)
    Property SymbolCommonProperties As New List(Of String)
    Property SymbolList As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)
    Property SymbolNoncommonPinProperties As New List(Of String)
    Property SymbolNoncommonProperties As New List(Of String)
    Property SymbolPinCommonProperties As New List(Of String)
    Property SymbolsByPartition As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

    'Object
    Property Type As LibType

    Public Data()

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    Dim Centlibprp As New List(Of String)
    Dim dicLeftovers As New Dictionary(Of Integer, Integer)

#End Region

#Region "Public Constructors + Destructors"

    Public Sub New()
        SI = New Dictionary(Of Char, Decimal)
        SI.Add("T", 1000000000000)
        SI.Add("G", 1000000000)
        SI.Add("M", 1000000)
        SI.Add("k", 1000)
        SI.Add("h", 100)
        SI.Add("d", 0.1)
        SI.Add("c", 0.01)
        SI.Add("m", 0.001)
        SI.Add("u", 0.000001)
        SI.Add("n", 0.000000001)
        SI.Add("p", 0.000000000001)

    End Sub

#End Region

#Region "Public Methods"

    Public Function MappingKeybyValue(ByRef Value As String) As String

        For Each kvp As KeyValuePair(Of String, String) In PropertyMapping
            If kvp.Value = Value Then
                Return kvp.Key
            End If
        Next

        Return String.Empty

    End Function

    ''' <summary>
    ''' Adds a new property to the centlib.prp file. 
    ''' </summary>
    ''' <param name="PropertyName"> Name of the new property. </param>
    ''' <param name="Type">         Property type. </param>
    ''' <param name="sLibPath">     Path to the central library. </param>
    ''' <returns> Index for the new property. </returns>
    ''' <exception cref="System.Exception"> CentlibPRP has not been read. </exception>
    Function NewCommonProperty(ByVal PropertyName As String, Optional ByVal Type As Data.PropertyType = PropertyType.Symbol, Optional ByVal AvailableEditor As Data.EditorType = EditorType.Symbol, Optional sLibPath As String = Nothing) As Integer

        If Centlibprp.Count = 0 Then
            Throw New System.Exception("CentlibPRP has not been read.")
        End If

        Dim Index As Integer = dicLeftovers.First.Key
        Dim sNewProperty As String
        Select Case Type

            Case PropertyType.Symbol
                sNewProperty = "*TEXTPROP  " & Index & "  CELL 132  "".*""  1  """ & PropertyName.Trim & """  0.080in  VISIBLE  SINGLE  TEMPLATE  ""DEFAULT""  4  3  3  1  1  1"
            Case PropertyType.Pin
                sNewProperty = "*TEXTPROP  " & Index & "  PIN 132  "".*""  1  """ & PropertyName.Trim & """  0.080in  VISIBLE  SINGLE  TEMPLATE  ""DEFAULT""  4  3  3  1  1  1"
            Case PropertyType.Both
                sNewProperty = "*TEXTPROP  " & Index & "  CELL|PIN 132  "".*""  1  """ & PropertyName.Trim & """  0.080in  VISIBLE  SINGLE  TEMPLATE  ""DEFAULT""  4  3  3  1  1  1"

        End Select

        Centlibprp.Insert(dicLeftovers.Item(Index) - 1, sNewProperty)

        dicLeftovers.Remove(Index)

        File.Delete(Path.GetDirectoryName(LibPath) & "\centlib.prp")

        Using writer As StreamWriter = New StreamWriter(Path.GetDirectoryName(LibPath) & "\centlib.prp", True, System.Text.Encoding.ASCII)
            For Each line As String In Centlibprp
                writer.WriteLine(line)
            Next
        End Using

        If SymbolNoncommonProperties.Contains(PropertyName) Then SymbolNoncommonProperties.Remove(PropertyName)
        SymbolCommonProperties.Add(PropertyName)

        Return Index

    End Function

    ''' <summary>
    ''' Reads the centlibrary.prp file. 
    ''' </summary>
    ''' <param name="sLibPath"> Path to the central library. </param>
    Sub ReadCentLibPRP(Optional sLibPath As String = Nothing)

        Dim arCentLibPRPFile As String()

        If (Not String.IsNullOrEmpty(sLibPath)) Then
            LibPath = sLibPath
        End If

        arCentLibPRPFile = File.ReadAllLines(Path.GetDirectoryName(LibPath) & "\centlib.prp")

        Dim PreviousIndex As Integer = 0

        For i As Integer = 0 To arCentLibPRPFile.Length - 1

            Dim m_Property As MatchCollection = Regex.Matches(arCentLibPRPFile(i), """(.*?)""")

            If arCentLibPRPFile(i).StartsWith("*TEXTPROP") Then
                Dim sLineSplit() As String = Regex.Split(arCentLibPRPFile(i), "\s+")

                Dim sProperty As String = String.Empty
                Dim iCurrentIndex As Integer = sLineSplit(1)

                If m_Property.Count > 1 Then
                    sProperty = m_Property(1).Value.Replace("""", String.Empty).Trim
                Else
                    sProperty = m_Property(0).Value.Replace("""", String.Empty).Trim
                End If

                Dim IndexDifference As Integer = iCurrentIndex - PreviousIndex

                Dim PredictedLineIndex As Integer = 1

                While (IndexDifference) > 1
                    If (220 < (PreviousIndex + 1) And (PreviousIndex + 1) < 256) Or (456 < (PreviousIndex + 1) And (PreviousIndex + 1) < 512) Or (1024 < (PreviousIndex + 1) And (PreviousIndex + 1) < 2048) Then
                        dicLeftovers.Add(PreviousIndex + 1, i + PredictedLineIndex)
                        PredictedLineIndex += 1
                    End If
                    PreviousIndex += 1
                    IndexDifference = iCurrentIndex - PreviousIndex

                End While

                PreviousIndex = iCurrentIndex

                If sLineSplit(2) = "CELL" Then
                    If Not SymbolCommonProperties.Contains(sProperty) Then SymbolCommonProperties.Add(sProperty)
                ElseIf sLineSplit(2) = "PIN" Then
                    If Not SymbolPinCommonProperties.Contains(sProperty) Then SymbolPinCommonProperties.Add(sProperty)
                ElseIf sLineSplit(2) = "CELL|PIN" Then
                    If Not SymbolCommonProperties.Contains(sProperty) Then SymbolCommonProperties.Add(sProperty)
                    If Not SymbolPinCommonProperties.Contains(sProperty) Then SymbolPinCommonProperties.Add(sProperty)
                End If

            End If

            Centlibprp.Add(arCentLibPRPFile(i))

        Next

    End Sub

#End Region

End Class