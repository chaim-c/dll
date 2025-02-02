using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200028D RID: 653
	public struct QueryFileListCallbackInfo : ICallbackInfo
	{
		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0001A1DE File Offset: 0x000183DE
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x0001A1E6 File Offset: 0x000183E6
		public Result ResultCode { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0001A1EF File Offset: 0x000183EF
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x0001A1F7 File Offset: 0x000183F7
		public object ClientData { get; set; }

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x0001A200 File Offset: 0x00018400
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x0001A208 File Offset: 0x00018408
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x0001A211 File Offset: 0x00018411
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x0001A219 File Offset: 0x00018419
		public uint FileCount { get; set; }

		// Token: 0x060011C3 RID: 4547 RVA: 0x0001A224 File Offset: 0x00018424
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x0001A241 File Offset: 0x00018441
		internal void Set(ref QueryFileListCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.FileCount = other.FileCount;
		}
	}
}
