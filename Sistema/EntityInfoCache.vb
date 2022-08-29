Imports System.Collections.Generic

Public Class EntityInfoCache(Of entityType As ClassEntityInfo)

    Private m_dict As New Dictionary(Of String, entityType)

    Public Function GetEntity(ByVal ParamArray PrimaryKey() As Object) As entityType
        Dim ClassInfo As entityType
        Dim Key As String = GetKey(PrimaryKey)

        If Len(Key) > 0 Then
            If m_dict.ContainsKey(Key) Then
                ClassInfo = m_dict(Key)
            Else
                ClassInfo = Activator.CreateInstance(GetType(entityType))
                ClassInfo.Fill(PrimaryKey)
                m_dict(Key) = ClassInfo
            End If
        End If

        Return ClassInfo
    End Function

    Protected Function GetKey(ByVal ParamArray PrimaryKey() As Object) As String
        Dim key As String = String.Empty
        If Not PrimaryKey Is Nothing Then
            If PrimaryKey.Length = 1 Then
                If TypeOf PrimaryKey(0) Is Guid Then
                    key = PrimaryKey(0).ToString
                Else
                    key = PrimaryKey(0) & String.Empty
                End If
            ElseIf PrimaryKey.Length > 1 Then
                For i As Integer = 0 To PrimaryKey.Length - 1
                    Dim PK As String = String.Empty
                    If TypeOf PrimaryKey(i) Is Guid Then
                        PK = PrimaryKey(i).ToString
                    Else
                        PK = PrimaryKey(i) & String.Empty
                    End If
                    If Len(key) > 0 Then
                        key &= "/" & PK
                    Else
                        key = PK
                    End If
                Next
            End If
        End If
        Return key
    End Function

    Public WriteOnly Property Add(ByVal ParamArray PrimaryKey() As Object) As entityType
        Set(ByVal value As entityType)
            Dim Key As String = GetKey(PrimaryKey)
            m_dict(Key) = value
        End Set
    End Property

    Sub Clear()
        m_dict.Clear()
    End Sub

End Class



