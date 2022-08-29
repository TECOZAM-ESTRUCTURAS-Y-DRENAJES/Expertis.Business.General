Public Class ObraCabeceraInfo
    Inherits ClassEntityInfo

    Public IDObra As Integer
    Public NObra As String
    Public DescObra As String
    Public IDFormaPago As String
    Public IDCondicionPago As String
    Public IDDiaPago As String
    Public IDMoneda As String
    Public IDTipoIva As String
    Public CambioA As Double
    Public CambioB As Double
    Public IDCliente As String
    Public IDClienteBanco As Integer
    Public NumeroPedido As String
    Public IDDireccion As Integer
    Public IDCentroGestion As String
    Public IDTipoObra As String
    Public TipoGeneracionSeguros As Integer
    Public FacturarTasaResiduos As Boolean
    Public SeguroCambio As Boolean
    Public FechaInicio As Date

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal data As DataRow)
        MyBase.New(data)
    End Sub

    Public Overloads Overrides Sub Fill(ByVal ParamArray PrimaryKey() As Object)
        Dim dt As DataTable = Nothing
        If Not IsNothing(PrimaryKey) AndAlso PrimaryKey.Length > 0 AndAlso Length(PrimaryKey(0)) > 0 Then
            dt = New BE.DataEngine().Filter("tbObraCabecera", New NumberFilterItem("IDObra", PrimaryKey(0)))
        End If

        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Me.Fill(dt.Rows(0))
        End If
    End Sub

End Class
