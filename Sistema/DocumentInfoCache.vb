Imports System.Collections.Generic

Public Class DocumentInfoCache(Of Doc As Document)

    Private m_dict As New Dictionary(Of String, Doc)

    Public Function GetDocument(ByVal ParamArray PrimaryKey() As Object) As Doc
        Dim DocInfo As Doc

        Dim Key As String = GetKey(PrimaryKey)

        If Len(Key) > 0 Then
            If m_dict.ContainsKey(Key) Then
                DocInfo = m_dict(Key)
            Else
                DocInfo = Activator.CreateInstance(GetType(Doc), PrimaryKey)
                m_dict(Key) = DocInfo
            End If
        End If

        Return DocInfo
    End Function


    Public Sub RemoveDocument(ByVal ParamArray PrimaryKey() As Object)
        Dim Key As String = GetKey(PrimaryKey)

        If Len(Key) > 0 Then
            If m_dict.ContainsKey(Key) Then
                m_dict.Remove(Key)
            End If
        End If
    End Sub

    Public Sub AddDocument(ByVal Key As String, ByVal Doc As Document)
        If Not m_dict.ContainsKey(Key) Then m_dict(Key) = Doc
    End Sub

    Public Function GetKey(ByVal ParamArray PrimaryKey() As Object) As String
        Dim key As String = ""
        If Not PrimaryKey Is Nothing Then
            If PrimaryKey.Length = 1 Then
                key = PrimaryKey(0) & String.Empty
            ElseIf PrimaryKey.Length > 1 Then
                For i As Integer = 0 To PrimaryKey.Length - 1
                    If Len(key) > 0 Then
                        key &= "/" & PrimaryKey(i) & String.Empty
                    Else
                        key = PrimaryKey(i) & String.Empty
                    End If
                Next
            End If
        End If
        Return key
    End Function

    Function Keys() As Dictionary(Of String, Doc).KeyCollection
        Return m_dict.Keys
    End Function

    Sub Clear()
        m_dict.Clear()
    End Sub

End Class