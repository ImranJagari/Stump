

// Generated on 10/30/2016 16:20:37
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Stump.Core.IO;
using Stump.DofusProtocol.Types;

namespace Stump.DofusProtocol.Messages
{
    public class FriendUpdateMessage : Message
    {
        public const uint Id = 5924;
        public override uint MessageId
        {
            get { return Id; }
        }
        
        public Types.FriendInformations friendUpdated;
        
        public FriendUpdateMessage()
        {
        }
        
        public FriendUpdateMessage(Types.FriendInformations friendUpdated)
        {
            this.friendUpdated = friendUpdated;
        }
        
        public override void Serialize(IDataWriter writer)
        {
            writer.WriteShort(friendUpdated.TypeId);
            friendUpdated.Serialize(writer);
        }
        
        public override void Deserialize(IDataReader reader)
        {
            friendUpdated = Types.ProtocolTypeManager.GetInstance<Types.FriendInformations>(reader.ReadShort());
            friendUpdated.Deserialize(reader);
        }
        
    }
    
}