'Public Class EMCSCodigos
'    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper
'    Public Sub New()
'        MyBase.New(cnEntidad)
'    End Sub

'    Private Const cnEntidad As String = "tbEMCSCodigos"

'    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
'        MyBase.RegisterValidateTasks(validateProcess)
'        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDatosObligatorios)
'    End Sub

'    <Task()> Public Shared Sub ValidarDatosObligatorios(ByVal data As DataRow, ByVal services As ServiceProvider)
'        If Length(data("TipoCodigo")) = 0 Then ApplicationService.GenerateError("El Tipo C�digo es un dato obligatorio.")
'        If Length(data("IDCodigo")) = 0 Then ApplicationService.GenerateError("El C�digo es un dato obligatorio.")
'        If Length(data("DescCodigo")) = 0 Then ApplicationService.GenerateError("La descripci�n es un dato obligatorio.")
'        If Length(data("FechaInicio")) = 0 Then ApplicationService.GenerateError("La Fecha Inicio es un dato obligatorio.")
'        If data.RowState = DataRowState.Added Then
'            Dim dt As DataTable = New EMCSCodigos().SelOnPrimaryKey(data("IDCodigo"), data("TipoCodigo"))
'            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
'                ApplicationService.GenerateError("El C�digo ya existe en la base de datos para ese Tipo C�digo.")
'            End If
'        End If
'    End Sub

'End Class