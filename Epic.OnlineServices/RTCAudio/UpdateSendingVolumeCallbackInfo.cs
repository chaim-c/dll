using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001F4 RID: 500
	public struct UpdateSendingVolumeCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x00015003 File Offset: 0x00013203
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x0001500B File Offset: 0x0001320B
		public Result ResultCode { get; set; }

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x00015014 File Offset: 0x00013214
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x0001501C File Offset: 0x0001321C
		public object ClientData { get; set; }

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x00015025 File Offset: 0x00013225
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x0001502D File Offset: 0x0001322D
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x00015036 File Offset: 0x00013236
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x0001503E File Offset: 0x0001323E
		public Utf8String RoomName { get; set; }

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x00015047 File Offset: 0x00013247
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x0001504F File Offset: 0x0001324F
		public float Volume { get; set; }

		// Token: 0x06000E26 RID: 3622 RVA: 0x00015058 File Offset: 0x00013258
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x00015078 File Offset: 0x00013278
		internal void Set(ref UpdateSendingVolumeCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Volume = other.Volume;
		}
	}
}
