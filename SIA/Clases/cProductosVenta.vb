Imports System.Data
Imports cConexion
Imports System.ComponentModel
Imports System.Collections.ObjectModel

Public Class cProductosVenta
    Inherits cVentas
    Implements IDataErrorInfo

    Private nCantidad As Double
    Private nValorProducto As Double
    Private nTotalVentaProducto As Double
    Private sMensaje As String
    Private oCatalogoBD As New cCatalogoBaseDatos
    Private oUtiles As New cUtiles
    Private bValida As Boolean
    Private sCodigo As String
    Public ListaProductos As List(Of Productos) = New List(Of Productos)()
    Public ListaVenta As New ObservableCollection(Of ListaVentas)
    Public ListaProductosCB As New ObservableCollection(Of ProductosCB)
    Public WithEvents CapturaCodigo As New ListaVentas


    Public Property Codigo
        Get
            Return sCodigo
        End Get
        Set(value)
            sCodigo = value

        End Set
    End Property

    Public Property ValidaVenta
        Get
            Return bValida
        End Get
        Set(value)
            bValida = value

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

    Public Property Mensaje
        Get
            Return sMensaje
        End Get
        Set(value)
            sMensaje = value

        End Set
    End Property

    Public Property ValorProducto
        Get
            Return nValorProducto
        End Get
        Set(value)
            Try
                nValorProducto = value
            Catch ex As Exception
                nValorProducto = 0
            End Try

        End Set
    End Property

    Public Property TotalVentaProducto
        Get
            Return nTotalVentaProducto
        End Get
        Set(value)
            Try
                nTotalVentaProducto = value
            Catch ex As Exception
                nTotalVentaProducto = 0
            End Try

        End Set
    End Property

    Public Function CargaProductosVenta() As DT
        Dim tbProductos As New DT
        Try
            tbProductos.RecordSet = oCatalogoBD.ObtieneProductosVenta
            Do While tbProductos.EOF = False
                ListaProductos.Add(New Productos(tbProductos.GetValue("codigo"), tbProductos.GetValue("descripcion"), tbProductos.GetValue("valor_venta")))
                tbProductos.MoveNext()
            Loop
            tbProductos.MoveFirst()
            Return tbProductos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CargaProductosCB() As DT
        Dim tbProductos As New DT
        Try
            tbProductos.RecordSet = oCatalogoBD.ObtieneProductosVenta
            Do While tbProductos.EOF = False
                ListaProductosCB.Add(New ProductosCB(tbProductos.GetValue("codigo"), tbProductos.GetValue("descripcion")))
                tbProductos.MoveNext()
            Loop
            tbProductos.MoveFirst()
            Return tbProductos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            sMensaje = isValid(columnName)
            Return sMensaje
        End Get
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
            Case "ValorProducto"
                If Not IsNumeric(nCantidad) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.ValorProducto
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.ValorProducto
                End If
                If nCantidad <= 0 Then
                    bValida = False
                    oErrores.Mensaje = cErrores.ValorProducto
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.ValorProducto
                End If
            Case "TotalVentaProducto"
                If Not IsNumeric(nCantidad) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.TotalValorProducto
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.TotalValorProducto
                End If
                If nCantidad <= 0 Then
                    bValida = False
                    oErrores.Mensaje = cErrores.TotalValorProducto
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.TotalValorProducto
                End If

            Case "Cantidad"
                If Not IsNumeric(nCantidad) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.ErrorCantidadVenta
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.ErrorCantidadVenta
                End If
                If nCantidad <= 0 Then
                    bValida = False
                    oErrores.Mensaje = cErrores.ErrorCantidadVenta
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.ErrorCantidadVenta
                End If
        End Select
        Return Nothing
    End Function
    Public Function IngresaDetalleVentasProduccion() As Boolean
        Try
            If oCatalogoBD.IngresaDetalleVentasProduccion(IdVenta, Codigo, Cantidad, ValorProducto) Then
                ActualizaMontosTotales(cConstantes.AGREGAR)
                Return True
            Else
                Mensaje = "Atención. Error al ingresar venta"
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error al ingresar  venta, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function

    Public Function ActualizaDetalleVentasProduccion() As Boolean
        Dim dTDetalle As New DT
        Dim nCantidadAnt As Double
        Dim nValorProductoAnt As Double
        Try
            dTDetalle.RecordSet = oCatalogoBD.ObtieneDetalleVentasProduccion(IdVenta, Codigo)
            If dTDetalle.EOF = False Then
                nCantidadAnt = dTDetalle.GetValue("cantidad")
                nValorProductoAnt = dTDetalle.GetValue("valor_venta_producto")
            End If

            If oCatalogoBD.ActualizaDetalleVentasProduccion(IdVenta, Codigo, Cantidad, ValorProducto) Then
                ActualizaMontosTotales(cConstantes.MODIFICAR, nCantidadAnt, nValorProductoAnt)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function ObtieneDetalleVentasProduccion() As DT
        Dim tbProductos As New DT
        Try
            tbProductos.RecordSet = oCatalogoBD.ObtieneDetalleVentasProduccion(IdVenta)
            Return tbProductos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Private Sub ActualizaMontosTotales(ByVal sTipoProceso As String, Optional ByVal nCantidadAnt As Double = 0, Optional ByVal nValorProductoAnt As Double = 0)
        If sTipoProceso = "A" Then
            TotalProductos = TotalProductos + Cantidad
            MontoTotalVenta = MontoTotalVenta + ValorProducto
        Else
            TotalProductos = TotalProductos + Cantidad - nCantidadAnt
            MontoTotalVenta = MontoTotalVenta + ValorProducto - nValorProductoAnt
        End If
        TotalImpuesto = Math.Round((MontoTotalVenta * 0.19), 0)
        MontoTotalNeto = MontoTotalVenta - TotalImpuesto
    End Sub

    Public Sub EliminaMontos()
        TotalProductos = TotalProductos - Cantidad
        MontoTotalVenta = MontoTotalVenta - TotalVentaProducto
        TotalImpuesto = Math.Round((MontoTotalVenta * 0.19), 0)
        MontoTotalNeto = MontoTotalVenta - TotalImpuesto

    End Sub

    Public Function EliminaDetalleVentasProduccionID() As Boolean
        Try
            If oCatalogoBD.EliminaDetalleVentasProduccion(IdVenta) Then

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EliminaDetalleVentasProduccion() As Boolean
        Try
            If oCatalogoBD.EliminaDetalleVentasProduccion(IdVenta, Codigo) Then

            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Sub prueba() Handles CapturaCodigo.PropertyChanged
        MsgBox("hola")
    End Sub
