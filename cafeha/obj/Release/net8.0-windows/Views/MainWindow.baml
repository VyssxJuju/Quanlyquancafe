   M S B A M L   `   `   `  ���� 
  cafeha    cafeha.MainWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Quản Lý Quán Cafe��$	��650��$
��1000��.��]� .+��� $	��Top=� �� Orientation$ 
Horizontal=�
�������$��80��.+��� $��images/logo.png��$��60��$��60��$��10q��� $'��!Café Hà - Quản Lý Quán Cafe��$��26(�$
��Bold"�$��Center=�$��10,0,0,0q��� $
��Left=�$	��200��
�������.+��� -    ��Name$ MenuItem_AddMenu��$��Quản lý đồ uống��$��50���� -   $ MenuItem_CreateOrder��$��Các Đơn Hàng��$��50���� -   $ MenuItem_AddRevenue��$��Ghi Nhận Doanh Thu��$��50���� -   $ MenuItem_AddStaff��$��Quản Lý Nhân Viên��$��50���� -   $ MenuItem_Logout��$��Đăng Xuất��$��35��$��0,270,100,0q�� $��10q�$	��550��.+��� $��Danh sách đồ uống��$��20(�$��Center=�$	��Top=��� $
��Auto=�$
��Auto=�$��0,32,0,0q�.���� -    ��Name$ 	DrinkList��$��10,10,10,10q�$	��522������ .R��� ���� *Name+.���� ���� *Items+���� .R��� $ 
Horizontal=�$��5q�.+��� -   
 � Tag �� �� $��50��$��50��$��5q����� *
ImageUrl+�� $��5q�$��Center=����� *Name+�� $��5q�$��Center=����� *Price+ �� StringFormat$ 
{0:N0} VND���� -    ��Name$ SelectedDrinksList��$��591,457,-134,2q����� .R��� ���� *Name+�� -	   $��Tạo Đơn Hàng��$��591,457,-134,2q��� $��Bottom=�$��10, 20, 10, 10q�.+��� $��Các Món Đặc Biệt��$��20(�$
��Bold"�$��0,0,0,10q��� -
   $ SpecialItemsList��$��Stretch=�$	��Top=�$	��200������ .R��� .+��� $
��Bold"�$��16(����� *Name+�� $��14(�
����������� *Price+$ {0:0.0} VND��