Public Class CalendarioOperario
    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbCalendarioOperario"



#Region "CopiarCalendarioOperarios"

    <Serializable()> _
    Public Class DatosCopiarCalenOper
        Public IDOperarioOrigen As String
        Public IDOperarioDesde As String
        Public IDOperarioHasta As String
        Public IDOficio As String
        Public IDCategoria As String
        Public IDEmpresa As String
        Public IDSeccion As String
        Public Año As String

        Public Sub New()
        End Sub

        Public Sub New(ByVal IDOperarioOrigen As String, ByVal Año As String, Optional ByVal IDOperarioDesde As String = "", _
                       Optional ByVal IDOperarioHasta As String = "", Optional ByVal IDOficio As String = "", _
                       Optional ByVal IDCategoria As String = "", Optional ByVal IDEmpresa As String = "", _
                       Optional ByVal IDSeccion As String = "")

            Me.IDOperarioOrigen = IDOperarioOrigen
            Me.Año = Año
            Me.IDOperarioDesde = IDOperarioDesde
            Me.IDOperarioHasta = IDOperarioHasta
            Me.IDOficio = IDOficio
            Me.IDCategoria = IDCategoria
            Me.IDEmpresa = IDEmpresa
            Me.IDSeccion = IDSeccion
        End Sub
    End Class

    <Task()> Public Shared Sub CopiarCalendarioOperarios(ByVal data As DatosCopiarCalenOper, ByVal services As ServiceProvider)
        Dim FechaDesde As New Date(data.Año, 1, 1)
        Dim FechaHasta As New Date(data.Año, 12, 31)

        Dim f As New Filter
        f.Add(New StringFilterItem("IDOperario", data.IDOperarioOrigen))
        f.Add(New BetweenFilterItem("Fecha", FechaDesde, FechaHasta, FilterType.DateTime))
        Dim dtCalendario As DataTable = New CalendarioOperario().Filter(f)
        If Not dtCalendario Is Nothing AndAlso dtCalendario.Rows.Count > 0 Then
            f.Clear()
            f.Add("IDOperario", FilterOperator.NotEqual, data.IDOperarioOrigen)
            If Length(data.IDOperarioDesde) > 0 Then
                f.Add("IDOperario", FilterOperator.GreaterThanOrEqual, data.IDOperarioDesde)
            End If
            If Length(data.IDOperarioHasta) > 0 Then
                f.Add("IDOperario", FilterOperator.LessThanOrEqual, data.IDOperarioHasta)
            End If
            If Length(data.IDOficio) > 0 Then
                f.Add("IDOficio", FilterOperator.Equal, data.IDOficio)
            End If
            If Length(data.IDCategoria) > 0 Then
                f.Add("IDCategoria", FilterOperator.Equal, data.IDCategoria)
            End If
            If Length(data.IDEmpresa) > 0 Then
                f.Add("IDEmpresa", FilterOperator.Equal, data.IDEmpresa)
            End If
            If Length(data.IDSeccion) > 0 Then
                f.Add("IDSeccion", FilterOperator.Equal, data.IDSeccion)
            End If
            Dim dtOperarios As DataTable = New Operario().Filter(f, , "IDOperario")
            If dtOperarios.Rows.Count > 0 Then
                For Each drOperario As DataRow In dtOperarios.Select
                    Dim sql As String = "DELETE FROM tbCalendarioOperario"
                    sql &= " WHERE IDOperario = '" & drOperario("IDOperario") & "' AND YEAR(Fecha) = " & data.Año
                    AdminData.Execute(sql)

                    sql = "INSERT INTO tbCalendarioOperario (IDOperario, Fecha, TipoDia, IDTipoTurno)"
                    sql &= " SELECT '" & drOperario("IDOperario") & "' AS IDOperario, Fecha, TipoDia, IDTipoTurno"
                    sql &= " FROM tbCalendarioOperario"
                    sql &= " WHERE (IDOperario = '" & data.IDOperarioOrigen & "' AND YEAR(Fecha) = " & data.Año & ")"
                    AdminData.Execute(sql)
                Next
            End If
        Else
            ApplicationService.GenerateError("El Operario seleccionado no tiene un Calendario configurado.")
        End If
    End Sub

#End Region

End Class