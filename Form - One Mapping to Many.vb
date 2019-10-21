Imports System.Threading
Imports System.Runtime.InteropServices
Imports Microsoft.Office.Interop
Imports System.Text.RegularExpressions
Imports System.IO
Imports MGCPCBPartsEditor
Imports System.Text
Imports System.Drawing

Public Class frmDuplicatePart

#Region "Public Fields + Properties + Events + Delegates + Enums"

    Public Property bContainsSI As Boolean

    Delegate Sub d_AnalysisComplete()

    'Delegates
    Delegate Sub d_BuildFailed()

    'Property frmmain.librarydata As Data
    Delegate Sub d_UpdateComplete()

    Delegate Sub d_UpdateMainParts(ByVal Partition As String, ByVal Part As String)

    Delegate Sub d_UpdateStatus(ByVal status As String)

    Event eAnalysisComplete()

    Event eBuildFailed()

    'Events
    Event eCopyComplete()

    Event eUpdateStatus(status As String)

    Public Property Label_Col As String
    Public Property Name_Col As String

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    Dim After_Col As String
    Dim bCloseExcel As Boolean = False
    Dim Before_Col As String

    'Boolean
    Dim bEvaluationComplete As Boolean = False

    Dim Description_Col As String

    'Dictionary
    Dim dic_PNCopy As New Dictionary(Of String, Dictionary(Of String, AAL.Part))(StringComparer.OrdinalIgnoreCase)

    Dim Height_Col As String

    'Integer
    Dim iDupPN As Integer = 0

    Dim iMissingPN As Integer = 0
    Dim iNoChange As Integer = 0

    'String
    Dim s_SwapFile As String = Nothing

    Dim Value_Col As String

    ''Object
    Dim xlsApp As Excel.Application

    Dim xlsBook As Excel.Workbook
    Dim xlsSheet As Excel.Worksheet = Nothing

#End Region

