using System.Runtime.Serialization;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.DatabaseItem" />
    public class Reward : DatabaseItem
    {
        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; set; }
        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public double Amount { get; set; }

        /// <summary>
        /// Gets a value indicating whether this <see cref="Reward"/> is valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if valid; otherwise, <c>false</c>.
        /// </value>
        [IgnoreDataMember]
        public bool Valid => Type != null && Amount > 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="Reward"/> class.
        /// </summary>
        public Reward() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Reward"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="amount">The amount.</param>
        public Reward(string type, double amount)
        {
            Type = type;
            Amount = amount;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Amount + " " + Type;
        }
    }
}