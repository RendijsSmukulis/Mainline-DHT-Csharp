using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainlineDHT.Contract
{
    public class TorrentInfoDictionary
    {
        public long? PieceLength { get; set; }

        public string Name { get; set; }

        public long? Length { get; set; }
    }
}
