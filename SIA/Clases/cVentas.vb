Imports System.Data
Imports cConexion
Imports System.ComponentModel
Public Class cVentas
    Implements IDataErrorInfo

    Private nIdVenta As Integer
    Private dFechaVenta As Date
    Private dFechaTerminoVenta As Date
    Private nTotalImpuesto As Double
    Private nTotalProductos As Double
    Private nMontoTotalNeto As Double
    Private nMontoTotalVenta As Double
    Private bValida As Boolean
    Private nTotalProductoVentaDiaria As Double
    Private nTotalVentaNetaDiaria As Double
    Private nTotalVentaDiaria As Double
    Private nTotalImpuestoVentaDiaria As Double
    Private oCatalogoBD As New cCatalogoBaseDatos
    Private sMensaje As String
    Private nTotalVentaOrig As Double
    Private nTotalProductoVentaOrig As Double
    Private nTotalNetoVentaOrig As Double

    Private sCodigoCliente As String


    Public Property IdVenta
        Get
            Return nIdVenta
        End Get
        Set(value)
            Try
                nIdVenta = value
            Catch ex As Exception
                nIdVenta = 0
            End Try

        End Set
    End Property
    Public Property FechaVenta
        Get
            Return dFechaVenta
        End Get
        Set(value)
            dFechaVenta = value
        End Set
    End Property

    Public Property FechaTerminoVenta
        Get
            Return dFechaTerminoVenta
        End Get
        Set(value)
            dFechaTerminoVenta = value
        End Set
    End Property

    Public Property CodigoCliente
        Get
            Return sCodigoCliente
        End Get
        Set(value)
            sCodigoCliente = value

        End Set
    End Property
    Public Property TotalImpuesto
        Get
            Return nTotalImpuesto
        End Get
        Set(value)
            Try
                nTotalImpuesto = value
            Catch ex As Exception
                nTotalImpuesto = 0
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
    Public Property MontoTotalNeto
        Get
            Return nMontoTotalNeto
        End Get
        Set(value)
            Try
                nMontoTotalNeto = value
            Catch ex As Exception
                nMontoTotalNeto = 0
            End Try

        End Set
    End Property

    Public Property MontoTotalVenta
        Get
            Return nMontoTotalVenta
        End Get
        Set(value)
            Try
                nMontoTotalVenta = value
            Catch ex As Exception
                nMontoTotalVenta = 0
            End Try

        End Set
    End Property

    Public Property TotalVentaDiaria
        Get
            Return nTotalVentaDiaria
        End Get
        Set(value)
            Try
                nTotalVentaDiaria = value
            Catch ex As Exception
                nTotalVentaDiaria = 0
            End Try

        End Set
    End Property

    Public Property TotalVentaNetaDiaria
        Get
            Return nTotalVentaNetaDiaria
        End Get
        Set(value)
            Try
                nTotalVentaNetaDiaria = value
            Catch ex As Exception
                nTotalVentaNetaDiaria = 0
            End Try

        End Set
    End Property

    Public Property TotalImpuestoVentaDiaria
        Get
            Return nTotalImpuestoVentaDiaria
        End Get
        Set(value)
            Try
                nTotalImpuestoVentaDiaria = value
            Catch ex As Exception
                nTotalImpuestoVentaDiaria = 0
            End Try

        End Set
    End Property


    Public Property TotalProductoVentaDiaria
        Get
            Return nTotalProductoVentaDiaria
        End Get
        Set(value)
            Try
                nTotalProductoVentaDiaria = value
            Catch ex As Exception
                nTotalProductoVentaDiaria = 0
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

    Public Property Valida
        Get
            Return bValida
        End Get
        Set(value)
            bValida = value
        End Set
    End Property

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
            Case "FechaVenta"
                If dFechaVenta > dFechaTerminoVenta Then
                    bValida = False
                    oErrores.Mensaje = cErrores.FechaMayorError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.FechaMayorError
                End If
            Case "FechaTerminoVenta"
                If dFechaTerminoVenta < dFechaVenta Then
                    bValida = False
                    oErrores.Mensaje = cErrores.FechaMayorError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.FechaMayorError
                End If

        End Select

        Return Nothing
    End Function
    Public Sub ObtieneIDNumeroVenta()

        Dim tbVenta As New DT
        Try
            tbVenta.RecordSet = oCatalogoBD.MaxNumeroVenta
            If tbVenta.EOF = False Then
                IdVenta = tbVenta.GetValue("numero_venta") + 1
            Else
                IdVenta = 1
            End If
        Catch ex As Exception
        End Try

    End Sub

    Public Function IngresaVentas(Optional ByRef sTipoProceso As String = "") As Boolean
        Try

            oCatalogoBD.EliminaVenta(IdVenta)
            If oCatalogoBD.IngresaVentas(IdVenta, dFechaVenta, CodigoCliente, TotalImpuesto, TotalProductos, MontoTotalNeto, MontoTotalVenta) Then
                If ActualizaVentaDiaria(sTipoProceso) Then
                    ObtieneIDNumeroVenta()
                    Return True
                Else
                    Mensaje = "Atención. Error al ingresar venta"
                    Return False
                End If
                
            Else
                Mensaje = "Atención. Error al ingresar venta"
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error al ingresar venta, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function
    Public Function ObtieneVenta() As DT
        Dim tbVenta As New DT
        Try
            tbVenta.RecordSet = oCatalogoBD.ObtieneVenta(dFechaVenta, dFechaTerminoVenta)
            Return tbVenta
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ObtieneVentaDiaria() As DT
        Dim tbVenta As New DT
        Try
            tbVenta.RecordSet = oCatalogoBD.ObtieneVentaDiaria(dFechaVenta)
            Return tbVenta
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function ObtieneVentaCliente() As DT
        Dim tbVenta As New DT
        Try
            tbVenta.RecordSet = oCatalogoBD.ObtieneVentaCliente(dFechaVenta, dFechaTerminoVenta, sCodigoCliente)
            Return tbVenta
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CargaDetalleVenta() As DT
        Dim tbDetalle As New DT
        Try
            tbDetalle.RecordSet = oCatalogoBD.ObtieneDetalleVenta(IdVenta)
            Return tbDetalle
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function EliminaVenta() As Boolean
        Try

            If oCatalogoBD.EliminaDetalleVenta(IdVenta) Then
                If oCatalogoBD.EliminaVenta(IdVenta) Then
                    If ActualizaEliminacionVentaDiaria() Then
                        Return True
                    Else
                        Mensaje = " Atención. Error al eliminar Venta"
                        Return False
                    End If
                Else
                    Mensaje = " Atención. Error al eliminar Venta"
                    Return False
                End If
            Else
                Mensaje = " Atención. Error al actualizar venta diaria"
                Return False
            End If
        Catch ex As Exception
            Mensaje = " Atención. Error al eliminar Venta, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function
    Private Function ActualizaEliminacionVentaDiaria() As Boolean
        Dim tbVenta As New DT
        Dim nTotalVenta As Double
        Dim nTotalProducto As Double
        Dim nTotalNeto As Double

        tbVenta.RecordSet = oCatalogoBD.ObtieneVentaFecha(FechaVenta)
        If tbVenta.EOF = True Then Return False
        Do While tbVenta.EOF = False
            nTotalVenta = tbVenta.GetValue("monto_total_venta")
            nTotalProducto = tbVenta.GetValue("total_productos")
            nTotalNeto = tbVenta.GetValue("monto_total_neto")
            tbVenta.MoveNext()
        Loop
        If oCatalogoBD.ActualizaVentaDiaria(FechaVenta, nTotalProducto, nTotalVenta, nTotalNeto) Then
            Return True
        Else
            Return False
        End If

    End Function

    Public Function ActualizaVentaDiaria(ByVal sTipoProceso As String) As Boolean
        Dim tbVenta As New DT

        If Not sTipoProceso = cConstantes.MODIFICAR Then
            If oCatalogoBD.IngresaVentaDiaria(dFechaVenta, TotalProductos, MontoTotalNeto, MontoTotalVenta) Then
            End If
            Return True
        Else

            oCatalogoBD.EliminaVenta(IdVenta)
            If oCatalogoBD.IngresaVentas(IdVenta, dFechaVenta, CodigoCliente, TotalImpuesto, TotalProductos, MontoTotalNeto, MontoTotalVenta) Then
            End If

            Return True
        End If
    End Function
    Private Sub ObtieneVentaOriginal()
        Dim tbVenta As New DT
        tbVenta.RecordSet = oCatalogoBD.ObtieneVentaID(IdVenta)
        Do While tbVenta.EOF = False
            nTotalVentaOrig = nTotalVentaOrig + tbVenta.GetValue("monto_total_venta")
            nTotalProductoVentaOrig = nTotalProductoVentaOrig + tbVenta.GetValue("total_productos")
            nTotalNetoVentaOrig = nTotalNetoVentaOrig + tbVenta.GetValue("monto_total_neto")
            tbVenta.MoveNext()
        Loop


    End Sub
    Public Function ActualizaVenta() As Boolean
        Dim tbVenta As New DT
        Dim nTotalVentaDiaria As Double
        Dim nTotalProductoVentaDiaria As Double
        Dim nTotalNetoVentaDiaria As Double
        ObtieneVentaOriginal()

        If oCatalogoBD.ActualizaVenta(IdVenta, TotalProductos, MontoTotalVenta, TotalImpuesto, MontoTotalNeto) Then
            tbVenta.RecordSet = oCatalogoBD.ObtieneModificaVentaDiaria(dFechaVenta)

            If tbVenta.EOF = True Then Return False
            Do While tbVenta.EOF = False
                nTotalVentaDiaria = tbVenta.GetValue("total_venta_diaria")
                nTotalProductoVentaDiaria = tbVenta.GetValue("total_productos_venta_diaria")
                nTotalNetoVentaDiaria = tbVenta.GetValue("total_venta_neta_diaria")
                tbVenta.MoveNext()
            Loop

            TotalProductos = nTotalProductoVentaDiaria + (TotalProductos - nTotalProductoVentaOrig)
            MontoTotalNeto = nTotalNetoVentaDiaria + (MontoTotalNeto - nTotalNetoVentaOrig)
            MontoTotalVenta = nTotalVentaDiaria + (MontoTotalVenta - nTotalVentaOrig)
            If oCatalogoBD.ActualizaVentaDiaria(dFechaVenta, TotalProductos, MontoTotalNeto, MontoTotalVenta) Then
            End If
        End If
        Return True
    End Function

End Class
