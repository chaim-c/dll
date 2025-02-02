using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004A9 RID: 1193
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyTransactionByIndexOptionsInternal : ISettable<CopyTransactionByIndexOptions>, IDisposable
	{
		// Token: 0x170008F6 RID: 2294
		// (set) Token: 0x06001EC9 RID: 7881 RVA: 0x0002D8AC File Offset: 0x0002BAAC
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170008F7 RID: 2295
		// (set) Token: 0x06001ECA RID: 7882 RVA: 0x0002D8BC File Offset: 0x0002BABC
		public uint TransactionIndex
		{
			set
			{
				this.m_TransactionIndex = value;
			}
		}

		// Token: 0x06001ECB RID: 7883 RVA: 0x0002D8C6 File Offset: 0x0002BAC6
		public void Set(ref CopyTransactionByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.TransactionIndex = other.TransactionIndex;
		}

		// Token: 0x06001ECC RID: 7884 RVA: 0x0002D8EC File Offset: 0x0002BAEC
		public void Set(ref CopyTransactionByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.TransactionIndex = other.Value.TransactionIndex;
			}
		}

		// Token: 0x06001ECD RID: 7885 RVA: 0x0002D937 File Offset: 0x0002BB37
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000DA8 RID: 3496
		private int m_ApiVersion;

		// Token: 0x04000DA9 RID: 3497
		private IntPtr m_LocalUserId;

		// Token: 0x04000DAA RID: 3498
		private uint m_TransactionIndex;
	}
}
