Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf

Public Class UVentas
    Private oClientes As New cClientes
    Private oVentas As New cVentas
    Private bCargaInicial As Boolean = False
    Private WithEvents oWVenta As WVentas
    Private bGrilla As Boolean = False
    Public Event ActualizaVenta()
    Public Event ActualizaVentaDiaria()
    Private Sub UVentas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbClientes As New DT
        Dim tbVentas As New DT
        txtIDVenta.IsEnabled = False
        txtTotalProductos.IsEnabled = False
        txtTotalNeto.IsEnabled = False
        txtTotalImpuesto.IsEnabled = False
        txtTotalVenta.IsEnabled = False
        tbClientes = oClientes.CargaCBClientes
        If tbClientes.EOF = False Then
            cbCliente.DataContext = tbClientes.DT
        End If
        DPFechaInicio.Text = Today
        DPFechaTermino.Text = Today
        oVentas.FechaVenta = Today
        oVentas.FechaTerminoVenta = Today
        RefreshGrid()
        Me.DataContext = oVentas
        bCargaInicial = True
        cmdEliminar.IsEnabled = False
        cmdModificar.IsEnabled = False
    End Sub
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()

        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(uVentas)
    End Sub



    Private Sub DPFechaVenta_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles DPFechaInicio.SelectedDateChanged
        Dim tbVentas As New DT
        If bCargaInicial = False Then Exit Sub
        oVentas.FechaVenta = DPFechaInicio.Text
        oVentas.FechaTerminoVenta = DPFechaTermino.Text
        oVentas.CodigoCliente = cbCliente.SelectedValue
        If DPFechaInicio.Text > DPFechaTermino.Text Then
            DPFechaInicio.Text = Today
            DPFechaTermino.Text = Today
            MsgBox("Fecha de inicio no puede ser mayor que fecha de termino")
            Exit Sub
        End If
        grdVenta.ItemsSource = Nothing
        grdVenta.Items.Clear()

        If cbCliente.SelectedValue = "" Then
            tbVentas = oVentas.ObtieneVenta()
            If tbVentas.EOF = False Then
                grdVenta.ItemsSource = tbVentas.DT.DefaultView
            End If
        Else
            tbVentas = oVentas.ObtieneVentaCliente()
            If tbVentas.EOF = False Then
                grdVenta.ItemsSource = tbVentas.DT.DefaultView
            End If
        End If
    End Sub


    Private Sub cbCliente_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbCliente.SelectionChanged
        Dim tbVentas As New DT
        If bGrilla = True Then
            bGrilla = False
            Exit Sub
        End If
        grdVenta.ItemsSource = Nothing
        grdVenta.Items.Clear()
        oVentas.FechaVenta = DPFechaInicio.Text
        oVentas.FechaTerminoVenta = DPFechaTermino.Text
        oVentas.CodigoCliente = cbCliente.SelectedValue
        tbVentas = oVentas.ObtieneVentaCliente()
        If tbVentas.EOF = False Then
            grdVenta.ItemsSource = tbVentas.DT.DefaultView
        End If
        grdDetalle.ItemsSource = Nothing
        grdDetalle.Items.Clear()

    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        Limpiar
    End Sub
    Private Sub Limpiar()
        Dim tbVentas As New DT
        cbCliente.SelectedValue = ""
        DPFechaInicio.Text = Today
        DPFechaTermino.Text = Today
        grdVenta.ItemsSource = Nothing
        grdVenta.Items.Clear()
        tbVentas = oVentas.ObtieneVenta()
        If tbVentas.EOF = False Then
            grdVenta.ItemsSource = tbVentas.DT.DefaultView
        End If
        grdDetalle.ItemsSource = Nothing
        grdDetalle.Items.Clear()
        cmdEliminar.IsEnabled = False
        cmdModificar.IsEnabled = False
    End Sub
    Private Sub DPFechaTermino_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles DPFechaTermino.SelectedDateChanged
        Dim tbVentas As New DT
        If bCargaInicial = False Then Exit Sub
        If DPFechaInicio.Text > DPFechaTermino.Text Then
            DPFechaInicio.Text = Today
            DPFechaTermino.Text = Today
            MsgBox("Fecha de inicio no puede ser mayor que fecha de termino")
            Exit Sub
        End If
        grdVenta.ItemsSource = Nothing
        grdVenta.Items.Clear()
        If cbCliente.SelectedValue = "" Then
            oVentas.FechaVenta = DPFechaInicio.Text
            oVentas.FechaTerminoVenta = DPFechaTermino.Text
            tbVentas = oVentas.ObtieneVenta()
            If tbVentas.EOF = False Then
                grdVenta.ItemsSource = tbVentas.DT.DefaultView
            End If
        Else
            oVentas.FechaVenta = DPFechaInicio.Text
            oVentas.FechaTerminoVenta = DPFechaTermino.Text
            oVentas.CodigoCliente = cbCliente.SelectedValue
            tbVentas = oVentas.ObtieneVentaCliente()
            If tbVentas.EOF = False Then
                grdVenta.ItemsSource = tbVentas.DT.DefaultView
            End If
        End If
    End Sub

    Private Sub cmBuscar_Click(sender As Object, e As RoutedEventArgs) Handles cmBuscar.Click

        If DPFechaInicio.Text > DPFechaTermino.Text Then
            DPFechaInicio.Text = Today
            DPFechaTermino.Text = Today
            MsgBox("Fecha de inicio no puede ser mayor que fecha de termino")
            Exit Sub
        End If
        CargaVentas()
    End Sub

    Public Sub CargaVentas()
        Dim tbVentas As New DT
        grdVenta.ItemsSource = Nothing
        grdDetalle.ItemsSource = Nothing
        If cbCliente.SelectedValue = "" Then
            grdVenta.Items.Clear()
            oVentas.FechaVenta = DPFechaInicio.Text
            oVentas.FechaTerminoVenta = DPFechaTermino.Text
            tbVentas = oVentas.ObtieneVenta()
            If tbVentas.EOF = False Then
                grdVenta.ItemsSource = tbVentas.DT.DefaultView
            End If
        Else
            oVentas.FechaVenta = DPFechaInicio.Text
            oVentas.FechaTerminoVenta = DPFechaTermino.Text
            oVentas.CodigoCliente = cbCliente.SelectedValue
            tbVentas = oVentas.ObtieneVentaCliente()
            If tbVentas.EOF = False Then
                grdVenta.ItemsSource = tbVentas.DT.DefaultView
            End If
        End If
    End Sub

    Private Sub grdVenta_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdVenta.SelectionChanged
        Try
            Me.DataContext = Nothing
            If grdVenta.SelectedIndex = -1 Then Exit Sub
            cmdEliminar.IsEnabled = True
            cmdModificar.IsEnabled = True
            bGrilla = True
            oVentas.IdVenta = grdVenta.SelectedItem(0).ToString
            cbCliente.SelectedValue = grdVenta.SelectedItem(2).ToString
            oVentas.TotalImpuesto = grdVenta.SelectedItem(3).ToString
            oVentas.TotalProductos = grdVenta.SelectedItem(4).ToString
            oVentas.MontoTotalNeto = grdVenta.SelectedItem(5).ToString
            oVentas.MontoTotalVenta = grdVenta.SelectedItem(6).ToString
            Me.DataContext = oVentas
            RefreshDetalle()
            'cmAgregarDetalle.IsEnabled = True
        Catch ex As Exception
        End Try
    End Sub
    Private Sub RefreshGrid()
        Dim tbVentas As New DT
        tbVentas = oVentas.ObtieneVenta()
        If tbVentas.EOF = False Then
            grdVenta.ItemsSource = tbVentas.DT.DefaultView
        End If
    End Sub
    Private Sub RefreshDetalle()
        Dim tbDetalle As New DT

        tbDetalle = oVentas.CargaDetalleVenta()
        If tbDetalle.EOF = False Then
            grdDetalle.ItemsSource = tbDetalle.DT.DefaultView
        Else
            grdDetalle.ItemsSource = Nothing
        End If
    End Sub

    Private Sub cmdModificar_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificar.Click
        oWVenta = New WVentas
        oWVenta.IdVenta = oVentas.IdVenta
        oWVenta.Cliente = cbCliente.SelectedValue
        oWVenta.TipoProceso = cConstantes.MODIFICAR
        oWVenta.TotalProducto = oVentas.TotalProductos
        oWVenta.TotalNeto = oVentas.MontoTotalNeto
        oWVenta.TotalImpuesto = oVentas.TotalImpuesto
        oWVenta.TotalVenta = oVentas.MontoTotalVenta
        Opacar()
        oWVenta.ShowDialog()
        Aclarar()
        Limpiar()

    End Sub

    Public Sub Opacar()
        Dim sb As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
        sb.Begin(uVentas)
    End Sub

    Public Sub Totalizadores(ByVal nTotalProducto As Double, ByVal nMontoVentaNetaDiaria As Double, ByVal nMontoVentaDiaria As Double) Handles oWVenta.ActualizaVenta
        Me.DataContext = Nothing

        oVentas.TotalVentaNetaDiaria = oVentas.TotalVentaNetaDiaria + nMontoVentaNetaDiaria
        oVentas.TotalVentaDiaria = oVentas.TotalVentaDiaria + nMontoVentaDiaria
        oVentas.TotalImpuestoVentaDiaria = oVentas.TotalVentaDiaria - oVentas.TotalVentaNetaDiaria
        oVentas.TotalProductoVentaDiaria = oVentas.TotalProductoVentaDiaria + nTotalProducto
    End Sub

    Private Sub cmdEliminar_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminar.Click
        Dim result As MessageBoxResult
        Try
            oVentas.IdVenta = txtIDVenta.Text
            oVentas.FechaVenta = DPFechaInicio.Text
            result = Toolkit.MessageBox.Show("¿Confirme intención de eliminar venta?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oVentas.EliminaVenta() Then
                    RaiseEvent ActualizaVenta()
                    Toolkit.MessageBox.Show("Eliminación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oClientes.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

            Limpiar()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible eliminar venta.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub
    Public Sub CapturaVentaModificaion() Handles oWVenta.ActualizaVentaModificacion
        CargaVentas()
        RaiseEvent ActualizaVentaDiaria()
    End Sub
End Class
