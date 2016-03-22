using System;
using mainlineDHT.BEncode;
using mainlineDHT.BEncode.ConversionHelpers;

namespace mainlineDHT.Contract
{
    public class TorrentMetainfo
    {
        public TorrentInfoDictionary Info { get; set; }

        public Uri Announce { get; set; }

        public DateTime? CreationTime { get; set; }

        public string Comment { get; set; }

        public string CreatedBy { get; set; }

        public string Encoding { get; set; }

        public static TorrentMetainfo FromBEncode(BEncodeDictionary entity)
        {
            var announceString =  entity.ValueOrNull("announce").AsString();
            
            var metainfo = new TorrentMetainfo();
            metainfo.Comment = entity.ValueOrNull("comment").AsString();
            metainfo.Encoding = entity.ValueOrNull("encoding").AsString();
            metainfo.CreatedBy = entity.ValueOrNull("created by").AsString();
            metainfo.Announce = announceString == null ? null : new Uri(announceString);

            var creationTimeLong = entity.ValueOrNull("creation date").AsLong();
            if (creationTimeLong != null)
            {
                var dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                metainfo.CreationTime = dtDateTime.AddSeconds(creationTimeLong.Value);
            }
            return metainfo;
        }
    }
}