Public Class ParametroCompra
    Private mIVAProveedor As Boolean?
    Private mTipoCompraNormal As String
    Private mTipoCompraSubcontratacion As String
    Private mTipoCompraPedidoVenta As String
    Private mGeneral As ParametroGeneral
    Private mEmpresaRE As Boolean?

    Public Sub New()
    End Sub

    Public ReadOnly Property IVAProveedor() As Boolean
        Get
            If mIVAProveedor Is Nothing Then mIVAProveedor = New Parametro().IVAProveedor
            Return mIVAProveedor
        End Get
    End Property

    Public ReadOnly Property TipoCompraPedidoVenta() As String
        Get
            If mTipoCompraPedidoVenta Is Nothing Then mTipoCompraPedidoVenta = New Parametro().TipoCompraPedidoVenta
            Return mTipoCompraPedidoVenta
        End Get
    End Property

    Public ReadOnly Property TipoCompraSubcontratacion() As String
        Get
            If mTipoCompraSubcontratacion Is Nothing Then mTipoCompraSubcontratacion = New Parametro().TipoCompraSubcontratacion
            Return mTipoCompraSubcontratacion
        End Get
    End Property
    Public ReadOnly Property TipoCompraNormal() As String
        Get
            If mTipoCompraNormal Is Nothing Then mTipoCompraNormal = New Parametro().TipoCompraNormal
            Return mTipoCompraNormal
        End Get
    End Property

    Public ReadOnly Property General() As ParametroGeneral
        Get
            If mGeneral Is Nothing Then mGeneral = New ParametroGeneral
            Return mGeneral
        End Get
    End Property
    Public ReadOnly Property EmpresaConRecargoEquivalencia() As Boolean
        Get
            If mEmpresaRE Is Nothing Then mEmpresaRE = New Parametro().EmpresaRecargoEquivPredeterminado
            Return mEmpresaRE
        End Get
    End Property

End Class
