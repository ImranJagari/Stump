using System;
using System.IO;
using Stump.Core.IO;

namespace Stump.Server.BaseServer.IPC
{
    public class IPCMessagePart
    {
        private byte[] m_data;
        private bool m_dataMissing;

        /// <summary>
        ///     Set to true when the message is whole
        /// </summary>
        public bool IsValid
        {
            get
            {
                return LengthBytesCount.HasValue && Length.HasValue && Data != null &&
                       Length == Data.Length;
            }
        }


        public byte? LengthBytesCount
        {
            get;
            private set;
        }

        public int? Length
        {
            get;
            private set;
        }

        /// <summary>
        ///     Set only if ReadData or ExceedBufferSize is true
        /// </summary>
        public byte[] Data
        {
            get { return m_data; }
            private set { m_data = value; }
        }

        /// <summary>
        ///     Build or continue building the message. Returns true if the resulted message is valid and ready to be parsed
        /// </summary>
        public bool Build(BinaryReader reader, long count)
        {
            if (count <= 0)
                return false;

            if (IsValid)
                return true;

            if (!LengthBytesCount.HasValue && count < 1)
                return false;

            if (count >= 1 && !LengthBytesCount.HasValue)
                LengthBytesCount = reader.ReadByte();

            if (LengthBytesCount.HasValue &&
                count >= LengthBytesCount && !Length.HasValue)
            {
                Length = 0;

                for (var i = LengthBytesCount.Value - 1; i >= 0; i--)
                {
                    Length |= reader.ReadByte() << (i*8);
                }
            }

            // first case : no data read
            if (Length.HasValue && !m_dataMissing)
            {
                if (Length == 0)
                {
                    Data = new byte[0];
                    return true;
                }

                // enough bytes in the buffer to build a complete message
                if (count >= Length)
                {
                    Data = reader.ReadBytes(Length.Value);

                    return true;
                }

                // not enough bytes, so we read what we can
                if (!(Length > count))
                    return IsValid;

                Data = reader.ReadBytes((int) count);

                m_dataMissing = true;
                return false;
            }

            //second case : the message was split and it missed some bytes
            if (!Length.HasValue || !m_dataMissing)
                return IsValid;

            // still miss some bytes ...
            if (Data.Length + count < Length)
            {
                var lastLength = m_data.Length;
                Array.Resize(ref m_data, (int) (Data.Length + count));
                var array = reader.ReadBytes((int) count);

                Array.Copy(array, 0, Data, lastLength, array.Length);

                m_dataMissing = true;
                return false;
            }

            // there is enough bytes in the buffer to complete the message :)
            if (Data.Length + count >= Length)
            {
                var bytesToRead = Length.Value - Data.Length;


                Array.Resize(ref m_data, Data.Length + bytesToRead);
                var array = reader.ReadBytes(bytesToRead);

                Array.Copy(array, 0, Data, Data.Length - bytesToRead, bytesToRead);
            }

            return IsValid;
        }
    }
}