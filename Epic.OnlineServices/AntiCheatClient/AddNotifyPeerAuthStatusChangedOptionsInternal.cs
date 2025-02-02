using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x0200061B RID: 1563
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPeerAuthStatusChangedOptionsInternal : ISettable<AddNotifyPeerAuthStatusChangedOptions>, IDisposable
	{
		// Token: 0x060027F2 RID: 10226 RVA: 0x0003B804 File Offset: 0x00039A04
		public void Set(ref AddNotifyPeerAuthStatusChangedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060027F3 RID: 10227 RVA: 0x0003B810 File Offset: 0x00039A10
		public void Set(ref AddNotifyPeerAuthStatusChangedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060027F4 RID: 10228 RVA: 0x0003B831 File Offset: 0x00039A31
		public void Dispose()
		{
		}

		// Token: 0x040011F3 RID: 4595
		private int m_ApiVersion;
	}
}
