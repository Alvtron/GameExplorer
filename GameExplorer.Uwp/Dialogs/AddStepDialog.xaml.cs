using GameExplorer.Model;
using GameExplorer.Uwp.Utils;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameExplorer.Uwp.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class AddStepDialog : ContentDialog
    {
        /// <summary>
        /// Gets the step data.
        /// </summary>
        /// <value>
        /// The step data.
        /// </value>
        public Step StepData { get; } = new Step();

        /// <summary>
        /// Initializes a new instance of the <see cref="AddStepDialog" /> class.
        /// </summary>
        public AddStepDialog()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Click event of the BoldButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void BoldButton_Click(object sender, RoutedEventArgs e)
        {
            RichEditBoxExtended.Bold(AboutBox);
        }

        /// <summary>
        /// Handles the Click event of the ItalicButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void ItalicButton_Click(object sender, RoutedEventArgs e)
        {
            RichEditBoxExtended.Italic(AboutBox);
        }

        /// <summary>
        /// Handles the Click event of the UnderlineButton control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private void UnderlineButton_Click(object sender, RoutedEventArgs e)
        {
            RichEditBoxExtended.Underline(AboutBox);
        }

        /// <summary>
        /// Handles the Click event of the AttachScreenshot control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> instance containing the event data.</param>
        private async void AttachScreenshot_Click(object sender, RoutedEventArgs e)
        {
            var image = await ImageUtils.PickSingleImage();
            StepData.Screenshot = image;
        }
    }
}
