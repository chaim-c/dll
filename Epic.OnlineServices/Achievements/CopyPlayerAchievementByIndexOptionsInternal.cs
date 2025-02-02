using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000671 RID: 1649
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPlayerAchievementByIndexOptionsInternal : ISettable<CopyPlayerAchievementByIndexOptions>, IDisposable
	{
		// Token: 0x17000CB3 RID: 3251
		// (set) Token: 0x06002A28 RID: 10792 RVA: 0x0003F226 File Offset: 0x0003D426
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (set) Token: 0x06002A29 RID: 10793 RVA: 0x0003F236 File Offset: 0x0003D436
		public uint AchievementIndex
		{
			set
			{
				this.m_AchievementIndex = value;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x0003F240 File Offset: 0x0003D440
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06002A2B RID: 10795 RVA: 0x0003F250 File Offset: 0x0003D450
		public void Set(ref CopyPlayerAchievementByIndexOptions other)
		{
			this.m_ApiVersion = 2;
			this.TargetUserId = other.TargetUserId;
			this.AchievementIndex = other.AchievementIndex;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002A2C RID: 10796 RVA: 0x0003F284 File Offset: 0x0003D484
		public void Set(ref CopyPlayerAchievementByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.TargetUserId = other.Value.TargetUserId;
				this.AchievementIndex = other.Value.AchievementIndex;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002A2D RID: 10797 RVA: 0x0003F2E4 File Offset: 0x0003D4E4
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04001335 RID: 4917
		private int m_ApiVersion;

		// Token: 0x04001336 RID: 4918
		private IntPtr m_TargetUserId;

		// Token: 0x04001337 RID: 4919
		private uint m_AchievementIndex;

		// Token: 0x04001338 RID: 4920
		private IntPtr m_LocalUserId;
	}
}
