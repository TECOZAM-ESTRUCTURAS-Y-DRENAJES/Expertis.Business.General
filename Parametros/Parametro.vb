Public Class Parametro
    Inherits Solmicro.Expertis.Engine.BE.BusinessHelper

    Public Sub New()
        MyBase.New(cnEntidad)
    End Sub

    Private Const cnEntidad As String = "tbParametro"

#Region " CONSTANTES PARAMETROS "
    Public Const cnSMTPSERVER As String = "SmtpServer"
    Public Const cgFwPathPdfFactElec As String = "PATHPDFFE"
    Public Const cgFwComprasEmpresasGrupo As String = "CEMPGRP"
    Public Const cgFwTipoCompraPedidoVenta As String = "TIPO_COM_V"

    Public Const cgFwDigitosMaxPresup As Short = 6
    Public Const cnCobroContSI As String = "COBCONTSI"
    Public Const cnPagoContSI As String = "PAGCONTSI"
    Public Const cgFwContAuto As String = "AFCONT"
    Public Const cgFwClienteAuto As String = "AFCLI"

    Public Const cgFwServidorOlap As String = "SERV_OLAP"
    Public Const cgFwBDOlap As String = "BD_OLAP"
    Public Const cgFwCuboOlap As String = "CUBO_OLAP"
    Public Const cgFwWebOlap As String = "WEB_OLAP"

    Public Const cgFwAgrupacionGasto As String = "AGRUPGASTO"
    Public Const cgFwAgrupacionIngreso As String = "AGRUPINGR"
    Public Const cgFwBancoPropio As String = "BANCOPROP"

    Public Const cgFwUnidadLogGestionDoc As String = "UNIDAD_LOG"
    Public Const cgFwServidorGestionDoc As String = "GDSERVIDOR"
    Public Const cgFwWorkSpaceGestionDoc As String = "GDWSPACE"
    Public Const cgFwEstadoTarifa As String = "EST_TARIFA"
    Public Const cgFwPrioridadOTs As String = "MNTO_PRIOT"
    Public Const cgFwTipoProvContrata As String = "MNTO_CONTR"
    Public Const cgFwEstadoOT As String = "MNTO_ESTOT"
    Public Const cgFwTipoIva As String = "PREIVA"
    Public Const cgFwMoneda As String = "PREMON"
    Public Const cgFwFormaPago As String = "PREFP"
    Public Const cgFwCondicionPago As String = "PRECP"
    Public Const cgFwDiaPago As String = "PREDP"
    Public Const cgFwPais As String = "PREPAIS"
    Public Const cgFwUdTiempo As String = "UD_TIEMPO"
    Public Const cgFwGPromociones As String = "G_PROMOC"
    Public Const cgFwPromocionPre As String = "PRO_PREDET"
    Public Const cgFwHora As String = "PR_T_HORA"
    Public Const cgFwCierreAutoOrden As String = "CIERRE_AU"
    Public Const cgFwUdMedidaPred As String = "UD_MEDIDA"
    Public Const cgFwPec As String = "PEC"
    Public Const cgFwLoteMinimo As String = "LOTEMINIMO"
    Public Const cgFwPuntosImporte As String = "PUNTOS_IMP"
    Public Const cgFwPuntosMoneda As String = "PUNTOS_MON"
    Public Const cgFwControlProd As String = "CON_PROD"
    Public Const cgFwCajaInicial As String = "CAJA_INI"
    Public Const cgFwFPEfectivo As String = "FP_EFECTIV"
    Public Const cgFwCPEfectivo As String = "CP_EFECTIV"
    Public Const cgFwFPTarjeta As String = "FP_TARJETA"
    Public Const cgFwIvaCliente As String = "IVACLIENTE"
    Public Const cgFwIvaProveedor As String = "IVAPROV"
    Public Const cgCContableDeudoraLiqIVA As String = "LIQ_COMPRA"
    Public Const cgCContableAcreedoraLiqIVA As String = "LIQ_VENTA"
    Public Const cgFwActAutoAC As String = "AUTACT_ALC"
    Public Const cgFwActAutoAV As String = "AUTACT_ALV"
    Public Const cgFwActAutoArtMNTO As String = "ACTARTMNTO"
    Public Const cgFwTarifaPre As String = "TAR_PRE"
    Public Const cgFwAlmacenPre As String = "ALM_PREDET"
    Public Const cgFwMonInterAPre As String = "MON_INT_A"
    Public Const cgFwMonInterBPre As String = "MON_INT_B"
    Public Const cgFwContHistMov As String = "HIST_MOV"
    Public Const cgFwEmpRE As String = "EMP_RE"
    Public Const cgFwInmovAuto As String = "INMOV_AUTO"
    Public Const cgFwCGestPre As String = "CGEST_PRE"
    Public Const cgCCostePre As String = "CCOSTEPRED"
    Public Const cgFwCAnalitic As String = "C_ANALITIC"
    Public Const cgFwCAnaliticGes As String = "C_ANALIT_G"
    Public Const cgFwCAnalitTipo As String = "C_ANALIT_T"
    Public Const cgFwCAnalitAsig As String = "C_ANALIT_A"
    Public Const cgFwCAnalitOrig As String = "C_ANALIT_O"
    Public Const cgFwPrefCCAr As String = "PREF_CC_AR"
    Public Const cgFwPrefCCBa As String = "PREF_CC_BA"
    Public Const cgFwPrefCCPr As String = "PREF_CC_PR"
    Public Const cgFwPrefCCEfPr As String = "PREF_CC_EF"
    Public Const cgFwPrefCCPrGr As String = "PREFCCPRGR"
    Public Const cgFwPrefCCCl As String = "P_CC_CL"
    Public Const cgFwPrefCCECl As String = "P_CC_CL_C"
    Public Const cgFwPrefCCECA As String = "P_CC_CL_CA"
    Public Const cgFwPrefCCEGC As String = "P_CC_CL_GC"
    Public Const cgFwPrefCCClGr As String = "P_CC_CL_GR"
    Public Const cgFwPrefCCClAnticipo As String = "P_CC_CL_AN"
    Public Const cgFwCobrosBancoCli As String = "CO_GR_BCLE"
    Public Const cgFwCobrosCambioA As String = "CO_GR_CAMA"
    Public Const cgFwCobrosCambioB As String = "CO_GR_CAMB"
    Public Const cgFwCobrosCliente As String = "CO_GR_CLTE"
    Public Const cgFwCobrosDirEnv As String = "CO_GR_DIRE"
    Public Const cgFwCobrosFPago As String = "CO_GR_FP"
    Public Const cgFwCobrosFechaV As String = "CO_GR_FVTO"
    Public Const cgFwCobrosMoneda As String = "CO_GR_MONE"
    Public Const cgFwPagosBancoProv As String = "PA_GR_BPRO"
    Public Const cgFwPagosCambioA As String = "PA_GR_CAMA"
    Public Const cgFwPagosCambioB As String = "PA_GR_CAMB"
    Public Const cgFwPagosProveedor As String = "PA_GR_PROV"
    Public Const cgFwPagosFPago As String = "PA_GR_FP"
    Public Const cgFwPagosFechaV As String = "PA_GR_FVTO"
    Public Const cgFwPagosMoneda As String = "PA_GR_MONE"
    Public Const cgFwTipoAsientoRemesa As String = "ASIENT_REM"
    Public Const cgFwCtaIngesoFinanciero As String = "CtaIngFin"
    Public Const cgFwCtaEfectGstonCobros As String = "CtaEGC"
    Public Const cgFwCtaEfect As String = "CtaEF"
    Public Const cgFwCtaDevolucionGasto As String = "CtaDevGas"
    Public Const cgFwCtaDevolucionComision As String = "CtaDevCom"
    Public Const cgFwCtaDevolucionIvaFra As String = "CtaDevIva"
    Public Const cgFwCobroDescontSituacion As String = "CobDescSit"
    Public Const cgFwPagoDescontSituacion As String = "PagDescSit"
    Public Const cgFwCobroContSituacion As String = "CobContSit"
    Public Const cgFwPagoContSituacion As String = "PagContSit"
    Public Const cgFwCobroDiasSeg1 As String = "CODIASSEG1"
    Public Const cgFwCobroDiasSeg2 As String = "CODIASSEG2"
    Public Const cgFwCobroDiasSeg3 As String = "CODIASSEG3"
    Public Const cgFwCobroCambioAuto1 As String = "COCAMAUTO1"
    Public Const cgFwCobroCambioAuto2 As String = "COCAMAUTO2"
    Public Const cgFwCobroCambioAuto3 As String = "COCAMAUTO3"
    Public Const cgFwContadorRemesa As String = "CONT_REM"
    Public Const cgFwContadorRemesaAnticipo As String = "CONT_ANT"
    Public Const cgFwActivoPre As String = "ACTIVO_PRE"
    Public Const cgFwArticuloCons As String = "ART_CONS"
    Public Const cgFwArticuloReten As String = "ART_RETEN"
    Public Const cgFwClientePre As String = "CLIE_GEN"
    Public Const cgFwRecalcularPedido As String = "CP_PL_REC"
    Public Const cgFwProvRetencion As String = "PROV_RETEN"
    Public Const cgFwPrecioMov As String = "STPREC_MOV"
    Public Const cgFwPathFoto As String = "PATHFOTOS"
    Public Const cgFwCCABonoProp As String = "CCABONPROP"
    Public Const cgFwCCCargoCentro As String = "CCCARGCENT"
    Public Const cgFwInforme347Dec347 As String = "347DEC"
    Public Const cgFwCCFCompra As String = "CC_FCOMPRA"
    Public Const cgFwGestionStockNegativo As String = "STNEGATIVO"
    Public Const cgFwGestionDobleUnidad As String = "STOCK2UD"
    Public Const cgPrecioMedioPorMovimiento As String = "PMXMOVTO"
    Public Const cgFwRecalcularValoracion As String = "STRECVAL"
    Public Const cgFwTipoInventario As String = "STTIPOINV"
    Public Const cgFwFormaEnvio As String = "PREFE"
    Public Const cgFwCondicionEnvio As String = "PRECE"
    Public Const cgFwIDUbicacionNoDefinida As String = "UBIC_NODEF"
    Public Const cgFwPedidoEnTransferencia As String = "PED_TRANSF"
    Public Const cgFwFechaAlbaran As String = "FECHA_FACT"
    Public Const cgFwCodAutomatica As String = "COD_AUTOM"
    Public Const cgFwCierreInventario As String = "CIERREINV"
    Public Const cgFwGestionStockNegativoArticulo As String = "ST_NEG_ART"
    Public Const cgFwValoracionCosteStd As String = "VAL_CSTD"
    Public Const cgFwContadorNotaTransportista As String = "CNTRANS"
    Public Const cgRemesaPago As String = "REMESAPAGO"
    Public Const cgPuntoVerde As String = "PTOVERDE"

    'David Inserción de Parametros para la gestión de tipos de albarán
    Public Const cgFwAlbaranDefault As String = "TIPO_ALB"
    Public Const cgFwAlbaranServicioDefault As String = "TIPO_ALB_S"
    Public Const cgFwAlbaranDeDeposito As String = "TIPO_ALB_D"
    Public Const cgFwAlbaranDeConsumo As String = "TIPO_ALB_C"
    Public Const cgFwAlbaranRetornoAlquiler As String = "TIPO_ALB_A"
    Public Const cgFwAlbaranDeDevolucion As String = "TIPOALB_DV"
    Public Const cgFwAlbaranDeIntercambio As String = "TIPO_ALB_I"

    Public Const cgFwCTipoFV As String = "CO_TFV"
    Public Const cgFwCTipoFVB As String = "CO_TFVB"
    Public Const cgFwCTipoFC As String = "PA_TFC"
    Public Const cgFwCTipoFCB As String = "PA_TFCB"
    Public Const cgFwCTipoPC As String = "PA_TPC"
    Public Const cgFwCTipoCP As String = "CO_TPC"
    Public Const cgFwCTipoCA As String = "CO_ANTICI"
    Public Const cgFwCTipoCF As String = "CO_FIANZA"
    Public Const cgFwCTipoCR As String = "CO_RETEN"
    Public Const cgFwPTipoPA As String = "PA_ANTICI"
    Public Const cgFwPTipoPF As String = "PA_FIANZA"
    Public Const cgFwPTipoPR As String = "PA_RETEN"
    'Constante para controlar la Fecha de la Factura de Compra
    Public Const cgFwFechaFC As String = "FECH_FRA_C"
    Public Const cgFwTipoCompraNormal As String = "TIPO_COM"
    Public Const cgFwTipoCompraSubcontratacion As String = "TIPO_COM_S"
    Public Const cgFwTipoOfertaComercialPredeterminado As String = "TIPOOC_PRE"
    Public Const cgFwOperarioGenerico As String = "OPE_GEN"
    'Calidad
    Public Const cgFwCalControl As String = "CALCONTROL"
    Public Const cgFwCalDefCal As String = "CALDEFCAL"
    Public Const cgFwCalDefEst As String = "CALDEFEST"
    Public Const cgFwCalDefLoc As String = "CALDEFLOC"
    Public Const cgFwCalDem As String = "CALDEM"
    Public Const cgFwCalPeriodo As String = "CALPERIODO"
    Public Const cgFwCalPuntos As String = "CALPUNTOS"
    Public Const cgFwCalNUltRecepcion As String = "CALULTRECEP"
    'Activos
    Public Const cgFwEstadoActivoPorDefecto As String = "DEFESTACT"
    Public Const cgFwGestionSeriesConActivos As String = "GESSERACT"
    Public Const cgFwCategoriaActivoPorDefecto As String = "DEF_CATEGA"
    Public Const cgFwZonaPorDefecto As String = "DEF_ZONA"
    Public Const cgFwClaseActivoPorDefecto As String = "DEF_CLASEA"
    Public Const cgFwCentroCostePorDefecto As String = "DEF_CCOSTE"
    ''Estados de activo predeterminados de la aplicacion (con marca Sistema=1)
    'Public Const ESTADOACTIVO_DISPONIBLE As String = "0"
    'Public Const ESTADOACTIVO_ENMANTENIMIENTO As String = "1"
    'Public Const ESTADOACTIVO_RESERVADA As String = "2"
    'Public Const ESTADOACTIVO_TRABAJANDO As String = "3"
    'Public Const ESTADOACTIVO_VENDIDO As String = "4"
    'Public Const ESTADOACTIVO_BAJA As String = "5"
    'Public Const ESTADOACTIVO_AVERIADO As String = "6"
    'Public Const ESTADOACTIVO_ENTRANSITO As String = "7"
    'Public Const ESTADOACTIVO_OCUPADOENPORTE As String = "8"
    'Public Const ESTADOACTIVO_PENDIENTEDERETORNAR As String = "14"
    'Parametro Planificación Obras
    Public Const cgFwHorasDiaOperario As String = "HORDIAOPE"
    Public Const cgFwMaxDiasConsulta As String = "MAXDIASCON"
    'Parametros Alquiler
    Public Const cgFwDiasLaborables As String = "DIAS_LABOR"
    Public Const cgFwDiasNaturales As String = "DIAS_NATUR"
    Public Const cgFwMESES As String = "MESES"
    Public Const cgFwLimHoraAlquiler As String = "LIMHORAALQ"
    Public Const cgFwLimHoraAlquilerRet As String = "LIMHORAALR"
    Public Const cgFwConceptoAlquiler As String = "CONCEP_ALQ"
    Public Const cgGestAlq As String = "GEST_ALQ"
    Public Const cgArtSegAlq As String = "ART_SEG_AL"  'parametro de Articulo por Defecto para facturacion de seguros
    Public Const cgEstadoActivoRetorno As String = "ESTACTRET"
    Public Const cgSeguroExceso As String = "SEGUROEXC"
    'Parametro de almacen para articulo de portes
    Public Const cgAlmPortes As String = "ALM_PORTE"
    'Parametro de Cuenta Contable para consumo de Material de Alquiler
    Public Const cgCContableMAt As String = "CCALBCALQ"
    'Parametro de Recuperacion de Almacen para Alquileres
    Public Const cgRecupAlm As String = "RECUP_ALM"
    'Parametro de Recuperacion de Porcentaje Seguros Generales para Alquiler
    Public Const cgPorcSeg As String = "ALQSEGPORC"
    'Parametro de agrupación cobros remesables
    Public Const cgAgrupCobroRem As String = "AGRUPCOBRO"
    'Parametro de Recuperacion del número de factura en la agrupación de pagos
    Public Const cgAgrupPago As String = "FRAPAGOAGR"
    'Parametro de Recuperacion del número de factura en la agrupación de cobros
    Public Const cgAgrupCobro As String = "FRACOBROAG"
    'Parametro para habilitar los permisos de expedición
    Public Const cgFwPedAviso As String = "PED_AVISO"
    Public Const cgFwPedPreparado As String = "PED_PREP"
    'Parametro contador por defecto de observaciones
    Public Const cgFwContObservaciones As String = "CONT_OBS"
    Public Const cgFwActAutoSitRemC As String = "REMCAMAUT"
    Public Const cgBancoFrDto As String = "BANCOFRDTO"
    'Parámetro de Valoración de almacen para la Regularización de Existencias
    Public Const cgValoracionAlm As String = "VALALM"
    'Parametros Proyectos
    Public Const cgGESTPROMO As String = "GEST_PROMO"
    Public Const cgGESTCONST As String = "GEST_CONST"
    Public Const cgNOACUMTRAB As String = "NOACUMTRAB"
    Public Const cgFwArtFactVtosProy As String = "ART_PROYEC"
    Public Const cgFwGastosProy As String = "GAS_PROYEC"
    Public Const cgFwVariosProy As String = "VAR_PROYEC"
    Public Const cgMultinivel As String = "MULTINIVEL"
    Public Const cgTIPOPROY As String = "TIPOPROY"

    '//Balances Personalizados
    Public Const PERSONALIZAR_BALANCES As String = "BP_PERBAL"
    Public Const CLASE_BALANCE_PERSONALIZADO As String = "BP_CLASS"
    Public Const ENSAMBLADO_BALANCE_PERSONALIZADO As String = "BP_ASSEMBL"
    Public Const cgEmpresaGrupo As String = "EMP_VCG"
    'Parametros de Cuentas Contables
    Public Const cgCCVenta As String = "CCVENTA"
    Public Const cgCCExport As String = "CCEXPORT"
    Public Const cgCCVentaGrupo As String = "CCVENTAGR"
    Public Const cgCCExportGrupo As String = "CCEXPORTGR"
    Public Const cgCCCompra As String = "CCCOMPRA"
    Public Const cgCCImportGrupo As String = "CCIMPORTGR"
    Public Const cgCCCompraGrupo As String = "CCCOMPRAGR"
    Public Const cgCCImport As String = "CCIMPORT"
    Public Const cgCCVentaRegalo As String = "CCVENTAREG"
    Public Const cgCCGastoRegalo As String = "CCGASTOREG"
    Public Const cgCCStocks As String = "CCSTOCKS"
    Public Const cgCCCRM As String = "CCCRM"
    Public Const cgPRECTACONC As String = "DIG_CONBAN"
    'Produccion
    Public Const cgOpeRecDef As String = "OPEREC"
    Public Const cgCenRecDef As String = "CENREC"

    Public Const cgEstadoCobroFactoring As String = "ESTFACTORI"
    Public Const cgEstadoAveria As String = "SUSESTAAV"
    Public Const cgIncidenciaAveria As String = "SUSINCAVER"
    Public Const cgRutaDefecto As String = "RUTA"
    Public Const cgEstructuraDefecto As String = "ESTRUCTURA"

    Public Const cgContPagare As String = "CONT_PAG"
    Public Const cgDescPagEnv As String = "IMPR_PAG"
    Public Const cgTransAutoAlquiler As String = "TRANS_ALM"
    'Parametros de Fianzas
    Public Const cgCONTFIANZA As String = "CONTFIANZA"
    Public Const cgCOMPFIANZA As String = "COMPFIANZA"
    'Parametro para la facturacion de la tasa de Residuos
    Public Const cgFwResiPorc As String = "RESI_PORC"
    Public Const cgFwResiLimite As String = "RESI_LIMIT"
    Public Const cgFwResiArtic As String = "RESI_ARTIC"
    'Parametro de Incremento del precio de venta de la OT
    Public Const cgFwIncPrecVtaOt As String = "INC_PR_OT"
    Public Const cgFwArtFactMod As String = "ART_FACT_H"
    Public Const cgFwArtFactCont As String = "ART_FACT_C"
    Public Const cgFwArtFactGastos As String = "ART_FACT_G"

    Public Const cgTIPOCLASIF As String = "TIPOCLASIF"

    Public Const cgFICPATHIMP As String = "FICPATHIMP"
    Public Const cgFICPATHADM As String = "FICPATHADM"
    Public Const cgFICPATHXSD As String = "FICPATHXSD"

    Public Const cgALMCC As String = "ALMCC"
    Public Const cgFwTrabTareaPred As String = "OBTAREA"

    'Parámetros para la gestión de Leasing
    Public Const cgFwContLeasing As String = "CONTLEASIN"
    Public Const cgFwPrefLeasingGastos As String = "LEASPREGAS"
    Public Const cgFwPrefLeasingCP As String = "LEASPRECP"
    Public Const cgFwPrefLeasingLP As String = "LEASPRELP"
    Public Const cgFwPrefLeasingCoste As String = "LEASPRECOS"
    Public Const cgFwPrefLeasingIntereses As String = "LEASPREINT"
    Public Const cgFwArticuloAlternativo As String = "ARTALTER"
    'Parámetros para importación de BC3
    Public Const cgBC3Material As String = "IMPMAT"
    Public Const cgBC3Mod As String = "IMPMOD"
    Public Const cgBC3Centro As String = "IMPCENTRO"
    Public Const cgBC3vaRIOS As String = "IMPVAR"
    'Parámetros para el Configurador
    Public Const CONFIGURADOR_INSTALADO As String = "CONFIG"
    Public Const cgNivelesAnalitica As String = "NIVELANALI"
    'Parámetros para Bodegas
    Public Const cgAnalisisPreVino As String = "ANALISIPRE"
    Public Const cgAnalisisPreFinca As String = "ANALISIFIN"
    Public Const cgAnalisisPreUva As String = "ANALISIUVA"
    'Parametros TPV
    Public Const cgTPVCADVALE As String = "TPV_CADVAL"
    Public Const cgGESTCONCEPTOOFC As String = "GESCONOFC"
    Public Const cgCONTPROVTPV As String = "CONTPROTPV"

    Public Const cgRUTAIMPORT As String = "PATHIMPORT"

    'Parámetros de coinfiguración de módulos
    Public Const cgFwExpertisSAAS As String = "SAAS"
    Public Const cgFwContabilidad As String = "C_CONTA"
    Public Const cgLogistica As String = "C_LOGISTI"
    Public Const cgGESTBDG As String = "C_BODEGA"

    Public Const cgAPPDDIG As String = "APPDDIG"

    Public Const cnRIESGOCLTE As String = "RIESGOCLTE"

    Public Const cnCSB43ORIGEN As String = "CSB43ORIG"
    Public Const cnCSB43DESTINO As String = "CSB43DESTI"
    Public Const cnCSB43DIAS As String = "CSB43DIAS"

    Public Const cgFwActAutoRT As String = "TR_ACTAUT"
    Public Const cgFwSTCompleta As String = "TR_SOLCOMP"

    Public Const cgContabilidadMultiple As String = "CONTMULT"

    Public Const cgPedidoConfirmado As String = "PED_CONF"
    Public Const cgExpedirPedConfir As String = "PREP_EXP"

    Public Const cnIVA_CAJA_VENTAS As String = "IVA_CAJA"

    '//Inventario Permanente
    Public Const cnINV_PERMANENTE As String = "CONT_MOV"
    Public Const cnCCPRODUCTO_CURSO As String = "CCPROCUR"
    Public Const cnCCINGRESO As String = "CCPROCURMO"

    Public Const cnTIPO_ALB_DISTRIBUIDOR As String = "TIPOALB_ED"
    Public Const cnTIPO_ALB_ABONO_DISTRIBUIDOR As String = "TIPOALB_AD"

    '//Contador para los pedidos realizados desde el ejecutable para Tablet
    Public Const cnCONTADOR_PEDIDO_COMPRA_TABLET As String = "CONT_PCT"

    '//Tipo Documento por defecto para Clientes, Proveedores y Operarios  
    Public Const cnTIPO_DOCUMENTO As String = "TIPO_DOC"

