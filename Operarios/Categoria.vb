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
        If Length(data("IDCategoria")) = 0 Then ApplicationService.GenerateError("La Categor�a es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dtCat As DataTable = New Categoria().SelOnPrimaryKey(data("IDCategoria"))
            If Not dtCat Is Nothing AndAlso dtCat.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe una categor�a con el mismo c�digo")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarDescCategoria(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescCategoria")) = 0 Then ApplicationService.GenerateError("La descripci�n de la categor�a es un dato obligatorio.")
    End Sub

#End Region

#Region "Funciones P�blicas"

    <Task()> Public Shared Function ValidaCategoria(ByVal data As String, ByVal services As ServiceProvider) As DataTable
        Dim dt As DataTable = New Categoria().SelOnPrimaryKey(data)
        If IsNothing(dt) OrElse dt.Rows.Count = 0 Then
            ApplicationService.GenerateError("La Categor�a | no existe.", data)
        End If
        Return dt
    End Function

#End Region

End Class