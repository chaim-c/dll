using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000292 RID: 658
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileOptionsInternal : ISettable<QueryFileOptions>, IDisposable
	{
		// Token: 0x170004B4 RID: 1204
		// (set) Token: 0x060011DC RID: 4572 RVA: 0x0001A4B3 File Offset: 0x000186B3
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (set) Token: 0x060011DD RID: 4573 RVA: 0x0001A4C3 File Offset: 0x000186C3
		public Utf8String Filename
		{
			set
			{
				Helper.Set(value, ref this.m_Filename);
			}
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x0001A4D3 File Offset: 0x000186D3
		public void Set(ref QueryFileOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Filename = other.Filename;
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x0001A4F8 File Offset: 0x000186F8
		public void Set(ref QueryFileOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Filename = other.Value.Filename;
			}
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x0001A543 File Offset: 0x00018743
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
			Helper.Dispose(ref this.m_Filename);
		}

		// Token: 0x040007E0 RID: 2016
		private int m_ApiVersion;

		// Token: 0x040007E1 RID: 2017
		private IntPtr m_LocalUserId;

		// Token: 0x040007E2 RID: 2018
		private IntPtr m_Filename;
	}
}
