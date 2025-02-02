using System;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005CA RID: 1482
	public struct ProtectMessageOptions
	{
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06002610 RID: 9744 RVA: 0x000389A5 File Offset: 0x00036BA5
		// (set) Token: 0x06002611 RID: 9745 RVA: 0x000389AD File Offset: 0x00036BAD
		public IntPtr ClientHandle { get; set; }

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06002612 RID: 9746 RVA: 0x000389B6 File Offset: 0x00036BB6
		// (set) Token: 0x06002613 RID: 9747 RVA: 0x000389BE File Offset: 0x00036BBE
		public ArraySegment<byte> Data { get; set; }

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06002614 RID: 9748 RVA: 0x000389C7 File Offset: 0x00036BC7
		// (set) Token: 0x06002615 RID: 9749 RVA: 0x000389CF File Offset: 0x00036BCF
		public uint OutBufferSizeBytes { get; set; }
	}
}
