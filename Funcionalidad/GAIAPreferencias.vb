Public Class GAIAPreferencias

#Region "Constructor"

    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbGaiaPreferencias"

#End Region

#Region "Tareas RegisterAddNewTasks"

    Protected Overrides Sub RegisterAddnewTasks(ByVal addnewProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterAddnewTasks(addnewProcess)
        addnewProcess.AddTask(Of DataRow)(AddressOf AsignarValoresPredeterminados)
    End Sub

    <Task()> Public Shared Sub AsignarValoresPredeterminados(ByVal data As DataRow, ByVal services As ServiceProvider)
        data("DiasReenvioCorreo") = 5
        data("UseSSLCorreo") = 0
        data("TipoReenvioAviso") = 0
        data("TipoEnvioCorreo") = enumGAIATipoEnvioCorreo.LinkDescarga
    End Sub

#End Region

#Region "Tareas RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf ValidarClave)
    End Sub

    <Task()> Public Shared Sub ValidarClave(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added Then
            If Length(data("ID")) = 0 Then data("ID") = AdminData.GetAutoNumeric
        End If
    End Sub

#End Region

End Class