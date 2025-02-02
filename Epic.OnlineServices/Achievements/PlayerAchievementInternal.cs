using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000695 RID: 1685
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct PlayerAchievementInternal : IGettable<PlayerAchievement>, ISettable<PlayerAchievement>, IDisposable
	{
		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x06002B49 RID: 11081 RVA: 0x00040BC0 File Offset: 0x0003EDC0
		// (set) Token: 0x06002B4A RID: 11082 RVA: 0x00040BE1 File Offset: 0x0003EDE1
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

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06002B4B RID: 11083 RVA: 0x00040BF4 File Offset: 0x0003EDF4
		// (set) Token: 0x06002B4C RID: 11084 RVA: 0x00040C0C File Offset: 0x0003EE0C
		public double Progress
		{
			get
			{
				return this.m_Progress;
			}
			set
			{
				this.m_Progress = value;
			}
		}

		// Token: 0x17000D1B RID: 3355
		// (get) Token: 0x06002B4D RID: 11085 RVA: 0x00040C18 File Offset: 0x0003EE18
		// (set) Token: 0x06002B4E RID: 11086 RVA: 0x00040C39 File Offset: 0x0003EE39
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

		// Token: 0x17000D1C RID: 3356
		// (get) Token: 0x06002B4F RID: 11087 RVA: 0x00040C4C File Offset: 0x0003EE4C
		// (set) Token: 0x06002B50 RID: 11088 RVA: 0x00040C73 File Offset: 0x0003EE73
		public PlayerStatInfo[] StatInfo
		{
			get
			{
				PlayerStatInfo[] result;
				Helper.Get<PlayerStatInfoInternal, PlayerStatInfo>(this.m_StatInfo, out result, this.m_StatInfoCount);
				return result;
			}
			set
			{
				Helper.Set<PlayerStatInfo, PlayerStatInfoInternal>(ref value, ref this.m_StatInfo, out this.m_StatInfoCount);
			}
		}

		// Token: 0x17000D1D RID: 3357
		// (get) Token: 0x06002B51 RID: 11089 RVA: 0x00040C8C File Offset: 0x0003EE8C
		// (set) Token: 0x06002B52 RID: 11090 RVA: 0x00040CAD File Offset: 0x0003EEAD
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

		// Token: 0x17000D1E RID: 3358
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x00040CC0 File Offset: 0x0003EEC0
		// (set) Token: 0x06002B54 RID: 11092 RVA: 0x00040CE1 File Offset: 0x0003EEE1
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

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x06002B55 RID: 11093 RVA: 0x00040CF4 File Offset: 0x0003EEF4
		// (set) Token: 0x06002B56 RID: 11094 RVA: 0x00040D15 File Offset: 0x0003EF15
		public Utf8String IconURL
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_IconURL, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_IconURL);
			}
		}

		// Token: 0x17000D20 RID: 3360
		// (get) Token: 0x06002B57 RID: 11095 RVA: 0x00040D28 File Offset: 0x0003EF28
		// (set) Token: 0x06002B58 RID: 11096 RVA: 0x00040D49 File Offset: 0x0003EF49
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

		// Token: 0x06002B59 RID: 11097 RVA: 0x00040D5C File Offset: 0x0003EF5C
		public void Set(ref PlayerAchievement other)
		{
			this.m_ApiVersion = 2;
			this.AchievementId = other.AchievementId;
			this.Progress = other.Progress;
			this.UnlockTime = other.UnlockTime;
			this.StatInfo = other.StatInfo;
			this.DisplayName = other.DisplayName;
			this.Description = other.Description;
			this.IconURL = other.IconURL;
			this.FlavorText = other.FlavorText;
		}

		// Token: 0x06002B5A RID: 11098 RVA: 0x00040DDC File Offset: 0x0003EFDC
		public void Set(ref PlayerAchievement? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.AchievementId = other.Value.AchievementId;
				this.Progress = other.Value.Progress;
				this.UnlockTime = other.Value.UnlockTime;
				this.StatInfo = other.Value.StatInfo;
				this.DisplayName = other.Value.DisplayName;
				this.Description = other.Value.Description;
				this.IconURL = other.Value.IconURL;
				this.FlavorText = other.Value.FlavorText;
			}
		}

		// Token: 0x06002B5B RID: 11099 RVA: 0x00040EA8 File Offset: 0x0003F0A8
		public void Dispose()
		{
			Helper.Dispose(ref this.m_AchievementId);
			Helper.Dispose(ref this.m_StatInfo);
			Helper.Dispose(ref this.m_DisplayName);
			Helper.Dispose(ref this.m_Description);
			Helper.Dispose(ref this.m_IconURL);
			Helper.Dispose(ref this.m_FlavorText);
		}

		// Token: 0x06002B5C RID: 11100 RVA: 0x00040EFE File Offset: 0x0003F0FE
		public void Get(out PlayerAchievement output)
		{
			output = default(PlayerAchievement);
			output.Set(ref this);
		}

		// Token: 0x040013A1 RID: 5025
		private int m_ApiVersion;

		// Token: 0x040013A2 RID: 5026
		private IntPtr m_AchievementId;

		// Token: 0x040013A3 RID: 5027
		private double m_Progress;

		// Token: 0x040013A4 RID: 5028
		private long m_UnlockTime;

		// Token: 0x040013A5 RID: 5029
		private int m_StatInfoCount;

		// Token: 0x040013A6 RID: 5030
		private IntPtr m_StatInfo;

		// Token: 0x040013A7 RID: 5031
		private IntPtr m_DisplayName;

		// Token: 0x040013A8 RID: 5032
		private IntPtr m_Description;

		// Token: 0x040013A9 RID: 5033
		private IntPtr m_IconURL;

		// Token: 0x040013AA RID: 5034
		private IntPtr m_FlavorText;
	}
}
