Imports System.IO
Imports System.Threading

Public Class frmSymPurge

    Dim alSymExt As New ArrayList()

    'Property frmmain.librarydata As Data

    Private Sub btnPurge_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPurge.Click

        Dim reply As DialogResult = MessageBox.Show("Purge current library of selected symbols?", "Confirm Purge...", _
      MessageBoxButtons.YesNo, MessageBoxIcon.Stop, MessageBoxDefaultButton.Button1)

        If reply = DialogResult.Yes Then

            alSymExt.Clear()

            For Each Entry In chklist_SymExt.CheckedItems
                alSymExt.Add(Entry.ToString())
            Next

            Dim Root As New DirectoryInfo(frmMain.librarydata.LogPath & "\SymbolLibs\")

            Dim Dirs As DirectoryInfo() = Root.GetDirectories("*.*")

            Dim cProcessSymAtts(Dirs.Count - 1) As Symbols
            For i As Integer = 0 To Dirs.Count - 1
                Dim cModSymAtt As Symbols = New Symbols()
                cModSymAtt.alSymExt = alSymExt
                cProcessSymAtts(i) = cModSymAtt
            Next

            Dim newThreads(Dirs.Count) As Thread
            For i As Integer = 0 To Dirs.Count - 1
                newThreads(i) = New Thread(AddressOf cProcessSymAtts(i).PurgeDotX)
                newThreads(i).IsBackground = True
                newThreads(i).Start(Dirs(i).FullName)
            Next i

            For i As Integer = 0 To Dirs.Count - 1
                newThreads(i).Join()
            Next

            MsgBox("Finished")

        End If

    End Sub
End Class