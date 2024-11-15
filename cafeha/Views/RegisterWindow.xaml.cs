using System;
using System.Windows;
using cafeha.Controller;

namespace cafeha
{
    public partial class RegisterWindow : Window
    {
        private FirebaseService _firebaseService;

        public RegisterWindow()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService(); // Khởi tạo FirebaseService
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim(); // Lấy username từ TextBox
            string password = PasswordTextBox.Password.Trim();

            // Kiểm tra thông tin đăng ký
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ username và mật khẩu.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Không thực hiện đăng ký nếu thông tin thiếu
            }

            try
            {
                // Đăng ký người dùng và lấy UID
                string uid = await _firebaseService.RegisterUser(username, password);

                // Lưu vai trò vào Firestore
                await _firebaseService.SetUserRoleAsync(uid, "none");

                MessageBox.Show("Đăng ký thành công! UID: " + uid, "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);

                // Chuyển đến cửa sổ đăng nhập
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                this.Close(); // Đóng cửa sổ đăng ký
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi đăng ký", MessageBoxButton.OK, MessageBoxImage.Error); // Hiển thị thông báo lỗi
            }
        }



        private void SwitchToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show(); // Mở cửa sổ đăng nhập
            this.Close(); // Đóng cửa sổ đăng ký
        }
    }
}
