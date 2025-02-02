using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200036C RID: 876
	public struct LobbyDetailsCopyMemberAttributeByIndexOptions
	{
		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600171C RID: 5916 RVA: 0x000226F8 File Offset: 0x000208F8
		// (set) Token: 0x0600171D RID: 5917 RVA: 0x00022700 File Offset: 0x00020900
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600171E RID: 5918 RVA: 0x00022709 File Offset: 0x00020909
		// (set) Token: 0x0600171F RID: 5919 RVA: 0x00022711 File Offset: 0x00020911
		public uint AttrIndex { get; set; }
	}
}
