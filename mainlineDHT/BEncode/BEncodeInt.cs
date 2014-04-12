// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BEncodeInt.cs" company="Rendijs Smukulis">
//   By rendijs.smukulis@gmail.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace mainlineDHT.BEncode
{
    using System.Text;

    /// <summary>
    /// The BEncode integer class. Helps with BEncode integer manipulation.
    /// </summary>
    public class BEncodeInt : IBEncodeEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BEncodeInt"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public BEncodeInt(int value)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets or sets the integer value.
        /// </summary>
        public int Value { get; set; }

        /// <summary>
        /// Serializes the entity to bytes
        /// </summary>
        /// <returns>
        /// Entity serialized to a byte array
        /// </returns>
        public byte[] ToBytes()
        {
            var formatted = "i" + this.Value + "e";
            return Encoding.ASCII.GetBytes(formatted);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}
