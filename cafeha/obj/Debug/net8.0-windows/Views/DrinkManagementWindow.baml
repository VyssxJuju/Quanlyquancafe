   M S B A M L   `   `   `  ����
  cafeha"    cafeha.DrinkManagementWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Quản Lý Đồ Uống��5   	   $	��450��6!   $	��700��6.   .��5      � $��20q�6   .+�5   
   �� $��Quản lý đồ uống��6   $��24(�6+   $
��Bold"�69   $��Center=�6K   $	��Top=�6h   $��0,-1,0,0q�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ DrinkDataGrid��5      $��
0,36,0,100q�6*      AutoGenerateColumns$ False��6>      CanUserAddRows$ False��6Z      CanUserDeleteRows$ False��6q      SelectionMode$ Single=�6�      AlternatingRowBackground �#f5f5f5���#fff
���   �$��1q�5   \      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Tên Đồ Uống��6%   	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 250 6S   1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 6:   �� *Name+6S     5      $
 Giá��6%   $		 120 6U   
 62   �� *FormattedPrice+6U     5      $ 
Danh Mục��6%   $		 150 6T   
 67   �� *
Category+6T   4  .System.Windows.Controls.DataGridTemplateColumn  5      $ Ảnh��6)      CellTemplate 5      �� 5      .R�5      �� $��50��6@   $��50��6K   $��Center=�6W   $��Center=�6t   ��6$   �� *
ImageUrl+6t   5      5      5      �� 5   
    �� Orientation$ 
Horizontal=�6   $��Center=�6.   $��Bottom=�6K   $��0,0,0,20q�6f   .+�5      �� -   5        $��Thêm Đồ Uống��5      $	��150��6,   $��40��68   $��0,0,10,0q�6D   6V   �� 5       -   5        $��Sửa Đồ Uống��5       $	��150��6+   $��40��67   $��0,0,10,0q�6C   6U   �� 5!      -   5        $��Xóa Đồ Uống��5!      $	��150��6+   $��40��67   6C   5"      5$      5%      