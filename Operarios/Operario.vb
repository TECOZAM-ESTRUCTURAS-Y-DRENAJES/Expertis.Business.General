Public Class OperarioInfo
    Inherits ClassEntityInfo

    Public IDOperario As String
    Public DescOperario As String
    Public IDCategoria As String
    Public DescCategoria As String
    Public IDOficio As String
    Public DescOficio As String
    Public DNI As String
    Public Email As String
    Public IDUsuario As Guid
    Public PermisoGD As Boolean
    Public FacturacionObras As Boolean
    Public IDEmpresa As String
    Public Direccion As String
    Public CodPostal As String
    Public Poblacion As String
    Public Provincia As String
    Public IDPais As String
    Public Telefono As String
    Public Fax As String
    Public NumeroIncripcionROPO As String
    Public CarneBasico As Boolean
    Public CarneCualif As Boolean
    Public CarneFumig As Boolean
    Public CarnePiloto As Boolean
    Public Asesor As Boolean
    Public NumeroIdentificacionAsesor As String
    Public IDGestionPlagas As String

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal data As DataRow)
        MyBase.New(data)
    End Sub

    Public Sub New(ByVal IDOperario As String)
        MyBase.New()
        Me.Fill(IDOperario)
    End Sub

    Public Sub New(ByVal gUsuario As Guid)
        MyBase.New()
        Me.Fill(gUsuario)
    End Sub

    Public Overloads Overrides Sub Fill(ByVal ParamArray PrimaryKey() As Object)
        Dim f As New Filter
        If TypeOf (PrimaryKey(0)) Is Guid Then
            f.Add(New GuidFilterItem("IDUsuario", PrimaryKey(0)))
        Else
            f.Add(New StringFilterItem("IDOperario", PrimaryKey(0)))
        End If
        Dim dtOpInfo As DataTable = New BE.DataEngine().Filter("vNegOperarioInfo", f)
        If dtOpInfo.Rows.Count > 0 Then
            Me.Fill(dtOpInfo.Rows(0))
        ElseIf Not TypeOf (PrimaryKey(0)) Is Guid Then
            ApplicationService.GenerateError("El Operario | no existe.", Quoted(PrimaryKey(0)))
            'Else
            '    ApplicationService.GenerateError("El Usuario indicado no tiene un Operario asociado.", Quoted(PrimaryKey(0)))
        End If
    End Sub

End Class

Public Class Operario
    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroOperario"

