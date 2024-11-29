using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows;
using System.IO;

namespace cafeha
{
    public partial class EditDrinkWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;"; // Kết nối MySQL
        private string _drinkName;

        public EditDrinkWindow(string name, decimal price, string imageUrl, string category)
        {
            InitializeComponent();


            // Hiển thị các thông tin cũ của đồ uống
            NameTextBox.Text = name;
            PriceTextBox.Text = price.ToString();
            ImageUrlTextBox.Text = imageUrl;
            CategoryComboBox.SelectedItem = CategoryComboBox.Items.Cast<ComboBoxItem>()
                                                        .FirstOrDefault(item => ((ComboBoxItem)item).Content.ToString() == category);

            string fileUrl = $"file:///{imageUrl.Replace("\\", "/")}";
            DrinkImage.Source = new BitmapImage(new Uri(fileUrl));




            _drinkName = name; // Lưu tên của đồ uống để dùng khi cập nhật
        }

        // Lưu các thay đổi
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string newName = NameTextBox.Text;
            decimal newPrice = decimal.TryParse(PriceTextBox.Text, out decimal result) ? result : 0;
            string newCategory = ((ComboBoxItem)CategoryComboBox.SelectedItem)?.Content.ToString();
            string newImageUrl = ImageUrlTextBox.Text;

            // Cập nhật thông tin đồ uống trong cơ sở dữ liệu
            string query = "UPDATE CafeItems SET Name = @Name, Price = @Price, Category = @Category, ImageUrl = @ImageUrl WHERE Name = @OldName";
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", newName);
                        command.Parameters.AddWithValue("@Price", newPrice);
                        command.Parameters.AddWithValue("@Category", newCategory);
                        command.Parameters.AddWithValue("@ImageUrl", newImageUrl);
                        command.Parameters.AddWithValue("@OldName", _drinkName);
                        command.ExecuteNonQuery();
                    }

                    MessageBox.Show("Đồ uống đã được cập nhật thành công.");
                    this.Close(); // Đóng cửa sổ sau khi cập nhật
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cập nhật đồ uống: " + ex.Message);
                }
            }
        }

        // Chọn ảnh mới
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

                // Lưu tên tệp vào TextBox
                ImageUrlTextBox.Text = fileName;

                // Hiển thị ảnh
                Uri imageUri = new Uri($"file:///{selectedFile.Replace("\\", "/")}");
                DrinkImage.Source = new BitmapImage(imageUri);
            }
        }
    }
}
