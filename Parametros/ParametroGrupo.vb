Public Class ParametroGrupo

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper
    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub
    Private Const cnEntidad As String = "tbParametroGrupo"

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarIDGrupoParametro)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescGrupoParametro)
    End Sub

    <Task()> Public Shared Sub ValidarIDGrupoParametro(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            If Length(data("IDGrupoParametro")) > 0 Then
                If Not IsNumeric(data("IDGrupoParametro")) Then
                    ApplicationService.GenerateError("El código del grupo de parámetros debe ser numérico.")
                Else
                    Dim DtTemp As DataTable = New ParametroGrupo().SelOnPrimaryKey(data("IDGrupoParametro"))
                    If Not DtTemp Is Nothing AndAlso DtTemp.Rows.Count > 0 Then
                        ApplicationService.GenerateError("El Grupo ya existe.")
                    End If
                End If
            Else : ApplicationService.GenerateError("Introduzca el código del grupo de parámetros.")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarDescGrupoParametro(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescGrupoParametro")) = 0 Then
            ApplicationService.GenerateError("Introduzca la descripción del grupo de parámetros.")
        End If
    End Sub

#End Region

End Class