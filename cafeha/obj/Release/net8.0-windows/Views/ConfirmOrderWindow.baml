   M S B A M L   `   `   `  ���� 
  cafeha%    cafeha.Views.ConfirmOrderWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Xác Nhận Đơn Hàng��$	��450��$	��600��.��� $��10q�.+��� $��Chi Tiết Đơn Hàng��$��20(�$
��Bold"�$��Center=�$��	0,-4,0,-4q�&   System.Windows.Controls.DataGrid  -     Name$ OrderItemsDataGrid��   AutoGenerateColumns$ False��   CanUserAddRows$ False��   
IsReadOnly$
 True��$��0,0,0,78q�$	��309��$��Bottom=�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Tên Nước��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	DrinkName+  $ Số Lượng��$	 100  �� *
Quantity+  $ Đơn Giá (VND)��$	 150  �� *
DrinkPrice+	 �� StringFormat$	 {0:N0}��  $ Thành Tiền (VND)��$	 150  �� *
TotalPrice+$	 {0:N0}���� 
 �� Orientation$
 
Horizontal=�$
��Left=�$��
0,357,0,10q�$	��242��.+��� $��Tổng Tiền: ��$
��Bold"�$��16(�$��82��$��24���� -    ��Name$ TotalPriceText��$��16(�$��Center=�$	��5,0q�$	��135���� $
 
Horizontal=�$��right=�$��
0,320,0,20q�$	��221��.+��� -   $��Xác Nhận��$	��100��$��5q��� -   $��Đóng��$	��100��$��5q