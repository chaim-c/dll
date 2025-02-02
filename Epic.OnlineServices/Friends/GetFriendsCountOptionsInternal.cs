using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000469 RID: 1129
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFriendsCountOptionsInternal : ISettable<GetFriendsCountOptions>, IDisposable
	{
		// Token: 0x17000832 RID: 2098
		// (set) Token: 0x06001CDE RID: 7390 RVA: 0x0002ACA7 File Offset: 0x00028EA7
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001CDF RID: 7391 RVA: 0x0002ACB7 File Offset: 0x00028EB7
		public void Set(ref GetFriendsCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001CE0 RID: 7392 RVA: 0x0002ACD0 File Offset: 0x00028ED0
		public void Set(ref GetFriendsCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001CE1 RID: 7393 RVA: 0x0002AD06 File Offset: 0x00028F06
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000CD0 RID: 3280
		private int m_ApiVersion;

		// Token: 0x04000CD1 RID: 3281
		private IntPtr m_LocalUserId;
	}
}
