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
        /// Gets or sets the query method.
        /// </summary>
        /// <remarks>"q"</remarks>        
        public string QueryMethod { get; set; }

        /// <summary>
        /// Gets or sets the arguments.
        /// </summary>
        /// <remarks>"a"</remarks>
        public BEncodeDictionary Arguments { get; set; }

        /// <summary>
        /// Gets or sets the response values.
        /// </summary>
        /// <remarks>"r"</remarks>
        public BEncodeDictionary Response { get; set; }


        public static DhtResponseMessage FromBEncode(BEncodeDictionary entity)
        {
            var dhtMessage = DhtMessage.FromBEncode(entity) as DhtResponseMessage;

            if (entity.ContainsKey("r"))
            {
                var response = entity["r"] as BEncodeDictionary;
                dhtMessage.Response = response;
            }

            return dhtMessage;
        }
    }
}