#End Region


#Region "Eventos RegisterValidateTasks"

    Protected Overrides Sub RegisterValidateTasks(ByVal validateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterValidateTasks(validateProcess)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarIDParametro)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarDescParametro)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarTipoParametro)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarGrupoParametro)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarSubGrupoParametro)
        validateProcess.AddTask(Of DataRow)(AddressOf ValidarContadorMovimientos)
    End Sub

    <Task()> Public Shared Sub ValidarIDParametro(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDParametro")) > 0 Then
            If data.RowState = DataRowState.Added Then
                'Miramos que el código del parámetro no esté repetido
                Dim DtParametro As DataTable = New Parametro().SelOnPrimaryKey(data("IDParametro"))
                If Not DtParametro Is Nothing AndAlso DtParametro.Rows.Count > 0 Then
                    ApplicationService.GenerateError("El campo 'IDParametro' está duplicado.")
                End If
            End If
        Else
            ApplicationService.GenerateError("El ID Parametro es un dato obligatorio.")
        End If
    End Sub

    <Task()> Public Shared Sub ValidarDescParametro(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("DescParametro")) = 0 Then
            ApplicationService.GenerateError("La Descripción del Parámetro es un dato Obligatorio.")
        End If
    End Sub

    <Task()> Public Shared Sub ValidarTipoParametro(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("TipoParametro")) = 0 Then
            ApplicationService.GenerateError("El Tipo de parámetro es un dato obligatorio.")
        End If
    End Sub

    <Task()> Public Shared Sub ValidarGrupoParametro(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDGrupoParametro")) > 0 Then
            Dim dt As DataTable = New ParametroGrupo().SelOnPrimaryKey(data("IDGrupoParametro"))
            If dt Is Nothing OrElse dt.Rows.Count = 0 Then
                ApplicationService.GenerateError("El Grupo de Parámetro introducido no existe")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarSubGrupoParametro(ByVal data As DataRow, ByVal services As ServiceProvider)
        If Length(data("IDSubGrupoParametro")) > 0 Then
            Dim Dt As DataTable = New ParametroSubGrupo().SelOnPrimaryKey(data("IDSubGrupoParametro"), data("IDGrupoParametro"))
            If Dt Is Nothing OrElse Dt.Rows.Count = 0 Then
                ApplicationService.GenerateError("El SubGrupo de Parámetro introducido no existe en el grupo de parámetro actual")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub ValidarContadorMovimientos(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data.RowState = DataRowState.Added OrElse (data.RowState = DataRowState.Modified AndAlso (data("IDParametro") <> data("IDParametro", DataRowVersion.Original) OrElse Nz(data("Valor")) <> Nz(data("Valor", DataRowVersion.Original)))) Then
            If data("IDParametro").Equals(cgFwContHistMov) AndAlso Length(data("Valor")) > 0 Then
                Dim Contadores As EntityInfoCache(Of ContadorInfo) = services.GetService(Of EntityInfoCache(Of ContadorInfo))()
                Dim ContInfo As ContadorInfo = Contadores.GetEntity(data("Valor"))
                If Not ContInfo.Numerico Then
                    ApplicationService.GenerateError("El contador de movimientos debe ser configurado como contador numérico.")
                ElseIf Nz(ContInfo.Contador, 0) < ProcessServer.ExecuteTask(Of Object, Integer)(AddressOf Contador.GetMaxNumeroMovimiento, Nothing, services) Then
                    ApplicationService.GenerateError("El valor del contador '|' para los movimientos no puede ser inferior al mayor movimiento generado.", ContInfo.IDContador)
                End If
            End If
        End If
    End Sub

#End Region

#Region "Eventos RegisterUpdateTasks"

    Protected Overrides Sub RegisterUpdateTasks(ByVal updateProcess As Engine.BE.BusinessProcesses.Process)
        MyBase.RegisterUpdateTasks(updateProcess)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarAgrupCobrosPagos)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarTipoParamBooleano)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarTipoParamDate)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarTipoParamDoubleInt)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarTipoParamEntidad)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarTipoParamEnumerado)
        updateProcess.AddTask(Of DataRow)(AddressOf AsignarTipoParamTexto)
    End Sub

    <Task()> Public Shared Sub AsignarAgrupCobrosPagos(ByVal data As DataRow, ByVal services As ServiceProvider)
        If data("IDParametro") = cgFwCobrosCliente OrElse data("IDParametro") = cgFwPagosProveedor Then
            data("Valor") = 1
        End If
    End Sub

    <Task()> Public Shared Sub AsignarTipoParamBooleano(ByVal data As DataRow, ByVal services As ServiceProvider)
        If CInt(data("TipoParametro").ToString) = enumprtTipoParametro.prtBoolean Then
            data("Entidad") = System.DBNull.Value
            data("CampoEntidad") = System.DBNull.Value
            data("NombEnumerado") = System.DBNull.Value
            If data("Valor") = -1 Then data("Valor") = 1
            If Not IsNumeric(data("Valor")) OrElse (CInt(data("Valor")) <> 0 And CInt(data("Valor")) <> 1) Then
                ApplicationService.GenerateError("El valor introducido no es válido de acuerdo con el tipo de parámetro (Booleano)")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub AsignarTipoParamDate(ByVal data As DataRow, ByVal services As ServiceProvider)
        If CInt(data("TipoParametro").ToString) = enumprtTipoParametro.prtDate Then
            data("Entidad") = System.DBNull.Value
            data("CampoEntidad") = System.DBNull.Value
            data("NombEnumerado") = System.DBNull.Value
            If Not IsDate(data("Valor")) Then
                ApplicationService.GenerateError("El valor introducido no es válido de acuerdo con el tipo de parámetro (Fecha)")
            Else : data("Valor") = CDate(data("Valor")).ToString("d")
            End If
        End If
    End Sub

    <Task()> Public Shared Sub AsignarTipoParamDoubleInt(ByVal data As DataRow, ByVal services As ServiceProvider)
        If CInt(data("TipoParametro").ToString) = enumprtTipoParametro.prtDouble OrElse CInt(data("TipoParametro").ToString) = enumprtTipoParametro.prtInteger Then
            data("Entidad") = System.DBNull.Value
            data("CampoEntidad") = System.DBNull.Value
            data("NombEnumerado") = System.DBNull.Value
            If Not IsNumeric(data("Valor")) Then
                ApplicationService.GenerateError("El valor introducido no es válido de acuerdo con el tipo de parámetro (Numérico)")
            ElseIf CInt(data("TipoParametro").ToString) = enumprtTipoParametro.prtInteger Then
                data("Valor") = CInt(data("Valor"))
            End If
        End If
    End Sub

    <Task()> Public Shared Sub AsignarTipoParamEntidad(ByVal data As DataRow, ByVal services As ServiceProvider)
        If CInt(data("TipoParametro").ToString) = enumprtTipoParametro.prtEntidad Then
            data("NombEnumerado") = System.DBNull.Value
            If Length(data("Valor")) = 0 Then ApplicationService.GenerateError("El valor introducido no es válido de acuerdo con el tipo de parámetro (Entidad)")

            If Length(data("Entidad")) = 0 Then
                ApplicationService.GenerateError("Introduzca la entidad")
            Else
                Dim dtEntidad As DataTable = ProcessServer.ExecuteTask(Of Object, DataTable)(AddressOf DatosSistema.GetEntitys, Nothing, services)
                Dim DrEntidad() As DataRow = dtEntidad.Select(String.Format("Entidad='{0}'", data("Entidad")))
                If DrEntidad Is Nothing OrElse DrEntidad.GetLength(0) = 0 Then
                    ApplicationService.GenerateError("La entidad elegida no existe")
                Else
                    If Length(data("CampoEntidad")) = 0 Then
                        ApplicationService.GenerateError("Introduzca el campo de la entidad")
                    Else
                        Dim DtFields As DataTable = ProcessServer.ExecuteTask(Of String, DataTable)(AddressOf Parametro.ObtenerCamposEntidad, data("Entidad"), services)
                        If DtFields Is Nothing OrElse DtFields.Rows.Count = 0 Then
                            ApplicationService.GenerateError("El campo de la entidad elegida no existe")
                        Else
                            Dim drCampos() As DataRow = DtFields.Select(String.Format("CampoEntidad='{0}'", data("CampoEntidad")))
                            If drCampos Is Nothing OrElse drCampos.GetLength(0) = 0 Then
                                ApplicationService.GenerateError("El campo de la entidad elegida no existe")
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    <Task()> Public Shared Sub AsignarTipoParamEnumerado(ByVal data As DataRow, ByVal services As ServiceProvider)
        If CInt(data("TipoParametro").ToString) = enumprtTipoParametro.prtEnumerado Then
            data("Entidad") = System.DBNull.Value
            data("CampoEntidad") = System.DBNull.Value
            If Not IsNumeric(data("Valor")) Then ApplicationService.GenerateError("El valor introducido no es válido de acuerdo con el tipo de parámetro (Enumerado)")
            If Length(data("NombEnumerado")) = 0 Then
                ApplicationService.GenerateError("Introduzca el nombre del enumerado")
            Else
                'Miramos si el enumerado existe.
                Dim DrEnums() As DataRow = New Parametro().ObtenerEnumerados().Select(String.Format("NombEnum='{0}'", data("NombEnumerado")))
                If DrEnums Is Nothing OrElse DrEnums.GetLength(0) = 0 Then
                    ApplicationService.GenerateError("El enumerado elegido no existe")
                Else
                    'Comprobamos que el valor del enumerado esté de acuerdo con el enumerado elegido
                    Dim f As New Filter(FilterUnionOperator.And)
                    f.Add(New StringFilterItem("NombEnum", FilterOperator.Equal, data("NombEnumerado")))
                    f.Add(New NumberFilterItem("Valor", FilterOperator.Equal, data("Valor")))
                    Dim DtEnum As DataTable = New BE.DataEngine().Filter("xEnumerate", f, , , , True)
                    If DtEnum Is Nothing OrElse DtEnum.Rows.Count = 0 Then
                        ApplicationService.GenerateError("No existe el valor para el enumerado elegido")
                    End If
                End If
            End If
        End If
    End Sub

    <Task()> Public Shared Sub AsignarTipoParamTexto(ByVal data As DataRow, ByVal services As ServiceProvider)
        If CInt(data("TipoParametro").ToString) = enumprtTipoParametro.prtString Then
            data("Entidad") = System.DBNull.Value
            data("CampoEntidad") = System.DBNull.Value
            data("NombEnumerado") = System.DBNull.Value
            If Length(data("Valor")) = 0 Then ApplicationService.GenerateError("El Valor es un dato obligatorio")
        End If
    End Sub

