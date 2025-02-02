using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000175 RID: 373
	public struct AddNotifyParticipantStatusChangedOptions
	{
		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000A9D RID: 2717 RVA: 0x0000FDCA File Offset: 0x0000DFCA
		// (set) Token: 0x06000A9E RID: 2718 RVA: 0x0000FDD2 File Offset: 0x0000DFD2
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x0000FDDB File Offset: 0x0000DFDB
		// (set) Token: 0x06000AA0 RID: 2720 RVA: 0x0000FDE3 File Offset: 0x0000DFE3
		public Utf8String RoomName { get; set; }
	}
}
