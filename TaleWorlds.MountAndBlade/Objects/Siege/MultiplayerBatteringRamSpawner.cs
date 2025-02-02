using System;

namespace TaleWorlds.MountAndBlade.Objects.Siege
{
	// Token: 0x02000391 RID: 913
	public class MultiplayerBatteringRamSpawner : BatteringRamSpawner
	{
		// Token: 0x060031AE RID: 12718 RVA: 0x000CCFF8 File Offset: 0x000CB1F8
		public override void AssignParameters(SpawnerEntityMissionHelper _spawnerMissionHelper)
		{
			base.AssignParameters(_spawnerMissionHelper);
			_spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<DestructableComponent>().MaxHitPoint = 12000f;
			BatteringRam firstScriptOfType = _spawnerMissionHelper.SpawnedEntity.GetFirstScriptOfType<BatteringRam>();
			firstScriptOfType.MaxSpeed *= 1f;
			firstScriptOfType.MinSpeed *= 1f;
		}

		// Token: 0x04001558 RID: 5464
		private const float MaxHitPoint = 12000f;

		// Token: 0x04001559 RID: 5465
		private const float SpeedMultiplier = 1f;
	}
}
