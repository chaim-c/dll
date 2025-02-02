using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x0200003D RID: 61
	[DataContract]
	[Serializable]
	public class RestObjectResponseMessage : RestResponseMessage
	{
		// Token: 0x06000139 RID: 313 RVA: 0x0000403A File Offset: 0x0000223A
		public override Message GetMessage()
		{
			return this._message;
		}

		// Token: 0x0600013A RID: 314 RVA: 0x00004042 File Offset: 0x00002242
		public RestObjectResponseMessage()
		{
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000404A File Offset: 0x0000224A
		public RestObjectResponseMessage(Message message)
		{
			this._message = message;
		}

		// Token: 0x04000055 RID: 85
		[DataMember]
		private Message _message;
	}
}
