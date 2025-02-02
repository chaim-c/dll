using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200055C RID: 1372
	public struct QueryProductUserIdMappingsCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x00033D3B File Offset: 0x00031F3B
		// (set) Token: 0x06002308 RID: 8968 RVA: 0x00033D43 File Offset: 0x00031F43
		public Result ResultCode { get; set; }

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x00033D4C File Offset: 0x00031F4C
		// (set) Token: 0x0600230A RID: 8970 RVA: 0x00033D54 File Offset: 0x00031F54
		public object ClientData { get; set; }

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x0600230B RID: 8971 RVA: 0x00033D5D File Offset: 0x00031F5D
		// (set) Token: 0x0600230C RID: 8972 RVA: 0x00033D65 File Offset: 0x00031F65
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x0600230D RID: 8973 RVA: 0x00033D70 File Offset: 0x00031F70
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600230E RID: 8974 RVA: 0x00033D8D File Offset: 0x00031F8D
		internal void Set(ref QueryProductUserIdMappingsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