#End Region

    <Serializable()> _
    Public Class MonedasInternas
        Public strMonedaA As String
        Public strMonedaB As String
    End Class

    Public Function MargenPredeterminadoOT() As Double
        Return LeerParametro("INC_PR_OT")
    End Function

    Public Function AgrupacionCobroRem() As String
        AgrupacionCobroRem = LeerParametro(cgAgrupCobroRem)
    End Function

    Public Function ComprasEmpresasGrupo() As Boolean
        ComprasEmpresasGrupo = CBool(LeerParametro(cgFwComprasEmpresasGrupo))
    End Function

    Public Function TipoCompraPedidoVenta() As String
        TipoCompraPedidoVenta = LeerParametro(cgFwTipoCompraPedidoVenta)
    End Function

    Public Function LeerParametro(ByVal strIdParametro As String) As String
        Return LeerParametro(strIdParametro, True)
    End Function

    Protected Function LeerParametro(ByVal IdParametro As String, ByVal MostrarMensaje As Boolean) As String
        Dim resultado As String = String.Empty
        Dim dt As DataTable = SelOnPrimaryKey(IdParametro)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            resultado = dt.Rows(0)("Valor") & String.Empty
        Else
            If MostrarMensaje Then
                ApplicationService.GenerateError("El parámetro <|> no existe.", IdParametro)
            End If
        End If
        Return resultado
    End Function

    Public Function EstadoMaquinaAveriada() As String
        EstadoMaquinaAveriada = LeerParametro(cgEstadoAveria)
    End Function

    Public Function IncidenciaSustitucionMaquinasAveriadas() As String
        IncidenciaSustitucionMaquinasAveriadas = LeerParametro(cgIncidenciaAveria)
    End Function

    Public Function ContabilizacionFianza() As String
        ContabilizacionFianza = LeerParametro(cgCONTFIANZA)
    End Function

    Public Function CompensacionFianza() As String
        CompensacionFianza = LeerParametro(cgCOMPFIANZA)
    End Function

    Public Function EstadoActivoRetornosPorDefecto() As String
        EstadoActivoRetornosPorDefecto = LeerParametro(cgEstadoActivoRetorno)
    End Function

    Public Function TransAutoAlquiler() As String
        TransAutoAlquiler = LeerParametro(cgTransAutoAlquiler)
    End Function

    Public Function AplicarMultinivelEnObras() As Boolean
        AplicarMultinivelEnObras = LeerParametro(cgMultinivel)
    End Function

    Public Function NoAcumularEnTrabajo() As Boolean
        NoAcumularEnTrabajo = LeerParametro(cgNOACUMTRAB)
    End Function

    Public Function GenerarAsientoPago() As Boolean
        GenerarAsientoPago = LeerParametro(cnPagoContSI)
    End Function

    Public Function GestionConstructoras() As Boolean
        GestionConstructoras = LeerParametro(cgGESTCONST)
    End Function

    Public Function GestionPromotoras() As Boolean
        GestionPromotoras = LeerParametro(cgGESTPROMO)
    End Function

    Public Function GestionBodegas() As Boolean
        GestionBodegas = LeerParametro(cgGESTBDG)
    End Function

    Public Function AplicacionGestionAlquiler() As Boolean
        AplicacionGestionAlquiler = CBool(LeerParametro(cgGestAlq))
    End Function

    Public Function GenerarAsientoCobro() As Boolean
        GenerarAsientoCobro = LeerParametro(cnCobroContSI)
    End Function

    Public Function RemesaPago() As String
        RemesaPago = LeerParametro(cgRemesaPago)
    End Function

    Public Function LimiteHoraAlquiler() As String
        LimiteHoraAlquiler = LeerParametro(cgFwLimHoraAlquiler)
    End Function

    Public Function LimiteHoraAlquilerRetorno() As String
        LimiteHoraAlquilerRetorno = LeerParametro(cgFwLimHoraAlquilerRet)
    End Function

    Public Function EstadoTarifa() As String
        EstadoTarifa = LeerParametro(cgFwEstadoTarifa)
    End Function

    Public Function UnidadLogicaGestionDoc() As String
        UnidadLogicaGestionDoc = LeerParametro(cgFwUnidadLogGestionDoc)
    End Function

    Public Function ConceptoAlquiler() As String
        ConceptoAlquiler = LeerParametro(cgFwConceptoAlquiler)
    End Function

    Public Function DiasLaborables() As String
        DiasLaborables = LeerParametro(cgFwDiasLaborables)
    End Function

    Public Function DiasNaturales() As String
        DiasNaturales = LeerParametro(cgFwDiasNaturales)
    End Function

    Public Function Meses() As String
        Meses = LeerParametro(cgFwMESES)
    End Function

    Public Function ServidorOLAP() As String
        ServidorOLAP = LeerParametro(cgFwServidorOlap)
    End Function

    Public Function BDOLAP() As String
        BDOLAP = LeerParametro(cgFwBDOlap)
    End Function

    Public Function CuboOLAP() As String
        CuboOLAP = LeerParametro(cgFwCuboOlap)
    End Function

    Public Function TieneRE() As String
        TieneRE = LeerParametro(cgFwEmpRE)
    End Function

    Public Function WebOLAP() As String
        WebOLAP = LeerParametro(cgFwWebOlap)
    End Function

    Public Function EstadoOT() As String
        EstadoOT = LeerParametro(cgFwEstadoOT)
    End Function

    Public Function PrioridadOT() As Integer
        PrioridadOT = CInt(LeerParametro(cgFwPrioridadOTs))
    End Function

    Public Function WorkSpaceGestionDoc() As String
        WorkSpaceGestionDoc = LeerParametro(cgFwWorkSpaceGestionDoc)
    End Function

    Public Function ServidorGestionDoc() As String
        ServidorGestionDoc = LeerParametro(cgFwServidorGestionDoc)
    End Function

    Public Function TipoProvContrata() As String
        TipoProvContrata = LeerParametro(cgFwTipoProvContrata)
    End Function

    Public Function Pais() As String
        Pais = LeerParametro(cgFwPais)
    End Function

    Public Function IVACliente() As Boolean
        IVACliente = LeerParametro(cgFwIvaCliente) = "1"
    End Function

    Public Function IVAProveedor() As Boolean
        IVAProveedor = LeerParametro(cgFwIvaProveedor) = "1"
    End Function

    Public Function TipoAsientoRemesa() As Integer
        TipoAsientoRemesa = CInt(LeerParametro(cgFwTipoAsientoRemesa))
    End Function

    Public Function CtaEfectGestonCobros() As String
        CtaEfectGestonCobros = LeerParametro(cgFwCtaEfectGstonCobros)
    End Function
    Public Function CtaEfectos() As String
        CtaEfectos = LeerParametro(cgFwCtaEfect)
    End Function
    Public Function CtaDevolucionIvaFra() As String
        CtaDevolucionIvaFra = LeerParametro(cgFwCtaDevolucionIvaFra)
    End Function

    Public Function CtaDevolucionGasto() As String
        CtaDevolucionGasto = LeerParametro(cgFwCtaDevolucionGasto)
    End Function

    Public Function CtaDevolucionComision() As String
        CtaDevolucionComision = LeerParametro(cgFwCtaDevolucionComision)
    End Function

    Public Function CtaIngesoFinanciero() As String
        CtaIngesoFinanciero = LeerParametro(cgFwCtaIngesoFinanciero)
    End Function

    Public Function ClaseBalancePersonalizado() As String
        ClaseBalancePersonalizado = LeerParametro(CLASE_BALANCE_PERSONALIZADO) & String.Empty
    End Function

    Public Function EnsambladoBalancePersonalizado() As String
        EnsambladoBalancePersonalizado = LeerParametro(ENSAMBLADO_BALANCE_PERSONALIZADO) & String.Empty
    End Function

    Public Function PersonalizarBalances() As Boolean
        PersonalizarBalances = LeerParametro(PERSONALIZAR_BALANCES)
    End Function

    Public Function VariosProy() As String
        VariosProy = LeerParametro(cgFwVariosProy)
    End Function

    Public Function RutaDefecto() As String
        RutaDefecto = LeerParametro(cgRutaDefecto)
    End Function

    Public Function EstrucDefecto() As String
        EstrucDefecto = LeerParametro(cgEstructuraDefecto)
    End Function

