using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Stats
{
	// Token: 0x020000A6 RID: 166
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyStatByIndexOptionsInternal : ISettable<CopyStatByIndexOptions>, IDisposable
	{
		// Token: 0x17000114 RID: 276
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x000094DB File Offset: 0x000076DB
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000115 RID: 277
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x000094EB File Offset: 0x000076EB
		public uint StatIndex
		{
			set
			{
				this.m_StatIndex = value;
			}
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000094F5 File Offset: 0x000076F5
		public void Set(ref CopyStatByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.TargetUserId = other.TargetUserId;
			this.StatIndex = other.StatIndex;
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x0000951C File Offset: 0x0000771C
		public void Set(ref CopyStatByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.TargetUserId = other.Value.TargetUserId;
				this.StatIndex = other.Value.StatIndex;
			}
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00009567 File Offset: 0x00007767
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x040002FB RID: 763
		private int m_ApiVersion;

		// Token: 0x040002FC RID: 764
		private IntPtr m_TargetUserId;

		// Token: 0x040002FD RID: 765
		private uint m_StatIndex;
	}
}
