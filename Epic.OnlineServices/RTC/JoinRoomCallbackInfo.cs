using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x0200017D RID: 381
	public struct JoinRoomCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000AEB RID: 2795 RVA: 0x0001059F File Offset: 0x0000E79F
		// (set) Token: 0x06000AEC RID: 2796 RVA: 0x000105A7 File Offset: 0x0000E7A7
		public Result ResultCode { get; set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000AED RID: 2797 RVA: 0x000105B0 File Offset: 0x0000E7B0
		// (set) Token: 0x06000AEE RID: 2798 RVA: 0x000105B8 File Offset: 0x0000E7B8
		public object ClientData { get; set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000AEF RID: 2799 RVA: 0x000105C1 File Offset: 0x0000E7C1
		// (set) Token: 0x06000AF0 RID: 2800 RVA: 0x000105C9 File Offset: 0x0000E7C9
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000AF1 RID: 2801 RVA: 0x000105D2 File Offset: 0x0000E7D2
		// (set) Token: 0x06000AF2 RID: 2802 RVA: 0x000105DA File Offset: 0x0000E7DA
		public Utf8String RoomName { get; set; }

		// Token: 0x06000AF3 RID: 2803 RVA: 0x000105E4 File Offset: 0x0000E7E4
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00010601 File Offset: 0x0000E801
		internal void Set(ref JoinRoomCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}
	}
}
