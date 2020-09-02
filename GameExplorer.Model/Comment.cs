using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class Comment : DatabaseItem
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }

        /// <summary>
        /// The message
        /// </summary>
        private string _message;
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message
        {
            get => _message;
            set
            {
                if (SetField(ref _message, value))
                {
                    OnPropertyChanged(nameof(_message));
                }
            }
        }

        /// <summary>
        /// The likes
        /// </summary>
        private ObservableCollection<Rating> _likes = new ObservableCollection<Rating>();
        /// <summary>
        /// Gets or sets the likes.
        /// </summary>
        /// <value>
        /// The likes.
        /// </value>
        public ObservableCollection<Rating> Likes
        {
            get => _likes;
            set
            {
                if (SetField(ref _likes, value))
                {
                    OnPropertyChanged(nameof(_likes));
                }
            }
        }

        /// <summary>
        /// The replies
        /// </summary>
        private ObservableCollection<Comment> _replies = new ObservableCollection<Comment>();
        /// <summary>
        /// Gets or sets the replies.
        /// </summary>
        /// <value>
        /// The replies.
        /// </value>
        public ObservableCollection<Comment> Replies
        {
            get => _replies;
            set
            {
                if (SetField(ref _replies, value))
                {
                    OnPropertyChanged(nameof(_replies));
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
        /// Gets a value indicating whether this instance has likes.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has likes; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool HasLikes => Likes?.Count > 0;
        /// <summary>
        /// Gets the number of likes.
        /// </summary>
        /// <value>
        /// The number of likes.
        /// </value>
        [IgnoreDataMember]
        public int NumberOfLikes => Likes?.Count(x => x.Value) ?? 0;
        /// <summary>
        /// Gets a value indicating whether this instance has replies.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance has replies; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool HasReplies => Replies?.Count > 0;
        /// <summary>
        /// Gets the number of replies.
        /// </summary>
        /// <value>
        /// The number of replies.
        /// </value>
        [IgnoreDataMember]
        public int NumberOfReplies => Replies?.Count ?? 0;
        /// <summary>
        /// Gets the created.
        /// </summary>
        /// <value>
        /// The created.
        /// </value>
        [IgnoreDataMember]
        public DateTime? Created => (Logs?.Count > 0) ? Logs.OrderBy(x => x.Date).First().Date : null;
        /// <summary>
        /// Gets the updated.
        /// </summary>
        /// <value>
        /// The updated.
        /// </value>
        [IgnoreDataMember]
        public DateTime? Updated => (Logs?.Count > 0) ? Logs.OrderByDescending(x => x.Date).First().Date : null;

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        public Comment()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Comment"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        public Comment(User user, string message)
        {
            User = user;
            Message = message;
            Logs = new ObservableCollection<Log> { new Log(user, "created this") };
            User.IncreaseExperience(Experience.Action.AddComment);
        }

        /// <summary>
        /// Replies the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="comment">The comment.</param>
        public void Reply(User user, Comment comment)
        {
            if (comment == null || user == null)
                return;

            if (Replies == null)
                Replies = new ObservableCollection<Comment>();

            Replies.Add(comment);
            Logs.Add(new Log(user, "replied"));
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return GetType().Name;
        }

        /// <summary>
        /// Likes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Like(User user)
        {
            if (Likes == null || HasLiked(user))
                return;

            Likes.Add(new Rating { User = user, Value = true });
            Logs.Add(new Log(user, "liked"));
        }

        /// <summary>
        /// Dislikes the specified user.
        /// </summary>
        /// <param name="user">The user.</param>
        public void Dislike(User user)
        {
            if (!HasLikes)
                return;

            Likes.Remove(Likes.Single(i => i.User == user));
            Logs.Add(new Log(user, "disliked"));
        }

        /// <summary>
        /// Determines whether the specified user has liked.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>
        ///   <c>true</c> if the specified user has liked; otherwise, <c>false</c>.
        /// </returns>
        public bool HasLiked(User user)
        {
            return HasLikes && Likes.Any(x => x.User.Match(user));
        }
    }
}
