using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001B4 RID: 436
	public struct AudioOutputStateCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00012ABD File Offset: 0x00010CBD
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x00012AC5 File Offset: 0x00010CC5
		public object ClientData { get; set; }

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00012ACE File Offset: 0x00010CCE
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x00012AD6 File Offset: 0x00010CD6
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x00012ADF File Offset: 0x00010CDF
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x00012AE7 File Offset: 0x00010CE7
		public Utf8String RoomName { get; set; }

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x00012AF0 File Offset: 0x00010CF0
		// (set) Token: 0x06000C7F RID: 3199 RVA: 0x00012AF8 File Offset: 0x00010CF8
		public RTCAudioOutputStatus Status { get; set; }

		// Token: 0x06000C80 RID: 3200 RVA: 0x00012B04 File Offset: 0x00010D04
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x00012B1F File Offset: 0x00010D1F
		internal void Set(ref AudioOutputStateCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Status = other.Status;
		}
	}
}
