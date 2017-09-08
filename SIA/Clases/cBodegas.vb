Imports System.ComponentModel
Imports cConexion


''' <summary>
''' Pruyeba de gitttttt
''' </summary>

Public Class cBodegas
    Implements IDataErrorInfo
    Private oCatalogoBD As New cCatalogoBaseDatos
    Private oUtiles As New cUtiles
    Private nNumeroIngreso As Double
    Private nNumeroOrden As Double
    Private dFechaIngreso As Date
    Private nValorCompra As Double
    Private sMensaje As String
    Private bValida As Boolean
    Private sProveedor As String
    Private nCantidad As Double
    Private nValor As String

    Public Property NumeroIngreso
        Get
            Return nNumeroIngreso
        End Get
        Set(value)

            nNumeroIngreso = value
        End Set
    End Property

    Public Property NumeroOrden

        Get
            Return nNumeroOrden
        End Get


        Set(value)
            Try
                nNumeroOrden = value
            Catch ex As Exception
                nNumeroOrden = 0
            End Try

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
    Public Property ValorCompra
        Get
            Return nValorCompra
        End Get
        Set(value)

            nValorCompra = value
        End Set
    End Property
    Public Property Valida
        Get
            Return bValida
        End Get
        Set(value)
            bValida = True
        End Set

    End Property

    Public Property Proveedor
        Get
            Return sProveedor
        End Get
        Set(value)
            sProveedor = value
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

    Public Property Valor
        Get
            Return nValor
        End Get
        Set(value)

            nValor = value
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
    Public Sub New()
        oErrores.InicializaErrores()
    End Sub
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

    Private Function isValid(ByVal sNombre As String) As String

        Select Case sNombre
            Case "NumeroIngreso"
                If String.IsNullOrEmpty(nNumeroIngreso) Then
                    bValida = False
                    'Return "Codigo no puede ser vacio"
                    oErrores.Mensaje = cErrores.NumeroIngresoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumeroIngresoError
                End If
            Case "NumeroOrden"
                If String.IsNullOrEmpty(nNumeroOrden) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumeroOrdenError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumeroOrdenError
                End If
            Case "Proveedor"
                If String.IsNullOrEmpty(sProveedor) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.ProveedorError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.ProveedorError
                End If
            Case "ValorCompra"
                If Not IsNumeric(nValorCompra) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.ValorCompraError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.ValorCompraError
                End If
            Case "FechaIngreso"
                If Not IsDate(dFechaIngreso) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.FechaError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.FechaError
                End If
        End Select
        Return Nothing
    End Function
    Public Function CargaDatosBodega() As DT
        Dim tbBodega As New DT
        Try
            tbBodega.RecordSet = oCatalogoBD.CargaDatosBodega
            Return tbBodega
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function IngresaDatosBodega() As Boolean
        Try
            If oCatalogoBD.IngresaDatosBodega(nNumeroIngreso, nNumeroOrden, dFechaIngreso, nValorCompra, sProveedor) Then
                Return True
            Else
                Mensaje = "Atención. Error al ingresar compra"
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error al ingresar  compra, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function
    Public Function ActualizaDatosBodega() As Boolean
        Try
            If oCatalogoBD.ActualizaDatosBodega(NumeroIngreso, nNumeroOrden, dFechaIngreso, nValorCompra, sProveedor) Then
                Return True
            Else
                Mensaje = " Atención. Error al actualizar datos de la compra"
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al actualizar datos de la compra, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function

    Public Function EliminaIngresoBodega() As Boolean
        Try
            If oCatalogoBD.EliminaIngresoBodega(nNumeroIngreso) Then
                Return True
            Else
                Mensaje = " Atención. Error al eliminar compra"
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al actualizar eliminar compra, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function

    Public Function ObtieneNumeroIngreso() As Double
        Dim tbBodega As New DT
        Try
            tbBodega.RecordSet = oCatalogoBD.MaxNumeroIngreso
            If tbBodega.EOF = False Then
                nNumeroIngreso = tbBodega.GetValue("numero_ingreso") + 1
            Else
                nNumeroIngreso = 1
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
