Imports System.Data
Imports cConexion
Imports System.ComponentModel
Public Class cTiposUnidad
    Inherits cProductos
    Private oCatalogoBD As New cCatalogoBaseDatos
    Public Function TiposUnidad() As DT
        Dim tbTipos As New DT
        Try
            tbTipos.RecordSet = oCatalogoBD.TiposUnidad(TipoUnidad1)
            Return tbTipos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CBTiposUnidad() As DT
        Dim tbTipos As New DT
        Try
            tbTipos.RecordSet = oCatalogoBD.CBTiposUnidad(TipoUnidad1)
            Return tbTipos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ObtieneGrupoTiposUnidad() As DT
        Dim tbTipos As New DT
        Try
            tbTipos.RecordSet = oCatalogoBD.ObtieneTiposUnidadGrupo(GrupotipoUnidad, TipoUnidad1)
            Return tbTipos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function IngresaTiposUnidad() As Boolean
        Try
            oCatalogoBD.IngresaTiposUnidad(TipoUnidad1, Descripcion)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function CargaTiposUnidad() As DT
        Dim tbTiposUnidad As New DT
        Try
            tbTiposUnidad.RecordSet = oCatalogoBD.TiposUnidad
            Return tbTiposUnidad
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function ActualizaTiposUnidad() As Boolean
        Try
            oCatalogoBD.ActualizaTiposUnidad(TipoUnidad1, Descripcion)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function EliminaTiposUnidad() As Boolean
        Try
            oCatalogoBD.EliminaTiposUnidad(TipoUnidad1)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
