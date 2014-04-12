// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IBEncodeEntity.cs" company="Rendijs Smukulis">
//   By rendijs.smukulis@gmail.com
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace mainlineDHT.BEncode
{
    /// <summary>
    /// BEncode interface
    /// </summary>
    public interface IBEncodeEntity
    {
        /// <summary>
        /// Serializes the entity to bytes
        /// </summary>
        /// <returns>Entity serialized to a byte array</returns>
        byte[] ToBytes();
    }
}
