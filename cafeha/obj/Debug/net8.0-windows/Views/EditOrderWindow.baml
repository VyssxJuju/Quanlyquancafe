   M S B A M L   `   `   `  ����
  cafeha"    cafeha.Views.EditOrderWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Sửa Đơn Hàng��5   	   $	��600��6   $
��1000��6+   .��5      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5	      $
��Auto �6   .+�5   
   �� $��Sửa Đơn Hàng��6   $��20(�6(   $
��Bold"�66   $��Center=�6H   $��0,0,0,10q�6e   � 5   
   $��1��6   $��	0,10,0,10q�6    � ColumnDefinitions 5      �� 5      $��3* �6#   �� 5      $��2* �6#   .+�5      �� $��Chi Tiết Đơn Hàng��6   $��0,0,10,0q�62   .��5      &   System.Windows.Controls.DataGrid  -   5          Name$ OrderItemsDataGrid��5         AutoGenerateColumns$ False��67      CanUserAddRows$ False��6S      SelectionMode$ Single=�5         
IsReadOnly$ False��62      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Tên Đồ uống��6-   
   
IsReadOnly$

 True��6`      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6r   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6B   �� *	DrinkName+6r     5      $	 Số Lượng��6-   $	 100 6\    6?   �� *
Quantity+6\     5      $	 Đơn Giá (VND)��6-   $	 150 6z   $

 True��6�    6D   �� *
DrinkPrice+ �� StringFormat$ {0:N0}��6�     5      $	 Thành Tiền (VND)��6-   $	 150 6}   $

 True��6�    6G   �� *
TotalPrice+$ {0:N0}��6�   5       5!      �� 5$      $��Danh Sách Nước Uống��6   $��1��64   .��5%      �� -   5         ��Name$ AllDrinksListView��5%       �� SelectionMode$ Single=�66    �� View 5&      �� 5'      .P�5(      �� $��Tên Đồ Uống��6-    �� Width$	 150��6h    �� DisplayMemberBinding 6B   �� *Name+6h   �� 5)      $��
Giá (VND)��6-   $	 100��6~    6@   �� *Price+$ {0:N0}��6~   5*      5,      5-      5.      �� 51   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Center=�6;   $��0,10,0,0q�6X   .+�52      �� -   5        $��Thêm Đồ Uống��52      $��5q�6C   $	��100��6N   �� 53      -   5        $��Xóa Đồ Uống��53      $��5q�6E   $	��100��6P   �� 54      -   5        $
��Lưu��54      $��5q�6;   $	��100��6F   �� 55      -   5        $��Hủy��55      $��5q�68   $	��100��6C   56      57      58      