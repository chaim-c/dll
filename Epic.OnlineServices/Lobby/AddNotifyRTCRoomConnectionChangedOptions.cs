using System;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200032C RID: 812
	public struct AddNotifyRTCRoomConnectionChangedOptions
	{
		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06001574 RID: 5492 RVA: 0x0001FCB8 File Offset: 0x0001DEB8
		// (set) Token: 0x06001575 RID: 5493 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		public Utf8String LobbyId_DEPRECATED { get; set; }

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06001576 RID: 5494 RVA: 0x0001FCC9 File Offset: 0x0001DEC9
		// (set) Token: 0x06001577 RID: 5495 RVA: 0x0001FCD1 File Offset: 0x0001DED1
		public ProductUserId LocalUserId_DEPRECATED { get; set; }
	}
}
