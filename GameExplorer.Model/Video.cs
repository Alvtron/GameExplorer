using System;
using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class Video : DatabaseItem
    {
        /// <summary>
        /// You tube identifier
        /// </summary>
        private string _youTubeId;
        /// <summary>
        /// Gets or sets you tube identifier.
        /// </summary>
        /// <value>
        /// You tube identifier.
        /// </value>
        public string YouTubeId
        {
            get => _youTubeId;
            set
            {
                if (SetField(ref _youTubeId, value))
                {
                    OnPropertyChanged(nameof(_youTubeId));
                }
            }
        }

        /// <summary>
        /// Gets you tube URI.
        /// </summary>
        /// <value>
        /// You tube URI.
        /// </value>
        [IgnoreDataMember]
        public Uri YouTubeUri => new Uri(@"https://www.youtube.com/embed/" + YouTubeId);
        /// <summary>
        /// Gets you tube thumbnail.
        /// </summary>
        /// <value>
        /// You tube thumbnail.
        /// </value>
        [IgnoreDataMember]
        public Uri YouTubeThumbnail => new Uri(@"https://img.youtube.com/vi/" + YouTubeId + @"/0.jpg");

        /// <summary>
        /// The title
        /// </summary>
        private string _title;
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title
        {
            get => _title;
            set
            {
                if (SetField(ref _title, value))
                {
                    OnPropertyChanged(nameof(_title));
                }
            }
        }

        /// <summary>
        /// The uploaded
        /// </summary>
        private DateTime _uploaded = DateTime.Now;
        /// <summary>
        /// Gets or sets the uploaded.
        /// </summary>
        /// <value>
        /// The uploaded.
        /// </value>
        public DateTime Uploaded
        {
            get => _uploaded;
            set
            {
                if (SetField(ref _uploaded, value))
                {
                    OnPropertyChanged(nameof(_uploaded));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Video"/> is empty.
        /// </summary>
        /// <value>
        ///   <c>true</c> if empty; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool Empty => string.IsNullOrWhiteSpace(YouTubeId);

        /// <summary>
        /// Initializes a new instance of the <see cref="Video"/> class.
        /// </summary>
        public Video()
        {
        }
    }
}
