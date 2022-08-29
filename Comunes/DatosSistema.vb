Public Class DatosSistema

    <Task()> Public Shared Function GetEnumerados(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Return New BE.DataEngine().Filter("xEnumerate", "DISTINCT NombEnum", "", "NombEnum", , True)
    End Function

    <Task()> Public Shared Function GetEntitys(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Return New Engine.BE.DataEngine().Filter("xEntity", Nothing, , "Entidad", , True)
    End Function

    <Task()> Public Shared Function GetTablas(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Dim f As New Filter
        f.Add(New IsNullFilterItem("Tabla", False))
        f.Add(New IsNullFilterItem("Entidad", False))

        Return New BE.DataEngine().Filter("xEntity", f, "Tabla,Entidad", "Tabla", , True)
    End Function

    <Task()> Public Shared Function GetCampos(ByVal Entity As String, ByVal services As ServiceProvider) As DataTable
        Dim d As New Engine.BE.DataEngine
        Dim dtEntity As DataTable = d.Filter("xEntity", New StringFilterItem("Entidad", Entity), , , , True)
        Dim dtCampos As DataTable = Nothing
        If Not dtEntity Is Nothing AndAlso dtEntity.Rows.Count > 0 Then
            If Length(dtEntity.Rows(0)("Tabla")) > 0 Then
                dtCampos = d.Filter("xViewCampos", New StringFilterItem("Tabla", dtEntity.Rows(0)("Tabla")))
            End If
        End If
        Return dtCampos
    End Function

    <Task()> Public Shared Function GetTablasHijas(ByVal Tabla As String, ByVal services As ServiceProvider) As DataTable
        Return New BE.DataEngine().Filter("xViewRelaciones", New StringFilterItem("TablaPrim", Tabla), "DISTINCT TablaPrim,TablaRel", "TablaRel", , True)
    End Function

    <Task()> Public Shared Function GetMaquinas(ByVal f As Filter, ByVal services As ServiceProvider) As DataTable
        Return New Engine.BE.DataEngine().Filter("xMachine", f, "IDMaquina, DescMaquina", , False, True)
    End Function

    <Task()> Public Shared Function DevuelveUsuariosBD(ByVal data As Filter, ByVal services As ServiceProvider) As DataTable
        Dim EntID As Guid = AdminData.GetSessionInfo.Enterprise.EnterpriseID
        If IsNothing(data) OrElse data.Count = 0 Then data = New Filter
        data.Add(New GuidFilterItem("IDEmpresa", FilterOperator.Equal, EntID))
        Return New BE.DataEngine().Filter("xUser", data, "IDUsuario, CUsuario, IDGrupo", "CUsuario", , True)
    End Function

    <Task()> Public Shared Function GetBDsSistema(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Dim EntID As Guid = AdminData.GetSessionInfo.Enterprise.EnterpriseID
        Return New BE.DataEngine().Filter("xDataBase", New GuidFilterItem("IDEmpresa", FilterOperator.NotEqual, EntID), "IDBaseDatos, BaseDatos", "BaseDatos", , True)
    End Function

    <Task()> Public Shared Function GetDataBases(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Return New BE.DataEngine().Filter("xDataBase", "IDBaseDatos, BaseDatos, DescBaseDatos", String.Empty, "BaseDatos", , True)
    End Function

    <Task()> Public Shared Function GetRolesSistema(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Dim EntID As Guid = AdminData.GetSessionInfo.Enterprise.EnterpriseID
        Return New BE.DataEngine().Filter("xGroup", New GuidFilterItem("IDEmpresa", FilterOperator.Equal, EntID), "IDGrupo, DescGrupo", "DescGrupo", , True)
    End Function
End Class
