using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200069F RID: 1695
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlockAchievementsOptionsInternal : ISettable<UnlockAchievementsOptions>, IDisposable
	{
		// Token: 0x17000D37 RID: 3383
		// (set) Token: 0x06002B94 RID: 11156 RVA: 0x000413DA File Offset: 0x0003F5DA
		public ProductUserId UserId
		{
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (set) Token: 0x06002B95 RID: 11157 RVA: 0x000413EA File Offset: 0x0003F5EA
		public Utf8String[] AchievementIds
		{
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_AchievementIds, true, out this.m_AchievementsCount);
			}
		}

		// Token: 0x06002B96 RID: 11158 RVA: 0x00041401 File Offset: 0x0003F601
		public void Set(ref UnlockAchievementsOptions other)
		{
			this.m_ApiVersion = 1;
			this.UserId = other.UserId;
			this.AchievementIds = other.AchievementIds;
		}

		// Token: 0x06002B97 RID: 11159 RVA: 0x00041428 File Offset: 0x0003F628
		public void Set(ref UnlockAchievementsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.Value.UserId;
				this.AchievementIds = other.Value.AchievementIds;
			}
		}

		// Token: 0x06002B98 RID: 11160 RVA: 0x00041473 File Offset: 0x0003F673
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
			Helper.Dispose(ref this.m_AchievementIds);
		}

		// Token: 0x040013C6 RID: 5062
		private int m_ApiVersion;

		// Token: 0x040013C7 RID: 5063
		private IntPtr m_UserId;

		// Token: 0x040013C8 RID: 5064
		private IntPtr m_AchievementIds;

		// Token: 0x040013C9 RID: 5065
		private uint m_AchievementsCount;
	}
}
