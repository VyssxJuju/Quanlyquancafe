   M S B A M L   `   `   `  ����
  cafeha(    "cafeha.Views.StaffManagementWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Quản lý Nhân Viên��5   	   $	��500��6#   $	��800��60   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5
      $��* �6   �� 5      $
��Auto �6    � ColumnDefinitions 5   
   �� 5      $��* �6   .+�5   
   �� $��Quản lý nhân viên��6   $��20(�6-   $
��Bold"�6;   $��Center=�6M   $��Center=�6j   $��0��6�   $
��0,10q�6�   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ StaffDataGrid��5         AutoGenerateColumns$ False��6*   $
��0,10q�6F   $��1��6T      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Tên Nhân Viên��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 150 6]   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *EmployeeName+6]     5      $ Tài Khoản��6%   $	 150 6U    68   �� *
Username+6U     5      $ Mật Khẩu��6%   $	 150 6T    67   �� *
Password+6T     5      $ Mức Lương��6%   $	 150 6\    68   �� *FormattedSalary+6\     5      $ Vai Trò��6%   $	 100 6O    66   �� *Role+6O     5       $ Số Điện Thoại��6%   $	 150 6\    6<   �� *PhoneNumber+6\     5!      $ Địa Chỉ��6%   $	 200 6R    66   �� *	Address+6R     5"      $ 
Ngày Sinh��6%   $	 150 6a    68   �� *FormattedDateOfBirth+6a   5$      �� 5)   
   	 �� Orientation$	 
Horizontal=�6   $��Center=�6.   $��2��6K   $
��0,10q�6X   .+�5*      �� -   5        $��Thêm��5*      $��10q�6;   $	��100��6G   �� 5+      -   5        $��Sửa��5+      $��10q�6;   $	��100��6G   �� 5,      -   5        $
��Xóa��5,      $��10q�6=   $	��100��6I   �� 5-      -   5        $��Cấp Quyền��5-      $��0��6A   
 J� RenderTransformOrigin$
 0.5,0.5&�6K   z�5.      V� 5/      .~�50      ��  �� ScaleX$ -1Q�6)   �� 51      �� 52      T� 53      54      56      57      58      59      