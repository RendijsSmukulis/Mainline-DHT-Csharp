// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BEncodeList.cs" company="Rendijs Smukulis">
//   By rendijs.smukulis@gmail.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace mainlineDHT.BEncode
{
    using System.Collections.Generic;

    /// <summary>
    /// The BEncodeList class. Helps with BEncode dictionary manipulation.
    /// </summary>
    public class BEncodeList : IBEncodeEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BEncodeList"/> class.
        /// </summary>
        public BEncodeList()
        {
            this.Values = new List<IBEncodeEntity>();
        }

        /// <summary>
        /// Gets or sets the list of key/value pairs in the dictionary.
        /// </summary>
        public List<IBEncodeEntity> Values { get; set; }

        /// <summary>
        /// Adds the specified value to the dictionary.
        /// </summary>
        /// <param name="value">The value.</param>
        public void Add(IBEncodeEntity value)
        {
            this.Values.Add(value);
        }
        
        /// <summary>
        /// Serializes the list to bytes
        /// </summary>
        /// <returns>List serialized to a byte array</returns>
        public byte[] ToBytes()
        {
            // list starts with 'l', 
            // then contains a list of all values,
            // then closes with an 'e'.
            // e.g.: "l2:id5:abbbae" equals ["id", "abbba"]
            var bytes = new List<byte>();
            bytes.Add(108); // 'l', marks beginning of list
            
            foreach (var listItem in this.Values)
            {
                bytes.AddRange(listItem.ToBytes());
            }

            bytes.Add(101); // 'e', marks the end of dictionary

            return bytes.ToArray();
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "List [" + this.Values.Count + " items]";
        }
    }
}
