using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000619 RID: 1561
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerActionRequiredOptionsInternal : ISettable<AddNotifyPeerActionRequiredOptions>, IDisposable
	{
		// Token: 0x060027EF RID: 10223 RVA: 0x0003B7D4 File Offset: 0x000399D4
		public void Set(ref AddNotifyPeerActionRequiredOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060027F0 RID: 10224 RVA: 0x0003B7E0 File Offset: 0x000399E0
		public void Set(ref AddNotifyPeerActionRequiredOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060027F1 RID: 10225 RVA: 0x0003B801 File Offset: 0x00039A01
		public void Dispose()
		{
		}

		// Token: 0x040011F2 RID: 4594
		private int m_ApiVersion;
	}
}
