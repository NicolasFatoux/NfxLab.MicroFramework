using NfxLab.MicroFramework.Network;
using System.Collections;
using System.IO.Ports;

namespace NfxLab.MicroFramework.Drivers.Grove
{
    /// <summary>
    /// Receiver driver for the RF Link Kit.
    /// </summary>
    public class RFReceiver
    {
        SerialPort port;

        public RFReceiver(string portName)
        {
            port = new SerialPort(portName, RFTransmitter.BaudRate, RFTransmitter.Parity, RFTransmitter.DataBits, RFTransmitter.StopBits);
            port.Open();
        }

        /// <summary>
        /// Receive the data.
        /// </summary>
        /// <returns>An enumeration of byte[].</returns>
        public IEnumerable ReceiveData()
        {
            return PacketManager.Read(port);
        }

    }
}