#Region "Parámetros Pagos Agrupados "

    Public Function ConfiguracionAgrupacionPagos() As DataTable
        Dim f As New Filter
        f.UnionOperator = FilterUnionOperator.Or
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwPagosBancoProv))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwPagosCambioA))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwPagosCambioB))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwPagosProveedor))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwPagosFPago))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwPagosFechaV))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwPagosMoneda))

        Dim dt As DataTable = Filter(f)

        If Not dt Is Nothing AndAlso dt.Rows.Count < f.Count Then
            Dim strParamArray As String = "<" & cgFwPagosBancoProv & "> - <" & cgFwPagosCambioA & "> - <" & cgFwPagosCambioB & "> - <" & cgFwPagosProveedor & "> - <" & cgFwPagosFechaV & "> - <" & cgFwPagosMoneda & "> - <" & cgFwPagosFPago & ">"
            ApplicationService.GenerateError("Alguno de los siguientes parámetros | no existe.", strParamArray)
        End If

        Return dt
    End Function

    Public Function ConfiguracionAgrupacionPagos(ByVal dtPagos As DataTable, _
                                                 ByRef strParametrosActivos As String) As Filter

        ConfiguracionAgrupacionPagos(strParametrosActivos)

        Dim blnProveedorBanco As Boolean = InStr(strParametrosActivos, "IDProveedorBanco", CompareMethod.Text) <> 0
        Dim blnCambioA As Boolean = InStr(strParametrosActivos, "CambioA", CompareMethod.Text) <> 0
        Dim blnCambioB As Boolean = InStr(strParametrosActivos, "CambioB", CompareMethod.Text) <> 0
        Dim blnProveedor As Boolean = InStr(strParametrosActivos, "IDProveedor", CompareMethod.Text) <> 0
        Dim blnFPago As Boolean = InStr(strParametrosActivos, "IDFormaPago", CompareMethod.Text) <> 0
        Dim blnFecha As Boolean = InStr(strParametrosActivos, "FechaVencimiento", CompareMethod.Text) <> 0
        Dim blnMoneda As Boolean = InStr(strParametrosActivos, "IDMoneda", CompareMethod.Text) <> 0

        Dim oWhere As New Filter
        oWhere.UnionOperator = FilterUnionOperator.Or

        Dim c As BusinessHelper = BusinessHelper.CreateBusinessObject("Pago")
        For Each drPagos As DataRow In dtPagos.Rows
            Dim f As New Filter
            Dim dtPago As DataTable = c.SelOnPrimaryKey(drPagos("IDPago"))
            If Not dtPago Is Nothing AndAlso dtPago.Rows.Count > 0 Then
                If blnProveedorBanco Then
                    If Length(dtPago.Rows(0)("IDProveedorBanco")) > 0 Then
                        f.Add(New NumberFilterItem("IDProveedorBanco", FilterOperator.Equal, dtPago.Rows(0)("IDProveedorBanco")))
                    End If
                End If
                If blnCambioA Then
                    If Length(dtPago.Rows(0)("CambioA")) > 0 Then
                        f.Add(New NumberFilterItem("CambioA", FilterOperator.Equal, dtPago.Rows(0)("CambioA")))
                    End If
                End If

                If blnCambioB Then
                    If Length(dtPago.Rows(0)("CambioB")) > 0 Then
                        f.Add(New NumberFilterItem("CambioB", FilterOperator.Equal, dtPago.Rows(0)("CambioB")))
                    End If
                End If
                If blnProveedor Then
                    If Length(dtPago.Rows(0)("IDProveedor")) > 0 Then
                        f.Add(New StringFilterItem("IDProveedor", FilterOperator.Equal, dtPago.Rows(0)("IDProveedor")))
                    End If
                End If
                If blnFPago Then
                    If Length(dtPago.Rows(0)("IDFormaPago")) > 0 Then
                        f.Add(New StringFilterItem("IDFormaPago", FilterOperator.Equal, dtPago.Rows(0)("IDFormaPago")))
                    End If
                End If
                If blnFecha Then
                    If Length(dtPago.Rows(0)("FechaVencimiento")) > 0 Then
                        f.Add(New DateFilterItem("FechaVencimiento", FilterOperator.Equal, dtPago.Rows(0)("FechaVencimiento")))
                    End If
                End If
                If blnMoneda Then
                    If Length(dtPago.Rows(0)("IDMoneda")) > 0 Then
                        f.Add(New StringFilterItem("IDMoneda", FilterOperator.Equal, dtPago.Rows(0)("IDMoneda")))
                    End If
                End If
            End If
            oWhere.Add(f)
        Next

        Return oWhere
    End Function

    Public Sub ConfiguracionAgrupacionPagos(ByRef strGroupBy As String)
        Dim dt As DataTable = ConfiguracionAgrupacionPagos()
        For Each dr As DataRow In dt.Rows
            If dr("Valor") = 1 Then
                If Length(strGroupBy) > 0 Then strGroupBy = strGroupBy & ","
                If dr("IDParametro") = cgFwPagosBancoProv Then
                    strGroupBy = strGroupBy & "IDProveedorBanco"
                ElseIf dr("IDParametro") = cgFwPagosCambioA Then
                    strGroupBy = strGroupBy & "CambioA"
                ElseIf dr("IDParametro") = cgFwPagosCambioB Then
                    strGroupBy = strGroupBy & "CambioB"
                ElseIf dr("IDParametro") = cgFwPagosProveedor Then
                    strGroupBy = strGroupBy & "IDProveedor"
                ElseIf dr("IDParametro") = cgFwPagosFPago Then
                    strGroupBy = strGroupBy & "IDFormaPago"
                ElseIf dr("IDParametro") = cgFwPagosFechaV Then
                    strGroupBy = strGroupBy & "FechaVencimiento"
                ElseIf dr("IDParametro") = cgFwPagosMoneda Then
                    strGroupBy = strGroupBy & "IDMoneda"
                End If
            End If
        Next
    End Sub
#End Region

#Region "Parámetros Cobros Agrupados "
    Public Function ConfiguracionAgrupacionCobros() As DataTable

        Dim f As New Filter
        f.UnionOperator = FilterUnionOperator.Or
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwCobrosBancoCli))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwCobrosCambioA))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwCobrosCambioB))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwCobrosCliente))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwCobrosDirEnv))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwCobrosFPago))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwCobrosFechaV))
        f.Add(New StringFilterItem("IdParametro", FilterOperator.Equal, cgFwCobrosMoneda))

        Dim dt As DataTable = Filter(f)

        If Not dt Is Nothing AndAlso dt.Rows.Count < f.Count Then
            Dim strParamArray As String = "<" & cgFwCobrosBancoCli & "> - <" & cgFwCobrosCambioA & "> - <" & cgFwCobrosCambioB & "> - <" & cgFwCobrosCliente & "> - <" & cgFwCobrosDirEnv & "> - <" & cgFwCobrosFPago & "> - <" & cgFwCobrosFechaV & "> - <" & cgFwCobrosMoneda & ">"
            ApplicationService.GenerateError("Alguno de los siguientes parámetros | no existe.", strParamArray)
        End If

        Return dt
    End Function

    Public Function ConfiguracionAgrupacionCobros(ByVal dtCobros As DataTable, _
                                                  ByRef strParametrosActivos As String) As Filter

        ConfiguracionAgrupacionCobros(strParametrosActivos)

        Dim blnClienteBanco As Boolean = InStr(strParametrosActivos, "IDClienteBanco", CompareMethod.Text) <> 0
        Dim blnCambioA As Boolean = InStr(strParametrosActivos, "CambioA", CompareMethod.Text) <> 0
        Dim blnCambioB As Boolean = InStr(strParametrosActivos, "CambioB", CompareMethod.Text) <> 0
        Dim blnCliente As Boolean = InStr(strParametrosActivos, "IDCliente", CompareMethod.Text) <> 0
        Dim blnDireccion As Boolean = InStr(strParametrosActivos, "IDDireccion", CompareMethod.Text) <> 0
        Dim blnFPago As Boolean = InStr(strParametrosActivos, "IDFormaPago", CompareMethod.Text) <> 0
        Dim blnFecha As Boolean = InStr(strParametrosActivos, "FechaVencimiento", CompareMethod.Text) <> 0
        Dim blnMoneda As Boolean = InStr(strParametrosActivos, "IDMoneda", CompareMethod.Text) <> 0

        Dim oWhere As New Filter
        oWhere.UnionOperator = FilterUnionOperator.Or

        Dim c As BusinessHelper = BusinessHelper.CreateBusinessObject("Cobro")
        For Each drCobros As DataRow In dtCobros.Rows
            Dim f As New Filter
            f.Add(New BooleanFilterItem("Especial", FilterOperator.Equal, drCobros("Especial")))
            Dim dtCobro As DataTable = c.SelOnPrimaryKey(drCobros("IDCobro"))
            If Not dtCobro Is Nothing AndAlso dtCobro.Rows.Count > 0 Then
                If blnClienteBanco Then
                    If Length(dtCobro.Rows(0)("IDClienteBanco")) > 0 Then
                        f.Add(New NumberFilterItem("IDClienteBanco", FilterOperator.Equal, dtCobro.Rows(0)("IDClienteBanco")))
                    End If
                End If
                If blnCambioA Then
                    If Length(dtCobro.Rows(0)("CambioA")) > 0 Then
                        f.Add(New NumberFilterItem("CambioA", FilterOperator.Equal, dtCobro.Rows(0)("CambioA")))
                    End If
                End If
                If blnCambioB Then
                    If Length(dtCobro.Rows(0)("CambioB")) > 0 Then
                        f.Add(New NumberFilterItem("CambioB", FilterOperator.Equal, dtCobro.Rows(0)("CambioB")))
                    End If
                End If
                If blnCliente Then
                    If Length(dtCobro.Rows(0)("IDCliente")) > 0 Then
                        f.Add(New StringFilterItem("IDCliente", FilterOperator.Equal, dtCobro.Rows(0)("IDCliente")))
                    End If
                End If
                If blnDireccion Then
                    If Length(dtCobro.Rows(0)("IDDireccion")) > 0 Then
                        f.Add(New NumberFilterItem("IDDireccion", FilterOperator.Equal, dtCobro.Rows(0)("IDDireccion")))
                    End If
                End If
                If blnFPago Then
                    If Length(dtCobro.Rows(0)("IDFormaPago")) > 0 Then
                        f.Add(New StringFilterItem("IDFormaPago", FilterOperator.Equal, dtCobro.Rows(0)("IDFormaPago")))
                    End If
                End If
                If blnFecha Then
                    If Length(dtCobro.Rows(0)("FechaVencimiento")) > 0 Then
                        f.Add(New DateFilterItem("FechaVencimiento", FilterOperator.Equal, dtCobro.Rows(0)("FechaVencimiento")))
                    End If
                End If
                If blnMoneda Then
                    If Length(dtCobro.Rows(0)("IDMoneda")) > 0 Then
                        f.Add(New StringFilterItem("IDMoneda", FilterOperator.Equal, dtCobro.Rows(0)("IDMoneda")))
                    End If
                End If
            End If
            oWhere.Add(f)
        Next

        Return oWhere
    End Function

    Public Sub ConfiguracionAgrupacionCobros(ByRef strGroupBy As String)
        Dim dt As DataTable = ConfiguracionAgrupacionCobros()

        For Each dr As DataRow In dt.Rows
            If dr("Valor") = 1 Then
                If Length(strGroupBy) > 0 Then strGroupBy = strGroupBy & ","
                If dr("IDParametro") = cgFwCobrosBancoCli Then
                    strGroupBy = strGroupBy & "IDClienteBanco"
                ElseIf dr("IDParametro") = cgFwCobrosCambioA Then
                    strGroupBy = strGroupBy & "CambioA"
                ElseIf dr("IDParametro") = cgFwCobrosCambioB Then
                    strGroupBy = strGroupBy & "CambioB"
                ElseIf dr("IDParametro") = cgFwCobrosCliente Then
                    strGroupBy = strGroupBy & "IDCliente"
                ElseIf dr("IDParametro") = cgFwCobrosDirEnv Then
                    strGroupBy = strGroupBy & "IDDireccion"
                ElseIf dr("IDParametro") = cgFwCobrosFPago Then
                    strGroupBy = strGroupBy & "IDFormaPago"
                ElseIf dr("IDParametro") = cgFwCobrosFechaV Then
                    strGroupBy = strGroupBy & "FechaVencimiento"
                ElseIf dr("IDParametro") = cgFwCobrosMoneda Then
                    strGroupBy = strGroupBy & "IDMoneda"
                End If
            End If
        Next
    End Sub
