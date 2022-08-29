Public Class HistoricoTasaOperarioHora

#Region "Constructor"

    Inherits BE.BusinessHelper

    Public Const cnEntidad As String = "tbHistoricoTasaOperarioHora"

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

#End Region

#Region "Eventos ValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDatosObligatorios)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDatosRelaciones)
    End Sub

    <Task()> Public Shared Sub ValidarDatosObligatorios(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDHora")) = 0 Then ApplicationService.GenerateError("La Hora es un dato obligatorio.")
        If Length(data("IDOperario")) = 0 Then ApplicationService.GenerateError("El Operario es un dato obligatorio.")
        If Length(data("TasaHorariaA")) = 0 Then ApplicationService.GenerateError("La Tasa Horaria es un dato obligatorio.")
        If Length(data("FechaDesde")) = 0 Then ApplicationService.GenerateError("La Fecha Desde es un dato obligatorio.")
        If Length(data("FechaHasta")) = 0 Then ApplicationService.GenerateError("La Fecha Hasta es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarDatosRelaciones(ByVal data As DataRow, ByVal services As ServiceProvider)
        Dim DtHora As DataTable = New Hora().SelOnPrimaryKey(data("IDHora"))
        If DtHora Is Nothing OrElse DtHora.Rows.Count = 0 Then
            ApplicationService.GenerateError("La Hora | no existe en la base de datos.", data("IDHora"))
        End If

        Dim DtOperario As DataTable = New Operario().SelOnPrimaryKey(data("IDOperario"))
        If DtOperario Is Nothing OrElse DtOperario.Rows.Count = 0 Then
            ApplicationService.GenerateError("El Operario | no existe en la base de datos.", data("IDOperario"))
        End If
    End Sub

#End Region

#Region "Eventos UpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarClavePrimaria)
    End Sub

    <Task()> Public Shared Sub AsignarClavePrimaria(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            If Length(data("IDHistoricoTasa")) = 0 Then data("IDHistoricoTasa") = AdminData.GetAutoNumeric
        End If
    End Sub

#End Region

End Class