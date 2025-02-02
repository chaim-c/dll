using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000392 RID: 914
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationSetBucketIdOptionsInternal : ISettable<LobbyModificationSetBucketIdOptions>, IDisposable
	{
		// Token: 0x170006F3 RID: 1779
		// (set) Token: 0x06001855 RID: 6229 RVA: 0x00024FA6 File Offset: 0x000231A6
		public Utf8String BucketId
		{
			set
			{
				Helper.Set(value, ref this.m_BucketId);
			}
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x00024FB6 File Offset: 0x000231B6
		public void Set(ref LobbyModificationSetBucketIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.BucketId = other.BucketId;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x00024FD0 File Offset: 0x000231D0
		public void Set(ref LobbyModificationSetBucketIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.BucketId = other.Value.BucketId;
			}
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x00025006 File Offset: 0x00023206
		public void Dispose()
		{
			Helper.Dispose(ref this.m_BucketId);
		}

		// Token: 0x04000B21 RID: 2849
		private int m_ApiVersion;

		// Token: 0x04000B22 RID: 2850
		private IntPtr m_BucketId;
	}
}
