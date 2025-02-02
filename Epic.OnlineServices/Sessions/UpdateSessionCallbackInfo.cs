using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000160 RID: 352
	public struct UpdateSessionCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700023C RID: 572
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x0000F21A File Offset: 0x0000D41A
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0000F222 File Offset: 0x0000D422
		public Result ResultCode { get; set; }

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x06000A21 RID: 2593 RVA: 0x0000F22B File Offset: 0x0000D42B
		// (set) Token: 0x06000A22 RID: 2594 RVA: 0x0000F233 File Offset: 0x0000D433
		public object ClientData { get; set; }

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000A23 RID: 2595 RVA: 0x0000F23C File Offset: 0x0000D43C
		// (set) Token: 0x06000A24 RID: 2596 RVA: 0x0000F244 File Offset: 0x0000D444
		public Utf8String SessionName { get; set; }

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000A25 RID: 2597 RVA: 0x0000F24D File Offset: 0x0000D44D
		// (set) Token: 0x06000A26 RID: 2598 RVA: 0x0000F255 File Offset: 0x0000D455
		public Utf8String SessionId { get; set; }

		// Token: 0x06000A27 RID: 2599 RVA: 0x0000F260 File Offset: 0x0000D460
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0000F27D File Offset: 0x0000D47D
		internal void Set(ref UpdateSessionCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.SessionName = other.SessionName;
			this.SessionId = other.SessionId;
		}
	}
}
