using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001EC RID: 492
	public struct UpdateReceivingVolumeCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000DCE RID: 3534 RVA: 0x00014850 File Offset: 0x00012A50
		// (set) Token: 0x06000DCF RID: 3535 RVA: 0x00014858 File Offset: 0x00012A58
		public Result ResultCode { get; set; }

		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000DD0 RID: 3536 RVA: 0x00014861 File Offset: 0x00012A61
		// (set) Token: 0x06000DD1 RID: 3537 RVA: 0x00014869 File Offset: 0x00012A69
		public object ClientData { get; set; }

		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000DD2 RID: 3538 RVA: 0x00014872 File Offset: 0x00012A72
		// (set) Token: 0x06000DD3 RID: 3539 RVA: 0x0001487A File Offset: 0x00012A7A
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000DD4 RID: 3540 RVA: 0x00014883 File Offset: 0x00012A83
		// (set) Token: 0x06000DD5 RID: 3541 RVA: 0x0001488B File Offset: 0x00012A8B
		public Utf8String RoomName { get; set; }

		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00014894 File Offset: 0x00012A94
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x0001489C File Offset: 0x00012A9C
		public float Volume { get; set; }

		// Token: 0x06000DD8 RID: 3544 RVA: 0x000148A8 File Offset: 0x00012AA8
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x000148C8 File Offset: 0x00012AC8
		internal void Set(ref UpdateReceivingVolumeCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.Volume = other.Volume;
		}
	}
}
