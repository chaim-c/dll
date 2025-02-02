using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000BE RID: 190
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionCopyInfoOptionsInternal : ISettable<ActiveSessionCopyInfoOptions>, IDisposable
	{
		// Token: 0x060006CF RID: 1743 RVA: 0x0000A481 File Offset: 0x00008681
		public void Set(ref ActiveSessionCopyInfoOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000A48C File Offset: 0x0000868C
		public void Set(ref ActiveSessionCopyInfoOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x0000A4AD File Offset: 0x000086AD
		public void Dispose()
		{
		}

		// Token: 0x04000347 RID: 839
		private int m_ApiVersion;
	}
}
