using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace cafeha
{
    public partial class LoginWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;"; // Kết nối với MySQL

        public LoginWindow()
        {
            InitializeComponent();
        }

        // Sự kiện khi nhấn nút Đăng nhập
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.");
                return;
            }

            // Kiểm tra thông tin đăng nhập với cơ sở dữ liệu
            var role = AuthenticateUser(username, password);
            if (!string.IsNullOrEmpty(role))
            {
                MessageBox.Show("Đăng nhập thành công!");
                // Chuyển sang cửa sổ chính (MainWindow)
                MessageBox.Show("Đang mở MainWindow...");
                MainWindow mainWindow = new MainWindow(role);
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng.");
            }
        }

        // Kiểm tra thông tin đăng nhập với cơ sở dữ liệu MySQL và lấy vai trò người dùng
        private string AuthenticateUser(string username, string password)
        {
            string query = "SELECT role FROM Users WHERE username = @username AND password = @password";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                return reader["role"].ToString(); // Lấy vai trò của người dùng
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }

            return null; // Nếu không tìm thấy người dùng
        }

        // Sự kiện khi nhấn nút Đăng ký
        private void SwitchToRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Close();  // Đóng cửa sổ đăng nhập khi chuyển sang đăng ký
        }

        // Sự kiện khi nhấn nút Thoát
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();  // Đóng ứng dụng
        }
    }
}
