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
                        command.Parameters.AddWithValue("@ImageUrl", imageUrl); // Chỉ lưu tên tệp vào CSDL
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
