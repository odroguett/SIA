Imports cConexion
Imports System.Data
Public Class cCatalogoBaseDatos
    Private Outiles As New cUtiles
#Region "Parametros"
    Public Function Parametros() As DataTable
        Dim sSQL As String
        sSQL = "select * from Parametros"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function ActualizParametros(ByVal nPorcentajeTransbank As Double, ByVal nPorcentajeGanancia As Double, ByVal nPorcentajeIVA As Double,
                                         ByVal iBodega As Integer, ByVal iVenta As Integer) As Boolean
        Dim sUpdate As String = ""
        Outiles.AddVarUpdate(sUpdate, "PORCENTAJE_TRANSBANK", nPorcentajeTransbank, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "PORCENTAJE_GANANCIA", nPorcentajeGanancia, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "PORCENTAJE_IVA", nPorcentajeIVA, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "BODEGA", iBodega, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "VENTA", iVenta, "NUMBER")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update parametros set " + sUpdate

        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function
#End Region
#Region "Utilidades"
    Public Function Ciudades() As DataTable
        Dim sSQL As String
        sSQL = "select ciudad,descripcion from Ciudades"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function Comunas() As DataTable
        Dim sSQL As String
        sSQL = "select comuna,descripcion from Comunas"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function Regiones() As DataTable
        Dim sSQL As String
        sSQL = "select region,descripcion from Regiones"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

#End Region
#Region "Productos"
    Public Function TiposProductos() As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""

        sSQL = "select codigo,descripcion from tipos_producto "
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function TipoProductos(ByVal sCodigo As String) As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""
        sWhere = "where codigo = " + Outiles.GetSQLValue(sCodigo, "STRING")

        sSQL = "select codigo,descripcion from tipos_producto " + sWhere
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function IngresaTipoProducto(ByVal sCodigo As String, ByVal sDescripcion As String) As Boolean
        Dim sInsert As String = ""
        sInsert = "insert into tipos_producto values (" +
                  "'" + sCodigo + "','" + sDescripcion + "')"
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function
    Public Function ActualizaTipoProducto(ByVal sCodigo As String, ByVal sDescripcion As String) As Boolean
        Dim sUpdate As String = ""
        sUpdate = "update tipos_producto set " +
                   "descripcion = '" + sDescripcion + "'" +
                   " where codigo = '" + sCodigo + "'"
        If oConexion.ExecBool(sUpdate) Then
            Return True
        End If
        Return False

    End Function

    Public Function EliminaTipoProducto(ByVal sCodigo As String) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from tipos_producto  " +
                  " where codigo = '" + sCodigo + "'"
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False

    End Function

    Public Function TiposUnidad(Optional sTipoUnidad As String = "") As DataTable
        Dim sSQL As String
        Dim sSQl1 As String = ""
        If sTipoUnidad <> "" Then sSQl1 = " where  unidad = " + Outiles.GetSQLValue(sTipoUnidad, "STRING")
        sSQL = " select unidad,descripcion,grupo,conversion from tipos_unidad" + sSQl1 +
               " order by grupo desc "
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function CBTiposUnidad(Optional sTipoUnidad As String = "") As DataTable
        Dim sSQL As String
        Dim sSQl1 As String = ""
        If sTipoUnidad <> "" Then sSQl1 = " where  unidad = " + Outiles.GetSQLValue(sTipoUnidad, "STRING")
        sSQL = " select unidad,descripcion from tipos_unidad" + sSQl1 +
               " order by grupo desc "
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function ObtieneTiposUnidadGrupo(ByVal sGrupo As String, ByVal sTipoUnidad As String) As DataTable
        Dim sSQL As String
        sSQL = "select unidad,descripcion from tipos_unidad " +
               " where GRUPO = " + Outiles.GetSQLValue(sGrupo, "STRING") +
               " and unidad <> " + Outiles.GetSQLValue(sTipoUnidad, "STRING")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function IngresaTiposUnidad(ByVal sTipoUnidad As String, ByVal sDescripcion As String) As Boolean
        Dim sInsert As String = ""
        sInsert = "insert into tipos_unidad values (" +
                  "'" + sTipoUnidad + "','" + sDescripcion + "')"
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function ActualizaTiposUnidad(ByVal sTipoUnidad As String, ByVal sDescripcion As String) As Boolean
        Dim sUpdate As String = ""
        sUpdate = "update tipos_Unidad set " +
                   "descripcion = '" + sDescripcion + "'" +
                   " where codigo = '" + sTipoUnidad + "'"
        If oConexion.ExecBool(sUpdate) Then
            Return True
        End If
        Return False

    End Function

    Public Function EliminaTiposUnidad(ByVal sTipoUnidad As String) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from tipos_unidad  " +
                  " where unidad = '" + sTipoUnidad + "'"
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False

    End Function

    Public Function GruposProducto() As DataTable
        Dim sSQL As String
        sSQL = "select id_grupo as codigo,descripcion from Grupos_producto"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function Productos() As DataTable
        Dim sSQL As String
        sSQL = "select * from productos"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function ProductosDetalleBodega() As DataTable
        Dim sSQL As String
        sSQL = "select codigo_producto as codigo,descripcion, proceso from productos " +
               "where proceso in ('B','V') "

        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function CargaProductosBodega() As DataTable
        Dim sSQL As String
        sSQL = "select codigo_producto as codigo,descripcion from productos " +
               "where proceso = 'B' "

        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function IngresaProductos(ByVal sCodigo As String, ByVal sDescripcion As String,
                                       ByVal sTipoProducto As String, ByVal sCodigoGrupo As String, ByVal sTipoProceso As String,
                                     ByVal iGranel As Integer) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO", sCodigo, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "DESCRIPCION", sDescripcion, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_GRUPO", sCodigoGrupo, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "TIPO_PRODUCTO", sTipoProducto, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "PROCESO", sTipoProceso, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "GRANEL", iGranel, "NUMBER")
        sInsert = "insert into Productos"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function ActualizaProducto(ByVal sCodigo As String, ByVal sDescripcion As String, ByVal sCodigoGrupo As String, ByVal sTipoProducto As String, ByVal sTipoProceso As String, ByVal iGranel As Integer) As Boolean
        Dim sUpdate As String = ""
        sUpdate = "update productos Set " +
                   "descripcion = '" + sDescripcion + "'," +
                   "codigo_grupo = '" + sCodigoGrupo + "'," +
                   "tipo_producto = '" + sTipoProducto + "'," +
                   "proceso = '" + sTipoProceso + "'," +
                   "granel = '" + Str(iGranel) + "'" +
                   " where codigo_producto = '" + sCodigo + "'"
        If oConexion.ExecBool(sUpdate) Then
            Return True
        End If
        Return False

    End Function

    Public Function IngresaProductosBodega(ByVal nNumeroIngreso As Double, ByVal sCodigoProducto As String, ByVal nValor As Double, ByVal nValorUnitario As Double, ByVal nValorUnCompra As Double, ByVal nTotalProductos As Double) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "NUMERO_INGRESO", nNumeroIngreso, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO", sCodigoProducto, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "VALOR_UNITARIO", nValorUnitario, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "VALOR_UNITARIO_COMPRA", nValorUnCompra, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "VALOR_TOTAL", nValor, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "TOTAL_PRODUCTOS", nTotalProductos, "NUMBER")


        sInsert = "insert into detalle_Productos_bodega"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function IngresaTipoGrupo(ByVal sCodigo As String, ByVal sDescripcion As String) As Boolean
        Dim sInsert As String = ""
        sInsert = "insert into grupos_producto values (" +
                  "'" + sCodigo + "','" + sDescripcion + "')"
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function ActualizaTipoGrupo(ByVal sCodigo As String, ByVal sDescripcion As String) As Boolean
        Dim sUpdate As String = ""
        sUpdate = "update grupos_producto set " +
                   "descripcion = '" + sDescripcion + "'" +
                   " where id_grupo = '" + sCodigo + "'"
        If oConexion.ExecBool(sUpdate) Then
            Return True
        End If
        Return False

    End Function
    Public Function EliminaTipoGrupo(ByVal sCodigo As String) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from grupos_producto  " +
                  " where id_grupo = '" + sCodigo + "'"
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False

    End Function

