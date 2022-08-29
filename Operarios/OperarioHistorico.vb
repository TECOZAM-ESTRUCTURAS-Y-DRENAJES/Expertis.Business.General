Public Class OperarioHistorico
    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbOperarioHistorico"

#Region "Eventos RegisterAddNewTasks"

    Protected Overrides Sub RegisterAddnewTasks(ByVal addnewProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterAddnewTasks(addnewProcess)
        addnewProcess.AddTask(Of DataRow)(AddressOf AsignarValoresPredeterminados)
        '    'addnewProcess.AddTask(Of DataRow)(AddressOf AsignarTipoDoc)
    End Sub
    <Task()> Public Shared Sub AsignarValoresPredeterminados(ByVal data As DataRow, ByVal services As ServiceProvider)
        data("IDOperarioHistorico") = AdminData.GetAutoNumeric

    End Sub

    '<Task()> Public Shared Sub AsignarContadorPorDefecto(ByVal data As DataRow, ByVal services As ServiceProvider)
    '    Dim StDatos As New Contador.DatosDefaultCounterValue
    '    StDatos.Row = data
    '    StDatos.EntityName = "OperarioHistorico"
    '    StDatos.FieldName = "IDOperarioHistorico"
    '    ProcessServer.ExecuteTask(Of Contador.DatosDefaultCounterValue)(AddressOf Contador.LoadDefaultCounterValue, StDatos, services)
    'End Sub


#End Region

#Region "Eventos RegisterDeleteTasks"
    Protected Overrides Sub RegisterDeleteTasks(ByVal DeleteProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterDeleteTasks(DeleteProcess)
        '    addnewProcess.AddTask(Of DataRow)(AddressOf AsignarContadorPorDefecto)
        '    'addnewProcess.AddTask(Of DataRow)(AddressOf AsignarTipoDoc)
    End Sub
#End Region

#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarOperario)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarClaveDuplicada)
    End Sub

    <Task()> Public Shared Sub ValidarOperario(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDOperario")) = 0 Then ApplicationService.GenerateError("El Operario es un dato obligatorio.")
    End Sub



    <Task()> Public Shared Sub ValidarClaveDuplicada(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            Dim dt As DataTable = New Oficio().SelOnPrimaryKey(data("IDOperarioHistorico"))
            If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
                ApplicationService.GenerateError("Esta linea ya existe en la base de datos.")
            End If
        End If
    End Sub
#End Region

#Region "Eventos RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
    End Sub
#End Region


    'Public Overloads Overrides Function Update(ByVal dttSource As System.Data.DataTable) As System.Data.DataTable
    '    If Not dttSource Is Nothing AndAlso dttSource.Rows.Count > 0 Then

    '        Me.BeginTx()
    '        For Each dr As DataRow In dttSource.Rows
    '            If Lenght(dr("IDOperario")) = 0 Then ApplicationService.GenerateError("El Operario es un dato obligatorio.")
    '            'If Lenght(dr("IDHora")) = 0 Then ApplicationService.GenerateError("Hora es un dato obligatorio.")

    '            If dr.RowState = DataRowState.Added Then
    '                If Lenght(dr("IDOperariohistorico")) = 0 Then dr("IDOperariohistorico") = AdminData.GetAutoNumeric

    '                Dim ofilter As New Filter
    '                ofilter.Add(New StringFilterItem("IDOperario", dr("IDOperario")))
    '                'ofilter.Add(New StringFilterItem("ObraPredet", dr("ObraPredet")))
    '                'ofilter.Add(New StringFilterItem("ID", dr("IDHora")))
    '                'Dim dtOperarioHistorico As DataTable = Me.Filter(ofilter)
    '                'If Not dtOperarioHistorico Is Nothing AndAlso dtOperarioHistorico.Rows.Count > 0 Then
    '                'ApplicationService.GenerateError("Ya existe una Tasa para la Hora | ", dr("IDH"))
    '                'End If
    '            End If

    '        Next
    '        AdminData.SetData(dttSource)
    '    End If
    '    Return dttSource
    'End Function
    '<Task()> Public Shared Function ObtenerIDUsuario() As Guid
    '    Return AdminData.GetSessionInfo.UserID
    'End Function
    '<Task()> Public Shared Function ObtenerOperarioUsuario() As DataTable
    '    Dim idUsuario As Guid = AdminData.GetSessionInfo.UserID
    '    Return Filter(New GuidFilterItem("IDUsuario", FilterOperator.Equal, idUsuario))
    'End Function
    'Public Function ObtenerIDOperarioUsuario() As String
    '    'Dim idUsuario As Guid = AdminData.GetSessionInfo.UserID
    '    'Dim dtOp As DataTable = Filter(New GuidFilterItem("IDUsuario", FilterOperator.Equal, idUsuario))
    '    Dim dtOp As DataTable
    '    dtOp = ObtenerOperarioUsuario()
    '    If Not dtOp Is Nothing AndAlso dtOp.Rows.Count > 0 Then
    '        Return dtOp.Rows(0)("IDOperario")
    '    End If
    'End Function
    'Public Function DevuelveUsuariosBD(Optional ByVal f As Filter = Nothing) As DataTable
    '    Dim EntID As Guid = AdminData.GetSessionInfo.Enterprise.EnterpriseID
    '    If IsNothing(f) Then f = New Filter
    '    f.Add(New GuidFilterItem("IDEmpresa", FilterOperator.Equal, EntID))
    '    Return AdminData.GetData("xUser", f, "IDUsuario, CUsuario", "CUsuario", , True)
    'End Function
    'Public Function GetBDsSistema() As DataTable
    '    Dim EntID As Guid = AdminData.GetSessionInfo.Enterprise.EnterpriseID
    '    Return AdminData.GetData("xDataBase", New GuidFilterItem("IDEmpresa", FilterOperator.NotEqual, EntID), "IDBaseDatos, BaseDatos", "BaseDatos", , True)
    'End Function
End Class

