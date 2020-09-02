using GameExplorer.Model;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using GameExplorer.Uwp.Dialogs;

namespace GameExplorer.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Uwp.ViewModels.PostViewModel" />
    public class GameViewModel : PostViewModel
    {
        /// <summary>
        /// The add or remove game from user command
        /// </summary>
        private RelayCommand _addOrRemoveGameFromUserCommand;
        /// <summary>
        /// Gets the add or remove game from user command.
        /// </summary>
        /// <value>
        /// The add or remove game from user command.
        /// </value>
        public ICommand AddOrRemoveGameFromUserCommand => _addOrRemoveGameFromUserCommand = _addOrRemoveGameFromUserCommand ?? new RelayCommand(async (param) => await AddOrRemoveGameFromUserAsync());

        /// <summary>
        /// The game
        /// </summary>
        private Game _game = new Game();
        /// <summary>
        /// Gets or sets the game.
        /// </summary>
        /// <value>
        /// The game.
        /// </value>
        public Game Game
        {
            get => _game;
            set
            {
                if (SetField(ref _game, value))
                {
                    OnPropertyChanged(nameof(_game));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether [game in my library].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [game in my library]; otherwise, <c>false</c>.
        /// </value>
        public bool GameInMyLibrary => MainViewReference.CurrentUser.Games.Any(g => g.Uid == Game.Uid);


        /// <summary>
        /// Adds the or remove game from user asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task AddOrRemoveGameFromUserAsync()
        {
            if (Game == null) return;

            MainViewReference.CurrentUser.ToggleAddGame(Game);
            await MainViewReference.Database.Upload(MainViewReference.Database.CurrentUser);
            OnPropertyChanged(nameof(GameInMyLibrary));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameViewModel"/> class.
        /// </summary>
        public GameViewModel()
        {
            Game = new Game(MainViewReference.CurrentUser);
            Game.PropertyChanged += (s, e) => { Changed = true; };
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public override async Task Create()
        {

        }

        /// <summary>
        /// Edits this instance.
        /// </summary>
        public override void Edit()
        {
            NavigationService.NavigateTo(typeof(AddGamePage), Game);
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public override void Reset()
        {
            // TODO: Implement resetting
        }

        /// <summary>
        /// Saves the changes asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task<bool> SaveChangesAsync()
        {
            if (Game == null || !Game.Valid)
            {
                await NotifyUtils.DisplayErrorMessage("Some changes are not valid.");
                return false;
            }

            Lock = true;

            if (await MainViewReference.Database.Upload(Game) == DatabaseService.Response.Failed)
            {
                await NotifyUtils.DisplayErrorMessage("An error occurred during the upload. No changes where made.");
                return Lock = false; ;
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
            Game?.SetPhoto(MainViewReference.CurrentUser, image);
            Lock = false;
            Changed = true;
        }

        /// <summary>
        /// Uploads the banner asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task UploadBannerAsync()
        {
            var image = await ImageUtils.PickSingleImage();
            if (image == null) return;

            Lock = true;
            Game?.SetBanner(MainViewReference.CurrentUser, image);
            Lock = false;
            Changed = true;
        }

        /// <summary>
        /// Uploads the screenshots asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task UploadScreenshotsAsync()
        {
            var images = await ImageUtils.PickMultipleImages();
            if (images?.Count == 0) return;

            Lock = true;
            Game?.AddScreenshots(MainViewReference.CurrentUser, images);
            Lock = false;
            Changed = true;
        }

        /// <summary>
        /// Uploads the video asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task UploadVideoAsync()
        {
            var dialog = new AddVideoDialog();
            Lock = true;

            if (await dialog.ShowAsync() != ContentDialogResult.Secondary || dialog.VideoData == null)
            {
                await NotifyUtils.DisplayErrorMessage("Video was invalid");
                Lock = false;
                return;
            }

            Game?.AddVideo(MainViewReference.CurrentUser, dialog.VideoData);
            Lock = false;
            Changed = true;
        }

        /// <summary>
        /// Uploads the comment asynchronous.
        /// </summary>
        /// <param name="editor">The editor.</param>
        /// <returns></returns>
        public override async Task UploadCommentAsync(RichEditBoxExtended editor)
        {
            if (MainViewReference.CurrentUser == null || !MainViewReference.CurrentUser.Valid)
            {
                await NotifyUtils.DisplayErrorMessage("You are not logged in!");
                return;
            }

            if (editor == null || string.IsNullOrWhiteSpace(editor.RtfText))
            {
                await NotifyUtils.DisplayErrorMessage("Comment was invalid");
                return;
            }

            Lock = true;
            var comment = new Comment(MainViewReference.CurrentUser, editor.RtfText);
            Game?.AddComment(comment);

            if (await MainViewReference.Database.Upload(Game) == DatabaseService.Response.Failed)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong when uploading your comment.");
                Game?.Comments.Remove(comment);
                Lock = false;
                return;
            }
            Lock = false;
            Changed = true;
        }

        /// <summary>
        /// Views the video.
        /// </summary>
        /// <param name="video">The video.</param>
        public override void ViewVideo(Video video)
        {
            NavigationService.NavigateTo(typeof(MediaPage), video);
        }

        /// <summary>
        /// Views the image.
        /// </summary>
        /// <param name="image">The image.</param>
        public override void ViewImage(Model.Image image)
        {
            NavigationService.NavigateTo(typeof(MediaPage), image);
        }

        /// <summary>
        /// Deletes the screenshot.
        /// </summary>
        /// <param name="screenshot">The screenshot.</param>
        public override void DeleteScreenshot(Model.Image screenshot)
        {
            Game?.Screenshots.Remove(screenshot);
            Changed = true;
        }

        /// <summary>
        /// Deletes the video.
        /// </summary>
        /// <param name="video">The video.</param>
        public override void DeleteVideo(Video video)
        {
            Game?.Videos.Remove(video);
            Changed = true;
        }

        /// <summary>
        /// Likes the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public override async Task LikeComment(Comment comment)
        {
            comment = Game?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

            if (comment == null)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong. Try again later.");
                return;
            }

            comment.Like(MainViewReference.CurrentUser);
            await SaveChangesAsync();
        }

        /// <summary>
        /// Replies the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public override async Task ReplyComment(Comment comment)
        {
            comment = Game?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

            if (comment == null)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong. Try again later.");
                return;
            }

            await NotifyUtils.DisplayErrorMessage("This is not implemented.");
        }

        /// <summary>
        /// Shares the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public override async Task ShareComment(Comment comment)
        {
            comment = Game?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

            if (comment == null)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong. Try again later.");
                return;
            }

            await NotifyUtils.DisplayErrorMessage("This is not implemented.");
        }

        /// <summary>
        /// Reports the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public override async Task ReportComment(Comment comment)
        {
            comment = Game?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

            if (comment == null)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong. Try again later.");
                return;
            }

            var reportDialog = new ReportDialog("comment by " + comment.User?.Username);

            if (await reportDialog.ShowAsync() != ContentDialogResult.Secondary || !reportDialog.Valid)
            {
                return;
            }
            await MainViewReference.Database.Add(new Report(Game.Uid, MainViewReference.CurrentUser, reportDialog.Message));
            await NotifyUtils.DisplayThankYouMessage("Thanks for contributing to a nicer and safer community! You rock!");
        }

        /// <summary>
        /// Reports the asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task ReportAsync()
        {
            if (MainViewReference.CurrentUser == null || Game == null)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong. Try again later.");
                return;
            }

            var reportDialog = new ReportDialog(Game?.Title);

            if (await reportDialog.ShowAsync() != ContentDialogResult.Secondary || !reportDialog.Valid)
            {
                return;
            }

            await MainViewReference.Database.Add(new Report(Game.Uid, MainViewReference.CurrentUser, reportDialog.Message));
            await NotifyUtils.DisplayThankYouMessage("Thanks for contributing to a nicer and safer community! You rock!");
        }
    }
}
