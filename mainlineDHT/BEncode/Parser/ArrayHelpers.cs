// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArrayHelpers.cs" company="Rendijs Smukulis">
//   By rendijs.smukulis@gmail.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace mainlineDHT.BEncode.Parser
{
    using System.Linq;

    /// <summary>
    /// Static methods to expand array operations.
    /// </summary>
    public static class ArrayHelpers
    {
        /// <summary>
        /// Gets a substring of the specified array.
        /// </summary>
        /// <typeparam name="T">Type of the items in the array</typeparam>
        /// <param name="arr">The array to find the substring of</param>
        /// <param name="start">Index (inclusive) where to start sub-stringing the array</param>
        /// <returns>A new array that's a substring of the old array</returns>
        public static T[] Substring<T>(this T[] arr, int start)
        {
            return arr.Skip(start).ToArray();
        }

        /// <summary>
        /// Gets a substring of the specified array.
        /// </summary>
        /// <typeparam name="T">Type of the items in the array</typeparam>
        /// <param name="arr">The array to find the substring of</param>
        /// <param name="start">Index (inclusive) where to start sub-stringing the array</param>
        /// <param name="length">The number of items to include in substring.</param>
        /// <returns>
        /// A new array that's a substring of the old array
        /// </returns>
        public static T[] Substring<T>(this T[] arr, int start, int length)
        {
            return arr.Skip(start).Take(length).ToArray();
        }

        /// <summary>
        /// Identifies if the byte array starts with a certain char
        /// </summary>
        /// <remarks>
        /// Ghostdoc suggested "Startses the with". Dirty little hobbitsses.
        /// </remarks>
        /// <param name="arr">The array to check.</param>
        /// <param name="startsWith">The character to check for.</param>
        /// <returns>True if the array starts with that character, false otherwise.</returns>
        public static bool StartsWith(this byte[] arr, char startsWith)
        {
            return arr.StartsWith((byte)startsWith);
        }

        /// <summary>
        /// Identifies if the byte array starts with a certain byte
        /// </summary>
        /// <param name="arr">The array to check.</param>
        /// <param name="startsWith">The byte to check for.</param>
        /// <returns>True if the array starts with that byte, false otherwise.</returns>
        public static bool StartsWith(this byte[] arr, byte startsWith)
        {
            if (arr == null || arr.Length == 0)
            {
                return false;
            }

            return arr[0] == startsWith;
        }
    }
}
