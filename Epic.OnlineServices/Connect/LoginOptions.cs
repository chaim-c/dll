using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200053C RID: 1340
	public struct LoginOptions
	{
		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002268 RID: 8808 RVA: 0x00033757 File Offset: 0x00031957
		// (set) Token: 0x06002269 RID: 8809 RVA: 0x0003375F File Offset: 0x0003195F
		public Credentials? Credentials { get; set; }

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x00033768 File Offset: 0x00031968
		// (set) Token: 0x0600226B RID: 8811 RVA: 0x00033770 File Offset: 0x00031970
		public UserLoginInfo? UserLoginInfo { get; set; }
	}
}
