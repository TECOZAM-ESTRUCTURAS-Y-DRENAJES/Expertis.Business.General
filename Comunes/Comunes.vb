Imports System.Reflection
Imports System.Collections.Generic


<Transactional()> _
Public Class Comunes
    Inherits ContextBoundObject

#Region " Margenes (Código común para Comercial y Obras)"

#Region " CalcularMargen "

    <Serializable()> _
    Public Class DatosCalculoMargen
        Public Venta As Double
        Public Coste As Double

        Public Sub New()
        End Sub

        Public Sub New(ByVal Venta As Double, ByVal Coste As Double)
            Me.Venta = Venta
            Me.Coste = Coste
        End Sub
    End Class

    <Task()> Public Shared Function CalcularMargen(ByVal data As DatosCalculoMargen, ByVal services As ServiceProvider) As Double
        Dim Margen As Double = 0
        Dim Parametros As ParametroGeneral = services.GetService(Of ParametroGeneral)()

        If Parametros.MargenBruto Then
            If data.Venta <> 0 Then
                Margen = ((data.Venta - data.Coste) / data.Venta) * 100
            End If
        ElseIf data.Coste <> 0 Then
            Margen = ((data.Venta - data.Coste) / data.Coste) * 100
        End If

        'If Margen = -100 Then
        '    Return 0
        'Else
        Return xRound(Margen, 2)
        'End If
    End Function

#End Region

#Region " ValidarMargen "

    <Serializable()> _
    Public Class DatosValidarMargen
        Public Margen As Double
        Public Coste As Double

        Public Sub New()
        End Sub

        Public Sub New(ByVal Margen As Double, ByVal Coste As Double)
            Me.Margen = Margen
            Me.Coste = Coste
        End Sub
    End Class

    <Task()> Public Shared Sub ValidarMargen(ByVal data As DatosValidarMargen, ByVal services As ServiceProvider)
        Dim Parametros As ParametroGeneral = services.GetService(Of ParametroGeneral)()
        If Parametros.MargenBruto Then
            If data.Margen > 100 Then
                ApplicationService.GenerateError("El margen no puede ser superior a 100.")
            ElseIf data.Margen = 100 And data.Coste <> 0 Then
                ApplicationService.GenerateError("El margen no puede ser igual a 100 con un coste distinto de 0.")
            End If
        End If
    End Sub

#End Region

#Region " AplicarMargen "

    <Serializable()> _
    Public Class datosAplicarMargen
        Public Coste, Margen, Venta As Double?
        Public Cancel As Boolean = False

        Public Sub New(ByVal Coste As Double, ByVal Margen As Double)
            Me.Coste = Coste
            Me.Margen = Margen
            Me.Venta = 0
        End Sub
    End Class

    <Task()> Public Shared Sub AplicarMargen(ByVal data As datosAplicarMargen, ByVal services As ServiceProvider)
        If data.Margen = 0 Then
            data.Venta = data.Coste
        Else
            Dim Parametros As ParametroGeneral = services.GetService(Of ParametroGeneral)()
            If Parametros.MargenBruto Then
                If data.Margen >= 100 Then
                    data.Cancel = True
                Else
                    data.Venta = (data.Coste * 100) / (100 - data.Margen)
                End If
            Else
                data.Venta = data.Coste * (100 + data.Margen) / 100
            End If
        End If
    End Sub

#End Region

#End Region

