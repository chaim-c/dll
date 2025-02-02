using System;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x02000501 RID: 1281
	public struct OnCustomInviteReceivedCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700099C RID: 2460
		// (get) Token: 0x060020ED RID: 8429 RVA: 0x00030F48 File Offset: 0x0002F148
		// (set) Token: 0x060020EE RID: 8430 RVA: 0x00030F50 File Offset: 0x0002F150
		public object ClientData { get; set; }

		// Token: 0x1700099D RID: 2461
		// (get) Token: 0x060020EF RID: 8431 RVA: 0x00030F59 File Offset: 0x0002F159
		// (set) Token: 0x060020F0 RID: 8432 RVA: 0x00030F61 File Offset: 0x0002F161
		public ProductUserId TargetUserId { get; set; }

		// Token: 0x1700099E RID: 2462
		// (get) Token: 0x060020F1 RID: 8433 RVA: 0x00030F6A File Offset: 0x0002F16A
		// (set) Token: 0x060020F2 RID: 8434 RVA: 0x00030F72 File Offset: 0x0002F172
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060020F3 RID: 8435 RVA: 0x00030F7B File Offset: 0x0002F17B
		// (set) Token: 0x060020F4 RID: 8436 RVA: 0x00030F83 File Offset: 0x0002F183
		public Utf8String CustomInviteId { get; set; }

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060020F5 RID: 8437 RVA: 0x00030F8C File Offset: 0x0002F18C
		// (set) Token: 0x060020F6 RID: 8438 RVA: 0x00030F94 File Offset: 0x0002F194
		public Utf8String Payload { get; set; }

		// Token: 0x060020F7 RID: 8439 RVA: 0x00030FA0 File Offset: 0x0002F1A0
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x00030FBC File Offset: 0x0002F1BC
		internal void Set(ref OnCustomInviteReceivedCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.TargetUserId = other.TargetUserId;
			this.LocalUserId = other.LocalUserId;
			this.CustomInviteId = other.CustomInviteId;
			this.Payload = other.Payload;
		}
	}
}
