Imports System.Data
Imports cConexion
Imports System.ComponentModel
Public Class cProductos
    Implements IDataErrorInfo

    Private sCodigo As String
    Private sCodigoProductoBodega As String
    Private sDescripcionProducto As String
    Private sDescripcionUnidad As String
    Private sDescripcion As String
    Private sTipoUnidad1 As String
    Private sTipoUnidad2 As String
    Private sTipoUnidad3 As String
    Private sTipoUnidadInventario As String
    Private sGrupoProducto As String
    Private nCantidad1 As Double
    Private nCantidad2 As Double
    Private nCantidad3 As Double
    Private nVenta As Double
    Private nGasto As Double
    Private nBruto As Double
    Private nIva As Double
    Private nValor As Double
    Private oCatalogoBD As New cCatalogoBaseDatos
    Private sMensaje As String
    Private bValida As Boolean
    Private oUtiles As New cUtiles
    Private sTipoProceso As String
    Private nNumeroIngreso As Double
    Private sGrupoUnidad As String
    Private nValorUnitario As Double
    Private nTotalProductos As Double
    Private iNumeroProducto As Integer
    Private dFechaIngreso As Date
    Private nValorUNCompra As Double
    Private iGranel As Integer
    Private sTipoProducto As String
    Private nMovimiento As Double
    Private nPrecioNeto As Double
    Private nCantidad As Double
    Private nTransbank As Double
    Private nPrecioVenta As Double
    Private nPorcentajeGanancia As Double
    Private nMontoGanancia As Double
    Public Const BODEGA = "B"
    Public Const VENTA = "V"
    Public Const PRODUCCION = "P"
    Public Const GRUPO_MASAS = 1
    Public Const GRUPO_VOLUMEN = 2
    Public Const GRUPO_UNIDAD = 3


    Public Sub New()
        oErrores.InicializaErrores()
    End Sub

    Public Property GrupoProducto
        Get
            Return sGrupoProducto
        End Get
        Set(value)
            sGrupoProducto = value
        End Set
    End Property

    Public Property TipoProducto
        Get
            Return sTipoProducto
        End Get
        Set(value)
            sTipoProducto = value
        End Set
    End Property


    Public Property GrupoTipoUnidad
        Get
            Return sGrupoUnidad
        End Get
        Set(value)
            sGrupoUnidad = value
        End Set
    End Property

    Public Property TipoProceso
        Get
            Return sTipoProceso
        End Get
        Set(value)
            sTipoProceso = value
        End Set
    End Property


    Public Property Codigo
        Get
            Return sCodigo
        End Get
        Set(value)
            sCodigo = value
        End Set
    End Property

    Public Property Mensaje
        Get
            Return sMensaje
        End Get
        Set(value)
            sMensaje = value
        End Set
    End Property


    Public Property CodigoProductoBodega
        Get
            Return sCodigoProductoBodega
        End Get
        Set(value)
            sCodigoProductoBodega = value
        End Set
    End Property

    Public Property Descripcion
        Get
            Return sDescripcion
        End Get
        Set(value)
            sDescripcion = value
        End Set
    End Property


    Public Property TipoUnidad1
        Get
            Return sTipoUnidad1
        End Get
        Set(value)
            sTipoUnidad1 = value
        End Set
    End Property

    Public Property TipoUnidad2
        Get
            Return sTipoUnidad2
        End Get
        Set(value)
            sTipoUnidad2 = value
        End Set
    End Property
    Public Property TipoUnidad3
        Get
            Return sTipoUnidad3
        End Get
        Set(value)
            sTipoUnidad3 = value
        End Set
    End Property

    Public Property TipoUnidadInventario
        Get
            Return sTipoUnidadInventario
        End Get
        Set(value)
            sTipoUnidadInventario = value
        End Set
    End Property

    Public Property Cantidad1
        Get
            Return nCantidad1
        End Get
        Set(value)
            Try
                nCantidad1 = value

            Catch ex As Exception
                nCantidad1 = 0

            End Try
        End Set
    End Property

    Public Property Cantidad2
        Get
            Return nCantidad2
        End Get
        Set(value)
            Try
                nCantidad2 = value

            Catch ex As Exception
                nCantidad2 = 0

            End Try
        End Set
    End Property

    Public Property Cantidad3
        Get
            Return nCantidad3
        End Get
        Set(value)
            Try
                nCantidad3 = value

            Catch ex As Exception
                nCantidad3 = 0

            End Try
        End Set
    End Property

    Public Property Gasto
        Get
            Return nGasto
        End Get
        Set(value)
            Try
                nGasto = value
            Catch ex As Exception
                nGasto = 0
            End Try

        End Set
    End Property

    Public Property PrecioBruto
        Get
            Return nBruto
        End Get
        Set(value)
            Try
                nBruto = value
            Catch ex As Exception
                nBruto = 0
            End Try

        End Set
    End Property

    Public Property Valor


        Get
            Return nValor
        End Get
        Set(value)
            Try
                nValor = value
            Catch ex As Exception
                nValor = 0
            End Try

        End Set
    End Property

    Public Property NumeroIngreso
        Get
            Return nNumeroIngreso
        End Get
        Set(value)
            Try
                nNumeroIngreso = value
            Catch ex As Exception
                nNumeroIngreso = 0
            End Try

        End Set
    End Property

    Public Property TotalProductos
        Get
            Return nTotalProductos
        End Get
        Set(value)
            Try
                nTotalProductos = value
            Catch ex As Exception
                nTotalProductos = 0
            End Try

        End Set
    End Property


    Public Property ValorUnitario
        Get
            Return nValorUnitario
        End Get
        Set(value)
            Try
                nValorUnitario = value
            Catch ex As Exception
                nValorUnitario = 0
            End Try

        End Set
    End Property

    Public Property NumeroProducto
        Get
            Return iNumeroProducto
        End Get
        Set(value)
            iNumeroProducto = value
        End Set
    End Property

    Public Property FechaIngreso
        Get
            Return dFechaIngreso
        End Get
        Set(value)
            dFechaIngreso = value
        End Set
    End Property


    Public Property ValorUnCompra
        Get
            Return nValorUNCompra
        End Get
        Set(value)
            Try
                nValorUNCompra = value
            Catch ex As Exception
                nValorUNCompra = 0
            End Try

        End Set
    End Property

    Public Property Granel
        Get
            Return iGranel
        End Get
        Set(value)
            Try
                iGranel = value
            Catch ex As Exception
                iGranel = 0
            End Try

        End Set
    End Property

    Public Property DescripcionProducto
        Get
            Return sDescripcionProducto
        End Get
        Set(value)
            sDescripcionProducto = value

        End Set
    End Property
    Public Property DescripcionUnidad
        Get
            Return sDescripcionUnidad
        End Get
        Set(value)
            sDescripcionUnidad = value
        End Set
    End Property
    Public Property Movimiento
        Get
            Return nMovimiento
        End Get
        Set(value)
            Try
                nMovimiento = value
            Catch ex As Exception
                nMovimiento = 0
            End Try

        End Set
    End Property
    Public Property PrecioNeto
        Get
            Return nPrecioNeto
        End Get
        Set(value)
            Try
                nPrecioNeto = value
            Catch ex As Exception
                nPrecioNeto = 0
            End Try

        End Set
    End Property
    Public Property IVA
        Get
            Return nIva
        End Get
        Set(value)
            Try
                nIva = value
            Catch ex As Exception
                nIva = 0
            End Try

        End Set
    End Property
    Public Property Transbank
        Get
            Return nTransbank
        End Get
        Set(value)
            Try
                nTransbank = value
            Catch ex As Exception
                nTransbank = 0
            End Try

        End Set
    End Property
    Public Property PrecioVenta
        Get
            Return nPrecioVenta
        End Get
        Set(value)
            Try
                nPrecioVenta = value
            Catch ex As Exception
                nPrecioVenta = 0
            End Try

        End Set
    End Property
    Public Property PorcentajeGanancia
        Get
            Return nPorcentajeGanancia
        End Get
        Set(value)
            Try
                nPorcentajeGanancia = value
            Catch ex As Exception
                nPorcentajeGanancia = 0
            End Try

        End Set
    End Property


    Public Property MontoGanancia
        Get
            Return nMontoGanancia
        End Get
        Set(value)
            Try
                nMontoGanancia = value
            Catch ex As Exception
                nMontoGanancia = 0
            End Try

        End Set
    End Property
    Public Property Cantidad
        Get
            Return nCantidad
        End Get
        Set(value)
            Try
                nCantidad = value
            Catch ex As Exception
                nCantidad = 0
            End Try

        End Set
    End Property

    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            sMensaje = isValid(columnName)
            Return sMensaje

        End Get
    End Property

    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Property Valida
        Get
            Return bValida
        End Get
        Set(value)
            bValida = True
        End Set

    End Property

    Private Function isValid(ByVal sNombre As String) As String

        Select Case sNombre
            Case "Codigo"
                If String.IsNullOrEmpty(sCodigo) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.CodigoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.CodigoError
                End If

            Case "Cantidad1"
                If Not IsNumeric(nCantidad1) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If
                If nCantidad1 <= 0 Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NegativoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NegativoError
                End If
            Case "Cantidad2"
                If Not IsNumeric(nCantidad2) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If

            Case "Cantidad3"
                If Not IsNumeric(nCantidad3) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If
            Case "Valor"
                If Not IsNumeric(nValor) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If

            Case "Descripcion"
                If sDescripcion = "" Then
                    bValida = False
                    oErrores.Mensaje = cErrores.DescripcionProductoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.DescripcionProductoError
                End If
            Case "Movimiento"
                If Not IsNumeric(nMovimiento) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If
                If nMovimiento <= 0 Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NegativoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NegativoError
                End If
            Case "ValorUnitario"
                If String.IsNullOrEmpty(ValorUnitario) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.CodigoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.CodigoError
                End If
                If ValorUnitario <= 0 Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NegativoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NegativoError
                End If

            Case "Cantidad"
                If Not IsNumeric(Cantidad) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If
                If Cantidad <= 0 Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NegativoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NegativoError
                End If
            Case "Gasto"
                If Not IsNumeric(Gasto) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If

            Case "PorcentajeGanancia"
                If Not IsNumeric(PorcentajeGanancia) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If
                If PorcentajeGanancia <= 0 Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NegativoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NegativoError
                End If
            Case "PrecioNeto"
                If Not IsNumeric(PrecioNeto) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If

            Case "IVA"
                If Not IsNumeric(IVA) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If

            Case "Transbank"
                If Not IsNumeric(Transbank) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If

            Case "PrecioVenta"
                If Not IsNumeric(PrecioVenta) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If


        End Select

        Return Nothing
    End Function
    Public Function CargaProductos() As DT
        Dim tbProductos As New DT
        Try
            tbProductos.RecordSet = oCatalogoBD.Productos
            Return tbProductos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function IngresaProductos() As Boolean
        Try
            If oCatalogoBD.IngresaProductos(sCodigo, sDescripcion, sTipoProducto, sGrupoProducto, sTipoProceso, iGranel) Then
                If nCantidad1 > 0 Then
                    oCatalogoBD.IngresaTiposUnidadProducto(sCodigo, sTipoUnidad1, nCantidad1)

                End If
                If nCantidad2 > 0 Then
                    oCatalogoBD.IngresaTiposUnidadProducto(sCodigo, sTipoUnidad2, nCantidad2)

                End If
                If nCantidad3 > 0 Then
                    oCatalogoBD.IngresaTiposUnidadProducto(sCodigo, sTipoUnidad3, nCantidad3)

                End If
                Return True
            Else
                Mensaje = "Atención. Error al ingresar producto, revise duplicidad de código."
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error al ingresar  producto, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function
    Public Function ActualizaProducto() As Boolean
        Try
            If oCatalogoBD.ActualizaProducto(sCodigo, sDescripcion, sGrupoProducto, sTipoProducto, sTipoProceso, iGranel) Then
                If nCantidad1 > 0 Then
                    oCatalogoBD.ActualizaTiposUnidadProducto(sCodigo, sTipoUnidad1, nCantidad1)
                End If
                If nCantidad2 > 0 Then
                    oCatalogoBD.ActualizaTiposUnidadProducto(sCodigo, sTipoUnidad2, nCantidad2)
                End If
                If nCantidad3 > 0 Then
                    oCatalogoBD.ActualizaTiposUnidadProducto(sCodigo, sTipoUnidad3, nCantidad3)
                End If
                Return True
            Else
                Mensaje = " Atención. Error al actualizar datos , revise si código es unico"
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al actualizar datos del cliente, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function
    Public Function EliminaProducto() As Boolean
        Try
            If oCatalogoBD.EliminaProducto(sCodigo) Then
                oCatalogoBD.EliminaTiposUnidadProducto(sCodigo)
                Return True
            Else
                Mensaje = " Atención. Error al eliminar producto"
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al eliminar producto, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function

    Private Function RealizarConversion() As Double
        Dim nValor As Double

        Return nValor
    End Function


    Public Function ValorCompra() As Double
        Dim nValorCompra As Double
        Dim tbValorCompra As New DT
        Try
            tbValorCompra.RecordSet = oCatalogoBD.ValorCompra(nNumeroIngreso)
            If tbValorCompra.EOF = False Then
                nValorCompra = tbValorCompra.GetValue("valor_compra")
                If oCatalogoBD.ActualizaValorCompra(nNumeroIngreso, nValorCompra) Then
                End If
            End If
            Return nValorCompra
        Catch ex As Exception
            Return 0
        End Try
    End Function


    Public Function CargaTipoUnidadProducto() As DT
        Dim tbProductosUnidad As New DT
        Try
            tbProductosUnidad.RecordSet = oCatalogoBD.CargaTiposUnidadProducto(sCodigo)
            Return tbProductosUnidad
        Catch ex As Exception
            Return Nothing
        End Try
    End Function



    Public Function ActualizaTipoUnidadProducto() As Boolean
        Dim nCantidad As Double
        Try
            oCatalogoBD.ActualizaTiposUnidadProducto(sCodigo, sTipoUnidad1, nCantidad)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EliminaTipoUnidadProducto() As Boolean

        Try
            oCatalogoBD.EliminaTiposUnidadProducto(sCodigo, sTipoUnidad1)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function IngresaTipoGrupo() As Boolean
        Try
            If oCatalogoBD.IngresaTipoGrupo(sCodigo, sDescripcion) Then

                Return True
            Else
                sMensaje = "Código de tipo de grupo invalido."
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ActualizaTipoGrupo() As Boolean
        Try
            oCatalogoBD.ActualizaTipoGrupo(sCodigo, sDescripcion)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EliminaTipoGrupo() As Boolean
        Try
            oCatalogoBD.EliminaTipoGrupo(sCodigo)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CargaProduccionProducto() As DT
        Dim tbProduccionUnidad As New DT
        Try
            tbProduccionUnidad.RecordSet = oCatalogoBD.CargaProduccionProductos(Codigo)
            Return tbProduccionUnidad
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CargaCBProduccionProducto() As DT
        Dim tbProduccionUnidad As New DT
        Try
            tbProduccionUnidad.RecordSet = oCatalogoBD.CargaCBProduccionProductos()
            Return tbProduccionUnidad
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Sub CargaValorUnitarioProducto()
        Dim tbValorUnitario As New DT
        Try
            tbValorUnitario.RecordSet = oCatalogoBD.CargaValorUnitarioProducto(Codigo)
            If tbValorUnitario.EOF = False Then
                ValorUnitario = tbValorUnitario.GetValue("valor_unitario")
            Else
                ValorUnitario = 0
            End If

        Catch ex As Exception

        End Try
    End Sub



    Public Function ProcesoProductos(ByVal sCodigoProducto As String) As String
        Dim sProceso As String
        sProceso = oUtiles.SQLGet("proceso", "productos", "codigo_producto = " + oUtiles.GetSQLValue(sCodigoProducto, "STRING"), "", "")
        Return sProceso
    End Function
    Public Function ValidaCB() As Boolean
        If TipoProducto = "" Then
            oErrores.Mensaje = cErrores.TipoProductoError
            oErrores.AgregaError()
            oErrores.NumError = oErrores.NumError + 1
            Return False
        End If
        If TipoUnidad1 = "" Then
            oErrores.Mensaje = cErrores.TipoUnidad1Error
            oErrores.AgregaError()
            oErrores.NumError = oErrores.NumError + 1
            Return False
        End If
        If nCantidad2 > 0 And (TipoUnidad2 = "" Or IsNothing(sTipoUnidad2)) Then
            oErrores.Mensaje = cErrores.TipoUnidad2Error
            oErrores.AgregaError()
            oErrores.NumError = oErrores.NumError + 1
            Return False
        End If
        If nCantidad3 > 0 And (TipoUnidad3 = "" Or IsNothing(sTipoUnidad3)) Then
            oErrores.Mensaje = cErrores.TipoUnidad3Error
            oErrores.AgregaError()
            oErrores.NumError = oErrores.NumError + 1
            Return False
        End If
        Return True
    End Function

End Class
