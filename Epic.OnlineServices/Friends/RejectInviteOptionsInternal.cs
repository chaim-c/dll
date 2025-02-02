using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200047F RID: 1151
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteOptionsInternal : ISettable<RejectInviteOptions>, IDisposable
	{
		// Token: 0x17000856 RID: 2134
		// (set) Token: 0x06001D62 RID: 7522 RVA: 0x0002B569 File Offset: 0x00029769
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000857 RID: 2135
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x0002B579 File Offset: 0x00029779
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0002B589 File Offset: 0x00029789
		public void Set(ref RejectInviteOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0002B5B0 File Offset: 0x000297B0
		public void Set(ref RejectInviteOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0002B5FB File Offset: 0x000297FB
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000CF4 RID: 3316
		private int m_ApiVersion;

		// Token: 0x04000CF5 RID: 3317
		private IntPtr m_LocalUserId;

		// Token: 0x04000CF6 RID: 3318
		private IntPtr m_TargetUserId;
	}
}