#Region " ValidarDocumentoIdentificativo "

    ' Este mismo algoritmo también puede utilizarse para el calculo del Número de Identificación 
    ' de Extranjeros (NIE), despreciando la X y utilizando los 7 dígitos.

    <Task()> Public Shared Function ValidarDocumentoIdentificativo(ByVal data As DataDocIdentificacion, ByVal services As ServiceProvider) As DataDocIdentificacion
        If Length(data.Documento) > 0 Then
            Dim Nacional As Boolean = ProcessServer.ExecuteTask(Of DataDocIdentificacion, Boolean)(AddressOf EsNacional, data, services)
            If Nacional Then
                Select Case data.TipoDocumento
                    Case enumTipoDocIdent.NIF
                        ProcessServer.ExecuteTask(Of DataDocIdentificacion)(AddressOf ValidarNIF, data, services)
                    Case enumTipoDocIdent.CertificiadoResiFiscal
                        ProcessServer.ExecuteTask(Of DataDocIdentificacion)(AddressOf ValidarNIE, data, services)
                    Case enumTipoDocIdent.NIFOperadorIntra
                        ProcessServer.ExecuteTask(Of DataDocIdentificacion)(AddressOf ValidarNIF, data, services)
                    Case Else
                        data.EsCorrecto = True
                End Select
            Else
                Select Case data.TipoDocumento
                    Case enumTipoDocIdent.NIFOperadorIntra
                        ProcessServer.ExecuteTask(Of DataDocIdentificacion)(AddressOf ValidarNIFIntracomunitario, data, services)
                    Case Else
                        data.EsCorrecto = True
                End Select
            End If
        End If

        Return data
    End Function

    <Task()> Public Shared Sub ValidarNIF(ByVal data As DataDocIdentificacion, ByVal services As ServiceProvider)
        If IsNumeric(Left(data.Documento, 1)) Then
            ProcessServer.ExecuteTask(Of DataDocIdentificacion)(AddressOf PersonasFisicas, data, services)
        ElseIf Not IsNumeric(Left(data.Documento, 1)) Then
            ProcessServer.ExecuteTask(Of DataDocIdentificacion)(AddressOf PersonasJuridicas, data, services)
        End If
    End Sub

    <Task()> Public Shared Sub PersonasFisicas(ByVal data As DataDocIdentificacion, ByVal services As ServiceProvider)
        If data.Documento.Length <= 9 Then
            Const cnFormatoDocumento As String = "00000000"
            Dim Documento As String = data.Documento
            If Not IsNumeric(data.Documento) Then
                Dim Letra As String = Right(data.Documento, 1)
                If Not IsNumeric(Letra) Then
                    Documento = Mid(data.Documento, 1, Len(data.Documento) - 1)  'quitamos la letra del documento
                End If
            End If

            If Length(Documento) > 0 AndAlso IsNumeric(Documento) AndAlso IsNumeric(Right(Documento, 1)) Then
                Documento = Mid(Documento, 1, 8)
                Dim LetraFinal As String = ProcessServer.ExecuteTask(Of Integer, String)(AddressOf Letra, Documento, services)
                Documento = Strings.Format(CInt(Documento), cnFormatoDocumento)
                data.DocumentoCorrecto = Documento & LetraFinal
                data.EsCorrecto = (data.DocumentoCorrecto = data.Documento)
            End If
        End If
    End Sub

    <Task()> Public Shared Sub PersonasJuridicas(ByVal data As DataDocIdentificacion, ByVal services As ServiceProvider)
        Dim Documento As String = data.Documento
        If Length(Documento) = 8 Or Length(Documento) = 9 Then
            Dim SumaPar As Integer = 0
            Dim SumaImpar As Integer = 0

            Dim LetraInicial As String = Left(Documento, 1)
            Dim Digitos As String = Documento.Substring(1, 7)
            If IsNumeric(Digitos) AndAlso IsNumeric(Right(Digitos, 1)) Then
                'Dim LetrasValidas As String = "ABCDEFGHJKLMNPQRSUVW"
                Dim LetrasValidas As String = "ABCDEFGHJNPQRSUVW"
                If LetrasValidas.IndexOf(LetraInicial) <> -1 Then
                    For n As Integer = 0 To Digitos.Length - 1 Step 2
                        If n < 6 Then
                            SumaImpar += CInt(CStr(Digitos.Chars(n + 1)))
                        End If
                        Dim dobleImpar As Integer = 2 * CInt(CStr(Digitos.Chars(n)))
                        SumaPar += (dobleImpar Mod 10) + (dobleImpar \ 10)
                    Next
                    Dim SumaTotal As Integer = SumaPar + SumaImpar
                    SumaTotal = (10 - (SumaTotal Mod 10)) Mod 10

                    Dim Control As String
                    Select Case LetraInicial
                        'Case "C", "K", "L", "M", "N", "P", "Q", "R", "S", "W"
                        Case "N", "P", "Q", "R", "S", "W"
                            Dim characters() As String = {"J", "A", "B", "C", "D", "E", "F", "G", "H", "I"}
                            Control = characters(SumaTotal)
                        Case Else
                            Control = CStr(SumaTotal)
                    End Select

                    data.DocumentoCorrecto = LetraInicial & Digitos & Control
                    data.EsCorrecto = (data.DocumentoCorrecto = data.Documento)
                End If
            End If
        End If
    End Sub


    <Serializable()> _
    Public Class DataValidarNIFIntracomunitarioVIES
        Public CodigoISO As String
        Public CIF As String
        Public Configurado As Boolean

        Public Sub New(ByVal CodigoISO As String, ByVal CIF As String)
            Me.CodigoISO = CodigoISO
            Me.CIF = CIF
        End Sub
    End Class

    <Task()> Public Shared Function ValidarNIFIntracomunitarioVIES(ByVal data As DataValidarNIFIntracomunitarioVIES, ByVal services As ServiceProvider) As Boolean
        Try
            Dim URL As String = New Parametro().URLWebServicesVIES
            If Length(URL) > 0 Then
                data.Configurado = True
                '"http://ec.europa.eu/taxation_customs/vies/services/checkVatService"
                Dim VIES As New Servicio_VIES.checkVatService
                VIES.Url = URL

                Dim CIFValido As Boolean
                Dim Name As String = String.Empty
                Dim Address As String = String.Empty
                Dim response As Date = VIES.checkVat(data.CodigoISO, data.CIF, CIFValido, Name, Address)
                Return CIFValido
            End If
        Catch ex As Exception
            ApplicationService.GenerateError("Ha ocurrido algún problema con el Servicio de Validación de NIF Intracomunitario (VIES).{0}{1}", vbNewLine, ex.Message)
        End Try
    End Function
    <Task()> Public Shared Sub ValidarNIFIntracomunitario(ByVal data As DataDocIdentificacion, ByVal services As ServiceProvider)
        If Length(data.IDPais) > 0 AndAlso Length(data.Documento) > 0 Then
            Dim dtPais As DataTable = New Pais().SelOnPrimaryKey(data.IDPais)
            If dtPais.Rows.Count > 0 Then
                Dim CodigoISO As String = dtPais.Rows(0)("CodigoISO") & String.Empty
                Dim Long_ISO As Integer = Length(CodigoISO)
                If Long_ISO > 0 Then
                    Dim NIFTieneISO As Boolean = True
                    Dim DocumentoSinISO As String = data.Documento
                    If CodigoISO = "GR" Then CodigoISO = "EL"

                    '//Validamos la parte del NIF que se corresponde con el Código ISO
                    If Length(data.Documento) > Long_ISO Then
                        Dim ParteISO As String = Left(data.Documento, Long_ISO)
                        If IsNumeric(ParteISO) Then
                            NIFTieneISO = False
                        Else
                            If ParteISO = CodigoISO Then
                                '    If (CodigoISO <> "ES" AndAlso CodigoISO <> "FR" AndAlso CodigoISO <> "IE" AndAlso CodigoISO <> "AT") Then
                                '        ApplicationService.GenerateError("El código ISO del Documento ({0}) no coincide con el Código ISO del País (País {1} - Cod.ISO {2}).", ParteISO, data.IDPais, CodigoISO)
                                '    End If
                                'Else
                                Dim LONG_NIF_FR As String = 12 '//Si es FR, puede que vengan "FRFR 999999999" o "FR 999999999"
                                If CodigoISO <> "FR" OrElse Length(data.Documento) > LONG_NIF_FR Then
                                    DocumentoSinISO = Right(data.Documento, Length(data.Documento) - Long_ISO)
                                End If
                            End If
                        End If
                    Else
                        If CodigoISO <> "RO" Then ApplicationService.GenerateError("La longitud del documento no es correcta.")
                    End If

                    If Length(DocumentoSinISO) = 0 Then
                        ApplicationService.GenerateError("La longitud del Documento sin Código ISO no es correcta.")
                    Else
                        DocumentoSinISO = UCase(DocumentoSinISO)
                    End If

                    Dim LETTER_CONTROL As New List(Of String)
                    '//A-Z
                    For i As Integer = 65 To 90
                        Dim str As String = Chr(i)
                        LETTER_CONTROL.Add(str)
                    Next

                    Dim VIESIncorrecto As Boolean
                    Dim datVIES As New DataValidarNIFIntracomunitarioVIES(CodigoISO, DocumentoSinISO)
                    If Not ProcessServer.ExecuteTask(Of DataValidarNIFIntracomunitarioVIES, Boolean)(AddressOf ValidarNIFIntracomunitarioVIES, datVIES, services) Then
                        If datVIES.Configurado Then VIESIncorrecto = True
                    Else
                        data.EsCorrecto = True
                        Exit Sub
                    End If


                    Dim Patron As String = ""
                    Dim blnIncorrecto As Boolean
                    Select Case CodigoISO
                        'Case "ES" '//España
                        '    Patron = "ESX9999999X = ES + 1 bloque de 9 caracteres (primero y último: letra o dígitos, resto: sólo dígitos)"
                        Case "SI", "FI", "MT", "LU", "HU" '//Eslovenia, Finlandia, Malta, Luxemburgo, Hungría
                            Patron = UCase(CodigoISO) + "99999999 = " + UCase(CodigoISO) + " + 1 bloque de 8 dígitos numéricos."

                            Dim Long_Doc As Integer = 8
                            If Length(DocumentoSinISO) <> Long_Doc OrElse Not IsNumeric(DocumentoSinISO) Then
                                blnIncorrecto = True
                            End If
                        Case "DE", "EE", "EL", "PT" '//Alemania, Estonia, Grecia, Portugal
                            Dim Long_Doc As Integer = 9
                            If Length(DocumentoSinISO) <> Long_Doc OrElse Not IsNumeric(DocumentoSinISO) Then
                                blnIncorrecto = True
                                Patron = UCase(CodigoISO) + "999999999 = " + UCase(CodigoISO) + " + 1 bloque de 9 dígitos numéricos"
                            End If
                        Case "PL", "SK"          '//Polonia, Eslovaquia
                            Dim Long_Doc As Integer = 10
                            If Length(DocumentoSinISO) <> Long_Doc OrElse Not IsNumeric(DocumentoSinISO) Then
                                blnIncorrecto = True
                                Patron = UCase(CodigoISO) + "9999999999 = " + UCase(CodigoISO) + " + 1 bloque de 10 dígitos numéricos"
                            End If
                        Case "IT", "LV", "HR" '//Italia, Letonia, Croacia
                            Dim Long_Doc As Integer = 11
                            If Length(DocumentoSinISO) <> Long_Doc OrElse Not IsNumeric(DocumentoSinISO) Then
                                blnIncorrecto = True
                                Patron = UCase(CodigoISO) + "99999999999 = " + UCase(CodigoISO) + " + 1 bloque de 11 dígitos numéricos"
                            End If
                        Case "AT" '//Austria
                            Patron = "ATU99999999 = AT + 1 bloque con una ""U"" seguida de 8 dígitos numéricos"

                            Dim INITIAL_CHAR As String = "U"
                            Dim Long_Doc As Integer = 9
                            If Length(DocumentoSinISO) <> Long_Doc OrElse UCase(Left(DocumentoSinISO, 1)) <> INITIAL_CHAR OrElse Not IsNumeric(Right(DocumentoSinISO, Long_Doc - 1)) Then
                                blnIncorrecto = True
                            End If
                        Case "BE" '//Belgica
                            Patron = "BE9999999999 = BE + 1 bloque de 10 dígitos numéricos, el primero ""0"" o ""1"""

                            Dim INITIAL_CHAR() As String = New String() {"0", "1"}
                            Dim Long_Doc As Integer = 10
                            Dim EsDigitoControlValido As Integer = (From c In INITIAL_CHAR.ToList Where c = UCase(Left(DocumentoSinISO, 1))).Count
                            If Length(DocumentoSinISO) <> Long_Doc OrElse EsDigitoControlValido = 0 OrElse Not IsNumeric(DocumentoSinISO) Then
                                blnIncorrecto = True
                            End If
                        Case "BG" '//Bulgaria
                            Patron = "BG9999999999 = BG + 1 bloque de 9 o 10 dígitos numéricos"
                            Dim Long_MIN As Integer = 9
                            Dim Long_MAX As Integer = 10
                            If Length(DocumentoSinISO) < Long_MIN OrElse Length(DocumentoSinISO) > Long_MAX OrElse Not IsNumeric(DocumentoSinISO) Then
                                blnIncorrecto = True
                            End If
                        Case "CZ" '//Rep.Checa
                            Patron = "CZ9999999999 = CZ + 1 bloque de 8, 9 o 10 dígitos numéricos"

                            Dim Long_MIN As Integer = 8
                            Dim Long_MAX As Integer = 10
                            If Length(DocumentoSinISO) < Long_MIN OrElse Length(DocumentoSinISO) > Long_MAX OrElse Not IsNumeric(DocumentoSinISO) Then
                                blnIncorrecto = True
                            End If
                        Case "CY" '//Chipre
                            Patron = "CY99999999L = CY + 1 bloque de 8 dígitos numéricos y al final 1 letra"
                            Dim Long_Doc As Integer = 9
                            Dim EsLetraControlValido As Integer = (From c In LETTER_CONTROL.ToList Where c = UCase(Right(DocumentoSinISO, 1))).Count
                            If Length(DocumentoSinISO) <> Long_Doc OrElse Not IsNumeric(Left(DocumentoSinISO, Long_Doc - 1)) OrElse EsLetraControlValido = 0 Then
                                blnIncorrecto = True
                            End If
                        Case "DK" '//Dinamarca
                            Patron = "DK99 99 99 99 = DK + 4 bloques de 2 dígitos numéricos"
                            Dim NumBloques As Integer = 4
                            Dim Long_Bloque As Integer = 2
                            Dim Bloques() As String = DocumentoSinISO.Split(Space(1))
                            If Bloques.Count <> NumBloques Then
                                blnIncorrecto = True
                            Else
                                For Each blq As String In Bloques
                                    If Length(blq) <> Long_Bloque OrElse Not IsNumeric(blq) Then
                                        blnIncorrecto = True
                                        Exit For
                                    End If
                                Next
                            End If

                        Case "FR" '//Francia
                            Patron = "FRXX 999999999 = FR + 1 bloque de 2 caracteres y otro de 9 dígitos numéricos, o bien, un único bloque de 11 caracteres, los últimos 9 numéricos"
                            Dim NumBloques As Integer = 2
                            Dim Long_Bloques() As Integer = New Integer() {2, 9}

                            Dim Bloques() As String = DocumentoSinISO.Split(Space(1))
                            If Bloques.Count <> NumBloques Then
                                blnIncorrecto = True
                                If Bloques.Count = 1 Then
                                    Dim Long_Total_Bloques As Integer = (Aggregate b In Long_Bloques Into Sum(b))
                                    If Length(Bloques(0)) = Long_Total_Bloques Then
                                        blnIncorrecto = False

                                        Dim UltimosNDigitos As String = Right(Bloques(0), Long_Bloques(NumBloques - 1))
                                        If Not IsNumeric(UltimosNDigitos) Then
                                            blnIncorrecto = True
                                        End If
                                    End If
                                End If
                            Else
                                Dim i As Integer = 0
                                For Each blq As String In Bloques
                                    If Length(blq) <> Long_Bloques(i) Then
                                        blnIncorrecto = True
                                        Exit For
                                    Else
                                        If (i = 1 AndAlso Not IsNumeric(blq)) Then
                                            blnIncorrecto = True
                                            Exit For
                                        End If
                                    End If

                                    i += 1
                                Next
                            End If

                        Case "IE" '//Irlanda
                            Patron = "IE9S99999L = IE + 1 bloque de 8 dígitos y al final 1 letra. ""S"" puede ser letra (A-Z), dígito (0-9); ""+"" o ""*"""
                            Patron &= vbNewLine & " o " & vbNewLine
                            Patron &= "IE9999999WI"

                            Dim Long_Doc As Integer = 8

                            Dim CHARS_CONTROL As New List(Of String)
                            '//A-Z
                            For Each s As String In LETTER_CONTROL
                                CHARS_CONTROL.Add(s)
                            Next
                            '//0-9
                            For i As Integer = 48 To 57
                                Dim str As String = Chr(i)
                                CHARS_CONTROL.Add(str)
                            Next
                            CHARS_CONTROL.Add(Chr(43)) '+
                            CHARS_CONTROL.Add(Chr(42)) '*

                            Dim POS_CHAR_CONTROL As Integer = 2
                            Dim BloqueNum As String = Right(DocumentoSinISO, Length(DocumentoSinISO) - POS_CHAR_CONTROL)
                            BloqueNum = Left(BloqueNum, Length(BloqueNum) - 1)
                            Dim EsDigitoControlValido As Integer = (From c In CHARS_CONTROL.ToList Where c = UCase(Right(Left(DocumentoSinISO, POS_CHAR_CONTROL), 1))).Count
                            Dim EsLetraControlValido As Integer = (From c In LETTER_CONTROL.ToList Where c = UCase(Right(DocumentoSinISO, 1))).Count
                            If Length(DocumentoSinISO) <> Long_Doc OrElse EsDigitoControlValido = 0 OrElse EsLetraControlValido = 0 OrElse _
                               Not IsNumeric(Left(DocumentoSinISO, 1)) OrElse Not IsNumeric(BloqueNum) Then
                                blnIncorrecto = True
                            End If
                            If blnIncorrecto Then
                                Long_Doc = 9
                                Dim OTHER_CHAR_CONTROL As String = "WI"
                                blnIncorrecto = False
                                If Length(DocumentoSinISO) <> Long_Doc OrElse Right(DocumentoSinISO, Length(OTHER_CHAR_CONTROL)) <> OTHER_CHAR_CONTROL OrElse Not IsNumeric(Left(DocumentoSinISO, Length(DocumentoSinISO) - Length(OTHER_CHAR_CONTROL))) Then
                                    blnIncorrecto = True
                                End If
                            End If
                        Case "LT" '//Lituania
                            Patron = "LT999999999999 = LT + 1 bloque de 9 o 12 dígitos numéricos"
                            Dim Long_MAX As Integer = 12
                            Dim Long_MIN As Integer = 9

                            If (Length(DocumentoSinISO) <> Long_MIN AndAlso Length(DocumentoSinISO) <> Long_MAX) OrElse Not IsNumeric(DocumentoSinISO) Then
                                blnIncorrecto = True
                            End If
                        Case "NL" '//Paises Bajos
                            Patron = "NL999999999B99 = NL + 1 bloque de 12 caracteres, siendo el décimo siempre ""B"""
                            Dim Long_Doc As Integer = 12
                            Dim CHAR_CONTROL As String = "B"
                            Dim POS_CHAR_CONTROL As Integer = 10
                            If Length(DocumentoSinISO) <> Long_Doc OrElse Right(Left(DocumentoSinISO, POS_CHAR_CONTROL), 1) <> CHAR_CONTROL OrElse _
                               Not IsNumeric(Left(DocumentoSinISO, POS_CHAR_CONTROL - 1)) OrElse Not IsNumeric(Right(DocumentoSinISO, Length(DocumentoSinISO) - POS_CHAR_CONTROL)) Then
                                blnIncorrecto = True
                            End If
                        Case "GB" '//Reino Unido
                            Patron = "GB999 9999 99 (999) = GR + 3 bloques de 3, 4 y 2 dígitos numéricos (Empresas de grupos: + otro de 3 dígitos)"

                            Dim NumBloques_MAX As Integer = 4
                            Dim NumBloques_MIN As Integer = 3


                            Dim Bloques() As String = DocumentoSinISO.Split(Space(1))
                            If Bloques.Count < NumBloques_MIN OrElse Bloques.Count > NumBloques_MAX Then
                                blnIncorrecto = True
                            Else
                                Dim Long_Bloques() As Integer
                                If Bloques.Count = NumBloques_MIN Then
                                    Long_Bloques = New Integer() {3, 4, 2}
                                ElseIf Bloques.Count = NumBloques_MAX Then
                                    Long_Bloques = New Integer() {3, 4, 2, 3}
                                End If

                                Dim i As Integer = 0
                                For Each blq As String In Bloques
                                    If Length(blq) <> Long_Bloques(i) OrElse Not IsNumeric(blq) Then
                                        blnIncorrecto = True
                                        Exit For
                                    End If
                                    i += 1
                                Next
                            End If
                        Case "RO" '//Rumania
                            Patron = "RO999999999 = RO + 1 bloque de 2 a 10 dígitos numéricos"
                            Dim Long_MIN As Integer = 2
                            Dim Long_MAX As Integer = 10
                            If Length(DocumentoSinISO) < Long_MIN OrElse Length(DocumentoSinISO) > Long_MAX OrElse Not IsNumeric(DocumentoSinISO) Then
                                blnIncorrecto = True
                            End If
                        Case "SE" '//Suecia
                            Patron = "SE999999999901 = 1 bloque de 12 dígitos numéricos, los dos últimos siempre ""01"""
                            Dim Long_Doc As Integer = 12
                            If Length(DocumentoSinISO) <> Long_Doc OrElse Not IsNumeric(DocumentoSinISO) OrElse Right(DocumentoSinISO, 2) <> "01" Then
                                blnIncorrecto = True
                            End If
                    End Select

                    If blnIncorrecto Then
                        ApplicationService.GenerateError("El Documento Identificativo {0} no es correcto. Debe tener el siguiente patrón:{1}{2}", Quoted(data.Documento), vbNewLine, Patron)
                    Else
                        If VIESIncorrecto Then
                            ApplicationService.GenerateError("El Documento Identificativo {0} no es es un Documento válido, aunque cumpla el patrón requerido. (VIES)", Quoted(data.Documento), vbNewLine, Patron)
                        Else
                            data.EsCorrecto = True
                        End If
                    End If
                Else
                    ApplicationService.GenerateError("El Pais {0} no tiene un Código ISO identificado.", Quoted(data.IDPais))
                End If
            Else
                ApplicationService.GenerateError("El Pais {0} no se encuentra en el sistema.", Quoted(data.IDPais))
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarNIE(ByVal data As DataDocIdentificacion, ByVal services As ServiceProvider)
        Const cnFormatoDocumento As String = "0000000"

        Dim Documento As String = data.Documento
        Dim LetraInicial As String = String.Empty
        If Not IsNumeric(data.Documento) Then
            Dim Letra As String = Right(data.Documento, 1)
            If Not IsNumeric(Letra) Then
                Documento = Mid(data.Documento, 1, Len(data.Documento) - 1)  'quitamos la letra del documento
            End If
            If Length(Documento) > 0 AndAlso Not IsNumeric(Documento) Then
                LetraInicial = Left(Documento, 1)
                If IsNumeric(Letra) Then
                    Documento = Mid(data.Documento, 2, Len(data.Documento) - 1)  'quitamos la letra del documento
                Else
                    Documento = Mid(data.Documento, 2, Len(data.Documento) - 2)  'quitamos la letra por delacte y por detras del documento
                End If
            End If
        End If

        If Length(LetraInicial) > 0 AndAlso Length(Documento) > 0 AndAlso IsNumeric(Documento) Then
            Documento = Strings.Format(CInt(Documento), cnFormatoDocumento)
            Dim blnNifExtranjero As Boolean = ProcessServer.ExecuteTask(Of String, Boolean)(AddressOf LetraInicialExtranjero, LetraInicial, services)
            If Length(LetraInicial) > 0 And blnNifExtranjero Then
                Dim DocumentoAux As Integer = Documento
                If LetraInicial = "Y" Then DocumentoAux = CInt("1" & CStr(Documento))
                If LetraInicial = "Z" Then DocumentoAux = CInt("2" & CStr(Documento))
                Dim LetraFinal As String = ProcessServer.ExecuteTask(Of Integer, String)(AddressOf Letra, DocumentoAux, services)
                data.DocumentoCorrecto = LetraInicial & Documento & LetraFinal
                data.EsCorrecto = (data.DocumentoCorrecto = data.Documento)
            End If
        End If
    End Sub

    <Task()> Public Shared Function LetraInicialExtranjero(ByVal Letra As String, ByVal services As ServiceProvider) As Boolean
        Return InStr("LKXYM", Letra, CompareMethod.Text) > 0
    End Function

    <Task()> Public Shared Function Letra(ByVal Documento As Integer, ByVal services As ServiceProvider) As String
        Return Mid$("TRWAGMYFPDXBNJZSQVHLCKE", (Documento Mod 23) + 1, 1)
    End Function

    <Task()> Public Shared Function EsNacional(ByVal data As DataDocIdentificacion, ByVal services As ServiceProvider) As Boolean
        If Length(data.IDPais) > 0 Then
            Dim dtPais As DataTable = New BE.DataEngine().Filter("tbMaestroPais", New StringFilterItem("IDPais", data.IDPais))
            If Not IsNothing(dtPais) AndAlso dtPais.Rows.Count > 0 Then
                Return Not dtPais.Rows(0)("Extranjero")
            End If
        End If
        'Si el Cliente no tiene País se le considerará como Nacional.
        Return True
    End Function

