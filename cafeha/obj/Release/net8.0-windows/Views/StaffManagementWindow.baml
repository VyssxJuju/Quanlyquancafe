   M S B A M L   `   `   `  ���� 
  cafeha(    "cafeha.Views.StaffManagementWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Quản lý Nhân Viên��$	��500��$	��800��.��� $��20q� � RowDefinitions �� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $��* �.+��� $��Quản lý nhân viên��$��20(�$
��Bold"�$��Center=�$��Center=�$��0��$
��0,10q�&   System.Windows.Controls.DataGrid  -     Name$ StaffDataGrid��   AutoGenerateColumns$ False��$
��0,10q�$��1��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn   Header$ Tên Nhân Viên��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *EmployeeName+  $ Tài Khoản��$	 150  �� *
Username+  $ Mật Khẩu��$	 150  �� *
Password+  $ Mức Lương��$	 150  �� *FormattedSalary+  $ Vai Trò��$	 100  �� *Role+  $ Số Điện Thoại��$	 150  �� *PhoneNumber+  $ Địa Chỉ��$	 200  �� *	Address+  $ 
Ngày Sinh��$	 150  �� *FormattedDateOfBirth+�� 	 �� Orientation$	 
Horizontal=�$��Center=�$��2��$
��0,10q�.+��� -   $��Thêm��$��10q�$	��100���� -   $��Sửa��$��10q�$	��100���� -   $
��Xóa��$��10q�$	��100���� -   $��Cấp Quyền��$��0��
 J� RenderTransformOrigin$
 0.5,0.5&�z�V� .~���  �� ScaleX$ -1Q��� �� T� 