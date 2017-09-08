
Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf
Public Class UProduccion
    Private oProduccionProductos As New cProduccionProductos

    Private oValidador As New uValidador
    Private oGasto As New cGastos
    Private nTotalGasto As Double
    Private bSeleccionGrilla As Boolean = False
    Private bSeleccionaCodigoBarra = False
    Private bLimipiaControl As Boolean = True

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(cvProduccion)
    End Sub



    Private Sub UProduccion_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbProduccionProductos As New DT
        tbProduccionProductos = oProduccionProductos.CargaCBProduccionProducto()
        grdProductos.IsReadOnly = True
        cmAgregarGasto.IsEnabled = False
        txtPrecioNeto.IsEnabled = False
        txtIVA.IsEnabled = False
        txtVenta.IsEnabled = False
        RefreshGrid()
        DPIngreso.Text = Today
        If tbProduccionProductos.EOF = False Then
            cbProducto.DataContext = tbProduccionProductos.DT
        End If
        rbVenta.IsEnabled = False
        rbProduccion.IsEnabled = False
        cmdGenerarProducto.IsEnabled = False
        cmAgregarGasto.IsEnabled = False
        cmdModificar.IsEnabled = False
        cmdEliminar.IsEnabled = False
        cmAgregar.IsEnabled = True
        txtCod.Focus()

    End Sub

    Private Sub cbProducto_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbProducto.SelectionChanged
        Dim tbProduccionProductos As New DT
        Dim tbTotalGasto As New DT
        If bSeleccionGrilla = True Then
            bSeleccionGrilla = False
            Exit Sub
        End If

        Me.DataContext = Nothing
        cmdGenerarProducto.IsEnabled = False
        oProduccionProductos.ValorUnitario = 0
        'If oProduccionProductos.Codigo = "" Then
        '    oProduccionProductos.Codigo = cbProducto.SelectedValue
        'End If   
        If bSeleccionaCodigoBarra = False Then
            oProduccionProductos.Codigo = cbProducto.SelectedValue
        End If



        oProduccionProductos.Transbank = oParametros.Transbank
        oProduccionProductos.PorcentajeGanancia = oParametros.PorcentajeGanancia
        tbProduccionProductos = oProduccionProductos.CargaProduccionProducto()
        If tbProduccionProductos.EOF = False Then
            oProduccionProductos.Proceso = tbProduccionProductos.GetValue("proceso")
            Select Case tbProduccionProductos.GetValue("proceso")
                Case "V"
                    rbVenta.IsChecked = True
                    oProduccionProductos.CargaValorUnitarioProducto()
                    ObtieneValores()
                Case "P"
                    rbProduccion.IsChecked = True
                    cmdGenerarProducto.IsEnabled = True
            End Select
        End If
        tbProduccionProductos.Disconnect()
        oGasto.Codigo = cbProducto.SelectedValue
        oGasto.FechaIngreso = tbProduccionProductos.GetValue("FECHA_PRODUCCION")
        oProduccionProductos.Gasto = oGasto.ObtieneTotalGasto

        Me.DataContext = oProduccionProductos
        tbTotalGasto.Disconnect()
        bSeleccionaCodigoBarra = False
        'cmAgregarGasto.IsEnabled = True

    End Sub

    Private Sub cmdGenerarProducto_Click(sender As Object, e As RoutedEventArgs) Handles cmdGenerarProducto.Click
        Dim oWDetalleProduccionProductos As New WDetalleProduccionProducto
        oWDetalleProduccionProductos.CodigoProducto = oProduccionProductos.Codigo
        oWDetalleProduccionProductos.FechaIngreso = DPIngreso.Text
        Opacar()
        oWDetalleProduccionProductos.ShowDialog()
        Aclarar()
    End Sub

    Public Sub Opacar()
        Dim sb As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
        sb.Begin(cvProduccion)
    End Sub

    Private Sub cmAgregarGasto_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregarGasto.Click
        Dim oWGasto As New WGastos
        Opacar()
        oWGasto.Codigo = cbProducto.SelectedValue
        oWGasto.FechaIngreso = DPIngreso.Text
        oWGasto.ShowDialog()
        nTotalGasto = oWGasto.TotalGasto
        Aclarar()
        txtGasto.Text = nTotalGasto
    End Sub
    Private Sub txtValorCompra1_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtValorCompra.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtCantidad_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCantidad.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtGastod_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtGasto.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtMargen_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtMargen.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtPrecioNeto_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtPrecioNeto.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtIVA_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtIVA.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtTransbank_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtTransbank.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtVenta_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtVenta.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub cmAgregar_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregar.Click
        Dim result As MessageBoxResult
        Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
        Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
        Try
            oErrores.InicializaErrores()
            oProduccionProductos.FechaProduccion = DPIngreso.Text
            oProduccionProductos.Valida = True
            Me.DataContext = oProduccionProductos
            If rbProduccion.IsChecked = True Then
                If oProduccionProductos.CantidadProduccionProducto <= 0 Then
                    Toolkit.MessageBox.Show("Falta ingresar detalle de productos.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    Exit Sub
                End If
            End If
            If oProduccionProductos.Valida = False Then

                sb1.Begin(Me.cvProduccion)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Agrega Producción"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvProduccion)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de ingresar producción para producto?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                oErrores.InicializaErrores()
                If oProduccionProductos.IngresaProduccionProducto() Then
                    If oErrores.ConError = True Then
                        oValidador.Titulo = "Inventario"
                        oValidador.ArrayError = oErrores.ArrayError
                        oValidador.Advertencia()
                        oValidador.ShowDialog()
                        sb.Begin(Me.cvProduccion)
                    End If
                    Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oProduccionProductos.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible ingresar Producción.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub

    Private Sub txtValorCompra_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtValorCompra.LostFocus
        ObtieneValores()
    End Sub
    Private Sub ObtieneValores()
        'CargaDatos()
        ' Me.DataContext = Nothing
        oProduccionProductos.CalculaValores()
        'txtPrecioNeto.Text = oProduccionProductos.PrecioNeto
        'txtGanancia.Text = oProduccionProductos.MontoGanancia
        'txtIVA.Text = oProduccionProductos.IVA
        'txtVenta.Text = oProduccionProductos.PrecioVenta

    End Sub
    Private Sub RefreshGrid()
        Dim tbProduccionProductos As New DT
        tbProduccionProductos = oProduccionProductos.ProduccionProducto

        If tbProduccionProductos.EOF = False Then
            grdProductos.ItemsSource = tbProduccionProductos.DT.DefaultView
        Else
            grdProductos.ItemsSource = Nothing
        End If
    End Sub

    Private Sub cmdModificar_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificar.Click
        Dim result As MessageBoxResult

        Try
            CargaDatos()
            oErrores.InicializaErrores()
            oProduccionProductos.Valida = True
            Me.DataContext = oProduccionProductos
            If oProduccionProductos.Valida = False Then
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvProduccion)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Modifica Producto"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvProduccion)
                Exit Sub
            End If
            If rbProduccion.IsChecked = True Then
                If oProduccionProductos.CantidadProduccionProducto <= 0 Then
                    Toolkit.MessageBox.Show("Falta ingresar detalle de productos.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    Exit Sub
                End If
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de actualizar producción para producto?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProduccionProductos.ActualizaProduccion Then
                    Toolkit.MessageBox.Show("Operación actualizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oProduccionProductos.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible actualizar Producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub
    Private Sub LimpiaControles()
        Me.DataContext = Nothing
        bSeleccionGrilla = True
        oProduccionProductos.Codigo = ""
        oProduccionProductos.PrecioNeto = 0
        oProduccionProductos.PrecioVenta = 0
        oProduccionProductos.Transbank = 0
        oProduccionProductos.Gasto = 0
        oProduccionProductos.MontoGanancia = 0
        oProduccionProductos.PorcentajeGanancia = 0
        oProduccionProductos.Cantidad = 0
        oProduccionProductos.ValorUnitario = 0
        oProduccionProductos.IVA = 0
        oProduccionProductos.FechaProduccion = Nothing
        Me.DataContext = oProduccionProductos
        cbProducto.SelectedValue = ""
        cmAgregar.IsEnabled = True
        cmdModificar.IsEnabled = False
        cmdEliminar.IsEnabled = False
        cmAgregarGasto.IsEnabled = False
        cmdGenerarProducto.IsEnabled = False
        rbProduccion.IsChecked = False
        rbVenta.IsChecked = False
        cmAgregarGasto.IsEnabled = False
        DPIngreso.IsEnabled = True
        txtCod.Focus()


    End Sub
    Private Sub grdProductos_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdProductos.SelectionChanged
        Dim tbProduccionProductos As New DT
        Try
            bSeleccionGrilla = True
            Me.DataContext = Nothing
            If grdProductos.SelectedIndex = -1 Then Exit Sub
            cmAgregar.IsEnabled = False
            DPIngreso.IsEnabled = False
            cmdGenerarProducto.IsEnabled = False
            oProduccionProductos.Codigo = grdProductos.SelectedItem(0).ToString
            cbProducto.SelectedValue = grdProductos.SelectedItem(0).ToString
            oProduccionProductos.FechaProduccion = grdProductos.SelectedItem(1).ToString
            DPIngreso.Text = grdProductos.SelectedItem(1).ToString
            oProduccionProductos.Cantidad = grdProductos.SelectedItem(3).ToString
            oProduccionProductos.Gasto = grdProductos.SelectedItem(4).ToString
            oProduccionProductos.ValorUnitario = grdProductos.SelectedItem(5).ToString
            oProduccionProductos.PorcentajeGanancia = grdProductos.SelectedItem(6).ToString
            oProduccionProductos.PrecioNeto = grdProductos.SelectedItem(7).ToString
            oProduccionProductos.IVA = grdProductos.SelectedItem(8).ToString()
            oProduccionProductos.PrecioVenta = grdProductos.SelectedItem(9).ToString()
            oProduccionProductos.MontoGanancia = grdProductos.SelectedItem(10).ToString()
            oProduccionProductos.Transbank = oParametros.Transbank
            tbProduccionProductos = oProduccionProductos.CargaProduccionProducto()
            If tbProduccionProductos.EOF = False Then
                Select Case tbProduccionProductos.GetValue("proceso")
                    Case "V"
                        rbVenta.IsChecked = True
                        rbProduccion.IsChecked = False
                        If oProduccionProductos.ValorUnitario < 0 Then
                            oProduccionProductos.CargaValorUnitarioProducto()
                        End If

                    Case "P"
                        rbProduccion.IsChecked = True
                        rbVenta.IsChecked = False
                        ' cmdGenerarProducto.IsEnabled = True
                End Select
            End If
            tbProduccionProductos.Disconnect()
            cmdModificar.IsEnabled = True
            cmdEliminar.IsEnabled = True
            cmAgregarGasto.IsEnabled = True
            'cmdGenerarProducto.IsEnabled = True
            cmAgregarGasto.IsEnabled = True

            Me.DataContext = oProduccionProductos

        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        LimpiaControles()
    End Sub

    Private Sub cmdEliminar_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminar.Click
        Dim result As MessageBoxResult
        Try
            oProduccionProductos.Codigo = txtCod.Text
            result = Toolkit.MessageBox.Show("¿Confirme intención de eliminar producción?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProduccionProductos.EliminaProduccionProducto() Then
                    Toolkit.MessageBox.Show("Eliminación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                Else
                    Toolkit.MessageBox.Show(oProduccionProductos.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            RefreshGrid()
            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible eliminar producción.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub

    Private Sub CargaDatos()
        oProduccionProductos.Codigo = txtCod.Text
        oProduccionProductos.ValorUnitario = txtValorCompra.Text
        oProduccionProductos.Cantidad = txtCantidad.Text
        oProduccionProductos.Gasto = txtGasto.Text
        oProduccionProductos.PorcentajeGanancia = txtMargen.Text
        oProduccionProductos.MontoGanancia = txtGanancia.Text
        oProduccionProductos.Transbank = txtTransbank.Text
        oProduccionProductos.PrecioNeto = txtPrecioNeto.Text
        oProduccionProductos.IVA = txtIVA.Text
        oProduccionProductos.PrecioVenta = txtVenta.Text
        oProduccionProductos.FechaIngreso = DPIngreso.Text
        If rbProduccion.IsChecked = True Then
            oProduccionProductos.Proceso = "P"
        Else
            oProduccionProductos.Proceso = "V"
        End If
        Me.DataContext = oProduccionProductos
    End Sub

    Private Sub txtGasto_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtGasto.LostFocus
        Try
            Dim nGasto As Double
            oGasto.Codigo = txtCod.Text
            oGasto.FechaIngreso = DPIngreso.Text
            ' Me.DataContext = oGasto
            nGasto = oGasto.ObtieneTotalGasto
            txtGasto.Text = nGasto
            If nGasto <> oGasto.Gasto Then
                Toolkit.MessageBox.Show("Atención, monto de gasto no coincide con detalle ingresado.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            End If

        Catch ex As Exception

        End Try


    End Sub


    Private Sub txtMargen_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtMargen.LostFocus
        ObtieneValores()
    End Sub


    Private Sub txtTransbank_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtTransbank.LostFocus
        ObtieneValores()
    End Sub


    Private Sub txtCod_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCod.KeyDown
        If e.Key = Key.Enter Then

            bSeleccionaCodigoBarra = True
            oProduccionProductos.Codigo = txtCod.Text
            cbProducto.SelectedValue = txtCod.Text

        End If
    End Sub

    Private Sub txtCod_TextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCod.TextInput
        bSeleccionaCodigoBarra = True
        txtCod.Text = txtCod.Text + e.Text

    End Sub

    Private Sub txtCantidad_KeyDown(sender As Object, e As KeyEventArgs) Handles txtCantidad.KeyDown
        If e.Key = Key.Enter Then
            bSeleccionaCodigoBarra = True
            AccionInvalida()

        End If
    End Sub

    Private Sub AccionInvalida()
        Toolkit.MessageBox.Show("Acción no valida,no es posible ingresar dato a través de lector de código de barras.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        LimpiaControles()
    End Sub

    Private Sub txtPrecioNeto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtPrecioNeto.KeyDown
        If e.Key = Key.Enter Then
            bSeleccionaCodigoBarra = True
            AccionInvalida()

        End If
    End Sub

    Private Sub txtTransbank_KeyDown(sender As Object, e As KeyEventArgs) Handles txtTransbank.KeyDown
        If e.Key = Key.Enter Then
            bSeleccionaCodigoBarra = True
            AccionInvalida()
        End If
    End Sub

    Private Sub txtMargen_KeyDown(sender As Object, e As KeyEventArgs) Handles txtMargen.KeyDown
        If e.Key = Key.Enter Then
            bSeleccionaCodigoBarra = True
            AccionInvalida()
        End If
    End Sub

    Private Sub txtValorCompra_KeyDown(sender As Object, e As KeyEventArgs) Handles txtValorCompra.KeyDown
        If e.Key = Key.Enter Then
            bSeleccionaCodigoBarra = True
            AccionInvalida()
        End If
    End Sub

    Private Sub txtVenta_KeyDown(sender As Object, e As KeyEventArgs) Handles txtVenta.KeyDown
        If e.Key = Key.Enter Then
            bSeleccionaCodigoBarra = True
            AccionInvalida()
        End If
    End Sub

    Private Sub txtGasto_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGasto.KeyDown
        If e.Key = Key.Enter Then
             bSeleccionaCodigoBarra = True
            AccionInvalida()
        End If
    End Sub

    Private Sub txtGanancia_KeyDown(sender As Object, e As KeyEventArgs) Handles txtGanancia.KeyDown
        If e.Key = Key.Enter Then
            bSeleccionaCodigoBarra = True
            AccionInvalida()
        End If
    End Sub

    Private Sub txtCod_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCod.PreviewTextInput
        If bLimipiaControl = True Then
            bLimipiaControl = False
            txtCod.Text = ""
        End If
    End Sub

    Private Sub txtCod_PreviewLostKeyboardFocus(sender As Object, e As KeyboardFocusChangedEventArgs) Handles txtCod.PreviewLostKeyboardFocus
        bLimipiaControl = True
    End Sub
End Class
