using System;

namespace Epic.OnlineServices.Sanctions
{
	// Token: 0x0200016E RID: 366
	public struct QueryActivePlayerSanctionsCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000A6E RID: 2670 RVA: 0x0000F8AA File Offset: 0x0000DAAA
		// (set) Token: 0x06000A6F RID: 2671 RVA: 0x0000F8B2 File Offset: 0x0000DAB2
		public Result ResultCode { get; set; }

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000A70 RID: 2672 RVA: 0x0000F8BB File Offset: 0x0000DABB
		// (set) Token: 0x06000A71 RID: 2673 RVA: 0x0000F8C3 File Offset: 0x0000DAC3
		public object ClientData { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000A72 RID: 2674 RVA: 0x0000F8CC File Offset: 0x0000DACC
		// (set) Token: 0x06000A73 RID: 2675 RVA: 0x0000F8D4 File Offset: 0x0000DAD4
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000A74 RID: 2676 RVA: 0x0000F8DD File Offset: 0x0000DADD
		// (set) Token: 0x06000A75 RID: 2677 RVA: 0x0000F8E5 File Offset: 0x0000DAE5
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x06000A76 RID: 2678 RVA: 0x0000F8F0 File Offset: 0x0000DAF0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000A77 RID: 2679 RVA: 0x0000F90D File Offset: 0x0000DB0D
		internal void Set(ref QueryActivePlayerSanctionsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
