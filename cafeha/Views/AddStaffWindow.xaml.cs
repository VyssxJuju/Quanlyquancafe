using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace cafeha.Views
{
    public partial class AddStaffWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;"; // Kết nối cơ sở dữ liệu

        public AddStaffWindow()
        {
            InitializeComponent();
        }

        // Xử lý sự kiện Lưu
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            string employeeName = EmployeeNameTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string phoneNumber = PhoneNumberTextBox.Text;
            string position = PositionTextBox.Text;
            string address = AddressTextBox.Text;
            DateTime? dateOfBirth = DateOfBirthPicker.SelectedDate;
            decimal salary = 0; // Mức lương mặc định

            if (decimal.TryParse(SalaryTextBox.Text, out decimal enteredSalary))
            {
                salary = enteredSalary; // Nếu người dùng nhập mức lương, dùng giá trị đó
            }

            if (string.IsNullOrEmpty(employeeName) || string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) ||
                string.IsNullOrEmpty(phoneNumber) || string.IsNullOrEmpty(position) || string.IsNullOrEmpty(address) || !dateOfBirth.HasValue)
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Thêm tài khoản vào bảng Users
            bool isAccountCreated = CreateAccount(username, password);

            if (!isAccountCreated)
            {
                MessageBox.Show("Có lỗi khi tạo tài khoản.");
                return;
            }

            // Lấy UserId từ bảng Users
            int userId = GetUserId(username);

            // Thêm thông tin nhân viên vào bảng Employees
            bool isAdded = AddEmployee(userId, employeeName, phoneNumber, position, address, dateOfBirth.Value, salary);

            if (isAdded)
            {
                MessageBox.Show("Thêm nhân viên thành công.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Có lỗi xảy ra khi thêm nhân viên.");
            }
        }

        // Tạo tài khoản cho nhân viên trong bảng Users
        private bool CreateAccount(string username, string password)
        {
            string query = "INSERT INTO Users (Username, Password, Role) VALUES (@Username, @Password, 'Staff')";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);
                        command.Parameters.AddWithValue("@Password", password); // Lưu mật khẩu dưới dạng thô (Nên mã hóa mật khẩu trước khi lưu)

                        int result = command.ExecuteNonQuery();
                        return result > 0;  // Nếu thêm thành công
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }

            return false;
        }

        // Lấy UserId của người dùng từ bảng Users
        private int GetUserId(string username)
        {
            string query = "SELECT UserId FROM Users WHERE Username = @Username";
            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Username", username);

                        using (var reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return reader.GetInt32("UserId");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
            return 0; // Nếu không tìm thấy UserId
        }

        // Thêm thông tin nhân viên vào bảng Employees
        private bool AddEmployee(int userId, string employeeName, string phoneNumber, string position, string address, DateTime dateOfBirth, decimal salary)
        {
            string query = "INSERT INTO Employee (UserId, EmployeeName, PhoneNumber, Position, Address, DateOfBirth, Salary) " +
               "VALUES (@UserId, @EmployeeName, @PhoneNumber, @Position, @Address, @DateOfBirth, @Salary)";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        // Thêm các tham số vào câu lệnh SQL
                        command.Parameters.AddWithValue("@UserId", userId);  // userId lấy từ bảng Users
                        command.Parameters.AddWithValue("@EmployeeName", employeeName);
                        command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                        command.Parameters.AddWithValue("@Position", position);
                        command.Parameters.AddWithValue("@Address", address);
                        command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth);
                        command.Parameters.AddWithValue("@Salary", salary); // Mức lương

                        // Thực thi câu lệnh SQL
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            // Cập nhật EmployeeId vào bảng Users sau khi thêm nhân viên thành công
                            UpdateEmployeeId(userId);
                            return true;
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

        // Cập nhật EmployeeId trong bảng Users
        private void UpdateEmployeeId(int userId)
        {
            string query = "UPDATE Users SET EmployeeId = (SELECT EmployeeId FROM Employee WHERE UserId = @UserId) WHERE UserId = @UserId";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@UserId", userId);
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }
    }
}
