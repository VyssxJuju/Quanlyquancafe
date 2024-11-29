using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

namespace cafeha.Models
{
    // Lớp đại diện cho đồ uống
    public class Drink
    {
        public int Id { get; set; }
        public int ItemId { get; set; }  // ID của đồ uống
        public string Name { get; set; } // Tên đồ uống
        public decimal Price { get; set; } // Giá đồ uống
        public string ImageUrl { get; set; } // Đường dẫn ảnh của đồ uống
        public string Category { get; set; } // Danh mục đồ uống

        // Thuộc tính ImageSource để hiển thị ảnh từ URL
        public BitmapImage ImageSource
        {
            get
            {
                // Kiểm tra nếu URL hợp lệ
                if (Uri.IsWellFormedUriString(ImageUrl, UriKind.RelativeOrAbsolute))
                {
                    return new BitmapImage(new Uri(ImageUrl, UriKind.RelativeOrAbsolute)); // Chuyển đổi URL thành BitmapImage
                }
                return null; // Trả về null nếu đường dẫn ảnh không hợp lệ
            }
        }
    }

    // Lớp đại diện cho danh mục đồ uống, có thể chứa nhiều đồ uống
    public class DrinkCategory
    {
        public string Name { get; set; } // Tên danh mục đồ uống
        public List<Drink> Items { get; set; } // Danh sách đồ uống thuộc danh mục này
    }
}
