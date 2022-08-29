Imports System.Collections.Generic
Public MustInherit Class Document
    Inherits Dictionary(Of String, DataTable)

    Private m_HeaderRow As DataRow
    Public ReadOnly Property HeaderRow() As DataRow
        Get
            Return m_HeaderRow
        End Get
    End Property

    Public Sub AddHeader(ByVal key As String, ByVal value As DataTable)
        If Not value Is Nothing AndAlso value.Rows.Count > 0 Then
            MyBase.Add(key, value)
            m_HeaderRow = value.Rows(0)
        Else
            Throw New ApplicationException("No hay registro de cabecera para el documento")
        End If
    End Sub

    Public Function GetTables() As DataTable()
        Dim rslt(Me.Count - 1) As DataTable
        MyBase.Values.CopyTo(rslt, 0)
        Return rslt
    End Function

    Public Sub SetData()
        SetData(False)
    End Sub

    Public Sub SetData(ByVal resync As Boolean)
        Dim dt() As DataTable = GetTables()
        If dt Is Nothing Then
            Throw New eXpertisException("El documento no tiene ninguna tabla")
        End If
        For i As Integer = 0 To dt.Length - 1
            BusinessHelper.UpdateTable(dt(i))
        Next
    End Sub

    Public Sub ClearDoc()
        Dim dt() As DataTable = GetTables()
        If Not dt Is Nothing Then
            For i As Integer = 0 To dt.Length - 1
                If Not dt(i) Is Nothing Then
                    dt(i).Rows.Clear()
                End If
            Next
        End If
        MyBase.Clear()
    End Sub


    Public Function MergeData(ByVal updtCtx As UpdatePackage, _
                            ByVal businessEntity As String, _
                            ByVal primaryFields() As String, _
                            ByVal secondaryFields() As String, _
                            ByVal autonumeric As Boolean) As DataTable

        Dim oBusinessEntity As BusinessHelper = BusinessHelper.CreateBusinessObject(businessEntity)
        Dim dtBusinessData As DataTable = oBusinessEntity.Filter(CreateFilter(primaryFields, secondaryFields))

        For i As Integer = updtCtx.Count - 1 To 0 Step -1
            Dim oUD As UpdatePackageItem = updtCtx(i)
            Select Case oUD.EntityName
                Case businessEntity
                    Dim dtKeys As DataTable = oBusinessEntity.PrimaryKeyTable

                    'TODO sacar keys de aqui
                    Dim Keys(dtKeys.Columns.Count - 1) As String
                    For j As Integer = 0 To dtKeys.Columns.Count - 1
                        Keys(j) = dtKeys.Columns(j).ColumnName
                    Next

                    Dim dvBusinessData As New DataView(dtBusinessData, Nothing, Strings.Join(Keys, ", "), DataViewRowState.CurrentRows)
                    For Each oRw As DataRow In oUD.Data.Rows
                        If oRw.RowState = DataRowState.Modified Then
                            Dim KeyValues(Keys.Length - 1) As Object
                            For j As Integer = 0 To Keys.Length - 1
                                KeyValues(j) = oRw(Keys(j))
                            Next

                            Dim idx As Integer = dvBusinessData.Find(KeyValues)
                            If idx >= 0 Then
                                dtBusinessData.Rows.Remove(dvBusinessData(idx).Row)
                                dtBusinessData.ImportRow(oRw)
                            End If
                        ElseIf oRw.RowState = DataRowState.Added Then
                            If autonumeric Then
                                If Length(oRw(Keys(0))) = 0 Then oRw(Keys(0)) = DAL.AdminData.GetAutoNumeric
                            End If

                            '//Nos aseguramos la relación entre cabecera y lineas
                            For j As Integer = 0 To primaryFields.Length - 1
                                oRw(secondaryFields(j)) = HeaderRow(primaryFields(j))
                            Next

                            dtBusinessData.ImportRow(oRw)
                        End If
                    Next
                    updtCtx.Remove(oUD)
            End Select
        Next

        Return dtBusinessData
    End Function

    Private Function CreateFilter(ByVal primaryFields() As String, _
                            ByVal secondaryFields() As String) As IFilter

        Dim oFltr As New Filter
        For i As Integer = 0 To primaryFields.Length - 1
            oFltr.Add(New FilterItem(secondaryFields(i), HeaderRow(primaryFields(i))))
        Next
        Return oFltr
    End Function
End Class
