using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200026B RID: 619
	public struct DuplicateFileCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x060010E3 RID: 4323 RVA: 0x000190B2 File Offset: 0x000172B2
		// (set) Token: 0x060010E4 RID: 4324 RVA: 0x000190BA File Offset: 0x000172BA
		public Result ResultCode { get; set; }

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x060010E5 RID: 4325 RVA: 0x000190C3 File Offset: 0x000172C3
		// (set) Token: 0x060010E6 RID: 4326 RVA: 0x000190CB File Offset: 0x000172CB
		public object ClientData { get; set; }

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x060010E7 RID: 4327 RVA: 0x000190D4 File Offset: 0x000172D4
		// (set) Token: 0x060010E8 RID: 4328 RVA: 0x000190DC File Offset: 0x000172DC
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x060010E9 RID: 4329 RVA: 0x000190E8 File Offset: 0x000172E8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x00019105 File Offset: 0x00017305
		internal void Set(ref DuplicateFileCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
