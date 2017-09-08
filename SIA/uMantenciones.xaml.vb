Imports System.Windows.Media.Animation
Imports cConexion
Imports Xceed.Wpf


Public Class uMantenciones
    Private oTipoProducto As New cTiposProducto
    Private oTiposUnidad As New cTiposUnidad
    Private oGruposProducto As New cGruposProducto
    Private oGasto As New cGastos
    Private oParametros As New cParametros
    Private bTipoProductoGrupo As Boolean = False
    Private bTipoUnidadGasto As Boolean = False
    Private sPreviewTextProducto As String
    Private sPreviewTextGrupo As String
    Private sPreviewTextGasto As String
#Region "Comunes"

    Private Sub RefreshGridMantenciones()
        Dim tbProductos As New DT
        Dim tbTiposUnidad As New DT
        Dim tbTipoGrupo As New DT
        Dim tbTipoGasto As New DT

        grdProducto.IsReadOnly = True
        grdTiposUnidad.IsReadOnly = True
        grdGrupo.IsReadOnly = True
        grdTipoGasto.IsReadOnly = True
        tbProductos = oTipoProducto.CargaTipoProductos()
        If tbProductos.EOF = False Then
            grdProducto.ItemsSource = tbProductos.DT.DefaultView
        End If

        tbTiposUnidad = oTiposUnidad.CargaTiposUnidad
        If tbTiposUnidad.EOF = False Then
            grdTiposUnidad.ItemsSource = tbTiposUnidad.DT.DefaultView
        End If

        tbTipoGrupo = oGruposProducto.CargaGrupos()
        If tbTipoGrupo.EOF = False Then
            grdGrupo.ItemsSource = tbTipoGrupo.DT.DefaultView
        End If
        tbTipoGasto = oGasto.CargaTipoGastos()
        If tbTipoGasto.EOF = False Then
            grdTipoGasto.ItemsSource = tbTipoGasto.DT.DefaultView
        End If
    End Sub
    Private Sub uMantenciones_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        RefreshGridMantenciones()
        BloqueaControlesTipoUnidad()
        CargaParametros()
    End Sub
    Private Sub BloqueaControlesTipoUnidad()
        txtCodigoUnidad.IsEnabled = False
        txtDescripcionUnidad.IsEnabled = False
        cmdAgregarTipoUnidad.IsEnabled = False
        cmdModificarTipoUnidad.IsEnabled = False
        cmdEliminarTipoUnidad.IsEnabled = False
        cmdLimpiarTipoUnidad.IsEnabled = False
        cmdImpTipoUni.IsEnabled = False
    End Sub
    Public Sub Aclarar()
        Dim sb As Storyboard = DirectCast(FindResource("Aclarar"), Storyboard)
        sb.Begin(GridMantenciones)
    End Sub

    Private Sub cmdTipoProducto_Click(sender As Object, e As RoutedEventArgs) Handles cmdTipoProducto.Click
        Dim sb1 As Storyboard = DirectCast(FindResource("Invisible"), Storyboard)
        Dim sb As Storyboard = DirectCast(FindResource("Visible"), Storyboard)
        '   If bTipoProductoGrupo = True Then
        sb1.Begin(cvTiposGrupo)
            sb1.Begin(cvParametros)
            sb.Begin(cvTiposProducto)
            cvTiposGrupo.Visibility = Visibility.Hidden
            cvTiposProducto.Visibility = Visibility.Visible
            cvParametros.Visibility = Visibility.Hidden
            bTipoProductoGrupo = False
        '  End If
    End Sub

    Private Sub cndTiposUnidad_Click(sender As Object, e As RoutedEventArgs) Handles cndTiposUnidad.Click
        Dim sb1 As Storyboard = DirectCast(FindResource("Invisible"), Storyboard)
        Dim sb As Storyboard = DirectCast(FindResource("Visible"), Storyboard)
        ' If bTipoUnidadGasto = True Then
        sb1.Begin(cvTiposGasto)
            sb.Begin(cvTiposUnidad)
            cvTiposUnidad.Visibility = Visibility.Visible
            cvTiposGasto.Visibility = Visibility.Hidden

            bTipoUnidadGasto = False
        '   End If

    End Sub

    Private Sub cmdTiposGrupos_Click(sender As Object, e As RoutedEventArgs) Handles cmdTiposGrupos.Click
        Dim sb1 As Storyboard = DirectCast(FindResource("Invisible"), Storyboard)
        Dim sb As Storyboard = DirectCast(FindResource("Visible"), Storyboard)
        '   If bTipoProductoGrupo = False Then
        sb1.Begin(cvTiposProducto)
            sb1.Begin(cvParametros)
            sb.Begin(cvTiposGrupo)
            cvTiposGrupo.Visibility = Visibility.Visible
            cvTiposProducto.Visibility = Visibility.Hidden
            cvParametros.Visibility = Visibility.Hidden
            bTipoProductoGrupo = True
        '  End If

    End Sub

    Private Sub cmdGastos_Click(sender As Object, e As RoutedEventArgs) Handles cmdGastos.Click
        Dim sb1 As Storyboard = DirectCast(FindResource("Invisible"), Storyboard)
        Dim sb As Storyboard = DirectCast(FindResource("Visible"), Storyboard)
        '    If bTipoUnidadGasto = False Then
        sb1.Begin(cvTiposUnidad)
            sb.Begin(cvTiposGasto)
            cvTiposGasto.Visibility = Visibility.Visible
            cvTiposUnidad.Visibility = Visibility.Hidden
            cmdGastos.IsEnabled = True
            grdTipoGasto.IsEnabled = True
            bTipoUnidadGasto = True
        '    End If
    End Sub
    Private Sub cmaParametro_Click(sender As Object, e As RoutedEventArgs) Handles cmaParametro.Click
        Dim sb1 As Storyboard = DirectCast(FindResource("Invisible"), Storyboard)
        Dim sb As Storyboard = DirectCast(FindResource("Visible"), Storyboard)
        '   If bTipoProductoGrupo = True Then
        sb1.Begin(cvTiposGrupo)
        sb1.Begin(cvTiposProducto)
        sb.Begin(cvParametros)
        cvTiposGrupo.Visibility = Visibility.Hidden
        cvTiposProducto.Visibility = Visibility.Hidden
        cvParametros.Visibility = Visibility.Visible
        bTipoProductoGrupo = True
        '    End If
    End Sub

