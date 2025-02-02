using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000C0 RID: 192
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ActiveSessionGetRegisteredPlayerByIndexOptionsInternal : ISettable<ActiveSessionGetRegisteredPlayerByIndexOptions>, IDisposable
	{
		// Token: 0x1700014B RID: 331
		// (set) Token: 0x060006D4 RID: 1748 RVA: 0x0000A4C1 File Offset: 0x000086C1
		public uint PlayerIndex
		{
			set
			{
				this.m_PlayerIndex = value;
			}
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x0000A4CB File Offset: 0x000086CB
		public void Set(ref ActiveSessionGetRegisteredPlayerByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.PlayerIndex = other.PlayerIndex;
		}

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000A4E4 File Offset: 0x000086E4
		public void Set(ref ActiveSessionGetRegisteredPlayerByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PlayerIndex = other.Value.PlayerIndex;
			}
		}

		// Token: 0x060006D7 RID: 1751 RVA: 0x0000A51A File Offset: 0x0000871A
		public void Dispose()
		{
		}

		// Token: 0x04000349 RID: 841
		private int m_ApiVersion;

		// Token: 0x0400034A RID: 842
		private uint m_PlayerIndex;
	}
}
