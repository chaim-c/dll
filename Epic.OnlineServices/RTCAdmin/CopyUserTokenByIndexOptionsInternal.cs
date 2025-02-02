using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.RTCAdmin
{
	// Token: 0x020001F9 RID: 505
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopyUserTokenByIndexOptionsInternal : ISettable<CopyUserTokenByIndexOptions>, IDisposable
	{
		// Token: 0x170003A8 RID: 936
		// (set) Token: 0x06000E47 RID: 3655 RVA: 0x000153FD File Offset: 0x000135FD
		public uint UserTokenIndex
		{
			set
			{
				this.m_UserTokenIndex = value;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (set) Token: 0x06000E48 RID: 3656 RVA: 0x00015407 File Offset: 0x00013607
		public uint QueryId
		{
			set
			{
				this.m_QueryId = value;
			}
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x00015411 File Offset: 0x00013611
		public void Set(ref CopyUserTokenByIndexOptions other)
		{
			this.m_ApiVersion = 2;
			this.UserTokenIndex = other.UserTokenIndex;
			this.QueryId = other.QueryId;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x00015438 File Offset: 0x00013638
		public void Set(ref CopyUserTokenByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 2;
				this.UserTokenIndex = other.Value.UserTokenIndex;
				this.QueryId = other.Value.QueryId;
			}
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x00015483 File Offset: 0x00013683
		public void Dispose()
		{
		}

		// Token: 0x0400066D RID: 1645
		private int m_ApiVersion;

		// Token: 0x0400066E RID: 1646
		private uint m_UserTokenIndex;

		// Token: 0x0400066F RID: 1647
		private uint m_QueryId;
	}
}
