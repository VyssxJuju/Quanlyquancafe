using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using cafeha.Model;
using cafeha.Models;
using MySql.Data.MySqlClient;

namespace cafeha.Views
{
    public partial class AddOrderWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;";
        private List<OrderItem> _orderItems = new List<OrderItem>();
        private List<Drink> _allDrinks = new List<Drink>(); // Danh sách tất cả các đồ uống

        public AddOrderWindow()
        {
            InitializeComponent();
            LoadAllDrinks(); // Tải tất cả đồ uống khi mở cửa sổ
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
                        DrinkName = selectedDrink.Name, // Gán tên món từ đối tượng Drink
                        Quantity = 1,
                        DrinkPrice = selectedDrink.Price, // Gán đơn giá từ đối tượng Drink
                        TotalPrice = selectedDrink.Price // Tính thành tiền ban đầu
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
            }
        }

        // Lưu đơn hàng vào cơ sở dữ liệu
        private void SaveOrder_Click(object sender, RoutedEventArgs e)
        {
            if (_orderItems.Count == 0)
            {
                MessageBox.Show("Vui lòng thêm ít nhất một món vào đơn hàng.");
                return;
            }

            // Lưu đơn hàng vào bảng Orders
            string insertOrderQuery = "INSERT INTO Orders (TotalPrice, OrderDate) VALUES (@TotalPrice, @OrderDate)";
            decimal totalOrderPrice = _orderItems.Sum(item => item.TotalPrice);

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(insertOrderQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TotalPrice", totalOrderPrice);
                        command.Parameters.AddWithValue("@OrderDate", DateTime.Now);
                        command.ExecuteNonQuery();

                        // Lấy OrderId vừa thêm vào
                        int orderId = (int)command.LastInsertedId;

                        // Lưu các món trong đơn hàng vào bảng OrderItems
                        foreach (var item in _orderItems)
                        {
                            string insertOrderItemQuery = "INSERT INTO OrderItems (OrderId, ItemId, Quantity, TotalPrice) VALUES (@OrderId, @ItemId, @Quantity, @TotalPrice)";
                            using (var orderItemCommand = new MySqlCommand(insertOrderItemQuery, connection))
                            {
                                orderItemCommand.Parameters.AddWithValue("@OrderId", orderId);
                                orderItemCommand.Parameters.AddWithValue("@ItemId", item.ItemId);
                                orderItemCommand.Parameters.AddWithValue("@Quantity", item.Quantity);
                                orderItemCommand.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                                orderItemCommand.ExecuteNonQuery();
                            }
                        }

                        MessageBox.Show("Đơn hàng đã được lưu thành công!");
                        this.Close(); // Đóng cửa sổ sau khi lưu đơn hàng
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lưu đơn hàng: " + ex.Message);
                }
            }
        }

        // Hủy và đóng cửa sổ
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
