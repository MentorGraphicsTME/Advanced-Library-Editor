Public Class Rename

    Event ThreadComplete()

    Property oMoreInfo As New Rename_MoreInfo

    Dim cellDB As CellEditorAddinLib.CellDB

    Sub LoopCells(ByRef cellEd As CellEditorAddinLib.CellEditorDlg)

        ' Open the Cell Editor dialog and open the library database
        cellDB = cellEd.ActiveDatabase

        For Each oCellPartition As CellEditorAddinLib.Partition In cellDB.Partitions()

            If oMoreInfo.Partitions.Contains(oCellPartition.Name) Then
                For Each oCell As CellEditorAddinLib.Cell In oCellPartition.Cells  ' process each cell in the partition

                    'Log File writer:
                    'Using writer As StreamWriter = New StreamWriter(LogPath & "\Logfiles\Cell Cleanup.log", True, System.Text.Encoding.ASCII)
                    '    writer.WriteLine()
                    'End Using

                    If oMoreInfo.NameCase = "Lowercase" Then
                        oCell.Name = LCase(oCell.Name)
                    Else
                        oCell.Name = UCase(oCell.Name)
                    End If

                Next

                cellEd.SaveActiveDatabase()

            End If

            oCellPartition = Nothing

        Next
        '
        cellDB = Nothing
        cellEd.Quit()
        cellEd = Nothing

        RaiseEvent ThreadComplete()

    End Sub

End Class

Public Class Rename_MoreInfo
    Inherits System.EventArgs
    Private _Partitions As New ArrayList()
    Private _CaseTo As String

    Public Property Partitions() As ArrayList
        Get
            Return _Partitions
        End Get
        Set(ByVal partitions As ArrayList)
            _Partitions = partitions
        End Set
    End Property
    Public Property NameCase() As String
        Get
            Return _CaseTo
        End Get
        Set(ByVal namecase As String)
            _CaseTo = namecase
        End Set
    End Property

End Class