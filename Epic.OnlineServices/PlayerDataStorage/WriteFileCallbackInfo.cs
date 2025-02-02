using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200029A RID: 666
	public struct WriteFileCallbackInfo : ICallbackInfo
	{
		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x0001AD64 File Offset: 0x00018F64
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x0001AD6C File Offset: 0x00018F6C
		public Result ResultCode { get; set; }

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x0001AD75 File Offset: 0x00018F75
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x0001AD7D File Offset: 0x00018F7D
		public object ClientData { get; set; }

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x0001AD86 File Offset: 0x00018F86
		// (set) Token: 0x0600122E RID: 4654 RVA: 0x0001AD8E File Offset: 0x00018F8E
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0001AD97 File Offset: 0x00018F97
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x0001AD9F File Offset: 0x00018F9F
		public Utf8String Filename { get; set; }

		// Token: 0x06001231 RID: 4657 RVA: 0x0001ADA8 File Offset: 0x00018FA8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001232 RID: 4658 RVA: 0x0001ADC5 File Offset: 0x00018FC5
		internal void Set(ref WriteFileCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}
	}
}
