Public Class ParametroContabilidadCompra
    Inherits ParametroContabilidad

    Private mCCImportacion As String
    Private mCCImportacionGrupo As String
    Private mCCCompra As String
    Private mCCCompraGrupo As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public ReadOnly Property CuentaImportacion() As String
        Get
            If mCCImportacion Is Nothing Then mCCImportacion = New Parametro().CCImport
            Return mCCImportacion
        End Get
    End Property
    Public ReadOnly Property CuentaImportacionGrupo() As String
        Get
            If mCCImportacionGrupo Is Nothing Then mCCImportacionGrupo = New Parametro().CCImportGrupo
            Return mCCImportacionGrupo
        End Get
    End Property
    Public ReadOnly Property CuentaCompra() As String
        Get
            If mCCCompra Is Nothing Then mCCCompra = New Parametro().CCCompra
            Return mCCCompra
        End Get
    End Property
    Public ReadOnly Property CuentaCompraGrupo() As String
        Get
            If mCCCompraGrupo Is Nothing Then mCCCompraGrupo = New Parametro().CCCompraGrupo
            Return mCCCompraGrupo
        End Get
    End Property

End Class
