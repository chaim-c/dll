using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x0200055D RID: 1373
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryProductUserIdMappingsCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryProductUserIdMappingsCallbackInfo>, ISettable<QueryProductUserIdMappingsCallbackInfo>, IDisposable
	{
		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x0600230F RID: 8975 RVA: 0x00033DB8 File Offset: 0x00031FB8
		// (set) Token: 0x06002310 RID: 8976 RVA: 0x00033DD0 File Offset: 0x00031FD0
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

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06002311 RID: 8977 RVA: 0x00033DDC File Offset: 0x00031FDC
		// (set) Token: 0x06002312 RID: 8978 RVA: 0x00033DFD File Offset: 0x00031FFD
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

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x00033E10 File Offset: 0x00032010
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x00033E28 File Offset: 0x00032028
		// (set) Token: 0x06002315 RID: 8981 RVA: 0x00033E49 File Offset: 0x00032049
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

		// Token: 0x06002316 RID: 8982 RVA: 0x00033E59 File Offset: 0x00032059
		public void Set(ref QueryProductUserIdMappingsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06002317 RID: 8983 RVA: 0x00033E84 File Offset: 0x00032084
		public void Set(ref QueryProductUserIdMappingsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002318 RID: 8984 RVA: 0x00033EDD File Offset: 0x000320DD
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x00033EF8 File Offset: 0x000320F8
		public void Get(out QueryProductUserIdMappingsCallbackInfo output)
		{
			output = default(QueryProductUserIdMappingsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F6A RID: 3946
		private Result m_ResultCode;

		// Token: 0x04000F6B RID: 3947
		private IntPtr m_ClientData;

		// Token: 0x04000F6C RID: 3948
		private IntPtr m_LocalUserId;
	}
}
