using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200038B RID: 907
	public struct LobbyModificationAddMemberAttributeOptions
	{
		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00024DDA File Offset: 0x00022FDA
		// (set) Token: 0x0600183F RID: 6207 RVA: 0x00024DE2 File Offset: 0x00022FE2
		public AttributeData? Attribute { get; set; }

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x06001840 RID: 6208 RVA: 0x00024DEB File Offset: 0x00022FEB
		// (set) Token: 0x06001841 RID: 6209 RVA: 0x00024DF3 File Offset: 0x00022FF3
		public LobbyAttributeVisibility Visibility { get; set; }
	}
}
