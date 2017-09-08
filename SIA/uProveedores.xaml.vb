Imports System.Windows.Media.Animation
Imports Xceed.Wpf
Imports cConexion
Public Class uProveedores
    Private oProveedores As New cProveedores
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
    Private Sub RefreshGridProv()
        Dim tbPoveedores As New DT
        tbPoveedores = oProveedores.CargaProveedores()
        If tbPoveedores.EOF = False Then
            grdProveedores.ItemsSource = tbPoveedores.DT.DefaultView
            ' grdProveedores.SelectedValuePath = "CODIGO_PROVEEDOR"
        End If
    End Sub

    Private Sub uProveedores_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbComunas As New DT
        Dim tbCiudades As New DT
        Dim tbRegiones As New DT
        tbComunas = oComunas.CargaComunas()
        cmdModificarProv.IsEnabled = False
        cmdEliminarProv.IsEnabled = False
        grdProveedores.IsReadOnly = True
        If tbComunas.EOF = False Then
            cbComunaProv.DataContext = tbComunas.DT
        End If
        tbComunas.Disconnect()
        tbCiudades = oCiudades.CargaCiudades
        If tbCiudades.EOF = False Then
            cbCiudadProv.DataContext = tbCiudades.DT
        End If
        tbCiudades.Disconnect()
        tbRegiones = oRegiones.CargaRegiones
        If tbRegiones.EOF = False Then
            cbRegionProv.DataContext = tbRegiones.DT
        End If
        tbRegiones.Disconnect()
        RefreshGridProv()

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(cvProveedores)
    End Sub

    Private Sub cmAgregarProv_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregarProv.Click
        Dim result As MessageBoxResult
        Dim oValidador As New uValidador
        Try
            oErrores.InicializaErrores()
            CargaDatos()
            oProveedores.Valida = True
            Me.DataContext = oProveedores
            If oProveedores.Valida = False Then
                'MsgBox("Datos ingresados se encuentran erroneos, no es posible continuar")
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvProveedores)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Agrega Proveedor"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvProveedores)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de ingresar nuevo proveedor?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProveedores.IngresaProveedores Then
                    Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGridProv()
                Else
                    Toolkit.MessageBox.Show(oProveedores.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible ingresar proveedor.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub
    Private Sub CargaDatos()
        oProveedores.Codigo = txtCodProv.Text
        oProveedores.Descripcion = txtDesProv.Text
        oProveedores.RutProveedor = txtRutProv.Text
        oProveedores.Direccion = txtDireccionProv.Text
        oProveedores.Comuna = cbComunaProv.SelectedValue
        oProveedores.Ciudad = cbCiudadProv.SelectedValue
        oProveedores.Region = cbRegionProv.SelectedValue
        oProveedores.Telefono = txtTelefonoProv.Text
        oProveedores.Mail = txtMail.Text
        oProveedores.Contacto = txtContacoProv.Text
    End Sub

    Private Sub cmdEliminarProv_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminarProv.Click
        Dim result As MessageBoxResult
        Try
            oProveedores.Codigo = txtCodProv.Text
            result = Toolkit.MessageBox.Show("Advertencia!!, proveedor puede estar asociado a producto en bodega.¿Confirma intención de eliminar proveedor?.", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProveedores.EliminaProvedores() Then
                    Toolkit.MessageBox.Show("Eliminación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGridProv()

                Else
                    Toolkit.MessageBox.Show(oProveedores.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            LimpiaControles()

        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible eliminar proveedor.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub

    Private Sub grdProveedores_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdProveedores.SelectionChanged
        Try
            If grdProveedores.SelectedIndex = -1 Then Exit Sub
            txtCodProv.Text = grdProveedores.SelectedItem(0).ToString
            txtDesProv.Text = grdProveedores.SelectedItem(1).ToString
            txtRutProv.Text = grdProveedores.SelectedItem(2).ToString
            txtDireccionProv.Text = grdProveedores.SelectedItem(3).ToString
            cbComunaProv.SelectedValue = grdProveedores.SelectedItem(4).ToString
            cbCiudadProv.SelectedValue = grdProveedores.SelectedItem(5).ToString
            cbRegionProv.SelectedValue = grdProveedores.SelectedItem(6).ToString
            txtTelefonoProv.Text = grdProveedores.SelectedItem(7).ToString
            txtMail.Text = grdProveedores.SelectedItem(8).ToString
            txtContacoProv.Text = grdProveedores.SelectedItem(9).ToString
            cmAgregarProv.IsEnabled = False
            txtCodProv.IsEnabled = False
            cmdModificarProv.IsEnabled = True
            cmdEliminarProv.IsEnabled = True
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cmdModificarProv_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificarProv.Click
        Dim result As MessageBoxResult
        Dim oValidador As New uValidador
        Try
            oErrores.InicializaErrores()
            CargaDatos()
            oProveedores.Valida = True
            Me.DataContext = oProveedores
            If oProveedores.Valida = False Then
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvProveedores)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Modifica Cliente"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvProveedores)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de actualizar datos del proveedor?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProveedores.ActualizaProveedores Then
                    Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGridProv()
                Else
                    Toolkit.MessageBox.Show(oProveedores.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es actualizar datos del  proveedor.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        LimpiaControles()
    End Sub
    Private Sub LimpiaControles()
        txtCodProv.Text = ""
        txtDesProv.Text = ""
        txtRutProv.Text = ""
        txtDireccionProv.Text = ""
        txtTelefonoProv.Text = ""
        txtMail.Text = ""
        txtContacoProv.Text = ""
        txtCodProv.IsEnabled = True
        cmdModificarProv.IsEnabled = False
        cmdEliminarProv.IsEnabled = False
        cmAgregarProv.IsEnabled = True
    End Sub

    Private Sub txtTelefonoProv_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtTelefonoProv.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub txtFonoProv_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtFonoProv.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub
End Class
