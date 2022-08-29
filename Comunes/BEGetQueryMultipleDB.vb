
<Transactional()> _
Public Class BEGetQueryMultipleDB
    Inherits ContextBoundObject

    <Serializable()> _
    Public Class DataGetQueryMultipleDB
        Public ViewName As String = String.Empty
        Public FilterCriteria As IFilter
        Public FilterCriteriaEmpresas As Filter
        Public blnMultiple As Boolean = True
        Public blnReemplazaEsquema As Boolean = False
        Public blnVista As Boolean

        Public Sub New(ByVal ViewName As String, _
                        ByVal FilterCriteria As IFilter, _
                        ByVal FilterCriteriaEmpresas As IFilter, _
                        ByVal blnMultiple As Boolean)
            Me.ViewName = ViewName
            Me.FilterCriteria = FilterCriteria
            Me.FilterCriteriaEmpresas = FilterCriteriaEmpresas
            Me.blnMultiple = blnMultiple
            If Left(Me.ViewName, 6) = "SELECT" Then
                Me.blnVista = False
                Me.blnReemplazaEsquema = Me.ViewName.Contains("Esquema.")
            Else
                Me.blnVista = True
            End If
        End Sub
    End Class

    Public Function Execute(ByVal data As DataGetQueryMultipleDB) As DataTable
        If Length(data.ViewName) = 0 Then
            Exit Function
        End If

        Dim services As New ServiceProvider
        Dim strEsquemaOriginal As String = ProcessServer.ExecuteTask(Of Object, String)(AddressOf General.Comunes.GetEsquemaBD, Nothing, services)
        Dim strTextoEsquema As String = "Esquema."

        Dim BEDataEngine As New BE.DataEngine
        Dim dtResult As DataTable = Nothing

        Dim strWhereEmpresas As String = ""
        If data.blnMultiple Then
            If Not data.FilterCriteriaEmpresas Is Nothing AndAlso data.FilterCriteriaEmpresas.Count > 0 Then
                strWhereEmpresas = data.FilterCriteriaEmpresas.Compose(New AdoFilterComposer)
            End If
        Else
            Dim fBBDD As New Filter
            fBBDD.Add(New GuidFilterItem("IDBaseDatos", AdminData.GetConnectionInfo.IDDataBase))
            strWhereEmpresas = fBBDD.Compose(New AdoFilterComposer)
        End If

        Dim dtBBDD As DataTable = AdminData.GetUserDataBases
        For Each drBBDD As DataRow In dtBBDD.Select(strWhereEmpresas)
            '//Cambiar BBDD
            AdminData.SetCurrentConnection(drBBDD("IDBaseDatos"))

            Dim strSQL As String = "SELECT " & Quoted(drBBDD("IDBaseDatos").ToString) & " AS IDBaseDatos," & Quoted(drBBDD("DescBaseDatos")) & " AS Empresa, * "
            Dim strViewNameActual As String = data.ViewName
            Dim strEsquemaActual As String = ProcessServer.ExecuteTask(Of Object, String)(AddressOf General.Comunes.GetEsquemaBD, Nothing, services)
            If Not data.blnVista Then
                If data.blnReemplazaEsquema Then
                    strViewNameActual = data.ViewName.Replace(strTextoEsquema, strEsquemaActual & ".")
                End If
                strSQL &= " FROM (" & strViewNameActual & ") AS SubSelect"
            Else
                strSQL &= " FROM " & strEsquemaActual & "." & strViewNameActual
            End If
            Dim dt As DataTable = BEDataEngine.Filter(strSQL, data.FilterCriteria)
            If dt.Rows.Count > 0 Then
                If dtResult Is Nothing Then
                    dtResult = dt.Copy
                Else
                    For Each dr As DataRow In dt.Rows
                        dtResult.ImportRow(dr)
                    Next
                End If
            Else
                If dtResult Is Nothing Then
                    dtResult = dt.Copy
                End If
            End If
        Next
        AdminData.SetCurrentConnection(AdminData.GetConnectionInfo.IDDataBase)

        Return dtResult
    End Function
End Class