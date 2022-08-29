Public Class FacturaElec

    <Task()> Public Shared Function LoadFacturaElecInfo(ByVal data As Object, ByVal services As ServiceProvider) As FacturaElecInfo
        Dim InfoElec As New FacturaElecInfo
        Dim Params(3) As String
        Params(0) = "PATHPDFFE" : Params(1) = "USELASTCER" : Params(2) = "URLTS" : Params(3) = "USEXADESXL"
        Dim FilParam As New Filter
        FilParam.Add(New InListFilterItem("IDParametro", Params, FilterType.String))
        Dim DtParams As DataTable = New BE.DataEngine().Filter("tbParametro", FilParam)
        If Not DtParams Is Nothing AndAlso DtParams.Rows.Count = Params.Length Then
            InfoElec.DirectorioFacturasPDF = DtParams.Select("IDParametro = 'PATHPDFFE'")(0)("Valor")
            InfoElec.UsarUltimoCertificado = DtParams.Select("IDParametro = 'USELASTCER'")(0)("Valor")
            InfoElec.URLServidorTS = DtParams.Select("IDParametro = 'URLTS'")(0)("Valor")
            InfoElec.FormatXADESXL = DtParams.Select("IDParametro = 'USEXADESXL'")(0)("Valor")
        Else
            For Each StrPrm As String In Params
                Dim DrSel() As DataRow = DtParams.Select("IDParametro = '" & StrPrm & "'")
                If DrSel.Length = 0 Then
                    ApplicationService.GenerateError("No se ha creado / configurado el parámetro:|. Por favor, revise la configuración de parámetros.", StrPrm)
                End If
            Next
        End If
        Dim DtFich As DataTable = New BE.DataEngine().Filter("tbFicherosModeloDetalle", New FilterItem("IDTipoFichero", FilterOperator.Equal, "FACTURAE"))
        If Not DtFich Is Nothing AndAlso DtFich.Rows.Count > 0 Then
            InfoElec.DirectorioFacturasXML = DtFich.Rows(0)("RutaFichero")
        End If
        Return InfoElec
    End Function

    <Task()> Public Shared Sub SaveFacturaElecInfo(ByVal data As FacturaElecInfo, ByVal services As ServiceProvider)
        'Salvamos Directorio de Facturas Electronicas en PDF
        Dim StrUpdate As String = String.Empty
        StrUpdate = "UPDATE tbParametro "
        StrUpdate &= "SET Valor = '" & data.DirectorioFacturasPDF & "' "
        StrUpdate &= "WHERE IDParametro = 'PATHPDFFE'"
        AdminData.Execute(StrUpdate)

        StrUpdate = "UPDATE tbParametro "
        StrUpdate &= "SET Valor = '" & data.UsarUltimoCertificado & "' "
        StrUpdate &= "WHERE IDParametro = 'USELASTCER'"
        AdminData.Execute(StrUpdate)
        If data.UsarUltimoCertificado = False Then
            StrUpdate = "UPDATE tbParametro "
            StrUpdate &= "SET Valor = NULL "
            StrUpdate &= "WHERE IDParametro = 'LASTCERID'"
            AdminData.Execute(StrUpdate)
        End If

        'Salvamos Directorio de Facturas Electronicas en XML
        StrUpdate = "UPDATE tbFicherosModeloDetalle "
        StrUpdate &= "SET RutaFichero = '" & data.DirectorioFacturasXML & "' "
        StrUpdate &= "WHERE IDTipoFichero = 'FACTURAE'"
        AdminData.Execute(StrUpdate)

        StrUpdate = "UPDATE tbParametro "
        StrUpdate &= "SET Valor = '" & data.URLServidorTS & "' "
        StrUpdate &= "WHERE IDParametro = 'URLTS'"
        AdminData.Execute(StrUpdate)

        StrUpdate = "UPDATE tbParametro "
        StrUpdate &= "SET Valor = '" & data.FormatXADESXL & "' "
        StrUpdate &= "WHERE IDParametro = 'USEXADESXL'"
        AdminData.Execute(StrUpdate)
    End Sub

    <Task()> Public Shared Function GetLastCerID(ByVal data As Object, ByVal services As ServiceProvider) As String
        Dim DtParams As DataTable = New BE.DataEngine().Filter("tbParametro", New FilterItem("IDParametro", FilterOperator.Equal, "LASTCERID"))
        If Not DtParams Is Nothing AndAlso DtParams.Rows.Count > 0 Then
            Return Nz(DtParams.Rows(0)("Valor"), String.Empty)
        Else
            ApplicationService.GenerateError("No se ha creado / configurado el parámetro: LASTCERID. Por favor, revise la configuración de parámetros.")
        End If
    End Function

    <Task()> Public Shared Sub SaveLastCerID(ByVal data As String, ByVal services As ServiceProvider)
        Dim StrUpdate As String = String.Empty
        StrUpdate = "UPDATE tbParametro "
        StrUpdate &= "SET Valor = '" & data & "' "
        StrUpdate &= "WHERE IDParametro = 'LASTCERID'"
        AdminData.Execute(StrUpdate)
    End Sub

    <Task()> Public Shared Function GetUseLastCert(ByVal data As Object, ByVal services As ServiceProvider) As Boolean
        Dim DtParams As DataTable = New BE.DataEngine().Filter("tbParametro", New FilterItem("IDParametro", FilterOperator.Equal, "USELASTCER"))
        If Not DtParams Is Nothing AndAlso DtParams.Rows.Count > 0 Then
            Return DtParams.Rows(0)("Valor")
        Else
            ApplicationService.GenerateError("No se ha creado / configurado el parámetro: USELASTCER. Por favor, revise la configuración de parámetros.")
        End If
    End Function

    <Task()> Public Shared Function GetUseXADESXL(ByVal data As Object, ByVal services As ServiceProvider) As Boolean
        Dim DtParams As DataTable = New BE.DataEngine().Filter("tbParametro", New FilterItem("IDParametro", FilterOperator.Equal, "USEXADESXL"))
        If Not DtParams Is Nothing AndAlso DtParams.Rows.Count > 0 Then
            Return CBool(DtParams.Rows(0)("Valor"))
        Else
            ApplicationService.GenerateError("No se ha creado / configurado el parámetro: USEXADESXL. Por favor, revise la configuración de parámetros.")
        End If
    End Function

End Class

<Serializable()> _
Public Class FacturaElecInfo
    Public DirectorioFacturasPDF As String
    Public DirectorioFacturasXML As String
    Public URLServidorTS As String
    Public FormatXADESXL As Boolean
    Public Fichero As String
    Public UsarUltimoCertificado As Boolean
    Public InfoFact As FacturaElecInfo
End Class

<Serializable()> _
Public Class SignatureInfo
    Public FicheroFirmarOrigen As String
    Public FicheroFirmarDestino As String
    Public NombreCampoFirma As String
    Public RazonFirma As String
    Public LocalizacionFirma As String
    Public InfoContactoFirma As String
End Class