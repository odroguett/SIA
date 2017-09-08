Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf
Public Class WVentas
    Private oProductosVenta As New cProductosVenta
    Private oClientes As New cClientes
    Private bGrilla As Boolean = False
    Private nIdVenta As Double
    Private sTipoProceso As String
    Private sCliente As String
    Private nTotalProducto As Double
    Private nTotalNeto As Double
    Private nTotalImpuesto As Double
    Private nTotalVenta As Double
    Private sCodigoBarra As String
    Private sCodigoBarraAnterior As String
    Private iCantidad As Double = 0
    Private iCantidadAnt As Double
    Private bCodigoBarra As Boolean = False
    Private bPrimer As Boolean = True
    Private oValidador As New uValidador
    Public Event ActualizaVenta(ByVal nTotalProductosVentaDiaria As Double, ByVal nMontoNetaVentaDiaria As Double, ByVal nMontoTotalVentaDiaria As Double)
    Public Event ActualizaVentaModificacion()



    Public Property IdVenta
        Get
            Return nIdVenta
        End Get
        Set(value)
            Try
                nIdVenta = value
            Catch ex As Exception
                nIdVenta = 0
            End Try

        End Set
    End Property

    Public Property TipoProceso
        Get
            Return sTipoProceso
        End Get
        Set(value)
            sTipoProceso = value
        End Set
    End Property
    Public Property Cliente
        Get
            Return sCliente
        End Get
        Set(value)
            sCliente = value
        End Set
    End Property
    Public Property TotalProducto
        Get
            Return nTotalProducto
        End Get
        Set(value)
            Try
                nTotalProducto = value
            Catch ex As Exception
                nTotalProducto = 0
            End Try

        End Set
    End Property
    Public Property TotalNeto
        Get
            Return nTotalNeto
        End Get
        Set(value)
            Try
                nTotalNeto = value
            Catch ex As Exception
                nTotalNeto = 0
            End Try

        End Set
    End Property
    Public Property TotalImpuesto
        Get
            Return nTotalImpuesto
        End Get
        Set(value)
            Try
                nTotalImpuesto = value
            Catch ex As Exception
                nTotalImpuesto = 0
            End Try

        End Set
    End Property
    Public Property TotalVenta
        Get
            Return nTotalVenta
        End Get
        Set(value)
            Try
                nTotalVenta = value
            Catch ex As Exception
                nTotalVenta = 0
            End Try

        End Set
    End Property
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()
        ' Add any initialization after the InitializeComponent() call.
        oErrores.InicializaErrores()
    End Sub
    Private Sub WVentas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbProductos As New DT
        Dim tbClientes As New DT
        '   grdVentas.IsReadOnly = True
        cmdVenta.IsEnabled = False
        cmdEliminar.IsEnabled = False
        cmdModificar.IsEnabled = False
        txtTotalVenta.IsEnabled = False
        DPFechaVenta.Text = Today
        DPFechaVenta.IsEnabled = False
        txtIDVenta.IsEnabled = False
        ' txtMontoProducto.IsEnabled = False
        txtTotalProductos.IsEnabled = False
        txtTotalImpuesto.IsEnabled = False
        txtTotalVenta.IsEnabled = False
        txtTotalNeto.IsEnabled = False
        txtTotalVentaProducto.IsEnabled = False
        ' txtMontoProducto.IsEnabled = False
        oProductosVenta.CargaProductosCB()
        Cbproducto.ItemsSource = oProductosVenta.ListaProductosCB
        'If sTipoProceso = "M" Then
        '    cmdVenta.Visibility = Visibility.Hidden
        '    '   txtCod.IsEnabled = False
        '    oProductosVenta.IdVenta = nIdVenta
        '    oProductosVenta.TotalProductos = nTotalProducto
        '    oProductosVenta.MontoTotalNeto = nTotalNeto
        '    oProductosVenta.MontoTotalVenta = nTotalVenta
        '    oProductosVenta.TotalImpuesto = nTotalImpuesto
        '    oClientes.Codigo = sCliente
        '    tbClientes = oClientes.CargaCBClientes
        '    If tbClientes.EOF = False Then
        '        cbCliente.DataContext = tbClientes.DT
        '        PosicionaCombo(cbCliente, sCliente)
        '    End If
        RefreshData()
        'Else
        '    oProductosVenta.ObtieneIDNumeroVenta()
        '    oProductosVenta.EliminaDetalleVentasProduccionID()
        '    tbClientes = oClientes.CargaCBClientes
        '    If tbClientes.EOF = False Then
        '        cbCliente.DataContext = tbClientes.DT
        '        PosicionaCombo(cbCliente, "01")
        '    End If
        'End If
        '   Me.DataContext = oProductosVenta
        '  txtCod.Focus()
    End Sub

    'Private Sub txtCantidad_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCantidad.PreviewTextInput
    '    Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

    '    If (ascci >= 48 And ascci <= 57) Then
    '        e.Handled = False
    '    Else
    '        e.Handled = True
    '    End If



    'End Sub

    Private Sub SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        Try
            Dim Combo As ComboBox = sender
            oProductosVenta.ListaVenta.Add(New ListaVentas(Combo.SelectedItem.Codigo, Combo.SelectedItem.Descripcion, 2000, 0, 1))
            bCodigoBarra = False
            'End If
            bGrilla = False

        Catch ex As Exception

        End Try
        'grdVentas.DataContext = Nothing
        'If bGrilla = True Then
        'Else
        '    If bCodigoBarra = True Then
        '        oProductosVenta.TotalVentaProducto = 0
        '        Recorrelistaproductos()
        '    Else
        '    Me.DataContext = Nothing
        ' grdVentas.DataContext = oProductosVenta.ListaVenta

    End Sub


    Private Sub Recorrelistaproductos()
        For Each RecorreListaProductos As Productos In oProductosVenta.ListaProductos
            '    If UCase(CStr(RecorreListaProductos.Codigo)) = cbProducto.SelectedValue Then
            oProductosVenta.ValorProducto = RecorreListaProductos.ValorVenta
            '   End If
        Next
    End Sub

    Private Sub cmAgregar_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregar.Click

        Try
            Cargadatos()
            oProductosVenta.ValidaVenta = True
            Me.DataContext = oProductosVenta
            If oProductosVenta.ValidaVenta = False Then
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvVentas)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Agrega producto venta"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvVentas)
                Exit Sub
            End If
            If oProductosVenta.IngresaDetalleVentasProduccion() Then
                cmdVenta.IsEnabled = True
            End If
            txtTotalProductos.Text = oProductosVenta.TotalProductos
            txtTotalNeto.Text = oProductosVenta.MontoTotalNeto
            txtTotalImpuesto.Text = oProductosVenta.TotalImpuesto
            txtTotalVenta.Text = oProductosVenta.MontoTotalVenta
            RefreshData()
            LimpiarDetalle()
            '  txtCod.Focus()
        Catch ex As Exception
        End Try

    End Sub
    Private Sub RefreshData()
        Dim tbDetalle As New DT

        'tbDetalle = oProductosVenta.ObtieneDetalleVentasProduccion()
        'If tbDetalle.EOF = False Then
        '    grdVentas.ItemsSource = tbDetalle.DT.DefaultView
        '    ' grdProveedores.SelectedValuePath = "CODIGO_PROVEEDOR"
        'Else
        '    grdVentas.ItemsSource = Nothing
        'End If
        grdVentas.ItemsSource = oProductosVenta.ListaVenta
        '  grdVentas.DataContext = oProductosVenta
    End Sub
    Private Sub PosicionaCombo(ByVal cControl As ComboBox, ByVal sValor As String)
        cControl.SelectedValue = sValor
    End Sub
    Public Sub Cargadatos()
        oProductosVenta.IdVenta = txtIDVenta.Text
        ' oProductosVenta.Cantidad = txtCantidad.Text
        oProductosVenta.ValorProducto = txtTotalVentaProducto.Text
        '  oProductosVenta.Codigo = cbProducto.SelectedValue

    End Sub

    Private Sub LimpiarDetalle()
        Me.DataContext = Nothing
        bGrilla = True
        ' cbProducto.SelectedValue = ""
        '  txtMontoProducto.Text = ""
        oProductosVenta.Cantidad = 0
        oProductosVenta.TotalVentaProducto = 0
        oProductosVenta.ValorProducto = 0
        oProductosVenta.Codigo = ""
        iCantidad = 0
        Me.DataContext = oProductosVenta
        cmdEliminar.IsEnabled = False
        cmdModificar.IsEnabled = False
        cmAgregar.IsEnabled = True
        ' cbProducto.IsEnabled = True
    End Sub
    Private Sub Limpiar()
        Me.DataContext = Nothing
        oProductosVenta.ValorProducto = 0
        oProductosVenta.Cantidad = 0
        oProductosVenta.TotalVentaProducto = 0
        oProductosVenta.TotalProductos = 0
        oProductosVenta.MontoTotalNeto = 0
        oProductosVenta.TotalImpuesto = 0
        oProductosVenta.MontoTotalVenta = 0
        oProductosVenta.ValorProducto = 0
        ' cbProducto.SelectedValue = ""
        Me.DataContext = oProductosVenta
    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        LimpiarDetalle()
    End Sub

    Private Sub cmdEliminar_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminar.Click
        '  oProductosVenta.Cantidad = txtCantidad.Text
        oProductosVenta.TotalVentaProducto = txtTotalVentaProducto.Text
        If oProductosVenta.EliminaDetalleVentasProduccion() Then
            oProductosVenta.EliminaMontos()
            If grdVentas.Items.Count = 0 Then cmdVenta.IsEnabled = False

        Else

        End If
        cmdVenta.IsEnabled = True
        Me.DataContext = oProductosVenta
        RefreshData()
        LimpiarDetalle()
        '  txtCod.Focus()
    End Sub

    'Private Sub grdVentas_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdVentas.SelectionChanged
    '    Try
    '        Me.DataContext = Nothing
    '        cmdEliminar.IsEnabled = True
    '        cmdModificar.IsEnabled = True
    '        cmAgregar.IsEnabled = False
    '        '      If sTipoProceso = cConstantes.MODIFICAR Then cbProducto.IsEnabled = False
    '        If grdVentas.SelectedIndex = -1 Then Exit Sub
    '        bGrilla = True
    '        '    cbProducto.SelectedValue = grdVentas.SelectedItem(1).ToString
    '        oProductosVenta.IdVenta = grdVentas.SelectedItem(0).ToString
    '        oProductosVenta.Codigo = grdVentas.SelectedItem(1).ToString
    '        oProductosVenta.Cantidad = grdVentas.SelectedItem(2).ToString
    '        oProductosVenta.TotalVentaProducto = grdVentas.SelectedItem(3).ToString
    '        Recorrelistaproductos()
    '        Me.DataContext = oProductosVenta
    '    Catch ex As Exception
    '        '   cbProducto.SelectedValue = ""
    '        bGrilla = False
    '    End Try
    'End Sub
    Private Sub cmdModificar_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificar.Click
        Cargadatos()
        oProductosVenta.ValidaVenta = True
            Me.DataContext = oProductosVenta
        If oProductosVenta.ValidaVenta = False Then
            Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
            Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
            sb1.Begin(Me.cvVentas)
            sb.Begin(oValidador.grValidador)
            oValidador.Titulo = "Modifica producto venta"
            oValidador.ArrayError = oErrores.ArrayError
            oValidador.Advertencia()
            oValidador.ShowDialog()
            sb.Begin(Me.cvVentas)
            Exit Sub
        End If
        If oProductosVenta.ActualizaDetalleVentasProduccion() Then
            ' cmdVenta.IsEnabled = True
            'If sTipoProceso = cConstantes.MODIFICAR Then
            '    oProductosVenta.FechaVenta = DPFechaVenta.Text
            '    oProductosVenta.ActualizaVentaDiaria(sTipoProceso)
            'End If
        End If

        txtTotalProductos.Text = oProductosVenta.TotalProductos
        txtTotalNeto.Text = oProductosVenta.MontoTotalNeto
        txtTotalImpuesto.Text = oProductosVenta.TotalImpuesto
        txtTotalVenta.Text = oProductosVenta.MontoTotalVenta
        '    txtCod.Focus()
        RefreshData()
        LimpiarDetalle()
    End Sub

    Private Sub cmdVenta_Click(sender As Object, e As RoutedEventArgs) Handles cmdVenta.Click
        Try
            Dim result As MessageBoxResult
            If grdVentas.Items.Count = 0 Then
                Toolkit.MessageBox.Show("Al menos debe ingresar un producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                Exit Sub
            End If
            oProductosVenta.TotalImpuesto = txtTotalImpuesto.Text
            oProductosVenta.CodigoCliente = cbCliente.SelectedValue
            oProductosVenta.MontoTotalVenta = txtTotalVenta.Text
            oProductosVenta.TotalProductos = txtTotalProductos.Text
            oProductosVenta.MontoTotalNeto = txtTotalNeto.Text
            oProductosVenta.FechaVenta = DPFechaVenta.Text
            result = Toolkit.MessageBox.Show("¿Confirme intención de ingresar venta?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProductosVenta.IngresaVentas(sTipoProceso) Then
                    Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RaiseEvent ActualizaVenta(oProductosVenta.TotalProductos, oProductosVenta.MontoTotalNeto, oProductosVenta.MontoTotalVenta)
                Else
                    Toolkit.MessageBox.Show(oProductosVenta.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)

                End If
            End If

            Me.DataContext = oProductosVenta
            grdVentas.ItemsSource = Nothing
            grdVentas.Items.Clear()
            Limpiar()
            If sTipoProceso = cConstantes.MODIFICAR Then Me.Close()
            txtIDVenta.Text = oProductosVenta.IdVenta
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible ingresar venta.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(cvVentas)
    End Sub

    'Private Sub txtCod_TextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCod.TextInput
    '    oProductosVenta.Codigo = oProductosVenta.Codigo + e.Text
    'End Sub

    'Private Sub txtCod_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCod.KeyDown

    '    If e.Key = Key.Enter Then
    '        bCodigoBarra = True

    '        If sCodigoBarra = "" Or sCodigoBarra = oProductosVenta.Codigo Then
    '            iCantidad = iCantidad + 1
    '        Else
    '            iCantidad = 1
    '        End If
    '        '    cbProducto.SelectedValue = oProductosVenta.Codigo
    '        'If cbProducto.SelectedValue = Nothing Then
    '        '    Me.DataContext = Nothing
    '        '    Toolkit.MessageBox.Show("Producto no reconocido, No es posible ingresaer en venta.", "SIA", MessageBoxButton.OK, MessageBoxImage.Error)
    '        '    oProductosVenta.Codigo = ""
    '        '    sCodigoBarra = ""
    '        '    oProductosVenta.Cantidad = 0
    '        '    oProductosVenta.ValorProducto = 0
    '        '    oProductosVenta.TotalVentaProducto = 0
    '        '    iCantidad = 0
    '        '    Me.DataContext = oProductosVenta
    '        '    Exit Sub
    '        'End If
    '        sCodigoBarra = oProductosVenta.Codigo
    '        oProductosVenta.Cantidad = iCantidad
    '        'oProductosVenta.ValorProducto = oProductosVenta.ValorProducto
    '        oProductosVenta.TotalVentaProducto = oProductosVenta.Cantidad * oProductosVenta.ValorProducto
    '        Me.DataContext = Nothing
    '        Me.DataContext = oProductosVenta
    '        bPrimer = True
    '        bCodigoBarra = True
    '    Else

    '        If bPrimer = True Then
    '            '     txtCod.Text = ""
    '            bPrimer = False
    '        End If
    '        ' e.Handled = False
    '    End If
    'End Sub
    'Private Sub txtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown
    '    If e.Key = Key.Enter Then
    '        bCodigoBarra = True
    '        Me.DataContext = Nothing
    '        oProductosVenta.Cantidad = iCantidad
    '        oProductosVenta.Codigo = oProductosVenta.Codigo
    '        oProductosVenta.Cantidad = oProductosVenta.Cantidad
    '        oProductosVenta.ValorProducto = oProductosVenta.ValorProducto
    '        oProductosVenta.TotalVentaProducto = iCantidad + oProductosVenta.ValorProducto
    '        Me.DataContext = oProductosVenta


    '    End If
    'End Sub

    'Private Sub txtCantidad_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtCantidad.TextChanged
    '    txtTotalVentaProducto.Text = oProductosVenta.Cantidad * oProductosVenta.ValorProducto
    '    If txtCantidad.Text = "" Or txtMontoProducto.Text = "" Then Exit Sub
    'End Sub


    Private Sub txtTotalVentaProducto_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtTotalVentaProducto.TextChanged
        oProductosVenta.TotalVentaProducto = txtTotalVentaProducto.Text

    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As RoutedEventArgs) Handles cmdSalir.Click
        Me.Close()
    End Sub

    Private Sub WVentas_Unloaded(sender As Object, e As RoutedEventArgs) Handles Me.Unloaded
        If sTipoProceso = cConstantes.MODIFICAR Then
            oProductosVenta.TotalImpuesto = txtTotalImpuesto.Text
            oProductosVenta.CodigoCliente = cbCliente.SelectedValue
            oProductosVenta.MontoTotalVenta = txtTotalVenta.Text
            oProductosVenta.TotalProductos = txtTotalProductos.Text
            oProductosVenta.MontoTotalNeto = txtTotalNeto.Text
            oProductosVenta.FechaVenta = DPFechaVenta.Text

            If oProductosVenta.ActualizaVenta() Then
                RaiseEvent ActualizaVentaModificacion()
            End If
        End If
        ' End Ifs
    End Sub

    Private Sub grdVentas_CellEditEnding(sender As Object, e As DataGridCellEditEndingEventArgs) Handles grdVentas.CellEditEnding
        Dim sValor As String = grdVentas.SelectedItem.codigoProducto
        MsgBox(sValor)
    End Sub





    'Private Sub grdVentas_KeyUp(sender As Object, e As KeyEventArgs) Handles grdVentas.KeyUp
    '    If e.Key = Key.Delete Then
    '        e.Handled = True
    '        If grdVentas.SelectedIndex = 0 Then
    '            oProductosVenta.ListaVenta.RemoveAt(grdVentas.SelectedIndex)
    '        Else
    '            oProductosVenta.ListaVenta.RemoveAt(grdVentas.SelectedIndex - 1)
    '        End If


    '    End If
    'End Sub


End Class
