using GameExplorer.Model;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace GameExplorer.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class AddGamePage : Page
    {
        /// <summary>
        /// The view model
        /// </summary>
        public GameViewModel ViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddGamePage"/> class.
        /// </summary>
        public AddGamePage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = new GameViewModel { Lock = true };

            switch (e.Parameter)
            {
                case Game game:
                    ViewModel.Game = game;
                    NavigationService.SetHeaderTitle("Edit " + game.Title);
                    break;
                case null:
                    NavigationService.SetHeaderTitle("Add new game");
                    break;
                default:
                    Frame.GoBack();
                    return;
            }
            await ViewModel?.Create();
            ViewModel.Lock = false;
        }
    }
}
