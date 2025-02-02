using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200066B RID: 1643
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyAchievementDefinitionV2ByAchievementIdOptionsInternal : ISettable<CopyAchievementDefinitionV2ByAchievementIdOptions>, IDisposable
	{
		// Token: 0x17000CA7 RID: 3239
		// (set) Token: 0x06002A0C RID: 10764 RVA: 0x0003EFFA File Offset: 0x0003D1FA
		public Utf8String AchievementId
		{
			set
			{
				Helper.Set(value, ref this.m_AchievementId);
			}
		}

		// Token: 0x06002A0D RID: 10765 RVA: 0x0003F00A File Offset: 0x0003D20A
		public void Set(ref CopyAchievementDefinitionV2ByAchievementIdOptions other)
		{
			this.m_ApiVersion = 2;
			this.AchievementId = other.AchievementId;
		}

		// Token: 0x06002A0E RID: 10766 RVA: 0x0003F024 File Offset: 0x0003D224
		public void Set(ref CopyAchievementDefinitionV2ByAchievementIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.AchievementId = other.Value.AchievementId;
			}
		}

		// Token: 0x06002A0F RID: 10767 RVA: 0x0003F05A File Offset: 0x0003D25A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AchievementId);
		}

		// Token: 0x04001326 RID: 4902
		private int m_ApiVersion;

		// Token: 0x04001327 RID: 4903
		private IntPtr m_AchievementId;
	}
}
