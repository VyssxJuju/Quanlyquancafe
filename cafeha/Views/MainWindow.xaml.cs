using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using cafeha.Controller;
using cafeha.Views;
using cafeha.Model;

namespace cafeha
{
    public partial class MainWindow : Window
    {
        private FirebaseService _firebaseService;
        private string _userRole;
        private string _userToken; // Thêm biến token
        private string _username;
        private List<CafeItem> selectedDrinks = new List<CafeItem>();

        public MainWindow(string role, string username, string token) // Thêm tham số token
        {
            InitializeComponent();
            _firebaseService = new FirebaseService();
            _username = username;
            _userToken = token; // Lưu token vào biến
            _userRole = role;

            // Kiểm tra quyền và token
            if (string.IsNullOrEmpty(_username) || string.IsNullOrEmpty(_userRole))
            {
                MessageBox.Show("Bạn không có quyền truy cập. Vui lòng đăng nhập lại.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                Application.Current.Shutdown(); // Đóng ứng dụng nếu không có quyền
            }

            LoadCafeItems(); 
            CheckUserRole();
        }

        private void MenuItem_Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }

        private async void LoadCafeItems()
        {
            try
            {
                var cafeItems = await _firebaseService.GetCafeItemsAsync();

                // Phân nhóm các đồ uống theo loại
                var drinksByCategory = cafeItems.GroupBy(item => item.Category)
                                                 .Select(g => new
                                                 {
                                                     Name = g.Key,
                                                     Items = g.ToList()
                                                 });

                // Thiết lập ItemsSource cho ItemsControl
                DrinkList.ItemsSource = drinksByCategory.ToList();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi tải danh sách đồ uống: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CheckUserRole()
        {
            switch (_userRole)
            {
                case "manager":
                    break;

                case "staff":
                    MenuItem_AddMenu.Visibility = Visibility.Hidden;
                    MenuItem_AddStaff.Visibility = Visibility.Hidden;
                    break;

                case "none":
                    MenuItem_AddMenu.Visibility = Visibility.Hidden;
                    MenuItem_CreateOrder.Visibility = Visibility.Hidden;
                    MenuItem_AddRevenue.Visibility = Visibility.Hidden;
                    MenuItem_AddStaff.Visibility = Visibility.Hidden;
                    MenuItem_AddInventory.Visibility = Visibility.Hidden;
                    break;

                default:
                    MenuItem_AddMenu.Visibility = Visibility.Hidden;
                    MenuItem_CreateOrder.Visibility = Visibility.Hidden;
                    MenuItem_AddRevenue.Visibility = Visibility.Hidden;
                    MenuItem_AddStaff.Visibility = Visibility.Hidden;
                    MenuItem_AddInventory.Visibility = Visibility.Hidden;
                    break;
            }
        }

        private void MenuItem_AddMenu_Click(object sender, RoutedEventArgs e)
        {
            var addMenuWindow = new AddMenuWindow(_userToken); // Truyền token vào
            addMenuWindow.ShowDialog();
            LoadCafeItems();
        }

        private void MenuItem_AddRevenue_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Ghi Nhận Doanh Thu được nhấn");
        }

        private void MenuItem_AddStaff_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thêm Nhân Viên được nhấn");
        }

        private void MenuItem_AddInventory_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Thêm Nguyên Liệu được nhấn");
        }

        private void DrinkCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            CafeItem selectedDrink = checkBox.Tag as CafeItem;
            if (selectedDrink != null && !selectedDrinks.Contains(selectedDrink))
            {
                selectedDrinks.Add(selectedDrink);
                SelectedDrinksList.Items.Add(selectedDrink);
            }
        }

        private void DrinkCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            CafeItem selectedDrink = checkBox.Tag as CafeItem;
            if (selectedDrink != null)
            {
                selectedDrinks.Remove(selectedDrink);
                SelectedDrinksList.Items.Remove(selectedDrink);
            }
        }

        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            OrderWindow orderWindow = new OrderWindow(selectedDrinks);
            orderWindow.ShowDialog();
        }

        private void MenuItem_CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            CreateOrder_Click(sender, e);
        }
    }
}
