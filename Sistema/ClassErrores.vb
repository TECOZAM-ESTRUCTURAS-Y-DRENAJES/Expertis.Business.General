#Region " Clases para tratamiento de errores en procesos masivos "

<Serializable()> _
Public Class CreateElement
    Public IDElement As Integer
    Public NElement As String
    Public ExtraInfo As Object
End Class

<Serializable()> _
Public Class ClassErrors
    Public Elements As String
    Public MessageError As String

    Public Sub New(ByVal elements As String, ByVal messageError As String)
        Me.Elements = elements
        Me.MessageError = messageError
    End Sub
    Public Sub New()
    End Sub
End Class

<Serializable()> _
Public Class LogProcess
    Public CreatedElements(-1) As CreateElement
    Public Errors(-1) As ClassErrors
End Class

#End Region