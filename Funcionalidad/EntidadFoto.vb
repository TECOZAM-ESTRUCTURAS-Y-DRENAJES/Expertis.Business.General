Public Class EntidadFoto

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbEntidadFoto"

#End Region

#Region "Eventos RegisterDeleteTasks"

    Protected Overrides Sub RegisterDeleteTasks(ByVal deleteProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterDeleteTasks(deleteProcess)
        deleteProcess.AddTask(Of DataRow)(AddressOf Comunes.DeleteEntityRow)
        deleteProcess.AddTask(Of DataRow)(AddressOf Comunes.MarcarComoEliminado)
        deleteProcess.AddTask(Of DataRow)(AddressOf TratarDeletePredeterminado)
    End Sub

    <Task()> Public Shared Sub TratarDeletePredeterminado(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data("Predeterminado") Then
            Dim FilFoto As New Filter
            FilFoto.Add("Entidad", FilterOperator.Equal, data("Entidad"))
            FilFoto.Add("Clave", FilterOperator.Equal, data("Clave"))
            Dim ClsEntFoto As New EntidadFoto
            Dim DtFotos As DataTable = ClsEntFoto.Filter(FilFoto, "IDEntidadFoto")
            If Not DtFotos Is Nothing AndAlso DtFotos.Rows.Count > 0 Then
                DtFotos.Rows(0)("Predeterminado") = True
                ClsEntFoto.Update(DtFotos)
            End If
        End If
    End Sub

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDatosObligatorios)
    End Sub

    <Task()> Public Shared Sub ValidarDatosObligatorios(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("Entidad")) = 0 Then ApplicationService.GenerateError("El Campo Entidad es un dato obligatorio.")
        If Length(data("Clave")) = 0 Then ApplicationService.GenerateError("El Campo Clave es un dato obligatorio.")
    End Sub

#End Region

#Region "Eventos RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarClavePrimaria)
        updateProcess.AddTask(Of DataRow)(AddressOf TratarAltaPredeterminado)
        updateProcess.AddTask(Of DataRow)(AddressOf Comunes.UpdateEntityRow)
        updateProcess.AddTask(Of DataRow)(AddressOf Comunes.MarcarComoActualizado)
        updateProcess.AddTask(Of DataRow)(AddressOf TratarModifPredeterminado)
    End Sub

    <Task()> Public Shared Sub AsignarClavePrimaria(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            If Length(data("IDEntidadFoto")) = 0 Then data("IDEntidadFoto") = AdminData.GetAutoNumeric
        End If
    End Sub

    <Task()> Public Shared Sub TratarAltaPredeterminado(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim FilFoto As New Filter
            FilFoto.Add("Entidad", FilterOperator.Equal, data("Entidad"))
            FilFoto.Add("Clave", FilterOperator.Equal, data("Clave"))
            Dim DtFotos As DataTable = New EntidadFoto().Filter(FilFoto)
            If DtFotos Is Nothing OrElse DtFotos.Rows.Count = 0 Then
                data("Predeterminado") = True
            End If
        End If
    End Sub

    <Task()> Public Shared Sub TratarModifPredeterminado(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Modified Then
            Dim DataEnt As New EntidadFotoInfo
            DataEnt.Entidad = data("Entidad")
            DataEnt.Clave = data("Clave")
            DataEnt.IDEntidadFoto = data("IDEntidadFoto")
            ProcessServer.ExecuteTask(Of EntidadFotoInfo)(AddressOf GrabarPredeterminado, DataEnt, services)
        End If
    End Sub

#End Region

#Region "Funciones Públicas"

    <Task()> Public Shared Sub GrabarPredeterminado(ByVal DataEntidad As EntidadFotoInfo, ByVal services As ServiceProvider)
        Dim StrSql As String = "UPDATE tbEntidadFoto SET Predeterminado = 0 "
        StrSql &= "WHERE Entidad = '" & DataEntidad.Entidad & "' AND Clave = '" & DataEntidad.Clave & "'"
        AdminData.Execute(StrSql)

        StrSql = "UPDATE tbEntidadFoto SET Predeterminado = 1 WHERE IDEntidadFoto = " & DataEntidad.IDEntidadFoto
        AdminData.Execute(StrSql)
    End Sub

    <Task()> Public Shared Function ObtenerEntidades(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Return New BE.DataEngine().Filter("xEntity", "Tabla, Entidad", "", , , True)
    End Function

    <Task()> Public Shared Function ObtenerFotosEntidad(ByVal data As EntidadFotoInfo, ByVal services As ServiceProvider) As DataTable
        If Not data Is Nothing Then
            Dim FilEntity As New Filter
            Dim f As New Filter
            If Length(data.Entidad) AndAlso data.Entidad <> String.Empty Then FilEntity.Add("Entidad", FilterOperator.Equal, data.Entidad)
            If Length(data.Clave) > 0 AndAlso data.Clave <> String.Empty Then FilEntity.Add("Clave", FilterOperator.Equal, data.Clave)
            If data.FiltrarIDEntidadFoto Then
                If Length(data.IDEntidadFoto) > 0 AndAlso data.IDEntidadFoto <> 0 Then FilEntity.Add("IDEntidadFoto", FilterOperator.Equal, data.IDEntidadFoto)
            End If
            If data.Distintos Then
                Dim StrSQl As String = "SELECT DISTINCT Entidad, Clave "
                StrSQl &= "FROM tbEntidadFoto "
                If FilEntity.Count > 0 Then StrSQl &= "WHERE " & AdminData.ComposeFilter(FilEntity)
                Return AdminData.Execute(StrSQl, ExecuteCommand.ExecuteReader)
            ElseIf FilEntity.Count > 0 Then
                Return New EntidadFoto().Filter(FilEntity, "Predeterminado DESC, IDEntidadFoto")
            Else
                Return New EntidadFoto().Filter(f)
            End If
        End If
    End Function

    <Task()> Public Shared Function GetPrimaryKeyTable(ByVal data As String, ByVal services As ServiceProvider) As DataTable
        Try
            Dim ClsEnt As BE.BusinessHelper = BE.BusinessHelper.CreateBusinessObject(data)
            Return ClsEnt.PrimaryKeyTable
        Catch ex As Exception
            ApplicationService.GenerateError("Ocurrió un error al intentar recuperar la información de la entidad: |.|Por favor, revise la información de entidades en el sistema.", data, vbNewLine)
        End Try
    End Function

#End Region

End Class

<Serializable()> _
Public Class EntidadFotoInfo

    Public IDEntidadFoto As Integer
    Public FiltrarIDEntidadFoto As Boolean = False
    Public Entidad As String
    Public Clave As String
    Public Distintos As Boolean = False

End Class