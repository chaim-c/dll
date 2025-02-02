using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000362 RID: 866
	public struct LeaveLobbyOptions
	{
		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x000221D2 File Offset: 0x000203D2
		// (set) Token: 0x060016F8 RID: 5880 RVA: 0x000221DA File Offset: 0x000203DA
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x000221E3 File Offset: 0x000203E3
		// (set) Token: 0x060016FA RID: 5882 RVA: 0x000221EB File Offset: 0x000203EB
		public Utf8String LobbyId { get; set; }
	}
}
