Public Class ParametroActivo

    Private mEstadoActivoPred As String
    Private mClaseActivoPred As String
    Private mCategoriaActivoPred As String
    Private mZonaActivoPred As String
    Private mCentroCosteActivoPred As String
    Private mGestionNumeroSerieConActivos As Boolean?


    Public Sub New()
    End Sub


    Public ReadOnly Property EstadoActivoPredeterminado() As String
        Get
            If mEstadoActivoPred Is Nothing Then mEstadoActivoPred = New Parametro().estadoactivopordefecto
            Return mEstadoActivoPred
        End Get
    End Property

    Public ReadOnly Property ClaseActivoPredeterminado() As String
        Get
            If mClaseActivoPred Is Nothing Then mClaseActivoPred = New Parametro().ClaseActivoPorDefecto
            Return mClaseActivoPred
        End Get
    End Property

    Public ReadOnly Property CategoriaActivoPredeterminado() As String
        Get
            If mCategoriaActivoPred Is Nothing Then mCategoriaActivoPred = New Parametro().CategoriaActivoPorDefecto
            Return mCategoriaActivoPred
        End Get
    End Property

    Public ReadOnly Property ZonaActivoPredeterminado() As String
        Get
            If mZonaActivoPred Is Nothing Then mZonaActivoPred = New Parametro().ZonaActivoPorDefecto
            Return mZonaActivoPred
        End Get
    End Property

    Public ReadOnly Property CentroCosteActivoPredeterminado() As String
        Get
            If mCentroCosteActivoPred Is Nothing Then mCentroCosteActivoPred = New Parametro().CentroCosteActivoPorDefecto
            Return mCentroCosteActivoPred
        End Get
    End Property

    Public ReadOnly Property GestionNumeroSerieConActivos() As Boolean
        Get
            If mGestionNumeroSerieConActivos Is Nothing Then mGestionNumeroSerieConActivos = New Parametro().GestionNumeroSerieConActivos
            Return mGestionNumeroSerieConActivos
        End Get
    End Property

End Class
