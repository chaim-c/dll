using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000386 RID: 902
	public struct LobbyMemberUpdateReceivedCallbackInfo : ICallbackInfo
	{
		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0002492F File Offset: 0x00022B2F
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x00024937 File Offset: 0x00022B37
		public object ClientData { get; set; }

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x00024940 File Offset: 0x00022B40
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x00024948 File Offset: 0x00022B48
		public Utf8String LobbyId { get; set; }

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x00024951 File Offset: 0x00022B51
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x00024959 File Offset: 0x00022B59
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x0600181D RID: 6173 RVA: 0x00024964 File Offset: 0x00022B64
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x0002497F File Offset: 0x00022B7F
		internal void Set(ref LobbyMemberUpdateReceivedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
