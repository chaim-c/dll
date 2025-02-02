using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D4 RID: 212
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopySessionHandleByUiEventIdOptionsInternal : ISettable<CopySessionHandleByUiEventIdOptions>, IDisposable
	{
		// Token: 0x17000166 RID: 358
		// (set) Token: 0x06000730 RID: 1840 RVA: 0x0000AE7E File Offset: 0x0000907E
		public ulong UiEventId
		{
			set
			{
				this.m_UiEventId = value;
			}
		}

		// Token: 0x06000731 RID: 1841 RVA: 0x0000AE88 File Offset: 0x00009088
		public void Set(ref CopySessionHandleByUiEventIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.UiEventId = other.UiEventId;
		}

		// Token: 0x06000732 RID: 1842 RVA: 0x0000AEA0 File Offset: 0x000090A0
		public void Set(ref CopySessionHandleByUiEventIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UiEventId = other.Value.UiEventId;
			}
		}

		// Token: 0x06000733 RID: 1843 RVA: 0x0000AED6 File Offset: 0x000090D6
		public void Dispose()
		{
		}

		// Token: 0x0400036E RID: 878
		private int m_ApiVersion;

		// Token: 0x0400036F RID: 879
		private ulong m_UiEventId;
	}
}
