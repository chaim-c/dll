using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C8 RID: 200
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifySessionInviteAcceptedOptionsInternal : ISettable<AddNotifySessionInviteAcceptedOptions>, IDisposable
	{
		// Token: 0x060006F3 RID: 1779 RVA: 0x0000A7A0 File Offset: 0x000089A0
		public void Set(ref AddNotifySessionInviteAcceptedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x060006F4 RID: 1780 RVA: 0x0000A7AC File Offset: 0x000089AC
		public void Set(ref AddNotifySessionInviteAcceptedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x060006F5 RID: 1781 RVA: 0x0000A7CD File Offset: 0x000089CD
		public void Dispose()
		{
		}

		// Token: 0x04000356 RID: 854
		private int m_ApiVersion;
	}
}