#Region "Public Methods"

    Sub readExcel(xlsSheet As Excel.Worksheet)

        Dim index As Integer = 1
        If chkbox_IgnoreHeader.Checked = True Then
            index = 2
        End If

        Do While Not IsNothing(xlsSheet.Range(Before_Col & index).Value)
            RaiseEvent eUpdateStatus("Reading " & xlsSheet.Name & " row: " & index)

            Dim sBefore As String = xlsSheet.Range((Before_Col & index).ToString()).Value.ToString

            sBefore = sBefore.Trim()

            Dim linesplit As String()

            If sBefore.Contains(",") Then
                linesplit = Regex.Split(sBefore, ",")
            ElseIf sBefore.Contains(";") Then
                linesplit = Regex.Split(sBefore, ";")
            Else
                linesplit = {sBefore}
            End If

            If Not String.IsNullOrEmpty(xlsSheet.Range((After_Col & index).ToString()).Value.ToString) Then

                If linesplit.Length > 1 Then

                    Dim Pipe As Boolean = False
                    For Each sPN As String In linesplit
                        sBefore = sPN.Trim

                        Dim sAfter As String = xlsSheet.Range((After_Col & index).ToString()).Value.ToString.Trim

                        If Pipe = True Then
                            sAfter = sAfter & "|" & sPN
                        End If

                        Dim aalPart As New AAL.Part

                        aalPart.Number = sAfter

                        If Not String.IsNullOrEmpty(Description_Col) Then
                            aalPart.Description = xlsSheet.Range((Description_Col & index).ToString()).Value.ToString.Trim
                        End If

                        If Not String.IsNullOrEmpty(Name_Col) Then
                            aalPart.Name = xlsSheet.Range((Name_Col & index).ToString()).Value.ToString.Trim
                        End If

                        If Not String.IsNullOrEmpty(Label_Col) Then
                            aalPart.Label = xlsSheet.Range((Label_Col & index).ToString()).Value.ToString.Trim
                        End If

                        If Not String.IsNullOrEmpty(Height_Col) Then
                            aalPart.Properties("Height") = xlsSheet.Range((Height_Col & index).ToString()).Value.ToString.Trim()
                        End If

                        If Not String.IsNullOrEmpty(Value_Col) Then
                            aalPart.Properties.Add("Value", xlsSheet.Range((Value_Col & index).ToString()).Value.ToString.Trim)
                            If bContainsSI Then
                                Dim value As String = aalPart.Properties("Value")
                                Dim charArray As Char() = value.ToArray()
                                Dim decValue As Decimal = 0.0
                                value = ""
                                For Each c In charArray
                                    If (frmMain.librarydata.SI.ContainsKey(c)) Then
                                        decValue = Convert.ToDecimal(value) * frmMain.librarydata.SI(c)
                                        Exit For
                                    Else
                                        value = value & c
                                    End If
                                Next
                                aalPart.Properties("Value") = decValue
                            End If
                        End If

                        Dim dicParts As Dictionary(Of String, AAL.Part)
                        If dic_PNCopy.ContainsKey(sBefore) Then
                            dicParts = dic_PNCopy.Item(sBefore)
                        Else
                            dicParts = New Dictionary(Of String, AAL.Part)
                        End If

                        If Not dicParts.ContainsKey(sAfter) Then
                            dicParts.Add(sAfter, aalPart)
                        End If

                        dic_PNCopy.Item(sBefore) = dicParts
                        Pipe = True
                    Next
                Else

                    Dim sAfter As String = xlsSheet.Range((After_Col & index).ToString()).Value.ToString.Trim

                    Dim aalPart As New AAL.Part

                    aalPart.Number = sAfter

                    If Not String.IsNullOrEmpty(Description_Col) Then
                        aalPart.Description = xlsSheet.Range((Description_Col & index).ToString()).Value.ToString.Trim
                    End If

                    If Not String.IsNullOrEmpty(Name_Col) Then
                        aalPart.Name = xlsSheet.Range((Name_Col & index).ToString()).Value.ToString.Trim
                    End If

                    If Not String.IsNullOrEmpty(Label_Col) Then
                        aalPart.Label = xlsSheet.Range((Label_Col & index).ToString()).Value.ToString.Trim
                    End If

                    If Not String.IsNullOrEmpty(Height_Col) Then
                        aalPart.Properties("Height") = xlsSheet.Range((Height_Col & index).ToString()).Value.ToString.Trim()
                    End If

                    If Not String.IsNullOrEmpty(Value_Col) Then
                        aalPart.Properties.Add("Value", xlsSheet.Range((Value_Col & index).ToString()).Value.ToString.Trim)
                        If bContainsSI Then
                            Dim value As String = aalPart.Properties("Value")
                            Dim charArray As Char() = value.ToArray()
                            Dim decValue As Decimal = 0.0
                            value = ""
                            For Each c In charArray
                                If (frmMain.librarydata.SI.ContainsKey(c)) Then
                                    decValue = Convert.ToDecimal(value) * frmMain.librarydata.SI(c)
                                    Exit For
                                Else
                                    value = value & c
                                End If
                            Next
                            aalPart.Properties("Value") = decValue
                        End If
                    End If

                    Dim dicParts As Dictionary(Of String, AAL.Part)
                    If dic_PNCopy.ContainsKey(sBefore) Then
                        dicParts = dic_PNCopy.Item(sBefore)
                    Else
                        dicParts = New Dictionary(Of String, AAL.Part)
                    End If

                    If Not dicParts.ContainsKey(sAfter) Then
                        dicParts.Add(sAfter, aalPart)
                    End If

                    dic_PNCopy.Item(sBefore) = dicParts

                End If

            End If

            index += 1
        Loop

    End Sub

#End Region

