using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004DC RID: 1244
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryOffersCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryOffersCallbackInfo>, ISettable<QueryOffersCallbackInfo>, IDisposable
	{
		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001FF7 RID: 8183 RVA: 0x0002F694 File Offset: 0x0002D894
		// (set) Token: 0x06001FF8 RID: 8184 RVA: 0x0002F6AC File Offset: 0x0002D8AC
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

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001FF9 RID: 8185 RVA: 0x0002F6B8 File Offset: 0x0002D8B8
		// (set) Token: 0x06001FFA RID: 8186 RVA: 0x0002F6D9 File Offset: 0x0002D8D9
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

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001FFB RID: 8187 RVA: 0x0002F6EC File Offset: 0x0002D8EC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001FFC RID: 8188 RVA: 0x0002F704 File Offset: 0x0002D904
		// (set) Token: 0x06001FFD RID: 8189 RVA: 0x0002F725 File Offset: 0x0002D925
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

		// Token: 0x06001FFE RID: 8190 RVA: 0x0002F735 File Offset: 0x0002D935
		public void Set(ref QueryOffersCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001FFF RID: 8191 RVA: 0x0002F760 File Offset: 0x0002D960
		public void Set(ref QueryOffersCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06002000 RID: 8192 RVA: 0x0002F7B9 File Offset: 0x0002D9B9
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x06002001 RID: 8193 RVA: 0x0002F7D4 File Offset: 0x0002D9D4
		public void Get(out QueryOffersCallbackInfo output)
		{
			output = default(QueryOffersCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000E42 RID: 3650
		private Result m_ResultCode;

		// Token: 0x04000E43 RID: 3651
		private IntPtr m_ClientData;

		// Token: 0x04000E44 RID: 3652
		private IntPtr m_LocalUserId;
	}
}
