using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200020E RID: 526
	public struct FormationSceneSpawnEntry
	{
		// Token: 0x06001D0C RID: 7436 RVA: 0x000660E1 File Offset: 0x000642E1
		public FormationSceneSpawnEntry(FormationClass formationClass, GameEntity spawnEntity, GameEntity reinforcementSpawnEntity)
		{
			this.FormationClass = formationClass;
			this.SpawnEntity = spawnEntity;
			this.ReinforcementSpawnEntity = reinforcementSpawnEntity;
		}

		// Token: 0x04000968 RID: 2408
		public readonly FormationClass FormationClass;

		// Token: 0x04000969 RID: 2409
		public readonly GameEntity SpawnEntity;

		// Token: 0x0400096A RID: 2410
		public readonly GameEntity ReinforcementSpawnEntity;
	}
}
