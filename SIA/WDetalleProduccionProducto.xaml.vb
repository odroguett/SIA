Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf
Public Class WDetalleProduccionProducto
    Private oProduccionProducto As New cProduccionProductos
    Private oProductosBodega As New cProductosBodega
    Private oTiposUnidad As New cTiposUnidad
    Private oGruposProducto As New cGruposProducto
    Private sCodigoProducto As String
    Private dFechaIngreso As Date
    Private oValidador As New uValidador
    Private iNumeroProducto As Integer
    Public WriteOnly Property CodigoProducto
        Set(value)
            sCodigoProducto = value
        End Set
    End Property
    Public WriteOnly Property FechaIngreso
        Set(value)
            dFechaIngreso = value
        End Set
    End Property
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Private Sub WDetalleProduccionProducto_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbProducto As New DT
        Dim tbTipoUnidad As New DT
        grdProductos.IsReadOnly = True
        cmdModificar.IsEnabled = False
        cmdEliminar.IsEnabled = False
        oProduccionProducto.Codigo = sCodigoProducto
        oProduccionProducto.FechaIngreso = dFechaIngreso
        RefreshGrid()
        tbProducto = oProductosBodega.CargaProductosBodega()
        If tbProducto.EOF = False Then
            cbProducto.DataContext = tbProducto.DT
        End If
        tbProducto.Disconnect()
        tbTipoUnidad = oTiposUnidad.CargaTiposUnidad
        If tbTipoUnidad.EOF = False Then
            cbUnidad.DataContext = tbTipoUnidad.DT
        End If
        Me.DataContext = oProduccionProducto
    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(wDetalleProduccionProducto)
    End Sub

    Private Sub cmdCancelar_Click(sender As Object, e As RoutedEventArgs) Handles cmdCancelar.Click
        Me.Close()
    End Sub

    Private Sub cmAgregar_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregar.Click
        Dim result As MessageBoxResult

        Try
            oErrores.InicializaErrores()
            CargaDatos()
            oProduccionProducto.ObtieneNumeroProduccionProducto()
            oProduccionProducto.Valida = True
            Me.DataContext = oProduccionProducto
            If oProduccionProducto.Valida = False Then
                'MsgBox("Datos ingresados se encuentran erroneos, no es posible continuar")
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvProduccionProducto)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Agrega Productos"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvProduccionProducto)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("Nota: Recuerde que detalle solo puede ser modificado antes de ingresar producción de producto¿Confirme intención de ingresar detalle?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProduccionProducto.IngresaProduccionProductos Then
                    Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                End If
            Else
                Toolkit.MessageBox.Show(oProduccionProducto.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            End If

        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible ingresar detalle.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub
    Private Sub CargaDatos()
        Me.DataContext = Nothing
        oProduccionProducto.CodigoProductoBodega = cbProducto.SelectedValue
        oProduccionProducto.TipoUnidad1 = cbUnidad.SelectedValue
        oProduccionProducto.NumeroProducto = iNumeroProducto
        Me.DataContext = oProduccionProducto
    End Sub
    Private Sub RefreshGrid()
        Dim tbProductos As New DT

        tbProductos = oProduccionProducto.DetalleProduccionProductos()

        If tbProductos.EOF = False Then
            grdProductos.ItemsSource = tbProductos.DT.DefaultView
        Else
            grdProductos.ItemsSource = Nothing
        End If
    End Sub

    Private Sub cmdEliminar_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminar.Click
        Dim result As MessageBoxResult
        Try
            CargaDatos()
            oProduccionProducto.NumeroProducto = iNumeroProducto
            result = Toolkit.MessageBox.Show("¿Confirme intención de eliminar detalle?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProduccionProducto.EliminaProducionProducto() Then
                    Toolkit.MessageBox.Show("Eliminación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                Else
                    Toolkit.MessageBox.Show(oProduccionProducto.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            RefreshGrid()
            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible eliminar detalle.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub
    Private Sub LimpiaControles()
        txtCantidad.Text = ""
        cbUnidad.SelectedValue = ""
        cbProducto.SelectedValue = ""
        cbProducto.IsEnabled = True
        cbUnidad.IsEnabled = True
        cmAgregar.IsEnabled = True
        cmdEliminar.IsEnabled = False
        cmdModificar.IsEnabled = False

    End Sub

    Private Sub grdProductos_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdProductos.SelectionChanged
        Try
            Me.DataContext = Nothing
            If grdProductos.SelectedIndex = -1 Then Exit Sub
            cbProducto.IsEnabled = False
            cbUnidad.IsEnabled = False
            cmAgregar.IsEnabled = False
            cmdModificar.IsEnabled = True
            cmdEliminar.IsEnabled = True
            iNumeroProducto = grdProductos.SelectedItem(2).ToString
            cbProducto.SelectedValue = grdProductos.SelectedItem(1).ToString
            cbUnidad.SelectedValue = grdProductos.SelectedItem(4).ToString
            oProduccionProducto.Cantidad1 = grdProductos.SelectedItem(5).ToString
            Me.DataContext = oProduccionProducto
        Catch ex As Exception

        End Try

    End Sub
    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        LimpiaControles()
    End Sub

    Private Sub cmdModificar_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificar.Click
        Dim result As MessageBoxResult
        Try
            CargaDatos()

            oErrores.InicializaErrores()
            oProduccionProducto.Valida = True
            Me.DataContext = oProduccionProducto
            If oProduccionProducto.Valida = False Then
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvProduccionProducto)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Modifica detalle"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvProduccionProducto)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("¿Confirme intención de actualizar detalle?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oProduccionProducto.ActualizaProduccionProducto Then
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oProduccionProducto.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible actualizar detalle.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub
End Class
