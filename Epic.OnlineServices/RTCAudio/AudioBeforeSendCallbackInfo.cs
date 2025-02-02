using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001A8 RID: 424
	public struct AudioBeforeSendCallbackInfo : ICallbackInfo
	{
		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000C0C RID: 3084 RVA: 0x00012010 File Offset: 0x00010210
		// (set) Token: 0x06000C0D RID: 3085 RVA: 0x00012018 File Offset: 0x00010218
		public object ClientData { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000C0E RID: 3086 RVA: 0x00012021 File Offset: 0x00010221
		// (set) Token: 0x06000C0F RID: 3087 RVA: 0x00012029 File Offset: 0x00010229
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000C10 RID: 3088 RVA: 0x00012032 File Offset: 0x00010232
		// (set) Token: 0x06000C11 RID: 3089 RVA: 0x0001203A File Offset: 0x0001023A
		public Utf8String RoomName { get; set; }

		// Token: 0x170002F3 RID: 755
		// (get) Token: 0x06000C12 RID: 3090 RVA: 0x00012043 File Offset: 0x00010243
		// (set) Token: 0x06000C13 RID: 3091 RVA: 0x0001204B File Offset: 0x0001024B
		public AudioBuffer? Buffer { get; set; }

		// Token: 0x06000C14 RID: 3092 RVA: 0x00012054 File Offset: 0x00010254
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x0001206F File Offset: 0x0001026F
		internal void Set(ref AudioBeforeSendCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Buffer = other.Buffer;
		}
	}
}
