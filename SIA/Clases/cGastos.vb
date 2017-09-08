Imports cConexion
Imports System.ComponentModel
Public Class cGastos
    Implements IDataErrorInfo

    Private oCatalogoBD As New cCatalogoBaseDatos
    Private sCodigo As String
    Private sCodigoGasto As String
    Private sDescripcion As String
    Private nTotalGasto As Double
    Private nGasto As Double
    Private bValida As Boolean = True
    Private sMensaje As String
    Private dFechaIngreso As Date


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

    Public Property CodigoGasto
        Get
            Return sCodigoGasto
        End Get
        Set(value)
            sCodigoGasto = value
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

    Public Property TotalGastos
        Get
            Return ObtieneTotalGasto()
        End Get
        Set(value)
            nTotalGasto = value
        End Set
    End Property

    Public Property Gasto

        Get
            Return nGasto
        End Get
        Set(value)
            Try
                nGasto = value
            Catch ex As Exception
                nGasto = 0
            End Try

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
    Public Property FechaIngreso
        Get
            Return dFechaIngreso
        End Get
        Set(value)
            dFechaIngreso = value
        End Set
    End Property

    Default Public ReadOnly Property Item(columnName As String) As String Implements IDataErrorInfo.Item

        Get
            sMensaje = isValid(columnName)
            Return sMensaje

        End Get

    End Property

    Public ReadOnly Property [Error] As String Implements IDataErrorInfo.Error
        Get
            Throw New NotImplementedException()
        End Get
    End Property

    Public Function CargaTipoGastos() As DT
        Dim tbGastos As New DT
        Try
            tbGastos.RecordSet = oCatalogoBD.TiposGasto()
            Return tbGastos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function CargaGastos() As DT
        Dim tbGastos As New DT
        Try
            tbGastos.RecordSet = oCatalogoBD.CargaGastos(sCodigo, FechaIngreso)
            Return tbGastos
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Public Function IngresaTipoGasto() As Boolean
        Try
            If Not oCatalogoBD.IngresaTipoGasto(sCodigo, sDescripcion) Then
                Mensaje = "Error al ingresar nuevo gasto."
                Return False
            End If
            Return True
        Catch ex As Exception
            Mensaje = ex
            Return False
        End Try
    End Function
    Public Function ActualizaTipoGasto() As Boolean
        Try
            oCatalogoBD.ActualizaTipoGasto(sCodigo, sDescripcion)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Public Function EliminaTipoGasto() As Boolean
        Try
            oCatalogoBD.EliminaTipoGasto(sCodigo)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    Private Function isValid(ByVal sNombre As String) As String
        bValida = True
        Select Case sNombre
            Case "Codigo"
                If String.IsNullOrEmpty(sCodigo) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.CodigoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.CodigoError
                End If

            Case "Gasto"
                If Not IsNumeric(nGasto) Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NumericoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NumericoError
                End If
                If nGasto <= 0 Then
                    bValida = False
                    oErrores.Mensaje = cErrores.NegativoError
                    oErrores.AgregaError()
                    oErrores.NumError = oErrores.NumError + 1
                    Return cErrores.NegativoError
                End If


        End Select
        Return Nothing
    End Function

    Public Function IngresaGastoProducto() As Boolean
        Try
            If oCatalogoBD.IngresaGastoProducto(sCodigo, sCodigoGasto, nGasto, dFechaIngreso) Then
                Return True
            Else
                Mensaje = "Atención. Error al ingresar gasto, revise si código no se encuentra previamente ingresado."
                Return False
            End If

        Catch ex As Exception
            Mensaje = "Atención. Error al ingresar  gasto, descripción del error :" + ex.ToString
            Return False
        End Try
    End Function

    Public Function ActualizaGastoProducto() As Boolean
        Try
            If oCatalogoBD.ActualizaGastoProducto(sCodigo, sCodigoGasto, nGasto, dFechaIngreso) Then
                Return True
            Else
                Mensaje = " Atención. Error al actualizar gasto. Revise si código no se encuentra previamente ingresado."
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al actualizar gasto, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function

    Public Function EliminaGastoProducto() As Boolean
        Try
            If oCatalogoBD.EliminaGastoProducto(sCodigo, sCodigoGasto, dFechaIngreso) Then
                Return True
            Else
                Mensaje = " Atención. Error al eliminar gasto"
                Return False
            End If

        Catch ex As Exception
            Mensaje = " Atención. Error al eliminar gasto, descripción del error: " + ex.ToString
            Return False
        End Try
    End Function
    Public Function ObtieneTotalGasto() As Double
        Dim tbGastos As New DT
        Try
            tbGastos.RecordSet = oCatalogoBD.TotalGastoProducto(sCodigo, FechaIngreso)
            If tbGastos.EOF = False Then
                nTotalGasto = tbGastos.GetValue("TOTAL")
            End If
            Return nTotalGasto
        Catch ex As Exception
            Return 0
        End Try
    End Function
End Class
