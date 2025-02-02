using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200057F RID: 1407
	public struct IdToken
	{
		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002403 RID: 9219 RVA: 0x000359B1 File Offset: 0x00033BB1
		// (set) Token: 0x06002404 RID: 9220 RVA: 0x000359B9 File Offset: 0x00033BB9
		public EpicAccountId AccountId { get; set; }

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002405 RID: 9221 RVA: 0x000359C2 File Offset: 0x00033BC2
		// (set) Token: 0x06002406 RID: 9222 RVA: 0x000359CA File Offset: 0x00033BCA
		public Utf8String JsonWebToken { get; set; }

		// Token: 0x06002407 RID: 9223 RVA: 0x000359D3 File Offset: 0x00033BD3
		internal void Set(ref IdTokenInternal other)
		{
			this.AccountId = other.AccountId;
			this.JsonWebToken = other.JsonWebToken;
		}
	}
}