#Region "Private Methods"

    Private Sub AnalysisComplete()
        If Me.InvokeRequired Then

            Dim d As New d_AnalysisComplete(AddressOf AnalysisComplete)
            Me.Invoke(d)
        Else

            Dim l_Values As New List(Of String)

            UpdateStatus("Analyzing results...")
            Refresh()

            Dim dic_Duplicates As New Dictionary(Of String, List(Of String))
            Dim dic_Missing As New Dictionary(Of String, List(Of String))

            Dim dic_TempDictionary As New Dictionary(Of String, Dictionary(Of String, AAL.Part))

            For Each kvp As KeyValuePair(Of String, Dictionary(Of String, AAL.Part)) In dic_PNCopy
                Dim tempPNs As New Dictionary(Of String, AAL.Part)

                For Each aalPart As AAL.Part In kvp.Value.Values
                    Dim i As Integer = dgvPNSwap.Rows.Add(kvp.Key, aalPart.Number)
                    If Not kvp.Key = aalPart.Number Then

                        If Not frmMain.librarydata.PartList.ContainsKey(kvp.Key) Then
                            dgvPNSwap.Rows(dgvPNSwap.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Red

                            Dim l_PNs As List(Of String)
                            If dic_Missing.ContainsKey(kvp.Key) Then
                                l_PNs = dic_Missing.Item(kvp.Key)
                            Else
                                l_PNs = New List(Of String)
                            End If

                            If Not l_PNs.Contains(aalPart.Number) Then
                                l_PNs.Add(aalPart.Number)
                                dic_Missing.Item(kvp.Key) = l_PNs
                            End If

                            iMissingPN += 1
                        Else
                            If l_Values.Contains(aalPart.Number) Or frmMain.librarydata.PartList.ContainsKey(aalPart.Number) Then
                                dgvPNSwap.Rows(dgvPNSwap.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Orange
                                iDupPN += 1

                                Dim l_PNs As List(Of String)
                                If dic_Duplicates.ContainsKey(kvp.Key) Then
                                    l_PNs = dic_Duplicates.Item(kvp.Key)
                                Else
                                    l_PNs = New List(Of String)
                                End If

                                If Not l_PNs.Contains(aalPart.Number) Then
                                    l_PNs.Add(aalPart.Number)
                                    dic_Duplicates.Item(kvp.Key) = l_PNs
                                End If
                            Else
                                l_Values.Add(aalPart.Number)
                                tempPNs(aalPart.Number) = aalPart
                            End If
                        End If
                    Else
                        dgvPNSwap.Rows(dgvPNSwap.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                        iNoChange += 1
                    End If
                    dgvPNSwap.Refresh()
                    dgvPNSwap.FirstDisplayedScrollingRowIndex = i
                Next

                If tempPNs.Count > 0 Then
                    dic_TempDictionary.Item(kvp.Key) = tempPNs
                End If

            Next

            dic_PNCopy = New Dictionary(Of String, Dictionary(Of String, AAL.Part))(dic_TempDictionary)
            dic_TempDictionary = Nothing

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Copy One Part to Many - Bad Data.log", True)

                writer.WriteLine("Duplicate Parts:")
                For Each kvp_Duplicates As KeyValuePair(Of String, List(Of String)) In dic_Duplicates

                    writer.WriteLine(vbTab & kvp_Duplicates.Key)

                    For Each sPN As String In kvp_Duplicates.Value
                        writer.WriteLine(vbTab & vbTab & sPN)
                    Next

                Next

                writer.WriteLine()
                writer.WriteLine("Missing Parts:")
                For Each kvp_Missing As KeyValuePair(Of String, List(Of String)) In dic_Missing

                    writer.WriteLine(vbTab & kvp_Missing.Key)

                    For Each sPN As String In kvp_Missing.Value
                        writer.WriteLine(vbTab & vbTab & sPN)
                    Next

                Next

            End Using

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Copy One Part to Many - Input.log", True)
                For Each kvp As KeyValuePair(Of String, Dictionary(Of String, AAL.Part)) In dic_PNCopy
                    writer.WriteLine(kvp.Key)
                    For Each sPN As String In kvp.Value.Keys
                        writer.WriteLine(vbTab & sPN)
                    Next
                    writer.WriteLine()
                Next
            End Using

            lblMissingPNs.Text = iMissingPN
            lblNoChange.Text = iNoChange
            lblDupPN.Text = iDupPN

            bEvaluationComplete = True

            If Not IsNothing(xlsApp) And bCloseExcel = True Then

                GC.Collect()
                GC.WaitForPendingFinalizers()

                xlsBook.Close(SaveChanges:=False)
                Marshal.FinalReleaseComObject(xlsBook)

                xlsApp.Quit()
                Marshal.FinalReleaseComObject(xlsApp)

                xlsApp = Nothing
            End If

            WaitGif.Enabled = False

            If iNoChange = 0 & iDupPN = 0 & iMissingPN = 0 Then
                frmMain.NotifyIcon.ShowBalloonTip(2000, "One to Many:", "All items were found to be unique...", ToolTipIcon.Info)
                ts_Status.Text = "All items are unique, please proceed..."
                gb_Options.Enabled = True

                cbox_After.Enabled = False
                cbox_Before.Enabled = False
                chkbox_IgnoreHeader.Enabled = False
                tbox_Input.Enabled = False
                btn_Browse_SwapPN.Enabled = False
            Else
                frmMain.NotifyIcon.ShowBalloonTip(2000, "One to Many:", "All items were not found to be unique...", ToolTipIcon.Error)
                If chkbox_IgnoreDuplicatesMissing.Checked = True Then
                    MsgBox("Problems were found during analysis. These problems will be ignored.", MsgBoxStyle.OkOnly, "Info:")
                    gb_Options.Enabled = True
                Else
                    MsgBox("There were problems found. Please fix these problems before proceeding...", MsgBoxStyle.OkOnly, "Error:")
                    gb_Options.Enabled = False
                End If

            End If
        End If
    End Sub

    Private Sub btn_Browse_SwapPN_Click(sender As System.Object, e As System.EventArgs) Handles btn_Browse_SwapPN.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "Text files (*.txt)|*.txt|Excel Files|*.xlsx;*.xls"
            ' ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                s_SwapFile = ofd.FileName
                tbox_Input.Text = ofd.FileName

                Dim ext As String = Path.GetExtension(tbox_Input.Text)

                If (ext = ".xls" Or ext = ".xlsx") Then

                    bCloseExcel = True

                    plSpreadsheetInfo.Enabled = True

                    ts_Status.Text = "Opening spreadsheet..."

                    Me.Refresh()

                    xlsApp = New Excel.Application

                    xlsApp.Visible = True

                    xlsApp.Workbooks.Open(ofd.FileName)

                    xlsBook = xlsApp.ActiveWorkbook

                    For Each sheet In xlsBook.Worksheets

                        cboxActiveSheet.Items.Add(sheet.name)

                    Next

                    xlsSheet = xlsBook.ActiveSheet

                    If xlsBook.Worksheets.Count = 1 Then

                        cboxActiveSheet.SelectedIndex = 0
                    Else

                        cboxActiveSheet.Text = xlsSheet.Name

                    End If

                    ts_Status.Text = String.Empty

                    btnEvaluate.Enabled = True

                End If

                dgvPNSwap.Rows.Clear()

            End If

        End Using
    End Sub

    Private Sub btn_Copy_Click(sender As System.Object, e As System.EventArgs) Handles btn_Copy.Click
        If File.Exists(frmMain.librarydata.LogPath & "\One to Many.log") Then

            File.Delete(frmMain.librarydata.LogPath & "\One to Many.log")

        End If

        WaitGif.Enabled = True

        plSpreadsheetInfo.Enabled = False

        ts_Status.Text = "Starting copy process..."
        frmMain.NotifyIcon.ShowBalloonTip(2000, "One to Many:", "Starting Copy", ToolTipIcon.Info)

        Dim t_SwapParts As Thread = New Threading.Thread(AddressOf CopyParts)
        t_SwapParts.IsBackground = True
        t_SwapParts.Start()
    End Sub

    Private Sub btnClearDescription_Click(sender As Object, e As EventArgs) Handles btnClearDescription.Click
        cbox_Description.SelectedIndex = -1
        btnClearDescription.Visible = False
    End Sub

    Private Sub btnClearHeight_Click(sender As Object, e As EventArgs) Handles btnClearHeight.Click
        cbox_Height.SelectedIndex = -1
        btnClearHeight.Visible = False
    End Sub

    Private Sub btnClearNew_Click(sender As Object, e As EventArgs) Handles btnClearNew.Click
        cbox_After.SelectedIndex = -1
        btnClearNew.Visible = False
    End Sub

    Private Sub btnClearSource_Click(sender As Object, e As EventArgs) Handles btnClearSource.Click
        cbox_Before.SelectedIndex = -1
        btnClearSource.Visible = False
    End Sub

    Private Sub btnClearValue_Click(sender As Object, e As EventArgs) Handles btnClearValue.Click
        cbox_Value.SelectedIndex = -1
        btnClearValue.Visible = False
    End Sub

    Private Sub btnEvaluate_Click(sender As System.Object, e As System.EventArgs) Handles btnEvaluate.Click

        Dim ext As String = Path.GetExtension(tbox_Input.Text)

        If (ext = ".xls" Or ext = ".xlsx") And (cbox_Before.SelectedIndex = -1 Or cbox_After.SelectedIndex = -1) Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "One to Many:", "Please select a before and after column before proceeding...", ToolTipIcon.Error)

            MessageBox.Show("Please select a before and after column before proceeding.")
            Exit Sub
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Copy One Part to Many - Bad Data.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Copy One Part to Many - Bad Data.log")
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Copy One Part to Many - Input.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Copy One Part to Many - Input.log")
        End If

        iDupPN = 0
        iNoChange = 0
        iMissingPN = 0

        Before_Col = cbox_Before.Text
        After_Col = cbox_After.Text
        Description_Col = cbox_Description.Text
        Value_Col = cbox_Value.Text
        Name_Col = cboxName.Text
        Label_Col = cboxLabel.Text
        Height_Col = cbox_Height.Text

        bContainsSI = chkbox_SINotation.Checked

        plSpreadsheetInfo.Enabled = False

        s_SwapFile = tbox_Input.Text

        dgvPNSwap.Rows.Clear()

        frmMain.NotifyIcon.ShowBalloonTip(2000, "One to Many:", "Reading input file...",
     ToolTipIcon.Info)
        ts_Status.Text = "Reading input file..."

        WaitGif.Enabled = True

        Dim t_ReadSwapFile As Thread = New Threading.Thread(AddressOf ReadSwapFile)
        t_ReadSwapFile.IsBackground = True
        t_ReadSwapFile.Start()
    End Sub

    Private Sub BuildFailed()
        If Me.InvokeRequired Then

            Dim d As New d_BuildFailed(AddressOf BuildFailed)
            Me.Invoke(d)
        Else
            WaitGif.Enabled = False
            MsgBox("Unable to open the PDB editor. This is most likely due to a reserved partition.")
            ts_Status.Text = "Build Failed..."
        End If
    End Sub

    Private Sub cbox_After_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_After.SelectedIndexChanged
        btnClearNew.Visible = True
    End Sub

    Private Sub cbox_Before_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_Before.SelectedIndexChanged
        btnClearSource.Visible = True
    End Sub

    Private Sub cbox_Description_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_Description.SelectedIndexChanged
        btnClearDescription.Visible = True
    End Sub

    Private Sub cbox_Height_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_Height.SelectedIndexChanged
        btnClearHeight.Visible = True
    End Sub

    Private Sub cbox_Value_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_Value.SelectedIndexChanged
        btnClearValue.Visible = True
    End Sub

    Private Sub cboxActiveSheet_SelectedIndexChanged(sender As System.Object, e As System.EventArgs)
        If Not cboxActiveSheet.SelectedIndex = -1 Then
            xlsSheet = xlsBook.Sheets(cboxActiveSheet.Text)
            xlsSheet.Select()
        End If
    End Sub

    Private Sub chkbox_IgnoreDuplicatesMissing_CheckedChanged(sender As System.Object, e As System.EventArgs)

        If bEvaluationComplete = True Then
            If chkbox_IgnoreDuplicatesMissing.Checked = False Then

                If iDupPN = 0 And iMissingPN = 0 Then
                    gb_Options.Enabled = True
                Else
                    gb_Options.Enabled = False

                End If
            Else gb_Options.Enabled = True
            End If
        End If

    End Sub

    Private Sub chkbox_ReadAllSheets_CheckedChanged(sender As System.Object, e As System.EventArgs)
        If chkbox_ReadAllSheets.Checked = True Then
            cboxActiveSheet.Enabled = False
            cboxActiveSheet.SelectedIndex = -1
        Else
            cboxActiveSheet.Enabled = True
            cboxActiveSheet.SelectedIndex = 0
        End If
    End Sub

    Private Sub CopyComplete()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateComplete(AddressOf CopyComplete)
            Me.Invoke(d)
        Else
            WaitGif.Enabled = False
            gb_ExcelInfo.Enabled = True
            btn_Copy.Enabled = False
            ts_Status.Text = "Finished"
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Copy One to Many:", "Finished", ToolTipIcon.Info)
            MessageBox.Show("Copy process completed with no errors warnings.", "Information:", MessageBoxButtons.OK, MessageBoxIcon.Information)

        End If
    End Sub

    Private Sub CopyParts()

        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        'pedApp = CreateObject("MGCPCBLibraries.PartsEditorDlg")
        pedApp = frmMain.libDoc.PartEditor
        Try
            pedDoc = pedApp.ActiveDatabaseEx
            'pedDoc = pedApp.OpenDatabaseEx(frmMain.librarydata.LibPath, False)
        Catch ex As Exception
            RaiseEvent eBuildFailed()
            pedApp.Quit()
            pedApp = Nothing
            Exit Sub
        End Try

        Dim b_PrintPartition As Boolean = False
        Dim b_SaveDatabase As Boolean = False

        For Each partition As MGCPCBPartsEditor.Partition In pedDoc.Partitions

            For Each kvp As KeyValuePair(Of String, Dictionary(Of String, AAL.Part)) In dic_PNCopy

                For Each part As Part In partition.Parts(EPDBPartType.epdbPartAll, kvp.Key)

                    b_SaveDatabase = True

                    RaiseEvent eUpdateStatus("Copying: " & partition.Name & ":" & part.Number)

                    'If b_PrintPartition = True Then

                    ' Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath &
                    ' "Swap Part Numbers.log", True)

                    ' writer.WriteLine(partition.Name & ":") writer.WriteLine()

                    ' End Using b_PrintPartition = False

                    'End If

                    For Each aalPN As AAL.Part In kvp.Value.Values

                        Dim pedPart As MGCPCBPartsEditor.Part = part.Copy()

                        pedPart.Number = aalPN.Number
                        If Not String.IsNullOrEmpty(aalPN.Name) Then
                            pedPart.Name = aalPN.Name
                        End If
                        If Not String.IsNullOrEmpty(aalPN.Label) Then
                            pedPart.Label = aalPN.Label
                        End If

                        For Each prop As KeyValuePair(Of String, String) In aalPN.Properties
                            Try
                                pedPart.Properties.Remove(prop.Key)
                            Catch ex As Exception

                            End Try
                            pedPart.PutPropertyEx(prop.Key, prop.Value, EPDBUnit.epdbUnitCurrent)
                        Next

                        UpdateMainParts(partition.Name, aalPN.Number)

                    Next

                    If chkbox_RemoveOriginal.Checked = True Then
                        RemoveMainParts(partition.Name, part.Number)
                        part.Delete()

                    End If

                Next

            Next

            If b_PrintPartition = False Then
                b_PrintPartition = True
            End If

            If b_SaveDatabase = True Then
                pedApp.SaveActiveDatabase()
                b_SaveDatabase = False
            End If

        Next

        ' unload
        pedApp.Quit()
        pedApp = Nothing
        pedDoc = Nothing

        RaiseEvent eCopyComplete()
    End Sub

    Private Sub dgvPNSwap_Leave(sender As System.Object, e As System.EventArgs) Handles dgvPNSwap.Leave
        dgvPNSwap.ClearSelection()
    End Sub

    Private Sub frmDuplicatePart_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        Try

            xlsApp = GetObject(, "Excel.Application") 'MsgBox(excelApp.ActiveWorkbook.FullName())
            xlsBook = xlsApp.ActiveWorkbook
            tbox_Input.Text = Path.GetFileName(xlsBook.Name)

            plSpreadsheetInfo.Enabled = True

            cbox_Before.SelectedIndex = -1
            cbox_After.SelectedIndex = -1

            For Each sheet In xlsBook.Worksheets

                cboxActiveSheet.Items.Add(sheet.name)

            Next

            xlsSheet = xlsBook.ActiveSheet

            If xlsBook.Worksheets.Count = 1 Then

                cboxActiveSheet.SelectedIndex = 0
            Else

                cboxActiveSheet.Text = xlsSheet.Name

            End If
        Catch ex As Exception

        End Try

        AddHandler eAnalysisComplete, AddressOf AnalysisComplete
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eCopyComplete, AddressOf CopyComplete

    End Sub

    Private Sub ReadSwapFile()
        dic_PNCopy.Clear()

        If (Path.GetExtension(s_SwapFile) = ".xls") Or (Path.GetExtension(s_SwapFile) = ".xlsx") Then

            If IsNothing(xlsApp) Then
                xlsApp = GetObject(, "Excel.Application")
                'MsgBox(excelApp.ActiveWorkbook.FullName())
                xlsBook = xlsApp.ActiveWorkbook
                xlsSheet = xlsBook.Sheets(cboxActiveSheet.Text)
            End If

            If chkbox_ReadAllSheets.Checked = True Then

                For Each xlsSheet As Excel.Worksheet In xlsBook.Worksheets

                    xlsSheet.Select()

                    readExcel(xlsSheet)

                Next
            Else

                readExcel(xlsSheet)

            End If

        ElseIf Path.GetExtension(s_SwapFile) = ".txt" Then

            Dim arFile As String() = File.ReadAllLines(s_SwapFile)

            Dim line As String
            For Each line In arFile

                'line = line.Trim()

                'Dim linesplit As String()

                'If line.Contains(",") Then
                '    linesplit = Regex.Split(line, ",")
                'ElseIf line.Contains(";") Then
                '    linesplit = Regex.Split(line, ";")
                'Else
                '    linesplit = Regex.Split(line, "\s+")
                'End If

                'If linesplit.Length > 1 Then

                ' Dim dicParts As Dictionary(Of String, AAL.Part) If
                ' dic_PNCopy.ContainsKey(linesplit(0)) Then dicParts = dic_PNCopy.Item(linesplit(0))
                ' Else dicParts = New Dictionary(Of String, AAL.Part) End If

                ' If Not dicParts.ContainsKey(linesplit(1)) Then dicParts(linesplit(1)) End If
                ' dic_PNCopy.Item(linesplit(0)) = dicParts

                'End If

            Next

        End If

        RaiseEvent eAnalysisComplete()
    End Sub

    Private Sub RemoveMainParts(Partition As String, Part As String)

        If Me.InvokeRequired Then

            Dim d As New d_UpdateMainParts(AddressOf RemoveMainParts)
            Me.Invoke(d, New Object() {Partition, Part})
        Else
            Dim l_Parts As List(Of String)
            If frmMain.librarydata.PartsByPartition.ContainsKey(Partition) Then
                l_Parts = frmMain.librarydata.PartsByPartition.Item(Partition)
            Else
                l_Parts = New List(Of String)
            End If

            l_Parts.Remove(Part)

            frmMain.librarydata.PartsByPartition.Item(Partition) = l_Parts

            frmMain.librarydata.PartList.Remove(Part)

            frmMain.ts_Parts.Text = "Parts: " & frmMain.librarydata.PartList.Count
        End If
    End Sub

    Private Sub UpdateMainParts(Partition As String, Part As String)

        If Me.InvokeRequired Then

            Dim d As New d_UpdateMainParts(AddressOf UpdateMainParts)
            Me.Invoke(d, New Object() {Partition, Part})
        Else
            Dim l_Parts As List(Of String)
            If frmMain.librarydata.PartsByPartition.ContainsKey(Partition) Then
                l_Parts = frmMain.librarydata.PartsByPartition.Item(Partition)
            Else
                l_Parts = New List(Of String)
            End If

            l_Parts.Add(Part)

            frmMain.librarydata.PartsByPartition.Item(Partition) = l_Parts

            frmMain.librarydata.PartList.Add(Part, Partition)

            frmMain.ts_Parts.Text = "Parts: " & frmMain.librarydata.PartList.Count
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