using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000290 RID: 656
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryFileListOptionsInternal : ISettable<QueryFileListOptions>, IDisposable
	{
		// Token: 0x170004B1 RID: 1201
		// (set) Token: 0x060011D4 RID: 4564 RVA: 0x0001A424 File Offset: 0x00018624
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x0001A434 File Offset: 0x00018634
		public void Set(ref QueryFileListOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x0001A44C File Offset: 0x0001864C
		public void Set(ref QueryFileListOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x0001A482 File Offset: 0x00018682
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040007DC RID: 2012
		private int m_ApiVersion;

		// Token: 0x040007DD RID: 2013
		private IntPtr m_LocalUserId;
	}
}
