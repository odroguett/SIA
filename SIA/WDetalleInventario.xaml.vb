Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf
Public Class WDetalleInventario
    Private sCodigoProducto As String
    Private sTipoUnidad As String
    Private nCantidad As Double
    Private sDescripcionProducto As String
    Private sDescripcionUnidad As String
    Private oInventario As New cInventario
    Private oValidador As New uValidador
    Public WriteOnly Property CodigoProducto
        Set(value)
            sCodigoProducto = value
        End Set
    End Property

    Public WriteOnly Property TipoUnidad
        Set(value)
            sTipoUnidad = value
        End Set
    End Property
    Public WriteOnly Property Cantidad
        Set(value)
            nCantidad = value
        End Set
    End Property
    Public WriteOnly Property DescripcionProducto
        Set(value)
            sDescripcionProducto = value
        End Set
    End Property
    Public WriteOnly Property DescripcionUnidad
        Set(value)
            sDescripcionUnidad = value
        End Set
    End Property
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(WDetalleInventario1)
    End Sub
    Private Sub WDetalleInventario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        txtCantidad.IsEnabled = False
        txtProducto.IsEnabled = False
        txtTipoUnidad.IsEnabled = False
        cmAgregar.IsEnabled = True
        cmdEliminar.IsEnabled = False
        cmdLimpiar.IsEnabled = True
        rbAbono.IsChecked = True
        grdDetalleInventario.IsReadOnly = True
        If rbAbono.IsChecked = True Then
            tbModificacion.Text = "Abono"
        Else
            tbModificacion.Text = "Merma"
        End If
        RefreshGrid()
        DPIngreso.Text = Today
        oInventario.Codigo = sCodigoProducto
        oInventario.DescripcionProducto = sDescripcionProducto
        oInventario.TipoUnidad1 = sTipoUnidad
        oInventario.DescripcionUnidad = sDescripcionUnidad
        oInventario.Cantidad1 = nCantidad
        oInventario.FechaIngreso = Today
        Me.DataContext = oInventario
    End Sub
    Private Sub cmdCancelar_Click(sender As Object, e As RoutedEventArgs) Handles cmdCancelar.Click
        Me.Close()
    End Sub


    Private Sub cmAgregar_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregar.Click
        Dim result As MessageBoxResult
        Try
            oErrores.InicializaErrores()
            CargaDatos()
            oInventario.Valida = True
            Me.DataContext = oInventario
            If oInventario.Valida = False Then
                'MsgBox("Datos ingresados se encuentran erroneos, no es posible continuar")
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvDetalleInventario)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Agrega Producto Bodega"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvDetalleInventario)
                Exit Sub
            End If
            If rbAbono.IsChecked = True Then
                result = Toolkit.MessageBox.Show("¿Esta seguro que desea ingresar abono por " + txtAbono.Text + "?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
                If result = MessageBoxResult.OK Then
                    If oInventario.IngresaDetalleInventario() Then
                        Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                        RefreshGrid()
                        ActualizaInventario()
                    Else
                        Toolkit.MessageBox.Show(oInventario.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    End If
                End If
            Else
                result = Toolkit.MessageBox.Show("¿Esta seguro que desea ingresar merma por " + txtAbono.Text + "?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
                If result = MessageBoxResult.OK Then
                    If oInventario.IngresaDetalleInventario() Then
                        Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                        RefreshGrid()
                        ActualizaInventario()
                    Else
                        Toolkit.MessageBox.Show(oInventario.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    End If
                End If
            End If
            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible ingresar producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub

    Private Sub CargaDatos()
        Me.DataContext = Nothing
        oInventario.FechaIngreso = dpIngreso.Text
        If rbMerma.IsChecked = True Then
            oInventario.TipoMovimiento = cConstantes.MERMA
        Else
            oInventario.TipoMovimiento = cConstantes.ABONO
        End If
        oInventario.Observacion = txtObservacion.Text
    End Sub

    Private Sub txtCantidad_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCantidad.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub grdDetalleInventario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdDetalleInventario.SelectionChanged
        Try
            LimpiaControles()
            cmAgregar.IsEnabled = False
            cmdEliminar.IsEnabled = True
            Me.DataContext = Nothing
            If grdDetalleInventario.SelectedIndex = -1 Then Exit Sub
            dpIngreso.Text = grdDetalleInventario.SelectedItem(0).ToString
            oInventario.FechaIngreso = grdDetalleInventario.SelectedItem(0).ToString
            oInventario.TipoMovimiento = grdDetalleInventario.SelectedItem(3).ToString
            If grdDetalleInventario.SelectedItem(3).ToString = cConstantes.ABONO Then
                rbMerma.IsChecked = False
                rbAbono.IsChecked = True
            Else
                rbMerma.IsChecked = True
                rbAbono.IsChecked = False
            End If
            oInventario.Observacion = grdDetalleInventario.SelectedItem(4).ToString
            oInventario.Movimiento = grdDetalleInventario.SelectedItem(5).ToString
            Me.DataContext = oInventario
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rbAbono_Click(sender As Object, e As RoutedEventArgs) Handles rbAbono.Click
        If rbAbono.IsChecked = True Then
            oInventario.TipoMovimiento = cConstantes.ABONO
            tbModificacion.Text = "Abono"
        Else
            oInventario.TipoMovimiento = cConstantes.MERMA
            tbModificacion.Text = "Merma"
        End If
    End Sub

    Private Sub rbMerma_Click(sender As Object, e As RoutedEventArgs) Handles rbMerma.Click
        If rbAbono.IsChecked = True Then
            oInventario.TipoMovimiento = cConstantes.ABONO
            tbModificacion.Text = "Abono"
        Else
            oInventario.TipoMovimiento = cConstantes.MERMA
            tbModificacion.Text = "Merma"
        End If
    End Sub

    Private Sub RefreshGrid()
        Dim tbInventario As New DT
        tbInventario = oInventario.CargaDetalleInventario

        If tbInventario.EOF = False Then
            grdDetalleInventario.ItemsSource = tbInventario.DT.DefaultView
        Else
            grdDetalleInventario.ItemsSource = Nothing
        End If
    End Sub
    Private Sub LimpiaControles()
        cmAgregar.IsEnabled = True
        cmdEliminar.IsEnabled = False
        rbAbono.IsChecked = True
        rbMerma.IsChecked = False
        Me.DataContext = Nothing
        dpIngreso.Text = Today
        oInventario.FechaIngreso = Today
        oInventario.Movimiento = 0
        oInventario.Observacion = ""
        tbModificacion.Text = "Abono"
        Me.DataContext = oInventario

    End Sub

    Private Sub cmdEliminar_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminar.Click
        Dim result As MessageBoxResult

        Try
            CargaDatos()
            result = Toolkit.MessageBox.Show("Advertencia!!, se va a eliminar detalle.¿Confirma intención de continuar?.", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oInventario.EliminaDetalleInventario() Then
                    Toolkit.MessageBox.Show("Eliminación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                    ActualizaInventario()
                Else
                    Toolkit.MessageBox.Show(oInventario.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            LimpiaControles()

        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible eliminar proveedor.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub
    Private Sub ActualizaInventario()
        Dim tbInventario As New DT
        tbInventario = oInventario.CargaInventario
        If tbInventario.EOF = False Then
            oInventario.Cantidad1 = tbInventario.GetValue("cantidad")
        End If
        Me.DataContext = oInventario
    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        LimpiaControles()
    End Sub
End Class
