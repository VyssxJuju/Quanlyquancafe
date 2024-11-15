using cafeha.Model;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using Google.Cloud.Firestore;
using System;

namespace cafeha.Views
{
    public partial class OrderWindow : Window
    {
        public OrderWindow(List<CafeItem> drinks)
        {
            InitializeComponent();

            // Hiển thị thông tin đồ uống đã chọn
            foreach (var drink in drinks)
            {
                // Hiển thị tên và giá đồ uống (có thể tùy chỉnh theo nhu cầu)
                DrinkNameTextBlock.Text += drink.Name + "\n";
                DrinkPriceTextBlock.Text += drink.Price.ToString("N0") + " đ\n";
            }
        }

        private async void ConfirmOrder_Click(object sender, RoutedEventArgs e)
        {
            string drinkName = DrinkNameTextBlock.Text.Trim();
            double drinkPrice = Convert.ToDouble(DrinkPriceTextBlock.Text.Replace(" đ", "").Replace(",", ""));
            int quantity = int.Parse(QuantityTextBox.Text);
            string takeawayType = (TakeAwayComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            // Tạo đối tượng đơn hàng
            var order = new
            {
                DrinkName = drinkName,
                DrinkPrice = drinkPrice,
                Quantity = quantity,
                TakeawayType = takeawayType,
                OrderDate = DateTime.UtcNow // Sử dụng thời gian UTC
            };

            // Kết nối với Firestore
            FirestoreDb db = FirestoreDb.Create("cafe-manager-c433e"); // Thay "cafe-manager-c433e" bằng ID dự án Firestore của bạn

            // Lưu đơn hàng vào collection "Orders"
            DocumentReference docRef = db.Collection("Orders").Document(); // Tạo một tài liệu mới
            await docRef.SetAsync(order); // Lưu đơn hàng

            // Hiển thị thông báo thành công
            MessageBox.Show($"Đặt hàng thành công!\nTên: {drinkName}\nGiá: {drinkPrice} đ\nSố lượng: {quantity}\nLoại mang về: {takeawayType}");

            this.Close(); // Đóng cửa sổ đặt hàng
        }
    }
}
