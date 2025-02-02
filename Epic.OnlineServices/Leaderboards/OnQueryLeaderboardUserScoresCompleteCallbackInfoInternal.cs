using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x0200041C RID: 1052
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryLeaderboardUserScoresCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnQueryLeaderboardUserScoresCompleteCallbackInfo>, ISettable<OnQueryLeaderboardUserScoresCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x0002824C File Offset: 0x0002644C
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x00028264 File Offset: 0x00026464
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

		// Token: 0x170007A6 RID: 1958
		// (get) Token: 0x06001B1C RID: 6940 RVA: 0x00028270 File Offset: 0x00026470
		// (set) Token: 0x06001B1D RID: 6941 RVA: 0x00028291 File Offset: 0x00026491
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

		// Token: 0x170007A7 RID: 1959
		// (get) Token: 0x06001B1E RID: 6942 RVA: 0x000282A4 File Offset: 0x000264A4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000282BC File Offset: 0x000264BC
		public void Set(ref OnQueryLeaderboardUserScoresCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06001B20 RID: 6944 RVA: 0x000282DC File Offset: 0x000264DC
		public void Set(ref OnQueryLeaderboardUserScoresCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06001B21 RID: 6945 RVA: 0x00028320 File Offset: 0x00026520
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06001B22 RID: 6946 RVA: 0x0002832F File Offset: 0x0002652F
		public void Get(out OnQueryLeaderboardUserScoresCompleteCallbackInfo output)
		{
			output = default(OnQueryLeaderboardUserScoresCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000C09 RID: 3081
		private Result m_ResultCode;

		// Token: 0x04000C0A RID: 3082
		private IntPtr m_ClientData;
	}
}
