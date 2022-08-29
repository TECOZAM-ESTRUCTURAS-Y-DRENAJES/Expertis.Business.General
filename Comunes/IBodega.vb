Public Interface IBodega

End Interface


Public Interface IEMCS

    Sub ReconstruirDAALineas(ByVal data As DataReconstruirDAALineas, ByVal services As ServiceProvider)


End Interface

<Serializable()> _
Public Class DataReconstruirDAALineas

    Public IDBaseDatosOrigen As Guid
    Public IDBaseDatosDAA As Guid

    Public IDDaa As Guid
    Public IDAlbaran As Integer
    Public dtCabeceraAlbaran As DataTable
    'Public dtLineasAlbaran As DataTable

End Class
