<Serializable()> _
Public Class DataDocIdentificacion
    Public IDPais As String
    Public Documento As String
    Public DocumentoCorrecto As String
    Public EsCorrecto As Boolean = False
    Public TipoDocumento As enumTipoDocIdent

    Public Sub New(ByVal Documento As String, ByVal IDPais As String, ByVal TipoDocumento As enumTipoDocIdent)
        Me.IDPais = IDPais
        If TipoDocumento = enumTipoDocIdent.NIF Or TipoDocumento = enumTipoDocIdent.CertificiadoResiFiscal Then
            Me.Documento = Trim(Replace(Documento, "-", "")).ToUpper
        Else
            Me.Documento = Documento
        End If
        Me.TipoDocumento = TipoDocumento
    End Sub

End Class
