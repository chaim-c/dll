using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200067F RID: 1663
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetUnlockedAchievementCountOptionsInternal : ISettable<GetUnlockedAchievementCountOptions>, IDisposable
	{
		// Token: 0x17000CEB RID: 3307
		// (set) Token: 0x06002AA9 RID: 10921 RVA: 0x000400A2 File Offset: 0x0003E2A2
		public ProductUserId UserId
		{
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000400B2 File Offset: 0x0003E2B2
		public void Set(ref GetUnlockedAchievementCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.UserId = other.UserId;
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000400CC File Offset: 0x0003E2CC
		public void Set(ref GetUnlockedAchievementCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.Value.UserId;
			}
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x00040102 File Offset: 0x0003E302
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
		}

		// Token: 0x04001376 RID: 4982
		private int m_ApiVersion;

		// Token: 0x04001377 RID: 4983
		private IntPtr m_UserId;
	}
}
