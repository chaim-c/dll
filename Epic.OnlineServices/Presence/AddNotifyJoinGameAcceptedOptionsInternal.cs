using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x0200022D RID: 557
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AddNotifyJoinGameAcceptedOptionsInternal : ISettable<AddNotifyJoinGameAcceptedOptions>, IDisposable
	{
		// Token: 0x06000F75 RID: 3957 RVA: 0x00016DF9 File Offset: 0x00014FF9
		public void Set(ref AddNotifyJoinGameAcceptedOptions other)
		{
			this.m_ApiVersion = 2;
		}

		// Token: 0x06000F76 RID: 3958 RVA: 0x00016E04 File Offset: 0x00015004
		public void Set(ref AddNotifyJoinGameAcceptedOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
			}
		}

		// Token: 0x06000F77 RID: 3959 RVA: 0x00016E25 File Offset: 0x00015025
		public void Dispose()
		{
		}

		// Token: 0x040006EA RID: 1770
		private int m_ApiVersion;
	}
}
