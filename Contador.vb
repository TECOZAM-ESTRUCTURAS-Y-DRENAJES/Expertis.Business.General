Public Class ContadorInfo
    Inherits ClassEntityInfo

    Public IDContador As String
    Public DescContador As String
    Public Texto As String
    Public Contador As Integer
    Public Longitud As Integer
    Public Formato As String
    Public Numerico As Boolean
    Public ContadorIni As Integer
    Public ContadorFin As Integer
    Public AIva As Boolean
    Public IDTipoComprobante As String
    Public DescTipoComprobante As String
    Public ClaveOperacion As Integer?

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal data As DataRow)
        MyBase.New(data)
    End Sub

    Public Sub New(ByVal IDContador As String)
        MyBase.New()
        Me.Fill(IDContador)
    End Sub

    Public Overloads Overrides Sub Fill(ByVal ParamArray PrimaryKey() As Object)
        Dim dt As DataTable
        Try
            dt = New BE.DataEngine().Filter("vNegContadorInfo", New StringFilterItem("IDContador", PrimaryKey(0)))
        Catch ex As Exception
            dt = New Contador().SelOnPrimaryKey(PrimaryKey(0))
        End Try

        If dt.Rows.Count > 0 Then
            Me.Fill(dt.Rows(0))
        Else
            ApplicationService.GenerateError("El Contador | no existe.", Quoted(PrimaryKey(0)))
        End If
    End Sub

End Class

Public Class Contador

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroContador"

    Private Const cCounterIDFieldName As String = "IDContador"

#End Region

