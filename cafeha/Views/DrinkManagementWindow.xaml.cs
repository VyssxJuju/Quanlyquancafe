using System;
using System.Collections.Generic;
using System.Windows;
using MySql.Data.MySqlClient;


namespace cafeha
{
    public partial class DrinkManagementWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;"; // Kết nối với MySQL
        private List<Drink> _drinks = new List<Drink>(); // Lưu danh sách đồ uống

        public DrinkManagementWindow()
        {
            InitializeComponent();
            LoadDrinks();  // Tải danh sách đồ uống khi cửa sổ mở
        }

        // Lấy danh sách đồ uống từ cơ sở dữ liệu và hiển thị lên DataGrid
        private void LoadDrinks()
        {
            _drinks.Clear();
            DrinkDataGrid.Items.Clear();

            string query = "SELECT * FROM CafeItems";  // Giả sử CafeItems chứa trường Category
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
                                var drink = new Drink
                                {
                                    Name = reader.GetString("Name"),
                                    Price = reader.GetDecimal("Price"),
                                    ImageUrl = reader.GetString("ImageUrl"),
                                    Category = reader.GetString("Category")  // Thêm Category vào Drink
                                };

                                _drinks.Add(drink);
                                DrinkDataGrid.Items.Add(drink); // Hiển thị đồ uống trong DataGrid
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message);
                }
            }
        }


        // Thêm đồ uống mới
        private void AddDrink_Click(object sender, RoutedEventArgs e)
        {
            // Mở cửa sổ nhập liệu hoặc thực hiện logic thêm đồ uống
            var addDrinkWindow = new AddDrinkWindow(); // Tạo một cửa sổ mới để thêm đồ uống
            addDrinkWindow.ShowDialog();

            // Sau khi thêm đồ uống thành công, tải lại danh sách
            LoadDrinks();
        }

        // Sửa đồ uống
        // Sửa đồ uống
        private void EditDrink_Click(object sender, RoutedEventArgs e)
        {
            // Kiểm tra xem có đồ uống nào được chọn trong DataGrid không
            var selectedDrink = (Drink)DrinkDataGrid.SelectedItem;

            if (selectedDrink != null)
            {
                // Mở cửa sổ chỉnh sửa đồ uống với thông tin đã chọn
                var editDrinkWindow = new EditDrinkWindow(
                    selectedDrink.Name,
                    selectedDrink.Price, 
                    selectedDrink.ImageUrl,
                    selectedDrink.Category
                );
                editDrinkWindow.ShowDialog();

                // Sau khi đóng cửa sổ chỉnh sửa, tải lại danh sách đồ uống
                LoadDrinks();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một đồ uống để sửa.");
            }
        }


        // Xóa đồ uống
        private void DeleteDrink_Click(object sender, RoutedEventArgs e)
        {
            if (DrinkDataGrid.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn đồ uống để xóa.");
                return;
            }

            var selectedDrink = (Drink)DrinkDataGrid.SelectedItem;

            // Hiển thị hộp thoại xác nhận yêu cầu xóa
            var result = MessageBox.Show(
                $"Bạn có chắc chắn muốn xóa đồ uống {selectedDrink.Name}?",
                "Xác nhận xóa",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            // Kiểm tra nếu người dùng chọn Yes
            if (result == MessageBoxResult.Yes)
            {
                // Xóa khỏi cơ sở dữ liệu
                string query = "DELETE FROM CafeItems WHERE Name = @Name";
                using (var connection = new MySqlConnection(_connectionString))
                {
                    try
                    {
                        connection.Open();
                        using (var command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Name", selectedDrink.Name);
                            command.ExecuteNonQuery();
                        }

                        // Sau khi xóa đồ uống, tải lại danh sách
                        LoadDrinks();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi xóa đồ uống: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Đã hủy thao tác xóa.");
            }
        }


        // Sự kiện chọn đồ uống từ DataGrid
        private void DrinkDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Hiển thị thông tin chi tiết của đồ uống đã chọn (nếu cần)
        }



        public class Drink
        {
            public string Name { get; set; }
            public decimal Price { get; set; }
            public string Category { get; set; }
            public string ImageUrl { get; set; }

            // Thuộc tính này dùng để hiển thị giá dưới dạng VND
            public string FormattedPrice
            {
                get
                {
                    return Price.ToString("N0") + " VND"; // Định dạng với dấu phân cách hàng nghìn và VND
                }
            }



        }

    }
}
