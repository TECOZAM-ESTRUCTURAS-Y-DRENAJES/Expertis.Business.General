Public Class ParametroFacturaVenta

    Private mintTipoCobroFacturaVenta As Integer?   '//Con el ? indicamos que puede ser Nothing
    Private mintTipoCobroFacturaVentaB As Integer?  '//Con el ? indicamos que puede ser Nothing
    Private mintTipoCobroRetencion As Integer?      '//Con el ? indicamos que puede ser Nothing
    Private mstrContadorAutofactura As String      '//Con el ? indicamos que puede ser Nothing
    Private mAgrupacionCobroRemesable As String
    Private mCalcularPuntoVerde As Boolean?

    Public Sub New()
    End Sub

    Public ReadOnly Property TipoCobroFacturaVenta() As Integer
        Get
            If mintTipoCobroFacturaVenta Is Nothing Then mintTipoCobroFacturaVenta = New Parametro().CTipoCobroFV
            Return mintTipoCobroFacturaVenta
        End Get
    End Property

    Public ReadOnly Property TipoCobroFacturaVentaB() As Integer
        Get
            If mintTipoCobroFacturaVentaB Is Nothing Then mintTipoCobroFacturaVentaB = New Parametro().CTipoCobroFVB
            Return mintTipoCobroFacturaVentaB
        End Get
    End Property

    Public ReadOnly Property TipoCobroRetencion() As Integer
        Get
            If mintTipoCobroRetencion Is Nothing Then mintTipoCobroRetencion = New Parametro().TipoCobroRetencion
            Return mintTipoCobroRetencion
        End Get
    End Property

    Public ReadOnly Property ContadorAutofactura() As String
        Get
            If mstrContadorAutofactura Is Nothing Then mstrContadorAutofactura = New Parametro().ContadorAutofactura
            Return mstrContadorAutofactura
        End Get
    End Property

    Public ReadOnly Property AgrupacionCobroRemesable() As String
        Get
            If mAgrupacionCobroRemesable Is Nothing Then mAgrupacionCobroRemesable = New Parametro().AgrupacionCobroRem
            Return mAgrupacionCobroRemesable
        End Get
    End Property

    Public ReadOnly Property CalcularPuntoVerde() As Boolean
        Get
            If mCalcularPuntoVerde Is Nothing Then mCalcularPuntoVerde = New Parametro().CalcularPuntoVerde
            Return mCalcularPuntoVerde
        End Get
    End Property

End Class