#End Region
#Region "Proveedores"
    Public Function Proveedores() As DataTable
        Dim sSQL As String
        sSQL = "select * from proveedores"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function IngresaProveedores(ByVal sCodigo As String, ByVal sDescripcion As String, ByVal sRut As String, ByVal sDireccion As String, ByVal sComuna As String,
                                       ByVal sCiudad As String, ByVal sRegion As String, ByVal sTelefono As String, ByVal sMail As String, ByVal sContacto As String) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PROVEEDOR", sCodigo, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "DESCRIPCION", sDescripcion, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "RUT_PROVEEDOR", sRut, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "DIRECCION", sDireccion, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "COMUNA", sComuna, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CIUDAD", sCiudad, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "REGION", sRegion, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "TELEFONO", sTelefono, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "MAIL", sMail, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CONTACTO", sContacto, "STRING")
        sInsert = "insert into PROVEEDORES"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function EliminaProveedores(ByVal sCodigo As String) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from proveedores  " +
                  " where codigo_proveedor = '" + sCodigo + "'"
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False
    End Function

    Public Function ActualizaProveedores(ByVal sCodigo As String, ByVal sDescripcion As String, ByVal sRutProveedor As String,
                                         ByVal sDireccion As String, ByVal sComuna As String, ByVal sCiudad As String, ByVal sRegion As String,
                                         ByVal sTelefono As String, ByVal sMail As String, ByVal sContacto As String) As Boolean
        Dim sUpdate As String = ""
        Outiles.AddVarUpdate(sUpdate, "DESCRIPCION", sDescripcion, "STRING")
        Outiles.AddVarUpdate(sUpdate, "RUT_PROVEEDOR", sRutProveedor, "STRING")
        Outiles.AddVarUpdate(sUpdate, "DIRECCION", sDireccion, "STRING")
        Outiles.AddVarUpdate(sUpdate, "COMUNA", sComuna, "STRING")
        Outiles.AddVarUpdate(sUpdate, "CIUDAD", sCiudad, "STRING")
        Outiles.AddVarUpdate(sUpdate, "REGION", sRegion, "STRING")
        Outiles.AddVarUpdate(sUpdate, "TELEFONO", sTelefono, "STRING")
        Outiles.AddVarUpdate(sUpdate, "MAIL", sMail, "STRING")
        Outiles.AddVarUpdate(sUpdate, "CONTACTO", sContacto, "STRING")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update proveedores set " + sUpdate +
                  " where codigo_proveedor = " + Outiles.GetSQLValue(sCodigo, "STRING")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function EliminaProducto(ByVal sCodigo As String) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from productos  " +
                  " where codigo_producto = '" + sCodigo + "'"
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False

    End Function
#End Region

