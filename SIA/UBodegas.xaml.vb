Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf
Public Class UBodegas
    Private oProveedores As New cProveedores
    Private oBodega As New cBodegas
    Private oValidador As New uValidador
    Private oProductosBodega As New cProductosBodega
    Private nNumeroIngreso As Double
    Private sCodigoProducto As String
    Private sProceso As String
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(grdBodega)
    End Sub

    Private Sub UBodegas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbProveedores As New DT
        tbProveedores = oProveedores.CargaCBProveedores
        grdIngresos.IsReadOnly = True
        grdDetalle.IsReadOnly = True
        cmdModificar.IsEnabled = False
        cmAgregarDetalle.IsEnabled = False
        cmdEliminarDetalle.IsEnabled = False
        cmdModificaDetalle.IsEnabled = False
        If tbProveedores.EOF = False Then
            cbProveedor.DataContext = tbProveedores.DT
        End If
        txtNumeroIngreso.IsEnabled = False
        RefreshGrid()
        DPIngreso.Text = Today


    End Sub
    Private Sub RefreshGrid()
        Dim tbBodega As New DT
        tbBodega = oBodega.CargaDatosBodega()
        If tbBodega.EOF = False Then
            grdIngresos.ItemsSource = tbBodega.DT.DefaultView
        Else
            grdIngresos.ItemsSource = Nothing
        End If
    End Sub

    Private Sub RefreshDetalle()
        Dim tbDetalle As New DT
        oProductosBodega.NumeroIngreso = txtNumeroIngreso.Text
        tbDetalle = oProductosBodega.CargaDetalleProductosBodega()
        If tbDetalle.EOF = False Then
            grdDetalle.ItemsSource = tbDetalle.DT.DefaultView
            ' grdProveedores.SelectedValuePath = "CODIGO_PROVEEDOR"
        Else
            grdDetalle.ItemsSource = Nothing
        End If
    End Sub
    Public Sub LimpiaControles()
        Me.DataContext = Nothing
        oBodega.NumeroIngreso = 0
        oBodega.NumeroOrden = 0
        oBodega.Valor = 0
        oBodega.ValorCompra = 0
        DPIngreso.Text = ""
        DPIngreso.Text = Today
        cmdModificar.IsEnabled = False
        cmdEliminar.IsEnabled = False
        cmAgregar.IsEnabled = True
        cbProveedor.SelectedValue = ""
        Me.DataContext = oBodega
    End Sub
    Private Sub txtNumeroOrden_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtNumeroOrden.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtNumeroIngreso_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtNumeroIngreso.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtValorCompra_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtValorCompra.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub cmdEliminar_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminar.Click
        Dim result As MessageBoxResult
        Try
            nNumeroIngreso = txtNumeroIngreso.Text
            oProductosBodega.NumeroIngreso = nNumeroIngreso
            oBodega.NumeroIngreso = nNumeroIngreso

            result = Toolkit.MessageBox.Show("¿Confirme intención de eliminar Compra?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then

                If oProductosBodega.EliminaProductoBodega() Then

                    If oBodega.EliminaIngresoBodega() Then
                        Toolkit.MessageBox.Show("Operación realizada con exito", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                        RefreshDetalle()
                        RefreshGrid()
                    Else
                        Toolkit.MessageBox.Show(oProductosBodega.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    End If

                Else
                    Toolkit.MessageBox.Show(oBodega.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            LimpiaControles()

        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible eliminar compra.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub

    Private Sub cmAgregar_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregar.Click
        Dim result As MessageBoxResult
        Try
            oBodega.ObtieneNumeroIngreso()
            CargaDatos()
            oBodega.Valida = True
            Me.DataContext = oBodega
            If oBodega.Valida = False Then
                'MsgBox("Datos ingresados se encuentran erroneos, no es posible continuar")
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvIngresos)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Agrega Datos Bodega"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvIngresos)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de ingresar compra?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oBodega.IngresaDatosBodega Then
                    Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oBodega.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible ingresar compra.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub
    Private Sub CargaDatos()

        oBodega.NumeroOrden = txtNumeroOrden.Text
        oBodega.ValorCompra = txtValorCompra.Text
        oBodega.Proveedor = cbProveedor.SelectedValue
        oBodega.FechaIngreso = DPIngreso.Text
    End Sub

    Private Sub cmdModificar_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificar.Click
        Dim result As MessageBoxResult
        Try
            CargaDatos()
            oBodega.Valida = True
            Me.DataContext = oBodega
            If oBodega.Valida = False Then
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvIngresos)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Modifica Bodega"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvIngresos)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de actualizar datos?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oBodega.ActualizaDatosBodega Then
                    Toolkit.MessageBox.Show("Actualización realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oBodega.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            Else
                Toolkit.MessageBox.Show(oBodega.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            End If
            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible actualizar compra.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub

    Private Sub grdIngresos_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdIngresos.SelectionChanged
        Try
            If grdIngresos.SelectedIndex = -1 Then Exit Sub
            txtNumeroIngreso.Text = grdIngresos.SelectedItem(0).ToString
            cbProveedor.SelectedValue = grdIngresos.SelectedItem(1).ToString
            txtNumeroOrden.Text = grdIngresos.SelectedItem(3).ToString
            txtValorCompra.Text = grdIngresos.SelectedItem(4).ToString
            DPIngreso.Text = grdIngresos.SelectedItem(2).ToString
            RefreshDetalle()
            cmAgregar.IsEnabled = False
            cmdModificar.IsEnabled = True
            cmAgregarDetalle.IsEnabled = True
            cmdEliminar.IsEnabled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        LimpiaControles()
    End Sub

    Private Sub cmAgregarDetalle_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregarDetalle.Click
        Dim oWDetalleBodega As New WDetalleBodega

        nNumeroIngreso = txtNumeroIngreso.Text
        oWDetalleBodega.NumeroIngreso = nNumeroIngreso
        oWDetalleBodega.CodigoProducto = Nothing
        Opacar()
        oWDetalleBodega.ShowDialog()
        RefreshDetalle()
        oProductosBodega.NumeroIngreso = nNumeroIngreso
        txtValorCompra.Text = oProductosBodega.ValorCompra()
        Aclarar()

    End Sub

    Public Sub Opacar()
        Dim sb As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
        sb.Begin(grdBodega)
    End Sub

    Private Sub grdDetalle_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdDetalle.SelectionChanged
        Try
            If grdDetalle.SelectedIndex = -1 Then Exit Sub
            nNumeroIngreso = grdDetalle.SelectedItem(0).ToString
            sCodigoProducto = grdDetalle.SelectedItem(1).ToString
            cmdEliminarDetalle.IsEnabled = True
            cmdModificaDetalle.IsEnabled = True
        Catch ex As Exception
        End Try
    End Sub

    Private Sub cmdEliminarDetalle_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminarDetalle.Click
        Try
            Dim result As MessageBoxResult
            oProductosBodega.NumeroIngreso = nNumeroIngreso
            oProductosBodega.Codigo = sCodigoProducto
            result = Toolkit.MessageBox.Show("Advertencia!!, ¿Esta seguro que quiere eliminar producto?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProductosBodega.EliminaDetalleProductoBodega() Then
                    Toolkit.MessageBox.Show("Operación relizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                Else
                    Toolkit.MessageBox.Show(oProductosBodega.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
                txtValorCompra.Text = oProductosBodega.ValorCompra()
                RefreshDetalle()
            End If

        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible eliminar producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub

    Private Sub cmdModificaDetalle_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificaDetalle.Click

        Dim oWDetalleBodega As New WDetalleBodega
        Try
            nNumeroIngreso = txtNumeroIngreso.Text
            oWDetalleBodega.NumeroIngreso = CStr(nNumeroIngreso)
            oWDetalleBodega.TipoProceso = "M"
            oWDetalleBodega.CodigoProducto = sCodigoProducto
            Opacar()
            oWDetalleBodega.ShowDialog()
            RefreshDetalle()
            oProductosBodega.NumeroIngreso = nNumeroIngreso
            txtValorCompra.Text = oProductosBodega.ValorCompra()
            Aclarar()
        Catch ex As Exception

        End Try
    End Sub



End Class
