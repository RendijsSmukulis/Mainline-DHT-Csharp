// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BEncodeDictionary.cs" company="Rendijs Smukulis">
//   By rendijs.smukulis@gmail.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace mainlineDHT.BEncode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The BEncodeDictionary class. Helps with BEncode dictionary manipulation.
    /// </summary>
    public class BEncodeDictionary : IBEncodeEntity, IDictionary<BEncodeByteString, IBEncodeEntity>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BEncodeDictionary"/> class.
        /// Initializes a new instance of the <see cref="BEncodeDictionary"/> class.
        /// </summary>
        public BEncodeDictionary()
        {
            this.values = new Dictionary<BEncodeByteString, IBEncodeEntity>();
        }

        /// <summary>
        /// Gets or sets the list of key/value pairs in the dictionary.
        /// </summary>
        private Dictionary<BEncodeByteString, IBEncodeEntity> values;

        /// <summary>
        /// Adds the specified key/value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public void Add(BEncodeByteString key, IBEncodeEntity value)
        {
            this.values.Add(key, value);
        }

        /// <summary>
        /// Adds the specified key/value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key (will be converted to BEncodeByteString).</param>
        /// <param name="value">The value.</param>
        public void Add(string key, IBEncodeEntity value)
        {
            this.values.Add(new BEncodeByteString(key), value);
        }

        /// <summary>
        /// Serializes the dictionary to bytes
        /// </summary>
        /// <returns>Dictionary serialized to a byte array</returns>
        public byte[] ToBytes()
        {
            // dictionary starts with 'd', 
            // then contains a list of all key/value pairs,
            // then closes with an 'e'.
            // e.g.: "d2:id5:abbbae" equals {id:"abbba"}
            var bytes = new List<byte>();
            bytes.Add(100); // 'd', marks beginning of dictionary
            
            foreach (var dictionaryEntry in this.values)
            {
                bytes.AddRange(dictionaryEntry.Key.ToBytes());
                bytes.AddRange(dictionaryEntry.Value.ToBytes());
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
            return "Dictionary [" + this.values.Count + " items]";
        }


        public bool ContainsKey(BEncodeByteString key)
        {
            return this.values.ContainsKey(key);
        }

        public ICollection<BEncodeByteString> Keys
        {
            get
            {
                return this.values.Keys;
            }
        }

        public bool Remove(BEncodeByteString key)
        {
            return this.values.Remove(key);
        }

        public bool TryGetValue(BEncodeByteString key, out IBEncodeEntity value)
        {
            return this.values.TryGetValue(key, out value);
        }

        public ICollection<IBEncodeEntity> Values
        {
            get
            {
                return this.values.Values;
            }
        }

        public IBEncodeEntity this[BEncodeByteString key]
        {
            get
            {
                return this.values[key];
            }
            set
            {
                this.values[key] = value;
            }
        }

        public void Add(KeyValuePair<BEncodeByteString, IBEncodeEntity> item)
        {
            this.values.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            this.values.Clear();
        }

        public bool Contains(KeyValuePair<BEncodeByteString, IBEncodeEntity> item)
        {
            return this.values.Contains(item);
        }

        public void CopyTo(KeyValuePair<BEncodeByteString, IBEncodeEntity>[] array, int arrayIndex)
        {
            int i = 0;
            foreach (var value in this.values)
            {
                array[i++] = value;
            }
        }

        public int Count
        {
            get
            {
                return this.values.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public bool Remove(KeyValuePair<BEncodeByteString, IBEncodeEntity> item)
        {
            return this.values.Remove(item.Key);
        }

        public IEnumerator<KeyValuePair<BEncodeByteString, IBEncodeEntity>> GetEnumerator()
        {
            return this.values.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.values.GetEnumerator();
        }
    }
}
