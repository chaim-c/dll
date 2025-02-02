using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000534 RID: 1332
	public struct IdToken
	{
		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002228 RID: 8744 RVA: 0x0003312B File Offset: 0x0003132B
		// (set) Token: 0x06002229 RID: 8745 RVA: 0x00033133 File Offset: 0x00031333
		public ProductUserId ProductUserId { get; set; }

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x0600222A RID: 8746 RVA: 0x0003313C File Offset: 0x0003133C
		// (set) Token: 0x0600222B RID: 8747 RVA: 0x00033144 File Offset: 0x00031344
		public Utf8String JsonWebToken { get; set; }

		// Token: 0x0600222C RID: 8748 RVA: 0x0003314D File Offset: 0x0003134D
		internal void Set(ref IdTokenInternal other)
		{
			this.ProductUserId = other.ProductUserId;
			this.JsonWebToken = other.JsonWebToken;
		}
	}
}
