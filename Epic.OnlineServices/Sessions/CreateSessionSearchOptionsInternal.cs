using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Sessions
{
	// Token: 0x020000DA RID: 218
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct CreateSessionSearchOptionsInternal : ISettable<CreateSessionSearchOptions>, IDisposable
	{
		// Token: 0x17000178 RID: 376
		// (set) Token: 0x06000754 RID: 1876 RVA: 0x0000B1A7 File Offset: 0x000093A7
		public uint MaxSearchResults
		{
			set
			{
				this.m_MaxSearchResults = value;
			}
		}

		// Token: 0x06000755 RID: 1877 RVA: 0x0000B1B1 File Offset: 0x000093B1
		public void Set(ref CreateSessionSearchOptions other)
		{
			this.m_ApiVersion = 1;
			this.MaxSearchResults = other.MaxSearchResults;
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0000B1C8 File Offset: 0x000093C8
		public void Set(ref CreateSessionSearchOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.MaxSearchResults = other.Value.MaxSearchResults;
			}
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0000B1FE File Offset: 0x000093FE
		public void Dispose()
		{
		}

		// Token: 0x04000383 RID: 899
		private int m_ApiVersion;

		// Token: 0x04000384 RID: 900
		private uint m_MaxSearchResults;
	}
}
