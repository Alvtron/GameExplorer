using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class Questline : DatabaseItem
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the game.
        /// </summary>
        /// <value>
        /// The game.
        /// </value>
        public Game Game { get; set; }

        /// <summary>
        /// Gets or sets the quests.
        /// </summary>
        /// <value>
        /// The quests.
        /// </value>
        public ObservableCollection<Quest> Quests { get; set; } = new ObservableCollection<Quest>();

        /// <summary>
        /// Gets a value indicating whether this <see cref="Questline"/> is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool Valid => !string.IsNullOrWhiteSpace(Title) && Game != null;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Title;
        }
    }
}