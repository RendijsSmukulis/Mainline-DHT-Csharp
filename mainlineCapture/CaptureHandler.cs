using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;

using mainlineDHT;
using mainlineDHT.BEncode.Parser;
using mainlineDHT.BEncode;
using mainlineDHT.BEncode.Formatters;

namespace mainlineCapture
{
    using mainlineDHT.Contract;

    public static class CaptureHandler
    {
        public static void EnumerateDevices()
        {
            IList<LivePacketDevice> allDevices = LivePacketDevice.AllLocalMachine;
            if (allDevices.Count == 0)
            {
                Console.WriteLine("No interfaces found! Make sure WinPcap is installed.");
                return;
            }

            foreach (var device in allDevices)
            {
                Console.Write(device.Name);
                
                if (device.Description != null)
                {
                    Console.WriteLine(" (" + device.Description + ")");
                }
                else
                {
                    Console.WriteLine(" (No description available)");
                }
            }

            using (var communicator = allDevices[4].Open(65536, PacketDeviceOpenAttributes.None, 1000)) {
                Console.WriteLine("Listening on " + allDevices[4].Description + "...");

                // start the capture
                //communicator.CreateFilter("portrange 6881-6889");
                communicator.SetFilter("udp port 8999");
                communicator.ReceivePackets(0, PacketHandler);
            }
        }

        // Callback function invoked by Pcap.Net for every incoming packet
        private static void PacketHandler(Packet packet)
        {
            IpV4Datagram ip = packet.Ethernet.IpV4;
            UdpDatagram udp = ip.Udp;

            if (udp.IsValid && udp.DestinationPort == 8999) 
            {
                byte[] remStr;

                try
                {
                    var parsed = FromBytes.ToEntity(udp.Payload.ToArray(), out remStr) as BEncodeDictionary;

                    if (parsed.ContainsKey("q") || parsed.ContainsKey("r"))
                    {
                        var obj = DhtMessage.FromBEncode(parsed);
                        if (obj.MessageType == DhtMessageType.Query)
                        {
                            obj = DhtQueryMessage.FromBEncode(parsed);
                        }
                        else if (obj.MessageType == DhtMessageType.Response)
                        {
                            obj = DhtResponseMessage.FromBEncode(parsed);
                        }
                        
                        Console.WriteLine(obj);
                    }


                }
                catch
                {
                    
                }
            }
        }
    }
}
