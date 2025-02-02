using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000076 RID: 118
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct ShowFriendsOptionsInternal : ISettable<ShowFriendsOptions>, IDisposable
	{
		// Token: 0x170000AF RID: 175
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x00007327 File Offset: 0x00005527
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x00007337 File Offset: 0x00005537
		public void Set(ref ShowFriendsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00007350 File Offset: 0x00005550
		public void Set(ref ShowFriendsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060004DF RID: 1247 RVA: 0x00007386 File Offset: 0x00005586
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000269 RID: 617
		private int m_ApiVersion;

		// Token: 0x0400026A RID: 618
		private IntPtr m_LocalUserId;
	}
}
