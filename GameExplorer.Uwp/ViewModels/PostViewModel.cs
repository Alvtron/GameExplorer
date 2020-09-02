using GameExplorer.Model;
using GameExplorer.Uwp.Utils;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GameExplorer.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Uwp.ViewModels.EditorViewModel" />
    public abstract class PostViewModel : EditorViewModel
    {
        /// <summary>
        /// Uploads the banner asynchronous.
        /// </summary>
        /// <returns></returns>
        public abstract Task UploadBannerAsync();
        /// <summary>
        /// Uploads the screenshots asynchronous.
        /// </summary>
        /// <returns></returns>
        public abstract Task UploadScreenshotsAsync();
        /// <summary>
        /// Uploads the video asynchronous.
        /// </summary>
        /// <returns></returns>
        public abstract Task UploadVideoAsync();
        /// <summary>
        /// Uploads the comment asynchronous.
        /// </summary>
        /// <param name="editor">The editor.</param>
        /// <returns></returns>
        public abstract Task UploadCommentAsync(RichEditBoxExtended editor);
        /// <summary>
        /// Likes the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public abstract Task LikeComment(Comment comment);
        /// <summary>
        /// Replies the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public abstract Task ReplyComment(Comment comment);
        /// <summary>
        /// Shares the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public abstract Task ShareComment(Comment comment);
        /// <summary>
        /// Reports the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public abstract Task ReportComment(Comment comment);
        /// <summary>
        /// Deletes the screenshot.
        /// </summary>
        /// <param name="screenshot">The screenshot.</param>
        public abstract void DeleteScreenshot(Model.Image screenshot);
        /// <summary>
        /// Deletes the video.
        /// </summary>
        /// <param name="video">The video.</param>
        public abstract void DeleteVideo(Video video);
        /// <summary>
        /// Views the video.
        /// </summary>
        /// <param name="video">The video.</param>
        public abstract void ViewVideo(Video video);
        /// <summary>
        /// Views the image.
        /// </summary>
        /// <param name="image">The image.</param>
        public abstract void ViewImage(Model.Image image);

        /// <summary>
        /// The upload banner command
        /// </summary>
        private RelayCommand _uploadBannerCommand;
        /// <summary>
        /// Gets the upload banner command.
        /// </summary>
        /// <value>
        /// The upload banner command.
        /// </value>
        public ICommand UploadBannerCommand => _uploadBannerCommand = _uploadBannerCommand ?? new RelayCommand(async param => await UploadBannerAsync());

        /// <summary>
        /// The upload screenshots command
        /// </summary>
        private RelayCommand _uploadScreenshotsCommand;
        /// <summary>
        /// Gets the upload screenshots command.
        /// </summary>
        /// <value>
        /// The upload screenshots command.
        /// </value>
        public ICommand UploadScreenshotsCommand => _uploadScreenshotsCommand = _uploadScreenshotsCommand ?? new RelayCommand(async (param) => await UploadScreenshotsAsync());

        /// <summary>
        /// The upload video command
        /// </summary>
        private RelayCommand _uploadVideoCommand;
        /// <summary>
        /// Gets the upload video command.
        /// </summary>
        /// <value>
        /// The upload video command.
        /// </value>
        public ICommand UploadVideoCommand => _uploadVideoCommand = _uploadVideoCommand ?? new RelayCommand(async param => await UploadVideoAsync());

        /// <summary>
        /// The upload comment command
        /// </summary>
        private RelayCommand<RichEditBoxExtended> _uploadCommentCommand;
        /// <summary>
        /// Gets the upload comment command.
        /// </summary>
        /// <value>
        /// The upload comment command.
        /// </value>
        public ICommand UploadCommentCommand => _uploadCommentCommand = _uploadCommentCommand ?? new RelayCommand<RichEditBoxExtended>(async (param) => await UploadCommentAsync(param));

        /// <summary>
        /// The view video command
        /// </summary>
        private RelayCommand<Video> _viewVideoCommand;
        /// <summary>
        /// Gets the view video command.
        /// </summary>
        /// <value>
        /// The view video command.
        /// </value>
        public ICommand ViewVideoCommand => _viewVideoCommand = _viewVideoCommand ?? new RelayCommand<Video>(param => ViewVideo(param));

        /// <summary>
        /// The view image command
        /// </summary>
        private RelayCommand<Model.Image> _viewImageCommand;
        /// <summary>
        /// Gets the view image command.
        /// </summary>
        /// <value>
        /// The view image command.
        /// </value>
        public ICommand ViewImageCommand => _viewImageCommand = _viewImageCommand ?? new RelayCommand<Model.Image>(param => ViewImage(param));

        /// <summary>
        /// The like comment command
        /// </summary>
        private RelayCommand<Comment> _likeCommentCommand;
        /// <summary>
        /// Gets the like comment command.
        /// </summary>
        /// <value>
        /// The like comment command.
        /// </value>
        public ICommand LikeCommentCommand => _likeCommentCommand = _likeCommentCommand ?? new RelayCommand<Comment>(async param => await LikeComment(param));

        /// <summary>
        /// The reply comment command
        /// </summary>
        private RelayCommand<Comment> _replyCommentCommand;
        /// <summary>
        /// Gets the reply comment command.
        /// </summary>
        /// <value>
        /// The reply comment command.
        /// </value>
        public ICommand ReplyCommentCommand => _replyCommentCommand = _replyCommentCommand ?? new RelayCommand<Comment>(async param => await ReplyComment(param));

        /// <summary>
        /// The share comment command
        /// </summary>
        private RelayCommand<Comment> _shareCommentCommand;
        /// <summary>
        /// Gets the share comment command.
        /// </summary>
        /// <value>
        /// The share comment command.
        /// </value>
        public ICommand ShareCommentCommand => _shareCommentCommand = _shareCommentCommand ?? new RelayCommand<Comment>(async param => await ShareComment(param));

        /// <summary>
        /// The report comment command
        /// </summary>
        private RelayCommand<Comment> _reportCommentCommand;
        /// <summary>
        /// Gets the report comment command.
        /// </summary>
        /// <value>
        /// The report comment command.
        /// </value>
        public ICommand ReportCommentCommand => _reportCommentCommand = _reportCommentCommand ?? new RelayCommand<Comment>(async param => await ReportComment(param));

        /// <summary>
        /// The delete screenshot command
        /// </summary>
        private RelayCommand<Model.Image> _deleteScreenshotCommand;
        /// <summary>
        /// Gets the delete screenshot command.
        /// </summary>
        /// <value>
        /// The delete screenshot command.
        /// </value>
        public ICommand DeleteScreenshotCommand => _deleteScreenshotCommand = _deleteScreenshotCommand ?? new RelayCommand<Model.Image>(param => DeleteScreenshot(param));

        /// <summary>
        /// The delete video command
        /// </summary>
        private RelayCommand<Video> _deleteVideoCommand;
        /// <summary>
        /// Gets the delete video command.
        /// </summary>
        /// <value>
        /// The delete video command.
        /// </value>
        public ICommand DeleteVideoCommand => _deleteVideoCommand = _deleteVideoCommand ?? new RelayCommand<Video>(param => DeleteVideo(param));
    }
}
