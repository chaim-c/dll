using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.PlayerDataStorage
{
	// Token: 0x02000274 RID: 628
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetFileMetadataCountOptionsInternal : ISettable<GetFileMetadataCountOptions>, IDisposable
	{
		// Token: 0x1700049F RID: 1183
		// (set) Token: 0x06001138 RID: 4408 RVA: 0x00019911 File Offset: 0x00017B11
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x06001139 RID: 4409 RVA: 0x00019921 File Offset: 0x00017B21
		public void Set(ref GetFileMetadataCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x0600113A RID: 4410 RVA: 0x00019938 File Offset: 0x00017B38
		public void Set(ref GetFileMetadataCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600113B RID: 4411 RVA: 0x0001996E File Offset: 0x00017B6E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x040007B4 RID: 1972
		private int m_ApiVersion;

		// Token: 0x040007B5 RID: 1973
		private IntPtr m_LocalUserId;
	}
}
