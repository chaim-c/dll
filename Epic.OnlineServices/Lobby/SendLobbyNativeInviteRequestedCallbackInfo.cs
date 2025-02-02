using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003EF RID: 1007
	public struct SendLobbyNativeInviteRequestedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06001A1B RID: 6683 RVA: 0x00026A13 File Offset: 0x00024C13
		// (set) Token: 0x06001A1C RID: 6684 RVA: 0x00026A1B File Offset: 0x00024C1B
		public object ClientData { get; set; }

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06001A1D RID: 6685 RVA: 0x00026A24 File Offset: 0x00024C24
		// (set) Token: 0x06001A1E RID: 6686 RVA: 0x00026A2C File Offset: 0x00024C2C
		public ulong UiEventId { get; set; }

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x00026A35 File Offset: 0x00024C35
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x00026A3D File Offset: 0x00024C3D
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x00026A46 File Offset: 0x00024C46
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x00026A4E File Offset: 0x00024C4E
		public Utf8String TargetNativeAccountType { get; set; }

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x00026A57 File Offset: 0x00024C57
		// (set) Token: 0x06001A24 RID: 6692 RVA: 0x00026A5F File Offset: 0x00024C5F
		public Utf8String TargetUserNativeAccountId { get; set; }

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x00026A68 File Offset: 0x00024C68
		// (set) Token: 0x06001A26 RID: 6694 RVA: 0x00026A70 File Offset: 0x00024C70
		public Utf8String LobbyId { get; set; }

		// Token: 0x06001A27 RID: 6695 RVA: 0x00026A7C File Offset: 0x00024C7C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00026A98 File Offset: 0x00024C98
		internal void Set(ref SendLobbyNativeInviteRequestedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.UiEventId = other.UiEventId;
			this.LocalUserId = other.LocalUserId;
			this.TargetNativeAccountType = other.TargetNativeAccountType;
			this.TargetUserNativeAccountId = other.TargetUserNativeAccountId;
			this.LobbyId = other.LobbyId;
		}
	}
}
