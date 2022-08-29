'Infertace para la integración de Financiero
Public Interface IFinanciero

    '//Ejercicio y Plan Contable
    Function EjercicioPredeterminado(ByVal Fecha As Date, ByVal services As ServiceProvider) As String
    Function EjercicioPredeterminadoB(ByVal Fecha As Date, ByVal services As ServiceProvider) As String
    Sub FormatoCuentaContable(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
    Sub CambioCContable(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)
    Sub ComprobarCContable(ByVal data As BusinessRuleData, ByVal services As ServiceProvider)

    '//Analitica
    Sub ActualizarAnalitica(ByVal data As DataDocRow, ByVal services As ServiceProvider)
    Sub NuevaAnaliticaLinea(ByVal data As DataDocRow, ByVal services As ServiceProvider)

    Function AnaliticaCommonBusinessRules(ByVal data As BusinessRuleData, ByVal services As ServiceProvider) As IPropertyAccessor
    'Function AnaliticaCommonValidateRules(ByVal dttSource As DataTable, ByVal services As ServiceProvider) As DataTable
    Sub AnaliticaCommonValidateRules(ByVal dttSource As DataRow, ByVal services As ServiceProvider)

    '//Diario Contable
    Function CuentaSaldo(ByVal IDEjercicio As String, ByVal IDCContable As String) As DataTable
    Function ExtractoCuenta(ByVal IDEjercicio As String, ByVal IDCContable As String) As DataTable
    Function DeleteWhere(ByVal IDEjercicio As String, ByVal Filtro As Filter) As Boolean

End Interface
