// File generated by 'DofusProtocolBuilder.exe v1.0.0.0'
// From 'IdentificationWithLoginTokenMessage.xml' the '24/06/2011 12:04:45'
using System;
using Stump.Core.IO;

namespace Stump.DofusProtocol.Messages
{
	public class IdentificationWithLoginTokenMessage : IdentificationMessage
	{
		public const uint Id = 6201;
		public override uint MessageId
		{
			get
			{
				return 6201;
			}
		}
		
		public string loginToken;
		
		public IdentificationWithLoginTokenMessage()
		{
		}
		
		public IdentificationWithLoginTokenMessage(Types.Version version, string login, string password, Types.TrustCertificate[] certificate, bool autoconnect, string loginToken)
			 : base(version, login, password, certificate, autoconnect)
		{
			this.loginToken = loginToken;
		}
		
		public override void Serialize(IDataWriter writer)
		{
			base.Serialize(writer);
			writer.WriteUTF(loginToken);
		}
		
		public override void Deserialize(IDataReader reader)
		{
			base.Deserialize(reader);
			loginToken = reader.ReadUTF();
		}
	}
}
