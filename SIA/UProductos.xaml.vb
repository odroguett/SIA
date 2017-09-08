Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf
Public Class UProductos
    Private oProductos As New cProductos
    Private oTipoProductos As New cTiposProducto
    Private oTiposUnidad As New cTiposUnidad
    Private oGruposProducto As New cGruposProducto
    Private oUtiles As New cUtiles
    Private bGrilla As Boolean = False
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(CVProductos)
    End Sub

    Private Sub UProductos_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbTipoProducto As New DT

        Dim tbGrupo As New DT
        grdProductos.IsReadOnly = True
        tbTipoProducto = oTipoProductos.CargaTipoProductos()
        If tbTipoProducto.EOF = False Then
            cbDetalle.DataContext = tbTipoProducto.DT
        End If
        tbTipoProducto.Disconnect()
        CargaTiposUnidad()
        tbGrupo = oGruposProducto.CargaGrupos
        If tbGrupo.EOF = False Then
            cbGrupo.DataContext = tbGrupo.DT
        End If
        tbGrupo.Disconnect()
        RefreshGrid()
        LimpiaControles()
        txtCod.Focus()
    End Sub
    Private Sub CargaTiposUnidad()
        Dim tbTipoUnidad As New DT
        tbTipoUnidad = oTiposUnidad.CargaTiposUnidad
        If tbTipoUnidad.EOF = False Then
            cbUnidad1.DataContext = tbTipoUnidad.DT
            cbUnidad2.DataContext = tbTipoUnidad.DT
            cbUnidad3.DataContext = tbTipoUnidad.DT
        End If
        tbTipoUnidad.Disconnect()
    End Sub

    Private Sub cmAgregar_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregar.Click
        Dim result As MessageBoxResult
        Try
            Dim oValidador As New uValidador

            oErrores.InicializaErrores()
            CargaDatos()
            oProductos.Valida = True
            Me.DataContext = oProductos
            If oProductos.Valida = False Or ValidaOtros() = False Then
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.CVProductos)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Agrega Productos"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.CVProductos)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de ingresar nuevo producto?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProductos.IngresaProductos Then
                    Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oProductos.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible ingresar producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Error)
        End Try
    End Sub

    Private Sub txtCantidad1_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtCantidad1.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub CargaDatos()
        ' Me.DataContext = Nothing
        oProductos.Codigo = txtCod.Text
        oProductos.Descripcion = txtDesc.Text
        oProductos.GrupoProducto = cbGrupo.SelectedValue
        oProductos.TipoProducto = cbDetalle.SelectedValue
        oProductos.TipoUnidad1 = cbUnidad1.SelectedValue
        oProductos.Cantidad1 = txtCantidad1.Text
        oProductos.TipoUnidad2 = cbUnidad2.SelectedValue
        oProductos.Cantidad2 = txtCantidad2.Text
        oProductos.TipoUnidad3 = cbUnidad3.SelectedValue
        oProductos.Cantidad3 = txtCantidad3.Text

        If rbBodega.IsChecked = True Then
            oProductos.TipoProceso = "B"
        ElseIf rbProduccion.IsChecked = True Then
            oProductos.TipoProceso = "P"
        Else
            oProductos.TipoProceso = "V"
        End If
        If chkGranel.IsChecked = True Then
            oProductos.Granel = 1
        Else
            oProductos.Granel = 0
        End If

    End Sub
    Private Sub RefreshGrid()
        Dim tbProductos As New DT
        tbProductos = oProductos.CargaProductos()

        If tbProductos.EOF = False Then
            grdProductos.ItemsSource = tbProductos.DT.DefaultView
        Else
            grdProductos.ItemsSource = Nothing
        End If
    End Sub

    Private Sub grdProductos_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdProductos.SelectionChanged
        Dim tbAux As New DT
        Dim sNumero As String = 1
        Try
            If grdProductos.SelectedIndex = -1 Then Exit Sub
            LimpiaControles()
            bGrilla = True
            txtCod.Text = grdProductos.SelectedItem(0).ToString
            txtDesc.Text = grdProductos.SelectedItem(1).ToString
            cbGrupo.SelectedValue = grdProductos.SelectedItem(2).ToString
            cbDetalle.SelectedValue = grdProductos.SelectedItem(3).ToString
            If grdProductos.SelectedItem(5).ToString <> "" Then
                chkGranel.IsChecked = grdProductos.SelectedItem(5).ToString
            End If
            cmAgregar.IsEnabled = False
            cmdModificar.IsEnabled = True
            cmdEliminar.IsEnabled = True
            txtCod.IsEnabled = False
            rbBodega.IsChecked = False
            rbProduccion.IsChecked = False
            rbVenta.IsChecked = False
            Select Case grdProductos.SelectedItem(4).ToString
                Case "B"
                    rbBodega.IsChecked = True
                Case "P"
                    rbProduccion.IsChecked = True
                Case Else
                    rbVenta.IsChecked = True
            End Select
            CargaTiposUnidad()
            oProductos.Codigo = txtCod.Text
            tbAux = oProductos.CargaTipoUnidadProducto
            Do While tbAux.EOF = False
                If sNumero = 1 Then
                    cbUnidad1.IsEnabled = False
                    txtCantidad1.IsEnabled = True
                    cbUnidad1.SelectedValue = tbAux.GetValue("tipo_unidad")
                    txtCantidad1.Text = tbAux.GetValue("cantidad")
                ElseIf sNumero = 2 Then
                    cbUnidad2.IsEnabled = False
                    txtCantidad2.IsEnabled = True
                    cbUnidad2.SelectedValue = tbAux.GetValue("tipo_unidad")
                    txtCantidad2.Text = tbAux.GetValue("cantidad")
                ElseIf sNumero = 3 Then
                    cbUnidad3.IsEnabled = False
                    txtCantidad3.IsEnabled = True
                    cbUnidad3.SelectedValue = tbAux.GetValue("tipo_unidad")
                    txtCantidad3.Text = tbAux.GetValue("cantidad")
                End If
                sNumero = sNumero + 1
                tbAux.MoveNext()
            Loop
        Catch ex As Exception

        End Try
    End Sub


    Private Sub cmdModificar_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificar.Click
        Dim result As MessageBoxResult
        Dim oValidador As New uValidador
        Try
            oErrores.InicializaErrores()
            CargaDatos()
            Me.DataContext = oProductos
            oProductos.Valida = True

            If oProductos.Valida = False Then
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.CVProductos)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Modifica Cliente"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.CVProductos)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de actualizar datos?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProductos.ActualizaProducto Then
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oProductos.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible actualizar Producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        LimpiaControles()
    End Sub
    Private Sub LimpiaControles()
        cmAgregar.IsEnabled = True
        cmdModificar.IsEnabled = False
        cmdEliminar.IsEnabled = False
        txtCod.IsEnabled = True
        txtCod.Text = ""
        txtDesc.Text = ""
        cbGrupo.SelectedValue = ""
        cbDetalle.SelectedValue = ""
        cbUnidad1.SelectedValue = ""
        txtCantidad1.Text = "0"
        cbUnidad2.SelectedValue = ""
        txtCantidad2.Text = "0"
        cbUnidad3.SelectedValue = ""
        txtCantidad3.Text = "0"
        rbBodega.IsChecked = True
        rbProduccion.IsChecked = False
        rbVenta.IsChecked = False
        cbUnidad2.IsEnabled = False
        cbUnidad3.IsEnabled = False
        txtCantidad2.IsEnabled = False
        txtCantidad3.IsEnabled = False
        cbUnidad1.IsEnabled = True
        chkGranel.IsChecked = False


    End Sub

    Private Sub cmdEliminar_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminar.Click
        Dim result As MessageBoxResult
        Try
            oErrores.InicializaErrores()
            oProductos.Codigo = txtCod.Text
            result = Toolkit.MessageBox.Show("Atención!!!, eliminar un producto puede producir inconsistencias en bodega y producción,¿Confirme intención de eliminar producto?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProductos.EliminaProducto() Then
                    Toolkit.MessageBox.Show("Eliminación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    LimpiaControles()
                Else
                    Toolkit.MessageBox.Show(oProductos.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

            RefreshGrid()

        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible eliminar producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub

    Private Sub cbDetalle_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDetalle.SelectionChanged
        oProductos.Codigo = cbDetalle.SelectedValue

    End Sub

    Private Sub cbUnidad1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbUnidad1.SelectionChanged
        Dim tbAux As New DT
        Dim sGrupo As String
        Dim iTipoUnidad As Integer
        If bGrilla = True Or chkGranel.IsChecked = True Then
            bGrilla = False
            Exit Sub
        End If
        cbUnidad2.IsEnabled = False
        cbUnidad3.IsEnabled = False
        cbUnidad2.DataContext = Nothing
        cbUnidad3.DataContext = Nothing
        oTiposUnidad.TipoUnidad1 = cbUnidad1.SelectedValue
        tbAux = oTiposUnidad.TiposUnidad
        If tbAux.EOF = False Then
            sGrupo = tbAux.GetValue("grupo")
        End If
        tbAux.Disconnect()
        oTiposUnidad.GrupoTipoUnidad = sGrupo

        tbAux = oTiposUnidad.ObtieneGrupoTiposUnidad
        iTipoUnidad = 1
        Do While tbAux.EOF = False
            If iTipoUnidad = 1 Then
                cbUnidad2.IsEnabled = True
                txtCantidad2.IsEnabled = True
                cbUnidad2.DataContext = tbAux.DT
            ElseIf iTipoUnidad = 2 Then
                cbUnidad3.IsEnabled = True
                txtCantidad3.IsEnabled = True
                cbUnidad3.DataContext = tbAux.DT
            End If
            iTipoUnidad = iTipoUnidad + 1
            tbAux.MoveNext()
        Loop
    End Sub

    Private Sub cbUnidad2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbUnidad2.SelectionChanged
        If (cbUnidad1.SelectedValue = cbUnidad2.SelectedValue Or cbUnidad3.SelectedValue = cbUnidad2.SelectedValue) And cbUnidad2.SelectedValue <> Nothing Then
            Toolkit.MessageBox.Show("No es posible ingresar dos veces el tipo de unidad", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            cbUnidad2.SelectedValue = Nothing
            Exit Sub
        End If
    End Sub

    Private Sub cbUnidad3_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbUnidad3.SelectionChanged
        If (cbUnidad1.SelectedValue = cbUnidad3.SelectedValue Or cbUnidad3.SelectedValue = cbUnidad2.SelectedValue) And cbUnidad3.SelectedValue <> Nothing Then
            Toolkit.MessageBox.Show("No es posible ingresar dos veces el tipo de unidad", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            cbUnidad3.SelectedValue = Nothing
            Exit Sub
        End If
    End Sub
    Private Function ValidaOtros() As Boolean
        oProductos.TipoProducto = cbDetalle.SelectedValue
        oProductos.TipoUnidad1 = cbUnidad1.SelectedValue
        oProductos.TipoUnidad2 = cbUnidad2.SelectedValue
        oProductos.TipoUnidad3 = cbUnidad3.SelectedValue
        If oProductos.ValidaCB() Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub chkGranel_Click(sender As Object, e As RoutedEventArgs) Handles chkGranel.Click
        If chkGranel.IsChecked = True Then
            Toolkit.MessageBox.Show("Atención,selección de tipo de granel para producto permite ingresar solo tipo de unidad.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            oProductos.Codigo = txtCod.Text
            oProductos.Descripcion = txtDesc.Text
            oProductos.Cantidad1 = 1
            oProductos.Cantidad2 = 0
            oProductos.Cantidad3 = 0
            rbVenta.IsChecked = True
            rbProduccion.IsChecked = False
            rbBodega.IsChecked = False
            rbBodega.IsEnabled = False
            rbProduccion.IsEnabled = False
            cbUnidad2.IsEnabled = False
            cbUnidad3.IsEnabled = False
            txtCantidad1.IsEnabled = False
            txtCantidad2.IsEnabled = False
            txtCantidad3.IsEnabled = False
            Me.DataContext = oProductos
        Else
            rbVenta.IsChecked = False
            rbProduccion.IsChecked = False
            rbBodega.IsChecked = True
            rbBodega.IsEnabled = True
            rbProduccion.IsEnabled = True
            txtCantidad1.IsEnabled = True
            txtCantidad2.IsEnabled = True
            txtCantidad3.IsEnabled = True
            cbUnidad2.IsEnabled = True
            cbUnidad3.IsEnabled = True
        End If
    End Sub
End Class
