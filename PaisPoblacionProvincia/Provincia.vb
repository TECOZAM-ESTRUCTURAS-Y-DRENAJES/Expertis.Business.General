Public Class Provincia

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroProvincia"

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescProvincia)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarPais)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarProvincia)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarDescProvincia(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescProvincia")) = 0 Then ApplicationService.GenerateError("La Descripción de la provincia es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarPais(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDPais")) = 0 Then ApplicationService.GenerateError("El País es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarProvincia(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDProvincia")) = 0 Then ApplicationService.GenerateError("El Código de provincia es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dt As DataTable = New Provincia().SelOnPrimaryKey(data("IDPais"), data("IDProvincia"))
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe este Código en la base de datos.")
            End If
        End If
    End Sub

#End Region

End Class