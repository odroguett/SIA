Imports System.Windows.Media.Animation
Imports cConexion
Imports System.Diagnostics
Imports Xceed.Wpf
Imports Microsoft.Win32

Class MainWindow

    Private oUproveedores As New uProveedores
    Private oUmantenciones As New uMantenciones
    Private oUClientes As New UClientes
    Private oUProductos As New UProductos
    Private oUBodegas As New UBodegas
    Private oUproduccion As New UProduccion
    Private oUInventario As New UInventario
    Private WithEvents oUVentas As New UVentas
    Private oVentas As New cVentas
    Private WithEvents oWVenta As WVentas
    Private bAperturaVentas As Boolean = False

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        InicializarVariables()
        oConversion.CargaDatos()
        oParametros.CargaParametros()
    End Sub
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbVentaDiaria As New DT
        DPFechaVenta.IsEnabled = False
        DPFechaVenta.Text = Today

        oVentas.TotalVentaDiaria = 0
        oVentas.TotalVentaNetaDiaria = 0
        oVentas.TotalImpuestoVentaDiaria = 0
        oVentas.TotalProductoVentaDiaria = 0
        oVentas.FechaVenta = Today

        tbVentaDiaria = oVentas.ObtieneVentaDiaria
        If tbVentaDiaria.EOF = False Then
            oVentas.TotalVentaDiaria = tbVentaDiaria.GetValue("TOTAL_VENTA_DIARIA")
            oVentas.TotalVentaNetaDiaria = tbVentaDiaria.GetValue("TOTAL_VENTA_NETA_DIARIA")
            oVentas.TotalImpuestoVentaDiaria = tbVentaDiaria.GetValue("TOTAL_VENTA_DIARIA") - tbVentaDiaria.GetValue("TOTAL_VENTA_NETA_DIARIA")
            oVentas.TotalProductoVentaDiaria = tbVentaDiaria.GetValue("TOTAL_PRODUCTOS_VENTA_DIARIA")
        End If
        Me.DataContext = oVentas
        cvPanel.Children.Clear()
        cvPanel.Children.Add(oUVentas)
        oUVentas.Aclarar()
    End Sub
    Private Sub cmdVenta_Click(sender As Object, e As RoutedEventArgs) Handles cmdVenta.Click
        cvPanel.Children.Clear()
        cvPanel.Children.Add(oUVentas)
        oUVentas.Aclarar()
    End Sub
    Private Sub cmdBodega_Click(sender As Object, e As RoutedEventArgs) Handles cmdBodega.Click
        cvPanel.Children.Clear()
        cvPanel.Children.Add(oUBodegas)
        oUBodegas.Aclarar()
    End Sub

    Private Sub cmdProductos_Click(sender As Object, e As RoutedEventArgs) Handles cmdProductos.Click
        cvPanel.Children.Clear()
        cvPanel.Children.Add(oUProductos)
        oUProductos.Aclarar()
    End Sub


    Private Sub cmdClientes_Click(sender As Object, e As RoutedEventArgs) Handles cmdClientes.Click
        cvPanel.Children.Clear()
        cvPanel.Children.Add(oUClientes)
        oUClientes.Aclarar()
    End Sub

    Private Sub cmdProveedores_Click(sender As Object, e As RoutedEventArgs) Handles cmdProveedores.Click

        cvPanel.Children.Clear()
        cvPanel.Children.Add(oUproveedores)
        oUproveedores.Aclarar()

    End Sub

    Private Sub cmdMantenciones_Click(sender As Object, e As RoutedEventArgs) Handles cmdMantenciones.Click
        cvPanel.Children.Clear()
        cvPanel.Children.Add(oUmantenciones)
        oUmantenciones.Aclarar()
        cmdMantenciones.Visibility = False
    End Sub


    Private Sub cmdProduccion_Click(sender As Object, e As RoutedEventArgs) Handles cmdProduccion.Click
        cvPanel.Children.Clear()
        cvPanel.Children.Add(oUproduccion)
        oUproduccion.Aclarar()

    End Sub
    Private Sub cmdInventario_Click(sender As Object, e As RoutedEventArgs) Handles cmdInventario.Click
        cvPanel.Children.Clear()
        cvPanel.Children.Add(oUInventario)
        oUInventario.Aclarar()
    End Sub

    Private Sub cmdDetalleVenta_Click(sender As Object, e As RoutedEventArgs) Handles cmdDetalleVenta.Click
        If bAperturaVentas = False Then
            bAperturaVentas = True
            oWVenta = New WVentas
            oWVenta.Show()
        End If

        'cmdDetalleVenta.IsEnabled = False
    End Sub
    Private Sub CapturaCierreVentas() Handles oWVenta.Closed
        bAperturaVentas = False
    End Sub

    Public Sub Totalizadores(ByVal nTotalProducto As Double, ByVal nMontoVentaNetaDiaria As Double, ByVal nMontoVentaDiaria As Double) Handles oWVenta.ActualizaVenta
        Me.DataContext = Nothing
        oVentas.TotalVentaNetaDiaria = oVentas.TotalVentaNetaDiaria + nMontoVentaNetaDiaria
        oVentas.TotalVentaDiaria = oVentas.TotalVentaDiaria + nMontoVentaDiaria
        oVentas.TotalImpuestoVentaDiaria = oVentas.TotalVentaDiaria - oVentas.TotalVentaNetaDiaria
        oVentas.TotalProductoVentaDiaria = oVentas.TotalProductoVentaDiaria + nTotalProducto
        Me.DataContext = oVentas
    End Sub

    Public Sub Totalizadores1() Handles oUVentas.ActualizaVenta
        Dim tbVentaDiaria As New DT
        Me.DataContext = Nothing
        oVentas.FechaVenta = Today
        tbVentaDiaria = oVentas.ObtieneVentaDiaria
        If tbVentaDiaria.EOF = False Then
            oVentas.TotalVentaNetaDiaria = tbVentaDiaria.GetValue("TOTAL_VENTA_NETA_DIARIA")
            oVentas.TotalVentaDiaria = tbVentaDiaria.GetValue("TOTAL_VENTA_DIARIA")
            oVentas.TotalImpuestoVentaDiaria = tbVentaDiaria.GetValue("TOTAL_VENTA_DIARIA") - tbVentaDiaria.GetValue("TOTAL_VENTA_NETA_DIARIA")
            oVentas.TotalProductoVentaDiaria = tbVentaDiaria.GetValue("TOTAL_PRODUCTOS_VENTA_DIARIA")
        End If

        Me.DataContext = oVentas
    End Sub
    Public Sub Totalizadores2() Handles oUVentas.ActualizaVentaDiaria
        Dim tbVentaDiaria As New DT
        Me.DataContext = Nothing
        oVentas.FechaVenta = Today
        tbVentaDiaria = oVentas.ObtieneVentaDiaria
        If tbVentaDiaria.EOF = False Then
            oVentas.TotalVentaNetaDiaria = tbVentaDiaria.GetValue("TOTAL_VENTA_NETA_DIARIA")
            oVentas.TotalVentaDiaria = tbVentaDiaria.GetValue("TOTAL_VENTA_DIARIA")
            oVentas.TotalImpuestoVentaDiaria = tbVentaDiaria.GetValue("TOTAL_VENTA_DIARIA") - tbVentaDiaria.GetValue("TOTAL_VENTA_NETA_DIARIA")
            oVentas.TotalProductoVentaDiaria = tbVentaDiaria.GetValue("TOTAL_PRODUCTOS_VENTA_DIARIA")
        End If

        Me.DataContext = oVentas
    End Sub

    Private Sub cmdSalir_Click(sender As Object, e As RoutedEventArgs) Handles cmdSalir.Click
        System.Windows.Application.Current.Shutdown()
    End Sub

    Private Sub cmdCalculadora_Click(sender As Object, e As RoutedEventArgs) Handles cmdCalculadora.Click
        Try
            Dim Proceso As New Process()
            Proceso.StartInfo.FileName = "calc.exe"
            Proceso.StartInfo.Arguments = ""
            Proceso.Start()
        Catch ex As Exception
            Toolkit.MessageBox.Show("Aplicacion no disponible.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub

    Private Sub cmdNotePad_Click(sender As Object, e As RoutedEventArgs) Handles cmdNotePad.Click
        Try
            Dim Proceso As New Process()
            Proceso.StartInfo.FileName = "notepad.exe"
            Proceso.StartInfo.Arguments = ""
            Proceso.Start()
        Catch ex As Exception
            Toolkit.MessageBox.Show("Aplicacion no disponible.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub

    Private Sub cmdexplorador_Click(sender As Object, e As RoutedEventArgs) Handles cmdexplorador.Click
        Try
            Dim Proceso As New Process()
            Proceso.StartInfo.FileName = "explorer.exe"
            Proceso.StartInfo.Arguments = ""
            Proceso.Start()
        Catch ex As Exception
            Toolkit.MessageBox.Show("Aplicacion no disponible.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub

    Private Sub cmdCierre_Click(sender As Object, e As RoutedEventArgs) Handles cmdCierre.Click
        Dim sNombre As String
        Dim saveFileDialog As New SaveFileDialog

        sNombre = "CierreDiario"

        saveFileDialog.FileName = sNombre + ".xls"
        saveFileDialog.Filter = sNombre + "|*.xls"

        'saveFileDialog.InitialDirectory = sDirectorioTraspaso
        'If saveFileDialog.ShowDialog() = System.Windows.DialogResult.OK Then

        '    oArchivos.GeneraExcel(SaveFileDialog1.FileName, sNombre, grdRegistros)

        ' End If
    End Sub

    Private Sub cmdIExplorer_Click(sender As Object, e As RoutedEventArgs) Handles cmdIExplorer.Click
        Try
            Dim Proceso As New Process()
            Proceso.StartInfo.FileName = "iExplore.exe"
            Proceso.StartInfo.Arguments = ""
            Proceso.Start()
        Catch ex As Exception
            Toolkit.MessageBox.Show("Aplicacion no disponible.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub
End Class
