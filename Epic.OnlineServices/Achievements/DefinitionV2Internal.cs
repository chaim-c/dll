using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000679 RID: 1657
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DefinitionV2Internal : IGettable<DefinitionV2>, ISettable<DefinitionV2>, IDisposable
	{
		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x06002A86 RID: 10886 RVA: 0x0003FBB4 File Offset: 0x0003DDB4
		// (set) Token: 0x06002A87 RID: 10887 RVA: 0x0003FBD5 File Offset: 0x0003DDD5
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

		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x0003FBE8 File Offset: 0x0003DDE8
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x0003FC09 File Offset: 0x0003DE09
		public Utf8String UnlockedDisplayName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_UnlockedDisplayName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UnlockedDisplayName);
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x0003FC1C File Offset: 0x0003DE1C
		// (set) Token: 0x06002A8B RID: 10891 RVA: 0x0003FC3D File Offset: 0x0003DE3D
		public Utf8String UnlockedDescription
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_UnlockedDescription, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UnlockedDescription);
			}
		}

		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x0003FC50 File Offset: 0x0003DE50
		// (set) Token: 0x06002A8D RID: 10893 RVA: 0x0003FC71 File Offset: 0x0003DE71
		public Utf8String LockedDisplayName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_LockedDisplayName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LockedDisplayName);
			}
		}

		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06002A8E RID: 10894 RVA: 0x0003FC84 File Offset: 0x0003DE84
		// (set) Token: 0x06002A8F RID: 10895 RVA: 0x0003FCA5 File Offset: 0x0003DEA5
		public Utf8String LockedDescription
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_LockedDescription, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LockedDescription);
			}
		}

		// Token: 0x17000CE3 RID: 3299
		// (get) Token: 0x06002A90 RID: 10896 RVA: 0x0003FCB8 File Offset: 0x0003DEB8
		// (set) Token: 0x06002A91 RID: 10897 RVA: 0x0003FCD9 File Offset: 0x0003DED9
		public Utf8String FlavorText
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_FlavorText, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_FlavorText);
			}
		}

		// Token: 0x17000CE4 RID: 3300
		// (get) Token: 0x06002A92 RID: 10898 RVA: 0x0003FCEC File Offset: 0x0003DEEC
		// (set) Token: 0x06002A93 RID: 10899 RVA: 0x0003FD0D File Offset: 0x0003DF0D
		public Utf8String UnlockedIconURL
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_UnlockedIconURL, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UnlockedIconURL);
			}
		}

		// Token: 0x17000CE5 RID: 3301
		// (get) Token: 0x06002A94 RID: 10900 RVA: 0x0003FD20 File Offset: 0x0003DF20
		// (set) Token: 0x06002A95 RID: 10901 RVA: 0x0003FD41 File Offset: 0x0003DF41
		public Utf8String LockedIconURL
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_LockedIconURL, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LockedIconURL);
			}
		}

		// Token: 0x17000CE6 RID: 3302
		// (get) Token: 0x06002A96 RID: 10902 RVA: 0x0003FD54 File Offset: 0x0003DF54
		// (set) Token: 0x06002A97 RID: 10903 RVA: 0x0003FD75 File Offset: 0x0003DF75
		public bool IsHidden
		{
			get
			{
				bool result;
				Helper.Get(this.m_IsHidden, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IsHidden);
			}
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06002A98 RID: 10904 RVA: 0x0003FD88 File Offset: 0x0003DF88
		// (set) Token: 0x06002A99 RID: 10905 RVA: 0x0003FDAF File Offset: 0x0003DFAF
		public StatThresholds[] StatThresholds
		{
			get
			{
				StatThresholds[] result;
				Helper.Get<StatThresholdsInternal, StatThresholds>(this.m_StatThresholds, out result, this.m_StatThresholdsCount);
				return result;
			}
			set
			{
				Helper.Set<StatThresholds, StatThresholdsInternal>(ref value, ref this.m_StatThresholds, out this.m_StatThresholdsCount);
			}
		}

		// Token: 0x06002A9A RID: 10906 RVA: 0x0003FDC8 File Offset: 0x0003DFC8
		public void Set(ref DefinitionV2 other)
		{
			this.m_ApiVersion = 2;
			this.AchievementId = other.AchievementId;
			this.UnlockedDisplayName = other.UnlockedDisplayName;
			this.UnlockedDescription = other.UnlockedDescription;
			this.LockedDisplayName = other.LockedDisplayName;
			this.LockedDescription = other.LockedDescription;
			this.FlavorText = other.FlavorText;
			this.UnlockedIconURL = other.UnlockedIconURL;
			this.LockedIconURL = other.LockedIconURL;
			this.IsHidden = other.IsHidden;
			this.StatThresholds = other.StatThresholds;
		}

		// Token: 0x06002A9B RID: 10907 RVA: 0x0003FE60 File Offset: 0x0003E060
		public void Set(ref DefinitionV2? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.AchievementId = other.Value.AchievementId;
				this.UnlockedDisplayName = other.Value.UnlockedDisplayName;
				this.UnlockedDescription = other.Value.UnlockedDescription;
				this.LockedDisplayName = other.Value.LockedDisplayName;
				this.LockedDescription = other.Value.LockedDescription;
				this.FlavorText = other.Value.FlavorText;
				this.UnlockedIconURL = other.Value.UnlockedIconURL;
				this.LockedIconURL = other.Value.LockedIconURL;
				this.IsHidden = other.Value.IsHidden;
				this.StatThresholds = other.Value.StatThresholds;
			}
		}

		// Token: 0x06002A9C RID: 10908 RVA: 0x0003FF58 File Offset: 0x0003E158
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AchievementId);
			Helper.Dispose(ref this.m_UnlockedDisplayName);
			Helper.Dispose(ref this.m_UnlockedDescription);
			Helper.Dispose(ref this.m_LockedDisplayName);
			Helper.Dispose(ref this.m_LockedDescription);
			Helper.Dispose(ref this.m_FlavorText);
			Helper.Dispose(ref this.m_UnlockedIconURL);
			Helper.Dispose(ref this.m_LockedIconURL);
			Helper.Dispose(ref this.m_StatThresholds);
		}

		// Token: 0x06002A9D RID: 10909 RVA: 0x0003FFD2 File Offset: 0x0003E1D2
		public void Get(out DefinitionV2 output)
		{
			output = default(DefinitionV2);
			output.Set(ref this);
		}

		// Token: 0x04001365 RID: 4965
		private int m_ApiVersion;

		// Token: 0x04001366 RID: 4966
		private IntPtr m_AchievementId;

		// Token: 0x04001367 RID: 4967
		private IntPtr m_UnlockedDisplayName;

		// Token: 0x04001368 RID: 4968
		private IntPtr m_UnlockedDescription;

		// Token: 0x04001369 RID: 4969
		private IntPtr m_LockedDisplayName;

		// Token: 0x0400136A RID: 4970
		private IntPtr m_LockedDescription;

		// Token: 0x0400136B RID: 4971
		private IntPtr m_FlavorText;

		// Token: 0x0400136C RID: 4972
		private IntPtr m_UnlockedIconURL;

		// Token: 0x0400136D RID: 4973
		private IntPtr m_LockedIconURL;

		// Token: 0x0400136E RID: 4974
		private int m_IsHidden;

		// Token: 0x0400136F RID: 4975
		private uint m_StatThresholdsCount;

		// Token: 0x04001370 RID: 4976
		private IntPtr m_StatThresholds;
	}
}
