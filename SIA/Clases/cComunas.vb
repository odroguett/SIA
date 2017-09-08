Imports cConexion
Public Class cComunas
    Private oCatalogoBD As New cCatalogoBaseDatos
    Public Function CargaComunas() As DT
        Dim tbComunas As New DT
        Try
            tbComunas.RecordSet = oCatalogoBD.Comunas
            Return tbComunas
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
End Class
