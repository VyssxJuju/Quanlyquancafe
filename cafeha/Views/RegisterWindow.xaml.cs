using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace cafeha
{
    public partial class RegisterWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;"; // Kết nối với MySQL

        public RegisterWindow()
        {
            InitializeComponent();
        }

        // Sự kiện khi nhấn nút Đăng ký
        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordTextBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu.");
                return;
            }

            // Kiểm tra xem tên đăng nhập đã tồn tại chưa
            if (IsUsernameExists(username))
            {
                MessageBox.Show("Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.");
                return;
            }

            // Thêm người dùng mới vào cơ sở dữ liệu
            bool isRegistered = RegisterUser(username, password);

            if (isRegistered)
            {
                MessageBox.Show("Đăng ký thành công!");
                this.Close();  // Đóng cửa sổ đăng ký
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();  // Chuyển sang cửa sổ đăng nhập
            }
            else
            {
                MessageBox.Show("Đã có lỗi xảy ra trong quá trình đăng ký.");
            }
        }

        // Kiểm tra tên đăng nhập có tồn tại trong cơ sở dữ liệu hay không
        private bool IsUsernameExists(string username)
        {
            string query = "SELECT * FROM Users WHERE username = @username";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.HasRows) // Nếu tên đăng nhập đã tồn tại
                            {
                                return true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }

            return false;
        }

        // Thêm người dùng mới vào cơ sở dữ liệu
        private bool RegisterUser(string username, string password)
        {
            string query = "INSERT INTO Users (username, password, role) VALUES (@username, @password, 'none')";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@password", password);  // Lưu mật khẩu dưới dạng thô (Nên mã hóa mật khẩu trước khi lưu)

                        int result = command.ExecuteNonQuery();

                        return result > 0;  // Nếu câu lệnh thực thi thành công
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }

            return false;
        }

        // Sự kiện khi nhấn nút Trở lại Đăng nhập
        private void SwitchToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();  // Đóng cửa sổ đăng ký khi chuyển sang đăng nhập
        }
    }
}
