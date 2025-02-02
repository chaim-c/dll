using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003A2 RID: 930
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchGetSearchResultCountOptionsInternal : ISettable<LobbySearchGetSearchResultCountOptions>, IDisposable
	{
		// Token: 0x06001892 RID: 6290 RVA: 0x00025601 File Offset: 0x00023801
		public void Set(ref LobbySearchGetSearchResultCountOptions other)
		{
			this.m_ApiVersion = 1;
		}

		// Token: 0x06001893 RID: 6291 RVA: 0x0002560C File Offset: 0x0002380C
		public void Set(ref LobbySearchGetSearchResultCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
			}
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x0002562D File Offset: 0x0002382D
		public void Dispose()
		{
		}

		// Token: 0x04000B42 RID: 2882
		private int m_ApiVersion;
	}
}
