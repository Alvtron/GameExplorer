using GameExplorer.Model;
using GameExplorer.Uwp.Data;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.Views;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using GameExplorer.Uwp.Dialogs;

namespace GameExplorer.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Uwp.ViewModels.PostViewModel" />
    public class WikiViewModel : PostViewModel
    {
        /// <summary>
        /// The wiki
        /// </summary>
        private Wiki _wiki;
        /// <summary>
        /// Gets or sets the wiki.
        /// </summary>
        /// <value>
        /// The wiki.
        /// </value>
        public Wiki Wiki
        {
            get => _wiki;
            set
            {
                if (SetField(ref _wiki, value))
                {
                    OnPropertyChanged(nameof(_wiki));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WikiViewModel" /> class.
        /// </summary>
        public WikiViewModel()
        {
            Wiki = new Wiki(MainViewReference.CurrentUser);
            Wiki.PropertyChanged += (s, e) => { Changed = true; };
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
            NavigationService.NavigateTo(typeof(AddWikiPage), Wiki);
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
            if (Wiki == null || !Wiki.Valid)
            {
                await NotifyUtils.DisplayErrorMessage("Some changes are not valid.");
                return false;
            }

            Lock = true;

            if (await MainViewReference.Database.Upload(Wiki) == DatabaseService.Response.Failed)
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
            Wiki?.SetPhoto(MainViewReference.CurrentUser, image);
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
            Wiki?.SetBanner(MainViewReference.CurrentUser, image);
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
            Wiki?.AddScreenshots(MainViewReference.CurrentUser, images);
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

            Wiki?.AddVideo(MainViewReference.CurrentUser, dialog.VideoData);
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
            Wiki?.AddComment(comment);

            if (await MainViewReference.Database.Upload(Wiki) == DatabaseService.Response.Failed)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong when uploading your comment.");
                Wiki.Comments.Remove(comment);
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
            Wiki?.Screenshots?.Remove(screenshot);
            Changed = true;
        }

        /// <summary>
        /// Deletes the video.
        /// </summary>
        /// <param name="video">The video.</param>
        public override void DeleteVideo(Video video)
        {
            Wiki?.Videos?.Remove(video);
            Changed = true;
        }

        /// <summary>
        /// Likes the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public override async Task LikeComment(Comment comment)
        {
            comment = Wiki?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

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
            comment = Wiki?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

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
            comment = Wiki?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

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
            comment = Wiki?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

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
            await MainViewReference.Database.Add(new Report(Wiki.Uid, MainViewReference.CurrentUser, reportDialog.Message));
            await NotifyUtils.DisplayThankYouMessage("Thanks for contributing to a nicer and safer community! You rock!");
        }

        /// <summary>
        /// Reports the asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task ReportAsync()
        {
            if (MainViewReference.CurrentUser == null || Wiki == null)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong. Try again later.");
                return;
            }

            var reportDialog = new ReportDialog(Wiki.Title);

            if (await reportDialog.ShowAsync() != ContentDialogResult.Secondary || !reportDialog.Valid)
            {
                return;
            }

            await MainViewReference.Database.Add(new Report(Wiki.Uid, MainViewReference.CurrentUser, reportDialog.Message));
            await NotifyUtils.DisplayThankYouMessage("Thanks for contributing to a nicer and safer community! You rock!");
        }
    }
}
