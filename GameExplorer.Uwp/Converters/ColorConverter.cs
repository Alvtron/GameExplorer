using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Markup;
using Windows.UI.Xaml.Media;

namespace GameExplorer.Uwp.Converters
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Data.IValueConverter" />
    class ColorConverter : IValueConverter
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
            if (value is Model.Color color)
            {
                return new SolidColorBrush(Color.FromArgb(color.A, color.R, color.G, color.B));
            }

            return (SolidColorBrush)Application.Current.Resources["GameExplorerColor"];
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
            switch (value)
            {
                case Color color:
                    return new Model.Color(color.R, color.G, color.B, color.A);
                case SolidColorBrush brush:
                    return new Model.Color(brush.Color.R, brush.Color.G, brush.Color.B, brush.Color.A);
            }

            return null;
        }
    }
}
