using System.Collections.ObjectModel;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.Image" />
    public class Map : Image
    {
        /// <summary>
        /// Gets or sets the coordinates.
        /// </summary>
        /// <value>
        /// The coordinates.
        /// </value>
        public ObservableCollection<Coordinate> Coordinates { get; set; } = new ObservableCollection<Coordinate>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        public Map() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Map"/> class.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <param name="coordinates">The coordinates.</param>
        /// <param name="description">The description.</param>
        public Map(Image image, ObservableCollection<Coordinate> coordinates = null, string description = null)
            : base(image.ImageInBytes, image.Width, image.Height, description)
        {
            Coordinates = coordinates;
        }
    }
}
