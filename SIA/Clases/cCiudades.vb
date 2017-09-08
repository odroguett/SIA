Imports cConexion
Public Class cCiudades
    Private oCatalogoBD As New cCatalogoBaseDatos
    Public Function CargaCiudades() As DT
        Dim tbCiudades As New DT
        Try
            tbCiudades.RecordSet = oCatalogoBD.Ciudades
            Return tbCiudades
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
