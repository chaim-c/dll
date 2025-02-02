using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Friends
{
	// Token: 0x0200047D RID: 1149
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteCallbackInfoInternal : ICallbackInfoInternal, IGettable<RejectInviteCallbackInfo>, ISettable<RejectInviteCallbackInfo>, IDisposable
	{
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x06001D51 RID: 7505 RVA: 0x0002B394 File Offset: 0x00029594
		// (set) Token: 0x06001D52 RID: 7506 RVA: 0x0002B3AC File Offset: 0x000295AC
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

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x06001D53 RID: 7507 RVA: 0x0002B3B8 File Offset: 0x000295B8
		// (set) Token: 0x06001D54 RID: 7508 RVA: 0x0002B3D9 File Offset: 0x000295D9
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

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x06001D55 RID: 7509 RVA: 0x0002B3EC File Offset: 0x000295EC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x06001D56 RID: 7510 RVA: 0x0002B404 File Offset: 0x00029604
		// (set) Token: 0x06001D57 RID: 7511 RVA: 0x0002B425 File Offset: 0x00029625
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

		// Token: 0x17000853 RID: 2131
		// (get) Token: 0x06001D58 RID: 7512 RVA: 0x0002B438 File Offset: 0x00029638
		// (set) Token: 0x06001D59 RID: 7513 RVA: 0x0002B459 File Offset: 0x00029659
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

		// Token: 0x06001D5A RID: 7514 RVA: 0x0002B469 File Offset: 0x00029669
		public void Set(ref RejectInviteCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0002B4A0 File Offset: 0x000296A0
		public void Set(ref RejectInviteCallbackInfo? other)
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

		// Token: 0x06001D5C RID: 7516 RVA: 0x0002B50E File Offset: 0x0002970E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0002B535 File Offset: 0x00029735
		public void Get(out RejectInviteCallbackInfo output)
		{
			output = default(RejectInviteCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000CEE RID: 3310
		private Result m_ResultCode;

		// Token: 0x04000CEF RID: 3311
		private IntPtr m_ClientData;

		// Token: 0x04000CF0 RID: 3312
		private IntPtr m_LocalUserId;

		// Token: 0x04000CF1 RID: 3313
		private IntPtr m_TargetUserId;
	}
}
