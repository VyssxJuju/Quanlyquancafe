using System;
using System.Collections.Generic;
using System.Windows;
using cafeha.Views;
using cafeha.Model;
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
            OrdersDataGrid.Items.Clear();

            string query = "SELECT o.OrderId, o.TotalPrice, o.OrderDate " +
                           "FROM Orders o";

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
                                    TotalPrice = reader.GetDecimal("TotalPrice"),
                                    OrderDate = reader.GetDateTime("OrderDate")
                                };
                                _orders.Add(order);
                                OrdersDataGrid.Items.Add(order);
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
            var selectedOrder = (Order)OrdersDataGrid.SelectedItem;

            if (selectedOrder != null)
            {
                // Truyền OrderId vào EditOrderWindow
                var editOrderWindow = new EditOrderWindow(selectedOrder.OrderId);
                editOrderWindow.ShowDialog();

                // Sau khi chỉnh sửa xong, tải lại danh sách đơn hàng
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
            var selectedOrder = (Order)OrdersDataGrid.SelectedItem;

            if (selectedOrder != null)
            {
                // Hiển thị hộp thoại xác nhận
                var result = MessageBox.Show("Bạn có chắc chắn muốn xóa đơn hàng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
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
                            LoadOrders();  // Cập nhật lại danh sách đơn hàng sau khi xóa
                            MessageBox.Show("Đơn hàng đã được xóa thành công.");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi khi xóa đơn hàng: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đơn hàng để xóa.");
            }
        }


        private void OrdersDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Xóa các mục cũ trong OrderItemsDataGrid
            OrderItemsDataGrid.Items.Clear();

            // Lấy đơn hàng được chọn từ OrdersDataGrid
            var selectedOrder = (Order)OrdersDataGrid.SelectedItem;

            if (selectedOrder != null)
            {
                // Truy vấn để lấy chi tiết các OrderItems của đơn hàng được chọn
                string query = "SELECT oi.Quantity, ci.Name AS DrinkName, ci.Price AS DrinkPrice " +
                               "FROM OrderItems oi " +
                               "JOIN CafeItems ci ON oi.ItemId = ci.Id " +
                               "WHERE oi.OrderId = @OrderId";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (var command = new MySqlCommand(query, connection))
                        {
                            // Thêm tham số vào truy vấn để tránh SQL Injection
                            command.Parameters.AddWithValue("@OrderId", selectedOrder.OrderId);

                            using (var reader = command.ExecuteReader())
                            {
                                // Duyệt qua các OrderItems và thêm vào OrderItemsDataGrid
                                while (reader.Read())
                                {
                                    var orderItem = new OrderItem
                                    {
                                        DrinkName = reader.GetString("DrinkName"),
                                        Quantity = reader.GetInt32("Quantity"),
                                        // Hiển thị giá với định dạng VND
                                        DrinkPrice = reader.GetDecimal("DrinkPrice"),
                                        TotalPrice = reader.GetInt32("Quantity") * reader.GetDecimal("DrinkPrice")
                                    };

                                    OrderItemsDataGrid.Items.Add(new
                                    {
                                        DrinkName = orderItem.DrinkName,
                                        Quantity = orderItem.Quantity,
                                        // Định dạng giá với đơn vị VND
                                        DrinkPrice = $"{orderItem.DrinkPrice:N0} VND",
                                        TotalPrice = $"{orderItem.TotalPrice:N0} VND"
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
            }
        }


    }
}
