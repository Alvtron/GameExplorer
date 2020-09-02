using GameExplorer.Model;
using GameExplorer.Uwp.Data;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.ViewModels;
using System;
using System.Diagnostics;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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
    public sealed partial class GamePage : Page
    {
        /// <summary>
        /// The view model
        /// </summary>
        public GameViewModel ViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="GamePage"/> class.
        /// </summary>
        public GamePage()
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
                case Guid guid:
                    ViewModel.Game = await MainViewReference.Database.GetGame(guid);
                    NavigationService.SetHeaderTitle(ViewModel.Game?.Title);
                    break;
                case Game game:
                    ViewModel.Game = game;
                    NavigationService.SetHeaderTitle(ViewModel.Game?.Title);
                    break;
                case SearchablePost post:
                    NavigationService.SetHeaderTitle(ViewModel.Game?.Title);
                    ViewModel.Game = await MainViewReference.Database.GetGame(post.Uid);
                    break;
                default:
                    NavigationService.NavigateBack();
                    return;
            }

            ViewModel.Lock = false;
            await ViewModel?.Create();
        }
    }
}
