using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D6 RID: 214
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopySessionHandleForPresenceOptionsInternal : ISettable<CopySessionHandleForPresenceOptions>, IDisposable
	{
		// Token: 0x17000168 RID: 360
		// (set) Token: 0x06000736 RID: 1846 RVA: 0x0000AEEA File Offset: 0x000090EA
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06000737 RID: 1847 RVA: 0x0000AEFA File Offset: 0x000090FA
		public void Set(ref CopySessionHandleForPresenceOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000738 RID: 1848 RVA: 0x0000AF14 File Offset: 0x00009114
		public void Set(ref CopySessionHandleForPresenceOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000739 RID: 1849 RVA: 0x0000AF4A File Offset: 0x0000914A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000371 RID: 881
		private int m_ApiVersion;

		// Token: 0x04000372 RID: 882
		private IntPtr m_LocalUserId;
	}
}
