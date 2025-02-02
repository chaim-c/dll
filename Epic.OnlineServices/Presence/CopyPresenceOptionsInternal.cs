using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000231 RID: 561
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPresenceOptionsInternal : ISettable<CopyPresenceOptions>, IDisposable
	{
		// Token: 0x17000408 RID: 1032
		// (set) Token: 0x06000F7F RID: 3967 RVA: 0x00016E7A File Offset: 0x0001507A
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000409 RID: 1033
		// (set) Token: 0x06000F80 RID: 3968 RVA: 0x00016E8A File Offset: 0x0001508A
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06000F81 RID: 3969 RVA: 0x00016E9A File Offset: 0x0001509A
		public void Set(ref CopyPresenceOptions other)
		{
			this.m_ApiVersion = 3;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000F82 RID: 3970 RVA: 0x00016EC0 File Offset: 0x000150C0
		public void Set(ref CopyPresenceOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06000F83 RID: 3971 RVA: 0x00016F0B File Offset: 0x0001510B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x040006EE RID: 1774
		private int m_ApiVersion;

		// Token: 0x040006EF RID: 1775
		private IntPtr m_LocalUserId;

		// Token: 0x040006F0 RID: 1776
		private IntPtr m_TargetUserId;
	}
}
