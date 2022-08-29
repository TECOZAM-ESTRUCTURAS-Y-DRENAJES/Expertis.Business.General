Public Class HistoricoMoneda
    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbHistoricoMoneda"

#Region " RegisterDeleteTasks "

    Protected Overrides Sub RegisterDeleteTasks(ByVal deleteProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterDeleteTasks(deleteProcess)
        deleteProcess.AddTask(Of DataRow)(AddressOf ActualizarMonedaDelete)
    End Sub
    <Task()> Public Shared Sub ActualizarMonedaDelete(ByVal data As DataRow, ByVal services As ServiceProvider)
        Dim m As New Moneda
        Dim drMoneda As DataRow = m.GetItemRow(data("IDMoneda"))
        If drMoneda("FechaCambio") = data("Fecha") Then
            Dim f As New Filter
            f.Add(New StringFilterItem("IDMoneda", data("IDMoneda")))
            f.Add(New DateFilterItem("Fecha", FilterOperator.LessThan, data("Fecha")))
            Dim dtMoneda As DataTable = New HistoricoMoneda().Filter(f, "Fecha desc")
            If Not dtMoneda Is Nothing AndAlso dtMoneda.Rows.Count > 0 Then
                drMoneda("CambioA") = dtMoneda.Rows(0)("CambioA")
                drMoneda("CambioB") = dtMoneda.Rows(0)("CambioB")
                drMoneda("FechaCambio") = dtMoneda.Rows(0)("Fecha")
                BusinessHelper.UpdateTable(drMoneda.Table)
            End If
        End If
    End Sub

#End Region

#Region " RegisterUpdateTasks "

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf ActualizarMoneda)
        updateProcess.AddTask(Of DataRow)(AddressOf ActualizarMonedaAB)
    End Sub

    <Task()> Public Shared Sub ActualizarMoneda(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Or data.RowState = DataRowState.Modified Then
            Dim f As New Filter
            f.Add(New StringFilterItem("IDMoneda", data("IDMoneda")))
            f.Add(New DateFilterItem("FechaCambio", FilterOperator.LessThanOrEqual, data("Fecha")))
            Dim dtMoneda As DataTable = New Moneda().Filter(f)
            If Not dtMoneda Is Nothing AndAlso dtMoneda.Rows.Count > 0 Then
                dtMoneda.Rows(0)("CambioA") = data("CambioA")
                dtMoneda.Rows(0)("CambioB") = data("CambioB")
                dtMoneda.Rows(0)("FechaCambio") = data("Fecha")
                BusinessHelper.UpdateTable(dtMoneda)
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ActualizarMonedaAB(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Or data.RowState = DataRowState.Modified Then
            Dim p As Parametro.MonedasInternas = New Parametro().MonedasInternasPredeterminadas
            If data("IDMoneda") = p.strMonedaA OrElse data("IDMoneda") = p.strMonedaB Then
                Dim IDMoneda As String = p.strMonedaA
                If data("IDMoneda") = p.strMonedaA Then IDMoneda = p.strMonedaB

                Dim hm As New HistoricoMoneda
                Dim f As New Filter
                f.Add("IDMoneda", FilterOperator.Equal, IDMoneda)
                f.Add("Fecha", FilterOperator.Equal, data("Fecha"))
                Dim dtMoneda As DataTable = hm.Filter(f)
                If Not dtMoneda Is Nothing AndAlso dtMoneda.Rows.Count = 0 Then
                    Dim drMoneda As DataRow = dtMoneda.NewRow
                    drMoneda("IDMoneda") = IDMoneda
                    drMoneda("Fecha") = data("Fecha")
                    If data("IDMoneda") = p.strMonedaA Then
                        drMoneda("CambioA") = 1 / data("CambioB")
                        drMoneda("CambioB") = 1
                    Else
                        drMoneda("CambioA") = 1
                        drMoneda("CambioB") = 1 / data("CambioA")
                    End If
                    dtMoneda.Rows.Add(drMoneda)
                ElseIf data("IDMoneda") = p.strMonedaA Then
                    dtMoneda.Rows(0)("CambioA") = 1 / data("CambioB")
                Else
                    dtMoneda.Rows(0)("CambioB") = 1 / data("CambioA")
                End If

                Dim dt As DataTable = dtMoneda.Copy
                BusinessHelper.UpdateTable(dtMoneda)

                ProcessServer.ExecuteTask(Of DataRow)(AddressOf ActualizarMoneda, dt.Rows(0), services)
            End If
        End If
    End Sub

#End Region

End Class