using GameExplorer.Model;
using GameExplorer.Uwp.Data;
using System.Collections.ObjectModel;
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
    public sealed partial class CreateQuestLineDialog : ContentDialog
    {
        /// <summary>
        /// Gets the questline.
        /// </summary>
        /// <value>
        /// The questline.
        /// </value>
        public Questline Questline { get; } = new Questline();
        /// <summary>
        /// The games
        /// </summary>
        private ObservableCollection<SearchablePost> _games;
        /// <summary>
        /// Gets a value indicating whether [valid result].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [valid result]; otherwise, <c>false</c>.
        /// </value>
        public bool ValidResult => !string.IsNullOrWhiteSpace(Questline.Title) && Questline.Game != null;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateQuestLineDialog"/> class.
        /// </summary>
        public CreateQuestLineDialog()
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
        /// Games the asb query submitted.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="AutoSuggestBoxQuerySubmittedEventArgs"/> instance containing the event data.</param>
        private void GameASB_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            sender.IsSuggestionListOpen = false;
            
            if (args?.ChosenSuggestion is SearchablePost game)
            {
                Questline.Game = game as Game;
            }
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
                sender.ItemsSource = _games.Where(x => x.Title.ToLower().Contains(sender.Text.ToLower())).ToList();
                // TODO: Implement better search algorithm
            }
        }

        /// <summary>
        /// Handles the TextChanged event of the TextBox control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="TextChangedEventArgs"/> instance containing the event data.</param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            IsSecondaryButtonEnabled = !string.IsNullOrWhiteSpace(TitleBox.Text);
        }

        /// <summary>
        /// Handles the OnLoaded event of the GameASB control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private async void GameASB_OnLoaded(object sender, RoutedEventArgs e)
        {
            _games = await DatabaseService.GetGames();
        }
    }
}
