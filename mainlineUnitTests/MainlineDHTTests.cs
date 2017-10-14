using System;
using System.Diagnostics;
using System.IO;
using mainlineDHT.BEncode;
using mainlineDHT.BEncode.Formatters;
using mainlineDHT.BEncode.Parser;
using mainlineDHT.Contract;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace mainlineUnitTests
{
    [TestClass]
    public class MainlineDHTTests
    {
        [TestMethod]
        [DeploymentItem("TestData\\ubuntu-15.10-desktop-amd64.iso.torrent")]
        public void MainlineDHTTests_ValidateBEncodeParsing_DictionaryFromTorrentFile()
        {
            var filePath = @"ubuntu-15.10-desktop-amd64.iso.torrent";

            var str2 = File.ReadAllBytes(filePath);

            byte[] remStr;

            var dictionary = FromBytes.ToEntity(str2, out remStr) as BEncodeDictionary;
            
            Assert.IsNotNull(dictionary, "Dictionary was null");
            Assert.AreEqual(5, dictionary.Keys.Count, "Unexpected number of items in dictionary.");

            Assert.IsTrue(dictionary.ContainsKey(new BEncodeByteString("announce")), "announce missing from dictionary");
            Assert.IsTrue(dictionary.ContainsKey(new BEncodeByteString("announce-list")), "announce-list missing from dictionary");
            Assert.IsTrue(dictionary.ContainsKey(new BEncodeByteString("comment")), "comment missing from dictionary");
            Assert.IsTrue(dictionary.ContainsKey(new BEncodeByteString("creation date")), "creation date missing from dictionary");
            Assert.IsTrue(dictionary.ContainsKey(new BEncodeByteString("info")), "info missing from dictionary");

            var torrentObj = TorrentMetainfo.FromBEncode(dictionary);

            
            
        }
    }
}
