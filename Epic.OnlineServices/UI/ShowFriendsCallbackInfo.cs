using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000073 RID: 115
	public struct ShowFriendsCallbackInfo : ICallbackInfo
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060004C7 RID: 1223 RVA: 0x00007146 File Offset: 0x00005346
		// (set) Token: 0x060004C8 RID: 1224 RVA: 0x0000714E File Offset: 0x0000534E
		public Result ResultCode { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00007157 File Offset: 0x00005357
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x0000715F File Offset: 0x0000535F
		public object ClientData { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00007168 File Offset: 0x00005368
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00007170 File Offset: 0x00005370
		public EpicAccountId LocalUserId { get; set; }

		// Token: 0x060004CD RID: 1229 RVA: 0x0000717C File Offset: 0x0000537C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00007199 File Offset: 0x00005399
		internal void Set(ref ShowFriendsCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}
	}
}
