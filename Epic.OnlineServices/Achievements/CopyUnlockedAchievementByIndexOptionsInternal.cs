using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000675 RID: 1653
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUnlockedAchievementByIndexOptionsInternal : ISettable<CopyUnlockedAchievementByIndexOptions>, IDisposable
	{
		// Token: 0x17000CBC RID: 3260
		// (set) Token: 0x06002A3B RID: 10811 RVA: 0x0003F3F0 File Offset: 0x0003D5F0
		public ProductUserId UserId
		{
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x17000CBD RID: 3261
		// (set) Token: 0x06002A3C RID: 10812 RVA: 0x0003F400 File Offset: 0x0003D600
		public uint AchievementIndex
		{
			set
			{
				this.m_AchievementIndex = value;
			}
		}

		// Token: 0x06002A3D RID: 10813 RVA: 0x0003F40A File Offset: 0x0003D60A
		public void Set(ref CopyUnlockedAchievementByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.UserId = other.UserId;
			this.AchievementIndex = other.AchievementIndex;
		}

		// Token: 0x06002A3E RID: 10814 RVA: 0x0003F430 File Offset: 0x0003D630
		public void Set(ref CopyUnlockedAchievementByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.Value.UserId;
				this.AchievementIndex = other.Value.AchievementIndex;
			}
		}

		// Token: 0x06002A3F RID: 10815 RVA: 0x0003F47B File Offset: 0x0003D67B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
		}

		// Token: 0x04001340 RID: 4928
		private int m_ApiVersion;

		// Token: 0x04001341 RID: 4929
		private IntPtr m_UserId;

		// Token: 0x04001342 RID: 4930
		private uint m_AchievementIndex;
	}
}
