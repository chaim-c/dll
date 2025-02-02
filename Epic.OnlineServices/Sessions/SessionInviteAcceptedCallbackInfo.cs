using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012D RID: 301
	public struct SessionInviteAcceptedCallbackInfo : ICallbackInfo
	{
		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060008FD RID: 2301 RVA: 0x0000CFE3 File Offset: 0x0000B1E3
		// (set) Token: 0x060008FE RID: 2302 RVA: 0x0000CFEB File Offset: 0x0000B1EB
		public object ClientData { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060008FF RID: 2303 RVA: 0x0000CFF4 File Offset: 0x0000B1F4
		// (set) Token: 0x06000900 RID: 2304 RVA: 0x0000CFFC File Offset: 0x0000B1FC
		public Utf8String SessionId { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x0000D005 File Offset: 0x0000B205
		// (set) Token: 0x06000902 RID: 2306 RVA: 0x0000D00D File Offset: 0x0000B20D
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x0000D016 File Offset: 0x0000B216
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x0000D01E File Offset: 0x0000B21E
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000905 RID: 2309 RVA: 0x0000D027 File Offset: 0x0000B227
		// (set) Token: 0x06000906 RID: 2310 RVA: 0x0000D02F File Offset: 0x0000B22F
		public Utf8String InviteId { get; set; }

		// Token: 0x06000907 RID: 2311 RVA: 0x0000D038 File Offset: 0x0000B238
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x0000D054 File Offset: 0x0000B254
		internal void Set(ref SessionInviteAcceptedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.SessionId = other.SessionId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.InviteId = other.InviteId;
		}
	}
}
