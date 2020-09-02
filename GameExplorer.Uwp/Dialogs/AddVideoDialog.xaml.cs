using GameExplorer.Model;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameExplorer.Uwp.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class AddVideoDialog : ContentDialog
    {
        /// <summary>
        /// Gets the video data.
        /// </summary>
        /// <value>
        /// The video data.
        /// </value>
        public Video VideoData { get; } = new Video();

        /// <summary>
        /// Initializes a new instance of the <see cref="AddVideoDialog"/> class.
        /// </summary>
        public AddVideoDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Contents the dialog primary button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// Contents the dialog secondary button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="ContentDialogButtonClickEventArgs"/> instance containing the event data.</param>
        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        /// <summary>
        /// Handles the Click event of the ViewVideoButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void ViewVideoButton_Click(object sender, RoutedEventArgs e)
        {
            VideoPlayer.Visibility = Visibility.Visible;
            VideoPlayer.Source = VideoData.YouTubeUri;
        }
    }
}
