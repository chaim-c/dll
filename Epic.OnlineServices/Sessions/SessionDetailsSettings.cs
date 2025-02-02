using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012B RID: 299
	public struct SessionDetailsSettings
	{
		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060008E0 RID: 2272 RVA: 0x0000CCE2 File Offset: 0x0000AEE2
		// (set) Token: 0x060008E1 RID: 2273 RVA: 0x0000CCEA File Offset: 0x0000AEEA
		public Utf8String BucketId { get; set; }

		// Token: 0x170001E2 RID: 482
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0000CCF3 File Offset: 0x0000AEF3
		// (set) Token: 0x060008E3 RID: 2275 RVA: 0x0000CCFB File Offset: 0x0000AEFB
		public uint NumPublicConnections { get; set; }

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060008E4 RID: 2276 RVA: 0x0000CD04 File Offset: 0x0000AF04
		// (set) Token: 0x060008E5 RID: 2277 RVA: 0x0000CD0C File Offset: 0x0000AF0C
		public bool AllowJoinInProgress { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060008E6 RID: 2278 RVA: 0x0000CD15 File Offset: 0x0000AF15
		// (set) Token: 0x060008E7 RID: 2279 RVA: 0x0000CD1D File Offset: 0x0000AF1D
		public OnlineSessionPermissionLevel PermissionLevel { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060008E8 RID: 2280 RVA: 0x0000CD26 File Offset: 0x0000AF26
		// (set) Token: 0x060008E9 RID: 2281 RVA: 0x0000CD2E File Offset: 0x0000AF2E
		public bool InvitesAllowed { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060008EA RID: 2282 RVA: 0x0000CD37 File Offset: 0x0000AF37
		// (set) Token: 0x060008EB RID: 2283 RVA: 0x0000CD3F File Offset: 0x0000AF3F
		public bool SanctionsEnabled { get; set; }

		// Token: 0x060008EC RID: 2284 RVA: 0x0000CD48 File Offset: 0x0000AF48
		internal void Set(ref SessionDetailsSettingsInternal other)
		{
			this.BucketId = other.BucketId;
			this.NumPublicConnections = other.NumPublicConnections;
			this.AllowJoinInProgress = other.AllowJoinInProgress;
			this.PermissionLevel = other.PermissionLevel;
			this.InvitesAllowed = other.InvitesAllowed;
			this.SanctionsEnabled = other.SanctionsEnabled;
		}
	}
}
