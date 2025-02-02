using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C3 RID: 195
	public struct ActiveSessionInfo
	{
		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060006DB RID: 1755 RVA: 0x0000A54C File Offset: 0x0000874C
		// (set) Token: 0x060006DC RID: 1756 RVA: 0x0000A554 File Offset: 0x00008754
		public Utf8String SessionName { get; set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0000A55D File Offset: 0x0000875D
		// (set) Token: 0x060006DE RID: 1758 RVA: 0x0000A565 File Offset: 0x00008765
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060006DF RID: 1759 RVA: 0x0000A56E File Offset: 0x0000876E
		// (set) Token: 0x060006E0 RID: 1760 RVA: 0x0000A576 File Offset: 0x00008776
		public OnlineSessionState State { get; set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060006E1 RID: 1761 RVA: 0x0000A57F File Offset: 0x0000877F
		// (set) Token: 0x060006E2 RID: 1762 RVA: 0x0000A587 File Offset: 0x00008787
		public SessionDetailsInfo? SessionDetails { get; set; }

		// Token: 0x060006E3 RID: 1763 RVA: 0x0000A590 File Offset: 0x00008790
		internal void Set(ref ActiveSessionInfoInternal other)
		{
			this.SessionName = other.SessionName;
			this.LocalUserId = other.LocalUserId;
			this.State = other.State;
			this.SessionDetails = other.SessionDetails;
		}
	}
}
