using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.SearchablePost" />
    public class Post : SearchablePost
    {
        /// <summary>
        /// The summary
        /// </summary>
        private string _summary;
        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        /// <value>
        /// The summary.
        /// </value>
        public string Summary
        {
            get => _summary;
            set
            {
                if (SetField(ref _summary, value))
                {
                    OnPropertyChanged(nameof(_summary));
                }
            }
        }

        /// <summary>
        /// The about
        /// </summary>
        private string _about;
        /// <summary>
        /// Gets or sets the about.
        /// </summary>
        /// <value>
        /// The about.
        /// </value>
        public string About
        {
            get => _about;
            set
            {
                if (SetField(ref _about, value))
                {
                    OnPropertyChanged(nameof(_about));
                }
            }
        }

        /// <summary>
        /// The banner
        /// </summary>
        private Image _banner;
        /// <summary>
        /// Gets or sets the banner.
        /// </summary>
        /// <value>
        /// The banner.
        /// </value>
        public Image Banner
        {
            get => _banner;
            set
            {
                if (SetField(ref _banner, value))
                {
                    OnPropertyChanged(nameof(_banner));
                }
            }
        }

        /// <summary>
        /// The screenshots
        /// </summary>
        private ObservableCollection<Image> _screenshots = new ObservableCollection<Image>();
        /// <summary>
        /// Gets or sets the screenshots.
        /// </summary>
        /// <value>
        /// The screenshots.
        /// </value>
        public ObservableCollection<Image> Screenshots
        {
            get => _screenshots;
            set
            {
                if (SetField(ref _screenshots, value))
                {
                    OnPropertyChanged(nameof(_screenshots));
                }
            }
        }

        /// <summary>
        /// The videos
        /// </summary>
        private ObservableCollection<Video> _videos = new ObservableCollection<Video>();
        /// <summary>
        /// Gets or sets the videos.
        /// </summary>
        /// <value>
        /// The videos.
        /// </value>
        public ObservableCollection<Video> Videos
        {
            get => _videos;
            set
            {
                if (SetField(ref _videos, value))
                {
                    OnPropertyChanged(nameof(_videos));
                }
            }
        }

        /// <summary>
        /// The logs
        /// </summary>
        private ObservableCollection<Log> _logs = new ObservableCollection<Log>();
        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        /// <value>
        /// The logs.
        /// </value>
        public ObservableCollection<Log> Logs
        {
            get => _logs;
            set
            {
                if (SetField(ref _logs, value))
                {
                    OnPropertyChanged(nameof(_logs));
                }
            }
        }

        /// <summary>
        /// The comments
        /// </summary>
        private ObservableCollection<Comment> _comments = new ObservableCollection<Comment>();
        /// <summary>
        /// Gets or sets the comments.
        /// </summary>
        /// <value>
        /// The comments.
        /// </value>
        public ObservableCollection<Comment> Comments
        {
            get => _comments;
            set
            {
                if (SetField(ref _comments, value))
                {
                    OnPropertyChanged(nameof(_comments));
                }
            }
        }


        /// <summary>
        /// Gets a value indicating whether this instance has photo.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has photo; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool HasPhoto => !Photo?.Empty ?? false;
        /// <summary>
        /// Gets a value indicating whether this instance has banner.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has banner; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool HasBanner => !Banner?.Empty ?? false;
        /// <summary>
        /// Gets a value indicating whether this instance has screenshots.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has screenshots; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool HasScreenshots => (Screenshots != null) && Screenshots.Count != 0;
        /// <summary>
        /// Gets a value indicating whether this instance has videos.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has videos; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool HasVideos => (Videos != null) && Videos.Count != 0;

        /* Methods */

        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        public Post()
        {
        }

        /// <summary>
        /// Sets the photo.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="image">The image.</param>
        public void SetPhoto(User user, Image image)
        {
            if (image == null || image.Empty)
                return;

            Photo = image;
            Logs.Add(new Log(user, "changed the photo"));
        }

        /// <summary>
        /// Sets the banner.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="image">The image.</param>
        public void SetBanner(User user, Image image)
        {
            if (image == null || image.Empty)
                return;

            Banner = image;
            Logs.Add(new Log(user, "changed the banner"));
        }

        /// <summary>
        /// Adds the screenshot.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="image">The image.</param>
        public void AddScreenshot(User user, Image image)
        {
            if (image == null || image.Empty)
                return;

            Screenshots.Add(image);
            Logs.Add(new Log(user, "added a new screenshot"));
        }

        /// <summary>
        /// Adds the screenshots.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="images">The images.</param>
        public void AddScreenshots(User user, List<Image> images)
        {
            if (images == null || images.Count == 0)
                return;

            var numberOfAddedImages = 0;

            foreach (var image in images)
            {
                if (image == null || image.Empty) continue;

                Screenshots.Add(image);
                numberOfAddedImages++;
            }

            Logs.Add(new Log(user, "added " + images.Count + " new screenshot" + ((numberOfAddedImages > 1) ? "s" : "")));
        }

        /// <summary>
        /// Adds the video.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="video">The video.</param>
        public void AddVideo(User user, Video video)
        {
            if (video == null || video.Empty)
                return;

            Videos.Add(video);
            Logs.Add(new Log(user, "added a new video"));
        }

        /// <summary>
        /// Adds the comment.
        /// </summary>
        /// <param name="comment">The comment.</param>
        public void AddComment(Comment comment)
        {
            Comments.Add(comment);
            Comments = new ObservableCollection<Comment>(Comments.OrderByDescending(c => c.Created));
            Logs.Add(new Log(comment.User, "added a new comment"));
        }
    }
}
