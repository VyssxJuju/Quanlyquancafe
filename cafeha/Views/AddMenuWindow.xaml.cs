using cafeha.Controller;
using Microsoft.Win32;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Firebase.Storage;
using cafeha.Model;

namespace cafeha
{
    public partial class AddMenuWindow : Window
    {
        private string selectedImagePath; // Biến để lưu đường dẫn ảnh được chọn
        private FirebaseService _firebaseService; // Biến lưu đối tượng FirebaseService
        private string _userToken; // Thêm biến để lưu token của người dùng

        public AddMenuWindow(string userToken) // Truyền token từ MainWindow
        {
            InitializeComponent();
            _firebaseService = new FirebaseService(); // Khởi tạo FirebaseService
            _userToken = userToken; // Lưu token của người dùng
        }

        // Sự kiện khi nhấn nút "Chọn Ảnh"
        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png)|*.jpg;*.jpeg;*.png"; // Lọc các loại tệp ảnh
            if (openFileDialog.ShowDialog() == true)
            {
                selectedImagePath = openFileDialog.FileName; // Lưu đường dẫn của ảnh đã chọn
                MessageBox.Show($"Đã chọn ảnh: {Path.GetFileName(selectedImagePath)}"); // Hiển thị tên ảnh đã chọn

                // Hiển thị ảnh đã chọn
                PreviewImage.Source = new BitmapImage(new Uri(selectedImagePath));
                PreviewImage.Visibility = Visibility.Visible; // Hiện hình ảnh
            }
        }

        // Sự kiện khi nhấn nút "Thêm Món"
        private async void AddMenuButton_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra token người dùng trước khi cho phép thêm món
            bool isTokenValid = await _firebaseService.IsUserTokenValid(_userToken); // Truyền token của người dùng

            if (!isTokenValid)
            {
                MessageBox.Show("Token không hợp lệ. Bạn không có quyền thêm món.", "Lỗi quyền", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Lấy thông tin từ các trường nhập liệu
            string name = NameTextBox.Text;

            // Lấy giá từ TextBox và chuyển đổi nó thành decimal
            if (!decimal.TryParse(PriceTextBox.Text, out decimal priceDecimal))
            {
                MessageBox.Show("Giá không hợp lệ. Vui lòng nhập lại.");
                return; // Thoát nếu giá không hợp lệ
            }

            // Chuyển đổi từ decimal sang double
            double price = (double)priceDecimal; // Chuyển đổi từ decimal sang double

            string category = ((ComboBoxItem)CategoryComboBox.SelectedItem).Content.ToString();

            if (selectedImagePath == null)
            {
                MessageBox.Show("Hãy chọn ảnh cho món.");
                return;
            }

            // Tải ảnh lên Firebase Storage và lấy URL
            string imageUrl = await UploadImageToFirebase(selectedImagePath);

            if (imageUrl == null)
            {
                MessageBox.Show("Có lỗi xảy ra khi tải ảnh lên Firebase.");
                return;
            }

            // Tạo đối tượng CafeItem
            CafeItem newItem = new CafeItem
            {
                Name = name,
                Price = price,
                Category = category,
                ImageUrl = imageUrl // Lưu URL của ảnh
            };

            // Thêm đồ uống vào Firebase
            await _firebaseService.AddCafeItemAsync(newItem);

            MessageBox.Show("Món đã được thêm thành công!");
            this.Close(); // Đóng cửa sổ sau khi thêm món
        }

        private async Task<string> UploadImageToFirebase(string filePath)
        {
            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    // Đặt đường dẫn lưu trữ cho ảnh
                    var uploadTask = new FirebaseStorage("cafe-manager-c433e.appspot.com")
                        .Child("images")
                        .Child(Path.GetFileName(filePath))
                        .PutAsync(stream);

                    var downloadUrl = await uploadTask; // Lấy URL tải về

                    return downloadUrl; // Trả về URL tải về
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Có lỗi khi tải ảnh lên: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                return null; // Trả về null nếu có lỗi
            }
        }
    }
}
