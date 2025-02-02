using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Metrics
{
	// Token: 0x02000311 RID: 785
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct EndPlayerSessionOptionsInternal : ISettable<EndPlayerSessionOptions>, IDisposable
	{
		// Token: 0x170005E0 RID: 1504
		// (set) Token: 0x06001528 RID: 5416 RVA: 0x0001F5E5 File Offset: 0x0001D7E5
		public EndPlayerSessionOptionsAccountId AccountId
		{
			set
			{
				Helper.Set<EndPlayerSessionOptionsAccountId, EndPlayerSessionOptionsAccountIdInternal>(ref value, ref this.m_AccountId);
			}
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x0001F5F6 File Offset: 0x0001D7F6
		public void Set(ref EndPlayerSessionOptions other)
		{
			this.m_ApiVersion = 1;
			this.AccountId = other.AccountId;
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x0001F610 File Offset: 0x0001D810
		public void Set(ref EndPlayerSessionOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.AccountId = other.Value.AccountId;
			}
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0001F646 File Offset: 0x0001D846
		public void Dispose()
		{
			Helper.Dispose<EndPlayerSessionOptionsAccountIdInternal>(ref this.m_AccountId);
		}

		// Token: 0x04000972 RID: 2418
		private int m_ApiVersion;

		// Token: 0x04000973 RID: 2419
		private EndPlayerSessionOptionsAccountIdInternal m_AccountId;
	}
}
