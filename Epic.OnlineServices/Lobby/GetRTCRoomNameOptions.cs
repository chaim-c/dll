using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200034A RID: 842
	public struct GetRTCRoomNameOptions
	{
		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001636 RID: 5686 RVA: 0x00020F9E File Offset: 0x0001F19E
		// (set) Token: 0x06001637 RID: 5687 RVA: 0x00020FA6 File Offset: 0x0001F1A6
		public Utf8String LobbyId { get; set; }

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001638 RID: 5688 RVA: 0x00020FAF File Offset: 0x0001F1AF
		// (set) Token: 0x06001639 RID: 5689 RVA: 0x00020FB7 File Offset: 0x0001F1B7
		public ProductUserId LocalUserId { get; set; }
	}
}
