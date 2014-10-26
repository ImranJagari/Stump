

// Generated on 10/26/2014 23:30:18
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
    public class HumanOptionTitle : HumanOption
    {
        public const short Id = 408;
        public override short TypeId
        {
            get { return Id; }
        }
        
        public short titleId;
        public string titleParam;
        
        public HumanOptionTitle()
        {
        }
        
        public HumanOptionTitle(short titleId, string titleParam)
        {
            this.titleId = titleId;
            this.titleParam = titleParam;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            base.Serialize(writer);
            writer.WriteShort(titleId);
            writer.WriteUTF(titleParam);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            base.Deserialize(reader);
            titleId = reader.ReadShort();
            if (titleId < 0)
                throw new Exception("Forbidden value on titleId = " + titleId + ", it doesn't respect the following condition : titleId < 0");
            titleParam = reader.ReadUTF();
        }
        
        public override int GetSerializationSize()
        {
            return base.GetSerializationSize() + sizeof(short) + sizeof(short) + Encoding.UTF8.GetByteCount(titleParam);
        }
        
    }
    
}