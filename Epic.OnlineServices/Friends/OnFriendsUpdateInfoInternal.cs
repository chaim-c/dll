using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x02000471 RID: 1137
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnFriendsUpdateInfoInternal : ICallbackInfoInternal, IGettable<OnFriendsUpdateInfo>, ISettable<OnFriendsUpdateInfo>, IDisposable
	{
		// Token: 0x1700083C RID: 2108
		// (get) Token: 0x06001D07 RID: 7431 RVA: 0x0002AEA4 File Offset: 0x000290A4
		// (set) Token: 0x06001D08 RID: 7432 RVA: 0x0002AEC5 File Offset: 0x000290C5
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

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x06001D09 RID: 7433 RVA: 0x0002AED8 File Offset: 0x000290D8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x06001D0A RID: 7434 RVA: 0x0002AEF0 File Offset: 0x000290F0
		// (set) Token: 0x06001D0B RID: 7435 RVA: 0x0002AF11 File Offset: 0x00029111
		public EpicAccountId LocalUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x06001D0C RID: 7436 RVA: 0x0002AF24 File Offset: 0x00029124
		// (set) Token: 0x06001D0D RID: 7437 RVA: 0x0002AF45 File Offset: 0x00029145
		public EpicAccountId TargetUserId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_TargetUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x06001D0E RID: 7438 RVA: 0x0002AF58 File Offset: 0x00029158
		// (set) Token: 0x06001D0F RID: 7439 RVA: 0x0002AF70 File Offset: 0x00029170
		public FriendsStatus PreviousStatus
		{
			get
			{
				return this.m_PreviousStatus;
			}
			set
			{
				this.m_PreviousStatus = value;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x06001D10 RID: 7440 RVA: 0x0002AF7C File Offset: 0x0002917C
		// (set) Token: 0x06001D11 RID: 7441 RVA: 0x0002AF94 File Offset: 0x00029194
		public FriendsStatus CurrentStatus
		{
			get
			{
				return this.m_CurrentStatus;
			}
			set
			{
				this.m_CurrentStatus = value;
			}
		}

		// Token: 0x06001D12 RID: 7442 RVA: 0x0002AFA0 File Offset: 0x000291A0
		public void Set(ref OnFriendsUpdateInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.PreviousStatus = other.PreviousStatus;
			this.CurrentStatus = other.CurrentStatus;
		}

		// Token: 0x06001D13 RID: 7443 RVA: 0x0002AFF0 File Offset: 0x000291F0
		public void Set(ref OnFriendsUpdateInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.PreviousStatus = other.Value.PreviousStatus;
				this.CurrentStatus = other.Value.CurrentStatus;
			}
		}

		// Token: 0x06001D14 RID: 7444 RVA: 0x0002B073 File Offset: 0x00029273
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06001D15 RID: 7445 RVA: 0x0002B09A File Offset: 0x0002929A
		public void Get(out OnFriendsUpdateInfo output)
		{
			output = default(OnFriendsUpdateInfo);
			output.Set(ref this);
		}

		// Token: 0x04000CDC RID: 3292
		private IntPtr m_ClientData;

		// Token: 0x04000CDD RID: 3293
		private IntPtr m_LocalUserId;

		// Token: 0x04000CDE RID: 3294
		private IntPtr m_TargetUserId;

		// Token: 0x04000CDF RID: 3295
		private FriendsStatus m_PreviousStatus;

		// Token: 0x04000CE0 RID: 3296
		private FriendsStatus m_CurrentStatus;
	}
}
