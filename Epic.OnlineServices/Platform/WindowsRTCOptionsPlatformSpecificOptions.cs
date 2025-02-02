using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200065F RID: 1631
	public struct WindowsRTCOptionsPlatformSpecificOptions
	{
		// Token: 0x17000CA0 RID: 3232
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x0003E74B File Offset: 0x0003C94B
		// (set) Token: 0x060029D8 RID: 10712 RVA: 0x0003E753 File Offset: 0x0003C953
		public Utf8String XAudio29DllPath { get; set; }

		// Token: 0x060029D9 RID: 10713 RVA: 0x0003E75C File Offset: 0x0003C95C
		internal void Set(ref WindowsRTCOptionsPlatformSpecificOptionsInternal other)
		{
			this.XAudio29DllPath = other.XAudio29DllPath;
		}
	}
}
