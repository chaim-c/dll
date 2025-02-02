using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x0200017B RID: 379
	public struct DisconnectedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000AD4 RID: 2772 RVA: 0x00010354 File Offset: 0x0000E554
		// (set) Token: 0x06000AD5 RID: 2773 RVA: 0x0001035C File Offset: 0x0000E55C
		public Result ResultCode { get; set; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000AD6 RID: 2774 RVA: 0x00010365 File Offset: 0x0000E565
		// (set) Token: 0x06000AD7 RID: 2775 RVA: 0x0001036D File Offset: 0x0000E56D
		public object ClientData { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000AD8 RID: 2776 RVA: 0x00010376 File Offset: 0x0000E576
		// (set) Token: 0x06000AD9 RID: 2777 RVA: 0x0001037E File Offset: 0x0000E57E
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000ADA RID: 2778 RVA: 0x00010387 File Offset: 0x0000E587
		// (set) Token: 0x06000ADB RID: 2779 RVA: 0x0001038F File Offset: 0x0000E58F
		public Utf8String RoomName { get; set; }

		// Token: 0x06000ADC RID: 2780 RVA: 0x00010398 File Offset: 0x0000E598
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000103B5 File Offset: 0x0000E5B5
		internal void Set(ref DisconnectedCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}
	}
}
