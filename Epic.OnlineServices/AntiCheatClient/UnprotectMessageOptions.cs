using System;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200063D RID: 1597
	public struct UnprotectMessageOptions
	{
		// Token: 0x17000C26 RID: 3110
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x0003C7E2 File Offset: 0x0003A9E2
		// (set) Token: 0x060028A4 RID: 10404 RVA: 0x0003C7EA File Offset: 0x0003A9EA
		public ArraySegment<byte> Data { get; set; }

		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060028A5 RID: 10405 RVA: 0x0003C7F3 File Offset: 0x0003A9F3
		// (set) Token: 0x060028A6 RID: 10406 RVA: 0x0003C7FB File Offset: 0x0003A9FB
		public uint OutBufferSizeBytes { get; set; }
	}
}
