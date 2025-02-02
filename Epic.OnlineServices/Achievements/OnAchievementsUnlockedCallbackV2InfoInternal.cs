using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000687 RID: 1671
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnAchievementsUnlockedCallbackV2InfoInternal : ICallbackInfoInternal, IGettable<OnAchievementsUnlockedCallbackV2Info>, ISettable<OnAchievementsUnlockedCallbackV2Info>, IDisposable
	{
		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x000403A4 File Offset: 0x0003E5A4
		// (set) Token: 0x06002ADB RID: 10971 RVA: 0x000403C5 File Offset: 0x0003E5C5
		public object ClientData
		{
			get
			{
				object result;
				Helper.Get(this.m_ClientData, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ClientData);
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06002ADC RID: 10972 RVA: 0x000403D8 File Offset: 0x0003E5D8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000CF9 RID: 3321
		// (get) Token: 0x06002ADD RID: 10973 RVA: 0x000403F0 File Offset: 0x0003E5F0
		// (set) Token: 0x06002ADE RID: 10974 RVA: 0x00040411 File Offset: 0x0003E611
		public ProductUserId UserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_UserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_UserId);
			}
		}

		// Token: 0x17000CFA RID: 3322
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x00040424 File Offset: 0x0003E624
		// (set) Token: 0x06002AE0 RID: 10976 RVA: 0x00040445 File Offset: 0x0003E645
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

		// Token: 0x17000CFB RID: 3323
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x00040458 File Offset: 0x0003E658
		// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x00040479 File Offset: 0x0003E679
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

		// Token: 0x06002AE3 RID: 10979 RVA: 0x00040489 File Offset: 0x0003E689
		public void Set(ref OnAchievementsUnlockedCallbackV2Info other)
		{
			this.ClientData = other.ClientData;
			this.UserId = other.UserId;
			this.AchievementId = other.AchievementId;
			this.UnlockTime = other.UnlockTime;
		}

		// Token: 0x06002AE4 RID: 10980 RVA: 0x000404C0 File Offset: 0x0003E6C0
		public void Set(ref OnAchievementsUnlockedCallbackV2Info? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.UserId = other.Value.UserId;
				this.AchievementId = other.Value.AchievementId;
				this.UnlockTime = other.Value.UnlockTime;
			}
		}

		// Token: 0x06002AE5 RID: 10981 RVA: 0x0004052E File Offset: 0x0003E72E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_UserId);
			Helper.Dispose(ref this.m_AchievementId);
		}

		// Token: 0x06002AE6 RID: 10982 RVA: 0x00040555 File Offset: 0x0003E755
		public void Get(out OnAchievementsUnlockedCallbackV2Info output)
		{
			output = default(OnAchievementsUnlockedCallbackV2Info);
			output.Set(ref this);
		}

		// Token: 0x04001383 RID: 4995
		private IntPtr m_ClientData;

		// Token: 0x04001384 RID: 4996
		private IntPtr m_UserId;

		// Token: 0x04001385 RID: 4997
		private IntPtr m_AchievementId;

		// Token: 0x04001386 RID: 4998
		private long m_UnlockTime;
	}
}
