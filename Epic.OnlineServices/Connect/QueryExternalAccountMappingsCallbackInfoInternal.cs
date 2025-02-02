using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Connect
{
	// Token: 0x02000559 RID: 1369
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryExternalAccountMappingsCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryExternalAccountMappingsCallbackInfo>, ISettable<QueryExternalAccountMappingsCallbackInfo>, IDisposable
	{
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x00033AD8 File Offset: 0x00031CD8
		// (set) Token: 0x060022F1 RID: 8945 RVA: 0x00033AF0 File Offset: 0x00031CF0
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

		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x00033AFC File Offset: 0x00031CFC
		// (set) Token: 0x060022F3 RID: 8947 RVA: 0x00033B1D File Offset: 0x00031D1D
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

		// Token: 0x17000A28 RID: 2600
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x00033B30 File Offset: 0x00031D30
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x060022F5 RID: 8949 RVA: 0x00033B48 File Offset: 0x00031D48
		// (set) Token: 0x060022F6 RID: 8950 RVA: 0x00033B69 File Offset: 0x00031D69
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

		// Token: 0x060022F7 RID: 8951 RVA: 0x00033B79 File Offset: 0x00031D79
		public void Set(ref QueryExternalAccountMappingsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x00033BA4 File Offset: 0x00031DA4
		public void Set(ref QueryExternalAccountMappingsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x00033BFD File Offset: 0x00031DFD
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x00033C18 File Offset: 0x00031E18
		public void Get(out QueryExternalAccountMappingsCallbackInfo output)
		{
			output = default(QueryExternalAccountMappingsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000F5C RID: 3932
		private Result m_ResultCode;

		// Token: 0x04000F5D RID: 3933
		private IntPtr m_ClientData;

		// Token: 0x04000F5E RID: 3934
		private IntPtr m_LocalUserId;
	}
}
