using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainlineDHT.BEncode.Formatters
{
    /// <summary>
    /// A class to generate JSON from BEncoded entities. This can be used to pretty-print the values.
    /// </summary>
    public static class Json
    {
        public static string ToJson(this IBEncodeEntity entity)
        {
            if (entity is BEncodeInt)
            {
                return IntToJson(entity as BEncodeInt);
            }

            if (entity is BEncodeByteString)
            {
                return StringToJson(entity as BEncodeByteString);
            }

            if (entity is BEncodeList)
            {
                return ListToJson(entity as BEncodeList);
            }

            if (entity is BEncodeDictionary)
            {
                return DictionaryToJson(entity as BEncodeDictionary);
            }

            throw new Exception("Unexpected BEncode type");
        }


        private static string ListToJson(BEncodeList source)
        {
            var sb = new StringBuilder();
            sb.Append("[");

            sb.Append(string.Join(",", source.Values.Select(ToJson)));

            sb.Append("]");

            return sb.ToString();
        }

        private static string DictionaryToJson(BEncodeDictionary source)
        {
            var sb = new StringBuilder();
            sb.Append("{");

            sb.Append(string.Join(",", source.Select(e => "\"" + e.Key + "\" :" + e.Value.ToJson())));
            
            //sb.Append(string.Join(",", source.Values.Select(ToJson)));

            sb.Append("}");

            return sb.ToString();
        }

        private static string IntToJson(BEncodeInt source)
        {
            return source.ToString();
        }

        private static string StringToJson(BEncodeByteString source)
        {
            var src = source.ToString();

            // filter out unprintable chars - 0-31 and 127
            src = string.Join(string.Empty, src.Where(s => s > 31 && s != 127));

            // Can't have terminators in the string, so repalce them.
            src = src.Replace("\\", "\\\\");
            src = src.Replace("\"", "\\\"");

            return "\"" + src  + "\"";
        }
    }
}
