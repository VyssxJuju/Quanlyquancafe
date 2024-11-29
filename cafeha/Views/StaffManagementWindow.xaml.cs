using System;
using System.Collections.Generic;
using System.Windows;
using cafeha.Model;
using MySql.Data.MySqlClient;

namespace cafeha.Views
{
    public partial class StaffManagementWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;";

        public StaffManagementWindow()
        {
            InitializeComponent();
            LoadStaffData();
        }

        // Hàm tải dữ liệu nhân viên vào DataGrid
        private void LoadStaffData()
        {
            List<Staff> staffList = GetStaffList();
            StaffDataGrid.ItemsSource = staffList;  // Chỉ gán lại dữ liệu chứ không sửa thuộc tính FormattedSalary
        }


        // Hàm lấy danh sách nhân viên từ cơ sở dữ liệu
        private List<Staff> GetStaffList()
        {
            List<Staff> staffList = new List<Staff>();
            string query = "SELECT e.EmployeeId, e.EmployeeName, u.Username, u.Password, e.Salary, e.PhoneNumber, e.Address, e.DateOfBirth, u.Role FROM Employee e INNER JOIN Users u ON e.EmployeeId = u.EmployeeId";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var staff = new Staff
                                {
                                    EmployeeId = reader.GetInt32("EmployeeId"),
                                    EmployeeName = reader.GetString("EmployeeName"),
                                    Username = reader.GetString("Username"),
                                    Password = reader.GetString("Password"),
                                    Salary = reader.GetDecimal("Salary"),
                                    PhoneNumber = reader.GetString("PhoneNumber"),
                                    Address = reader.GetString("Address"),
                                    DateOfBirth = reader.GetDateTime("DateOfBirth"),
                                    Role = reader.GetString("Role")
                                };
                                staffList.Add(staff);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy danh sách nhân viên: " + ex.Message);
                }
            }
            return staffList;
        }

        // Xử lý sự kiện Thêm nhân viên
        private void AddStaff_Click(object sender, RoutedEventArgs e)
        {
            var addStaffWindow = new AddStaffWindow();
            addStaffWindow.ShowDialog();
            LoadStaffData(); // Tải lại dữ liệu nhân viên sau khi thêm
        }

        // Xử lý sự kiện Sửa nhân viên
        private void EditStaff_Click(object sender, RoutedEventArgs e)
        {
            var selectedStaff = (Staff)StaffDataGrid.SelectedItem;
            if (selectedStaff != null)
            {
                var editStaffWindow = new EditStaffWindow(selectedStaff);
                editStaffWindow.ShowDialog();
                LoadStaffData(); // Tải lại dữ liệu nhân viên sau khi sửa
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để sửa.");
            }
        }

        // Xử lý sự kiện Xóa nhân viên
        private void DeleteStaff_Click(object sender, RoutedEventArgs e)
        {
            var selectedStaff = (Staff)StaffDataGrid.SelectedItem;
            if (selectedStaff != null)
            {
                MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa nhân viên này?", "Xóa nhân viên", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    DeleteStaff(selectedStaff.EmployeeId);
                    LoadStaffData(); // Tải lại dữ liệu nhân viên sau khi xóa
                }
            }
            else
            {
                MessageBox.Show("Vui lòng chọn nhân viên để xóa.");
            }
        }

        // Hàm xóa nhân viên khỏi cơ sở dữ liệu
        private void DeleteStaff(int employeeId)
        {
            string queryUser = "DELETE FROM Users WHERE EmployeeId = @EmployeeId";
            string queryEmployee = "DELETE FROM Employee WHERE EmployeeId = @EmployeeId";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var transaction = connection.BeginTransaction())
                    {
                        try
                        {
                            using (var command = new MySqlCommand(queryUser, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                                command.ExecuteNonQuery();
                            }

                            using (var command = new MySqlCommand(queryEmployee, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                                command.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            MessageBox.Show("Xóa nhân viên thành công!");
                        }
                        catch (Exception ex)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Lỗi khi xóa nhân viên: " + ex.Message);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi kết nối tới cơ sở dữ liệu: " + ex.Message);
                }
            }
        }

        private void GrantRole_Click(object sender, RoutedEventArgs e)
        {
            var selectedStaff = StaffDataGrid.SelectedItem as Staff;

            if (selectedStaff == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên để cấp quyền.");
                return;
            }

            var result = MessageBox.Show($"Bạn có chắc chắn muốn cấp quyền Staff cho nhân viên {selectedStaff.EmployeeName}?", "Cấp Quyền", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                UpdateEmployeeRole(selectedStaff.EmployeeId, "staff");
            }
        }

        private void UpdateEmployeeRole(int employeeId, string newRole)
        {
            string query = "UPDATE Users SET Role = @Role WHERE EmployeeId = @EmployeeId";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Role", newRole);
                        command.Parameters.AddWithValue("@EmployeeId", employeeId);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Cấp quyền thành công.");
                            LoadStaffData();  // Tải lại dữ liệu nhân viên sau khi cập nhật quyền
                        }
                        else
                        {
                            MessageBox.Show("Cấp quyền không thành công. Vui lòng thử lại.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi cấp quyền: " + ex.Message);
                }
            }
        }
    }

    public class Staff
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public decimal Salary { get; set; }
        public string FormattedSalary
        {
            get
            {
                return Salary.ToString("#,0") + " VND";
            }
        }
        public string Role { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }

        // Định dạng lại ngày sinh không có giờ
        public string FormattedDateOfBirth
        {
            get
            {
                return DateOfBirth.ToString("yyyy-MM-dd");
            }
        }

        public DateTime DateOfBirth { get; set; }
    }
}
