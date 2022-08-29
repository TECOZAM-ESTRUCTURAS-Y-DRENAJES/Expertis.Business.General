Public Class Graficos

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper
    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbGraficos"

#End Region

#Region "Estructuras"

    <Serializable()> _
    Public Class StDatosOptions
        Public Options As GraphicOptionsInfo
        Public Entidad As String
        Public DrRecord As BusinessData
    End Class

#End Region

#Region "RegisterAddnewTasks"

    Protected Overrides Sub RegisterAddnewTasks(ByVal addnewProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterAddnewTasks(addnewProcess)
        addnewProcess.AddTask(Of DataRow)(AddressOf FillDefaultValues)
    End Sub

    <Task()> Public Shared Sub FillDefaultValues(ByVal data As DataRow, ByVal services As ServiceProvider)
        data("TipoGrafico") = CInt(enumTipoGrafico.Column)
        data("TipoSentencia") = CInt(enumTipoSentencia.SentenciaSQL)
        data("Predeterminado") = 0
        data("TipoRegistro") = CInt(enumTipoRegistro.TipoGrafico)
    End Sub

#End Region

#Region "Validate / Update"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarObligatorios)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarRegistro)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarSentencia)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarServidorBaseDatos)
    End Sub

    <Task()> Public Shared Sub ValidarObligatorios(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("Entidad")) = 0 Then ApplicationService.GenerateError("La entidad es un dato Obligatorio.")
        If Length(data("Descripcion")) = 0 Then ApplicationService.GenerateError("La Descripcion es un Dato Obligatorio.")
        If Length(data("Sentencia")) = 0 Then ApplicationService.GenerateError("La Sentencia es un Dato Obligatorio.")
        If Length(data("TipoSentencia")) = 0 Then ApplicationService.GenerateError("El Tipo de Sentencia es un Dato Obligatorio.")
        If Length(data("SentenciaFiltro1")) > 0 Then
            If Length(data("TipoCampoFiltro1")) = 0 Then ApplicationService.GenerateError("El Tipo de Campo de Filtrado es obligatorio.")
            If Length(data("CampoFiltro1")) = 0 Then ApplicationService.GenerateError("El Campo de Filtrado es obligatorio.")
        End If
    End Sub

    <Task()> Public Shared Sub ValidarRegistro(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim FilReg As New Filter
            If Length(data("TipoAreaGrafico")) > 0 Then FilReg.Add("TipoAreagrafico", FilterOperator.Equal, data("TipoAreaGrafico"))
            FilReg.Add("Entidad", FilterOperator.Equal, data("Entidad"))
            FilReg.Add("Descripcion", FilterOperator.Equal, data("Descripcion"))
            FilReg.Add("TipoSentencia", FilterOperator.Equal, data("TipoSentencia"))
            Dim DtSelect As DataTable = New Graficos().Filter(FilReg)
            If Not DtSelect Is Nothing AndAlso DtSelect.Rows.Count > 0 Then
                ApplicationService.GenerateError("El Registro Con Descripción: |, para la Entidad: |, ya existe en la Base de Datos.", data("Descripcion"), data("Entidad"))
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarSentencia(ByVal data As DataRow, ByVal services As ServiceProvider)
        Dim StrSentencia As String = CStr(data("Sentencia")).ToLower
        If StrSentencia.IndexOf("update") <> -1 OrElse StrSentencia.IndexOf("insert") <> -1 OrElse StrSentencia.IndexOf("delete") <> -1 OrElse _
            StrSentencia.IndexOf("create") <> -1 OrElse StrSentencia.IndexOf("drop") <> -1 OrElse StrSentencia.IndexOf("execute") <> -1 Then
            ApplicationService.GenerateError("No se pueden insertar instrucciones Insert o Update o Delete en el campo de Sentencia")
        End If
    End Sub

    <Task()> Public Shared Sub ValidarServidorBaseDatos(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("NombreServidor")) > 0 AndAlso Length(data("NombreBaseDatos")) = 0 Then
            ApplicationService.GenerateError("Nombre de Base de Datos es obligatorio si Nombre de Servidor está relleno.")
        End If
        If Length(data("NombreServidor")) = 0 AndAlso Length(data("NombreBaseDatos")) > 0 Then
            ApplicationService.GenerateError("Nombre de Servidor es obligatorio si Nombre de Base de Datos está relleno.")
        End If
    End Sub

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarIdentificador)
    End Sub

    <Task()> Public Shared Sub AsignarIdentificador(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then data("IDGrafico") = AdminData.GetAutoNumeric
    End Sub

#End Region

#Region "BusinessRules"

    Public Overrides Function GetBusinessRules() As Engine.BE.BusinessRules
        Dim oBRL As New BusinessRules
        oBRL.Add("Descripcion", AddressOf CambioDescripcion)
        oBRL.Add("TipoSentencia", AddressOf CambioTipoSentencia)
        Return oBRL
    End Function

    <Task()> Public Shared Sub CambioTipoSentencia(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        data.Current("Sentencia") = String.Empty
    End Sub

    <Task()> Public Shared Sub CambioDescripcion(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        If Length(data.Value) > 0 AndAlso Length(data.Current("Comentario")) = 0 Then
            data.Current("Comentario") = data.Value
        End If
    End Sub


#End Region

#Region "Funciones Públicas"

    <Task()> Public Shared Function GetEntitys(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Return New BE.DataEngine().Filter("xEntity", "Entidad", "", , , True)
    End Function

    <Task()> Public Shared Function GetEntitysGraphics(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Return AdminData.Execute("SELECT DISTINCT Entidad FROM tbGraficos", ExecuteCommand.ExecuteReader)
    End Function

    <Task()> Public Shared Function GetPrimaryKeyEntity(ByVal data As String, ByVal services As ServiceProvider) As DataTable
        Dim ClsBE As BusinessHelper = BusinessHelper.CreateBusinessObject(data)
        Return ClsBE.PrimaryKeyTable
    End Function

    <Task()> Public Shared Function GetSentencias(ByVal data As String, ByVal services As ServiceProvider) As DataTable
        Dim IntTipo As enumTipoAreaGrafico
        Select Case data
            Case "GRAPHCRM", "DIAGCMDCRM"
                'AREA CRM
                IntTipo = enumTipoAreaGrafico.CRM
            Case "GRAPHCOMER", "DIAGCMDCLIE"
                'AREA COMERCIAL
                IntTipo = enumTipoAreaGrafico.Comercial
            Case "GRAPHCOMP", "DIAGCMDPROV"
                'AREA COMPRAS
                IntTipo = enumTipoAreaGrafico.Compras
            Case "GRAPHCOMPVENTA", "DIAGCMDCOMPVTA"
                'AREA COMPRAVENTA
                IntTipo = enumTipoAreaGrafico.CompraVenta
            Case "GRAPHFINAN", "DIAGCMDCOBRO"
                'AREA FINANCIERO
                IntTipo = enumTipoAreaGrafico.Financiero
        End Select
        Dim FilSen As New Filter
        FilSen.Add("TipoAreaGrafico", FilterOperator.Equal, IntTipo)
        FilSen.Add("TipoRegistro", FilterOperator.Equal, CInt(enumTipoRegistro.TipoGrafico))
        Return New Graficos().Filter(FilSen)
    End Function

    <Task()> Public Shared Function LoadKeys(ByVal data As Graficos.StDatosOptions, ByVal services As ServiceProvider) As GraphicOptionsInfo
        Dim DtPrim As DataTable = ProcessServer.ExecuteTask(Of String, DataTable)(AddressOf Graficos.GetPrimaryKeyEntity, data.Entidad, services)
        Dim i As Integer = 0
        For Each Col As DataColumn In DtPrim.Columns
            data.Options.CamposEntidad &= IIf(Length(data.Options.CamposEntidad) > 0, ", " & Col.ColumnName, Col.ColumnName)
            data.Options.ValorCamposEntidad &= IIf(Length(data.Options.ValorCamposEntidad) > 0, ", " & data.DrRecord(Col.ColumnName), data.DrRecord(Col.ColumnName))
        Next
        Return data.Options
    End Function

    <Task()> Public Shared Function GetGraphicStyles(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Dim DtNew As New DataTable
        DtNew.Columns.Add("Text", GetType(String))

        Dim StrStyles(10) As String
        StrStyles(0) = "Aquarius"
        StrStyles(1) = "Default"
        StrStyles(2) = "LucidDream"
        StrStyles(3) = "Luminol"
        StrStyles(4) = "Nautilus"
        StrStyles(5) = "Neon"
        StrStyles(6) = "Pumpkin"
        StrStyles(7) = "RedPlanet"
        StrStyles(8) = "RoyaleVelvet"
        StrStyles(9) = "Peach"
        StrStyles(10) = "ThemePark"

        For Each StrSt As String In StrStyles
            Dim DrNew As DataRow = DtNew.NewRow
            DrNew("Text") = StrSt
            DtNew.Rows.Add(DrNew)
        Next
        Return DtNew
    End Function

    <Task()> Public Shared Function GetSentenciaCampos(ByVal data As String, ByVal services As ServiceProvider) As String
        Dim StrSentencia As String = data.Replace("@AñoMes", String.Empty)
        StrSentencia = data.Replace("@Año", String.Empty)
        If data.IndexOf("'@") <> -1 Then
            'Tiene Campo de Filtrado especial
            Return data.Substring(data.IndexOf("'@") + 2, data.IndexOf("'", data.IndexOf("'@") + 2) - (data.IndexOf("'@") + 2))
        Else
            ' no tiene campo de filtrado especial
            Return String.Empty
        End If
    End Function

#Region "Ejecución de Consultas"

    <Task()> Public Shared Function EjecutarSentencia(ByVal data As GraphicInfo, ByVal services As ServiceProvider) As DataTable
        If ProcessServer.ExecuteTask(Of GraphicInfo, Boolean)(AddressOf ComprobarEjecucionSentencia, data, New ServiceProvider) Then
            Select Case data.TipoSentencia
                Case enumTipoSentencia.SentenciaMDX
                    ProcessServer.ExecuteTask(Of GraphicInfo)(AddressOf Graficos.EjecutarSentenciaMDX, data, New ServiceProvider)
                Case enumTipoSentencia.SentenciaSQL
                    ProcessServer.ExecuteTask(Of GraphicInfo)(AddressOf Graficos.EjecutarSentenciaSQL, data, New ServiceProvider)
            End Select
            ProcessServer.ExecuteTask(Of GraphicInfo)(AddressOf Graficos.TratarTipografico, data, New ServiceProvider)
            Return data.DtResult
        Else : ApplicationService.GenerateError("La Sentencia del Grafico a ejecutar, necesita de Introducción de parámetros.")
        End If
    End Function

    <Task()> Public Shared Function ComprobarEjecucionSentencia(ByVal data As GraphicInfo, ByVal services As ServiceProvider) As Boolean
        If Length(data.SentenciaFiltro1) = 0 Then
            If Not data.FilterOptions.CamposEntidad Is Nothing AndAlso data.FilterOptions.CamposEntidad.Length > 0 _
            AndAlso Not data.FilterOptions.ValorCamposEntidad Is Nothing AndAlso data.FilterOptions.ValorCamposEntidad.Length > 0 _
            AndAlso (data.Sentencia.IndexOf("@ ") > 0 OrElse data.Sentencia.IndexOf("@" & data.FilterOptions.CamposEntidad)) > 0 Then
                Return True
            ElseIf data.Sentencia.IndexOf("@") <> -1 AndAlso ((data.FilterOptions.CamposEntidad Is Nothing OrElse data.FilterOptions.CamposEntidad.Length = 0) _
            AndAlso (data.FilterOptions.ValorCamposEntidad Is Nothing OrElse data.FilterOptions.ValorCamposEntidad.Length = 0)) Then
                Return False
            Else : Return True
            End If
        ElseIf Length(data.SentenciaFiltro1) > 0 Then
            Return True
        End If
    End Function

    <Task()> Public Shared Function TratarSentencia(ByVal data As GraphicInfo, ByVal services As ServiceProvider) As String
        'Interpretamos los @ que haya en las sentecias para traducirlos Valores @Año y @AñoMes
        Dim StrFinal As String = data.Sentencia.ToLower
        Select Case data.TipoSentencia
            Case enumTipoSentencia.SentenciaMDX
                StrFinal = StrFinal.Replace("@AñoMes", "[" & StrFinal.Replace("@AñoMes", Date.Today.Month) & "]")
                StrFinal = StrFinal.Replace("@Año", "[" & StrFinal.Replace("@Año", Date.Today.Month) & "]")
                StrFinal = StrFinal.Replace("@ ", data.FilterOptions.ValorCamposEntidad)
            Case enumTipoSentencia.SentenciaSQL
                If Length(data.SentenciaFiltro1) = 0 Then
                    StrFinal = StrFinal.Replace("@AñoMes", Date.Today.Month)
                    StrFinal = StrFinal.Replace("@Año", Date.Today.Year)
                    If Length(data.FilterOptions.CamposEntidad) > 0 AndAlso Length(data.FilterOptions.ValorCamposEntidad) > 0 Then
                        StrFinal = StrFinal.Replace("@" & data.FilterOptions.CamposEntidad.ToLower, data.FilterOptions.ValorCamposEntidad)
                    End If
                ElseIf Length(data.SentenciaFiltro1) > 0 AndAlso Length(data.ValorCampoFiltro1) > 0 Then
                    StrFinal = data.SentenciaFiltro1
                    StrFinal = StrFinal.Replace("@AñoMes", Date.Today.Month)
                    StrFinal = StrFinal.Replace("@Año", Date.Today.Year)
                    If Length(data.CampoFiltro1) > 0 AndAlso Length(data.ValorCampoFiltro1) > 0 Then
                        Select Case data.TipoCampoFiltro1
                            Case enumprtTipoParametro.prtInteger
                                StrFinal = StrFinal.Replace("@" & data.CampoFiltro1, CInt(data.ValorCampoFiltro1))
                            Case enumprtTipoParametro.prtDouble
                                StrFinal = StrFinal.Replace("@" & data.CampoFiltro1, CDbl(data.ValorCampoFiltro1))
                            Case enumprtTipoParametro.prtDate
                                StrFinal = StrFinal.Replace("@" & data.CampoFiltro1, data.ValorCampoFiltro1)
                            Case enumprtTipoParametro.prtString
                                StrFinal = StrFinal.Replace("@" & data.CampoFiltro1, "'" & data.ValorCampoFiltro1 & "'")
                            Case Else
                                StrFinal = StrFinal.Replace("@" & data.CampoFiltro1, data.ValorCampoFiltro1)
                        End Select
                    End If
                Else
                    StrFinal = StrFinal.Replace("@AñoMes", Date.Today.Month)
                    StrFinal = StrFinal.Replace("@Año", Date.Today.Year)
                    If Length(data.FilterOptions.CamposEntidad) > 0 AndAlso Length(data.FilterOptions.ValorCamposEntidad) > 0 Then
                        StrFinal = StrFinal.Replace("@" & data.FilterOptions.CamposEntidad.ToLower, data.FilterOptions.ValorCamposEntidad)
                    End If
                End If
        End Select
        Return StrFinal
    End Function

    <Task()> Public Shared Sub EjecutarSentenciaMDX(ByVal data As GraphicInfo, ByVal services As ServiceProvider)
        Try
            Dim ClsConex As OleDb.OleDbConnection
            If Length(data.NombreServidor) > 0 AndAlso Length(data.NombreBaseDatos) > 0 Then
                ClsConex = New OleDb.OleDbConnection("Provider=MSOLAP.4;Integrated Security=SSPI;Initial Catalog=" & data.NombreBaseDatos & ";Data Source=" & data.NombreServidor)
            Else : ClsConex = New OleDb.OleDbConnection("Provider=MSOLAP.4;Integrated Security=SSPI;Initial Catalog=" & data.ParamServer.BaseDatos & ";Data Source=" & data.ParamServer.Servidor)
            End If
            Dim StrSentencia As String = ProcessServer.ExecuteTask(Of GraphicInfo, String)(AddressOf TratarSentencia, data, New ServiceProvider)
            Dim AdapConex As New OleDb.OleDbDataAdapter(StrSentencia, ClsConex)
            Dim DtDatos As New DataTable
            AdapConex.Fill(DtDatos)
            If Not DtDatos Is Nothing AndAlso DtDatos.Rows.Count > 0 Then
                data.DtResult = DtDatos
            Else : ApplicationService.GenerateError("La sentencia ejecutada no devolvió datos.")
            End If
        Catch ex As Exception
            ApplicationService.GenerateError("Ocurrió un error en la ejecución de la sentencia: |", ex.Message)
        End Try
    End Sub

    <Task()> Public Shared Sub EjecutarSentenciaSQL(ByVal data As GraphicInfo, ByVal services As ServiceProvider)
        Try
            Dim StrSentencia As String = ProcessServer.ExecuteTask(Of GraphicInfo, String)(AddressOf TratarSentencia, data, New ServiceProvider)
            Dim DtDatos As DataTable = AdminData.Execute(StrSentencia, ExecuteCommand.ExecuteReader)
            If Not DtDatos Is Nothing AndAlso DtDatos.Rows.Count > 0 Then
                data.DtResult = DtDatos
            Else : ApplicationService.GenerateError("La sentencia ejecutada no devolvió datos.")
            End If
        Catch ex As Exception
            ApplicationService.GenerateError("Ocurrió un error en la ejecución de la sentencia: |", ex.Message)
        End Try
    End Sub

    <Task()> Public Shared Sub TratarTipografico(ByVal data As GraphicInfo, ByVal services As ServiceProvider)
        Select Case data.TipoGrafico
            Case enumTipoGrafico.Doughnut, enumTipoGrafico.Pie
                If data.DtResult.Columns.Count > 2 Then
                    For i As Integer = data.DtResult.Columns.Count - 1 To 2 Step -1
                        data.DtResult.Columns.RemoveAt(i)
                    Next
                End If
            Case enumTipoGrafico.StackedBar, enumTipoGrafico.StackedArea, enumTipoGrafico.StackedColumn
                If data.DtResult.Columns.Count > 3 OrElse data.DtResult.Columns.Count < 3 Then
                    ApplicationService.GenerateError("No se puede representar el gráfico en el tipo Seleccionado.")
                End If
        End Select
    End Sub

#End Region

#End Region

End Class

<Serializable()> _
Public Class ParamServerBDInfo

    Public Servidor As String
    Public BaseDatos As String

    Public Sub New()
        Dim DtDatos As DataTable = New BE.DataEngine().Filter("vGrafParametrosInfo", Nothing)
        If Not DtDatos Is Nothing AndAlso DtDatos.Rows.Count = 2 Then
            For Each Dr As DataRow In DtDatos.Select
                Select Case CStr(Dr("IDParametro"))
                    Case "GRFSERVER"
                        Me.Servidor = Dr("Valor")
                    Case "GRFBD"
                        Me.BaseDatos = Dr("Valor")
                End Select
            Next
        End If
    End Sub

    Public Sub SaveChanges(ByVal StrServidor As String, ByVal StrBaseDatos As String)
        If Length(StrServidor) > 0 Then
            Dim ClsParam As New Parametro
            Dim DtParam As DataTable = ClsParam.SelOnPrimaryKey("GRFSERVER")
            If Not DtParam Is Nothing AndAlso DtParam.Rows.Count > 0 Then
                DtParam.Rows(0)("Valor") = StrServidor
                ClsParam.Update(DtParam)
            End If
        End If
        If Length(BaseDatos) > 0 Then
            Dim ClsParam As New Parametro
            Dim DtParam As DataTable = ClsParam.SelOnPrimaryKey("GRFBD")
            If Not DtParam Is Nothing AndAlso DtParam.Rows.Count > 0 Then
                DtParam.Rows(0)("Valor") = StrBaseDatos
                ClsParam.Update(DtParam)
            End If
        End If
    End Sub

End Class

<Serializable()> _
Public Class GraphicInfo

    Public IDGrafico As Integer
    Public Entidad As String
    Public Descripcion As String
    Public Comentario As String
    Public Sentencia As String = String.Empty
    Public TipoSentencia As enumTipoSentencia
    Public TipoGrafico As enumTipoGrafico
    Public TipoAreaGrafico As enumTipoAreaGrafico
    Public EstiloGrafico As String
    Public UDMedida As Integer
    Public Predeterminado As Boolean
    Public NumeroGrafico As Integer
    Public ParamServer As New ParamServerBDInfo
    Public FilterOptions As New GraphicOptionsInfo
    Public DtResult As DataTable
    Public NombreServidor As String
    Public NombreBaseDatos As String
    Public CampoFiltro1 As String
    Public ValorCampoFiltro1 As String
    Public SentenciaFiltro1 As String
    Public TipoCampoFiltro1 As enumprtTipoParametro
    Public EntidadFiltro1 As String

    Public Sub New(ByVal StrEntidad As String, Optional ByVal FiltroSinArea As Boolean = False)
        Dim FilPred As New Filter
        FilPred.Add("Entidad", FilterOperator.Equal, StrEntidad)
        FilPred.Add("Predeterminado", FilterOperator.Equal, 1)
        If FiltroSinArea Then FilPred.Add(New IsNullFilterItem("TipoAreaGrafico"))
        Dim DtPredeter As DataTable = New Graficos().Filter(FilPred)
        If Not DtPredeter Is Nothing AndAlso DtPredeter.Rows.Count > 0 Then
            Dim DrG As DataRow = DtPredeter.Rows(0)
            Me.IDGrafico = DrG("IDGrafico")
            Me.Entidad = DrG("Entidad")
            Me.Descripcion = DrG("Descripcion")
            Me.Comentario = DrG("Comentario")
            Me.Sentencia = DrG("Sentencia")
            Me.TipoSentencia = DrG("TipoSentencia")
            Me.TipoGrafico = DrG("TipoGrafico")
            If Length(DrG("TipoAreaGrafico")) > 0 AndAlso Not FiltroSinArea Then
                Me.TipoAreaGrafico = DrG("TipoAreaGrafico")
            Else : Me.TipoAreaGrafico = -1
            End If
            Me.UDMedida = DrG("UDmedidaNums")
            Me.Predeterminado = DrG("Predeterminado")
            Me.NumeroGrafico = Nz(DrG("NumeroGrafico"), 0)
            Me.ParamServer = New ParamServerBDInfo
            Me.FilterOptions = New GraphicOptionsInfo
            Me.NombreBaseDatos = Nz(DrG("NombreBaseDatos"), String.Empty)
            Me.NombreServidor = Nz(DrG("NombreServidor"), String.Empty)
            Me.CampoFiltro1 = Nz(DrG("CampoFiltro1"), String.Empty)
            Me.SentenciaFiltro1 = Nz(DrG("SentenciaFiltro1"), String.Empty)
            Me.TipoCampoFiltro1 = Nz(DrG("TipoCampoFiltro1"), -1)
            Me.EntidadFiltro1 = Nz(DrG("EntidadFiltro1"), String.Empty)
            'Else : ApplicationService.GenerateError("No se han Encontrado un Gráfico Predeterminado para la entidad:|.|Revise los datos de Configuración de Gráficos.", StrEntidad, vbNewLine)
        End If
    End Sub

    Public Sub New(ByVal IntIDGrafico As Integer, Optional ByVal FiltroSinArea As Boolean = False)
        Dim DtGrafico As DataTable = New Graficos().SelOnPrimaryKey(IntIDGrafico)
        If Not DtGrafico Is Nothing AndAlso DtGrafico.Rows.Count > 0 Then
            Dim DrG As DataRow = DtGrafico.Rows(0)
            If Length(DrG("TipoAreaGrafico")) > 0 AndAlso Not FiltroSinArea Then
                Me.TipoAreaGrafico = DrG("TipoAreaGrafico")
            Else : Me.TipoAreaGrafico = -1
            End If
            Me.IDGrafico = DrG("IDGrafico")
            Me.Entidad = DrG("Entidad")
            Me.Descripcion = DrG("Descripcion")
            Me.Comentario = DrG("Comentario")
            Me.Sentencia = DrG("Sentencia")
            Me.TipoSentencia = DrG("TipoSentencia")
            Me.TipoGrafico = DrG("TipoGrafico")
            Me.UDMedida = DrG("UDMedidaNums")
            Me.Predeterminado = DrG("Predeterminado")
            Me.NumeroGrafico = Nz(DrG("NumeroGrafico"), 0)
            Me.ParamServer = New ParamServerBDInfo
            Me.FilterOptions = New GraphicOptionsInfo
            Me.NombreBaseDatos = Nz(DrG("NombreBaseDatos"), String.Empty)
            Me.NombreServidor = Nz(DrG("NombreServidor"), String.Empty)
            Me.CampoFiltro1 = Nz(DrG("CampoFiltro1"), String.Empty)
            Me.SentenciaFiltro1 = Nz(DrG("SentenciaFiltro1"), String.Empty)
            Me.TipoCampoFiltro1 = Nz(DrG("TipoCampoFiltro1"), -1)
            Me.EntidadFiltro1 = Nz(DrG("EntidadFiltro1"), String.Empty)
            'Else : ApplicationService.GenerateError("No se han Encontrado Datos relacionados con la configuración del Gráfico Buscado.")
        End If
    End Sub

    Public Sub ActualizarDatosGrafico(ByRef GrpInfo As GraphicInfo, ByVal IntIDGrafico As Integer)
        Dim DtGrafico As DataTable = New Graficos().SelOnPrimaryKey(IntIDGrafico)
        If Not DtGrafico Is Nothing AndAlso DtGrafico.Rows.Count > 0 Then
            Dim DrG As DataRow = DtGrafico.Rows(0)
            GrpInfo.IDGrafico = DrG("IDGrafico")
            GrpInfo.Entidad = DrG("Entidad")
            GrpInfo.Descripcion = DrG("Descripcion")
            GrpInfo.Comentario = DrG("Comentario")
            GrpInfo.Sentencia = DrG("Sentencia")
            GrpInfo.TipoSentencia = DrG("TipoSentencia")
            GrpInfo.TipoGrafico = DrG("TipoGrafico")
            GrpInfo.Predeterminado = DrG("Predeterminado")
            GrpInfo.NumeroGrafico = Nz(DrG("NumeroGrafico"))
            GrpInfo.UDMedida = Nz(DrG("UDMedidaNums"), CInt(enumUDMedidaNums.Millares))
            If GrpInfo.ParamServer Is Nothing Then Me.ParamServer = New ParamServerBDInfo
            If GrpInfo.FilterOptions Is Nothing Then Me.FilterOptions = New GraphicOptionsInfo
            GrpInfo.NombreServidor = Nz(DrG("NombreServidor"), String.Empty)
            GrpInfo.NombreBaseDatos = Nz(DrG("NombreBaseDatos"), String.Empty)
            GrpInfo.CampoFiltro1 = Nz(DrG("CampoFiltro1"), String.Empty)
            GrpInfo.SentenciaFiltro1 = Nz(DrG("SentenciaFiltro1"), String.Empty)
            GrpInfo.TipoCampoFiltro1 = Nz(DrG("TipoCampoFiltro1"), -1)
            GrpInfo.EntidadFiltro1 = Nz(DrG("EntidadFiltro1"), String.Empty)
            'Else : ApplicationService.GenerateError("No se han Encontrado Datos relacionados con la configuración del Gráfico Buscado.")
        End If
    End Sub

End Class

<Serializable()> _
Public Class GraphicOptionsInfo

    Public ProgramID As Guid
    Public ProgramAlias As String
    Public CamposEntidad As String
    Public ValorCamposEntidad As String
    Public VerLeyenda As Boolean
    Public Ver3D As Boolean
    Public VerResultados As Boolean
    Public OptionsEnabled As Boolean

    Public Sub New()
        Me.Ver3D = False
        Me.VerLeyenda = False
        Me.VerResultados = False
        Me.OptionsEnabled = False
    End Sub

End Class