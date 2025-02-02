using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000065 RID: 101
	public struct OnShowReportPlayerCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x00006B77 File Offset: 0x00004D77
		// (set) Token: 0x06000484 RID: 1156 RVA: 0x00006B7F File Offset: 0x00004D7F
		public Result ResultCode { get; set; }

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x00006B88 File Offset: 0x00004D88
		// (set) Token: 0x06000486 RID: 1158 RVA: 0x00006B90 File Offset: 0x00004D90
		public object ClientData { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x00006B99 File Offset: 0x00004D99
		// (set) Token: 0x06000488 RID: 1160 RVA: 0x00006BA1 File Offset: 0x00004DA1
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x00006BAA File Offset: 0x00004DAA
		// (set) Token: 0x0600048A RID: 1162 RVA: 0x00006BB2 File Offset: 0x00004DB2
		public EpicAccountId TargetUserId { get; set; }

		// Token: 0x0600048B RID: 1163 RVA: 0x00006BBC File Offset: 0x00004DBC
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x00006BD9 File Offset: 0x00004DD9
		internal void Set(ref OnShowReportPlayerCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
