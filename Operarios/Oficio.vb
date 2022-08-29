Public Class Oficio

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroOficio"

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarOficio)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescOficio)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarOficio(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDOficio")) = 0 Then ApplicationService.GenerateError("El Oficio es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarDescOficio(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescOficio")) = 0 Then ApplicationService.GenerateError("La descripción es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dt As DataTable = New Oficio().SelOnPrimaryKey(data("IDOficio"))
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ApplicationService.GenerateError("Este Oficio ya existe en la base de datos.")
            End If
        End If
    End Sub

#End Region

#Region "Funciones Públicas"

    <Task()> Public Shared Function ValidaOficio(ByVal data As String, ByVal services As ServiceProvider) As DataTable
        If Length(data) Then
            Dim dt As DataTable = New Oficio().SelOnPrimaryKey(data)
            If IsNothing(dt) OrElse dt.Rows.Count = 0 Then
                ApplicationService.GenerateError("El Oficio | no existe.", data)
            End If
            Return dt
        End If
    End Function

#End Region

End Class