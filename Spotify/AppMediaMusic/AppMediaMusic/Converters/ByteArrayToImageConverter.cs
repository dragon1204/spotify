using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace AppMediaMusic.Converters
{
    class ByteArrayToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] imageBytes && imageBytes.Length > 0)
            {
                return imageBytes;  // Convert byte array to image
            }

            // If no album art is available, return a default image
            return new Uri("pack://application:,,,/AppMediaMusic;component/Images/defaultAlbumArt.jpg");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
