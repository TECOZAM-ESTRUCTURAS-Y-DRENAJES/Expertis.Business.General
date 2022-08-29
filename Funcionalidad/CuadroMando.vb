Public Class CuadroMando

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper
    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbConfigsCuadroMando"

#End Region

#Region "Eventos RegisterAddNewTasks"

    Protected Overrides Sub RegisterAddnewTasks(ByVal addnewProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterAddnewTasks(addnewProcess)
        addnewProcess.AddTask(Of DataRow)(AddressOf AltaClavePrimaria)
    End Sub

    <Task()> Public Shared Sub AltaClavePrimaria(ByVal data As DataRow, ByVal services As ServiceProvider)
        data("IDConfig") = AdminData.GetAutoNumeric
    End Sub

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDatosObligatorios)
    End Sub

    <Task()> Public Shared Sub ValidarDatosObligatorios(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescConfig")) = 0 Then ApplicationService.GenerateError("La Descripción de la Configuración es Obligatoria.")
    End Sub

#End Region

#Region "Eventos RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarClavePrimaria)
    End Sub

    <Task()> Public Shared Sub AsignarClavePrimaria(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added AndAlso Length(data("IDConfig")) = 0 Then
            data("IDConfig") = AdminData.GetAutoNumeric
        End If
    End Sub

#End Region

#Region "Funciones Públicas"

    <Task()> Public Shared Function GetGridData(ByVal data As String, ByVal services As ServiceProvider) As DataTable
        Dim DtDatos As DataTable = AdminData.Execute(data, ExecuteCommand.ExecuteReader)
        Return DtDatos
    End Function

    <Task()> Public Shared Function GetGridDataIDGrafico(ByVal data As Integer, ByVal services As ServiceProvider) As DataTable
        Dim DtDatos As DataTable = New Graficos().Filter(New FilterItem("IDGrafico", FilterOperator.Equal, data))
        If Not DtDatos Is Nothing AndAlso DtDatos.Rows.Count > 0 Then
            Return ProcessServer.ExecuteTask(Of String, DataTable)(AddressOf GetGridData, DtDatos.Rows(0)("Sentencia"), services)
        End If
    End Function

#End Region

End Class