using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200015E RID: 350
	public struct UnregisterPlayersOptions
	{
		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000A16 RID: 2582 RVA: 0x0000F146 File Offset: 0x0000D346
		// (set) Token: 0x06000A17 RID: 2583 RVA: 0x0000F14E File Offset: 0x0000D34E
		public Utf8String SessionName { get; set; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000A18 RID: 2584 RVA: 0x0000F157 File Offset: 0x0000D357
		// (set) Token: 0x06000A19 RID: 2585 RVA: 0x0000F15F File Offset: 0x0000D35F
		public ProductUserId[] PlayersToUnregister { get; set; }
	}
}
