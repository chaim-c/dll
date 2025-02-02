using System;
using System.Runtime.Serialization;

namespace TaleWorlds.Diamond.Rest
{
	// Token: 0x0200004D RID: 77
	[DataContract]
	[Serializable]
	public abstract class RestRequestMessage : RestData
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x000055CC File Offset: 0x000037CC
		// (set) Token: 0x060001B7 RID: 439 RVA: 0x000055D4 File Offset: 0x000037D4
		[DataMember]
		public byte[] UserCertificate { get; set; }
	}
}
