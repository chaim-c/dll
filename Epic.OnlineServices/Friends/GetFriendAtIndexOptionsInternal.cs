using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000467 RID: 1127
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFriendAtIndexOptionsInternal : ISettable<GetFriendAtIndexOptions>, IDisposable
	{
		// Token: 0x1700082F RID: 2095
		// (set) Token: 0x06001CD7 RID: 7383 RVA: 0x0002ABFD File Offset: 0x00028DFD
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000830 RID: 2096
		// (set) Token: 0x06001CD8 RID: 7384 RVA: 0x0002AC0D File Offset: 0x00028E0D
		public int Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06001CD9 RID: 7385 RVA: 0x0002AC17 File Offset: 0x00028E17
		public void Set(ref GetFriendAtIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Index = other.Index;
		}

		// Token: 0x06001CDA RID: 7386 RVA: 0x0002AC3C File Offset: 0x00028E3C
		public void Set(ref GetFriendAtIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Index = other.Value.Index;
			}
		}

		// Token: 0x06001CDB RID: 7387 RVA: 0x0002AC87 File Offset: 0x00028E87
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000CCC RID: 3276
		private int m_ApiVersion;

		// Token: 0x04000CCD RID: 3277
		private IntPtr m_LocalUserId;

		// Token: 0x04000CCE RID: 3278
		private int m_Index;
	}
}
