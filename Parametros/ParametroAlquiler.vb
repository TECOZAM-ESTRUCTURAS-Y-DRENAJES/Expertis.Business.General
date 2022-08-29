Public Class ParametroAlquiler
    Private mCContableMaterial As String
    Private mLimiteHoraAlquiler As String
    Private mContadorAvisoRetorno As String
    Private mGestionNumeroSerieConActivos As Boolean?
    Private mIntervienenExcesosContadoresEnCalculoSeguros As Boolean?
    Private mPorcentajeTasaResiduos As Double?
    Private mImporteLimiteTasaResiduos As Double?
    Private mArticuloTasaResiduos As String

    Public ReadOnly Property CContableMaterialAlquiler() As String
        Get
            If mCContableMaterial Is Nothing Then mCContableMaterial = New Parametro().CContableMaterialAlquiler
            Return mCContableMaterial
        End Get
    End Property

    Public ReadOnly Property LimiteHoraAlquiler() As String
        Get
            If mLimiteHoraAlquiler Is Nothing Then mLimiteHoraAlquiler = New Parametro().LimiteHoraAlquiler
            Return mLimiteHoraAlquiler
        End Get
    End Property

    Public ReadOnly Property ContadorAvisoRetorno() As String
        Get
            If mContadorAvisoRetorno Is Nothing Then mContadorAvisoRetorno = New Parametro().ContadorAvisoRetorno
            Return mContadorAvisoRetorno
        End Get
    End Property

    Public ReadOnly Property GestionNumeroSerieConActivos() As Boolean
        Get
            If mGestionNumeroSerieConActivos Is Nothing Then mGestionNumeroSerieConActivos = New Parametro().GestionNumeroSerieConActivos
            Return mGestionNumeroSerieConActivos
        End Get
    End Property

    Public ReadOnly Property IntervienenExcesosContadoresEnCalculoSeguros() As Boolean
        Get
            If mIntervienenExcesosContadoresEnCalculoSeguros Is Nothing Then mIntervienenExcesosContadoresEnCalculoSeguros = New Parametro().IntervienenExcesosContadoresEnCalculoSeguros
            Return mIntervienenExcesosContadoresEnCalculoSeguros
        End Get
    End Property

    Public ReadOnly Property PorcentajeTasaResiduos() As Double
        Get
            If mPorcentajeTasaResiduos Is Nothing Then mPorcentajeTasaResiduos = New Parametro().PorcTasaResiduosFact
            Return mPorcentajeTasaResiduos
        End Get
    End Property

    Public ReadOnly Property ImporteLimiteTasaResiduos() As Double
        Get
            If mImporteLimiteTasaResiduos Is Nothing Then mImporteLimiteTasaResiduos = New Parametro().ImporteLimiteTasaResiduos
            Return mImporteLimiteTasaResiduos
        End Get
    End Property

    Public ReadOnly Property ArticuloTasaResiduos() As String
        Get
            If mArticuloTasaResiduos Is Nothing Then mArticuloTasaResiduos = New Parametro().ArticuloTasaResiduos
            Return mArticuloTasaResiduos
        End Get
    End Property

End Class
