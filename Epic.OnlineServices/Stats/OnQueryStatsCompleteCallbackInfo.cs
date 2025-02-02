using System;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000B5 RID: 181
	public struct OnQueryStatsCompleteCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000682 RID: 1666 RVA: 0x00009B57 File Offset: 0x00007D57
		// (set) Token: 0x06000683 RID: 1667 RVA: 0x00009B5F File Offset: 0x00007D5F
		public Result ResultCode { get; set; }

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000684 RID: 1668 RVA: 0x00009B68 File Offset: 0x00007D68
		// (set) Token: 0x06000685 RID: 1669 RVA: 0x00009B70 File Offset: 0x00007D70
		public object ClientData { get; set; }

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x00009B79 File Offset: 0x00007D79
		// (set) Token: 0x06000687 RID: 1671 RVA: 0x00009B81 File Offset: 0x00007D81
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000688 RID: 1672 RVA: 0x00009B8A File Offset: 0x00007D8A
		// (set) Token: 0x06000689 RID: 1673 RVA: 0x00009B92 File Offset: 0x00007D92
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x0600068A RID: 1674 RVA: 0x00009B9C File Offset: 0x00007D9C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x0600068B RID: 1675 RVA: 0x00009BB9 File Offset: 0x00007DB9
		internal void Set(ref OnQueryStatsCompleteCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}
	}
}
