Imports cConexion
Public Class cParametros
    Private nTransbank As Double
    Private nPorcentajeGanancia As Double
    Private bBodega As Boolean
    Private bVenta As Boolean
    Private sMensaje As String
    Private nPorcentajeIVA As Double = 19
    Private oCatalogoBD As New cCatalogoBaseDatos
    Public Property Transbank
        Get
            Return nTransbank
        End Get
        Set(value)
            nTransbank = value
        End Set
    End Property
    Public Property PorcentajeGanancia
        Get
            Return nPorcentajeGanancia
        End Get
        Set(value)
            nPorcentajeGanancia = value
        End Set

    End Property
    Public Property Bodega
        Get
            Return bBodega
        End Get
        Set(value)
            bBodega = value
        End Set
    End Property
    Public Property Venta
        Get
            Return bVenta
        End Get
        Set(value)
            Venta = value
        End Set
    End Property
    Public Property PorcentajeIVA
        Get
            Return nPorcentajeIVA
        End Get
        Set(value)
            nPorcentajeIVA = value
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

    Public Sub CargaParametros()
        Dim tbParametros As New DT
        Try
            tbParametros.RecordSet = oCatalogoBD.Parametros
            If tbParametros.EOF = False Then
                nTransbank = tbParametros.GetValue("porcentaje_transbank")
                nPorcentajeGanancia = tbParametros.GetValue("porcentaje_ganancia")
                bBodega = tbParametros.GetValue("Bodega")
                bVenta = tbParametros.GetValue("venta")
            End If

        Catch ex As Exception

        End Try
    End Sub
    Public Function ActualizaParametros() As Boolean
        Try
            If oCatalogoBD.ActualizParametros(nTransbank, nPorcentajeGanancia, nPorcentajeIVA, bBodega, bVenta) Then
                Return True
            Else
                Mensaje = " Atención. Error al actualizar datos del cliente, revise si código es unico"
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al actualizar datos del cliente, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function
End Class