#Region "Eventos RegisterAddNewTasks"

    Protected Overrides Sub RegisterAddnewTasks(ByVal addnewProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterAddnewTasks(addnewProcess)
        addnewProcess.AddTask(Of DataRow)(AddressOf FillDefaultValues)
    End Sub

    <Task()> Public Shared Sub FillDefaultValues(ByVal data As DataRow, ByVal services As ServiceProvider)
        data("ContadorIni") = 1
        data("ContadorFin") = 0
        data("Contador") = 1
    End Sub

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescContador)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarLongitud)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarContadores)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarContador)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarContadorMovimientos)
    End Sub

    <Task()> Public Shared Sub ValidarDescContador(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescContador")) = 0 Then ApplicationService.GenerateError("Introduzca la descripción del contador")
    End Sub

    <Task()> Public Shared Sub ValidarLongitud(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("Longitud")) = 0 Then ApplicationService.GenerateError("Debe establecer valor a Longitud.")
    End Sub

    <Task()> Public Shared Sub ValidarContadores(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("ContadorIni")) = 0 Then ApplicationService.GenerateError("Introduzca el valor inicial del contador.")
        If Length(data("ContadorFin")) = 0 Then ApplicationService.GenerateError("Introduzca el valor final del contador.")
        If data("ContadorIni") > data("ContadorFin") Then ApplicationService.GenerateError("El valor del contador inicial es mayor que el valor del contador final.")
    End Sub

    <Task()> Public Shared Sub ValidarContador(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDContador")) = 0 Then ApplicationService.GenerateError("Introduzca el código del contado")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim DtTemp As DataTable = New Contador().SelOnPrimaryKey(data("IDContador"))
            If Not DtTemp Is Nothing AndAlso DtTemp.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe un contador con esa clave")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarContadorMovimientos(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added OrElse (data.RowState = DataRowState.Modified AndAlso (data("IDContador") <> data("IDContador", DataRowVersion.Original) OrElse Nz(data("Contador")) <> Nz(data("Contador", DataRowVersion.Original)))) Then
            Dim AppParamsSTK As ParametroStocks = services.GetService(Of ParametroStocks)()
            Dim IDContador As String = AppParamsSTK.ContadorHistMovimientoPredeterminado
            If Length(IDContador) > 0 AndAlso IDContador.Equals(data("IDContador")) Then
                Dim Contadores As EntityInfoCache(Of ContadorInfo) = services.GetService(Of EntityInfoCache(Of ContadorInfo))()
                Dim ContInfo As ContadorInfo = Contadores.GetEntity(IDContador)
                If Not ContInfo.Numerico Then
                    ApplicationService.GenerateError("El contador de movimientos debe ser configurado como contador numérico.")
                ElseIf Nz(data("Contador"), 0) < ProcessServer.ExecuteTask(Of Object, Integer)(AddressOf GetMaxNumeroMovimiento, Nothing, services) Then
                    ApplicationService.GenerateError("El valor del contador '|' para los movimientos no puede ser inferior al mayor movimiento generado.", IDContador)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Funciones Públicas"

#Region " Controlar contador asociado a los movimientos "

    <Task()> Public Shared Function GetMaxNumeroMovimiento(ByVal data As Object, ByVal services As ServiceProvider) As Integer
        Dim strGetMax As String = "SELECT MAX(IDMovimiento) AS MaxMovimiento FROM tbHistoricoMovimiento"
        Return Nz(AdminData.Execute(strGetMax, ExecuteCommand.ExecuteScalar), 0)
    End Function

#End Region

    <Task()> Public Shared Function CounterDt(ByVal strEntityName As String, ByVal services As ServiceProvider) As DataTable
        Return AdminData.Execute("FwObtenerContadores", False, strEntityName)
    End Function

    <Task()> Public Shared Function CounterValueTx(ByVal strIDContador As String, ByVal services As ServiceProvider) As CounterTx
        Dim oRslt As New CounterTx
        Dim oRw As DataRow = Nothing
        Dim mMe As New Contador     'debido a que esta función es shared
        Dim DtDatos As DataTable = mMe.SelOnPrimaryKey(strIDContador)
        If DtDatos Is Nothing OrElse DtDatos.Rows.Count = 0 Then
            ApplicationService.GenerateError("No se encontró el contador |", strIDContador)
        Else : oRw = DtDatos.Rows(0)
        End If
        oRslt.DtCounter = DtDatos
        ProcessServer.ExecuteTask(Of DataRow, String)(AddressOf FormatCounterDr, oRw, services)
        oRslt.strCounterValue = ProcessServer.ExecuteTask(Of DataRow, String)(AddressOf FormatCounterDr, oRw, services)
        oRw("Contador") += 1
        Return oRslt
    End Function

    <Task()> Public Shared Function CounterValueID(ByVal strIdCounter As String, ByVal services As ServiceProvider) As String
        Dim dttCounter As DataTable
        Dim dtrCounter As DataRow
        Dim strCounterValue As String

        Dim mMe As New Contador     'debido a que esta función es shared
        dttCounter = mMe.SelOnPrimaryKey(strIdCounter)
        If Not dttCounter Is Nothing Then
            If dttCounter.Rows.Count > 0 Then
                dtrCounter = dttCounter.Rows(0)
                strCounterValue = ProcessServer.ExecuteTask(Of DataRow, String)(AddressOf FormatCounterDr, dtrCounter, services)
                dtrCounter("Contador") = CInt(dtrCounter("Contador")) + 1
                mMe.Update(dttCounter)
                Return strCounterValue
            End If
        End If
    End Function

    <Serializable()> _
    Public Class DatosCounterValue
        Public IDCounter As String
        Public TargetClass As BusinessHelper
        Public TargetField As String
        Public DateField As String
        Public DateValue As Date
        Public IDEjercicio As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal IDCounter As String, ByVal TargetClass As BusinessHelper, ByVal TargetField As String, ByVal DateField As String, ByVal DateValue As Date)
            Me.IDCounter = IDCounter
            Me.TargetClass = TargetClass
            Me.TargetField = TargetField
            Me.DateField = DateField
            Me.DateValue = DateValue
        End Sub
    End Class

    <Task()> Public Shared Function CounterValue(ByVal data As DatosCounterValue, ByVal services As ServiceProvider) As String
        Dim dtCounter As DataTable
        Dim Counter As DataRow
        Dim formattedValue As String

        Dim mMe As New Contador
        dtCounter = mMe.SelOnPrimaryKey(data.IDCounter)
        If dtCounter.Rows.Count > 0 Then
            Counter = dtCounter.Rows(0)
            formattedValue = ProcessServer.ExecuteTask(Of DataRow, String)(AddressOf FormatCounterDr, Counter, services)
            Dim f As New Filter
            f.Add(New StringFilterItem("IDContador", Counter("IDContador")))
            f.Add(New StringFilterItem(data.TargetField, formattedValue))
            If Length(data.IDEjercicio) > 0 Then
                f.Add(New StringFilterItem("IDEjercicio", data.IDEjercicio))
            Else
                f.Add(New NumberFilterItem("YEAR(" & data.DateField & ")", data.DateValue.Year))
            End If
            Dim control As DataTable = data.TargetClass.Filter(f)
            If control.Rows.Count > 0 Then
                ApplicationService.GenerateError("El " & data.TargetField & " {0} ya existe. Modifique el contador correspondiente.", Quoted(formattedValue))
            End If

            Counter("Contador") += 1
            mMe.Update(dtCounter)

            Return formattedValue
        End If
    End Function

    <Task()> Public Shared Function CounterDefault(ByVal strIdEntity As String, ByVal services As ServiceProvider) As DataTable
        Return AdminData.Execute("GetCounterDefault", False, strIdEntity)
    End Function

    <Serializable()> _
    Public Class DatosFormatCounter
        Public Numeric As Boolean
        Public Counter As Integer
        Public Len As Integer
        Public Text As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal Numeric As Boolean, ByVal Counter As Integer, ByVal Len As Integer, ByVal Text As String)
            Me.Numeric = Numeric
            Me.Counter = Counter
            Me.Len = Len
            Me.Text = Text
        End Sub
    End Class

    <Task()> Public Shared Function FormatCounter(ByVal data As DatosFormatCounter, ByVal services As ServiceProvider) As String
        Dim strCounter As String = CStr(data.Counter)
        If data.Numeric Then
        Else
            Dim intPad As Integer = data.Len - Len(strCounter) - Len(data.Text)
            If intPad > 0 Then
                strCounter = data.Text & New String("0", intPad) & strCounter
            Else
                strCounter = data.Text & strCounter
            End If
        End If
        Return strCounter
    End Function

    'No usar desde presentación
    <Task()> Public Shared Function FormatCounterDr(ByVal rwCounter As DataRow, ByVal services As ServiceProvider) As String
        Dim EsNumerico As Boolean
        Dim texto As String
        Dim longitud As Integer
        Dim ValorContador As Integer

        EsNumerico = rwCounter("Numerico")
        ValorContador = rwCounter("Contador")

        If Not rwCounter.IsNull("Longitud") Then
            longitud = rwCounter("Longitud")
        End If
        If rwCounter.IsNull("Texto") Then
            texto = String.Empty
        Else
            texto = rwCounter("Texto")
        End If
        Dim StDatos As New DatosFormatCounter
        StDatos.Numeric = EsNumerico
        StDatos.Len = longitud
        StDatos.Text = texto
        StDatos.Counter = ValorContador
        Return ProcessServer.ExecuteTask(Of DatosFormatCounter, String)(AddressOf FormatCounter, StDatos, services)
    End Function

    <Task()> Public Shared Function FormattedValueToValue(ByVal formattedValue As String, ByVal service As ServiceProvider) As Integer
        Dim valor As Integer
        If IsNumeric(formattedValue) Then
            valor = CInt(formattedValue)
        Else
            Dim pos As Integer = 1
            For Each c As Char In formattedValue
                If Char.IsNumber(c) Then
                    valor = CInt(formattedValue.Substring(pos))
                    Exit For
                Else
                    pos += 1
                End If
            Next
        End If

        Return valor
    End Function

    <Task()> Public Shared Function GetDefaultCounterValue(ByVal EntityName As String, ByVal services As ServiceProvider) As DefaultCounter
        Dim oRslt As New DefaultCounter
        Dim dtCont As DataTable = ProcessServer.ExecuteTask(Of String, DataTable)(AddressOf CounterDefault, EntityName, services)
        If Not dtCont Is Nothing AndAlso dtCont.Rows.Count <> 0 Then
            oRslt.CounterID = dtCont.Rows(0)("IDContador")
            Dim StDatos As New DatosFormatCounter(dtCont.Rows(0)("Numerico"), dtCont.Rows(0)("Contador"), dtCont.Rows(0)("Longitud"), dtCont.Rows(0)("Texto") & String.Empty)
            oRslt.CounterValue = ProcessServer.ExecuteTask(Of DatosFormatCounter, String)(AddressOf FormatCounter, StDatos, services)
        End If
        Return oRslt
    End Function

    <Serializable()> _
    Public Class DatosDefaultCounterValue
        Public row As DataRow
        Public EntityName As String
        Public FieldName As String
        Public CounterIDFieldName As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal row As DataRow, ByVal EntityName As String, ByVal FieldName As String, Optional ByVal CounterIDFieldName As String = Nothing)
            Me.row = row
            Me.EntityName = EntityName
            Me.FieldName = FieldName
            If Length(CounterIDFieldName) > 0 Then Me.CounterIDFieldName = CounterIDFieldName
        End Sub
    End Class

    <Task()> Public Shared Sub LoadDefaultCounterValue(ByVal data As DatosDefaultCounterValue, ByVal services As ServiceProvider)
        If Length(data.CounterIDFieldName) = 0 Then data.CounterIDFieldName = cCounterIDFieldName
        Dim oDC As DefaultCounter = ProcessServer.ExecuteTask(Of String, DefaultCounter)(AddressOf GetDefaultCounterValue, data.EntityName, services)
        If Not oDC Is Nothing Then
            data.row(data.FieldName) = oDC.CounterValue
            data.row(data.CounterIDFieldName) = oDC.CounterID
        End If
    End Sub

    <Serializable()> _
    Public Class DatosDecrementCounter
        Public IDCounter As String
        Public Value As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal IDCounter As String, ByVal Value As String)
            Me.IDCounter = IDCounter
            Me.Value = Value
        End Sub
    End Class
    <Task()> Public Shared Sub DecrementCounter(ByVal data As DatosDecrementCounter, ByVal services As ServiceProvider)
        Dim oMe As New Contador
        Dim oRw As DataRow = oMe.GetItemRow(data.IDCounter)
        oRw("Contador") -= 1
        If ProcessServer.ExecuteTask(Of DataRow, String)(AddressOf FormatCounterDr, oRw, services) = data.Value Then oMe.Update(oRw.Table)
    End Sub

    <Serializable()> _
    Public Class CounterTx
        Public strCounterValue As String
        Public DtCounter As DataTable
    End Class

    <Serializable()> _
    Public Class DefaultCounter
        Public CounterValue As String
        Public CounterID As String
    End Class

    <Task()> Public Shared Function ContadorNumerico(ByVal idContador As String, ByVal services As ServiceProvider) As Boolean
        Dim dt As DataTable = New Contador().SelOnPrimaryKey(idContador)
        Return dt.Rows(0)("Numerico")
    End Function

#End Region

End Class