#End Region

    Public Function MonedaInternaA() As String
        Return LeerParametro(cgFwMonInterAPre)
    End Function

    Public Function MonedaInternaB() As String
        Return LeerParametro(cgFwMonInterBPre)
    End Function

    Public Function MonedasInternasPredeterminadas() As MonedasInternas
        Dim m As New MonedasInternas
        m.strMonedaA = LeerParametro(cgFwMonInterAPre)
        m.strMonedaB = LeerParametro(cgFwMonInterBPre)

        Return m
    End Function

    Public Function TarifaPredeterminada() As String
        TarifaPredeterminada = LeerParametro(cgFwTarifaPre)
    End Function

    Public Function ContadorHistMovimientoPredeterminado() As String
        ContadorHistMovimientoPredeterminado = LeerParametro(cgFwContHistMov)
    End Function

    Public Function ContadorAutofactura() As String
        ContadorAutofactura = LeerParametro(cgFwContAuto)
    End Function

    Public Function ContadorObservaciones() As String
        Return LeerParametro(cgFwContObservaciones)
    End Function

    Public Function EmpresaRecargoEquivPredeterminado() As Boolean
        EmpresaRecargoEquivPredeterminado = LeerParametro(cgFwEmpRE) = "1"
    End Function

    Public Function BC3MaterialPredeterminado() As String
        BC3MaterialPredeterminado = LeerParametro(cgBC3Material)
    End Function

    Public Function BC3MODPredeterminado() As String
        BC3MODPredeterminado = LeerParametro(cgBC3Mod)
    End Function

    Public Function BC3CentroPredeterminado() As String
        BC3CentroPredeterminado = LeerParametro(cgBC3Centro)
    End Function

    Public Function BC3VariosPredeterminado() As String
        BC3VariosPredeterminado = LeerParametro(cgBC3vaRIOS)
    End Function

    Public Function ActAutomaticaArticuloMnto() As Boolean
        If LeerParametro(cgFwActAutoArtMNTO) = "1" Then
            ActAutomaticaArticuloMnto = True
        Else
            ActAutomaticaArticuloMnto = False
        End If
    End Function

    Public Function ActAutomaticaAlbVenta() As Boolean
        If LeerParametro(cgFwActAutoAV) = "1" Then
            ActAutomaticaAlbVenta = True
        Else
            ActAutomaticaAlbVenta = False
        End If
    End Function

    Public Function PermisoAlbaran() As Boolean
        If LeerParametro(cgFwPedPreparado) = "1" Then
            PermisoAlbaran = True
        Else
            PermisoAlbaran = False
        End If
    End Function

    Public Function PermisoExpedicion() As Boolean
        If LeerParametro(cgFwPedAviso) = "1" Then
            PermisoExpedicion = True
        Else
            PermisoExpedicion = False
        End If
    End Function

    Public Function ActAutomaticaAlbCompra() As Boolean
        If LeerParametro(cgFwActAutoAC) = "1" Then
            ActAutomaticaAlbCompra = True
        Else
            ActAutomaticaAlbCompra = False
        End If
    End Function

    Public Function ActAutomaticaSituacionRemCobro() As Boolean
        If LeerParametro(cgFwActAutoSitRemC) = "1" Then
            ActAutomaticaSituacionRemCobro = True
        Else
            ActAutomaticaSituacionRemCobro = False
        End If
    End Function

    Public Function AlmacenPredeterminado() As String
        AlmacenPredeterminado = LeerParametro(cgFwAlmacenPre)
    End Function

    Public Function ObtenerInmovAuto() As Integer
        Dim strValor As String

        strValor = LeerParametro(cgFwInmovAuto)
        If Len(strValor) = 0 Then
            ObtenerInmovAuto = 0
        Else
            ObtenerInmovAuto = CInt(strValor)
        End If
    End Function

    Public Function CGestionPredet() As String
        CGestionPredet = LeerParametro(cgFwCGestPre)
    End Function

    Public Function CCostePredet() As String
        CCostePredet = LeerParametro(cgCCostePre)
    End Function

    Public Function CAnaliticAsig() As Integer
        CAnaliticAsig = CInt(LeerParametro(cgFwCAnalitAsig))
    End Function

    Public Function CTipoCobroFV() As Integer
        CTipoCobroFV = CInt(LeerParametro(cgFwCTipoFV))
    End Function

    Public Function CTipoCobroFVB() As Integer
        CTipoCobroFVB = CInt(LeerParametro(cgFwCTipoFVB))
    End Function

    Public Function CTipoPagoFCB() As Integer
        CTipoPagoFCB = CInt(LeerParametro(cgFwCTipoFCB))
    End Function

    Public Function CTipoPagoFC() As Integer
        CTipoPagoFC = CInt(LeerParametro(cgFwCTipoFC))
    End Function

    Public Function CTipoPagoDesdeCobro() As Integer
        CTipoPagoDesdeCobro = CInt(LeerParametro(cgFwCTipoPC))
    End Function

    Public Function CTipoCobroDesdePago() As Integer
        CTipoCobroDesdePago = CInt(LeerParametro(cgFwCTipoCP))
    End Function

    Public Function CAnaliticTipo() As Integer
        CAnaliticTipo = CInt(LeerParametro(cgFwCAnalitTipo))
    End Function

    Public Function CAnaliticOrigen() As Boolean
        CAnaliticOrigen = CBool(LeerParametro(cgFwCAnalitOrig))
    End Function

    Public Function CAnaliticPredet() As Boolean
        CAnaliticPredet = CBool(LeerParametro(cgFwCAnalitic))
    End Function

    Public Function CAnaliticGestion() As Boolean
        CAnaliticGestion = CBool(LeerParametro(cgFwCAnaliticGes))
    End Function

    Public Function CCArticulo() As String
        CCArticulo = LeerParametro(cgFwPrefCCAr)
    End Function

    Public Function CCBanco() As String
        CCBanco = LeerParametro(cgFwPrefCCBa)
    End Function

    Public Function CCProveedor() As String
        CCProveedor = LeerParametro(cgFwPrefCCPr)
    End Function

    Public Function CCProveedorGrupo() As String
        CCProveedorGrupo = LeerParametro(cgFwPrefCCPrGr)
    End Function

    Public Function CCCliente() As String
        CCCliente = LeerParametro(cgFwPrefCCCl)
    End Function

    Public Function CCClienteGrupo() As String
        CCClienteGrupo = LeerParametro(cgFwPrefCCClGr)
    End Function

    Public Function CCEfectosCliente() As String
        CCEfectosCliente = LeerParametro(cgFwPrefCCECl)
    End Function

    Public Function CCEfectosCartera() As String
        CCEfectosCartera = LeerParametro(cgFwPrefCCECA)
    End Function

    Public Function CCEfectosGestionCobros() As String
        CCEfectosGestionCobros = LeerParametro(cgFwPrefCCEGC)
    End Function

    Public Function CCEfectosProveedor() As String
        CCEfectosProveedor = LeerParametro(cgFwPrefCCEfPr)
    End Function

    Public Function CCClienteAnticipo() As String
        CCClienteAnticipo = LeerParametro(cgFwPrefCCClAnticipo)
    End Function

    Public Function ValoracionAlmacen() As Boolean
        ValoracionAlmacen = LeerParametro(cgValoracionAlm)
    End Function

    Public Function ObtenerPredeterminado(ByVal strIdParametro As String) As String
        ObtenerPredeterminado = LeerParametro(strIdParametro)
    End Function

    Public Function PagoDescontSituacion() As Integer
        Dim strValor As String

        strValor = LeerParametro(cgFwPagoDescontSituacion)
        If Len(strValor) = 0 Then
            PagoDescontSituacion = 0
        Else
            PagoDescontSituacion = CInt(strValor)
        End If
    End Function

    Public Function CobroDescontSituacion() As Integer
        Dim strValor As String

        strValor = LeerParametro(cgFwCobroDescontSituacion)
        If Len(strValor) = 0 Then
            CobroDescontSituacion = enumCobroSituacion.NoNegociado
        Else
            CobroDescontSituacion = CInt(strValor)
        End If

    End Function

    Public Function PagoContSituacion() As Integer
        Dim strValor As String

        strValor = LeerParametro(cgFwPagoContSituacion)
        If Len(strValor) = 0 Then
            PagoContSituacion = 0
        Else
            PagoContSituacion = CInt(strValor)
        End If

    End Function

    Public Function CobroContSituacion() As Integer
        Dim strValor As String

        strValor = LeerParametro(cgFwCobroContSituacion)
        If Len(strValor) = 0 Then
            CobroContSituacion = enumCobroSituacion.Cobrado
        Else
            CobroContSituacion = CInt(strValor)
        End If
    End Function

    <Serializable()> _
    Public Class StDiasSeg
        Public DiasSeg1 As String
        Public DiasSeg2 As String
        Public DiasSeg3 As String
    End Class

    <Serializable()> _
    Public Class StCambiosAut
        Public CambioAut1 As Boolean
        Public CambioAut2 As Boolean
        Public CambioAut3 As Boolean
    End Class

    Public Function DiasSeguridad() As StDiasSeg
        Dim DiasSeg As New StDiasSeg
        DiasSeg.DiasSeg1 = LeerParametro(cgFwCobroDiasSeg1)
        DiasSeg.DiasSeg2 = LeerParametro(cgFwCobroDiasSeg2)
        DiasSeg.DiasSeg3 = LeerParametro(cgFwCobroDiasSeg3)
        Return DiasSeg
    End Function

    Public Function CambiosAutomaticos() As StCambiosAut
        Dim CambiosAut As New StCambiosAut
        CambiosAut.CambioAut1 = CBool(LeerParametro(cgFwCobroCambioAuto1))
        CambiosAut.CambioAut2 = CBool(LeerParametro(cgFwCobroCambioAuto2))
        CambiosAut.CambioAut3 = CBool(LeerParametro(cgFwCobroCambioAuto3))
        Return CambiosAut
    End Function

    Public Function Contabilidad() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey(cgFwContabilidad)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else
            Return False
        End If
    End Function

    Public Function Logistica() As Boolean
        Return CBool(LeerParametro(cgLogistica))
    End Function

    Public Function ParametroGasto() As String
        ParametroGasto = LeerParametro(cgFwAgrupacionGasto)
    End Function

    Public Function ParametroIngreso() As String
        ParametroIngreso = LeerParametro(cgFwAgrupacionIngreso)
    End Function

    Public Function ActivoPredeterminado() As String
        ActivoPredeterminado = LeerParametro(cgFwActivoPre)
    End Function

    Public Function ArticuloFacturacionProyectos() As String
        Dim strArticulo As String = LeerParametro(cgFwArtFactVtosProy)
        If Len(strArticulo) > 0 Then
            Dim a As BusinessHelper = BusinessHelper.CreateBusinessObject("Articulo")
            Dim dt As DataTable = a.SelOnPrimaryKey(strArticulo)
            If IsNothing(dt) OrElse dt.Rows.Count = 0 Then
                'El Artículo genérico | no está dado de alta en Artículos. Debe darlo darlo de alta o cambiar el valor del parámetro.
                ApplicationService.GenerateError("El Artículo genérico no está dado de alta en Artículos. Debe darlo darlo de alta o cambiar el valor del parámetro.", strArticulo)

            End If
        Else
            ApplicationService.GenerateError("Falta asignar un Artículo Predeterminado.")
        End If

        Return strArticulo
    End Function

    Public Function ConceptoGastosProyectos() As DataTable
        Dim dt As DataTable = Nothing

        Dim strIDGasto As String = LeerParametro(cgFwGastosProy)
        If Len(strIDGasto) > 0 Then
            Dim Gastos As BusinessHelper
            Gastos = BusinessHelper.CreateBusinessObject(AdminData.GetEntityInfo("Gasto"))
            dt = Gastos.Filter(New StringFilterItem("IDGasto", strIDGasto))
            If IsNothing(dt) OrElse dt.Rows.Count = 0 Then
                'El Gasto genérico | no está dado de alta en Gastos. Debe darlo darlo de alta o cambiar el valor del parámetro.
                ApplicationService.GenerateError("El Gasto genérico | no está dado de alta en tabla de Gastos. Debe darlo darlo de alta o cambiar el valor del parámetro.", strIDGasto)

            End If
        Else
            ApplicationService.GenerateError("Falta asignar un Gasto Predeterminado.")
        End If

        Return dt
    End Function

    Public Function ConceptoVariosProyectos() As DataTable
        Dim dt As DataTable = Nothing

        Dim strIDVarios As String = LeerParametro(cgFwVariosProy)
        If Len(strIDVarios) > 0 Then
            Dim v As BusinessHelper = BusinessHelper.CreateBusinessObject("Varios")
            dt = v.SelOnPrimaryKey(strIDVarios)
            If IsNothing(dt) OrElse dt.Rows.Count = 0 Then
                'El Varios genérico | no está dado de alta en la tabla Varios. Debe darlo darlo de alta o cambiar el valor del parámetro.
                ApplicationService.GenerateError("El Varios genérico | no está dado de alta en la tabla de Varios. Debe darlo darlo de alta o cambiar el valor del parámetro.", strIDVarios)

            End If
        Else
            ApplicationService.GenerateError("Falta asignar un Varios Predeterminado.")
        End If

        Return dt
    End Function

    Public Function ControlProduccion() As String
        ControlProduccion = LeerParametro(cgFwControlProd)
    End Function

    Public Function FormaPagoEnEfectivo() As String
        FormaPagoEnEfectivo = LeerParametro(cgFwFPEfectivo)
    End Function

    Public Function CondicionPagoEnEfectivo() As String
        CondicionPagoEnEfectivo = LeerParametro(cgFwCPEfectivo)
    End Function

    Public Function FormaPagoTarjeta() As String
        FormaPagoTarjeta = LeerParametro(cgFwFPTarjeta)
    End Function

    Public Function CajaInicial() As Integer
        Dim strValor As String

        strValor = CStr(CInt(LeerParametro(cgFwCajaInicial)))
        If Len(strValor) = 0 Then
            CajaInicial = 0
        Else
            CajaInicial = CInt(strValor)
        End If
    End Function

    Public Function ArticuloConstructora() As String
        ArticuloConstructora = LeerParametro(cgFwArticuloCons)
    End Function

    Public Function ArticuloFacturacionContrata() As String
        ArticuloFacturacionContrata = LeerParametro(cgFwArtFactCont)
    End Function

    Public Function ArticuloFacturacionMOD() As String
        ArticuloFacturacionMOD = LeerParametro(cgFwArtFactMod)
    End Function

    Public Function ArticuloFacturacionGastos() As String
        ArticuloFacturacionGastos = LeerParametro(cgFwArtFactGastos)
    End Function

    Public Function ArticuloRetencion() As String
        ArticuloRetencion = LeerParametro(cgFwArticuloReten)
    End Function

    Public Function ClientePredeterminado() As String
        ClientePredeterminado = LeerParametro(cgFwClientePre)
    End Function

    Public Function ClienteAutofactura() As String
        ClienteAutofactura = LeerParametro(cgFwClienteAuto)
    End Function

    Public Function PuntosPredeterminados() As String
        Dim strImporte As String
        Dim strMoneda As String

        strImporte = LeerParametro(cgFwPuntosImporte)
        strMoneda = LeerParametro(cgFwPuntosMoneda)
        PuntosPredeterminados = "Importe=" & strImporte & ";Moneda=" & strMoneda
    End Function

    Public Function PecPredeterminado() As String
        PecPredeterminado = LeerParametro(cgFwPec)
    End Function

    Public Function LoteMinimoPredeterminado() As String
        LoteMinimoPredeterminado = LeerParametro(cgFwLoteMinimo)
    End Function

    Public Function RecalcularPrecioGeneracionPedido() As Boolean
        RecalcularPrecioGeneracionPedido = LeerParametro(cgFwRecalcularPedido) = "1"
    End Function

    Public Function HoraPred() As String
        HoraPred = LeerParametro(cgFwHora)
    End Function

    Public Function HoraExtra() As String
        Dim dt As DataTable = SelOnPrimaryKey("HORAEXTRA")
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0)("Valor")
        Else
            Return String.Empty
        End If
    End Function

    Public Function CierreAutomaticoOF() As Boolean
        CierreAutomaticoOF = CBool(LeerParametro(cgFwCierreAutoOrden))
    End Function

    Public Function TipoCodificacionAutomatica() As Integer
        Dim strValor As String

        strValor = LeerParametro(cgFwCodAutomatica)
        If Len(strValor) = 0 Then
            TipoCodificacionAutomatica = 0
        Else
            TipoCodificacionAutomatica = CInt(strValor)
        End If
    End Function

    Public Function UdMedidaPred() As String
        UdMedidaPred = LeerParametro(cgFwUdMedidaPred)
    End Function

    Public Function PromocionPredeterminada() As String
        PromocionPredeterminada = LeerParametro(cgFwPromocionPre)
    End Function

    Public Function GPromociones() As Boolean
        GPromociones = LeerParametro(cgFwGPromociones) = "1"
    End Function

    Public Function UdTiempoPred() As Integer
        UdTiempoPred = LeerParametro(cgFwUdTiempo)
    End Function

    Public Function ProveedorRetencion() As String
        ProveedorRetencion = LeerParametro(cgFwProvRetencion)
    End Function

    Public Function PrecioMovimientoCero() As Boolean
        PrecioMovimientoCero = CBool(LeerParametro(cgFwPrecioMov))
    End Function

    Public Function PathFoto() As String
        PathFoto = LeerParametro(cgFwPathFoto)
    End Function

    Public Function CCAbonoPropiertario() As String
        CCAbonoPropiertario = LeerParametro(cgFwCCABonoProp)
    End Function

    Public Function CCCargoCentro() As String
        CCCargoCentro = LeerParametro(cgFwCCCargoCentro)
    End Function

    Public Function CCFCompra() As String
        CCFCompra = LeerParametro(cgFwCCFCompra)
    End Function

    Public Function GestionStockNegativo() As Boolean
        GestionStockNegativo = CBool(LeerParametro(cgFwGestionStockNegativo))
    End Function

    Public Function GestionDobleUnidad() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey(cgFwGestionDobleUnidad)
        If dt.Rows.Count > 0 Then
            GestionDobleUnidad = CBool(dt.Rows(0)("Valor") & String.Empty)
        End If
    End Function

    Public Function RecalcularValoracion() As Integer
        Dim strValor As String

        strValor = LeerParametro(cgFwRecalcularValoracion)
        If Len(strValor) > 0 Then
            RecalcularValoracion = CInt(strValor)
        Else
            RecalcularValoracion = enumtaValoracionSalidas.taNoRecalcular
        End If
    End Function

    Public Function FormaEnvio() As String
        FormaEnvio = LeerParametro(cgFwFormaEnvio)
    End Function

    Public Function CondicionEnvio() As String
        CondicionEnvio = LeerParametro(cgFwCondicionEnvio)
    End Function

