using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003E9 RID: 1001
	public struct RTCRoomConnectionChangedCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x0002644E File Offset: 0x0002464E
		// (set) Token: 0x060019E2 RID: 6626 RVA: 0x00026456 File Offset: 0x00024656
		public object ClientData { get; set; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x060019E3 RID: 6627 RVA: 0x0002645F File Offset: 0x0002465F
		// (set) Token: 0x060019E4 RID: 6628 RVA: 0x00026467 File Offset: 0x00024667
		public Utf8String LobbyId { get; set; }

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x060019E5 RID: 6629 RVA: 0x00026470 File Offset: 0x00024670
		// (set) Token: 0x060019E6 RID: 6630 RVA: 0x00026478 File Offset: 0x00024678
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00026481 File Offset: 0x00024681
		// (set) Token: 0x060019E8 RID: 6632 RVA: 0x00026489 File Offset: 0x00024689
		public bool IsConnected { get; set; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x00026492 File Offset: 0x00024692
		// (set) Token: 0x060019EA RID: 6634 RVA: 0x0002649A File Offset: 0x0002469A
		public Result DisconnectReason { get; set; }

		// Token: 0x060019EB RID: 6635 RVA: 0x000264A4 File Offset: 0x000246A4
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x000264C0 File Offset: 0x000246C0
		internal void Set(ref RTCRoomConnectionChangedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
			this.LocalUserId = other.LocalUserId;
			this.IsConnected = other.IsConnected;
			this.DisconnectReason = other.DisconnectReason;
		}
	}
}
