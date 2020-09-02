using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameExplorer.Uwp.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.Page" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class SettingsPage : Page
    {
        /// <summary>
        /// The credentials list
        /// </summary>
        public ObservableCollection<string> CredentialsList = new ObservableCollection<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsPage"/> class.
        /// </summary>
        public SettingsPage()
        {
            InitializeComponent();

            UpdateCredentialList();
        }

        /// <summary>
        /// Handles the ItemClick event of the CredentialList control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemClickEventArgs"/> instance containing the event data.</param>
        private async void CredentialList_ItemClick(object sender, ItemClickEventArgs e)
        {
            var credential = (PasswordCredential)e.ClickedItem;

            if (credential == null) return;

            Settings.SetDefaultUser(credential.UserName);
            await NavigationService.UpdateCurrentUser();
        }

        /// <summary>
        /// Updates the credential list.
        /// </summary>
        private void UpdateCredentialList()
        {
            CredentialsList.Clear();

            foreach (var pc in CredentialService.GetAll())
                CredentialsList.Add(pc.UserName);

            CredentialList.SelectedItem = CredentialService.GetCurrent().UserName;
        }
    }
}
