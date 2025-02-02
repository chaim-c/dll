using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200036E RID: 878
	public struct LobbyDetailsCopyMemberAttributeByKeyOptions
	{
		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001725 RID: 5925 RVA: 0x000227B2 File Offset: 0x000209B2
		// (set) Token: 0x06001726 RID: 5926 RVA: 0x000227BA File Offset: 0x000209BA
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700069A RID: 1690
		// (get) Token: 0x06001727 RID: 5927 RVA: 0x000227C3 File Offset: 0x000209C3
		// (set) Token: 0x06001728 RID: 5928 RVA: 0x000227CB File Offset: 0x000209CB
		public Utf8String AttrKey { get; set; }
	}
}
