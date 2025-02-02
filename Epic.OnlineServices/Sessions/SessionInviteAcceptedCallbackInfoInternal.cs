using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200012E RID: 302
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionInviteAcceptedCallbackInfoInternal : ICallbackInfoInternal, IGettable<SessionInviteAcceptedCallbackInfo>, ISettable<SessionInviteAcceptedCallbackInfo>, IDisposable
	{
		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x0000D0A4 File Offset: 0x0000B2A4
		// (set) Token: 0x0600090A RID: 2314 RVA: 0x0000D0C5 File Offset: 0x0000B2C5
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

		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x0000D0D8 File Offset: 0x0000B2D8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x0600090C RID: 2316 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		// (set) Token: 0x0600090D RID: 2317 RVA: 0x0000D111 File Offset: 0x0000B311
		public Utf8String SessionId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_SessionId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_SessionId);
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0000D124 File Offset: 0x0000B324
		// (set) Token: 0x0600090F RID: 2319 RVA: 0x0000D145 File Offset: 0x0000B345
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

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0000D158 File Offset: 0x0000B358
		// (set) Token: 0x06000911 RID: 2321 RVA: 0x0000D179 File Offset: 0x0000B379
		public ProductUserId TargetUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_TargetUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TargetUserId);
			}
		}

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x0000D18C File Offset: 0x0000B38C
		// (set) Token: 0x06000913 RID: 2323 RVA: 0x0000D1AD File Offset: 0x0000B3AD
		public Utf8String InviteId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_InviteId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_InviteId);
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x0000D1C0 File Offset: 0x0000B3C0
		public void Set(ref SessionInviteAcceptedCallbackInfo other)
		{
			this.ClientData = other.ClientData;
			this.SessionId = other.SessionId;
			this.LocalUserId = other.LocalUserId;
			this.TargetUserId = other.TargetUserId;
			this.InviteId = other.InviteId;
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x0000D210 File Offset: 0x0000B410
		public void Set(ref SessionInviteAcceptedCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ClientData = other.Value.ClientData;
				this.SessionId = other.Value.SessionId;
				this.LocalUserId = other.Value.LocalUserId;
				this.TargetUserId = other.Value.TargetUserId;
				this.InviteId = other.Value.InviteId;
			}
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0000D293 File Offset: 0x0000B493
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_SessionId);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TargetUserId);
			Helper.Dispose(ref this.m_InviteId);
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x0000D2D2 File Offset: 0x0000B4D2
		public void Get(out SessionInviteAcceptedCallbackInfo output)
		{
			output = default(SessionInviteAcceptedCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000422 RID: 1058
		private IntPtr m_ClientData;

		// Token: 0x04000423 RID: 1059
		private IntPtr m_SessionId;

		// Token: 0x04000424 RID: 1060
		private IntPtr m_LocalUserId;

		// Token: 0x04000425 RID: 1061
		private IntPtr m_TargetUserId;

		// Token: 0x04000426 RID: 1062
		private IntPtr m_InviteId;
	}
}
