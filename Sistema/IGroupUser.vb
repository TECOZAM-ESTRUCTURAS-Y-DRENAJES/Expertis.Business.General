Public Interface IGroupUser
    Function NewGroupObject(ByVal oRow As DataRow) As Object
    Sub AddToGroupObject(ByVal oRow As DataRow, ByVal Group As Object)
End Interface