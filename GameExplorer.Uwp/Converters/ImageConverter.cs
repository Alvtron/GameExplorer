using System;
using System.Diagnostics;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;
using GameExplorer.Uwp.Utils;

namespace GameExplorer.Uwp.Converters
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class ImageConverter : IValueConverter
    {
        /// <summary>
        /// Converts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            switch (value)
            {
                case null:
                    return null;
                case byte[] bytes:
                    return ImageUtils.ByteArrayToBitmapImage(bytes);
                case Uri uri when string.IsNullOrWhiteSpace(uri.OriginalString):
                    return null;
                case Uri uri:
                    return new BitmapImage(uri);
                case Model.Map map when map?.Empty ?? true:
                    return null;
                case Model.Map map:
                    return ImageUtils.ByteArrayToBitmapImage(map.ImageInBytes);
                case Model.Image image when image.Empty:
                    return null;
                case Model.Image image:
                    return ImageUtils.ByteArrayToBitmapImage(image.ImageInBytes);
                case Model.User user when user.Photo?.Empty ?? true:
                    return null;
                case Model.User user:
                    return ImageUtils.ByteArrayToBitmapImage(user.Photo.ImageInBytes);
            }

            return null;
        }

        /// <summary>
        /// Converts the back.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">Type of the target.</param>
        /// <param name="parameter">The parameter.</param>
        /// <param name="language">The language.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        
    }
}
