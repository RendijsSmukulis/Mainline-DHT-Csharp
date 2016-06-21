// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BEncodeByteString.cs" company="Rendijs Smukulis">
//   By rendijs.smukulis@gmail.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace mainlineDHT.BEncode
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// The BEncodeByteString. Helps with BEncode string manipulation.
    /// </summary>
    public class BEncodeByteString : IBEncodeEntity, IEquatable<BEncodeByteString>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BEncodeByteString"/> class.
        /// </summary>
        public BEncodeByteString()
        {
            this.Value = new byte[0];
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BEncodeByteString"/> class.
        /// </summary>
        /// <param name="value">The string value to initialize with.</param>
        public BEncodeByteString(string value)
        {
            this.Value = Encoding.ASCII.GetBytes(value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BEncodeByteString"/> class.
        /// </summary>
        /// <param name="value">The bytes to initialize with.</param>
        public BEncodeByteString(byte[] value)
        {
            this.Value = value;
        }
        
        /// <summary>
        /// Gets or sets the string value.
        /// </summary>
        public byte[] Value { get; set; }

        /// <summary>
        /// Serializes the string to bytes
        /// </summary>
        /// <returns>String serialized to a byte array</returns>
        public byte[] ToBytes()
        {
            // String is represented as "strlen:string contents", e.g. "2:id"
            var bytes = new List<byte>();
            var prefix = this.Value.Length + ":";
            bytes.AddRange(Encoding.ASCII.GetBytes(prefix));
            bytes.AddRange(this.Value);

            return bytes.ToArray();
        }

        public bool Equals(BEncodeByteString other)
        {
            return this.Value.Equals(other.Value);
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Encoding.ASCII.GetString(this.Value);
        }

        bool IEquatable<BEncodeByteString>.Equals(BEncodeByteString other)
        {
            return this.ToString().Equals(other.ToString());
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
