using System;
using System.Runtime.Serialization;
using TaleWorlds.Library;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x0200004A RID: 74
	[DataContract]
	[Serializable]
	public class RestDataRequestMessage : RestRequestMessage
	{
		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000054AF File Offset: 0x000036AF
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x000054B7 File Offset: 0x000036B7
		[DataMember]
		public MessageType MessageType { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000054C0 File Offset: 0x000036C0
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x000054C8 File Offset: 0x000036C8
		[DataMember]
		public SessionCredentials SessionCredentials { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x000054D1 File Offset: 0x000036D1
		// (set) Token: 0x060001A7 RID: 423 RVA: 0x000054D9 File Offset: 0x000036D9
		[DataMember]
		public byte[] MessageData { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x000054E2 File Offset: 0x000036E2
		// (set) Token: 0x060001A9 RID: 425 RVA: 0x000054EA File Offset: 0x000036EA
		[DataMember]
		public string MessageName { get; private set; }

		// Token: 0x060001AA RID: 426 RVA: 0x000054F3 File Offset: 0x000036F3
		public Message GetMessage()
		{
			return (Message)Common.DeserializeObject(this.MessageData);
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00005505 File Offset: 0x00003705
		public RestDataRequestMessage()
		{
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000550D File Offset: 0x0000370D
		public RestDataRequestMessage(SessionCredentials sessionCredentials, Message message, MessageType messageType)
		{
			this.MessageData = Common.SerializeObject(message);
			this.MessageType = messageType;
			this.SessionCredentials = sessionCredentials;
			this.MessageName = message.GetType().Name;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005540 File Offset: 0x00003740
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				"Rest Data Request Message: ",
				this.MessageName,
				"-",
				this.MessageType
			});
		}
	}
}
