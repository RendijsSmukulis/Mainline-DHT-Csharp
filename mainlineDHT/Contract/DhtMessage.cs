using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainlineDHT.Contract
{
    using mainlineDHT.BEncode;

    using mainlineDHT.BEncode.ConversionHelpers;

    public class DhtMessage
    {
        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        /// <remarks>"t"</remarks>
        public BEncodeByteString TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the type of the message.
        /// </summary>
        /// <remarks>"y"</remarks>
        public DhtMessageType MessageType { get; set; }

        public static DhtMessage FromBEncode(BEncodeDictionary entity)
        {
            var dhtMessage = new DhtMessage();
            dhtMessage.TransactionId = entity.ValueOrNull("t") as BEncodeByteString;

            if (entity.ContainsKey("y"))
            {
                var type = entity.ValueOrNull("y").ToString();

                if (type == "q")
                {
                    dhtMessage.MessageType = DhtMessageType.Query;
                }

                if (type == "r")
                {
                    dhtMessage.MessageType = DhtMessageType.Response;
                }
            }

            return dhtMessage;
        }
    }
}
