Imports System.Text.RegularExpressions
Imports System.IO
Imports System.Text
Imports System.Threading
Imports System.Drawing

Public Class frmModSymbols

#Region "Public Fields + Properties + Events + Delegates + Enums"

    'Delegates
    Delegate Sub d_CheckNode(ByVal value As TreeNode, ByVal checkstate As Boolean)

    Delegate Sub d_CheckNodesComplete()

    Delegate Sub d_ReadComplete()

    Delegate Sub d_ReadPartitionComplete(Partition As String, UniqueSymProperties As Dictionary(Of String, AAL.SymbolProperty), Symbols As Dictionary(Of String, Object), SymPropswithTrailingSpaces As Dictionary(Of String, List(Of String)), UniquePinProperties As Dictionary(Of String, AAL.SymbolPinProperty), dic_NonCommonProperties As Dictionary(Of String, List(Of String)))

    Delegate Sub d_UpdateCount(ByVal status As String)

    Delegate Sub d_UpdateStatus(ByVal status As String)

    Event eCheckNode(ByVal node As TreeNode, ByVal check As Boolean)

    Event eProcessComplete()

    'Events
    Event eReadComplete()

    Event eUpdateNodeCheck()

    Event eUpdateStatus(status As String)

    Property LibDoc As LibraryManager.IMGCLMLibrary

    Enum PropertyType As Integer

        Symbol = 0
        Pin = 1

    End Enum

#End Region

#Region "Private Fields + Properties + Events + Delegates + Enums"

    'Arraylist
    Dim alSymPartitionToProcess As New ArrayList()

    Dim attributeStatus As String

    'Boolean
    Dim bAttsWithSpaces As Boolean = False

    Dim bEnableTimeMachine As Boolean = False

    Dim bReadSymsAtts As Boolean

    Dim countlock As New Object

    Dim dic_ModifiedPinProperties As New Dictionary(Of String, AAL.SymbolPinProperty)(StringComparer.OrdinalIgnoreCase)

    'Dictionary
    Dim dic_ModifiedSymbolProperties As New Dictionary(Of String, AAL.SymbolProperty)(StringComparer.OrdinalIgnoreCase)

    Dim dic_newPinProperties As New Dictionary(Of String, AAL.SymbolPinProperty)(StringComparer.OrdinalIgnoreCase)

    Dim dic_newProperties As New Dictionary(Of String, AAL.SymbolProperty)(StringComparer.OrdinalIgnoreCase)

    Dim dic_PinProperties As New Dictionary(Of String, AAL.SymbolPinProperty)(StringComparer.OrdinalIgnoreCase)

    Dim dic_PinPropertyNodes As New Dictionary(Of String, TreeNode)(StringComparer.OrdinalIgnoreCase)

    Dim dic_PropertyNodes As New Dictionary(Of String, TreeNode)(StringComparer.OrdinalIgnoreCase)

    Dim dic_SymProperties As New Dictionary(Of String, AAL.SymbolProperty)(StringComparer.OrdinalIgnoreCase)

    Dim dicPartitionSymbolsAtts As New Dictionary(Of String, Dictionary(Of String, ArrayList))(StringComparer.OrdinalIgnoreCase)

    Dim dicPinPropsbyPartition As New Dictionary(Of String, Dictionary(Of String, AAL.SymbolPinProperty))(StringComparer.OrdinalIgnoreCase)

    Dim dicSymAttsbyPartition As New Dictionary(Of String, Dictionary(Of String, AAL.SymbolProperty))(StringComparer.OrdinalIgnoreCase)

    Dim dicSymtoProcess As New Dictionary(Of String, List(Of String))(StringComparer.OrdinalIgnoreCase)

    'Integers
    Dim i_Count As Integer = 0

    Dim l_NonCommonSymPinProperties As New List(Of String)

    Dim l_NonCommonSymProperties As New List(Of String)

    Dim l_PinPropertiesToRemove As New List(Of String)

    Dim l_PinPropertiesToRemoveDefaultValue As New List(Of String)

    'List
    Dim l_PropertiesToRemove As New List(Of String)

    Dim l_PropertiesToRemoveDefaultValue As New List(Of String)

    Dim oCurrentNode As TreeNode

    Dim ProcessLog As StringBuilder

    Dim sb_SymbolAtts As New StringBuilder

    'Strings
    Dim sline, sCaf, sAttTimer As String

    Dim sSymDirectory As String

    'Other
    Dim sublock As New Object

#End Region

