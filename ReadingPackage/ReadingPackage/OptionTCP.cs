using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharpPcap;
using PacketDotNet;
using SharpPcap.LibPcap;
using System.Net;
using System.Net.NetworkInformation;

namespace NetCard
{
    public sealed class OptionTCP
    {
        internal static unsafe bool UnsafeCompareByte(byte[] a1, byte[] a2)
        {
            if (a1 == null || a2 == null || a1.Length != a2.Length)
                return false;
            fixed (byte* p1 = a1, p2 = a2)
            {
                byte* x1 = p1, x2 = p2;
                int l = a1.Length;
                for (int i = 0; i < l / 8; i++, x1 += 8, x2 += 8)
                    if (*((long*)x1) != *((long*)x2))
                        return false;
                if ((l & 4) != 0)
                {
                    if (*((int*)x1) != *((int*)x2)) return false;
                    x1 += 4;
                    x2 += 4;
                }
                if ((l & 2) != 0)
                {
                    if (*((short*)x1) != *((short*)x2)) return false;
                    x1 += 2;
                    x2 += 2;
                }
                if ((l & 1) != 0)
                    if (*((byte*)x1) != *((byte*)x2))
                        return false;
                return true;
            }
        }
    }

    public sealed class InfoNetCard 
    {
    
    }

    //other
    // LibPcapLiveDevice ddevices = LibPcapLiveDevice// other lib on devices mb
    //  int timer = 10000;
    /*  Device.Open(DeviceMode.Normal);

      string capFile = "text.txt";
      CaptureFileWriterDevice q = new CaptureFileWriterDevice(capFile);

      Console.WriteLine("Listen, {0}", Device.Description);

      RawCapture raw = null;
      Packet packet = null; 
      while ((raw = Device.GetNextPacket()) != null) {
          packet = Packet.ParsePacket(raw.LinkLayerType, raw.Data);
          var tcpPac = TcpPacket.GetEncapsulated(packet);
          if (tcpPac != null) {
              Console.WriteLine(tcpPac.ToString());
          }
      }*/

    // LibPcapLiveDevice ddevices = LibPcapLiveDevice// other lib on devices mb
    //  int timer = 10000;
    /*  Device.Open(DeviceMode.Normal);

      string capFile = "text.txt";
      CaptureFileWriterDevice q = new CaptureFileWriterDevice(capFile);

      Console.WriteLine("Listen, {0}", Device.Description);

      RawCapture raw = null;
      Packet packet = null; 
      while ((raw = Device.GetNextPacket()) != null) {
          packet = Packet.ParsePacket(raw.LinkLayerType, raw.Data);
          var tcpPac = TcpPacket.GetEncapsulated(packet);
          if (tcpPac != null) {
              Console.WriteLine(tcpPac.ToString());
          }
      }*/
        

}
