Imports System.IO
Imports System.Text
Imports System.Threading
Imports Microsoft.Office.Interop

Public Class frmBatch_PDB_Properties

#Region "Private Fields"

    Private bCloseExcel As Boolean

    Private col_Name As String

    Private col_PartNumber As String

    Private col_Value As String

    Dim dicPartitions As New Dictionary(Of String, Dictionary(Of String, AAL.Part))

    Private modifyProperty As Boolean

    Dim xlsApp As Excel.Application

    Dim xlsBook As Excel.Workbook

    Dim xlsSheet As Excel.Worksheet = Nothing

    Dim xlsSheetName As String = Nothing

#End Region

#Region "Public Delegates"

    Delegate Sub d_ReadComplete()

    Delegate Sub d_UpdateComplete()

    Delegate Sub d_UpdateStatus(ByVal status As String)

#End Region

#Region "Public Events"

    Event eReadComplete()

    Event eUpdateComplete()

    Event eUpdateStatus(status As String)

#End Region

#Region "Private Methods"

    Private Sub btn_Browse_Excel_Click(sender As Object, e As EventArgs) Handles btn_Browse_Excel.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "Excel Files|*.xlsx;*.xls"
            ' ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                bCloseExcel = True

                tboxWorkbook.Text = Path.GetFileName(ofd.FileName)

                xlsApp = New Excel.Application

                xlsApp.Visible = True

                xlsApp.Workbooks.Open(ofd.FileName)

                xlsBook = xlsApp.ActiveWorkbook

                'MsgBox("Neutral Data has been loaded...")

                For Each sheet In xlsBook.Worksheets

                    cboxActiveSheet.Items.Add(sheet.name)

                Next

                If xlsBook.Worksheets.Count = 1 Then

                    cboxActiveSheet.SelectedIndex = 0
                Else

                    cboxActiveSheet.SelectedIndex = -1

                End If

                cbox_Value.SelectedIndex = -1
                cbox_Name.SelectedIndex = -1
                cbox_PartNumber.SelectedIndex = -1

                gb_ExcelInfo.Enabled = True
                gb_Options.Enabled = True

                ts_Status.Text = "Provide the required information..."

            End If

            Me.BringToFront()

        End Using
    End Sub

    Private Sub btnClearName_Click(sender As Object, e As EventArgs) Handles btnClearName.Click
        cbox_Name.SelectedIndex = -1
        btnClearName.Visible = False
    End Sub

    Private Sub btnClearPN_Click(sender As Object, e As EventArgs) Handles btnClearPN.Click
        cbox_PartNumber.SelectedIndex = -1
        btnClearPN.Visible = False
    End Sub

    Private Sub btnClearValue_Click(sender As Object, e As EventArgs) Handles btnClearValue.Click
        cbox_Value.SelectedIndex = -1
        btnClearValue.Visible = False
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        ts_Status.Text = "Processing PDBs..."

        If (cboxAction.Text = "Add/Modify") Then
            modifyProperty = True
        End If

        WaitGif.Enabled = True

        Dim t_Update As Thread = New Threading.Thread(AddressOf ProcessPDB)
        t_Update.IsBackground = True
        t_Update.Start()
    End Sub

    Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click
        If Not (cbox_Name.SelectedItem = Nothing) Or Not (cbox_PartNumber.SelectedItem = Nothing) Or Not (cbox_Value.SelectedItem = Nothing) Then

            col_Name = cbox_Name.Text

            col_PartNumber = cbox_PartNumber.Text

            col_Value = cbox_Value.Text

            dgvInput.Rows.Clear()

            ts_Status.Text = "Reading Excel Spreadsheet..."

            dicPartitions = New Dictionary(Of String, Dictionary(Of String, AAL.Part))

            WaitGif.Enabled = True

            Dim t_Update As Thread = New Threading.Thread(AddressOf ReadExcel)
            t_Update.IsBackground = True
            t_Update.Start()

        End If
    End Sub

    Private Sub cboxAction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboxAction.SelectedIndexChanged
        If Not cboxAction.SelectedIndex = -1 Then
            btnProcess.Enabled = True
        End If
    End Sub

    Private Sub cboxActiveSheet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboxActiveSheet.SelectedIndexChanged
        If Not cboxActiveSheet.SelectedIndex = -1 Then
            xlsSheet = xlsBook.Sheets(cboxActiveSheet.Text)
            xlsSheet.Select()
        End If
    End Sub

    Private Sub frmBatch_PDB_Properties_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Location = New System.Drawing.Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eReadComplete, AddressOf ReadExcelComplete
        AddHandler eUpdateComplete, AddressOf UpdateComplete

        Try
            xlsApp = GetObject(, "Excel.Application")
            'MsgBox(excelApp.ActiveWorkbook.FullName())
            xlsBook = xlsApp.ActiveWorkbook
            If Not IsNothing(xlsBook) Then

                tboxWorkbook.Text = Path.GetFileName(xlsBook.Name)

                For Each sheet In xlsBook.Worksheets

                    cboxActiveSheet.Items.Add(sheet.name)

                Next

                xlsSheet = xlsBook.ActiveSheet

                If xlsBook.Worksheets.Count = 1 Then

                    cboxActiveSheet.SelectedIndex = 0
                Else

                    cboxActiveSheet.SelectedIndex = -1

                End If

                cbox_Name.SelectedIndex = -1
                cbox_PartNumber.SelectedIndex = -1
                cbox_Value.SelectedIndex = -1

                gb_ExcelInfo.Enabled = True
                gb_Options.Enabled = True
                ts_Status.Text = "Provided the needed information..."
            End If
        Catch ex As Exception
            'WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
        End Try

    End Sub

    Private Sub ProcessPDB()

        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg

        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        pedApp = frmMain.libDoc.PartEditor
        pedDoc = pedApp.ActiveDatabaseEx

        Dim LogFile As String = frmMain.librarydata.LogPath & "Batch PDB Properties.log"

        Dim l_NewProperties As New List(Of String)
        Dim sbMods As New StringBuilder

        For Each kvpPartition As KeyValuePair(Of String, Dictionary(Of String, AAL.Part)) In dicPartitions
            Dim pdbPartition As MGCPCBPartsEditor.Partition = pedDoc.Partitions(kvpPartition.Key).Item(1)

            For Each kvpPart As KeyValuePair(Of String, AAL.Part) In kvpPartition.Value
                For Each pdbPart As MGCPCBPartsEditor.Part In pdbPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll, kvpPart.Key)
                    RaiseEvent eUpdateStatus("Modifying: " & pdbPartition.Name & ":" & kvpPart.Key)
                    sbMods.AppendLine(pdbPartition.Name & ": " & kvpPart.Key)
                    For Each prop As KeyValuePair(Of String, String) In kvpPart.Value.Properties
                        If Not frmMain.librarydata.PDBCommonProperties.Contains(prop.Key) And chkbox_AddNonCommonProperty.Checked Then
                            frmMain.librarydata.NewCommonProperty(prop.Key, Data.PropertyType.Symbol, Data.EditorType.Part)
                            l_NewProperties.Add(prop.Key)
                        End If

                        Dim pdbProperty As MGCPCBPartsEditor.Property = pdbPart.Properties.Item(prop.Key)
                        Try

                            If IsNothing(pdbProperty) Then
                                pdbProperty = pdbPart.PutPropertyEx(prop.Key, prop.Value)
                                sbMods.AppendLine(vbTab & "Adding - " & prop.Key & ", Value: " & pdbProperty.Value)
                            ElseIf modifyProperty = True Then
                                sbMods.AppendLine(vbTab & "Modified - " & prop.Key & ", From: " & pdbProperty.Value & ", To: " & prop.Value)
                                pdbProperty.Value = prop.Value
                            Else
                                sbMods.AppendLine(vbTab & "Removed - " & prop.Key & ", Value: " & pdbProperty.Value)
                                pdbProperty.Delete()
                            End If
                        Catch ex As Exception
                            sbMods.AppendLine(vbTab & "Error: Unable to modify property " & prop.Key & " because of reported error (" & ex.Message.ToString() & ")")
                        End Try

                    Next

                Next
            Next

        Next

        pedApp.SaveActiveDatabase()
        pedDoc = Nothing
        pedApp = Nothing

        Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)

            If l_NewProperties.Count > 0 Then
                writer.WriteLine("New common properties:")
                For Each newProp As String In l_NewProperties
                    writer.WriteLine(vbTab & newProp)
                Next

                writer.WriteLine()
                writer.WriteLine()
            End If

            writer.WriteLine("Property Modification:")

            writer.WriteLine(sbMods.ToString())
        End Using

        RaiseEvent eUpdateComplete()

    End Sub

    Private Sub ReadExcel()
        If (IsNothing(xlsApp)) Then
            Try
                xlsApp = GetObject(, "Excel.Application")
                xlsBook = xlsApp.ActiveWorkbook
            Catch ex As Exception
                ' WriterErrorToLog(ex.Message.ToString() & Environment.NewLine & Environment.NewLine & ex.StackTrace.ToString())
            End Try
        End If

        xlsSheet.Select()

        Dim LogFile As String = frmMain.librarydata.LogPath & "Batch PDB Properties.log"

        If File.Exists(LogFile) Then

            File.Delete(LogFile)

        End If

        Dim i As Integer = 1

        If cbox_ignoreHeader.Checked = True Then i = 2

        Dim lastRow As Integer = xlsSheet.UsedRange.Rows.Count

        Dim toGo As Integer = lastRow

        For row As Integer = i To lastRow
            RaiseEvent eUpdateStatus("Rows left to read: " & toGo)

            Dim PN As String = xlsSheet.Range(col_PartNumber & row).Value.ToString().Trim

            If frmMain.librarydata.PartList.ContainsKey(PN) Then
                Dim aalPart As AAL.Part

                Dim partition As String = frmMain.librarydata.PartList.Item(PN)
                Dim dicParts As Dictionary(Of String, AAL.Part)
                If dicPartitions.ContainsKey(partition) Then
                    dicParts = dicPartitions.Item(partition)
                Else
                    dicParts = New Dictionary(Of String, AAL.Part)
                End If

                If (dicParts.ContainsKey(PN)) Then
                    aalPart = dicParts.Item(PN)
                Else
                    aalPart = New AAL.Part()
                    aalPart.Number = PN
                    aalPart.Partition = partition
                End If

                aalPart.Properties.Item(xlsSheet.Range(col_Name & row).Value.ToString().Trim()) = xlsSheet.Range(col_Value & row).Value.ToString().Trim

                dicParts.Item(PN) = aalPart
                dicPartitions.Item(partition) = dicParts
            Else
                Using writer As StreamWriter = New StreamWriter(LogFile, True, System.Text.Encoding.ASCII)
                    writer.WriteLine(PN & " - Not found in the library.")
                End Using
            End If

            lastRow -= 1
        Next

        RaiseEvent eReadComplete()

    End Sub

    Private Sub ReadExcelComplete()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateComplete(AddressOf ReadExcelComplete)
            Me.Invoke(d)
        Else

            For Each kvp As KeyValuePair(Of String, Dictionary(Of String, AAL.Part)) In dicPartitions
                For Each kvpPart As KeyValuePair(Of String, AAL.Part) In kvp.Value
                    For Each kvpProperty As KeyValuePair(Of String, String) In kvpPart.Value.Properties
                        dgvInput.Rows.Add(kvpPart.Key, kvpProperty.Key, kvpProperty.Value)
                    Next
                Next
            Next

            WaitGif.Enabled = False
            If (dicPartitions.Count > 0) Then
                gb_Options.Visible = True
            End If
        End If
    End Sub

    Private Sub UpdateComplete()
        If Me.InvokeRequired Then

            Dim d As New d_ReadComplete(AddressOf UpdateComplete)
            Me.Invoke(d)
        Else

            WaitGif.Enabled = False
            gb_ExcelInfo.Enabled = True
            gb_Options.Visible = False
            WaitGif.Enabled = False
            RaiseEvent eUpdateStatus("Update complete.")

            Dim reply As DialogResult = MessageBox.Show("Batch update of properties has finished. Would you like to view the results?", "Finished", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

            If reply = DialogResult.Yes Then

                frmMain.OpenLogFile("Batch PDB Properties.log")
            Else
                MessageBox.Show("For more information, please see: " & Environment.NewLine & frmMain.librarydata.LogPath & "Batch PDB Properties.log")
            End If

        End If
    End Sub

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

#End Region

End Class