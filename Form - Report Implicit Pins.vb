Imports System.IO
Imports System.Threading
Imports System.Text

Public Class frmCheckImplicitPins

#Region "Private Fields"

    'Arraylist
    Dim alModifyParts As New ArrayList

    'Dictionary
    Dim dicPartScanResults As New Dictionary(Of String, ArrayList)

    'Other
    Private lvwColumnSorter As ListViewColumnSorter

    'String
    Dim sSymDirectory As String

#End Region

#Region "Public Delegates"

    Delegate Sub d_ReadComplete()

    Delegate Sub d_UpdateComplete()

    Delegate Sub d_UpdateStatus(ByVal status As String)

#End Region

#Region "Public Events"

    Event eReadComplete()

    Event eUpdateComplete()

    'Events
    Event eUpdateStatus(status As String)

#End Region

#Region "Private Methods"

    Private Sub btnMarkParts_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMarkParts.Click

        Dim reply As DialogResult
        If rb_Ext.Checked = True Then
            reply = MessageBox.Show("All parts will be marked with an extension of " & tbox_PartTag.Text & Environment.NewLine & Environment.NewLine & "Example: 12345" & tbox_PartTag.Text, "Continue:",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
        Else
            reply = MessageBox.Show("All parts will be marked with an prefix of " & tbox_PartTag.Text & Environment.NewLine & Environment.NewLine & "Example: " & tbox_PartTag.Text & "12345", "Continue:",
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)
        End If

        If reply = DialogResult.Yes Then

            UpdateStatus("Tagging Implicit Parts")
            WaitGif.Enabled = True

            Dim t_Build As Thread = New Threading.Thread(AddressOf UpdatePDB)
            t_Build.IsBackground = True
            t_Build.Start()

        End If

    End Sub

    Private Sub btnScan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnScan.Click

        dicPartScanResults.Clear()
        alModifyParts.Clear()

        UpdateStatus("Looking for Implicit Pins")
        WaitGif.Enabled = True

        If File.Exists(Environment.NewLine & frmMain.librarydata.LogPath & "Report - PDB Implicit Pins.log") Then

            File.Delete(Environment.NewLine & frmMain.librarydata.LogPath & "Report - PDB Implicit Pins.log")

        End If

        Dim t_Build As Thread = New Threading.Thread(AddressOf ScanPDB)
        t_Build.IsBackground = True
        t_Build.Start()

    End Sub

    Private Sub chkbox_Filter_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

        UpdateListview()

    End Sub

    Private Sub chklistbox_PDBPartitions_ItemCheck(sender As Object, e As ItemCheckEventArgs) Handles chklistbox_PDBPartitions.ItemCheck
        If (chklistbox_PDBPartitions.CheckedItems.Count > 1) Then
            Panel2.Visible = True
        ElseIf (chklistbox_PDBPartitions.CheckedItems.Count = 1 And e.CurrentValue = CheckState.Checked) Then
            Panel2.Visible = False
        ElseIf (chklistbox_PDBPartitions.CheckedItems.Count = 0 And e.CurrentValue = CheckState.Unchecked) Then
            Panel2.Visible = True
        End If
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eReadComplete, AddressOf ReadComplete
        AddHandler eUpdateComplete, AddressOf UpdateComplete

        Me.Location = New System.Drawing.Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        For Each sPartition As String In frmMain.librarydata.PartsByPartition.Keys

            chklistbox_PDBPartitions.Items.Add(sPartition, True)

        Next

        lvwColumnSorter = New ListViewColumnSorter()
        Me.lv_SymInfo.ListViewItemSorter = lvwColumnSorter

    End Sub

    Private Sub lv_PartInfo_Click(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ColumnClickEventArgs) Handles lv_SymInfo.ColumnClick

        ' Determine if the clicked column is already the column that is
        ' being sorted.
        If (e.Column = lvwColumnSorter.SortColumn) Then
            ' Reverse the current sort direction for this column.
            If (lvwColumnSorter.Order = SortOrder.Ascending) Then
                lvwColumnSorter.Order = SortOrder.Descending
            Else
                lvwColumnSorter.Order = SortOrder.Ascending
            End If
        Else
            ' Set the column number that is to be sorted; default to ascending.
            lvwColumnSorter.SortColumn = e.Column
            lvwColumnSorter.Order = SortOrder.Ascending
        End If

        ' Perform the sort with these new sort options.
        Me.lv_SymInfo.Sort()

    End Sub

    Private Sub rb_Ext_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_Ext.CheckedChanged

        rb_Prefix.Checked = False

    End Sub

    Private Sub rb_Prefix_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rb_Prefix.CheckedChanged

        rb_Ext.Checked = False

    End Sub

    Private Sub ReadComplete()

        If Me.InvokeRequired Then

            Dim d As New d_ReadComplete(AddressOf ReadComplete)
            Me.Invoke(d)
        Else
            UpdateListview()

            WaitGif.Enabled = False

            UpdateStatus("Scan complete.")

            If dicPartScanResults.Count > 0 Then

                plMarkParts.Visible = True

                Dim reply As DialogResult = MessageBox.Show("Implicit pins were found. Would you like to view the results?", "Finished",
            MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Report - PDB Implicit Pins.log")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & Environment.NewLine & frmMain.librarydata.LogPath & "Report - PDB Implicit Pins.log")
                End If
            Else
                MessageBox.Show("Congratulation! No implicit pins were found in this library.")
            End If
        End If

    End Sub

    Private Sub ScanPDB()

        Dim sbLog As New StringBuilder

        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg = frmMain.libDoc.PartEditor

        Dim pedDoc As MGCPCBPartsEditor.PartsDB = pedApp.ActiveDatabaseEx

        For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions
            If chklistbox_PDBPartitions.CheckedItems.Contains(pdbPartition.Name) Then

                sbLog.AppendLine(pdbPartition.Name & ":")
                sbLog.AppendLine()

                Dim alParts As New ArrayList()

                For Each pdbPart As MGCPCBPartsEditor.Part In pdbPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll)

                    RaiseEvent eUpdateStatus(pdbPartition.Name & ":" & pdbPart.Number)

                    Dim bNoConnect As Boolean = False
                    Dim bNoRoute As Boolean = False
                    Dim bSupply As Boolean = False

                    If (pdbPart.PinMapping.NoConnect.Count > 0) Then
                        bNoConnect = True
                    End If

                    If (pdbPart.PinMapping.NoRoute.Count > 0) Or (pdbPart.PinMapping.Supply.Count > 0) Then
                        bSupply = True
                    End If

                    If (pdbPart.PinMapping.NoConnect.Count > 0) Or (pdbPart.PinMapping.NoRoute.Count > 0) Or (pdbPart.PinMapping.Supply.Count > 0) Then
                        Dim pdbPartInfo As Object = New Object() {pdbPart.Number, bNoConnect, bSupply}

                        alParts.Add(pdbPartInfo)
                        alModifyParts.Add(pdbPart.Number)

                        sbLog.AppendLine(vbTab & pdbPart.Number)

                        Dim oSymRef As MGCPCBPartsEditor.SymbolReference
                        Dim oSymRefs As MGCPCBPartsEditor.SymbolReferences

                        oSymRefs = pdbPart.SymbolReferences

                        For Each oSymRef In oSymRefs

                            sbLog.AppendLine(vbTab & vbTab & oSymRef.Name)

                        Next

                        sbLog.AppendLine()

                    End If

                Next

                If alParts.Count > 0 Then

                    dicPartScanResults.Add(pdbPartition.Name, alParts)
                Else
                    sbLog.AppendLine(vbTab & "None found")
                End If

                sbLog.AppendLine()

                pdbPartition = Nothing

            End If

        Next

        Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Report - PDB Implicit Pins.log", True, System.Text.Encoding.ASCII)
            writer.Write(sbLog.ToString())
        End Using

        pedDoc = Nothing
        pedApp = Nothing

        RaiseEvent eReadComplete()

    End Sub

    Private Sub UpdateComplete()
        If Me.InvokeRequired Then

            Dim d As New d_UpdateComplete(AddressOf UpdateComplete)
            Me.Invoke(d)
        Else
            plMarkParts.Visible = False
            MessageBox.Show("All implicit parts have been tagged with the prefix: " & tbox_PartTag.Text)
            UpdateStatus("Update Complete.")
        End If
    End Sub

    Private Sub UpdateListview()

        lv_SymInfo.Items.Clear()

        For Each kvp As KeyValuePair(Of String, ArrayList) In dicPartScanResults
            'Grab part number and part attributes:
            Dim sPartition As String = kvp.Key
            Dim alParts As ArrayList = kvp.Value

            For Each pdbPartInfo In alParts

                Dim lv As ListViewItem = lv_SymInfo.Items.Add(sPartition)
                lv.SubItems.Add(pdbPartInfo(0))

                If pdbPartInfo(1) = True Then
                    lv.SubItems.Add(pdbPartInfo(1))
                Else
                    lv.SubItems.Add(" ")
                End If

                If pdbPartInfo(2) = True Then
                    lv.SubItems.Add(pdbPartInfo(2))
                Else
                    lv.SubItems.Add(" ")
                End If

            Next

            If alParts.Count > 0 Then

                plMarkParts.Enabled = True

            End If

        Next
    End Sub

    Private Sub UpdatePDB()
        Dim pedApp As MGCPCBPartsEditor.PartsEditorDlg = frmMain.libDoc.PartEditor

        Dim pedDoc As MGCPCBPartsEditor.PartsDB = pedApp.ActiveDatabaseEx

        For Each pdbPartition As MGCPCBPartsEditor.Partition In pedDoc.Partitions

            If chklistbox_PDBPartitions.CheckedItems.Contains(pdbPartition.Name) Then
                Dim pdbParts As MGCPCBPartsEditor.Parts
                Dim pdbPart As MGCPCBPartsEditor.Part
                Dim alParts As New ArrayList()

                pdbParts = pdbPartition.Parts(MGCPCBPartsEditor.EPDBPartType.epdbPartAll)

                For Each pdbPart In pdbParts

                    If alModifyParts.Contains(pdbPart.Number) Then

                        If rb_Ext.Checked = True Then
                            pdbPart.Number = pdbPart.Number & tbox_PartTag.Text
                        Else
                            pdbPart.Number = tbox_PartTag.Text & pdbPart.Number
                        End If

                    End If

                Next

                frmMain.libDoc.PartEditor.SaveActiveDatabase()

                pdbPartition = Nothing

            End If

        Next

        pedApp.SaveActiveDatabase()
        pedDoc = Nothing
        pedApp = Nothing

        RaiseEvent eUpdateComplete()

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