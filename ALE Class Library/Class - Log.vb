Public Class BuildPDBLogItem

#Region "Public Fields + Properties + Events + Delegates + Enums"

    Public AlternateSymbols As New ArrayList
    Public Errors As List(Of String)
    Public Notes As List(Of String)
    Public PN As String
    Public success As Boolean
    Public Warnings As List(Of String)

#End Region

#Region "Public Constructors + Destructors"

    Public Sub New(ByVal success As Boolean, ByVal number As String, ByVal errors As List(Of String), ByVal warnings As List(Of String), ByVal notes As List(Of String), ByVal logAlternateSymbols As ArrayList)
        Me.success = success
        Me.PN = number
        Me.Errors = errors
        Me.Warnings = warnings
        Me.Notes = notes
        Me.AlternateSymbols = logAlternateSymbols
    End Sub

#End Region

End Class

Public Class Log

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

    Event eUpdateStatus(status As String)

    Enum Type
        Warning
        Note
        Other
    End Enum

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    Dim _Errors As New List(Of String)
    Dim _Notes As New List(Of String)
    Dim _Warnings As New List(Of String)

#End Region

#Region "Public Methods"

    Public Sub Add(ByVal Type As Type, ByVal Reason As String)

        Select Case Type
            Case Log.Type.Warning
                Warnings.Add(Reason)
                RaiseEvent eUpdateStatus("Warning: " & Reason)
            Case Log.Type.Note
                Notes.Add(Reason)
                RaiseEvent eUpdateStatus("Note: " & Reason)
            Case Else
                Errors.Add(Reason)
                RaiseEvent eUpdateStatus("Error: " & Reason)
        End Select

    End Sub

#End Region

End Class