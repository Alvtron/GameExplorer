using GameExplorer.Model;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameExplorer.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class UserGamesPage : Page
    {
        /// <summary>
        /// The user data
        /// </summary>
        private User _userData;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserGamesPage"/> class.
        /// </summary>
        public UserGamesPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is User user)
            {
                _userData = user;
                NavigationService.SetHeaderTitle(user.Username + "'s games");
            }
            else
            {
                Frame.GoBack();
            }
        }

        /// <summary>
        /// Handles the Loaded event of the RemoveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void RemoveButton_Loaded(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;

            if (button != null)
                button.Visibility = MainViewReference.Database.CurrentUser.Uid.ToString() == _userData.Uid.ToString() ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <summary>
        /// Handles the Click event of the RemoveButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var game = (Game)button.Tag;

            var dialog = new ContentDialog()
            {
                Title = "Remove '" + game.Title + "'",
                Content = "Are you sure you want to remove '" + game.Title + "' from your games?",
                PrimaryButtonText = "Remove it",
                CloseButtonText = "Cancel"
            };

            await dialog.ShowAsync();
        }
    }
}
