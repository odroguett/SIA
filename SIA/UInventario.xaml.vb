Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf
Public Class UInventario
    Private oInventario As New cInventario
    Private oValidador As New uValidador
    Private sCodigoProducto As String

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(uInventario)
    End Sub

    Private Sub UBodegas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        grdInventario.IsReadOnly = True
        txtProducto.IsEnabled = False
        txtTipoUnidad.IsEnabled = False
        txtCantidad.IsEnabled = False
        cmdMovimiento.IsEnabled = False
        txtProducto.Text = ""
        txtTipoUnidad.Text = ""
        txtCantidad.Text = ""
        RefreshGrid()
    End Sub
    Private Sub RefreshGrid()
        Dim tbInventario As New DT
        tbInventario = oInventario.CargaInventario
        If tbInventario.EOF = False Then
            grdInventario.ItemsSource = tbInventario.DT.DefaultView
        Else
            grdInventario.ItemsSource = Nothing
        End If
    End Sub
    Private Sub grdInventario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdInventario.SelectionChanged
        Try
            cmdMovimiento.IsEnabled = True
            If grdInventario.SelectedIndex = -1 Then Exit Sub
            oInventario.Codigo = grdInventario.SelectedItem(0).ToString
            oInventario.DescripcionProducto = grdInventario.SelectedItem(1).ToString
            oInventario.TipoUnidad1 = grdInventario.SelectedItem(2).ToString
            oInventario.DescripcionUnidad = grdInventario.SelectedItem(3).ToString
            oInventario.Cantidad1 = grdInventario.SelectedItem(4).ToString
            Me.DataContext = oInventario
        Catch ex As Exception
        End Try
    End Sub

    Public Sub Opacar()
        Dim sb As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
        sb.Begin(uInventario)
    End Sub

    Private Sub cmdMovimiento_Click(sender As Object, e As RoutedEventArgs) Handles cmdMovimiento.Click
        Dim oWDetalleInventario As New WDetalleInventario
        Try
            oWDetalleInventario.CodigoProducto = oInventario.Codigo
            oWDetalleInventario.DescripcionProducto = oInventario.DescripcionProducto
            oWDetalleInventario.TipoUnidad = oInventario.TipoUnidad1
            oWDetalleInventario.DescripcionUnidad = oInventario.DescripcionUnidad
            oWDetalleInventario.Cantidad = oInventario.Cantidad1
            Opacar()
            oWDetalleInventario.ShowDialog()
            Aclarar()
            RefreshGrid()
            cmdMovimiento.IsEnabled = False
        Catch ex As Exception
        End Try
    End Sub
End Class
