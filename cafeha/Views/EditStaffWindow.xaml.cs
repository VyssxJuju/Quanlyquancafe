using System;
using System.Windows;
using MySql.Data.MySqlClient;

namespace cafeha.Views
{
    public partial class EditStaffWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;";
        public int EmployeeId { get; set; }  // Nhận EmployeeId từ StaffManagementWindow

        public EditStaffWindow(Staff selectedStaff)
        {
            InitializeComponent();
            EmployeeId = selectedStaff.EmployeeId;

            // Điền các trường vào các textbox từ thông tin nhân viên đã chọn
            EmployeeNameTextBox.Text = selectedStaff.EmployeeName;
            UsernameTextBox.Text = selectedStaff.Username;
            PasswordBox.Password = selectedStaff.Password;  // Mật khẩu
            PhoneNumberTextBox.Text = selectedStaff.PhoneNumber;
            PositionTextBox.Text = selectedStaff.Role;
            AddressTextBox.Text = selectedStaff.Address;
            DateOfBirthPicker.SelectedDate = selectedStaff.DateOfBirth;
            SalaryTextBox.Text = selectedStaff.Salary.ToString();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            // Lấy dữ liệu từ các trường nhập liệu
            string employeeName = EmployeeNameTextBox.Text;
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;
            string phoneNumber = PhoneNumberTextBox.Text;
            string position = PositionTextBox.Text;
            string address = AddressTextBox.Text;
            DateTime? dateOfBirth = DateOfBirthPicker.SelectedDate;
            decimal salary;

            if (string.IsNullOrWhiteSpace(employeeName) || string.IsNullOrWhiteSpace(username) ||
                string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(phoneNumber) ||
                string.IsNullOrWhiteSpace(position) || string.IsNullOrWhiteSpace(address) ||
                !dateOfBirth.HasValue || !decimal.TryParse(SalaryTextBox.Text, out salary))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin.");
                return;
            }

            // Cập nhật thông tin nhân viên vào cơ sở dữ liệu
            string updateEmployeeQuery = @"UPDATE Employee 
                                           SET EmployeeName = @EmployeeName, PhoneNumber = @PhoneNumber, 
                                               Position = @Position, Address = @Address, DateOfBirth = @DateOfBirth, 
                                               Salary = @Salary
                                           WHERE EmployeeId = @EmployeeId";

            string updateUserQuery = @"UPDATE Users 
                                       SET Username = @Username, Password = @Password 
                                       WHERE EmployeeId = @EmployeeId";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            // Cập nhật bảng Employee
                            using (var command = new MySqlCommand(updateEmployeeQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@EmployeeName", employeeName);
                                command.Parameters.AddWithValue("@PhoneNumber", phoneNumber);
                                command.Parameters.AddWithValue("@Position", position);
                                command.Parameters.AddWithValue("@Address", address);
                                command.Parameters.AddWithValue("@DateOfBirth", dateOfBirth.Value);
                                command.Parameters.AddWithValue("@Salary", salary);
                                command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                                command.ExecuteNonQuery();
                            }

                            // Cập nhật bảng Users
                            using (var command = new MySqlCommand(updateUserQuery, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@Username", username);
                                command.Parameters.AddWithValue("@Password", password);
                                command.Parameters.AddWithValue("@EmployeeId", EmployeeId);
                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Cập nhật thông tin nhân viên thành công!");
                            this.Close(); // Đóng cửa sổ sau khi cập nhật
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi khi cập nhật thông tin nhân viên: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối tới cơ sở dữ liệu: " + ex.Message);
                }
            }
        }
    }
}
