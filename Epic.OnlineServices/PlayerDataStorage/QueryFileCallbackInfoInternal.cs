using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x0200028C RID: 652
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileCallbackInfoInternal : ICallbackInfoInternal, IGettable<QueryFileCallbackInfo>, ISettable<QueryFileCallbackInfo>, IDisposable
	{
		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0001A08C File Offset: 0x0001828C
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x0001A0A4 File Offset: 0x000182A4
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

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0001A0B0 File Offset: 0x000182B0
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0001A0D1 File Offset: 0x000182D1
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

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x060011B4 RID: 4532 RVA: 0x0001A0E4 File Offset: 0x000182E4
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x060011B5 RID: 4533 RVA: 0x0001A0FC File Offset: 0x000182FC
		// (set) Token: 0x060011B6 RID: 4534 RVA: 0x0001A11D File Offset: 0x0001831D
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

		// Token: 0x060011B7 RID: 4535 RVA: 0x0001A12D File Offset: 0x0001832D
		public void Set(ref QueryFileCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x0001A158 File Offset: 0x00018358
		public void Set(ref QueryFileCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x0001A1B1 File Offset: 0x000183B1
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x0001A1CC File Offset: 0x000183CC
		public void Get(out QueryFileCallbackInfo output)
		{
			output = default(QueryFileCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x040007D0 RID: 2000
		private Result m_ResultCode;

		// Token: 0x040007D1 RID: 2001
		private IntPtr m_ClientData;

		// Token: 0x040007D2 RID: 2002
		private IntPtr m_LocalUserId;
	}
}
