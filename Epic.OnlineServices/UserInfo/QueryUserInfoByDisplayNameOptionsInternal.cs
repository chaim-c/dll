using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UserInfo
{
	// Token: 0x02000039 RID: 57
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryUserInfoByDisplayNameOptionsInternal : ISettable<QueryUserInfoByDisplayNameOptions>, IDisposable
	{
		// Token: 0x17000041 RID: 65
		// (set) Token: 0x06000380 RID: 896 RVA: 0x000053EE File Offset: 0x000035EE
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000042 RID: 66
		// (set) Token: 0x06000381 RID: 897 RVA: 0x000053FE File Offset: 0x000035FE
		public Utf8String DisplayName
		{
			set
			{
				Helper.Set(value, ref this.m_DisplayName);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0000540E File Offset: 0x0000360E
		public void Set(ref QueryUserInfoByDisplayNameOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.DisplayName = other.DisplayName;
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00005434 File Offset: 0x00003634
		public void Set(ref QueryUserInfoByDisplayNameOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.DisplayName = other.Value.DisplayName;
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x0000547F File Offset: 0x0000367F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_DisplayName);
		}

		// Token: 0x04000171 RID: 369
		private int m_ApiVersion;

		// Token: 0x04000172 RID: 370
		private IntPtr m_LocalUserId;

		// Token: 0x04000173 RID: 371
		private IntPtr m_DisplayName;
	}
}
