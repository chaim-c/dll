using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000182 RID: 386
	public struct LeaveRoomCallbackInfo : ICallbackInfo
	{
		// Token: 0x170002A3 RID: 675
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x00010A7B File Offset: 0x0000EC7B
		// (set) Token: 0x06000B1E RID: 2846 RVA: 0x00010A83 File Offset: 0x0000EC83
		public Result ResultCode { get; set; }

		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000B1F RID: 2847 RVA: 0x00010A8C File Offset: 0x0000EC8C
		// (set) Token: 0x06000B20 RID: 2848 RVA: 0x00010A94 File Offset: 0x0000EC94
		public object ClientData { get; set; }

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00010A9D File Offset: 0x0000EC9D
		// (set) Token: 0x06000B22 RID: 2850 RVA: 0x00010AA5 File Offset: 0x0000ECA5
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x00010AAE File Offset: 0x0000ECAE
		// (set) Token: 0x06000B24 RID: 2852 RVA: 0x00010AB6 File Offset: 0x0000ECB6
		public Utf8String RoomName { get; set; }

		// Token: 0x06000B25 RID: 2853 RVA: 0x00010AC0 File Offset: 0x0000ECC0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x00010ADD File Offset: 0x0000ECDD
		internal void Set(ref LeaveRoomCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
		}
	}
}
