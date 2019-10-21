Imports System.Threading
Imports System.Text
Imports System.IO
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.Text.RegularExpressions
Imports System.Drawing

Public Class frmModifyPartDes

    'String
    Dim Pattern As String
    Dim NewPattern As String
    Dim sProperty As String
    Dim sb_Report As New StringBuilder
    Dim sSheetName As String
    Dim PN_Col As String
    Dim Value_Col As String

    'dictionary
    Dim dicPropertiesAndParts As Dictionary(Of String, Dictionary(Of String, List(Of String)))
    Dim dicPartsToModify As Dictionary(Of String, Dictionary(Of String, String))
    Dim dicBadParts As New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)

    'Boolean
    Dim bReplaceValue As Boolean = False
    Dim bCloseExcel As Boolean = False
    Dim bAddProperty As Boolean = False

    'Object
    Dim xlsApp As Excel.Application
    Dim xlsBook As Excel.Workbook
    'Dim xlsSheet As Excel.Worksheet = Nothing
    Dim xmlDoc As Xml.XmlDocument

    'Events
    Event eUpdatePartsComplete()
    Event eCheckNode(ByVal node As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)
    Event eUpdateNodesFinished()
    Event eUpdateStatus(status As String)
    Event eReadComplete()
    Event eReadInputComplete()
    Event eUpdateFailed()

    'Delegates
    Delegate Sub d_ReadInputComplete()
    Delegate Sub ScanComplete()
    Delegate Sub dReadComplete()
    Delegate Sub d_CheckNode(ByVal value As TreeNode, ByVal checkstate As Boolean)
    Delegate Sub d_CheckNodesComplete()
    Delegate Sub d_UpdateFinished()
    Delegate Sub d_UpdateStatus(ByVal status As String)

    Private Sub ModifyProperty_Click(sender As System.Object, e As System.EventArgs) Handles btn_Modify.Click

        WaitGif.Enabled = True
        tv_Parts.Enabled = False
        SplitContainer1.Panel2.Enabled = False
        cbox_Modification.Text = "Replace"

        Dim dicPartsToProcess As New Dictionary(Of String, List(Of String))

        For Each oNode As TreeNode In tv_Parts.Nodes
            If oNode.Level = 0 Then

                Dim sPartition As String = oNode.Text

                Dim l_PartsToProcess As New List(Of String)

                For Each oChildNode As TreeNode In oNode.Nodes

                    If oChildNode.Checked = True Then
                        l_PartsToProcess.Add(oChildNode.Text)
                    End If

                Next

                If l_PartsToProcess.Count > 0 Then
                    dicPartsToProcess.Item(sPartition) = l_PartsToProcess
                End If

            End If

        Next

        If (dicPartsToProcess.Count = 0) Then
            MessageBox.Show("Please select some parts to modify before proceeding...")
            tv_Parts.Enabled = True
            SplitContainer1.Panel2.Enabled = True
            Exit Sub

        End If

        If File.Exists(frmMain.librarydata.LogPath & "\Modify Part Property Values.log") Then

            File.Delete(frmMain.librarydata.LogPath & "\Modify Part Property Values.log")

        End If

        Pattern = tbox_Pattern.Text
        NewPattern = tbox_NewPattern.Text

        sProperty = cbox_PartProperties.Text

        ts_Status.Text = "Modifying properties..."
        frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Part Property:", "Modifying properties.", ToolTipIcon.Info)

        Dim th_UpdateCount As Thread = New Thread(AddressOf ModifyProperties)
        th_UpdateCount.IsBackground = True
        th_UpdateCount.Start(dicPartsToProcess)

    End Sub

    Private Sub UpdateStatus(ByVal status As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateStatus(AddressOf UpdateStatus)
            Me.Invoke(d, New Object() {status})
        Else
            ts_Status.Text = status
        End If
    End Sub

    Private Sub frmModifyPartProperty_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eReadComplete, AddressOf ReadComplete
        AddHandler eReadInputComplete, AddressOf ReadInputComplete
        AddHandler eUpdatePartsComplete, AddressOf UpdateFinished

        btn_AllProperties.Checked = True

    End Sub

    Private Sub CheckNode(ByVal node As TreeNode, ByVal checkstate As Boolean)
        If Me.InvokeRequired Then

            Dim d As New d_CheckNode(AddressOf CheckNode)
            Me.Invoke(d, New Object() {node, checkstate})
        Else
            node.Checked = checkstate
        End If

    End Sub

    Private Sub CheckNodesComplete()
        If Me.InvokeRequired Then
            Dim d As New d_CheckNodesComplete(AddressOf CheckNodesComplete)
            Me.Invoke(d)
        Else

            AddHandler tv_Parts.BeforeCheck, AddressOf tv_Parts_BeforeCheck
            tv_Parts.Enabled = True
            SplitContainer1.Panel2.Enabled = True
        End If

    End Sub

    Private Sub tv_Parts_BeforeCheck(sender As System.Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tv_Parts.BeforeCheck

        tv_Parts.Enabled = False
        SplitContainer1.Panel2.Enabled = False
        Dim oCheckNodes As New NodeCount

        RemoveHandler tv_Parts.BeforeCheck, AddressOf tv_Parts_BeforeCheck

        Dim oPartCount As New CheckNode
        AddHandler oPartCount.CheckNode, AddressOf CheckNode
        AddHandler oPartCount.CheckNodesComplete, AddressOf CheckNodesComplete

        Dim th_CheckNodes As Thread = New Thread(AddressOf oPartCount.Update)
        th_CheckNodes.IsBackground = True
        th_CheckNodes.Start(e.Node)

    End Sub

    Private Sub UpdateFinished()
        If Me.InvokeRequired Then

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Modify Part Property Values.log", True)
                writer.WriteLine(sb_Report.ToString())
            End Using

            Dim d As New d_UpdateFinished(AddressOf UpdateFinished)
            Me.Invoke(d)
        Else
            ts_Status.Text = "Process complete."
            WaitGif.Enabled = False
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Part Properties:", "Process Complete.", ToolTipIcon.Info)
            tv_Parts.Enabled = True
            SplitContainer1.Panel2.Enabled = True

            If sb_Report.Length > 0 Then

                Dim replyCell As DialogResult = MessageBox.Show("Part property values have been modified. Would you like to view the results?", "Finished",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

                If replyCell = DialogResult.Yes Then
                    frmMain.OpenLogFile("Modify Part Property Values")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Modify Part Property Values.log")
                End If
            End If

        End If
    End Sub

    Private Sub ModifyProperties(ByVal dicPartsToProcess As Dictionary(Of String, List(Of String)))

        For Each kvp_Partition As KeyValuePair(Of String, List(Of String)) In dicPartsToProcess

            RaiseEvent eUpdateStatus("Creating instance of PDB editor...")

            'MentorGraphics
            Dim pedApp As New MGCPCBPartsEditor.PartsEditorDlg
            Dim pedDoc As MGCPCBPartsEditor.PartsDB

            Try
                pedDoc = pedApp.OpenDatabaseEx(frmMain.librarydata.LibPath, False)
            Catch ex As Exception
                RaiseEvent eUpdateFailed()
                pedApp.Quit()
                pedApp = Nothing
                Exit Sub
            End Try

            Dim pdbPartition As MGCPCBPartsEditor.Partition = pedDoc.Partitions(kvp_Partition.Key).Item(1)

            sb_Report.AppendLine(pdbPartition.Name)

            For Each sPart As String In kvp_Partition.Value

                sb_Report.AppendLine(vbTab & sPart)

                RaiseEvent eUpdateStatus("Modifying: " & pdbPartition.Name & ":" & sPart)

                Dim pdbPart As MGCPCBPartsEditor.Part = pdbPartition.Parts.Item(sPart)
                Dim bFoundProperty As Boolean = False

                For Each pdbProperty As MGCPCBPartsEditor.Property In pdbPart.Properties

                    If String.Compare(pdbProperty.Name, sProperty, False) = 0 Then

                        bFoundProperty = True

                        If chkbox_Regex.Checked Then
                            Dim r As Regex = New Regex(Pattern)
                            For Each Match As Match In r.Matches(pdbProperty.Value.ToString)
                                ModifyValue(pdbProperty.Value, Match.Value)
                            Next
                        ElseIf Not chkbox_Regex.Checked = False Then
                            pdbProperty.Value = ModifyValue(pdbProperty.Value, NewPattern)
                        End If
                    End If

                Next

                pdbPart = Nothing

                If bFoundProperty = False Then
                    sb_Report.AppendLine(vbTab & vbTab & "Property not found.")
                End If

            Next

            pdbPartition = Nothing

            pedApp.SaveActiveDatabase()
            pedApp.Quit()

        Next

        RaiseEvent eUpdatePartsComplete()
    End Sub

    Private Function ModifyValue(ByRef propertyvalue As String, ByVal modifyingvalue As String)

        'sb_Report.AppendLine(vbTab & vbTab & "Replacing value: " & pdbProperty.Value & ", with new value: " & NewPattern)
        'pdbProperty.Value = NewPattern
        'Else
        'sb_Report.Append(vbTab & vbTab & "Replacing value: " & pdbProperty.Value)
        'pdbProperty.Value.ToString.Replace(Pattern, NewPattern)
        'sb_Report.Append(", with new value: " & pdbProperty.Value)

        Dim s_Modification = tbox_NewPattern.Text

        Select Case cbox_Modification.Text
            Case "Append"
                propertyvalue = modifyingvalue & s_Modification
                sb_Report.AppendLine(vbTab & vbTab & "Appending value: " & s_Modification & ", to value: " & modifyingvalue)
            Case "Prepend"
                propertyvalue = s_Modification & modifyingvalue
                sb_Report.AppendLine(vbTab & vbTab & "Prepending value: " & s_Modification & ", to value: " & modifyingvalue)
            Case "Replace"
                propertyvalue = s_Modification
                sb_Report.AppendLine(vbTab & vbTab & "Replacing value: " & propertyvalue & ", with value: " & s_Modification)
            Case "Remove"
                propertyvalue = ""
                sb_Report.AppendLine(vbTab & vbTab & "Removing value : " & s_Modification & ", from value: " & modifyingvalue)
            Case Else
                propertyvalue = Nothing
        End Select
        Return propertyvalue
    End Function

    Private Sub tbValue_TextChanged(sender As System.Object, e As System.EventArgs) Handles tbox_Pattern.TextChanged

        If String.IsNullOrEmpty(tbox_Pattern.Text) Then
            btn_Modify.Enabled = False
        Else
            btn_Modify.Enabled = True
        End If

    End Sub

    Private Sub btn_ReportCurrentProperties_Click(sender As System.Object, e As System.EventArgs)

        dicPropertiesAndParts = New Dictionary(Of String, Dictionary(Of String, List(Of String)))

        tv_Parts.Enabled = False
        SplitContainer1.Panel2.Enabled = False

        WaitGif.Enabled = True

        ts_Status.Text = "Reading part properties..."

        Dim th_ReadProperties As Thread = New Thread(AddressOf ReadProperties)
        th_ReadProperties.IsBackground = True
        th_ReadProperties.Start()

    End Sub

    Private Sub ReadProperties()
        Dim pedApp As New MGCPCBPartsEditor.PartsEditorDlg
        Dim pedDoc As MGCPCBPartsEditor.PartsDB

        Try
            pedDoc = pedApp.OpenDatabaseEx(frmMain.librarydata.LibPath, True)
        Catch ex As Exception
            RaiseEvent eUpdateFailed()
            pedApp.Quit()
            pedApp = Nothing
            Exit Sub
        End Try

        For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions

            Dim l_Parts As New List(Of AAL.Part)

            For Each pdbPart As MGCPCBPartsEditor.Part In pdbPartition.Parts

                RaiseEvent eUpdateStatus("Analyzing: " & pdbPartition.Name & ":" & pdbPart.Number)

                Dim oPart As New AAL.Part

                oPart.Number = pdbPart.Number

                For Each pdbProperty As MGCPCBPartsEditor.Property In pdbPart.Properties

                    Dim dicPartition As Dictionary(Of String, List(Of String))
                    Dim PartitionParts As List(Of String)

                    If dicPropertiesAndParts.ContainsKey(pdbProperty.Name) Then
                        dicPartition = dicPropertiesAndParts.Item(pdbProperty.Name)

                        If dicPartition.ContainsKey(pdbPartition.Name) Then
                            PartitionParts = dicPartition.Item(pdbPartition.Name)
                        Else
                            PartitionParts = New List(Of String)
                        End If
                    Else
                        dicPartition = New Dictionary(Of String, List(Of String))
                        PartitionParts = New List(Of String)
                    End If

                    If Not PartitionParts.Contains(pdbPart.Number) Then
                        PartitionParts.Add(pdbPart.Number)
                    End If

                    dicPartition.Item(pdbPartition.Name) = PartitionParts
                    dicPropertiesAndParts.Item(pdbProperty.Name) = dicPartition

                    oPart.Properties.Add(pdbProperty.Name, pdbProperty.Value.ToString())

                Next

                l_Parts.Add(oPart)

            Next

        Next

        pedApp.Quit()

        RaiseEvent eReadComplete()

    End Sub

    Private Sub ReadComplete()

        If Me.InvokeRequired Then

            Dim d As New dReadComplete(AddressOf ReadComplete)
            Me.Invoke(d)
        Else

            tv_Parts.Nodes.Clear()

            cbox_PartProperties.SelectedIndex = 0

            tv_Parts.Sort()

            cbox_PartProperties.DataSource = Nothing
            cbox_PartProperties.DataSource = dicPropertiesAndParts.Keys.ToList()

            tv_Parts.Enabled = True
            SplitContainer1.Panel2.Enabled = True

            WaitGif.Enabled = False
            ts_Status.Text = "Read complete."

        End If

    End Sub

    Private Sub cbox_PartProperties_SelectedIndexChanged(sender As System.Object, e As System.EventArgs) Handles cbox_PartProperties.SelectedIndexChanged

        If Not IsNothing(dicPropertiesAndParts) Then

            tv_Parts.Nodes.Clear()

            If dicPropertiesAndParts.ContainsKey(cbox_PartProperties.Text) Then

                For Each kvp As KeyValuePair(Of String, List(Of String)) In dicPropertiesAndParts.Item(cbox_PartProperties.Text)
                    Dim nodeParent As TreeNode = tv_Parts.Nodes.Add(kvp.Key)

                    For Each sPart As String In kvp.Value
                        Dim nodePart As TreeNode = nodeParent.Nodes.Add(sPart)
                    Next
                Next

            End If
        End If

    End Sub

    Private Sub btn_Browse_Click(sender As Object, e As EventArgs) Handles btn_Browse.Click
        Using ofd As New OpenFileDialog
            ofd.Filter = "Excel Files|*.xlsx;*.xls| Text File|*.txt"
            '            ofd.Filter = "All files (*.*)|*.*"
            ofd.Title = "Select File"
            If ofd.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                If (Path.GetExtension(ofd.FileName) = ".xls" Or Path.GetExtension(ofd.FileName) = ".xlsx") Then

                    bCloseExcel = True

                    pl_Excel.Enabled = True

                    ts_Status.Text = "Opening spreadsheet..."

                    Me.Refresh()

                    xlsApp = New Excel.Application

                    xlsApp.Visible = True

                    xlsApp.Workbooks.Open(ofd.FileName)

                    xlsBook = xlsApp.ActiveWorkbook

                    For Each sheet In xlsBook.Worksheets

                        cboxActiveSheet.Items.Add(sheet.name)

                    Next

                    If xlsBook.Worksheets.Count = 1 Then

                        cboxActiveSheet.SelectedIndex = 0
                    Else

                        cboxActiveSheet.SelectedIndex = -1

                    End If

                End If

                tbox_Input.Text = ofd.FileName

                btnRead.Enabled = True

            End If

            frmMain.BringToFront()

        End Using
    End Sub

    Private Sub btnRead_Click(sender As Object, e As EventArgs) Handles btnRead.Click
        Dim ext As String = Path.GetExtension(tbox_Input.Text)

        If (ext = ".xls" Or ext = ".xlsx") And (cbox_PN.SelectedIndex = -1 And cbox_Cell.SelectedIndex = -1) Then
            frmMain.NotifyIcon.ShowBalloonTip(2000, "Add Cell(s):", "Please select a part number and cell column before proceeding...", ToolTipIcon.Error)
            MessageBox.Show("Please select a part number and cell column before proceeding...")
            Exit Sub
        End If

        If File.Exists(frmMain.librarydata.LogPath & "Add Cell - Bad Data.log") Then

            File.Delete(frmMain.librarydata.LogPath & "Add Cell - Bad Data.log")

        End If

        pl_FileInput.Enabled = False
        WaitGif.Enabled = True

        tv_Parts.Nodes.Clear()

        Dim t_Update As Thread

        dicPartsToModify = New Dictionary(Of String, Dictionary(Of String, String))

        If Not ext = ".txt" Then
            sSheetName = cboxActiveSheet.Text
            PN_Col = cbox_PN.Text
            Value_Col = cbox_Cell.Text

            ts_Status.Text = "Reading Excel Spreadsheet..."
            t_Update = New Threading.Thread(AddressOf ReadWorkbook)
        Else
            ts_Status.Text = "Reading Text File..."

            t_Update = New Threading.Thread(AddressOf ReadTextFile)
        End If

        t_Update.IsBackground = True
        t_Update.Start()
    End Sub

    Private Sub ReadWorkbook()
        xlsBook = xlsApp.ActiveWorkbook

        If chkbox_ReadAllSheets.Checked = True Then

            For Each xlsSheet As Excel.Worksheet In xlsBook.Worksheets

                xlsSheet.Select()

                ReadExcel(xlsSheet)

            Next
        Else

            For Each xlsSheet As Excel.Worksheet In xlsBook.Worksheets

                xlsSheet.Select()

                If xlsSheet.Name = sSheetName Then
                    ReadExcel(xlsSheet)
                    Exit For
                End If

            Next

        End If

        GC.Collect()
        GC.WaitForPendingFinalizers()

        If bCloseExcel = True Then
            xlsBook.Close()
            Marshal.FinalReleaseComObject(xlsBook)

            xlsApp.Quit()
            Marshal.FinalReleaseComObject(xlsApp)
        End If

        xlsBook = Nothing
        xlsApp = Nothing

        RaiseEvent eReadInputComplete()
    End Sub

    Private Sub ReadTextFile()
        Dim arFile As String() = File.ReadAllLines(tbox_Input.Text)

        Dim line As String
        For Each line In arFile
            Dim linesplit As String()
            Dim bProblem As Boolean = False

            If line.Contains(";") Then
                linesplit = Regex.Split(line, ";")
            Else
                linesplit = Regex.Split(line, "\s+")
            End If

            If linesplit.Length > 1 Then

                Dim sPN As String = linesplit(0)
                Dim sValue As String = linesplit(1)

                Dim sPartition As String

                If frmMain.librarydata.PartList.ContainsKey(sPN.Trim) Then
                    sPartition = frmMain.librarydata.PartList.Item(sPN)
                Else
                    dicBadParts.Item(sPN) = "Part Number does not exist in library."
                    Continue For
                End If

                Dim dicParts As Dictionary(Of String, String)

                If dicPartsToModify.ContainsKey(sPartition) Then
                    dicParts = dicPartsToModify.Item(sPartition)
                Else
                    dicParts = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                End If

                dicParts.Item(sPN) = sValue

            End If

        Next

        RaiseEvent eReadInputComplete()
    End Sub

    Private Sub ReadExcel(xlsSheet As Excel.Worksheet)

        Dim i As Integer

        If chkbox_IgnoreHeader.Checked = True Then
            i = 2
        Else
            i = 1
        End If

        Do While Not IsNothing(xlsSheet.Range(PN_Col & i).Value)

            RaiseEvent eUpdateStatus("Reading " & xlsSheet.Name & " row: " & i)

            Dim sPartition As String
            Dim bProblem As Boolean = False
            Dim sPN As String = xlsSheet.Range(PN_Col & i).Value

            If Not frmMain.librarydata.PartList.ContainsKey(xlsSheet.Range(PN_Col & i).Value) Then

                dicBadParts.Item(xlsSheet.Range(PN_Col & i).Value) = "Part Number does not exist in library."

                bProblem = True

                i += 1
                Continue Do
            Else
                sPN = xlsSheet.Range(PN_Col & i).Value

                If frmMain.librarydata.PartList.ContainsKey(sPN.Trim) Then
                    sPartition = frmMain.librarydata.PartList.Item(sPN)
                Else
                    dicBadParts.Item(sPN) = "Part Number does not exist in library."
                    i += 1
                    Continue Do
                End If

            End If

            Dim xlsValue As String = xlsSheet.Range(Value_Col & i).Value

            If String.IsNullOrEmpty(xlsValue) Then

                dicBadParts.Item(sPN) = "No Value Defined."

                bProblem = True

                i += 1
                Continue Do
            Else

                Dim dicParts As Dictionary(Of String, String)

                If dicPartsToModify.ContainsKey(sPartition) Then
                    dicParts = dicPartsToModify.Item(sPartition)
                Else
                    dicParts = New Dictionary(Of String, String)(StringComparer.OrdinalIgnoreCase)
                End If

                Dim sValue As String = xlsSheet.Range(Value_Col & i).Value

                dicParts.Item(sPN) = sValue

                If dicParts.Count > 0 Then
                    dicPartsToModify.Item(sPartition) = dicParts
                End If

            End If

            i += 1

        Loop
    End Sub

    Private Sub btnProcess_Click(sender As Object, e As EventArgs) Handles btnProcess.Click
        WaitGif.Enabled = True
        pl_FileInput.Enabled = False

        If File.Exists(frmMain.librarydata.LogPath & "\Modify Part Property Values.log") Then

            File.Delete(frmMain.librarydata.LogPath & "\Modify Part Property Values.log")

        End If

        bAddProperty = chkbox_AddProperty.Checked
        sProperty = cbox_BatchProperties.Text

        ts_Status.Text = "Modifying properties..."
        frmMain.NotifyIcon.ShowBalloonTip(2000, "Modify Part Property:", "Modifying properties.", ToolTipIcon.Info)

        Dim th_UpdateCount As Thread = New Thread(AddressOf BatchModifyProperties)
        th_UpdateCount.IsBackground = True
        th_UpdateCount.Start()
    End Sub

    Private Sub ReadInputComplete()
        If Me.InvokeRequired Then

            Dim d As New d_ReadInputComplete(AddressOf ReadInputComplete)
            Me.Invoke(d)
        Else

            Dim iBadPartCount As Integer = 0

            If dicBadParts.Count > 0 Then

                For Each kvpParts As KeyValuePair(Of String, String) In dicBadParts

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Modify Part Properties - Bad Data.log", True, System.Text.Encoding.ASCII)
                        writer.WriteLine(kvpParts.Key & " - " & kvpParts.Value)
                    End Using

                    iBadPartCount += 1

                Next

                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Modify Part Properties - Bad Data.log", True, System.Text.Encoding.ASCII)
                    writer.WriteLine()
                    writer.WriteLine(iBadPartCount & " parts had issues during the update process.")
                End Using

            End If

            WaitGif.Enabled = False

            Dim iCount As Integer = 0

            For Each kvp_Partition As KeyValuePair(Of String, Dictionary(Of String, String)) In dicPartsToModify

                For Each kvp_Part As KeyValuePair(Of String, String) In kvp_Partition.Value

                    dgv_Parts.Rows.Add(kvp_Part.Key, kvp_Part.Value)
                Next

            Next

            tv_Parts.Sort()

            ts_Status.Text = "Read Complete: " & iCount & " parts to be modified."

            btnProcess.Enabled = True

            If dicBadParts.Count > 0 Then

                Dim replyCell As DialogResult = MessageBox.Show("File has been read, but " & dicBadParts.Keys.Count() & " parts might not be fully modified. Would you like to view the results?", "Finished",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If replyCell = DialogResult.Yes Then
                    frmMain.OpenLogFile("Modify Part Properties - Bad Data")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & frmMain.librarydata.LogPath & "Modify Part Properties - Bad Data.log")
                End If
            End If

            If dicPartsToModify.Count > 0 Then
                pl_Process.Enabled = True
            End If

        End If
    End Sub

    Private Sub BatchModifyProperties()
        For Each kvp_Partition As KeyValuePair(Of String, Dictionary(Of String, String)) In dicPartsToModify

            RaiseEvent eUpdateStatus("Creating instance of PDB editor...")

            'MentorGraphics
            Dim pedApp As New MGCPCBPartsEditor.PartsEditorDlg
            Dim pedDoc As MGCPCBPartsEditor.PartsDB

            Try
                pedDoc = pedApp.OpenDatabaseEx(frmMain.librarydata.LibPath, False)
            Catch ex As Exception
                RaiseEvent eUpdateFailed()
                pedApp.Quit()
                pedApp = Nothing
                Exit Sub
            End Try

            Dim pdbPartition As MGCPCBPartsEditor.Partition = pedDoc.Partitions(kvp_Partition.Key).Item(1)

            sb_Report.AppendLine(pdbPartition.Name)

            Dim dicParts As Dictionary(Of String, String) = kvp_Partition.Value

            For Each kvp As KeyValuePair(Of String, String) In dicParts

                sb_Report.AppendLine(vbTab & kvp.Key)

                Dim sPart As String = kvp.Key

                RaiseEvent eUpdateStatus("Modifying: " & pdbPartition.Name & ":" & sPart)

                Dim pdbPart As MGCPCBPartsEditor.Part = pdbPartition.Parts.Item(sPart)
                Dim bFoundProperty As Boolean = False

                For Each pdbProperty As MGCPCBPartsEditor.Property In pdbPart.Properties

                    If String.Compare(pdbProperty.Name, sProperty, False) = 0 Then

                        bFoundProperty = True

                        sb_Report.AppendLine(vbTab & vbTab & "Replacing value: " & pdbProperty.Value & ", with new value: " & kvp.Value)
                        pdbProperty.Value = kvp.Value

                    End If

                Next

                If bFoundProperty = False And bAddProperty = True Then
                    sb_Report.AppendLine(vbTab & vbTab & "Adding property " & sProperty & " with value of " & kvp.Value)
                    pdbPart.PutPropertyEx(sProperty, kvp.Value)
                ElseIf bFoundProperty = False Then
                    sb_Report.AppendLine(vbTab & vbTab & "Property not found.")

                End If

                pdbPart = Nothing

            Next

            pdbPartition = Nothing

            pedApp.SaveActiveDatabase()
            pedApp.Quit()

        Next

        RaiseEvent eUpdatePartsComplete()
    End Sub

    Private Sub chkbox_ReadAllSheets_CheckedChanged(sender As Object, e As EventArgs) Handles chkbox_ReadAllSheets.CheckedChanged

        If chkbox_ReadAllSheets.Checked = True Then
            lbl_Property.Enabled = False
            cboxActiveSheet.Enabled = False
            cboxActiveSheet.SelectedIndex = -1
        Else
            lbl_Property.Enabled = True
            cboxActiveSheet.Enabled = True
            cboxActiveSheet.SelectedIndex = 0
        End If
    End Sub

    Private Sub btn_UsedProperties_CheckedChanged(sender As Object, e As EventArgs) Handles btn_UsedProperties.CheckedChanged
        If btn_UsedProperties.Checked Then
            dicPropertiesAndParts = New Dictionary(Of String, Dictionary(Of String, List(Of String)))

            tv_Parts.Enabled = False
            SplitContainer1.Panel2.Enabled = False

            WaitGif.Enabled = True

            ts_Status.Text = "Reading part properties..."

            Dim th_ReadProperties As Thread = New Thread(AddressOf ReadProperties)
            th_ReadProperties.IsBackground = True
            th_ReadProperties.Start()
        End If
    End Sub

    Private Sub btn_AllProperties_CheckedChanged(sender As Object, e As EventArgs) Handles btn_AllProperties.CheckedChanged

        dicPropertiesAndParts = Nothing
        tv_Parts.Nodes.Clear()

        If btn_AllProperties.Checked Then

            For Each kvp As KeyValuePair(Of String, List(Of String)) In frmMain.librarydata.PartsByPartition

                Dim nodeParent As TreeNode = tv_Parts.Nodes.Add(kvp.Key)

                For Each sPart As String In kvp.Value
                    nodeParent.Nodes.Add(sPart)
                Next

            Next

            cbox_PartProperties.DataSource = frmMain.librarydata.PDBCommonProperties
            cbox_BatchProperties.DataSource = frmMain.librarydata.PDBCommonProperties

            tv_Parts.Sort()

        End If
    End Sub
End Class