#End Region

#Region " ValidarDocumentoCedulaRUC (Ecuador) "
    <Task()> Public Shared Sub ValidarCedula(ByVal data As DataDocIdentificacion, ByVal services As ServiceProvider)

        Dim Documento As String = data.Documento
        If Not IsNumeric(data.Documento) Then
            data.EsCorrecto = False
            Exit Sub
        End If
        If Len(Trim(Documento)) <> 10 Then
            data.EsCorrecto = False
            Exit Sub
        End If
        If Len(Trim(Documento)) <> 10 Then
            data.EsCorrecto = False
            Exit Sub
        End If

        If Val(Mid(Documento, 1, 2)) > 25 Then
            data.EsCorrecto = False
            Exit Sub
        End If

        If Val(Mid(Documento, 3, 1)) > 5 Then
            data.EsCorrecto = False
            Exit Sub
        End If


        Dim Total As Integer
        Dim Cifra As Integer
        Dim a As Integer
        Total = 0

        For a = 1 To 9

            If (a Mod 2) = 0 Then
                Cifra = Val(Mid(Documento, a, 1))
            Else
                Cifra = Val(Mid(Documento, a, 1)) * 2
                If Cifra > 9 Then
                    Cifra = Cifra - 9
                End If
            End If
            Total = Total + Cifra
        Next

        Cifra = Total Mod 10

        If Cifra > 0 Then
            Cifra = 10 - Cifra
        End If

        If Cifra = Val(Mid(Documento, 10, 1)) Then
            data.EsCorrecto = True
        Else
            data.EsCorrecto = False
        End If



    End Sub
    <Task()> Public Shared Sub ValidarRUC(ByVal data As DataDocIdentificacion, ByVal services As ServiceProvider)

        Dim Documento As String = data.Documento

        If Not IsNumeric(data.Documento) Then
            data.DocumentoCorrecto = False
            Exit Sub
        End If
        If Len(Trim(Documento)) <> 13 Then
            data.EsCorrecto = False
            Exit Sub
        End If
        If Right(Documento, 3) = "000" Then
            data.EsCorrecto = False
            Exit Sub
        End If
        data.Documento = Left(data.Documento, 10)
        ProcessServer.ExecuteTask(Of DataDocIdentificacion)(AddressOf ValidarCedula, data, services)

    End Sub
    <Task()> Public Shared Function RucDigito(ByVal Ruc As Long, ByVal services As ServiceProvider) As String
        Dim Total As Integer
        Dim Cifra As Integer
        Dim a As Integer
        Total = 0

        For a = 1 To 9

            If (a Mod 2) = 0 Then
                Cifra = Val(Mid(Ruc, a, 1))
            Else
                Cifra = Val(Mid(Ruc, a, 1)) * 2
                If Cifra > 9 Then
                    Cifra = Cifra - 9
                End If
            End If
            Total = Total + Cifra
        Next

        Cifra = Total Mod 10

        If Cifra > 0 Then
            Cifra = 10 - Cifra
        End If

        If Cifra = Val(Mid(Ruc, 10, 1)) Then
            'Data.EsCorrecto = True
        Else
            'Data.EsCorrecto = False
        End If
    End Function

