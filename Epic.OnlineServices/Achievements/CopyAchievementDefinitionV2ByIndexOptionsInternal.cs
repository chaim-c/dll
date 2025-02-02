using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200066D RID: 1645
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionV2ByIndexOptionsInternal : ISettable<CopyAchievementDefinitionV2ByIndexOptions>, IDisposable
	{
		// Token: 0x17000CA9 RID: 3241
		// (set) Token: 0x06002A12 RID: 10770 RVA: 0x0003F07A File Offset: 0x0003D27A
		public uint AchievementIndex
		{
			set
			{
				this.m_AchievementIndex = value;
			}
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x0003F084 File Offset: 0x0003D284
		public void Set(ref CopyAchievementDefinitionV2ByIndexOptions other)
		{
			this.m_ApiVersion = 2;
			this.AchievementIndex = other.AchievementIndex;
		}

		// Token: 0x06002A14 RID: 10772 RVA: 0x0003F09C File Offset: 0x0003D29C
		public void Set(ref CopyAchievementDefinitionV2ByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.AchievementIndex = other.Value.AchievementIndex;
			}
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x0003F0D2 File Offset: 0x0003D2D2
		public void Dispose()
		{
		}

		// Token: 0x04001329 RID: 4905
		private int m_ApiVersion;

		// Token: 0x0400132A RID: 4906
		private uint m_AchievementIndex;
	}
}
