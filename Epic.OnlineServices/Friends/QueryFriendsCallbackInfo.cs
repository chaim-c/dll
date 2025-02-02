using System;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000478 RID: 1144
	public struct QueryFriendsCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x0002B0AC File Offset: 0x000292AC
		// (set) Token: 0x06001D2F RID: 7471 RVA: 0x0002B0B4 File Offset: 0x000292B4
		public Result ResultCode { get; set; }

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x0002B0BD File Offset: 0x000292BD
		// (set) Token: 0x06001D31 RID: 7473 RVA: 0x0002B0C5 File Offset: 0x000292C5
		public object ClientData { get; set; }

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x06001D32 RID: 7474 RVA: 0x0002B0CE File Offset: 0x000292CE
		// (set) Token: 0x06001D33 RID: 7475 RVA: 0x0002B0D6 File Offset: 0x000292D6
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x06001D34 RID: 7476 RVA: 0x0002B0E0 File Offset: 0x000292E0
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x0002B0FD File Offset: 0x000292FD
		internal void Set(ref QueryFriendsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
