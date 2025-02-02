using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000271 RID: 625
	public struct FileTransferProgressCallbackInfo : ICallbackInfo
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x0600111B RID: 4379 RVA: 0x00019637 File Offset: 0x00017837
		// (set) Token: 0x0600111C RID: 4380 RVA: 0x0001963F File Offset: 0x0001783F
		public object ClientData { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x0600111D RID: 4381 RVA: 0x00019648 File Offset: 0x00017848
		// (set) Token: 0x0600111E RID: 4382 RVA: 0x00019650 File Offset: 0x00017850
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600111F RID: 4383 RVA: 0x00019659 File Offset: 0x00017859
		// (set) Token: 0x06001120 RID: 4384 RVA: 0x00019661 File Offset: 0x00017861
		public Utf8String Filename { get; set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06001121 RID: 4385 RVA: 0x0001966A File Offset: 0x0001786A
		// (set) Token: 0x06001122 RID: 4386 RVA: 0x00019672 File Offset: 0x00017872
		public uint BytesTransferred { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06001123 RID: 4387 RVA: 0x0001967B File Offset: 0x0001787B
		// (set) Token: 0x06001124 RID: 4388 RVA: 0x00019683 File Offset: 0x00017883
		public uint TotalFileSizeBytes { get; set; }

		// Token: 0x06001125 RID: 4389 RVA: 0x0001968C File Offset: 0x0001788C
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001126 RID: 4390 RVA: 0x000196A8 File Offset: 0x000178A8
		internal void Set(ref FileTransferProgressCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.BytesTransferred = other.BytesTransferred;
			this.TotalFileSizeBytes = other.TotalFileSizeBytes;
		}
	}
}
