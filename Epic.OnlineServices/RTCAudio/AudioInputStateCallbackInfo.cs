using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001B0 RID: 432
	public struct AudioInputStateCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000308 RID: 776
		// (get) Token: 0x06000C50 RID: 3152 RVA: 0x000126B5 File Offset: 0x000108B5
		// (set) Token: 0x06000C51 RID: 3153 RVA: 0x000126BD File Offset: 0x000108BD
		public object ClientData { get; set; }

		// Token: 0x17000309 RID: 777
		// (get) Token: 0x06000C52 RID: 3154 RVA: 0x000126C6 File Offset: 0x000108C6
		// (set) Token: 0x06000C53 RID: 3155 RVA: 0x000126CE File Offset: 0x000108CE
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x000126D7 File Offset: 0x000108D7
		// (set) Token: 0x06000C55 RID: 3157 RVA: 0x000126DF File Offset: 0x000108DF
		public Utf8String RoomName { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x000126E8 File Offset: 0x000108E8
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x000126F0 File Offset: 0x000108F0
		public RTCAudioInputStatus Status { get; set; }

		// Token: 0x06000C58 RID: 3160 RVA: 0x000126FC File Offset: 0x000108FC
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x00012717 File Offset: 0x00010917
		internal void Set(ref AudioInputStateCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Status = other.Status;
		}
	}
}
