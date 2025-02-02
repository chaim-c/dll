using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000173 RID: 371
	public struct AddNotifyDisconnectedOptions
	{
		// Token: 0x17000264 RID: 612
		// (get) Token: 0x06000A94 RID: 2708 RVA: 0x0000FCFB File Offset: 0x0000DEFB
		// (set) Token: 0x06000A95 RID: 2709 RVA: 0x0000FD03 File Offset: 0x0000DF03
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0000FD0C File Offset: 0x0000DF0C
		// (set) Token: 0x06000A97 RID: 2711 RVA: 0x0000FD14 File Offset: 0x0000DF14
		public Utf8String RoomName { get; set; }
	}
}