#End Region

#Region " ValidarDocumentoRUT (Chile) "

    <Task()> Public Shared Sub ValidarRUT(ByVal data As DataDocIdentificacion, ByVal services As ServiceProvider)
        data.EsCorrecto = False
        data.Documento = Replace(data.Documento, "-", "")
        data.Documento = Replace(data.Documento, ".", "")
        data.Documento = data.Documento.Trim

        Dim Digito As String = Mid(data.Documento.ToUpper(), data.Documento.Length(), 1)
        Dim Documento As String = Mid(data.Documento, 1, data.Documento.Length() - 1)
        If Not IsNumeric(Documento) Then ApplicationService.GenerateError("El Rut es incorrecto.")
        Dim DigitoCalculado As String = ProcessServer.ExecuteTask(Of String, String)(AddressOf RutDigito, Documento, services)
        If Digito = DigitoCalculado Then
            data.DocumentoCorrecto = Documento & "-" & DigitoCalculado
        Else
            ApplicationService.GenerateError("El Rut es incorrecto.")
        End If
    End Sub
    <Task()> Public Shared Function RutDigito(ByVal Rut As Integer, ByVal services As ServiceProvider) As String
        Dim Digito As Integer
        Dim Contador As Integer
        Dim Multiplo As Integer
        Dim Acumulador As Integer
        Contador = 2
        Acumulador = 0
        While Rut <> 0
            Multiplo = (Rut Mod 10) * Contador
            Acumulador = Acumulador + Multiplo
            Rut = Rut \ 10
            Contador = Contador + 1
            If Contador > 7 Then
                Contador = 2
            End If
        End While
        Digito = 11 - (Acumulador Mod 11)
        RutDigito = CStr(Digito)
        If Digito = 10 Then RutDigito = "K"
        If Digito = 11 Then RutDigito = "0"
    End Function

