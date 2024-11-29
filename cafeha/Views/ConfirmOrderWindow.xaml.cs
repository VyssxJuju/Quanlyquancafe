using System;
using System.Collections.Generic;
using System.Windows;
using cafeha.Model;
using MySql.Data.MySqlClient;

namespace cafeha.Views
{
    public partial class ConfirmOrderWindow : Window
    {
        private string _connectionString;
        private Order _order;
        private List<OrderItem> _orderItems;

        public ConfirmOrderWindow(string connectionString, Order order, List<OrderItem> orderItems)
        {
            InitializeComponent();
            _connectionString = connectionString;
            _order = order;
            _orderItems = orderItems;

            // Hiển thị thông tin đơn hàng
            DisplayOrderDetails();
        }

        // Hiển thị thông tin đơn hàng lên UI
        private void DisplayOrderDetails()
        {
            // Hiển thị danh sách các món trong đơn hàng
            OrderItemsDataGrid.ItemsSource = _orderItems;

            // Tính tổng tiền và hiển thị
            decimal totalPrice = 0;
            foreach (var item in _orderItems)
            {
                totalPrice += item.TotalPrice;
            }
            TotalPriceText.Text = $"{totalPrice:N0} VND"; // Hiển thị tổng tiền
            _order.TotalPrice = totalPrice; // Cập nhật lại tổng tiền cho order
        }

        // Lưu thông tin đơn hàng vào cơ sở dữ liệu khi xác nhận
        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            // Tính lại tổng tiền cho đơn hàng
            decimal totalPrice = 0;
            foreach (var item in _orderItems)
            {
                totalPrice += item.TotalPrice; // Tính tổng tiền cho các món
            }
            _order.TotalPrice = totalPrice; // Cập nhật lại tổng tiền cho đơn

            // Cập nhật bảng Orders với tổng tiền và ngày tháng
            string updateOrderQuery = "UPDATE Orders SET TotalPrice = @TotalPrice, OrderDate = @OrderDate WHERE OrderId = @OrderId";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(updateOrderQuery, connection))
                    {
                        command.Parameters.AddWithValue("@TotalPrice", _order.TotalPrice);
                        command.Parameters.AddWithValue("@OrderDate", _order.OrderDate);
                        command.Parameters.AddWithValue("@OrderId", _order.OrderId);

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật đơn hàng: " + ex.Message);
                    return; // Dừng lại nếu có lỗi
                }
            }

            // Cập nhật OrderItems (xóa tất cả các món cũ và thêm lại món mới)
            UpdateOrderItems();

            MessageBox.Show("Đơn hàng đã được lưu thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

            // Đóng cửa sổ sau khi lưu
            this.Close();
        }

        // Kiểm tra ItemId trong CafeItems trước khi thêm vào OrderItems
        private bool IsItemExistInCafeItems(int itemId)
        {
            using (var connection = new MySqlConnection(_connectionString))
            {
                string query = "SELECT COUNT(*) FROM CafeItems WHERE Id = @ItemId";
                using (var command = new MySqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ItemId", itemId);
                    connection.Open();
                    var result = Convert.ToInt32(command.ExecuteScalar());
                    return result > 0; // Trả về true nếu ItemId tồn tại trong CafeItems
                }
            }
        }

        // Xóa tất cả các món trong OrderItems của đơn hàng
        private void DeleteAllOrderItems()
        {
            string deleteQuery = "DELETE FROM OrderItems WHERE OrderId = @OrderId";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", _order.OrderId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi xóa các món trong OrderItems: " + ex.Message);
                }
            }
        }

        // Cập nhật lại OrderItems (xóa tất cả các món cũ và thêm lại món mới)
        private void UpdateOrderItems()
        {
            // 1. Xóa tất cả các món trong OrderItems của đơn hàng này
            DeleteAllOrderItems();

            // 2. Thêm lại tất cả các món trong _orderItems
            foreach (var item in _orderItems)
            {
                // Kiểm tra xem ItemId có tồn tại trong CafeItems không
                if (!IsItemExistInCafeItems(item.ItemId))
                {
                    MessageBox.Show($"Món với ID {item.ItemId} không tồn tại trong hệ thống!");
                    return; // Dừng lại nếu không tìm thấy món
                }

                // Thêm lại món vào cơ sở dữ liệu
                InsertOrderItem(item);
            }
        }

        // Thêm mới OrderItem vào cơ sở dữ liệu
        private void InsertOrderItem(OrderItem item)
        {
            string insertQuery = "INSERT INTO OrderItems (OrderId, ItemId, Quantity, TotalPrice, OrderDate) " +
                                 "VALUES (@OrderId, @ItemId, @Quantity, @TotalPrice, @OrderDate)";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(insertQuery, connection))
                    {
                        command.Parameters.AddWithValue("@OrderId", _order.OrderId);
                        command.Parameters.AddWithValue("@ItemId", item.ItemId); // Chắc chắn có `ItemId` của món
                        command.Parameters.AddWithValue("@Quantity", item.Quantity);
                        command.Parameters.AddWithValue("@TotalPrice", item.TotalPrice);
                        command.Parameters.AddWithValue("@OrderDate", _order.OrderDate); // Ngày đơn hàng

                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm OrderItems mới: " + ex.Message);
                }
            }
        }

        // Hủy và đóng cửa sổ
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
