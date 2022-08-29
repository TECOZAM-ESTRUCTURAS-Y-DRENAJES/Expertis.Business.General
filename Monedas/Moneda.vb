Imports Solmicro.Expertis.Engine.BE.Alerts

<Serializable()> _
Public Class MonedaInfo
    Public ID As String
    Public Texto As String
    Public CambioA As Double
    Public CambioB As Double
    Public Abreviatura As String
    Public NDecimalesPrecio As Integer
    Public NDecimalesImporte As Integer
    Public Fecha As Date
End Class

Public Class Moneda

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroMoneda"

#End Region

#Region "Eventos GetBusinessRules"

    Public Overrides Function GetBusinessRules() As Engine.BE.BusinessRules
        Dim oBRL As New BusinessRules
        oBRL.Add("CambioA", AddressOf CambioCambio)
        Return oBRL
    End Function

    <Task()> Public Shared Sub CambioCambio(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        data.Current("FechaCambio") = Date.Today
    End Sub

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescMoneda)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarMoneda)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarDescMoneda(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescMoneda")) = 0 Then ApplicationService.GenerateError("Introduzca la descripción de la moneda")
    End Sub

    <Task()> Public Shared Sub ValidarMoneda(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDMoneda")) = 0 Then ApplicationService.GenerateError("La Moneda es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim DtTemp As DataTable = New Moneda().SelOnPrimaryKey(data("IDMoneda"))
            If Not DtTemp Is Nothing AndAlso DtTemp.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe una moneda con esa clave. -")
            End If
        End If
    End Sub

#End Region

#Region "Eventos RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf General.Comunes.UpdateEntityRow)
        updateProcess.AddTask(Of DataRow)(AddressOf General.Comunes.MarcarComoActualizado)
        updateProcess.AddTask(Of DataRow)(AddressOf GuardarHistoricoMonedas)
    End Sub

    <Task()> Public Shared Sub GuardarHistoricoMonedas(ByVal data As DataRow, ByVal services As ServiceProvider)
        ' Guardamos en el Histórico de monedas.
        ' Si no existe esa fecha y moneda en la tabla tbHistoricoMoneda, la insertamos
        ' Si existe, cambiamos los valores CambioA y CambioB

        '.....................................
        Dim blnCambio As Boolean
        Dim ClsHistMon As New HistoricoMoneda
        Dim oF1 As New Filter
        oF1.Add("IDMoneda", FilterOperator.Equal, data("IDMoneda"))
        oF1.Add("Fecha", FilterOperator.Equal, data("FechaCambio"))
        Dim DtMoneda As DataTable = ClsHistMon.Filter(oF1)
        If Not DtMoneda Is Nothing AndAlso DtMoneda.Rows.Count > 0 Then
            ' Ya existe esa moneda-fecha. Si los tipos de cambio han variado, actualizamos 
            ' en histórico.
            If data("CambioA") <> DtMoneda.Rows(0)("CambioA") Then
                DtMoneda.Rows(0)("CambioA") = data("CambioA")
                blnCambio = True
            End If
            If data("CambioB") <> DtMoneda.Rows(0)("CambioB") Then
                DtMoneda.Rows(0)("CambioB") = data("CambioB")
                blnCambio = True
            End If
            If blnCambio Then ClsHistMon.Update(DtMoneda)
        Else
            ' No existe esa moneda-fecha
            Dim DrNew As DataRow = DtMoneda.NewRow
            DrNew("IDMoneda") = data("IDMoneda") 'Almacenamos los nuevos valores.
            DrNew("Fecha") = data("FechaCambio")
            DrNew("CambioA") = data("CambioA")
            DrNew("CambioB") = data("CambioB")
            DtMoneda.Rows.Add(DrNew)
            ClsHistMon.Update(DtMoneda)
        End If
    End Sub

#End Region

#Region "CAMBIO DE MONEDA"
    'Public Function CambioMoneda(ByVal data As DataRow, ByVal IDMoneda1 As String, ByVal IDMoneda2 As String, Optional ByVal Fecha As Date = cnMinDate) As DataRow
    '    Return General.CambioMoneda(data, IDMoneda1, IDMoneda2, Fecha)
    'End Function

    'Public Function CambioMoneda(ByVal data As DataTable, ByVal IDMoneda1 As String, ByVal IDMoneda2 As String, Optional ByVal Fecha As Date = cnMinDate) As DataTable
    '    Return General.CambioMoneda(data, IDMoneda1, IDMoneda2, Fecha)
    'End Function

    'Public Function CambioMoneda(ByVal data As IPropertyAccessor, ByVal IDMoneda1 As String, ByVal IDMoneda2 As String, Optional ByVal Fecha As Date = cnMinDate) As IPropertyAccessor
    '    Return General.CambioMoneda(data, IDMoneda1, IDMoneda2, Fecha)
    'End Function
#End Region

#Region "Funciones Públicas"

    <Serializable()> _
    Public Class DatosActuaCambiosAFecha
        Public Moneda As MonedaInfo
        Public Fecha As Date
    End Class

    <Task()> Public Shared Function ActualizarCambiosAFecha(ByVal data As DatosActuaCambiosAFecha, ByVal services As ServiceProvider) As MonedaInfo
        If data.Fecha > cnMinDate Then
            Dim historico As DataTable = AdminData.Execute("sp_DivisaCambio", False, data.Moneda.ID, data.Fecha)
            If historico.Rows.Count > 0 Then
                data.Moneda.CambioA = historico.Rows(0)("CambioA")
                data.Moneda.CambioB = historico.Rows(0)("CambioB")
                data.Moneda.Fecha = historico.Rows(0)("Fecha")
            End If
        End If
        Return data.Moneda
    End Function


    <Serializable()> _
   Public Class DataGetCambioMoneda
        Public IDMoneda As String
        Public Fecha As Date

        Public Sub New(ByVal IDMoneda As String, ByVal Fecha As Date)
            Me.IDMoneda = IDMoneda
            Me.Fecha = Fecha
        End Sub
    End Class
    <Task()> Public Shared Function GetCambioMoneda(ByVal data As DataGetCambioMoneda, ByVal services As ServiceProvider) As Double
        If Length(data.IDMoneda) > 0 AndAlso data.Fecha <> cnMinDate Then
            Dim mEsquema As String = ProcessServer.ExecuteTask(Of Object, String)(AddressOf Business.General.Comunes.GetEsquemaBD, Nothing, services)

            Dim strSQL As String = "SELECT " + mEsquema + ".fDivisaCambio('" + data.IDMoneda + "', '" + Format(data.Fecha, "yyyyMMdd") + "') AS CambioDivisa"

            Dim dtDivisa As DataTable = New BE.DataEngine().Filter(strSQL, Nothing)
            If dtDivisa.Rows.Count > 0 Then
                Return Nz(dtDivisa.Rows(0)("CambioDivisa"), 0)
            End If
        End If
    End Function


    <Task()> Public Shared Function ObtenerMonedas(ByVal strIDMoneda As String, ByVal services As ServiceProvider) As DataTable
        'Dim StrIDMonedaA As String = String.Empty
        'Dim StrIDMonedaB As String = String.Empty
        Dim StrSufix As String = String.Empty

        Dim p As New Parametro
        Dim MonedaInternaA As String = p.MonedaInternaA
        Dim MonedaInternaB As String = p.MonedaInternaB
        Dim FilMon As New Filter(FilterUnionOperator.Or)
        FilMon.Add("IDMoneda", FilterOperator.Equal, strIDMoneda)
        FilMon.Add("IDMoneda", FilterOperator.Equal, MonedaInternaA)
        FilMon.Add("IDMoneda", FilterOperator.Equal, MonedaInternaB)
        Dim DtMoneda As DataTable = New Moneda().Filter(FilMon)

        Dim DtMonedas As New DataTable
        DtMonedas.Columns.Add("IDMoneda", GetType(String))
        DtMonedas.Columns.Add("DescMoneda", GetType(String))
        DtMonedas.Columns.Add("Abreviatura", GetType(String))
        DtMonedas.Columns.Add("NDecimalesPrec", GetType(Integer))
        DtMonedas.Columns.Add("NDecimalesImp", GetType(Integer))
        DtMonedas.Columns.Add("IDMonedaA", GetType(String))
        DtMonedas.Columns.Add("DescMonedaA", GetType(String))
        DtMonedas.Columns.Add("AbreviaturaA", GetType(String))
        DtMonedas.Columns.Add("NDecimalesPrecA", GetType(Integer))
        DtMonedas.Columns.Add("NDecimalesImpA", GetType(Integer))
        DtMonedas.Columns.Add("IDMonedaB", GetType(String))
        DtMonedas.Columns.Add("DescMonedaB", GetType(String))
        DtMonedas.Columns.Add("AbreviaturaB", GetType(String))
        DtMonedas.Columns.Add("NDecimalesPrecB", GetType(Integer))
        DtMonedas.Columns.Add("NDecimalesImpB", GetType(Integer))

        Dim DrNew As DataRow = DtMonedas.NewRow
        For i As Integer = 1 To 3
            Dim StrMoneda As String = String.Empty
            Select Case i
                Case 1
                    StrMoneda = "IDMoneda = '" & strIDMoneda & "'"
                    StrSufix = String.Empty
                Case 2
                    StrMoneda = "IDMoneda = '" & MonedaInternaA & "'"
                    StrSufix = "A"
                Case 3
                    StrMoneda = "IDMoneda = '" & MonedaInternaB & "'"
                    StrSufix = "B"
            End Select
            Dim Dr() As DataRow = DtMoneda.Select(StrMoneda)
            If Dr.Length > 0 Then
                DrNew("IDMoneda" & StrSufix) = Dr(0)("IDMoneda")
                DrNew("DescMoneda" & StrSufix) = Dr(0)("DescMoneda")
                DrNew("Abreviatura" & StrSufix) = Dr(0)("Abreviatura")
                DrNew("NDecimalesPrec" & StrSufix) = Dr(0)("NDecimalesPrec")
                DrNew("NDecimalesImp" & StrSufix) = Dr(0)("NDecimalesImp")
            End If
        Next
        DtMonedas.Rows.Add(DrNew)
        Return DtMonedas
    End Function

    <Task()> Public Shared Function ObtenerMonedas2(ByVal strIDMoneda As String, ByVal services As ServiceProvider) As Hashtable
        Dim htMonedas As New Hashtable
        Dim Monedas As MonedaCache = services.GetService(Of MonedaCache)()
        If Length(strIDMoneda) > 0 Then
            htMonedas("Moneda") = Monedas.GetMoneda(strIDMoneda, cnMinDate)
        End If
        htMonedas("MonedaA") = Monedas.MonedaA
        htMonedas("MonedaB") = Monedas.MonedaB
        Return htMonedas
    End Function

    <Task()> Public Shared Function ObtenerMonedaA(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Dim MonedaInternaA As String = New Parametro().MonedaInternaA
        If Length(MonedaInternaA) > 0 Then
            Return New Moneda().SelOnPrimaryKey(MonedaInternaA)
        End If
        Return Nothing
    End Function

    <Task()> Public Shared Function ObtenerMonedaB(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Dim MonedaInternab As String = New Parametro().MonedaInternaB
        If Length(MonedaInternab) > 0 Then
            Return New Moneda().SelOnPrimaryKey(MonedaInternab)
        End If
        Return Nothing
    End Function

    <Task()> Public Shared Function MonedaA(ByVal Fecha As Date, ByVal services As ServiceProvider) As MonedaInfo
        Dim IDMonedaA As String = New Parametro().MonedaInternaA
        Dim StDatos As New DatosObtenerMoneda
        StDatos.IDMoneda = IDMonedaA
        StDatos.Fecha = Fecha
        MonedaA = ProcessServer.ExecuteTask(Of DatosObtenerMoneda, MonedaInfo)(AddressOf ObtenerMoneda, StDatos, services)
    End Function

    <Task()> Public Shared Function MonedaB(ByVal Fecha As Date, ByVal services As ServiceProvider) As MonedaInfo
        Dim IDMonedaB As String = New Parametro().MonedaInternaB
        Dim StDatos As New DatosObtenerMoneda
        StDatos.IDMoneda = IDMonedaB
        StDatos.Fecha = Fecha
        MonedaB = ProcessServer.ExecuteTask(Of DatosObtenerMoneda, MonedaInfo)(AddressOf ObtenerMoneda, StDatos, services)
    End Function

    <Serializable()> _
    Public Class DatosObtenerMoneda
        Public IDMoneda As String
        Public Fecha As Date
    End Class

    <Task()> Public Shared Function ObtenerMoneda(ByVal data As DatosObtenerMoneda, ByVal services As ServiceProvider) As MonedaInfo
        Dim m As New MonedaInfo
        Dim dt As DataTable = New Moneda().SelOnPrimaryKey(data.IDMoneda)
        If Not IsNothing(dt) And dt.Rows.Count Then
            Dim oRw As DataRow = dt.Rows(0)
            m.ID = dt.Rows(0)("IDMoneda")
            m.Texto = dt.Rows(0)("DescMoneda")
            m.CambioA = dt.Rows(0)("CambioA")
            m.CambioB = dt.Rows(0)("CambioB")
            If Not oRw.IsNull("Abreviatura") Then m.Abreviatura = dt.Rows(0)("Abreviatura")
            m.NDecimalesPrecio = dt.Rows(0)("NDecimalesPrec")
            m.NDecimalesImporte = dt.Rows(0)("NDecimalesImp")
            If data.Fecha = cnMinDate Then
                m.Fecha = Today
            Else
                m.Fecha = data.Fecha
            End If
            If m.Fecha <> Today OrElse dt.Rows(0)("FechaCambio") > Today Then
                Dim StDatos As New DatosActuaCambiosAFecha
                StDatos.Moneda = m
                StDatos.Fecha = data.Fecha
                m = ProcessServer.ExecuteTask(Of DatosActuaCambiosAFecha, MonedaInfo)(AddressOf ActualizarCambiosAFecha, StDatos, services)
            End If
        End If
        Return m
    End Function

    <Serializable()> _
    Public Class DatosFechaHistorico
        Public IDMoneda As String
        Public Fecha As Date
    End Class

    <Task()> Public Shared Function FechaHistorico(ByVal data As DatosFechaHistorico, ByVal services As ServiceProvider) As Boolean
        Dim oF1 As New Filter
        oF1.Add("IDMoneda", FilterOperator.Equal, data.IDMoneda)
        oF1.Add("Fecha", FilterOperator.Equal, data.Fecha)
        Dim DtMoneda As DataTable = New HistoricoMoneda().Filter(oF1)
        If Not DtMoneda Is Nothing AndAlso DtMoneda.Rows.Count > 0 Then
            Return True
        Else : Return False
        End If
    End Function

    <Task()> Public Shared Function ValidaMoneda(ByVal strIDMoneda As String, ByVal services As ServiceProvider) As DataTable
        Dim dt As DataTable = New Moneda().SelOnPrimaryKey(strIDMoneda)
        If IsNothing(dt) OrElse dt.Rows.Count = 0 Then
            ApplicationService.GenerateError("La Moneda | no existe.", strIDMoneda)
        End If
        Return dt
    End Function

#End Region

#Region " Obtención Divisas desde el BCE "

    <Task()> Public Shared Sub ImportarFicheroDivisasBCE(ByVal data As Object, ByVal services As ServiceProvider)
        Dim dtDivisasBCD As DataTable = ProcessServer.ExecuteTask(Of Object, DataTable)(AddressOf ObtenerDivisasBCE, Nothing, services)
        ProcessServer.ExecuteTask(Of DataTable)(AddressOf ActualizaDivisasBCE, dtDivisasBCD, services)
    End Sub

    <Task()> Public Shared Function ObtenerDivisasBCE(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Dim NombreFicheroBCE As String = ProcessServer.ExecuteTask(Of Object, String)(AddressOf GetNombreFicheroBCE, Nothing, services)
        If Length(NombreFicheroBCE) > 0 Then
            Dim m_xmlr As New XmlTextReader(NombreFicheroBCE)
            m_xmlr.WhitespaceHandling = WhitespaceHandling.None
            m_xmlr.Read()

            Dim DtNew As New DataTable
            DtNew.Columns.Add("Fecha", GetType(Date))
            DtNew.Columns.Add("CodISOMoneda", GetType(String))
            DtNew.Columns.Add("Cambio", GetType(Double))

            Dim FechaFichero As String
            Dim Fecha As Date
            While Not m_xmlr.EOF
                m_xmlr.Read()

                FechaFichero = m_xmlr.GetAttribute("time")
                If Length(FechaFichero) > 0 Then Fecha = ProcessServer.ExecuteTask(Of String, Date)(AddressOf GetFechaFicheroBCE, FechaFichero, services)

                Dim IDMoneda As String = m_xmlr.GetAttribute("currency")
                Dim Cambio As String = m_xmlr.GetAttribute("rate")
                If Length(IDMoneda) > 0 AndAlso Length(Cambio) > 0 And Fecha <> cnMinDate Then
                    Dim DrNew As DataRow = DtNew.NewRow
                    DrNew("Fecha") = Fecha
                    DrNew("CodISOMoneda") = IDMoneda
                    DrNew("Cambio") = ProcessServer.ExecuteTask(Of String, Double)(AddressOf ConvertirDecimal, Cambio, services)

                    DtNew.Rows.Add(DrNew)
                End If
            End While
            If Fecha = cnMinDate Then ApplicationService.GenerateError("En el fichero no se ha detectado la Fecha")
            m_xmlr.Close()
            Return DtNew
        Else
            ApplicationService.GenerateError("No se ha especificado el nombre del fichero de Divisas a Importar.")
        End If
    End Function

    <Task()> Public Shared Function GetNombreFicheroBCE(ByVal data As Object, ByVal services As ServiceProvider) As String
        Dim NombreFichero As String '= "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-daily.xml"
        NombreFichero = New Parametro().FicheroImportacionDivisas
        Return NombreFichero
    End Function

    <Task()> Public Shared Function GetFechaFicheroBCE(ByVal FechaFichero As String, ByVal services As ServiceProvider) As Date
        Dim Fecha As Date
        If Length(FechaFichero) > 0 Then
            Dim PartesFecha() As String = Split(FechaFichero, "-")
            If Not PartesFecha Is Nothing AndAlso PartesFecha.Count > 0 Then
                Dim Anio As Integer = PartesFecha(0)
                Dim Mes As Integer = PartesFecha(1)
                Dim Dia As Integer = PartesFecha(2)
                Fecha = New Date(Anio, Mes, Dia)
            End If

            If Fecha = cnMinDate Then
                ApplicationService.GenerateError("Ha ocurrido un error en la configuración de la fecha de los cambios. Se esperaba el formato yyyy-MM-dd")
            End If
        End If
        Return Fecha
    End Function

    <Task()> Public Shared Function ConvertirDecimal(ByVal sNumero As String, ByVal services As ServiceProvider) As Double
        Dim cnCHAR_NUMERIC_PUNTO As String = "."
        Return CDbl(Replace(sNumero, cnCHAR_NUMERIC_PUNTO, My.Application.Culture.NumberFormat.CurrencyDecimalSeparator))
    End Function


    Public Class DataGetCambioMonedaFichDivisas
        Public dtDivisasBCE As DataTable
        Public drDivisaBCE As DataRow
        Public MonedaInterna As Boolean

        Public Sub New(ByVal drDivisaBCE As DataRow, ByVal dtDivisasBCE As DataTable)
            Me.drDivisaBCE = drDivisaBCE
            Me.dtDivisasBCE = dtDivisasBCE
        End Sub
    End Class
    <Task()> Public Shared Function GetCambioMonedaAFichDivisas(ByVal data As DataGetCambioMonedaFichDivisas, ByVal services As ServiceProvider) As Double
        Dim NDecimalesCambio As Integer = ProcessServer.ExecuteTask(Of Object, Double)(AddressOf GetNDecimalesCambioDefaultValue, Nothing, services)

        Dim CambioA As Double = 0
        If data.drDivisaBCE("Cambio") <> 0 Then
            CambioA = xRound(1 / data.drDivisaBCE("Cambio"), NDecimalesCambio)
        End If
        Return CambioA
    End Function

    <Task()> Public Shared Function GetCambioMonedaBFichDivisas(ByVal data As DataGetCambioMonedaFichDivisas, ByVal services As ServiceProvider) As Double
        Dim NDecimalesCambio As Integer = ProcessServer.ExecuteTask(Of Object, Double)(AddressOf GetNDecimalesCambioDefaultValue, Nothing, services)

        Dim Monedas As MonedaCache = services.GetService(Of MonedaCache)()
        Dim MonInfoA As MonedaInfo = Monedas.MonedaA
        Dim MonInfoB As MonedaInfo = Monedas.MonedaB

        Dim CambioB As Double = 0
        Dim CambioMonB_BCE As Double
        If MonInfoA.ID <> MonInfoB.ID Then
            If Not data.dtDivisasBCE Is Nothing Then
                If data.MonedaInterna Then
                    Dim ExisteMonedaBEnBCE As List(Of DataRow) = (From c In data.dtDivisasBCE Where c("CodISOMoneda") = MonInfoB.ID Select c).ToList
                    If Not ExisteMonedaBEnBCE Is Nothing AndAlso ExisteMonedaBEnBCE.Count > 0 Then
                        CambioMonB_BCE = ExisteMonedaBEnBCE(0)("Cambio")
                        CambioB = xRound(CambioMonB_BCE, NDecimalesCambio)
                    End If
                Else
                    If data.drDivisaBCE("CodISOMoneda") <> MonInfoB.ID Then
                        '    CambioB = 1
                        'Else
                        Dim CambioA As Double = ProcessServer.ExecuteTask(Of DataGetCambioMonedaFichDivisas, Double)(AddressOf GetCambioMonedaAFichDivisas, data, services)
                        CambioB = xRound(CambioA * MonInfoA.CambioB, NDecimalesCambio)
                    End If
                End If
            End If
        ElseIf MonInfoA.ID = MonInfoB.ID Then
            CambioB = ProcessServer.ExecuteTask(Of DataGetCambioMonedaFichDivisas, Double)(AddressOf GetCambioMonedaAFichDivisas, data, services)
        End If

        Return CambioB
    End Function

    <Task()> Public Shared Function GetNDecimalesPrecioDefaultValue(ByVal data As Object, ByVal services As ServiceProvider) As Integer
        Return 2
    End Function

    <Task()> Public Shared Function GetNDecimalesImporteDefaultValue(ByVal data As Object, ByVal services As ServiceProvider) As Integer
        Return 2
    End Function

    <Task()> Public Shared Function GetNDecimalesCambioDefaultValue(ByVal data As Object, ByVal services As ServiceProvider) As Integer
        Return 8
    End Function

    <Task()> Public Shared Function GetMonedaEURO(ByVal data As Object, ByVal services As ServiceProvider) As String
        Dim dtMoneda As DataTable = New Moneda().Filter(New StringFilterItem("CodigoISO", "EUR"))
        If dtMoneda.Rows.Count > 0 Then
            Return dtMoneda.Rows(0)("IDMoneda")
        End If
    End Function

    <Task()> Public Shared Sub ActualizaDivisasBCE(ByVal dtDivisasBCE As DataTable, ByVal services As ServiceProvider)
        If dtDivisasBCE Is Nothing OrElse dtDivisasBCE.Rows.Count = 0 Then Exit Sub

        Dim m As New Moneda
        Dim Monedas As MonedaCache = services.GetService(Of MonedaCache)()
        Dim MonInfoA As MonedaInfo = Monedas.MonedaA
        Dim MonInfoB As MonedaInfo = Monedas.MonedaB
        Dim cnMONEDA_FICHERO_BCE As String = ProcessServer.ExecuteTask(Of Object, String)(AddressOf GetMonedaEURO, Nothing, services) '"EUR" '//El archivo que se descarga solo es válido para EUR

        Dim NDecimalesPrecio As Integer = ProcessServer.ExecuteTask(Of Object, Double)(AddressOf GetNDecimalesPrecioDefaultValue, Nothing, services)
        Dim NDecimalesImporte As Integer = ProcessServer.ExecuteTask(Of Object, Double)(AddressOf GetNDecimalesImporteDefaultValue, Nothing, services)

        Dim FechaCambio As Date = dtDivisasBCE.Rows(0)("Fecha")
        If Length(cnMONEDA_FICHERO_BCE) > 0 AndAlso MonInfoA.ID = cnMONEDA_FICHERO_BCE Then
            '//Si mi moneda no está en el fichero tb tendré que actualizar el cambio a la moneda B
            If MonInfoA.ID <> MonInfoB.ID Then
                Dim ExisteMonedaAEnBCE As List(Of DataRow) = (From c In dtDivisasBCE Where c("CodISOMoneda") = MonInfoA.ID Select c).ToList
                If ExisteMonedaAEnBCE Is Nothing OrElse ExisteMonedaAEnBCE.Count = 0 Then
                    Dim dt As DataTable = m.Filter(New FilterItem("CodigoISO", FilterOperator.Equal, MonInfoA.ID))
                    If dt.Rows.Count > 0 Then
                        Dim drMoneda As DataRow = dt.Rows(0)
                        If drMoneda("FechaCambio") <> FechaCambio Then
                            drMoneda = m.ApplyBusinessRule("FechaCambio", FechaCambio, drMoneda)
                            Dim datCambio As New DataGetCambioMonedaFichDivisas(Nothing, dtDivisasBCE)
                            datCambio.MonedaInterna = True
                            Dim CambioB As Double = ProcessServer.ExecuteTask(Of DataGetCambioMonedaFichDivisas, Double)(AddressOf GetCambioMonedaBFichDivisas, datCambio, services)
                            drMoneda = m.ApplyBusinessRule("CambioB", CambioB, drMoneda)

                            m.Update(dt)

                            Monedas.Clear()
                            MonInfoA = Monedas.MonedaA
                        End If
                    End If
                End If
            End If

            Dim RestoDivisas As List(Of DataRow) = (From c In dtDivisasBCE Where c("CodISOMoneda") <> MonInfoB.ID Select c).ToList
            If Not RestoDivisas Is Nothing AndAlso RestoDivisas.Count > 0 Then
                For Each drDivisaBCE As DataRow In RestoDivisas
                    Dim IDMonedaBCE As String = drDivisaBCE("CodISOMoneda")

                    Dim datCambio As New DataGetCambioMonedaFichDivisas(drDivisaBCE, dtDivisasBCE)
                    Dim CambioA As Double = ProcessServer.ExecuteTask(Of DataGetCambioMonedaFichDivisas, Double)(AddressOf GetCambioMonedaAFichDivisas, datCambio, services)
                    Dim CambioB As Double = ProcessServer.ExecuteTask(Of DataGetCambioMonedaFichDivisas, Double)(AddressOf GetCambioMonedaBFichDivisas, datCambio, services)


                    Dim dt As DataTable = m.Filter(New FilterItem("CodigoISO", FilterOperator.Equal, IDMonedaBCE))
                    If dt.Rows.Count > 0 Then
                        Dim drMoneda As DataRow = dt.Rows(0)
                        If drMoneda("FechaCambio") <> FechaCambio Then
                            drMoneda = m.ApplyBusinessRule("CambioA", CambioA, drMoneda)
                            drMoneda = m.ApplyBusinessRule("FechaCambio", FechaCambio, drMoneda)
                            drMoneda = m.ApplyBusinessRule("CambioB", CambioB, drMoneda)
                        End If
                    End If

                    m.Update(dt)
                Next
            End If

        End If

    End Sub

#End Region


End Class


Public Class MonedaImportacionBCE
    Inherits AlertExtensionBase

    Public Overrides Function MatchesCondition(ByVal args As Engine.BE.Alerts.AlertExtensionArgs) As Boolean
        Dim services As New ServiceProvider
        ProcessServer.ExecuteTask(Of Object)(AddressOf Moneda.ImportarFicheroDivisasBCE, Nothing, services)
        Return MyBase.MatchesCondition(args)
    End Function

    Public Overrides Function Message(ByVal args As Engine.BE.Alerts.AlertExtensionArgs) As String
        Return MyBase.Message(args)
    End Function

End Class