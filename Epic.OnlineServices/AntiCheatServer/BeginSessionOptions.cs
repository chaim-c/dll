using System;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005BE RID: 1470
	public struct BeginSessionOptions
	{
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x060025E0 RID: 9696 RVA: 0x000387BB File Offset: 0x000369BB
		// (set) Token: 0x060025E1 RID: 9697 RVA: 0x000387C3 File Offset: 0x000369C3
		public uint RegisterTimeoutSeconds { get; set; }

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x060025E2 RID: 9698 RVA: 0x000387CC File Offset: 0x000369CC
		// (set) Token: 0x060025E3 RID: 9699 RVA: 0x000387D4 File Offset: 0x000369D4
		public Utf8String ServerName { get; set; }

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060025E4 RID: 9700 RVA: 0x000387DD File Offset: 0x000369DD
		// (set) Token: 0x060025E5 RID: 9701 RVA: 0x000387E5 File Offset: 0x000369E5
		public bool EnableGameplayData { get; set; }

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060025E6 RID: 9702 RVA: 0x000387EE File Offset: 0x000369EE
		// (set) Token: 0x060025E7 RID: 9703 RVA: 0x000387F6 File Offset: 0x000369F6
		public ProductUserId LocalUserId { get; set; }
	}
}
