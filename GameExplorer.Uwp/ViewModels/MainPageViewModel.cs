using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using GameExplorer.Model;
using GameExplorer.Uwp.Data;
using GameExplorer.Uwp.Dialogs;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.Views;

namespace GameExplorer.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Uwp.ViewModels.BaseViewModel" />
    public class MainPageViewModel : BaseViewModel
    {

        /// <summary>
        /// The header title
        /// </summary>
        private string _headerTitle;
        /// <summary>
        /// Gets or sets the header title.
        /// </summary>
        /// <value>
        /// The header title.
        /// </value>
        public string HeaderTitle
        {
            get => _headerTitle;
            set
            {
                if (SetField(ref _headerTitle, value))
                {
                    OnPropertyChanged(nameof(_headerTitle));
                }
            }
        }

        /// <summary>
        /// The searchable posts
        /// </summary>
        private List<SearchablePost> _searchablePosts;
        /// <summary>
        /// Gets or sets the search results.
        /// </summary>
        /// <value>
        /// The search results.
        /// </value>
        public List<SearchablePost> SearchResults
        {
            get => _searchablePosts;
            set
            {
                if (SetField(ref _searchablePosts, value))
                {
                    OnPropertyChanged(nameof(_searchablePosts));
                }
            }
        }

        /// <summary>
        /// The back button visible
        /// </summary>
        private bool _backButtonVisible;
        /// <summary>
        /// Gets or sets a value indicating whether [back button visible].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [back button visible]; otherwise, <c>false</c>.
        /// </value>
        public bool BackButtonVisible
        {
            get => _backButtonVisible;
            set
            {
                if (SetField(ref _backButtonVisible, value))
                {
                    OnPropertyChanged(nameof(_backButtonVisible));
                }
            }
        }

        /// <summary>
        /// Updates the current user.
        /// </summary>
        /// <returns></returns>
        public async Task UpdateCurrentUser()
        {
            Lock = true;

            await MainViewReference.Database.LoadCurrentUser();

            Lock = false;
        }

        /// <summary>
        /// Searches the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        public void Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return;

            //Check each item in searchlist if it contains the query
            SearchResults = MainViewReference.Database.CombineAll().Where(x => x.Title != null && x.Title.ToLower().Contains(query.ToLower())).ToList();
        }

        /// <summary>
        /// Submits the search.
        /// </summary>
        /// <param name="args">The <see cref="AutoSuggestBoxQuerySubmittedEventArgs"/> instance containing the event data.</param>
        public void SubmitSearch(AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            if (args.ChosenSuggestion != null)
            {
                var item = (SearchablePost)args.ChosenSuggestion;

                switch (item)
                {
                    case Quest _:
                        NavigationService.NavigateTo(typeof(QuestPage), item.Uid);
                        break;
                    case Game _:
                        NavigationService.NavigateTo(typeof(GamePage), item.Uid);
                        break;
                    case Wiki _:
                        NavigationService.NavigateTo(typeof(WikiPage), item.Uid);
                        break;
                }
            }
            else
            {
                // No match. Navigate to searchpage and list appropriate results for the search-query
                NavigationService.NavigateTo(typeof(SearchPage), args.QueryText);
            }
        }

        /// <summary>
        /// The navigate to sign in page command
        /// </summary>
        private RelayCommand _navigateToSignInPageCommand;
        /// <summary>
        /// Gets the navigate to sign in page command.
        /// </summary>
        /// <value>
        /// The navigate to sign in page command.
        /// </value>
        public ICommand NavigateToSignInPageCommand => _navigateToSignInPageCommand = _navigateToSignInPageCommand ?? new RelayCommand(async param => await NavigateToSignInPage());
        /// <summary>
        /// Navigates to sign in page.
        /// </summary>
        /// <returns></returns>
        public async Task NavigateToSignInPage()
        {
            var signInDialog = new SignInDialog();
            await signInDialog.ShowAsync();
        }

        /// <summary>
        /// The sign out command command
        /// </summary>
        private RelayCommand _signOutCommandCommand;
        /// <summary>
        /// Gets the sign out command.
        /// </summary>
        /// <value>
        /// The sign out command.
        /// </value>
        public ICommand SignOutCommand => _signOutCommandCommand = _signOutCommandCommand ?? new RelayCommand(async param => await SignOut());
        /// <summary>
        /// Represents an event that is raised when the sign-out operation is complete.
        /// </summary>
        /// <returns></returns>
        public async Task SignOut()
        {
            Lock = true; 
            await MainViewReference.Database.SignOut();
            Settings.RemoveDefaultUser();
            Lock = false;
        }

        /// <summary>
        /// Updates the back button visibillity.
        /// </summary>
        public void UpdateBackButtonVisibillity()
        {
            BackButtonVisible = NavigationService.CanGoBack;
        }

        /// <summary>
        /// The go back command
        /// </summary>
        private RelayCommand _goBackCommand;
        /// <summary>
        /// Gets the go back command.
        /// </summary>
        /// <value>
        /// The go back command.
        /// </value>
        public ICommand GoBackCommand => _goBackCommand = _goBackCommand ?? new RelayCommand(param => GoBack());
        /// <summary>
        /// Goes the back.
        /// </summary>
        public void GoBack()
        {
            NavigationService.NavigateBack();
        }

        /// <summary>
        /// The go to my profile command
        /// </summary>
        private RelayCommand _goToMyProfileCommand;
        /// <summary>
        /// Gets the go to my profile command.
        /// </summary>
        /// <value>
        /// The go to my profile command.
        /// </value>
        public ICommand GoToMyProfileCommand => _goToMyProfileCommand = _goToMyProfileCommand ?? new RelayCommand(param => GoToMyProfile());
        /// <summary>
        /// Goes to my profile.
        /// </summary>
        public void GoToMyProfile()
        {
            if (MainViewReference.Database.CurrentUser != null)
                NavigationService.NavigateTo(typeof(UserPage), MainViewReference.Database.CurrentUser);
        }

        /// <summary>
        /// The go to my account settings command
        /// </summary>
        private RelayCommand _goToMyAccountSettingsCommand;
        /// <summary>
        /// Gets the go to my account settings command.
        /// </summary>
        /// <value>
        /// The go to my account settings command.
        /// </value>
        public ICommand GoToMyAccountSettingsCommand => _goToMyAccountSettingsCommand = _goToMyAccountSettingsCommand ?? new RelayCommand(param => GoToMyAccountSettings());
        /// <summary>
        /// Goes to my account settings.
        /// </summary>
        public void GoToMyAccountSettings()
        {
            if (MainViewReference.Database.CurrentUser != null)
                NavigationService.NavigateTo(typeof(AccountSettingsPage), MainViewReference.Database.CurrentUser);
        }

        /// <summary>
        /// The go to search page command
        /// </summary>
        private RelayCommand<string> _goToSearchPageCommand;
        /// <summary>
        /// Gets the go to search page command.
        /// </summary>
        /// <value>
        /// The go to search page command.
        /// </value>
        public ICommand GoToSearchPageCommand => _goToSearchPageCommand = _goToSearchPageCommand ?? new RelayCommand<string>(param => GoToSearchPage(param));
        /// <summary>
        /// Goes to search page.
        /// </summary>
        /// <param name="query">The query.</param>
        public void GoToSearchPage(string query = null)
        {
            NavigationService.NavigateTo(typeof(SearchPage), query);
        }

        /// <summary>
        /// The set current game command
        /// </summary>
        private RelayCommand<Game> _setCurrentGameCommand;
        /// <summary>
        /// Gets the set current game command.
        /// </summary>
        /// <value>
        /// The set current game command.
        /// </value>
        public ICommand SetCurrentGameCommand => _setCurrentGameCommand = _setCurrentGameCommand ?? new RelayCommand<Game>(param => SetCurrentGame(param));
        /// <summary>
        /// Sets the current game.
        /// </summary>
        /// <param name="game">The game.</param>
        public void SetCurrentGame(Game game)
        {
            if (game == null) return;
            Lock = true;

            Settings.SetDefaultGame(game);
            MainViewReference.Database.LoadCurrentGame();
            NavigationService.ClearBackStack();
            NavigationService.NavigateTo(typeof(HomePage));

            Lock = false;
        }
    }
}
