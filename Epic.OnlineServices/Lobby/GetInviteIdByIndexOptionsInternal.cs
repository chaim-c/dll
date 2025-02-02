using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000349 RID: 841
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteIdByIndexOptionsInternal : ISettable<GetInviteIdByIndexOptions>, IDisposable
	{
		// Token: 0x17000639 RID: 1593
		// (set) Token: 0x06001631 RID: 5681 RVA: 0x00020F03 File Offset: 0x0001F103
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x1700063A RID: 1594
		// (set) Token: 0x06001632 RID: 5682 RVA: 0x00020F13 File Offset: 0x0001F113
		public uint Index
		{
			set
			{
				this.m_Index = value;
			}
		}

		// Token: 0x06001633 RID: 5683 RVA: 0x00020F1D File Offset: 0x0001F11D
		public void Set(ref GetInviteIdByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
			this.Index = other.Index;
		}

		// Token: 0x06001634 RID: 5684 RVA: 0x00020F44 File Offset: 0x0001F144
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

		// Token: 0x06001635 RID: 5685 RVA: 0x00020F8F File Offset: 0x0001F18F
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000A12 RID: 2578
		private int m_ApiVersion;

		// Token: 0x04000A13 RID: 2579
		private IntPtr m_LocalUserId;

		// Token: 0x04000A14 RID: 2580
		private uint m_Index;
	}
}
