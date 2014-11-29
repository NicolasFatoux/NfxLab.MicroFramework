#if NETWORK
using System;
using System.Collections;
using System.IO;

namespace NfxLab.MicroFramework.Network
{
    public class PacketManager
    {
        static byte currentId = 0;
        static readonly byte[] StartSequence = new byte[3] { 0xFF, 0xFF, 0xFF };

        /// <summary>
        /// Builds a Secure Packet
        /// </summary>
        /// <param name="data">Data</param>
        /// <returns></returns>
        public static byte[] BuildPacket(byte[] data)
        {
            if (data.Length > byte.MaxValue)
                throw new ArgumentException("data too big !", "data");

            int index;

            // Creating packet
            byte[] packet = new byte[StartSequence.Length + 4 + data.Length];

            // Writing start sequence
            Array.Copy(StartSequence, packet, StartSequence.Length);
            index = StartSequence.Length;

            // Writing header
            // - ID
            packet[index++] = currentId++;

            // - Checksum
            byte[] checksum = CheckSum(data);
            packet[index++] = checksum[0];
            packet[index++] = checksum[1];

            // - Length
            packet[index++] = (byte)data.Length;

            // Writing data
            Array.Copy(data, 0, packet, index, data.Length);

            return packet;
        }


        /// <summary>
        /// Reads data from a Stream
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns>An IEnumerable of byte[] containing packet data</returns>
        public static IEnumerable Read(Stream stream)
        {
            while (stream.CanRead)
            {
                // Reading start sequence
                ReadStartSequence(stream);

                // Reading header
                // - ID
                byte id = (byte)stream.ReadByte();
                if (id == currentId)
                {
                    // We skip the packet if it has already been read
                    continue;
                }

                // - Checksum & size
                byte checksum0 = (byte)stream.ReadByte();
                byte checksum1 = (byte)stream.ReadByte();
                byte[] checksum = new byte[] { checksum0, checksum1 };
                byte size = (byte)stream.ReadByte();

                // Reading data
                byte[] data = new byte[size];
                for (int i = 0; i < size; i++)
                    data[i] = (byte)stream.ReadByte();

                // Integrity check
                byte[] dataChecksum = CheckSum(data);
                if (dataChecksum[0] != checksum[0] && dataChecksum[1] != checksum[1])
                {
                    // Checksum doesn't match
                    continue;
                }

                // Checksum OK
                currentId = id;
                yield return data;
            }
        }

        /// <summary>
        /// Computes a checksum
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        static byte[] CheckSum(byte[] data)
        {
            byte[] checksum = { 0x00, 0x00 };

            for (int i = 0; i < data.Length; i += 2)
            {
                checksum[0] = (byte)(checksum[i] ^ data[i]);

                int j = i + 1;
                if (j <= data.Length)
                    checksum[1] = (byte)(checksum[1] ^ data[j]);
            }

            return checksum;
        }

        /// <summary>
        /// Reads a start sequence from a Stream.
        /// </summary>
        /// <param name="stream">Stream</param>
        static void ReadStartSequence(Stream stream)
        {
            byte byte0 = (byte)stream.ReadByte();
            byte byte1 = (byte)stream.ReadByte();
            byte byte2 = (byte)stream.ReadByte();

            while (byte0 != StartSequence[0] || byte1 != StartSequence[1] || byte2 != StartSequence[2])
            {
                byte0 = byte1;
                byte1 = byte2;
                byte2 = (byte)stream.ReadByte();
            }
        }
    }
}
#endif