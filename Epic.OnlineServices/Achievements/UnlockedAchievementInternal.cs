using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x020006A1 RID: 1697
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnlockedAchievementInternal : IGettable<UnlockedAchievement>, ISettable<UnlockedAchievement>, IDisposable
	{
		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x06002B9E RID: 11166 RVA: 0x000414D0 File Offset: 0x0003F6D0
		// (set) Token: 0x06002B9F RID: 11167 RVA: 0x000414F1 File Offset: 0x0003F6F1
		public Utf8String AchievementId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_AchievementId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_AchievementId);
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x06002BA0 RID: 11168 RVA: 0x00041504 File Offset: 0x0003F704
		// (set) Token: 0x06002BA1 RID: 11169 RVA: 0x00041525 File Offset: 0x0003F725
		public DateTimeOffset? UnlockTime
		{
			get
			{
				DateTimeOffset? result;
				Helper.Get(this.m_UnlockTime, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UnlockTime);
			}
		}

		// Token: 0x06002BA2 RID: 11170 RVA: 0x00041535 File Offset: 0x0003F735
		public void Set(ref UnlockedAchievement other)
		{
			this.m_ApiVersion = 1;
			this.AchievementId = other.AchievementId;
			this.UnlockTime = other.UnlockTime;
		}

		// Token: 0x06002BA3 RID: 11171 RVA: 0x0004155C File Offset: 0x0003F75C
		public void Set(ref UnlockedAchievement? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AchievementId = other.Value.AchievementId;
				this.UnlockTime = other.Value.UnlockTime;
			}
		}

		// Token: 0x06002BA4 RID: 11172 RVA: 0x000415A7 File Offset: 0x0003F7A7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AchievementId);
		}

		// Token: 0x06002BA5 RID: 11173 RVA: 0x000415B6 File Offset: 0x0003F7B6
		public void Get(out UnlockedAchievement output)
		{
			output = default(UnlockedAchievement);
			output.Set(ref this);
		}

		// Token: 0x040013CC RID: 5068
		private int m_ApiVersion;

		// Token: 0x040013CD RID: 5069
		private IntPtr m_AchievementId;

		// Token: 0x040013CE RID: 5070
		private long m_UnlockTime;
	}
}
