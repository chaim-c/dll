using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.UI
{
	// Token: 0x02000052 RID: 82
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct HideFriendsOptionsInternal : ISettable<HideFriendsOptions>, IDisposable
	{
		// Token: 0x1700007F RID: 127
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x000066BB File Offset: 0x000048BB
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000066CB File Offset: 0x000048CB
		public void Set(ref HideFriendsOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000066E4 File Offset: 0x000048E4
		public void Set(ref HideFriendsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x0000671A File Offset: 0x0000491A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040001C0 RID: 448
		private int m_ApiVersion;

		// Token: 0x040001C1 RID: 449
		private IntPtr m_LocalUserId;
	}
}
