using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.Post" />
    public class Game : Post
    {
        /// <summary>
        /// The storyline
        /// </summary>
        private string _storyline;
        /// <summary>
        /// Gets or sets the storyline.
        /// </summary>
        /// <value>
        /// The storyline.
        /// </value>
        public string Storyline
        {
            get => _storyline;
            set
            {
                if (SetField(ref _storyline, value))
                {
                    OnPropertyChanged(nameof(_storyline));
                }
            }
        }

        /// <summary>
        /// The rating
        /// </summary>
        private double _rating;
        /// <summary>
        /// Gets or sets the rating.
        /// </summary>
        /// <value>
        /// The rating.
        /// </value>
        public double Rating
        {
            get => _rating;
            set
            {
                if (SetField(ref _rating, value))
                {
                    OnPropertyChanged(nameof(_rating));
                }
            }
        }

        /// <summary>
        /// The genre
        /// </summary>
        private string _genre;
        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        /// <value>
        /// The genre.
        /// </value>
        public string Genre
        {
            get => _genre;
            set
            {
                if (SetField(ref _genre, value))
                {
                    OnPropertyChanged(nameof(_genre));
                }
            }
        }

        /// <summary>
        /// The developer
        /// </summary>
        private string _developer;
        /// <summary>
        /// Gets or sets the developer.
        /// </summary>
        /// <value>
        /// The developer.
        /// </value>
        public string Developer
        {
            get => _developer;
            set
            {
                if (SetField(ref _developer, value))
                {
                    OnPropertyChanged(nameof(_developer));
                }
            }
        }

        /// <summary>
        /// The release
        /// </summary>
        private DateTime? _release;
        /// <summary>
        /// Gets or sets the release.
        /// </summary>
        /// <value>
        /// The release.
        /// </value>
        public DateTime? Release
        {
            get => _release;
            set
            {
                if (SetField(ref _release, value))
                {
                    OnPropertyChanged(nameof(_release));
                }
            }
        }

        /// <summary>
        /// The quests
        /// </summary>
        private ObservableCollection<Quest> _quests = new ObservableCollection<Quest>();
        /// <summary>
        /// Gets or sets the quests.
        /// </summary>
        /// <value>
        /// The quests.
        /// </value>
        public ObservableCollection<Quest> Quests
        {
            get => _quests;
            set
            {
                if (SetField(ref _quests, value))
                {
                    OnPropertyChanged(nameof(_quests));
                }
            }
        }

        /// <summary>
        /// The wikis
        /// </summary>
        private ObservableCollection<Wiki> _wikis = new ObservableCollection<Wiki>();
        /// <summary>
        /// Gets or sets the wikis.
        /// </summary>
        /// <value>
        /// The wikis.
        /// </value>
        public ObservableCollection<Wiki> Wikis
        {
            get => _wikis;
            set
            {
                if (SetField(ref _wikis, value))
                {
                    OnPropertyChanged(nameof(_wikis));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Game"/> is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool Valid => !string.IsNullOrWhiteSpace(Title);
        /// <summary>
        /// Gets the release year.
        /// </summary>
        /// <value>
        /// The release year.
        /// </value>
        [IgnoreDataMember]
        public int? ReleaseYear => Release?.Year;

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        public Game()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Game"/> class.
        /// </summary>
        /// <param name="creator">The creator.</param>
        public Game(User creator)
        {
            Logs.Add(new Log(creator, "created this"));
        }
    }
}
