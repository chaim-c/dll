using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x02000582 RID: 1410
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LinkAccountCallbackInfoInternal : ICallbackInfoInternal, IGettable<LinkAccountCallbackInfo>, ISettable<LinkAccountCallbackInfo>, IDisposable
	{
		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x0600241C RID: 9244 RVA: 0x00035BBC File Offset: 0x00033DBC
		// (set) Token: 0x0600241D RID: 9245 RVA: 0x00035BD4 File Offset: 0x00033DD4
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

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x0600241E RID: 9246 RVA: 0x00035BE0 File Offset: 0x00033DE0
		// (set) Token: 0x0600241F RID: 9247 RVA: 0x00035C01 File Offset: 0x00033E01
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

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002420 RID: 9248 RVA: 0x00035C14 File Offset: 0x00033E14
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002421 RID: 9249 RVA: 0x00035C2C File Offset: 0x00033E2C
		// (set) Token: 0x06002422 RID: 9250 RVA: 0x00035C4D File Offset: 0x00033E4D
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

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002423 RID: 9251 RVA: 0x00035C60 File Offset: 0x00033E60
		// (set) Token: 0x06002424 RID: 9252 RVA: 0x00035C81 File Offset: 0x00033E81
		public PinGrantInfo? PinGrantInfo
		{
			get
			{
				PinGrantInfo? result;
				Helper.Get<PinGrantInfoInternal, PinGrantInfo>(this.m_PinGrantInfo, out result);
				return result;
			}
			set
			{
				Helper.Set<PinGrantInfo, PinGrantInfoInternal>(ref value, ref this.m_PinGrantInfo);
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002425 RID: 9253 RVA: 0x00035C94 File Offset: 0x00033E94
		// (set) Token: 0x06002426 RID: 9254 RVA: 0x00035CB5 File Offset: 0x00033EB5
		public EpicAccountId SelectedAccountId
		{
			get
			{
				EpicAccountId result;
				Helper.Get<EpicAccountId>(this.m_SelectedAccountId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_SelectedAccountId);
			}
		}

		// Token: 0x06002427 RID: 9255 RVA: 0x00035CC8 File Offset: 0x00033EC8
		public void Set(ref LinkAccountCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.PinGrantInfo = other.PinGrantInfo;
			this.SelectedAccountId = other.SelectedAccountId;
		}

		// Token: 0x06002428 RID: 9256 RVA: 0x00035D18 File Offset: 0x00033F18
		public void Set(ref LinkAccountCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.PinGrantInfo = other.Value.PinGrantInfo;
				this.SelectedAccountId = other.Value.SelectedAccountId;
			}
		}

		// Token: 0x06002429 RID: 9257 RVA: 0x00035D9B File Offset: 0x00033F9B
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_PinGrantInfo);
			Helper.Dispose(ref this.m_SelectedAccountId);
		}

		// Token: 0x0600242A RID: 9258 RVA: 0x00035DCE File Offset: 0x00033FCE
		public void Get(out LinkAccountCallbackInfo output)
		{
			output = default(LinkAccountCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000FE8 RID: 4072
		private Result m_ResultCode;

		// Token: 0x04000FE9 RID: 4073
		private IntPtr m_ClientData;

		// Token: 0x04000FEA RID: 4074
		private IntPtr m_LocalUserId;

		// Token: 0x04000FEB RID: 4075
		private IntPtr m_PinGrantInfo;

		// Token: 0x04000FEC RID: 4076
		private IntPtr m_SelectedAccountId;
	}
}
