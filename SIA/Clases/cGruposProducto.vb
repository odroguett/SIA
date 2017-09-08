Imports System.Data
Imports cConexion
Imports System.ComponentModel
Public Class cGruposProducto
    Inherits cProductos
    Private oCatalogoBD As New cCatalogoBaseDatos
    Public Function CargaGrupos() As DT
        Dim tbGrupos As New DT
        Try
            tbGrupos.RecordSet = oCatalogoBD.GruposProducto
            Return tbGrupos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
