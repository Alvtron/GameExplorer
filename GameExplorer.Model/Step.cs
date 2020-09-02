namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class Step : DatabaseItem
    {
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
        /// The screenshot
        /// </summary>
        private Image _screenshot;
        /// <summary>
        /// Gets or sets the screenshot.
        /// </summary>
        /// <value>
        /// The screenshot.
        /// </value>
        public Image Screenshot
        {
            get => _screenshot;
            set
            {
                if (SetField(ref _screenshot, value))
                {
                    OnPropertyChanged(nameof(_screenshot));
                }
            }
        }

        /// <summary>
        /// The map
        /// </summary>
        private Map _map;
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
        /// Initializes a new instance of the <see cref="Step"/> class.
        /// </summary>
        public Step() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Step"/> class.
        /// </summary>
        /// <param name="about">The about.</param>
        /// <param name="screenshot">The screenshot.</param>
        /// <param name="map">The map.</param>
        public Step(string about, Image screenshot, Map map)
        {
            About = about;
            Screenshot = screenshot;
            Map = map;
        }
    }
}