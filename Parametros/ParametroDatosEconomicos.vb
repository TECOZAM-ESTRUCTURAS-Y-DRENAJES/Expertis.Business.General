Public Class ParametroDatosEconomicos
    Private mDiaPago As String
    Private mFormaPago As String
    Private mCondicionPago As String
    Private mFormaEnvio As String
    Private mCondicionEnvio As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public ReadOnly Property DiaPago() As String
        Get
            If mDiaPago Is Nothing Then mDiaPago = New Parametro().DiaPago
            Return mDiaPago
        End Get
    End Property

    Public ReadOnly Property FormaPago() As String
        Get
            If mFormaPago Is Nothing Then mFormaPago = New Parametro().FormaPago
            Return mFormaPago
        End Get
    End Property

    Public ReadOnly Property CondicionPago() As String
        Get
            If mCondicionPago Is Nothing Then mCondicionPago = New Parametro().CondicionPago
            Return mCondicionPago
        End Get
    End Property

    Public ReadOnly Property FormaEnvio() As String
        Get
            If mFormaEnvio Is Nothing Then mFormaEnvio = New Parametro().FormaEnvio
            Return mFormaEnvio
        End Get
    End Property

    Public ReadOnly Property CondicionEnvio() As String
        Get
            If mCondicionEnvio Is Nothing Then mCondicionEnvio = New Parametro().CondicionEnvio
            Return mCondicionEnvio
        End Get
    End Property

End Class
