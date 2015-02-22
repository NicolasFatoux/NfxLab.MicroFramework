using NfxLab.MicroFramework.Network;
using System.IO.Ports;

namespace NfxLab.MicroFramework.Drivers.Grove
{
    /// <summary>
    /// Transmitter Driver for the RF Link Kit.
    /// </summary>
    public class RFTransmitter
    {
        // Serial port parameters
        internal const int BaudRate = 2400;
        internal const Parity Parity = System.IO.Ports.Parity.None;
        internal const int DataBits = 8;
        internal const StopBits StopBits = System.IO.Ports.StopBits.One;

        // Initial sequence for receiver initialisation
        readonly byte[] SyncData = new byte[] { 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0xFF, 0x00 };
       
        SerialPort port;

        public RFTransmitter(string portName)
        {
            port = new SerialPort(portName, BaudRate, Parity, DataBits, StopBits);
        }

        /// <summary>
        /// Send data
        /// </summary>
        /// <param name="data">Data</param>
        /// <param name="repeat">number of time the packet will be repeated</param>
        public void Send(byte[] data, int repeat = 0)
        {
            var packet = PacketManager.BuildPacket(data);

            port.Open();
            
            port.Write(SyncData, 0, SyncData.Length);
            for (int i = 0; i < repeat + 1; i++)
                port.Write(packet, 0, packet.Length);
            
            port.Close();
        }
    }
}