#Region " UbicacionNoDefinida "

    'Public Function UbicacionNoDefinida() As String
    '    UbicacionNoDefinida = LeerParametro(cgFwIDUbicacionNoDefinida)
    'End Function

    <Serializable()> _
    Public Class infoUbicacion
        Public IDUbicacion As String
        Public DescUbicacion As String
    End Class
    Public Function UbicacionNoDefinida() As infoUbicacion
        Dim info As New infoUbicacion
        Dim parametro As DataRow = Me.GetItemRow(cgFwIDUbicacionNoDefinida)
        info.IDUbicacion = parametro("Valor")
        info.DescUbicacion = parametro("DescParametro")

        Return info
    End Function

#End Region

    Public Function GenerarPedidosEnTransferencias() As Boolean
        GenerarPedidosEnTransferencias = CBool(LeerParametro(cgFwPedidoEnTransferencia))
    End Function

    Public Function FacturacionFechaAlbaran() As Integer
        Dim strValor As String

        strValor = LeerParametro(cgFwFechaAlbaran)
        If Len(strValor) = 0 Then
            FacturacionFechaAlbaran = 0
        Else
            FacturacionFechaAlbaran = CInt(strValor)
        End If
    End Function

    Public Function CierreInventario() As Boolean
        CierreInventario = CBool(LeerParametro(cgFwCierreInventario))
    End Function

    Public Function FormaPago() As String
        FormaPago = LeerParametro(cgFwFormaPago)
        If Length(FormaPago) > 0 Then
            Dim c As BusinessHelper = BusinessHelper.CreateBusinessObject("FormaPago")
            Dim dt As DataTable = c.SelOnPrimaryKey(FormaPago)
            If Not dt Is Nothing Then
                If dt.Rows.Count = 0 Then ApplicationService.GenerateError("La FormaPago Predeterminada | no está dada de alta en Forma de Pago. Debe darla de alta o cambiar el valor del parámetro.", FormaPago)
            Else
                ApplicationService.GenerateError("La FormaPago Predeterminada | no está dada de alta en Forma de Pago. Debe darla de alta o cambiar el valor del parámetro.", FormaPago)
            End If
        Else
            ApplicationService.GenerateError("Falta asignar una Forma Pago Predeterminada.")
        End If
    End Function

    Public Function CondicionPago() As String
        CondicionPago = LeerParametro(cgFwCondicionPago)
        If Length(CondicionPago) > 0 Then
            Dim c As BusinessHelper = BusinessHelper.CreateBusinessObject("CondicionPago")
            Dim dt As DataTable = c.SelOnPrimaryKey(CondicionPago)
            If Not dt Is Nothing Then
                If dt.Rows.Count = 0 Then ApplicationService.GenerateError("La Condición de Pago Predeterminada | no está dado de alta en Condición de Pago. Debe darla de alta o cambiar el valor del parámetro.", "CondicionPago")
            Else
                ApplicationService.GenerateError("La Condición de Pago Predeterminada | no está dado de alta en Condición de Pago. Debe darla de alta o cambiar el valor del parámetro.", "CondicionPago")
            End If
        Else
            ApplicationService.GenerateError("Falta asignar una Condicion de Pago Predeterminada.")
        End If
    End Function

    Public Function DiaPago() As String
        DiaPago = LeerParametro(cgFwDiaPago)
        If Length(DiaPago) > 0 Then
            Dim c As BusinessHelper = BusinessHelper.CreateBusinessObject("DiaPago")
            Dim dt As DataTable = c.SelOnPrimaryKey(DiaPago)
            If Not dt Is Nothing Then
                If dt.Rows.Count = 0 Then ApplicationService.GenerateError("El Día de Pago Predeterminado | no está dado de alta en Dia Pago. Debe darlo de alta o cambiar el valor del parámetro.", "DiaPago")
            Else
                ApplicationService.GenerateError("El Día de Pago Predeterminado | no está dado de alta en Dia Pago. Debe darlo de alta o cambiar el valor del parámetro.", "DiaPago")
            End If
        Else
            ApplicationService.GenerateError("Falta asignar un Dia de Pago Predeterminado.")
        End If
    End Function

    Public Function MonedaPred() As String

        MonedaPred = LeerParametro(cgFwMoneda)

        If MonedaPred <> vbNullString Then

            Dim dt As DataTable = New Moneda().SelOnPrimaryKey(MonedaPred)
            If Not dt Is Nothing Then
                If dt.Rows.Count = 0 Then ApplicationService.GenerateError("La Moneda Predeterminada | no está dado de alta en Monedas. Debe darla de alta o cambiar el valor del parámetro.", "MonedaPred")

            Else
                'La Moneda Predeterminada | no está dado de alta en Monedas. Debe darla de alta o cambiar el valor del parámetro.
                ApplicationService.GenerateError("La Moneda Predeterminada | no está dado de alta en Monedas. Debe darla de alta o cambiar el valor del parámetro.", "MonedaPred")

            End If
        Else
            ApplicationService.GenerateError("Falta asignar una Moneda Predeterminada.")
        End If
    End Function

    Public Function TipoIva() As String
        TipoIva = LeerParametro(cgFwTipoIva)
        If Length(TipoIva) > 0 Then
            Dim c As BusinessHelper = BusinessHelper.CreateBusinessObject("TipoIva")
            Dim dt As DataTable = c.SelOnPrimaryKey(TipoIva)
            If Not dt Is Nothing Then
                If dt.Rows.Count = 0 Then ApplicationService.GenerateError("El Tipo Iva Predeterminado | no está dado de alta en Tipo Iva. Debe darlo de alta o cambiar el valor del parámetro.", "TipoIva")
            Else
                ApplicationService.GenerateError("El Tipo Iva Predeterminado | no está dado de alta en Tipo Iva. Debe darlo de alta o cambiar el valor del parámetro.", "TipoIva")
            End If
        Else
            ApplicationService.GenerateError("Falta asignar un Tipo Iva Predeterminado.")
        End If
    End Function

    Public Function GestionStockNegativoPorArticulo() As Boolean
        GestionStockNegativoPorArticulo = CBool(LeerParametro(cgFwGestionStockNegativoArticulo))
    End Function

    Public Function CriterioValoracionCosteStd() As Integer
        Dim intValor As Integer = Nz(LeerParametro(cgFwValoracionCosteStd), enumstdCriterioValoracion.stdPrecioEstandar)
        Return intValor
    End Function

    Public Function TipoAlbaranPorDefecto() As String
        TipoAlbaranPorDefecto = LeerParametro(cgFwAlbaranDefault)
    End Function

    Public Function TipoAlbaranServiciosPorDefecto() As String
        TipoAlbaranServiciosPorDefecto = LeerParametro(cgFwAlbaranServicioDefault)
    End Function

    Public Function TipoAlbaranDeDeposito() As String
        TipoAlbaranDeDeposito = LeerParametro(cgFwAlbaranDeDeposito)
    End Function

    Public Function TipoAlbaranRetornoAlquiler() As String
        TipoAlbaranRetornoAlquiler = LeerParametro(cgFwAlbaranRetornoAlquiler)
    End Function

    Public Function TipoAlbaranDeConsumo() As String
        TipoAlbaranDeConsumo = LeerParametro(cgFwAlbaranDeConsumo)
    End Function

    Public Function TipoAlbaranDeDevolucion() As String
        TipoAlbaranDeDevolucion = LeerParametro(cgFwAlbaranDeDevolucion)
    End Function

    Public Function TipoAlbaranDeIntercambio() As String
        Return LeerParametro(cgFwAlbaranDeIntercambio, False)
    End Function

    Public Function ControlarFechaFCProveedor() As Boolean
        ControlarFechaFCProveedor = CBool(LeerParametro(cgFwFechaFC))
    End Function

    Public Function TipoCompraNormal() As String
        TipoCompraNormal = LeerParametro(cgFwTipoCompraNormal)
    End Function

    Public Function TipoClasificacionProveedor() As String
        TipoClasificacionProveedor = LeerParametro(cgTIPOCLASIF)
    End Function

    Public Function EstadoCobroFactoringPredeterminado() As Integer
        EstadoCobroFactoringPredeterminado = LeerParametro(cgEstadoCobroFactoring)
    End Function

    Public Function TipoCobroAnticipo() As String
        TipoCobroAnticipo = LeerParametro(cgFwCTipoCA)
    End Function

    Public Function TipoCobroFianza() As String
        TipoCobroFianza = LeerParametro(cgFwCTipoCF)
    End Function

    Public Function TipoCobroRetencion() As String
        TipoCobroRetencion = LeerParametro(cgFwCTipoCR)
    End Function

    Public Function TipoPagoAnticipo() As String
        TipoPagoAnticipo = LeerParametro(cgFwPTipoPA)
    End Function

    Public Function TipoPagoFianza() As String
        TipoPagoFianza = LeerParametro(cgFwPTipoPF)
    End Function

    Public Function TipoPagoRetencion() As String
        TipoPagoRetencion = LeerParametro(cgFwPTipoPR)
    End Function

    Public Function TipoCompraSubcontratacion() As String
        TipoCompraSubcontratacion = LeerParametro(cgFwTipoCompraSubcontratacion)
    End Function

    Public Function TipoOfertaComercialPredeterminado() As String
        TipoOfertaComercialPredeterminado = LeerParametro(cgFwTipoOfertaComercialPredeterminado)
    End Function

    Public Function OperarioGenerico() As String
        OperarioGenerico = LeerParametro(cgFwOperarioGenerico)
    End Function

    Public Function ParametroBancoPropio() As String
        ParametroBancoPropio = LeerParametro(cgFwBancoPropio)
    End Function

    Public Function CalidadDemeritosRecepcion() As Integer
        CalidadDemeritosRecepcion = CInt(LeerParametro(cgFwCalControl))
    End Function

    Public Function CalidadCalificacionPorDefecto() As String
        CalidadCalificacionPorDefecto = LeerParametro(cgFwCalDefCal)
    End Function

    Public Function CalidadEstadoHomologacionPorDefecto() As String
        CalidadEstadoHomologacionPorDefecto = LeerParametro(cgFwCalDefEst)
    End Function

    Public Function CalidadLocalizacionPorDefecto() As String
        CalidadLocalizacionPorDefecto = LeerParametro(cgFwCalDefLoc)
    End Function

    Public Function CalidadDemeritoMinimo() As Integer
        CalidadDemeritoMinimo = CInt(LeerParametro(cgFwCalDem))
    End Function

    Public Function CalidadPeriodoControl() As Integer
        CalidadPeriodoControl = CInt(LeerParametro(cgFwCalPeriodo))
    End Function

    Public Function CalidadNumeroUltimasRecepciones() As Integer
        CalidadNumeroUltimasRecepciones = CInt(LeerParametro(cgFwCalNUltRecepcion))
    End Function

    Public Function EstadoActivoPorDefecto() As String
        Return LeerParametro(cgFwEstadoActivoPorDefecto)
    End Function

    Public Function ClaseActivoPorDefecto() As String
        Return LeerParametro(cgFwClaseActivoPorDefecto)
    End Function

    Public Function CategoriaActivoPorDefecto() As String
        Return LeerParametro(cgFwCategoriaActivoPorDefecto)
    End Function

    Public Function ZonaActivoPorDefecto() As String
        Return LeerParametro(cgFwZonaPorDefecto)
    End Function

    Public Function CentroCosteActivoPorDefecto() As String
        Return LeerParametro(cgFwCentroCostePorDefecto)
    End Function

    Public Function GestionNumeroSerieConActivos() As Boolean
        GestionNumeroSerieConActivos = CBool(LeerParametro(cgFwGestionSeriesConActivos))
    End Function

    Public Function HorasDiaOperario() As Double
        HorasDiaOperario = CDbl(LeerParametro(cgFwHorasDiaOperario))
    End Function

    Public Function MaxDiasConsulta() As Double
        MaxDiasConsulta = CDbl(LeerParametro(cgFwMaxDiasConsulta))
    End Function

    Public Function ArticuloSeguroAlquiler() As String
        ArticuloSeguroAlquiler = LeerParametro(cgArtSegAlq)
    End Function

    Public Function NFacturaPagoAgupado() As String
        NFacturaPagoAgupado = LeerParametro(cgAgrupPago)
    End Function

    Public Function NFacturaCobroAgupado() As String
        NFacturaCobroAgupado = LeerParametro(cgAgrupCobro)
    End Function

    Public Function AlmacendePortes() As String
        AlmacendePortes = LeerParametro(cgAlmPortes)
    End Function

    Public Function CContableMaterialAlquiler() As String
        CContableMaterialAlquiler = LeerParametro(cgCContableMAt)
    End Function

    Public Function RecuperarAlmacenAlq() As Integer
        RecuperarAlmacenAlq = CInt(LeerParametro(cgRecupAlm))
    End Function

    Public Function PorcSegurosGeneralAlq() As Double
        PorcSegurosGeneralAlq = CDbl(LeerParametro(cgPorcSeg))
    End Function

    Public Function ArticuloTransportePropio() As String
        Const BDGARTTRAN As String = "BDGARTTRAN"
        Return LeerParametro(BDGARTTRAN)
    End Function

    Public Function EmpresaGrupo() As Integer
        Return LeerParametro(cgEmpresaGrupo)
    End Function

    Public Function CCVenta() As String
        Return LeerParametro(cgCCVenta)
    End Function

    Public Function CCExport() As String
        Return LeerParametro(cgCCExport)
    End Function

    Public Function CCVentaGrupo() As String
        Return LeerParametro(cgCCVentaGrupo)
    End Function

    Public Function CCExportGrupo() As String
        Return LeerParametro(cgCCExportGrupo)
    End Function

    Public Function CCCompra() As String
        Return LeerParametro(cgCCCompra)
    End Function

    Public Function CCImportGrupo() As String
        Return LeerParametro(cgCCImportGrupo)
    End Function

    Public Function CCCompraGrupo() As String
        Return LeerParametro(cgCCCompraGrupo)
    End Function

    Public Function CCImport() As String
        Return LeerParametro(cgCCImport)
    End Function

    Public Function CCVentaRegalo() As String
        Return LeerParametro(cgCCVentaRegalo)
    End Function

    Public Function CCGastoRegalo() As String
        Return LeerParametro(cgCCGastoRegalo)
    End Function

    Public Function CCStocks() As String
        Return LeerParametro(cgCCStocks)
    End Function

    Public Function TipoInventario() As Integer
        TipoInventario = CInt(LeerParametro(cgFwTipoInventario))
    End Function

    Public Function InteresesUsarFechaValor() As Boolean
        InteresesUsarFechaValor = CBool(LeerParametro(cgBancoFrDto))
    End Function

    'Public Function ObtenerEntidades() As DataTable
    '    Return New BE.DataEngine().Filter("xEntity", "Entidad", , "Entidad", , True)
    'End Function

    <Task()> Public Shared Function ObtenerCamposEntidad(ByVal idEntidad As String, ByVal services As ServiceProvider) As DataTable
        Dim dt As New DataTable
        dt.Columns.Add(New DataColumn("CampoEntidad", GetType(String)))

        Dim dts As DataTable = AdminData.GetEntityData(idEntidad, "", , , True)
        If Not dts Is Nothing AndAlso dts.Columns.Count > 0 Then
            Dim dr As DataRow
            For Each dc As DataColumn In dts.Columns
                dr = dt.NewRow
                dr("CampoEntidad") = dc.ColumnName
                dt.Rows.Add(dr)
            Next
        End If

        Return dt

    End Function

    Public Function ObtenerEnumerados() As DataTable
        Return New BE.DataEngine().Filter("xEnumerate", "DISTINCT NombEnum", "", "NombEnum", , True)
    End Function

    Public Function ChequearParametros() As DataTable
        Dim dtPrm As DataTable = Me.Filter(New NumberFilterItem("TipoParametro", FilterOperator.Equal, enumprtTipoParametro.prtEntidad))
        Dim dtRslt As DataTable = dtPrm.Clone

        For Each oRw As DataRow In dtPrm.Rows
            Dim strVal As String = oRw("Valor") & String.Empty
            If Len(strVal) Then
                Dim strEntity As String = oRw("Entidad") & String.Empty
                If Len(strEntity) > 0 Then
                    Dim strField As String = oRw("CampoEntidad") & String.Empty
                    If Len(strField) > 0 Then
                        Try
                            Dim oBe As BusinessHelper = BusinessHelper.CreateBusinessObject(strEntity)
                            Dim dtBe As DataTable = oBe.AddNew
                            Dim ftp As FilterType = Engine.Filter.SystemTypeToFilterType(dtBe.Columns(strField).DataType)
                            Dim f As FilterItem = New FilterItem(strField, FilterOperator.Equal, strVal, ftp)
                            If oBe.Filter(f).Rows.Count = 0 Then
                                'no se ha encontrado ningún registro según el filtro que se ha creado
                                dtRslt.ImportRow(oRw)
                            End If
                        Catch
                            'errores en la creación de la entidad de negocio
                            dtRslt.ImportRow(oRw)
                        End Try
                    Else
                        'no se ha especificado un campo
                        dtRslt.ImportRow(oRw)
                    End If
                Else
                    'no se ha especificado una entidad
                    dtRslt.ImportRow(oRw)
                End If
            End If
        Next
        If dtRslt.Rows.Count > 0 Then
            Return dtRslt
        End If
    End Function

    Public Function CrearCuentasContables() As Boolean
        Return LeerParametro(cgCCCRM)
    End Function

    Public Function OperacionRecuperacionPorDefecto() As String
        Return LeerParametro(cgOpeRecDef)
    End Function

    Public Function CentroRecuperacionPorDefecto() As String
        Return LeerParametro(cgCenRecDef)
    End Function

    Public Function ContadorPagare() As String
        Return LeerParametro(cgContPagare)
    End Function

    Public Function DescontabilizarPagaresEnviados() As String
        Return LeerParametro(cgDescPagEnv)
    End Function

    Public Function PorcTasaResiduosFact() As Double
        Return LeerParametro(cgFwResiPorc)
    End Function

    Public Function ImporteLimiteTasaResiduos() As Double
        Return LeerParametro(cgFwResiLimite)
    End Function

    Public Function ArticuloTasaResiduos() As String
        Return LeerParametro(cgFwResiArtic)
    End Function

    Public Function IncrementoPrecioVentaOT() As Double
        Return LeerParametro(cgFwIncPrecVtaOt)
    End Function

    Public Function CContableDeudoraEnLiquidacionIVA() As String
        Return LeerParametro(cgCContableDeudoraLiqIVA)
    End Function

    Public Function CContableAcreedoraEnLiquidacionIVA() As String
        Return LeerParametro(cgCContableAcreedoraLiqIVA)
    End Function

    Public Function IntervienenExcesosContadoresEnCalculoSeguros() As Boolean
        Return CBool(LeerParametro(cgSeguroExceso))
    End Function

    Public Function FicheroRutaProgramaImpresion() As String
        Return LeerParametro(cgFICPATHIMP)
    End Function

    Public Function RutaFicherosHacienda() As String
        Return LeerParametro(cgFICPATHADM)
    End Function

    Public Function RutaFichero390XSD() As String
        Return LeerParametro(cgFICPATHXSD)
    End Function

    Public Function AlmacenCentroGestionActivo() As Boolean
        Return CBool(LeerParametro(cgALMCC))
    End Function

    Public Function ContadorLeasing() As String
        ContadorLeasing = LeerParametro(cgFwContLeasing)
    End Function

    Public Function PrefijoCCLeasingGastos() As String
        PrefijoCCLeasingGastos = LeerParametro(cgFwPrefLeasingGastos)
    End Function

    Public Function PrefijoCCLeasingCP() As String
        PrefijoCCLeasingCP = LeerParametro(cgFwPrefLeasingCP)
    End Function

    Public Function PrefijoCCLeasingLP() As String
        PrefijoCCLeasingLP = LeerParametro(cgFwPrefLeasingLP)
    End Function

    Public Function PrefijoCCLeasingCosteBien() As String
        PrefijoCCLeasingCosteBien = LeerParametro(cgFwPrefLeasingCoste)
    End Function

    Public Function PrefijoCCLeasingInteresesDiferidos() As String
        PrefijoCCLeasingInteresesDiferidos = LeerParametro(cgFwPrefLeasingIntereses)
    End Function

    Public Function ArticuloAlternativo() As Boolean
        ArticuloAlternativo = LeerParametro(cgFwArticuloAlternativo)
    End Function

    Public Function TrabajoTareaPredefinida() As String
        Return LeerParametro(cgFwTrabTareaPred)
    End Function

    Public Function ConfiguradorInstalado() As Boolean
        Return LeerParametro(CONFIGURADOR_INSTALADO)
    End Function

    Public Function NivelesDeAnalitica() As String
        Dim dt As DataTable = SelOnPrimaryKey(cgNivelesAnalitica)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0)("Valor") & String.Empty
        Else
            Return 0
        End If
    End Function

    Public Function AnalisiPredeterminadoVino() As String
        Return LeerParametro(cgAnalisisPreVino)
    End Function

    Public Function AnalisiPredeterminadoUVA() As String
        Return LeerParametro(cgAnalisisPreUva)
    End Function

    Public Function AnalisiPredeterminadoFinca() As String
        Return LeerParametro(cgAnalisisPreFinca)
    End Function

    Public Function ArchivoDAA() As String
        Return LeerParametro("BDGFICDAA")
    End Function

    Public Function DirectorioFactElec() As String
        Return LeerParametro("PATHPDFFE")
    End Function

    Public Function MargenBruto() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey("MARGEN")
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else
            Return False
        End If
    End Function

    Public Function ExpertisSAAS() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey(cgFwExpertisSAAS)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function ExpertisCRM() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey("C_CRM")
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function ExpertisERP() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey("C_ERP")
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function TPVValidezVales() As Integer
        Return CInt(LeerParametro(cgTPVCADVALE))
    End Function

    Public Function CajaActiva() As Boolean
        Return LeerParametro("CajaActiva")
    End Function

    Public Function ServidorSMTP() As String
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnSMTPSERVER)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0)("Valor") & String.Empty
        Else
            ApplicationService.GenerateError("El parámetro <|> no existe.", cnSMTPSERVER)
        End If
    End Function

    Public Function TPVBotones() As Boolean
        Return LeerParametro("TPVBotones")
    End Function

    Public Function GestionDeConceptosEnOfertasComerciales() As Boolean
        Return LeerParametro(cgGESTCONCEPTOOFC)
    End Function

    Public Function ContadorRemesa() As String
        Return LeerParametro(cgFwContadorRemesa)
    End Function

    Public Function InformeDeclaracion347() As String
        Return LeerParametro(cgFwInforme347Dec347)
    End Function

    Public Function PrefijoCuentaConciliacionBancaria() As String
        Return LeerParametro(cgPRECTACONC)
    End Function

    Public Function RutaImportacionFicheros() As String
        Return LeerParametro(cgRUTAIMPORT)
    End Function

    Public Function ContadorAvisoRetorno() As String
        Return LeerParametro("CONT_AR")
    End Function

    Public Function ServidorTimeStampCert() As String
        Return LeerParametro("URLTS")
    End Function

    Public Function CalcularPuntoVerde() As Boolean
        Return CBool(LeerParametro(cgPuntoVerde))
    End Function

    Public Function GAIANetExchange() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey("GAIANETEX")
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else
            Return False
        End If
    End Function

    Public Function TPVTicketsOPOS() As Boolean
        Dim Dt As DataTable = SelOnPrimaryKey("TPVTKOPOS")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return CBool(Dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function TPVVisorCajonOPOS() As Boolean
        Dim Dt As DataTable = SelOnPrimaryKey("TPVVCOPOS")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return CBool(Dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function CuentaCRMInterna() As String
        Dim Dt As DataTable = SelOnPrimaryKey("CUENTACRMI")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return Dt.Rows(0)("Valor")
        Else : Return String.Empty
        End If
    End Function

    Public Function TipoActividadCRM() As String
        Dim Dt As DataTable = SelOnPrimaryKey("CRMTIPOACT")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return Dt.Rows(0)("Valor")
        Else : Return String.Empty
        End If
    End Function

    Public Function SituacionActividadCRM() As String
        Dim Dt As DataTable = SelOnPrimaryKey("CRMSITACT")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return Dt.Rows(0)("Valor")
        Else : Return String.Empty
        End If
    End Function

    Public Function ValidarCambioFechaFacturas() As Boolean
        Dim Dt As DataTable = SelOnPrimaryKey("VALCAMF")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return CBool(Dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function CContableDevolucion() As String
        Dim dt As DataTable = SelOnPrimaryKey("CC_DEVOL")
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            Return dt.Rows(0)("Valor")
        Else
            Return String.Empty
        End If
    End Function

    Public Function DatosTransporteAutomatico() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey("BDGTRANAUT")
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else
            Return False
        End If
    End Function

    Public Function CalculoPrecioMedioPorMovimiento() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey(cgPrecioMedioPorMovimiento)
        If Not dt Is Nothing AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else
            Return False
        End If
    End Function

    Public Function ClienteDepositoDigital() As String
        Return LeerParametro(cgAPPDDIG)
    End Function

    Public Function RiesgoCliente() As Boolean
        RiesgoCliente = CBool(LeerParametro(cnRIESGOCLTE))
    End Function

    Public Function TipoProyectoPredeterminado() As String
        Return LeerParametro(cgTIPOPROY)
    End Function

    Public Function ContadorProvisionalTPV() As String
        Dim IDContador As String = LeerParametro(cgCONTPROVTPV, False)
        If Length(IDContador) > 0 Then
            Dim dtContador As DataTable = New Contador().SelOnPrimaryKey(IDContador)
            If dtContador.Rows.Count = 0 Then
                IDContador = String.Empty
            Else
                Dim f As New Filter
                f.Add(New StringFilterItem("Entidad", "AlbaranVentaCabecera"))
                f.Add(New StringFilterItem("IDContador", IDContador))
                Dim dtContadorEntidad As DataTable = AdminData.GetData("tbEntidadContador", f)
                If dtContadorEntidad.Rows.Count = 0 Then
                    IDContador = String.Empty
                End If
            End If
        End If

        Return IDContador
    End Function


    Public Function CSB43PathFicheroOrigen() As String
        CSB43PathFicheroOrigen = LeerParametro(cnCSB43ORIGEN)
    End Function

    Public Function CSB43PathFicheroDestino() As String
        CSB43PathFicheroDestino = LeerParametro(cnCSB43DESTINO)
    End Function
    Public Function CSB43DiasTolerancia() As Integer
        CSB43DiasTolerancia = LeerParametro(cnCSB43DIAS)
    End Function

    Public Function ActAutomaticaRecepcionTransferencia() As Boolean
        If LeerParametro(cgFwActAutoRT) = "1" Then
            ActAutomaticaRecepcionTransferencia = True
        Else
            ActAutomaticaRecepcionTransferencia = False
        End If
    End Function

    Public Function GenerarSolicitudTransferenciaCompleta() As Boolean
        If LeerParametro(cgFwSTCompleta) = "1" Then
            GenerarSolicitudTransferenciaCompleta = True
        Else
            GenerarSolicitudTransferenciaCompleta = False
        End If
    End Function

    'Public Function ContabilidadMultiple() As Integer
    '    Dim dt As DataTable = SelOnPrimaryKey(cgContabilidadMultiple)
    '    If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
    '        Return dt.Rows(0)("Valor")
    '    Else
    '        Return 0
    '    End If
    'End Function

    Public Function ContabilidadMultiple() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey(cgContabilidadMultiple)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else
            Return False
        End If
    End Function

    '//Quitarlo cuando se ponga todo OK
    Public Function DobleContabilidad() As Integer
        Return ContabilidadMultiple()
    End Function


    Public Function ContadorRemesaAnticipo() As String
        Return LeerParametro(cgFwContadorRemesaAnticipo)
    End Function


    Public Function PedidoConfirmado() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey(cgPedidoConfirmado)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else
            Return False
        End If
    End Function

    Public Function ExpedirPedidosConfirmados() As Boolean
        Dim dt As DataTable = SelOnPrimaryKey(cgExpedirPedConfir)
        If Not IsNothing(dt) AndAlso dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else
            Return False
        End If
    End Function

    Public Function AltaCitaActividadCRMOutlook() As Boolean
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey("CRMADDAPP")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return CBool(Dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function FacturacionSinPropuesta() As Boolean
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey("FACTSINPRO")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return CBool(Dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function TPVTiempoCambio() As Integer
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey("TPVSECCAMB")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return CInt(Dt.Rows(0)("Valor"))
        Else : Return 0
        End If
    End Function

    Public Function TPVSaldarTotalVale() As Boolean
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey("TPVSALVAL")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return CBool(Dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function TPVCambioVendedor() As Boolean
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey("TPVCAMBV")
        If Not Dt Is Nothing AndAlso Dt.Rows.Count > 0 Then
            Return CBool(Dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function IvaCajaCircuitoVentas() As Boolean
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnIVA_CAJA_VENTAS)
        If dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        End If
    End Function

    Public Function GestionInventarioPermanente() As Boolean
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnINV_PERMANENTE)
        If dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        End If
    End Function

    Public Function CContableProductoEnCurso() As String
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnCCPRODUCTO_CURSO)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("Valor")
        End If
    End Function

    Public Function CContableIngresos() As String
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnCCINGRESO)
        If dt.Rows.Count > 0 Then
            Return dt.Rows(0)("Valor")
        End If
    End Function

    Public Function LimiteVentaTPV() As Double
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey("LIMTPV")
        If dt.Rows.Count > 0 Then
            Return CDbl(dt.Rows(0)("Valor"))
        Else : Return True
        End If
    End Function

    Public Function LiquidacionRepresentante() As Boolean
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey("LIQ_REPRES")
        If dt.Rows.Count > 0 Then
            Return CBool(dt.Rows(0)("Valor"))
        Else : Return True
        End If
    End Function

    Public Function BodegaSeguimiento() As Boolean
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey("BDGSEG")
        If Dt.Rows.Count > 0 Then
            Return CBool(Dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function BodegaSeguimientoObligatorio() As Boolean
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey("BDGSEGOBG")
        If Dt.Rows.Count > 0 Then
            Return CBool(Dt.Rows(0)("Valor"))
        Else : Return False
        End If
    End Function

    Public Function BodegaTipoOperacionArtCompatibles() As String
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey("BDGTOPCOMP")
        If Dt.Rows.Count > 0 Then
            Return CStr(Dt.Rows(0)("Valor"))
        Else : Return String.Empty
        End If
    End Function

    Public Function BodegaDepositoArtCompatibles() As String
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey("BDGDEPCOMP")
        If Dt.Rows.Count > 0 Then
            Return CStr(Dt.Rows(0)("Valor"))
        Else : Return String.Empty
        End If
    End Function

    Public Function TipoAlbaranExpDistribuidor() As String
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey(cnTIPO_ALB_DISTRIBUIDOR)
        If Dt.Rows.Count > 0 Then
            Return Dt.Rows(0)("Valor")
        Else : Return String.Empty
        End If
    End Function

    Public Function TipoAlbaranAbonoDistribuidor() As String
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey(cnTIPO_ALB_ABONO_DISTRIBUIDOR)
        If Dt.Rows.Count > 0 Then
            Return Dt.Rows(0)("Valor")
        Else : Return String.Empty
        End If
    End Function

    Public Function TarifaComponenteKit() As String
        Dim Dt As DataTable = New Parametro().SelOnPrimaryKey("TARIFAKIT")
        If Dt.Rows.Count > 0 Then
            Return Dt.Rows(0)("Valor")
        Else : Return String.Empty
        End If
    End Function

    Public Function AmortizacionPorGrupo() As Boolean
        Dim dtParam As DataTable = New Parametro().SelOnPrimaryKey("AMORTGRUPO")
        If dtParam.Rows.Count > 0 Then
            AmortizacionPorGrupo = CBool(Nz(dtParam.Rows(0)("Valor"), False))
        End If
    End Function

    Public Function TPVGestionPorFacturas() As Boolean
        Dim dtParam As DataTable = New Parametro().SelOnPrimaryKey("TPVFACTURA")
        If dtParam.Rows.Count > 0 Then
            TPVGestionPorFacturas = CBool(Nz(dtParam.Rows(0)("Valor"), False))
        End If
    End Function

    Public Function TarifaLimitarResultado() As Boolean
        Dim DtParam As DataTable = New Parametro().SelOnPrimaryKey("TARRESUL")
        If DtParam.Rows.Count > 0 Then
            Return DtParam.Rows(0)("Valor")
        Else : Return False
        End If
    End Function

    Public Function TPVPermitirVentasImporte0() As Boolean
        TPVPermitirVentasImporte0 = True
        Dim dtParam As DataTable = New Parametro().SelOnPrimaryKey("TPV_IMP0")
        If dtParam.Rows.Count > 0 Then
            TPVPermitirVentasImporte0 = CBool(Nz(dtParam.Rows(0)("Valor"), True))
        End If
    End Function

    Public Function CalidadMuestraNueva() As Boolean
        Dim DtParam As DataTable = New Parametro().SelOnPrimaryKey("CALMUEST")
        If DtParam.Rows.Count > 0 Then
            Return CBool(Nz(DtParam.Rows(0)("Valor"), False))
        End If
    End Function

    Public Function FactElecCuerpoMensajeHTMCorreo() As String
        Dim DtParam As DataTable = New Parametro().SelOnPrimaryKey("FELECHTM")
        If DtParam.Rows.Count > 0 Then
            Return CStr(Nz(DtParam.Rows(0)("Valor"), String.Empty))
        End If
    End Function

    Public Function FactElecAsuntoCorreo() As String
        Dim DtParam As DataTable = New Parametro().SelOnPrimaryKey("FELECASUN")
        If DtParam.Rows.Count > 0 Then
            Return CStr(Nz(DtParam.Rows(0)("Valor"), String.Empty))
        End If
    End Function

    Public Function ContadorPedidoCompraTablet() As String
        Dim DtParam As DataTable = New Parametro().SelOnPrimaryKey(cnCONTADOR_PEDIDO_COMPRA_TABLET)
        If DtParam.Rows.Count > 0 Then
            Return CStr(Nz(DtParam.Rows(0)("Valor"), String.Empty))
        End If
    End Function

    Public Const cnPRECIO_ALB_PERIODOS_CERRADOS As String = "PRCALBCICE"
    Public Function ActualizarPrecioAlbaranPeriodoCerrado() As Boolean
        ActualizarPrecioAlbaranPeriodoCerrado = False
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnPRECIO_ALB_PERIODOS_CERRADOS)
        If dt.Rows.Count > 0 Then
            Return CBool(Nz(dt.Rows(0)("Valor"), False))
        End If
    End Function

    Public Const cnTIPO_MOVIMIENTO_CANTIDAD_0 As String = "TIPOMOV0"
    Public Function TipoMovimientoCantidad0() As Integer
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnTIPO_MOVIMIENTO_CANTIDAD_0)
        If dt.Rows.Count > 0 Then
            Return CInt(Nz(dt.Rows(0)("Valor"), 0))
        End If
    End Function

    Public Const cnFIFO_TAMANO_PAGINACION As String = "FIFO_PAG"
    Public Function TamanioPaginacionPreciosFIFO() As Integer
        Dim TamanioInicial As Integer = 1000
        TamanioPaginacionPreciosFIFO = TamanioInicial
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnFIFO_TAMANO_PAGINACION)
        If dt.Rows.Count > 0 Then
            Return CInt(Nz(dt.Rows(0)("Valor"), TamanioInicial))
        End If
    End Function

    Public Const cnCALC_PRECIO_STD_MOVTOS_FECHA As String = "STOCK_STDF"
    Public Function CalcularPrecioStdEnMovimientosAFecha() As Boolean
        CalcularPrecioStdEnMovimientosAFecha = True
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnCALC_PRECIO_STD_MOVTOS_FECHA)
        If dt.Rows.Count > 0 Then
            Return CBool(Nz(dt.Rows(0)("Valor"), True))
        End If
    End Function

    Public Const cnCALC_PRECIO_MEDIO_MOVTOS As String = "STOCK_PM"
    Public Function CalcularPrecioMedioEnMovimientos() As Boolean
        CalcularPrecioMedioEnMovimientos = True
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnCALC_PRECIO_MEDIO_MOVTOS)
        If dt.Rows.Count > 0 Then
            Return CBool(Nz(dt.Rows(0)("Valor"), True))
        End If
    End Function

    Public Const cnCALC_PRECIO_FIFO_F_MOVTOS As String = "STOCK_FFD"
    Public Function CalcularPrecioFIFOFechaEnMovimientos() As Boolean
        CalcularPrecioFIFOFechaEnMovimientos = True
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnCALC_PRECIO_FIFO_F_MOVTOS)
        If dt.Rows.Count > 0 Then
            Return CBool(Nz(dt.Rows(0)("Valor"), True))
        End If
    End Function

    Public Const cnCALC_PRECIO_FIFO_M_MOVTOS As String = "STOCK_FFM"
    Public Function CalcularPrecioFIFOMovtoEnMovimientos() As Boolean
        CalcularPrecioFIFOMovtoEnMovimientos = True
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnCALC_PRECIO_FIFO_M_MOVTOS)
        If dt.Rows.Count > 0 Then
            Return CBool(Nz(dt.Rows(0)("Valor"), True))
        End If
    End Function

    Public Function TipoDocIdentificativo() As String
        Dim DtParam As DataTable = New Parametro().SelOnPrimaryKey(cnTIPO_DOCUMENTO)
        If DtParam.Rows.Count > 0 Then
            Return CInt(Nz(DtParam.Rows(0)("Valor"), 1))
        End If
    End Function


    Public Const cnTPV_IMPRIMIR_APARCAR As String = "TPV_IMPAP"
    Public Function TPVImprimirAparcar() As Boolean
        TPVImprimirAparcar = False
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnTPV_IMPRIMIR_APARCAR)
        If dt.Rows.Count > 0 Then
            Return CBool(Nz(dt.Rows(0)("Valor"), False))
        End If
    End Function

    Public Const cnAUDI_NOMBRE As String = "AUDINOMBRE"
    Public Function AuditoriaCompraNombre() As String
        Return CStr(LeerParametro(cnAUDI_NOMBRE))
    End Function

    Public Const cnAUDI_DIRECCION As String = "AUDIDIREC"
    Public Function AuditoriaCompraDireccion() As String
        Return CStr(LeerParametro(cnAUDI_DIRECCION))
    End Function

    Public Const cnAUDI_CORREO As String = "AUDIEMAIL"
    Public Function AuditoriaCompraEMail() As String
        Return CStr(LeerParametro(cnAUDI_CORREO))
    End Function

    '//Servicio para la validación de NIFs intracomunitarios
    Public Const cnURL_VIES As String = "URLVIES"
    Public Function URLWebServicesVIES() As String
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnURL_VIES)
        If dt.Rows.Count > 0 Then
            Return Trim(dt.Rows(0)("Valor") & String.Empty)
        End If
    End Function

    Public Const cnFICH_DIVISAS As String = "FICHDIVISA"
    Public Function FicheroImportacionDivisas() As String
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnFICH_DIVISAS)
        If dt.Rows.Count > 0 Then
            Return Trim(dt.Rows(0)("Valor") & String.Empty)
        End If
    End Function


    Public Const cnSIT_PAG_DEF As String = "PAGDFTOSIT"
    Public Function SituacionPagoSinContaDefecto() As enumPagoSituacion
        Dim p As New Parametro
        Dim dt As DataTable = p.SelOnPrimaryKey(cnSIT_PAG_DEF)
        If dt.Rows.Count > 0 Then
            Return Nz(dt.Rows(0)("Valor"), enumPagoSituacion.Pagado)
        Else
            Return p.PagoContSituacion
        End If
    End Function

    'Public Const cnOPER_VIES As String = "OPERVIES"
    'Public Function OperadorVIES() As Boolean
    '    Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnOPER_VIES)
    '    If dt.Rows.Count > 0 Then
    '        Return Nz(dt.Rows(0)("Valor"), False)
    '    End If
    'End Function

    Public Const cnFP_MANDATO_SEPA As String = "FPSEPA"
    Public Function FormaPagoMandatoSEPA() As List(Of String)
        Dim dt As DataTable = New Parametro().SelOnPrimaryKey(cnFP_MANDATO_SEPA)
        If dt.Rows.Count > 0 Then
            Dim FPSEPA As String = dt.Rows(0)("Valor") & String.Empty
            Dim FormasPagoSEPA As List(Of String) = FPSEPA.Split(";").ToList

            For i As Integer = 0 To FormasPagoSEPA.Count - 1
                FormasPagoSEPA(i) = UCase(FormasPagoSEPA(i))
            Next
            Return FormasPagoSEPA
        End If
    End Function

End Class