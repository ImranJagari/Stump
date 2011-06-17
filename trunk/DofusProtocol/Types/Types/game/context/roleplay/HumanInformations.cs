// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'HumanInformations.xml' the '14/06/2011 11:32:47'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Types
{
	public class HumanInformations
	{
		public const uint Id = 157;
		public short TypeId
		{
			get
			{
				return 157;
			}
		}
		
		public Types.EntityLook[] followingCharactersLook;
		public byte emoteId;
		public ushort emoteEndTime;
		public Types.ActorRestrictionsInformations restrictions;
		public short titleId;
		public string titleParam;
		
		public HumanInformations()
		{
		}
		
		public HumanInformations(Types.EntityLook[] followingCharactersLook, byte emoteId, ushort emoteEndTime, Types.ActorRestrictionsInformations restrictions, short titleId, string titleParam)
		{
			this.followingCharactersLook = followingCharactersLook;
			this.emoteId = emoteId;
			this.emoteEndTime = emoteEndTime;
			this.restrictions = restrictions;
			this.titleId = titleId;
			this.titleParam = titleParam;
		}
		
		public virtual void Serialize(IDataWriter writer)
		{
			writer.WriteUShort((ushort)followingCharactersLook.Length);
			for (int i = 0; i < followingCharactersLook.Length; i++)
			{
				followingCharactersLook[i].Serialize(writer);
			}
			writer.WriteByte(emoteId);
			writer.WriteUShort(emoteEndTime);
			restrictions.Serialize(writer);
			writer.WriteShort(titleId);
			writer.WriteUTF(titleParam);
		}
		
		public virtual void Deserialize(IDataReader reader)
		{
			int limit = reader.ReadUShort();
			followingCharactersLook = new Types.EntityLook[limit];
			for (int i = 0; i < limit; i++)
			{
				followingCharactersLook[i] = new Types.EntityLook();
				followingCharactersLook[i].Deserialize(reader);
			}
			emoteId = reader.ReadByte();
			emoteEndTime = reader.ReadUShort();
			if ( emoteEndTime < 0 || emoteEndTime > 65535 )
			{
				throw new Exception("Forbidden value on emoteEndTime = " + emoteEndTime + ", it doesn't respect the following condition : emoteEndTime < 0 || emoteEndTime > 65535");
			}
			restrictions = new Types.ActorRestrictionsInformations();
			restrictions.Deserialize(reader);
			titleId = reader.ReadShort();
			if ( titleId < 0 )
			{
				throw new Exception("Forbidden value on titleId = " + titleId + ", it doesn't respect the following condition : titleId < 0");
			}
			titleParam = reader.ReadUTF();
		}
	}
}