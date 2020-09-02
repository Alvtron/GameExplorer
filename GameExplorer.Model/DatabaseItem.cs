using System;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.NotifyPropertyChanged" />
    public class DatabaseItem : NotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the uid.
        /// </summary>
        /// <value>
        /// The uid.
        /// </value>
        public Guid Uid { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseItem"/> class.
        /// </summary>
        public DatabaseItem()
        {
            Uid = Guid.NewGuid();
        }
    }
}
