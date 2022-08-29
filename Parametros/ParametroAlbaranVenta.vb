Public Class ParametroAlbaranVenta
    Private mActualizacionAutomaticaStock As Boolean?   '//Con el ? indicamos que puede ser Nothing
    Private mIDTipoAlbaran As String
    Private mAlbConsumo As String
    Private mAlbDeposito As String
    Private mAlbDevolucion As String
    Private mTipoAlbaranRetornoAlquiler As String
    Private mTipoAlbaranDeIntercambio As String
    Private mArticuloTransportePropio As String
    Private mTipoAlbaranExpDistribuidor As String
    Private mTipoAlbaranAbonoDistribuidor As String
 

    Public Sub New()
        '    Dim p As New Parametro
        '    mActualizacionAutomaticaStock = p.ActAutomaticaAlbVenta
        '    mIDTipoAlbaran = p.TipoAlbaranPorDefecto
    End Sub

    Public ReadOnly Property ActualizacionAutomaticaStock() As Boolean
        Get
            If mActualizacionAutomaticaStock Is Nothing Then mActualizacionAutomaticaStock = New Parametro().ActAutomaticaAlbVenta
            Return mActualizacionAutomaticaStock
        End Get
    End Property

    Public ReadOnly Property TipoAlbaranPorDefecto() As String
        Get
            If mIDTipoAlbaran Is Nothing Then mIDTipoAlbaran = New Parametro().TipoAlbaranPorDefecto
            Return mIDTipoAlbaran
        End Get
    End Property

    Public ReadOnly Property TipoAlbaranDeConsumo() As String
        Get
            If mAlbConsumo Is Nothing Then mAlbConsumo = New Parametro().TipoAlbaranDeConsumo
            Return mAlbConsumo
        End Get
    End Property

    Public ReadOnly Property TipoAlbaranDeDeposito() As String
        Get
            If mAlbDeposito Is Nothing Then mAlbDeposito = New Parametro().TipoAlbaranDeDeposito
            Return mAlbDeposito
        End Get
    End Property

    Public ReadOnly Property TipoAlbaranDeDevolucion() As String
        Get
            If mAlbDevolucion Is Nothing Then mAlbDevolucion = New Parametro().TipoAlbaranDeDevolucion
            Return mAlbDevolucion
        End Get
    End Property

    Public ReadOnly Property TipoAlbaranRetornoAlquiler() As String
        Get
            If mTipoAlbaranRetornoAlquiler Is Nothing Then mTipoAlbaranRetornoAlquiler = New Parametro().TipoAlbaranRetornoAlquiler
            Return mTipoAlbaranRetornoAlquiler
        End Get
    End Property

    Public ReadOnly Property TipoAlbaranDeIntercambio() As String
        Get
            If String.IsNullOrEmpty(mTipoAlbaranDeIntercambio) Then
                mTipoAlbaranDeIntercambio = New Parametro().TipoAlbaranDeIntercambio()
                '//Control que se hace para el caso en que no exista el parametro (en distribucion).
                '//Si el parametro no existe o el tipo no se ha dado de alta, el proceso sigue como si fuera un albaran normal por defecto.
                If String.IsNullOrEmpty(mTipoAlbaranDeIntercambio) Then
                    mTipoAlbaranDeIntercambio = "@"
                End If
            End If
            Return mTipoAlbaranDeIntercambio
        End Get
    End Property

    Public ReadOnly Property ArticuloTransportePropio() As String
        Get
            If mArticuloTransportePropio Is Nothing Then mArticuloTransportePropio = New Parametro().ArticuloTransportePropio
            Return mArticuloTransportePropio
        End Get
    End Property

    Public ReadOnly Property TipoAlbaranExpDistribuidor() As String
        Get
            If mTipoAlbaranExpDistribuidor Is Nothing Then mTipoAlbaranExpDistribuidor = New Parametro().TipoAlbaranExpDistribuidor
            Return mTipoAlbaranExpDistribuidor
        End Get
    End Property

    Public ReadOnly Property TipoAlbaranAbonoDistribuidor() As String
        Get
            If mTipoAlbaranAbonoDistribuidor Is Nothing Then mTipoAlbaranAbonoDistribuidor = New Parametro().TipoAlbaranAbonoDistribuidor
            Return mTipoAlbaranAbonoDistribuidor
        End Get
    End Property

End Class

