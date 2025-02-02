using System;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200006B RID: 107
	public struct ReportKeyEventOptions
	{
		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x00006EEB File Offset: 0x000050EB
		// (set) Token: 0x060004AA RID: 1194 RVA: 0x00006EF3 File Offset: 0x000050F3
		public IntPtr PlatformSpecificInputData { get; set; }

		// Token: 0x060004AB RID: 1195 RVA: 0x00006EFC File Offset: 0x000050FC
		internal void Set(ref ReportKeyEventOptionsInternal other)
		{
			this.PlatformSpecificInputData = other.PlatformSpecificInputData;
		}
	}
}