#End Region

#Region " Validar Código IBAN (SEPA) "


    <Task()> Public Shared Sub ValidarCodigoIBAN(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.Table.Columns.Contains("CodigoIBAN") Then
            If data.RowState = DataRowState.Added OrElse (data.RowState = DataRowState.Modified AndAlso Length(data("CodigoIBAN")) > 0 AndAlso data("CodigoIBAN") & String.Empty <> data("CodigoIBAN", DataRowVersion.Original) & String.Empty) Then
                ProcessServer.ExecuteTask(Of String, Boolean)(AddressOf ValidarIBAN, data("CodigoIBAN") & String.Empty, services)
            End If
        End If
    End Sub


    <Task()> Public Shared Function ValidarIBAN(ByVal CodigoIBAN As String, ByVal services As ServiceProvider) As Boolean
        ValidarIBAN = False
        If Length(CodigoIBAN) > 0 Then
            CodigoIBAN = UCase(CodigoIBAN)
            If Length(CodigoIBAN) >= 5 AndAlso Length(CodigoIBAN) <= 34 Then

                Dim ISOPais As String = Strings.Left(CodigoIBAN, 2)
                If IsNumeric(ISOPais) Then
                    ApplicationService.GenerateError("Los 2 primeros caracteres, deben corresponder al Código ISO del País. Revise sus datos.")
                End If
                If ISOPais = "ES" AndAlso Length(CodigoIBAN) < 24 Then
                    ApplicationService.GenerateError("La longitud del Código IBAN introducido no es correcta. Revise sus datos.")
                End If

                If ISOPais <> "ES" Then
                    Return True
                End If

                Dim DC As String = Strings.Mid(CodigoIBAN, 3, 2)
                If Not IsNumeric(DC) Then
                    ApplicationService.GenerateError("El dígito de control debe ser un valor numérico. Revise sus datos.")
                End If

                Dim NumeroCuenta As String = Strings.Mid(CodigoIBAN, 5, Length(CodigoIBAN))

                Dim CodVerificar As String = NumeroCuenta + ProcessServer.ExecuteTask(Of String, String)(AddressOf GetCaracteresEnNumero, ISOPais, services) + DC
                Dim Resto As Integer = CDec(CodVerificar) Mod 97
                If Resto = 1 Then
                    ValidarIBAN = True
                Else
                    ApplicationService.GenerateError("El Código IBAN introducido no es correcto. Revise sus datos.")
                End If
            Else
                ApplicationService.GenerateError("La longitud del Código IBAN introducido no es correcta. Revise sus datos.")
            End If
        End If
    End Function

    <Task()> Public Shared Function GetCaracteresEnNumero(ByVal data As String, ByVal services As ServiceProvider) As String
        Dim Num As String = String.Empty
        For Each c As Char In data
            Select Case UCase(c)
                Case "A"
                    Num &= "10"
                Case "B"
                    Num &= "11"
                Case "C"
                    Num &= "12"
                Case "D"
                    Num &= "13"
                Case "E"
                    Num &= "14"
                Case "F"
                    Num &= "15"
                Case "G"
                    Num &= "16"
                Case "H"
                    Num &= "17"
                Case "I"
                    Num &= "18"
                Case "J"
                    Num &= "19"
                Case "K"
                    Num &= "20"
                Case "L"
                    Num &= "21"
                Case "M"
                    Num &= "22"
                Case "N"
                    Num &= "23"
                Case "O"
                    Num &= "24"
                Case "P"
                    Num &= "25"
                Case "Q"
                    Num &= "26"
                Case "R"
                    Num &= "27"
                Case "S"
                    Num &= "28"
                Case "T"
                    Num &= "29"
                Case "U"
                    Num &= "30"
                Case "V"
                    Num &= "31"
                Case "W"
                    Num &= "32"
                Case "X"
                    Num &= "33"
                Case "Y"
                    Num &= "34"
                Case "Z"
                    Num &= "35"
            End Select
        Next
        Return Num
    End Function

#End Region

#Region " Transacciones "

    <Task()> Public Shared Sub BeginTransaction(ByVal data As Object, ByVal services As ServiceProvider)
        AdminData.BeginTx()
    End Sub

    <Task()> Public Shared Sub CommitTransaction(ByVal forceCommit As Boolean, ByVal services As ServiceProvider)
        AdminData.CommitTx(forceCommit)
    End Sub

    <Task()> Public Shared Sub RollbackTransaction(ByVal forceRollback As Boolean, ByVal services As ServiceProvider)
        AdminData.RollBackTx(forceRollback)
    End Sub

#End Region

#Region " Métodos de Actuliazacion y Borrado "

    '//Métodos para indicar al motor que ya se ha Actualizado/Borrado lo que tenemos en el UpdatePackage
    <Task()> Public Shared Sub MarcarComoActualizado(ByVal data As Object, ByVal services As ServiceProvider)
        Dim upc As UpdateProcessContext = services.GetService(Of UpdateProcessContext)()
        upc.Updated = True
    End Sub

    <Task()> Public Shared Sub MarcarComoEliminado(ByVal data As Object, ByVal services As ServiceProvider)
        Dim dpc As DeleteProcessContext = services.GetService(Of DeleteProcessContext)()
        dpc.Deleted = True
    End Sub

#End Region

#Region " Delete "

    <Task()> Public Shared Sub DeleteEntityRow(ByVal data As DataRow, ByVal services As ServiceProvider)
        ''Dim row As DataTable = data.Table.Clone
        ''row.ImportRow(data)
        ''row.Rows(0).Delete()
        ''BusinessHelper.UpdateTable(row)
        BusinessHelper.DeleteRowCascade(data, services)
        '//NOTA: Esto no podemos hacerlo aquí, ya que nos diría que está borrado, pero no sabremos que entidad.
        '//      Por lo que cuando utilicemos DeleteEntityRow en un RegisterDeleteTask, tendremos que hacer una 
        '//      a MarcarComoEliminado.
        'Dim dpc As DeleteProcessContext = services.GetService(Of DeleteProcessContext)()
        'dpc.Deleted = True
    End Sub

#End Region

#Region " Update "

    ''// Métodos para poder guardar datos de entidades y que el motor sepa que hemos guardado. Para que este salve los cambios o no,
    ''// en función de si lo hemos hecho ya.
    <Task()> Public Shared Sub UpdateEntityRow(ByVal data As DataRow, ByVal services As ServiceProvider)
        '//Debemos guardar únicamente el registro que nos llega y mantener su estado.
        Dim State As DataRowState = data.RowState
        Dim dtUpdate As DataTable = data.Table.Clone
        dtUpdate.ImportRow(data)
        BusinessHelper.UpdateTable(dtUpdate)
        'Dim upc As UpdateProcessContext = services.GetService(Of UpdateProcessContext)()
        'upc.Updated = True
    End Sub

    <Task()> Public Shared Sub UpdateEntityDataTable(ByVal data As DataTable, ByVal services As ServiceProvider)
        '//Debemos mantener el estado de los registros del DataTable tras realizar los cambios.
        Dim dtCopy As DataTable = data.Copy
        BusinessHelper.UpdateTable(data)
        data = dtCopy.Copy
        'Dim upc As UpdateProcessContext = services.GetService(Of UpdateProcessContext)()
        'upc.Updated = True
    End Sub

    '<Task()> Public Shared Sub UpdateEntityDataSet(ByVal data As DataSet, ByVal services As ServiceProvider)
    '    For Each dt As DataTable In data.Tables
    '        UpdateEntityDataTable(dt, services)
    '    Next
    'End Sub

    <Task()> Public Shared Sub UpdateDocument(ByVal Doc As Document, ByVal services As ServiceProvider)
        If Not Doc Is Nothing Then
            Dim dt() As DataTable = Doc.GetTables()
            ProcessServer.ExecuteTask(Of Object)(AddressOf Business.General.Comunes.BeginTransaction, Nothing, services)
            Try
                For i As Integer = 0 To dt.Length - 1
                    BusinessHelper.UpdateTable(dt(i).Copy) ' Se está haciendo un copy para mantener el estado.
                    'i += 1
                Next
            Catch ex As Exception

            End Try
            

            ProcessServer.ExecuteTask(Of Boolean)(AddressOf Business.General.Comunes.CommitTransaction, False, services)
        End If
    End Sub

#End Region

#Region " Bases de Datos "

    <Task()> Public Shared Sub ActualizarDatosEntidad(ByVal data As DataTable, ByVal services As ServiceProvider)
        BusinessHelper.UpdateTable(data)
    End Sub

    <Task()> Public Shared Function GetComposeFilterSql(ByVal f As Filter, ByVal services As ServiceProvider) As String
        Return AdminData.ComposeFilter(f)
    End Function

    <Task()> Public Shared Function GetConnectionString(ByVal data As Object, ByVal services As ServiceProvider) As String
        Return AdminData.GetConnectionString
    End Function

    Public Function GetServer(Optional ByVal BaseDatos As String = "") As String
        If Length(BaseDatos) = 0 Then
            BaseDatos = AdminData.GetConnectionInfo.DataBase
        End If

        Dim ConnectionString As String = AdminData.GetConnectionInfo(BaseDatos).ConnectionString
        Dim SqlConn As New System.Data.SqlClient.SqlConnectionStringBuilder(ConnectionString)
        Return SqlConn.DataSource
    End Function

    Public Function GetServer(ByVal IDBaseDatos As Guid) As String
        If Length(IDBaseDatos) = 0 Then
            IDBaseDatos = AdminData.GetConnectionInfo.IDDataBase
        End If

        Dim ConnectionString As String = AdminData.GetConnectionInfo(IDBaseDatos).ConnectionString
        Dim SqlConn As New System.Data.SqlClient.SqlConnectionStringBuilder(ConnectionString)
        Return SqlConn.DataSource
    End Function

    Public Function GetBDsEmpresa() As DataTable
        '//Devuelve la BDs de un IDEmpresa, que están en el mismo servidor que la BD actual, exceptuando la BD actual.
        Dim f As New Filter
        f.Add(New GuidFilterItem("IDEmpresa", FilterOperator.NotEqual, AdminData.GetConnectionInfo.IDDataBase))
        'f.Add(New StringFilterItem("IDServidor", FilterOperator.Equal, GetServer))
        Return New BE.DataEngine().Filter("xDataBase", f, "IDBaseDatos, BaseDatos, DescBaseDatos, ConnectionString", "BaseDatos", , True)
    End Function

    Public Function GetEjerciciosEmpresa(ByVal strEmpresa As String) As DataTable
        Dim dt As DataTable = Nothing
        If Length(strEmpresa) > 0 Then
            Dim strBBDDInic As String = AdminData.GetConnectionInfo.DataBase
            '//Habilitamos la BD destino. Es decir, la BD activa a partir de este momento es la de Destino.
            AdminData.SetSessionDataBase(strEmpresa)
            dt = AdminData.GetData("tbMaestroEjercicio")
            '//Habilitamos la BD Origen. Es decir, la BD activa a partir de este momento es la de Origen.
            AdminData.SetSessionDataBase(strBBDDInic)
        End If
        Return dt
    End Function

    Public Function GetUserDataBase() As DataTable
        '//Devuelve la BDs del usuario
        Return AdminData.GetUserDataBases()
    End Function

    <Task()> Public Shared Function GetEsquemaBD(ByVal data As Object, ByVal services As ServiceProvider) As String
        Dim esquema As String = "dbo"
        Dim selectSQL As New System.Text.StringBuilder
        selectSQL.Append("select schema_name()")

        Dim cmd As Common.DbCommand = AdminData.GetCommand
        cmd.CommandText = selectSQL.ToString()
        Dim dt As DataTable = AdminData.Execute(cmd, ExecuteCommand.ExecuteReader)
        If dt.Rows.Count > 0 Then
            esquema = dt.Rows(0)(0)
        End If

        Return esquema
    End Function

    <Task()> Public Shared Function GetUserDataBases(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Return AdminData.GetUserDataBases
    End Function

#End Region

    <Serializable()> _
   Public Class DataValidarEstado
        Public IDDocumento As Integer
        Public TipoApunte As enumDiarioTipoApunte
        Public Descontabilizar As Boolean
        Public Sub New(ByVal IDDocumento As Integer, ByVal TipoApunte As enumDiarioTipoApunte)
            Me.IDDocumento = IDDocumento
            Me.TipoApunte = TipoApunte
        End Sub
    End Class

    <Task()> Public Shared Function ValidarEstado(ByVal data As DataValidarEstado, ByVal services As ServiceProvider) As Integer
        Dim APPParams As ParametroContabilidad = services.GetService(Of ParametroContabilidad)()
        If APPParams.ContabilidadMultiple Then
            Dim f As New Filter
            f.Add(New NumberFilterItem("IDDocumento", data.IDDocumento))
            f.Add(New NumberFilterItem("IDTipoApunte", data.TipoApunte))
            Dim dtTipoConta As DataTable = AdminData.GetData("vContabilidadMultiple", f)
            If dtTipoConta.Rows.Count > 0 Then
                If dtTipoConta.Rows.Count = 2 Then
                    ValidarEstado = enumContabilizado.Contabilizado
                ElseIf dtTipoConta.Rows.Count = 1 Then
                    If Nz(dtTipoConta.Rows(0)("EjercicioTributario"), False) Then
                        ValidarEstado = enumContabilizado.ContabilizadoTributario
                    Else
                        ValidarEstado = enumContabilizado.ContabilizadoNIIF
                    End If
                    If Not Nz(dtTipoConta.Rows(0)("AIva"), False) Then
                        ValidarEstado = enumContabilizado.NoContabilizado
                    End If
                End If
            Else
                ValidarEstado = enumContabilizado.NoContabilizado
            End If
        Else
            If data.Descontabilizar Then
                ValidarEstado = enumContabilizado.NoContabilizado
            Else
                ValidarEstado = enumContabilizado.Contabilizado
            End If
        End If
        Return ValidarEstado
    End Function


    'TODO: Revisar como mejorar esto
    <Task()> Public Shared Function CreateFinancieroGeneral(ByVal data As Object, ByVal services As ServiceProvider) As IFinanciero
        Dim assemblyFile As String = "Expertis.Business.Financiero.dll"
        Dim typeName As String = "Solmicro.Expertis.Business.Financiero.FinancieroInterface"
        If Len(assemblyFile) > 0 And Len(typeName) > 0 Then
            Dim assemblyObject As System.Reflection.Assembly

            If System.IO.File.Exists(assemblyFile) Then
                assemblyObject = System.Reflection.Assembly.LoadFrom(assemblyFile)
            Else
                assemblyObject = System.Reflection.Assembly.Load(IO.Path.GetFileNameWithoutExtension(IO.Path.GetFileName(assemblyFile)))
            End If
            Return CType(assemblyObject.CreateInstance(typeName, True), IFinanciero)
        End If
    End Function


    <Task()> Public Shared Function CreateBodegaGeneral(ByVal data As Object, ByVal services As ServiceProvider) As IBodega
        Dim assemblyFile As String = "Expertis.Business.Bodega.dll"
        Dim typeName As String = "Solmicro.Expertis.Business.Bodega.BdgInterface"
        If Len(assemblyFile) > 0 And Len(typeName) > 0 Then
            Dim assemblyObject As System.Reflection.Assembly

            If System.IO.File.Exists(assemblyFile) Then
                assemblyObject = System.Reflection.Assembly.LoadFrom(assemblyFile)
            Else
                assemblyObject = System.Reflection.Assembly.Load(IO.Path.GetFileNameWithoutExtension(IO.Path.GetFileName(assemblyFile)))
            End If
            Return CType(assemblyObject.CreateInstance(typeName, True), IBodega)
        End If
    End Function

    <Task()> Public Shared Function CreateEMCSGeneral(ByVal data As Object, ByVal services As ServiceProvider) As IEMCS
        Dim assemblyFile As String = "Expertis.Business.EMCS.dll"
        Dim typeName As String = "Solmicro.Expertis.Business.EMCS.EMCSInterface"
        If Len(assemblyFile) > 0 And Len(typeName) > 0 Then
            Dim assemblyObject As System.Reflection.Assembly

            If System.IO.File.Exists(assemblyFile) Then
                assemblyObject = System.Reflection.Assembly.LoadFrom(assemblyFile)
            Else
                assemblyObject = System.Reflection.Assembly.Load(IO.Path.GetFileNameWithoutExtension(IO.Path.GetFileName(assemblyFile)))
            End If
            Return CType(assemblyObject.CreateInstance(typeName, True), IEMCS)
        End If
    End Function

End Class

Public Class Utilidades


#Region " BulkCopy "

    Public Shared Sub BulkCopy(ByVal dt As DataTable, Optional ByVal DestinationTableName As String = "")
        Dim ConnectionString As String = AdminData.GetConnectionString
        If dt Is Nothing OrElse dt.Rows.Count = 0 Then Exit Sub

        Dim TableName As String = dt.TableName
        If Length(DestinationTableName) > 0 Then
            TableName = DestinationTableName
        End If
        If Length(TableName) = 0 Then ApplicationService.GenerateError("Debe indicar el nombre de la tabla.")

        Dim bCopy As New System.Data.SqlClient.SqlBulkCopy(ConnectionString)

        Dim datTableSchm As New DataGetTableSchema(TableName, ConnectionString)
        Dim tableSchema As DataTable = GetTableSchema(datTableSchm)
        Dim ColsFechas As New List(Of String)

        For Each col As DataColumn In dt.Columns
            Dim IdxDatatable As Integer = col.Ordinal
            If tableSchema.Columns.Contains(col.ColumnName) Then
                If tableSchema.Columns(col.ColumnName).DataType Is GetType(DateTime) Then
                    ColsFechas.Add(col.ColumnName)
                End If
                Dim IdxTable As Integer = tableSchema.Columns(col.ColumnName).Ordinal
                If IdxTable >= 0 Then
                    bCopy.ColumnMappings.Add(IdxDatatable, IdxTable)
                End If
            End If
        Next
        Dim colFecha As String
        If ColsFechas.Count > 0 Then
            For Each colFecha In ColsFechas
                Dim FechasIncorrectas As List(Of DataRow) = (From c In dt Where Not c.IsNull(colFecha) AndAlso Year(c(colFecha)) < Year(System.Data.SqlTypes.SqlDateTime.MinValue) Select c).ToList
                If Not FechasIncorrectas Is Nothing AndAlso FechasIncorrectas.Count > 0 Then
                    For Each rowIncorrecta As DataRow In FechasIncorrectas
                        rowIncorrecta(colFecha) = New Date(Year(System.Data.SqlTypes.SqlDateTime.MinValue), Month(rowIncorrecta(colFecha)), Day(rowIncorrecta(colFecha)))
                    Next
                End If
            Next
        End If


        bCopy.BulkCopyTimeout = 0
        bCopy.DestinationTableName = TableName
        bCopy.WriteToServer(dt)
    End Sub

    <Serializable()> _
    Public Class DataGetTableSchema
        Public TableName As String
        Public ConnectionString As String

        Public Sub New(ByVal TableName As String, ByVal ConnectionString As String)
            Me.TableName = TableName
            Me.ConnectionString = ConnectionString
        End Sub
    End Class
    Public Shared Function GetTableSchema(ByVal data As DataGetTableSchema) As DataTable
        If Length(data.TableName) = 0 Then ApplicationService.GenerateError("Debe indicar el nombre de la tabla.")
        If Length(data.ConnectionString) = 0 Then data.ConnectionString = AdminData.GetConnectionString

        Dim dt As New DataTable
        dt.TableName = data.TableName
        Dim strSelect As String = "SELECT * FROM " & data.TableName & " WHERE 1=2"
        Dim adapter As New SqlDataAdapter(strSelect, data.ConnectionString)
        Dim cmb As SqlCommandBuilder = New SqlCommandBuilder(adapter)
        adapter.Fill(dt)
        Return dt
    End Function

#End Region

#Region " Utilidades - Listas Genericas a Datatable "

    Public Shared Function GenericListToDataTable(Of T)(ByVal list As List(Of T)) As DataTable
        Dim table As New DataTable()
        Dim fields() As FieldInfo = GetType(T).GetFields
        For Each field As FieldInfo In fields
            If Not Nullable.GetUnderlyingType(field.FieldType) Is Nothing Then
                table.Columns.Add(field.Name, Nullable.GetUnderlyingType(field.FieldType))
            Else
                table.Columns.Add(field.Name, field.FieldType)
            End If
        Next
        Dim properties() As PropertyInfo = GetType(T).GetProperties
        For Each field As PropertyInfo In properties
            If Not Nullable.GetUnderlyingType(field.PropertyType) Is Nothing Then
                table.Columns.Add(field.Name, Nullable.GetUnderlyingType(field.PropertyType))
            Else
                table.Columns.Add(field.Name, field.PropertyType)
            End If
        Next
        For Each item As T In list
            Dim row As DataRow = table.NewRow()
            For Each field As FieldInfo In fields
                If field.GetValue(item) Is Nothing OrElse field.GetValue(item) Is DBNull.Value Then
                    row(field.Name) = System.DBNull.Value
                Else
                    row(field.Name) = field.GetValue(item)
                End If
            Next
            For Each field As PropertyInfo In properties
                If field.GetValue(item, Nothing) Is Nothing OrElse field.GetValue(item, Nothing) Is DBNull.Value Then
                    row(field.Name) = System.DBNull.Value
                Else
                    row(field.Name) = field.GetValue(item, Nothing)
                End If
            Next
            table.Rows.Add(row)
        Next
        table.AcceptChanges()
        Return table
    End Function

#End Region


End Class
