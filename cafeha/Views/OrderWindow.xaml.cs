using System;
using System.Collections.Generic;
using System.Windows;
using cafeha.Views;
using MySql.Data.MySqlClient;

namespace cafeha
{
    public partial class OrderWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;";
        private List<Order> _orders = new List<Order>();

        public OrderWindow()
        {
            InitializeComponent();
            LoadOrders(); // Tải danh sách đơn hàng khi cửa sổ mở
        }

        // Lấy danh sách đơn hàng từ cơ sở dữ liệu và hiển thị lên DataGrid
        private void LoadOrders()
        {
            _orders.Clear();
            OrderDataGrid.Items.Clear();

            string query = "SELECT * FROM Orders";
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
                                OrderDataGrid.Items.Add(order);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }

        // Thêm đơn hàng mới
        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            var addOrderWindow = new AddOrderWindow();
            addOrderWindow.ShowDialog();

            // Sau khi thêm đơn hàng thành công, tải lại danh sách đơn hàng
            LoadOrders();
        }

        // Sửa đơn hàng
        private void EditOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (Order)OrderDataGrid.SelectedItem;

            if (selectedOrder != null)
            {
                var editOrderWindow = new EditOrderWindow(selectedOrder);
                editOrderWindow.ShowDialog();
                LoadOrders();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để sửa.");
            }
        }

        // Xóa đơn hàng
        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            var selectedOrder = (Order)OrderDataGrid.SelectedItem;

            if (selectedOrder != null)
            {
                string query = "DELETE FROM Orders WHERE OrderId = @OrderId";
                using (var connection = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@OrderId", selectedOrder.OrderId);
                            command.ExecuteNonQuery();
                        }
                        LoadOrders();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa đơn hàng: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xóa.");
            }
        }
    }
}