#End Region
#Region "Tipos Producto"
    Private Sub cmdAgregaProducto_Click(sender As Object, e As RoutedEventArgs) Handles cmdAgregaProducto.Click
        Dim result As MessageBoxResult
        Try
            txtCodigoProducto.IsEnabled = True
            If Not ValidacionesProducto(cConstantes.AGREGAR) Then
                Toolkit.MessageBox.Show("Atencion, debe ingresar Código y Descripción.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                Exit Sub
            End If
            oTipoProducto.Codigo = txtCodigoProducto.Text
            oTipoProducto.Descripcion = txtDescripcionProducto.Text
            result = Toolkit.MessageBox.Show("Confirme intención de agregar tipo de producto.", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If Not oTipoProducto.IngresaTipoProducto() Then
                    Toolkit.MessageBox.Show(oTipoProducto.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)

                End If
                txtCodigoProducto.Text = ""
                txtDescripcionProducto.Text = ""
                RefreshGridMantenciones()
            End If
        Catch ex As Exception
            Toolkit.MessageBox.Show("Error al ingresar tipo de producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try



    End Sub

    Private Sub cmdModificarProducto_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificarProducto.Click
        Dim result As MessageBoxResult
        Try
            If ValidacionesProducto(cConstantes.MODIFICAR) Then
                result = Toolkit.MessageBox.Show("Confirme intención de modificar tipo de producto.", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
                If result = MessageBoxResult.OK Then
                    oTipoProducto.Codigo = txtCodigoProducto.Text
                    oTipoProducto.Descripcion = txtDescripcionProducto.Text
                    oTipoProducto.ActualizaTipoProducto()
                    DesHabilitaControlesTiposProducto()
                End If
            Else

                Toolkit.MessageBox.Show("Atencion, debe ingresar Código y Descripción.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                Exit Sub

            End If
            cmdAgregaProducto.IsEnabled = True
            txtCodigoProducto.IsEnabled = True
            txtCodigoProducto.Text = ""
            txtDescripcionProducto.Text = ""
        Catch ex As Exception
            Toolkit.MessageBox.Show("Error al modificar tipo de producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub



    Private Function ValidacionesProducto(ByVal sModo As String) As Boolean
        Dim bValida As Boolean = True
        If sModo = cConstantes.AGREGAR Then
            If txtCodigoProducto.Text = "" Or txtDescripcionProducto.Text = "" Then
                Return False
            End If
        Else
            If txtCodigoProducto.Text = "" Or txtDescripcionProducto.Text = "" Then
                Return False
            End If
        End If
        Return bValida
    End Function
    Private Sub cmdEliminaProducto_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminaProducto.Click
        Dim result As MessageBoxResult
        Try
            If Not ValidacionesProducto(cConstantes.ELIMINAR) Then
                Toolkit.MessageBox.Show("Atencion, debe Seleccionar producto a eliminar.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                DesHabilitaControlesTiposProducto()
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("Precaución. Pueden existir productos asociados al tipo de producto seleccionado, ¿Esta seguro que desea continuar?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                oTipoProducto.Codigo = txtCodigoProducto.Text
                oTipoProducto.EliminaTipoProducto()
            End If
            RefreshGridMantenciones()
            DesHabilitaControlesTiposProducto()

        Catch ex As Exception
            Toolkit.MessageBox.Show("Error al eliminar tipo de producto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub
    Private Sub grdProducto_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdProducto.SelectionChanged
        Try
            HabilitaControlesTiposProducto()
            cmdAgregaProducto.IsEnabled = False
            txtCodigoProducto.IsEnabled = False
            If Not IsNothing(grdProducto.SelectedItem(1).ToString) Then sPreviewTextProducto = grdProducto.SelectedItem(1).ToString

        Catch ex As Exception

        End Try

    End Sub
    Private Sub DesHabilitaControlesTiposProducto()
        cmdModificarProducto.IsEnabled = False
        cmdEliminaProducto.IsEnabled = False
        cmdAgregaProducto.IsEnabled = True
        txtCodigoProducto.IsEnabled = True
        txtCodigoProducto.Text = ""
        txtDescripcionProducto.Text = ""
    End Sub
    Private Sub HabilitaControlesTiposProducto()
        cmdModificarProducto.IsEnabled = True
        cmdEliminaProducto.IsEnabled = True

    End Sub
    Private Sub cmdLimpiarProducto_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiarProducto.Click
        cmdAgregaProducto.IsEnabled = True
        cmdModificarProducto.IsEnabled = False
        txtCodigoProducto.Text = ""
        txtDescripcionProducto.Text = ""
        txtCodigoProducto.IsEnabled = True

    End Sub

    Private Sub txtDescripcionProducto_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtDescripcionProducto.LostFocus
        If txtDescripcionProducto.Text = "" Then
            Toolkit.MessageBox.Show("Atencion,Descripción de tipo de producto no puede ser vacia.", "SIA", MessageBoxButton.OK, MessageBoxImage.Warning)
            txtDescripcionProducto.Text = sPreviewTextProducto
        End If
    End Sub






#End Region
#Region "Tipos Unidad"

    'Private Sub cmdAgregarTipoUnidad_Click(sender As Object, e As RoutedEventArgs) Handles cmdAgregarTipoUnidad.Click
    '    txtCodigoUnidad.IsEnabled = True
    '    oProducto.TipoUnidad1 = txtCodigoUnidad.Text
    '    oProducto.Descripcion = txtDescripcionUnidad.Text
    '    oProducto.IngresaTiposUnidad()
    '    txtCodigoUnidad.Text = ""
    '    txtDescripcionUnidad.Text = ""
    '    RefreshGridMantenciones()
    'End Sub

    'Private Sub cmdModificarTipoUnidad_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificarTipoUnidad.Click
    '    If ValidacionesTipoUnidad("M") Then
    '        oProducto.TipoUnidad1 = txtCodigoUnidad.Text
    '        oProducto.Descripcion = txtDescripcionUnidad.Text
    '        oProducto.ActualizaTiposUnidad()
    '        DesHabilitaControlesTiposUnidad()
    '    End If
    '    cmdAgregarTipoUnidad.IsEnabled = True
    '    txtCodigoUnidad.IsEnabled = True
    '    txtCodigoUnidad.Text = ""
    '    txtDescripcionUnidad.Text = ""
    'End Sub

    'Private Sub grdTiposUnidad_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdTiposUnidad.SelectionChanged
    '    HabilitaControlesTiposUnidad()
    '    cmdAgregarTipoUnidad.IsEnabled = False
    '    txtCodigoUnidad.IsEnabled = False
    'End Sub

    'Private Sub cmdEliminarTipoUnidad_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminarTipoUnidad.Click
    '    oProducto.TipoUnidad1 = txtCodigoUnidad.Text
    '    oProducto.EliminaTiposUnidad()
    '    RefreshGridMantenciones()
    '    DesHabilitaControlesTiposUnidad()
    '    cmdAgregarTipoUnidad.IsEnabled = True
    '    txtCodigoUnidad.IsEnabled = True
    '    txtCodigoUnidad.Text = ""
    '    txtDescripcionUnidad.Text = ""
    'End Sub
    'Private Function ValidacionesTipoUnidad(ByVal sModo As String) As Boolean
    '    Dim bValida As Boolean = True
    '    If sModo = cConstantes.AGREGAR Then
    '        If txtCodigoUnidad.Text = "" And txtDescripcionUnidad.Text = "" Then
    '            MsgBox("Debe ingresar tipo unidad y descripción.")
    '            Return False
    '        End If
    '    Else
    '        If txtCodigoUnidad.Text = "" And txtDescripcionUnidad.Text = "" Then
    '            MsgBox("Debe ingresar tipo unidad y descripción.")
    '            Return False
    '        End If
    '    End If
    '    Return bValida
    'End Function
    'Private Sub DesHabilitaControlesTiposUnidad()
    '    cmdModificarTipoUnidad.IsEnabled = False
    '    cmdEliminarTipoUnidad.IsEnabled = False
    'End Sub
    'Private Sub HabilitaControlesTiposUnidad()
    '    cmdModificarTipoUnidad.IsEnabled = True
    '    cmdEliminarTipoUnidad.IsEnabled = True

    'End Sub

#End Region
#Region "Tipo Gasto"
    Private Sub cmdAgregarTipoGasto_Click(sender As Object, e As RoutedEventArgs) Handles cmdAgregarTipoGasto.Click
        Dim result As MessageBoxResult
        Try
            If Not ValidacionesGasto(cConstantes.AGREGAR) Then
                Toolkit.MessageBox.Show("Atencion, debe ingresar Código y Descripción.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                Exit Sub
            End If
            txtCodigoGasto.IsEnabled = True
            oGasto.Codigo = txtCodigoGasto.Text
            oGasto.Descripcion = txtDescripcionGasto.Text
            result = Toolkit.MessageBox.Show("Confirme intención de agregar nuevo tipo de gasto.", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If Not oGasto.IngresaTipoGasto() Then
                    Toolkit.MessageBox.Show(oGasto.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                End If
            End If

            txtCodigoGasto.Text = ""
            txtDescripcionGasto.Text = ""
            RefreshGridMantenciones()
        Catch ex As Exception
            Toolkit.MessageBox.Show("Error al ingresar tipo de gasto.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub
    Private Sub cmdModificarTipoGasto_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificarTipoGasto.Click
        Dim result As MessageBoxResult
        If ValidacionesGasto(cConstantes.MODIFICAR) Then
            oGasto.Codigo = txtCodigoGasto.Text
            oGasto.Descripcion = txtDescripcionGasto.Text
            result = Toolkit.MessageBox.Show("Confirme intención de modificar tipo de gasto.", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                oGasto.ActualizaTipoGasto()
            End If
            DesHabilitaControlesTiposGasto()
        Else
            Toolkit.MessageBox.Show("Atencion, debe ingresar Código y Descripción.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            Exit Sub

        End If
        cmdAgregarTipoGasto.IsEnabled = True
        txtCodigoGasto.IsEnabled = True
        txtCodigoGasto.Text = ""
        txtDescripcionGasto.Text = ""
    End Sub
    Private Sub cmdEliminarTipoGasto_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminarTipoGasto.Click
        Dim result As MessageBoxResult
        oGasto.Codigo = txtCodigoGasto.Text

        If Not ValidacionesGasto(cConstantes.ELIMINAR) Then
            Toolkit.MessageBox.Show("Atencion, debe Seleccionar producto a eliminar.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            DesHabilitaControlesTiposProducto()
            Exit Sub
        End If
        result = Toolkit.MessageBox.Show("Precaución. Pueden existir productos asociados al tipo de gasto seleccionado, ¿Esta seguro que desea continuar?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
        If result = MessageBoxResult.OK Then
            oGasto.EliminaTipoGasto()
        End If

        RefreshGridMantenciones()
        DesHabilitaControlesTiposGasto()
        cmdAgregarTipoGasto.IsEnabled = True
        txtCodigoGasto.IsEnabled = True
        txtCodigoGasto.Text = ""
        txtDescripcionGasto.Text = ""
    End Sub
    Private Function ValidacionesGasto(ByVal sModo As String) As Boolean
        Dim bValida As Boolean = True
        If sModo = cConstantes.AGREGAR Then
            If txtCodigoGasto.Text = "" Or txtDescripcionGasto.Text = "" Then
                Return False
            End If
        Else
            If txtCodigoGasto.Text = "" Or txtDescripcionGasto.Text = "" Then
                Return False
            End If
        End If
        Return bValida
    End Function
    Private Sub DesHabilitaControlesTiposGasto()
        cmdModificarProducto.IsEnabled = False
        cmdEliminaProducto.IsEnabled = False
    End Sub
    Private Sub HabilitaControlesTiposGasto()
        cmdModificarTipoGasto.IsEnabled = True
        cmdEliminarTipoGasto.IsEnabled = True

    End Sub
    Private Sub grdTipoGasto_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdTipoGasto.SelectionChanged
        Try
            HabilitaControlesTiposGasto()
            cmdAgregarTipoGasto.IsEnabled = False
            txtCodigoGasto.IsEnabled = False
            sPreviewTextGasto = grdTipoGasto.SelectedItem(1).ToString
        Catch ex As Exception

        End Try

    End Sub
    Private Sub txtDescripcionGasto_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtDescripcionGasto.LostFocus
        If txtDescripcionGasto.Text = "" Then
            Toolkit.MessageBox.Show("Atencion,Descripción de tipo de producto no puede ser vacia.", "SIA", MessageBoxButton.OK, MessageBoxImage.Warning)
            txtDescripcionGasto.Text = sPreviewTextGasto
        End If
    End Sub
    Private Sub cmdLimpiarGasto_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiarGasto.Click
        cmdAgregarTipoGasto.IsEnabled = True
        cmdModificarTipoGasto.IsEnabled = False
        cmdEliminarTipoGasto.IsEnabled = False
        txtCodigoGasto.IsEnabled = True
        txtCodigoGasto.Text = ""
        txtDescripcionGasto.Text = ""
    End Sub

#End Region

#Region "Tipos Grupo"

    Private Sub cmdAgregarGrupo_Click(sender As Object, e As RoutedEventArgs) Handles cmdAgregarGrupo.Click
        Dim result As MessageBoxResult
        Try
            txtCodigoGrupo.IsEnabled = True
            oTipoProducto.Codigo = txtCodigoGrupo.Text
            oTipoProducto.Descripcion = txtDescripcionGrupo.Text
            If Not ValidacionesGrupo(cConstantes.AGREGAR) Then
                Toolkit.MessageBox.Show("Atencion, debe ingresar Código y Descripción.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
                Exit Sub
            End If
            result = Toolkit.MessageBox.Show("Confirme intención de agregar tipo de grupo.", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                If Not oTipoProducto.IngresaTipoGrupo() Then
                    Toolkit.MessageBox.Show(oTipoProducto.Mensaje, "SIA", MessageBoxButton.OK, MessageBoxImage.Warning)
                End If
            End If
            txtCodigoGrupo.Text = ""
            txtDescripcionGrupo.Text = ""
            RefreshGridMantenciones()
        Catch ex As Exception
            Toolkit.MessageBox.Show("Error al ingresar tipo de grupo.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
        End Try

    End Sub
    Private Sub cmdModificarGrupo_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificarGrupo.Click
        Dim result As MessageBoxResult
        If ValidacionesGrupo(cConstantes.MODIFICAR) Then
            oTipoProducto.Codigo = txtCodigoGrupo.Text
            oTipoProducto.Descripcion = txtDescripcionGrupo.Text
            result = Toolkit.MessageBox.Show("Confirme intención de modificar tipo de grupo.", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                oTipoProducto.ActualizaTipoGrupo()
                DesHabilitaControlesTiposGrupo()
            End If
        Else
            Toolkit.MessageBox.Show("Atencion, debe ingresar Código y Descripción.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            Exit Sub
        End If

    End Sub

    Private Sub cmdEliminarGrupo_Click(sender As Object, e As RoutedEventArgs) Handles cmdEliminarGrupo.Click
        Dim result As MessageBoxResult
        oTipoProducto.Codigo = txtCodigoGrupo.Text

        If Not ValidacionesGrupo(cConstantes.ELIMINAR) Then
            Toolkit.MessageBox.Show("Atencion, debe Seleccionar producto a eliminar.", "SIA", MessageBoxButton.OK, MessageBoxImage.Information)
            DesHabilitaControlesTiposGrupo()
            Exit Sub
        End If
        result = Toolkit.MessageBox.Show("Precaución. Pueden existir productos asociados al tipo de grupo seleccionado, ¿Esta seguro que desea continuar?", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
        If result = MessageBoxResult.OK Then
            oTipoProducto.EliminaTipoGrupo()
        End If

        RefreshGridMantenciones()
        DesHabilitaControlesTiposGrupo()
        cmdAgregarGrupo.IsEnabled = True
        txtCodigoGrupo.IsEnabled = True
        txtCodigoGrupo.Text = ""
        txtDescripcionGrupo.Text = ""
    End Sub

    Private Function ValidacionesGrupo(ByVal sModo As String) As Boolean
        Dim bValida As Boolean = True
        If sModo = cConstantes.AGREGAR Then
            If txtCodigoGrupo.Text = "" Or txtDescripcionGrupo.Text = "" Then
                Return False
            End If
        Else
            If txtCodigoGrupo.Text = "" Or txtDescripcionGrupo.Text = "" Then
                Return False
            End If
        End If
        Return bValida
    End Function

    Private Sub DesHabilitaControlesTiposGrupo()
        cmdModificarGrupo.IsEnabled = False
        cmdEliminarGrupo.IsEnabled = False
    End Sub
    Private Sub HabilitaControlesTiposGrupo()
        cmdModificarGrupo.IsEnabled = True
        cmdEliminarGrupo.IsEnabled = True
    End Sub

    Private Sub grdGrupo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles grdGrupo.SelectionChanged
        Try
            HabilitaControlesTiposGrupo()
            cmdAgregarGrupo.IsEnabled = False
            txtCodigoGrupo.IsEnabled = False
            sPreviewTextGrupo = grdGrupo.SelectedItem(1).ToString
        Catch ex As Exception

        End Try

    End Sub

    Private Sub cmdLimpiarGrupo_Click(sender As Object, e As RoutedEventArgs) Handles cmdLimpiarGrupo.Click
        cmdAgregarGrupo.IsEnabled = True
        cmdModificarGrupo.IsEnabled = False
        cmdEliminarGrupo.IsEnabled = False
        txtCodigoGrupo.IsEnabled = True
        txtCodigoGrupo.Text = ""
        txtDescripcionGrupo.Text = ""
    End Sub


























#End Region
#Region "Parametros"
    Private Sub CargaParametros()
        oParametros.CargaParametros()
        Me.DataContext = oParametros
    End Sub

    Private Sub cmdModificarParametro_Click(sender As Object, e As RoutedEventArgs) Handles cmdModificarParametro.Click
        Dim result As MessageBoxResult
        Try
            result = Toolkit.MessageBox.Show("Confirme intención de modificar parametro.", "SIA", MessageBoxButton.OKCancel, MessageBoxImage.Question)
            If result = MessageBoxResult.OK Then
                oParametros.ActualizaParametros()
            End If
            oParametros.CargaParametros()
            Me.DataContext = oParametros
        Catch ex As Exception
            Toolkit.MessageBox.Show("Atencion,Error al actualizar parametros", "SIA", MessageBoxButton.OK, MessageBoxImage.Warning)
        End Try

    End Sub


#End Region



End Class
