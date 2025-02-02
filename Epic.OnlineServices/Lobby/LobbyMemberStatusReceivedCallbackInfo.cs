using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000384 RID: 900
	public struct LobbyMemberStatusReceivedCallbackInfo : ICallbackInfo
	{
		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x000246E0 File Offset: 0x000228E0
		// (set) Token: 0x06001801 RID: 6145 RVA: 0x000246E8 File Offset: 0x000228E8
		public object ClientData { get; set; }

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x000246F1 File Offset: 0x000228F1
		// (set) Token: 0x06001803 RID: 6147 RVA: 0x000246F9 File Offset: 0x000228F9
		public Utf8String LobbyId { get; set; }

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001804 RID: 6148 RVA: 0x00024702 File Offset: 0x00022902
		// (set) Token: 0x06001805 RID: 6149 RVA: 0x0002470A File Offset: 0x0002290A
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001806 RID: 6150 RVA: 0x00024713 File Offset: 0x00022913
		// (set) Token: 0x06001807 RID: 6151 RVA: 0x0002471B File Offset: 0x0002291B
		public LobbyMemberStatus CurrentStatus { get; set; }

		// Token: 0x06001808 RID: 6152 RVA: 0x00024724 File Offset: 0x00022924
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0002473F File Offset: 0x0002293F
		internal void Set(ref LobbyMemberStatusReceivedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
			this.TargetUserId = other.TargetUserId;
			this.CurrentStatus = other.CurrentStatus;
		}
	}
}
