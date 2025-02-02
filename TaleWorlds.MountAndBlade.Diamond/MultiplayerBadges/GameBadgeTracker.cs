using System;

namespace TaleWorlds.MountAndBlade.Diamond.MultiplayerBadges
{
	// Token: 0x02000173 RID: 371
	public abstract class GameBadgeTracker
	{
		// Token: 0x06000A48 RID: 2632 RVA: 0x00010D82 File Offset: 0x0000EF82
		public virtual void OnPlayerJoin(PlayerData playerData)
		{
		}

		// Token: 0x06000A49 RID: 2633 RVA: 0x00010D84 File Offset: 0x0000EF84
		public virtual void OnKill(KillData killData)
		{
		}

		// Token: 0x06000A4A RID: 2634 RVA: 0x00010D86 File Offset: 0x0000EF86
		public virtual void OnStartingNextBattle()
		{
		}
	}
}
