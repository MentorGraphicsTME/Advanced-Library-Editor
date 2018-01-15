Class NodeCount

    'Boolean
    Property CurrentCheckedState As Boolean = False
    Property NewCheckedState As Boolean = False

    'Event
    Public Event CheckNode(ByVal node As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)
    Event eUpdateNodesFinished()

    Sub Update(ByVal e As TreeNode)

        RaiseEvent CheckNode(e, CurrentCheckedState, NewCheckedState)

        If e.Nodes.Count > 0 Then
            TraverseChilderen(e, CurrentCheckedState, NewCheckedState)
        Else
            If NewCheckedState = False Then
                TraverseParents(e.Parent, CurrentCheckedState, NewCheckedState)
            End If
        End If

        RaiseEvent eUpdateNodesFinished()
    End Sub

    Function TraverseParents(ByVal node As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)

        If IsNothing(node) Then
            Return Nothing
        End If

        RaiseEvent CheckNode(node, CurrentCheckedState, NewCheckedState)

        Return TraverseParents(node.Parent, CurrentCheckedState, NewCheckedState)

    End Function

    Function TraverseChilderen(ByVal node As TreeNode, ByVal CurrentCheckedState As Boolean, ByVal NewCheckedState As Boolean)

        RaiseEvent CheckNode(node, CurrentCheckedState, NewCheckedState)

        If node.Nodes.Count = 0 Then
            Return Nothing
        End If

        For Each nodeChild As TreeNode In node.Nodes
            TraverseChilderen(nodeChild, CurrentCheckedState, NewCheckedState)
        Next

        Return Nothing

    End Function

End Class

Class CheckNode

    Event CheckNode(ByVal node As TreeNode, ByVal check As Boolean)
    Event CheckNodesComplete()

    Sub Update(ByVal e As TreeNode)
        If e.Level = 0 Then
            For Each oNode As TreeNode In e.Nodes
                Dim checked As Boolean = e.Checked
                If e.Checked = True And oNode.Checked = False Then

                    RaiseEvent CheckNode(oNode, True)

                ElseIf e.Checked = False And oNode.Checked = True Then

                    RaiseEvent CheckNode(oNode, False)

                End If

            Next
        Else
            If e.Checked = False Then
                RaiseEvent CheckNode(e.Parent, False)
            End If

        End If

        RaiseEvent CheckNodesComplete()
    End Sub

End Class