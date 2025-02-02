using System;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000560 RID: 1376
	public struct TransferDeviceIdAccountCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06002326 RID: 8998 RVA: 0x0003401B File Offset: 0x0003221B
		// (set) Token: 0x06002327 RID: 8999 RVA: 0x00034023 File Offset: 0x00032223
		public Result ResultCode { get; set; }

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06002328 RID: 9000 RVA: 0x0003402C File Offset: 0x0003222C
		// (set) Token: 0x06002329 RID: 9001 RVA: 0x00034034 File Offset: 0x00032234
		public object ClientData { get; set; }

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x0600232A RID: 9002 RVA: 0x0003403D File Offset: 0x0003223D
		// (set) Token: 0x0600232B RID: 9003 RVA: 0x00034045 File Offset: 0x00032245
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x0600232C RID: 9004 RVA: 0x00034050 File Offset: 0x00032250
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x0003406D File Offset: 0x0003226D
		internal void Set(ref TransferDeviceIdAccountCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
