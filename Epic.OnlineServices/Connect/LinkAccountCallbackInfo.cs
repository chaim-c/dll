using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000536 RID: 1334
	public struct LinkAccountCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002235 RID: 8757 RVA: 0x00033270 File Offset: 0x00031470
		// (set) Token: 0x06002236 RID: 8758 RVA: 0x00033278 File Offset: 0x00031478
		public Result ResultCode { get; set; }

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002237 RID: 8759 RVA: 0x00033281 File Offset: 0x00031481
		// (set) Token: 0x06002238 RID: 8760 RVA: 0x00033289 File Offset: 0x00031489
		public object ClientData { get; set; }

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002239 RID: 8761 RVA: 0x00033292 File Offset: 0x00031492
		// (set) Token: 0x0600223A RID: 8762 RVA: 0x0003329A File Offset: 0x0003149A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x0600223B RID: 8763 RVA: 0x000332A4 File Offset: 0x000314A4
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600223C RID: 8764 RVA: 0x000332C1 File Offset: 0x000314C1
		internal void Set(ref LinkAccountCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
