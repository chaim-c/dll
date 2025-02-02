using System;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x020005AD RID: 1453
	public struct VerifyUserAuthCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x06002576 RID: 9590 RVA: 0x000379D5 File Offset: 0x00035BD5
		// (set) Token: 0x06002577 RID: 9591 RVA: 0x000379DD File Offset: 0x00035BDD
		public Result ResultCode { get; set; }

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x06002578 RID: 9592 RVA: 0x000379E6 File Offset: 0x00035BE6
		// (set) Token: 0x06002579 RID: 9593 RVA: 0x000379EE File Offset: 0x00035BEE
		public object ClientData { get; set; }

		// Token: 0x0600257A RID: 9594 RVA: 0x000379F8 File Offset: 0x00035BF8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00037A15 File Offset: 0x00035C15
		internal void Set(ref VerifyUserAuthCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}
	}
}
