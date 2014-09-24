using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpPcap;
using PacketDotNet;
using SharpPcap.LibPcap;
using NetCard;

namespace ReadingPackage
{
    class BaseRead
    {
        private static Byte[] KeyWord;
        private static string argLayerType;
        static void Main(string[] args)
        {
            if (args.Length == 0){
                //Console.WriteLine("no Key Word.. by by..");
                //Environment.Exit(0);
                argLayerType = "t";
            } else
                argLayerType = args[0];


            var devices = CaptureDeviceList.Instance;
            int numberDevice = -1;
            foreach (var dev in devices) {
                Console.WriteLine(dev);
            }

            Console.WriteLine("enter number device.. (see flags) ");

            numberDevice = Convert.ToInt32(Console.ReadLine());
            if (numberDevice != -1)
            {
                ICaptureDevice Device = devices[numberDevice];

                Encoding EncodUnicode = Encoding.Unicode;
                //KeyWord = EncodUnicode.GetBytes(args[0]);

                Device.OnPacketArrival += new SharpPcap.PacketArrivalEventHandler(device_OnPaketArrival);

                Device.Open(DeviceMode.Normal);

                Console.WriteLine("Listein {0}", Device.Description);

                Device.StartCapture();

                Console.ReadLine();

                Device.StopCapture();
                Device.Close();
            }
            else {
                Console.WriteLine("Device not active..");
                Environment.Exit(0);
            }
             
          }

          static void device_OnPaketArrival(object sender, CaptureEventArgs e) {
              Packet packet = Packet.ParsePacket(e.Packet.LinkLayerType, e.Packet.Data);
              if(argLayerType == "l")
                printHead(packet.Header);

              if (argLayerType == "n") {
                  IpPacket ipPac = packet.Extract(typeof(IpPacket)) as IpPacket;
                  if (ipPac != null)
                  {
                      Console.WriteLine("Ver: {0} ", ipPac.Version);
                      Console.WriteLine("Sadr: {0}", ipPac.SourceAddress);
                      printHead(packet.PayloadPacket.Header);

                  }

              }

              if (argLayerType == "t")
              {
                  var tcpPac = packet.Extract(typeof(TcpPacket)) as TcpPacket;
                  if (tcpPac != null) {
                      printHead(packet.PayloadPacket.PayloadPacket.Header);
                      Console.WriteLine("SPort: {0}", tcpPac.SourcePort);
                      Console.WriteLine("DPort: {0}", tcpPac.DestinationPort);
                      Console.WriteLine("Check: {0}", tcpPac.Checksum);
                      Console.WriteLine("PayData: {0}", Encoding.ASCII.GetString(tcpPac.PayloadData));
                  }
              }
                
          }


          static void printHead(byte[] head)
          {
              int i;
              for (i = 0; i < head.Length; i++) {
                  if (i % 16 == 15)
                      Console.WriteLine();
                  Console.Write("{0} ", head[i].ToString("X"));
              }
              Console.WriteLine();
              Console.WriteLine("====================================================");
          }
      }
   }

   

