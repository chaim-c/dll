using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x02000396 RID: 918
	public class MultiplayerSiegeTowerSpawner : SiegeTowerSpawner
	{
		// Token: 0x060031B8 RID: 12728 RVA: 0x000CD0B3 File Offset: 0x000CB2B3
		public override void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			base.AssignParameters(_spawnerMissionHelper);
			_spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<DestructableComponent>().MaxHitPoint = 15000f;
			SiegeTower firstScriptOfType = _spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<SiegeTower>();
			firstScriptOfType.MaxSpeed = 1f;
			firstScriptOfType.MinSpeed = 0.5f;
		}

		// Token: 0x0400155A RID: 5466
		private const float MaxHitPoint = 15000f;

		// Token: 0x0400155B RID: 5467
		private const float MinimumSpeed = 0.5f;

		// Token: 0x0400155C RID: 5468
		private const float MaximumSpeed = 1f;
	}
}
