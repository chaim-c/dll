using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatServer
{
	// Token: 0x020005C1 RID: 1473
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndSessionOptionsInternal : ISettable<EndSessionOptions>, IDisposable
	{
		// Token: 0x060025EF RID: 9711 RVA: 0x00038908 File Offset: 0x00036B08
		public void Set(ref EndSessionOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060025F0 RID: 9712 RVA: 0x00038914 File Offset: 0x00036B14
		public void Set(ref EndSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060025F1 RID: 9713 RVA: 0x00038935 File Offset: 0x00036B35
		public void Dispose()
		{
		}

		// Token: 0x040010A0 RID: 4256
		private int m_ApiVersion;
	}
}
