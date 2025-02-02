using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Platform
{
	// Token: 0x0200064E RID: 1614
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetDesktopCrossplayStatusOptionsInternal : ISettable<GetDesktopCrossplayStatusOptions>, IDisposable
	{
		// Token: 0x0600292B RID: 10539 RVA: 0x0003D765 File Offset: 0x0003B965
		public void Set(ref GetDesktopCrossplayStatusOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x0600292C RID: 10540 RVA: 0x0003D770 File Offset: 0x0003B970
		public void Set(ref GetDesktopCrossplayStatusOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x0600292D RID: 10541 RVA: 0x0003D791 File Offset: 0x0003B991
		public void Dispose()
		{
		}

		// Token: 0x04001295 RID: 4757
		private int m_ApiVersion;
	}
}
