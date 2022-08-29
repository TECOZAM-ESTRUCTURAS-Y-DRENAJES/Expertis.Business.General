Public MustInherit Class ClassEntityInfo
    Public Overridable Sub Fill(ByVal ParamArray PrimaryKey() As Object)

    End Sub

    Public Sub New()

    End Sub

    Public Sub New(ByVal data As DataRow)
        Me.Fill(data)
    End Sub

    Public Sub Fill(ByVal data As DataRow)
        Dim obdr As New Binder
        For Each oPi As Reflection.PropertyInfo In Me.GetType.GetProperties()
            If data.Table.Columns.Contains(oPi.Name) Then
                If Length(data(oPi.Name)) > 0 Then
                    oPi.SetValue(Me, data(oPi.Name), Reflection.BindingFlags.Default, obdr, Nothing, Nothing)
                End If
            End If
        Next

        For Each oFi As Reflection.FieldInfo In Me.GetType.GetFields
            If data.Table.Columns.Contains(oFi.Name) Then
                If Length(data(oFi.Name)) > 0 Then
                    'HECHO EL TRY POR DAVID VELASCO-ERROR SOLAPA ORDENES, GESTION DE ALQUILER 15/09/2021
                    Try
                        oFi.SetValue(Me, data(oFi.Name), Reflection.BindingFlags.Default, obdr, Nothing)
                    Catch ex As Exception

                    End Try

                End If
            End If
        Next
    End Sub

    Private Class Binder
        Inherits Reflection.Binder

        Public Overrides Function BindToField(ByVal bindingAttr As System.Reflection.BindingFlags, ByVal match() As System.Reflection.FieldInfo, ByVal value As Object, ByVal culture As System.Globalization.CultureInfo) As System.Reflection.FieldInfo

        End Function

        Public Overrides Function BindToMethod(ByVal bindingAttr As System.Reflection.BindingFlags, ByVal match() As System.Reflection.MethodBase, ByRef args() As Object, ByVal modifiers() As System.Reflection.ParameterModifier, ByVal culture As System.Globalization.CultureInfo, ByVal names() As String, ByRef state As Object) As System.Reflection.MethodBase

        End Function

        Public Overrides Function ChangeType(ByVal value As Object, ByVal type As System.Type, ByVal culture As System.Globalization.CultureInfo) As Object
            If TypeOf value Is Decimal Then
                Return CDbl(value)
            ElseIf type.IsEnum Then
                Return System.Enum.Parse(type, value)
            Else
                Return value
            End If

        End Function

        Public Overrides Sub ReorderArgumentArray(ByRef args() As Object, ByVal state As Object)

        End Sub

        Public Overrides Function SelectMethod(ByVal bindingAttr As System.Reflection.BindingFlags, ByVal match() As System.Reflection.MethodBase, ByVal types() As System.Type, ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.MethodBase

        End Function

        Public Overrides Function SelectProperty(ByVal bindingAttr As System.Reflection.BindingFlags, ByVal match() As System.Reflection.PropertyInfo, ByVal returnType As System.Type, ByVal indexes() As System.Type, ByVal modifiers() As System.Reflection.ParameterModifier) As System.Reflection.PropertyInfo

        End Function
    End Class

End Class