#Region "Public Methods"

    Sub Read_Attributes()

        dicSymAttsbyPartition.Clear()
        dicPinPropsbyPartition.Clear()
        dicSymtoProcess.Clear()
        dic_PinProperties.Clear()
        dic_SymProperties.Clear()

        l_NonCommonSymProperties = New List(Of String)
        l_NonCommonSymPinProperties = New List(Of String)

        bReadSymsAtts = False

        If My.Computer.FileSystem.FileExists(sCaf) Then

            My.Computer.FileSystem.DeleteFile(sCaf)

        End If

        Dim Root As New DirectoryInfo(Path.GetDirectoryName(frmMain.librarydata.LibPath) & "\SymbolLibs\")

        Dim Dirs As DirectoryInfo() = Root.GetDirectories("*.*")

        sSymDirectory = Path.GetDirectoryName(frmMain.librarydata.LibPath) & "\SymbolLibs\"

        Dim newThreads(alSymPartitionToProcess.Count) As Thread
        For i As Integer = 0 To alSymPartitionToProcess.Count - 1
            Dim ReadSymAtts As New Symbols()
            AddHandler ReadSymAtts.eReadComplete, AddressOf ReadPartitionComplete
            AddHandler ReadSymAtts.eUpdateCount, AddressOf UpdateCount
            ReadSymAtts.LibraryData = frmMain.librarydata
            ReadSymAtts.libDoc = frmMain.libDoc
            newThreads(i) = New Thread(AddressOf ReadSymAtts.ReadAttributes)
            newThreads(i).IsBackground = True
            newThreads(i).Start(alSymPartitionToProcess(i))
            i_Count += My.Computer.FileSystem.GetFiles(sSymDirectory & alSymPartitionToProcess(i) & "\sym\", FileIO.SearchOption.SearchTopLevelOnly).Count
            RaiseEvent eUpdateStatus("Symbols left to analyze: " & i_Count)
        Next

        For i As Integer = 0 To alSymPartitionToProcess.Count - 1
            newThreads(i).Join()
        Next

        RaiseEvent eReadComplete()

        'End If

    End Sub

#End Region

#Region "Private Methods"

    Private Function AddDefaultValue(treeNode As TreeNode) As TreeNode
        Dim oAction As TreeNode = Nothing

        For Each oNode As TreeNode In treeNode.Nodes
            If oNode.Name = "AddValue" Then
                oAction = oNode
                Exit For
            End If
        Next

        If IsNothing(oAction) Then
            oAction = New TreeNode()
            oAction.Name = "AddValue"
            oAction.Text = "Add Default Value"
        End If

        oAction.Nodes.Clear()

        Dim sPropertyName As String = InputBox("Provide a default value for property: " & treeNode.Text, "Add Default Value", "Default Value")

        If Not sPropertyName = "" Then
            oAction.Nodes.Add(sPropertyName)

            Return oAction
        End If

        Return Nothing
    End Function

    Private Sub btn_Process_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Process.Click

        Dim oDate As Date = dateChooser.Value

        ts_Status.Text = "Processing Properties..."

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "Symbol Property Modification.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "Symbol Property Modification.log")

        End If

        gb_symbols.Enabled = False
        gb_Options.Enabled = False
        btn_Process.Enabled = False
        dateChooser.Enabled = False
        chkbox_RemoveSpaces.Enabled = False
        WaitGif.Enabled = True
        bEnableTimeMachine = dateChooser.Checked

        ProcessLog = New StringBuilder()

        Dim t_ProcessProperties As Thread = New Threading.Thread(AddressOf Process_Properties)
        t_ProcessProperties.IsBackground = True
        t_ProcessProperties.Start()

    End Sub

    Private Sub btn_Read_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn_Read.Click

        tv_Properties.Nodes.Clear()
        ts_Status.Text = "Reading Properties..."

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "Symbol Properties.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "Symbol Properties.log")

        End If

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "Symbol Properties with Spaces.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "Symbol Properties with Spaces.log")

        End If

        gb_symbols.Enabled = False
        gb_Options.Enabled = False
        btn_Read.Enabled = False
        WaitGif.Enabled = True

        Dim t_ReadAttributes As Thread = New Threading.Thread(AddressOf Read_Attributes)
        t_ReadAttributes.IsBackground = True
        t_ReadAttributes.Start()

    End Sub

    Private Sub CheckAllNodes(bCheckNode As Boolean)
        Dim th_UpdateCount(tv_Symbols.Nodes.Count - 1) As Thread

        Dim i As Integer = 0

        For Each oNode As TreeNode In tv_Symbols.Nodes

            RaiseEvent eCheckNode(oNode, bCheckNode)

            Dim oPartCount As New CheckNode
            If bCheckNode = True Then
                alSymPartitionToProcess.Add(oNode.Text)
            Else
                alSymPartitionToProcess.Remove(oNode.Text)
            End If

            AddHandler oPartCount.CheckNode, AddressOf CheckNode
            th_UpdateCount(i) = New Thread(AddressOf oPartCount.Update)
            th_UpdateCount(i).IsBackground = True
            th_UpdateCount(i).Start(oNode)

            i += 1

        Next

        For iCount As Integer = 0 To th_UpdateCount.Length - 1
            th_UpdateCount(iCount).Join()
        Next

        RaiseEvent eUpdateNodeCheck()

        RemoveHandler eUpdateNodeCheck, AddressOf CheckNodesComplete
        RemoveHandler eCheckNode, AddressOf CheckNode

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

            Refresh_TreeView()

            AddHandler tv_Symbols.BeforeCheck, AddressOf tv_Symbols_BeforeCheck
            tv_Symbols.Enabled = True
            btn_Process.Enabled = True
            btn_Read.Enabled = True
            tv_Properties.Enabled = True
        End If

    End Sub

    Private Sub chkbox_RemoveSpaces_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkbox_RemoveSpaces.CheckedChanged

        RemoveHandler chkbox_RemoveSpaces.CheckedChanged, AddressOf chkbox_RemoveSpaces_CheckedChanged

        If MessageBox.Show("Enabling remove trailing spaces will delete any spaces from the end of an attribute. Would you like to continue?", "Remove Trailing Spaces?",
  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
            chkbox_RemoveSpaces.Checked = True
        Else
            chkbox_RemoveSpaces.Checked = False
        End If

        AddHandler chkbox_RemoveSpaces.CheckedChanged, AddressOf chkbox_RemoveSpaces_CheckedChanged

        Refresh_TreeView()

    End Sub

    Private Sub copyPinNode(sender As Object, e As EventArgs)

        Dim oNode As TreeNode

        If dic_PinPropertyNodes.TryGetValue(sender.text, oNode) Then

            Dim oClonedNode As TreeNode = oNode.Clone

            oClonedNode.Text = tv_Properties.SelectedNode.Text
            Dim i_Index As Integer = tv_Properties.SelectedNode.Index
            tv_PinProperties.SelectedNode.Remove()

            tv_PinProperties.Nodes.Insert(i_Index, oClonedNode)

            oClonedNode.Expand()

        End If

    End Sub

    Private Sub copyPropertyNode(sender As Object, e As EventArgs)

        Dim oNode As TreeNode

        If dic_PropertyNodes.TryGetValue(sender.text, oNode) Then

            Dim oClonedNode As TreeNode = oNode.Clone

            oClonedNode.Text = tv_Properties.SelectedNode.Text
            Dim i_Index As Integer = tv_Properties.SelectedNode.Index
            tv_Properties.SelectedNode.Remove()

            tv_Properties.Nodes.Insert(i_Index, oClonedNode)

            oClonedNode.Expand()

        End If

    End Sub

    Private Function EditNode(ByVal oEditProperty As frmEdit_Property_Info, ByVal treeNode As TreeNode, Optional ByVal Type As PropertyType = PropertyType.Symbol) As Object
        If Not IsNothing(treeNode) Then
            If Not treeNode.Level = 1 Then
                If treeNode.Nodes.Count > 0 Then
                    For Each oNode As TreeNode In treeNode.Nodes
                        If oNode.Name = "Edit" Then
                            For Each oChild As TreeNode In oNode.Nodes
                                Dim NodeSplit() As String = Split(oChild.Text, ": ")
                                Select Case NodeSplit(0)

                                    Case "Font Style"
                                        oEditProperty.cbox_Font.Text = NodeSplit(1)
                                        oEditProperty.argsInfo.Font = NodeSplit(1)
                                        oEditProperty.btn_FontClear.Visible = True
                                    Case "Font Size"
                                        oEditProperty.cbox_Size.Text = NodeSplit(1)
                                        oEditProperty.argsInfo.Size = NodeSplit(1)
                                        oEditProperty.btn_SizeClear.Visible = True
                                    Case "Alignment"
                                        oEditProperty.cbox_Alignment.Text = NodeSplit(1)
                                        oEditProperty.argsInfo.Alignment = NodeSplit(1)
                                        oEditProperty.btn_AlignmentClear.Visible = True
                                    Case "Color"
                                        If NodeSplit(1) = "Automatic" Then
                                            oEditProperty.chkbox_Automatic.Checked = True
                                            oEditProperty.argsInfo.AutomaticColor = True
                                        Else
                                            oEditProperty.btnColor.Text = NodeSplit(1)
                                            oEditProperty.btnColor.BackColor = oChild.ForeColor
                                            oEditProperty.argsInfo.Color = oChild.ForeColor
                                            oEditProperty.btn_ColorClear.Visible = True

                                        End If

                                    Case "Visibility"
                                        oEditProperty.cbox_Visibility.Text = NodeSplit(1)
                                        oEditProperty.argsInfo.Visibility = NodeSplit(1)
                                        oEditProperty.btn_VisibilityClear.Visible = True
                                End Select

                            Next
                            Exit For
                        End If
                    Next
                End If

                'oEditProperty.MdiParent = Me.MdiParent

                Dim results As DialogResult = oEditProperty.ShowDialog()

                If results = System.Windows.Forms.DialogResult.OK Then

                    Dim oProperty As Object
                    Dim b_foundProperty As Boolean = False

                    If Type = PropertyType.Pin Then

                        If dic_ModifiedPinProperties.TryGetValue(treeNode.Text, oProperty) Then
                            dic_ModifiedPinProperties.Remove(treeNode.Text)
                            b_foundProperty = True
                        Else
                            oProperty = New AAL.SymbolPinProperty
                        End If
                    Else
                        If dic_ModifiedSymbolProperties.TryGetValue(treeNode.Text, oProperty) Then
                            dic_ModifiedSymbolProperties.Remove(treeNode.Text)
                            b_foundProperty = True
                        Else
                            oProperty = New AAL.SymbolProperty
                        End If
                    End If

                    Dim oAction As TreeNode = Nothing

                    If b_foundProperty = True Then
                        For Each oNode As TreeNode In treeNode.Nodes
                            If oNode.Name = "Edit" Then
                                oAction = oNode
                                b_foundProperty = True
                                Exit For
                            End If
                        Next
                    End If

                    If IsNothing(oAction) Then
                        oAction = New TreeNode()
                        oAction.Name = "Edit"
                        oAction.Text = "Edit Attributes"
                    End If

                    oAction.Nodes.Clear()

                    If Not IsNothing(oEditProperty.argsInfo.Font) Then
                        Dim oFontFontTypeNode As New TreeNode
                        oFontFontTypeNode.Name = "Font"
                        oFontFontTypeNode.Text = "Font Style: " & oEditProperty.argsInfo.Font
                        oAction.Nodes.Add(oFontFontTypeNode)

                        If Type = PropertyType.Pin Then
                            oProperty.FontType = DirectCast([Enum].Parse(GetType(AAL.Font), oEditProperty.argsInfo.Font.Replace(" ", String.Empty)), AAL.Font)
                        Else
                            oProperty.FontType = DirectCast([Enum].Parse(GetType(AAL.Font), oEditProperty.argsInfo.Font.Replace(" ", String.Empty)), AAL.Font)
                        End If

                    End If

                    If Not oEditProperty.argsInfo.Size = 0 Then
                        Dim oFontSizeNode As New TreeNode
                        oFontSizeNode.Name = "Size"
                        oFontSizeNode.Text = "Font Size: " & oEditProperty.argsInfo.Size
                        oAction.Nodes.Add(oFontSizeNode)

                        oProperty.FontSize = oEditProperty.argsInfo.Size

                    End If

                    If Not IsNothing(oEditProperty.argsInfo.Alignment) Then
                        Dim oAlignmentNode As New TreeNode
                        oAlignmentNode.Name = "Alignment"
                        oAlignmentNode.Text = "Alignment: " & oEditProperty.argsInfo.Alignment
                        oAction.Nodes.Add(oAlignmentNode)

                        If Type = PropertyType.Pin Then
                            oProperty.Alignment = DirectCast([Enum].Parse(GetType(AAL.Justification), oEditProperty.argsInfo.Alignment.Replace(" ", String.Empty)), AAL.Justification)
                        Else
                            oProperty.Alignment = DirectCast([Enum].Parse(GetType(AAL.Justification), oEditProperty.argsInfo.Alignment.Replace(" ", String.Empty)), AAL.Justification)
                        End If

                    End If

                    If oEditProperty.argsInfo.AutomaticColor = True Then

                        Dim oColorNode As New TreeNode
                        oColorNode.Name = "Color"
                        oColorNode.Text = "Color: Automatic"
                        oAction.Nodes.Add(oColorNode)

                        oProperty.AutomaticColor = True

                    ElseIf Not String.IsNullOrEmpty(oEditProperty.btnColor.Text) Then
                        Dim oColor As Color = oEditProperty.argsInfo.Color

                        Dim oColorNode As New TreeNode
                        oColorNode.Name = "Color"
                        oColorNode.Text = "Color: R:" & oEditProperty.argsInfo.Color.R & " G:" & oEditProperty.argsInfo.Color.G & " B:" & oEditProperty.argsInfo.Color.B
                        oColorNode.ForeColor = oColor
                        oAction.Nodes.Add(oColorNode)

                        oProperty.Color = oEditProperty.argsInfo.Color

                    End If

                    If Not IsNothing(oEditProperty.argsInfo.Visibility) Then
                        Dim oVisibilityNode As New TreeNode
                        oVisibilityNode.Name = "Visibility"
                        oVisibilityNode.Text = "Visibility: " & oEditProperty.argsInfo.Visibility
                        oAction.Nodes.Add(oVisibilityNode)

                        If Type = PropertyType.Pin Then
                            oProperty.Display = DirectCast([Enum].Parse(GetType(AAL.Visibility), oEditProperty.argsInfo.Visibility.Replace(" ", String.Empty)), AAL.Visibility)
                        Else
                            oProperty.Display = DirectCast([Enum].Parse(GetType(AAL.Visibility), oEditProperty.argsInfo.Visibility.Replace(" ", String.Empty)), AAL.Visibility)
                        End If

                    End If

                    If b_foundProperty = False Then
                        treeNode.Nodes.Add(oAction)
                        treeNode.ExpandAll()
                    End If

                    oProperty.Name = treeNode.Text

                    If Type = PropertyType.Pin Then
                        dic_PinPropertyNodes.Remove(treeNode.Text)
                        dic_PinPropertyNodes.Add(treeNode.Text, treeNode)
                        dic_ModifiedPinProperties.Add(treeNode.Text, oProperty)
                    Else
                        dic_PropertyNodes.Remove(treeNode.Text)
                        dic_PropertyNodes.Add(treeNode.Text, treeNode)
                        dic_ModifiedSymbolProperties.Add(treeNode.Text, oProperty)
                    End If

                    Return oProperty
                Else
                    Return Nothing
                End If

            End If
        End If
    End Function

    Private Sub ModifyPropertiesComplete()
        If Me.InvokeRequired Then

            For Each sProperty As String In l_PropertiesToRemove
                If frmMain.librarydata.SymbolNoncommonProperties.Contains(sProperty) Then frmMain.librarydata.SymbolNoncommonProperties.Remove(sProperty)
            Next

            For Each sProperty As String In l_PinPropertiesToRemove
                If frmMain.librarydata.SymbolNoncommonProperties.Contains(sProperty) Then frmMain.librarydata.SymbolNoncommonProperties.Remove(sProperty)
            Next

            If ProcessLog.Length > 0 Then
                Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Property Modification.log", True)

                    writer.WriteLine(ProcessLog.ToString())
                    writer.WriteLine()

                End Using
            End If

            Dim d As New d_ReadComplete(AddressOf ModifyPropertiesComplete)
            Me.Invoke(d)
        Else

            ts_Status.Text = "Modify Properties Complete"

            WaitGif.Enabled = False

            If MessageBox.Show("Property modification has completed. Would you like to review the results?", "Property modification completed.",
  MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) = System.Windows.Forms.DialogResult.Yes Then
                frmMain.OpenLogFile("Symbol Property Modification")
            Else
                MessageBox.Show("For more information, please see:" & Environment.NewLine & Environment.NewLine & frmMain.librarydata.LogPath & "Symbol Property Modification.log")
            End If

            ResetForm()
            RemoveHandler eProcessComplete, AddressOf ModifyPropertiesComplete

        End If

    End Sub

    Private Sub ModSymbols_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New Point(Me.MdiParent.ClientSize.Width / 2 - Me.Width / 2, Me.MdiParent.ClientSize.Height / 2 - Me.Height / 2)
        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eReadComplete, AddressOf ReadAttsComplete
        AddHandler eProcessComplete, AddressOf ModifyPropertiesComplete

        frmMain.librarydata.LibPath = frmMain.librarydata.LibPath

        dateChooser.CustomFormat = "ddd, MMM dd, yyyy hh:mm:ss tt"

        sCaf = Path.GetDirectoryName(frmMain.librarydata.LibPath) & "\SymAttributes.caf"

        ResetForm()

    End Sub

    Private Function PrepareProperty(oTreeNode As TreeNode, oProperty As Object) As Object

        For Each oSubNode As TreeNode In oTreeNode.Nodes

            Select Case oSubNode.Name

                Case "Edit"

                    For Each oAttribute As TreeNode In oSubNode.Nodes

                        Select Case oAttribute.Name

                            Case "Font"

                                oProperty.Modifications.Add(AAL.PropertyMods.Font)

                                Dim Font() As String = oAttribute.Text.Split(":")

                                Dim sFont As String = Font(1).Trim

                                Select Case sFont
                                    Case "Fixed"
                                        oProperty.FontType = AAL.Font.Fixed
                                    Case "Gothic"
                                        oProperty.FontType = AAL.Font.Gothic
                                    Case "Kanji"
                                        oProperty.FontType = AAL.Font.Kanji
                                    Case "Old English"
                                        oProperty.FontType = AAL.Font.OldEnglish
                                    Case "Plot"
                                        oProperty.FontType = AAL.Font.Plot
                                    Case "Roman"
                                        oProperty.FontType = AAL.Font.Roman
                                    Case "Roman Bold"
                                        oProperty.FontType = AAL.Font.RomanBold
                                    Case "Roman Bold Italic"
                                        oProperty.FontType = AAL.Font.RomanBoldItalic
                                    Case "Roman Italic"
                                        oProperty.FontType = AAL.Font.RomanItalic
                                    Case "Sans Serif"
                                        oProperty.FontType = AAL.Font.SansSerif
                                    Case "Sans Serif Bold"
                                        oProperty.FontType = AAL.Font.SansSerifBold
                                    Case "Script"
                                        oProperty.FontType = AAL.Font.Script
                                    Case "Script Bold"
                                        oProperty.FontType = AAL.Font.ScriptBold
                                End Select

                            Case "Size"

                                oProperty.Modifications.Add(AAL.PropertyMods.FontSize)

                                Dim Size() As String = oAttribute.Text.Split(":")

                                Dim iSize As Integer = Size(1).Trim

                                oProperty.FontSize = iSize

                            Case "Alignment"

                                oProperty.Modifications.Add(AAL.PropertyMods.Justification)

                                Dim Alignment() As String = oAttribute.Text.Split(":")

                                Dim sAlignment As String = Alignment(1).Trim

                                Select Case sAlignment
                                    Case "Middle Center"
                                        oProperty.Alignment = AAL.Justification.MiddleCenter
                                    Case "Middle Left"
                                        oProperty.Alignment = AAL.Justification.MiddleLeft
                                    Case "Middle Right"
                                        oProperty.Alignment = AAL.Justification.MiddleRight
                                    Case "Upper Center"
                                        oProperty.Alignment = AAL.Justification.UpperCenter
                                    Case "Upper Left"
                                        oProperty.Alignment = AAL.Justification.UpperLeft
                                    Case "Upper Right"
                                        oProperty.Alignment = AAL.Justification.UpperRight
                                    Case "Lower Center"
                                        oProperty.Alignment = AAL.Justification.LowerCenter
                                    Case "Lower Left"
                                        oProperty.Alignment = AAL.Justification.LowerLeft
                                    Case "Lower Right"
                                        oProperty.Alignment = AAL.Justification.LowerRight
                                End Select

                            Case "Color"

                                oProperty.Modifications.Add(AAL.PropertyMods.Color)

                                If oAttribute.Text = "Automatic" Then
                                    oProperty.AutomaticColor = True
                                Else
                                    Dim sColorSplit() As String = Split(oAttribute.Text, ":")
                                    Dim iRed() As String = Split(sColorSplit(2))
                                    Dim iGreen() As String = Split(sColorSplit(3))
                                    Dim iBlue As String = sColorSplit(4)

                                    oProperty.Color = Color.FromArgb(iRed(0), iGreen(0), iBlue)

                                    oProperty.AutomaticColor = False
                                End If

                            Case "Visibility"

                                oProperty.Modifications.Add(AAL.PropertyMods.Visbility)

                                Dim Visbility() As String = oAttribute.Text.Split(":")

                                Dim iVisibility As String = Visbility(1).Trim

                                Select Case iVisibility
                                    Case "All Hidden"
                                        oProperty.Display = AAL.Visibility.AllHidden
                                    Case "All Visible"
                                        oProperty.Display = AAL.Visibility.AllVisible
                                    Case "Name Visible"
                                        oProperty.Display = AAL.Visibility.NameVisible
                                    Case "Value Visible"
                                        oProperty.Display = AAL.Visibility.ValueVisible
                                End Select

                        End Select

                    Next

                Case "RemoveValue"

                    oProperty.Modifications.Add(AAL.PropertyMods.Value)

                    oProperty.Value = Nothing

                Case "AddValue"

                    oProperty.Modifications.Add(AAL.PropertyMods.Value)

                    oProperty.Value = oSubNode.FirstNode.Text

                Case "Rename"
                    oProperty.Modifications.Add(AAL.PropertyMods.Name)
                    oProperty.Name = oSubNode.FirstNode.Text

            End Select

        Next

        Return oProperty

    End Function

    Private Sub Process_Properties()

        'Dim dic_Properties As New Dictionary(Of String, AAL.SymbolProperty)

        dic_ModifiedSymbolProperties.Clear()
        alSymPartitionToProcess.Clear()

        For Each oTreeNode As TreeNode In tv_Properties.Nodes

            If dic_SymProperties.ContainsKey(oTreeNode.Text) Then

                Dim oProperty As New AAL.SymbolProperty

                oProperty.Name = oTreeNode.Text

                If oTreeNode.Nodes.Count > 0 Then

                    oProperty = PrepareProperty(oTreeNode, oProperty)

                    dic_ModifiedSymbolProperties.Item(oTreeNode.Text) = oProperty

                End If

                dic_SymProperties.Item(oTreeNode.Text) = oProperty

            End If

        Next

        For Each oTreeNode As TreeNode In tv_PinProperties.Nodes

            If dic_PinProperties.ContainsKey(oTreeNode.Text) Then

                Dim oPinProperty As New AAL.SymbolPinProperty

                oPinProperty.Name = oTreeNode.Text

                If oTreeNode.Nodes.Count > 0 Then

                    oPinProperty = PrepareProperty(oTreeNode, oPinProperty)

                    dic_ModifiedPinProperties.Add(oPinProperty.Name, oPinProperty)

                End If

                dic_PinProperties.Item(oTreeNode.Text) = oPinProperty

            End If

        Next

        sSymDirectory = Path.GetDirectoryName(frmMain.librarydata.LibPath) & "\SymbolLibs\"

        For Each oNode As TreeNode In tv_Symbols.Nodes

            If oNode.Checked = True Then
                alSymPartitionToProcess.Add(oNode.Text)
            End If

        Next

        Dim newThreads(alSymPartitionToProcess.Count) As Thread
        For i As Integer = 0 To alSymPartitionToProcess.Count - 1
            Dim modifySymProps As Symbols = New Symbols()
            modifySymProps.libDoc = frmMain.libDoc
            modifySymProps.LibraryData = frmMain.librarydata
            modifySymProps.l_RemoveProperties = l_PropertiesToRemove
            Dim l_NewProperties As New List(Of AAL.SymbolProperty)(dic_newProperties.Values)
            modifySymProps.l_NewProperties = l_NewProperties
            modifySymProps.l_RemovePinProperties = l_PinPropertiesToRemove
            Dim l_NewPinProperties As New List(Of AAL.SymbolPinProperty)(dic_newPinProperties.Values)
            modifySymProps.l_NewPinProperties = l_NewPinProperties

            modifySymProps.dic_ModifiedProperties = dic_ModifiedSymbolProperties
            modifySymProps.dic_ModifiedPinProperties = dic_ModifiedPinProperties
            modifySymProps.bRemoveTrailingSpaces = chkbox_RemoveSpaces.Checked
            modifySymProps.bUpdateTimeStamp = bEnableTimeMachine
            modifySymProps.oDate = dateChooser.Value

            AddHandler modifySymProps.eUpdateCount, AddressOf UpdateCount
            AddHandler modifySymProps.eProcessComplete, AddressOf SymbolUpdateToLog

            i_Count += frmMain.librarydata.SymbolsByPartition.Item(alSymPartitionToProcess(i)).Count()
            RaiseEvent eUpdateStatus("Symbols left to process: " & i_Count)

            newThreads(i) = New Thread(AddressOf modifySymProps.ProcessSymbols)
            newThreads(i).IsBackground = True
            newThreads(i).Start(alSymPartitionToProcess(i))

        Next

        For i As Integer = 0 To alSymPartitionToProcess.Count - 1
            newThreads(i).Join()
        Next

        RaiseEvent eProcessComplete()

    End Sub

    Private Sub ReadAttsComplete()
        If Me.InvokeRequired Then

            Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Properties.log", True)

                writer.WriteLine("NonCommon Symbol Properties:")
                l_NonCommonSymProperties.Sort()
                For Each sProperty As String In l_NonCommonSymProperties
                    writer.WriteLine(vbTab & sProperty)
                Next

                writer.WriteLine()

                writer.WriteLine("NonCommon Pin Properties:")
                l_NonCommonSymPinProperties.Sort()
                For Each sProperty As String In l_NonCommonSymPinProperties
                    writer.WriteLine(vbTab & sProperty)
                Next
                writer.WriteLine()

                writer.WriteLine(sb_SymbolAtts.ToString())

            End Using

            Dim d As New d_ReadComplete(AddressOf ReadAttsComplete)
            Me.Invoke(d)
        Else

            Refresh_TreeView()

            bReadSymsAtts = True
            gb_symbols.Enabled = True
            gb_Options.Enabled = True
            chkbox_RemoveSpaces.Enabled = True
            dateChooser.Enabled = True
            lbl_TimeMachine.Enabled = True
            WaitGif.Enabled = False
            btn_Process.Enabled = True
            SplitContainer1.Panel2.Enabled = True

            If bAttsWithSpaces = True Then

                ts_Status.Text = "Read complete, properties found with trailing spaces."

                Dim reply As DialogResult = MessageBox.Show("Symbols properties have been read. Some properties have trailing spaces, would you like to review these properties?", "Finished",
    MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Symbol Properties with Spaces")
                Else
                    Dim reply2 As DialogResult = MessageBox.Show("Would you like to review the results for all symbol properties?", "Finished",
    MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

                    If reply = DialogResult.Yes Then
                        frmMain.OpenLogFile("Symbol Properties")
                    Else
                        MessageBox.Show("For more information, please see:" & Environment.NewLine & Environment.NewLine & frmMain.librarydata.LogPath & "Symbol Properties.log" & Environment.NewLine & Environment.NewLine & frmMain.librarydata.LogPath & "Symbol Properties with Spaces.log")
                    End If
                End If
            Else

                ts_Status.Text = "Read complete..."

                Dim reply As DialogResult = MessageBox.Show("Symbols properties have been read, would you like to review the results?", "Finished",
          MessageBoxButtons.YesNo, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1)

                If reply = DialogResult.Yes Then
                    frmMain.OpenLogFile("Symbol Properties")
                Else
                    MessageBox.Show("For more information, please see:" & Environment.NewLine & Environment.NewLine & frmMain.librarydata.LogPath & "Symbol Properties.log")
                End If
            End If

        End If
    End Sub

    Private Sub ReadPartitionComplete(ByVal sPartition As String, ByVal dic_UniqueSymProperties As Dictionary(Of String, AAL.SymbolProperty), ByVal dic_Symbols As Dictionary(Of String, Object), ByVal dic_SymPropswithTrailingSpaces As Dictionary(Of String, List(Of String)), ByVal dic_UniquePinProperties As Dictionary(Of String, AAL.SymbolPinProperty), ByVal dic_NonCommonProperties As Dictionary(Of String, List(Of String)))
        If Me.InvokeRequired Then

            Dim d As New d_ReadPartitionComplete(AddressOf ReadPartitionComplete)
            Me.Invoke(d, New Object() {sPartition, dic_UniqueSymProperties, dic_Symbols, dic_SymPropswithTrailingSpaces, dic_UniquePinProperties, Nothing})
        Else

            SyncLock sublock

                If dic_Symbols.Count > 0 Then

                    sb_SymbolAtts.AppendLine(sPartition & ":")

                    Dim dic_PartitionProperties As New Dictionary(Of String, AAL.SymbolProperty)(StringComparer.OrdinalIgnoreCase)
                    Dim dic_PartitionPinProperties As New Dictionary(Of String, AAL.SymbolPinProperty)(StringComparer.OrdinalIgnoreCase)

                    For Each kvp As KeyValuePair(Of String, Object) In dic_Symbols
                        Dim sSym As String = kvp.Key
                        Dim dic_SymbolProperties As Dictionary(Of String, AAL.SymbolProperty) = kvp.Value.Symbol
                        Dim dic_SymbolPinProperties As Dictionary(Of String, AAL.SymbolPinProperty) = kvp.Value.Pin

                        Dim l_NonCommonProperties As New List(Of String)
                        Dim l_NonCommonPinProperties As New List(Of String)

                        Dim bPrintSymbolName As Boolean = True
                        Dim bPrintHeader As Boolean = True
                        Dim bPrintPinHeader As Boolean = True

                        For Each oProperty As AAL.SymbolProperty In dic_SymbolProperties.Values

                            If oProperty.Name.StartsWith("@") Or oProperty.Name = "FORWARD_PCB" Then
                                Continue For
                            End If

                            If bPrintSymbolName = True Then
                                sb_SymbolAtts.AppendLine(vbTab & sSym)
                                bPrintSymbolName = False
                            End If

                            If Not dic_PartitionProperties.ContainsKey(oProperty.Name) Then
                                dic_PartitionProperties.Item(oProperty.Name) = oProperty
                            End If

                            If frmMain.librarydata.SymbolNoncommonProperties.Contains(oProperty.Name.Trim, StringComparer.OrdinalIgnoreCase) Then
                                If Not l_NonCommonProperties.Contains(oProperty.Name.Trim) Then l_NonCommonProperties.Add(oProperty.Name.Trim)
                            ElseIf frmMain.librarydata.SymbolCommonProperties.Contains(oProperty.Name.Trim, StringComparer.OrdinalIgnoreCase) Then
                                If bPrintHeader = True Then
                                    sb_SymbolAtts.AppendLine(vbTab & vbTab & "Common Properties:")
                                    bPrintHeader = False
                                End If
                                sb_SymbolAtts.AppendLine(vbTab & vbTab & vbTab & oProperty.Name)
                            Else
                                l_NonCommonProperties.Add(oProperty.Name.Trim)
                                frmMain.librarydata.SymbolNoncommonProperties.Add(oProperty.Name.Trim)
                                If Not l_NonCommonSymProperties.Contains(oProperty.Name.Trim) Then l_NonCommonSymProperties.Add(oProperty.Name.Trim)
                            End If

                        Next

                        For Each oProperty As AAL.SymbolPinProperty In dic_SymbolPinProperties.Values

                            If oProperty.Name.StartsWith("@") Or oProperty.Name = "FORWARD_PCB" Then
                                Continue For
                            End If

                            If bPrintSymbolName = True Then
                                sb_SymbolAtts.AppendLine(vbTab & sSym)
                                bPrintSymbolName = False
                            End If

                            If Not dic_PartitionProperties.ContainsKey(oProperty.Name) Then
                                dic_PartitionPinProperties.Item(oProperty.Name) = oProperty
                            End If

                            If frmMain.librarydata.SymbolNoncommonPinProperties.Contains(oProperty.Name.Trim, StringComparer.OrdinalIgnoreCase) Then
                                If Not l_NonCommonPinProperties.Contains(oProperty.Name.Trim) Then l_NonCommonPinProperties.Add(oProperty.Name.Trim)
                            ElseIf frmMain.librarydata.SymbolCommonProperties.Contains(oProperty.Name.Trim, StringComparer.OrdinalIgnoreCase) Then
                                If bPrintPinHeader = True Then
                                    sb_SymbolAtts.AppendLine(vbTab & vbTab & "Common Pin Properties:")
                                    bPrintPinHeader = False
                                End If
                                sb_SymbolAtts.AppendLine(vbTab & vbTab & vbTab & oProperty.Name)
                            Else
                                l_NonCommonPinProperties.Add(oProperty.Name.Trim)
                                frmMain.librarydata.SymbolNoncommonPinProperties.Add(oProperty.Name.Trim)
                                If Not l_NonCommonSymPinProperties.Contains(oProperty.Name.Trim) Then l_NonCommonSymPinProperties.Add(oProperty.Name.Trim)
                            End If

                        Next

                        If l_NonCommonProperties.Count > 0 Then
                            sb_SymbolAtts.AppendLine(vbTab & vbTab & "NonCommon Properties:")
                            l_NonCommonProperties.Sort()
                            For Each sProperty As String In l_NonCommonProperties
                                sb_SymbolAtts.AppendLine(vbTab & vbTab & vbTab & sProperty)
                            Next

                        End If

                        If l_NonCommonPinProperties.Count > 0 Then
                            sb_SymbolAtts.AppendLine(vbTab & vbTab & "NonCommon Pin Properties:")
                            l_NonCommonPinProperties.Sort()
                            For Each sProperty As String In l_NonCommonPinProperties
                                sb_SymbolAtts.AppendLine(vbTab & vbTab & vbTab & sProperty)
                            Next

                        End If

                        If bPrintSymbolName = False Then
                            sb_SymbolAtts.AppendLine()
                        End If

                    Next

                    dicSymAttsbyPartition.Item(sPartition) = dic_PartitionProperties
                    dicPinPropsbyPartition.Item(sPartition) = dic_UniquePinProperties

                    sb_SymbolAtts.AppendLine()

                End If

                If dic_SymPropswithTrailingSpaces.Count > 0 Then
                    Dim sb_PropsWithTrailingSpaces As New StringBuilder

                    sb_PropsWithTrailingSpaces.AppendLine(sPartition & ":")

                    For Each kvp As KeyValuePair(Of String, List(Of String)) In dic_SymPropswithTrailingSpaces
                        Dim sSym As String = kvp.Key
                        Dim alAtt As List(Of String) = kvp.Value

                        bAttsWithSpaces = True

                        sb_PropsWithTrailingSpaces.AppendLine(vbTab & sSym)

                        alAtt.Sort()

                        For Each att In alAtt
                            sb_PropsWithTrailingSpaces.AppendLine(vbTab & vbTab & att)
                        Next

                        sb_PropsWithTrailingSpaces.AppendLine()

                    Next

                    Using writer As StreamWriter = New StreamWriter(frmMain.librarydata.LogPath & "Symbol Properties with Spaces.log", True)

                        writer.WriteLine(sb_PropsWithTrailingSpaces.ToString())
                        writer.WriteLine()

                    End Using

                End If
            End SyncLock

        End If
    End Sub

    Private Sub Refresh_TreeView()

        tv_Properties.Nodes.Clear()
        tv_PinProperties.Nodes.Clear()

        dic_SymProperties.Clear()
        dic_PinProperties.Clear()

        For Each kvp As KeyValuePair(Of String, Dictionary(Of String, AAL.SymbolProperty)) In dicSymAttsbyPartition
            Dim sPartition As String = kvp.Key
            Dim l_Properties As New List(Of AAL.SymbolProperty)(kvp.Value.Values)
            If alSymPartitionToProcess.Contains(sPartition) Then
                For Each oProperty As AAL.SymbolProperty In l_Properties

                    If oProperty.Name = "FORWARD_PCB" Or oProperty.Name.StartsWith("@") Then
                        Continue For
                    End If

                    If chkbox_RemoveSpaces.Checked = True Then

                        oProperty.Name = oProperty.Name.Trim

                    End If

                    If Not dic_SymProperties.ContainsKey(oProperty.Name) Then
                        dic_SymProperties.Add(oProperty.Name, Nothing)
                        Dim nodeProperty As TreeNode = tv_Properties.Nodes.Add(oProperty.Name)

                        If frmMain.librarydata.SymbolNoncommonProperties.Contains(oProperty.Name) Then
                            nodeProperty.ForeColor = Color.Red
                        End If

                    End If

                Next
            End If
        Next

        For Each kvp As KeyValuePair(Of String, Dictionary(Of String, AAL.SymbolPinProperty)) In dicPinPropsbyPartition
            Dim sPartition As String = kvp.Key
            Dim l_Properties As New List(Of AAL.SymbolPinProperty)(kvp.Value.Values)
            If alSymPartitionToProcess.Contains(sPartition) Then
                For Each oProperty As AAL.SymbolPinProperty In l_Properties

                    If chkbox_RemoveSpaces.Checked = True Then

                        oProperty.Name = oProperty.Name.Trim

                    End If

                    If Not dic_PinProperties.ContainsKey(oProperty.Name) Then
                        dic_PinProperties.Add(oProperty.Name, Nothing)
                        Dim nodeProperty As TreeNode = tv_PinProperties.Nodes.Add(oProperty.Name)

                        If Not frmMain.librarydata.SymbolPinCommonProperties.Contains(oProperty.Name) Then
                            nodeProperty.ForeColor = Color.Red
                        End If

                    End If

                Next
            End If
        Next

        tv_Properties.Sort()
        tv_PinProperties.Sort()

    End Sub

    Private Function RemoveDefaultValue(treeNode As TreeNode) As TreeNode

        Dim oAction As TreeNode = Nothing

        For Each oNode As TreeNode In treeNode.Nodes
            If oNode.Name = "RemoveValue" Then
                oAction = oNode
                Exit For
            End If
        Next

        If IsNothing(oAction) Then
            oAction = New TreeNode()
            oAction.Name = "RemoveValue"
            oAction.Text = "Remove Default Value"

            Return oAction
        End If
    End Function

    Private Function RenameProperty(treeNode As TreeNode) As TreeNode

        Dim oAction As TreeNode = Nothing

        For Each oNode As TreeNode In treeNode.Nodes
            If oNode.Name = "Rename" Then
                oAction = oNode
                Exit For
            End If
        Next

        If IsNothing(oAction) Then
            oAction = New TreeNode()
            oAction.Name = "Rename"
            oAction.Text = "Rename To"
        End If

        oAction.Nodes.Clear()

        Dim sPropertyName As String = InputBox("Provide a new property name for property: " & treeNode.Text, "Rename Property", "New Property Name")

        If Not sPropertyName = "" Then
            oAction.Nodes.Add(sPropertyName)
        End If

        Return oAction
    End Function

    Private Sub ResetForm()

        tv_Symbols.Nodes.Clear()
        tv_PinProperties.Nodes.Clear()
        tv_Properties.Nodes.Clear()

        dic_ModifiedPinProperties.Clear()
        dic_ModifiedSymbolProperties.Clear()
        dic_newPinProperties.Clear()
        dic_newProperties.Clear()
        dic_PinProperties.Clear()
        dic_PinPropertyNodes.Clear()
        dic_SymProperties.Clear()
        dic_PropertyNodes.Clear()
        dicPartitionSymbolsAtts.Clear()
        dicPinPropsbyPartition.Clear()
        dicSymAttsbyPartition.Clear()
        dicSymtoProcess.Clear()

        alSymPartitionToProcess.Clear()

        l_PropertiesToRemove.Clear()
        l_PinPropertiesToRemove.Clear()
        l_PinPropertiesToRemoveDefaultValue.Clear()
        l_PropertiesToRemoveDefaultValue.Clear()

        For Each sPartition As String In frmMain.librarydata.SymbolsByPartition.Keys

            Dim oNode As New TreeNode
            oNode.Text = sPartition
            oNode.Checked = True
            tv_Symbols.Nodes.Add(oNode)
            alSymPartitionToProcess.Add(oNode.Text)

        Next

        gb_Options.Enabled = True
        btn_Read.Enabled = True
        btn_Process.Enabled = False
        chkbox_RemoveSpaces.Enabled = False
        dateChooser.Enabled = False
        dateChooser.Enabled = False
        dateChooser.Checked = False
        dateChooser.Checked = False
        SplitContainer1.Panel2.Enabled = False
        gb_symbols.Enabled = True

    End Sub

    Private Sub SymbolUpdateToLog(Partition As String, Report As StringBuilder)

        If Not IsNothing(Report) Then
            SyncLock sublock

                ProcessLog.AppendLine(Report.ToString())
                ProcessLog.AppendLine()

            End SyncLock
        End If

    End Sub

    Private Sub ts_RemoveAll_Click(sender As System.Object, e As System.EventArgs) Handles ts_RemoveAll.Click

    End Sub

    Private Sub tsm_AddDefaultValue_Click(sender As System.Object, e As System.EventArgs) Handles tsm_AddDefaultValue.Click

        Dim oAction As TreeNode = AddDefaultValue(tv_Properties.SelectedNode)

        If Not IsNothing(oAction) Then
            tv_Properties.SelectedNode.Nodes.Add(oAction)
            tv_Properties.SelectedNode.ExpandAll()
        End If

    End Sub

    Private Sub tsm_AddPinPropertyValue_Click(sender As System.Object, e As System.EventArgs) Handles tsm_AddPinPropertyValue.Click
        Dim oAction As TreeNode = AddDefaultValue(tv_PinProperties.SelectedNode)

        If Not IsNothing(oAction) Then
            tv_PinProperties.SelectedNode.Nodes.Add(oAction)
            tv_PinProperties.SelectedNode.ExpandAll()
        End If
    End Sub

    Private Sub tsm_EditPinPropertyAtts_Click(sender As System.Object, e As System.EventArgs) Handles tsm_EditPinPropertyAtts.Click
        Dim oEditProperty As New frmEdit_Property_Info

        oEditProperty.oParentNode = oCurrentNode

        EditNode(oEditProperty, oCurrentNode, PropertyType.Pin)
    End Sub

    Private Sub tsm_EditProperty_Click(sender As System.Object, e As System.EventArgs) Handles tsm_EditProperty.Click
        Dim oEditProperty As New frmEdit_Property_Info
        oEditProperty.oParentNode = oCurrentNode

        EditNode(oEditProperty, oCurrentNode)
    End Sub

    Private Sub tsm_NewPinProperty_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click

        Dim sPropertyName As String = InputBox("Provide a property name for the new pin property. ", "New Pin Property", "New Pin Property Name")

        If Not sPropertyName = "" Then

            If dic_PinProperties.ContainsKey(sPropertyName) Or dic_newPinProperties.ContainsKey(sPropertyName) Then
                MessageBox.Show("Property " & sPropertyName & " already exists.")
            Else

                Dim oPinPropertyNode As New TreeNode
                oPinPropertyNode.Text = sPropertyName

                Dim oEditProperty As New frmEdit_Property_Info
                oEditProperty.bNewProperty = True
                oEditProperty.oParentNode = oCurrentNode

                Dim oPinProperty As AAL.SymbolPinProperty = EditNode(oEditProperty, oPinPropertyNode, PropertyType.Pin)

                If Not IsNothing(oPinProperty) Then
                    dic_newPinProperties.Add(oPinProperty.Name, oPinProperty)
                    oPinPropertyNode.ForeColor = Color.DarkBlue

                    For Each oNode As TreeNode In oPinPropertyNode.Nodes
                        oNode.ForeColor = Color.DarkBlue
                    Next

                    oPinPropertyNode.ExpandAll()
                    tv_PinProperties.Nodes.Add(oPinPropertyNode)
                End If

                oPinPropertyNode = Nothing
                oEditProperty.Dispose()

            End If

        End If

    End Sub

    Private Sub tsm_NewSymbolProperty_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click

        Dim sPropertyName As String = InputBox("Provide a property name for the new property. ", "New Property", "New Property Name")

        If Not String.IsNullOrEmpty(sPropertyName) Then

            If dic_SymProperties.ContainsKey(sPropertyName) Or dic_newProperties.ContainsKey(sPropertyName) Then

                MessageBox.Show("Property " & sPropertyName & " already exists.")
            Else

                Dim oPropertyNode As New TreeNode
                oPropertyNode.Text = sPropertyName

                Dim oEditProperty As New frmEdit_Property_Info
                oEditProperty.bNewProperty = True

                oEditProperty.oParentNode = oCurrentNode

                Dim oProperty As AAL.SymbolProperty = EditNode(oEditProperty, oPropertyNode)

                If Not IsNothing(oProperty) Then

                    dic_newProperties.Add(oProperty.Name, oProperty)

                    oPropertyNode.ForeColor = Color.DarkBlue

                    For Each oNode As TreeNode In oPropertyNode.Nodes
                        oNode.ForeColor = Color.DarkBlue
                    Next

                    oPropertyNode.ExpandAll()
                    tv_Properties.Nodes.Add(oPropertyNode)
                End If

                oPropertyNode = Nothing
                oEditProperty.Dispose()

            End If

        End If

    End Sub

    Private Sub tsm_PinRemoveDefaultValue_Click(sender As System.Object, e As System.EventArgs) Handles tsm_RemovePinPropertyValue.Click

        Dim oAction As TreeNode = RemoveDefaultValue(tv_PinProperties.SelectedNode)

        tv_PinProperties.SelectedNode.Nodes.Add(oAction)
        tv_PinProperties.SelectedNode.ExpandAll()

        If l_PinPropertiesToRemoveDefaultValue.Contains(tv_PinProperties.SelectedNode.Text) Then
            l_PinPropertiesToRemoveDefaultValue.Remove(tv_PinProperties.SelectedNode.Text)
        Else
            l_PinPropertiesToRemoveDefaultValue.Add(tv_PinProperties.SelectedNode.Text)
        End If

    End Sub

    Private Sub tsm_PromoteCommonSymbol_Click(sender As System.Object, e As System.EventArgs) Handles tsm_PromoteCommonSymbol.Click

        If frmMain.librarydata.SymbolPinCommonProperties.Contains(tv_Properties.SelectedNode.Text) Then
            frmMain.librarydata.NewCommonProperty(tv_Properties.SelectedNode.Text, Data.PropertyType.Both)
        Else
            frmMain.librarydata.NewCommonProperty(tv_Properties.SelectedNode.Text, Data.PropertyType.Symbol)
        End If

        tv_Properties.SelectedNode.ForeColor = Color.Black

    End Sub

    Private Sub tsm_PromotePinProperty_Click(sender As System.Object, e As System.EventArgs) Handles tsm_PromotePinProperty.Click

        If frmMain.librarydata.SymbolCommonProperties.Contains(tv_PinProperties.SelectedNode.Text) Then
            frmMain.librarydata.NewCommonProperty(tv_PinProperties.SelectedNode.Text, Data.PropertyType.Both)
        Else
            frmMain.librarydata.NewCommonProperty(tv_PinProperties.SelectedNode.Text, Data.PropertyType.Pin)
        End If

        tv_PinProperties.SelectedNode.ForeColor = Color.Black
    End Sub

    Private Sub tsm_Read_Click(sender As System.Object, e As System.EventArgs)
        tv_Properties.Nodes.Clear()
        ts_Status.Text = "Reading Properties..."

        If My.Computer.FileSystem.FileExists(frmMain.librarydata.LogPath & "Symbol Properties.log") Then

            My.Computer.FileSystem.DeleteFile(frmMain.librarydata.LogPath & "Symbol Properties.log")

        End If

        gb_symbols.Enabled = False
        gb_Options.Enabled = False
        WaitGif.Enabled = True

        AddHandler eUpdateStatus, AddressOf UpdateStatus
        AddHandler eReadComplete, AddressOf ReadAttsComplete
        Dim t_ReadAttributes As Thread = New Threading.Thread(AddressOf Read_Attributes)
        t_ReadAttributes.IsBackground = True
        t_ReadAttributes.Start()
    End Sub

    Private Sub tsm_RemoveDefaultValue_Click(sender As System.Object, e As System.EventArgs) Handles tsm_RemoveDefaultValue.Click

        Dim oAction As TreeNode = RemoveDefaultValue(tv_Properties.SelectedNode)

        tv_Properties.SelectedNode.Nodes.Add(oAction)
        tv_Properties.SelectedNode.ExpandAll()

        If l_PropertiesToRemoveDefaultValue.Contains(tv_Properties.SelectedNode.Text) Then
            l_PropertiesToRemoveDefaultValue.Remove(tv_Properties.SelectedNode.Text)
        Else
            l_PropertiesToRemoveDefaultValue.Add(tv_Properties.SelectedNode.Text)
        End If
    End Sub

    Private Sub tsm_RemovePinAction_Click(sender As System.Object, e As System.EventArgs) Handles tsm_RemovePinAction.Click

        If dic_ModifiedPinProperties.ContainsKey(tv_PinProperties.SelectedNode.Parent.Text) Then
            dic_ModifiedPinProperties.Remove(tv_PinProperties.SelectedNode.Parent.Text)
        End If

        tv_PinProperties.SelectedNode.Remove()

    End Sub

    Private Sub tsm_RemovePinProperty_Click(sender As System.Object, e As System.EventArgs) Handles tsm_RemovePinProperty.Click
        tv_PinProperties.SelectedNode.Nodes.Clear()

        If l_PinPropertiesToRemove.Contains(tv_PinProperties.SelectedNode.Text) Then
            tv_PinProperties.SelectedNode.ForeColor = System.Drawing.SystemColors.MenuText
            l_PinPropertiesToRemove.Remove(tv_Properties.SelectedNode.Text)
        Else
            tv_PinProperties.SelectedNode.ForeColor = Color.Gray
            l_PinPropertiesToRemove.Add(tv_PinProperties.SelectedNode.Text)
        End If
    End Sub

    Private Sub tsm_RemoveProperty_Click(sender As System.Object, e As System.EventArgs) Handles tsm_RemoveProperty.Click

        tv_Properties.SelectedNode.Nodes.Clear()

        If l_PropertiesToRemove.Contains(tv_Properties.SelectedNode.Text) Then
            tv_Properties.SelectedNode.ForeColor = System.Drawing.SystemColors.MenuText
            l_PropertiesToRemove.Remove(tv_Properties.SelectedNode.Text)
        Else
            tv_Properties.SelectedNode.ForeColor = Color.Gray
            l_PropertiesToRemove.Add(tv_Properties.SelectedNode.Text)
        End If

    End Sub

    Private Sub tsm_RemovePropertyAction_Click(sender As System.Object, e As System.EventArgs) Handles tsm_RemovePropertyAction.Click
        If dic_ModifiedSymbolProperties.ContainsKey(tv_Properties.SelectedNode.Parent.Text) Then
            dic_ModifiedSymbolProperties.Remove(tv_Properties.SelectedNode.Parent.Text)
        End If

        tv_Properties.SelectedNode.Remove()
    End Sub

    Private Sub tsm_Rename_Click(sender As System.Object, e As System.EventArgs) Handles tsm_Rename.Click

        Dim oAction As TreeNode = RenameProperty(tv_Properties.SelectedNode)

        If Not IsNothing(oAction) Then
            tv_Properties.SelectedNode.Nodes.Add(oAction)
            tv_Properties.SelectedNode.ExpandAll()
        End If

    End Sub

    Private Sub tsm_RenamePinProperty_Click(sender As System.Object, e As System.EventArgs) Handles tsm_RenamePinProperty.Click
        Dim oAction As TreeNode = RenameProperty(tv_PinProperties.SelectedNode)

        If Not IsNothing(oAction) Then
            tv_PinProperties.SelectedNode.Nodes.Add(oAction)
            tv_PinProperties.SelectedNode.ExpandAll()
        End If
    End Sub

    Private Sub tv_PinProperties_NodeMouseClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tv_PinProperties.NodeMouseClick

        If e.Button = System.Windows.Forms.MouseButtons.Right And e.Node.Level = 0 And Not dic_newPinProperties.ContainsKey(e.Node.Text) Then

            tv_PinProperties.ContextMenuStrip = PinPropertyAction

            If frmMain.librarydata.SymbolNoncommonPinProperties.Contains(e.Node.Text) Then
                tsm_PromotePinProperty.Visible = True
                ToolStripSeparator5.Visible = True
            End If

            If l_PinPropertiesToRemove.Contains(e.Node.Text) Then
                tsm_RemovePinProperty.Checked = True

                tsm_EditPinPropertyAtts.Enabled = False
                tsm_CopyPinPropertyAtts.Enabled = False
                tsm_RemovePinProperty.Enabled = False
                tsm_AddPinPropertyValue.Enabled = False
                tsm_RemovePinPropertyValue.Enabled = False
                tsm_RemovePinPropertyValue.Checked = False
                tsm_RenamePinProperty.Enabled = False
            Else
                tsm_RemovePinProperty.Checked = False
                tsm_RemovePinProperty.Enabled = True

                If tv_PinProperties.Nodes.Count > 1 Then
                    tsm_CopyPinPropertyAtts.Enabled = True
                    tsm_CopyPinPropertyAtts.DropDownItems.Clear()
                    For Each oNode As TreeNode In tv_PinProperties.Nodes
                        If Not oNode.Text = e.Node.Text And Not oNode.Nodes.Count = 0 Then
                            Dim oCopyNodeMenu As ToolStripMenuItem = tsm_CopyPinPropertyAtts.DropDownItems.Add(oNode.Text)
                            AddHandler oCopyNodeMenu.Click, AddressOf copyPinNode
                        End If
                    Next

                    If tsm_CopyPinPropertyAtts.DropDownItems.Count = 0 Then
                        tsm_CopyPinPropertyAtts.Enabled = False
                    End If
                Else
                    tsm_CopyPinPropertyAtts.Enabled = False
                End If

                tsm_EditPinPropertyAtts.Enabled = True
                tsm_RenamePinProperty.Enabled = True
                tsm_AddPinPropertyValue.Enabled = True
                tsm_RemovePinPropertyValue.Enabled = True

                If l_PinPropertiesToRemoveDefaultValue.Contains(e.Node.Text) Then
                    tsm_RemovePinPropertyValue.Checked = True
                Else
                    tsm_RemovePinPropertyValue.Checked = False
                End If

            End If

            oCurrentNode = e.Node
            tv_PinProperties.SelectedNode = e.Node
            PinPropertyAction.Enabled = True

            'Display the context menu
            PinPropertyAction.Show(tv_PinProperties, New Point(e.X, e.Y))

        ElseIf e.Button = System.Windows.Forms.MouseButtons.Right And e.Node.Level = 1 And Not dic_newPinProperties.ContainsKey(e.Node.Text) Then

            tv_PinProperties.ContextMenuStrip = RemovePinAction
            RemovePinAction.Enabled = True
            tv_PinProperties.SelectedNode = e.Node
            RemovePinAction.Show(tv_PinProperties, New Point(e.X, e.Y))
        Else

            PinPropertyAction.Enabled = False
            RemovePinAction.Enabled = False

        End If
    End Sub

    Private Sub tv_Properties_NodeMouseClick(sender As System.Object, e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles tv_Properties.NodeMouseClick
        If e.Button = System.Windows.Forms.MouseButtons.Right And e.Node.Level = 0 Then

            tv_Properties.ContextMenuStrip = PropertyAction
            tsm_RemoveProperty.Checked = False
            tsm_PromoteCommonSymbol.Visible = False
            tsm_EditProperty.Enabled = False
            tsm_SeperatorPromote.Visible = False
            tsm_Copy.Enabled = False
            tsm_Rename.Enabled = False
            tsm_AddDefaultValue.Enabled = False
            tsm_RemoveDefaultValue.Enabled = False
            tsm_RemoveDefaultValue.Checked = False

            If frmMain.librarydata.SymbolNoncommonProperties.Contains(e.Node.Text) Then
                tsm_PromoteCommonSymbol.Visible = True
                tsm_SeperatorPromote.Visible = True
            End If

            If dic_newProperties.ContainsKey(e.Node.Text) Then

                tsm_EditProperty.Enabled = True

            ElseIf l_PropertiesToRemove.Contains(e.Node.Text) Then

                tsm_RemoveProperty.Checked = True
            Else
                If tv_Properties.Nodes.Count > 1 Then
                    tsm_Copy.Enabled = True
                    tsm_Copy.DropDownItems.Clear()
                    For Each oNode As TreeNode In tv_Properties.Nodes
                        If Not oNode.Text = e.Node.Text And Not oNode.Nodes.Count = 0 Then
                            Dim oCopyNodeMenu As ToolStripMenuItem = tsm_Copy.DropDownItems.Add(oNode.Text)
                            AddHandler oCopyNodeMenu.Click, AddressOf copyPropertyNode
                        End If
                    Next

                    If tsm_Copy.DropDownItems.Count = 0 Then
                        tsm_Copy.Enabled = False
                    End If

                End If

                tsm_EditProperty.Enabled = True
                tsm_Rename.Enabled = True
                tsm_AddDefaultValue.Enabled = True
                tsm_RemoveDefaultValue.Enabled = True

                If l_PropertiesToRemoveDefaultValue.Contains(e.Node.Text) Then
                    tsm_RemoveDefaultValue.Checked = True
                End If

            End If

            oCurrentNode = e.Node
            tv_Properties.SelectedNode = e.Node
            PropertyAction.Enabled = True

            'Display the context menu
            PropertyAction.Show(tv_Properties, New Point(e.X, e.Y))

        ElseIf e.Button = System.Windows.Forms.MouseButtons.Right And e.Node.Level = 1 And Not dic_newPinProperties.ContainsKey(e.Node.Text) Then

            tv_Properties.ContextMenuStrip = RemovePropertyAction
            RemovePropertyAction.Enabled = True
            tv_Properties.SelectedNode = e.Node
            RemovePropertyAction.Show(tv_Properties, New Point(e.X, e.Y))
        Else

            PropertyAction.Enabled = False
            RemovePropertyAction.Enabled = False
        End If

    End Sub

    Private Sub tv_Symbols_BeforeCheck(sender As System.Object, e As System.Windows.Forms.TreeViewCancelEventArgs) Handles tv_Symbols.BeforeCheck

        tv_Symbols.Enabled = False
        btn_Process.Enabled = False
        btn_Read.Enabled = False
        tv_Properties.Enabled = False

        RemoveHandler tv_Symbols.BeforeCheck, AddressOf tv_Symbols_BeforeCheck

        If e.Node.Checked = True And e.Node.Level = 0 Then
            alSymPartitionToProcess.Remove(e.Node.Text)
        ElseIf e.Node.Checked = False And e.Node.Level = 0 Then
            alSymPartitionToProcess.Add(e.Node.Text)
        End If

        Dim oPartCount As New CheckNode
        AddHandler oPartCount.CheckNode, AddressOf CheckNode
        AddHandler oPartCount.CheckNodesComplete, AddressOf CheckNodesComplete
        Dim th_UpdateCount As Thread = New Thread(AddressOf oPartCount.Update)
        th_UpdateCount.IsBackground = True
        th_UpdateCount.Start(e.Node)

    End Sub

    Private Sub UpdateCount(ByVal process As String)
        If Me.InvokeRequired Then

            Dim d As New d_UpdateCount(AddressOf UpdateCount)
            Me.Invoke(d, New Object() {process})
        Else
            SyncLock countlock
                If (i_Count <= 1) Then
                    i_Count = 0
                Else
                    i_Count -= 1
                End If

                RaiseEvent eUpdateStatus("Symbols left to " & process & ": " & i_Count)
            End SyncLock
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