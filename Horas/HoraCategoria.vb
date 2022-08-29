Public Class HoraCategoria

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroHoraCategoria"

#End Region

#Region "Eventos RegisterDeleteTasks"

    Protected Overrides Sub RegisterDeleteTasks(ByVal deleteProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterDeleteTasks(deleteProcess)
        deleteProcess.AddTask(Of DataRow)(AddressOf General.Comunes.DeleteEntityRow)
        deleteProcess.AddTask(Of DataRow)(AddressOf General.Comunes.MarcarComoEliminado)
        deleteProcess.AddTask(Of DataRow)(AddressOf ActualizarHoraPredeterminada)
    End Sub

    <Task()> Public Shared Sub ActualizarHoraPredeterminada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data("HoraPredeterminada") Then
            Dim dtHora As DataTable = New HoraCategoria().Filter(New StringFilterItem("IDCategoria", data("IDCategoria")))
            If Not IsNothing(dtHora) AndAlso dtHora.Rows.Count > 0 Then
                dtHora.Rows(0)("HoraPredeterminada") = True
                BusinessHelper.UpdateTable(dtHora)
            End If
        End If
    End Sub

#End Region

#Region "Eventos GetBusinessRules"

    Public Overrides Function GetBusinessRules() As Engine.BE.BusinessRules
        Dim oBrl As New BusinessRules
        oBrl.Add("IDHora", AddressOf CambioHora)
        oBrl.Add("PrecioHoraA", AddressOf CambioPrecio)
        oBrl.Add("PrecioVentaA", AddressOf CambioPrecio)
        oBrl.Add("TasaDirectaA", AddressOf CambioTasa)
        oBrl.Add("TasaIndirectaA", AddressOf CambioTasa)
        Return oBrl
    End Function

    <Task()> Public Shared Sub CambioHora(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        If Length(data.Value) > 0 Then
            Dim dt As DataTable = New Hora().SelOnPrimaryKey(data.Value)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                ApplicationService.GenerateError("El código de hora no existe.")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub CambioPrecio(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        If Not IsNumeric(data.Value) Then ApplicationService.GenerateError("Campo no numérico.")
    End Sub

    <Task()> Public Shared Sub CambioTasa(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        If Not IsNumeric(data.Value) Then
            ApplicationService.GenerateError("Campo no numérico.")
        Else
            data.Current(data.ColumnName) = data.Value
            data.Current("PrecioHoraA") = Nz(data.Current("TasaDirectaA"), 0) + Nz(data.Current("TasaIndirectaA"), 0)
        End If
    End Sub

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarHora)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarHoraCategoria)
    End Sub

    <Task()> Public Shared Sub ValidarHora(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDHora")) = 0 Then ApplicationService.GenerateError("Introduzca el código de Hora.")
    End Sub

    <Task()> Public Shared Sub ValidarHoraCategoria(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dtKey As DataTable = New HoraCategoria().SelOnPrimaryKey(data("IDCategoria"), data("IDHora"), data("FechaDesde"))
            If Not dtKey Is Nothing AndAlso dtKey.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe este par Hora - Fecha Desde para la categoría actual.")
            End If
        End If
    End Sub

#End Region

#Region "Eventos RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf ActualizarFechaDesde)
        updateProcess.AddTask(Of DataRow)(AddressOf ActualizarTasaPrecio)
        updateProcess.AddTask(Of DataRow)(AddressOf TratarHoraPredeterminada)
    End Sub

    <Task()> Public Shared Sub ActualizarFechaDesde(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("FechaDesde")) = 0 Then data("FechaDesde") = Date.Today
    End Sub

    <Task()> Public Shared Sub ActualizarTasaPrecio(ByVal data As DataRow, ByVal services As ServiceProvider)
        Dim mA As MonedaInfo = ProcessServer.ExecuteTask(Of Date, MonedaInfo)(AddressOf Moneda.MonedaA, cnMinDate, services)
        Dim mB As MonedaInfo = ProcessServer.ExecuteTask(Of Date, MonedaInfo)(AddressOf Moneda.MonedaB, cnMinDate, services)
        data("TasaDirectaB") = xRound(Nz(data("TasaDirectaA"), 0) * mA.CambioB, mB.NDecimalesPrecio)
        data("TasaIndirectaB") = xRound(Nz(data("TasaIndirectaA"), 0) * mA.CambioB, mB.NDecimalesPrecio)
        data("PrecioHoraB") = xRound(Nz(data("PrecioHoraA"), 0) * mA.CambioB, mB.NDecimalesPrecio)
        data("PrecioVentaB") = xRound(Nz(data("PrecioVentaA"), 0) * mA.CambioB, mB.NDecimalesPrecio)
    End Sub

    <Task()> Public Shared Sub TratarHoraPredeterminada(ByVal data As DataRow, ByVal services As ServiceProvider)
        Dim strWhere As String = "IDCategoria='" & data("IDCategoria") & "' AND HoraPredeterminada= 1"
        Dim f As New Filter
        f.Add(New StringFilterItem("IDCategoria", data("IDCategoria")))
        f.Add(New BooleanFilterItem("HoraPredeterminada", True))

        Dim dtHora As DataTable = New HoraCategoria().Filter(f, "IDHora,FechaDesde,HoraPredeterminada")
        If dtHora.Rows.Count = 0 Then
            ' No hay más horas de ese tipo dentro de la categoría actual con lo cual será la predeterminada.
            data("HoraPredeterminada") = True
        Else
            If IsDBNull(data("HoraPredeterminada")) Then data("HoraPredeterminada") = False
            ' Si la hora ha sido marcada como predeterminada
            If data("HoraPredeterminada") Then
                dtHora.Rows(0)("HoraPredeterminada") = False
                BusinessHelper.UpdateTable(dtHora)
            ElseIf data.RowState = DataRowState.Modified AndAlso data("HoraPredeterminada") <> data("HoraPredeterminada", DataRowVersion.Original) AndAlso dtHora.Rows.Count = 1 Then
                data("HoraPredeterminada") = True
            End If
        End If
    End Sub

#End Region

#Region "Funciones Públicas"

    <Task()> Public Shared Sub ObtenerPrecioHoraCategoria(ByVal Data As IPropertyAccessor, ByVal services As ServiceProvider)
        If Not IsNothing(Data) Then
            If Data.ContainsKey("TasaPresupModA") Then Data("TasaPresupModA") = 0
            If Data.ContainsKey("PrecioVentaA") Then Data("PrecioVentaA") = 0
            If Data.ContainsKey("TasaRealModA") Then Data("TasaRealModA") = 0
            If Length(Data("IDCategoria")) > 0 And Length(Data("IDHora")) > 0 Then
                Dim dtmFecha As Date = Nz(Data("FechaInicio"), Date.Today)
                Dim StDatos As New DatosPrecioHoraCat
                StDatos.IDCategoria = Data("IDCategoria")
                StDatos.IDHora = Data("IDHora")
                StDatos.Fecha = dtmFecha
                Dim dt As DataTable = ProcessServer.ExecuteTask(Of DatosPrecioHoraCat, DataTable)(AddressOf ObtenerPrecioHoraCategoriaFecha, StDatos, services)
                If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                    If Data.ContainsKey("ImpPresupModVentaA") Then
                        Data("PrecioVentaA") = dt.Rows(0)("PrecioVentaA")
                        Data("TasaPresupModA") = dt.Rows(0)("PrecioHoraA")
                    Else
                        Data("PrecioVentaA") = dt.Rows(0)("PrecioVentaA")
                        If Data.ContainsKey("TasaRealModA") Then
                            If Nz(Data("TasaRealModA"), 0) = 0 Then
                                Data("TasaRealModA") = dt.Rows(0)("PrecioHoraA")
                            End If
                        Else
                            Data("TasaPrevModA") = dt.Rows(0)("PrecioHoraA")
                        End If
                    End If
                End If
                If Data.ContainsKey("TasaRealModA") AndAlso Data.ContainsKey("IDOperario") AndAlso Length(Data("IDOperario")) > 0 Then
                    'Meter aqui el acceso primero al histórico por fecha del operario para ver si hay que coger la tasa de ahi y no la general de su tabla

                    Dim FechaHora As New DateTime
                    If (Data.ContainsKey("FechaInicio") AndAlso Length(Data("FechaInicio")) > 0) Then
                        FechaHora = Data("FechaInicio")
                    ElseIf (Data.ContainsKey("Fecha") AndAlso Length(Data("Fecha")) > 0) Then
                        FechaHora = Data("Fecha")
                    Else : FechaHora = Today.Date
                    End If

                    Dim ClsHist As New HistoricoTasaOperarioHora
                    Dim FilHist As New Filter
                    FilHist.Add("IDOperario", FilterOperator.Equal, Data("IDOperario"))
                    FilHist.Add("IDHora", FilterOperator.Equal, Data("IDHora"))
                    FilHist.Add("FechaDesde", FilterOperator.LessThanOrEqual, FechaHora)
                    FilHist.Add("FechaHasta", FilterOperator.GreaterThanOrEqual, FechaHora)
                    Dim DtHist As DataTable = New HistoricoTasaOperarioHora().Filter(FilHist, "FechaHasta Desc")
                    If Not DtHist Is Nothing AndAlso DtHist.Rows.Count > 0 Then
                        Data("TasaRealModA") = DtHist.Rows(0)("TasaHorariaA")
                    Else
                        'Si tenemos el Operario se recupera su tasa horaria
                        Dim oh As New OperarioHora
                        Dim f As New Filter
                        f.Add(New StringFilterItem("IDOperario", Data("IDOperario")))
                        f.Add(New StringFilterItem("IDHora", Data("IDHora")))

                        Dim dtOH As DataTable = oh.Filter(f)
                        If Not IsNothing(dtOH) AndAlso dtOH.Rows.Count > 0 Then
                            Data("TasaRealModA") = dtOH.Rows(0)("TasaHorariaA")
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    <Serializable()> _
    Public Class DatosPrecioHoraCat
        Public IDCategoria As String
        Public IDHora As String
        Public Fecha As Date

        Public Sub New()
        End Sub

        Public Sub New(ByVal IDCategoria As String, ByVal IDHora As String, ByVal Fecha As Date)
            Me.IDCategoria = IDCategoria
            Me.IDHora = IDHora
            Me.Fecha = Fecha
        End Sub
    End Class
    <Task()> Public Shared Function ObtenerPrecioHoraCategoriaFecha(ByVal data As DatosPrecioHoraCat, ByVal services As ServiceProvider) As DataTable
        If Length(data.IDCategoria) > 0 Then
            Dim strOrderBy As String = String.Empty
            Dim f As New Filter
            f.Add(New StringFilterItem("IDCategoria", data.IDCategoria))
            If data.IDHora <> String.Empty Then
                f.Add(New StringFilterItem("IDHora", data.IDHora))
                f.Add(New DateFilterItem("FechaDesde", FilterOperator.LessThanOrEqual, data.Fecha))
                strOrderBy = "FechaDesde Desc"
            Else : f.Add(New BooleanFilterItem("HoraPredeterminada", True))
            End If
            Return New HoraCategoria().Filter(f, strOrderBy)
        End If
    End Function

    <Serializable()> _
    Public Class DatosPrecioHoraCatOper
        Public IDCategoria As String
        Public IDHora As String
        Public Fecha As Date
        Public IDOperario As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal IDCategoria As String, ByVal IDHora As String, ByVal Fecha As Date, ByVal IDOperario As String)
            Me.IDCategoria = IDCategoria
            Me.IDHora = IDHora
            Me.Fecha = Fecha
            Me.IDOperario = IDOperario
        End Sub
    End Class
   


    <Task()> Public Shared Function ObtenerPrecioHoraCategoriaOperario(ByVal data As DatosPrecioHoraCatOper, ByVal services As ServiceProvider) As Double
        If Length(data.IDOperario) > 0 AndAlso Length(data.IDHora) > 0 Then
            Dim ClsHist As New HistoricoTasaOperarioHora
            Dim FilHist As New Filter
            FilHist.Add("IDOperario", FilterOperator.Equal, data.IDOperario)
            FilHist.Add("IDHora", FilterOperator.Equal, data.IDHora)
            FilHist.Add("FechaDesde", FilterOperator.LessThanOrEqual, data.Fecha)
            FilHist.Add("FechaHasta", FilterOperator.GreaterThanOrEqual, data.Fecha)
            Dim DtHist As DataTable = ClsHist.Filter(FilHist)
            If Not DtHist Is Nothing AndAlso DtHist.Rows.Count > 0 Then
                Return Nz(DtHist.Rows(0)("TasaHorariaA"), 0)
            Else
                Dim OpeHora As New OperarioHora
                Dim e As New Filter
                e.Add("IDOperario", FilterOperator.Equal, data.IDOperario, FilterType.String)
                e.Add("IDHora", FilterOperator.Equal, data.IDHora, FilterType.String)
                Dim dtOperario As DataTable = OpeHora.Filter(e)
                If Not dtOperario Is Nothing AndAlso dtOperario.Rows.Count > 0 AndAlso Length(dtOperario.Rows(0)("TasaHorariaA")) > 0 Then
                    Return dtOperario.Rows(0)("TasaHorariaA")
                Else
                    Dim StDatos As New DatosPrecioHoraCat(data.IDCategoria, data.IDHora, data.Fecha)
                    Dim dtTasaHora As DataTable = ProcessServer.ExecuteTask(Of DatosPrecioHoraCat, DataTable)(AddressOf ObtenerPrecioHoraCategoriaFecha, StDatos, services)
                    If Not dtTasaHora Is Nothing AndAlso dtTasaHora.Rows.Count > 0 Then
                        Return Nz(dtTasaHora.Rows(0)("PrecioHoraA"), 0)
                    End If
                End If
            End If
        End If
    End Function

    <Task()> Public Shared Function GetHoraPredeterminada(ByVal IDCategoria As String, ByVal services As ServiceProvider) As String
        If Length(IDCategoria) > 0 Then
            Dim StDatos As New DatosPrecioHoraCat(IDCategoria, String.Empty, Date.Today)
            Dim dt As DataTable = ProcessServer.ExecuteTask(Of DatosPrecioHoraCat, DataTable)(AddressOf ObtenerPrecioHoraCategoriaFecha, StDatos, services)
            If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
                Return dt.Rows(0)("IDHora") & String.Empty
            End If
        End If
    End Function

    <Task()> Public Shared Function ObtenerPrecioHora(ByVal data As DatosPrecioHoraCatOper, ByVal services As ServiceProvider) As Double
        If Length(data.IDHora) = 0 Then
            data.IDHora = ProcessServer.ExecuteTask(Of String, String)(AddressOf GetHoraPredeterminada, data.IDCategoria, services)
        End If
        If Length(data.IDOperario) > 0 Then
            ObtenerPrecioHora = ProcessServer.ExecuteTask(Of DatosPrecioHoraCatOper, Double)(AddressOf ObtenerPrecioHoraCategoriaOperario, data, services)
        Else
            Dim StDatos As New DatosPrecioHoraCat(data.IDCategoria, data.IDHora, data.Fecha)
            Dim dtTasas As DataTable = ProcessServer.ExecuteTask(Of DatosPrecioHoraCat, DataTable)(AddressOf ObtenerPrecioHoraCategoriaFecha, StDatos, services)
            If Not dtTasas Is Nothing AndAlso dtTasas.Rows.Count > 0 Then
                ObtenerPrecioHora = Nz(dtTasas.Rows(0)("PrecioHoraA"), 0)
            End If
        End If
    End Function
#End Region

End Class



