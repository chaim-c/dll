using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.P2P
{
	// Token: 0x020002E7 RID: 743
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SetPortRangeOptionsInternal : ISettable<SetPortRangeOptions>, IDisposable
	{
		// Token: 0x17000582 RID: 1410
		// (set) Token: 0x06001411 RID: 5137 RVA: 0x0001DC2C File Offset: 0x0001BE2C
		public ushort Port
		{
			set
			{
				this.m_Port = value;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (set) Token: 0x06001412 RID: 5138 RVA: 0x0001DC36 File Offset: 0x0001BE36
		public ushort MaxAdditionalPortsToTry
		{
			set
			{
				this.m_MaxAdditionalPortsToTry = value;
			}
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x0001DC40 File Offset: 0x0001BE40
		public void Set(ref SetPortRangeOptions other)
		{
			this.m_ApiVersion = 1;
			this.Port = other.Port;
			this.MaxAdditionalPortsToTry = other.MaxAdditionalPortsToTry;
		}

		// Token: 0x06001414 RID: 5140 RVA: 0x0001DC64 File Offset: 0x0001BE64
		public void Set(ref SetPortRangeOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.Port = other.Value.Port;
				this.MaxAdditionalPortsToTry = other.Value.MaxAdditionalPortsToTry;
			}
		}

		// Token: 0x06001415 RID: 5141 RVA: 0x0001DCAF File Offset: 0x0001BEAF
		public void Dispose()
		{
		}

		// Token: 0x04000901 RID: 2305
		private int m_ApiVersion;

		// Token: 0x04000902 RID: 2306
		private ushort m_Port;

		// Token: 0x04000903 RID: 2307
		private ushort m_MaxAdditionalPortsToTry;
	}
}
