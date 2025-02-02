using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A6 RID: 422
	public struct AudioBeforeRenderCallbackInfo : ICallbackInfo
	{
		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x06000BF1 RID: 3057 RVA: 0x00011D0E File Offset: 0x0000FF0E
		// (set) Token: 0x06000BF2 RID: 3058 RVA: 0x00011D16 File Offset: 0x0000FF16
		public object ClientData { get; set; }

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x06000BF3 RID: 3059 RVA: 0x00011D1F File Offset: 0x0000FF1F
		// (set) Token: 0x06000BF4 RID: 3060 RVA: 0x00011D27 File Offset: 0x0000FF27
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x06000BF5 RID: 3061 RVA: 0x00011D30 File Offset: 0x0000FF30
		// (set) Token: 0x06000BF6 RID: 3062 RVA: 0x00011D38 File Offset: 0x0000FF38
		public Utf8String RoomName { get; set; }

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x06000BF7 RID: 3063 RVA: 0x00011D41 File Offset: 0x0000FF41
		// (set) Token: 0x06000BF8 RID: 3064 RVA: 0x00011D49 File Offset: 0x0000FF49
		public AudioBuffer? Buffer { get; set; }

		// Token: 0x170002E9 RID: 745
		// (get) Token: 0x06000BF9 RID: 3065 RVA: 0x00011D52 File Offset: 0x0000FF52
		// (set) Token: 0x06000BFA RID: 3066 RVA: 0x00011D5A File Offset: 0x0000FF5A
		public ProductUserId ParticipantId { get; set; }

		// Token: 0x06000BFB RID: 3067 RVA: 0x00011D64 File Offset: 0x0000FF64
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00011D80 File Offset: 0x0000FF80
		internal void Set(ref AudioBeforeRenderCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Buffer = other.Buffer;
			this.ParticipantId = other.ParticipantId;
		}
	}
}
