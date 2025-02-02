using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x02000045 RID: 69
	[DataContract]
	[Serializable]
	public class AliveMessage : RestRequestMessage
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00004708 File Offset: 0x00002908
		// (set) Token: 0x0600016D RID: 365 RVA: 0x00004710 File Offset: 0x00002910
		[DataMember]
		public SessionCredentials SessionCredentials { get; private set; }

		// Token: 0x0600016E RID: 366 RVA: 0x00004719 File Offset: 0x00002919
		public AliveMessage()
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00004721 File Offset: 0x00002921
		[JsonConstructor]
		public AliveMessage(SessionCredentials sessionCredentials)
		{
			this.SessionCredentials = sessionCredentials;
		}
	}
}
