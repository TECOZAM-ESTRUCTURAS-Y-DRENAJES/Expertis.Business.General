Public Class CalendarioCentro
    
#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbCalendarioCentro"

#End Region

#Region "Funciones Públicas"

    <Serializable()> _
    Public Class DatosCopiarCalenCentro
        Public IDCentroOrigen As String
        Public IDCentroDesde As String
        Public IDCentroHasta As String
        Public IDSeccion As String
        Public Año As String
    End Class

    <Task()> Public Shared Sub CopiarCalendariosCentros(ByVal data As DatosCopiarCalenCentro, ByVal services As ServiceProvider)
        Dim fd As New Date(data.Año, 1, 1)
        Dim fh As New Date(data.Año, 12, 31)
        Dim f As New Filter
        f.Add(New StringFilterItem("IDCentro", data.IDCentroOrigen))
        f.Add(New BetweenFilterItem("Fecha", fd, fh, FilterType.DateTime))

        Dim DtCentroOrigen As DataTable = New CalendarioCentro().Filter(f)
        If Not DtCentroOrigen Is Nothing AndAlso DtCentroOrigen.Rows.Count > 0 Then
            ProcessServer.ExecuteTask(Of DatosCopiarCalenCentro)(AddressOf CopiarCentros, data, services)
        Else : ApplicationService.GenerateError("El Centro seleccionado no tiene un Calendario configurado.")
        End If
    End Sub

    <Serializable()> _
    Public Class DatosCopiarCentro
        Public IDCentroOrigen As String
        Public IDCentroDestino As String
        Public Año As String
    End Class

    <Task()> Public Shared Sub CopiarCentro(ByVal data As DatosCopiarCentro, ByVal services As ServiceProvider)
        Dim StrWhere, StrSql As String
        StrWhere = "IDCentro = '" & data.IDCentroDestino & "' And Year(Fecha) = " & data.Año
        AdminData.Execute("delete from tbCalendarioCentro where " & StrWhere, False)
        StrSql = "INSERT INTO tbCalendarioCentro(IDCentro, Fecha, TipoDia, IDTipoTurno) SELECT  '" & data.IDCentroDestino & "' as IDCentro,  Fecha, TipoDia, IDTipoTurno From tbCalendarioCentro where Year(Fecha) = " & data.Año & " AND IDCentro = '" & data.IDCentroOrigen & "'"
        AdminData.Execute(StrSql)
    End Sub

    <Task()> Public Shared Sub CopiarCentros(ByVal Data As DatosCopiarCalenCentro, ByVal services As ServiceProvider)
        Dim FilOrigenCab As New Filter
        FilOrigenCab.Add("IDCentro", FilterOperator.Equal, Data.IDCentroOrigen)
        FilOrigenCab.Add("YEAR(FECHA)", FilterOperator.Equal, Data.Año)
        Dim DtCalenCentCabOrigen As DataTable = New CalendarioCentro().Filter(FilOrigenCab)

        Dim FilWhere As New Filter
        FilWhere.Add("IDCentro", FilterOperator.NotEqual, Data.IDCentroOrigen)
        If Length(Data.IDCentroDesde) > 0 Then
            FilWhere.Add("IDCentro", FilterOperator.GreaterThanOrEqual, Data.IDCentroDesde)
        End If
        If Length(Data.IDCentroHasta) > 0 Then
            FilWhere.Add("IDCentro", FilterOperator.LessThanOrEqual, Data.IDCentroHasta)
        End If
        If Length(Data.IDSeccion) > 0 Then
            FilWhere.Add("IDSeccion", FilterOperator.Equal, Data.IDSeccion)
        End If
        Dim DtCents As DataTable = New BE.DataEngine().Filter("tbMaestroCentro", FilWhere)

        Dim StrDelete As String = String.Empty
        Dim StrSql As String = String.Empty
        For Each Dr As DataRow In DtCents.Select
            StrDelete = "DELETE FROM tbCalendarioCentro"
            StrDelete &= " WHERE IDCentro = '" & Dr("IDCentro") & "' AND YEAR(Fecha) = " & Data.Año
            AdminData.Execute(StrDelete)

            StrSql = "INSERT INTO tbCalendarioCentro (IDCentro, Fecha, TipoDia, IDTipoTurno)"
            StrSql &= " SELECT '" & Dr("IDCentro") & "' AS IDCentro, Fecha, TipoDia, IDTipoTurno"
            StrSql &= " FROM tbCalendarioCentro"
            StrSql &= " WHERE (IDCentro = '" & Data.IDCentroOrigen & "' AND YEAR(Fecha) = " & Data.Año & ")"
            AdminData.Execute(StrSql)
        Next
    End Sub

#End Region

End Class