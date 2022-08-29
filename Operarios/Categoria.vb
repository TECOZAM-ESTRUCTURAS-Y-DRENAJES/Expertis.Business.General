Public Class Categoria

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroCategoria"

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarCategoria)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescCategoria)
    End Sub

    <Task()> Public Shared Sub ValidarCategoria(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDCategoria")) = 0 Then ApplicationService.GenerateError("La Categoría es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dtCat As DataTable = New Categoria().SelOnPrimaryKey(data("IDCategoria"))
            If Not dtCat Is Nothing AndAlso dtCat.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe una categoría con el mismo código")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarDescCategoria(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescCategoria")) = 0 Then ApplicationService.GenerateError("La descripción de la categoría es un dato obligatorio.")
    End Sub

#End Region

#Region "Funciones Públicas"

    <Task()> Public Shared Function ValidaCategoria(ByVal data As String, ByVal services As ServiceProvider) As DataTable
        Dim dt As DataTable = New Categoria().SelOnPrimaryKey(data)
        If IsNothing(dt) OrElse dt.Rows.Count = 0 Then
            ApplicationService.GenerateError("La Categoría | no existe.", data)
        End If
        Return dt
    End Function

#End Region

End Class