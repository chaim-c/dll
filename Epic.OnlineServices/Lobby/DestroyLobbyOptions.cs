using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000344 RID: 836
	public struct DestroyLobbyOptions
	{
		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600161E RID: 5662 RVA: 0x00020D96 File Offset: 0x0001EF96
		// (set) Token: 0x0600161F RID: 5663 RVA: 0x00020D9E File Offset: 0x0001EF9E
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001620 RID: 5664 RVA: 0x00020DA7 File Offset: 0x0001EFA7
		// (set) Token: 0x06001621 RID: 5665 RVA: 0x00020DAF File Offset: 0x0001EFAF
		public Utf8String LobbyId { get; set; }
	}
}
