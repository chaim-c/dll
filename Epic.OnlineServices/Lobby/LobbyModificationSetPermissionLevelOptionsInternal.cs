using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.Lobby
{
	// Token: 0x02000398 RID: 920
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LobbyModificationSetPermissionLevelOptionsInternal : ISettable<LobbyModificationSetPermissionLevelOptions>, IDisposable
	{
		// Token: 0x170006F9 RID: 1785
		// (set) Token: 0x06001867 RID: 6247 RVA: 0x00025106 File Offset: 0x00023306
		public LobbyPermissionLevel PermissionLevel
		{
			set
			{
				this.m_PermissionLevel = value;
			}
		}

		// Token: 0x06001868 RID: 6248 RVA: 0x00025110 File Offset: 0x00023310
		public void Set(ref LobbyModificationSetPermissionLevelOptions other)
		{
			this.m_ApiVersion = 1;
			this.PermissionLevel = other.PermissionLevel;
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x00025128 File Offset: 0x00023328
		public void Set(ref LobbyModificationSetPermissionLevelOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.PermissionLevel = other.Value.PermissionLevel;
			}
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0002515E File Offset: 0x0002335E
		public void Dispose()
		{
		}

		// Token: 0x04000B2A RID: 2858
		private int m_ApiVersion;

		// Token: 0x04000B2B RID: 2859
		private LobbyPermissionLevel m_PermissionLevel;
	}
}
