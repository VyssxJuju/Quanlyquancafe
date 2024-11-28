using System;
using System.Windows;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace cafeha
{
    public partial class AddDrinkWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;"; // Kết nối MySQL

        public AddDrinkWindow()
        {
            InitializeComponent();
        }

        // Chọn ảnh từ máy tính
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ImageUrlTextBox.Text = openFileDialog.FileName; // Lấy đường dẫn ảnh
            }
        }

        // Sự kiện khi nhấn "Thêm"
        private void AddDrink_Click(object sender, RoutedEventArgs e)
        {

            // Lấy dữ liệu từ các trường nhập liệu
            string name = NameTextBox.Text;
            decimal price = decimal.TryParse(PriceTextBox.Text, out decimal result) ? result : 0;
            string category = CategoryComboBox.Text;
            string imageUrl = ImageUrlTextBox.Text;

            // Thêm đồ uống vào cơ sở dữ liệu
            string query = "INSERT INTO CafeItems (Name, Price, Category, ImageUrl) VALUES (@Name, @Price, @Category, @ImageUrl)";
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@Category", category);
                        command.Parameters.AddWithValue("@ImageUrl", imageUrl);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Đồ uống đã được thêm thành công.");
                    this.Close(); // Đóng cửa sổ sau khi lưu thành công
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm đồ uống: " + ex.Message);
                }
            }
        }
    }
}
