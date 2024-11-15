using System;
using System.Windows;
using cafeha.Controller;

namespace cafeha
{
    public partial class LoginWindow : Window
    {
        private FirebaseService _firebaseService;

        public LoginWindow()
        {
            InitializeComponent();
            _firebaseService = new FirebaseService(); // Khởi tạo FirebaseService
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordTextBox.Password.Trim();

            // Kiểm tra thông tin đăng nhập
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ tên đăng nhập và mật khẩu.", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                return; // Không thực hiện đăng nhập nếu thông tin thiếu
            }

            try
            {
                // Đăng nhập người dùng và kiểm tra thông tin trong Firestore
                bool isAuthenticated = await _firebaseService.LoginUser(username, password);

                if (isAuthenticated)
                {
                    // Lấy thông tin người dùng hiện tại từ Firestore
                    string role = await _firebaseService.GetUserRoleAsync(username);
                    string token = await _firebaseService.GetUserTokenAsync(username);

                    // Kiểm tra token
                    if (string.IsNullOrEmpty(token))
                    {
                        MessageBox.Show("Không có token hợp lệ.", "Lỗi đăng nhập", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    // Chuyển đến MainWindow với vai trò người dùng (có thể là null)
                    MainWindow mainWindow = new MainWindow(role ?? "guest", username,token); // Truyền vào "guest" nếu role là null
                    mainWindow.Show(); // Hiện MainWindow
                    this.Close(); // Đóng cửa sổ đăng nhập
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.", "Lỗi đăng nhập", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi không xác định: " + ex.Message, "Lỗi đăng nhập", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SwitchToRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show(); // Mở cửa sổ đăng ký
            this.Close(); // Đóng cửa sổ đăng nhập
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); 
        }
    }
}
