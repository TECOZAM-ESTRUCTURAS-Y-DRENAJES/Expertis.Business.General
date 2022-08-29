Public Class TipoTurnoLinea

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbTipoTurnoLinea"

#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarInicio)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarFin)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarFactor)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarIDTurno)
    End Sub

    <Task()> Public Shared Sub ValidarInicio(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("Inicio")) <= 0 Then ApplicationService.GenerateError("El campo Inicio es obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarFin(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("Fin")) <= 0 Then ApplicationService.GenerateError("El campo Fin es obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarFactor(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("Factor")) <= 0 Then ApplicationService.GenerateError("El campo Factor es obligatorio.")
    End Sub

    <Task()> Public Shared Sub ValidarIDTurno(ByVal data As DataRow, ByVal services As ServiceProvider)
        Dim StDatos As New DatosValidarTurno
        StDatos.IDTipoTurno = data("IDTipoTurno") : StDatos.DtNuevoTurno = data.Table
        If Not ProcessServer.ExecuteTask(Of DatosValidarTurno, Boolean)(AddressOf ValidarTurno, StDatos, services) Then
            ApplicationService.GenerateError("No se permite solapamiento entre turnos. Verifique los rangos horarios introducidos.")
        End If
    End Sub

    <Serializable()> _
    Public Class DatosValidarTurno
        Public IDTipoTurno As String
        Public DtNuevoTurno As DataTable
    End Class

    <Task()> Public Shared Function ValidarTurno(ByVal data As DatosValidarTurno, ByVal services As ServiceProvider) As Boolean
        Dim BlnTurnoNoValido As Boolean = False
        Dim FilTurnos As New Filter
        FilTurnos.Add("IdTipoTurno", FilterOperator.Equal, data.IDTipoTurno)

        If Length(data.DtNuevoTurno.Rows(0)("IDTurno")) > 0 Then
            FilTurnos.Add("IdTurno", FilterOperator.NotEqual, data.DtNuevoTurno.Rows(0)("IDTurno"))
        End If
        Dim DtTurnos As DataTable = New TipoTurnoLinea().Filter(FilTurnos, "Inicio")
        For Each Dr As DataRow In DtTurnos.Rows
            If Dr("inicio") = data.DtNuevoTurno.Rows(0)("inicio") Then
                BlnTurnoNoValido = True
                Exit For
            Else
                If data.DtNuevoTurno.Rows(0)("inicio") > Dr("inicio") Then
                    If data.DtNuevoTurno.Rows(0)("inicio") < Dr("fin") Then
                        BlnTurnoNoValido = True
                        Exit For
                    End If
                ElseIf data.DtNuevoTurno.Rows(0)("Fin") > Dr("inicio") Then
                    BlnTurnoNoValido = True
                    Exit For
                End If
            End If
        Next
        Return Not BlnTurnoNoValido
    End Function

#End Region

#Region "Eventos RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarTurno)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarDuracion)
    End Sub

    <Task()> Public Shared Sub AsignarTurno(ByVal data As Object, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            If Length(data("IDTurno")) = 0 Then data("IDTurno") = AdminData.GetAutoNumeric
        End If
    End Sub

    <Task()> Public Shared Sub AsignarDuracion(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("Inicio")) > 0 AndAlso Length(data("Fin")) > 0 Then
            Dim Inicio As Date = Format(data("Inicio"), "HH:mm")
            Dim Fin As Date = Format(data("Fin"), "HH:mm")
            Dim Duracion As TimeSpan = Fin.Subtract(Inicio)
            data("Duracion") = xRound(Duracion.Hours + (Duracion.Minutes / 60), 2)
        End If
    End Sub

#End Region

End Class