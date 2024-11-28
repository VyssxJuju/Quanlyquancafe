using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;

namespace cafeha
{
    public partial class MainWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;"; // Kết nối với MySQL
        private List<DrinkCategory> _drinkCategories = new List<DrinkCategory>();
        private List<Drink> _selectedDrinks = new List<Drink>();
        private string _userRole;

        public MainWindow(string role)
        {
            InitializeComponent();
            _userRole = role;  // Lưu vai trò người dùng
            LoadDrinkMenu();
            LoadSpecialItems();
            SetupUIForRole(_userRole);  // Thiết lập quyền truy cập dựa trên vai trò
        }

        // Tải danh sách đồ uống từ cơ sở dữ liệu
        private void LoadDrinkMenu()
        {
            _drinkCategories.Clear();
            string query = "SELECT Category, Name, Price, ImageUrl FROM CafeItems";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string category = reader.GetString("Category");
                                string name = reader.GetString("Name");
                                decimal price = reader.GetDecimal("Price");
                                string imageUrl = reader.GetString("ImageUrl");

                                // Thêm món vào danh mục
                                var categoryItem = _drinkCategories.Find(c => c.Name == category);
                                if (categoryItem == null)
                                {
                                    categoryItem = new DrinkCategory { Name = category, Items = new List<Drink>() };
                                    _drinkCategories.Add(categoryItem);
                                }
                                categoryItem.Items.Add(new Drink { Name = name, Price = price, ImageUrl = imageUrl });
                            }
                        }
                    }

                    DrinkList.ItemsSource = _drinkCategories;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }



        // Tải các món đặc biệt
        private void LoadSpecialItems()
        {
            // Ví dụ các món đặc biệt, bạn có thể thêm vào cơ sở dữ liệu hoặc các yêu cầu khác
            SpecialItemsList.Items.Add("Cafe Sữa Đá - 25k");
            SpecialItemsList.Items.Add("Trà Sữa Trân Châu - 30k");
            SpecialItemsList.Items.Add("Sinh Tố Xoài - 35k");
        }

        // Thiết lập các quyền truy cập cho UI dựa trên vai trò người dùng
        private void SetupUIForRole(string role)
        {
            if (role == "admin")
            {
                // Admin có thể nhìn thấy tất cả các chức năng
                MenuItem_AddStaff.Visibility = Visibility.Visible; // Hiển thị chức năng quản lý nhân viên
            }
            else if (role == "staff")
            {
                // Nhân viên không thể thấy chức năng quản lý nhân viên
                MenuItem_AddStaff.Visibility = Visibility.Collapsed; // Ẩn chức năng quản lý nhân viên
            }
        }

        // Sự kiện khi checkbox đồ uống được chọn hoặc bỏ chọn
        private void DrinkCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            var drink = (Drink)((CheckBox)sender).Tag;
            _selectedDrinks.Add(drink);
            SelectedDrinksList.Items.Add(drink.Name);
        }

        private void DrinkCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            var drink = (Drink)((CheckBox)sender).Tag;
            _selectedDrinks.Remove(drink);
            SelectedDrinksList.Items.Remove(drink.Name);
        }

        // Sự kiện khi nhấn nút "Tạo Đơn Hàng"
        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedDrinks.Count > 0)
            {
                string orderDetails = "Đơn hàng gồm:\n";
                decimal totalPrice = 0;

                foreach (var drink in _selectedDrinks)
                {
                    orderDetails += $"{drink.Name} - {drink.Price:N0} VND\n";
                    totalPrice += drink.Price;
                }

                orderDetails += $"\nTổng tiền: {totalPrice:N0} VND";

                MessageBox.Show(orderDetails, "Đơn Hàng Mới");
                _selectedDrinks.Clear();
                SelectedDrinksList.Items.Clear();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ít nhất một món để tạo đơn hàng.");
            }
        }

        // Các sự kiện cho menu bên trái (Các nút khác)
        private void MenuItem_AddMenu_Click(object sender, RoutedEventArgs e)
        {
            DrinkManagementWindow drinkManagementWindow = new DrinkManagementWindow();
            drinkManagementWindow.ShowDialog();
        }

        private void MenuItem_CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderWindow = new OrderWindow();
            orderWindow.ShowDialog();
        }

        private void MenuItem_AddRevenue_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vui lòng chọn ít nhất một món để tạo đơn hàng.");
        }

        private void MenuItem_AddStaff_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Vui lòng chọn ít nhất một món để tạo đơn hàng.");
        }

        private void MenuItem_Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close(); // Đóng cửa sổ hiện tại
        }
    }

    // Các lớp đại diện cho đồ uống và danh mục đồ uống
    public class Drink
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }
        public string Category { get; set; }
    }

    public class DrinkCategory
    {
        public string Name { get; set; }
        public List<Drink> Items { get; set; }
    }

    public class Order
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }

}
