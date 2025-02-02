using System;
using System.Runtime.Serialization;
using TaleWorlds.Library;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x0200003A RID: 58
	[DataContract]
	[Serializable]
	public class RestDataResponseMessage : RestResponseMessage
	{
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x0600012F RID: 303 RVA: 0x00003FD4 File Offset: 0x000021D4
		// (set) Token: 0x06000130 RID: 304 RVA: 0x00003FDC File Offset: 0x000021DC
		[DataMember]
		public byte[] MessageData { get; private set; }

		// Token: 0x06000131 RID: 305 RVA: 0x00003FE5 File Offset: 0x000021E5
		public override Message GetMessage()
		{
			return (Message)Common.DeserializeObject(this.MessageData);
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00003FF7 File Offset: 0x000021F7
		public RestDataResponseMessage()
		{
		}

		// Token: 0x06000133 RID: 307 RVA: 0x00003FFF File Offset: 0x000021FF
		public RestDataResponseMessage(Message message)
		{
			this.MessageData = Common.SerializeObject(message);
		}
	}
}
