using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.ComponentInterfaces;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x020001FE RID: 510
	public class MultiplayerBattleInitializationModel : BattleInitializationModel
	{
		// Token: 0x06001C55 RID: 7253 RVA: 0x00062F2B File Offset: 0x0006112B
		public override List<FormationClass> GetAllAvailableTroopTypes()
		{
			return new List<FormationClass>();
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x00062F32 File Offset: 0x00061132
		protected override bool CanPlayerSideDeployWithOrderOfBattleAux()
		{
			return false;
		}
	}
}
