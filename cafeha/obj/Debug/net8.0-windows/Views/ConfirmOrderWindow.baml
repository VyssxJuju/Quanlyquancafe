   M S B A M L   `   `   `  ����
  cafeha%    cafeha.Views.ConfirmOrderWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Xác Nhận Đơn Hàng��5   	   $	��450��6#   $	��600��60   .��5      � $��10q�6   .+�5   
   �� $��Chi Tiết Đơn Hàng��6   $��20(�6-   $
��Bold"�6;   $��Center=�6M   $��	0,-4,0,-4q�6j   &   System.Windows.Controls.DataGrid  5	   
   -   5          Name$ OrderItemsDataGrid��5	         AutoGenerateColumns$ False��6/      CanUserAddRows$ False��6K      
IsReadOnly$
 True��6b   $��0,0,0,78q�6t   $	��309��6�   $��Bottom=�6�      Columns 5
      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Tên Nước��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6U   1  +System.Windows.Controls.DataGridBoundColumn   Binding 67   �� *	DrinkName+6U     5      $ Số Lượng��6%   $	 100 6T    67   �� *
Quantity+6T     5      $ Đơn Giá (VND)��6%   $	 150 6r    6<   �� *
DrinkPrice+	 �� StringFormat$	 {0:N0}��6r     5      $ Thành Tiền (VND)��6%   $	 150 6u    6?   �� *
TotalPrice+$	 {0:N0}��6u   5      �� 5   
   
 �� Orientation$
 
Horizontal=�6   $
��Left=�6.   $��
0,357,0,10q�6I   $	��242��6]   .+�5      �� $��Tổng Tiền: ��6   $
��Bold"�6+   $��16(�6=   $��82��6K   $��24��6V   �� 5      -   5         ��Name$ TotalPriceText��5      $��16(�60   $��Center=�6>   $	��5,0q�6Y   $	��135��6f   5      �� 5   
   $
 
Horizontal=�6   $��right=�6.   $��
0,320,0,20q�6J   $	��221��6^   .+�5      �� -   5        $��Xác Nhận��5      $	��100��6D   $��5q�6P   �� 5      -   5        $��Đóng��5      $	��100��6>   $��5q�6J   5      5      5      