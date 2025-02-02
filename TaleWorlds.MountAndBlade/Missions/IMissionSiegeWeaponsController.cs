using System;
using System.Collections.Generic;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade.Missions
{
	// Token: 0x020003BE RID: 958
	public interface IMissionSiegeWeaponsController
	{
		// Token: 0x06003310 RID: 13072
		int GetMaxDeployableWeaponCount(Type t);

		// Token: 0x06003311 RID: 13073
		IEnumerable<IMissionSiegeWeapon> GetSiegeWeapons();

		// Token: 0x06003312 RID: 13074
		void OnWeaponDeployed(SiegeWeapon missionWeapon);

		// Token: 0x06003313 RID: 13075
		void OnWeaponUndeployed(SiegeWeapon missionWeapon);
	}
}
