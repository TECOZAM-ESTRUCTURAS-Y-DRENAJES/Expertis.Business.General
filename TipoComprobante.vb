Public Class TipoComprobante
    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

#Region " Constructor "

    Private Const cnEntidad As String = "tbMaestroTipoComprobante"

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

#End Region

#Region " RegisterValidateTaks "

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Solmicro.Expertis.Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDatosObligatorios)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClavePrimaria)
    End Sub


    <Task()> Public Shared Sub ValidarDatosObligatorios(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDTipoComprobante")) = 0 Then ApplicationService.GenerateError("El Tipo de Comprobante es un dato obligatorio.")
        If Length(data("DescTipoComprobante")) = 0 Then ApplicationService.GenerateError("La descripción es un dato obligatorio.")
    End Sub


    <Task()> Public Shared Sub ValidarClavePrimaria(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dt As DataTable = New TipoComprobante().SelOnPrimaryKey(data("IDTipoComprobante"))
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ApplicationService.GenerateError("El registro introducido ya existe.")
            End If
        End If
    End Sub

#End Region
End Class
