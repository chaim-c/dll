using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005B5 RID: 1461
	public struct IOSLoginOptions
	{
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x060025AD RID: 9645 RVA: 0x00037F03 File Offset: 0x00036103
		// (set) Token: 0x060025AE RID: 9646 RVA: 0x00037F0B File Offset: 0x0003610B
		public IOSCredentials? Credentials { get; set; }

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x060025AF RID: 9647 RVA: 0x00037F14 File Offset: 0x00036114
		// (set) Token: 0x060025B0 RID: 9648 RVA: 0x00037F1C File Offset: 0x0003611C
		public AuthScopeFlags ScopeFlags { get; set; }
	}
}
