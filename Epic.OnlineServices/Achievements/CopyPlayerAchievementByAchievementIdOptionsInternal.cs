using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x0200066F RID: 1647
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyPlayerAchievementByAchievementIdOptionsInternal : ISettable<CopyPlayerAchievementByAchievementIdOptions>, IDisposable
	{
		// Token: 0x17000CAD RID: 3245
		// (set) Token: 0x06002A1C RID: 10780 RVA: 0x0003F108 File Offset: 0x0003D308
		public ProductUserId TargetUserId
		{
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000CAE RID: 3246
		// (set) Token: 0x06002A1D RID: 10781 RVA: 0x0003F118 File Offset: 0x0003D318
		public Utf8String AchievementId
		{
			set
			{
				Helper.Set(value, ref this.m_AchievementId);
			}
		}

		// Token: 0x17000CAF RID: 3247
		// (set) Token: 0x06002A1E RID: 10782 RVA: 0x0003F128 File Offset: 0x0003D328
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06002A1F RID: 10783 RVA: 0x0003F138 File Offset: 0x0003D338
		public void Set(ref CopyPlayerAchievementByAchievementIdOptions other)
		{
			this.m_ApiVersion = 2;
			this.TargetUserId = other.TargetUserId;
			this.AchievementId = other.AchievementId;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002A20 RID: 10784 RVA: 0x0003F16C File Offset: 0x0003D36C
		public void Set(ref CopyPlayerAchievementByAchievementIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.TargetUserId = other.Value.TargetUserId;
				this.AchievementId = other.Value.AchievementId;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002A21 RID: 10785 RVA: 0x0003F1CC File Offset: 0x0003D3CC
		public void Dispose()
		{
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_AchievementId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400132E RID: 4910
		private int m_ApiVersion;

		// Token: 0x0400132F RID: 4911
		private IntPtr m_TargetUserId;

		// Token: 0x04001330 RID: 4912
		private IntPtr m_AchievementId;

		// Token: 0x04001331 RID: 4913
		private IntPtr m_LocalUserId;
	}
}
