Public Class ParametroAlbaranCompra
    Inherits ParametroCompra

    Private mExpertisSAAS As Boolean?  '//Con el ? indicamos que puede ser Nothing
    Private mActualizacionAutomaticaStock As Boolean? '//Con el ? indicamos que puede ser Nothing
    Private mAlmacen As String
    Private mTipoCompra As String

    Public Sub New()
    End Sub

    Public ReadOnly Property ExpertisSAAS() As Boolean
        Get
            If mExpertisSAAS Is Nothing Then mExpertisSAAS = New Parametro().ExpertisSAAS
            Return mExpertisSAAS
        End Get
    End Property

    Public ReadOnly Property ActualizacionAutomaticaStock() As Boolean
        Get
            If mActualizacionAutomaticaStock Is Nothing Then mActualizacionAutomaticaStock = New Parametro().ActAutomaticaAlbCompra
            Return mActualizacionAutomaticaStock
        End Get
    End Property

    Public ReadOnly Property Almacen() As String
        Get
            If mAlmacen Is Nothing Then mAlmacen = New Parametro().AlmacenPredeterminado
            Return mAlmacen
        End Get
    End Property

End Class
