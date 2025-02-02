using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000266 RID: 614
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct DeleteCacheOptionsInternal : ISettable<DeleteCacheOptions>, IDisposable
	{
		// Token: 0x17000470 RID: 1136
		// (set) Token: 0x060010C3 RID: 4291 RVA: 0x00018DAB File Offset: 0x00016FAB
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x00018DBB File Offset: 0x00016FBB
		public void Set(ref DeleteCacheOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x00018DD4 File Offset: 0x00016FD4
		public void Set(ref DeleteCacheOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x00018E0A File Offset: 0x0001700A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000784 RID: 1924
		private int m_ApiVersion;

		// Token: 0x04000785 RID: 1925
		private IntPtr m_LocalUserId;
	}
}
