Public Class GAIADirectorios

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbGAIADirectorios"

#End Region

#Region "Eventos Entidad"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDatosObligatorios)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarFormatoDuplicado)
    End Sub

    <Task()> Public Shared Sub ValidarDatosObligatorios(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescDirectorio")) = 0 Then ApplicationService.GenerateError("La Descripción es obligatoria.")
        If Length(data("RutaDirectorio")) = 0 Then ApplicationService.GenerateError("La Ruta es obligatoria.")
    End Sub

    <Task()> Public Shared Sub ValidarFormatoDuplicado(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added OrElse data.RowState = DataRowState.Modified Then
            Dim FilFormato As New Filter
            FilFormato.Add("FormatoFichero", FilterOperator.Equal, data("FormatoFichero"))
            FilFormato.Add("IDDirectorio", FilterOperator.NotEqual, data("IDDirectorio"))
            Dim DtFormato As DataTable = New GAIADirectorios().Filter(FilFormato)
            If Not DtFormato Is Nothing AndAlso DtFormato.Rows.Count > 0 Then
                ApplicationService.GenerateError("El Formato | ya está agregado a la lista.", data("DescDirectorio"))
            End If
        End If
    End Sub

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarClavePrimaria)
    End Sub

    <Task()> Public Shared Sub AsignarClavePrimaria(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            If Length(data("IDDirectorio")) = 0 Then data("IDDirectorio") = AdminData.GetAutoNumeric
        End If
    End Sub

#End Region

End Class