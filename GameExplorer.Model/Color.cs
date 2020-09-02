using System.Collections.Generic;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.ValueObject" />
    public class Color : ValueObject
    {
        /// <summary>
        /// Gets or sets the r.
        /// </summary>
        /// <value>
        /// The r.
        /// </value>
        public byte R { get; set; }
        /// <summary>
        /// Gets or sets the g.
        /// </summary>
        /// <value>
        /// The g.
        /// </value>
        public byte G { get; set; }
        /// <summary>
        /// Gets or sets the b.
        /// </summary>
        /// <value>
        /// The b.
        /// </value>
        public byte B { get; set; }
        /// <summary>
        /// Gets or sets a.
        /// </summary>
        /// <value>
        /// a.
        /// </value>
        public byte A { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class.
        /// </summary>
        public Color() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Color"/> class.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        /// <param name="a">a.</param>
        public Color(byte r, byte g, byte b, byte a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        /// <summary>
        /// Gets the atomic values.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return R;
            yield return G;
            yield return B;
            yield return A;
        }
    }
}
