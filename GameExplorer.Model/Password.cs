using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace GameExplorer.Model
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="GameExplorer.Model.ValueObject" />
    public class Password : ValueObject
    {
        /// <summary>
        /// Gets or sets the iterations.
        /// </summary>
        /// <value>
        /// The iterations.
        /// </value>
        public int Iterations { get; set; }
        /// <summary>
        /// Gets or sets the salt.
        /// </summary>
        /// <value>
        /// The salt.
        /// </value>
        public byte[] Salt { get; set; }
        /// <summary>
        /// Gets or sets the hash.
        /// </summary>
        /// <value>
        /// The hash.
        /// </value>
        public string Hash { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Password"/> class.
        /// </summary>
        public Password() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Password"/> class.
        /// </summary>
        /// <param name="iterations">The iterations.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="hash">The hash.</param>
        public Password(int iterations, byte[] salt, string hash)
        {
            Iterations = iterations;
            Salt = salt;
            Hash = hash;
        }

        /// <summary>
        /// Gets the atomic values.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<object> GetAtomicValues()
        {
            // Using a yield return statement to return each element one at a time
            yield return Iterations;
            yield return Salt;
            yield return Hash;
        }
    }
}
