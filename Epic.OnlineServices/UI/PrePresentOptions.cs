using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000069 RID: 105
	public struct PrePresentOptions
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x00006E35 File Offset: 0x00005035
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x00006E3D File Offset: 0x0000503D
		public IntPtr PlatformSpecificData { get; set; }

		// Token: 0x060004A2 RID: 1186 RVA: 0x00006E46 File Offset: 0x00005046
		internal void Set(ref PrePresentOptionsInternal other)
		{
			this.PlatformSpecificData = other.PlatformSpecificData;
		}
	}
}
