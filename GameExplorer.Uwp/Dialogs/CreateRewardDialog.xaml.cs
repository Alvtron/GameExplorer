using System.Collections.Generic;
using GameExplorer.Model;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameExplorer.Uwp.Services;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace GameExplorer.Uwp.Dialogs
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="Windows.UI.Xaml.Controls.ContentDialog" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector" />
    /// <seealso cref="Windows.UI.Xaml.Markup.IComponentConnector2" />
    public sealed partial class CreateRewardDialog : ContentDialog
    {
        /// <summary>
        /// Gets the reward.
        /// </summary>
        /// <value>
        /// The reward.
        /// </value>
        public Reward Reward { get; } = new Reward();
        /// <summary>
        /// The reward types
        /// </summary>
        public List<string> RewardTypes;
        /// <summary>
        /// Gets a value indicating whether [valid result].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [valid result]; otherwise, <c>false</c>.
        /// </value>
        public bool ValidResult => Reward.Amount > 0 && !string.IsNullOrWhiteSpace(Reward.Type);

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateRewardDialog"/> class.
        /// </summary>
        public CreateRewardDialog()
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
        /// Games the asb text changed.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="AutoSuggestBoxTextChangedEventArgs"/> instance containing the event data.</param>
        private void GameASB_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            if (args.Reason == AutoSuggestionBoxTextChangeReason.UserInput)
            {
                //Check each item in searchlist if it contains the query
                sender.ItemsSource = RewardTypes.Where(x => x != null && x.ToLower().Contains(sender.Text.ToLower())).ToList();
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsSecondaryButtonEnabled = int.TryParse(AmountBox.Text, out var number) && number > 0;
        }

        /// <summary>
        /// Handles the OnLoaded event of the TypeASB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void TypeASB_OnLoaded(object sender, RoutedEventArgs e)
        {
            RewardTypes = await DatabaseService.GetRewardTypes();
        }
    }
}
