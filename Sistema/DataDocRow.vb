'//Clase pública para utilizar desde otros negocios, pero no desde presentación
Public Class DataDocRow

    Public Doc As Document
    Public Row As DataRow

    Public Sub New(ByVal Doc As Document, ByVal Row As DataRow)
        Me.Doc = Doc
        Me.Row = Row
    End Sub

End Class
