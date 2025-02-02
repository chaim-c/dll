using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000137 RID: 311
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionModificationSetBucketIdOptionsInternal : ISettable<SessionModificationSetBucketIdOptions>, IDisposable
	{
		// Token: 0x17000208 RID: 520
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x0000D89A File Offset: 0x0000BA9A
		public Utf8String BucketId
		{
			set
			{
				Helper.Set(value, ref this.m_BucketId);
			}
		}

		// Token: 0x0600094C RID: 2380 RVA: 0x0000D8AA File Offset: 0x0000BAAA
		public void Set(ref SessionModificationSetBucketIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.BucketId = other.BucketId;
		}

		// Token: 0x0600094D RID: 2381 RVA: 0x0000D8C4 File Offset: 0x0000BAC4
		public void Set(ref SessionModificationSetBucketIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.BucketId = other.Value.BucketId;
			}
		}

		// Token: 0x0600094E RID: 2382 RVA: 0x0000D8FA File Offset: 0x0000BAFA
		public void Dispose()
		{
			Helper.Dispose(ref this.m_BucketId);
		}

		// Token: 0x04000444 RID: 1092
		private int m_ApiVersion;

		// Token: 0x04000445 RID: 1093
		private IntPtr m_BucketId;
	}
}
