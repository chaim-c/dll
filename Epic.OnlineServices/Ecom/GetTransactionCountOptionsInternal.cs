using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004BF RID: 1215
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetTransactionCountOptionsInternal : ISettable<GetTransactionCountOptions>, IDisposable
	{
		// Token: 0x1700091F RID: 2335
		// (set) Token: 0x06001F52 RID: 8018 RVA: 0x0002EC73 File Offset: 0x0002CE73
		public EpicAccountId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001F53 RID: 8019 RVA: 0x0002EC83 File Offset: 0x0002CE83
		public void Set(ref GetTransactionCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x06001F54 RID: 8020 RVA: 0x0002EC9C File Offset: 0x0002CE9C
		public void Set(ref GetTransactionCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x06001F55 RID: 8021 RVA: 0x0002ECD2 File Offset: 0x0002CED2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000E10 RID: 3600
		private int m_ApiVersion;

		// Token: 0x04000E11 RID: 3601
		private IntPtr m_LocalUserId;
	}
}
