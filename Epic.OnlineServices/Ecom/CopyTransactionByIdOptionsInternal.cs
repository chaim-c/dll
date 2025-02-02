using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004A7 RID: 1191
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyTransactionByIdOptionsInternal : ISettable<CopyTransactionByIdOptions>, IDisposable
	{
		// Token: 0x170008F2 RID: 2290
		// (set) Token: 0x06001EC0 RID: 7872 RVA: 0x0002D7DD File Offset: 0x0002B9DD
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008F3 RID: 2291
		// (set) Token: 0x06001EC1 RID: 7873 RVA: 0x0002D7ED File Offset: 0x0002B9ED
		public Utf8String TransactionId
		{
			set
			{
				Helper.Set(value, ref this.m_TransactionId);
			}
		}

		// Token: 0x06001EC2 RID: 7874 RVA: 0x0002D7FD File Offset: 0x0002B9FD
		public void Set(ref CopyTransactionByIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TransactionId = other.TransactionId;
		}

		// Token: 0x06001EC3 RID: 7875 RVA: 0x0002D824 File Offset: 0x0002BA24
		public void Set(ref CopyTransactionByIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TransactionId = other.Value.TransactionId;
			}
		}

		// Token: 0x06001EC4 RID: 7876 RVA: 0x0002D86F File Offset: 0x0002BA6F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_TransactionId);
		}

		// Token: 0x04000DA3 RID: 3491
		private int m_ApiVersion;

		// Token: 0x04000DA4 RID: 3492
		private IntPtr m_LocalUserId;

		// Token: 0x04000DA5 RID: 3493
		private IntPtr m_TransactionId;
	}
}
