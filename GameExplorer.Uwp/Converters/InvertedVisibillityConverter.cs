using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GameExplorer.Uwp.Converters
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    class InvertedVisibillityConverter : IValueConverter
    {
        /// <summary>
        /// The visible
        /// </summary>
        private const Visibility Visible = Visibility.Visible;
        /// <summary>
        /// The collapsed
        /// </summary>
        private const Visibility Collapsed = Visibility.Collapsed;

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
            if (value is bool boolean)
            {
                return (boolean) ? Collapsed : Visible;
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
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is Visibility visibility)
            {
                return (visibility != Visible);
            }

            return null;
        }
    }
}
