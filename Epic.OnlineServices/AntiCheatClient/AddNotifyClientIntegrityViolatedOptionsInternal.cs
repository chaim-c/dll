using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000613 RID: 1555
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyClientIntegrityViolatedOptionsInternal : ISettable<AddNotifyClientIntegrityViolatedOptions>, IDisposable
	{
		// Token: 0x060027E6 RID: 10214 RVA: 0x0003B745 File Offset: 0x00039945
		public void Set(ref AddNotifyClientIntegrityViolatedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060027E7 RID: 10215 RVA: 0x0003B750 File Offset: 0x00039950
		public void Set(ref AddNotifyClientIntegrityViolatedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060027E8 RID: 10216 RVA: 0x0003B771 File Offset: 0x00039971
		public void Dispose()
		{
		}

		// Token: 0x040011EF RID: 4591
		private int m_ApiVersion;
	}
}
