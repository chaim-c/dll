using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000CA RID: 202
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifySessionInviteReceivedOptionsInternal : ISettable<AddNotifySessionInviteReceivedOptions>, IDisposable
	{
		// Token: 0x060006F6 RID: 1782 RVA: 0x0000A7D0 File Offset: 0x000089D0
		public void Set(ref AddNotifySessionInviteReceivedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060006F7 RID: 1783 RVA: 0x0000A7DC File Offset: 0x000089DC
		public void Set(ref AddNotifySessionInviteReceivedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060006F8 RID: 1784 RVA: 0x0000A7FD File Offset: 0x000089FD
		public void Dispose()
		{
		}

		// Token: 0x04000357 RID: 855
		private int m_ApiVersion;
	}
}
