Public Class TipoTurno

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbTipoTurno"

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarIDTipoTurno)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescTipoTurno)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarIDTipoTurno(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDTipoTurno")) = 0 Then ApplicationService.GenerateError("El Tipo Turno es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarDescTipoTurno(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescTipoTurno")) = 0 Then ApplicationService.GenerateError("La descripción es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim DtTemp As DataTable = New TipoTurno().SelOnPrimaryKey(data("IDTipoTurno"))
            If Not DtTemp Is Nothing AndAlso DtTemp.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe un Tipo Turno con este código.")
            End If
        End If
    End Sub

#End Region

#Region "Eventos RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarColor)
    End Sub

    <Task()> Public Shared Sub AsignarColor(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("Color")) = 0 Then data("Color") = 0
    End Sub

#End Region

End Class