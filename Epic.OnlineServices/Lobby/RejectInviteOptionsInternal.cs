using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003E8 RID: 1000
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct RejectInviteOptionsInternal : ISettable<RejectInviteOptions>, IDisposable
	{
		// Token: 0x1700073D RID: 1853
		// (set) Token: 0x060019DC RID: 6620 RVA: 0x000263A4 File Offset: 0x000245A4
		public Utf8String InviteId
		{
			set
			{
				Helper.Set(value, ref this.m_InviteId);
			}
		}

		// Token: 0x1700073E RID: 1854
		// (set) Token: 0x060019DD RID: 6621 RVA: 0x000263B4 File Offset: 0x000245B4
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x060019DE RID: 6622 RVA: 0x000263C4 File Offset: 0x000245C4
		public void Set(ref RejectInviteOptions other)
		{
			this.m_ApiVersion = 1;
			this.InviteId = other.InviteId;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x000263E8 File Offset: 0x000245E8
		public void Set(ref RejectInviteOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.InviteId = other.Value.InviteId;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00026433 File Offset: 0x00024633
		public void Dispose()
		{
			Helper.Dispose(ref this.m_InviteId);
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000B81 RID: 2945
		private int m_ApiVersion;

		// Token: 0x04000B82 RID: 2946
		private IntPtr m_InviteId;

		// Token: 0x04000B83 RID: 2947
		private IntPtr m_LocalUserId;
	}
}
