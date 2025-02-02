using System;

namespace Epic.OnlineServices.TitleStorage
{
	// Token: 0x020000A0 RID: 160
	public struct ReadFileOptions
	{
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x00008DF4 File Offset: 0x00006FF4
		// (set) Token: 0x06000604 RID: 1540 RVA: 0x00008DFC File Offset: 0x00006FFC
		public ProductUserId LocalUserId { get; set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000605 RID: 1541 RVA: 0x00008E05 File Offset: 0x00007005
		// (set) Token: 0x06000606 RID: 1542 RVA: 0x00008E0D File Offset: 0x0000700D
		public Utf8String Filename { get; set; }

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x00008E16 File Offset: 0x00007016
		// (set) Token: 0x06000608 RID: 1544 RVA: 0x00008E1E File Offset: 0x0000701E
		public uint ReadChunkLengthBytes { get; set; }

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x00008E27 File Offset: 0x00007027
		// (set) Token: 0x0600060A RID: 1546 RVA: 0x00008E2F File Offset: 0x0000702F
		public OnReadFileDataCallback ReadFileDataCallback { get; set; }

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00008E38 File Offset: 0x00007038
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x00008E40 File Offset: 0x00007040
		public OnFileTransferProgressCallback FileTransferProgressCallback { get; set; }
	}
}
