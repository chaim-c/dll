using System;

namespace Epic.OnlineServices.RTCAudio
{
	// Token: 0x020001AA RID: 426
	public struct AudioBuffer
	{
		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000C23 RID: 3107 RVA: 0x0001227B File Offset: 0x0001047B
		// (set) Token: 0x06000C24 RID: 3108 RVA: 0x00012283 File Offset: 0x00010483
		public short[] Frames { get; set; }

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C25 RID: 3109 RVA: 0x0001228C File Offset: 0x0001048C
		// (set) Token: 0x06000C26 RID: 3110 RVA: 0x00012294 File Offset: 0x00010494
		public uint SampleRate { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C27 RID: 3111 RVA: 0x0001229D File Offset: 0x0001049D
		// (set) Token: 0x06000C28 RID: 3112 RVA: 0x000122A5 File Offset: 0x000104A5
		public uint Channels { get; set; }

		// Token: 0x06000C29 RID: 3113 RVA: 0x000122AE File Offset: 0x000104AE
		internal void Set(ref AudioBufferInternal other)
		{
			this.Frames = other.Frames;
			this.SampleRate = other.SampleRate;
			this.Channels = other.Channels;
		}
	}
}
