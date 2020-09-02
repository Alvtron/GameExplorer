using GameExplorer.Model;
using GameExplorer.Uwp.Services;
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
    public sealed partial class UserActivityPage : Page
    {
        /// <summary>
        /// The user data
        /// </summary>
        private User _userData;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserActivityPage"/> class.
        /// </summary>
        public UserActivityPage()
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

                NavigationService.SetHeaderTitle(user.Username + "'s activity");
            }
            else
            {
                Frame.GoBack();
            }
        }
    }
}
