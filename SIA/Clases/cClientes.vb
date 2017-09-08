Imports System.ComponentModel
Imports System.Data
Imports System.Text.RegularExpressions
Imports cConexion
Public Class cClientes
    Implements IDataErrorInfo
    Private oCatalogoBD As New cCatalogoBaseDatos
    Private oUtiles As New cUtiles
    Private sCodigo As String
    Private sDescripcion As String
    Private sRutCliente As String
    Private sDireccion As String
    Private sCiudad As String
    Private sComuna As String
    Private sRegion As String
    Private sTelefono As String
    Private sMail As String
    Private sCelular As String
    Private sMensaje As String
    Private bValida As Boolean

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

    Public Property RutCliente
        Get
            Return sRutCliente
        End Get
        Set(value)
            sRutCliente = value
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

    Public Property Celular
        Get
            Return sCelular
        End Get
        Set(value)
            sCelular = value
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
    Public Sub New()
        oErrores.InicializaErrores()
    End Sub

    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item
        Get
            sMensaje = isValid(columnName)
            Return sMensaje
        End Get
    End Property
    Public Function CargaClientes() As DT
        Dim tbClientes As New DT
        Try
            tbClientes.RecordSet = oCatalogoBD.Clientes
            Return tbClientes
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function CargaCBClientes() As DT
        Dim tbClientes As New DT
        Try
            tbClientes.RecordSet = oCatalogoBD.CargaCBClientes(sCodigo)
            Return tbClientes
        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Function IngresaClientes() As Boolean
        Try
            If oCatalogoBD.IngresaClientes(sCodigo, sDescripcion, sRutCliente, sDireccion, sComuna,
                                       sCiudad, sRegion, sTelefono, sMail, sCelular) Then
                Return True
            Else
                Mensaje = "Atención. Error al ingresar cliente, revise si código es unico"
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error al ingresar  cliente, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function
    Public Function ActualizaClientes() As Boolean
        Try
            If oCatalogoBD.ActualizaClientes(sCodigo, sDescripcion, sRutCliente, sDireccion, sComuna,
                                       sCiudad, sRegion, sTelefono, sMail, sCelular) Then
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
    Public Function EliminaClientes() As Boolean
        Try
            If oCatalogoBD.EliminaClientes(sCodigo) Then

                Return True
            Else
                Mensaje = " Atención. Error al eliminar cliente"
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al eliminar cliente, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function
    Private Function isValid(ByVal sNombre As String) As String

        Select Case sNombre
            Case "Codigo"
                If String.IsNullOrEmpty(sCodigo) Then
                    bValida = False
                    'Return "Codigo no puede ser vacio"
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
            Case "RutCliente"
                If Not oUtiles.ValidaRut(sRutCliente) Then
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
            Case "Celular"
                If Not IsNumeric(sCelular) Then
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
