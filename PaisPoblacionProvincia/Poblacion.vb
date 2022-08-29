Public Class CodPostalInfo
    Inherits ClassEntityInfo

    Public IDPais As String
    Public DescPais As String
    Public DescPoblacion As String
    Public DescProvincia As String
    Public CodPostal As String
    Public NumeroPoblaciones As Integer
    Public DatosPoblaciones As DataTable

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal CodPostal As String, Optional ByVal IDPais As String = "")
        MyBase.New()
        Me.Fill(CodPostal, IDPais)
    End Sub

    Public Overloads Overrides Sub Fill(ByVal ParamArray PrimaryKey() As Object)
        Dim IDPais As String
        If Length(PrimaryKey(1)) > 0 Then
            IDPais = PrimaryKey(1)
        Else
            IDPais = New Parametro().Pais()
        End If
        Dim f As New Filter
        f.Add(New StringFilterItem("CodPostal", PrimaryKey(0)))
        f.Add(New StringFilterItem("IDPais", IDPais))
        DatosPoblaciones = New BE.DataEngine().Filter("vNegDatosCodPostal", f)
        NumeroPoblaciones = DatosPoblaciones.Rows.Count
        If NumeroPoblaciones = 1 Then
            Me.Fill(DatosPoblaciones.Rows(0))
        ElseIf DatosPoblaciones.Rows.Count > 0 Then
            DatosPoblaciones.Rows(0)("DescPoblacion") = ""
            Me.Fill(DatosPoblaciones.Rows(0))
        End If
    End Sub

End Class

Public Class Poblacion
    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroPoblacion"

#Region " RegisterValidateTasks "

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescPoblacion)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarCP)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarPais)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarProvincia)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarDescPoblacion(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescPoblacion")) = 0 Then ApplicationService.GenerateError("La Descripción de la población es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarCP(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("CodPostal")) = 0 Then ApplicationService.GenerateError("El Código Postal es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarPais(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDPais")) = 0 Then ApplicationService.GenerateError("El País es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarProvincia(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDProvincia")) = 0 Then ApplicationService.GenerateError("El Código de provincia es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dt As DataTable = New Poblacion().SelOnPrimaryKey(data("IDPais"), data("IDProvincia"), data("CodPostal"), data("DescPoblacion"))
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe este Código en la base de datos.")
            End If
        End If
    End Sub

#End Region

End Class