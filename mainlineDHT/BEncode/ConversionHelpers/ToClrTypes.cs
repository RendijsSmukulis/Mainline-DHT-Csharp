using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mainlineDHT.BEncode.ConversionHelpers
{
    public static class ToClrTypes
    {
        public static string AsString(this IBEncodeEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            var cast = entity as BEncodeByteString;
            if (cast == null)
            {
                return null;
            }

            return cast.ToString();
        }

        public static long? AsLong(this IBEncodeEntity entity)
        {
            if (entity == null)
            {
                return null;
            }

            var cast = entity as BEncodeInt;
            if (cast == null)
            {
                return null;
            }

            return cast.Value;
        }
    }
}
