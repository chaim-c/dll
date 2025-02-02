using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200053F RID: 1343
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LoginStatusChangedCallbackInfoInternal : ICallbackInfoInternal, IGettable<LoginStatusChangedCallbackInfo>, ISettable<LoginStatusChangedCallbackInfo>, IDisposable
	{
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x0600227B RID: 8827 RVA: 0x000338C0 File Offset: 0x00031AC0
		// (set) Token: 0x0600227C RID: 8828 RVA: 0x000338E1 File Offset: 0x00031AE1
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

		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x0600227D RID: 8829 RVA: 0x000338F4 File Offset: 0x00031AF4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x0600227E RID: 8830 RVA: 0x0003390C File Offset: 0x00031B0C
		// (set) Token: 0x0600227F RID: 8831 RVA: 0x0003392D File Offset: 0x00031B2D
		public ProductUserId LocalUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_LocalUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x06002280 RID: 8832 RVA: 0x00033940 File Offset: 0x00031B40
		// (set) Token: 0x06002281 RID: 8833 RVA: 0x00033958 File Offset: 0x00031B58
		public LoginStatus PreviousStatus
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

		// Token: 0x17000A22 RID: 2594
		// (get) Token: 0x06002282 RID: 8834 RVA: 0x00033964 File Offset: 0x00031B64
		// (set) Token: 0x06002283 RID: 8835 RVA: 0x0003397C File Offset: 0x00031B7C
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

		// Token: 0x06002284 RID: 8836 RVA: 0x00033986 File Offset: 0x00031B86
		public void Set(ref LoginStatusChangedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.PreviousStatus = other.PreviousStatus;
			this.CurrentStatus = other.CurrentStatus;
		}

		// Token: 0x06002285 RID: 8837 RVA: 0x000339C0 File Offset: 0x00031BC0
		public void Set(ref LoginStatusChangedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.PreviousStatus = other.Value.PreviousStatus;
				this.CurrentStatus = other.Value.CurrentStatus;
			}
		}

		// Token: 0x06002286 RID: 8838 RVA: 0x00033A2E File Offset: 0x00031C2E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06002287 RID: 8839 RVA: 0x00033A49 File Offset: 0x00031C49
		public void Get(out LoginStatusChangedCallbackInfo output)
		{
			output = default(LoginStatusChangedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F55 RID: 3925
		private IntPtr m_ClientData;

		// Token: 0x04000F56 RID: 3926
		private IntPtr m_LocalUserId;

		// Token: 0x04000F57 RID: 3927
		private LoginStatus m_PreviousStatus;

		// Token: 0x04000F58 RID: 3928
		private LoginStatus m_CurrentStatus;
	}
}
