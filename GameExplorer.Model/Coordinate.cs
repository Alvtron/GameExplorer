namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class Coordinate : DatabaseItem
    {
        /// <summary>
        /// Gets the map.
        /// </summary>
        /// <value>
        /// The map.
        /// </value>
        public Map Map { get; }

        /// <summary>
        /// The x
        /// </summary>
        private double _x;
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        /// <value>
        /// The x.
        /// </value>
        public double X
        {
            get => _x;
            set
            {
                if (SetField(ref _x, value))
                {
                    OnPropertyChanged(nameof(_x));
                }
            }
        }

        /// <summary>
        /// The y
        /// </summary>
        private double _y;
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        /// <value>
        /// The y.
        /// </value>
        public double Y
        {
            get => _y;
            set
            {
                if (SetField(ref _y, value))
                {
                    OnPropertyChanged(nameof(_y));
                }
            }
        }

        /// <summary>
        /// The label
        /// </summary>
        private string _label;
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        /// <value>
        /// The label.
        /// </value>
        public string Label
        {
            get => _label;
            set
            {
                if (SetField(ref _label, value))
                {
                    OnPropertyChanged(nameof(_label));
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        public Coordinate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Coordinate"/> class.
        /// </summary>
        /// <param name="map">The map.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="label">The label.</param>
        public Coordinate(Map map, double x, double y, string label = null)
        {
            Map = map;
            X = x;
            Y = y;
            Label = label;
        }
    }
}
