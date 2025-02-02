using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000341 RID: 833
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateLobbySearchOptionsInternal : ISettable<CreateLobbySearchOptions>, IDisposable
	{
		// Token: 0x17000629 RID: 1577
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x00020B6C File Offset: 0x0001ED6C
		public uint MaxResults
		{
			set
			{
				this.m_MaxResults = value;
			}
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x00020B76 File Offset: 0x0001ED76
		public void Set(ref CreateLobbySearchOptions other)
		{
			this.m_ApiVersion = 1;
			this.MaxResults = other.MaxResults;
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x00020B90 File Offset: 0x0001ED90
		public void Set(ref CreateLobbySearchOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.MaxResults = other.Value.MaxResults;
			}
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x00020BC6 File Offset: 0x0001EDC6
		public void Dispose()
		{
		}

		// Token: 0x04000A00 RID: 2560
		private int m_ApiVersion;

		// Token: 0x04000A01 RID: 2561
		private uint m_MaxResults;
	}
}
