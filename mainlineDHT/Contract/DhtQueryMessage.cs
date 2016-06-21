namespace mainlineDHT.Contract
{
    using mainlineDHT.BEncode;

    public class DhtQueryMessage : DhtMessage
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

        public static DhtQueryMessage FromBEncode(BEncodeDictionary entity)
        {
            var dhtMessage = DhtMessage.FromBEncode(entity);
            var dhtQueryMessage = new DhtQueryMessage();
            dhtQueryMessage.MessageType = dhtMessage.MessageType;
            dhtQueryMessage.TransactionId = dhtMessage.TransactionId;

            if (entity.ContainsKey("q"))
            {
                dhtQueryMessage.QueryMethod = entity["q"].ToString();
            }

            if (entity.ContainsKey("a"))
            {
                var arguments = entity["a"] as BEncodeDictionary;
                dhtQueryMessage.Arguments = arguments;
            }

            return dhtQueryMessage;
        }
    }
}