#Region "Bodega"
    Public Function cbProveedores() As DataTable
        Dim sSQL As String
        sSQL = "select codigo_proveedor,descripcion from proveedores"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function CargaDatosBodega() As DataTable
        Dim sSQL As String
        sSQL = "select * from Bodega"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function MaxNumeroIngreso() As DataTable
        Dim sSQL As String
        sSQL = "select max(numero_ingreso) as numero_ingreso from Bodega"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function IngresaDatosBodega(ByVal nNumeroIngreso As Double, ByVal nNumeroOrden As Double, ByVal dFechaIngreso As Date, ByVal nValorCompra As String, ByVal sCodigoProveedor As String)

        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "NUMERO_INGRESO", nNumeroIngreso, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PROVEEDOR", sCodigoProveedor, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "FECHA_INGRESO", dFechaIngreso, "DATE")
        Outiles.AddVarInsert(sColumns, sValues, "NUMERO_ORDEN", nNumeroOrden, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "VALOR_COMPRA", nValorCompra, "NUMBER")
        sInsert = "insert into Bodega"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function ActualizaDatosBodega(ByVal nNumeroIngreso As Double, ByVal nNumeroOrden As Double, ByVal dFechaIngreso As Date, ByVal nValorCompra As String, ByVal sCodigoProveedor As String) As Boolean
        Dim sUpdate As String = ""
        Outiles.AddVarUpdate(sUpdate, "CODIGO_PROVEEDOR", sCodigoProveedor, "STRING")
        Outiles.AddVarUpdate(sUpdate, "FECHA_INGRESO", dFechaIngreso, "DATE")
        Outiles.AddVarUpdate(sUpdate, "NUMERO_ORDEN", nNumeroOrden, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "VALOR_COMPRA", nValorCompra, "NUMBER")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update BODEGA set " + sUpdate +
                  " where numero_ingreso = " + Outiles.GetSQLValue(nNumeroIngreso, "NUMBER")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ActualizaProductoBodega(ByVal nNumeroIngreso As Double, ByVal sCodigoProducto As String, ByVal nValor As Double, ByVal nValorUnitario As Double, ByVal nValorUnCompra As Double, ByVal nTotalProductos As Double) As Boolean
        Dim sUpdate As String = ""

        Outiles.AddVarUpdate(sUpdate, "VALOR_TOTAL", nValor, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "VALOR_UNITARIO", nValorUnitario, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "VALOR_UNITARIO_COMPRA", nValorUnCompra, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "TOTAL_PRODUCTOS", nTotalProductos, "NUMBER")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update DETALLE_PRODUCTOS_BODEGA set " + sUpdate +
                  " where numero_ingreso = " + Outiles.GetSQLValue(nNumeroIngreso, "NUMBER") +
                  " and codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function EliminaIngresoBodega(ByVal nNumeroIngreso As Double) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from bodega  " +
                  " where numero_ingreso = " + CStr(nNumeroIngreso)
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False
    End Function

    Public Function DetalleProductosBodega(ByVal nNumeroIngreso As Double, Optional ByVal sCodigoProducto As String = "") As DataTable
        Dim sSQL As String
        Dim sSQl1 As String = ""
        If sCodigoProducto <> "" Then sSQl1 = " and codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        sSQL = "select * from detalle_productos_Bodega" +
               " where numero_ingreso = " + Outiles.GetSQLValue(nNumeroIngreso, "NUMBER") + sSQl1
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function EliminaDetalleProductosBodega(ByVal nNumeroIngreso As Double, Optional sCodigoTipProducto As String = "") As Boolean
        Dim sSQL As String
        Dim sSqlAux As String = ""
        If sCodigoTipProducto <> "" Then sSqlAux = " and codigo_producto = " + Outiles.GetSQLValue(sCodigoTipProducto, "STRING")
        sSQL = "delete from detalle_productos_Bodega" +
               " where numero_ingreso = " + Outiles.GetSQLValue(nNumeroIngreso, "NUMBER") + sSqlAux
        If oConexion.ExecBool(sSQL) Then
            Return True
        End If
    End Function

    Public Function EliminaDesgloseProductosBodega(ByVal nNumeroIngreso As Double, sCodigoTipProducto As String) As Boolean
        Dim sSQL As String
        sSQL = "delete from desglose_productos_Bodega" +
               " where codigo_producto = " + Outiles.GetSQLValue(sCodigoTipProducto, "STRING") +
               " and numero_ingreso = " + Outiles.GetSQLValue(nNumeroIngreso, "NUMBER")
        If oConexion.ExecBool(sSQL) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function ActualizaDetalleProductosBodega(ByVal nNumeroIngreso As Double, sCodigoTipProducto As String) As Boolean
        Dim sSQL As String
        sSQL = "delete from detalle_productos_Bodega" +
               " where numero_ingreso = " + Outiles.GetSQLValue(nNumeroIngreso, "NUMBER") +
               " and codigo_tipo_producto = " + Outiles.GetSQLValue(sCodigoTipProducto, "STRING")
        If oConexion.ExecBool(sSQL) Then
            Return True
        End If
    End Function

    Public Function ValorCompra(ByVal nNumeroIngreso As Double) As DataTable
        Dim sSQL As String
        sSQL = "select sum(valor_total) as Valor_Compra from detalle_productos_Bodega" +
               " where numero_ingreso = " + Outiles.GetSQLValue(nNumeroIngreso, "NUMBER")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function ActualizaValorCompra(ByVal nNumeroIngreso As Double, ByVal nValorCompra As Double) As Boolean
        Dim sUpdate As String = ""
        Outiles.AddVarUpdate(sUpdate, "valor_compra", nValorCompra, "NUMBER")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update BODEGA set " + sUpdate +
                  " where numero_ingreso = " + Outiles.GetSQLValue(nNumeroIngreso, "NUMBER")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function IngresaTiposUnidadProducto(ByVal sCodigoProducto As String, ByVal sTipoUnidad As String, ByVal nCantidad As Double) As Double

        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO", sCodigoProducto, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "TIPO_UNIDAD", sTipoUnidad, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CANTIDAD", nCantidad, "NUMBER")
        sInsert = "insert into tipo_unidad_producto"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta

    End Function

    Public Function IngresaDesgloseProductosBodega(ByVal nNumeroIngreso As Double, ByVal sCodigoProducto As String, ByVal sTipoUnidad As String, ByVal nCantidad As Double) As Double

        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "NUMERO_INGRESO", nNumeroIngreso, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO", sCodigoProducto, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "TIPO_UNIDAD", sTipoUnidad, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CANTIDAD", nCantidad, "NUMBER")
        sInsert = "insert into desglose_productos_bodega"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If

        Return bRespuesta
    End Function

    Public Function IngresaInventarioProductosBodega(ByVal sCodigoProducto As String, ByVal sTipoUnidad As String, ByVal nCantidad As Double) As Double

        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        Dim sUpdate As String = ""
        ' Dim oConexion As New cConexion


        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO", sCodigoProducto, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "TIPO_UNIDAD", sTipoUnidad, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CANTIDAD", nCantidad, "NUMBER")
        sInsert = "insert into inventario_productos_bodega"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            sUpdate = Outiles.NoTail(sUpdate, ",")
            sUpdate = "Update inventario_productos_bodega set cantidad = cantidad + " + Outiles.GetSQLValue(nCantidad, "NUMBER") +
                      " where tipo_unidad = " + Outiles.GetSQLValue(sTipoUnidad, "STRING") +
                      " and CODIGO_PRODUCTO = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
            If oConexion.ExecBool(sUpdate) Then
                Return True
            Else
                Return False
            End If
        End If

        Return bRespuesta
    End Function


    Public Function ActualizaInventarioProductosBodega(ByVal sCodigoProducto As String, ByVal sTipoUnidad As String, ByVal nCantidad As Double) As Double


        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        Dim sUpdate As String = ""
        ' Dim oConexion As New cConexion


        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update inventario_productos_bodega set cantidad = " + Outiles.GetSQLValue(nCantidad, "NUMBER") +
                     " where tipo_unidad = " + Outiles.GetSQLValue(sTipoUnidad, "STRING") +
                      " and CODIGO_PRODUCTO = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If


        Return bRespuesta
    End Function

    Public Function EliminaProductosInventarioBodega(ByVal sCodigoProducto As String, ByVal sTipoUnidad As String, ByVal nCantidad As Double) As Double


        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        Dim sUpdate As String = ""
        Dim sDelete As String = ""
        Dim iCantidadDesglose As Integer

        ' Dim oConexion As New cConexion
        iCantidadDesglose = CantidadDesgloseProductosBodega(sCodigoProducto)
        If iCantidadDesglose = 0 Then
            sDelete = "delete from detalle_movimientos_inventario " +
                      "where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
            If oConexion.ExecBool(sDelete) Then
                sDelete = "delete from inventario_productos_bodega " +
                      "where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
                If oConexion.ExecBool(sDelete) Then
                    Return True
                Else
                    Return False
                End If
            Else
                Return False
            End If


        Else
            sUpdate = Outiles.NoTail(sUpdate, ",")
            sUpdate = "Update inventario_productos_bodega set cantidad = cantidad + " + Outiles.GetSQLValue(nCantidad, "NUMBER") +
                     " where tipo_unidad = " + Outiles.GetSQLValue(sTipoUnidad, "STRING") +
                      " and CODIGO_PRODUCTO = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
            If oConexion.ExecBool(sUpdate) Then
                Return True
            Else
                Return False
            End If

        End If


        Return bRespuesta
    End Function
    Private Function CantidadDesgloseProductosBodega(ByVal sCodigo As String) As Integer
        Dim iCantidad As Integer
        Dim tbDesglose As New DT
        tbDesglose.RecordSet = CantidadDesgloseProductoBodega(sCodigo)
        If tbDesglose.EOF = False Then
            iCantidad = tbDesglose.GetValue("cantidad")
        End If
        Return iCantidad
    End Function

    Public Function CargaDatosBodegaInventario(ByVal iNumeroIngreso As Integer, ByVal sCodigoProducto As String) As DataTable
        Dim sSQL As String
        sSQL = "select dpb.cantidad as Cantidad_bodega, dpb.tipo_unidad as tipo_unidad_bodega,ipb.* from desglose_productos_bodega dpb,inventario_productos_bodega ipb " +
               " where  dpb.codigo_producto = ipb.codigo_producto and dpb.numero_ingreso = " + Outiles.GetSQLValue(iNumeroIngreso, "NUMBER") +
               " and dpb.codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function CargaDatosInventarioProduccion(ByVal sCodigoProducto As String)
        Dim sSQL As String
        sSQL = " Select * From inventario_productos_bodega Where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If

    End Function


    Public Function ActualizaTiposUnidadProducto(ByVal sCodigoProducto As String, ByVal sTipoUnidad As String, ByVal nCantidad As Double) As Boolean
        Dim sUpdate As String = ""
        If IngresaTiposUnidadProducto(sCodigoProducto, sTipoUnidad, nCantidad) = False Then
            Outiles.AddVarUpdate(sUpdate, "CANTIDAD", nCantidad, "NUMBER")
            sUpdate = Outiles.NoTail(sUpdate, ",")
            sUpdate = "Update tipo_unidad_producto set " + sUpdate +
                      " where tipo_unidad = " + Outiles.GetSQLValue(sTipoUnidad, "STRING") +
                      " and CODIGO_PRODUCTO = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
            If oConexion.ExecBool(sUpdate) Then
                Return True
            Else
                Return False
            End If
        End If

    End Function


    Public Function EliminaTiposUnidadProducto(ByVal sCodigoProducto As String, Optional ByVal sTipoUnidad As String = "") As Boolean
        Dim sDelete As String = ""
        Dim sDelete1 As String
        If sTipoUnidad <> "" Then sDelete1 = " and tipo_unidad = " + Outiles.GetSQLValue(sTipoUnidad, "STRING")
        sDelete = "delete from tipo_unidad_producto where " +
            "codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") + "and " + sDelete1

        If oConexion.ExecBool(sDelete) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function CargaTiposUnidadProducto(ByVal sCodigoProducto As String) As DataTable
        Dim sSQL As String
        sSQL = "select * from tipo_unidad_producto" +
               " where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") +
               "order by cantidad asc"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function CargaDesgloseProductoBodega(ByVal sCodigoProducto As String, ByVal iNumeroIngreso As Integer) As DataTable
        Dim sSQL As String
        sSQL = "select * from desglose_productos_bodega" +
               " where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") +
               " and numero_ingreso = " + Outiles.GetSQLValue(iNumeroIngreso, "NUMBER") +
               " order by cantidad asc"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function ActualizaDesgloseProductosBodega(ByVal nNumeroIngreso As Double, ByVal sCodigoProducto As String, ByVal sTipoUnidad As String, ByVal nCantidad As Double) As Boolean
        Dim sUpdate As String = ""
        If IngresaDesgloseProductosBodega(nNumeroIngreso, sCodigoProducto, sTipoUnidad, nCantidad) = False Then
            Outiles.AddVarUpdate(sUpdate, "CANTIDAD", nCantidad, "NUMBER")
            sUpdate = Outiles.NoTail(sUpdate, ",")
            sUpdate = "Update desglose_productos_bodega set " + sUpdate +
                      " where tipo_unidad = " + Outiles.GetSQLValue(sTipoUnidad, "STRING") +
                      " and CODIGO_PRODUCTO = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") +
                      " and NUMERO_INGRESO = " + Outiles.GetSQLValue(nNumeroIngreso, "NUMBER")
            If oConexion.ExecBool(sUpdate) Then
                Return True
            Else
                Return False
            End If

        End If

    End Function
    Public Function CantidadDesgloseProductoBodega(ByVal sCodigoProducto As String) As DataTable
        Dim sSQL As String
        sSQL = "select count(*) as cantidad from desglose_productos_bodega" +
               " where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function DesgloseProductoBodega(ByVal iNumeroIngreso As Double) As DataTable
        Dim sSQL As String
        sSQL = "  Select * From desglose_productos_bodega" +
               " where numero_ingreso = " + Outiles.GetSQLValue(iNumeroIngreso, "NUMBER")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function



