using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000293 RID: 659
	public struct ReadFileCallbackInfo : ICallbackInfo
	{
		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x0001A55E File Offset: 0x0001875E
		// (set) Token: 0x060011E2 RID: 4578 RVA: 0x0001A566 File Offset: 0x00018766
		public Result ResultCode { get; set; }

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x0001A56F File Offset: 0x0001876F
		// (set) Token: 0x060011E4 RID: 4580 RVA: 0x0001A577 File Offset: 0x00018777
		public object ClientData { get; set; }

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x0001A580 File Offset: 0x00018780
		// (set) Token: 0x060011E6 RID: 4582 RVA: 0x0001A588 File Offset: 0x00018788
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060011E7 RID: 4583 RVA: 0x0001A591 File Offset: 0x00018791
		// (set) Token: 0x060011E8 RID: 4584 RVA: 0x0001A599 File Offset: 0x00018799
		public Utf8String Filename { get; set; }

		// Token: 0x060011E9 RID: 4585 RVA: 0x0001A5A4 File Offset: 0x000187A4
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0001A5C1 File Offset: 0x000187C1
		internal void Set(ref ReadFileCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}
	}
}
