   M S B A M L   `   `   `  ����
  cafeha    cafeha.OrderWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Order Management��5   	   $	��600��6"   $	��800��6/   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��2* �6   �� 5	      $��3* �6   .+�5   
   ��  �� Orientation$ 
Horizontal=�6   $
��Left=�6.   $��10q�6I   $��0��6U   .+�5      �� -   5        $��Thêm Đơn��5      $	��100��6(   $��5q�64   6?   �� 5      -   5        $��Chỉnh Sửa��5      $	��100��6)   $��5q�65   6@   �� 5      -   5        $��
Xóa Đơn��5      $	��100��6'   $��5q�63   6>   5      �� 5   
   $��Danh Sách Đơn Hàng��6   $��10q�6/   $��1��6;   .��5      &   System.Windows.Controls.DataGrid  -   5        $��OrdersDataGrid��5         AutoGenerateColumns$ False��6-   $   HorizontalScrollBarVisibility$
 Auto=�5      "   VerticalScrollBarVisibility$
 Auto=�6<      
IsReadOnly$
 True��6_      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Order ID��6)   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 100 6W   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6;   �� *	OrderId+6W     5      $ Tổng Giá��6)   $		 150 6c   
 6;   �� *FormattedTotalPrice+6c     5      $ Ngày Đặt��6)   $		 200 6x   
 6;   �� *	OrderDate+ �� StringFormat$ {0:dd/MM/yyyy}��6x   5      5      �� 5    
   $��Chi Tiết Đơn Hàng��6   $��10q�6.   $��2��6:   .��5!        -   5        $��OrderItemsDataGrid��5!      $ False��61   $
 Auto=�6M   $
 Auto=�6r   $
 True��6�    5"        5#      $ Tên Đồ Uống��6)   $		 200 6\   
 6>   �� *	DrinkName+6\     5$      $ Số Lượng��6)   $		 100 6X   
 6;   �� *
Quantity+6X     5%      $
 Giá��6)   $		 100 6k   
 66   �� *
DrinkPrice+$ {0:C}��6k   5'      5(      5)      5*      