Public Class CalendarioEmpresa

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbCalendarioEmpresa"

#End Region

#Region "Funciones Públicas"

    <Serializable()> _
    Public Class DatosOperFechaCalen
        Public Fecha As Date
        Public Plazo As Integer
    End Class

    <Task()> Public Shared Function RestarFechaCalendario(ByVal data As DatosOperFechaCalen, ByVal services As ServiceProvider) As Date
        Dim calendario As DataTable
        Dim resultado As Date = data.Fecha
        If data.Plazo <> 0 Then
            Dim f As New Filter
            f.Add(New DateFilterItem("Fecha", FilterOperator.LessThan, data.Fecha))
            f.Add(New NumberFilterItem("TipoDia", 0))
            calendario = New CalendarioEmpresa().Filter(f, "Fecha Desc")
            If calendario.Rows.Count > 0 Then
                For Each dr As DataRow In calendario.Rows
                    If data.Plazo = 0 Then
                        Exit For
                    Else : data.Plazo -= 1
                    End If
                    resultado = dr("Fecha")
                Next
            End If
        End If
        Return resultado
    End Function

    <Task()> Public Shared Function SumarFechaCalendario(ByVal data As DatosOperFechaCalen, ByVal services As ServiceProvider) As Date
        Dim calendario As DataTable
        Dim resultado As Date = data.Fecha
        If data.Plazo <> 0 Then
            Dim f As New Filter
            f.Add(New DateFilterItem("Fecha", FilterOperator.GreaterThan, data.Fecha))
            f.Add(New NumberFilterItem("TipoDia", 0))
            calendario = New CalendarioEmpresa().Filter(f, "Fecha asc")
            If calendario.Rows.Count > 0 Then
                For Each dr As DataRow In calendario.Rows
                    If data.Plazo = 0 Then
                        Exit For
                    Else : data.Plazo -= 1
                    End If
                    resultado = dr("Fecha")
                Next
            End If
        End If
        Return resultado
    End Function

    <Serializable()> _
    Public Class DatosExportCalen
        Public Año As String
        Public DtDatos As DataTable
        Public IDClave As String
    End Class

    <Task()> Public Shared Sub ExportarCalendarioACentro(ByVal data As DatosExportCalen, ByVal services As ServiceProvider)
        Dim clsCalendarioCentro As New CalendarioCentro
        If Not data.DtDatos Is Nothing AndAlso data.DtDatos.Rows.Count > 0 Then
            For Each Dr As DataRow In data.DtDatos.Select
                Dim StDatos As New DatosExport
                StDatos.Año = data.Año : StDatos.IDCentro = Dr("IDCentro")
                ProcessServer.ExecuteTask(Of DatosExport)(AddressOf Exportar, StDatos, services)
            Next
        ElseIf Length(data.IDClave) > 0 Then
            Dim StDatos As New DatosExport
            StDatos.Año = data.Año : StDatos.IDCentro = data.IDClave
            ProcessServer.ExecuteTask(Of DatosExport)(AddressOf Exportar, StDatos, services)
        End If
    End Sub

    <Task()> Public Shared Sub ExportarCalendarioAOperario(ByVal data As DatosExportCalen, ByVal services As ServiceProvider)
        Dim ClsCalenOperario As New CalendarioOperario
        Dim fd As New Date(data.Año, 1, 1)
        Dim fh As New Date(data.Año, 12, 31)
        Dim f As New Filter
        f.Add(New BetweenFilterItem("Fecha", fd, fh, FilterType.DateTime))


        Dim DtCalenEmpresa As DataTable = New CalendarioEmpresa().Filter(f)
        If Not data.DtDatos Is Nothing AndAlso data.DtDatos.Rows.Count > 0 Then
            For Each Dr As DataRow In data.DtDatos.Select
                Dim StDatos As New DatosExportOper
                StDatos.Año = data.Año : StDatos.IDOperario = Dr("IDOperario")
                ProcessServer.ExecuteTask(Of DatosExportOper)(AddressOf ExportarOperario, StDatos, services)
            Next
        ElseIf Length(data.IDClave) > 0 Then
            Dim StDatos As New DatosExportOper
            StDatos.Año = data.Año : StDatos.IDOperario = data.IDClave
            ProcessServer.ExecuteTask(Of DatosExportOper)(AddressOf ExportarOperario, StDatos, services)
        End If
    End Sub

    <Serializable()> _
    Public Class DatosExport
        Public Año As String
        Public IDCentro As String
    End Class

    <Task()> Public Shared Sub Exportar(ByVal data As DatosExport, ByVal services As ServiceProvider)
        Dim StrWhere, StrSql As String

        StrWhere = "IDCentro = '" & data.IDCentro & "' And Year(Fecha) = " & data.Año
        AdminData.Execute("delete from tbCalendarioCentro where " & StrWhere, ExecuteCommand.ExecuteNonQuery)

        StrSql = "INSERT INTO tbCalendarioCentro(IDCentro, Fecha, TipoDia, IDTipoTurno) SELECT  '" & data.IDCentro & "' as IDCentro,  Fecha, TipoDia, IDTipoTurno From tbCalendarioEmpresa where Year(Fecha) = " & data.Año
        AdminData.Execute(StrSql)

    End Sub

    <Serializable()> _
    Public Class DatosExportOper
        Public Año As String
        Public IDOperario As String
    End Class

    <Task()> Public Shared Sub ExportarOperario(ByVal data As DatosExportOper, ByVal services As ServiceProvider)
        Dim StrDelete As String = String.Empty
        StrDelete = "DELETE FROM tbCalendarioOperario"
        StrDelete &= " WHERE IDOperario = '" & data.IDOperario & "' AND YEAR(Fecha) = " & data.Año
        AdminData.Execute(StrDelete)

        Dim StrSql As String = String.Empty
        StrSql = "INSERT INTO tbCalendarioOperario (IDOperario, Fecha, TipoDia, IDTipoTurno)"
        StrSql &= " SELECT '" & data.IDOperario & "' AS IDOperario, Fecha, TipoDia, IDTipoTurno"
        StrSql &= " FROM tbCalendarioEmpresa"
        StrSql &= " WHERE YEAR(Fecha) = " & data.Año
        AdminData.Execute(StrSql)
    End Sub

#End Region

End Class