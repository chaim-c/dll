using System;

namespace Epic.OnlineServices.RTC
{
	// Token: 0x02000177 RID: 375
	public struct BlockParticipantCallbackInfo : ICallbackInfo
	{
		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x0000FE96 File Offset: 0x0000E096
		// (set) Token: 0x06000AA7 RID: 2727 RVA: 0x0000FE9E File Offset: 0x0000E09E
		public Result ResultCode { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x0000FEA7 File Offset: 0x0000E0A7
		// (set) Token: 0x06000AA9 RID: 2729 RVA: 0x0000FEAF File Offset: 0x0000E0AF
		public object ClientData { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x0000FEB8 File Offset: 0x0000E0B8
		// (set) Token: 0x06000AAB RID: 2731 RVA: 0x0000FEC0 File Offset: 0x0000E0C0
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000AAC RID: 2732 RVA: 0x0000FEC9 File Offset: 0x0000E0C9
		// (set) Token: 0x06000AAD RID: 2733 RVA: 0x0000FED1 File Offset: 0x0000E0D1
		public Utf8String RoomName { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000AAE RID: 2734 RVA: 0x0000FEDA File Offset: 0x0000E0DA
		// (set) Token: 0x06000AAF RID: 2735 RVA: 0x0000FEE2 File Offset: 0x0000E0E2
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000AB0 RID: 2736 RVA: 0x0000FEEB File Offset: 0x0000E0EB
		// (set) Token: 0x06000AB1 RID: 2737 RVA: 0x0000FEF3 File Offset: 0x0000E0F3
		public bool Blocked { get; set; }

		// Token: 0x06000AB2 RID: 2738 RVA: 0x0000FEFC File Offset: 0x0000E0FC
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x0000FF1C File Offset: 0x0000E11C
		internal void Set(ref BlockParticipantCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.ParticipantId = other.ParticipantId;
			this.Blocked = other.Blocked;
		}
	}
}
