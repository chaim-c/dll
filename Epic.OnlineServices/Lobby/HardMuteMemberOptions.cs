using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034E RID: 846
	public struct HardMuteMemberOptions
	{
		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001656 RID: 5718 RVA: 0x000212B7 File Offset: 0x0001F4B7
		// (set) Token: 0x06001657 RID: 5719 RVA: 0x000212BF File Offset: 0x0001F4BF
		public Utf8String LobbyId { get; set; }

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001658 RID: 5720 RVA: 0x000212C8 File Offset: 0x0001F4C8
		// (set) Token: 0x06001659 RID: 5721 RVA: 0x000212D0 File Offset: 0x0001F4D0
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x0600165A RID: 5722 RVA: 0x000212D9 File Offset: 0x0001F4D9
		// (set) Token: 0x0600165B RID: 5723 RVA: 0x000212E1 File Offset: 0x0001F4E1
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x0600165C RID: 5724 RVA: 0x000212EA File Offset: 0x0001F4EA
		// (set) Token: 0x0600165D RID: 5725 RVA: 0x000212F2 File Offset: 0x0001F4F2
		public bool HardMute { get; set; }
	}
}
