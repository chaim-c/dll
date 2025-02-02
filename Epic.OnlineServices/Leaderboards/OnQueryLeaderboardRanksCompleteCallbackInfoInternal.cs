using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000418 RID: 1048
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryLeaderboardRanksCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnQueryLeaderboardRanksCompleteCallbackInfo>, ISettable<OnQueryLeaderboardRanksCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001B03 RID: 6915 RVA: 0x000280F8 File Offset: 0x000262F8
		// (set) Token: 0x06001B04 RID: 6916 RVA: 0x00028110 File Offset: 0x00026310
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

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001B05 RID: 6917 RVA: 0x0002811C File Offset: 0x0002631C
		// (set) Token: 0x06001B06 RID: 6918 RVA: 0x0002813D File Offset: 0x0002633D
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

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001B07 RID: 6919 RVA: 0x00028150 File Offset: 0x00026350
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x00028168 File Offset: 0x00026368
		public void Set(ref OnQueryLeaderboardRanksCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x00028188 File Offset: 0x00026388
		public void Set(ref OnQueryLeaderboardRanksCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000281CC File Offset: 0x000263CC
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000281DB File Offset: 0x000263DB
		public void Get(out OnQueryLeaderboardRanksCompleteCallbackInfo output)
		{
			output = default(OnQueryLeaderboardRanksCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000C05 RID: 3077
		private Result m_ResultCode;

		// Token: 0x04000C06 RID: 3078
		private IntPtr m_ClientData;
	}
}
