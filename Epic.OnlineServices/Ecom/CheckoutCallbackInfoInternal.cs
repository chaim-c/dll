using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x0200048B RID: 1163
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CheckoutCallbackInfoInternal : ICallbackInfoInternal, IGettable<CheckoutCallbackInfo>, ISettable<CheckoutCallbackInfo>, IDisposable
	{
		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x06001E28 RID: 7720 RVA: 0x0002CA54 File Offset: 0x0002AC54
		// (set) Token: 0x06001E29 RID: 7721 RVA: 0x0002CA6C File Offset: 0x0002AC6C
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

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x06001E2A RID: 7722 RVA: 0x0002CA78 File Offset: 0x0002AC78
		// (set) Token: 0x06001E2B RID: 7723 RVA: 0x0002CA99 File Offset: 0x0002AC99
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

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x06001E2C RID: 7724 RVA: 0x0002CAAC File Offset: 0x0002ACAC
		public IntPtr ClientDataAddress
		{
			get
			{
				return this.m_ClientData;
			}
		}

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x06001E2D RID: 7725 RVA: 0x0002CAC4 File Offset: 0x0002ACC4
		// (set) Token: 0x06001E2E RID: 7726 RVA: 0x0002CAE5 File Offset: 0x0002ACE5
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

		// Token: 0x170008B1 RID: 2225
		// (get) Token: 0x06001E2F RID: 7727 RVA: 0x0002CAF8 File Offset: 0x0002ACF8
		// (set) Token: 0x06001E30 RID: 7728 RVA: 0x0002CB19 File Offset: 0x0002AD19
		public Utf8String TransactionId
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_TransactionId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_TransactionId);
			}
		}

		// Token: 0x06001E31 RID: 7729 RVA: 0x0002CB29 File Offset: 0x0002AD29
		public void Set(ref CheckoutCallbackInfo other)
		{
			this.ResultCode = other.ResultCode;
			this.ClientData = other.ClientData;
			this.LocalUserId = other.LocalUserId;
			this.TransactionId = other.TransactionId;
		}

		// Token: 0x06001E32 RID: 7730 RVA: 0x0002CB60 File Offset: 0x0002AD60
		public void Set(ref CheckoutCallbackInfo? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.ResultCode = other.Value.ResultCode;
				this.ClientData = other.Value.ClientData;
				this.LocalUserId = other.Value.LocalUserId;
				this.TransactionId = other.Value.TransactionId;
			}
		}

		// Token: 0x06001E33 RID: 7731 RVA: 0x0002CBCE File Offset: 0x0002ADCE
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ClientData);
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TransactionId);
		}

		// Token: 0x06001E34 RID: 7732 RVA: 0x0002CBF5 File Offset: 0x0002ADF5
		public void Get(out CheckoutCallbackInfo output)
		{
			output = default(CheckoutCallbackInfo);
			output.Set(ref this);
		}

		// Token: 0x04000D51 RID: 3409
		private Result m_ResultCode;

		// Token: 0x04000D52 RID: 3410
		private IntPtr m_ClientData;

		// Token: 0x04000D53 RID: 3411
		private IntPtr m_LocalUserId;

		// Token: 0x04000D54 RID: 3412
		private IntPtr m_TransactionId;
	}
}
