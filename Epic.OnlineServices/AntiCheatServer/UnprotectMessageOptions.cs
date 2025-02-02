using System;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005D2 RID: 1490
	public struct UnprotectMessageOptions
	{
		// Token: 0x17000B40 RID: 2880
		// (get) Token: 0x06002643 RID: 9795 RVA: 0x00038E2A File Offset: 0x0003702A
		// (set) Token: 0x06002644 RID: 9796 RVA: 0x00038E32 File Offset: 0x00037032
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000B41 RID: 2881
		// (get) Token: 0x06002645 RID: 9797 RVA: 0x00038E3B File Offset: 0x0003703B
		// (set) Token: 0x06002646 RID: 9798 RVA: 0x00038E43 File Offset: 0x00037043
		public ArraySegment<byte> Data { get; set; }

		// Token: 0x17000B42 RID: 2882
		// (get) Token: 0x06002647 RID: 9799 RVA: 0x00038E4C File Offset: 0x0003704C
		// (set) Token: 0x06002648 RID: 9800 RVA: 0x00038E54 File Offset: 0x00037054
		public uint OutBufferSizeBytes { get; set; }
	}
}
