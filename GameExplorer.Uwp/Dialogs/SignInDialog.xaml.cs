using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Security.Credentials;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using GameExplorer.Model;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.ViewModels;
using GameExplorer.Uwp.Views;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameExplorer.Uwp.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class SignInDialog : ContentDialog
    {
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        /// <value>
        /// The view model.
        /// </value>
        public SignInViewModel ViewModel { get; set; } = new SignInViewModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="SignInDialog"/> class.
        /// </summary>
        public SignInDialog()
        {
            if (MainViewReference.MainView == null)
                Hide();

            InitializeComponent();

            ViewModel.PropertyChanged += (s, e) => { if (ViewModel.CanClose) Hide();  };

        }
    }
}
