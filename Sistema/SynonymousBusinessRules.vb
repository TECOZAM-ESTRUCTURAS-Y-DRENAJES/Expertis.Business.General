Public Class SynonymousBusinessRules
    Inherits BusinessRules

    Private m_Synonymous As New Generic.Dictionary(Of String, String)

    Public Sub AddSynonymous(ByVal fromColumnName As String, ByVal toColumnName As String)
        '//Dictionary es sensible a mayusculas/minusculas
        m_Synonymous.Add(fromColumnName.ToLower, toColumnName.ToLower)
    End Sub

    Public Overrides Sub Execute(ByVal ruleData As BusinessRuleData, ByVal services As ServiceProvider)
        ruleData.ColumnName = ruleData.ColumnName.ToLower

        For Each key As String In m_Synonymous.Keys
            ruleData.Current(m_Synonymous(key)) = ruleData.Current(key)
            If ruleData.ColumnName = key Then ruleData.ColumnName = m_Synonymous(key)
        Next

        MyBase.Execute(ruleData, services)

        For Each key As String In m_Synonymous.Keys
            ruleData.Current(key) = ruleData.Current(m_Synonymous(key))
        Next

    End Sub

End Class
