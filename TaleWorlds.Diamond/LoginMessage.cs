using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond
{
	// Token: 0x02000017 RID: 23
	[DataContract]
	[Serializable]
	public abstract class LoginMessage : Message
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000065 RID: 101 RVA: 0x0000297A File Offset: 0x00000B7A
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00002982 File Offset: 0x00000B82
		[DataMember]
		public PeerId PeerId { get; set; }

		// Token: 0x06000067 RID: 103 RVA: 0x0000298B File Offset: 0x00000B8B
		public LoginMessage()
		{
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00002993 File Offset: 0x00000B93
		protected LoginMessage(PeerId peerId)
		{
			this.PeerId = peerId;
		}
	}
}
