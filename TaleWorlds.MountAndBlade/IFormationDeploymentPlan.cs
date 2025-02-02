using System;
using TaleWorlds.Core;
using TaleWorlds.Engine;
using TaleWorlds.Library;

namespace TaleWorlds.MountAndBlade
{
	// Token: 0x0200020F RID: 527
	public interface IFormationDeploymentPlan
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x06001D0D RID: 7437
		FormationClass Class { get; }

		// Token: 0x170005C2 RID: 1474
		// (get) Token: 0x06001D0E RID: 7438
		FormationClass SpawnClass { get; }

		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x06001D0F RID: 7439
		float PlannedWidth { get; }

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x06001D10 RID: 7440
		float PlannedDepth { get; }

		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x06001D11 RID: 7441
		int PlannedTroopCount { get; }

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x06001D12 RID: 7442
		int PlannedFootTroopCount { get; }

		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x06001D13 RID: 7443
		int PlannedMountedTroopCount { get; }

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x06001D14 RID: 7444
		bool HasDimensions { get; }

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001D15 RID: 7445
		bool HasSignificantMountedTroops { get; }

		// Token: 0x06001D16 RID: 7446
		bool HasFrame();

		// Token: 0x06001D17 RID: 7447
		FormationDeploymentFlank GetDefaultFlank(bool spawnWithHorses, int formationTroopCount, int infantryCount);

		// Token: 0x06001D18 RID: 7448
		FormationDeploymentOrder GetFlankDeploymentOrder(int offset = 0);

		// Token: 0x06001D19 RID: 7449
		MatrixFrame GetGroundFrame();

		// Token: 0x06001D1A RID: 7450
		Vec3 GetGroundPosition();

		// Token: 0x06001D1B RID: 7451
		Vec2 GetDirection();

		// Token: 0x06001D1C RID: 7452
		WorldPosition CreateNewDeploymentWorldPosition(WorldPosition.WorldPositionEnforcedCache worldPositionEnforcedCache);
	}
}
