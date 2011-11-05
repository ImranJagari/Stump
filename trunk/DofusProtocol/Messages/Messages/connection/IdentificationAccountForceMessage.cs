// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IdentificationAccountForceMessage.xml' the '05/11/2011 17:25:44'
using System;
using Stump.Core.IO;
using System.Collections.Generic;

namespace Stump.DofusProtocol.Messages
{
	public class IdentificationAccountForceMessage : IdentificationMessage
	{
		public const uint Id = 6119;
		public override uint MessageId
		{
			get
			{
				return 6119;
			}
		}
		
		public string forcedAccountLogin;
		
		public IdentificationAccountForceMessage()
		{
		}
		
		public IdentificationAccountForceMessage(Types.Version version, string login, string password, IEnumerable<Types.TrustCertificate> certificate, bool autoconnect, string forcedAccountLogin)
			 : base(version, login, password, certificate, autoconnect)
		{
			this.forcedAccountLogin = forcedAccountLogin;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUTF(forcedAccountLogin);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			forcedAccountLogin = reader.ReadUTF();
		}
	}
}
