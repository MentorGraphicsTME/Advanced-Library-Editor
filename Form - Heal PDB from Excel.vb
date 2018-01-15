Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Text

Public Class frmHealPDBfromExcel

    Dim xlsApp As New Excel.Application
    Dim xlsBook As Excel.Workbook
    Dim xlsSheet As Excel.Worksheet = Nothing
    Dim xlsNewSheet As Excel.Worksheet = Nothing
    Dim bCloseExcel As Boolean = False
    Dim dicPartData As New Dictionary(Of String, Object)(StringComparer.OrdinalIgnoreCase)

    Dim alPartPartitionList As New ArrayList()

    Private Sub btn_Browse_Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Browse_Excel.Click

        Using ofd As New OpenFileDialog
            ofd.Filter = "Excel Files|*.xlsx;*.xls"
            '            ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"

            If ofd.ShowDialog() = Windows.Forms.DialogResult.OK Then

                bCloseExcel = True

                tboxWorkbook.Text = Path.GetFileName(ofd.FileName)

                xlsApp = New Excel.Application

                xlsApp.Visible = True

                xlsApp.Workbooks.Open(ofd.FileName)

                xlsBook = xlsApp.ActiveWorkbook

                'MsgBox("Neutral Data has been loaded...")

                gbActions.Enabled = True
                lblCell.Enabled = True
                cbox_CellName.Enabled = True
                lblPN.Enabled = True
                cbox_PartNumber.Enabled = True
                lblSymbol.Enabled = True
                cbox_SymbolName.Enabled = True

                cbox_CellName.SelectedIndex = -1
                cbox_PartNumber.SelectedIndex = -1
                cbox_SymbolName.SelectedIndex = -1

            End If

            Me.BringToFront()

        End Using

    End Sub

    Private Sub Form___Heal_PDB_using_Excel_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        Try
            xlsApp = GetObject(, "Excel.Application")
            'MsgBox(excelApp.ActiveWorkbook.FullName())
            xlsBook = xlsApp.ActiveWorkbook
            tboxWorkbook.Text = Path.GetFileName(xlsBook.Name)

            gbActions.Enabled = True
            lblCell.Enabled = True
            cbox_CellName.Enabled = True
            lblPN.Enabled = True
            cbox_PartNumber.Enabled = True
            lblSymbol.Enabled = True
            cbox_SymbolName.Enabled = True

            cbox_CellName.SelectedIndex = -1
            cbox_PartNumber.SelectedIndex = -1
            cbox_SymbolName.SelectedIndex = -1

        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnRead_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRead.Click
        If Not (cbox_PartNumber.SelectedItem = Nothing) Or Not (cbox_SymbolName.SelectedItem = Nothing) Or Not (cbox_CellName.SelectedItem = Nothing) Then

            gbOptions.Enabled = True
            gbLog.Enabled = True
            btnProcess.Enabled = True

            For Each xlsSheet In xlsBook.Worksheets

                xlsSheet.Select()

                Dim i As Integer

                i = 1

                Do While Not IsNothing(xlsSheet.Range(cbox_PartNumber.Text & i).Value)

                    Dim sPartNumber As String
                    Dim alSymbols As New ArrayList()
                    Dim alCells As New ArrayList()

                    sPartNumber = xlsSheet.Range(cbox_PartNumber.Text & i).Value

                    sPartNumber = sPartNumber.Trim()

                    Dim xlsSymValue As String = xlsSheet.Range(cbox_SymbolName.Text & i).Value

                    If xlsSymValue.Contains(";") Then
                        Dim sSymSplit As String() = Split(xlsSheet.Range(cbox_SymbolName.Text & i).Value, ";")

                        For Each sSymbol In sSymSplit

                            sSymbol = sSymbol.Trim()
                            alSymbols.Add(sSymbol)

                        Next

                    ElseIf xlsSymValue.Contains(",") Then
                        Dim sSymSplit As String() = Split(xlsSheet.Range(cbox_SymbolName.Text & i).Value, ",")

                        For Each sSymbol In sSymSplit

                            sSymbol = sSymbol.Trim()
                            alSymbols.Add(sSymbol)

                        Next

                    Else

                        alSymbols.Add(xlsSheet.Range(cbox_SymbolName.Text & i).Value)

                    End If

                    Dim xlsCellValue As String = xlsSheet.Range(cbox_CellName.Text & i).Value

                    If xlsSymValue.Contains(";") Then
                        Dim sCellSplit As String() = Split(xlsSheet.Range(cbox_CellName.Text & i).Value, ";")

                        For Each sCell In sCellSplit

                            sCell = sCell.Trim()
                            alCells.Add(sCell)

                        Next

                    ElseIf xlsSymValue.Contains(",") Then
                        Dim sCellSplit As String() = Split(xlsSheet.Range(cbox_CellName.Text & i).Value, ",")

                        For Each sCell In sCellSplit

                            sCell = sCell.Trim()
                            alCells.Add(sCell)

                        Next

                    Else

                        alCells.Add(xlsSheet.Range(cbox_CellName.Text & i).Value)

                    End If

                    i = i + 1

                    Dim partAtts As Object = New Object() {Nothing, alSymbols, alCells}

                    dicPartData.Add(sPartNumber, partAtts)

                Loop

            Next

            If bCloseExcel = True Then
                xlsApp.Quit()
            End If

        End If
    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click

    End Sub
End Class