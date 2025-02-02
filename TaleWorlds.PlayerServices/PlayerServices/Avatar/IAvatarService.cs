using System;

namespace TaleWorlds.PlayerServices.Avatar
{
	// Token: 0x0200000C RID: 12
	public interface IAvatarService
	{
		// Token: 0x0600006B RID: 107
		AvatarData GetPlayerAvatar(PlayerId playerId);

		// Token: 0x0600006C RID: 108
		void Initialize();

		// Token: 0x0600006D RID: 109
		void ClearCache();

		// Token: 0x0600006E RID: 110
		bool IsInitialized();

		// Token: 0x0600006F RID: 111
		void Tick(float dt);
	}
}
