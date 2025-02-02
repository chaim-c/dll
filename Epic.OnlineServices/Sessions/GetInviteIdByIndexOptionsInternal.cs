using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000E8 RID: 232
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteIdByIndexOptionsInternal : ISettable<GetInviteIdByIndexOptions>, IDisposable
	{
		// Token: 0x1700018D RID: 397
		// (set) Token: 0x06000792 RID: 1938 RVA: 0x0000B6CB File Offset: 0x000098CB
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700018E RID: 398
		// (set) Token: 0x06000793 RID: 1939 RVA: 0x0000B6DB File Offset: 0x000098DB
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0000B6E5 File Offset: 0x000098E5
		public void Set(ref GetInviteIdByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Index = other.Index;
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000B70C File Offset: 0x0000990C
		public void Set(ref GetInviteIdByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
				this.Index = other.Value.Index;
			}
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x0000B757 File Offset: 0x00009957
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x0400039B RID: 923
		private int m_ApiVersion;

		// Token: 0x0400039C RID: 924
		private IntPtr m_LocalUserId;

		// Token: 0x0400039D RID: 925
		private uint m_Index;
	}
}
