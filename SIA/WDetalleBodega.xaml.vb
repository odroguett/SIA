Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf
Public Class WDetalleBodega
    Private oProductosBodega As New cProductosBodega
    Private oBodega As New cBodegas
    Private oTiposUnidad As New cTiposUnidad
    Private oGruposProducto As New cGruposProducto
    Private nNumeroIngreso As Double
    Private sCodigoProducto As String
    Private oValidador As New uValidador
    Private sTipoProceso As String = cConstantes.AGREGAR
    Private sProceso As String
    Private bCargaInicial As Boolean = True
    Public Property NumeroIngreso
        Get
            Return nNumeroIngreso
        End Get
        Set(value)
            nNumeroIngreso = value
        End Set
    End Property

    Public WriteOnly Property TipoProceso
        Set(value)
            sTipoProceso = value
        End Set
    End Property

    Public WriteOnly Property Proceso
        Set(value)
            sProceso = value
        End Set
    End Property
    Public WriteOnly Property CodigoProducto
        Set(value)
            sCodigoProducto = value
        End Set
    End Property
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub WDetalleBodega_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbTipoProducto As New DT
        Dim tbTipoUnidad As New DT
        Dim tbProductoBOdega As New DT
        Dim tbAux As New DT
        Dim sNumero As String = 1
        Dim sProceso As String

        txtNumeroIngreso.IsEnabled = False
        oProductosBodega.NumeroIngreso = nNumeroIngreso
        tbTipoUnidad = oTiposUnidad.CBTiposUnidad
        If tbTipoUnidad.EOF = False Then
            cbUnidad1.DataContext = tbTipoUnidad.DT
            cbUnidad2.DataContext = tbTipoUnidad.DT
            cbUnidad3.DataContext = tbTipoUnidad.DT
        End If
        cbUnidad1.SelectedValue = ""
        tbTipoProducto = oProductosBodega.ProductosDetalleBodega()
        If tbTipoProducto.EOF = False Then
            cbDetalle.DataContext = tbTipoProducto.DT
        End If
        tbTipoProducto.Disconnect()
        tbTipoUnidad.Disconnect()
        If sTipoProceso = cConstantes.MODIFICAR Then
            cmAgregar.Content = "Modificar"
            cbUnidad1.IsEnabled = False
            cbUnidad2.IsEnabled = False
            cbUnidad3.IsEnabled = False
            cbDetalle.IsEnabled = False
            txtCantidad2.IsEnabled = False
            txtCantidad3.IsEnabled = False
            oProductosBodega.NumeroIngreso = nNumeroIngreso
            tbProductoBOdega = oProductosBodega.CargaDetalleProductosBodega(sCodigoProducto)
            sProceso = oProductosBodega.ProcesoProductos(sCodigoProducto)
            If tbProductoBOdega.EOF = False Then
                cbDetalle.SelectedValue = tbProductoBOdega.GetValue("codigo_producto")
                oProductosBodega.Valor = tbProductoBOdega.GetValue("valor_total")
                oProductosBodega.ValorUnitario = tbProductoBOdega.GetValue("valor_unitario")
                oProductosBodega.TotalProductos = tbProductoBOdega.GetValue("total_productos")
                oProductosBodega.ValorUnCompra = tbProductoBOdega.GetValue("valor_unitario_compra")
                oProductosBodega.Codigo = sCodigoProducto
                tbAux = oProductosBodega.CargaDesgloseProductoBodega
                Do While tbAux.EOF = False
                    If sNumero = 1 Then
                        cbUnidad1.SelectedValue = tbAux.GetValue("tipo_unidad")
                        oProductosBodega.Cantidad1 = tbAux.GetValue("cantidad")

                    ElseIf sNumero = 2 Then
                        cbUnidad2.SelectedValue = tbAux.GetValue("tipo_unidad")
                        oProductosBodega.Cantidad2 = tbAux.GetValue("cantidad")
                        cbUnidad2.IsEnabled = True
                        txtCantidad2.IsEnabled = True
                    ElseIf sNumero = 3 Then
                        cbUnidad3.SelectedValue = tbAux.GetValue("tipo_unidad")
                        oProductosBodega.Cantidad3 = tbAux.GetValue("cantidad")
                        cbUnidad3.IsEnabled = True
                        txtCantidad3.IsEnabled = True
                    End If
                    sNumero = sNumero + 1
                    If sProceso = cConstantes.VENTA Then
                        DeSHabilitaControles()
                    End If
                    tbAux.MoveNext()
                Loop
                tbProductoBOdega.Disconnect()
            End If
        Else
            cbUnidad2.IsEnabled = False
            cbUnidad3.IsEnabled = False
        End If
        Me.DataContext = oProductosBodega

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(wDetalleBodega)
    End Sub

    Private Sub cmdCancelar_Click(sender As Object, e As RoutedEventArgs) Handles cmdCancelar.Click
        Me.Close()
    End Sub

    Private Sub cmAgregar_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregar.Click
        Dim result As MessageBoxResult
        Try
            CargaDatos()
            oProductosBodega.Valida = True
            Me.DataContext = oProductosBodega
            If oProductosBodega.Valida = False Then
                'MsgBox("Datos ingresados se encuentran erroneos, no es posible continuar")
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvDetalleBodega)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Agrega Producto Bodega"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvDetalleBodega)
                Exit Sub
            End If

            If sTipoProceso = cConstantes.AGREGAR Then
                oProductosBodega.NumeroIngreso = nNumeroIngreso
                result = Toolkit.MessageBox.Show("¿Confirme intención de ingresar producto a bodega?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
                If result = MessageBoxResult.OK Then
                    If oProductosBodega.IngresaProductosBodega() Then
                        Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                        Me.Close()
                    Else
                        Toolkit.MessageBox.Show(oProductosBodega.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    End If
                End If
            Else
                oProductosBodega.NumeroIngreso = nNumeroIngreso
                oProductosBodega.Codigo = sCodigoProducto
                result = Toolkit.MessageBox.Show("¿Confirme intención de actualizar producto a bodega?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
                If result = MessageBoxResult.OK Then
                    If oProductosBodega.ActualizaProductoBodega() Then
                        Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                        Me.Close()
                    Else
                        Toolkit.MessageBox.Show(oProductosBodega.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    End If

                End If
            End If


        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible ingresar producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub
    Private Sub CargaDatos()
        oProductosBodega.Valor = txtValorTotal.Text
        oProductosBodega.TipoProducto = cbDetalle.SelectedValue
        oProductosBodega.Cantidad1 = txtCantidad1.Text
        oProductosBodega.TipoUnidad1 = cbUnidad1.SelectedValue
        oProductosBodega.Cantidad2 = txtCantidad2.Text
        oProductosBodega.TipoUnidad2 = cbUnidad2.SelectedValue
        oProductosBodega.Cantidad3 = txtCantidad3.Text
        oProductosBodega.TipoUnidad3 = cbUnidad3.SelectedValue
        oProductosBodega.ValorUnitario = txtValorUnitario.Text
        oProductosBodega.ValorUnCompra = txtValorUNCompra.Text
        oProductosBodega.TotalProductos = txtTotalProductos.Text


    End Sub
    Private Sub txtValorTotal_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtValorTotal.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtCantidad1_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCantidad1.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtCantidad2_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCantidad2.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
    Private Sub txtCantidad3_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCantidad3.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtValorUnitario_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtValorUnitario.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub cbUnidad1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbUnidad1.SelectionChanged
        Dim tbAux As New DT
        Dim sGrupo As String = ""

        If sTipoProceso = cConstantes.MODIFICAR Then Exit Sub

        cbUnidad2.DataContext = Nothing
        cbUnidad3.DataContext = Nothing
        cbUnidad2.IsEnabled = True
        cbUnidad3.IsEnabled = True

        If sProceso = cConstantes.VENTA Then
            DeSHabilitaControles()
        Else
            oTiposUnidad.TipoUnidad1 = cbUnidad1.SelectedValue
            tbAux = oTiposUnidad.TiposUnidad
            If tbAux.EOF = False Then
                sGrupo = tbAux.GetValue("grupo")
            End If
            tbAux.Disconnect()
            oTiposUnidad.GrupoTipoUnidad = sGrupo
            tbAux = oTiposUnidad.ObtieneGrupoTiposUnidad
            If tbAux.EOF = False Then
                cbUnidad2.DataContext = tbAux.DT
                cbUnidad3.DataContext = tbAux.DT
            End If

        End If


    End Sub
    Private Sub cbUnidad2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbUnidad2.SelectionChanged
        If (cbUnidad1.SelectedValue = cbUnidad2.SelectedValue Or cbUnidad3.SelectedValue = cbUnidad2.SelectedValue) And cbUnidad2.SelectedValue <> Nothing Then
            MsgBox("No es posible ingresar dos veces el tipo de unidad")
            cbUnidad2.SelectedValue = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub cbUnidad3_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbUnidad3.SelectionChanged
        If (cbUnidad1.SelectedValue = cbUnidad3.SelectedValue Or cbUnidad3.SelectedValue = cbUnidad2.SelectedValue) And cbUnidad3.SelectedValue <> Nothing Then
            MsgBox("No es posible ingresar dos veces el tipo de unidad")
            cbUnidad3.SelectedValue = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub txtValorTotall_TextChanged(sender As Object, e As RoutedEventArgs) Handles txtValorTotal.TextChanged
        If oProductosBodega.Valor = 0 Or oProductosBodega.TotalProductos = 0 Or oProductosBodega.Cantidad1 = 0 Then Exit Sub
        ' oProductos.Valor = txtValorTotal.Text
        txtValorUNCompra.Text = oProductosBodega.CalculaValorUnitarioCompra()
        ' oProductos.Cantidad1 = txtCantidad1.Text
        txtValorUnitario.Text = oProductosBodega.CalculaValorUnitario
    End Sub

    Private Sub txtTotalProductos_TextChanged(sender As Object, e As RoutedEventArgs) Handles txtTotalProductos.TextChanged
        If oProductosBodega.Valor = 0 Or oProductosBodega.TotalProductos = 0 Then Exit Sub
        oProductosBodega.Valor = txtValorTotal.Text
        oProductosBodega.TotalProductos = txtTotalProductos.Text
        txtValorUNCompra.Text = oProductosBodega.CalculaValorUnitarioCompra()
    End Sub


    Private Sub cbDetalle_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDetalle.SelectionChanged
        oProductosBodega.Codigo = cbDetalle.SelectedValue
        sProceso = oProductosBodega.ProcesoProductos(cbDetalle.SelectedValue)
        ' HabilitaControles()
        If sProceso = cConstantes.VENTA Then
            DeSHabilitaControles()
        End If
    End Sub
    'Private Sub HabilitaControles()
    '    cbUnidad1.SelectedValue = ""
    '    cbUnidad1.IsEnabled = True
    '    cbUnidad2.IsEnabled = True
    '    cbUnidad3.IsEnabled = True
    '    txtCantidad2.IsEnabled = True
    '    txtCantidad3.IsEnabled = True
    'End Sub
    Private Sub DeSHabilitaControles()
        cbUnidad1.SelectedValue = cConstantes.UNIDAD
        cbUnidad1.IsEnabled = False
        cbUnidad2.IsEnabled = False
        cbUnidad3.IsEnabled = False
        txtCantidad2.IsEnabled = False
        txtCantidad3.IsEnabled = False
    End Sub

    Private Sub txtCantidad1_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtCantidad1.TextChanged
        If oProductosBodega.Cantidad1 = 0 Then Exit Sub
        oProductosBodega.Cantidad1 = txtCantidad1.Text
        txtValorUnitario.Text = oProductosBodega.CalculaValorUnitario
    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        If sTipoProceso <> cConstantes.MODIFICAR Then
            cbUnidad1.SelectedValue = ""
            cbUnidad2.SelectedValue = ""
            cbUnidad3.SelectedValue = ""
        End If
    End Sub
End Class
