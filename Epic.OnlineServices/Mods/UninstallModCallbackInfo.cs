using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x02000304 RID: 772
	public struct UninstallModCallbackInfo : ICallbackInfo
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060014C2 RID: 5314 RVA: 0x0001EB7B File Offset: 0x0001CD7B
		// (set) Token: 0x060014C3 RID: 5315 RVA: 0x0001EB83 File Offset: 0x0001CD83
		public Result ResultCode { get; set; }

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060014C4 RID: 5316 RVA: 0x0001EB8C File Offset: 0x0001CD8C
		// (set) Token: 0x060014C5 RID: 5317 RVA: 0x0001EB94 File Offset: 0x0001CD94
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0001EB9D File Offset: 0x0001CD9D
		// (set) Token: 0x060014C7 RID: 5319 RVA: 0x0001EBA5 File Offset: 0x0001CDA5
		public object ClientData { get; set; }

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0001EBAE File Offset: 0x0001CDAE
		// (set) Token: 0x060014C9 RID: 5321 RVA: 0x0001EBB6 File Offset: 0x0001CDB6
		public ModIdentifier? Mod { get; set; }

		// Token: 0x060014CA RID: 5322 RVA: 0x0001EBC0 File Offset: 0x0001CDC0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0001EBDD File Offset: 0x0001CDDD
		internal void Set(ref UninstallModCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.LocalUserId = other.LocalUserId;
			this.ClientData = other.ClientData;
			this.Mod = other.Mod;
		}
	}
}
