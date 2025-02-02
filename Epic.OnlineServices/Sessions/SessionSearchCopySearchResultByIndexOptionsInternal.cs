using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000144 RID: 324
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchCopySearchResultByIndexOptionsInternal : ISettable<SessionSearchCopySearchResultByIndexOptions>, IDisposable
	{
		// Token: 0x17000214 RID: 532
		// (set) Token: 0x0600097B RID: 2427 RVA: 0x0000DDB8 File Offset: 0x0000BFB8
		public uint SessionIndex
		{
			set
			{
				this.m_SessionIndex = value;
			}
		}

		// Token: 0x0600097C RID: 2428 RVA: 0x0000DDC2 File Offset: 0x0000BFC2
		public void Set(ref SessionSearchCopySearchResultByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.SessionIndex = other.SessionIndex;
		}

		// Token: 0x0600097D RID: 2429 RVA: 0x0000DDDC File Offset: 0x0000BFDC
		public void Set(ref SessionSearchCopySearchResultByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SessionIndex = other.Value.SessionIndex;
			}
		}

		// Token: 0x0600097E RID: 2430 RVA: 0x0000DE12 File Offset: 0x0000C012
		public void Dispose()
		{
		}

		// Token: 0x0400045E RID: 1118
		private int m_ApiVersion;

		// Token: 0x0400045F RID: 1119
		private uint m_SessionIndex;
	}
}
