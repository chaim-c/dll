using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatClient
{
	// Token: 0x02000640 RID: 1600
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct UnregisterPeerOptionsInternal : ISettable<UnregisterPeerOptions>, IDisposable
	{
		// Token: 0x17000C2B RID: 3115
		// (set) Token: 0x060028AE RID: 10414 RVA: 0x0003C8B3 File Offset: 0x0003AAB3
		public IntPtr PeerHandle
		{
			set
			{
				this.m_PeerHandle = value;
			}
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x0003C8BD File Offset: 0x0003AABD
		public void Set(ref UnregisterPeerOptions other)
		{
			this.m_ApiVersion = 1;
			this.PeerHandle = other.PeerHandle;
		}

		// Token: 0x060028B0 RID: 10416 RVA: 0x0003C8D4 File Offset: 0x0003AAD4
		public void Set(ref UnregisterPeerOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PeerHandle = other.Value.PeerHandle;
			}
		}

		// Token: 0x060028B1 RID: 10417 RVA: 0x0003C90A File Offset: 0x0003AB0A
		public void Dispose()
		{
			Helper.Dispose(ref this.m_PeerHandle);
		}

		// Token: 0x04001259 RID: 4697
		private int m_ApiVersion;

		// Token: 0x0400125A RID: 4698
		private IntPtr m_PeerHandle;
	}
}
