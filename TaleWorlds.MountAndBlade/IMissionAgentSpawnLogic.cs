using System;
using TaleWorlds.Core;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x02000256 RID: 598
	public interface IMissionAgentSpawnLogic : IMissionBehavior
	{
		// Token: 0x06001FFC RID: 8188
		void StartSpawner(BattleSideEnum side);

		// Token: 0x06001FFD RID: 8189
		void StopSpawner(BattleSideEnum side);

		// Token: 0x06001FFE RID: 8190
		bool IsSideSpawnEnabled(BattleSideEnum side);

		// Token: 0x06001FFF RID: 8191
		bool IsSideDepleted(BattleSideEnum side);

		// Token: 0x06002000 RID: 8192
		float GetReinforcementInterval();
	}
}
