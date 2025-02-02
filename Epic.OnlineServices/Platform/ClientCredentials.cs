using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000648 RID: 1608
	public struct ClientCredentials
	{
		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06002911 RID: 10513 RVA: 0x0003D535 File Offset: 0x0003B735
		// (set) Token: 0x06002912 RID: 10514 RVA: 0x0003D53D File Offset: 0x0003B73D
		public Utf8String ClientId { get; set; }

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06002913 RID: 10515 RVA: 0x0003D546 File Offset: 0x0003B746
		// (set) Token: 0x06002914 RID: 10516 RVA: 0x0003D54E File Offset: 0x0003B74E
		public Utf8String ClientSecret { get; set; }

		// Token: 0x06002915 RID: 10517 RVA: 0x0003D557 File Offset: 0x0003B757
		internal void Set(ref ClientCredentialsInternal other)
		{
			this.ClientId = other.ClientId;
			this.ClientSecret = other.ClientSecret;
		}
	}
}
