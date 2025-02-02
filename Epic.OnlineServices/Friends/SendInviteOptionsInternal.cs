using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000483 RID: 1155
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SendInviteOptionsInternal : ISettable<SendInviteOptions>, IDisposable
	{
		// Token: 0x17000863 RID: 2147
		// (set) Token: 0x06001D82 RID: 7554 RVA: 0x0002B885 File Offset: 0x00029A85
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000864 RID: 2148
		// (set) Token: 0x06001D83 RID: 7555 RVA: 0x0002B895 File Offset: 0x00029A95
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0002B8A5 File Offset: 0x00029AA5
		public void Set(ref SendInviteOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0002B8CC File Offset: 0x00029ACC
		public void Set(ref SendInviteOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0002B917 File Offset: 0x00029B17
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000D01 RID: 3329
		private int m_ApiVersion;

		// Token: 0x04000D02 RID: 3330
		private IntPtr m_LocalUserId;

		// Token: 0x04000D03 RID: 3331
		private IntPtr m_TargetUserId;
	}
}
