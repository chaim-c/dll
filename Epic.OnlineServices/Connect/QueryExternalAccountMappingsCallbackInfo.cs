using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000558 RID: 1368
	public struct QueryExternalAccountMappingsCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A23 RID: 2595
		// (get) Token: 0x060022E8 RID: 8936 RVA: 0x00033A5B File Offset: 0x00031C5B
		// (set) Token: 0x060022E9 RID: 8937 RVA: 0x00033A63 File Offset: 0x00031C63
		public Result ResultCode { get; set; }

		// Token: 0x17000A24 RID: 2596
		// (get) Token: 0x060022EA RID: 8938 RVA: 0x00033A6C File Offset: 0x00031C6C
		// (set) Token: 0x060022EB RID: 8939 RVA: 0x00033A74 File Offset: 0x00031C74
		public object ClientData { get; set; }

		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060022EC RID: 8940 RVA: 0x00033A7D File Offset: 0x00031C7D
		// (set) Token: 0x060022ED RID: 8941 RVA: 0x00033A85 File Offset: 0x00031C85
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x060022EE RID: 8942 RVA: 0x00033A90 File Offset: 0x00031C90
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x00033AAD File Offset: 0x00031CAD
		internal void Set(ref QueryExternalAccountMappingsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
