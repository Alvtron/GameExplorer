using GameExplorer.Model;
using GameExplorer.Uwp.Data;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.ViewModels;
using System;
using Windows.Foundation;
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
    public sealed partial class QuestPage : Page
    {
        /// <summary>
        /// The view model
        /// </summary>
        public QuestViewModel ViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestPage"/> class.
        /// </summary>
        public QuestPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.</param>
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = new QuestViewModel { Lock = true };
            switch (e.Parameter)
            {
                case Guid guid:
                    ViewModel.Quest = await MainViewReference.Database.GetQuest(guid);
                    NavigationService.SetHeaderTitle(ViewModel.Quest.Title);
                    break;
                case Quest quest:
                    ViewModel.Quest = quest;
                    NavigationService.SetHeaderTitle(ViewModel.Quest.Title);
                    break;
                case SearchablePost post:
                    NavigationService.SetHeaderTitle(ViewModel.Quest.Title);
                    ViewModel.Quest = await MainViewReference.Database.GetQuest(post.Uid);
                    break;
                default:
                    NavigationService.NavigateBack();
                    return;
            }

            ViewModel.Lock = false;

            NavigationService.SetHeaderTitle(ViewModel.Quest.Title);
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
            var map = (Canvas)sender;

            if (map == null)
                return;



            foreach (var coordinate in ViewModel.Quest.Map.Coordinates)
            {
                var point = new Ellipse
                {
                    Fill = new SolidColorBrush(Colors.Red),
                    Height = 4,
                    Width = 4
                };
                Canvas.SetLeft(point, coordinate.X);
                Canvas.SetTop(point, coordinate.Y);
                map.Children.Add(point);

                var txt = new TextBlock
                {
                    FontSize = 12,
                    Foreground = new SolidColorBrush(Colors.Red),
                    Text = "Point label"
                };
                Canvas.SetLeft(txt, coordinate.X - (FrameworkElementWidt(txt) / 2.0));
                Canvas.SetTop(txt, coordinate.Y + 5);
                map.Children.Add(txt);
            }
        }

        /// <summary>
        /// Frameworks the element widt.
        /// </summary>
        /// <param name="textBlock">The text block.</param>
        /// <returns></returns>
        private static double FrameworkElementWidt(FrameworkElement textBlock)
        {
            textBlock.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
            return textBlock.ActualWidth;
        }
    }
}
