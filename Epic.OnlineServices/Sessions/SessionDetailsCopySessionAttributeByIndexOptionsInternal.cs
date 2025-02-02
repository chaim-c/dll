using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000124 RID: 292
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionDetailsCopySessionAttributeByIndexOptionsInternal : ISettable<SessionDetailsCopySessionAttributeByIndexOptions>, IDisposable
	{
		// Token: 0x170001D6 RID: 470
		// (set) Token: 0x060008BE RID: 2238 RVA: 0x0000C9B1 File Offset: 0x0000ABB1
		public uint AttrIndex
		{
			set
			{
				this.m_AttrIndex = value;
			}
		}

		// Token: 0x060008BF RID: 2239 RVA: 0x0000C9BB File Offset: 0x0000ABBB
		public void Set(ref SessionDetailsCopySessionAttributeByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.AttrIndex = other.AttrIndex;
		}

		// Token: 0x060008C0 RID: 2240 RVA: 0x0000C9D4 File Offset: 0x0000ABD4
		public void Set(ref SessionDetailsCopySessionAttributeByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AttrIndex = other.Value.AttrIndex;
			}
		}

		// Token: 0x060008C1 RID: 2241 RVA: 0x0000CA0A File Offset: 0x0000AC0A
		public void Dispose()
		{
		}

		// Token: 0x04000401 RID: 1025
		private int m_ApiVersion;

		// Token: 0x04000402 RID: 1026
		private uint m_AttrIndex;
	}
}
