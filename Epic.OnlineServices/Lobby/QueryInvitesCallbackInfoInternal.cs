using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003E2 RID: 994
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryInvitesCallbackInfo>, ISettable<QueryInvitesCallbackInfo>, IDisposable
	{
		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060019B4 RID: 6580 RVA: 0x00025FE4 File Offset: 0x000241E4
		// (set) Token: 0x060019B5 RID: 6581 RVA: 0x00025FFC File Offset: 0x000241FC
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

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x00026008 File Offset: 0x00024208
		// (set) Token: 0x060019B7 RID: 6583 RVA: 0x00026029 File Offset: 0x00024229
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

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0002603C File Offset: 0x0002423C
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x00026054 File Offset: 0x00024254
		// (set) Token: 0x060019BA RID: 6586 RVA: 0x00026075 File Offset: 0x00024275
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

		// Token: 0x060019BB RID: 6587 RVA: 0x00026085 File Offset: 0x00024285
		public void Set(ref QueryInvitesCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000260B0 File Offset: 0x000242B0
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

		// Token: 0x060019BD RID: 6589 RVA: 0x00026109 File Offset: 0x00024309
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00026124 File Offset: 0x00024324
		public void Get(out QueryInvitesCallbackInfo output)
		{
			output = default(QueryInvitesCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000B73 RID: 2931
		private Result m_ResultCode;

		// Token: 0x04000B74 RID: 2932
		private IntPtr m_ClientData;

		// Token: 0x04000B75 RID: 2933
		private IntPtr m_LocalUserId;
	}
}
