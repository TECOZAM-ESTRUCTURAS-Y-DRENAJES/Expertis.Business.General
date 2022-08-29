Public Class Grupo
    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroGrupo"

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarIDGrupo)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescGrupo)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarIDHora)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarIDGrupo(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDGrupo")) = 0 Then ApplicationService.GenerateError("El Grupo es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarDescGrupo(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescGrupo")) = 0 Then ApplicationService.GenerateError("La descripción es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarIDHora(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDHora")) = 0 Then ApplicationService.GenerateError("El tipo Hora es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dt As DataTable = New Grupo().SelOnPrimaryKey(data("IDGrupo"))
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ApplicationService.GenerateError("Este Grupo ya existe en la base de datos.")
            End If
        End If
    End Sub

#End Region

End Class
