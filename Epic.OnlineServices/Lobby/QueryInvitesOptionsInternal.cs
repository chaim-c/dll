using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003E4 RID: 996
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct QueryInvitesOptionsInternal : ISettable<QueryInvitesOptions>, IDisposable
	{
		// Token: 0x17000733 RID: 1843
		// (set) Token: 0x060019C1 RID: 6593 RVA: 0x00026147 File Offset: 0x00024347
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00026157 File Offset: 0x00024357
		public void Set(ref QueryInvitesOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00026170 File Offset: 0x00024370
		public void Set(ref QueryInvitesOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000261A6 File Offset: 0x000243A6
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000B77 RID: 2935
		private int m_ApiVersion;

		// Token: 0x04000B78 RID: 2936
		private IntPtr m_LocalUserId;
	}
}
