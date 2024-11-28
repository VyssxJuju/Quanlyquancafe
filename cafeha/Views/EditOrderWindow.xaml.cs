using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;

namespace cafeha.Views
{
    public partial class EditOrderWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;";
        private List<Order> _orders = new List<Order>(); // Danh sách đơn hàng
        private List<Drink> _drinks = new List<Drink>(); // Danh sách đồ uống
        private Order _selectedOrder; // Đơn hàng đang được chỉnh sửa

        public EditOrderWindow()
        {
            InitializeComponent();
            LoadOrders(); // Tải danh sách đơn hàng khi cửa sổ mở
            LoadDrinks(); // Tải danh sách đồ uống vào ComboBox
        }

        // Lấy danh sách đơn hàng từ cơ sở dữ liệu và hiển thị trong ComboBox
        private void LoadOrders()
        {
            string query = "SELECT * FROM Orders"; // Lấy tất cả đơn hàng từ bảng Orders
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
                                var order = new Order
                                {
                                    OrderId = reader.GetInt32("OrderId"),
                                    ItemId = reader.GetInt32("ItemId"),
                                    Quantity = reader.GetInt32("Quantity"),
                                    TotalPrice = reader.GetDecimal("TotalPrice"),
                                    OrderDate = reader.GetDateTime("OrderDate")
                                };

                                _orders.Add(order);
                                OrderComboBox.Items.Add(order); // Thêm đơn hàng vào ComboBox
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu đơn hàng: " + ex.Message);
                }
            }
        }

        // Lấy danh sách đồ uống để hiển thị trong ComboBox
        private void LoadDrinks()
        {
            string query = "SELECT * FROM CafeItems"; // Lấy tất cả đồ uống từ bảng CafeItems
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
                                var drink = new Drink
                                {
                                    ItemId = reader.GetInt32("ItemId"),
                                    Name = reader.GetString("Name"),
                                    Price = reader.GetDecimal("Price"),
                                    ImageUrl = reader.GetString("ImageUrl")
                                };

                                _drinks.Add(drink);
                                DrinkComboBox.Items.Add(drink); // Thêm đồ uống vào ComboBox
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi tải dữ liệu đồ uống: " + ex.Message);
                }
            }
        }

        // Sự kiện khi chọn đơn hàng từ ComboBox
        private void OrderComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (OrderComboBox.SelectedItem != null)
            {
                _selectedOrder = (Order)OrderComboBox.SelectedItem;
                QuantityTextBox.Text = _selectedOrder.Quantity.ToString();
                TotalPriceTextBox.Text = _selectedOrder.TotalPrice.ToString("F2");

                // Chọn đồ uống liên quan đến đơn hàng
                DrinkComboBox.SelectedItem = _drinks.Find(d => d.ItemId == _selectedOrder.ItemId);
            }
        }

        // Sự kiện khi nhấn nút "Lưu"
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (OrderComboBox.SelectedItem == null || string.IsNullOrEmpty(QuantityTextBox.Text) || DrinkComboBox.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đơn hàng, chọn đồ uống và nhập số lượng.");
                return;
            }

            try
            {
                var selectedOrder = _selectedOrder;
                int orderId = selectedOrder.OrderId;
                int quantity = int.Parse(QuantityTextBox.Text);
                decimal totalPrice = selectedOrder.TotalPrice / selectedOrder.Quantity * quantity; // Tính lại tổng tiền
                int itemId = ((Drink)DrinkComboBox.SelectedItem).ItemId; // Lấy ID món đồ uống đã chọn

                // Cập nhật đơn hàng vào cơ sở dữ liệu
                string query = "UPDATE Orders SET Quantity = @Quantity, TotalPrice = @TotalPrice, ItemId = @ItemId WHERE OrderId = @OrderId";
                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", orderId);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        command.Parameters.AddWithValue("@ItemId", itemId);

                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đơn hàng đã được cập nhật thành công.");
                this.Close(); // Đóng cửa sổ sau khi lưu thành công
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật đơn hàng: " + ex.Message);
            }
        }
    }

    // Lớp Order để chứa thông tin đơn hàng
    public class Order
    {
        public int OrderId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

        public override string ToString()
        {
            return $"Đơn hàng #{OrderId} - {Quantity} x {TotalPrice:F2}";
        }
    }

    // Lớp Drink để chứa thông tin đồ uống
    public class Drink
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string ImageUrl { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
