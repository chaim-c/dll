using System;
using System.Collections.Generic;
using System.Linq;
using TaleWorlds.Engine;

namespace TaleWorlds.MountAndBlade.Source.Missions
{
	// Token: 0x020003AE RID: 942
	public class BattleSpawnLogic : MissionLogic
	{
		// Token: 0x060032B9 RID: 12985 RVA: 0x000D2517 File Offset: 0x000D0717
		public BattleSpawnLogic(string selectedSpawnPointSetTag)
		{
			this._selectedSpawnPointSetTag = selectedSpawnPointSetTag;
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x000D2528 File Offset: 0x000D0728
		public override void OnPreMissionTick(float dt)
		{
			if (this._isScenePrepared)
			{
				return;
			}
			GameEntity gameEntity = base.Mission.Scene.FindEntityWithTag(this._selectedSpawnPointSetTag);
			if (gameEntity != null)
			{
				List<GameEntity> list = base.Mission.Scene.FindEntitiesWithTag("spawnpoint_set").ToList<GameEntity>();
				list.Remove(gameEntity);
				foreach (GameEntity gameEntity2 in list)
				{
					gameEntity2.Remove(76);
				}
			}
			this._isScenePrepared = true;
		}

		// Token: 0x040015F5 RID: 5621
		public const string BattleTag = "battle_set";

		// Token: 0x040015F6 RID: 5622
		public const string SallyOutTag = "sally_out_set";

		// Token: 0x040015F7 RID: 5623
		public const string ReliefForceAttackTag = "relief_force_attack_set";

		// Token: 0x040015F8 RID: 5624
		private const string SpawnPointSetCommonTag = "spawnpoint_set";

		// Token: 0x040015F9 RID: 5625
		private readonly string _selectedSpawnPointSetTag;

		// Token: 0x040015FA RID: 5626
		private bool _isScenePrepared;
	}
}
