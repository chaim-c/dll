using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x0200010E RID: 270
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryInvitesCallbackInfo>, ISettable<QueryInvitesCallbackInfo>, IDisposable
	{
		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000841 RID: 2113 RVA: 0x0000BD30 File Offset: 0x00009F30
		// (set) Token: 0x06000842 RID: 2114 RVA: 0x0000BD48 File Offset: 0x00009F48
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

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000843 RID: 2115 RVA: 0x0000BD54 File Offset: 0x00009F54
		// (set) Token: 0x06000844 RID: 2116 RVA: 0x0000BD75 File Offset: 0x00009F75
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

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000845 RID: 2117 RVA: 0x0000BD88 File Offset: 0x00009F88
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000846 RID: 2118 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		// (set) Token: 0x06000847 RID: 2119 RVA: 0x0000BDC1 File Offset: 0x00009FC1
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

		// Token: 0x06000848 RID: 2120 RVA: 0x0000BDD1 File Offset: 0x00009FD1
		public void Set(ref QueryInvitesCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0000BDFC File Offset: 0x00009FFC
		public void Set(ref QueryInvitesCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0000BE55 File Offset: 0x0000A055
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0000BE70 File Offset: 0x0000A070
		public void Get(out QueryInvitesCallbackInfo output)
		{
			output = default(QueryInvitesCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040003C6 RID: 966
		private Result m_ResultCode;

		// Token: 0x040003C7 RID: 967
		private IntPtr m_ClientData;

		// Token: 0x040003C8 RID: 968
		private IntPtr m_LocalUserId;
	}
}
