Public Class Pais

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbMaestroPais"

#End Region

#Region "Funciones Públicas"

    <Task()> Public Shared Function Nacional(ByVal data As String, ByVal services As ServiceProvider) As Boolean
        If Length(data) > 0 Then
            Dim dtPais As DataTable = New Pais().SelOnPrimaryKey(data)
            If Not IsNothing(dtPais) AndAlso dtPais.Rows.Count > 0 Then
                'Si el Cliente no tiene País se le considerará como Nacional.
                Return Not dtPais.Rows(0)("Extranjero")
            End If
        End If
    End Function

    <Task()> Public Shared Function NacionalNoCanariasCeutaMelilla(ByVal data As String, ByVal services As ServiceProvider) As Boolean
        If Length(data) > 0 Then
            Dim dtPais As DataTable = New Pais().SelOnPrimaryKey(data)
            If Not IsNothing(dtPais) AndAlso dtPais.Rows.Count > 0 Then
                'Si el Cliente no tiene País se le considerará como Nacional.
                Return Not dtPais.Rows(0)("Extranjero") AndAlso Not dtPais.Rows(0)("CanariasCeutaMelilla")
            End If
        End If
    End Function

    <Task()> Public Shared Function Intracomunitario(ByVal data As String, ByVal services As ServiceProvider) As Boolean
        If Length(data) > 0 Then
            Dim dtPais As DataTable = New Pais().SelOnPrimaryKey(data)
            If Not IsNothing(dtPais) AndAlso dtPais.Rows.Count > 0 Then
                'Si el Cliente no tiene País se le considerará como Nacional.
                If dtPais.Rows(0)("Extranjero") AndAlso dtPais.Rows(0)("CEE") AndAlso dtPais.Rows(0)("CanariasCeutaMelilla") = False Then
                    Return True
                End If
            End If
        End If
        Return False
    End Function

#End Region

#Region "Eventos GetBusinessRules"

    Public Overrides Function GetBusinessRules() As Engine.BE.BusinessRules
        Dim oBrl As New BusinessRules
        oBrl.Add("CanariasCeutaMelilla", AddressOf CambioCanarias)
        Return oBrl
    End Function

    <Task()> Public Shared Sub CambioCanarias(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
        If Not data.Value Is Nothing AndAlso data.Value = True Then
            data.Current("Extranjero") = False
            data.Current("CEE") = False
        End If
    End Sub

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarPais)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescPais)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarPais(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDPais")) = 0 Then ApplicationService.GenerateError("El País es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarDescPais(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescPais")) = 0 Then ApplicationService.GenerateError("La descripción del país es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dt As DataTable = New Pais().SelOnPrimaryKey(data("IDPais"))
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ApplicationService.GenerateError("Este País ya existe en la base de datos.")
            End If
        End If
    End Sub

#End Region

End Class