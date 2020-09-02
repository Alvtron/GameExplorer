using GameExplorer.Model;
using GameExplorer.Uwp.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Image = GameExplorer.Model.Image;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameExplorer.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class MediaPage : Page
    {
        /// <summary>
        /// The video
        /// </summary>
        private Video _video;
        /// <summary>
        /// The image
        /// </summary>
        private Image _image;

        /// <summary>
        /// Initializes a new instance of the <see cref="MediaPage"/> class.
        /// </summary>
        public MediaPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually the most relevant property to examine is Parameter.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            switch (e.Parameter)
            {
                case Image image:
                    _image = image;
                    break;
                case Video video:
                    _video = video;
                    break;
                default:
                    Frame.GoBack();
                    break;
            }
        }

        /// <summary>
        /// Handles the Loaded event of the ImageView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ImageView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_image == null || _image.Empty)
            {
                ImageView.Visibility = Visibility.Collapsed;
                return;
            }

            ImageView.Visibility = Visibility.Visible;
            ImageView.Source = ImageUtils.ByteArrayToBitmapImage(_image.ImageInBytes);
        }

        /// <summary>
        /// Handles the Loaded event of the VideoView control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void VideoView_Loaded(object sender, RoutedEventArgs e)
        {
            if (_video == null || _video.Empty)
            {
                VideoView.Visibility = Visibility.Collapsed;
                return;
            }

            VideoView.Visibility = Visibility.Visible;
            VideoView.Source = _video.YouTubeUri;
        }
    }
}
