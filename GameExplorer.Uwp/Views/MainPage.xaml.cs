using GameExplorer.Model;
using GameExplorer.Uwp.Data;
using GameExplorer.Uwp.Utils;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.ViewModels;

namespace GameExplorer.Uwp.Views
{
    /// <summary>
    /// MainPage that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// Gets the navigation frame.
        /// </summary>
        /// <value>
        /// The navigation frame.
        /// </value>
        public Frame NavigationFrame => ContentFrame;
        /// <summary>
        /// Gets or sets the database.
        /// </summary>
        /// <value>
        /// The database.
        /// </value>
        public DatabaseService Database { get; set; }
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public MainPageViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage" /> class.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();
            ViewModel = new MainPageViewModel { Lock = true };
            Database = new DatabaseService();
        }

        /// <summary>
        /// Handles the Loaded event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await Database.LoadDatabase();

            //foreach (var game in StaticTestData.Games)
            //{
            //    await ViewModel.Database.Add(game);
            //}

            ViewModel.Lock = false;
        }

        /// <summary>
        /// Handles the Loaded event of the NavView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void NavView_Loaded(object sender, RoutedEventArgs e)
        {
            ContentFrame.Navigate(typeof(HomePage));
        }

        /// <summary>
        /// Navs the view item invoked.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NavigationViewItemInvokedEventArgs"/> instance containing the event data.</param>
        private void NavView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            if (args.IsSettingsInvoked)
            {
                ContentFrame.Navigate(typeof(SettingsPage));
                NavView.Header = "Settings";
            }
            else
            {
                NavView_Navigate(sender.MenuItems.OfType<NavigationViewItem>().First(x => (string)x.Content == (string)args.InvokedItem));
            }
        }

        /// <summary>
        /// Navs the view navigate.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <exception cref="ArgumentNullException">item</exception>
        public void NavView_Navigate(NavigationViewItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            switch (item.Tag)
            {
                case "home":
                    NavigationService.NavigateTo(typeof(HomePage));
                    break;
                case "search":
                    NavigationService.NavigateTo(typeof(SearchPage));
                    break;
                case "createGame":
                    NavigationService.NavigateTo(typeof(AddGamePage));
                    break;
                case "myGames":
                    NavigationService.NavigateTo(typeof(UserGamesPage), Database.CurrentUser);
                    break;
                case "myFriends":
                    NavigationService.NavigateTo(typeof(UserFriendsPage), Database.CurrentUser);
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Asbs the text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="AutoSuggestBoxTextChangedEventArgs"/> instance containing the event data.</param>
        public void ASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
                ViewModel.Search(sender.Text.ToLower());
        }

        /// <summary>
        /// Asbs the query submitted.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="AutoSuggestBoxQuerySubmittedEventArgs"/> instance containing the event data.</param>
        private void ASB_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            sender.IsSuggestionListOpen = false;
            ViewModel.SubmitSearch(args);
        }
    }
}
