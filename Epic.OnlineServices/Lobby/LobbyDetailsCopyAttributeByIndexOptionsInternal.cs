using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000367 RID: 871
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsCopyAttributeByIndexOptionsInternal : ISettable<LobbyDetailsCopyAttributeByIndexOptions>, IDisposable
	{
		// Token: 0x17000692 RID: 1682
		// (set) Token: 0x0600170F RID: 5903 RVA: 0x000225EE File Offset: 0x000207EE
		public uint AttrIndex
		{
			set
			{
				this.m_AttrIndex = value;
			}
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x000225F8 File Offset: 0x000207F8
		public void Set(ref LobbyDetailsCopyAttributeByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.AttrIndex = other.AttrIndex;
		}

		// Token: 0x06001711 RID: 5905 RVA: 0x00022610 File Offset: 0x00020810
		public void Set(ref LobbyDetailsCopyAttributeByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AttrIndex = other.Value.AttrIndex;
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00022646 File Offset: 0x00020846
		public void Dispose()
		{
		}

		// Token: 0x04000A7B RID: 2683
		private int m_ApiVersion;

		// Token: 0x04000A7C RID: 2684
		private uint m_AttrIndex;
	}
}
