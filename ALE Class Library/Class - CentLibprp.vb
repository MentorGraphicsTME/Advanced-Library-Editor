Imports System.Text.RegularExpressions
Imports System.IO

Public Class CentLibprp
    Property SymbolCommonProperties As New List(Of String)
    Property SymbolNoncommonProperties As New List(Of String)
    Property SymbolPinCommonProperties As New List(Of String)
    Dim dicLeftovers As New Dictionary(Of Integer, Integer)

    Sub Read(ByVal LibPath As String)
        Dim arCentLibPRPFile As String() = File.ReadAllLines(Path.GetDirectoryName(LibPath) & "\centlib.prp")

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

                If sLineSplit(2).StartsWith("CELL") Then
                    If Not SymbolCommonProperties.Contains(sProperty) Then SymbolCommonProperties.Add(sProperty)
                ElseIf sLineSplit(2).StartsWith("PIN") Then
                    If Not SymbolPinCommonProperties.Contains(sProperty) Then SymbolPinCommonProperties.Add(sProperty)
                End If

            End If

        Next
    End Sub

End Class