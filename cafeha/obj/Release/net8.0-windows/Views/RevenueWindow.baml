   M S B A M L   `   `   `  ���� 
  cafeha     cafeha.Views.RevenueWindow   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Thống Kê Doanh Thu��$	��600��$	��800��.��� $��20q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* �.+���  �� Orientation$ Vertical=�$��0��$
��0,10q�.+��� $ ��Chọn ngày bắt đầu:��$��0,0,0,5q�(  "System.Windows.Controls.DatePicker  -     Name$ StartDatePicker��$��30��$��0,0,0,10q�   SelectedDate �� *	StartDate+�� $ Vertical=�$��1��$
��0,10q�.+��� $��Doanh thu hôm nay:��$��0,0,0,5q��� -    ��Name$ RevenueTodayTextBlock��$��16(�$
��Bold"�
��� � ��� $ Vertical=�$��3��$
��0,10q�.+��� $��Doanh thu tháng này:��$��0,0,0,5q��� -   $ RevenueMonthTextBlock��$��16(�$
��Bold"�
��� ���&   System.Windows.Controls.DataGrid  -     Name$ RevenueDataGrid��   AutoGenerateColumns$ False��$��4��$��0,10,0,0q�$	��200��   Columns 0  *System.Windows.Controls.DataGridTextColumn  ,  &System.Windows.Controls.DataGridColumn	   Header$	 Tháng/Năm��
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 200 1  +System.Windows.Controls.DataGridBoundColumn   Binding �� *
TimePeriod+  $	 Doanh Thu (VND)��$	
 200  �� *	Revenue+