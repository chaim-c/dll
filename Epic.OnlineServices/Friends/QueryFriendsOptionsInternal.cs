using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200047B RID: 1147
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFriendsOptionsInternal : ISettable<QueryFriendsOptions>, IDisposable
	{
		// Token: 0x1700084A RID: 2122
		// (set) Token: 0x06001D43 RID: 7491 RVA: 0x0002B28B File Offset: 0x0002948B
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x0002B29B File Offset: 0x0002949B
		public void Set(ref QueryFriendsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x0002B2B4 File Offset: 0x000294B4
		public void Set(ref QueryFriendsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x0002B2EA File Offset: 0x000294EA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000CE8 RID: 3304
		private int m_ApiVersion;

		// Token: 0x04000CE9 RID: 3305
		private IntPtr m_LocalUserId;
	}
}
