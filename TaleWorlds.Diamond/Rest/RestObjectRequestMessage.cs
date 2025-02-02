using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x0200004B RID: 75
	[DataContract]
	[Serializable]
	public class RestObjectRequestMessage : RestRequestMessage
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005574 File Offset: 0x00003774
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0000557C File Offset: 0x0000377C
		[DataMember]
		public MessageType MessageType { get; private set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00005585 File Offset: 0x00003785
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x0000558D File Offset: 0x0000378D
		[DataMember]
		public SessionCredentials SessionCredentials { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00005596 File Offset: 0x00003796
		// (set) Token: 0x060001B3 RID: 435 RVA: 0x0000559E File Offset: 0x0000379E
		[DataMember]
		public Message Message { get; private set; }

		// Token: 0x060001B4 RID: 436 RVA: 0x000055A7 File Offset: 0x000037A7
		public RestObjectRequestMessage()
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000055AF File Offset: 0x000037AF
		public RestObjectRequestMessage(SessionCredentials sessionCredentials, Message message, MessageType messageType)
		{
			this.Message = message;
			this.MessageType = messageType;
			this.SessionCredentials = sessionCredentials;
		}
	}
}
