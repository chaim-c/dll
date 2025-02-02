using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.KWS
{
	// Token: 0x0200044C RID: 1100
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RequestPermissionsCallbackInfoInternal : ICallbackInfoInternal, IGettable<RequestPermissionsCallbackInfo>, ISettable<RequestPermissionsCallbackInfo>, IDisposable
	{
		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x06001C3E RID: 7230 RVA: 0x00029BB0 File Offset: 0x00027DB0
		// (set) Token: 0x06001C3F RID: 7231 RVA: 0x00029BC8 File Offset: 0x00027DC8
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

		// Token: 0x17000800 RID: 2048
		// (get) Token: 0x06001C40 RID: 7232 RVA: 0x00029BD4 File Offset: 0x00027DD4
		// (set) Token: 0x06001C41 RID: 7233 RVA: 0x00029BF5 File Offset: 0x00027DF5
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

		// Token: 0x17000801 RID: 2049
		// (get) Token: 0x06001C42 RID: 7234 RVA: 0x00029C08 File Offset: 0x00027E08
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000802 RID: 2050
		// (get) Token: 0x06001C43 RID: 7235 RVA: 0x00029C20 File Offset: 0x00027E20
		// (set) Token: 0x06001C44 RID: 7236 RVA: 0x00029C41 File Offset: 0x00027E41
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

		// Token: 0x06001C45 RID: 7237 RVA: 0x00029C51 File Offset: 0x00027E51
		public void Set(ref RequestPermissionsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001C46 RID: 7238 RVA: 0x00029C7C File Offset: 0x00027E7C
		public void Set(ref RequestPermissionsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001C47 RID: 7239 RVA: 0x00029CD5 File Offset: 0x00027ED5
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06001C48 RID: 7240 RVA: 0x00029CF0 File Offset: 0x00027EF0
		public void Get(out RequestPermissionsCallbackInfo output)
		{
			output = default(RequestPermissionsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000C7C RID: 3196
		private Result m_ResultCode;

		// Token: 0x04000C7D RID: 3197
		private IntPtr m_ClientData;

		// Token: 0x04000C7E RID: 3198
		private IntPtr m_LocalUserId;
	}
}
