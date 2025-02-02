using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000260 RID: 608
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyFileMetadataAtIndexOptionsInternal : ISettable<CopyFileMetadataAtIndexOptions>, IDisposable
	{
		// Token: 0x17000462 RID: 1122
		// (set) Token: 0x060010A0 RID: 4256 RVA: 0x00018A64 File Offset: 0x00016C64
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x17000463 RID: 1123
		// (set) Token: 0x060010A1 RID: 4257 RVA: 0x00018A74 File Offset: 0x00016C74
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x00018A7E File Offset: 0x00016C7E
		public void Set(ref CopyFileMetadataAtIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Index = other.Index;
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x00018AA4 File Offset: 0x00016CA4
		public void Set(ref CopyFileMetadataAtIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Index = other.Value.Index;
			}
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x00018AEF File Offset: 0x00016CEF
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000775 RID: 1909
		private int m_ApiVersion;

		// Token: 0x04000776 RID: 1910
		private IntPtr m_LocalUserId;

		// Token: 0x04000777 RID: 1911
		private uint m_Index;
	}
}
