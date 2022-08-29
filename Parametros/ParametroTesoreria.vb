Public Class ParametroTesoreria
    Private mTipoCobroFV As Integer?
    Private mTipoPagoFC As Integer?
    Private mTipoCobroDesdePago As Integer?
    Private mTipoPagoDesdeCobro As Integer?
    Private mTipoAlbaran As String
    Private mTipoAsientoRemesa As enumTipoAsientoRemesa?
    Private mContadorRemesa As String
    Private mContadorRemesaAnticipo As String
    Private mContadorPagare As String
    Private mNFacturaCobroAgupado As String
    Private mAlmacenPortes As String

    Public ReadOnly Property TipoAlbaranPorDefecto() As String
        Get
            If mTipoAlbaran Is Nothing Then mTipoAlbaran = New Parametro().TipoAlbaranPorDefecto
            Return mTipoAlbaran
        End Get
    End Property

    Public ReadOnly Property TipoCobroFV() As Integer
        Get
            If mTipoCobroFV Is Nothing Then mTipoCobroFV = New Parametro().CTipoCobroFV
            Return mTipoCobroFV
        End Get
    End Property

    Public ReadOnly Property TipoPagoFC() As Integer
        Get
            If mTipoPagoFC Is Nothing Then mTipoPagoFC = New Parametro().CTipoPagoFC
            Return mTipoPagoFC
        End Get
    End Property

    Public ReadOnly Property TipoCobroDesdePago() As Integer
        Get
            If mTipoCobroDesdePago Is Nothing Then mTipoCobroDesdePago = New Parametro().CTipoCobroDesdePago
            Return mTipoCobroDesdePago
        End Get
    End Property

    Public ReadOnly Property TipoPagoDesdeCobro() As Integer
        Get
            If mTipoPagoDesdeCobro Is Nothing Then mTipoPagoDesdeCobro = New Parametro().CTipoPagoDesdeCobro
            Return mTipoPagoDesdeCobro
        End Get
    End Property

    Public ReadOnly Property ContadorRemesa() As String
        Get
            If mContadorRemesa Is Nothing Then mContadorRemesa = New Parametro().ContadorRemesa
            Return mContadorRemesa
        End Get
    End Property

    Public ReadOnly Property ContadorRemesaAnticipo() As String
        Get
            If mContadorRemesaAnticipo Is Nothing Then mContadorRemesaAnticipo = New Parametro().ContadorRemesaAnticipo
            Return mContadorRemesaAnticipo
        End Get
    End Property

    Public ReadOnly Property ContadorPagare() As String
        Get
            If mContadorPagare Is Nothing Then mContadorPagare = New Parametro().ContadorPagare
            Return mContadorPagare
        End Get
    End Property

    Public ReadOnly Property NFacturaCobroAgupado() As String
        Get
            If mNFacturaCobroAgupado Is Nothing Then mNFacturaCobroAgupado = New Parametro().NFacturaCobroAgupado
            Return mNFacturaCobroAgupado
        End Get
    End Property

    Public ReadOnly Property AlmacenPortes() As String
        Get
            If mAlmacenPortes Is Nothing Then mAlmacenPortes = New Parametro().AlmacendePortes
            Return mAlmacenPortes
        End Get
    End Property

    Public ReadOnly Property TipoAsientoRemesa() As enumTipoAsientoRemesa
        Get
            If mTipoAsientoRemesa Is Nothing Then mTipoAsientoRemesa = New Parametro().TipoAsientoRemesa
            Return mTipoAsientoRemesa
        End Get
    End Property

End Class
