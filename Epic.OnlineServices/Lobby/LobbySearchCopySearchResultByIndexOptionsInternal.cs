using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x0200039C RID: 924
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchCopySearchResultByIndexOptionsInternal : ISettable<LobbySearchCopySearchResultByIndexOptions>, IDisposable
	{
		// Token: 0x170006FB RID: 1787
		// (set) Token: 0x06001879 RID: 6265 RVA: 0x000253D0 File Offset: 0x000235D0
		public uint LobbyIndex
		{
			set
			{
				this.m_LobbyIndex = value;
			}
		}

		// Token: 0x0600187A RID: 6266 RVA: 0x000253DA File Offset: 0x000235DA
		public void Set(ref LobbySearchCopySearchResultByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LobbyIndex = other.LobbyIndex;
		}

		// Token: 0x0600187B RID: 6267 RVA: 0x000253F4 File Offset: 0x000235F4
		public void Set(ref LobbySearchCopySearchResultByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LobbyIndex = other.Value.LobbyIndex;
			}
		}

		// Token: 0x0600187C RID: 6268 RVA: 0x0002542A File Offset: 0x0002362A
		public void Dispose()
		{
		}

		// Token: 0x04000B39 RID: 2873
		private int m_ApiVersion;

		// Token: 0x04000B3A RID: 2874
		private uint m_LobbyIndex;
	}
}
