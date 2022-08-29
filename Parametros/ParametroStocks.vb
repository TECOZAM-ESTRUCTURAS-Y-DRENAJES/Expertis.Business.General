Public Class ParametroStocks

    Private mTipoInventario As TipoInventario?
    Private mblnPrecioMovimientoCero As Boolean?
    Private mContadorHistMovimientoPredeterminado As String
    Private mGestionNumeroSerieConActivos As Boolean?
    Private mGestionStockNegativo As Boolean?
    Private mGestionStockNegativoPorArticulo As Boolean?
    Private mGestionDobleUnidad As Boolean?
    Private mblnCalculoPrecioMedioPorMovimiento As Boolean?
    Private mCContableProductoEnCurso As String
    Private mCContableIngresos As String
    Private mTipoMovimientoCantidad0 As Integer?

    Public ReadOnly Property TipoInventario() As TipoInventario
        Get
            If mTipoInventario Is Nothing Then mTipoInventario = New Parametro().TipoInventario
            Return mTipoInventario
        End Get
    End Property

    Public ReadOnly Property PrecioMovimientoCero() As Boolean
        Get
            If mblnPrecioMovimientoCero Is Nothing Then mblnPrecioMovimientoCero = New Parametro().PrecioMovimientoCero
            Return mblnPrecioMovimientoCero
        End Get
    End Property

    Public ReadOnly Property ContadorHistMovimientoPredeterminado() As String
        Get
            If mContadorHistMovimientoPredeterminado Is Nothing Then mContadorHistMovimientoPredeterminado = New Parametro().ContadorHistMovimientoPredeterminado
            Return mContadorHistMovimientoPredeterminado
        End Get
    End Property

    Public ReadOnly Property GestionNumeroSerieConActivos() As Boolean
        Get
            If mGestionNumeroSerieConActivos Is Nothing Then mGestionNumeroSerieConActivos = New Parametro().GestionNumeroSerieConActivos
            Return mGestionNumeroSerieConActivos
        End Get
    End Property

    Public ReadOnly Property GestionStockNegativo() As Boolean
        Get
            If mGestionStockNegativo Is Nothing Then mGestionStockNegativo = New Parametro().GestionStockNegativo
            Return mGestionStockNegativo
        End Get
    End Property

    Public ReadOnly Property GestionStockNegativoPorArticulo() As Boolean
        Get
            If mGestionStockNegativoPorArticulo Is Nothing Then mGestionStockNegativoPorArticulo = New Parametro().GestionStockNegativoPorArticulo
            Return mGestionStockNegativoPorArticulo
        End Get
    End Property

    Public ReadOnly Property GestionDobleUnidad() As Boolean
        Get
            If mGestionDobleUnidad Is Nothing Then mGestionDobleUnidad = New Parametro().GestionDobleUnidad
            Return mGestionDobleUnidad
        End Get
    End Property

    Public ReadOnly Property CalculoPrecioMedioPorMovimiento() As Boolean
        Get
            If mblnCalculoPrecioMedioPorMovimiento Is Nothing Then mblnCalculoPrecioMedioPorMovimiento = New Parametro().CalculoPrecioMedioPorMovimiento
            Return mblnCalculoPrecioMedioPorMovimiento
        End Get
    End Property

    Public ReadOnly Property CContableProductoEnCurso() As String
        Get
            If mCContableProductoEnCurso Is Nothing Then mCContableProductoEnCurso = New Parametro().CContableProductoEnCurso
            Return mCContableProductoEnCurso
        End Get
    End Property

    Public ReadOnly Property CContableIngresos() As String
        Get
            If mCContableIngresos Is Nothing Then mCContableIngresos = New Parametro().CContableIngresos
            Return mCContableIngresos
        End Get
    End Property

    Public ReadOnly Property TipoMovimientoCantidad0() As Integer
        Get
            If mTipoMovimientoCantidad0 Is Nothing Then mTipoMovimientoCantidad0 = New Parametro().TipoMovimientoCantidad0
            Return mTipoMovimientoCantidad0
        End Get
    End Property

End Class
