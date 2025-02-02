using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200009C RID: 156
	public struct ReadFileCallbackInfo : ICallbackInfo
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005CD RID: 1485 RVA: 0x0000883E File Offset: 0x00006A3E
		// (set) Token: 0x060005CE RID: 1486 RVA: 0x00008846 File Offset: 0x00006A46
		public Result ResultCode { get; set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005CF RID: 1487 RVA: 0x0000884F File Offset: 0x00006A4F
		// (set) Token: 0x060005D0 RID: 1488 RVA: 0x00008857 File Offset: 0x00006A57
		public object ClientData { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005D1 RID: 1489 RVA: 0x00008860 File Offset: 0x00006A60
		// (set) Token: 0x060005D2 RID: 1490 RVA: 0x00008868 File Offset: 0x00006A68
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005D3 RID: 1491 RVA: 0x00008871 File Offset: 0x00006A71
		// (set) Token: 0x060005D4 RID: 1492 RVA: 0x00008879 File Offset: 0x00006A79
		public Utf8String Filename { get; set; }

		// Token: 0x060005D5 RID: 1493 RVA: 0x00008884 File Offset: 0x00006A84
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x000088A1 File Offset: 0x00006AA1
		internal void Set(ref ReadFileCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}
	}
}
