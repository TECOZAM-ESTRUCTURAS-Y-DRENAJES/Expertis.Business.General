Public Class ParametroContabilidadVenta
    Inherits ParametroContabilidad

    Private mCCExportacion As String
    Private mCCExportacionGrupo As String
    Private mCCVenta As String
    Private mCCVentaGrupo As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public ReadOnly Property CuentaExportacion() As String
        Get
            If mCCExportacion Is Nothing Then mCCExportacion = New Parametro().CCExport
            Return mCCExportacion
        End Get
    End Property
    Public ReadOnly Property CuentaExportacionGrupo() As String
        Get
            If mCCExportacionGrupo Is Nothing Then mCCExportacionGrupo = New Parametro().CCExportGrupo
            Return mCCExportacionGrupo
        End Get
    End Property
    Public ReadOnly Property CuentaVenta() As String
        Get
            If mCCVenta Is Nothing Then mCCVenta = New Parametro().CCVenta
            Return mCCVenta
        End Get
    End Property
    Public ReadOnly Property CuentaVentaGrupo() As String
        Get
            If mCCVentaGrupo Is Nothing Then mCCVentaGrupo = New Parametro().CCVentaGrupo
            Return mCCVentaGrupo
        End Get
    End Property

End Class
