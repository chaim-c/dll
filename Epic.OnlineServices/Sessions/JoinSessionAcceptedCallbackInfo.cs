using System;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000EB RID: 235
	public struct JoinSessionAcceptedCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000193 RID: 403
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x0000B832 File Offset: 0x00009A32
		// (set) Token: 0x060007A1 RID: 1953 RVA: 0x0000B83A File Offset: 0x00009A3A
		public object ClientData { get; set; }

		// Token: 0x17000194 RID: 404
		// (get) Token: 0x060007A2 RID: 1954 RVA: 0x0000B843 File Offset: 0x00009A43
		// (set) Token: 0x060007A3 RID: 1955 RVA: 0x0000B84B File Offset: 0x00009A4B
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x060007A4 RID: 1956 RVA: 0x0000B854 File Offset: 0x00009A54
		// (set) Token: 0x060007A5 RID: 1957 RVA: 0x0000B85C File Offset: 0x00009A5C
		public ulong UiEventId { get; set; }

		// Token: 0x060007A6 RID: 1958 RVA: 0x0000B868 File Offset: 0x00009A68
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x0000B883 File Offset: 0x00009A83
		internal void Set(ref JoinSessionAcceptedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.UiEventId = other.UiEventId;
		}
	}
}
