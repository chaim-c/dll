using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200065D RID: 1629
	public struct WindowsRTCOptions
	{
		// Token: 0x17000C9E RID: 3230
		// (get) Token: 0x060029CE RID: 10702 RVA: 0x0003E686 File Offset: 0x0003C886
		// (set) Token: 0x060029CF RID: 10703 RVA: 0x0003E68E File Offset: 0x0003C88E
		public WindowsRTCOptionsPlatformSpecificOptions? PlatformSpecificOptions { get; set; }

		// Token: 0x060029D0 RID: 10704 RVA: 0x0003E697 File Offset: 0x0003C897
		internal void Set(ref WindowsRTCOptionsInternal other)
		{
			this.PlatformSpecificOptions = other.PlatformSpecificOptions;
		}
	}
}
