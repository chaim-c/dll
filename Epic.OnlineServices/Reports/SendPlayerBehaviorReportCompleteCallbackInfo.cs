using System;

namespace Epic.OnlineServices.Reports
{
	// Token: 0x02000215 RID: 533
	public struct SendPlayerBehaviorReportCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06000EF1 RID: 3825 RVA: 0x000162AF File Offset: 0x000144AF
		// (set) Token: 0x06000EF2 RID: 3826 RVA: 0x000162B7 File Offset: 0x000144B7
		public Result ResultCode { get; set; }

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06000EF3 RID: 3827 RVA: 0x000162C0 File Offset: 0x000144C0
		// (set) Token: 0x06000EF4 RID: 3828 RVA: 0x000162C8 File Offset: 0x000144C8
		public object ClientData { get; set; }

		// Token: 0x06000EF5 RID: 3829 RVA: 0x000162D4 File Offset: 0x000144D4
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000162F1 File Offset: 0x000144F1
		internal void Set(ref SendPlayerBehaviorReportCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
