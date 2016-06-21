using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainlineDHT.Contract
{
    using mainlineDHT.BEncode;

    using mainlineDHT.BEncode.ConversionHelpers;

    public class DhtResponseMessage : DhtMessage
    {
        /// <summary>
        /// Gets or sets the response values.
        /// </summary>
        /// <remarks>"r"</remarks>
        public BEncodeDictionary Response { get; set; }

        public override string ToString()
        {
            return "rsp: " + this.TransactionId;
        }

        public static DhtResponseMessage FromBEncode(BEncodeDictionary entity)
        {
            var dhtMessage = DhtMessage.FromBEncode(entity);
            var dhtResponseMessage = new DhtResponseMessage();
            dhtResponseMessage.TransactionId = dhtMessage.TransactionId;
            dhtResponseMessage.MessageType = dhtMessage.MessageType;

            if (entity.ContainsKey("r"))
            {
                var response = entity["r"] as BEncodeDictionary;
                dhtResponseMessage.Response = response;
            }

            return dhtResponseMessage;
        }
    }
}
