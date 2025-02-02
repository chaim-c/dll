using System;

namespace Epic.OnlineServices.ProgressionSnapshot
{
	// Token: 0x02000228 RID: 552
	public struct SubmitSnapshotCallbackInfo : ICallbackInfo
	{
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x00016BD7 File Offset: 0x00014DD7
		// (set) Token: 0x06000F5D RID: 3933 RVA: 0x00016BDF File Offset: 0x00014DDF
		public Result ResultCode { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x00016BE8 File Offset: 0x00014DE8
		// (set) Token: 0x06000F5F RID: 3935 RVA: 0x00016BF0 File Offset: 0x00014DF0
		public uint SnapshotId { get; set; }

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000F60 RID: 3936 RVA: 0x00016BF9 File Offset: 0x00014DF9
		// (set) Token: 0x06000F61 RID: 3937 RVA: 0x00016C01 File Offset: 0x00014E01
		public object ClientData { get; set; }

		// Token: 0x06000F62 RID: 3938 RVA: 0x00016C0C File Offset: 0x00014E0C
		public Result? GetResultCode()
		{
			return new Result?(this.ResultCode);
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x00016C29 File Offset: 0x00014E29
		internal void Set(ref SubmitSnapshotCallbackInfoInternal other)
		{
			this.ResultCode = other.ResultCode;
			this.SnapshotId = other.SnapshotId;
			this.ClientData = other.ClientData;
		}
	}
}
