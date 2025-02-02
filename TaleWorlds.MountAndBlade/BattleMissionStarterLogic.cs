using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200026C RID: 620
	public class BattleMissionStarterLogic : MissionLogic
	{
		// Token: 0x060020D6 RID: 8406 RVA: 0x00075E6C File Offset: 0x0007406C
		public BattleMissionStarterLogic()
		{
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x00075E74 File Offset: 0x00074074
		public BattleMissionStarterLogic(IMissionTroopSupplier defenderTroopSupplier = null, IMissionTroopSupplier attackerTroopSupplier = null)
		{
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x00075E7C File Offset: 0x0007407C
		public override void AfterStart()
		{
			base.Mission.SetMissionMode(MissionMode.Battle, true);
		}
	}
}
