using System;
using TaleWorlds.InputSystem;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200035C RID: 860
	public static class DebugSiegeBehavior
	{
		// Token: 0x06002F28 RID: 12072 RVA: 0x000C123C File Offset: 0x000BF43C
		public static void SiegeDebug(UsableMachine usableMachine)
		{
			if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyAimAtRam"))
			{
				DebugSiegeBehavior.DebugDefendState = DebugSiegeBehavior.DebugStateDefender.DebugDefendersToRam;
			}
			else if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyAimAtSt"))
			{
				DebugSiegeBehavior.DebugDefendState = DebugSiegeBehavior.DebugStateDefender.DebugDefendersToTower;
			}
			else if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyAimAtBallistas2"))
			{
				DebugSiegeBehavior.DebugDefendState = DebugSiegeBehavior.DebugStateDefender.DebugDefendersToBallistae;
			}
			else if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyAimAtMangonels2"))
			{
				DebugSiegeBehavior.DebugDefendState = DebugSiegeBehavior.DebugStateDefender.DebugDefendersToMangonels;
			}
			else if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyAimAtNone2"))
			{
				DebugSiegeBehavior.DebugDefendState = DebugSiegeBehavior.DebugStateDefender.None;
			}
			else if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyAimAtBallistas"))
			{
				DebugSiegeBehavior.DebugAttackState = DebugSiegeBehavior.DebugStateAttacker.DebugAttackersToBallistae;
			}
			else if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyAimAtMangonels"))
			{
				DebugSiegeBehavior.DebugAttackState = DebugSiegeBehavior.DebugStateAttacker.DebugAttackersToMangonels;
			}
			else if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyAimAtBattlements"))
			{
				DebugSiegeBehavior.DebugAttackState = DebugSiegeBehavior.DebugStateAttacker.DebugAttackersToBattlements;
			}
			else if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyAimAtNone"))
			{
				DebugSiegeBehavior.DebugAttackState = DebugSiegeBehavior.DebugStateAttacker.None;
			}
			else if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyTargetDebugActive"))
			{
				DebugSiegeBehavior.ToggleTargetDebug = true;
			}
			else if (Input.DebugInput.IsHotKeyPressed("DebugSiegeBehaviorHotkeyTargetDebugDisactive"))
			{
				DebugSiegeBehavior.ToggleTargetDebug = false;
			}
			bool toggleTargetDebug = DebugSiegeBehavior.ToggleTargetDebug;
		}

		// Token: 0x040013E9 RID: 5097
		public static bool ToggleTargetDebug;

		// Token: 0x040013EA RID: 5098
		public static DebugSiegeBehavior.DebugStateAttacker DebugAttackState;

		// Token: 0x040013EB RID: 5099
		public static DebugSiegeBehavior.DebugStateDefender DebugDefendState;

		// Token: 0x02000615 RID: 1557
		public enum DebugStateAttacker
		{
			// Token: 0x04001F9E RID: 8094
			None,
			// Token: 0x04001F9F RID: 8095
			DebugAttackersToBallistae,
			// Token: 0x04001FA0 RID: 8096
			DebugAttackersToMangonels,
			// Token: 0x04001FA1 RID: 8097
			DebugAttackersToBattlements
		}

		// Token: 0x02000616 RID: 1558
		public enum DebugStateDefender
		{
			// Token: 0x04001FA3 RID: 8099
			None,
			// Token: 0x04001FA4 RID: 8100
			DebugDefendersToBallistae,
			// Token: 0x04001FA5 RID: 8101
			DebugDefendersToMangonels,
			// Token: 0x04001FA6 RID: 8102
			DebugDefendersToRam,
			// Token: 0x04001FA7 RID: 8103
			DebugDefendersToTower
		}
	}
}
