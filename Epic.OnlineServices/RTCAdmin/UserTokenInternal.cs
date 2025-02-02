using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x02000210 RID: 528
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UserTokenInternal : IGettable<UserToken>, ISettable<UserToken>, IDisposable
	{
		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06000EDD RID: 3805 RVA: 0x00016110 File Offset: 0x00014310
		// (set) Token: 0x06000EDE RID: 3806 RVA: 0x00016131 File Offset: 0x00014331
		public ProductUserId ProductUserId
		{
			get
			{
				ProductUserId result;
				Helper.Get<ProductUserId>(this.m_ProductUserId, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_ProductUserId);
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06000EDF RID: 3807 RVA: 0x00016144 File Offset: 0x00014344
		// (set) Token: 0x06000EE0 RID: 3808 RVA: 0x00016165 File Offset: 0x00014365
		public Utf8String Token
		{
			get
			{
				Utf8String result;
				Helper.Get(this.m_Token, out result);
				return result;
			}
			set
			{
				Helper.Set(value, ref this.m_Token);
			}
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x00016175 File Offset: 0x00014375
		public void Set(ref UserToken other)
		{
			this.m_ApiVersion = 1;
			this.ProductUserId = other.ProductUserId;
			this.Token = other.Token;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0001619C File Offset: 0x0001439C
		public void Set(ref UserToken? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.ProductUserId = other.Value.ProductUserId;
				this.Token = other.Value.Token;
			}
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000161E7 File Offset: 0x000143E7
		public void Dispose()
		{
			Helper.Dispose(ref this.m_ProductUserId);
			Helper.Dispose(ref this.m_Token);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x00016202 File Offset: 0x00014402
		public void Get(out UserToken output)
		{
			output = default(UserToken);
			output.Set(ref this);
		}

		// Token: 0x040006A7 RID: 1703
		private int m_ApiVersion;

		// Token: 0x040006A8 RID: 1704
		private IntPtr m_ProductUserId;

		// Token: 0x040006A9 RID: 1705
		private IntPtr m_Token;
	}
}
