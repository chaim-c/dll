using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000D2 RID: 210
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CopySessionHandleByInviteIdOptionsInternal : ISettable<CopySessionHandleByInviteIdOptions>, IDisposable
	{
		// Token: 0x17000164 RID: 356
		// (set) Token: 0x0600072A RID: 1834 RVA: 0x0000ADFE File Offset: 0x00008FFE
		public Utf8String InviteId
		{
			set
			{
				Helper.Set(value, ref this.m_InviteId);
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000AE0E File Offset: 0x0000900E
		public void Set(ref CopySessionHandleByInviteIdOptions other)
		{
			this.m_ApiVersion = 1;
			this.InviteId = other.InviteId;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000AE28 File Offset: 0x00009028
		public void Set(ref CopySessionHandleByInviteIdOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.InviteId = other.Value.InviteId;
			}
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000AE5E File Offset: 0x0000905E
		public void Dispose()
		{
			Helper.Dispose(ref this.m_InviteId);
		}

		// Token: 0x0400036B RID: 875
		private int m_ApiVersion;

		// Token: 0x0400036C RID: 876
		private IntPtr m_InviteId;
	}
}
