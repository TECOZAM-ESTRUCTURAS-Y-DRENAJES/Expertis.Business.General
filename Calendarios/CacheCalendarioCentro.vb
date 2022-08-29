Public Class CacheCalendarioCentro
    Private mFechaInicial As Date = Date.MaxValue
    Private mFechaFinal As Date = Date.MinValue
    Private mPrimerPeriodo As Periodo
    Private mUltimoPeriodo As Periodo
    Public IDCentro As String

    Public FactorAfectaPreparacion As Boolean

    Private Const Intervalo As Integer = 100

    Public Sub New(ByVal IDCentro As String)
        Me.IDCentro = IDCentro
        Dim ClsCentro As BE.BusinessHelper = BusinessHelper.CreateBusinessObject("Centro")
        Dim dt As DataTable = ClsCentro.SelOnPrimaryKey(IDCentro)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Me.FactorAfectaPreparacion = dt.Rows(0)("FactorAfectaTP")
        Else
            Me.FactorAfectaPreparacion = False
        End If
    End Sub

    Public Function SiguientePeriodo(ByVal DtmFecha As Date) As Periodo

        If DtmFecha > mFechaFinal OrElse _
            DtmFecha < mFechaInicial Then
            Dim FecI As New Date(DtmFecha.Year, DtmFecha.Month, DtmFecha.Day)
            Rellenar(FecI, FecI.AddDays(Intervalo))
        End If

        Dim oPrd As Periodo = mPrimerPeriodo
        Do Until oPrd Is Nothing OrElse oPrd.FechaFin > DtmFecha
            oPrd = oPrd.Sgte
        Loop
        Return oPrd

    End Function

    Private Sub Rellenar(ByVal FechaDesde As Date, ByVal FechaHasta As Date)

        'TODO convertir esto en un procedimiento almacenado
        Dim sql As String = "select c.Fecha, t.Inicio, t.Fin, t.Factor" _
            & vbCrLf & "from tbTipoTurnoLinea t inner join tbCalendarioCentro c on t.IDTipoTurno = c.IDTipoTurno"

        Const OrderBy As String = " order by Fecha, Inicio"

        Dim fd As Date
        Dim fh As Date
        If mFechaInicial = Date.MaxValue Then
            fd = FechaDesde
            fh = FechaHasta
        ElseIf FechaDesde < mFechaInicial Then
            If mFechaInicial.Subtract(FechaDesde).TotalDays < Intervalo Then
                fd = mFechaInicial.AddDays(-Intervalo)
            Else
                fd = FechaDesde
            End If
            fh = mFechaInicial.AddDays(-1)
        ElseIf FechaHasta > mFechaFinal Then
            If FechaHasta.Subtract(mFechaFinal).TotalDays < Intervalo Then
                fh = mFechaFinal.AddDays(Intervalo)
            Else
                fh = FechaHasta
            End If
            fd = mFechaFinal.AddDays(1)
        End If

        Dim oFltr As New Filter
        oFltr.Add(New StringFilterItem("IDCentro", IDCentro))
        '//fd.AddDays(-1) se debe a que se quieren tener en cuenta los periodos de actividad que comienzan
        '//antes de fd y se extienden hasta fd
        oFltr.Add(New BetweenFilterItem("Fecha", fd.AddDays(-1), fh, FilterType.DateTime))

        Dim Where As String = AdminData.ComposeFilter(oFltr)
        Dim oCmd As Common.DbCommand = AdminData.GetCommand
        oCmd.CommandText = sql & " WHERE " & Where & OrderBy
        'oCmd.Connection = New SqlClient.SqlConnection(AdminData.GetSessionConnection.Connection.ConnectionString)
        oCmd.Connection = AdminData.GetSessionConnection.Connection
        oCmd.Transaction = AdminData.GetSessionConnection.Transaction
        'oCmd.Connection.Open()
        Dim oRdr As Common.DbDataReader = oCmd.ExecuteReader

        If oRdr.HasRows Then
        Else
            oRdr.Close()
            oFltr.RemoveAt(1)
            If FechaDesde < mFechaInicial Then
                fd = Date.MinValue
                oFltr.Add(New DateFilterItem("Fecha", FilterOperator.LessThan, ValidSqlDate(mFechaInicial)))
            ElseIf FechaHasta > mFechaFinal Then
                fh = Date.MaxValue
                oFltr.Add(New DateFilterItem("Fecha", FilterOperator.GreaterThan, ValidSqlDate(mFechaFinal)))
            End If
            Where = AdminData.ComposeFilter(oFltr)
            oCmd.CommandText = sql & " where " & Where & OrderBy
            oRdr = oCmd.ExecuteReader()
        End If

        Dim oPrimerPrd As Periodo
        Dim oUltimoPrd As Periodo
        If oRdr.HasRows Then
            Dim oPrdAnt As Periodo
            Dim FechaIdx As Integer = oRdr.GetOrdinal("Fecha")
            Dim InicioIdx As Integer = oRdr.GetOrdinal("Inicio")
            Dim FinIdx As Integer = oRdr.GetOrdinal("Fin")
            Dim FactorIdx As Integer = oRdr.GetOrdinal("Factor")

            Do While oRdr.Read
                Dim Fecha As Date = oRdr(FechaIdx)
                Dim Inicio As Date = oRdr(InicioIdx)
                Inicio = New Date(Fecha.Year, Fecha.Month, Fecha.Day, Inicio.Hour, Inicio.Minute, Inicio.Second)
                Dim Fin As Date = oRdr(FinIdx)
                Fin = New Date(Fecha.Year, Fecha.Month, Fecha.Day, Fin.Hour, Fin.Minute, Fin.Second)
                If Fin < Inicio Then Fin = Fin.AddDays(1)
                If Fin > fd Then
                    If Inicio < fd Then Inicio = fd
                    If Fin > fh.AddDays(1) Then Fin = fh.AddDays(1)

                    oUltimoPrd = New Periodo(Inicio, Fin, oRdr(FactorIdx))

                    If oPrdAnt Is Nothing Then
                        oPrimerPrd = oUltimoPrd
                    Else
                        oPrdAnt.Sgte = oUltimoPrd
                    End If
                    oPrdAnt = oUltimoPrd
                End If
            Loop
            oRdr.Close()
            '//ajuste fino. Si un periodo empieza un día y termina en otro, se separa en dos periodos
            Dim oPrd As Periodo = oPrimerPrd
            Do Until oPrd Is Nothing
                Dim ff As Date = New Date(oPrd.FechaInicio.Year, oPrd.FechaInicio.Month, oPrd.FechaInicio.Day).AddDays(1)
                If oPrd.FechaFin > ff Then
                    Dim nwPrd As New Periodo(ff, oPrd.FechaFin, oPrd.Factor)
                    nwPrd.Sgte = oPrd.Sgte

                    oPrd.FechaFin = ff
                    oPrd.Sgte = nwPrd
                End If
                oPrd = oPrd.Sgte
            Loop
        Else
            oRdr.Close()
        End If
        If FechaDesde < mFechaInicial Then
            mFechaInicial = fd
            If Me.mFechaFinal = Date.MinValue Then mFechaFinal = fh
            If Not oUltimoPrd Is Nothing Then oUltimoPrd.Sgte = Me.mPrimerPeriodo
            If mUltimoPeriodo Is Nothing Then mUltimoPeriodo = oUltimoPrd
            If Not oPrimerPrd Is Nothing Then mPrimerPeriodo = oPrimerPrd
        ElseIf FechaHasta > mFechaFinal Then
            mFechaFinal = fh
            If Me.mFechaInicial = Date.MaxValue Then mFechaInicial = fd
            If Not mUltimoPeriodo Is Nothing Then mUltimoPeriodo.Sgte = oPrimerPrd
            If Not oUltimoPrd Is Nothing Then mUltimoPeriodo = oUltimoPrd
            If mPrimerPeriodo Is Nothing Then mPrimerPeriodo = oPrimerPrd
        End If
    End Sub

    Private Function ValidSqlDate(ByVal fecha As Date) As Date
        If fecha = Date.MaxValue Then
            Return System.Data.SqlTypes.SqlDateTime.MaxValue.Value
        ElseIf fecha = Date.MinValue Then
            Return System.Data.SqlTypes.SqlDateTime.MinValue.Value
        Else
            Return fecha
        End If
    End Function

    Public Function Seleccionar(ByVal FechaDesde As Date, ByVal FechaHasta As Date) As Periodo()
        Dim oPrdI As Periodo = SiguientePeriodo(FechaDesde)
        Dim rslt(-1) As Periodo
        If oPrdI Is Nothing Then
        Else
            Dim oPrd As Periodo = oPrdI
            Dim cont As Integer = 0
            Do Until oPrd Is Nothing OrElse oPrd.FechaInicio > FechaHasta
                cont += 1
                oPrd = oPrd.Sgte
            Loop

            ReDim rslt(cont - 1)

            oPrd = oPrdI
            For i As Integer = 0 To cont - 1
                rslt(i) = oPrd
                oPrd = oPrd.Sgte
            Next
        End If
        Return rslt
    End Function

    Public Sub RellenarIntervalo(ByVal FechaDesde As Date, ByVal FechaHasta As Date)
        If mFechaFinal = Date.MinValue Then
            Rellenar(FechaDesde, FechaHasta)
        Else
            If FechaDesde < Me.mFechaInicial Then
                Rellenar(FechaDesde, mFechaInicial)
            End If
            If FechaHasta > Me.mFechaFinal Then
                Rellenar(mFechaFinal, FechaHasta)
            End If
        End If
    End Sub
End Class

Public Class Periodo
    Public FechaInicio As Date
    Public FechaFin As Date
    Public Factor As Double
    Public Sgte As Periodo
    Public Sub New()
    End Sub
    Public Sub New(ByVal FechaInicio As Date, ByVal FechaFin As Date, ByVal Factor As Double)
        Me.FechaInicio = FechaInicio
        Me.FechaFin = FechaFin
        Me.Factor = Factor
    End Sub
End Class