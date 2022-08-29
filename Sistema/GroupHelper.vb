Public Class GroupHelper
    Private mColumns() As DataColumn
    Private mGroups As New Hashtable(New System.Collections.CaseInsensitiveHashCodeProvider, New System.Collections.CaseInsensitiveComparer)
    Private mGroupUser As IGroupUser

    Public Sub New(ByVal columns() As DataColumn, ByVal GroupUser As IGroupUser)
        mColumns = columns
        mGroupUser = GroupUser
    End Sub
    Public Sub Group(ByVal data As DataTable)
        For Each oRow As DataRow In data.Rows
            Group(oRow)
        Next
    End Sub
    Public Sub Group(ByVal oRow As DataRow)

        Dim strKey As String = GetKey(oRow)

        Dim obj As Object = mGroups(strKey)
        If obj Is Nothing Then
            obj = mGroupUser.NewGroupObject(oRow)
            mGroups.Add(strKey, obj)
        Else
            mGroupUser.AddToGroupObject(oRow, obj)
        End If
    End Sub

    Private Function GetKey(ByVal oRow As DataRow) As String
        Dim strKey As New System.Text.StringBuilder
        For Each oCol As DataColumn In mColumns
            Dim strVal As String
            If TypeOf oRow(oCol) Is DBNull Then
                strVal = "null"
            ElseIf TypeOf oRow(oCol) Is Date Then
                strVal = CDate(oRow(oCol)).ToString("yyyyMMdd")
            ElseIf TypeOf oRow(oCol) Is Guid Then
                strVal = CType(oRow(oCol), Guid).ToString
            Else
                strVal = CStr(oRow(oCol))
            End If
            strKey.Append(strVal)
            strKey.Append(",")
        Next
        Return strKey.ToString
    End Function

    Public Function GetGroups() As Object()
        Dim rslt(mGroups.Count - 1) As Object
        mGroups.Values.CopyTo(rslt, 0)
        Return rslt
    End Function
End Class