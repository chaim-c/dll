using System;
using System.Collections.Generic;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000337 RID: 823
	public interface IPrimarySiegeWeapon
	{
		// Token: 0x06002C56 RID: 11350
		bool HasCompletedAction();

		// Token: 0x1700082A RID: 2090
		// (get) Token: 0x06002C57 RID: 11351
		float SiegeWeaponPriority { get; }

		// Token: 0x1700082B RID: 2091
		// (get) Token: 0x06002C58 RID: 11352
		int OverTheWallNavMeshID { get; }

		// Token: 0x1700082C RID: 2092
		// (get) Token: 0x06002C59 RID: 11353
		bool HoldLadders { get; }

		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002C5A RID: 11354
		bool SendLadders { get; }

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x06002C5B RID: 11355
		MissionObject TargetCastlePosition { get; }

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x06002C5C RID: 11356
		FormationAI.BehaviorSide WeaponSide { get; }

		// Token: 0x06002C5D RID: 11357
		bool GetNavmeshFaceIds(out List<int> navmeshFaceIds);
	}
}
