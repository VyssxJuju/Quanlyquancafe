using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MySql.Data.MySqlClient;
using cafeha.Models;
using System.Linq;
using cafeha.Views;
using System.Text;
using System.Collections.ObjectModel;   

namespace cafeha
{
    public partial class MainWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;";
        private List<DrinkCategory> _drinkCategories = new List<DrinkCategory>();
        private List<Drink> _selectedDrinks = new List<Drink>();
        private string _userRole;

        public MainWindow(string role)
        {
            InitializeComponent();
            _userRole = role;
            LoadDrinkMenu();
            LoadSpecialItems();
            SetupUIForRole(_userRole);
        }

        // Tải danh sách đồ uống từ cơ sở dữ liệu
        private void LoadDrinkMenu()
        {
            var drinks = new ObservableCollection<DrinkCategory>();
            string query = "SELECT Id, Category, Name, Price, ImageUrl FROM CafeItems"; // Lấy tất cả đồ uống từ cơ sở dữ liệu

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var categories = new Dictionary<string, DrinkCategory>();

                            while (reader.Read())
                            {
                                int id = reader.GetInt32("Id");
                                string category = reader.GetString("Category");
                                string name = reader.GetString("Name");
                                decimal price = reader.GetDecimal("Price");
                                string imageUrl = reader.GetString("ImageUrl");

                                // Kiểm tra nếu danh mục đã tồn tại trong dictionary, nếu chưa thì tạo mới
                                if (!categories.ContainsKey(category))
                                {
                                    categories[category] = new DrinkCategory
                                    {
                                        Name = category,
                                        Items = new List<Drink>()
                                    };
                                }

                                // Thêm món đồ uống vào danh mục tương ứng
                                categories[category].Items.Add(new Drink
                                {
                                    ItemId = id,
                                    Name = name,
                                    Price = price,
                                    ImageUrl = imageUrl,
                                    Category = category
                                });
                            }

                            // Chuyển danh sách các danh mục đồ uống thành ObservableCollection
                            drinks = new ObservableCollection<DrinkCategory>(categories.Values);
                        }
                    }

                    // Hiển thị danh sách các danh mục đồ uống trong ItemsControl
                    DrinkList.ItemsSource = drinks;
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
            SpecialItemsList.Items.Clear();  // Xóa danh sách hiện tại

            // Truy vấn cơ sở dữ liệu để lấy các món bán chạy nhất
            string query = @"
        SELECT di.Name, di.Price, SUM(oi.Quantity) AS TotalSold
        FROM OrderItems oi
        JOIN CafeItems di ON oi.ItemId = di.Id
        GROUP BY di.Name, di.Price
        ORDER BY TotalSold DESC
        LIMIT 5";  // Lấy 5 món bán chạy nhất

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            var specialItems = new List<SpecialItem>();  // Danh sách các món đặc biệt

                            while (reader.Read())
                            {
                                string name = reader.GetString("Name");
                                decimal price = reader.GetDecimal("Price");

                                // Thêm món vào danh sách SpecialItemsList
                                specialItems.Add(new SpecialItem
                                {
                                    Name = name,
                                    Price = price
                                });
                            }

                            // Gán danh sách món đặc biệt vào ItemsControl
                            SpecialItemsList.ItemsSource = specialItems;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }



        // Thiết lập các quyền truy cập cho UI dựa trên vai trò người dùng
        private void SetupUIForRole(string role)
        {
            if (role == "admin")
            {
                MenuItem_AddStaff.Visibility = Visibility.Visible; // Hiển thị chức năng quản lý nhân viên
            }
            else if (role == "staff")
            {
                MenuItem_AddStaff.Visibility = Visibility.Collapsed; // Ẩn chức năng quản lý nhân viên
            }
        }

        // Sự kiện khi checkbox đồ uống được chọn hoặc bỏ chọn
        // Khi checkbox đồ uống được chọn
        private void DrinkCheckbox_Checked(object sender, RoutedEventArgs e)
        {
            var drink = (sender as CheckBox)?.Tag as Drink;
            if (drink != null)
            {
                // Thêm món vào danh sách đã chọn
                if (!SelectedDrinksList.Items.Contains(drink))
                {
                    SelectedDrinksList.Items.Add(drink);
                }
            }
        }

        // Khi checkbox đồ uống bị bỏ chọn
        private void DrinkCheckbox_Unchecked(object sender, RoutedEventArgs e)
        {
            var drink = (sender as CheckBox)?.Tag as Drink;
            if (drink != null)
            {
                // Xóa món khỏi danh sách đã chọn
                SelectedDrinksList.Items.Remove(drink);
            }
        }


        // Sự kiện khi nhấn nút "Tạo Đơn Hàng"
        private void CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedDrinks = SelectedDrinksList.Items.Cast<Drink>().ToList();

            // Kiểm tra nếu danh sách món chọn còn trống
            if (selectedDrinks.Count == 0)
            {
                MessageBox.Show("Vui lòng chọn ít nhất một món để tạo đơn hàng.");
                return;
            }

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();

                    // Bắt đầu một transaction để đảm bảo tính nhất quán dữ liệu
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Tạo đơn hàng mới trong bảng Orders
                            var createOrderQuery = "INSERT INTO Orders (TotalPrice, OrderDate) VALUES (@TotalPrice, NOW())";
                            using (var command = new MySqlCommand(createOrderQuery, connection, transaction))
                            {
                                decimal totalPrice = selectedDrinks.Sum(d => d.Price);  // Tổng giá trị đơn hàng

                                command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                                command.ExecuteNonQuery();

                                // Lấy ID của đơn hàng vừa tạo
                                int orderId = (int)command.LastInsertedId;

                                // Thêm các món đã chọn vào bảng OrderItems
                                foreach (var drink in selectedDrinks)
                                {
                                    if (drink.ItemId != 0)  // Kiểm tra nếu ItemId hợp lệ
                                    {
                                        var addOrderItemQuery = "INSERT INTO OrderItems (OrderId, ItemId, Quantity, TotalPrice, OrderDate) VALUES (@OrderId, @ItemId, @Quantity, @TotalPrice, NOW())";
                                        using (var itemCommand = new MySqlCommand(addOrderItemQuery, connection, transaction))
                                        {
                                            itemCommand.Parameters.AddWithValue("@OrderId", orderId);
                                            itemCommand.Parameters.AddWithValue("@ItemId", drink.ItemId);
                                            itemCommand.Parameters.AddWithValue("@Quantity", 1);  // Số lượng mặc định là 1
                                            itemCommand.Parameters.AddWithValue("@TotalPrice", drink.Price);  // Tính giá trị món

                                            itemCommand.ExecuteNonQuery();
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Món {drink.Name} không có mã món hợp lệ!");
                                    }
                                }

                                // Commit transaction nếu không có lỗi
                                transaction.Commit();
                            }

                            MessageBox.Show("Đơn hàng đã được tạo thành công!");
                        }
                        catch (Exception ex)
                        {
                            // Nếu có lỗi, rollback transaction
                            transaction.Rollback();
                            MessageBox.Show("Lỗi khi tạo đơn hàng: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }

            // Làm mới danh sách món đã chọn sau khi tạo đơn hàng
            SelectedDrinksList.Items.Clear();
        }




        // Các sự kiện cho menu bên trái (Các nút khác)a
        private void MenuItem_AddMenu_Click(object sender, RoutedEventArgs e)
        {
            DrinkManagementWindow drinkManagementWindow = new DrinkManagementWindow();
            drinkManagementWindow.Closed += DrinkManagementWindow_Closed;
            drinkManagementWindow.ShowDialog();
        }

        private void DrinkManagementWindow_Closed(object sender, EventArgs e)
        {
            // Gọi lại phương thức LoadDrinkMenu để làm mới danh sách đồ uống
            LoadDrinkMenu();
        }

        private void MenuItem_CreateOrder_Click(object sender, RoutedEventArgs e)
        {
            var orderWindow = new OrderWindow();
            orderWindow.ShowDialog();
        }

        private void MenuItem_AddRevenue_Click(object sender, RoutedEventArgs e)
        {
            RevenueWindow revenueWindow = new RevenueWindow();
            revenueWindow.ShowDialog();
        }

        private void MenuItem_AddStaff_Click(object sender, RoutedEventArgs e)
        {
            StaffManagementWindow staffManagementWindow = new StaffManagementWindow();
            staffManagementWindow.Show();  // Mở cửa sổ Quản lý Nhân viên
        }

        private void MenuItem_Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close(); // Đóng cửa sổ hiện tại
        }
    }
    public class SpecialItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

}
