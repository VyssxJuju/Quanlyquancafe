   M S B A M L   `   `   `  ����
  cafeha     cafeha.Views.RevenueWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Thống Kê Doanh Thu��5   	   $	��500��6$   $	��800��61   .��5      � $��20q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5	      $
��Auto �6   �� 5
      $
��Auto �6   �� 5      $��* �6   .+�5   
   ��  �� Orientation$ Vertical=�6   $��0��6,   $
��0,10q�69   .+�5      �� $ ��Chọn ngày bắt đầu:��6   $��0,0,0,5q�62   (  "System.Windows.Controls.DatePicker  5      -   5          Name$ StartDatePicker��5      $��30��6U   $��0,0,0,10q�6a      SelectedDate 62   �� *	StartDate+6a   5      �� 5   
   $ Vertical=�6   $��1��6,   $
��0,10q�69   .+�5      �� $��Doanh thu hôm nay:��6   $��0,0,0,5q�62   �� 5      -   5         ��Name$ RevenueTodayTextBlock��5      $��16(�67   $
��Bold"�6E   
��� � �6W   5      �� 5   
   $ Vertical=�6   $��2��6,   $
��0,10q�69   .+�5      �� $��Doanh thu tháng này:��6   $��0,0,0,5q�64   �� 5      -   5        $ RevenueMonthTextBlock��5      $��16(�67   $
��Bold"�6E   
��� ���6W   5      �� 5!   
   $ Vertical=�6   $��3��6,   $
��0,10q�69   .+�5"      �� $%��Doanh thu các tháng trước:��6   $��0,0,0,5q�6:   &   System.Windows.Controls.DataGrid  5#      -   5          Name$# PreviousMonthsRevenueDataGrid��5#         AutoGenerateColumns$ False��6>   $	��200��6Z      Columns 5$      0  *System.Windows.Controls.DataGridTextColumn  5%      ,  &System.Windows.Controls.DataGridColumn	   Header$	 Tháng/Năm��6)   
   Width5  /System.Windows.Controls.DataGridLengthConverter$	
 200 6[   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6<   �� *
TimePeriod+6[     5&      $	 Doanh Thu (VND)��6)   $	
 200 6w    6B   �� *	Revenue+ �� StringFormat$ {0:N0}��6w   5(      5)      5,      5-      