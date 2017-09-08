Imports cConexion
Imports System.ComponentModel
Public Class cProduccionProductos
    Inherits cProductos
    ' Implements IDataErrorInfo

    Private oCatalogoBD As New cCatalogoBaseDatos
    Private bValida As Boolean
    Private sMensaje As String
    Private nValorInventario As Double
    Private dFechaProduccion As Date
    Private sProceso As String
    Private nCantidadAnt As Double


    Public Property Mensaje
        Get
            Return sMensaje
        End Get
        Set(value)
            sMensaje = value
        End Set
    End Property
    Public Property FechaProduccion
        Get
            Return dFechaProduccion
        End Get
        Set(value)
            dFechaProduccion = value
        End Set
    End Property
    Public Property Proceso
        Get
            Return sProceso
        End Get
        Set(value)
            sProceso = value
        End Set
    End Property


    Public Function IngresaProduccionProducto() As Boolean
        Try
            If Not CuadraInventarioProduccion("A") Then Return False
            If ActualizaFechaProduccionProducto() Then
                CalculaValores()

                If oCatalogoBD.IngresaProduccionProducto(Codigo, FechaProduccion, Cantidad, Gasto,
                                                     ValorUnitario, PorcentajeGanancia, MontoGanancia, PrecioNeto, IVA, PrecioVenta) Then
                    Return True
                Else
                    Mensaje = "Atención. Error al ingresar producción"
                    Return False
                End If
            Else
                Mensaje = "Atención. Error al ingresar producción"
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error al ingresar  producción, descripción del error :" + ex.ToString
            Return False
        End Try


    End Function

    Private Function CuadraInventarioProduccion(ByVal sTipoProceso As String) As Boolean
        Dim tbProductos As New DT
        Dim tbProduccion As New DT
        Dim nTotalOriginal As Double = 0
        Dim nNuevoTotal As Double = 0
        Dim oUtiles As New cUtiles

        Try
            If sProceso = "P" Then
                tbProductos.RecordSet = oCatalogoBD.InventarioProduccionProductos(Codigo, FechaProduccion)
                If tbProductos.EOF = True Then
                    oErrores.ConError = True
                    oErrores.Mensaje = "Atencion, no existe productos ingresados en inventario para producto =" + Codigo + "."
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                End If
                Do While tbProductos.EOF = False
                    nTotalOriginal = 0
                    nNuevoTotal = 0
                    oConversion.Unidad = tbProductos.GetValue("tipo_unidad_inventario")
                    oConversion.Cantidad = tbProductos.GetValue("cantidad_inventario")
                    nTotalOriginal = nTotalOriginal + oConversion.RealizaConversion
                    If Cantidad > 0 Then
                        oConversion.Unidad = tbProductos.GetValue("tipo_unidad_detalle")
                        oConversion.Cantidad = Cantidad * tbProductos.GetValue("cantidad_detalle")
                        nNuevoTotal = oConversion.RealizaConversion()
                        CodigoProductoBodega = tbProductos.GetValue("codigo_producto_bodega")
                    End If

                    If sTipoProceso = "E" Then
                        nNuevoTotal = nTotalOriginal + nNuevoTotal
                    ElseIf sTipoProceso = "M" Then
                        tbProduccion.RecordSet = oCatalogoBD.ProducionProductos(Codigo, CodigoProductoBodega, FechaProduccion)
                        If tbProduccion.EOF = False Then
                            nCantidadAnt = tbProduccion.GetValue("cantidad")
                            oConversion.Unidad = tbProduccion.GetValue("tipo_unidad")
                            oConversion.Cantidad = nCantidadAnt * tbProduccion.GetValue("total_productos")
                            nCantidadAnt = oConversion.RealizaConversion()

                        End If
                        tbProduccion.Disconnect()

                        If nCantidadAnt > nNuevoTotal Then
                            nNuevoTotal = nTotalOriginal + nCantidadAnt - nNuevoTotal
                        Else
                            nNuevoTotal = nTotalOriginal - nNuevoTotal + nCantidadAnt
                        End If
                    Else
                        nNuevoTotal = nTotalOriginal - nNuevoTotal
                    End If
                    If nNuevoTotal < 0 Then
                        oErrores.ConError = True
                        oErrores.Mensaje = "Atencion, no existe cantidad de producto en inventario" + Codigo
                        oErrores.AgregaError()
                        oErrores.NumError = oErrores.NumError + 1

                    End If
                    oCatalogoBD.ActualizaInventarioProductosBodega(CodigoProductoBodega, oConversion.UnidadConvercion, nNuevoTotal)
                    tbProductos.MoveNext()
                Loop
                tbProductos.Disconnect()
            Else

                tbProductos.RecordSet = oCatalogoBD.InventarioProduccionVenta(Codigo)
                If tbProductos.EOF = False Then
                    nTotalOriginal = 0
                    nNuevoTotal = 0
                    If sTipoProceso = "A" Then
                        If Cantidad > 0 Then
                            nNuevoTotal = tbProductos.GetValue("cantidad") - Cantidad
                            CodigoProductoBodega = tbProductos.GetValue("codigo_producto")
                            oCatalogoBD.ActualizaInventarioProductosBodega(CodigoProductoBodega, tbProductos.GetValue("tipo_unidad"), nNuevoTotal)
                        End If

                    ElseIf sTipoProceso = "M" Then
                        nCantidadAnt = oUtiles.SQLGet("cantidad", "Produccion_productos", "codigo_producto = " + oUtiles.GetSQLValue(Codigo, "STRING") + " and fecha_produccion = " + oUtiles.GetSQLValue(FechaProduccion, "DATE"), "", Nothing)
                        nTotalOriginal = tbProductos.GetValue("cantidad")
                        nNuevoTotal = Cantidad
                        If nCantidadAnt > nNuevoTotal Then
                            nNuevoTotal = nTotalOriginal + nCantidadAnt - nNuevoTotal
                        Else
                            nNuevoTotal = nTotalOriginal - nNuevoTotal + nCantidadAnt
                        End If

                        CodigoProductoBodega = tbProductos.GetValue("codigo_producto")
                        oCatalogoBD.ActualizaInventarioProductosBodega(CodigoProductoBodega, tbProductos.GetValue("tipo_unidad"), nNuevoTotal)

                    Else
                        If Cantidad > 0 Then
                            nNuevoTotal = tbProductos.GetValue("cantidad") + Cantidad
                            CodigoProductoBodega = tbProductos.GetValue("codigo_producto")
                            oCatalogoBD.ActualizaInventarioProductosBodega(CodigoProductoBodega, tbProductos.GetValue("tipo_unidad"), nNuevoTotal)
                        End If


                    End If

                End If
            End If
            Return True
        Catch ex As Exception

        End Try
    End Function
    Public Sub CalculaValores()
        MontoGanancia = ((ValorUnitario * PorcentajeGanancia) / 100)
        PrecioNeto = Math.Round(ValorUnitario + Gasto + MontoGanancia + (ValorUnitario * (Transbank / 100)), 0)
        IVA = Math.Round(PrecioNeto * (oParametros.PorcentajeIVA / 100), 0)
        PrecioVenta = PrecioNeto + IVA
    End Sub

    Public Function ProduccionProducto() As DT
        Dim tbProduccionProducto As New DT
        Try
            tbProduccionProducto.RecordSet = oCatalogoBD.ProducionProductos()
            Return tbProduccionProducto
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ActualizaProduccion() As Boolean
        Try
            If Not VerificaFechaTermino() Then Return False
            If Not CuadraInventarioProduccion("M") Then Return False
            CalculaValores()
            If oCatalogoBD.ActualizaProduccionProducto(Codigo, FechaProduccion, Cantidad, Gasto,
                                                 ValorUnitario, PorcentajeGanancia, MontoGanancia, PrecioNeto, IVA, PrecioVenta) Then
                Return True
            Else
                Mensaje = " Atención. Error al actualizar datos de producción, revise si código es unico"
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al actualizar datos de producción, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function
    Public Function EliminaProduccionProducto() As Boolean
        Try
            If Not VerificaFechaTermino() Then
                Return False
            End If
            If Not CuadraInventarioProduccion("E") Then Return False
            If oCatalogoBD.EliminaDetalleProduccionProducto(Codigo, FechaProduccion) Then
                If oCatalogoBD.EliminaGastoProduccionProducto(Codigo, FechaProduccion) Then
                    If oCatalogoBD.EliminaProduccionProducto(Codigo, FechaProduccion) Then
                        ActulizaUltimaFechaTermino()
                    End If
                End If
                Return True
            Else
                Mensaje = " Atención. Error al eliminar producción"
                Return False
            End If
        Catch ex As Exception
            Mensaje = " Atención. Error al eliminar producción, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function

    Public Function CantidadProduccionProducto() As Double
        Dim tbProduccionProducto As New DT
        Try
            tbProduccionProducto.RecordSet = oCatalogoBD.ObtieneCantidadProducto(Codigo, FechaProduccion)
            If tbProduccionProducto.EOF = False Then
                Return tbProduccionProducto.GetValue("cantidad")
            Else
                Return 0
            End If
        Catch ex As Exception
            Return 0
        End Try
    End Function
    Public Function EliminaProducionProducto() As Boolean
        Try
            If oCatalogoBD.EliminaProduccionProducto(NumeroProducto, Codigo, FechaIngreso) Then
                Return True
            Else
                Mensaje = " Atención. Error al eliminar detalle"
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al eliminar detalle, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function
    Public Function ActualizaProduccionProducto() As Boolean
        Try
            If oCatalogoBD.ActualizaProduccionProducto(NumeroProducto, Codigo, CodigoProductoBodega, FechaIngreso, Cantidad1, TipoUnidad1) Then
                Return True
            Else
                Mensaje = " Atención. Error al actualizar detalle"
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al actualizar detalle, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function
    Public Sub ObtieneNumeroProduccionProducto()

        Dim tbAux As New DT
        Try
            tbAux.DT = oCatalogoBD.ObtieneNumeroProducto(Codigo, FechaIngreso)
            If tbAux.EOF = False Then
                NumeroProducto = tbAux.GetValue("numero")
            End If
            NumeroProducto = NumeroProducto + 1
        Catch ex As Exception

        End Try
    End Sub
    Public Function IngresaProduccionProductos() As Boolean
        Try
            If oCatalogoBD.IngresaProduccionProductos(Codigo, CodigoProductoBodega, NumeroProducto, FechaIngreso, TipoUnidad1, Cantidad1) Then
                Return True
            Else
                Mensaje = "Atención. Error al ingresar detalle."
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error al ingresar  detalle, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function
    Public Function DetalleProduccionProductos() As DT
        Dim tbProductos As New DT
        Try
            tbProductos.RecordSet = oCatalogoBD.DetalleProducionProductos(Codigo, FechaIngreso)
            Return tbProductos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function ActualizaFechaProduccionProducto() As Boolean
        Try
            Dim tbFecha As New DT
            Dim dUltimaFecha As Date = Nothing
            Dim dFechaTermino As Date
            tbFecha.RecordSet = oCatalogoBD.ObtieneUltimaFechaProduccionProducto(Codigo)
            If tbFecha.EOF = False Then
                dUltimaFecha = tbFecha.GetValue("fecha_produccion")
            End If
            If Not IsNothing(dUltimaFecha) Then
                dFechaTermino = DateAdd(DateInterval.Day, -1, Today)
                If oCatalogoBD.ActualizaFechaProduccionProducto(Codigo, dUltimaFecha, dFechaTermino) Then
                    Return True
                Else
                    Return False
                End If
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function VerificaFechaTermino() As Boolean
        Dim dfechaTermino As Date = Nothing
        Dim tbFechaTermino As New DT
        tbFechaTermino.RecordSet = oCatalogoBD.VerificaFechaTermino(Codigo, FechaProduccion)
        If tbFechaTermino.EOF = False Then
            dfechaTermino = tbFechaTermino.GetValue("fecha_termino")
            If Year(dfechaTermino) <> Year(CDate(cConstantes.FECHA_TERMINO)) Then
                Mensaje = "Solo es posible modificar/eliminar última vigencia"
                Return False
            Else
                Return True
            End If
        Else
            Mensaje = "Solo es posible modificar/eliminar última vigencia"
            Return False
        End If
    End Function
    Private Function ActulizaUltimaFechaTermino() As Boolean
        Dim dFechaIngresoAnterior As Date
        dFechaIngresoAnterior = DateAdd(DateInterval.Day, -1, FechaProduccion)
        If oCatalogoBD.ActulizaUltimaFechaTermino(Codigo, dFechaIngresoAnterior) Then
            Return True
        Else
            Return False
        End If

    End Function

End Class
