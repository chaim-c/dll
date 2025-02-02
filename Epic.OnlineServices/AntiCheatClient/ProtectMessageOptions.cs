using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000635 RID: 1589
	public struct ProtectMessageOptions
	{
		// Token: 0x17000C0E RID: 3086
		// (get) Token: 0x06002873 RID: 10355 RVA: 0x0003C3A9 File Offset: 0x0003A5A9
		// (set) Token: 0x06002874 RID: 10356 RVA: 0x0003C3B1 File Offset: 0x0003A5B1
		public ArraySegment<byte> Data { get; set; }

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x06002875 RID: 10357 RVA: 0x0003C3BA File Offset: 0x0003A5BA
		// (set) Token: 0x06002876 RID: 10358 RVA: 0x0003C3C2 File Offset: 0x0003A5C2
		public uint OutBufferSizeBytes { get; set; }
	}
}
