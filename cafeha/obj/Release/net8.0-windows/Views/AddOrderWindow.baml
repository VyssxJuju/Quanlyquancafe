   M S B A M L   `   `   `  ���� 
  cafeha!    cafeha.Views.AddOrderWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Thêm Đơn Hàng��$	��600��$
��1000��.��� $��10q� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto �.+��� $��Thêm Đơn Hàng��$��20(�$
��Bold"�$��Center=�$��0,0,0,10q�� $��1��$��	0,10,0,10q� � ColumnDefinitions �� $��3* ��� $��2* �.+��� $��Chi Tiết Đơn Hàng��$��0,0,10,0q�.��&   System.Windows.Controls.DataGrid  -     Name$ OrderItemsDataGrid��   AutoGenerateColumns$ False��   CanUserAddRows$ False��   SelectionMode$ Single=�   
IsReadOnly$
 True��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Tên Đồ uống��
   
IsReadOnly$

 True��   Width5  /System.Windows.Controls.DataGridLengthConverter$ * 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *	DrinkName+  $	 Số Lượng��$	 100  �� *
Quantity+  $	 Đơn Giá (VND)��$	 150 $

 True�� �� *
DrinkPrice+ �� StringFormat$ {0:N0}��  $	 Thành Tiền (VND)��$	 150 $

 True�� �� *
TotalPrice+$ {0:N0}���� $��Danh Sách Đồ Uống��$��1��.���� -    ��Name$ AllDrinksListView�� �� SelectionMode$ Single=� �� View �� .P��� $��Tên Đồ Uống�� �� Width$	 150�� �� DisplayMemberBinding �� *Name+�� $��
Giá (VND)��$	 100�� �� *Price+$ {0:N0}���� $��
Danh Mục��$	 150�� �� *
Category+�� $��2�� �� Orientation$ 
Horizontal=�$��Center=�$��0,10,0,0q�.+��� -   $��Thêm Đồ Uống��$��5q�$	��100���� -   $��Xóa Đồ Uống��$��5q�$	��100���� -   $
��Lưu��$��5q�$	��100���� -   $��Hủy��$��5q�$	��100��