using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Presence
{
	// Token: 0x02000255 RID: 597
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryPresenceCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryPresenceCallbackInfo>, ISettable<QueryPresenceCallbackInfo>, IDisposable
	{
		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x00018524 File Offset: 0x00016724
		// (set) Token: 0x06001063 RID: 4195 RVA: 0x0001853C File Offset: 0x0001673C
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

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x00018548 File Offset: 0x00016748
		// (set) Token: 0x06001065 RID: 4197 RVA: 0x00018569 File Offset: 0x00016769
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

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0001857C File Offset: 0x0001677C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x00018594 File Offset: 0x00016794
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x000185B5 File Offset: 0x000167B5
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

		// Token: 0x17000450 RID: 1104
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x000185C8 File Offset: 0x000167C8
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x000185E9 File Offset: 0x000167E9
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

		// Token: 0x0600106B RID: 4203 RVA: 0x000185F9 File Offset: 0x000167F9
		public void Set(ref QueryPresenceCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x0600106C RID: 4204 RVA: 0x00018630 File Offset: 0x00016830
		public void Set(ref QueryPresenceCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
			}
		}

		// Token: 0x0600106D RID: 4205 RVA: 0x0001869E File Offset: 0x0001689E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x000186C5 File Offset: 0x000168C5
		public void Get(out QueryPresenceCallbackInfo output)
		{
			output = default(QueryPresenceCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000759 RID: 1881
		private Result m_ResultCode;

		// Token: 0x0400075A RID: 1882
		private IntPtr m_ClientData;

		// Token: 0x0400075B RID: 1883
		private IntPtr m_LocalUserId;

		// Token: 0x0400075C RID: 1884
		private IntPtr m_TargetUserId;
	}
}
