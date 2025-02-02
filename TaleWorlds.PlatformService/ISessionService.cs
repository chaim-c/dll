using System;

namespace TaleWorlds.PlatformService
{
	// Token: 0x0200000E RID: 14
	public interface ISessionService
	{
		// Token: 0x06000053 RID: 83
		void OnJoinJoinableSession(string connectionString);

		// Token: 0x06000054 RID: 84
		void OnLeaveJoinableSession();
	}
}
