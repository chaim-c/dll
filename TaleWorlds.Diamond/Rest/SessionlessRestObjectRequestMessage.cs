using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x02000042 RID: 66
	[DataContract]
	[Serializable]
	public class SessionlessRestObjectRequestMessage : SessionlessRestRequestMessage
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600015D RID: 349 RVA: 0x00004675 File Offset: 0x00002875
		// (set) Token: 0x0600015E RID: 350 RVA: 0x0000467D File Offset: 0x0000287D
		[DataMember]
		public MessageType MessageType { get; private set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600015F RID: 351 RVA: 0x00004686 File Offset: 0x00002886
		// (set) Token: 0x06000160 RID: 352 RVA: 0x0000468E File Offset: 0x0000288E
		[DataMember]
		public Message Message { get; private set; }

		// Token: 0x06000161 RID: 353 RVA: 0x00004697 File Offset: 0x00002897
		public SessionlessRestObjectRequestMessage()
		{
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000469F File Offset: 0x0000289F
		public SessionlessRestObjectRequestMessage(Message message, MessageType messageType)
		{
			this.Message = message;
			this.MessageType = messageType;
		}
	}
}
