using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000377 RID: 887
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyDetailsGetMemberByIndexOptionsInternal : ISettable<LobbyDetailsGetMemberByIndexOptions>, IDisposable
	{
		// Token: 0x170006A0 RID: 1696
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x0002296A File Offset: 0x00020B6A
		public uint MemberIndex
		{
			set
			{
				this.m_MemberIndex = value;
			}
		}

		// Token: 0x0600173D RID: 5949 RVA: 0x00022974 File Offset: 0x00020B74
		public void Set(ref LobbyDetailsGetMemberByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.MemberIndex = other.MemberIndex;
		}

		// Token: 0x0600173E RID: 5950 RVA: 0x0002298C File Offset: 0x00020B8C
		public void Set(ref LobbyDetailsGetMemberByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.MemberIndex = other.Value.MemberIndex;
			}
		}

		// Token: 0x0600173F RID: 5951 RVA: 0x000229C2 File Offset: 0x00020BC2
		public void Dispose()
		{
		}

		// Token: 0x04000A91 RID: 2705
		private int m_ApiVersion;

		// Token: 0x04000A92 RID: 2706
		private uint m_MemberIndex;
	}
}
