Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf
Public Class UClientes
    Private oClientes As New cClientes
    Private oCiudades As New cCiudades
    Private oComunas As New cComunas
    Private oRegiones As New cRegiones
    Private oUtiles As New cUtiles

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(cvClientes)
    End Sub

    Private Sub UClientes_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbComunas As New DT
        Dim tbCiudades As New DT
        Dim tbRegiones As New DT
        tbComunas = oComunas.CargaComunas()
        cmdModificar.IsEnabled = False
        cmdEliminar.IsEnabled = False
        grdClientes.IsReadOnly = True
        If tbComunas.EOF = False Then
            cbComuna.DataContext = tbComunas.DT
        End If
        tbComunas.Disconnect()
        tbCiudades = oCiudades.CargaCiudades
        If tbCiudades.EOF = False Then
            cbCiudad.DataContext = tbCiudades.DT
        End If
        tbCiudades.Disconnect()
        tbRegiones = oRegiones.CargaRegiones
        If tbRegiones.EOF = False Then
            cbRegion.DataContext = tbRegiones.DT
        End If
        tbRegiones.Disconnect()
        RefreshGrid()
    End Sub
    Private Sub RefreshGrid()
        Dim tbClientes As New DT
        tbClientes = oClientes.CargaClientes()
        If tbClientes.EOF = False Then
            grdClientes.ItemsSource = tbClientes.DT.DefaultView
            ' grdProveedores.SelectedValuePath = "CODIGO_PROVEEDOR"
        End If
    End Sub
    Private Sub LimpiaControles()
        txtCod.Text = ""
        txtDes.Text = ""
        txtRut.Text = ""
        txtDireccion.Text = ""
        txtTelefono.Text = ""
        txtMail.Text = ""
        txtCelular.Text = ""
        cmdModificar.IsEnabled = False
        cmdEliminar.IsEnabled = False
        txtCod.IsEnabled = True
        cmAgregar.IsEnabled = True
    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        LimpiaControles()
    End Sub
    Private Sub CargaDatos()
        oClientes.Codigo = txtCod.Text
        oClientes.Descripcion = txtDes.Text
        oClientes.RutCliente = txtRut.Text
        oClientes.Direccion = txtDireccion.Text
        oClientes.Comuna = cbComuna.SelectedValue
        oClientes.Ciudad = cbCiudad.SelectedValue
        oClientes.Region = cbRegion.SelectedValue
        oClientes.Telefono = txtTelefono.Text
        oClientes.Mail = txtMail.Text
        oClientes.Celular = txtCelular.Text
    End Sub


    Private Sub cmAgregar_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregar.Click
        Dim result As MessageBoxResult
        Dim oValidador As New uValidador
        Try
            oErrores.InicializaErrores()
            CargaDatos()
            oClientes.Valida = True
            Me.DataContext = oClientes
            If oClientes.Valida = False Then
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvClientes)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Agrega Cliente"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvClientes)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de ingresar nuevo cliente?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oClientes.IngresaClientes Then
                    Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oClientes.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            LimpiaControles()

        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible ingresar cliente.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub

    Private Sub cmdModificar_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificar.Click
        Dim result As MessageBoxResult
        Dim oValidador As New uValidador
        Try
            oErrores.InicializaErrores()
            CargaDatos()
            oClientes.Valida = True
            Me.DataContext = oClientes
            If oClientes.Valida = False Then
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvClientes)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Modifica Cliente"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvClientes)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de actualizar datos?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oClientes.ActualizaClientes Then
                    Toolkit.MessageBox.Show("Actualización realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oClientes.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible actualizar cliente.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            
        End Try
    End Sub

    Private Sub cmdEliminar_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminar.Click
        Dim result As MessageBoxResult
        Try
            oClientes.Codigo = txtCod.Text
            result = Toolkit.MessageBox.Show("¿Confirme intención de eliminar cliente?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oClientes.EliminaClientes() Then
                    Toolkit.MessageBox.Show("Eliminación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oClientes.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible eliminar cliente.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub

    Private Sub grdClientes_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdClientes.SelectionChanged
        Try
            If grdClientes.SelectedIndex = -1 Then Exit Sub
            txtCod.Text = grdClientes.SelectedItem(0).ToString
            txtRut.Text = grdClientes.SelectedItem(1).ToString
            txtDes.Text = grdClientes.SelectedItem(2).ToString
            txtDireccion.Text = grdClientes.SelectedItem(3).ToString
            cbComuna.SelectedValue = grdClientes.SelectedItem(4).ToString
            cbCiudad.SelectedValue = grdClientes.SelectedItem(5).ToString
            cbRegion.SelectedValue = grdClientes.SelectedItem(6).ToString
            txtTelefono.Text = grdClientes.SelectedItem(7).ToString
            txtCelular.Text = grdClientes.SelectedItem(8).ToString
            txtMail.Text = grdClientes.SelectedItem(9).ToString
            cmAgregar.IsEnabled = False
            cmdModificar.IsEnabled = True
            cmdEliminar.IsEnabled = True
            txtCod.IsEnabled = False
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtCelular_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCelular.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtTelefono_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtTelefono.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
End Class
