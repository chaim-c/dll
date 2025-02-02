using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000352 RID: 850
	public struct JoinLobbyAcceptedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000654 RID: 1620
		// (get) Token: 0x0600166E RID: 5742 RVA: 0x000214E6 File Offset: 0x0001F6E6
		// (set) Token: 0x0600166F RID: 5743 RVA: 0x000214EE File Offset: 0x0001F6EE
		public object ClientData { get; set; }

		// Token: 0x17000655 RID: 1621
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x000214F7 File Offset: 0x0001F6F7
		// (set) Token: 0x06001671 RID: 5745 RVA: 0x000214FF File Offset: 0x0001F6FF
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000656 RID: 1622
		// (get) Token: 0x06001672 RID: 5746 RVA: 0x00021508 File Offset: 0x0001F708
		// (set) Token: 0x06001673 RID: 5747 RVA: 0x00021510 File Offset: 0x0001F710
		public ulong UiEventId { get; set; }

		// Token: 0x06001674 RID: 5748 RVA: 0x0002151C File Offset: 0x0001F71C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001675 RID: 5749 RVA: 0x00021537 File Offset: 0x0001F737
		internal void Set(ref JoinLobbyAcceptedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.UiEventId = other.UiEventId;
		}
	}
}
