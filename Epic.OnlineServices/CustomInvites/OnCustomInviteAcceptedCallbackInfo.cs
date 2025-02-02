using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004FD RID: 1277
	public struct OnCustomInviteAcceptedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000991 RID: 2449
		// (get) Token: 0x060020CA RID: 8394 RVA: 0x00030C44 File Offset: 0x0002EE44
		// (set) Token: 0x060020CB RID: 8395 RVA: 0x00030C4C File Offset: 0x0002EE4C
		public object ClientData { get; set; }

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x00030C55 File Offset: 0x0002EE55
		// (set) Token: 0x060020CD RID: 8397 RVA: 0x00030C5D File Offset: 0x0002EE5D
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060020CE RID: 8398 RVA: 0x00030C66 File Offset: 0x0002EE66
		// (set) Token: 0x060020CF RID: 8399 RVA: 0x00030C6E File Offset: 0x0002EE6E
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x00030C77 File Offset: 0x0002EE77
		// (set) Token: 0x060020D1 RID: 8401 RVA: 0x00030C7F File Offset: 0x0002EE7F
		public Utf8String CustomInviteId { get; set; }

		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x00030C88 File Offset: 0x0002EE88
		// (set) Token: 0x060020D3 RID: 8403 RVA: 0x00030C90 File Offset: 0x0002EE90
		public Utf8String Payload { get; set; }

		// Token: 0x060020D4 RID: 8404 RVA: 0x00030C9C File Offset: 0x0002EE9C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x00030CB8 File Offset: 0x0002EEB8
		internal void Set(ref OnCustomInviteAcceptedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
			this.CustomInviteId = other.CustomInviteId;
			this.Payload = other.Payload;
		}
	}
}
