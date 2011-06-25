// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'FriendInformations.xml' the '24/06/2011 12:04:58'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class FriendInformations
	{
		public const uint Id = 78;
		public virtual short TypeId
		{
			get
			{
				return 78;
			}
		}
		
		public string name;
		public byte playerState;
		public int lastConnection;
		
		public FriendInformations()
		{
		}
		
		public FriendInformations(string name, byte playerState, int lastConnection)
		{
			this.name = name;
			this.playerState = playerState;
			this.lastConnection = lastConnection;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteUTF(name);
			writer.WriteByte(playerState);
			writer.WriteInt(lastConnection);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			name = reader.ReadUTF();
			playerState = reader.ReadByte();
			if ( playerState < 0 )
			{
				throw new Exception("Forbidden value on playerState = " + playerState + ", it doesn't respect the following condition : playerState < 0");
			}
			lastConnection = reader.ReadInt();
			if ( lastConnection < 0 )
			{
				throw new Exception("Forbidden value on lastConnection = " + lastConnection + ", it doesn't respect the following condition : lastConnection < 0");
			}
		}
	}
}
