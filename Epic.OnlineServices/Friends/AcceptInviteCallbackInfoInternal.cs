using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200045F RID: 1119
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct AcceptInviteCallbackInfoInternal : ICallbackInfoInternal, IGettable<AcceptInviteCallbackInfo>, ISettable<AcceptInviteCallbackInfo>, IDisposable
	{
		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x06001CAA RID: 7338 RVA: 0x0002A59C File Offset: 0x0002879C
		// (set) Token: 0x06001CAB RID: 7339 RVA: 0x0002A5B4 File Offset: 0x000287B4
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

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x06001CAC RID: 7340 RVA: 0x0002A5C0 File Offset: 0x000287C0
		// (set) Token: 0x06001CAD RID: 7341 RVA: 0x0002A5E1 File Offset: 0x000287E1
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

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x06001CAE RID: 7342 RVA: 0x0002A5F4 File Offset: 0x000287F4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000827 RID: 2087
		// (get) Token: 0x06001CAF RID: 7343 RVA: 0x0002A60C File Offset: 0x0002880C
		// (set) Token: 0x06001CB0 RID: 7344 RVA: 0x0002A62D File Offset: 0x0002882D
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

		// Token: 0x17000828 RID: 2088
		// (get) Token: 0x06001CB1 RID: 7345 RVA: 0x0002A640 File Offset: 0x00028840
		// (set) Token: 0x06001CB2 RID: 7346 RVA: 0x0002A661 File Offset: 0x00028861
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

		// Token: 0x06001CB3 RID: 7347 RVA: 0x0002A671 File Offset: 0x00028871
		public void Set(ref AcceptInviteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001CB4 RID: 7348 RVA: 0x0002A6A8 File Offset: 0x000288A8
		public void Set(ref AcceptInviteCallbackInfo? other)
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

		// Token: 0x06001CB5 RID: 7349 RVA: 0x0002A716 File Offset: 0x00028916
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06001CB6 RID: 7350 RVA: 0x0002A73D File Offset: 0x0002893D
		public void Get(out AcceptInviteCallbackInfo output)
		{
			output = default(AcceptInviteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000CB3 RID: 3251
		private Result m_ResultCode;

		// Token: 0x04000CB4 RID: 3252
		private IntPtr m_ClientData;

		// Token: 0x04000CB5 RID: 3253
		private IntPtr m_LocalUserId;

		// Token: 0x04000CB6 RID: 3254
		private IntPtr m_TargetUserId;
	}
}
