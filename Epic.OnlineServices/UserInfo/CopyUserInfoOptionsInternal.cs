using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x0200002B RID: 43
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUserInfoOptionsInternal : ISettable<CopyUserInfoOptions>, IDisposable
	{
		// Token: 0x17000026 RID: 38
		// (set) Token: 0x06000326 RID: 806 RVA: 0x00004D41 File Offset: 0x00002F41
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000027 RID: 39
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00004D51 File Offset: 0x00002F51
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00004D61 File Offset: 0x00002F61
		public void Set(ref CopyUserInfoOptions other)
		{
			this.m_ApiVersion = 3;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00004D88 File Offset: 0x00002F88
		public void Set(ref CopyUserInfoOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00004DD3 File Offset: 0x00002FD3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000154 RID: 340
		private int m_ApiVersion;

		// Token: 0x04000155 RID: 341
		private IntPtr m_LocalUserId;

		// Token: 0x04000156 RID: 342
		private IntPtr m_TargetUserId;
	}
}
