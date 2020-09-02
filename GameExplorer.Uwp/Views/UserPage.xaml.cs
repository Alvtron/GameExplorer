using GameExplorer.Model;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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
    public sealed partial class UserPage : Page
    {
        /// <summary>
        /// The view model
        /// </summary>
        private UserViewModel _viewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserPage"/> class.
        /// </summary>
        public UserPage()
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
                _viewModel = new UserViewModel(user);
                NavigationService.SetHeaderTitle(user.Username + "'s profile");
            }
            else
            {
                Frame.GoBack();
            }
        }
    }
}