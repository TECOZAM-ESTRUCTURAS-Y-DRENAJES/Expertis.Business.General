Public Class Hora

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroHora"

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarHora)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescHora)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarHora(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDHora")) = 0 Then ApplicationService.GenerateError("La Hora es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarDescHora(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescHora")) = 0 Then ApplicationService.GenerateError("La descripción es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dt As DataTable = New Hora().SelOnPrimaryKey(data("IDHora"))
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ApplicationService.GenerateError("La Hora introducida ya existe.")
            End If
        End If
    End Sub

#End Region

#Region "Funciones Públicas"

    <Task()> Public Shared Function ValidaHora(ByVal data As String, ByVal services As ServiceProvider) As DataTable
        Dim dt As DataTable = New Hora().SelOnPrimaryKey(data)
        If IsNothing(dt) OrElse dt.Rows.Count = 0 Then
            ApplicationService.GenerateError("La Hora | no existe.", data)
        End If

        Return dt
    End Function

#End Region

End Class