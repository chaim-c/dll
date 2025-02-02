using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000041 RID: 65
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoOptionsInternal : ISettable<QueryUserInfoOptions>, IDisposable
	{
		// Token: 0x17000061 RID: 97
		// (set) Token: 0x060003CB RID: 971 RVA: 0x00005B61 File Offset: 0x00003D61
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000062 RID: 98
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00005B71 File Offset: 0x00003D71
		public EpicAccountId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00005B81 File Offset: 0x00003D81
		public void Set(ref QueryUserInfoOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x060003CE RID: 974 RVA: 0x00005BA8 File Offset: 0x00003DA8
		public void Set(ref QueryUserInfoOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x060003CF RID: 975 RVA: 0x00005BF3 File Offset: 0x00003DF3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x04000191 RID: 401
		private int m_ApiVersion;

		// Token: 0x04000192 RID: 402
		private IntPtr m_LocalUserId;

		// Token: 0x04000193 RID: 403
		private IntPtr m_TargetUserId;
	}
}
