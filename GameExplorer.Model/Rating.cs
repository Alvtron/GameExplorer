using System;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class Rating : DatabaseItem
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Rating"/> is value.
        /// </summary>
        /// <value>
        ///   <c>true</c> if value; otherwise, <c>false</c>.
        /// </value>
        public bool Value { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime? Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        public Rating()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Rating"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="value">if set to <c>true</c> [value].</param>
        public Rating(User user, bool value)
        {
            User = user;
            Value = value;
        }
    }
}
