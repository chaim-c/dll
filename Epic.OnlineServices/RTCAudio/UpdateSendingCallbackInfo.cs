using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001F0 RID: 496
	public struct UpdateSendingCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000384 RID: 900
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x00014C2B File Offset: 0x00012E2B
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x00014C33 File Offset: 0x00012E33
		public Result ResultCode { get; set; }

		// Token: 0x17000385 RID: 901
		// (get) Token: 0x06000DF7 RID: 3575 RVA: 0x00014C3C File Offset: 0x00012E3C
		// (set) Token: 0x06000DF8 RID: 3576 RVA: 0x00014C44 File Offset: 0x00012E44
		public object ClientData { get; set; }

		// Token: 0x17000386 RID: 902
		// (get) Token: 0x06000DF9 RID: 3577 RVA: 0x00014C4D File Offset: 0x00012E4D
		// (set) Token: 0x06000DFA RID: 3578 RVA: 0x00014C55 File Offset: 0x00012E55
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000387 RID: 903
		// (get) Token: 0x06000DFB RID: 3579 RVA: 0x00014C5E File Offset: 0x00012E5E
		// (set) Token: 0x06000DFC RID: 3580 RVA: 0x00014C66 File Offset: 0x00012E66
		public Utf8String RoomName { get; set; }

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x00014C6F File Offset: 0x00012E6F
		// (set) Token: 0x06000DFE RID: 3582 RVA: 0x00014C77 File Offset: 0x00012E77
		public RTCAudioStatus AudioStatus { get; set; }

		// Token: 0x06000DFF RID: 3583 RVA: 0x00014C80 File Offset: 0x00012E80
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x00014CA0 File Offset: 0x00012EA0
		internal void Set(ref UpdateSendingCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.RoomName = other.RoomName;
			this.AudioStatus = other.AudioStatus;
		}
	}
}
