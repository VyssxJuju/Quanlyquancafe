using System;
using System.Windows;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace cafeha.Views
{
    public partial class AddOrderWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;";
        private List<Drink> _drinks = new List<Drink>(); // Danh sách đồ uống

        public AddOrderWindow()
        {
            InitializeComponent();
            LoadDrinks(); // Tải danh sách đồ uống từ cơ sở dữ liệu khi cửa sổ mở
        }

        // Lấy danh sách đồ uống từ cơ sở dữ liệu và hiển thị trong ComboBox
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
                                    Price = reader.GetDecimal("Price")
                                };

                                _drinks.Add(drink);
                                ItemComboBox.Items.Add(drink); // Thêm đồ uống vào ComboBox
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

        // Sự kiện khi nhấn nút "Lưu"
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (string.IsNullOrEmpty(UserIdTextBox.Text) ||
                ItemComboBox.SelectedItem == null ||
                string.IsNullOrEmpty(QuantityTextBox.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            try
            {
                int userId = int.Parse(UserIdTextBox.Text);
                var selectedDrink = (Drink)ItemComboBox.SelectedItem;
                int itemId = selectedDrink.ItemId;
                int quantity = int.Parse(QuantityTextBox.Text);
                decimal totalPrice = selectedDrink.Price * quantity;
                DateTime orderDate = DateTime.Now; // Ngày đặt hàng là thời điểm hiện tại

                // Câu lệnh SQL để thêm đơn hàng mới
                string query = "INSERT INTO Orders (UserId, ItemId, Quantity, TotalPrice, OrderDate) " +
                               "VALUES (@UserId, @ItemId, @Quantity, @TotalPrice, @OrderDate)";

                using (var connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.Parameters.AddWithValue("@ItemId", itemId);
                        command.Parameters.AddWithValue("@Quantity", quantity);
                        command.Parameters.AddWithValue("@TotalPrice", totalPrice);
                        command.Parameters.AddWithValue("@OrderDate", orderDate);

                        // Thực thi câu lệnh
                        command.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Đơn hàng đã được thêm thành công.");
                this.Close(); // Đóng cửa sổ sau khi lưu thành công
            }
            catch (FormatException)
            {
                MessageBox.Show("Vui lòng nhập đúng định dạng số.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm đơn hàng: " + ex.Message);
            }
        }
    }

    // Lớp Drink để chứa thông tin đồ uống
    public class Drink
    {
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }

        public override string ToString()
        {
            return Name; // Hiển thị tên đồ uống trong ComboBox
        }
    }
}
