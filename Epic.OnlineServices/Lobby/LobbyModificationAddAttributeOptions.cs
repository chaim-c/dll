using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000389 RID: 905
	public struct LobbyModificationAddAttributeOptions
	{
		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x06001835 RID: 6197 RVA: 0x00024D1E File Offset: 0x00022F1E
		// (set) Token: 0x06001836 RID: 6198 RVA: 0x00024D26 File Offset: 0x00022F26
		public AttributeData? Attribute { get; set; }

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00024D2F File Offset: 0x00022F2F
		// (set) Token: 0x06001838 RID: 6200 RVA: 0x00024D37 File Offset: 0x00022F37
		public LobbyAttributeVisibility Visibility { get; set; }
	}
}
