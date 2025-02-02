using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Achievements
{
	// Token: 0x02000693 RID: 1683
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnUnlockAchievementsCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnUnlockAchievementsCompleteCallbackInfo>, ISettable<OnUnlockAchievementsCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x17000D0C RID: 3340
		// (get) Token: 0x06002B2B RID: 11051 RVA: 0x00040924 File Offset: 0x0003EB24
		// (set) Token: 0x06002B2C RID: 11052 RVA: 0x0004093C File Offset: 0x0003EB3C
		public Result ResultCode
		{
			get
			{
				return this.m_ResultCode;
			}
			set
			{
				this.m_ResultCode = value;
			}
		}

		// Token: 0x17000D0D RID: 3341
		// (get) Token: 0x06002B2D RID: 11053 RVA: 0x00040948 File Offset: 0x0003EB48
		// (set) Token: 0x06002B2E RID: 11054 RVA: 0x00040969 File Offset: 0x0003EB69
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

		// Token: 0x17000D0E RID: 3342
		// (get) Token: 0x06002B2F RID: 11055 RVA: 0x0004097C File Offset: 0x0003EB7C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000D0F RID: 3343
		// (get) Token: 0x06002B30 RID: 11056 RVA: 0x00040994 File Offset: 0x0003EB94
		// (set) Token: 0x06002B31 RID: 11057 RVA: 0x000409B5 File Offset: 0x0003EBB5
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

		// Token: 0x17000D10 RID: 3344
		// (get) Token: 0x06002B32 RID: 11058 RVA: 0x000409C8 File Offset: 0x0003EBC8
		// (set) Token: 0x06002B33 RID: 11059 RVA: 0x000409E0 File Offset: 0x0003EBE0
		public uint AchievementsCount
		{
			get
			{
				return this.m_AchievementsCount;
			}
			set
			{
				this.m_AchievementsCount = value;
			}
		}

		// Token: 0x06002B34 RID: 11060 RVA: 0x000409EA File Offset: 0x0003EBEA
		public void Set(ref OnUnlockAchievementsCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.UserId = other.UserId;
			this.AchievementsCount = other.AchievementsCount;
		}

		// Token: 0x06002B35 RID: 11061 RVA: 0x00040A24 File Offset: 0x0003EC24
		public void Set(ref OnUnlockAchievementsCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.UserId = other.Value.UserId;
				this.AchievementsCount = other.Value.AchievementsCount;
			}
		}

		// Token: 0x06002B36 RID: 11062 RVA: 0x00040A92 File Offset: 0x0003EC92
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_UserId);
		}

		// Token: 0x06002B37 RID: 11063 RVA: 0x00040AAD File Offset: 0x0003ECAD
		public void Get(out OnUnlockAchievementsCompleteCallbackInfo output)
		{
			output = default(OnUnlockAchievementsCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04001395 RID: 5013
		private Result m_ResultCode;

		// Token: 0x04001396 RID: 5014
		private IntPtr m_ClientData;

		// Token: 0x04001397 RID: 5015
		private IntPtr m_UserId;

		// Token: 0x04001398 RID: 5016
		private uint m_AchievementsCount;
	}
}
