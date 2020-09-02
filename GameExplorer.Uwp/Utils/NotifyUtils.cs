using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace GameExplorer.Uwp.Utils
{
    /// <summary>
    /// 
    /// </summary>
    public static class NotifyUtils
    {
        /// <summary>
        /// Displays the general message.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        /// <param name="buttonText">The button text.</param>
        /// <returns></returns>
        public static async Task<ContentDialogResult> DisplayGeneralMessage(string title, string message, string buttonText)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                PrimaryButtonText = buttonText
            };

            return await dialog.ShowAsync();
        }

        /// <summary>
        /// Displays the error message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static async Task<ContentDialogResult> DisplayErrorMessage(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Woah, there!",
                Content = message,
                PrimaryButtonText = "OK"
            };

            return await dialog.ShowAsync();
        }

        /// <summary>
        /// Displays the thank you message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static async Task<ContentDialogResult> DisplayThankYouMessage(string message)
        {
            var dialog = new ContentDialog
            {
                Title = "Thank you!",
                Content = message,
                PrimaryButtonText = "OK"
            };

            return await dialog.ShowAsync();
        }
    }
}
