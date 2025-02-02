using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x02000150 RID: 336
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct SessionSearchSetMaxResultsOptionsInternal : ISettable<SessionSearchSetMaxResultsOptions>, IDisposable
	{
		// Token: 0x17000221 RID: 545
		// (set) Token: 0x060009AA RID: 2474 RVA: 0x0000E0E3 File Offset: 0x0000C2E3
		public uint MaxSearchResults
		{
			set
			{
				this.m_MaxSearchResults = value;
			}
		}

		// Token: 0x060009AB RID: 2475 RVA: 0x0000E0ED File Offset: 0x0000C2ED
		public void Set(ref SessionSearchSetMaxResultsOptions other)
		{
			this.m_ApiVersion = 1;
			this.MaxSearchResults = other.MaxSearchResults;
		}

		// Token: 0x060009AC RID: 2476 RVA: 0x0000E104 File Offset: 0x0000C304
		public void Set(ref SessionSearchSetMaxResultsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.MaxSearchResults = other.Value.MaxSearchResults;
			}
		}

		// Token: 0x060009AD RID: 2477 RVA: 0x0000E13A File Offset: 0x0000C33A
		public void Dispose()
		{
		}

		// Token: 0x0400046E RID: 1134
		private int m_ApiVersion;

		// Token: 0x0400046F RID: 1135
		private uint m_MaxSearchResults;
	}
}
