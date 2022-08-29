Public Class ParametroContabilidad
    Private mContabilidad As Boolean
    Private mAnalitica As ParametroAnalitica
    Private mPrefijoCuentaConciliacion As String
    Private mContabilidadMultiple As Boolean?
    Private mAmortizacionPorGrupo As Boolean?


    Public Sub New()
        Dim p As New Parametro
        mContabilidad = p.Contabilidad
        'If Me.Contabilidad Then
        '    mAnalitica = New ParametroAnalitica
        'End If
    End Sub

    Public ReadOnly Property Analitica() As ParametroAnalitica
        Get
            If mAnalitica Is Nothing Then
                mAnalitica = New ParametroAnalitica
            End If
            Return mAnalitica
        End Get
    End Property

    Public ReadOnly Property Contabilidad() As Boolean
        Get
            Return mContabilidad
        End Get
    End Property

    Public ReadOnly Property PrefijoCuentaConciliacionBancaria() As String
        Get
            If mPrefijoCuentaConciliacion Is Nothing Then mPrefijoCuentaConciliacion = New Parametro().PrefijoCuentaConciliacionBancaria
            Return mPrefijoCuentaConciliacion
        End Get
    End Property

    Public ReadOnly Property ContabilidadMultiple() As Boolean
        Get
            If mContabilidadMultiple Is Nothing Then
                mContabilidadMultiple = New Parametro().ContabilidadMultiple
            End If
            Return mContabilidadMultiple
        End Get
    End Property

    Public ReadOnly Property AmortizacionPorGrupo() As Boolean
        Get
            If mAmortizacionPorGrupo Is Nothing Then
                mAmortizacionPorGrupo = New Parametro().AmortizacionPorGrupo
            End If
            Return mAmortizacionPorGrupo
        End Get
    End Property

End Class
