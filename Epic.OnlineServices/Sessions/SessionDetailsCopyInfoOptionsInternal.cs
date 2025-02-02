using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000122 RID: 290
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsCopyInfoOptionsInternal : ISettable<SessionDetailsCopyInfoOptions>, IDisposable
	{
		// Token: 0x060008B9 RID: 2233 RVA: 0x0000C970 File Offset: 0x0000AB70
		public void Set(ref SessionDetailsCopyInfoOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060008BA RID: 2234 RVA: 0x0000C97C File Offset: 0x0000AB7C
		public void Set(ref SessionDetailsCopyInfoOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060008BB RID: 2235 RVA: 0x0000C99D File Offset: 0x0000AB9D
		public void Dispose()
		{
		}

		// Token: 0x040003FF RID: 1023
		private int m_ApiVersion;
	}
}
