using System;
using System.Runtime.InteropServices;

namespace Epic.OnlineServices.AntiCheatCommon
{
	// Token: 0x020005F3 RID: 1523
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	internal struct LogPlayerSpawnOptionsInternal : ISettable<LogPlayerSpawnOptions>, IDisposable
	{
		// Token: 0x17000B74 RID: 2932
		// (set) Token: 0x060026C5 RID: 9925 RVA: 0x00039CB9 File Offset: 0x00037EB9
		public IntPtr SpawnedPlayerHandle
		{
			set
			{
				this.m_SpawnedPlayerHandle = value;
			}
		}

		// Token: 0x17000B75 RID: 2933
		// (set) Token: 0x060026C6 RID: 9926 RVA: 0x00039CC3 File Offset: 0x00037EC3
		public uint TeamId
		{
			set
			{
				this.m_TeamId = value;
			}
		}

		// Token: 0x17000B76 RID: 2934
		// (set) Token: 0x060026C7 RID: 9927 RVA: 0x00039CCD File Offset: 0x00037ECD
		public uint CharacterId
		{
			set
			{
				this.m_CharacterId = value;
			}
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x00039CD7 File Offset: 0x00037ED7
		public void Set(ref LogPlayerSpawnOptions other)
		{
			this.m_ApiVersion = 1;
			this.SpawnedPlayerHandle = other.SpawnedPlayerHandle;
			this.TeamId = other.TeamId;
			this.CharacterId = other.CharacterId;
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x00039D08 File Offset: 0x00037F08
		public void Set(ref LogPlayerSpawnOptions? other)
		{
			bool flag = other != null;
			if (flag)
			{
				this.m_ApiVersion = 1;
				this.SpawnedPlayerHandle = other.Value.SpawnedPlayerHandle;
				this.TeamId = other.Value.TeamId;
				this.CharacterId = other.Value.CharacterId;
			}
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x00039D68 File Offset: 0x00037F68
		public void Dispose()
		{
			Helper.Dispose(ref this.m_SpawnedPlayerHandle);
		}

		// Token: 0x04001161 RID: 4449
		private int m_ApiVersion;

		// Token: 0x04001162 RID: 4450
		private IntPtr m_SpawnedPlayerHandle;

		// Token: 0x04001163 RID: 4451
		private uint m_TeamId;

		// Token: 0x04001164 RID: 4452
		private uint m_CharacterId;
	}
}
