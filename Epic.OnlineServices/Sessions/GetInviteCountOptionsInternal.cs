using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000E6 RID: 230
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteCountOptionsInternal : ISettable<GetInviteCountOptions>, IDisposable
	{
		// Token: 0x1700018A RID: 394
		// (set) Token: 0x0600078A RID: 1930 RVA: 0x0000B63A File Offset: 0x0000983A
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x0600078B RID: 1931 RVA: 0x0000B64A File Offset: 0x0000984A
		public void Set(ref GetInviteCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x0600078C RID: 1932 RVA: 0x0000B664 File Offset: 0x00009864
		public void Set(ref GetInviteCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600078D RID: 1933 RVA: 0x0000B69A File Offset: 0x0000989A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000397 RID: 919
		private int m_ApiVersion;

		// Token: 0x04000398 RID: 920
		private IntPtr m_LocalUserId;
	}
}
