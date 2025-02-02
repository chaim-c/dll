using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002E9 RID: 745
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetRelayControlOptionsInternal : ISettable<SetRelayControlOptions>, IDisposable
	{
		// Token: 0x17000585 RID: 1413
		// (set) Token: 0x06001418 RID: 5144 RVA: 0x0001DCC3 File Offset: 0x0001BEC3
		public RelayControl RelayControl
		{
			set
			{
				this.m_RelayControl = value;
			}
		}

		// Token: 0x06001419 RID: 5145 RVA: 0x0001DCCD File Offset: 0x0001BECD
		public void Set(ref SetRelayControlOptions other)
		{
			this.m_ApiVersion = 1;
			this.RelayControl = other.RelayControl;
		}

		// Token: 0x0600141A RID: 5146 RVA: 0x0001DCE4 File Offset: 0x0001BEE4
		public void Set(ref SetRelayControlOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.RelayControl = other.Value.RelayControl;
			}
		}

		// Token: 0x0600141B RID: 5147 RVA: 0x0001DD1A File Offset: 0x0001BF1A
		public void Dispose()
		{
		}

		// Token: 0x04000905 RID: 2309
		private int m_ApiVersion;

		// Token: 0x04000906 RID: 2310
		private RelayControl m_RelayControl;
	}
}
