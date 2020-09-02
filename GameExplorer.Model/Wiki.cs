using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.Post" />
    public class Wiki : Post
    {
        /// <summary>
        /// The game
        /// </summary>
        private Game _game;
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
        /// The map
        /// </summary>
        private Map _map = new Map();
        /// <summary>
        /// Gets or sets the map.
        /// </summary>
        /// <value>
        /// The map.
        /// </value>
        public Map Map
        {
            get => _map;
            set
            {
                if (SetField(ref _map, value))
                {
                    OnPropertyChanged(nameof(_map));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Wiki"/> is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool Valid => !string.IsNullOrWhiteSpace(Title);

        /// <summary>
        /// Initializes a new instance of the <see cref="Wiki"/> class.
        /// </summary>
        public Wiki()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Wiki"/> class.
        /// </summary>
        /// <param name="creator">The creator.</param>
        public Wiki(User creator)
        {
            Logs.Add(new Log(creator, "created this"));
        }
    }
}