#End Region

#Region "Clientes"
    Public Function Clientes() As DataTable
        Dim sSQL As String
        sSQL = "select * from Clientes"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function CargaCBClientes(Optional ByVal sCodigoCliente As String = "") As DataTable
        Dim sSQL As String
        Dim sSQlAux1 As String = ""
        If sCodigoCliente <> "" Then sSQlAux1 = " where codigo  = " + Outiles.GetSQLValue(sCodigoCliente, "STRING")
        sSQL = "select codigo,descripcion from Clientes" + sSQlAux1
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function IngresaClientes(ByVal sCodigo As String, ByVal sDescripcion As String, ByVal sRut As String, ByVal sDireccion As String, ByVal sComuna As String,
                                      ByVal sCiudad As String, ByVal sRegion As String, ByVal sTelefono As String, ByVal sMail As String, ByVal sCelular As String) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "CODIGO", sCodigo, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "RUT_CLIENTE", sRut, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "DESCRIPCION", sDescripcion, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "DIRECCION", sDireccion, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "COMUNA", sComuna, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CIUDAD", sCiudad, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "REGION", sRegion, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "TELEFONO", sTelefono, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CELULAR", sCelular, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "MAIL", sMail, "STRING")
        sInsert = "insert into Clientes"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function ActualizaClientes(ByVal sCodigo As String, ByVal sDescripcion As String, ByVal sRutProveedor As String,
                                         ByVal sDireccion As String, ByVal sComuna As String, ByVal sCiudad As String, ByVal sRegion As String,
                                         ByVal sTelefono As String, ByVal sMail As String, ByVal sCelular As String) As Boolean
        Dim sUpdate As String = ""
        Outiles.AddVarUpdate(sUpdate, "DESCRIPCION", sDescripcion, "STRING")
        Outiles.AddVarUpdate(sUpdate, "RUT_CLIENTE", sRutProveedor, "STRING")
        Outiles.AddVarUpdate(sUpdate, "DIRECCION", sDireccion, "STRING")
        Outiles.AddVarUpdate(sUpdate, "COMUNA", sComuna, "STRING")
        Outiles.AddVarUpdate(sUpdate, "CIUDAD", sCiudad, "STRING")
        Outiles.AddVarUpdate(sUpdate, "REGION", sRegion, "STRING")
        Outiles.AddVarUpdate(sUpdate, "TELEFONO", sTelefono, "STRING")
        Outiles.AddVarUpdate(sUpdate, "MAIL", sMail, "STRING")
        Outiles.AddVarUpdate(sUpdate, "CELULAR", sCelular, "STRING")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update CLIENTES set " + sUpdate +
                  " where codigo = " + Outiles.GetSQLValue(sCodigo, "STRING")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function EliminaClientes(ByVal sCodigo As String) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from clientes  " +
                  " where codigo = '" + sCodigo + "'"
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False
    End Function

