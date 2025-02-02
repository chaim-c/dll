using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003AF RID: 943
	public struct LobbyUpdateReceivedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x060018C1 RID: 6337 RVA: 0x00025911 File Offset: 0x00023B11
		// (set) Token: 0x060018C2 RID: 6338 RVA: 0x00025919 File Offset: 0x00023B19
		public object ClientData { get; set; }

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00025922 File Offset: 0x00023B22
		// (set) Token: 0x060018C4 RID: 6340 RVA: 0x0002592A File Offset: 0x00023B2A
		public Utf8String LobbyId { get; set; }

		// Token: 0x060018C5 RID: 6341 RVA: 0x00025934 File Offset: 0x00023B34
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0002594F File Offset: 0x00023B4F
		internal void Set(ref LobbyUpdateReceivedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LobbyId = other.LobbyId;
		}
	}
}
