using System;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x02000659 RID: 1625
	public struct RTCOptions
	{
		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06002998 RID: 10648 RVA: 0x0003E166 File Offset: 0x0003C366
		// (set) Token: 0x06002999 RID: 10649 RVA: 0x0003E16E File Offset: 0x0003C36E
		public IntPtr PlatformSpecificOptions { get; set; }

		// Token: 0x0600299A RID: 10650 RVA: 0x0003E177 File Offset: 0x0003C377
		internal void Set(ref RTCOptionsInternal other)
		{
			this.PlatformSpecificOptions = other.PlatformSpecificOptions;
		}
	}
}
