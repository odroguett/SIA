Imports cConexion
Imports System.ComponentModel
Public Class cProductosBodega
    Inherits cProductos
    Implements IDataErrorInfo
    Private oCatalogoBD As New cCatalogoBaseDatos
    Private oInventario As New cInventario
    Private nValorInventario As Double

    Public Function ProductosDetalleBodega() As DT
        Dim tbProductos As New DT
        Try
            tbProductos.RecordSet = oCatalogoBD.ProductosDetalleBodega
            Return tbProductos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function IngresaProductosBodega() As Boolean
        Dim nValorInventario As Double
        Try
            If oCatalogoBD.IngresaProductosBodega(NumeroIngreso, TipoProducto, Valor, ValorUnitario, ValorUnCompra, TotalProductos) Then
                nValorInventario = CuadraInventario()
                If Cantidad1 > 0 Then
                    oCatalogoBD.IngresaDesgloseProductosBodega(NumeroIngreso, Codigo, TipoUnidad1, Cantidad1)


                End If
                If Cantidad2 > 0 Then
                    oCatalogoBD.IngresaDesgloseProductosBodega(NumeroIngreso, Codigo, TipoUnidad2, Cantidad2)

                End If
                If Cantidad3 > 0 Then
                    oCatalogoBD.IngresaDesgloseProductosBodega(NumeroIngreso, Codigo, TipoUnidad3, Cantidad3)

                End If

                oCatalogoBD.IngresaInventarioProductosBodega(Codigo, oConversion.UnidadConvercion, nValorInventario)
                Return True
            Else
                Mensaje = "Atención. Error al ingresar producto."
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error al ingresar  producto, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function


    Public Function CargaDetalleProductosBodega(Optional sCodigoProducto As String = "") As DT
        Dim tbProductos As New DT
        Try
            tbProductos.RecordSet = oCatalogoBD.DetalleProductosBodega(NumeroIngreso, sCodigoProducto)
            Return tbProductos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function EliminaDetalleProductoBodega() As Boolean

        Try
            nValorInventario = CuadraInventario()
            If oCatalogoBD.EliminaDesgloseProductosBodega(NumeroIngreso, Codigo) Then
                oCatalogoBD.EliminaDetalleProductosBodega(NumeroIngreso, Codigo)
                oCatalogoBD.EliminaProductosInventarioBodega(Codigo, oConversion.UnidadConvercion, nValorInventario)
                Return True
            Else
                Mensaje = " Atención. Error al eliminar compra"
                Return False
            End If
        Catch ex As Exception
            Mensaje = " Atención. Error al eliminar compra, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function

    Public Function EliminaProductoBodega() As Boolean
        Dim sCodigoProducto As String
        Dim tbDesglose As New DT

        Try
            tbDesglose.RecordSet = oCatalogoBD.DesgloseProductoBodega(NumeroIngreso)
            Do While tbDesglose.EOF = False
                sCodigoProducto = tbDesglose.GetValue("codigo_producto")
                nValorInventario = CuadraInventario()
                If oCatalogoBD.EliminaDesgloseProductosBodega(NumeroIngreso, sCodigoProducto) Then
                    oCatalogoBD.EliminaDetalleProductosBodega(NumeroIngreso, sCodigoProducto)
                    oCatalogoBD.EliminaProductosInventarioBodega(sCodigoProducto, oConversion.UnidadConvercion, nValorInventario)
                   
                Else
                    Mensaje = " Atención. Error al eliminar compra"
                    Return False
                End If
                tbDesglose.MoveNext()
            Loop
            Return True
        Catch ex As Exception
            Mensaje = " Atención. Error al eliminar compra, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function

    Public Function ActualizaDetalleProductoBodega(Optional ByVal sCodigoProducto As String = "") As Boolean
        Try
            If oCatalogoBD.ActualizaDetalleProductosBodega(NumeroIngreso, sCodigoProducto) Then

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function ActualizaProductoBodega() As Boolean

        Try

            If Not oCatalogoBD.ActualizaProductoBodega(NumeroIngreso, TipoProducto, Valor, ValorUnitario, ValorUnCompra, TotalProductos) Then
                Mensaje = " Atención. Error al actualizar producto en bodega, revise si código es unico"
                Return False
            Else
                nValorInventario = CuadraInventario()
                If Cantidad1 > 0 Then
                    oCatalogoBD.ActualizaDesgloseProductosBodega(NumeroIngreso, Codigo, TipoUnidad1, Cantidad1)
                End If
                If Cantidad2 > 0 Then
                    oCatalogoBD.ActualizaDesgloseProductosBodega(NumeroIngreso, Codigo, TipoUnidad2, Cantidad2)
                End If
                If Cantidad3 > 0 Then
                End If
                oCatalogoBD.IngresaInventarioProductosBodega(Codigo, oConversion.UnidadConvercion, nValorInventario)
                Return True
            End If


        Catch ex As Exception
            Mensaje = " Atención. Error al actualizar datos del producto, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function

    Public Function CargaDesgloseProductoBodega() As DT
        Dim tbProductosUnidad As New DT
        Try
            tbProductosUnidad.RecordSet = oCatalogoBD.CargaDesgloseProductoBodega(Codigo, NumeroIngreso)
            Return tbProductosUnidad
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CalculaValorUnitarioCompra() As Double
        ValorUnCompra = Math.Round(Valor / TotalProductos, 0)
        Return ValorUnCompra
    End Function

    Public Function CalculaValorUnitario() As Double
        ValorUnitario = Math.Round(Valor / Cantidad1, 0)
        Return ValorUnitario
    End Function
    Public Function CargaProductosBodega() As DT
        Dim tbProductos As New DT
        Try
            tbProductos.RecordSet = oCatalogoBD.CargaProductosBodega()
            Return tbProductos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CuadraInventario() As Double
        Dim tbProductos As New DT
        Dim nTotalOriginal As Double = 0
        Dim nNuevoTotal As Double = 0
        Try
            tbProductos.RecordSet = oCatalogoBD.CargaDatosBodegaInventario(NumeroIngreso, Codigo)
            Do While tbProductos.EOF = False
                oConversion.Unidad = tbProductos.GetValue("tipo_unidad_bodega")
                oConversion.Cantidad = tbProductos.GetValue("cantidad_bodega")
                nTotalOriginal = nTotalOriginal + oConversion.RealizaConversion
                tbProductos.MoveNext()
            Loop
            If Cantidad1 > 0 Then
                oConversion.Unidad = TipoUnidad1
                oConversion.Cantidad = Cantidad1
                nNuevoTotal = oConversion.RealizaConversion()
            End If
            If Cantidad2 > 0 Then
                oConversion.Unidad = TipoUnidad2
                oConversion.Cantidad = Cantidad2
                nNuevoTotal = nNuevoTotal + oConversion.RealizaConversion()
            End If
            If Cantidad3 > 0 Then
                oConversion.Unidad = TipoUnidad3
                oConversion.Cantidad = Cantidad3
                nNuevoTotal = nNuevoTotal + oConversion.RealizaConversion()
            End If
            If nTotalOriginal = 0 Then Return nNuevoTotal
            nNuevoTotal = nNuevoTotal - nTotalOriginal
            Return nNuevoTotal


        Catch ex As Exception
            Return 0
        End Try
    End Function


End Class
