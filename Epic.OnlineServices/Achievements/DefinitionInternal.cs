using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000677 RID: 1655
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DefinitionInternal : IGettable<Definition>, ISettable<Definition>, IDisposable
	{
		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06002A57 RID: 10839 RVA: 0x0003F5E8 File Offset: 0x0003D7E8
		// (set) Token: 0x06002A58 RID: 10840 RVA: 0x0003F609 File Offset: 0x0003D809
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

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06002A59 RID: 10841 RVA: 0x0003F61C File Offset: 0x0003D81C
		// (set) Token: 0x06002A5A RID: 10842 RVA: 0x0003F63D File Offset: 0x0003D83D
		public Utf8String DisplayName
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_DisplayName, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_DisplayName);
			}
		}

		// Token: 0x17000CCB RID: 3275
		// (get) Token: 0x06002A5B RID: 10843 RVA: 0x0003F650 File Offset: 0x0003D850
		// (set) Token: 0x06002A5C RID: 10844 RVA: 0x0003F671 File Offset: 0x0003D871
		public Utf8String Description
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Description, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Description);
			}
		}

		// Token: 0x17000CCC RID: 3276
		// (get) Token: 0x06002A5D RID: 10845 RVA: 0x0003F684 File Offset: 0x0003D884
		// (set) Token: 0x06002A5E RID: 10846 RVA: 0x0003F6A5 File Offset: 0x0003D8A5
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

		// Token: 0x17000CCD RID: 3277
		// (get) Token: 0x06002A5F RID: 10847 RVA: 0x0003F6B8 File Offset: 0x0003D8B8
		// (set) Token: 0x06002A60 RID: 10848 RVA: 0x0003F6D9 File Offset: 0x0003D8D9
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

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06002A61 RID: 10849 RVA: 0x0003F6EC File Offset: 0x0003D8EC
		// (set) Token: 0x06002A62 RID: 10850 RVA: 0x0003F70D File Offset: 0x0003D90D
		public Utf8String HiddenDescription
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_HiddenDescription, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_HiddenDescription);
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06002A63 RID: 10851 RVA: 0x0003F720 File Offset: 0x0003D920
		// (set) Token: 0x06002A64 RID: 10852 RVA: 0x0003F741 File Offset: 0x0003D941
		public Utf8String CompletionDescription
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_CompletionDescription, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_CompletionDescription);
			}
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06002A65 RID: 10853 RVA: 0x0003F754 File Offset: 0x0003D954
		// (set) Token: 0x06002A66 RID: 10854 RVA: 0x0003F775 File Offset: 0x0003D975
		public Utf8String UnlockedIconId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_UnlockedIconId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UnlockedIconId);
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06002A67 RID: 10855 RVA: 0x0003F788 File Offset: 0x0003D988
		// (set) Token: 0x06002A68 RID: 10856 RVA: 0x0003F7A9 File Offset: 0x0003D9A9
		public Utf8String LockedIconId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_LockedIconId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LockedIconId);
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06002A69 RID: 10857 RVA: 0x0003F7BC File Offset: 0x0003D9BC
		// (set) Token: 0x06002A6A RID: 10858 RVA: 0x0003F7DD File Offset: 0x0003D9DD
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

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06002A6B RID: 10859 RVA: 0x0003F7F0 File Offset: 0x0003D9F0
		// (set) Token: 0x06002A6C RID: 10860 RVA: 0x0003F817 File Offset: 0x0003DA17
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

		// Token: 0x06002A6D RID: 10861 RVA: 0x0003F830 File Offset: 0x0003DA30
		public void Set(ref Definition other)
		{
			this.m_ApiVersion = 1;
			this.AchievementId = other.AchievementId;
			this.DisplayName = other.DisplayName;
			this.Description = other.Description;
			this.LockedDisplayName = other.LockedDisplayName;
			this.LockedDescription = other.LockedDescription;
			this.HiddenDescription = other.HiddenDescription;
			this.CompletionDescription = other.CompletionDescription;
			this.UnlockedIconId = other.UnlockedIconId;
			this.LockedIconId = other.LockedIconId;
			this.IsHidden = other.IsHidden;
			this.StatThresholds = other.StatThresholds;
		}

		// Token: 0x06002A6E RID: 10862 RVA: 0x0003F8D4 File Offset: 0x0003DAD4
		public void Set(ref Definition? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AchievementId = other.Value.AchievementId;
				this.DisplayName = other.Value.DisplayName;
				this.Description = other.Value.Description;
				this.LockedDisplayName = other.Value.LockedDisplayName;
				this.LockedDescription = other.Value.LockedDescription;
				this.HiddenDescription = other.Value.HiddenDescription;
				this.CompletionDescription = other.Value.CompletionDescription;
				this.UnlockedIconId = other.Value.UnlockedIconId;
				this.LockedIconId = other.Value.LockedIconId;
				this.IsHidden = other.Value.IsHidden;
				this.StatThresholds = other.Value.StatThresholds;
			}
		}

		// Token: 0x06002A6F RID: 10863 RVA: 0x0003F9E0 File Offset: 0x0003DBE0
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AchievementId);
			Helper.Dispose(ref this.m_DisplayName);
			Helper.Dispose(ref this.m_Description);
			Helper.Dispose(ref this.m_LockedDisplayName);
			Helper.Dispose(ref this.m_LockedDescription);
			Helper.Dispose(ref this.m_HiddenDescription);
			Helper.Dispose(ref this.m_CompletionDescription);
			Helper.Dispose(ref this.m_UnlockedIconId);
			Helper.Dispose(ref this.m_LockedIconId);
			Helper.Dispose(ref this.m_StatThresholds);
		}

		// Token: 0x06002A70 RID: 10864 RVA: 0x0003FA66 File Offset: 0x0003DC66
		public void Get(out Definition output)
		{
			output = default(Definition);
			output.Set(ref this);
		}

		// Token: 0x0400134E RID: 4942
		private int m_ApiVersion;

		// Token: 0x0400134F RID: 4943
		private IntPtr m_AchievementId;

		// Token: 0x04001350 RID: 4944
		private IntPtr m_DisplayName;

		// Token: 0x04001351 RID: 4945
		private IntPtr m_Description;

		// Token: 0x04001352 RID: 4946
		private IntPtr m_LockedDisplayName;

		// Token: 0x04001353 RID: 4947
		private IntPtr m_LockedDescription;

		// Token: 0x04001354 RID: 4948
		private IntPtr m_HiddenDescription;

		// Token: 0x04001355 RID: 4949
		private IntPtr m_CompletionDescription;

		// Token: 0x04001356 RID: 4950
		private IntPtr m_UnlockedIconId;

		// Token: 0x04001357 RID: 4951
		private IntPtr m_LockedIconId;

		// Token: 0x04001358 RID: 4952
		private int m_IsHidden;

		// Token: 0x04001359 RID: 4953
		private int m_StatThresholdsCount;

		// Token: 0x0400135A RID: 4954
		private IntPtr m_StatThresholds;
	}
}
