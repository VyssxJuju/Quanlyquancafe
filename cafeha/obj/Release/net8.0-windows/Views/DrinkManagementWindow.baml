   M S B A M L   `   `   `  ���� 
  cafeha"    cafeha.DrinkManagementWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Quản Lý Đồ Uống��$	��450��$	��700��.��� $��20q�.+��� $��Quản lý đồ uống��$��24(�$
��Bold"�$��Center=�$	��Top=�$��0,-1,0,0q�&   System.Windows.Controls.DataGrid  -     Name$ DrinkDataGrid��$��
0,36,0,100q�   AutoGenerateColumns$ False��   CanUserAddRows$ False��   CanUserDeleteRows$ False��   SelectionMode$ Single=�   AlternatingRowBackground �#f5f5f5���#fff
���   �$��1q�   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Tên Đồ Uống��	   Width5  /System.Windows.Controls.DataGridLengthConverter$		 250 1  +System.Windows.Controls.DataGridBoundColumn
   Binding
 �� *Name+  $
 Giá��$		 120 
 �� *FormattedPrice+  $ 
Danh Mục��$		 150 
 �� *
Category+4  .System.Windows.Controls.DataGridTemplateColumn  $ Ảnh��   CellTemplate �� .R��� $��50��$��50��$��Center=�$��Center=����� *
ImageUrl+��  �� Orientation$ 
Horizontal=�$��Center=�$��Bottom=�$��0,0,0,20q�.+��� -   $��Thêm Đồ Uống��$	��150��$��40��$��0,0,10,0q��� -   $��Sửa Đồ Uống��$	��150��$��40��$��0,0,10,0q��� -   $��Xóa Đồ Uống��$	��150��$��40��