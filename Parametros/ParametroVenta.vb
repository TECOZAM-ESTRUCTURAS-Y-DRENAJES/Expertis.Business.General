Public Class ParametroVenta
    Private mIVACliente As Boolean?
    Private mComprasEmpresasGrupo As Boolean?
    Private mGeneral As ParametroGeneral
    Private mPermisoExpedicion As Boolean?
    Private mIDContadorProvisionalTPV As String
    Private mPedidoConfirmado As Boolean?
    Private mExpedirPedidosConfirmados As Boolean?
    Private mIvaCajaVentas As Boolean?
    Private mTPVFactura As Boolean?

    Public Sub New()
    End Sub

    Public ReadOnly Property IVACliente() As Boolean
        Get
            If mIVACliente Is Nothing Then mIVACliente = New Parametro().IVACliente
            Return mIVACliente
        End Get
    End Property
    Public ReadOnly Property General() As ParametroGeneral
        Get
            If mGeneral Is Nothing Then mGeneral = New ParametroGeneral
            Return mGeneral
        End Get
    End Property

    Public ReadOnly Property ComprasEmpresasGrupo() As Boolean
        Get
            If mComprasEmpresasGrupo Is Nothing Then mComprasEmpresasGrupo = New Parametro().ComprasEmpresasGrupo
            Return mComprasEmpresasGrupo
        End Get
    End Property

    Public ReadOnly Property PermisoExpedicion() As Boolean
        Get
            If mPermisoExpedicion Is Nothing Then mPermisoExpedicion = New Parametro().PermisoExpedicion
            Return mPermisoExpedicion
        End Get
    End Property

    Public ReadOnly Property ContadorProvisionalTPV() As String
        Get
            If mIDContadorProvisionalTPV Is Nothing Then mIDContadorProvisionalTPV = New Parametro().ContadorProvisionalTPV
            Return mIDContadorProvisionalTPV
        End Get
    End Property

    Public ReadOnly Property PedidoConfirmado() As Boolean
        Get
            If mPedidoConfirmado Is Nothing Then mPedidoConfirmado = New Parametro().PedidoConfirmado
            Return mPedidoConfirmado
        End Get
    End Property

    Public ReadOnly Property ExpedirPedidosConfirmados() As Boolean
        Get
            If mExpedirPedidosConfirmados Is Nothing Then mExpedirPedidosConfirmados = New Parametro().ExpedirPedidosConfirmados
            Return mExpedirPedidosConfirmados
        End Get
    End Property

    Public ReadOnly Property IvaCajaCircuitoVentas() As Boolean
        Get
            If mIvaCajaVentas Is Nothing Then mIvaCajaVentas = New Parametro().IvaCajaCircuitoVentas
            Return mIvaCajaVentas
        End Get
    End Property

    Public ReadOnly Property TPVFactura() As Boolean
        Get
            If mTPVFactura Is Nothing Then mTPVFactura = New Parametro().TPVGestionPorFacturas
            Return mTPVFactura
        End Get
    End Property

End Class