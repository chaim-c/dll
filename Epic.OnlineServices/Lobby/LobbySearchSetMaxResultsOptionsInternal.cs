using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x020003AA RID: 938
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbySearchSetMaxResultsOptionsInternal : ISettable<LobbySearchSetMaxResultsOptions>, IDisposable
	{
		// Token: 0x1700070A RID: 1802
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0002577A File Offset: 0x0002397A
		public uint MaxResults
		{
			set
			{
				this.m_MaxResults = value;
			}
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x00025784 File Offset: 0x00023984
		public void Set(ref LobbySearchSetMaxResultsOptions other)
		{
			this.m_ApiVersion = 1;
			this.MaxResults = other.MaxResults;
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0002579C File Offset: 0x0002399C
		public void Set(ref LobbySearchSetMaxResultsOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.MaxResults = other.Value.MaxResults;
			}
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x000257D2 File Offset: 0x000239D2
		public void Dispose()
		{
		}

		// Token: 0x04000B4C RID: 2892
		private int m_ApiVersion;

		// Token: 0x04000B4D RID: 2893
		private uint m_MaxResults;
	}
}