#Region "Eventos RegisterAddNewTasks"

    Protected Overrides Sub RegisterAddnewTasks(ByVal addnewProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterAddnewTasks(addnewProcess)
        addnewProcess.AddTask(Of DataRow)(AddressOf AsignarContadorPorDefecto)
        addnewProcess.AddTask(Of DataRow)(AddressOf AsignarTipoDoc)
    End Sub

    <Task()> Public Shared Sub AsignarContadorPorDefecto(ByVal data As DataRow, ByVal services As ServiceProvider)
        Dim StDatos As New Contador.DatosDefaultCounterValue
        StDatos.Row = data
        StDatos.EntityName = "Operario"
        StDatos.FieldName = "IDOperario"
        ProcessServer.ExecuteTask(Of Contador.DatosDefaultCounterValue)(AddressOf Contador.LoadDefaultCounterValue, StDatos, services)
    End Sub

    <Task()> Public Shared Sub AsignarTipoDoc(ByVal data As DataRow, ByVal services As ServiceProvider)
        data("TipoDocIdentidad") = New Parametro().TipoDocIdentificativo
    End Sub

#End Region

#Region "Eventos RegisterDeleteTasks"

    Protected Overrides Sub RegisterDeleteTasks(ByVal deleteProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterDeleteTasks(deleteProcess)
        deleteProcess.AddTask(Of DataRow)(AddressOf BorrarCalendarioOperario)
    End Sub

    <Task()> Public Shared Sub BorrarCalendarioOperario(ByVal data As DataRow, ByVal services As ServiceProvider)
        Dim strSql As String = "DELETE FROM tbCalendarioOperario WHERE  IDOperario='" & data("IDOperario") & "'"
        AdminData.Execute(strSql)
    End Sub

#End Region

#Region "Eventos GetBusinessRules"

    Public Overrides Function GetBusinessRules() As Engine.BE.BusinessRules
        Dim oBrl As New BusinessRules
        oBrl.Add("Nombre", AddressOf CambioNombreApellidos)
        oBrl.Add("Apellidos", AddressOf CambioNombreApellidos)
        oBrl.Add("CodPostal", AddressOf CambioCodPostal)
        oBrl.Add("DNI", AddressOf CambioPaisDocDNI)
        oBrl.Add("IDPais", AddressOf CambioPaisDocDNI)
        oBrl.Add("TipoDocIdentidad", AddressOf CambioPaisDocDNI)
        Return oBrl
    End Function

    <Task()> Public Shared Sub CambioNombreApellidos(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        data.Current(data.ColumnName) = data.Value
        Dim strSeparador As String = ", "
        If Length(data.Current("Nombre")) = 0 Then strSeparador = String.Empty
        data.Current("DescOperario") = data.Current("Apellidos") & strSeparador & data.Current("Nombre")
    End Sub

    <Task()> Public Shared Sub CambioCodPostal(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        If Length(data.Value) > 0 Then
            Dim infoCP As New CodPostalInfo(CStr(data.Value), data.Current("IDPais") & String.Empty)
            If Length(infoCP.DescPoblacion) > 0 Then data.Current("Poblacion") = infoCP.DescPoblacion
            If Length(infoCP.DescProvincia) > 0 Then data.Current("Provincia") = infoCP.DescProvincia
            If Length(infoCP.IDPais) > 0 Then data.Current("IDPais") = infoCP.IDPais
        End If
    End Sub

    <Task()> Public Shared Sub CambioPaisDocDNI(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        If Length(data.Value) > 0 AndAlso Length(data.Current("DNI")) > 0 Then
            data.Current(data.ColumnName) = data.Value
            ProcessServer.ExecuteTask(Of IPropertyAccessor)(AddressOf ValidaDocumentoIdentificativo, data.Current, services)
        End If
    End Sub

    <Task()> Public Shared Sub ValidaDocumentoIdentificativo(ByVal data As IPropertyAccessor, ByVal services As ServiceProvider)
        If Length(data("DNI")) > 0 AndAlso Nz(data("TipoDocIdentidad"), -1) <> -1 AndAlso Length(data("IDPais")) = 0 Then
            ApplicationService.GenerateError("Debe indicar el Pais.")
        End If
        Dim info As New DataDocIdentificacion(data("DNI"), data("IDPais"), data("TipoDocIdentidad"))
        info = ProcessServer.ExecuteTask(Of DataDocIdentificacion, DataDocIdentificacion)(AddressOf Comunes.ValidarDocumentoIdentificativo, info, services)
        If Not info.EsCorrecto Then
            ApplicationService.GenerateError("El Documento introducido no es un '|'. Intoduzca uno correcto o cambie de tipo de documento", info.TipoDocumento)
        End If
    End Sub

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarOperario)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescOperario)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDNI)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarUsuario)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
        validateProcess.AddTask(Of DataRow)(AddressOf General.Comunes.ValidarCodigoIBAN)
    End Sub

    <Task()> Public Shared Sub ValidarOperario(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDOperario")) = 0 Then ApplicationService.GenerateError("El Operario es un dato obligatorio.")
        If IsDBNull(data("NOperarioOrden")) Or Len(Nz(data("NOperarioOrden"), "")) = 0 Then
            data("NOperarioOrden") = Right("000000000000000" & data("IDOperario"), 10)
        End If
    End Sub

    <Task()> Public Shared Sub ValidarDescOperario(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescOperario")) = 0 Then ApplicationService.GenerateError("La descripción del Operario es un dato obligatorio.")
        If Length(data("IDOficio")) = 0 Then ApplicationService.GenerateError("El oficio es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarDNI(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("TipoDocIdentidad")) = 0 Then ApplicationService.GenerateError("Es necesario especificar el tipo de documento.")
        If Length(data("DNI")) = 0 Then ApplicationService.GenerateError("El documento de Identificación es un dato obligatorio.")
        ProcessServer.ExecuteTask(Of IPropertyAccessor)(AddressOf ValidaDocumentoIdentificativo, New DataRowPropertyAccessor(data), services)
    End Sub

    <Task()> Public Shared Sub ValidarUsuario(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDUsuario")) > 0 Then
            Dim f As New Filter
            f.Add(New GuidFilterItem("IDUsuario", FilterOperator.Equal, data("IDUsuario")))
            Dim dt As DataTable = ProcessServer.ExecuteTask(Of Filter, DataTable)(AddressOf DatosSistema.DevuelveUsuariosBD, f, services)
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                ApplicationService.GenerateError("Debe introducir uno de los usuarios de la lista o dejarlo vacío.")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dtOp As DataTable = New Operario().SelOnPrimaryKey(data("IDOperario"))
            If Not dtOp Is Nothing AndAlso dtOp.Rows.Count > 0 Then
                ApplicationService.GenerateError("El Operario ya existe.")
            End If
        End If
    End Sub

#End Region

#Region "Eventos RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarValoresPredeterminados)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarOperario)
    End Sub

    <Task()> Public Shared Sub AsignarValoresPredeterminados(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.IsNull("PrefijoNSS") Then data("PrefijoNSS") = Strings.Format(0, New String("0", 2))
        If data.IsNull("NSS") Then data("NSS") = Strings.Format(0, New String("0", 8))
        If data.IsNull("SufijoNSS") Then data("SufijoNSS") = Strings.Format(0, New String("0", 2))
        'David Velasco 26/7/22 Evita errores al insertar.
        If data.IsNull("C_H_N") Then data("C_H_N") = 0.0
        If data.IsNull("C_H_E") Then data("C_H_E") = 0.0
        If data.IsNull("C_H_X") Then data("C_H_X") = 0.0
    End Sub

    <Task()> Public Shared Sub AsignarOperario(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            If Length(data("IdContador")) > 0 Then
                data("IDOperario") = ProcessServer.ExecuteTask(Of String, String)(AddressOf Contador.CounterValueID, data("IDContador"), services)
            End If
            Dim dtOp As DataTable = New Operario().SelOnPrimaryKey(data("IDOperario"))
            If Not dtOp Is Nothing AndAlso dtOp.Rows.Count > 0 Then
                ApplicationService.GenerateError("El Operario ya existe.")
            End If
        End If
    End Sub

#End Region

#Region " ObtenerOperarioUsuario "

    <Serializable()> _
    Public Class DatosOperario
        Public IDOperario As String
        Public DescOperario As String
        Public IDCategoria As String
        Public DescCategoria As String
        Public IDOficio As String
        Public DescOficio As String
        Public Email As String
        Public IDUsuario As Guid
        Public PermisoGD As Boolean
        Public FacturacionObras As Boolean
        Public IDEmpresa As String
    End Class

    <Task()> Public Shared Function ObtenerOperarioUsuario(ByVal data As Object, ByVal services As ServiceProvider) As DatosOperario
        Dim Operarios As EntityInfoCache(Of OperarioInfo) = services.GetService(Of EntityInfoCache(Of OperarioInfo))()
        Dim Operario As OperarioInfo = Operarios.GetEntity(AdminData.GetSessionInfo.UserID)

        Dim o As New DatosOperario
        o.IDOperario = Operario.IDOperario
        o.DescOperario = Operario.DescOperario
        o.IDCategoria = Operario.IDCategoria
        o.DescCategoria = Operario.DescCategoria
        o.IDOficio = Operario.IDOficio
        o.DescOficio = Operario.DescOficio
        o.Email = Operario.Email
        o.IDUsuario = Operario.IDUsuario
        o.PermisoGD = Operario.PermisoGD
        o.FacturacionObras = Operario.FacturacionObras
        o.IDEmpresa = Operario.IDEmpresa

        Return o
    End Function

#End Region

    <Task()> Public Shared Function ObtenerIDOperarioUsuario(ByVal data As Object, ByVal services As ServiceProvider) As String
        Dim Operarios As EntityInfoCache(Of OperarioInfo) = services.GetService(Of EntityInfoCache(Of OperarioInfo))()
        Dim Operario As OperarioInfo = Operarios.GetEntity(AdminData.GetSessionInfo.UserID)
        Return Operario.IDOperario
    End Function

    <Task()> Public Shared Function ObtenerDescOperarioUsuario(ByVal data As Object, ByVal services As ServiceProvider) As String
        Dim Operarios As EntityInfoCache(Of OperarioInfo) = services.GetService(Of EntityInfoCache(Of OperarioInfo))()
        Dim Operario As OperarioInfo = Operarios.GetEntity(AdminData.GetSessionInfo.UserID)
        Return Operario.DescOperario
    End Function

    <Task()> Public Shared Function ObtenerMailOperarioUsuario(ByVal data As Object, ByVal services As ServiceProvider) As String
        Dim Operarios As EntityInfoCache(Of OperarioInfo) = services.GetService(Of EntityInfoCache(Of OperarioInfo))()
        Dim Operario As OperarioInfo = Operarios.GetEntity(AdminData.GetSessionInfo.UserID)
        Return Operario.Email
    End Function

    <Task()> Public Shared Function CanMoveFile(ByVal data As Object, ByVal services As ServiceProvider) As Boolean
        Dim Operarios As EntityInfoCache(Of OperarioInfo) = services.GetService(Of EntityInfoCache(Of OperarioInfo))()
        Dim Operario As OperarioInfo = Operarios.GetEntity(AdminData.GetSessionInfo.UserID)
        Return Operario.PermisoGD
    End Function

    <Task()> Public Shared Function PestañasRol(ByVal data As Object, ByVal services As ServiceProvider) As DataTable
        Dim f As New Filter
        f.Add(New GuidFilterItem("IDUsuario", AdminData.GetSessionInfo.UserID))
        Dim dtUsuario As DataTable = ProcessServer.ExecuteTask(Of Filter, DataTable)(AddressOf DatosSistema.DevuelveUsuariosBD, f, services)
        If Not dtUsuario Is Nothing AndAlso dtUsuario.Rows.Count > 0 Then
            Dim IDGrupo As Guid = dtUsuario.Rows(0)("IDGrupo")
            Return New BE.DataEngine().Filter("tbRHRolSolapaOperario", New GuidFilterItem("IDGrupo", IDGrupo))
        Else
            Return Nothing
        End If
    End Function

    Public Function devuelveIDTrabajo(ByVal IDObra As String) As String
        Try
            Dim dt As DataTable
            Dim txtSQL As String
            txtSQL = "select DISTINCT (IDTrabajo) from tbObraMODControl where IDObra='" & IDObra & "'"
            dt = AdminData.GetData(txtSQL)
            Dim dr As DataRow = dt.Rows(0)
            Return dr("IDTrabajo")
        Catch ex As Exception

        End Try
        Return ""
    End Function
    Public Function DevuelveUsuariosBD(Optional ByVal f As Filter = Nothing) As DataTable
        Dim EntID As Guid = AdminData.GetSessionInfo.Enterprise.EnterpriseID
        If IsNothing(f) Then f = New Filter
        f.Add(New GuidFilterItem("IDEmpresa", FilterOperator.Equal, EntID))
        Return AdminData.GetData("xUser", f, "IDUsuario, CUsuario", "CUsuario", , True)
    End Function

End Class