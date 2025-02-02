using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200053A RID: 1338
	public struct LoginCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002251 RID: 8785 RVA: 0x0003350A File Offset: 0x0003170A
		// (set) Token: 0x06002252 RID: 8786 RVA: 0x00033512 File Offset: 0x00031712
		public Result ResultCode { get; set; }

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002253 RID: 8787 RVA: 0x0003351B File Offset: 0x0003171B
		// (set) Token: 0x06002254 RID: 8788 RVA: 0x00033523 File Offset: 0x00031723
		public object ClientData { get; set; }

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002255 RID: 8789 RVA: 0x0003352C File Offset: 0x0003172C
		// (set) Token: 0x06002256 RID: 8790 RVA: 0x00033534 File Offset: 0x00031734
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002257 RID: 8791 RVA: 0x0003353D File Offset: 0x0003173D
		// (set) Token: 0x06002258 RID: 8792 RVA: 0x00033545 File Offset: 0x00031745
		public ContinuanceToken ContinuanceToken { get; set; }

		// Token: 0x06002259 RID: 8793 RVA: 0x00033550 File Offset: 0x00031750
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600225A RID: 8794 RVA: 0x0003356D File Offset: 0x0003176D
		internal void Set(ref LoginCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.ContinuanceToken = other.ContinuanceToken;
		}
	}
}
