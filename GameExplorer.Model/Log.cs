using System;
using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class Log : DatabaseItem
    {
        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public User User { get; set; }
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        /// <value>
        /// The action.
        /// </value>
        public string Action { get; set; }
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime? Date { get; set; } = DateTime.Now;

        /// <summary>
        /// Gets the information.
        /// </summary>
        /// <value>
        /// The information.
        /// </value>
        [IgnoreDataMember]
        public string Information => InformationString();

        /// <summary>
        /// Informations the string.
        /// </summary>
        /// <returns></returns>
        private string InformationString()
        {
            return User?.Username + " " + Action?.ToLower() + ".";
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        public Log()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Log"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <param name="action">The action.</param>
        public Log(User user, string action)
        {
            User = user;
            Action = action;
        }
    }
}
