using GameExplorer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace GameExplorer.Uwp.Converters
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    public class VisibillityConverter : IValueConverter
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
            switch (value)
            {
                case null:
                    return Collapsed;
                case bool isVisible:
                    return (!isVisible) ? Collapsed : Visible;
                case User _:
                    return Visible;
                case SearchablePost _:
                    return Visible;
                case byte[] _:
                    return Visible;
                case Image _:
                    return Visible;
            }

            return Collapsed;
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