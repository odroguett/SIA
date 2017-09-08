Imports System.ComponentModel
Imports System.Data

Imports cConexion
Public Class cProveedores
    Implements IDataErrorInfo
    Private oCatalogoBD As New cCatalogoBaseDatos
    Private oUtiles As New cUtiles
    Private sCodigo As String
    Private sDescripcion As String
    Private sRutProveedor As String
    Private sDireccion As String
    Private sCiudad As String
    Private sComuna As String
    Private sRegion As String
    Private sTelefono As String
    Private sMail As String
    Private sContacto As String
    Private sMensaje As String
    Private bValida As Boolean
    Private bGrilla As Boolean = False

    Public Property Codigo
        Get
            Return sCodigo
        End Get
        Set(value)
            sCodigo = value
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

    Public Property Descripcion
        Get
            Return sDescripcion
        End Get
        Set(value)
            sDescripcion = value
        End Set
    End Property

    Public Property RutProveedor
        Get
            Return sRutProveedor
        End Get
        Set(value)
            sRutProveedor = value
        End Set
    End Property



    Public Property Direccion
        Get
            Return sDireccion
        End Get
        Set(value)
            sDireccion = value
        End Set
    End Property

    Public Property Comuna
        Get
            Return sComuna
        End Get
        Set(value)
            sComuna = value
        End Set
    End Property

    Public Property Ciudad
        Get
            Return sCiudad
        End Get
        Set(value)
            sCiudad = value
        End Set
    End Property

    Public Property Region
        Get
            Return sRegion
        End Get
        Set(value)
            sRegion = value
        End Set
    End Property

    Public Property Telefono
        Get
            Return sTelefono
        End Get
        Set(value)
            sTelefono = value
        End Set
    End Property

    Public Property Mail
        Get
            Return sMail
        End Get
        Set(value)
            sMail = value
        End Set
    End Property

    Public Property Contacto
        Get
            Return sContacto
        End Get
        Set(value)
            sContacto = value
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

    Public WriteOnly Property Grilla
        Set(value)
            bGrilla = value
        End Set
    End Property
    Public Sub New()
        oErrores.InicializaErrores()
    End Sub

    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            If bGrilla = True Then Exit Property
            sMensaje = isValid(columnName)
            Return Mensaje
        End Get

    End Property

    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Function CargaProveedores() As DT
        Dim tbProveedores As New DT
        Try
            tbProveedores.RecordSet = oCatalogoBD.Proveedores
            Return tbProveedores
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CargaCBProveedores() As DT
        Dim tbProveedores As New DT
        Try
            tbProveedores.RecordSet = oCatalogoBD.cbProveedores
            Return tbProveedores
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function IngresaProveedores() As Boolean
        Try
            If oCatalogoBD.IngresaProveedores(sCodigo, sDescripcion, sRutProveedor, sDireccion, sComuna,
                                       sCiudad, sRegion, sTelefono, sMail, sContacto) Then
                Return True
            Else
                Mensaje = "Atención. Error al ingresar proveedor, revise si código es unico"
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al ingresar proveedor, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function

    Public Function ActualizaProveedores() As Boolean
        Try
            If oCatalogoBD.ActualizaProveedores(sCodigo, sDescripcion, sRutProveedor, sDireccion, sComuna,
                                       sCiudad, sRegion, sTelefono, sMail, sContacto) Then
                Return True
            Else
                Mensaje = "Atención. Error al actualizar datos del proveedor, revise si código es unico"
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error al actualizar datos del proveedor, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function
    Public Function EliminaProvedores() As Boolean
        Try
            If oCatalogoBD.EliminaProveedores(sCodigo) Then
                Return True
            Else
                Mensaje = "Atención. Error al eliminar proveedor, vuelva a intentarlo"
            End If
        Catch ex As Exception
            Mensaje = "Atención. Error al eliminar datos del proveedor, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function
    Private Function isValid(ByVal sNombre As String) As String

        Select Case sNombre
            Case "Codigo"
                If String.IsNullOrEmpty(sCodigo) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.CodigoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.CodigoError
                End If
            Case "Mail"
                If Not oUtiles.ValidarMail(sMail) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.MailError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.MailError
                End If
            Case "RutProveedor"
                If Not oUtiles.ValidaRut(sRutProveedor) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.RUTError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.RUTError
                End If
            Case "Telefono"
                If Not IsNumeric(sTelefono) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.TelefonoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.TelefonoError
                End If
        End Select
        Return Nothing
    End Function

End Class
