using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x0200004C RID: 76
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFriendsVisibleOptionsInternal : ISettable<GetFriendsVisibleOptions>, IDisposable
	{
		// Token: 0x17000076 RID: 118
		// (set) Token: 0x0600040E RID: 1038 RVA: 0x0000643E File Offset: 0x0000463E
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000644E File Offset: 0x0000464E
		public void Set(ref GetFriendsVisibleOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x00006468 File Offset: 0x00004668
		public void Set(ref GetFriendsVisibleOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000649E File Offset: 0x0000469E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040001B6 RID: 438
		private int m_ApiVersion;

		// Token: 0x040001B7 RID: 439
		private IntPtr m_LocalUserId;
	}
}
