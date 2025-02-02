using System;
using System.Collections.Generic;
using TaleWorlds.Core;
using TaleWorlds.MountAndBlade.Missions;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000280 RID: 640
	public class MissionSiegeEnginesLogic : MissionLogic
	{
		// Token: 0x060021A6 RID: 8614 RVA: 0x0007AE49 File Offset: 0x00079049
		public MissionSiegeEnginesLogic(List<MissionSiegeWeapon> defenderSiegeWeapons, List<MissionSiegeWeapon> attackerSiegeWeapons)
		{
			this._defenderSiegeWeaponsController = new MissionSiegeWeaponsController(BattleSideEnum.Defender, defenderSiegeWeapons);
			this._attackerSiegeWeaponsController = new MissionSiegeWeaponsController(BattleSideEnum.Attacker, attackerSiegeWeapons);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0007AE6B File Offset: 0x0007906B
		public IMissionSiegeWeaponsController GetSiegeWeaponsController(BattleSideEnum side)
		{
			if (side == BattleSideEnum.Defender)
			{
				return this._defenderSiegeWeaponsController;
			}
			if (side == BattleSideEnum.Attacker)
			{
				return this._attackerSiegeWeaponsController;
			}
			return null;
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0007AE83 File Offset: 0x00079083
		public void GetMissionSiegeWeapons(out IEnumerable<IMissionSiegeWeapon> defenderSiegeWeapons, out IEnumerable<IMissionSiegeWeapon> attackerSiegeWeapons)
		{
			defenderSiegeWeapons = this._defenderSiegeWeaponsController.GetSiegeWeapons();
			attackerSiegeWeapons = this._attackerSiegeWeaponsController.GetSiegeWeapons();
		}

		// Token: 0x04000C80 RID: 3200
		private readonly MissionSiegeWeaponsController _defenderSiegeWeaponsController;

		// Token: 0x04000C81 RID: 3201
		private readonly MissionSiegeWeaponsController _attackerSiegeWeaponsController;
	}
}
