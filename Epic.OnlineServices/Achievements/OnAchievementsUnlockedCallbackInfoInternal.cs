using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000683 RID: 1667
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnAchievementsUnlockedCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnAchievementsUnlockedCallbackInfo>, ISettable<OnAchievementsUnlockedCallbackInfo>, IDisposable
	{
		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x0004018C File Offset: 0x0003E38C
		// (set) Token: 0x06002ABE RID: 10942 RVA: 0x000401AD File Offset: 0x0003E3AD
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

		// Token: 0x17000CF0 RID: 3312
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x000401C0 File Offset: 0x0003E3C0
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000CF1 RID: 3313
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x000401D8 File Offset: 0x0003E3D8
		// (set) Token: 0x06002AC1 RID: 10945 RVA: 0x000401F9 File Offset: 0x0003E3F9
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

		// Token: 0x17000CF2 RID: 3314
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x0004020C File Offset: 0x0003E40C
		// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x00040234 File Offset: 0x0003E434
		public Utf8String[] AchievementIds
		{
			get
			{
				Utf8String[] result;
				Helper.Get<Utf8String>(this.m_AchievementIds, out result, this.m_AchievementsCount, true);
				return result;
			}
			set
			{
				Helper.Set<Utf8String>(value, ref this.m_AchievementIds, true, out this.m_AchievementsCount);
			}
		}

		// Token: 0x06002AC4 RID: 10948 RVA: 0x0004024B File Offset: 0x0003E44B
		public void Set(ref OnAchievementsUnlockedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.UserId = other.UserId;
			this.AchievementIds = other.AchievementIds;
		}

		// Token: 0x06002AC5 RID: 10949 RVA: 0x00040278 File Offset: 0x0003E478
		public void Set(ref OnAchievementsUnlockedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.UserId = other.Value.UserId;
				this.AchievementIds = other.Value.AchievementIds;
			}
		}

		// Token: 0x06002AC6 RID: 10950 RVA: 0x000402D1 File Offset: 0x0003E4D1
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_UserId);
			Helper.Dispose(ref this.m_AchievementIds);
		}

		// Token: 0x06002AC7 RID: 10951 RVA: 0x000402F8 File Offset: 0x0003E4F8
		public void Get(out OnAchievementsUnlockedCallbackInfo output)
		{
			output = default(OnAchievementsUnlockedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x0400137B RID: 4987
		private IntPtr m_ClientData;

		// Token: 0x0400137C RID: 4988
		private IntPtr m_UserId;

		// Token: 0x0400137D RID: 4989
		private uint m_AchievementsCount;

		// Token: 0x0400137E RID: 4990
		private IntPtr m_AchievementIds;
	}
}
