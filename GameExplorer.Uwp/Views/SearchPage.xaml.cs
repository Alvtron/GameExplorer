using GameExplorer.Model;
using GameExplorer.Uwp.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
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
    public sealed partial class SearchPage : Page
    {
        /// <summary>
        /// The root
        /// </summary>
        private readonly MainPage _root;

        /// <summary>
        /// Initializes a new instance of the <see cref="SearchPage"/> class.
        /// </summary>
        public SearchPage()
        {
            InitializeComponent();
            _root = Window.Current?.Content as MainPage;
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is string query)
            {
                NavigationService.SetHeaderTitle("Results for: " + query);
                SearchBox.Text = query;
                Search(query);
            }
            else
            {
                NavigationService.SetHeaderTitle("Search");
            }
        }

        /// <summary>
        /// Searches the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        private void Search(string query)
        {
            GameList.ItemsSource = MainViewReference.Database.Games.Where(x => x.Title != null && x.Title.ToLower().Contains(query.ToLower())).ToList();
            QuestList.ItemsSource = MainViewReference.Database.Quests.Where(x => x.Title != null && x.Title.ToLower().Contains(query.ToLower())).ToList();
            WikiList.ItemsSource = MainViewReference.Database.Wikis.Where(x => x.Title != null && x.Title.ToLower().Contains(query.ToLower())).ToList();
        }

        /// <summary>
        /// Handles the ItemClick event of the GameList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        private void GameList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var game = (Game)e.ClickedItem;
            Frame.Navigate(typeof(GamePage), game.Uid);
        }

        /// <summary>
        /// Handles the ItemClick event of the QuestList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        private void QuestList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var quest = (Quest)e.ClickedItem;
            Frame.Navigate(typeof(QuestPage), quest.Uid);

        }

        /// <summary>
        /// Handles the ItemClick event of the WikiList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        private void WikiList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var wiki = (Wiki)e.ClickedItem;
            Frame.Navigate(typeof(WikiPage), wiki.Uid);
        }

        /// <summary>
        /// Handles the KeyDown event of the SearchBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyRoutedEventArgs"/> instance containing the event data.</param>
        private void SearchBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                Search(SearchBox.Text);
                NavigationService.SetHeaderTitle("Results for: " + SearchBox.Text);
            }
        }
    }
}
