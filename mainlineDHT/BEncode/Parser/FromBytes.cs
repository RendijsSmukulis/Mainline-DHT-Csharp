// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FromBytes.cs" company="Rendijs Smukulis">
//   By rendijs.smukulis@gmail.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace mainlineDHT.BEncode.Parser
{
    using System;

    /// <summary>
    /// Parses BEncode entities from byte arrays
    /// </summary>
    public class FromBytes
    {
        /// <summary>
        /// Parses a BEncode ByteString from a byte array.
        /// </summary>
        /// <param name="byteArr">The source byte array.</param>
        /// <param name="remainingByteArr">The remaining bytes (with the parsed item removed).</param>
        /// <returns>A parsed BEncodeByteString</returns>
        public static BEncodeByteString ToByteString(byte[] byteArr, out byte[] remainingByteArr)
        {
            remainingByteArr = byteArr;

            // Extract the length of the string (e.g. from "2:id")
            var numberStr = string.Empty;

            // Todo: optimize here.
            while (byteArr.Length > 0 && byteArr[0] >= '0' && byteArr[0] <= '9')
            {
                numberStr += (char)byteArr[0];
                byteArr = byteArr.Substring(1);
            }

            // We should now have removed all the numbers, and have a ":" + remaining string left 
            // (e.g. ":id")
            if (byteArr.Length == 0 || byteArr[0] != ':')
            {
                throw new Exception("parse error");
            }

            // Remove the ":"
            byteArr = byteArr.Substring(1);

            // Parse the actual length. The remaining string must be either the same length or longer
            var expectedStrLength = int.Parse(numberStr);
            if (expectedStrLength > byteArr.Length)
            {
                throw new Exception("parse error");
            }

            // Get the actual byte string
            remainingByteArr = byteArr.Substring(expectedStrLength);

            return new BEncodeByteString(byteArr.Substring(0, expectedStrLength));
        }

        /// <summary>
        /// Parses a BEncode integer from a byte array.
        /// </summary>
        /// <param name="byteArr">The source byte array.</param>
        /// <param name="remainingByteArr">The remaining bytes (with the parsed item removed).</param>
        /// <returns>A parsed BEncodeInt</returns>
        public static BEncodeInt ToInt(byte[] byteArr, out byte[] remainingByteArr)
        {
            remainingByteArr = byteArr;

            // The int looks like "i10e", "i-10e" or similar, so must be at least 3 bytes,
            // and start with "i".
            if (byteArr.Length < 3 || byteArr[0] != 'i')
            {
                throw new Exception("parse error");
            }

            // Remove the "i"
            byteArr = byteArr.Substring(1);

            // Extract the int into a string (optimize: maybe stringbuilder?)
            string val = string.Empty;
            if (byteArr[0] == '-')
            {
                val += "-";
                byteArr = byteArr.Substring(1);
            }

            while (byteArr[0] >= '0' && byteArr[0] <= '9')
            {
                val += (char)byteArr[0];
                byteArr = byteArr.Substring(1);
            }

            // After all the digits are extracted, we expect "e"
            long parsedVal;
            if (byteArr.Length == 0 || byteArr[0] != 'e' || !long.TryParse(val, out parsedVal))
            {
                var cause = "integer parse failed";

                if (byteArr.Length == 0)
                {
                    cause = "byteArray length was not expected to be 0";
                }

                if (byteArr[0] != 'e')
                {
                    cause = "byteArray was expected to start with 'e'";
                }
                
                throw new Exception("parse errorr, cause: " + cause);
            }

            // Remove the "e"
            remainingByteArr = byteArr.Substring(1);

            return new BEncodeInt(parsedVal);
        }

        /// <summary>
        /// Parses a BEncode dictionary from a byte array.
        /// </summary>
        /// <param name="byteArr">The source byte array.</param>
        /// <param name="remainingByteArr">The remaining bytes (with the parsed item removed).</param>
        /// <returns>A parsed BEncodeDictionary</returns>
        public static BEncodeDictionary ToDictionary(byte[] byteArr, out byte[] remainingByteArr)
        {
            remainingByteArr = byteArr;

            // The format of a dictionary is "d2:id5:valuee" (which equals "{id: "value"}"
            // the key must be a BEncodeByteString, the value is any BEncode entity
            // Dictionary must start with 'd' and be closed with an 'e'
            if (!byteArr.StartsWith('d'))
            {
                throw new Exception("parse error");
            }

            remainingByteArr = remainingByteArr.Substring(1);

            var dic = new BEncodeDictionary();

            while (!remainingByteArr.StartsWith('e'))
            {
                var key = ToByteString(remainingByteArr, out remainingByteArr);
                var value = ToEntity(remainingByteArr, out remainingByteArr);
                dic.Add(key, value);
            }

            // remove the 'e'
            remainingByteArr = remainingByteArr.Substring(1);

            return dic;
        }

        /// <summary>
        /// Parses a BEncode list from a byte array.
        /// </summary>
        /// <param name="byteArr">The source byte array.</param>
        /// <param name="remainingByteArr">The remaining bytes (with the parsed item removed).</param>
        /// <returns>A parsed BEncodeList</returns>
        public static BEncodeList ToList(byte[] byteArr, out byte[] remainingByteArr)
        {
            remainingByteArr = byteArr;

            // The format of a list is "l4:item11:anotherIteme" (which equals ["item", "anotherItem"])
            // the value is any BEncode entity, list must start with 'l' and be closed with an 'e'
            if (!byteArr.StartsWith('l'))
            {
                throw new Exception("parse error");
            }

            // Remove the 'l'
            remainingByteArr = remainingByteArr.Substring(1);

            var list = new BEncodeList();
            while (!remainingByteArr.StartsWith('e'))
            {
                var value = ToEntity(remainingByteArr, out remainingByteArr);
                list.Add(value);
            }

            // remove the 'e'
            remainingByteArr = remainingByteArr.Substring(1);

            return list;
        }

        /// <summary>
        /// Parses a BEncode entity from a byte array.
        /// </summary>
        /// <param name="byteArr">The source byte array.</param>
        /// <param name="remainingByteArr">The remaining bytes (with the parsed item removed).</param>
        /// <returns>A parsed BEncode entity</returns>
        public static IBEncodeEntity ToEntity(byte[] byteArr, out byte[] remainingByteArr)
        {
            remainingByteArr = byteArr;
            if (byteArr == null || byteArr.Length == 0)
            {
                return null;
            }

            // Dictionary starts with 'd'
            if (byteArr[0] == 'd')
            {
                return ToDictionary(byteArr, out remainingByteArr);
            }

            // Integer starts with 'i'
            if (byteArr[0] == 'i')
            {
                return ToInt(byteArr, out remainingByteArr);
            }

            // List starts with 'l'
            if (byteArr[0] == 'l')
            {
                return ToList(byteArr, out remainingByteArr);
            }

            // Strings start with a number, defining the length of the string
            if (byteArr[0] >= '0' && byteArr[0] <= '9')
            {
                return ToByteString(byteArr, out remainingByteArr);
            }

            // If nothing matched, throw
            throw new Exception("parse error");
        }
    }
}
