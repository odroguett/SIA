   M S B A M L   `   `   `  ����>  :SIA, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:SIASIA  5   	       SIA.WVentas   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   #localclr-namespace:SIA     5   	   -   5          �Name$  WVentas1��5   	    � Title$ Venta��5   	   $��381.429��6   $��550.929��6(   ���	#FF062761 � 
ResizeMode$ NoResize=�6O   .��5	      �� -   5         ��Name$ cvVentas��5	      $��
10,10,2,57q�6   .+�5
   
   �� -   5         ��Name$ 
txtIDVenta��5
      $��25��6&   
 �� Top$ 22��63   $	��105��6C   $��Center=�6O   $��Center=�6l      TextBoxStyle#���   6�   $��0q�6�   $��1q�6�   $��12(�6�   ���	#FFEEECDF���	#FFC4B8EA �� SelectionBrush �	#FFC4B8EA��6'  �� *	IdVenta+ �� UpdateSourceTrigger$ PropertyChanged=� �� ValidatesOnDataErrors . �� 5   
   -   5        	 ��Name$	 tbNumeroIngreso��5      $��22��6-   $
��Wrap=�69   $��Numero Venta��6M   $	��105��6a     TextBlockStyle1#���  6m   $��0q�6�   $��Italic$�6�   
 �� Padding$
 0,0,0,2q�6�   ���	#FFF1ECEC J� RenderTransformOrigin$ 0.143,0.591&�6�   ��6�   Z� 6�   (  "System.Windows.Controls.DatePicker  5   
   -   5          Name$ DPFechaVenta��5       �� Left$	 110��6+   $ 22��6=   $��25��6M   $	��183��6Y   ���	#FDFFFFFF���	#FF1602E2$
��Bold"�6�      Text 6~   �� *
FechaVenta+$ PropertyChanged=� . 6�   �� 5   
   -   5        $	 btFechaVenta��5      $��22��6*   $
��Wrap=�66   $��Fecha Venta��6J   $	��105��6]   #���  6i   $��0q�6�   $��Italic$�6�   $
 0,0,0,2q�6�   ���	#FFF1ECEC$ 0.143,0.591&�6�   $	 110��6  ��6�   Z� 6  &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	grdVentas��5      $	��166��6&   $	 135��63   $	��515��6D      AlternatingRowBackground �	#FFD8F3F5   CanUserDeleteRows .    CanUserAddRows . ���	#FF02002E   AutoGenerateColumns .  ��6�   �� *
ListaVenta+   Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      -   5        	txtCodigo��,  &System.Windows.Controls.DataGridColumn   Header$ Codigo Producto��5   8   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6Q   �� *CodigoProducto+ �� Mode$ TwoWay=�$ 	LostFocus=�5      4  .System.Windows.Controls.DataGridComboBoxColumn  5      -	   5        
CbProducto��   DisplayMemberPath$ Descripcion��5   ?      SelectedValuePath$ Descripcion��6�      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6�   $ Producto��6�      SelectedValueBinding 6_   �� *DescripcionProducto+   ItemsSource 6�   �� *
ListaVenta+   EditingElementStyle 5      �� 5       �� 
TargetType" ��6    .�5      -
   5        5      5        5      $ Valor Producto��6%    6=   �� *Valor+6Y     5      $ 	Descuento��6%    68   �� *	Descuento+6X     5      $ Cantidad��6%    67   �� *
Cantidad+6V   5       �� 5!   
   -   5         ��Name$ 	cmAgregar��5!      $��Agregar��6$   $��26��66   $	 104��6B   $��58��6S   $ 0.495,1.442&�6^     
ButonStyle#���  6�   $��0q�6�     J� Effect  5"      3  -System.Windows.Media.Effects.DropShadowEffect  5#      ��5%      �� 5&      5(      �� 5)   
   -   5        $ cmdEliminar��5)      $��Eliminar��6&   $��26��69   $	 104��6E   $��58��6V   $ 0.495,1.442&�6a   #���  6�   $��0q�6�   $	 130��6�     5*        5+      ��5-      �� 5.      51      �� 52   
   -   5        $ cmdVenta��52      $��Finalizar Venta��6#   $��26��6=   $	 104��6I   $	��103��6Z   $ 0.495,1.442&�6g   #���  6�   $��0q�6�   $	 257��6�     53        54      ��56      �� 57      59      �� 5:   
   -   5        $ 
cmdLimpiar��5:      $��Limpiar��6%   $ 0.495,1.442&�69   #���  6]   $��0q�6�   $	 193��6�   $	 104��6�   $��26��6�   $��59��6�     5;        5<      ��5>      �� 5?      5A      �� 5B   
   -   5        ! ��Name$! 	cbCliente��5B        cbStyle#���  6&   $��25��6H   $	 300��6T   $ 22��6f   $	��215��6v   $ 1.448,-0.136&�6�   " �� DisplayMemberPath$" descripcion��6�   # �� SelectedValuePath$# codigo��6�   ���	#FFEAD5D5$��0q�6  ���	#FF072576��6�   �� 6&  �� 5C   
   -   5        $	 	tbCliente��5C      $��22��6'   $
��Wrap=�63   $��Cliente��6G   $	��215��6V   #���  6b   $��0q�6�   $��Italic$�6�   $
 0,0,0,2q�6�   $	 300��6�   ���	#FFF1ECEC$ 0.143,0.591&�6  ��6�   Z� 6  �� 5D   
   -   5        $ txtTotalProductos��5D      $��25��6-   $ 74��6:   $	��105��6J   $��Center=�6V   $��Center=�6s   #���   6�   $��0q�6�   $��1q�6�   $��12(�6�   ���	#FFEEECDF���	#FFC4B8EA �	#FFC4B8EA$	 101��6�  $ 	0.543,0.2&�6�  ��6.  �� *TotalProductos+$ PropertyChanged=� . 6�  �� 5E   
   -   5        $	 tbTotalProducto��5E      $��22��6-   $
��Wrap=�69   $��Total Prod. Venta��6M   $	��105��6f   #���  6r   $��0q�6�   $��Italic$�6�   $
 0,0,0,2q�6�   ���	#FFF1ECEC$ 0.143,0.591&�6�   $ 52��6#  $	 101��63  ��6�   Z� 63  �� 5F   
   -   5        $	 tbTotalProducto_Copy��5F      $��22��62   $
��Wrap=�6>   $��Total Neto Venta��6R   $	��105��6j   #���  6v   $��0q�6�   $��Italic$�6�   $
 0,0,0,2q�6�   $	 211��6�   ���	#FFF1ECEC$ 0.143,0.591&�6  $ 52��69  ��6�   Z� 69  �� 5G   
   -   5        $ txtTotalNeto��5G      $��25��6(   $ 74��65   $	��105��6E   $��Center=�6Q   $��Center=�6n   #���   6�   $��0q�6�   $��1q�6�   $��12(�6�   ���	#FFEEECDF���	#FFC4B8EA �	#FFC4B8EA$	 211��6(  ��6;  �� *MontoTotalNeto+$ PropertyChanged=� . �� 5H   
   -   5        $	 tbTotalProducto_Copy1��5H      $��22��63   $
��Wrap=�6?   $��Total Imp. Venta��6S   $	��102��6k   #���  6w   $��0q�6�   $��Italic$�6�   $
 0,0,0,2q�6�   $	 321��6�   ���	#FFF1ECEC$ 0.143,0.591&�6  $ 52��6:  ��6�   Z� 6:  �� 5I   
   -   5        $ txtTotalImpuesto��5I      $��25��6,   $ 74��69   $	��102��6I   $��Center=�6U   $��Center=�6r   #���   6�   $��0q�6�   $��1q�6�   $��12(�6�   ���	#FFEEECDF���	#FFC4B8EA �	#FFC4B8EA$	 321��6,  ��6?  �� *TotalImpuesto+$ PropertyChanged=� . �� 5J   
   -   5        $ txtTotalVenta��5J      $��25��6)   $ 74��66   $��87��6F   $��Center=�6Q   $��Center=�6n   #���   6�   $��0q�6�   $��1q�6�   $��12(�6�   ���	#FFEEECDF���	#FFC4B8EA �	#FFC4B8EA$	 428��6(  ��6;  �� *MontoTotalVenta+$ PropertyChanged=� . �� 5K   
   -   5        $	 tbTotalProducto_Copy2��5K      $��22��63   $
��Wrap=�6?   $��Total Venta��6S   $��87��6f   #���  6q   $��0q�6�   $��Italic$�6�   $
 0,0,0,2q�6�   $	 428��6�   ���	#FFF1ECEC$ 0.143,0.591&�6  $ 52��64  ��6�   Z� 64  �� 5L   
   -   5        $	 tbMontoVenta_Copy��5L      $��22��6/   $
��Wrap=�6;   $��Total  $ Producto��6O   $��96��6h   #���  6s   $��0q�6�   $��Italic$�6�   $
 0,0,0,2q�6�   ���	#FFF1ECEC$ 0.143,0.591&�6   $ 52��6$  ��6�   Z� 6$  �� 5M   
   -   5        $ txtTotalVentaProducto��5M      $��25��61   $ 74��6>   $��96��6N   $��Center=�6Y   $��Center=�6v   #���   6�   $��0q�6�   $��1q�6�   $��12(�6�   ���	#FFEEECDF���	#FFC4B8EA �	#FFC4B8EA��61  �� *TotalVentaProducto+$ PropertyChanged=� . �� 5N   
   -   5        $ cmdModificar��5N      $��	Modificar��6'   $��26��6;   $	 104��6G   $��62��6X   $ 0.495,1.442&�6d   #���  6�   $��0q�6�   $ 63��6�     5O        5P      ��5R      �� 5S      5U      �� 5V   
   -   5        $ cmdSalir��5V      $��Salir��6#   $��26��63   $	 306��6?   $��58��6P   $ 0.495,1.442&�6[   #���  6   $��0q�6�   $	 457��6�     5W        5X      ��5Z      �� 5[      5]      5^      5_      