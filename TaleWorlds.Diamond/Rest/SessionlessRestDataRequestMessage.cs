using System;
using System.Runtime.Serialization;
using TaleWorlds.Library;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x02000041 RID: 65
	[DataContract]
	[Serializable]
	public class SessionlessRestDataRequestMessage : SessionlessRestRequestMessage
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000156 RID: 342 RVA: 0x0000461E File Offset: 0x0000281E
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00004626 File Offset: 0x00002826
		[DataMember]
		public MessageType MessageType { get; private set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000158 RID: 344 RVA: 0x0000462F File Offset: 0x0000282F
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00004637 File Offset: 0x00002837
		[DataMember]
		public byte[] MessageData { get; private set; }

		// Token: 0x0600015A RID: 346 RVA: 0x00004640 File Offset: 0x00002840
		public Message GetMessage()
		{
			return (Message)Common.DeserializeObject(this.MessageData);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x00004652 File Offset: 0x00002852
		public SessionlessRestDataRequestMessage()
		{
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000465A File Offset: 0x0000285A
		public SessionlessRestDataRequestMessage(Message message, MessageType messageType)
		{
			this.MessageData = Common.SerializeObject(message);
			this.MessageType = messageType;
		}
	}
}
