using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200061F RID: 1567
	public struct BeginSessionOptions
	{
		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06002811 RID: 10257 RVA: 0x0003BEA7 File Offset: 0x0003A0A7
		// (set) Token: 0x06002812 RID: 10258 RVA: 0x0003BEAF File Offset: 0x0003A0AF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06002813 RID: 10259 RVA: 0x0003BEB8 File Offset: 0x0003A0B8
		// (set) Token: 0x06002814 RID: 10260 RVA: 0x0003BEC0 File Offset: 0x0003A0C0
		public AntiCheatClientMode Mode { get; set; }
	}
}
