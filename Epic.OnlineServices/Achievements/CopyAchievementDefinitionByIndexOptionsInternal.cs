using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000669 RID: 1641
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionByIndexOptionsInternal : ISettable<CopyAchievementDefinitionByIndexOptions>, IDisposable
	{
		// Token: 0x17000CA5 RID: 3237
		// (set) Token: 0x06002A06 RID: 10758 RVA: 0x0003EF8E File Offset: 0x0003D18E
		public uint AchievementIndex
		{
			set
			{
				this.m_AchievementIndex = value;
			}
		}

		// Token: 0x06002A07 RID: 10759 RVA: 0x0003EF98 File Offset: 0x0003D198
		public void Set(ref CopyAchievementDefinitionByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.AchievementIndex = other.AchievementIndex;
		}

		// Token: 0x06002A08 RID: 10760 RVA: 0x0003EFB0 File Offset: 0x0003D1B0
		public void Set(ref CopyAchievementDefinitionByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AchievementIndex = other.Value.AchievementIndex;
			}
		}

		// Token: 0x06002A09 RID: 10761 RVA: 0x0003EFE6 File Offset: 0x0003D1E6
		public void Dispose()
		{
		}

		// Token: 0x04001323 RID: 4899
		private int m_ApiVersion;

		// Token: 0x04001324 RID: 4900
		private uint m_AchievementIndex;
	}
}
