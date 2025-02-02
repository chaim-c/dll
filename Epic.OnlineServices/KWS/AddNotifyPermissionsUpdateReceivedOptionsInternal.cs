using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x02000426 RID: 1062
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyPermissionsUpdateReceivedOptionsInternal : ISettable<AddNotifyPermissionsUpdateReceivedOptions>, IDisposable
	{
		// Token: 0x06001B57 RID: 6999 RVA: 0x000287F8 File Offset: 0x000269F8
		public void Set(ref AddNotifyPermissionsUpdateReceivedOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x00028804 File Offset: 0x00026A04
		public void Set(ref AddNotifyPermissionsUpdateReceivedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x00028825 File Offset: 0x00026A25
		public void Dispose()
		{
		}

		// Token: 0x04000C29 RID: 3113
		private int m_ApiVersion;
	}
}
