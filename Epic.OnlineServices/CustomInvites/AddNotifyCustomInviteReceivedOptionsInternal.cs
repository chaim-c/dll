using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.CustomInvites
{
	// Token: 0x020004F3 RID: 1267
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyCustomInviteReceivedOptionsInternal : ISettable<AddNotifyCustomInviteReceivedOptions>, IDisposable
	{
		// Token: 0x06002083 RID: 8323 RVA: 0x00030474 File Offset: 0x0002E674
		public void Set(ref AddNotifyCustomInviteReceivedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x00030480 File Offset: 0x0002E680
		public void Set(ref AddNotifyCustomInviteReceivedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x000304A1 File Offset: 0x0002E6A1
		public void Dispose()
		{
		}

		// Token: 0x04000E80 RID: 3712
		private int m_ApiVersion;
	}
}
