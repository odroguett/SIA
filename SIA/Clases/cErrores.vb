Public Class cErrores
    Private sError As String
    Private sCodigo As String
    Private bValida As Boolean
    Private iNumError As Integer
    Private sArrayError As New ArrayList
    Public Const CodigoError = "Codigo de producto no puede ser vacio."
    Public Const MailError = "Dirección de correo invalida.Vuelva a ingresar."
    Public Const RUTError = "Rut ingresado no es valido."
    Public Const TelefonoError = "Telefono ingresado no tiene un formato valido."
    Public Const NumericoError = "Valor del campo debe ser numerico."
    Public Const NegativoError = "Valor del campo debe ser mayor que cero."
    Public Const NumeroIngresoError = "Número de ingreso no valido."
    Public Const NumeroOrdenError = "Debe ingresar número de orden"
    Public Const ProveedorError = "Debe ingresar Proveedor"
    Public Const ValorCompraError = "Debe ingresar Valor Compra."
    Public Const FechaError = "Debe ingresar Fecha."
    Public Const FechaMayorError = "Fecha no puede ser mayor."
    Public Const FechaMenorError = "Fecha no puede ser menor."
    Public Const DescripcionProductoError = "Descripción de producto no puede ser vacia."
    Public Const TipoProductoError = "Debe ingresar tipo de producto."
    Public Const TipoUnidad1Error = "Debe ingresar tipo de unidad (1)."
    Public Const TipoUnidad2Error = "Debe ingresar tipo de unidad (2)."
    Public Const TipoUnidad3Error = "Debe ingresar tipo de unidad (3)."
    Public Const ErrorCantidadVenta = "Cantidad de productos debe ser mayor que cero."
    Public Const ValorProducto = "Valor de producto para venta debe ser mayor que cero."
    Public Const TotalValorProducto = " Precio total  de producto para venta debe ser mayor que cero."
    Public Property Mensaje()
        Get
            Return sError
        End Get
        Set(ByVal value)
            sError = value
        End Set
    End Property
    Public Property Codigo()
        Get
            Return sCodigo
        End Get
        Set(ByVal value)
            sCodigo = value
        End Set
    End Property

    Public Property ConError()
        Get
            Return bValida
        End Get
        Set(ByVal value)
            bValida = value
        End Set
    End Property

    Public Property NumError()
        Get
            Return iNumError
        End Get
        Set(ByVal value)
            iNumError = value
        End Set
    End Property

    Public ReadOnly Property ArrayError()
        Get
            Return sArrayError
        End Get
    End Property

    Public Sub InicializaErrores()
        Mensaje = ""
        Codigo = ""
        ConError = False
        sArrayError = Nothing
        sArrayError = New ArrayList
        iNumError = 0
    End Sub

    Public Sub AgregaError()
        sArrayError.Add(sError)
    End Sub
End Class
