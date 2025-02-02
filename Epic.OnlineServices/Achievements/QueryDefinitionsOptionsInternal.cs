using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000699 RID: 1689
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryDefinitionsOptionsInternal : ISettable<QueryDefinitionsOptions>, IDisposable
	{
		// Token: 0x17000D2A RID: 3370
		// (set) Token: 0x06002B74 RID: 11124 RVA: 0x000410D0 File Offset: 0x0003F2D0
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000D2B RID: 3371
		// (set) Token: 0x06002B75 RID: 11125 RVA: 0x000410E0 File Offset: 0x0003F2E0
		public EpicAccountId EpicUserId_DEPRECATED
		{
			set
			{
				Helper.Set(value, ref this.m_EpicUserId_DEPRECATED);
			}
		}

		// Token: 0x17000D2C RID: 3372
		// (set) Token: 0x06002B76 RID: 11126 RVA: 0x000410F0 File Offset: 0x0003F2F0
		public Utf8String[] HiddenAchievementIds_DEPRECATED
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_HiddenAchievementIds_DEPRECATED, true, out this.m_HiddenAchievementsCount_DEPRECATED);
			}
		}

		// Token: 0x06002B77 RID: 11127 RVA: 0x00041107 File Offset: 0x0003F307
		public void Set(ref QueryDefinitionsOptions other)
		{
			this.m_ApiVersion = 3;
			this.LocalUserId = other.LocalUserId;
			this.EpicUserId_DEPRECATED = other.EpicUserId_DEPRECATED;
			this.HiddenAchievementIds_DEPRECATED = other.HiddenAchievementIds_DEPRECATED;
		}

		// Token: 0x06002B78 RID: 11128 RVA: 0x00041138 File Offset: 0x0003F338
		public void Set(ref QueryDefinitionsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 3;
				this.LocalUserId = other.Value.LocalUserId;
				this.EpicUserId_DEPRECATED = other.Value.EpicUserId_DEPRECATED;
				this.HiddenAchievementIds_DEPRECATED = other.Value.HiddenAchievementIds_DEPRECATED;
			}
		}

		// Token: 0x06002B79 RID: 11129 RVA: 0x00041198 File Offset: 0x0003F398
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_EpicUserId_DEPRECATED);
			Helper.Dispose(ref this.m_HiddenAchievementIds_DEPRECATED);
		}

		// Token: 0x040013B5 RID: 5045
		private int m_ApiVersion;

		// Token: 0x040013B6 RID: 5046
		private IntPtr m_LocalUserId;

		// Token: 0x040013B7 RID: 5047
		private IntPtr m_EpicUserId_DEPRECATED;

		// Token: 0x040013B8 RID: 5048
		private IntPtr m_HiddenAchievementIds_DEPRECATED;

		// Token: 0x040013B9 RID: 5049
		private uint m_HiddenAchievementsCount_DEPRECATED;
	}
}
