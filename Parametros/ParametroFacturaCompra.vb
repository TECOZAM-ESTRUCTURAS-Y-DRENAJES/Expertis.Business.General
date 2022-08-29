Public Class ParametroFacturaCompra
    Private mintTipoPagoFacturaCompra As Integer?   '//Con el ? indicamos que puede ser Nothing
    Private mintTipoPagoFacturaCompraB As Integer?  '//Con el ? indicamos que puede ser Nothing
    Private mintTipoPagoRetencion As Integer?       '//Con el ? indicamos que puede ser Nothing
    Private mstrProveedorRetencion As String
    Private mblnControlarFechaFCProveedor As Boolean?

    Public Sub New()

    End Sub

    Public ReadOnly Property TipoPagoFacturaCompra() As Integer
        Get
            If mintTipoPagoFacturaCompra Is Nothing Then mintTipoPagoFacturaCompra = New Parametro().CTipoPagoFC
            Return mintTipoPagoFacturaCompra
        End Get
    End Property

    Public ReadOnly Property TipoPagoFacturaCompraB() As Integer
        Get
            If mintTipoPagoFacturaCompraB Is Nothing Then mintTipoPagoFacturaCompraB = New Parametro().CTipoPagoFCB
            Return mintTipoPagoFacturaCompraB
        End Get
    End Property

    Public ReadOnly Property TipoPagoRetencion() As Integer
        Get
            If mintTipoPagoRetencion Is Nothing Then mintTipoPagoRetencion = New Parametro().TipoPagoRetencion
            Return mintTipoPagoRetencion
        End Get
    End Property

    Public ReadOnly Property ProveedorRetencion() As String
        Get
            If mstrProveedorRetencion Is Nothing Then mstrProveedorRetencion = New Parametro().ProveedorRetencion
            Return mstrProveedorRetencion
        End Get
    End Property

    Public ReadOnly Property ControlarFechaFCProveedor() As Boolean
        Get
            If mblnControlarFechaFCProveedor Is Nothing Then mblnControlarFechaFCProveedor = New Parametro().ControlarFechaFCProveedor
            Return mblnControlarFechaFCProveedor
        End Get
    End Property

End Class
