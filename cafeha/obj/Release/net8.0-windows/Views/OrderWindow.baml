   M S B A M L   `   `   `  ���� 
  cafeha    cafeha.OrderWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Order Management��$	��600��$	��800��.���  � RowDefinitions �� $
��Auto ��� $��2* ��� $��3* �.+���  �� Orientation$ 
Horizontal=�$
��Left=�$��10q�$��0��.+��� -   $��Thêm Đơn��$	��100��$��5q��� -   $��Chỉnh Sửa��$	��100��$��5q��� -   $��
Xóa Đơn��$	��100��$��5q��� $��Danh Sách Đơn Hàng��$��10q�$��1��.��&   System.Windows.Controls.DataGrid  -   $��OrdersDataGrid��   AutoGenerateColumns$ False��$   HorizontalScrollBarVisibility$
 Auto=�"   VerticalScrollBarVisibility$
 Auto=�   
IsReadOnly$
 True��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Order ID��	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *	OrderId+  $ Tổng Giá��$		 150 
 �� *FormattedTotalPrice+  $ Ngày Đặt��$		 200 
 �� *	OrderDate+ �� StringFormat$ {0:dd/MM/yyyy}���� $��Chi Tiết Đơn Hàng��$��10q�$��2��.��  -   $��OrderItemsDataGrid��$ False��$
 Auto=�$
 Auto=�$
 True��   $ Tên Đồ Uống��$		 200 
 �� *	DrinkName+  $ Số Lượng��$		 100 
 �� *
Quantity+  $
 Giá��$		 100 
 �� *
DrinkPrice+$ {0:C}��