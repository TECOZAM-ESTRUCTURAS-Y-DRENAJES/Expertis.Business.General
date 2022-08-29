Public Class Correos

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroCorreos"

#End Region

#Region "Eventos Entidad"

    Protected Overrides Sub RegisterAddnewTasks(ByVal addnewProcess As Solmicro.Expertis.Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterAddnewTasks(addnewProcess)
        addnewProcess.AddTask(Of DataRow)(AddressOf FillDefaultValues)
    End Sub

    <Task()> Public Shared Sub FillDefaultValues(ByVal data As DataRow, ByVal services As ServiceProvider)
        data("UseSSL") = False
    End Sub

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Solmicro.Expertis.Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDatosObligatorios)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarEntidad)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarUsuario)
    End Sub

    <Task()> Public Shared Sub ValidarDatosObligatorios(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescCorreo")) = 0 Then ApplicationService.GenerateError("Debe establecer una descripción del correo configurado.")
        If Length(data("Correo")) = 0 Then ApplicationService.GenerateError("Se ha de establecer el correo electrónico.")
        If Length(data("SmtpServer")) = 0 Then ApplicationService.GenerateError("El Servidor SMTP es obligatorio.")
        If Length(data("PortServer")) = 0 Then ApplicationService.GenerateError("El Puerto del Servidor SMTP es obligatorio.")
        If Length(data("UserName")) = 0 Then ApplicationService.GenerateError("El Nombre de Usuario es obligatorio.")
        If Length(data("Entidad")) = 0 AndAlso Length(data("Usuario")) = 0 Then
            ApplicationService.GenerateError("Debe establecer una entidad o un usuario para la entrada: |.", data("DescCorreo"))
        End If
    End Sub

    <Task()> Public Shared Sub ValidarEntidad(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim DtCorreos As DataTable = New Correos().Filter(New FilterItem("Entidad", FilterOperator.Equal, data("Entidad")))
            If Not DtCorreos Is Nothing AndAlso DtCorreos.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe una cuenta de correo configurada para la entidad: |.", data("Entidad"))
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarUsuario(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim DtCorreos As DataTable = New Correos().Filter(New GuidFilterItem("Usuario", data("Usuario")))
            If Not DtCorreos Is Nothing AndAlso DtCorreos.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe una cuenta de correo configurada para el Usuario: |.", data("Usuario"))
            End If
        End If
    End Sub

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Solmicro.Expertis.Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarClavePrimaria)
    End Sub

    <Task()> Public Shared Sub AsignarClavePrimaria(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDCorreo")) = 0 AndAlso data.RowState = DataRowState.Added Then
            data("IDCorreo") = AdminData.GetAutoNumeric
        End If
    End Sub


#End Region

#Region "Tareas Públicas"

    <Serializable()> _
    Public Class DataCorreos
        Public Entidad As String
        Public IDUsuario As Guid

        Public Sub New()
        End Sub

        Public Sub New(ByVal Entidad As String, ByVal IDUsuario As Guid)
            Me.Entidad = Entidad
            Me.IDUsuario = IDUsuario
        End Sub
    End Class

    <Task()> Public Shared Function LoadSmtpServerInfo(ByVal data As DataCorreos, ByVal services As ServiceProvider) As SmtpServerInfo
        '1º Buscamos por usuario actual de Expertis
        Dim DtCorreos As DataTable = New Correos().Filter(New GuidFilterItem("Usuario", data.IDUsuario))
        If Not DtCorreos Is Nothing AndAlso DtCorreos.Rows.Count > 0 Then
            Return New SmtpServerInfo(DtCorreos, data.Entidad, data.IDUsuario)
        End If

        '2º Si no encontramos buscamos por Entidad
        If Length(data.Entidad) > 0 Then
            DtCorreos = New Correos().Filter(New FilterItem("Entidad", FilterOperator.Equal, data.Entidad))
            If Not DtCorreos Is Nothing AndAlso DtCorreos.Rows.Count > 0 Then
                Return New SmtpServerInfo(DtCorreos, data.Entidad, data.IDUsuario)
            Else : ApplicationService.GenerateError("No se ha configurado cuenta de correo electrónico para el módulo: | y/o usuario: |.", data.Entidad, AdminData.GetSessionInfo.UserName)
            End If
        Else : ApplicationService.GenerateError("No se ha configurado cuenta de correo electrónico para el módulo: | y/o usuario: |.", data.Entidad, AdminData.GetSessionInfo.UserName)
        End If
    End Function

    <Task()> Public Shared Sub SaveSmtpServerInfo(ByVal data As SmtpServerInfo, ByVal services As ServiceProvider)
        Dim ClsCorreos As New Correos
        Dim DtCorreos As DataTable = ClsCorreos.SelOnPrimaryKey(data.IDCorreo)
        If Not DtCorreos Is Nothing AndAlso DtCorreos.Rows.Count > 0 Then
            DtCorreos.Rows(0)("SmtpServer") = data.SmtpServerName
            DtCorreos.Rows(0)("PortServer") = data.SmtpServerPort
            DtCorreos.Rows(0)("UseSSL") = data.UseSSL
            DtCorreos.Rows(0)("UserName") = data.UserName
            DtCorreos.Rows(0)("Correo") = data.UserMail
            ClsCorreos.Update(DtCorreos)
        End If
        Dim FilParam As New Filter(FilterUnionOperator.Or)
        FilParam.Add("IDParametro", FilterOperator.Equal, SmtpServerInfo.cnAsuntoParam)
        FilParam.Add("IDParametro", FilterOperator.Equal, SmtpServerInfo.cnCuerpoParam)
        Dim DtParam As DataTable = New Parametro().Filter(FilParam)
        If Not DtParam Is Nothing AndAlso DtParam.Rows.Count > 0 Then
            For Each DrParam As DataRow In DtParam.Select
                Select Case CStr(DrParam("IDParametro"))
                    Case SmtpServerInfo.cnAsuntoParam
                        DrParam("Valor") = data.Asunto
                    Case SmtpServerInfo.cnCuerpoParam
                        DrParam("Valor") = data.Cuerpo
                End Select
            Next
            BusinessHelper.UpdateTable(DtParam)
        End If
    End Sub

#End Region

End Class

<Serializable()> _
Public Class EmailInfo
    Public Email As String
    Public EMailsCC As String
    Public EMailsCCO As String
    Public Asunto As String
    Public Mensaje As String
    Public FicheroMensaje As String
    Public FicheroAdjunto As String
End Class

<Serializable()> _
Public Class SmtpServerInfo
    Public IDCorreo As Integer
    Public Entidad As String
    Public Usuario As Guid
    Public SmtpServerName As String
    Public SmtpServerPort As Integer?
    Public UseSSL As Boolean?
    Public UserMail As String
    Public UserName As String
    Public UserPassword As String
    Public Asunto As String
    Public Const cnAsuntoParam As String = "FELECASUN"
    Public Const cnCuerpoParam As String = "FELECHTM"
    Public Cuerpo As String

    Public Sub New()
    End Sub

    Public Sub New(ByVal DtCorreos As DataTable, ByVal Entidad As String, ByVal Usuario As Guid)
        Me.IDCorreo = DtCorreos.Rows(0)("IDCorreo")
        Me.Entidad = Entidad
        Me.Usuario = Usuario
        Me.SmtpServerName = DtCorreos.Rows(0)("SmtpServer")
        Me.SmtpServerPort = DtCorreos.Rows(0)("PortServer")
        Me.UseSSL = DtCorreos.Rows(0)("UseSSL")
        Me.UserMail = DtCorreos.Rows(0)("Correo")
        Me.UserName = DtCorreos.Rows(0)("UserName")
        If Length(DtCorreos.Rows(0)("UserPassword")) > 0 Then Me.UserPassword = DtCorreos.Rows(0)("UserPassword")
        Dim ClsParam As New Parametro
        Me.Asunto = ClsParam.FactElecAsuntoCorreo
        Me.Cuerpo = ClsParam.FactElecCuerpoMensajeHTMCorreo
    End Sub
End Class

<Serializable()> _
Public Class MailOptionsInfo
    Public Mails() As EmailInfo
    Public SmtpServerConfig As New SmtpServerInfo
End Class