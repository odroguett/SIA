Imports cConexion
Public Class cRegiones
    Private oCatalogoBD As New cCatalogoBaseDatos
    Public Function CargaRegiones() As DT
        Dim tbRegiones As New DT
        Try
            tbRegiones.RecordSet = oCatalogoBD.Regiones
            Return tbRegiones
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
