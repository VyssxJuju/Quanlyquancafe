using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using cafeha.Model;
using cafeha.Models;
using MySql.Data.MySqlClient;

namespace cafeha.Views
{
    public partial class EditOrderWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;";
        private Order _order;
        private List<OrderItem> _orderItems = new List<OrderItem>();
        private List<Drink> _allDrinks = new List<Drink>(); // Danh sách tất cả các đồ uống

        public EditOrderWindow(int orderId)
        {
            InitializeComponent();
            LoadOrder(orderId); // Tải thông tin đơn hàng khi mở cửa sổ
            LoadAllDrinks(); // Tải tất cả đồ uống để hiển thị bên phải
        }

        // Lấy thông tin đơn hàng từ cơ sở dữ liệu
        private void LoadOrder(int orderId)
        {
            string orderQuery = "SELECT o.OrderId, o.TotalPrice, o.OrderDate " +
                                 "FROM Orders o WHERE o.OrderId = @OrderId";
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(orderQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _order = new Order
                                {
                                    OrderId = reader.GetInt32("OrderId"),
                                    TotalPrice = reader.GetDecimal("TotalPrice"),
                                    OrderDate = reader.GetDateTime("OrderDate")
                                };
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }

            string orderItemsQuery = "SELECT oi.Quantity, ci.Name AS DrinkName, ci.Price AS DrinkPrice, ci.Id AS ItemId " +
                                     "FROM OrderItems oi " +
                                     "JOIN CafeItems ci ON oi.ItemId = ci.Id " +
                                     "WHERE oi.OrderId = @OrderId";
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(orderItemsQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var orderItem = new OrderItem
                                {
                                    ItemId = reader.GetInt32("ItemId"),
                                    DrinkName = reader.GetString("DrinkName"),
                                    Quantity = reader.GetInt32("Quantity"),
                                    DrinkPrice = reader.GetDecimal("DrinkPrice"),
                                    TotalPrice = reader.GetInt32("Quantity") * reader.GetDecimal("DrinkPrice")
                                };
                                _orderItems.Add(orderItem);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }

            OrderItemsDataGrid.ItemsSource = _orderItems; // Hiển thị các món trong DataGrid
        }

        // Tải tất cả đồ uống từ cơ sở dữ liệu
        private void LoadAllDrinks()
        {
            string query = "SELECT * FROM CafeItems"; // Giả sử tên bảng đồ uống là CafeItems
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
                                _allDrinks.Add(new Drink
                                {
                                    Id = reader.GetInt32("Id"),
                                    Name = reader.GetString("Name"),
                                    Price = reader.GetDecimal("Price"),
                                    ImageUrl = reader.IsDBNull(reader.GetOrdinal("ImageUrl")) ? null : reader.GetString("ImageUrl"),
                                    Category = reader.IsDBNull(reader.GetOrdinal("Category")) ? null : reader.GetString("Category")
                                });
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }

            AllDrinksListView.ItemsSource = _allDrinks; // Hiển thị danh sách nước uống bên phải
        }

        // Thêm món vào đơn hàng
        private void AddDrink_Click(object sender, RoutedEventArgs e)
        {
            var selectedDrink = (Drink)AllDrinksListView.SelectedItem;
            if (selectedDrink != null)
            {
                // Kiểm tra ItemId hợp lệ trước khi thêm vào OrderItems
                if (selectedDrink.Id == 0)
                {
                    MessageBox.Show("Món không hợp lệ (ID không tồn tại)!");
                    return; // Không thêm món vào nếu ItemId không hợp lệ
                }

                var existingItem = _orderItems.FirstOrDefault(item => item.ItemId == selectedDrink.Id);
                if (existingItem != null)
                {
                    existingItem.Quantity++; // Nếu món đã có, tăng số lượng
                    existingItem.TotalPrice = existingItem.Quantity * existingItem.DrinkPrice; // Cập nhật lại tổng tiền
                }
                else
                {
                    // Nếu món chưa có trong đơn, thêm mới vào đơn
                    var newOrderItem = new OrderItem
                    {
                        ItemId = selectedDrink.Id, // Gán ItemId
                        DrinkName = selectedDrink.Name,
                        Quantity = 1,
                        DrinkPrice = selectedDrink.Price,
                        TotalPrice = selectedDrink.Price
                    };
                    _orderItems.Add(newOrderItem);
                }

                // Cập nhật lại DataGrid hiển thị các món trong đơn
                OrderItemsDataGrid.ItemsSource = null;
                OrderItemsDataGrid.ItemsSource = _orderItems;
            }
        }

        // Xóa món khỏi đơn hàng
        private void RemoveDrink_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (OrderItem)OrderItemsDataGrid.SelectedItem;
            if (selectedItem != null)
            {
                if (selectedItem.Quantity > 1)
                {
                    // Nếu số lượng món đồ uống lớn hơn 1, chỉ giảm số lượng đi 1
                    selectedItem.Quantity--;

                    // Cập nhật lại tổng tiền cho món
                    selectedItem.TotalPrice = selectedItem.Quantity * selectedItem.DrinkPrice;

                    // Cập nhật lại DataGrid
                    OrderItemsDataGrid.ItemsSource = null;
                    OrderItemsDataGrid.ItemsSource = _orderItems;
                }
                else
                {
                    // Nếu số lượng món là 1, xóa món khỏi danh sách
                    _orderItems.Remove(selectedItem);
                }

                // Tính lại tổng tiền đơn hàng
                decimal totalPrice = 0;
                foreach (var item in _orderItems)
                {
                    totalPrice += item.TotalPrice;
                }
                _order.TotalPrice = totalPrice;
            }
        }



        // Lưu đơn hàng
        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            decimal totalPrice = 0;
            foreach (var item in _orderItems)
            {
                totalPrice += item.TotalPrice; // Tính lại tổng tiền
            }
            _order.TotalPrice = totalPrice;

            // Mở cửa sổ xác nhận đơn hàng
            ConfirmOrderWindow confirmOrderWindow = new ConfirmOrderWindow(_connectionString, _order, _orderItems);
            confirmOrderWindow.ShowDialog();
        }

        // Hủy và đóng cửa sổ
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