End Class



Public Class Productos
    Private sCodigo As String
    Private sDescripcion As String
    Private nValorVenta As Double

    Public Sub New()

    End Sub
    Public Property Codigo
        Get
            Return sCodigo
        End Get
        Set(value)
            sCodigo = value

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
    Public Property ValorVenta
        Get
            Return nValorVenta
        End Get
        Set(value)
            nValorVenta = value

        End Set
    End Property
    Public Sub New(ByVal sCodigo As String, ByVal sDescripcion As String, ByVal nValorVenta As Double)
        sCodigo = sCodigo
        sDescripcion = sDescripcion
        nValorVenta = nValorVenta
    End Sub


End Class
Public Class ProductosCB
    Implements INotifyPropertyChanged

    Private WithEvents sCodigo As String
    Private sDecripcion As String
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Sub New()

    End Sub
    Public Sub New(ByVal Codigo As String, ByVal Descripcion As String)
        sCodigo = Codigo
        sDecripcion = Descripcion
    End Sub
    Public Property Codigo
        Get
            Return sCodigo
        End Get
        Set(value)
            sCodigo = value
            OnPropertyChanged("Codigo")
        End Set
    End Property
    Public Property Descripcion
        Get
            Return sDecripcion
        End Get
        Set(value)
            sDecripcion = value
        End Set
    End Property
    Protected Sub OnPropertyChanged(ByVal Codigo As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(Codigo))
    End Sub

End Class
Public Class ListaVentas
    Implements INotifyPropertyChanged

    Private sCodigoProducto As String
    Private sDecripcionProducto As String
    Private nValor As Double
    Private nDescuento As Double
    Private nCantidad As Double
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Public Property CodigoProducto
        Get
            Return sCodigoProducto
        End Get
        Set(value)
            sCodigoProducto = value
            OnPropertyChanged(sCodigoProducto)
        End Set
    End Property
    Public Property DescripcionProducto
        Get
            Return sDecripcionProducto
        End Get
        Set(value)
            sDecripcionProducto = value
        End Set
    End Property
    Public Property Valor
        Get
            Return nValor
        End Get
        Set(value)
            nValor = value
        End Set
    End Property
    Public Property Descuento
        Get
            Return nDescuento
        End Get
        Set(value)
            nDescuento = value
        End Set
    End Property

    Public Property Cantidad
        Get
            Return nCantidad
        End Get
        Set(value)
            nCantidad = value
        End Set
    End Property

    Public Sub New(ByVal CodigoProducto As String, ByVal Descripcion As String, ByVal Valor As Double, ByVal Descuento As Double, ByVal Cantidad As Double)
        sCodigoProducto = CodigoProducto
        sDecripcionProducto = Descripcion
        nValor = Valor
        nDescuento = Descuento
        nCantidad = Cantidad
    End Sub
    Public Sub New()

    End Sub

    Protected Sub OnPropertyChanged(ByVal Codigo As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(Codigo))
    End Sub

End Class