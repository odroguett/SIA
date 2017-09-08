Imports cConexion
Imports System.ComponentModel
Public Class cTiposProducto
    Inherits cProductos
    Implements IDataErrorInfo
    Private oCatalogoBD As New cCatalogoBaseDatos
    Private sTipoProducto As String
    Public Property TipoProducto
        Get
            Return sTipoProducto
        End Get
        Set(value)
            sTipoProducto = value
        End Set
    End Property
    Public Function CargaTipoProductos() As DT
        Dim tbProducto As New DT
        Try
            tbProducto.RecordSet = oCatalogoBD.TiposProductos()
            Return tbProducto
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function IngresaTipoProducto() As Boolean
        Try
            If oCatalogoBD.IngresaTipoProducto(Codigo, Descripcion) Then
                Return True
            Else
                Mensaje = "Código de tipo de producto invalido."
                Return False
            End If

        Catch ex As Exception
            Mensaje = ex
            Return False
        End Try
    End Function
    Public Function ActualizaTipoProducto() As Boolean
        Try
            oCatalogoBD.ActualizaTipoProducto(Codigo, Descripcion)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EliminaTipoProducto() As Boolean
        Try
            oCatalogoBD.EliminaTipoProducto(Codigo)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
