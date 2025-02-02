using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x0200009E RID: 158
	public struct ReadFileDataCallbackInfo : ICallbackInfo
	{
		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x00008A8B File Offset: 0x00006C8B
		// (set) Token: 0x060005E5 RID: 1509 RVA: 0x00008A93 File Offset: 0x00006C93
		public object ClientData { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x00008A9C File Offset: 0x00006C9C
		// (set) Token: 0x060005E7 RID: 1511 RVA: 0x00008AA4 File Offset: 0x00006CA4
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x00008AAD File Offset: 0x00006CAD
		// (set) Token: 0x060005E9 RID: 1513 RVA: 0x00008AB5 File Offset: 0x00006CB5
		public Utf8String Filename { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00008ABE File Offset: 0x00006CBE
		// (set) Token: 0x060005EB RID: 1515 RVA: 0x00008AC6 File Offset: 0x00006CC6
		public uint TotalFileSizeBytes { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x00008ACF File Offset: 0x00006CCF
		// (set) Token: 0x060005ED RID: 1517 RVA: 0x00008AD7 File Offset: 0x00006CD7
		public bool IsLastChunk { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00008AE0 File Offset: 0x00006CE0
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x00008AE8 File Offset: 0x00006CE8
		public ArraySegment<byte> DataChunk { get; set; }

		// Token: 0x060005F0 RID: 1520 RVA: 0x00008AF4 File Offset: 0x00006CF4
		public Result? GetResultCode()
		{
			return null;
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00008B10 File Offset: 0x00006D10
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
