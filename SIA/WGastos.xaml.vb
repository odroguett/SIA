Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf
Public Class WGastos
    Private oGastos As New cGastos
    Private oValidador As New uValidador
    Private sCodigo As String
    Private nTotalGasto As Double
    Private dFechaIngreso As Date
    Private bGrilla As Boolean = False
    Public Property Codigo
        Get
            Return sCodigo
        End Get
        Set(value)
            sCodigo = value
        End Set
    End Property
    Public Property TotalGasto
        Get
            Return oGastos.TotalGastos
        End Get
        Set(value)
            nTotalGasto = value
        End Set
    End Property
    Public WriteOnly Property FechaIngreso
        Set(value)
            '  dFechaIngreso = value
            oGastos.FechaIngreso = value
        End Set
    End Property

    Private Sub WGastos_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim tbGastos As New DT
        grdGasto.IsReadOnly = True
        oGastos.Codigo = sCodigo
        tbGastos = oGastos.CargaTipoGastos
        If tbGastos.EOF = False Then
            cbDescripcion.DataContext = tbGastos.DT
        End If
        cmdEliminar.IsEnabled = False
        cmdModificar.IsEnabled = False
        RefreshGrid()

    End Sub
    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()
        Aclarar()
        ' Add any initialization after the InitializeComponent() call.

    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(cvGastos)
    End Sub



    Private Sub cmAgregar_Click(sender As Object, e As RoutedEventArgs) Handles cmAgregar.Click
        Dim result As MessageBoxResult
        Try
            oErrores.InicializaErrores()
            CargaDatos()
            Me.DataContext = oGastos
            If oGastos.Valida = False Then
                'MsgBox("Datos ingresados se encuentran erroneos, no es posible continuar")
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvGastos)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Agrega Gasto"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvGastos)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("Recuerde que para aplicar gasto, debe modificar el producto generado!!!,¿Confirme intención de ingresar gasto?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oGastos.IngresaGastoProducto Then
                    Toolkit.MessageBox.Show("Operación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oGastos.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible ingresar gasto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub
    Private Sub cmdModificar_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificar.Click
        Dim result As MessageBoxResult
        Try
            oErrores.InicializaErrores()
            CargaDatos()
            Me.DataContext = oGastos
            oGastos.Valida = True

            If oGastos.Valida = False Then
                'MsgBox("Datos ingresados se encuentran erroneos, no es posible continuar")
                Dim sb As Storyboard = DirectCast(FindResource("AclararValidador"), Storyboard)
                Dim sb1 As Storyboard = DirectCast(FindResource("Opacidad"), Storyboard)
                sb1.Begin(Me.cvGastos)
                sb.Begin(oValidador.grValidador)
                oValidador.Titulo = "Modifica Gasto"
                oValidador.ArrayError = oErrores.ArrayError
                oValidador.Advertencia()
                oValidador.ShowDialog()
                sb.Begin(Me.cvGastos)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("Recuerde que para aplicar gasto, debe modificar el producto generado!!! ¿Confirme intención de actualizar gasto?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oGastos.ActualizaGastoProducto Then
                    Toolkit.MessageBox.Show("Actualización realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                    RefreshGrid()
                Else
                    Toolkit.MessageBox.Show(oGastos.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If
            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible actualizar gasto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub
    Private Sub cmdEliminar_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminar.Click
        Dim result As MessageBoxResult
        Try
            CargaDatos()
            Me.DataContext = oGastos
            result = Toolkit.MessageBox.Show("Recuerde que para aplicar gasto, debe modificar el producto generado!!! ¿Confirme intención de eliminar gasto?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If oGastos.EliminaGastoProducto() Then
                    Toolkit.MessageBox.Show("Eliminación realizada con exito.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                Else
                    Toolkit.MessageBox.Show(oGastos.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

            RefreshGrid()
            LimpiaControles()
        Catch ex As Exception
            Toolkit.MessageBox.Show("No es posible eliminar gasto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try
    End Sub
    Private Sub cmdCancelar_Click(sender As Object, e As RoutedEventArgs) Handles cmdCancelar.Click
        nTotalGasto = oGastos.TotalGastos
        Me.Close()
    End Sub

    Private Sub RefreshGrid()
        Dim tbGastos As New DT

        tbGastos = oGastos.CargaGastos
        If tbGastos.EOF = False Then
            grdGasto.ItemsSource = tbGastos.DT.DefaultView
        Else
            grdGasto.ItemsSource = Nothing
        End If
    End Sub

    Private Sub LimpiaControles()
        txtCodGasto.Text = ""
        cbDescripcion.SelectedValue = ""
        txtMonto.Text = 0
        oGastos.CodigoGasto = ""
        oGastos.Gasto = 0
        Me.DataContext = Nothing
        cmAgregar.IsEnabled = True
        cmdEliminar.IsEnabled = False
        cmdModificar.IsEnabled = False
        cbDescripcion.IsEnabled = True

    End Sub


    Private Sub CargaDatos()
        oGastos.Codigo = sCodigo
        oGastos.CodigoGasto = txtCodGasto.Text
        oGastos.Gasto = txtMonto.Text
        ' oGastos.FechaIngreso = dFechaIngreso
    End Sub

    Private Sub grdGasto_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdGasto.SelectionChanged
        Try
            If grdGasto.SelectedIndex = -1 Then Exit Sub
            Me.DataContext = Nothing
            cmAgregar.IsEnabled = False
            cmdEliminar.IsEnabled = True
            cmdModificar.IsEnabled = True
            cbDescripcion.IsEnabled = False
            bGrilla = True
            oGastos.Gasto = grdGasto.SelectedItem(3).ToString
            oGastos.CodigoGasto = grdGasto.SelectedItem(1).ToString
            cbDescripcion.SelectedValue = grdGasto.SelectedItem(1).ToString
            Me.DataContext = oGastos
        Catch ex As Exception

        End Try


    End Sub

    Private Sub txtMonto_PreviewTextInput(sender As Object, e As TextCompositionEventArgs) Handles txtMonto.PreviewTextInput
        Dim ascci = Convert.ToInt32(Convert.ToChar(e.Text))

        If (ascci >= 48 And ascci <= 57) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub cbDescripcion_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cbDescripcion.SelectionChanged
        If bGrilla = True Then
            bGrilla = False
            Exit Sub
        End If
        Me.DataContext = Nothing
        oGastos.CodigoGasto = cbDescripcion.SelectedValue
        Me.DataContext = oGastos
    End Sub

    Private Sub cmdLimpiar_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiar.Click
        LimpiaControles()
    End Sub
End Class
