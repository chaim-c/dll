using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Leaderboards
{
	// Token: 0x02000414 RID: 1044
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct OnQueryLeaderboardDefinitionsCompleteCallbackInfoInternal : ICallbackInfoInternal, IGettable<OnQueryLeaderboardDefinitionsCompleteCallbackInfo>, ISettable<OnQueryLeaderboardDefinitionsCompleteCallbackInfo>, IDisposable
	{
		// Token: 0x1700079B RID: 1947
		// (get) Token: 0x06001AEC RID: 6892 RVA: 0x00027FA4 File Offset: 0x000261A4
		// (set) Token: 0x06001AED RID: 6893 RVA: 0x00027FBC File Offset: 0x000261BC
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

		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x06001AEE RID: 6894 RVA: 0x00027FC8 File Offset: 0x000261C8
		// (set) Token: 0x06001AEF RID: 6895 RVA: 0x00027FE9 File Offset: 0x000261E9
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

		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00027FFC File Offset: 0x000261FC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x00028014 File Offset: 0x00026214
		public void Set(ref OnQueryLeaderboardDefinitionsCompleteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x00028034 File Offset: 0x00026234
		public void Set(ref OnQueryLeaderboardDefinitionsCompleteCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
			}
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x00028078 File Offset: 0x00026278
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x00028087 File Offset: 0x00026287
		public void Get(out OnQueryLeaderboardDefinitionsCompleteCallbackInfo output)
		{
			output = default(OnQueryLeaderboardDefinitionsCompleteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000C01 RID: 3073
		private Result m_ResultCode;

		// Token: 0x04000C02 RID: 3074
		private IntPtr m_ClientData;
	}
}
