Public Class ParametroGeneral
    Private mCentroGestion As String
    Private mAlmacen As String
    Private mValoracionAlmacen As Boolean?
    Private mAlmacenCentroGestionActivo As Boolean? '//Con el ? indicamos que puede ser Nothing
    Private mAplicacionGestionAlquiler As Boolean?  '//Con el ? indicamos que puede ser Nothing
    Private mOperario As String
    Private mSAAS As Boolean?
    Private mMonedaPredeterminada As String
    Private mGestionPromotoras As Boolean?
    Private mGestionPromociones As Boolean?
    Private mGestionBodegas As Boolean?
    Private mCondicionPago As String
    Private mArticuloFacturacionProyectos As String
    Private mMargen As Boolean?
    Private mUDMedidaPredeterminada As String
    Private mFormaPagoEfectivo As String
    Private mCondicionPagoEfectivo As String
    Private mArticuloRetencion As String
    Private mGestionNumeroSerieConActivos As Boolean?
    Private mRiesgoCliente As Boolean?
    Private mTrabajoPredeterminado As String
    Private mHoraPred As String
    Private mHoraExtra As String
    Private mNFacturaPagoAgrupado As String
    Private mNFacturaCobroAgrupado As String
    Private mInventarioPermanente As Boolean?
    Private mIDMonedaA As String
    Private mIDMonedaB As String
    Private mActualizarPrecioAlbaranPeriodoCerrado As Boolean?
    'Private mOperadorVIES As Boolean?



    Public Sub New()
    End Sub

    Public ReadOnly Property CentroGestion() As String
        Get
            If mCentroGestion Is Nothing Then mCentroGestion = New Parametro().CGestionPredet
            Return mCentroGestion
        End Get
    End Property

    Public ReadOnly Property Almacen() As String
        Get
            If mAlmacen Is Nothing Then mAlmacen = New Parametro().AlmacenPredeterminado
            Return mAlmacen
        End Get
    End Property

    Public ReadOnly Property ValoracionAlmacen() As Boolean
        Get
            If mValoracionAlmacen Is Nothing Then mValoracionAlmacen = New Parametro().ValoracionAlmacen
            Return mValoracionAlmacen
        End Get
    End Property

    Public ReadOnly Property AlmacenCentroGestionActivo() As Boolean
        Get
            If mAlmacenCentroGestionActivo Is Nothing Then mAlmacenCentroGestionActivo = New Parametro().AlmacenCentroGestionActivo
            Return mAlmacenCentroGestionActivo
        End Get
    End Property
    Public ReadOnly Property AplicacionGestionAlquiler() As Boolean
        Get
            If mAplicacionGestionAlquiler Is Nothing Then mAplicacionGestionAlquiler = New Parametro().AplicacionGestionAlquiler
            Return mAplicacionGestionAlquiler
        End Get
    End Property

    Public ReadOnly Property OperarioGenerico() As String
        Get
            If mOperario Is Nothing Then mOperario = New Parametro().OperarioGenerico
            Return mOperario
        End Get
    End Property

    Public ReadOnly Property MargenBruto() As Boolean
        Get
            If mMargen Is Nothing Then mMargen = New Parametro().MargenBruto
            Return mMargen
        End Get
    End Property

    Public ReadOnly Property SAAS() As Boolean
        Get
            If mSAAS Is Nothing Then mSAAS = New Parametro().ExpertisSAAS
            Return mSAAS
        End Get
    End Property

    Public ReadOnly Property MonedaPredeterminada() As String
        Get
            If mMonedaPredeterminada Is Nothing Then mMonedaPredeterminada = New Parametro().MonedaPred
            Return mMonedaPredeterminada
        End Get
    End Property

    Public ReadOnly Property GestionPromotoras() As Boolean
        Get
            If mGestionPromotoras Is Nothing Then mGestionPromotoras = New Parametro().GestionPromotoras
            Return mGestionPromotoras
        End Get
    End Property

    Public ReadOnly Property GestionPromocionesComerciales() As Boolean
        Get
            If mGestionPromociones Is Nothing Then mGestionPromociones = New Parametro().GPromociones
            Return mGestionPromociones
        End Get
    End Property

    Public ReadOnly Property GestionBodegas() As Boolean
        Get
            If mGestionBodegas Is Nothing Then mGestionBodegas = New Parametro().GestionBodegas
            Return mGestionBodegas
        End Get
    End Property

    Public ReadOnly Property CondicionPago() As String
        Get
            If mCondicionPago Is Nothing Then mCondicionPago = New Parametro().CondicionPago
            Return mCondicionPago
        End Get
    End Property

    Public ReadOnly Property ArticuloFacturacionProyectos() As String
        Get
            If mArticuloFacturacionProyectos Is Nothing Then mArticuloFacturacionProyectos = New Parametro().ArticuloFacturacionProyectos
            Return mArticuloFacturacionProyectos
        End Get
    End Property

    Public ReadOnly Property UDMedidaPredeterminada() As String
        Get
            If mUDMedidaPredeterminada Is Nothing Then mAlmacen = New Parametro().UdMedidaPred
            Return mUDMedidaPredeterminada
        End Get
    End Property

    '#Region " Provisional - Se han pasado a ParametroTesoreria "

    '    Private mTipoCobroFV As Integer?
    '    Private mTipoPagoFC As Integer?
    '    Private mTipoCobroDesdePago As Integer?
    '    Private mTipoPagoDesdeCobro As Integer?
    '    Private mTipoAlbaran As String
    '    Private mContadorRemesa As String

    '    Public ReadOnly Property TipoAlbaranPorDefecto() As String
    '        Get
    '            If mTipoAlbaran Is Nothing Then mTipoAlbaran = New Parametro().TipoAlbaranPorDefecto
    '            Return mTipoAlbaran
    '        End Get
    '    End Property

    '    Public ReadOnly Property TipoCobroFV() As Integer
    '        Get
    '            If mTipoCobroFV Is Nothing Then mTipoCobroFV = New Parametro().CTipoCobroFV
    '            Return mTipoCobroFV
    '        End Get
    '    End Property

    '    Public ReadOnly Property TipoPagoFC() As Integer
    '        Get
    '            If mTipoPagoFC Is Nothing Then mTipoPagoFC = New Parametro().CTipoPagoFC
    '            Return mTipoPagoFC
    '        End Get
    '    End Property

    '    Public ReadOnly Property TipoCobroDesdePago() As Integer
    '        Get
    '            If mTipoCobroDesdePago Is Nothing Then mTipoCobroDesdePago = New Parametro().CTipoCobroDesdePago
    '            Return mTipoCobroDesdePago
    '        End Get
    '    End Property

    '    Public ReadOnly Property TipoPagoDesdeCobro() As Integer
    '        Get
    '            If mTipoPagoDesdeCobro Is Nothing Then mTipoPagoDesdeCobro = New Parametro().CTipoPagoDesdeCobro
    '            Return mTipoPagoDesdeCobro
    '        End Get
    '    End Property

    '    Public ReadOnly Property ContadorRemesa() As String
    '        Get
    '            If mContadorRemesa Is Nothing Then mContadorRemesa = New Parametro().ContadorRemesa
    '            Return mContadorRemesa
    '        End Get
    '    End Property

    '#End Region

    Public ReadOnly Property FormaPagoEfectivo() As String
        Get
            If mFormaPagoEfectivo Is Nothing Then mFormaPagoEfectivo = New Parametro().FormaPagoEnEfectivo
            Return mFormaPagoEfectivo
        End Get
    End Property

    Public ReadOnly Property CondicionPagoEfectivo() As String
        Get
            If mCondicionPagoEfectivo Is Nothing Then mCondicionPagoEfectivo = New Parametro().CondicionPagoEnEfectivo
            Return mCondicionPagoEfectivo
        End Get
    End Property

    Public ReadOnly Property ArticuloRetencion() As String
        Get
            If mArticuloRetencion Is Nothing Then mArticuloRetencion = New Parametro().ArticuloRetencion
            Return mArticuloRetencion
        End Get
    End Property

    Public ReadOnly Property GestionNumeroSerieConActivos() As String
        Get
            If mGestionNumeroSerieConActivos Is Nothing Then mGestionNumeroSerieConActivos = New Parametro().GestionNumeroSerieConActivos
            Return mGestionNumeroSerieConActivos
        End Get
    End Property

    Public ReadOnly Property RiesgoCliente() As Boolean
        Get
            If mRiesgoCliente Is Nothing Then mRiesgoCliente = New Parametro().RiesgoCliente
            Return mRiesgoCliente
        End Get
    End Property

    Public ReadOnly Property TrabajoPredeterminado() As String
        Get
            If mTrabajoPredeterminado Is Nothing Then
                mTrabajoPredeterminado = New Parametro().TrabajoTareaPredefinida
            End If
            Return mTrabajoPredeterminado
        End Get
    End Property

    Public ReadOnly Property HoraPred() As String
        Get
            If mHoraPred Is Nothing Then mHoraPred = New Parametro().HoraPred
            Return mHoraPred
        End Get
    End Property

    Public ReadOnly Property HoraExtra() As String
        Get
            If mHoraExtra Is Nothing Then mHoraExtra = New Parametro().HoraExtra
            Return mHoraExtra
        End Get
    End Property

    Public ReadOnly Property NFacturaPagoAgrupado() As String
        Get
            If mNFacturaPagoAgrupado Is Nothing Then mNFacturaPagoAgrupado = New Parametro().NFacturaPagoAgupado
            Return mNFacturaPagoAgrupado
        End Get
    End Property


    Public ReadOnly Property NFacturaCobroAgrupado() As String
        Get
            If mNFacturaCobroAgrupado Is Nothing Then mNFacturaCobroAgrupado = New Parametro().NFacturaCobroAgupado
            Return mNFacturaCobroAgrupado
        End Get
    End Property

    Public ReadOnly Property GestionInventarioPermanente() As Boolean
        Get
            If mInventarioPermanente Is Nothing Then mInventarioPermanente = New Parametro().GestionInventarioPermanente
            Return mInventarioPermanente
        End Get
    End Property

    Public ReadOnly Property MonedaInternaA() As String
        Get
            If mIDMonedaA Is Nothing Then mIDMonedaA = New Parametro().MonedaInternaA
            Return mIDMonedaA
        End Get
    End Property

    Public ReadOnly Property MonedaInternaB() As String
        Get
            If mIDMonedaB Is Nothing Then mIDMonedaB = New Parametro().MonedaInternaB
            Return mIDMonedaB
        End Get
    End Property

    Public ReadOnly Property ActualizarPrecioAlbaranPeriodoCerrado() As Boolean
        Get
            If mActualizarPrecioAlbaranPeriodoCerrado Is Nothing Then mActualizarPrecioAlbaranPeriodoCerrado = New Parametro().ActualizarPrecioAlbaranPeriodoCerrado
            Return mActualizarPrecioAlbaranPeriodoCerrado
        End Get
    End Property

    'Public ReadOnly Property OperadorVIES() As Boolean
    '    Get
    '        If mOperadorVIES Is Nothing Then mOperadorVIES = New Parametro().OperadorVIES
    '        Return mOperadorVIES
    '    End Get
    'End Property

End Class
