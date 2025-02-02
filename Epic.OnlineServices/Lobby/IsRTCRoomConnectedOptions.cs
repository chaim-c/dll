using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000350 RID: 848
	public struct IsRTCRoomConnectedOptions
	{
		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00021418 File Offset: 0x0001F618
		// (set) Token: 0x06001666 RID: 5734 RVA: 0x00021420 File Offset: 0x0001F620
		public Utf8String LobbyId { get; set; }

		// Token: 0x17000651 RID: 1617
		// (get) Token: 0x06001667 RID: 5735 RVA: 0x00021429 File Offset: 0x0001F629
		// (set) Token: 0x06001668 RID: 5736 RVA: 0x00021431 File Offset: 0x0001F631
		public ProductUserId LocalUserId { get; set; }
	}
}
