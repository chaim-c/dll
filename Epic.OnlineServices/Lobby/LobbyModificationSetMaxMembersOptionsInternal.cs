using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000396 RID: 918
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationSetMaxMembersOptionsInternal : ISettable<LobbyModificationSetMaxMembersOptions>, IDisposable
	{
		// Token: 0x170006F7 RID: 1783
		// (set) Token: 0x06001861 RID: 6241 RVA: 0x0002509A File Offset: 0x0002329A
		public uint MaxMembers
		{
			set
			{
				this.m_MaxMembers = value;
			}
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x000250A4 File Offset: 0x000232A4
		public void Set(ref LobbyModificationSetMaxMembersOptions other)
		{
			this.m_ApiVersion = 1;
			this.MaxMembers = other.MaxMembers;
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x000250BC File Offset: 0x000232BC
		public void Set(ref LobbyModificationSetMaxMembersOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.MaxMembers = other.Value.MaxMembers;
			}
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x000250F2 File Offset: 0x000232F2
		public void Dispose()
		{
		}

		// Token: 0x04000B27 RID: 2855
		private int m_ApiVersion;

		// Token: 0x04000B28 RID: 2856
		private uint m_MaxMembers;
	}
}
