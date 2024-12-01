using System;
using System.IO;
using System.Windows;
using Microsoft.Win32;
using MySql.Data.MySqlClient;

namespace cafeha
{
    public partial class AddDrinkWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;"; // Kết nối MySQL
        private string _imageDirectory = "images"; // Thư mục lưu ảnh trong ứng dụng

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
                string selectedFile = openFileDialog.FileName;
                string fileName = Path.GetFileName(selectedFile); // Lấy tên tệp từ đường dẫn

                // Đảm bảo thư mục images trong ứng dụng tồn tại
                string imageDirectory = Path.Combine(Directory.GetCurrentDirectory(), "images"); // Đường dẫn đến thư mục images trong ứng dụng

                if (!Directory.Exists(imageDirectory))
                {
                    Directory.CreateDirectory(imageDirectory); // Tạo thư mục nếu không tồn tại
                }

                // Sao chép ảnh vào thư mục images
                string destFile = Path.Combine(imageDirectory, fileName);
                File.Copy(selectedFile, destFile, true); // Sao chép tệp

                // Chuyển đường dẫn file thành Uri hợp lệ để hiển thị ảnh
                Uri imageUri = new Uri($"file:///{destFile.Replace("\\", "/")}"); // Chuyển đổi dấu gạch chéo ngược thành gạch chéo xuôi
                SelectedImage.Source = new System.Windows.Media.Imaging.BitmapImage(imageUri); // Hiển thị ảnh

                // Hiển thị đường dẫn ảnh trong TextBox
                ImageUrlTextBox.Text = Path.Combine("images", fileName).Replace("\\", "/"); // Lưu đường dẫn tương đối với dấu gạch chéo xuôi
            }
        }





        // Sự kiện khi nhấn "Thêm"
        private void AddDrink_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ các trường nhập liệu
            string name = NameTextBox.Text.Trim();
            string priceText = PriceTextBox.Text.Trim();
            string category = CategoryComboBox.Text.Trim();
            string imageUrl = ImageUrlTextBox.Text.Trim();

            // Kiểm tra dữ liệu đầu vào
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Vui lòng nhập tên đồ uống.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(priceText) || !decimal.TryParse(priceText, out decimal price) || price <= 0)
            {
                MessageBox.Show("Vui lòng nhập giá hợp lệ (số lớn hơn 0).", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(category))
            {
                MessageBox.Show("Vui lòng chọn danh mục.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrEmpty(imageUrl))
            {
                MessageBox.Show("Vui lòng chọn ảnh cho đồ uống.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Kiểm tra tên đồ uống có bị trùng không
            string checkQuery = "SELECT COUNT(*) FROM CafeItems WHERE Name = @Name";
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@Name", name);
                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count > 0)
                        {
                            MessageBox.Show("Tên đồ uống đã tồn tại. Vui lòng chọn tên khác.", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kiểm tra dữ liệu: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }

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

                    MessageBox.Show("Đồ uống đã được thêm thành công.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close(); // Đóng cửa sổ sau khi lưu thành công
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi thêm đồ uống: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

    }
}
