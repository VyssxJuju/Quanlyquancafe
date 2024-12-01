using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using MySql.Data.MySqlClient;

namespace cafeha.Views
{
    public partial class RevenueWindow : Window
    {
        private string _connectionString = "Server=127.0.0.1; Database=cafehaaaaa; Uid=root; Pwd=;";

        public DateTime? StartDate { get; set; } // Ngày bắt đầu

        public RevenueWindow()
        {
            InitializeComponent();
            StartDatePicker.SelectedDate = DateTime.Now;  // Mặc định là ngày hôm nay
            StartDate = StartDatePicker.SelectedDate;
            LoadRevenueData();  // Tự động tính doanh thu khi cửa sổ được mở
        }

        // Tính doanh thu từ ngày bắt đầu mà người dùng chọn (hoặc mặc định ngày hôm nay)
        private void LoadRevenueData()
        {
            if (!StartDate.HasValue)
            {
                MessageBox.Show("Vui lòng chọn ngày bắt đầu.");
                return;
            }

            // Lấy doanh thu từ các khoảng thời gian khác nhau
            var revenueToday = GetRevenueForToday();
            var revenueMonth = GetRevenueForThisMonth();
            var revenueData = GetRevenueByMonth(StartDate.Value);

            // Cập nhật UI
            RevenueTodayTextBlock.Text = $"Doanh thu hôm nay: {revenueToday.ToString("N0")} VND";
            RevenueMonthTextBlock.Text = $"Doanh thu tháng này: {revenueMonth.ToString("N0")} VND";

            // Hiển thị doanh thu theo tháng
            RevenueDataGrid.ItemsSource = revenueData;
        }

        // Lấy doanh thu của ngày hôm nay
        private decimal GetRevenueForToday()
        {
            var today = DateTime.Now.Date;
            string query = "SELECT SUM(TotalPrice) FROM Orders WHERE DATE(OrderDate) = @Today";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Today", today);

                        var result = command.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy doanh thu hôm nay: " + ex.Message);
                    return 0m;
                }
            }
        }

        // Lấy doanh thu của tháng này
        private decimal GetRevenueForThisMonth()
        {
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            string query = "SELECT SUM(TotalPrice) FROM Orders WHERE OrderDate >= @StartOfMonth";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartOfMonth", startOfMonth);

                        var result = command.ExecuteScalar();
                        return result != DBNull.Value ? Convert.ToDecimal(result) : 0m;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy doanh thu tháng này: " + ex.Message);
                    return 0m;
                }
            }
        }

        // Lấy doanh thu theo từng tháng từ ngày bắt đầu
        private List<RevenueInfo> GetRevenueByMonth(DateTime startDate)
        {
            var revenueData = new List<RevenueInfo>();
            string query = @"
                SELECT 
                    DATE_FORMAT(OrderDate, '%Y-%m') AS MonthYear, 
                    SUM(TotalPrice) AS Revenue
                FROM Orders 
                WHERE OrderDate >= @StartDate
                GROUP BY MonthYear
                ORDER BY MonthYear DESC";

            using (var connection = new MySqlConnection(_connectionString))
            {
                try
                {
                    connection.Open();
                    using (var command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@StartDate", startDate);

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var revenueInfo = new RevenueInfo
                                {
                                    TimePeriod = reader.GetString("MonthYear"), // Tháng và năm
                                    Revenue = reader.GetDecimal("Revenue") // Doanh thu
                                };

                                revenueData.Add(revenueInfo);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi lấy doanh thu: " + ex.Message);
                }
            }

            return revenueData;
        }
    }

    // Lớp để chứa thông tin doanh thu
    public class RevenueInfo
    {
        public string TimePeriod { get; set; }  // Tháng và năm
        public decimal Revenue { get; set; }    // Doanh thu
    }
}
