Public Class OperarioHora

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbOperarioHora"

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarOperario)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarHora)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarOperario(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDOperario")) = 0 Then ApplicationService.GenerateError("El Operario es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarHora(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDHora")) = 0 Then ApplicationService.GenerateError("Hora es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim ofilter As New Filter
            ofilter.Add("IDOperario", FilterOperator.Equal, data("IDOperario"))
            ofilter.Add("IDHora", FilterOperator.Equal, data("IDHora"))
            Dim dtOperarioHora As DataTable = New OperarioHora().Filter(ofilter)
            If Not dtOperarioHora Is Nothing AndAlso dtOperarioHora.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe una Tasa para la Hora '|'", data("IDHora"))
            End If
        End If
    End Sub

#End Region

#Region "Eventos RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarOperarioHora)
    End Sub

    <Task()> Public Shared Sub AsignarOperarioHora(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            If Length(data("IDOperariohora")) = 0 Then data("IDOperariohora") = AdminData.GetAutoNumeric
        End If
    End Sub

#End Region

#Region "Eventos GetBusinessRules"

    Public Overrides Function GetBusinessRules() As Engine.BE.BusinessRules
        Dim oBrl As New BusinessRules
        oBrl.Add("IDHora", AddressOf CambioHora)
        Return oBrl
    End Function

    <Task()> Public Shared Sub CambioHora(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        If Length(data.Value) > 0 Then ProcessServer.ExecuteTask(Of String)(AddressOf Hora.ValidaHora, data.Value, services)
    End Sub

#End Region

#Region "Funciones Públicas"

    <Serializable()> _
    Public Class DatosTasa
        Public IDOperario As String
        Public IDHora As String
    End Class

    <Task()> Public Shared Function GetTasa(ByVal data As DatosTasa, ByVal services As ServiceProvider) As Double
        Dim f As New Filter
        f.Add("IDOperario", FilterOperator.Equal, data.IDOperario)
        f.Add("IDHora", FilterOperator.Equal, data.IDHora)
        Dim dtOH As DataTable = New OperarioHora().Filter(f)
        If Not IsNothing(dtOH) AndAlso dtOH.Rows.Count > 0 Then
            Return Nz(dtOH.Rows(0)("TasaHorariaA"), 0)
        Else : Return 0
        End If
    End Function

#End Region

End Class