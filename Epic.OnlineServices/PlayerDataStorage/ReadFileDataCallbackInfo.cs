using System;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000295 RID: 661
	public struct ReadFileDataCallbackInfo : ICallbackInfo
	{
		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x060011F8 RID: 4600 RVA: 0x0001A7AB File Offset: 0x000189AB
		// (set) Token: 0x060011F9 RID: 4601 RVA: 0x0001A7B3 File Offset: 0x000189B3
		public object ClientData { get; set; }

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x060011FA RID: 4602 RVA: 0x0001A7BC File Offset: 0x000189BC
		// (set) Token: 0x060011FB RID: 4603 RVA: 0x0001A7C4 File Offset: 0x000189C4
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x060011FC RID: 4604 RVA: 0x0001A7CD File Offset: 0x000189CD
		// (set) Token: 0x060011FD RID: 4605 RVA: 0x0001A7D5 File Offset: 0x000189D5
		public Utf8String Filename { get; set; }

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x060011FE RID: 4606 RVA: 0x0001A7DE File Offset: 0x000189DE
		// (set) Token: 0x060011FF RID: 4607 RVA: 0x0001A7E6 File Offset: 0x000189E6
		public uint TotalFileSizeBytes { get; set; }

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0001A7EF File Offset: 0x000189EF
		// (set) Token: 0x06001201 RID: 4609 RVA: 0x0001A7F7 File Offset: 0x000189F7
		public bool IsLastChunk { get; set; }

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001202 RID: 4610 RVA: 0x0001A800 File Offset: 0x00018A00
		// (set) Token: 0x06001203 RID: 4611 RVA: 0x0001A808 File Offset: 0x00018A08
		public ArraySegment<byte> DataChunk { get; set; }

		// Token: 0x06001204 RID: 4612 RVA: 0x0001A814 File Offset: 0x00018A14
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x0001A830 File Offset: 0x00018A30
		internal void Set(ref ReadFileDataCallbackInfoInternal other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
			this.TotalFileSizeBytes = other.TotalFileSizeBytes;
			this.IsLastChunk = other.IsLastChunk;
			this.DataChunk = other.DataChunk;
		}
	}
}
