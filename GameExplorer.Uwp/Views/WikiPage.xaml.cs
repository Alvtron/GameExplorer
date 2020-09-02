using GameExplorer.Model;
using GameExplorer.Uwp.Data;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.ViewModels;
using System;
using System.Drawing;
using System.Linq;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameExplorer.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class WikiPage : Page
    {
        /// <summary>
        /// The view model
        /// </summary>
        public WikiViewModel ViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="WikiPage"/> class.
        /// </summary>
        public WikiPage()
        {
            InitializeComponent();
        }


        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = new WikiViewModel() { Lock = true };
            switch (e.Parameter)
            {
                case Guid guid:
                    ViewModel.Wiki = await MainViewReference.Database.GetWiki(guid);
                    NavigationService.SetHeaderTitle(ViewModel.Wiki.Title);
                    break;
                case Wiki wiki:
                    ViewModel.Wiki = wiki;
                    NavigationService.SetHeaderTitle(ViewModel.Wiki.Title);
                    break;
                case SearchablePost post:
                    NavigationService.SetHeaderTitle(ViewModel.Wiki.Title);
                    ViewModel.Wiki = await MainViewReference.Database.GetWiki(post.Uid);
                    break;
                default:
                    NavigationService.NavigateBack();
                    return;
            }

            ViewModel.Lock = false;

            NavigationService.SetHeaderTitle(ViewModel.Wiki.Title);
            await ViewModel?.Create();
        }

        // TODO: CREATE GREAT GAME MAP HANDLER THINGY
        /// <summary>
        /// Handles the Loaded event of the Map control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void Map_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
