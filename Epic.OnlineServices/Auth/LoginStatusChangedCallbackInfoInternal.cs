using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Auth
{
	// Token: 0x0200058C RID: 1420
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginStatusChangedCallbackInfoInternal : ICallbackInfoInternal, IGettable<LoginStatusChangedCallbackInfo>, ISettable<LoginStatusChangedCallbackInfo>, IDisposable
	{
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x0600246D RID: 9325 RVA: 0x00036440 File Offset: 0x00034640
		// (set) Token: 0x0600246E RID: 9326 RVA: 0x00036461 File Offset: 0x00034661
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

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x00036474 File Offset: 0x00034674
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x0003648C File Offset: 0x0003468C
		// (set) Token: 0x06002471 RID: 9329 RVA: 0x000364AD File Offset: 0x000346AD
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

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x000364C0 File Offset: 0x000346C0
		// (set) Token: 0x06002473 RID: 9331 RVA: 0x000364D8 File Offset: 0x000346D8
		public LoginStatus PrevStatus
		{
			get
			{
				return this.m_PrevStatus;
			}
			set
			{
				this.m_PrevStatus = value;
			}
		}

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x000364E4 File Offset: 0x000346E4
		// (set) Token: 0x06002475 RID: 9333 RVA: 0x000364FC File Offset: 0x000346FC
		public LoginStatus CurrentStatus
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

		// Token: 0x06002476 RID: 9334 RVA: 0x00036506 File Offset: 0x00034706
		public void Set(ref LoginStatusChangedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.PrevStatus = other.PrevStatus;
			this.CurrentStatus = other.CurrentStatus;
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00036540 File Offset: 0x00034740
		public void Set(ref LoginStatusChangedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.PrevStatus = other.Value.PrevStatus;
				this.CurrentStatus = other.Value.CurrentStatus;
			}
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x000365AE File Offset: 0x000347AE
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x000365C9 File Offset: 0x000347C9
		public void Get(out LoginStatusChangedCallbackInfo output)
		{
			output = default(LoginStatusChangedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04001017 RID: 4119
		private IntPtr m_ClientData;

		// Token: 0x04001018 RID: 4120
		private IntPtr m_LocalUserId;

		// Token: 0x04001019 RID: 4121
		private LoginStatus m_PrevStatus;

		// Token: 0x0400101A RID: 4122
		private LoginStatus m_CurrentStatus;
	}
}
