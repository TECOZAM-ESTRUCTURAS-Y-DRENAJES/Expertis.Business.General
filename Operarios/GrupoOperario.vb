Public Class GrupoOperario
    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbGrupoOperario"

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarGrupo)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarOperario)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarGrupo(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDGrupo")) = 0 Then ApplicationService.GenerateError("El Grupo es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarOperario(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDOperario")) = 0 Then ApplicationService.GenerateError("El Operario es un dato obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim f As New Filter
            f.Add(New StringFilterItem("IDGrupo", data("IDGrupo")))
            f.Add(New StringFilterItem("IDOperario", data("IDOperario")))
            Dim dt As DataTable = New GrupoOperario().Filter(f)
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ApplicationService.GenerateError("Ya existe el Operario '{0}' para el Grupo '{1}'", data("IDOperario"), data("IDGrupo"))
            End If
        End If
    End Sub

#End Region

End Class