#End Region
#Region "Gastos"
    Public Function TiposGasto() As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""

        sSQL = "select codigo_gasto as codigo,descripcion from tipos_gasto "
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function IngresaTipoGasto(ByVal sCodigo As String, ByVal sDescripcion As String) As Boolean
        Dim sInsert As String = ""
        sInsert = "insert into tipos_gasto values (" +
                  "'" + sCodigo + "','" + sDescripcion + "')"
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function
    Public Function ActualizaTipoGasto(ByVal sCodigo As String, ByVal sDescripcion As String) As Boolean
        Dim sUpdate As String = ""
        sUpdate = "update tipos_gasto set " +
                   "descripcion = '" + sDescripcion + "'" +
                   " where codigo_gasto = '" + sCodigo + "'"
        If oConexion.ExecBool(sUpdate) Then
            Return True
        End If
        Return False

    End Function

    Public Function EliminaTipoGasto(ByVal sCodigo As String) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from tipos_gasto  " +
                  " where codigo_gasto = '" + sCodigo + "'"
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False

    End Function

    Public Function CargaTipoGastos() As DataTable
        Dim sSQL As String
        sSQL = "select codigo,descripcion from tipos_gasto "
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function CargaGastos(ByVal sCodigo As String, ByVal dFechaIngreso As Date) As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""

        sSQL = "select * from detalle_gastos_producto " +
              " where codigo_producto = " + Outiles.GetSQLValue(sCodigo, "STRING") + " and " +
              " fecha_produccion = " + Outiles.GetSQLValue(dFechaIngreso, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function IngresaGastoProducto(ByVal sCodigo As String, ByVal sCodigoGasto As String, ByVal nMontoGasto As Double, ByVal dFechaIngreso As Date) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO", sCodigo, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_GASTO", sCodigoGasto, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "MONTO", nMontoGasto, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "FECHA_PRODUCCION", dFechaIngreso, "DATE")
        sInsert = "insert into DETALLE_GASTOS_PRODUCTO"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function ActualizaGastoProducto(ByVal sCodigo As String, ByVal sCodigoGasto As String, ByVal nMontoGasto As Double, ByVal dFechaIngreso As Date) As Boolean
        Dim sUpdate As String = ""

        Outiles.AddVarUpdate(sUpdate, "MONTO", nMontoGasto, "NUMBER")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update DETALLE_GASTOS_PRODUCTO set " + sUpdate +
                  " where CODIGO_PRODUCTO = " + Outiles.GetSQLValue(sCodigo, "STRING") + " and " +
                  " CODIGO_GASTO = " + Outiles.GetSQLValue(sCodigoGasto, "STRING") + " and " +
                  " FECHA_PRODUCCION = " + Outiles.GetSQLValue(dFechaIngreso, "DATE")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function EliminaGastoProducto(ByVal sCodigo As String, ByVal sCodigoGasto As String, ByVal dFechaIngreso As Date) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from DETALLE_GASTOS_PRODUCTO  " +
                  " where codigo_Producto =" + Outiles.GetSQLValue(sCodigo, "STRING") + "  and " +
                  " codigo_gasto =" + Outiles.GetSQLValue(sCodigoGasto, "STRING") + " and  " +
                  " fecha_produccion =" + Outiles.GetSQLValue(dFechaIngreso, "DATE")
        If oConexion.ExecBool(sDelete) Then
            Return True
        Else
            Return False
        End If


    End Function
    Public Function TotalGastoProducto(ByVal sCodigo As String, ByVal dFechaProduccion As Date) As DataTable
        Dim sSQL As String
        Dim sWhere As String = ""

        sSQL = "select sum(monto) as total from detalle_gastos_producto " +
               " where codigo_producto = " + Outiles.GetSQLValue(sCodigo, "STRING") +
               " and fecha_produccion = " + Outiles.GetSQLValue(dFechaProduccion, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
#End Region
#Region "ProduccionProductos"
    Public Function CargaProduccionProductos(Optional sCodigoProducto As String = "") As DataTable
        Dim sSQL As String
        Dim sAnd As String = ""
        If sCodigoProducto <> "" Then sAnd = " and codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        sSQL = "select CODIGO_PRODUCTO as codigo,descripcion,TIPO_PRODUCTO, PROCESO from productos " +
                "where proceso in ('P','V') " + sAnd
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function CargaCBProduccionProductos() As DataTable
        Dim sSQL As String
        Dim sAnd As String = ""
        sSQL = "select CODIGO_PRODUCTO as codigo,descripcion from productos " +
                "where proceso in ('P','V') "
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function CargaValorUnitarioProducto(sCodigoProducto As String) As DataTable
        Dim sSQL As String

        sSQL = " Select  dpb.valor_unitario from detalle_productos_bodega dpb,bodega b " +
               " where dpb.numero_ingreso = b.numero_ingreso And dpb.codigo_producto =" + Outiles.GetSQLValue(sCodigoProducto, "STRING") +
               " order by fecha_ingreso desc "
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function ObtieneNumeroProducto(ByVal sCodigoProducto As String, ByVal dFechaproduccion As Date) As DataTable
        Dim sSQL As String
        sSQL = "select max(numero_producto) as numero from detalle_produccion_productos " +
               " where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") +
               " and fecha_produccion = " + Outiles.GetSQLValue(dFechaproduccion, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function DetalleProducionProductos(ByVal sCodigoProducto As String, ByVal dFechaIngreso As Date) As DataTable
        Dim sSQL As String
        sSQL = "select * from detalle_produccion_productos " +
               " where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") +
               " and fecha_produccion = " + Outiles.GetSQLValue(dFechaIngreso, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function ProducionProductos(ByVal sCodigoProducto As String, ByVal sCodigoProductoBodega As String, ByVal dFechaProduccion As Date) As DataTable
        Dim sSQL As String


        sSQL = " select pp.cantidad as total_productos, dp.tipo_unidad, dp.cantidad " +
               " from produccion_productos pp, detalle_produccion_productos dp " +
               " where pp.codigo_producto = dp.codigo_producto and  " +
               " pp.codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") +
               " and PP.FECHA_PRODUCCION = DP.FECHA_PRODUCCION " +
               " and dp.codigo_producto_bodega = " + Outiles.GetSQLValue(sCodigoProductoBodega, "STRING") + " and " +
               " dp.fecha_produccion = " + Outiles.GetSQLValue(dFechaProduccion, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function


    Public Function IngresaProduccionProductos(ByVal sCodigo As String, ByVal sCodigoProductoBodega As String, ByVal iNumeroProducto As Integer,
                                               ByVal dFechaIngreso As Date, ByVal sTipoUnidad As String, ByVal nCantidad As Double) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO", sCodigo, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "NUMERO_PRODUCTO", iNumeroProducto, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "TIPO_UNIDAD", sTipoUnidad, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "FECHA_PRODUCCION", dFechaIngreso, "DATE")
        Outiles.AddVarInsert(sColumns, sValues, "CANTIDAD", nCantidad, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO_BODEGA", sCodigoProductoBodega, "STRING")

        sInsert = "insert into detalle_produccion_productos"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function
    Public Function EliminaProduccionProducto(ByVal iNumeroProducto As Integer, ByVal sCodigo As String, ByVal dFechaIngreso As Date) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from detalle_produccion_productos  " +
                  " where numero_producto = " + Outiles.GetSQLValue(iNumeroProducto, "NUMBER") + " and " +
                  " codigo_producto = " + Outiles.GetSQLValue(sCodigo, "STRING") + " and " +
                  "fecha_produccion =  " + Outiles.GetSQLValue(dFechaIngreso, "DATE")
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False

    End Function
    Public Function ActualizaProduccionProducto(ByVal iNumeroProducto As Integer, ByVal sCodigo As String, ByVal sCodigoProductoBodega As String, dFechaInogreso As Date, nCantidad As Double, ByVal sTipoUnidad As String) As Boolean
        Dim sUpdate As String = ""
        Outiles.AddVarUpdate(sUpdate, "CODIGO_PRODUCTO_BODEGA", sCodigoProductoBodega, "STRING")
        Outiles.AddVarUpdate(sUpdate, "TIPO_UNIDAD", sTipoUnidad, "STRING")
        Outiles.AddVarUpdate(sUpdate, "CANTIDAD", nCantidad, "NUMBER")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update detalle_produccion_productos set " + sUpdate +
                  " where numero_producto = " + Outiles.GetSQLValue(iNumeroProducto, "NUMBER") + " and " +
                  " codigo_producto = " + Outiles.GetSQLValue(sCodigo, "STRING") + " and " +
                  " fecha_produccion = " + Outiles.GetSQLValue(dFechaInogreso, "DATE")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function InventarioProduccionProductos(ByVal sCodigoProducto As String, ByVal dFechaIngreso As Date) As DataTable
        Dim sSQL As String
        sSQL = " select ip.codigo_producto as codigo_producto_bodega, IP.tipo_unidad as tipo_unidad_inventario, dp.tipo_unidad as  tipo_unidad_detalle, ip.cantidad as cantidad_inventario,dp.cantidad as cantidad_detalle  " +
               " From detalle_produccion_productos dp, inventario_productos_bodega ip " +
               " Where dp.codigo_producto_bodega = ip.codigo_producto And " +
               " dp.codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") +
               " and dp.fecha_produccion = " + Outiles.GetSQLValue(dFechaIngreso, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function InventarioProduccionVenta(ByVal sCodigoProducto As String) As DataTable
        Dim sSQL As String
        sSQL = " select *  " +
               " From  inventario_productos_bodega " +
               " Where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function ActualizaFechaProduccionProducto(ByVal sCodigo As String, ByVal dFechaProduccion As Date, ByVal dFechaTermino As Date) As Boolean
        Dim sUpdate As String = ""

        Outiles.AddVarUpdate(sUpdate, "FECHA_TERMINO", dFechaTermino, "DATE")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update PRODUCCION_PRODUCTOS set " + sUpdate +
                  " where CODIGO_PRODUCTO = " + Outiles.GetSQLValue(sCodigo, "STRING") + " and " +
                  " FECHA_PRODUCCION = " + Outiles.GetSQLValue(dFechaProduccion, "DATE")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ActulizaUltimaFechaTermino(ByVal sCodigo As String, ByVal dFechaProduccion As Date) As Boolean
        Dim sUpdate As String = ""

        Outiles.AddVarUpdate(sUpdate, "FECHA_TERMINO", cConstantes.FECHA_TERMINO, "DATE")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update PRODUCCION_PRODUCTOS set " + sUpdate +
                  " where CODIGO_PRODUCTO = " + Outiles.GetSQLValue(sCodigo, "STRING") + " and " +
                  " FECHA_PRODUCCION = " + Outiles.GetSQLValue(dFechaProduccion, "DATE")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ObtieneUltimaFechaProduccionProducto(ByVal sCodigoProducto As String) As DataTable
        Dim sSQL As String
        sSQL = "select fecha_produccion from produccion_productos " +
               " where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") +
               " and fecha_termino = " + Outiles.GetSQLValue(cConstantes.FECHA_TERMINO, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function VerificaFechaTermino(ByVal sCodigoProducto As String, ByVal dFechaProduccion As Date) As DataTable
        Dim sSQL As String
        sSQL = "select fecha_termino from produccion_productos " +
               " where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") +
               " and fecha_produccion = " + Outiles.GetSQLValue(dFechaProduccion, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function IngresaProduccionProducto(ByVal sCodigo As String, ByVal dFechaProduccion As Date, ByVal iCantidad As Integer,
                                              ByVal nMontoGasto As Double, ByVal nValorCompra As Double, ByVal nPorcentajeGanancia As Double,
                                              ByVal nMontoGanancia As Double, ByVal nValorNeto As Double, ByVal nImpuesto As Double, ByVal nValorVenta As Double) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO", sCodigo, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "FECHA_PRODUCCION", dFechaProduccion, "DATE")
        Outiles.AddVarInsert(sColumns, sValues, "CANTIDAD", iCantidad, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "MONTO_GASTO", nMontoGasto, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "VALOR_COMPRA", nValorCompra, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "PORCENTAJE_GANANCIA", nPorcentajeGanancia, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "MONTO_GANANCIA", nMontoGanancia, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "VALOR_NETO", nValorNeto, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "IMPUESTO", nImpuesto, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "VALOR_VENTA", nValorVenta, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "FECHA_TERMINO", cConstantes.FECHA_TERMINO, "DATE")

        sInsert = "insert into PRODUCCION_PRODUCTOS"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function
    Public Function ActualizaProduccionProducto(ByVal sCodigo As String, ByVal dFechaProduccion As Date, ByVal iCantidad As Integer,
                                              ByVal nMontoGasto As Double, ByVal nValorCompra As Double, ByVal nPorcentajeGanancia As Double,
                                              ByVal nMontoGanancia As Double, ByVal nValorNeto As Double, ByVal nImpuesto As Double, ByVal nValorVenta As Double) As Boolean
        Dim sUpdate As String = ""

        Outiles.AddVarUpdate(sUpdate, "CANTIDAD", iCantidad, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "MONTO_GASTO", nMontoGasto, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "VALOR_COMPRA", nValorCompra, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "PORCENTAJE_GANANCIA", nPorcentajeGanancia, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "MONTO_GANANCIA", nMontoGanancia, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "VALOR_NETO", nValorNeto, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "IMPUESTO", nImpuesto, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "VALOR_VENTA", nValorVenta, "NUMBER")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update PRODUCCION_PRODUCTOS set " + sUpdate +
                  " where CODIGO_PRODUCTO = " + Outiles.GetSQLValue(sCodigo, "STRING") + " and " +
                  " FECHA_PRODUCCION = " + Outiles.GetSQLValue(dFechaProduccion, "DATE")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function




    Public Function ProducionProductos() As DataTable
        Dim sSQL As String
        sSQL = "select * from produccion_productos "
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function EliminaDetalleProduccionProducto(ByVal sCodigo As String, ByVal dFechaIngreso As Date) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from detalle_produccion_productos  " +
                  " where " +
                  " codigo_producto = " + Outiles.GetSQLValue(sCodigo, "STRING") + " and " +
                  "fecha_produccion =  " + Outiles.GetSQLValue(dFechaIngreso, "DATE")
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False

    End Function

    Public Function EliminaGastoProduccionProducto(ByVal sCodigo As String, ByVal dFechaIngreso As Date) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from detalle_gastos_producto  " +
                  " where " +
                  " codigo_producto = " + Outiles.GetSQLValue(sCodigo, "STRING") + " and " +
                  "fecha_produccion =  " + Outiles.GetSQLValue(dFechaIngreso, "DATE")
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False


    End Function

    Public Function EliminaProduccionProducto(ByVal sCodigo As String, ByVal dFechaIngreso As Date) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from produccion_productos  " +
                  " where " +
                  " codigo_producto = " + Outiles.GetSQLValue(sCodigo, "STRING") + " and " +
                  "fecha_produccion =  " + Outiles.GetSQLValue(dFechaIngreso, "DATE")
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False

    End Function

    Public Function ObtieneCantidadProducto(ByVal sCodigoProducto As String, ByVal dFechaProduccion As Date) As DataTable
        Dim sSQL As String
        sSQL = "select count(numero_producto) as cantidad from detalle_produccion_productos " +
               " where codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING") + " and " +
               " fecha_produccion = " + Outiles.GetSQLValue(dFechaProduccion, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
#End Region
#Region "Ventas"
    Public Function MaxNumeroVenta() As DataTable
        Dim sSQL As String
        sSQL = "select max(id_venta) as numero_venta from VENTAS"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function ObtieneProductosVenta() As DataTable
        Dim sSQL As String
        sSQL = " Select  p.codigo_producto as codigo, p.descripcion, pp.valor_venta " +
               " from productos p, produccion_productos pp " +
               " where p.codigo_producto = pp.codigo_producto And " +
               " p.proceso in ('V','P') AND " +
               " fecha_termino = " + Outiles.GetSQLValue(cConstantes.FECHA_TERMINO, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function IngresaDetalleVentasProduccion(ByVal nIDVenta As Integer, ByVal sCodigoProducto As String, ByVal nCantidad As Double,
                                               ByVal nValorProducto As Double) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "ID_VENTA", nIDVenta, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO", sCodigoProducto, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CANTIDAD", nCantidad, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "VALOR_VENTA_PRODUCTO", nValorProducto, "NUMBER")


        sInsert = "insert into detalle_ventas_produccion"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function ActualizaDetalleVentasProduccion(ByVal nIDVenta As Integer, ByVal sCodigoProducto As String, ByVal nCantidad As Double,
                                               ByVal nValorProducto As Double) As Boolean
        Dim sUpdate As String = ""

        Outiles.AddVarUpdate(sUpdate, "CANTIDAD", nCantidad, "NUMBER")
        Outiles.AddVarUpdate(sUpdate, "VALOR_VENTA_PRODUCTO", nValorProducto, "NUMBER")
        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update detalle_ventas_produccion set " + sUpdate +
                  " where ID_VENTA = " + Outiles.GetSQLValue(nIDVenta, "NUMBER") + " and " +
                  " CODIGO_PRODUCTO = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overloads Function ObtieneDetalleVentasProduccion(ByVal nIDVenta As Double) As DataTable
        Dim sSQL As String
        sSQL = " SELECT * FROM DETALLE_VENTAS_PRODUCCION " +
               " where id_venta = " + Outiles.GetSQLValue(nIDVenta, "NUMBER")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Overloads Function ObtieneDetalleVentasProduccion(ByVal nIDVenta As Double, ByVal sCodigoProducto As String) As DataTable
        Dim sSQL As String
        sSQL = " SELECT * FROM DETALLE_VENTAS_PRODUCCION " +
               " where id_venta = " + Outiles.GetSQLValue(nIDVenta, "NUMBER") + " and " +
               " codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Overloads Function EliminaDetalleVentasProduccion(ByVal nIDVenta As Double, ByVal sCodigoProducto As String) As Boolean
        Dim sSQL As String
        sSQL = "delete from DETALLE_VENTAS_PRODUCCION " +
               " where id_venta = " + Outiles.GetSQLValue(nIDVenta, "NUMBER") +
               " and codigo_producto = " + Outiles.GetSQLValue(sCodigoProducto, "STRING")
        If oConexion.ExecBool(sSQL) Then
            Return True
        End If
    End Function

    Public Overloads Function EliminaDetalleVentasProduccion(ByVal nIDVenta As Double) As Boolean
        Dim sSQL As String
        sSQL = "delete from DETALLE_VENTAS_PRODUCCION " +
               " where id_venta = " + Outiles.GetSQLValue(nIDVenta, "NUMBER")
        If oConexion.ExecBool(sSQL) Then
            Return True
        End If
    End Function


    Public Function IngresaVentas(ByVal nIDVenta As Integer, ByVal dFechaVenta As Date, ByVal sCodigoCliente As String,
                                              ByVal nTotalImpuesto As Double, ByVal nTotalProductos As Double,
                                  ByVal nMontoTotalNeto As Double, ByVal nMontoTotalVenta As Double) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        Dim sUpdate As String = ""


        Outiles.AddVarInsert(sColumns, sValues, "ID_VENTA", nIDVenta, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "FECHA_VENTA", dFechaVenta, "DATE")
        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_CLIENTE", sCodigoCliente, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "TOTAL_IMPUESTO", nTotalImpuesto, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "TOTAL_PRODUCTOS", nTotalProductos, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "MONTO_TOTAL_NETO", nMontoTotalNeto, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "MONTO_TOTAL_VENTA", nMontoTotalVenta, "NUMBER")
        sInsert = "insert into Ventas "
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta


    End Function
    Public Function ObtieneVenta(ByVal dFechaInicio As Date, ByVal dFechaTermino As Date) As DataTable
        Dim sSQL As String
        sSQL = "Select * from ventas " +
               " where fecha_venta >= " + Outiles.GetSQLValue(dFechaInicio, "DATE") + "and " +
               " fecha_venta <= " + Outiles.GetSQLValue(dFechaTermino, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function IngresaVentaDiaria(ByVal dFechaVenta As Date, ByVal nTotalProductoVentaDiaria As Double,
                                             ByVal nTotalVentaNetaDiaria As Double, ByVal nTotalVentaDiaria As Double) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        Dim sUpdate As String = ""


        Outiles.AddVarInsert(sColumns, sValues, "FECHA_VENTA", dFechaVenta, "DATE")
        Outiles.AddVarInsert(sColumns, sValues, "TOTAL_PRODUCTOS_VENTA_DIARIA", nTotalProductoVentaDiaria, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "TOTAL_VENTA_NETA_DIARIA", nTotalVentaNetaDiaria, "NUMBER")
        Outiles.AddVarInsert(sColumns, sValues, "TOTAL_VENTA_DIARIA", nTotalVentaDiaria, "NUMBER")
        sInsert = "insert into Venta_diaria "
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            sUpdate = "Update Venta_diaria set " + sUpdate +
                    "TOTAL_PRODUCTOS_VENTA_DIARIA =  TOTAL_PRODUCTOS_VENTA_DIARIA + " + Outiles.GetSQLValue(nTotalProductoVentaDiaria, "NUMBER") + "," +
                    "TOTAL_VENTA_NETA_DIARIA =  TOTAL_VENTA_NETA_DIARIA + " + Outiles.GetSQLValue(nTotalVentaNetaDiaria, "NUMBER") + "," +
                    "TOTAL_VENTA_DIARIA =  TOTAL_VENTA_DIARIA + " + Outiles.GetSQLValue(nTotalVentaDiaria, "NUMBER") +
                  " where FECHA_VENTA = " + Outiles.GetSQLValue(dFechaVenta, "DATE")
            If oConexion.ExecBool(sUpdate) Then
                Return True
            End If


        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function ObtieneVentaDiaria(ByVal dFechaInicioVenta As Date) As DataTable
        Dim sSQL As String
        sSQL = "Select * from venta_diaria " +
               " where fecha_venta >= " + Outiles.GetSQLValue(dFechaInicioVenta, "DATE")

        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function ObtieneVentaCliente(ByVal dFechaVenta As Date, ByVal dFechaTerminoVenta As Date, ByVal sCodigoCliente As String) As DataTable
        Dim sSQL As String
        sSQL = " Select * From ventas v, clientes c " +
               " Where v.CODIGO_CLIENTE = c.CODIGO And " +
               " v.FECHA_VENTA >= " + Outiles.GetSQLValue(dFechaVenta, "DATE") +
               " and  v.FECHA_VENTA <= " + Outiles.GetSQLValue(dFechaTerminoVenta, "DATE") +
               " and c.codigo = " + Outiles.GetSQLValue(sCodigoCliente, "STRING")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function ObtieneDetalleVenta(ByVal idVenta As Double) As DataTable
        Dim sSQL As String
        sSQL = " select * from detalle_ventas_produccion " +
               " where id_venta = " + Outiles.GetSQLValue(idVenta, "NUMBER")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Overloads Function EliminaVenta(ByVal nIDVenta As Double) As Boolean
        Dim sSQL As String
        sSQL = "delete from ventas " +
               " where id_venta = " + Outiles.GetSQLValue(nIDVenta, "NUMBER")
        If oConexion.ExecBool(sSQL) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Overloads Function EliminaDetalleVenta(ByVal nIDVenta As Double) As Boolean
        Dim sSQL As String
        sSQL = "delete from detalle_ventas_produccion " +
               " where id_venta = " + Outiles.GetSQLValue(nIDVenta, "NUMBER")
        If oConexion.ExecBool(sSQL) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Overloads Function EliminaVentaDiaria(ByVal dFechaVenta As Date) As Boolean
        Dim sSQL As String
        sSQL = "delete from ventas_diaria " +
               " where id_venta = " + Outiles.GetSQLValue(dFechaVenta, "DATE")
        If oConexion.ExecBool(sSQL) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Overloads Function ObtieneVentaID(ByVal nIDVenta As Double) As DataTable
        Dim sSQL As String
        sSQL = " SELECT * FROM ventas " +
               " where id_venta = " + Outiles.GetSQLValue(nIDVenta, "NUMBER")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Overloads Function ObtieneVentaFecha(ByVal dFechaVenta As Date) As DataTable
        Dim sSQL As String
        sSQL = " SELECT * FROM ventas " +
               " where fecha_venta = " + Outiles.GetSQLValue(dFechaVenta, "DATE")
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function ActualizaVentaDiaria(ByVal dFechaVenta As Date, ByVal nTotalProducto As Double, ByVal nTotalVenta As Double,
                                               ByVal nTotalNeto As Double) As Boolean
        Dim sUpdate As String = ""
        sUpdate = "Update venta_diaria set " +
                  "total_productos_venta_diaria = " + CStr(nTotalProducto) + "," +
                  "Total_Venta_Neta_Diaria =  " + CStr(nTotalNeto) + "," +
                  "Total_Venta_Diaria =   " + CStr(nTotalVenta) +
                  " where FECHA_VENTA = " + Outiles.GetSQLValue(dFechaVenta, "DATE")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ObtieneModificaVentaDiaria(ByVal dFechaInicioVenta As Date) As DataTable
        Dim sSQL As String
        sSQL = "Select * from venta_diaria " +
               " where fecha_venta = " + Outiles.GetSQLValue(dFechaInicioVenta, "DATE")

        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function
    Public Function ActualizaModVentaDiaria(ByVal dFechaVenta As Date, ByVal nTotalProductoVentaDiaria As Double,
                                             ByVal nTotalVentaNetaDiaria As Double, ByVal nTotalVentaDiaria As Double) As Boolean

        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        Dim sUpdate As String = ""
        sUpdate = "Update Venta_diaria set " + sUpdate +
                    "TOTAL_PRODUCTOS_VENTA_DIARIA = " + Outiles.GetSQLValue(nTotalProductoVentaDiaria, "NUMBER") + "," +
                    "TOTAL_VENTA_NETA_DIARIA = " + Outiles.GetSQLValue(nTotalVentaNetaDiaria, "NUMBER") + "," +
                    "TOTAL_VENTA_DIARIA = " + Outiles.GetSQLValue(nTotalVentaDiaria, "NUMBER") +
                  " where FECHA_VENTA = " + Outiles.GetSQLValue(dFechaVenta, "DATE")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If


    End Function
    Public Function ActualizaVenta(ByVal IdVenta As Integer, ByVal nTotalProducto As Double, ByVal nTotalVenta As Double,
                                   ByVal nTotalImpuesto As Double, ByVal nTotalNeto As Double) As Boolean
        Dim sUpdate As String = ""
        sUpdate = "Update ventas set " +
                  "total_productos = " + CStr(nTotalProducto) + "," +
                  "total_impuesto = " + CStr(nTotalImpuesto) + "," +
                  "monto_total_neto =  " + CStr(nTotalNeto) + "," +
                  "monto_total_venta =   " + CStr(nTotalVenta) +
                  " where id_Venta = " + Outiles.GetSQLValue(IdVenta, "INTEGER")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function

#End Region
#Region "Inventario"
    Public Function Inventario() As DataTable
        Dim sSQL As String
        sSQL = " select ip.codigo_producto,p.descripcion as descripcion_productos, " +
               " ip.tipo_unidad,tu.descripcion as descripcion_unidad,ip.cantidad " +
               " from inventario_productos_bodega ip, productos p, tipos_unidad tu " +
               " where ip.codigo_producto = p.codigo_producto and ip.tipo_unidad = tu.unidad"
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function DetalleInventario() As DataTable
        Dim sSQL As String
        sSQL = " select * FROM detalle_movimientos_inventario "
        If oConexion.OpenRecordSet(sSQL) Then
            Return oConexion.DT
        Else
            Return Nothing
        End If
    End Function

    Public Function IngresaDetalleInventario(ByVal dFechaIngreso As Date, ByVal sCodigo As String, ByVal sTipoUnidad As String, ByVal sTipoMovimiento As String,
                                            ByVal sObservacion As String, ByVal nCantidad As Double) As Boolean
        Dim sInsert As String = ""
        Dim sColumns As String = ""
        Dim sValues As String = ""
        Dim ds As DataSet = New DataSet
        Dim bRespuesta As Boolean = False
        ' Dim oConexion As New cConexion

        Outiles.AddVarInsert(sColumns, sValues, "FECHA_MOVIMIENTO", dFechaIngreso, "DATE")
        Outiles.AddVarInsert(sColumns, sValues, "CODIGO_PRODUCTO", sCodigo, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "TIPO_UNIDAD", sTipoUnidad, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "TIPO_MOVIMIENTO", sTipoMovimiento, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "OBSERVACION", sObservacion, "STRING")
        Outiles.AddVarInsert(sColumns, sValues, "CANTIDAD", nCantidad, "NUMBER")

        sInsert = "insert into Detalle_Movimientos_Inventario"
        sInsert = sInsert + " ( " + Outiles.NoTail(sColumns, ",") + " ) "
        sInsert = sInsert + " values ( " + Outiles.NoTail(sValues, ",") + " )"
        If oConexion.ExecBool(sInsert) = True Then
            bRespuesta = True
        Else
            'oWrite.EscribeLog(FalloInsercion, sInsert)
            bRespuesta = False
        End If
        Return bRespuesta
        If oConexion.ExecBool(sInsert) Then
            Return True
        End If
        Return False

    End Function

    Public Function ActualizaInventario(ByVal sCodigo As String, ByVal nMonto As Double, ByVal sTipoMovimiento As String) As Boolean
        Dim sUpdate As String = ""
        If sTipoMovimiento = cConstantes.MERMA Or sTipoMovimiento = cConstantes.ELIMINAR Then
            nMonto = nMonto * -1
        End If
        sUpdate = "update inventario_productos_bodega set " +
                   "cantidad =  cantidad + " + Str(nMonto) +
                   " where codigo_producto = '" + sCodigo + "'"
        If oConexion.ExecBool(sUpdate) Then
            Return True
        End If
        Return False
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Function ActualizaDetalleInventario(ByVal dFechaIngreso As Date, ByVal sCodigo As String, ByVal sTipoUnidad As String, ByVal sObservacion As String,
                                               ByVal nCantidad As Double, ByVal sTipoMovimiento As String) As Boolean
        Dim sUpdate As String = ""
        Outiles.AddVarUpdate(sUpdate, "OBSERVACION", sObservacion, "STRING")
        Outiles.AddVarUpdate(sUpdate, "TIPO_MOVIMIENTO", sTipoMovimiento, "STRING")
        Outiles.AddVarUpdate(sUpdate, "CANTIDAD", nCantidad, "NUMBER")

        sUpdate = Outiles.NoTail(sUpdate, ",")
        sUpdate = "Update detalle_movimientos_inventario set " + sUpdate +
                  " where fecha_movimiento = " + Outiles.GetSQLValue(dFechaIngreso, "DATE") +
                  " and codigo_producto = " + Outiles.GetSQLValue(sCodigo, "STRING")
        If oConexion.ExecBool(sUpdate) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function EliminaDetalleInventario(ByVal dFechaIngreso As Date, ByVal sCodigo As String) As Boolean
        Dim sDelete As String = ""
        sDelete = "delete from detalle_movimientos_inventario  " +
                  " where codigo_producto = '" + sCodigo + "'" +
                  " and fecha_movimiento = " + Outiles.GetSQLValue(dFechaIngreso, "DATE")
        If oConexion.ExecBool(sDelete) Then
            Return True
        End If
        Return False
    End Function


#End Region

End Class
