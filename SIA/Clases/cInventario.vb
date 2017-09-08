Imports cConexion
Imports System.ComponentModel
Public Class cInventario
    Inherits cProductos
    Private oCatalogoBD As New cCatalogoBaseDatos
    Private oUtiles As New cUtiles
    Private sObservacion As String
    Private sTipoMovimiento As String


    Public Property Observacion
        Get
            Return sObservacion
        End Get
        Set(value)
            sObservacion = value
        End Set
    End Property

    Public Property TipoMovimiento
        Get
            Return sTipoMovimiento
        End Get
        Set(value)
            sTipoMovimiento = value
        End Set
    End Property

    Public Function CargaInventario() As DT
        Dim tbProveedores As New DT
        Try
            tbProveedores.RecordSet = oCatalogoBD.Inventario
            Return tbProveedores
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CargaDetalleInventario() As DT
        Dim tbDetalle As New DT
        Try
            tbDetalle.RecordSet = oCatalogoBD.DetalleInventario
            Return tbDetalle
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function IngresaDetalleInventario() As Boolean
        Try
            If oCatalogoBD.IngresaDetalleInventario(FechaIngreso, Codigo, TipoUnidad1, TipoMovimiento, Observacion, Movimiento) Then
                If oCatalogoBD.ActualizaInventario(Codigo, Movimiento, TipoMovimiento) Then
                    Return True
                Else
                    Mensaje = "Atención. Error al actualizar inventario"
                    Return False
                End If

            Else
                Mensaje = "Atención. Error al ingresar detalle al inventario"
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error critico al ingresar detalle al inventario, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function
    Public Function ActualizaDetalleInventario() As Boolean

        Try
            If oCatalogoBD.ActualizaDetalleInventario(FechaIngreso, Codigo, TipoUnidad1, Observacion, Movimiento, TipoMovimiento) Then
                If oCatalogoBD.ActualizaInventario(Codigo, Movimiento, TipoMovimiento) Then
                    Return True
                Else
                    Mensaje = "Atención. Error al actualizar inventario"
                    Return False
                End If

            Else
                Mensaje = "Atención. Error al actualizar detalle al inventario"
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error critico al actualizar detalle al inventario, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function

    Public Function EliminaDetalleInventario() As Boolean
        Try
            TipoMovimiento = cConstantes.ELIMINAR
            If oCatalogoBD.EliminaDetalleInventario(FechaIngreso, Codigo) Then
                If oCatalogoBD.ActualizaInventario(Codigo, Movimiento, TipoMovimiento) Then
                    Return True
                Else
                    Mensaje = "Atención. Error al actualizar inventario"
                    Return False
                End If

            Else
                Mensaje = "Atención. Error al eliminar proveedor, vuelva a intentarlo"
                Return False
            End If
        Catch ex As Exception
            Mensaje = "Atención. Error al eliminar datos del proveedor, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function


End Class
