Imports System.IO
Imports System.ComponentModel
Imports Microsoft.Office.Interop
Imports System.Text
Imports System.Threading
Imports System.Text.RegularExpressions
Imports System.Runtime.InteropServices
Imports System.Drawing

Public Class frmFindPartsinLibrary
    Dim xlsApp As Excel.Application
    Dim xlsBook As Excel.Workbook
    Dim xlsSheet As Excel.Worksheet
    Dim typeOfInput As InputType
    Dim partsNotInLibrary As New ArrayList
    Dim totalNumOfParts As UInteger

    Enum InputType
        ExcelWorkbook
        TextFile
    End Enum

    Dim partLibrary As Dictionary(Of String, List(Of String)) = frmMain.librarydata.PartsByPartition

    Private Sub btn_browse_excel_Click(sender As Object, e As EventArgs) Handles btn_browse_excel.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "Excel Files|*.xlsx;*.xls| Text File|*.txt"
            ofd.Title = "Select File"

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                If (Path.GetExtension(ofd.FileName) = ".xls" Or Path.GetExtension(ofd.FileName) = ".xlsx") Then
                    tbox_input.Text = ofd.FileName
                    typeOfInput = InputType.ExcelWorkbook
                    pnl_exceloptions.Enabled = True
                    ts_Status.Text = "Opening spreadsheet ..."
                    xlsApp = New Excel.Application
                    xlsApp.Visible = True
                    xlsApp.Workbooks.Open(ofd.FileName)
                    xlsBook = xlsApp.ActiveWorkbook

                    For Each sheet As Excel.Worksheet In xlsBook.Worksheets
                        cbox_spreadsheet.Items.Add(sheet.Name)
                    Next

                    ts_Status.Text = "Select a worksheet and part number column ..."
                    btn_Read.Enabled = True
                Else
                    typeOfInput = InputType.TextFile
                    tbox_input.Text = ofd.FileName
                    btn_Read.Enabled = True
                End If

            End If

        End Using

    End Sub

    Private Sub btn_Read_Click(sender As Object, e As EventArgs) Handles btn_Read.Click
        Select Case typeOfInput
            Case InputType.TextFile
                Dim arFile As String() = File.ReadAllLines(tbox_input.Text)
                totalNumOfParts = arFile.Count
                For Each line As String In arFile
                    ts_Status.Text = "Checking if " & line.Trim() & " in library."
                    If Not frmMain.librarydata.PartList.Keys.Contains(line.Trim) Then
                        partsNotInLibrary.Add(line.Trim())
                    End If
                Next
            Case InputType.ExcelWorkbook
                If cbox_spreadsheet.SelectedIndex = -1 Or cbox_partNumber.SelectedIndex = -1 Then
                    MsgBox("Please fill out all options before continuing.")
                    Exit Sub
                End If

                Dim i As UInteger = 1
                Dim xlsSheet As Excel.Worksheet = xlsBook.Worksheets(cbox_spreadsheet.Text)
                Dim partNumberCol As String = cbox_partNumber.Text

                Dim partNumber As String = xlsSheet.Range(partNumberCol & i).Value
                Do While Not IsNothing(partNumber)
                    ts_Status.Text = "Checking if " & partNumber.Trim() & " in library."
                    If Not frmMain.librarydata.PartList.Keys.Contains(partNumber.Trim()) Then
                        partsNotInLibrary.Add(partNumber.Trim())
                    End If
                    i += 1
                    partNumber = xlsSheet.Range(partNumberCol & i).Value
                Loop
                totalNumOfParts = i
        End Select
        ts_Status.Text = "Read Complete"
        logResults()
    End Sub

    Private Sub logResults()

        If File.Exists(frmMain.librarydata.LogPath & "Parts in Library - Not Found.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Parts in Library - Not Found.log")
        End If

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Parts in Library - Not Found.log", True, System.Text.Encoding.ASCII)
            Dim percentNotFound As Decimal = (partsNotInLibrary.Count / totalNumOfParts) * 100.0
            Dim percentFound As Decimal = 100.0 - percentNotFound
            writer.WriteLine("Parts Read :" & vbTab & totalNumOfParts)
            writer.WriteLine("    % Found:" & vbTab & Format(percentFound, "0.00"))
            writer.WriteLine("% Not Found:" & vbTab & Format(percentNotFound, "0.00"))
            writer.WriteLine(vbNewLine & vbTab & vbTab & "Parts Not Found")
            For Each partNumber As String In partsNotInLibrary
                writer.WriteLine(vbTab & vbTab & vbTab & partNumber)
            Next
        End Using
    End Sub

End Class