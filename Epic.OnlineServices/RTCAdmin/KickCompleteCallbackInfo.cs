using System;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001FC RID: 508
	public struct KickCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00015542 File Offset: 0x00013742
		// (set) Token: 0x06000E56 RID: 3670 RVA: 0x0001554A File Offset: 0x0001374A
		public Result ResultCode { get; set; }

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x00015553 File Offset: 0x00013753
		// (set) Token: 0x06000E58 RID: 3672 RVA: 0x0001555B File Offset: 0x0001375B
		public object ClientData { get; set; }

		// Token: 0x06000E59 RID: 3673 RVA: 0x00015564 File Offset: 0x00013764
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x00015581 File Offset: 0x00013781
		internal void Set(ref KickCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
