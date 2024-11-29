using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace cafeha
{
    public class StringToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string imagePath && !string.IsNullOrEmpty(imagePath))
            {
                try
                {
                    // Chuyển đổi đường dẫn thành BitmapImage
                    return new BitmapImage(new Uri(imagePath, UriKind.RelativeOrAbsolute));
                }
                catch
                {
                    return null;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
