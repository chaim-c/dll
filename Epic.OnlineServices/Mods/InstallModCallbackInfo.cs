using System;

namespace Epic.OnlineServices.Mods
{
	// Token: 0x020002F2 RID: 754
	public struct InstallModCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000599 RID: 1433
		// (get) Token: 0x0600144E RID: 5198 RVA: 0x0001E182 File Offset: 0x0001C382
		// (set) Token: 0x0600144F RID: 5199 RVA: 0x0001E18A File Offset: 0x0001C38A
		public Result ResultCode { get; set; }

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x06001450 RID: 5200 RVA: 0x0001E193 File Offset: 0x0001C393
		// (set) Token: 0x06001451 RID: 5201 RVA: 0x0001E19B File Offset: 0x0001C39B
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700059B RID: 1435
		// (get) Token: 0x06001452 RID: 5202 RVA: 0x0001E1A4 File Offset: 0x0001C3A4
		// (set) Token: 0x06001453 RID: 5203 RVA: 0x0001E1AC File Offset: 0x0001C3AC
		public object ClientData { get; set; }

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x06001454 RID: 5204 RVA: 0x0001E1B5 File Offset: 0x0001C3B5
		// (set) Token: 0x06001455 RID: 5205 RVA: 0x0001E1BD File Offset: 0x0001C3BD
		public ModIdentifier? Mod { get; set; }

		// Token: 0x06001456 RID: 5206 RVA: 0x0001E1C8 File Offset: 0x0001C3C8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001457 RID: 5207 RVA: 0x0001E1E5 File Offset: 0x0001C3E5
		internal void Set(ref InstallModCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.LocalUserId = other.LocalUserId;
			this.ClientData = other.ClientData;
			this.Mod = other.Mod;
		}
	}
}
