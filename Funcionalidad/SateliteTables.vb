Imports System.Collections.Generic

Public Class SateliteTables

    <Serializable()> _
    Public Class DataSatTable
        Public Entity As String
        Public DtData As DataTable

        Public Sub New()
        End Sub
        Public Sub New(ByVal Entity As String, ByVal DtData As DataTable)
            Me.Entity = Entity
            Me.DtData = DtData
        End Sub
    End Class

    <Serializable()> _
    Public Class DataReturnSatTable
        Public DtDataSat As New DataTable
        Public LstRelations As New List(Of DataRelSat)

        Public Sub New()
        End Sub
        Public Sub New(ByVal DtDataSat As DataTable, ByVal LstRelations As List(Of DataRelSat))
            Me.DtDataSat = DtDataSat
            Me.LstRelations = LstRelations
        End Sub
    End Class

    <Serializable()> _
    Public Class DataRelSat
        Public EntityRel As String
        Public ColPrim As String
        Public ColRel As String

        Public Sub New()
        End Sub
        Public Sub New(ByVal EntityRel As String, ByVal ColPrim As String, ByVal ColRel As String)
            Me.EntityRel = EntityRel
            Me.ColPrim = ColPrim
            Me.ColRel = ColRel
        End Sub
    End Class

    <Task()> Public Shared Function GetSateliteTable(ByVal Data As DataSatTable, ByVal services As ServiceProvider) As DataReturnSatTable
        Dim StDataReturn As New DataReturnSatTable
        Dim ClsBE As New BE.DataEngine
        Dim DtEntity As DataTable = ClsBE.Filter("xEntity", New FilterItem("Entidad", Data.Entity), "Tabla", , , True)
        Dim DtTable As DataTable = ClsBE.Filter("xTable", New FilterItem("Tabla", DtEntity.Rows(0)("Tabla")), "IDTabla", , , True)
        Dim DtSat As DataTable = ClsBE.Filter("xSatelite", New GuidFilterItem("IDTabla", DtTable.Rows(0)("IDTabla")), "IDTablaSat", , , True)
        If Not DtSat Is Nothing AndAlso DtSat.Rows.Count > 0 Then
            Dim DtTableSat As DataTable = ClsBE.Filter("xTable", New GuidFilterItem("IDTabla", DtSat.Rows(0)("IDTablaSat")), "Tabla", , , True)

            Dim FilFinal As New Filter
            Dim ClsEnt As BusinessHelper = BusinessHelper.CreateBusinessObject(Data.Entity)
            For Each Dc As DataColumn In ClsEnt.PrimaryKeyTable.Columns
                FilFinal.Add(Dc.ColumnName, Data.DtData.Rows(0)(Dc.ColumnName))
            Next

            'Dim DtRel As DataTable = ClsBE.Filter("xViewRelaciones", New FilterItem("TablaRel", FilterOperator.Equal, CStr(DtTableSat.Rows(0)("Tabla"))), "TablaPrim,ColPrim,ColRel", , , True)
            'For Each DrRel As DataRow In DtRel.Select
            '    Dim StData As New DataRelSat
            '    StData.ColRel = DrRel("ColRel")
            '    StData.ColPrim = DrRel("ColPrim")
            '    DtEntity = ClsBE.Filter("xEntity", New FilterItem("Tabla", DrRel("TablaPrim")), "Entidad", , , True)
            '    If Not DtEntity Is Nothing AndAlso DtEntity.Rows.Count > 0 Then
            '        StData.EntityRel = DtEntity.Rows(0)("Entidad")
            '    End If
            '    StDataReturn.LstRelations.Add(StData)
            'Next

            Dim BDOrigen As DatabaseInfo = AdminData.GetSessionInfo().DataBase

            Dim DtOrigen As DataTable = ClsBE.Filter(CStr(DtTableSat.Rows(0)("Tabla")), FilFinal, , , True)
            Dim DcOrigen As New DataColumn
            DcOrigen.AllowDBNull = False
            DcOrigen.ColumnName = "IDBaseDatos"
            DcOrigen.Unique = True
            DtOrigen.Columns.Add(DcOrigen)
            DtOrigen.Columns.Add("BaseDatos", GetType(String))
            DtOrigen.Columns.Add("DescBaseDatos", GetType(String))
            If Not DtOrigen Is Nothing AndAlso DtOrigen.Rows.Count > 0 Then
                DtOrigen.Rows(0)("IDBaseDatos") = BDOrigen.DataBaseID
                DtOrigen.Rows(0)("BaseDatos") = BDOrigen.DataBaseName
                DtOrigen.Rows(0)("DescBaseDatos") = BDOrigen.DataBaseDescription
            Else
                Dim DrNew As DataRow = DtOrigen.NewRow
                For Each Dc As DataColumn In DtOrigen.Columns
                    If Data.DtData.Columns.Contains(Dc.ColumnName) Then
                        If Length(Data.DtData.Rows(0)(Dc.ColumnName)) > 0 Then
                            DrNew(Dc.ColumnName) = Data.DtData.Rows(0)(Dc.ColumnName)
                        Else : DrNew(Dc.ColumnName) = 0
                        End If
                    End If
                Next
                DrNew("IDBaseDatos") = BDOrigen.DataBaseID
                DrNew("BaseDatos") = BDOrigen.DataBaseName
                DrNew("DescBaseDatos") = BDOrigen.DataBaseDescription
                DtOrigen.Rows.Add(DrNew)
            End If
            StDataReturn.DtDataSat = DtOrigen
            Dim PKS() As DataColumn = StDataReturn.DtDataSat.PrimaryKey
            ReDim Preserve PKS(PKS.Length)
            PKS(PKS.Length - 1) = DcOrigen
            StDataReturn.DtDataSat.PrimaryKey = PKS
            For Each DrBD As DataRow In AdminData.GetUserDataBases.Select("IDBaseDatos <> '" & BDOrigen.DataBaseID.ToString & "'")
                AdminData.SetCurrentConnection(DrBD("IDBaseDatos"))
                Try
                    Dim DtSatBD As DataTable = ClsBE.Filter(CStr(DtTableSat.Rows(0)("Tabla")), FilFinal, , , True)
                    If Not DtSatBD Is Nothing AndAlso DtSatBD.Rows.Count > 0 Then
                        Dim DrNew As DataRow = StDataReturn.DtDataSat.NewRow
                        For Each Dc As DataColumn In DtSatBD.Columns
                            DrNew(Dc.ColumnName) = DtSatBD.Rows(0)(Dc.ColumnName)
                        Next
                        DrNew("IDBaseDatos") = DrBD("IDBaseDatos")
                        DrNew("BaseDatos") = DrBD("BaseDatos")
                        DrNew("DescBaseDatos") = DrBD("DescBaseDatos")
                        StDataReturn.DtDataSat.Rows.Add(DrNew)
                        StDataReturn.DtDataSat.Rows(StDataReturn.DtDataSat.Rows.Count - 1).AcceptChanges()
                    Else
                        Dim DrNew As DataRow = StDataReturn.DtDataSat.NewRow
                        DrNew.ItemArray = StDataReturn.DtDataSat.Rows(0).ItemArray
                        DrNew("IDBaseDatos") = DrBD("IDBaseDatos")
                        DrNew("BaseDatos") = DrBD("BaseDatos")
                        DrNew("DescBaseDatos") = DrBD("DescBaseDatos")
                        StDataReturn.DtDataSat.Rows.Add(DrNew.ItemArray)
                    End If
                Catch ex As Exception
                End Try
            Next
            AdminData.SetCurrentConnection(BDOrigen.DataBaseID)

            Return StDataReturn
        Else : Return Nothing
        End If
    End Function

    <Task()> Public Shared Sub SaveSateliteTable(ByVal data As DataSatTable, ByVal services As ServiceProvider)
        'Cambio a utilizar update de cabecera de la satélite para que se invoquen tareas y proceso update
        Dim BDOrigen As DatabaseInfo = AdminData.GetSessionInfo().DataBase
        Dim ClsEnt As BusinessHelper = BusinessHelper.CreateBusinessObject(data.Entity)
        Dim FilFinal As New Filter
        For Each Dc As DataColumn In ClsEnt.PrimaryKeyTable.Columns
            FilFinal.Add(Dc.ColumnName, data.DtData.Rows(0)(Dc.ColumnName))
        Next
        Dim DtEnt As DataTable = ClsEnt.Filter(FilFinal)
        For Each dc As DataColumn In data.DtData.Columns
            If dc.ColumnName <> "IDBaseDatos" AndAlso dc.ColumnName <> "BaseDatos" AndAlso dc.ColumnName <> "DescBaseDatos" AndAlso _
               dc.ColumnName <> "FechaCreacionAudi" AndAlso dc.ColumnName <> "FechaModificacionAudi" AndAlso dc.ColumnName <> "UsuarioAudi" Then
                DtEnt.Rows(0)(dc.ColumnName) = data.DtData.Rows(0)(dc.ColumnName)
            End If
        Next
        ClsEnt.Update(DtEnt)

        For Each DrBD As DataRow In AdminData.GetUserDataBases.Select("IDBaseDatos <> '" & BDOrigen.DataBaseID.ToString & "'")
            Dim DrFindBD() As DataRow = data.DtData.Select("IDBaseDatos = '" & DrBD("IDBaseDatos").ToString & "'")
            If DrFindBD.Length > 0 Then
                AdminData.SetCurrentConnection(DrBD("IDBaseDatos"))
                ClsEnt = BusinessHelper.CreateBusinessObject(data.Entity)
                DtEnt = ClsEnt.Filter(FilFinal)
                For Each dc As DataColumn In data.DtData.Columns
                    If dc.ColumnName <> "IDBaseDatos" AndAlso dc.ColumnName <> "BaseDatos" AndAlso dc.ColumnName <> "DescBaseDatos" AndAlso _
                       dc.ColumnName <> "FechaCreacionAudi" AndAlso dc.ColumnName <> "FechaModificacionAudi" AndAlso dc.ColumnName <> "UsuarioAudi" Then
                        DtEnt.Rows(0)(dc.ColumnName) = DrFindBD(0)(dc.ColumnName)
                    End If
                Next
                ClsEnt.Update(DtEnt)
            End If
        Next

        AdminData.SetCurrentConnection(BDOrigen.DataBaseID)
    End Sub

End Class