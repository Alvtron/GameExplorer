using System;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class Report : DatabaseItem
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime? Date { get; set; } = DateTime.Now;
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public Guid Target { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Report"/> is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        public bool Valid => User != null && string.IsNullOrWhiteSpace(Message);

        /// <summary>
        /// Initializes a new instance of the <see cref="Report"/> class.
        /// </summary>
        public Report()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Report"/> class.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="user">The user.</param>
        /// <param name="message">The message.</param>
        public Report(Guid target, User user, string message)
        {
            Target = target;
            User = user;
            Message = message;
        }
    }
}
