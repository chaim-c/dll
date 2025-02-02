using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000673 RID: 1651
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUnlockedAchievementByAchievementIdOptionsInternal : ISettable<CopyUnlockedAchievementByAchievementIdOptions>, IDisposable
	{
		// Token: 0x17000CB8 RID: 3256
		// (set) Token: 0x06002A32 RID: 10802 RVA: 0x0003F321 File Offset: 0x0003D521
		public ProductUserId UserId
		{
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x17000CB9 RID: 3257
		// (set) Token: 0x06002A33 RID: 10803 RVA: 0x0003F331 File Offset: 0x0003D531
		public Utf8String AchievementId
		{
			set
			{
				Helper.Set(value, ref this.m_AchievementId);
			}
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x0003F341 File Offset: 0x0003D541
		public void Set(ref CopyUnlockedAchievementByAchievementIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.UserId = other.UserId;
			this.AchievementId = other.AchievementId;
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x0003F368 File Offset: 0x0003D568
		public void Set(ref CopyUnlockedAchievementByAchievementIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.UserId = other.Value.UserId;
				this.AchievementId = other.Value.AchievementId;
			}
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x0003F3B3 File Offset: 0x0003D5B3
		public void Dispose()
		{
			Helper.Dispose(ref this.m_UserId);
			Helper.Dispose(ref this.m_AchievementId);
		}

		// Token: 0x0400133B RID: 4923
		private int m_ApiVersion;

		// Token: 0x0400133C RID: 4924
		private IntPtr m_UserId;

		// Token: 0x0400133D RID: 4925
		private IntPtr m_AchievementId;
	}
}
