Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Text
Imports System.Threading
Imports Microsoft.Win32
Imports System.Runtime.InteropServices
Imports System.Xml.Serialization
Imports System.Text.RegularExpressions
Imports System.Drawing

Public Class frmRenameSymbols

    'String
    Dim sColBefore As String = "A"
    Dim sColAfter As String = "B"
    Dim specificWorksheet As String = Nothing

    'Boolean
    Dim bCloseExcel As Boolean = False
    Dim bRestartLib As Boolean

    ''Dictionary
    Dim dicSymbolRename As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
    Dim dicProblems As New Dictionary(Of String, List(Of String))
    Dim dicParts As New Dictionary(Of String, List(Of String))

    'Integers
    Dim iSymbolCount As Integer = 0

    'Objects
    Dim xlsApp As Excel.Application
    Dim xlsBook As Excel.Workbook
    Dim StatusLock As New Object
    Dim PartLock As New Object
    'Property frmmain.librarydata As Data

    'Event
    Event eReadComplete()
    Event eRenameComplete()
    Event eUpdateStatus(status As String)
    Event eAddPart(Partition As String, Part As String)

    'Delegate
    Delegate Sub dReadComplete()
    Delegate Sub d_UpdateStatus(ByVal status As String)

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_status.Text = status
        End If
    End Sub

    Private Sub btn_Browse_Excel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Browse_Excel.Click

        Using ofd As New OpenFileDialog
            ofd.Filter = "Text files (*.txt)|*.txt|Excel Files|*.xlsx;*.xls"
            '            ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"

            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then

                If (Path.GetExtension(ofd.FileName) = ".xls") Or (Path.GetExtension(ofd.FileName) = ".xlsx") Then

                    bCloseExcel = True

                    tbox_Input.Text = Path.GetFileName(ofd.FileName)

                    xlsApp = New Excel.Application

                    xlsApp.Visible = True

                    xlsApp.Workbooks.Open(ofd.FileName)

                    xlsBook = xlsApp.ActiveWorkbook

                    cbox_After.Enabled = True
                    cbox_Before.Enabled = True
                    cbox_worksheet.Enabled = True
                    chkbox_IgnoreHeader.Enabled = True
                    chkbox_ReadAllSheets.Enabled = True
                    lblPN.Enabled = True
                    lblRefDes.Enabled = True
                    lbl_worksheet.Enabled = True

                    For Each worksheet As Worksheet In xlsBook.Worksheets
                        If Not cbox_worksheet.Items.Contains(worksheet.Name) Then cbox_worksheet.Items.Add(worksheet.Name)
                    Next

                ElseIf Path.GetExtension(ofd.FileName) = ".txt" Then

                    tbox_Input.Text = ofd.FileName
                    cbox_After.Enabled = False
                    cbox_Before.Enabled = False
                    cbox_worksheet.Enabled = False
                    chkbox_IgnoreHeader.Enabled = False
                    chkbox_ReadAllSheets.Enabled = False
                    lblRefDes.Enabled = False
                    lblPN.Enabled = False
                    lbl_worksheet.Enabled = False

                    cbox_worksheet.SelectedIndex = -1

                End If

                btnEvaluate.Enabled = True

            End If

        End Using

    End Sub

    Private Sub frmRenameSymbols_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        Try

            xlsApp = GetObject(, "Excel.Application")
            'MsgBox(excelApp.ActiveWorkbook.FullName())
            xlsBook = xlsApp.ActiveWorkbook
            tbox_Input.Text = Path.GetFileName(xlsBook.Name)

            cbox_Before.Enabled = True
            cbox_After.Enabled = True
            cbox_worksheet.Enabled = True
            chkbox_IgnoreHeader.Enabled = True
            chkbox_ReadAllSheets.Enabled = True
            lbl_worksheet.Enabled = True
            lblRefDes.Enabled = True
            lblPN.Enabled = True
            btnEvaluate.Enabled = True

            cbox_Before.SelectedIndex = -1
            cbox_After.SelectedIndex = -1

            For Each worksheet As Worksheet In xlsBook.Worksheets
                cbox_worksheet.Items.Add(worksheet.Name)
            Next

        Catch ex As Exception

        End Try

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eAddPart, AddressOf AddPart
        AddHandler eReadComplete, AddressOf ReadComplete

    End Sub

    Private Sub btnProcess_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProcess.Click

        Dim dicSymbolsProblem As New Dictionary(Of String, String)
        Dim sSymDirectory As String = frmMain.librarydata.LibPath & "\SymbolLibs\"
        'frmMain.swLog = My.Computer.FileSystem.OpenTextFileWriter(frmmain.librarydata.LogPath & "Symbol Rename.log", True, Encoding.ASCII)  'Create Config File swOutput Stream

        WaitGif.Enabled = True
        Panel2.Enabled = False
        pl_Process.Enabled = False
        dgv_Symbols.Enabled = False

        If File.Exists(frmMain.librarydata.LogPath & "Symbol Rename Problems.log") Then
            File.Delete(frmMain.librarydata.LogPath & "Symbol Rename Problems.log")
        End If

        If File.Exists(frmMain.libDoc.Path & "\Work\Heal PDB Diagnosis.xml.xml") Then
            File.Delete(frmMain.libDoc.Path & "\Work\Heal PDB Diagnosis.xml.xml")
        End If

        AddHandler eRenameComplete, AddressOf RenameComplete
        Dim t_Rename As Thread = New Threading.Thread(AddressOf RenameSymbols)
        t_Rename.IsBackground = True
        t_Rename.Start()

    End Sub

    Private Sub ReadComplete()
        If Me.InvokeRequired Then

            Dim d As New dReadComplete(AddressOf ReadComplete)
            Me.Invoke(d)
        Else

            UpdateStatus("Analyzing results...")
            Refresh()

            Dim iToRename As Integer = 0
            Dim iDup As Integer = 0
            Dim iNoChange As Integer = 0
            Dim iMissing As Integer = 0

            Dim dic_Duplicates As New Dictionary(Of String, List(Of String))
            Dim dic_Missing As New Dictionary(Of String, String)
            Dim dic_TempDictionary As New Dictionary(Of String, String)

            For Each kvp As KeyValuePair(Of String, String) In dicSymbolRename

                dgv_Symbols.Rows.Add(kvp.Key, kvp.Value)

                If Not String.Compare(kvp.Key, kvp.Value, StringComparison.OrdinalIgnoreCase) = 0 Then
                    If Not frmMain.librarydata.SymbolList.ContainsKey(kvp.Key) Then
                        dgv_Symbols.Rows(dgv_Symbols.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Orange
                        iMissing += 1
                        dic_Missing.Add(kvp.Key, kvp.Value)
                    Else
                        If dic_TempDictionary.ContainsValue(kvp.Value) Then

                            dgv_Symbols.Rows(dgv_Symbols.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Red
                            iDup += 1

                            Dim l_Symbols As List(Of String)
                            If dic_Duplicates.ContainsKey(kvp.Value) Then
                                l_Symbols = dic_Duplicates.Item(kvp.Value)
                            Else
                                l_Symbols = New List(Of String)
                            End If

                            If Not l_Symbols.Contains(kvp.Key) Then
                                l_Symbols.Add(kvp.Key)
                                dic_Duplicates.Item(kvp.Value) = l_Symbols
                            End If

                        Else
                            dic_TempDictionary.Add(kvp.Key, kvp.Value)
                            iToRename += 1
                        End If
                    End If
                Else
                    dgv_Symbols.Rows(dgv_Symbols.Rows.Count - 1).DefaultCellStyle.BackColor = System.Drawing.Color.Yellow
                    iNoChange += 1
                End If

            Next

            dicSymbolRename = New Dictionary(Of String, String)(dic_TempDictionary)
            dic_TempDictionary = Nothing

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Rename - Bad Data.log", True)

                writer.WriteLine("Duplicate Parts:")
                For Each kvp_Duplicates As KeyValuePair(Of String, List(Of String)) In dic_Duplicates

                    writer.WriteLine(vbTab & kvp_Duplicates.Key)

                    For Each sPN As String In kvp_Duplicates.Value
                        writer.WriteLine(vbTab & vbTab & sPN)
                    Next

                Next

                writer.WriteLine()
                writer.WriteLine("Missing Symbols:")
                For Each kvp_Missing As KeyValuePair(Of String, String) In dic_Missing

                    writer.WriteLine(vbTab & kvp_Missing.Key & ", Renaming to: " & kvp_Missing.Value)

                Next

            End Using

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Rename - Input.log", True)
                For Each kvp As KeyValuePair(Of String, String) In dicSymbolRename
                    writer.WriteLine("Before: " & kvp.Key & ", After: " & kvp.Value)
                Next
            End Using

            lblToRename.Text = iToRename
            lblMissingSymbols.Text = iMissing
            lblNoChange.Text = iNoChange
            lblDupPN.Text = iDup

            If Not IsNothing(xlsApp) Then
                GC.Collect()
                GC.WaitForPendingFinalizers()
                xlsBook = Nothing
                Try
                    Marshal.FinalReleaseComObject(xlsBook)
                    xlsApp.Quit()
                    Marshal.FinalReleaseComObject(xlsApp)
                Catch ex As Exception
                End Try
            End If

            WaitGif.Enabled = False

            If iNoChange = 0 & iDup = 0 & iMissing = 0 Then
                frmMain.NotifyIcon.ShowBalloonTip(2000, "Symbol Rename:", "All items were found to be unique...", ToolTipIcon.Info)
                ts_status.Text = "All items are unique, please proceed..."

                pl_Process.Enabled = True
                btnEvaluate.Enabled = False
                cbox_After.Enabled = False
                cbox_Before.Enabled = False
                tbox_Input.Enabled = False
                btn_Browse_Excel.Enabled = False
                chkbox_ReadAllSheets.Enabled = False

            Else
                frmMain.NotifyIcon.ShowBalloonTip(2000, "Symbol Rename:", "All items were not found to be unique...", ToolTipIcon.Error)
                If chkbox_IgnoreDuplicatesMissing.Checked = True Then
                    MsgBox("Problems were found during analysis. These problems will be ignored.", MsgBoxStyle.OkOnly, "Info:")
                    pl_Process.Enabled = True
                Else
                    MsgBox("There were problems found. Please fix these problems before proceeding...", MsgBoxStyle.OkOnly, "Error:")
                    pl_Process.Enabled = False
                    btnEvaluate.Enabled = True
                End If

            End If

        End If

    End Sub

    Private Sub ReadInput()

        If (Path.GetExtension(tbox_Input.Text) = ".xls") Or (Path.GetExtension(tbox_Input.Text) = ".xlsx") Then

            RaiseEvent eUpdateStatus("Reading Excel file...")

            If chkbox_ReadAllSheets.Checked = True Then

                For Each xlsSheet As Excel.Worksheet In xlsBook.Worksheets

                    xlsSheet.Select()

                    readExcel(xlsSheet)

                Next
            Else
                readExcel(xlsBook.Worksheets(specificWorksheet))
            End If

            If bCloseExcel = True Then
                xlsApp.Quit()
            End If

        ElseIf Path.GetExtension(tbox_Input.Text) = ".txt" Then

            Dim arFile As String() = File.ReadAllLines(tbox_Input.Text)
            Dim arFileLength As Integer = arFile.Length

            Dim i As Integer

            i = 1

            For Each Line In arFile

                Dim linesplit As String()

                If Line.Contains(",") Then
                    linesplit = Regex.Split(Line, ",")
                ElseIf Line.Contains(";") Then
                    linesplit = Regex.Split(Line, ";")
                Else
                    linesplit = Regex.Split(Line, "\s+")
                End If

                If linesplit.Length > 1 Then

                    Dim sBefore As String = linesplit(0).Trim()
                    Dim sAfter As String = linesplit(1).Trim()

                    If Not dicSymbolRename.ContainsKey(sBefore) Then
                        If Not sAfter.Contains(",") Then

                            dicSymbolRename.Add(sBefore, sAfter)

                        End If
                    End If

                End If

                RaiseEvent eUpdateStatus("Reading row: " & i)
                i += 1

            Next

        End If

        RaiseEvent eReadComplete()

    End Sub

    Private Sub btnEvaluate_Click(sender As System.Object, e As System.EventArgs) Handles btnEvaluate.Click

        Dim ext As String = Path.GetExtension(tbox_Input.Text)

        If (ext = ".xls" Or ext = ".xlsx") And (cbox_Before.SelectedIndex = -1 Or cbox_After.SelectedIndex = -1) Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Rename Symbols:", "Please select a before and after column before proceeding.", ToolTipIcon.Error)
            MessageBox.Show("Please select a before and after column before proceeding.")
            Exit Sub
        End If

        WaitGif.Enabled = True

        dicSymbolRename.Clear()

        sColBefore = cbox_Before.Text
        sColAfter = cbox_After.Text

        cbox_After.Enabled = False
        cbox_Before.Enabled = False
        chkbox_IgnoreHeader.Enabled = False
        btnEvaluate.Enabled = False

        pl_Process.Enabled = False

        frmMain.NotifyIcon.ShowBalloonTip(2000, "Rename Symbols:", "Reading input file...", ToolTipIcon.Info)
        ts_status.Text = "Reading input file..."

        Dim t_Update As Thread = New Threading.Thread(AddressOf ReadInput)
        t_Update.IsBackground = True
        t_Update.Start()
    End Sub

    Private Sub RenameSymbolsComplete(Partition As String, DuplicateSymbols As List(Of String))
        If DuplicateSymbols.Count > 0 Then
            dicProblems.Add(Partition, DuplicateSymbols)
        End If
    End Sub

    Private Sub RenameSymbols()

        For Each kvp As KeyValuePair(Of String, String) In dicSymbolRename

            Using writer As StreamWriter = New StreamWriter(frmMain.libDoc.Path & "\SymbolLibs\ALE_Rename_Symbols.txt", True, System.Text.Encoding.ASCII)
                writer.WriteLine(kvp.Key & vbTab & kvp.Value)
            End Using

        Next

        Dim i As Integer = 0

        Dim processThreads(frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL).Count) As Thread
        Dim readThreads(frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL).Count) As Thread
        For Each lmPartition As LibraryManager.IMGCLMPartition In frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL)
            Dim cProcessSym As Symbols = New Symbols()
            AddHandler cProcessSym.RenameSymbolsComplete, AddressOf RenameSymbolsComplete
            AddHandler cProcessSym.SymbolCount, AddressOf SymbolCount
            AddHandler cProcessSym.SymbolFinish, AddressOf FinishSymbol

            cProcessSym.dicSymbolRename = dicSymbolRename
            cProcessSym.libDoc = frmMain.libDoc

            processThreads(i) = New Thread(AddressOf cProcessSym.RenameSym)
            processThreads(i).IsBackground = True
            processThreads(i).Start(lmPartition.Name)

            readThreads(i) = New Thread(AddressOf GetAssociatedParts)
            readThreads(i).IsBackground = True
            readThreads(i).Start(lmPartition)

            i += 1

        Next

        For i = 0 To frmMain.libDoc.Partitions(LibraryManager.MGCLMObjectType.kSYMBOL).Count - 1
            processThreads(i).Join()
            readThreads(i).Join()
        Next

        RaiseEvent eUpdateStatus("Creating XML Part file...")

        Dim xmlSettings As New Xml.XmlWriterSettings
        xmlSettings.Indent = True
        xmlSettings.IndentChars = vbTab
        xmlSettings.NewLineChars = vbNewLine
        xmlSettings.NewLineHandling = Xml.NewLineHandling.Replace

        Using xmlWriter As Xml.XmlWriter = Xml.XmlWriter.Create(frmMain.libDoc.Path & "\Work\Rename_Symbols_Associated_Parts.xml", xmlSettings)
            xmlWriter.WriteStartDocument()
            xmlWriter.WriteStartElement("AssociatedParts")
            For Each kvp As KeyValuePair(Of String, List(Of String)) In dicParts

                xmlWriter.WriteStartElement("Partition")
                xmlWriter.WriteStartAttribute("Name")
                xmlWriter.WriteString(kvp.Key)
                xmlWriter.WriteEndAttribute()
                For Each sPart As String In kvp.Value

                    xmlWriter.WriteStartElement("Part")
                    xmlWriter.WriteString(sPart)
                    xmlWriter.WriteEndElement()

                Next

                xmlWriter.WriteEndElement()

            Next

            xmlWriter.WriteEndElement()
            xmlWriter.WriteEndDocument()

        End Using

        RaiseEvent eRenameComplete()

    End Sub

    Private Sub RenameComplete()

        If Me.InvokeRequired Then

            Dim d As New dReadComplete(AddressOf RenameComplete)
            Me.Invoke(d)
        Else

            If dicProblems.Count > 0 Then

                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Rename Problems.log", True, System.Text.Encoding.ASCII)
                    writer.WriteLine("The following symbols were found to be duplicates:")
                End Using

                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Rename Problems.log", True, System.Text.Encoding.ASCII)
                    writer.WriteLine()
                End Using

                For Each kvp As KeyValuePair(Of String, List(Of String)) In dicProblems

                    Dim sPartition As String = kvp.Key
                    Dim lSymbols As List(Of String) = kvp.Value

                    If lSymbols.Count > 0 Then

                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Rename Problems.log", True, System.Text.Encoding.ASCII)
                            writer.WriteLine("Partition:" & sPartition)
                        End Using

                        For Each symbol In lSymbols
                            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Rename Problems.log", True, System.Text.Encoding.ASCII)
                                writer.WriteLine(vbTab & symbol)
                            End Using
                        Next

                        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Rename Problems.log", True, System.Text.Encoding.ASCII)
                            writer.WriteLine()
                        End Using

                    End If

                Next

            End If

            WaitGif.Enabled = False
            ts_status.Text = "Rename process finished..."
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Process Complete:", "Rename Symbols", ToolTipIcon.Info)

            If dicProblems.Count > 0 Then

                Dim reply As DialogResult = MessageBox.Show("Some symbols were found to be duplicates." & Environment.NewLine & Environment.NewLine & "Would you like to view this log file?", "Finished", MessageBoxButtons.YesNo, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Symbol Rename Problems")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Symbol Rename Problems.log")
                End If

            End If

            frmMain.tsm_Symbol.Enabled = False
            frmMain.tsm_PDB.Enabled = False
            frmMain.tsm_LibraryIntegrity.Enabled = False

            If chkbox_HealPDB.Checked = False Then

                File.Delete(frmMain.libDoc.Path & "\SymbolLibs\ALE_Rename_Symbols.txt")
                File.Delete(frmMain.libDoc.Path & "\Work\Rename_Symbols_Associated_Parts.xml")

            Else

                Dim Now As DateTime = DateTime.Now

                Dim sNow As String = Now.ToLongTimeString

                sNow = sNow.Replace(" ", "_")
                sNow = sNow.Replace(":", "_")

                If File.Exists(frmMain.libDoc.Path & "\SysIndex.cbf") Then
                    Dim sPathBefore As String = frmMain.libDoc.Path & "\SysIndex.cbf"
                    Dim sPathAfter As String = frmMain.libDoc.Path & "\SysIndex.cbf_" & sNow

                    Microsoft.VisualBasic.Rename(sPathBefore, sPathAfter)

                End If

                If MessageBox.Show("Library Manager needs to restart to fix the symbol names in the PDB." & Environment.NewLine & Environment.NewLine & "Press OK to close Library Manager (and ALE) or cancel close it later.", "Restart", MessageBoxButtons.OKCancel,
                Nothing, MessageBoxDefaultButton.Button1) = DialogResult.OK Then

                    If DialogResult.OK Then

                        bRestartLib = True

                        frmMain.librarydata.LibPath = frmMain.libDoc.Path
                        frmMain.libDoc.Close()
                        frmMain.libDoc = Nothing
                        frmMain.libApp.Quit()
                        frmMain.libApp = Nothing
                        frmMain.tsm_OpenExplorer.Enabled = False
                        frmMain.tsm_Cell.Enabled = False
                        frmMain.tsm_Other.Enabled = False
                        frmMain.Close()

                    End If

                End If

            End If

        End If

    End Sub

    Private Sub SymbolCount(ByVal PartitionSymbolCount As Integer)

        SyncLock StatusLock
            iSymbolCount += PartitionSymbolCount
            RaiseEvent eUpdateStatus("Symbols left to process: " & iSymbolCount)
        End SyncLock

    End Sub

    Private Sub FinishSymbol()

        SyncLock StatusLock
            iSymbolCount -= 1
            RaiseEvent eUpdateStatus("Symbols left to process: " & iSymbolCount)
        End SyncLock
    End Sub

    Private Sub GetAssociatedParts(ByVal lmPartition As LibraryManager.IMGCLMPartition)

        For Each lmSymbol As LibraryManager.IMGCLMSymbol In lmPartition

            If dicSymbolRename.ContainsKey(lmSymbol.Name) Then

                For Each lmPart As LibraryManager.IMGCLMPart In lmSymbol.AssociatedParts

                    RaiseEvent eAddPart(lmPart.PartitionName, lmPart.Name)

                Next

            End If

        Next

    End Sub

    Private Sub AddPart(Partition As String, Part As String)

        SyncLock PartLock

            Dim l_Parts As List(Of String)

            If dicParts.ContainsKey(Partition) Then
                l_Parts = dicParts.Item(Partition)
            Else
                l_Parts = New List(Of String)
            End If

            If Not l_Parts.Contains(Part) Then l_Parts.Add(Part)

            dicParts.Item(Partition) = l_Parts

        End SyncLock

    End Sub

    Private Sub frmRenameSymbols_HelpButtonClicked(sender As System.Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.HelpButtonClicked

        MessageBox.Show("Provide help here...")

    End Sub

    Private Sub readExcel(xlsSheet As Excel.Worksheet)
        Dim i As Integer
        i = 1
        If chkbox_IgnoreHeader.Checked Then i = 2

        Do While Not IsNothing(xlsSheet.Range(sColBefore & i).Value)

            Dim sBefore As String
            Dim sAfter As String

            sBefore = xlsSheet.Range(sColBefore & i).Value

            sBefore = sBefore.Trim()

            sAfter = xlsSheet.Range(sColAfter & i).Value

            If Not String.IsNullOrEmpty(sAfter) Then
                sAfter = sAfter.Trim()

                If Not dicSymbolRename.ContainsKey(sBefore) Then
                    If Not sAfter.Contains(",") Then

                        dicSymbolRename.Add(sBefore, sAfter)

                    End If
                End If
            End If

            i += 1

            RaiseEvent eUpdateStatus("Reading " & xlsSheet.Name & " row: " & i)

        Loop

    End Sub

    Private Sub chkbox_ReadAllSheets_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_ReadAllSheets.CheckedChanged
        If chkbox_ReadAllSheets.Checked = True Then
            lbl_worksheet.Enabled = False
            cbox_worksheet.Enabled = False
        Else
            lbl_worksheet.Enabled = True
            cbox_worksheet.Enabled = True
        End If
    End Sub

    Private Sub cbox_worksheet_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cbox_worksheet.SelectedIndexChanged
        specificWorksheet = cbox_worksheet.SelectedItem
    End Sub
End Class