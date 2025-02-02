using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004D4 RID: 1236
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryEntitlementsCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryEntitlementsCallbackInfo>, ISettable<QueryEntitlementsCallbackInfo>, IDisposable
	{
		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06001FB8 RID: 8120 RVA: 0x0002F090 File Offset: 0x0002D290
		// (set) Token: 0x06001FB9 RID: 8121 RVA: 0x0002F0A8 File Offset: 0x0002D2A8
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

		// Token: 0x17000930 RID: 2352
		// (get) Token: 0x06001FBA RID: 8122 RVA: 0x0002F0B4 File Offset: 0x0002D2B4
		// (set) Token: 0x06001FBB RID: 8123 RVA: 0x0002F0D5 File Offset: 0x0002D2D5
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

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06001FBC RID: 8124 RVA: 0x0002F0E8 File Offset: 0x0002D2E8
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001FBD RID: 8125 RVA: 0x0002F100 File Offset: 0x0002D300
		// (set) Token: 0x06001FBE RID: 8126 RVA: 0x0002F121 File Offset: 0x0002D321
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

		// Token: 0x06001FBF RID: 8127 RVA: 0x0002F131 File Offset: 0x0002D331
		public void Set(ref QueryEntitlementsCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001FC0 RID: 8128 RVA: 0x0002F15C File Offset: 0x0002D35C
		public void Set(ref QueryEntitlementsCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001FC1 RID: 8129 RVA: 0x0002F1B5 File Offset: 0x0002D3B5
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06001FC2 RID: 8130 RVA: 0x0002F1D0 File Offset: 0x0002D3D0
		public void Get(out QueryEntitlementsCallbackInfo output)
		{
			output = default(QueryEntitlementsCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000E26 RID: 3622
		private Result m_ResultCode;

		// Token: 0x04000E27 RID: 3623
		private IntPtr m_ClientData;

		// Token: 0x04000E28 RID: 3624
		private IntPtr m_LocalUserId;
	}
}
