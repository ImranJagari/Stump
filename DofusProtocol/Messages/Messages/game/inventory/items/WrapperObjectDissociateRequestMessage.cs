

// Generated on 10/26/2014 23:29:41
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class WrapperObjectDissociateRequestMessage : Message
    {
        public const uint Id = 6524;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public int hostUID;
        public byte hostPos;
        
        public WrapperObjectDissociateRequestMessage()
        {
        }
        
        public WrapperObjectDissociateRequestMessage(int hostUID, byte hostPos)
        {
            this.hostUID = hostUID;
            this.hostPos = hostPos;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteInt(hostUID);
            writer.WriteByte(hostPos);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            hostUID = reader.ReadInt();
            if (hostUID < 0)
                throw new Exception("Forbidden value on hostUID = " + hostUID + ", it doesn't respect the following condition : hostUID < 0");
            hostPos = reader.ReadByte();
            if (hostPos < 0 || hostPos > 255)
                throw new Exception("Forbidden value on hostPos = " + hostPos + ", it doesn't respect the following condition : hostPos < 0 || hostPos > 255");
        }
        
        public override int GetSerializationSize()
        {
            return sizeof(int) + sizeof(byte);
        }
        
    }
    
}