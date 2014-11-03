using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mainlineDHT;
using mainlineDHT.BEncode;
using mainlineDHT.BEncode.Formatters;

using mainlineCapture;

namespace testapp
{
    using System.IO;

    using mainlineDHT.BEncode.Parser;

    class Program
    {
        static void Main(string[] args)
        {
            // ExerciseParser();
            ExerciseCapture();
            Debugger.Break();
        }

        static void ExerciseCapture()
        {
            mainlineCapture.CaptureHandler.EnumerateDevices();
        }

        static void ExerciseParser()
        {
            var outerDic = new BEncodeDictionary();
            var innderDic = new BEncodeDictionary();
            innderDic.Add("id", new BEncodeByteString("...;..o....Mk'.d..c."));
            innderDic.Add("info_hash", new BEncodeByteString(".#=.G.....N.).,l..q."));
            outerDic.Add("a", innderDic);

            outerDic.Add("q", new BEncodeByteString("get_peers"));
            outerDic.Add("t", new BEncodeByteString("...."));
            outerDic.Add("v", new BEncodeByteString("Utw."));
            outerDic.Add("y", new BEncodeByteString("q"));


            var formatted = outerDic.ToBytes();

            var str = Encoding.ASCII.GetString(formatted);

            var json = outerDic.ToJson();


            var filePath = @"testfiles\testfile2.torrent";

            var str2 = File.ReadAllBytes(filePath);

            byte[] remStr;
            //var parsed = ToEntity.ToEntity("d2:id4:abbae", out remStr);

            var sw = new Stopwatch();
            sw.Start();

            var parsed = FromBytes.ToEntity(str2, out remStr) as BEncodeDictionary;
            sw.Stop();
            Console.WriteLine("Bencode parse took: " + sw.ElapsedMilliseconds + "ms");
            sw.Restart();

            parsed.Remove(new BEncodeByteString("pieces"));

            var parsedJson = parsed.ToJson();

            sw.Stop();
            Console.WriteLine("JSON parse took: " + sw.ElapsedMilliseconds + "ms");


            File.WriteAllBytes(filePath + "_", parsed.ToBytes());
        }
    }
}
