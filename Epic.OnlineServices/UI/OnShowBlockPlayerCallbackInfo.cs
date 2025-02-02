using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200005F RID: 95
	public struct OnShowBlockPlayerCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600045C RID: 1116 RVA: 0x0000692A File Offset: 0x00004B2A
		// (set) Token: 0x0600045D RID: 1117 RVA: 0x00006932 File Offset: 0x00004B32
		public Result ResultCode { get; set; }

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600045E RID: 1118 RVA: 0x0000693B File Offset: 0x00004B3B
		// (set) Token: 0x0600045F RID: 1119 RVA: 0x00006943 File Offset: 0x00004B43
		public object ClientData { get; set; }

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0000694C File Offset: 0x00004B4C
		// (set) Token: 0x06000461 RID: 1121 RVA: 0x00006954 File Offset: 0x00004B54
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0000695D File Offset: 0x00004B5D
		// (set) Token: 0x06000463 RID: 1123 RVA: 0x00006965 File Offset: 0x00004B65
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x06000464 RID: 1124 RVA: 0x00006970 File Offset: 0x00004B70
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x0000698D File Offset: 0x00004B8D
		internal void Set(ref OnShowBlockPlayerCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
