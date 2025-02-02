using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Ecom
{
	// Token: 0x020004ED RID: 1261
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct TransactionCopyEntitlementByIndexOptionsInternal : ISettable<TransactionCopyEntitlementByIndexOptions>, IDisposable
	{
		// Token: 0x1700097D RID: 2429
		// (set) Token: 0x06002079 RID: 8313 RVA: 0x000303B8 File Offset: 0x0002E5B8
		public uint EntitlementIndex
		{
			set
			{
				this.m_EntitlementIndex = value;
			}
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x000303C2 File Offset: 0x0002E5C2
		public void Set(ref TransactionCopyEntitlementByIndexOptions other)
		{
			this.m_ApiVersion = 1;
			this.EntitlementIndex = other.EntitlementIndex;
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x000303DC File Offset: 0x0002E5DC
		public void Set(ref TransactionCopyEntitlementByIndexOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.EntitlementIndex = other.Value.EntitlementIndex;
			}
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x00030412 File Offset: 0x0002E612
		public void Dispose()
		{
		}

		// Token: 0x04000E7C RID: 3708
		private int m_ApiVersion;

		// Token: 0x04000E7D RID: 3709
		private uint m_EntitlementIndex;
	}
}
