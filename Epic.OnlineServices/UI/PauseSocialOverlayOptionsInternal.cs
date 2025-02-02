using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000068 RID: 104
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PauseSocialOverlayOptionsInternal : ISettable<PauseSocialOverlayOptions>, IDisposable
	{
		// Token: 0x1700009A RID: 154
		// (set) Token: 0x0600049C RID: 1180 RVA: 0x00006DD4 File Offset: 0x00004FD4
		public bool IsPaused
		{
			set
			{
				Helper.Set(value, ref this.m_IsPaused);
			}
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x00006DE4 File Offset: 0x00004FE4
		public void Set(ref PauseSocialOverlayOptions other)
		{
			this.m_ApiVersion = 1;
			this.IsPaused = other.IsPaused;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x00006DFC File Offset: 0x00004FFC
		public void Set(ref PauseSocialOverlayOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.IsPaused = other.Value.IsPaused;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x00006E32 File Offset: 0x00005032
		public void Dispose()
		{
		}

		// Token: 0x0400024F RID: 591
		private int m_ApiVersion;

		// Token: 0x04000250 RID: 592
		private int m_IsPaused;
	}
}
