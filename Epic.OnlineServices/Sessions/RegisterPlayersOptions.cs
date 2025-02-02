using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000113 RID: 275
	public struct RegisterPlayersOptions
	{
		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x06000869 RID: 2153 RVA: 0x0000C167 File Offset: 0x0000A367
		// (set) Token: 0x0600086A RID: 2154 RVA: 0x0000C16F File Offset: 0x0000A36F
		public Utf8String SessionName { get; set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0000C178 File Offset: 0x0000A378
		// (set) Token: 0x0600086C RID: 2156 RVA: 0x0000C180 File Offset: 0x0000A380
		public ProductUserId[] PlayersToRegister { get; set; }
	}
}
