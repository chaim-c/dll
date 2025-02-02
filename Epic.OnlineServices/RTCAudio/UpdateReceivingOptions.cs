using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001EA RID: 490
	public struct UpdateReceivingOptions
	{
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000146F0 File Offset: 0x000128F0
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x000146F8 File Offset: 0x000128F8
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x00014701 File Offset: 0x00012901
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x00014709 File Offset: 0x00012909
		public Utf8String RoomName { get; set; }

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00014712 File Offset: 0x00012912
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x0001471A File Offset: 0x0001291A
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00014723 File Offset: 0x00012923
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x0001472B File Offset: 0x0001292B
		public bool AudioEnabled { get; set; }
	}
}
