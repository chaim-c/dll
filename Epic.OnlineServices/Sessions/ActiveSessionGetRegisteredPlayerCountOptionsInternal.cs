using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C2 RID: 194
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionGetRegisteredPlayerCountOptionsInternal : ISettable<ActiveSessionGetRegisteredPlayerCountOptions>, IDisposable
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x0000A51D File Offset: 0x0000871D
		public void Set(ref ActiveSessionGetRegisteredPlayerCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x0000A528 File Offset: 0x00008728
		public void Set(ref ActiveSessionGetRegisteredPlayerCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x0000A549 File Offset: 0x00008749
		public void Dispose()
		{
		}

		// Token: 0x0400034B RID: 843
		private int m_ApiVersion;
	}
}
