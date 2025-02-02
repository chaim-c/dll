using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200006C RID: 108
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ReportKeyEventOptionsInternal : IGettable<ReportKeyEventOptions>, ISettable<ReportKeyEventOptions>, IDisposable
	{
		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x00006F0C File Offset: 0x0000510C
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x00006F24 File Offset: 0x00005124
		public IntPtr PlatformSpecificInputData
		{
			get
			{
				return this.m_PlatformSpecificInputData;
			}
			set
			{
				this.m_PlatformSpecificInputData = value;
			}
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00006F2E File Offset: 0x0000512E
		public void Set(ref ReportKeyEventOptions other)
		{
			this.m_ApiVersion = 1;
			this.PlatformSpecificInputData = other.PlatformSpecificInputData;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00006F48 File Offset: 0x00005148
		public void Set(ref ReportKeyEventOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PlatformSpecificInputData = other.Value.PlatformSpecificInputData;
			}
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00006F7E File Offset: 0x0000517E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PlatformSpecificInputData);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00006F8D File Offset: 0x0000518D
		public void Get(out ReportKeyEventOptions output)
		{
			output = default(ReportKeyEventOptions);
			output.Set(ref this);
		}

		// Token: 0x04000255 RID: 597
		private int m_ApiVersion;

		// Token: 0x04000256 RID: 598
		private IntPtr m_PlatformSpecificInputData;
	}
}
