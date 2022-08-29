Public Class MonedaCache
    Private mMonedaA As MonedaInfo
    Private mMonedaB As MonedaInfo

    Public Sub New()
        Dim Services As New ServiceProvider
        mMonedaA = ProcessServer.ExecuteTask(Of Date, MonedaInfo)(AddressOf Moneda.MonedaA, cnMinDate, Services)
        mMonedaHT(mMonedaA.ID) = mMonedaA
        mMonedaB = ProcessServer.ExecuteTask(Of Date, MonedaInfo)(AddressOf Moneda.MonedaB, cnMinDate, Services)
        mMonedaHT(mMonedaB.ID) = mMonedaB
    End Sub

    Public ReadOnly Property MonedaA() As MonedaInfo
        Get
            'If mMonedaA Is Nothing Then
            '    mMonedaA = New Moneda().MonedaA
            'End If
            Return mMonedaA
        End Get
    End Property


    Public ReadOnly Property MonedaB() As MonedaInfo
        Get
            'If mMonedaB Is Nothing Then
            '    mMonedaB = New Moneda().MonedaB
            'End If
            Return mMonedaB
        End Get
    End Property

    Private mMonedaHT As New Hashtable
    Public Function GetMoneda(ByVal IDMoneda As String, ByVal Fecha As Date) As MonedaInfo
        Dim MonInfo As MonedaInfo
        Dim Key As String = IDMoneda & "/" & Fecha.ToString

        If IsNothing(mMonedaHT) Then mMonedaHT = New Hashtable

        If mMonedaHT.ContainsKey(Key) Then
            MonInfo = mMonedaHT(Key)
        Else
            Dim StDatos As New Moneda.DatosObtenerMoneda
            StDatos.IDMoneda = IDMoneda
            StDatos.Fecha = Fecha
            MonInfo = ProcessServer.ExecuteTask(Of Moneda.DatosObtenerMoneda, MonedaInfo)(AddressOf Moneda.ObtenerMoneda, StDatos, New ServiceProvider)
            mMonedaHT(Key) = MonInfo
        End If

        Return MonInfo
    End Function
    Public Function GetMoneda(ByVal IDMoneda As String) As MonedaInfo
        Return GetMoneda(IDMoneda, Date.Today)
    End Function

    Public Sub Clear()
        mMonedaHT.Clear()
    End Sub
End Class
