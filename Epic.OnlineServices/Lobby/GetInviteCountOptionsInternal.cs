using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000347 RID: 839
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct GetInviteCountOptionsInternal : ISettable<GetInviteCountOptions>, IDisposable
	{
		// Token: 0x17000636 RID: 1590
		// (set) Token: 0x06001629 RID: 5673 RVA: 0x00020E73 File Offset: 0x0001F073
		public ProductUserId LocalUserId
		{
			set
			{
				Helper.Set(value, ref this.m_LocalUserId);
			}
		}

		// Token: 0x0600162A RID: 5674 RVA: 0x00020E83 File Offset: 0x0001F083
		public void Set(ref GetInviteCountOptions other)
		{
			this.m_ApiVersion = 1;
			this.LocalUserId = other.LocalUserId;
		}

		// Token: 0x0600162B RID: 5675 RVA: 0x00020E9C File Offset: 0x0001F09C
		public void Set(ref GetInviteCountOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.LocalUserId = other.Value.LocalUserId;
			}
		}

		// Token: 0x0600162C RID: 5676 RVA: 0x00020ED2 File Offset: 0x0001F0D2
		public void Dispose()
		{
			Helper.Dispose(ref this.m_LocalUserId);
		}

		// Token: 0x04000A0E RID: 2574
		private int m_ApiVersion;

		// Token: 0x04000A0F RID: 2575
		private IntPtr m_LocalUserId;
	}
}
