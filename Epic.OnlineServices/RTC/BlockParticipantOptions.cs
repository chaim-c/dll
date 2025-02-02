using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000179 RID: 377
	public struct BlockParticipantOptions
	{
		// Token: 0x17000279 RID: 633
		// (get) Token: 0x06000AC5 RID: 2757 RVA: 0x000101F4 File Offset: 0x0000E3F4
		// (set) Token: 0x06000AC6 RID: 2758 RVA: 0x000101FC File Offset: 0x0000E3FC
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700027A RID: 634
		// (get) Token: 0x06000AC7 RID: 2759 RVA: 0x00010205 File Offset: 0x0000E405
		// (set) Token: 0x06000AC8 RID: 2760 RVA: 0x0001020D File Offset: 0x0000E40D
		public Utf8String RoomName { get; set; }

		// Token: 0x1700027B RID: 635
		// (get) Token: 0x06000AC9 RID: 2761 RVA: 0x00010216 File Offset: 0x0000E416
		// (set) Token: 0x06000ACA RID: 2762 RVA: 0x0001021E File Offset: 0x0000E41E
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000ACB RID: 2763 RVA: 0x00010227 File Offset: 0x0000E427
		// (set) Token: 0x06000ACC RID: 2764 RVA: 0x0001022F File Offset: 0x0000E42F
		public bool Blocked { get; set; }
	}
}
