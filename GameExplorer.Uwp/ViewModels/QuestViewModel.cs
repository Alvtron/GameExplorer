using GameExplorer.Model;
using GameExplorer.Uwp.Data;
using GameExplorer.Uwp.Services;
using GameExplorer.Uwp.Utils;
using GameExplorer.Uwp.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using GameExplorer.Uwp.Dialogs;
using Image = GameExplorer.Model.Image;

namespace GameExplorer.Uwp.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Uwp.ViewModels.PostViewModel" />
    public class QuestViewModel : PostViewModel
    {
        /// <summary>
        /// The quest
        /// </summary>
        private Quest _quest = new Quest();
        /// <summary>
        /// Gets or sets the quest.
        /// </summary>
        /// <value>
        /// The quest.
        /// </value>
        public Quest Quest
        {
            get => _quest;
            set
            {
                if (SetField(ref _quest, value))
                {
                    OnPropertyChanged(nameof(_quest));
                }
            }
        }

        /// <summary>
        /// The questlines
        /// </summary>
        public ObservableCollection<Questline> Questlines;
        /// <summary>
        /// The rewards
        /// </summary>
        public ObservableCollection<Reward> Rewards;

        /// <summary>
        /// The add step command
        /// </summary>
        private RelayCommand _addStepCommand;
        /// <summary>
        /// Gets the add step command.
        /// </summary>
        /// <value>
        /// The add step command.
        /// </value>
        public ICommand AddStepCommand => _addStepCommand = _addStepCommand ?? new RelayCommand(async (param) => await AddStepAsync());

        /// <summary>
        /// The add reward command
        /// </summary>
        private RelayCommand<Reward> _addRewardCommand;
        /// <summary>
        /// Gets the add reward command.
        /// </summary>
        /// <value>
        /// The add reward command.
        /// </value>
        public ICommand AddRewardCommand => _addRewardCommand = _addRewardCommand ?? new RelayCommand<Reward>(async (param) => await AddReward(param));

        /// <summary>
        /// The create reward command
        /// </summary>
        private RelayCommand _createRewardCommand;
        /// <summary>
        /// Gets the create reward command.
        /// </summary>
        /// <value>
        /// The create reward command.
        /// </value>
        public ICommand CreateRewardCommand => _createRewardCommand = _createRewardCommand ?? new RelayCommand(async (param) => await CreateReward());

        /// <summary>
        /// The create questline command
        /// </summary>
        private RelayCommand _createQuestlineCommand;
        /// <summary>
        /// Gets the create questline command.
        /// </summary>
        /// <value>
        /// The create questline command.
        /// </value>
        public ICommand CreateQuestlineCommand => _createQuestlineCommand = _createQuestlineCommand ?? new RelayCommand(async (param) => await CreateQuestline());

        /// <summary>
        /// Initializes a new instance of the <see cref="QuestViewModel"/> class.
        /// </summary>
        public QuestViewModel()
        {
            Quest = new Quest(MainViewReference.CurrentUser);
            Quest.PropertyChanged += (s, e) => { Changed = true; };

        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public override async Task Create()
        {
            Questlines = await DatabaseService.GetQuestlines(Quest?.Game);
            Rewards = await DatabaseService.GetRewards(Quest?.Game);
        }

        /// <summary>
        /// Edits this instance.
        /// </summary>
        public override void Edit()
        {
            NavigationService.NavigateTo(typeof(AddQuestPage), Quest);
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
            if (Quest == null || !Quest.Valid)
            {
                await NotifyUtils.DisplayErrorMessage("Some changes are not valid.");
                return false;
            }

            Lock = true;

            if (await MainViewReference.Database.Upload(Quest) == DatabaseService.Response.Failed)
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
            Quest?.SetPhoto(MainViewReference.CurrentUser, image);
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
            Quest?.SetBanner(MainViewReference.CurrentUser, image);
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
            Quest?.AddScreenshots(MainViewReference.CurrentUser, images);
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

            Quest?.AddVideo(MainViewReference.CurrentUser, dialog.VideoData);
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
            Quest?.AddComment(comment);

            if (await MainViewReference.Database.Upload(Quest) == DatabaseService.Response.Failed)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong when uploading your comment.");
                Quest?.Comments.Remove(comment);
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
            Quest?.Screenshots.Remove(screenshot);
            Changed = true;
        }

        /// <summary>
        /// Deletes the video.
        /// </summary>
        /// <param name="video">The video.</param>
        public override void DeleteVideo(Video video)
        {
            Quest?.Videos.Remove(video);
            Changed = true;
        }

        /// <summary>
        /// Likes the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        /// <returns></returns>
        public override async Task LikeComment(Comment comment)
        {
            comment = Quest?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

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
            comment = Quest?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

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
            comment = Quest?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

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
            comment = Quest?.Comments?.FirstOrDefault(c => c.Uid == comment?.Uid);

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
            await MainViewReference.Database.Add(new Report(Quest.Uid, MainViewReference.CurrentUser, reportDialog.Message));
            await NotifyUtils.DisplayThankYouMessage("Thanks for contributing to a nicer and safer community! You rock!");
        }

        /// <summary>
        /// Reports the asynchronous.
        /// </summary>
        /// <returns></returns>
        public override async Task ReportAsync()
        {
            if (MainViewReference.CurrentUser == null || Quest == null)
            {
                await NotifyUtils.DisplayErrorMessage("Something went wrong. Try again later.");
                return;
            }

            var reportDialog = new ReportDialog(Quest?.Title);

            if (await reportDialog.ShowAsync() != ContentDialogResult.Secondary || !reportDialog.Valid)
            {
                return;
            }

            await MainViewReference.Database.Add(new Report(Quest.Uid, MainViewReference.CurrentUser, reportDialog.Message));
            await NotifyUtils.DisplayThankYouMessage("Thanks for contributing to a nicer and safer community! You rock!");
        }

        /// <summary>
        /// Adds the step asynchronous.
        /// </summary>
        /// <returns></returns>
        public async Task<bool> AddStepAsync()
        {
            var dialog = new AddStepDialog();
            if (await dialog.ShowAsync() != ContentDialogResult.Secondary)
            {
                return false;
            }

            Lock = true;

            Quest?.AddStep(MainViewReference.CurrentUser, dialog.StepData);

            Lock = false;

            return true;
        }

        /// <summary>
        /// Creates the reward.
        /// </summary>
        /// <returns></returns>
        public async Task CreateReward()
        {
            var dialog = new CreateRewardDialog();
            if (await dialog.ShowAsync() != ContentDialogResult.Secondary)
            {
                return;
            }

            Lock = true;

            if (Quest == null || !Quest.AddReward(MainViewReference.CurrentUser, dialog.Reward))
            {
                await NotifyUtils.DisplayErrorMessage("Reward was invalid");
            }

            Lock = false;
        }

        /// <summary>
        /// Adds the reward.
        /// </summary>
        /// <param name="reward">The reward.</param>
        /// <returns></returns>
        public async Task AddReward(Reward reward)
        {
            Lock = true;

            if (Quest == null || !Quest.AddReward(MainViewReference.CurrentUser, reward))
            {
                await NotifyUtils.DisplayErrorMessage("Reward is invalid");
            }

            Lock = false;
        }

        /// <summary>
        /// Creates the questline.
        /// </summary>
        /// <returns></returns>
        public async Task CreateQuestline()
        {
            var dialog = new CreateQuestLineDialog();
            if (await dialog.ShowAsync() != ContentDialogResult.Secondary)
            {
                return;
            }

            Lock = true;

            if (Quest == null || !Quest.SetQuestline(MainViewReference.CurrentUser, dialog.Questline))
            {
                await NotifyUtils.DisplayErrorMessage("Questline was invalid");
            }

            Lock = false;
        }
    }
}
