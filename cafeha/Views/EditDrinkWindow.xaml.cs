using System;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace cafeha
{
    public partial class EditDrinkWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;"; // Kết nối MySQL
        private string _currentImageUrl; // Lưu đường dẫn ảnh hiện tại
        private string _drinkName; // Tên đồ uống
        private decimal _drinkPrice; // Giá đồ uống
        private string _drinkCategory; // Danh mục đồ uống

        public EditDrinkWindow(string name, decimal price, string imageUrl, string category)
        {
            InitializeComponent();

            // Gán giá trị cho các thuộc tính khi cửa sổ mở
            _drinkName = name;
            _drinkPrice = price;
            _currentImageUrl = imageUrl;

            // Hiển thị thông tin lên form
            NameTextBox.Text = name;
            PriceTextBox.Text = price.ToString();
            ImageUrlTextBox.Text = imageUrl;


            // Nếu CategoryComboBox chứa các mục, chọn mục phù hợp
            foreach (ComboBoxItem item in CategoryComboBox.Items)
            {
                if (item.Content.ToString() == category)
                {
                    CategoryComboBox.SelectedItem = item;
                    break;
                }
            }
        }


        // Sự kiện khi nhấn "Chọn ảnh"
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

        // Sự kiện khi nhấn "Lưu" để lưu thông tin đồ uống
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra tính hợp lệ của dữ liệu
            if (string.IsNullOrEmpty(NameTextBox.Text) || string.IsNullOrEmpty(PriceTextBox.Text) || string.IsNullOrEmpty(ImageUrlTextBox.Text) || string.IsNullOrEmpty(CategoryComboBox.Text))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Lấy dữ liệu từ các trường nhập liệu
            string name = NameTextBox.Text;
            decimal price = decimal.TryParse(PriceTextBox.Text, out decimal result) ? result : 0;
            string imageUrl = ImageUrlTextBox.Text;
            string category = CategoryComboBox.Text; // Lấy giá trị danh mục từ ComboBox

            // Cập nhật thông tin đồ uống vào cơ sở dữ liệu
            string query = "UPDATE CafeItems SET Name = @Name, Price = @Price, ImageUrl = @ImageUrl, Category = @Category WHERE Name = @OldName";
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@Price", price);
                        command.Parameters.AddWithValue("@ImageUrl", imageUrl);
                        command.Parameters.AddWithValue("@Category", category); // Thêm tham số Category
                        command.Parameters.AddWithValue("@OldName", _drinkName); // Sử dụng tên cũ để tìm đồ uống cần sửa
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Đồ uống đã được cập nhật thành công.");
                    this.Close(); // Đóng cửa sổ sau khi lưu thành công
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật đồ uống: " + ex.Message);
                }
            }
        }
    }
}
