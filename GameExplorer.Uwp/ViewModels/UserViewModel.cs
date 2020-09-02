using GameExplorer.Model;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.Views;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using GameExplorer.Uwp.Dialogs;

namespace GameExplorer.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Uwp.ViewModels.EditorViewModel" />
    public class UserViewModel : EditorViewModel
    {
        /// <summary>
        /// Gets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; }

        /// <summary>
        /// The befriend command
        /// </summary>
        private RelayCommand _befriendCommand;
        /// <summary>
        /// Gets the befriend command.
        /// </summary>
        /// <value>
        /// The befriend command.
        /// </value>
        public ICommand BefriendCommand => _befriendCommand = _befriendCommand ?? new RelayCommand(async param => await Befriend());

        /// <summary>
        /// The see all friends command
        /// </summary>
        private RelayCommand _seeAllFriendsCommand;
        /// <summary>
        /// Gets the see all friends command.
        /// </summary>
        /// <value>
        /// The see all friends command.
        /// </value>
        public ICommand SeeAllFriendsCommand => _seeAllFriendsCommand = _seeAllFriendsCommand ?? new RelayCommand(param => SeeAllFriends());

        /// <summary>
        /// The see all games command
        /// </summary>
        private RelayCommand _seeAllGamesCommand;
        /// <summary>
        /// Gets the see all games command.
        /// </summary>
        /// <value>
        /// The see all games command.
        /// </value>
        public ICommand SeeAllGamesCommand => _seeAllGamesCommand = _seeAllGamesCommand ?? new RelayCommand(param => SeeAllGames());

        /// <summary>
        /// The see all activity command
        /// </summary>
        private RelayCommand _seeAllActivityCommand;
        /// <summary>
        /// Gets the see all activity command.
        /// </summary>
        /// <value>
        /// The see all activity command.
        /// </value>
        public ICommand SeeAllActivityCommand => _seeAllActivityCommand = _seeAllActivityCommand ?? new RelayCommand(param => SeeAllActivity());

        /// <summary>
        /// The create new color command
        /// </summary>
        private RelayCommand _createNewColorCommand;
        /// <summary>
        /// Gets the create new color command.
        /// </summary>
        /// <value>
        /// The create new color command.
        /// </value>
        public ICommand CreateNewColorCommand => _createNewColorCommand = _createNewColorCommand ?? new RelayCommand(async param => await CreateNewColor());

        /// <summary>
        /// Initializes a new instance of the <see cref="UserViewModel"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public UserViewModel(User user)
        {
            User = user;
            User.PropertyChanged += (s, e) => { Changed = true; };
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override Task Create()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> SaveChangesAsync()
        {
            if (User == null || !User.Valid)
            {
                await NotifyUtils.DisplayErrorMessage("Some changes are not valid.");
                return false;
            }

            Lock = true;

            if (await MainViewReference.Database.Upload(User) == DatabaseService.Response.Failed)
            {
                await NotifyUtils.DisplayErrorMessage("An error occurred during the upload. No changes where made.");
                Lock = false;
                return false;
            }
            Lock = false;
            Changed = false;
            return true;
        }

        /// <summary>
        /// Saves the changes and navigate back.
        /// </summary>
        /// <returns></returns>
        public override async Task SaveChangesAndNavigateBack()
        {
            if (!await SaveChangesAsync()) return;
            NavigationService.NavigateBack();
        }

        /// <summary>
        /// Uploads the photo asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task UploadPhotoAsync()
        {
            var image = await ImageUtils.PickSingleImage();
            if (image == null) return;

            Lock = true;
            User?.SetPhoto(image);
            Lock = false;
        }

        /// <summary>
        /// Edits this instance.
        /// </summary>
        public override void Edit()
        {
            NavigationService.NavigateTo(typeof(AccountSettingsPage), User);
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public override void Reset()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reports the asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task ReportAsync()
        {
            if (MainViewReference.CurrentUser == null || User == null)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong. Try again later.");
                return;
            }

            if (MainViewReference.CurrentUser.Match(User))
            {
                await NotifyUtils.DisplayErrorMessage("You can't report yourself!");
                return;
            }

            var reportDialog = new ReportDialog(User.Username);

            if (await reportDialog.ShowAsync() != ContentDialogResult.Secondary || !reportDialog.Valid)
            {
                return;
            }

            await MainViewReference.Database.Add(new Report(User.Uid, MainViewReference.CurrentUser, reportDialog.Message));
            await NotifyUtils.DisplayThankYouMessage("Thanks for contributing to a nicer and safer community! You rock!");
        }

        /// <summary>
        /// Sees all friends.
        /// </summary>
        private void SeeAllFriends()
        {
            NavigationService.NavigateTo(typeof(UserFriendsPage), User);
        }

        /// <summary>
        /// Sees all games.
        /// </summary>
        public void SeeAllGames()
        {
            NavigationService.NavigateTo(typeof(UserGamesPage), User);
        }

        /// <summary>
        /// Sees all activity.
        /// </summary>
        public void SeeAllActivity()
        {
            NavigationService.NavigateTo(typeof(UserActivityPage), User);
        }

        /// <summary>
        /// Befriends this instance.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Befriend()
        {
            if (MainViewReference.CurrentUser == null || User == null)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong. Try again later.");
                return false;
            }

            if (MainViewReference.CurrentUser.Match(User))
            {
                await NotifyUtils.DisplayErrorMessage("You can't be friends with yourself!");
                return false;
            }

            MainViewReference.CurrentUser.AddFriend(User);
            await SaveChangesAsync();
            return true;
        }

        /// <summary>
        /// Creates the new color.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> CreateNewColor()
        {
            var dialog = new ColorPickerDialog(User.Color);

            if (await dialog.ShowAsync() != ContentDialogResult.Secondary)
            {
                return false;
            }

            User.SetColor(dialog.Color.R, dialog.Color.G, dialog.Color.B, dialog.Color.A);
            return true;
        }
    }
}
