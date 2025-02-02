using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000589 RID: 1417
	public struct LoginOptions
	{
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x0600245A RID: 9306 RVA: 0x000362E8 File Offset: 0x000344E8
		// (set) Token: 0x0600245B RID: 9307 RVA: 0x000362F0 File Offset: 0x000344F0
		public Credentials? Credentials { get; set; }

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x0600245C RID: 9308 RVA: 0x000362F9 File Offset: 0x000344F9
		// (set) Token: 0x0600245D RID: 9309 RVA: 0x00036301 File Offset: 0x00034501
		public AuthScopeFlags ScopeFlags { get; set; }
	}
}
