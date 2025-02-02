using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000667 RID: 1639
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionByAchievementIdOptionsInternal : ISettable<CopyAchievementDefinitionByAchievementIdOptions>, IDisposable
	{
		// Token: 0x17000CA3 RID: 3235
		// (set) Token: 0x06002A00 RID: 10752 RVA: 0x0003EF11 File Offset: 0x0003D111
		public Utf8String AchievementId
		{
			set
			{
				Helper.Set(value, ref this.m_AchievementId);
			}
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x0003EF21 File Offset: 0x0003D121
		public void Set(ref CopyAchievementDefinitionByAchievementIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.AchievementId = other.AchievementId;
		}

		// Token: 0x06002A02 RID: 10754 RVA: 0x0003EF38 File Offset: 0x0003D138
		public void Set(ref CopyAchievementDefinitionByAchievementIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AchievementId = other.Value.AchievementId;
			}
		}

		// Token: 0x06002A03 RID: 10755 RVA: 0x0003EF6E File Offset: 0x0003D16E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AchievementId);
		}

		// Token: 0x04001320 RID: 4896
		private int m_ApiVersion;

		// Token: 0x04001321 RID: 4897
		private IntPtr m_AchievementId;
	}
}
