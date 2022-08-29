Public Class ParametroSubGrupo

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper
    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub
    Private Const cnEntidad As String = "tbParametroSubGrupo"

#End Region
    
#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarIDSubGrupoParametro)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescSubGrupoParametro)
    End Sub

    <Task()> Public Shared Sub ValidarIDSubGrupoParametro(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            If Length(data("IDSubGrupoParametro")) > 0 Then
                If Not IsNumeric(data("IDSubGrupoParametro")) OrElse CInt(data("IDSubGrupoParametro")) <= 0 Then
                    ApplicationService.GenerateError("El código del subgrupo de parámetros debe ser numérico y mayor que 0")
                Else
                    Dim DtTemp As DataTable = New ParametroSubGrupo().SelOnPrimaryKey(data("IDSubGrupoParametro"), data("IDGrupoParametro"))
                    If Not DtTemp Is Nothing AndAlso DtTemp.Rows.Count > 0 Then
                        ApplicationService.GenerateError("El código del subGrupo ya existe en el grupo actual. ")
                    End If
                End If
            Else : ApplicationService.GenerateError("Introduzca el código del subgrupo de parámetros.")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarDescSubGrupoParametro(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescSubGrupoParametro")) = 0 Then
            ApplicationService.GenerateError("Introduzca la descripción del subgrupo de parámetros")
        End If
    End Sub

#End Region

End Class