Public Class ParametroAnalitica
    Private mAplicarAnalitica As Boolean?                       '//Con el ? indicamos que puede ser Nothing
    Private mAnaliticaCentroGestion As Boolean?                 '//Con el ? indicamos que puede ser Nothing
    Private mAnaliticaTipo As enumGestionAnalitica?             '//Con el ? indicamos que puede ser Nothing
    Private mAnaliticaOrigen As Boolean?                        '//Con el ? indicamos que puede ser Nothing
    Private mCentroCoste As String
    Private mCentroGestion As String
    Private mAnaliticaAsignacion As enumAsignacionAnalitica?    '//Con el ? indicamos que puede ser Nothing
    Private mNivelesDeAnalitica As Integer?
    Private mContabilidad As Boolean?
    Public Sub New()
    End Sub

    Public ReadOnly Property AplicarAnalitica() As Boolean
        Get
            If mContabilidad Is Nothing Then mContabilidad = New Parametro().Contabilidad
            If mAplicarAnalitica Is Nothing Then mAplicarAnalitica = New Parametro().CAnaliticPredet
            If Not mContabilidad Then mAplicarAnalitica = False
            Return mAplicarAnalitica
        End Get
    End Property

    Public ReadOnly Property AnaliticaCentroGestion() As Boolean
        Get
            If mContabilidad Is Nothing Then mContabilidad = New Parametro().Contabilidad
            If mAnaliticaCentroGestion Is Nothing Then mAnaliticaCentroGestion = New Parametro().CAnaliticGestion
            If Not mContabilidad Then mAnaliticaCentroGestion = False

            Return mAnaliticaCentroGestion
        End Get
    End Property
    Public ReadOnly Property AnaliticaTipo() As enumGestionAnalitica
        Get
            If mAnaliticaTipo Is Nothing Then mAnaliticaTipo = New Parametro().CAnaliticTipo
            Return mAnaliticaTipo
        End Get
    End Property
    Public ReadOnly Property AnaliticaOrigen() As Boolean
        Get
            If mContabilidad Is Nothing Then mContabilidad = New Parametro().Contabilidad
            If mAnaliticaOrigen Is Nothing Then mAnaliticaOrigen = New Parametro().CAnaliticOrigen
            If Not mContabilidad Then mAnaliticaOrigen = False
            Return mAnaliticaOrigen
        End Get
    End Property
    Public ReadOnly Property CentroCostePredeterminado() As String
        Get
            If mCentroCoste Is Nothing Then mCentroCoste = New Parametro().CCostePredet
            Return mCentroCoste
        End Get
    End Property
    Public ReadOnly Property CentroGestionPredeterminado() As String
        Get
            If mCentroGestion Is Nothing Then mCentroGestion = New Parametro().CGestionPredet
            Return mCentroGestion
        End Get
    End Property
    Public ReadOnly Property AnaliticaAsignacion() As enumAsignacionAnalitica
        Get
            If mAnaliticaAsignacion Is Nothing Then mAnaliticaAsignacion = New Parametro().CAnaliticAsig
            Return mAnaliticaAsignacion
        End Get
    End Property

    Public ReadOnly Property NivelesDeAnalitica() As Integer
        Get
            If mNivelesDeAnalitica Is Nothing Then mNivelesDeAnalitica = New Parametro().NivelesDeAnalitica
            Return mNivelesDeAnalitica
        End Get
    End Property

End Class